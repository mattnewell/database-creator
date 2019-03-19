//Source: https://stackoverflow.com/questions/54991/generating-random-passwords
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DatabaseCreator
{
	public static class PasswordGenerator
	{
		public static string GetRandomString(int length) {
			const string alphanumericCharacters =
				"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
				"abcdefghijklmnopqrstuvwxyz" +
				"0123456789" +
				"!@#$%^&*()_+";
			return GetRandomString(length, alphanumericCharacters);
		}

		public static string GetRandomString(int length, IEnumerable<char> characterSet) {
			if (length < 0)
				throw new ArgumentException("length must not be negative", nameof(length));
			if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
				throw new ArgumentException("length is too big", nameof(length));
			if (characterSet == null)
				throw new ArgumentNullException(nameof(characterSet));
			var characterArray = characterSet.Distinct().ToArray();
			if (characterArray.Length == 0)
				throw new ArgumentException("characterSet must not be empty", nameof(characterSet));

			var bytes = new byte[length * 8];
			new RNGCryptoServiceProvider().GetBytes(bytes);
			var result = new char[length];
			for (var i = 0; i < length; i++) {
				var value = BitConverter.ToUInt64(bytes, i * 8);
				result[i] = characterArray[value % (uint) characterArray.Length];
			}
			return new string(result);
		}
	}
}
