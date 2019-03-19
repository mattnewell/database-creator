namespace DatabaseCreator
{
	public interface IDatabaseSecretClient
	{
		string Read(string name, string namespaceParameter);
		void Create(string name, string namespaceParameter, string stringValue);
	}
}