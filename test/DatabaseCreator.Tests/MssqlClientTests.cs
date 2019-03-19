using System;
using Xunit;
using Xunit.Abstractions;

namespace DatabaseCreator.Tests
{
	public class MssqlClientTests
	{
		[Fact]
		public void can_get_connection_string() {
			var sqlClient = new MssqlClient();
			var spec = new DatabaseSpecification {
				MasterConnectionString = "Server=tcp:server1;User Id=sa;Password=BLANKED",
				Name = "customer1",
				Password = "random"
			};
			var connectionString = sqlClient.BuildConnectionString(spec);
			
			Assert.Equal("Data Source=tcp:server1;Initial Catalog=customer1;User ID=customer1;Password=random", connectionString);
		}
	}
}