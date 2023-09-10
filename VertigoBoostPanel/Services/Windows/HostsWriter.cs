using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace VertigoBoostPanel.Services.Windows
{
	// Token: 0x02000085 RID: 133
	public class HostsWriter
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00017D08 File Offset: 0x00015F08
		public static HostsWriter GetInstance
		{
			get
			{
				if (HostsWriter.Class == null)
				{
					object syncObject = HostsWriter.SyncObject;
					lock (syncObject)
					{
						if (HostsWriter.Class == null)
						{
							HostsWriter.Class = new HostsWriter();
						}
					}
				}
				return HostsWriter.Class;
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00017D6C File Offset: 0x00015F6C
		public void AddTextToHostsFile(string text)
		{
			string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");
			FileSecurity accessControl = File.GetAccessControl(text2);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			accessControl.AddAccessRule(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
			File.SetAccessControl(text2, accessControl);
			new FileInfo(text2).IsReadOnly = false;
			using (StreamWriter streamWriter = File.AppendText(text2))
			{
				streamWriter.WriteLine();
				streamWriter.WriteLine(text);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00017DF8 File Offset: 0x00015FF8
		public void DeleteTextFromHosts(string text)
		{
			string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");
			FileSecurity accessControl = File.GetAccessControl(text2);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			accessControl.AddAccessRule(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
			File.SetAccessControl(text2, accessControl);
			string text3;
			using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					text3 = streamReader.ReadToEnd();
				}
			}
			text3 = text3.Replace(text, "");
			using (FileStream fileStream2 = new FileStream(text2, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream2))
				{
					streamWriter.Write(text3);
				}
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00017EFC File Offset: 0x000160FC
		public bool IsHostsFileHasText(string text)
		{
			string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");
			FileSecurity accessControl = File.GetAccessControl(text2);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			accessControl.AddAccessRule(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
			File.SetAccessControl(text2, accessControl);
			string text3;
			using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					text3 = streamReader.ReadToEnd();
				}
			}
			return text3.Contains(text);
		}

		// Token: 0x040002FA RID: 762
		private static volatile HostsWriter Class;

		// Token: 0x040002FB RID: 763
		private static object SyncObject = new object();
	}
}
