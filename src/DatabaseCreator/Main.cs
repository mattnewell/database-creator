using System;
using System.Runtime.CompilerServices;

namespace DatabaseCreator
{
	public class Main
	{
		public string CustomerSecretTemplate = "{0}-database-secret";
		private readonly CommandLineOptions options;
		private readonly IDatabaseSecretClient secretClient;
		private readonly ISqlClient sqlClient;

		public Main(CommandLineOptions options, IDatabaseSecretClient secretClient, ISqlClient sqlClient) {
			this.options = options;
			this.secretClient = secretClient;
			this.sqlClient = sqlClient;
		}

		public void Run() {
			//TODO: Validate options
			var customerSecretName = string.Format(CustomerSecretTemplate, options.CustomerName);
			try {
				secretClient.Read(customerSecretName, options.Namespace);
				Console.WriteLine("Existing secret found. Exit: Success");
				return;
			} catch {
				Console.WriteLine("No existing secret found. Continuing.");
			}
			var masterConnectionString = secretClient.Read(options.MasterSecretName, options.Namespace);
			var spec = new DatabaseSpecification {
				MasterConnectionString = masterConnectionString,
				Name = options.CustomerName,
				Password = PasswordGenerator.GetRandomString(32)
			};
			sqlClient.CreateDatabase(spec);
			Console.WriteLine($"Created database: {spec.Name}");
			var customerConnectionString = sqlClient.BuildConnectionString(spec);
			secretClient.Create(customerSecretName, options.Namespace, customerConnectionString);
			Console.WriteLine($"Created secret: {customerSecretName}");
		}
	}
}