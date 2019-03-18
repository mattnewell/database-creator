using CommandLine;

namespace database_creator
{
	public class CommandLineOptions
	{
		[Option('t', "test", Required = false)]
		public bool TestMode {
			get;
			set;
		}
	}
}