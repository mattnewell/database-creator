using System.Data.SqlClient;

namespace DatabaseCreator
{
	public class MssqlClient : ISqlClient
	{
		public void CreateDatabase(DatabaseSpecification spec) {
			using (var connection = new SqlConnection(spec.MasterConnectionString)) {
				connection.Open();
				using (var command = new SqlCommand(MssqlCommandBuilder.BuildForMasterDatabase(spec), connection)) {
					command.ExecuteNonQuery();
				}
				connection.ChangeDatabase(spec.Name);
				using (var command = new SqlCommand(MssqlCommandBuilder.BuildForCustomerDatabase(spec), connection)) {
					command.ExecuteNonQuery();
				}
			}
		}

		public string BuildConnectionString(DatabaseSpecification spec) {
			var server = new SqlConnectionStringBuilder(spec.MasterConnectionString)["Server"];
			var builder = new SqlConnectionStringBuilder {
				["Server"] = server,
				["Database"] = spec.Name,
				["User Id"] = spec.Name,
				["Password"] = spec.Password
			};

			return builder.ToString();
		}
	}
}