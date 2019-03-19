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
			var masterConnectionString = secretClient.Read(options.MasterSecretName, options.Namespace);
			var spec = new DatabaseSpecification {
				MasterConnectionString = masterConnectionString,
				Name = options.CustomerName,
				Password = PasswordGenerator.GetRandomString(32)
			};
			sqlClient.CreateDatabase(spec);
			var customerConnectionString = sqlClient.BuildConnectionString(spec);
			secretClient.Create(string.Format(CustomerSecretTemplate, options.CustomerName), options.Namespace, 
				customerConnectionString);
		}
	}
}