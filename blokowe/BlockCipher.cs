using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

public class BlockCipher 
{
	public BlockCipher(CipherMode mode)
	{
		this.mode = mode;
	}

	public string EncryptString(string message)
	{
		var byteKey = Encoding.UTF8.GetBytes(key);
		var byteMessage = Encoding.UTF8.GetBytes(message);
		byte[] array;

		var ecbBlock = new AesManaged()
		{
			Key = byteKey,
			BlockSize = 128,
			Mode = mode,
			IV = new byte[16]
		};

		var encryptor = ecbBlock.CreateEncryptor(ecbBlock.Key, ecbBlock.IV);
		using var memoryStream = new MemoryStream();
		using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
		{
			using (var streamWriter = new StreamWriter(cryptoStream))
			{
				streamWriter.Write(message);
			}

			array = memoryStream.ToArray();
		}

		return Convert.ToBase64String(array); ;
	}

	public string DecryptString(string message)
	{
		var byteKey = Encoding.UTF8.GetBytes(key);
		var byteMessage = Convert.FromBase64String(message);

		var ecbBlock = new AesManaged()
		{
			//KeySize = 128,
			Key = byteKey,
			//BlockSize = 128,
			Mode = mode,
			//Padding = PaddingMode.Zeros,
			IV = new byte[16]
		};

		var decryptor = ecbBlock.CreateDecryptor(ecbBlock.Key, ecbBlock.IV);
		using var memoryStream = new MemoryStream(byteMessage);
		using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
		using var streamReader = new StreamReader(cryptoStream);
		return streamReader.ReadToEnd();
	}

	private static string key = "0000000000000000";
	private CipherMode mode;
}
