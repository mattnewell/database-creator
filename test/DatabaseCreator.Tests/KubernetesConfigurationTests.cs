using k8s.Exceptions;
using Xunit;

namespace DatabaseCreator.Tests
{
	public class KubernetesConfigurationTests
	{
		[Fact]
		public void test_sets_local_url() {
			var options = new CommandLineOptions {
				TestMode = true
			};
			var config = KubernetesConfiguration.Get(options);
			Assert.Equal("http://127.0.0.1:8001", config.Host);
		}
		
		[Fact]
		public void not_test_sets_incluster() {
			var options = new CommandLineOptions {
				TestMode = false
			};
			Assert.Throws<KubeConfigException>(() => KubernetesConfiguration.Get(options));
		}
	}
}
