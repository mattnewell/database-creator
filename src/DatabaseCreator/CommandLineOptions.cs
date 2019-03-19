using CommandLine;

namespace DatabaseCreator
{
	public class CommandLineOptions
	{
		public const string DefaultMasterSecretName = "master-database-string";
		
		[Option('t', "test", Required = false)]
		public bool TestMode {
			get;
			set;
		}
		
		[Option('c', "customer", Required = true)]
		public string CustomerName {
			get;
			set;
		}

		[Option('n', "namespace", Required = true)]
		public string Namespace {
			get;
			set;
		}
		
		[Option('m', "master", Required = false, Default = DefaultMasterSecretName)]
		public string MasterSecretName {
			get;
			set;
		}
	}
}