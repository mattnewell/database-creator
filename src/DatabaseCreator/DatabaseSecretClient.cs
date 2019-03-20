using System;
using System.Collections.Generic;
using System.Text;
using k8s;
using k8s.Models;

namespace DatabaseCreator
{
	public class DatabaseSecretClient : IDatabaseSecretClient
	{
		private const string ConnectionStringKey = "connectionString";

		private readonly IKubernetes client;

		public DatabaseSecretClient(IKubernetes client) {
			this.client = client;
		}

		public string Read(string name, string namespaceParameter) {
			try {
				var secret = client.ReadNamespacedSecret(name, namespaceParameter);
				var bytes = secret.Data["connectionString"];
				return Encoding.UTF8.GetString(bytes);
			} catch (Exception ex) {
				Console.WriteLine($"Secret not found. name: {name}, namespace: {namespaceParameter}");
				throw;
			}
		}

		public void Create(string name, string namespaceParameter, string stringValue) {
			var customerSecret = new V1Secret {
				Metadata = new V1ObjectMeta {
					Name = name
				},
				StringData = new Dictionary<string, string>() {
					[ConnectionStringKey] = stringValue
					
				}
			};
			client.CreateNamespacedSecret(customerSecret, namespaceParameter);
		}
	}
}