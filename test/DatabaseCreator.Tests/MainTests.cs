using Moq;
using Xunit;

namespace DatabaseCreator.Tests
{
	public class MainTests
	{
		private const string Customer = "customer1";
		private const string NamespaceValue = "default";
		private const string MasterConnectionString = "Connection=String";

		[Fact]
		public void creates_database_from_options() {
			var sqlClient = new Mock<ISqlClient>();
			var secretClient = new Mock<IDatabaseSecretClient>();
			var main = SetupMain(sqlClient, secretClient);

			main.Run();

			sqlClient.Verify(x => x.CreateDatabase(new DatabaseSpecification {
				MasterConnectionString = MasterConnectionString,
				Name = Customer,
				Password = "password1"
			}));
		}
		
		private static Main SetupMain(Mock<ISqlClient> sqlClient, Mock<IDatabaseSecretClient> secretClient) {
			var options = new CommandLineOptions {
				MasterSecretName = CommandLineOptions.DefaultMasterSecretName,
				CustomerName = Customer,
				Namespace = NamespaceValue
			};

			secretClient.Setup(x => x.Read(CommandLineOptions.DefaultMasterSecretName, NamespaceValue)).Returns(MasterConnectionString);
			var main = new Main(options, secretClient.Object, sqlClient.Object);
			return main;
		}

		[Fact]
		public void creates_customer_secret() {
			const string customerSecretName = Customer + "-database-secret";
			var sqlClient = new Mock<ISqlClient>();
			var secretClient = new Mock<IDatabaseSecretClient>();
			var main = SetupMain(sqlClient, secretClient);

			main.Run();
			
			sqlClient.Verify(x => x.BuildConnectionString(It.IsAny<DatabaseSpecification>()));
			secretClient.Verify(x => x.Create(customerSecretName, NamespaceValue, It.IsAny<string>()));
		}
	}
}
