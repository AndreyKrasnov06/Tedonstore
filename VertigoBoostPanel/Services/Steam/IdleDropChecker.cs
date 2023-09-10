using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services.Steam
{
	// Token: 0x02000088 RID: 136
	internal class IdleDropChecker
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00018B80 File Offset: 0x00016D80
		public static IdleDropChecker GetInstance
		{
			get
			{
				if (IdleDropChecker.Class == null)
				{
					object syncObject = IdleDropChecker.SyncObject;
					lock (syncObject)
					{
						if (IdleDropChecker.Class == null)
						{
							IdleDropChecker.Class = new IdleDropChecker();
						}
					}
				}
				return IdleDropChecker.Class;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00018BE4 File Offset: 0x00016DE4
		public void RunService()
		{
			this._worker = new BackgroundWorker();
			this._worker.RunWorkerAsync();
			this._worker.WorkerSupportsCancellation = true;
			this._worker.DoWork += this.WorkerDoWork;
			this._worker.RunWorkerCompleted += this.WorkerCompleted;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00018C44 File Offset: 0x00016E44
		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			Thread.Sleep(1000);
			if (string.IsNullOrEmpty(Settings.GetInstance.IDLEPath))
			{
				return;
			}
			List<string> list = Settings.GetInstance.IDLEPath.Split(new char[] { '\\' }).ToList<string>();
			list[list.Count - 1] = "csgo\\addons\\sourcemod\\logs\\DropsSummoner.log";
			string text = string.Join("\\", list);
			if (File.Exists(text))
			{
				using (FileStream fileStream = new FileStream(text, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.Write(string.Empty);
					}
				}
				try
				{
					using (FileStream fileStream2 = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						using (StreamReader streamReader = new StreamReader(fileStream2, Encoding.UTF8))
						{
							for (;;)
							{
								Thread.Sleep(1);
								string text2;
								if ((text2 = streamReader.ReadLine()) != null)
								{
									text2 = text2.Replace("\n", string.Empty).Replace("\r", string.Empty);
									if (text2.Length != 0)
									{
										Match match = Regex.Match(text2, ".*Игроку (.*)<\\w*><STEAM_\\w:\\w:(\\w*)><> выпало .(\\d*)-\\d*-\\d*-\\d*.");
										if (!string.IsNullOrEmpty(match.Groups[0].Value))
										{
											string value = match.Groups[1].Value;
											ValueTuple<string, string, string> caseInfo = CaseInfo.GetCaseInfo(match.Groups[3].Value);
											using (WebClient webClient = new WebClient())
											{
												webClient.Encoding = Encoding.UTF8;
												webClient.Headers.Clear();
												webClient.Headers.Add(HttpRequestHeader.UserAgent, "VBP");
												webClient.QueryString.Add("token", Program.GetInstance.TOKEN);
												webClient.QueryString.Add("name", caseInfo.Item1);
												webClient.QueryString.Add("price", caseInfo.Item3);
												webClient.QueryString.Add("image", caseInfo.Item2);
												webClient.QueryString.Add("account_login", value);
												webClient.QueryString.Add("telegram_id", Settings.GetInstance.TelegramId);
												webClient.QueryString.Add("discord_hook", Settings.GetInstance.DiscordHook);
												webClient.UploadValues("https://panel.tedonstore.com/casefarm/stat.php", webClient.QueryString);
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				return;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00018F5C File Offset: 0x0001715C
		private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._worker.RunWorkerAsync();
		}

		// Token: 0x040002FF RID: 767
		private static object Class;

		// Token: 0x04000300 RID: 768
		private static object SyncObject = new object();

		// Token: 0x04000301 RID: 769
		private BackgroundWorker _worker;
	}
}
