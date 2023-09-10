using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace VertigoBoostPanel.Services.Windows
{
	// Token: 0x02000086 RID: 134
	public class NetworkAdapterWrapper
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00017FBC File Offset: 0x000161BC
		public static NetworkAdapterWrapper GetInstance
		{
			get
			{
				if (NetworkAdapterWrapper.Class == null)
				{
					object syncObject = NetworkAdapterWrapper.SyncObject;
					lock (syncObject)
					{
						if (NetworkAdapterWrapper.Class == null)
						{
							NetworkAdapterWrapper.Class = new NetworkAdapterWrapper();
						}
					}
				}
				return NetworkAdapterWrapper.Class;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00018020 File Offset: 0x00016220
		public bool CreateAdapter()
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				Arguments = "install " + Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\INF\\Netloop.inf *MSLOOP",
				FileName = "lib\\devcon\\devcon.exe",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};
			process.StartInfo = processStartInfo;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			text = text.Replace("\r\n", "");
			return !string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(Regex.Match(text, "Drivers installed successfully").Value);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000180C8 File Offset: 0x000162C8
		public bool RemoveAdapters()
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				Arguments = "remove =net *msloop*",
				FileName = "lib\\devcon\\devcon.exe",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};
			process.StartInfo = processStartInfo;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			text = text.Replace("\r\n", "");
			return !string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(Regex.Match(text, "device\\(s\\) were removed").Value);
		}

		// Token: 0x040002FC RID: 764
		private static volatile NetworkAdapterWrapper Class;

		// Token: 0x040002FD RID: 765
		private static object SyncObject = new object();
	}
}
