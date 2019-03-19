using k8s;

namespace DatabaseCreator
{
	public static class KubernetesConfiguration
	{
		public static KubernetesClientConfiguration Get(CommandLineOptions options) {
			if (options.TestMode) {
				return new KubernetesClientConfiguration {
					Host = "http://127.0.0.1:8001"
				};
			}
			return KubernetesClientConfiguration.InClusterConfig();
		}
	}
}