using System;
using System.IO;
using System.Text;

namespace blokowe
{
	public class BlockCIpherImplementation
	{
		public BlockCIpherImplementation()
		{

		}

		public static string messageToAsciiString(string message)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(message);
			string converted = null;
			for (int i = 0; i < bytes.Length; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					converted += (bytes[i] & 0x80) > 0 ? "1" : "0";
					bytes[i] <<= 1;
				}
			}

			return converted;
		}

		public static string convertAsciiStringToAlphabet(string asciiMessage)
		{
			var result = "";
			for (int i = 0; i < asciiMessage.Length; i += 8)
			{
				var oneSign = asciiMessage.Substring(i, 8);
				var intValue = Convert.ToInt32(oneSign, 2);
				result += (char)intValue;
			}
			return result;
		}

		public static string EcbEncrypt(string message)
		{
			var asciiMessage = messageToAsciiString(message);
			//Console.WriteLine($"message: {message}, input ascii: {asciiMessage}");
			var result = "";

			for (int i = 0; i < asciiMessage.Length; i = i + blockLength)
			{
				result += xor(key, asciiMessage.Substring(i, blockLength));
			}
			//Console.WriteLine($"result: {result}");
			return convertAsciiStringToAlphabet(result);
		}

		public static string xor(string inputKey, string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				var singleBlock = "";
				for (int i = 0; i < blockLength; i++)
				{
					if (message[i] == '0' && inputKey[i] == '0' || (message[i] == '1' && inputKey[i] == '1'))
						singleBlock += "0";
					else
						singleBlock += "1";
				}
				//Console.WriteLine($"result {singleBlock}");
				return singleBlock;
			}

			throw new Exception("null");
		}

		public static string CbcEncrypt(string message)
		{
			var asciiMessage = messageToAsciiString(message);
			//Console.WriteLine($"message: {message}, input ascii: {asciiMessage}");
			var result = "";

			for (int i = 0; i < asciiMessage.Length; i = i + blockLength)
			{
				if (i == 0)
				{
					var temp2 = xor(IV, asciiMessage.Substring(i, blockLength));
					result += xor(key, temp2);
					continue;
				}
				var temp = xor(result.Substring(i - 8, blockLength), asciiMessage.Substring(i, blockLength));
				result += xor(key, temp);
			}
			//Console.WriteLine($"result: {result}");
			return convertAsciiStringToAlphabet(result);
		}

		public static string CbcDecrypt(string message)
		{
			var asciiMessage = messageToAsciiString(message);
			//Console.WriteLine($"ascii: {asciiMessage}");
			var result = "";

			for (int i = asciiMessage.Length; i > 0; i = i - blockLength)
			{
				if (i == 8)
				{
					result = xor(IV, xor(key, asciiMessage.Substring(i - 8, blockLength))) + result;
					continue;
				}
				var temp = xor(asciiMessage.Substring(i - 16, blockLength),
					xor(key, asciiMessage.Substring(i - 8, blockLength)));
				result = temp + result;
			}
			//Console.WriteLine($"result: {result}");
			return convertAsciiStringToAlphabet(result);
		}

		private static int blockLength = 8;
		public static string IV = "00100101";
		private static string key = "01101000";
	}
}
