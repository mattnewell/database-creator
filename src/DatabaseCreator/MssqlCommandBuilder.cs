using System.Text;

namespace DatabaseCreator
{
	public static class MssqlCommandBuilder
	{
		public static string BuildForMasterDatabase(DatabaseSpecification spec) {
			var sb = new StringBuilder();
			sb.AppendLine($"CREATE DATABASE {spec.Name}");
			sb.AppendLine(
				$"CREATE LOGIN [{spec.Name}] WITH PASSWORD=N'{spec.Password}', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF");
			return sb.ToString();
		}
		
		public static string BuildForCustomerDatabase(DatabaseSpecification spec) {
			var sb = new StringBuilder();
			sb.AppendLine($"CREATE USER [{spec.Name}] FOR LOGIN [{spec.Name}]");
			sb.AppendLine($"ALTER ROLE [db_owner] ADD MEMBER [{spec.Name}]");
			return sb.ToString();
		}
	}
}