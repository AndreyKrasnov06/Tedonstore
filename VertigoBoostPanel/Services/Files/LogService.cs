using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VertigoBoostPanel.Services.Api;
using VertigoBoostPanel.Services.Crypto;
using VertigoBoostPanel.Services.RegistryInteraction;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services.Files
{
	// Token: 0x0200009B RID: 155
	public class LogService
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001AC34 File Offset: 0x00018E34
		public static LogService GetInstance
		{
			get
			{
				if (LogService.Class == null)
				{
					object syncObject = LogService.SyncObject;
					lock (syncObject)
					{
						if (LogService.Class == null)
						{
							LogService.Class = new LogService();
						}
					}
				}
				return LogService.Class;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001AC98 File Offset: 0x00018E98
		public void LogInformation(string message)
		{
			try
			{
				object fileLockObject = LogService._fileLockObject;
				lock (fileLockObject)
				{
					if (!Directory.Exists("data"))
					{
						Directory.CreateDirectory("data");
					}
					using (StreamWriter streamWriter = File.AppendText("data\\crashlog.txt"))
					{
						streamWriter.WriteLine(string.Format("[{0:dd.MM.yyyy HH:mm:ss}] {1}", DateTime.Now, message));
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to write crashlog:\n\n" + ex.ToString(), "VertigoBoostPanel", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001AD64 File Offset: 0x00018F64
		public void startVersionControl()
		{
			new Thread(async delegate
			{
				for (;;)
				{
					TaskAwaiter<bool> taskAwaiter = LogService.checkRequest().GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						break;
					}
					Thread.Sleep(1000);
				}
				LogService.CurrentOffset = DateTime.Now.Ticks;
				this.LogInformation("Panel started.");
			}).Start();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001AD88 File Offset: 0x00018F88
		public static async Task<bool> checkRequest()
		{
			bool flag;
			if (Settings.GetInstance.PanelOffset > 0L)
			{
				TaskAwaiter<string> taskAwaiter = new RequestManager(null).POST("https://panel.tedonstore.com/vb/login.php?h=" + Hardware.HWID, new Dictionary<string, string>
				{
					{
						"login",
						RegistryService.Login
					},
					{
						"password",
						RegistryService.Password
					}
				}, "application/json").GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<string> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<string>);
				}
				string result = taskAwaiter.GetResult();
				TaskAwaiter taskAwaiter3 = StaticData.GetInstance.RegisterNewSubscriber(RSAEncryption.GetInstance.DecryptByPrivateKeyString(result)).GetAwaiter();
				if (!taskAwaiter3.IsCompleted)
				{
					await taskAwaiter3;
					TaskAwaiter taskAwaiter4;
					taskAwaiter3 = taskAwaiter4;
					taskAwaiter4 = default(TaskAwaiter);
				}
				taskAwaiter3.GetResult();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000333 RID: 819
		private static volatile LogService Class;

		// Token: 0x04000334 RID: 820
		private static object SyncObject = new object();

		// Token: 0x04000335 RID: 821
		private static object _fileLockObject = new object();

		// Token: 0x04000336 RID: 822
		public static long CurrentOffset = 0L;
	}
}
