using System;
using System.Collections.Generic;
using CommandLine;
using k8s;
using k8s.Models;
using static System.Text.Encoding;

namespace database_creator
{
	class Program
	{
		static void Main(string[] args) {
			var options = Parser.Default.ParseArguments<CommandLineOptions>(args)
				.WithParsed<CommandLineOptions>(Run)
				.WithNotParsed<CommandLineOptions>(Error);
//			var config = KubernetesClientConfiguration.InClusterConfig();
////			var config = new KubernetesClientConfiguration {  Host = "http://127.0.0.1:8001" };
//			var client = new Kubernetes(config);
//			Console.WriteLine("Starting database creation");
////			var masterSecret = client.ReadNamespacedSecret("master-go-db-string", "default");
////			var bytes = masterSecret.Data["connectionString"];
////			Console.WriteLine(ASCII.GetString(bytes));
//
//			var customerSecretName = "customer1-connection-string";
//			try {
//				client.DeleteNamespacedSecret(new V1DeleteOptions(), customerSecretName, "default");
//			} catch (Exception ex) {
//				Console.WriteLine("Did not delete existing secret");
//			}
//			
//			//Create secret
//			var customerSecret = new V1Secret {
//				Metadata = new V1ObjectMeta {
//					Name = customerSecretName
//				},
//				StringData = new Dictionary<string, string>() {
//					["connectionString"] = args[0]
//					
//				}
//			};
//			client.CreateNamespacedSecret(customerSecret, "default");
		}

		private static void Run(CommandLineOptions options) {
			throw new NotImplementedException();
		}
		
		private static void Error(IEnumerable<Error> errors) {
			Environment.Exit(1);
		}
	}
}