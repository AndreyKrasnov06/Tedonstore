using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace VertigoBoostPanel.Services.Crypto
{
	// Token: 0x02000096 RID: 150
	public static class RSAManager
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0001A6DC File Offset: 0x000188DC
		public static ValueTuple<string, string> smethod_0()
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(2048);
			RSAParameters rsaparameters = rsacryptoServiceProvider.ExportParameters(true);
			RSAParameters rsaparameters2 = rsacryptoServiceProvider.ExportParameters(false);
			StringWriter stringWriter = new StringWriter();
			new XmlSerializer(typeof(RSAParameters)).Serialize(stringWriter, rsaparameters2);
			string text = stringWriter.ToString();
			StringWriter stringWriter2 = new StringWriter();
			new XmlSerializer(typeof(RSAParameters)).Serialize(stringWriter2, rsaparameters);
			string text2 = stringWriter2.ToString();
			return new ValueTuple<string, string>(text, text2);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001A75C File Offset: 0x0001895C
		public static string CryptByPublicKeyString(string data, string publicKeyString)
		{
			StringReader stringReader = new StringReader(publicKeyString);
			RSAParameters rsaparameters = (RSAParameters)new XmlSerializer(typeof(RSAParameters)).Deserialize(stringReader);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			byte[] bytes = Encoding.Unicode.GetBytes(data);
			return Convert.ToBase64String(rsacryptoServiceProvider.Encrypt(bytes, false));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public static string DecryptByPrivateKeyString(string data, string privateKeyString)
		{
			StringReader stringReader = new StringReader(privateKeyString);
			RSAParameters rsaparameters = (RSAParameters)new XmlSerializer(typeof(RSAParameters)).Deserialize(stringReader);
			byte[] array = Convert.FromBase64String(data);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			byte[] array2 = rsacryptoServiceProvider.Decrypt(array, false);
			return Encoding.Unicode.GetString(array2);
		}
	}
}
