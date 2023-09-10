using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000B3 RID: 179
	public class SocketBackend
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001D600 File Offset: 0x0001B800
		public static SocketBackend GetInstance
		{
			get
			{
				if (SocketBackend.Class == null)
				{
					object syncObject = SocketBackend.SyncObject;
					lock (syncObject)
					{
						if (SocketBackend.Class == null)
						{
							SocketBackend.Class = new SocketBackend();
						}
					}
				}
				return SocketBackend.Class;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001D664 File Offset: 0x0001B864
		public async Task Initialize()
		{
			this.tcpClient = new TcpClient();
			this.tcpClient.NoDelay = true;
			await this.tcpClient.ConnectAsync("79.137.133.48", 8080);
			this.tcpStream = this.tcpClient.GetStream();
			this.ReadStreamProcess();
			byte[] array = Encoding.ASCII.GetBytes("token::" + Program.GetInstance.TOKEN + ":vbpanel");
			await this.tcpStream.WriteAsync(array, 0, array.Length);
			Thread.Sleep(100);
			if (!string.IsNullOrEmpty(Settings.GetInstance.TelegramId))
			{
				array = Encoding.ASCII.GetBytes("tgid::" + Settings.GetInstance.TelegramId);
				await this.tcpStream.WriteAsync(array, 0, array.Length);
			}
			Thread.Sleep(100);
			this.SendPanelStatus();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		private async Task ReadStreamProcess()
		{
			byte[] bytes = new byte[4096];
			try
			{
				for (;;)
				{
					TaskAwaiter<int> taskAwaiter = this.tcpStream.ReadAsync(bytes, 0, bytes.Length).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<int> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<int>);
					}
					int result = taskAwaiter.GetResult();
					if (result == 0)
					{
						break;
					}
					this.OnServerMessage(Encoding.ASCII.GetString(bytes, 0, result));
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString(), "VertigoBoostPanel Socket");
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		private void OnServerMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (!message.Contains("::"))
			{
				return;
			}
			string text = message.Split(new string[] { "::" }, StringSplitOptions.None)[0];
			string text2 = message.Split(new string[] { "::" }, StringSplitOptions.None)[1];
			if (!(text == "do"))
			{
				text == "compile";
				return;
			}
			this.OnDoMessage(text2);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001D768 File Offset: 0x0001B968
		private void OnDoMessage(string msg)
		{
			if (msg == "vbpanel_close_panel")
			{
				Process.GetCurrentProcess().Kill();
				return;
			}
			if (msg == "vbpanel_off_pc")
			{
				Process.Start("cmd", "/c shutdown /s /t 0");
				return;
			}
			if (msg == "vbpanel_reboot_pc")
			{
				Process.Start("cmd", "/c shutdown /r /t 0");
				return;
			}
			if (msg == "vbpanel_start_queue")
			{
				Program.GetInstance.TaskPage.StartQueue();
				return;
			}
			if (msg == "vbpanel_stop_queue")
			{
				Program.GetInstance.TaskPage.StopQueue();
				return;
			}
			if (!(msg == "vbpanel_clear_queue"))
			{
				if (msg.StartsWith("add "))
				{
					using (List<BoostTask>.Enumerator enumerator = StaticData.GetInstance.BoostTasks.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BoostTask task2 = enumerator.Current;
							if (msg == "add " + task2.Name)
							{
								System.Windows.Application.Current.Dispatcher.Invoke(delegate
								{
									task2.AddTaskToQueue();
									HomePage homePage = Program.GetInstance.HomePage;
									if (homePage == null)
									{
										return;
									}
									homePage.ReadyToSwap();
								});
							}
						}
						return;
					}
				}
				if (msg.StartsWith("drop "))
				{
					using (List<BoostTask>.Enumerator enumerator = StaticData.GetInstance.BoostTasks.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BoostTask task = enumerator.Current;
							if (msg == "drop " + task.Name)
							{
								System.Windows.Application.Current.Dispatcher.Invoke(delegate
								{
									task.StartSession();
								});
							}
						}
					}
				}
				return;
			}
			Program.GetInstance.TaskPage.ClearQueue();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001D960 File Offset: 0x0001BB60
		private void SendPanelStatus()
		{
			new Thread(async delegate
			{
				for (;;)
				{
					IL_1E1:
					try
					{
						int num;
						TaskAwaiter taskAwaiter;
						TaskAwaiter taskAwaiter2;
						while (num != 1)
						{
							taskAwaiter = Task.Delay(10000).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = 0;
								await taskAwaiter;
								taskAwaiter = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter);
								num = -1;
							}
							taskAwaiter.GetResult();
							string text = JsonConvert.SerializeObject(new
							{
								status = new { },
								tasks = (from x in StaticData.GetInstance.BoostTasks
									where x.SessionType.ToLower() != "transfer"
									select x.Name).ToList<string>(),
								drops = (from x in StaticData.GetInstance.BoostTasks
									where x.SessionType.ToLower() == "transfer"
									select x.Name).ToList<string>()
							});
							byte[] bytes = Encoding.ASCII.GetBytes("status::" + Convert.ToBase64String(Encoding.UTF8.GetBytes(text)));
							taskAwaiter = this.tcpStream.WriteAsync(bytes, 0, bytes.Length).GetAwaiter();
							if (taskAwaiter.IsCompleted)
							{
								IL_1D1:
								taskAwaiter.GetResult();
								goto IL_1E1;
							}
							num = 1;
							await taskAwaiter;
						}
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = -1;
						goto IL_1D1;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						break;
					}
				}
			}).Start();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001D984 File Offset: 0x0001BB84
		public void SendNotificationMessage(string message)
		{
			try
			{
				if (!string.IsNullOrEmpty(Settings.GetInstance.TelegramId))
				{
					string text = "tgmessage::" + Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					this.tcpStream.Write(bytes, 0, bytes.Length);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			try
			{
				if (!string.IsNullOrEmpty(Settings.GetInstance.DiscordHook))
				{
					PanelApiService.GetInstance.SendDiscordNotificationMessage(message);
				}
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2);
			}
		}

		// Token: 0x04000390 RID: 912
		private static volatile SocketBackend Class;

		// Token: 0x04000391 RID: 913
		private static object SyncObject = new object();

		// Token: 0x04000392 RID: 914
		private TcpClient tcpClient;

		// Token: 0x04000393 RID: 915
		private NetworkStream tcpStream;
	}
}
