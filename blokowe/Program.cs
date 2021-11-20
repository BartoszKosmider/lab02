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
				Console.WriteLine("1 - ecb, 2 - cbc, 3 - cfb, 4 - ecb moje, 5 - cbc moje");
				var inputValue = Console.ReadLine();
				switch (inputValue)
				{
					case "1":
					{
						Start(CipherMode.ECB);
						break;
					}
					case "2":
					{
						Start(CipherMode.CBC);
						break;
					}
					case "3":
					{
						Start(CipherMode.CFB);
						break;
					}
					case "4":
					{
						EcbImplementation();
						break;
					}
					case "5":
					{
						CbcImplementation();
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

		private static void Start(CipherMode cipherMode)
		{
			var block = new BlockCipher(cipherMode);
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			Console.WriteLine($"input value: {input}, {input.Length}");
			var encryptedMessage = block.EncryptString(input);
			Console.WriteLine($"encrypted message: {encryptedMessage}, {encryptedMessage.Length}");
			var decryptedMessage = block.DecryptString(encryptedMessage);
			Console.WriteLine($"decrypted message: {decryptedMessage}, {decryptedMessage.Length}");
		}

		private static void EcbImplementation()
		{
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			if (string.IsNullOrEmpty(input))
			{
				Console.WriteLine("not provided value");
				return;
			}
			var encrypted = BlockCIpherImplementation.EcbEncrypt(input);
			Console.WriteLine($"decrypted: {encrypted}");
			var decrypted = BlockCIpherImplementation.EcbEncrypt(encrypted);
			Console.WriteLine($"decrypted: {decrypted}");
		}

		private static void CbcImplementation()
		{
			Console.WriteLine("input value to encrypt:");
			var input = Console.ReadLine();
			if (string.IsNullOrEmpty(input))
			{
				Console.WriteLine("not provided value");
				return;
			}
			var encrypted = BlockCIpherImplementation.CbcEncrypt(input);
			Console.WriteLine($"encrypted: {encrypted}");
			var decrypted = BlockCIpherImplementation.CbcDecrypt(encrypted);
			Console.WriteLine($"decrypted: {decrypted}");
		}
	}
}
