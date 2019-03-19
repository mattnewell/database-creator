using System;
using System.Collections.Generic;
using CommandLine;
using k8s;
using k8s.Models;
using static System.Text.Encoding;

namespace DatabaseCreator
{
	class Program
	{
		static void Main(string[] args) {
			Parser.Default.ParseArguments<CommandLineOptions>(args)
				.WithParsed<CommandLineOptions>(options => {
					var sqlClient = new MssqlClient();
					var kubernetesConfig = KubernetesConfiguration.Get(options);
					var kubernetesClient = new Kubernetes(kubernetesConfig);
					var secretClient = new DatabaseSecretClient(kubernetesClient);
					new Main(options, secretClient, sqlClient).Run();
				})
				.WithNotParsed<CommandLineOptions>(errors => Environment.Exit(1));
		}
	}
}