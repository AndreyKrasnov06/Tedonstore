using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.UI.windows
{
	// Token: 0x02000077 RID: 119
	public partial class SteamInstallation : Window
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00016D90 File Offset: 0x00014F90
		public SteamInstallation()
		{
			this.InitializeComponent();
			base.DataContext = this;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00016DB0 File Offset: 0x00014FB0
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Run(async delegate
			{
				int num;
				TaskAwaiter taskAwaiter;
				TaskAwaiter taskAwaiter2;
				while (num != 1)
				{
					try
					{
						Directory.Delete("temp", true);
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
					}
					if (!Directory.Exists("temp"))
					{
						Directory.CreateDirectory("temp");
					}
					taskAwaiter = Task.Delay(500).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
					}
					taskAwaiter.GetResult();
					taskAwaiter = this.DownloadFile().GetAwaiter();
					if (taskAwaiter.IsCompleted)
					{
						IL_110:
						taskAwaiter.GetResult();
						return;
					}
					await taskAwaiter;
				}
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter);
				goto IL_110;
			});
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00016DD0 File Offset: 0x00014FD0
		private async Task DownloadFile()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFileCompleted += this.Wc_DownloadFileCompleted;
				webClient.DownloadProgressChanged += this.Wc_DownloadProgressChanged;
				webClient.DownloadFileAsync(new Uri("https://panel.tedonstore.com/download/steam.old.zip"), Directory.GetCurrentDirectory() + "\\temp\\steam.old.zip");
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00016E14 File Offset: 0x00015014
		private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "\\temp\\steam.old.zip", Directory.GetCurrentDirectory() + "\\temp\\");
			File.Delete(Directory.GetCurrentDirectory() + "\\temp\\steam.old.zip");
			this.InstallSteam();
			try
			{
				Directory.Delete("temp", true);
			}
			catch (Exception ex)
			{
				LogService.GetInstance.LogInformation(ex.ToString());
			}
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.ProgressBar.Value = 100.0;
			});
			Thread.Sleep(500);
			Application.Current.Dispatcher.Invoke(delegate
			{
				Program.GetInstance.MainWindow.Show();
				base.Close();
			});
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00016ED0 File Offset: 0x000150D0
		private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.ProgressBar.Value = (double)(e.ProgressPercentage - 5);
			});
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00016F10 File Offset: 0x00015110
		private void InstallSteam()
		{
			string text = Directory.GetCurrentDirectory() + "\\temp\\steam.old\\";
			int num = 0;
			foreach (string text2 in Directory.GetFiles(text, "*", SearchOption.AllDirectories))
			{
				try
				{
					File.Copy(text2, text2.Replace(text, Settings.GetInstance.SteamPath), true);
				}
				catch
				{
					num++;
				}
			}
			Console.WriteLine(string.Format("Success with errors {0}.", num));
		}
	}
}
