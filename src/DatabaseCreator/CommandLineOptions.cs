using CommandLine;

namespace DatabaseCreator
{
	public class CommandLineOptions
	{
		public const string DefaultMasterSecretName = "master-database-string";

		[Option('t', "test", Required = false,
			HelpText = "Assume requests to Kubernetes API are proxied via http://127.0.0.1:8001")]
		public bool TestMode {
			get;
			set;
		}

		[Option('c', "customer", Required = true,
			HelpText = "The name of the SkySync customer. Will be used for database name and login.")]
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
