using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VertigoBoostPanel.Services.Crypto
{
	// Token: 0x02000094 RID: 148
	public static class Hardware
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0001A1D8 File Offset: 0x000183D8
		public static void GenerateHWID()
		{
			if (string.IsNullOrEmpty(Hardware.HWID))
			{
				try
				{
					Task[] array = new Task[3];
					array[0] = new Task(delegate
					{
						Hardware._cpuId = Hardware.cpuId();
					});
					array[1] = new Task(delegate
					{
						Hardware._biosId = Hardware.biosId();
					});
					array[2] = new Task(delegate
					{
						Hardware._baseId = Hardware.baseId();
					});
					Task[] array2 = array;
					Task[] array3 = array2;
					for (int i = 0; i < array3.Length; i++)
					{
						array3[i].Start();
					}
					array3 = array2;
					for (int i = 0; i < array3.Length; i++)
					{
						array3[i].Wait();
					}
					Hardware.HWID = Hardware.GetHash(string.Concat(new string[]
					{
						"CPU >> ",
						Hardware._cpuId,
						"\nBIOS >> ",
						Hardware._biosId,
						"\nBASE >> ",
						Hardware._baseId
					}));
				}
				catch
				{
					Hardware.HWID = "error";
				}
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001A324 File Offset: 0x00018524
		private static string GetHash(string s)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes = new ASCIIEncoding().GetBytes(s);
			return Hardware.GetHexString(hashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001A350 File Offset: 0x00018550
		private static string GetHexString(object bt)
		{
			string text = string.Empty;
			for (int i = 0; i < bt.Length; i++)
			{
				object obj = bt[i];
				int num = obj & 15;
				int num2 = (obj >> 4) & 15;
				text = ((num2 <= 9) ? (text + num2.ToString()) : (text + ((char)(num2 - 10 + 65)).ToString()));
				text = ((num <= 9) ? (text + num.ToString()) : (text + ((char)(num - 10 + 65)).ToString()));
				if (i + 1 != bt.Length && (i + 1) % 2 == 0)
				{
					text += "-";
				}
			}
			return text;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001A410 File Offset: 0x00018610
		private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
		{
			string text = "";
			foreach (ManagementBaseObject managementBaseObject in new ManagementClass(wmiClass).GetInstances())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				if (managementObject[wmiMustBeTrue].ToString() == "True" && text == "")
				{
					try
					{
						text = managementObject[wmiProperty].ToString();
						return text;
					}
					catch
					{
					}
				}
			}
			return text;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001A4C0 File Offset: 0x000186C0
		private static string identifier(string wmiClass, string wmiProperty)
		{
			string text = "";
			foreach (ManagementBaseObject managementBaseObject in new ManagementClass(wmiClass).GetInstances())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				if (text == "")
				{
					try
					{
						text = managementObject[wmiProperty].ToString();
						return text;
					}
					catch
					{
					}
				}
			}
			return text;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001A558 File Offset: 0x00018758
		private static string cpuId()
		{
			string text = Hardware.identifier("Win32_Processor", "UniqueId");
			if (text == "")
			{
				text = Hardware.identifier("Win32_Processor", "ProcessorId");
				if (text == "")
				{
					text = Hardware.identifier("Win32_Processor", "Name");
					if (text == "")
					{
						text = Hardware.identifier("Win32_Processor", "Manufacturer");
					}
					text += Hardware.identifier("Win32_Processor", "MaxClockSpeed");
				}
			}
			return text;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001A5F0 File Offset: 0x000187F0
		private static string biosId()
		{
			return string.Concat(new string[]
			{
				Hardware.identifier("Win32_BIOS", "Manufacturer"),
				Hardware.identifier("Win32_BIOS", "SMBIOSBIOSVersion"),
				Hardware.identifier("Win32_BIOS", "IdentificationCode"),
				Hardware.identifier("Win32_BIOS", "SerialNumber"),
				Hardware.identifier("Win32_BIOS", "ReleaseDate"),
				Hardware.identifier("Win32_BIOS", "Version")
			});
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001A674 File Offset: 0x00018874
		private static string baseId()
		{
			return Hardware.identifier("Win32_BaseBoard", "Model") + Hardware.identifier("Win32_BaseBoard", "Manufacturer") + Hardware.identifier("Win32_BaseBoard", "Name") + Hardware.identifier("Win32_BaseBoard", "SerialNumber");
		}

		// Token: 0x04000321 RID: 801
		public static string HWID;

		// Token: 0x04000322 RID: 802
		private static string _cpuId;

		// Token: 0x04000323 RID: 803
		private static string _biosId;

		// Token: 0x04000324 RID: 804
		private static string _baseId;
	}
}
