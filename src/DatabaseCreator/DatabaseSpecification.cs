using Org.BouncyCastle.Asn1.Esf;

namespace DatabaseCreator
{
	public class DatabaseSpecification
	{
		public string MasterConnectionString {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public string Password {
			get;
			set;
		}

		public override bool Equals(object other) {
			return other is DatabaseSpecification value
			       && MasterConnectionString == value.MasterConnectionString
			       && Name == value.Name;
		}
	}
}