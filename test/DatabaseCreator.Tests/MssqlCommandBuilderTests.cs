using System.Text;
using Xunit;

namespace DatabaseCreator.Tests
{
	public class MssqlCommandBuilderTests
	{
		private const string Customer = "customer1";
		private const string NamespaceValue = "default";

		[Fact]
		public void can_build_for_master() {
			var spec = new DatabaseSpecification {
				Name = "customer1",
				Password = "random"
			};
			var expected = new StringBuilder();
			expected.AppendLine("CREATE DATABASE customer1");
			expected.AppendLine(
				"CREATE LOGIN [customer1] WITH PASSWORD=N'random', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF");
			Assert.Equal(expected.ToString(), MssqlCommandBuilder.BuildForMasterDatabase(spec));
		}
		
		[Fact]
		public void can_build_for_customer() {
			var spec = new DatabaseSpecification {
				Name = "customer1",
				Password = "random"
			};
			var expected = new StringBuilder();
			expected.AppendLine("CREATE USER [customer1] FOR LOGIN [customer1]");
			expected.AppendLine("ALTER ROLE [db_owner] ADD MEMBER [customer1]");
			Assert.Equal(expected.ToString(), MssqlCommandBuilder.BuildForCustomerDatabase(spec));
		}
	}
}
