using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using VertigoBoostPanel.Pages;
using VertigoBoostPanel.Properties;
using VertigoBoostPanel.Services.Crypto;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel
{
	// Token: 0x02000005 RID: 5
	public partial class App : Application
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000054F8 File Offset: 0x000036F8
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00005538 File Offset: 0x00003738
		public static event EventHandler LanguageChanged;

		// Token: 0x06000018 RID: 24 RVA: 0x00005578 File Offset: 0x00003778
		private void App_LanguageChanged(object sender, EventArgs e)
		{
			VertigoBoostPanel.Properties.Settings.Default.DefaultLanguage = App.Language;
			VertigoBoostPanel.Properties.Settings.Default.Save();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000031BE File Offset: 0x000013BE
		public static List<CultureInfo> Languages
		{
			get
			{
				return App.m_Languages;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000031C5 File Offset: 0x000013C5
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000055A0 File Offset: 0x000037A0
		public static CultureInfo Language
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Language");
				}
				if (value == Thread.CurrentThread.CurrentUICulture)
				{
					return;
				}
				Thread.CurrentThread.CurrentUICulture = value;
				ResourceDictionary resourceDictionary = new ResourceDictionary();
				resourceDictionary.Source = new Uri(string.Format("Resources/languages/lang.{0}.xaml", value.Name), UriKind.Relative);
				ResourceDictionary resourceDictionary2 = Application.Current.Resources.MergedDictionaries.Where((ResourceDictionary d) => d.Source != null && d.Source.OriginalString.StartsWith("Resources/languages/lang.")).FirstOrDefault<ResourceDictionary>();
				if (resourceDictionary2 == null)
				{
					Console.WriteLine("here " + resourceDictionary.Source.OriginalString);
					Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
				}
				else
				{
					int num = Application.Current.Resources.MergedDictionaries.IndexOf(resourceDictionary2);
					Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary2);
					Application.Current.Resources.MergedDictionaries.Insert(num, resourceDictionary);
				}
				App.LanguageChanged(Application.Current, new EventArgs());
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000056C4 File Offset: 0x000038C4
		public App()
		{
			App.AttachConsole(-1);
			App.m_Languages.Clear();
			App.m_Languages.Add(new CultureInfo("en-US"));
			App.m_Languages.Add(new CultureInfo("ru-RU"));
			App.m_Languages.Add(new CultureInfo("de-DE"));
			App.m_Languages.Add(new CultureInfo("et-EE"));
			App.m_Languages.Add(new CultureInfo("pl-PL"));
			App.m_Languages.Add(new CultureInfo("zh-CN"));
			App.m_Languages.Add(new CultureInfo("sk-SK"));
			App.LanguageChanged += this.App_LanguageChanged;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00005788 File Offset: 0x00003988
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			LogService.GetInstance.LogInformation(e.Exception.ToString());
			e.Handled = true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000057B4 File Offset: 0x000039B4
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000057C8 File Offset: 0x000039C8
		[Obsolete]
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			Directory.SetCurrentDirectory(AppContext.BaseDirectory);
			if (File.Exists("debug.txt"))
			{
				App.AllocConsole();
			}
			Environment.GetCommandLineArgs().ToList<string>().ForEach(delegate(string x)
			{
				if (x.EndsWith("/disable-notifications"))
				{
					VertigoBoostPanel.Static.Settings.GetInstance.SendNotifications = false;
					return;
				}
				if (!x.EndsWith("/no-srt"))
				{
					if (x.EndsWith("/console"))
					{
						App.AllocConsole();
					}
					return;
				}
				VertigoBoostPanel.Static.Settings.GetInstance.Boolean_0 = true;
			});
			this.GetPrivileges();
			Hardware.GenerateHWID();
			this.LoginPage = new LoginPage();
			this.LoginPage.b_close.Click += delegate(object sender, RoutedEventArgs e)
			{
				Process.GetCurrentProcess().Kill();
			};
			this.LoginPage.Show();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000587C File Offset: 0x00003A7C
		private void GetPrivileges()
		{
			foreach (string text in new string[] { "SeRestorePrivilege", "SeBackupPrivilege", "SeTakeOwnershipPrivilege" })
			{
				IntPtr zero = IntPtr.Zero;
				App.OpenProcessToken(App.GetCurrentProcess(), 40, ref zero);
				App.TOKEN_PRIVILEGES token_PRIVILEGES;
				token_PRIVILEGES.int_0 = 1;
				token_PRIVILEGES.long_0 = 0L;
				token_PRIVILEGES.int_1 = 2;
				App.LookupPrivilegeValue(null, text, ref token_PRIVILEGES.long_0);
				App.AdjustTokenPrivileges(zero, false, ref token_PRIVILEGES, 0, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x06000022 RID: 34
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref App.TOKEN_PRIVILEGES NewState, int BufferLengthInBytes, IntPtr PreviousState, IntPtr ReturnLengthInBytes);

		// Token: 0x06000023 RID: 35
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LookupPrivilegeValue(string string_1, string string_2, ref long long_0);

		// Token: 0x06000024 RID: 36
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool OpenProcessToken(IntPtr handle, int int_0, ref IntPtr TokenHandle);

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AllocConsole();

		// Token: 0x06000026 RID: 38
		[DllImport("kernel32.dll")]
		private static extern bool AttachConsole(int dwProcessId);

		// Token: 0x04000006 RID: 6
		private static List<CultureInfo> m_Languages = new List<CultureInfo>();

		// Token: 0x04000008 RID: 8
		private LoginPage LoginPage;

		// Token: 0x02000006 RID: 6
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TOKEN_PRIVILEGES
		{
			// Token: 0x0400000A RID: 10
			public int int_0;

			// Token: 0x0400000B RID: 11
			public long long_0;

			// Token: 0x0400000C RID: 12
			public int int_1;
		}
	}
}
