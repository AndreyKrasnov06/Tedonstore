using System;
using Microsoft.Win32;

namespace VertigoBoostPanel.Services.RegistryInteraction
{
	// Token: 0x02000084 RID: 132
	public static class RegistryService
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00017AE8 File Offset: 0x00015CE8
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00017B54 File Offset: 0x00015D54
		public static string Login
		{
			get
			{
				string text2;
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
				{
					string text = ((registryKey == null) ? null : registryKey.GetValue("Login")) as string;
					if (text == null)
					{
						text2 = "";
					}
					else
					{
						text2 = text;
					}
				}
				return text2;
			}
			set
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
				{
					registryKey.SetValue("Login", value, RegistryValueKind.String);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00017BA0 File Offset: 0x00015DA0
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00017C0C File Offset: 0x00015E0C
		public static string Password
		{
			get
			{
				string text2;
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
				{
					string text = ((registryKey != null) ? registryKey.GetValue("Password") : null) as string;
					if (text == null)
					{
						text2 = "";
					}
					else
					{
						text2 = text;
					}
				}
				return text2;
			}
			set
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
				{
					registryKey.SetValue("Password", value, RegistryValueKind.String);
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00017C58 File Offset: 0x00015E58
		public static void setUniversalKey(string key, string value)
		{
			using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
			{
				registryKey.SetValue(key, value, RegistryValueKind.String);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00017CA0 File Offset: 0x00015EA0
		public static string getUniversalKey(string key)
		{
			string text2;
			using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TedonSoftware"))
			{
				string text = ((registryKey != null) ? registryKey.GetValue(key) : null) as string;
				if (text == null)
				{
					text2 = "";
				}
				else
				{
					text2 = text;
				}
			}
			return text2;
		}
	}
}
