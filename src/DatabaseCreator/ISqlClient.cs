namespace DatabaseCreator
{
	public interface ISqlClient
	{
		void CreateDatabase(DatabaseSpecification spec);

		string BuildConnectionString(DatabaseSpecification spec);
	}
}