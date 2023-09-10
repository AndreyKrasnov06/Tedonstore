using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000B2 RID: 178
	public class RSAEncryption
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001D520 File Offset: 0x0001B720
		public static RSAEncryption GetInstance
		{
			get
			{
				if (RSAEncryption.Class == null)
				{
					object syncObject = RSAEncryption.SyncObject;
					lock (syncObject)
					{
						if (RSAEncryption.Class == null)
						{
							RSAEncryption.Class = new RSAEncryption();
						}
					}
				}
				return RSAEncryption.Class;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private string Key
		{
			get
			{
				return "<RSAParameters><Exponent>AQAB</Exponent><Modulus>ynbR8hEI/6P52IkU0856er+2ik3I9Q1dDim/ONNAG4WIJ6feeVu69oQYOWMdebdu8P04jRa+EgZ2t5+vnasqu6U9opnor2g4LZiQ154DoEaWPGLyQbL0LHBC99C5taHbj5pLQmLRs/QpOuzeZ0KSDD9xVZh81bin+TDRZTMxmOfHkviKeCMs7FgATxGolD3gO0/5/LoNEvnRwdyRjiw1H64K3/whp6j3U8SjlQaTBuTwf4dA3l273HQeYAtnKw5mxsgR4VH4UEC/IkRb/xgfM6jThdQ0IruDvX0yRuUL0fRxhr1KcDlVLaTMTkYXKoFHhruszFoUWlj5iy8zWAGvlQ==</Modulus><P>9Ds3MnCCRV2Iftcwjijy/zESe/23DEsiMqKLSGp84a/uWej8a1uYfVl4GSW6BR+iyhxaPRhS7YGLCsAdJiWvmg+Au5+FHFSXl22fnXHBqEKDx/Z2b0LuuWwLOzfdRUMWHCA8nqofeDZtTnluqFu7toMXZgSsFDL+tf4qr4tOTks=</P><Q>1DhfrkIjpOvj7C5vO0JoilHx7GRGKbSgKBMqv1wU0W8VtdG5iOd4vGr9UYxBoc8jqD89en113rYIeHuilSmind8FmxYAGMS5fhrp5WPiKVE3B8vssI1UruriVuIaAFqI3+oaQrnoiz88yeQgAOzekDmcnlzcS38LDf0+FWjnzZ8=</Q><DP>yEWqnJzv5kutuz1gnqJrqOGno69IpDcUQutRRb+0yijHTkkkfDCvGdL9DboHEV6A0jVvtfu1PPdmehlRjc+HcNDXooNL1xBzAoStR6FZyU1J3PZ45gG/2qMDjrrfooVBoyy4KO+eQKh498nR+RLIGgDjdg/Mv/+VKM3UWQDl5IM=</DP><DQ>tk5TS5Q/oI6P9rWREjwjKCfrBmLBr1QefS6x+z90FtIxuUaX9ta57kqOoL43J3SVMhRZkN80IYUI08+JxE/HBY2v+CE3Jg/hNUaThV5/y3ScLJNvGd/fToAyveZv5bvgd1JZCtc/wFcrtlfFcmYJb6Y+OACXsjdJ3NOewK+k6gs=</DQ><InverseQ>6WJTHfIxZ71ozsagYkLP5dHK2mDl0DZ/Vjhtr2xARowR3PgxWIfx2dIoVEjjbL2JoqPfab7CoAy4M9Y+qSv6/RO/DCLksKmSD8EKt1JBclQpvD9ui/sF3hPcugGuhGLs6w22kbyExYSMIwz5s0JHGKAlj1iCDj1QF2Bf8Szj+4g=</InverseQ><D>MzrjntPOqhkM1EzDd2DMvxiZkN9j1RX/kODhYPibRRuBuz8P3Tdn1I7SLls1DyJqMRJx58l0aM035vjCECxKkNacBPPa29+ML4VmSRjTpA+YXklPMW6duEzjtR8cjqgZDXomRvMISveiIyXilNBUckLvY5BFLjMeVcCTi37rstuntYIZ2j36yYzA5qeopwi2lsWQX04fQd6MAggSXu5qrM5WHTVnsQdwUL3kEBcobGXy1xb6buI4zUiEGSgIoX45gEEJomiakOEN0jdkUcf7CFfKnf6T/NYlYMtA5X145K8nHkYvmIU2P05xj5xqcnnoSjar1/OeOeRpEitMrXXKtQ==</D></RSAParameters>";
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001D584 File Offset: 0x0001B784
		public string DecryptByPrivateKeyString(string data)
		{
			StringReader stringReader = new StringReader(this.Key);
			RSAParameters rsaparameters = (RSAParameters)new XmlSerializer(typeof(RSAParameters)).Deserialize(stringReader);
			byte[] array = Convert.FromBase64String(data);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			byte[] array2 = rsacryptoServiceProvider.Decrypt(array, true);
			return Encoding.UTF8.GetString(array2);
		}

		// Token: 0x0400038E RID: 910
		private static volatile RSAEncryption Class;

		// Token: 0x0400038F RID: 911
		private static object SyncObject = new object();
	}
}
