using System;
using System.Security.Cryptography;

namespace blokowe
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("1 - ecb, 2 - cbc, 3 - cfb");
				var inputValue = Console.ReadLine();
				switch (inputValue)
				{
					case "1":
					{
						Ecb();
						break;
					}
					case "2":
					{
						Cbc();
						break;
					}
					case "3":
					{
						Cfb();
						break;
					}
					default:
					{
						Console.WriteLine("invalid data provided");
						break;
					}
				}
			}
		}

		private static void Ecb()
		{
			var ecb = new BlockCipher(CipherMode.ECB);
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			Console.WriteLine($"input value: {input}, {input.Length}");
			var encryptedMessage = ecb.EncryptString(key, input);
			Console.WriteLine($"encrypted message: {encryptedMessage}, {encryptedMessage.Length}");
			var decryptedMessage = ecb.DecryptString(key, encryptedMessage);
			Console.WriteLine($"decrypted message: {decryptedMessage}, {decryptedMessage.Length}");
		}

		private static void Cbc()
		{
			var cbc = new BlockCipher(CipherMode.CBC);
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			Console.WriteLine($"input value: {input}, {input.Length}");
			var encryptedMessage = cbc.EncryptString(key, input);
			Console.WriteLine($"encrypted message: {encryptedMessage}, {encryptedMessage.Length}");
			var decryptedMessage = cbc.DecryptString(key, encryptedMessage);
			Console.WriteLine($"decrypted message: {decryptedMessage}, {decryptedMessage.Length}");
		}

		private static void Cfb()
		{
			var cfb = new BlockCipher(CipherMode.CFB);
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			Console.WriteLine($"input value: {input}, {input.Length}");
			var encryptedMessage = cfb.EncryptString(key, input);
			Console.WriteLine($"encrypted message: {encryptedMessage}, {encryptedMessage.Length}");
			var decryptedMessage = cfb.DecryptString(key, encryptedMessage);
			Console.WriteLine($"decrypted message: {decryptedMessage}, {decryptedMessage.Length}");
		}

		private static string key = "0000000000000000";
	}
}
