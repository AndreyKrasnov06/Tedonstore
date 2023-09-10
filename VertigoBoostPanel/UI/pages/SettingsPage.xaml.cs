using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x02000053 RID: 83
	public partial class SettingsPage : Page, INotifyPropertyChanged
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public SettingsPage()
		{
			if (string.IsNullOrEmpty(Settings.GetInstance.SteamPath) || string.IsNullOrWhiteSpace(Settings.GetInstance.SteamPath))
			{
				try
				{
					object obj = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Valve\\Steam");
					if (SettingsPage.<>o__0.<>p__3 == null)
					{
						SettingsPage.<>o__0.<>p__3 = CallSite<Func<CallSite, object, IDisposable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(IDisposable), typeof(SettingsPage)));
					}
					string text2;
					using (SettingsPage.<>o__0.<>p__3.Target(SettingsPage.<>o__0.<>p__3, obj))
					{
						if (SettingsPage.<>o__0.<>p__1 == null)
						{
							SettingsPage.<>o__0.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SettingsPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, bool> target = SettingsPage.<>o__0.<>p__1.Target;
						CallSite <>p__ = SettingsPage.<>o__0.<>p__1;
						if (SettingsPage.<>o__0.<>p__0 == null)
						{
							SettingsPage.<>o__0.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(SettingsPage), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						object obj2;
						if (target(<>p__, SettingsPage.<>o__0.<>p__0.Target(SettingsPage.<>o__0.<>p__0, obj, null)))
						{
							if (SettingsPage.<>o__0.<>p__2 == null)
							{
								SettingsPage.<>o__0.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetValue", null, typeof(SettingsPage), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							obj2 = SettingsPage.<>o__0.<>p__2.Target(SettingsPage.<>o__0.<>p__2, obj, "SteamPath");
						}
						else
						{
							obj2 = null;
						}
						string text = obj2 as string;
						if (text == null)
						{
							text2 = "";
						}
						else
						{
							text2 = text.Replace("/", "\\") + "\\";
						}
					}
					if (!string.IsNullOrEmpty(text2))
					{
						Settings.GetInstance.SteamPath = text2;
						if (File.Exists(text2 + "steamapps\\common\\Counter-Strike Global Offensive\\csgo.exe"))
						{
							Settings.GetInstance.CsGoPath = text2 + "steamapps\\common\\Counter-Strike Global Offensive\\";
						}
					}
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
			base.DataContext = this;
			this.InitializeComponent();
			this.windowWidthBox.TextChanged += this.windowWidthBox_TextChanged;
			this.windowHeightBox.TextChanged += this.windowHeightBox_TextChanged;
			this.GetIndexOfMonitor();
			this.ShowMonitorName(this.current_monitor_index);
			this.routeDataGrid.ItemsSource = this.DGroutes;
			if (!Settings.GetInstance.Boolean_0)
			{
				this.initSteamRoutTool();
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000FD14 File Offset: 0x0000DF14
		private void LanguageButton_Click(object sender, RoutedEventArgs e)
		{
			if (!(Settings.GetInstance.Language == "en-US"))
			{
				if (!(Settings.GetInstance.Language == "ru-RU"))
				{
					if (Settings.GetInstance.Language == "et-EE")
					{
						string newLang2 = "pl-PL";
						Settings.GetInstance.Language = newLang2;
						App.Language = (from obj in App.Languages
							where obj.Name == newLang2
							select (obj)).FirstOrDefault<CultureInfo>();
					}
					else if (Settings.GetInstance.Language == "pl-PL")
					{
						string newLang3 = "zh-CN";
						Settings.GetInstance.Language = newLang3;
						App.Language = (from obj in App.Languages
							where obj.Name == newLang3
							select (obj)).FirstOrDefault<CultureInfo>();
					}
					else if (!(Settings.GetInstance.Language == "zh-CN"))
					{
						if (!(Settings.GetInstance.Language == "de-DE"))
						{
							if (Settings.GetInstance.Language == "sk-SK")
							{
								string newLang4 = "en-US";
								Settings.GetInstance.Language = newLang4;
								App.Language = (from obj in App.Languages
									where obj.Name == newLang4
									select (obj)).FirstOrDefault<CultureInfo>();
							}
						}
						else
						{
							string newLang5 = "sk-SK";
							Settings.GetInstance.Language = newLang5;
							App.Language = (from obj in App.Languages
								where obj.Name == newLang5
								select (obj)).FirstOrDefault<CultureInfo>();
						}
					}
					else
					{
						string newLang6 = "de-DE";
						Settings.GetInstance.Language = newLang6;
						App.Language = (from obj in App.Languages
							where obj.Name == newLang6
							select (obj)).FirstOrDefault<CultureInfo>();
					}
				}
				else
				{
					string newLang7 = "et-EE";
					Settings.GetInstance.Language = newLang7;
					App.Language = (from obj in App.Languages
						where obj.Name == newLang7
						select (obj)).FirstOrDefault<CultureInfo>();
				}
			}
			else
			{
				string newLang = "ru-RU";
				Settings.GetInstance.Language = newLang;
				App.Language = (from obj in App.Languages
					where obj.Name == newLang
					select (obj)).FirstOrDefault<CultureInfo>();
			}
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs("CurrentLangFriendlyName"));
				return;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public string CurrentLangFriendlyName
		{
			get
			{
				if (Settings.GetInstance.Language == "en-US")
				{
					return "English";
				}
				if (Settings.GetInstance.Language == "ru-RU")
				{
					return "Русский";
				}
				if (Settings.GetInstance.Language == "et-EE")
				{
					return "Eesti";
				}
				if (Settings.GetInstance.Language == "pl-PL")
				{
					return "Polski";
				}
				if (Settings.GetInstance.Language == "zh-CN")
				{
					return "中国人";
				}
				if (Settings.GetInstance.Language == "de-DE")
				{
					return "Deutsch";
				}
				if (Settings.GetInstance.Language == "sk-SK")
				{
					return "Slovenský";
				}
				return "Unknown";
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000375B File Offset: 0x0000195B
		public string PanelVersion
		{
			get
			{
				return "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000377C File Offset: 0x0000197C
		public string NullSession
		{
			get
			{
				return "/Resources/img/null_session.png";
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00003783 File Offset: 0x00001983
		public string YesSession
		{
			get
			{
				return "/Resources/img/yes_session.png";
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000036CB File Offset: 0x000018CB
		public string AccountMark
		{
			get
			{
				return "/Resources/img/account_mark.png";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000034D2 File Offset: 0x000016D2
		public string SettingsLogo
		{
			get
			{
				return "/Resources/img/settings.png";
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000101BC File Offset: 0x0000E3BC
		public SettingsPage ReadyToSwap()
		{
			if (Settings.GetInstance.AlignPlayers)
			{
				this.alignBox.IsChecked = new bool?(true);
			}
			if (Settings.GetInstance.MouseEnabled)
			{
				this.mouseBox.IsChecked = new bool?(true);
			}
			if (Settings.GetInstance.RecentInvite)
			{
				this.inviteBox.IsChecked = new bool?(true);
			}
			if (Settings.GetInstance.SuspendOverlay)
			{
				Settings.GetInstance.SuspendOverlay = false;
			}
			this.windowWidthBox.Text = Settings.GetInstance.Resolution.Split(new char[] { 'x' })[0];
			this.windowHeightBox.Text = Settings.GetInstance.Resolution.Split(new char[] { 'x' })[1];
			this.IdBox.Text = Settings.GetInstance.TelegramId;
			this.argsBox.Text = Settings.GetInstance.Arguments;
			this.discordLoginsBox.Text = Settings.GetInstance.DiscordLogins;
			this.lobbyAtTime.Text = Settings.GetInstance.LobbyAtTime;
			this.CustomLaunchDelay.Text = Settings.GetInstance.CustomLaunchDelay;
			this.DHookBox.Text = Settings.GetInstance.DiscordHook;
			if (!string.IsNullOrEmpty(Settings.GetInstance.SteamPath) && !string.IsNullOrWhiteSpace(Settings.GetInstance.SteamPath))
			{
				this.SteamStatus.Fill = Brushes.Green;
			}
			else
			{
				this.SteamStatus.Fill = Brushes.Red;
			}
			if (!string.IsNullOrEmpty(Settings.GetInstance.CsGoPath) && !string.IsNullOrWhiteSpace(Settings.GetInstance.CsGoPath))
			{
				this.csgoStatus.Fill = Brushes.Green;
			}
			else
			{
				this.csgoStatus.Fill = Brushes.Red;
			}
			return this;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001038C File Offset: 0x0000E58C
		private void initSteamRoutTool()
		{
			string text = string.Empty;
			using (WebClient webClient = new WebClient())
			{
				text = webClient.DownloadString(this.networkconfigURL);
			}
			if (!string.IsNullOrEmpty(text))
			{
				foreach (KeyValuePair<string, JToken> keyValuePair in ((JObject)JsonConvert.DeserializeObject<JObject>(text)["pops"]))
				{
					if (keyValuePair.Value.ToString().Contains("relays") && !keyValuePair.Value.ToString().Contains("cloud-test"))
					{
						string text2 = keyValuePair.Key;
						if (keyValuePair.Value.ToString().Contains("\"desc\""))
						{
							text2 = keyValuePair.Value["desc"].ToString();
						}
						int num = 1;
						foreach (JToken jtoken in ((IEnumerable<JToken>)keyValuePair.Value["relays"]))
						{
							List<SteamRoute> dgroutes = this.DGroutes;
							string text3 = text2 + string.Format(" [{0}]", num++);
							string text4 = jtoken["ipv4"].ToString();
							JToken jtoken2 = jtoken["port_range"][0];
							string text5 = ((jtoken2 == null) ? null : jtoken2.ToString());
							string text6 = "-";
							JToken jtoken3 = jtoken["port_range"][1];
							dgroutes.Add(new SteamRoute(text3, text4, text5 + text6 + ((jtoken3 == null) ? null : jtoken3.ToString())));
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000105A4 File Offset: 0x0000E7A4
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
			commonOpenFileDialog.IsFolderPicker = true;
			if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				Settings.GetInstance.SteamPath = commonOpenFileDialog.FileName + "\\";
				if (!File.Exists(Settings.GetInstance.SteamPath + "steam.exe"))
				{
					Settings.GetInstance.SteamPath = "";
					CreateError.NewError("Failed to find steam.exe", false);
				}
				this.ReadyToSwap();
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00010624 File Offset: 0x0000E824
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
			commonOpenFileDialog.IsFolderPicker = true;
			if (!string.IsNullOrEmpty(Settings.GetInstance.SteamPath) && !string.IsNullOrWhiteSpace(Settings.GetInstance.SteamPath))
			{
				commonOpenFileDialog.InitialDirectory = Settings.GetInstance.SteamPath;
			}
			if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				Settings.GetInstance.CsGoPath = commonOpenFileDialog.FileName + "\\";
				if (!File.Exists(Settings.GetInstance.CsGoPath + "csgo.exe"))
				{
					Settings.GetInstance.CsGoPath = "";
					CreateError.NewError("Failed to find csgo.exe", false);
				}
				this.ReadyToSwap();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000106D8 File Offset: 0x0000E8D8
		private void IdBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string text = ((System.Windows.Controls.TextBox)sender).Text;
			long num;
			if (long.TryParse(text, out num))
			{
				Settings.GetInstance.TelegramId = text;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0001070C File Offset: 0x0000E90C
		private void argsBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.GetInstance.Arguments = ((System.Windows.Controls.TextBox)sender).Text;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00010730 File Offset: 0x0000E930
		private void discordLoginsBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.GetInstance.DiscordLogins = ((System.Windows.Controls.TextBox)sender).Text;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00010754 File Offset: 0x0000E954
		private void SetMonitorName(string Name)
		{
			WindowAlign.WorkMonitor = Name;
			Settings.GetInstance.StartMonitor = WindowAlign.WorkMonitor;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00010778 File Offset: 0x0000E978
		private void GetIndexOfMonitor()
		{
			List<string> list = new List<string>();
			foreach (Screen screen in WindowAlign.AllMonitors)
			{
				list.Add(screen.DeviceFriendlyName());
			}
			this.current_monitor_index = list.IndexOf(WindowAlign.WorkMonitor);
			if (this.current_monitor_index == -1)
			{
				WindowAlign.WorkMonitor = Screen.PrimaryScreen.DeviceFriendlyName();
				this.GetIndexOfMonitor();
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001080C File Offset: 0x0000EA0C
		private void ShowMonitorName(int index)
		{
			base.Dispatcher.Invoke(delegate
			{
				this.IdBox_Copy.Text = WindowAlign.AllMonitors[index].DeviceFriendlyName();
			});
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00010848 File Offset: 0x0000EA48
		private void IdBox_Copy_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta > 0)
			{
				this.current_monitor_index++;
				if (this.current_monitor_index > WindowAlign.AllMonitors.Count - 1)
				{
					this.current_monitor_index = 0;
				}
			}
			else
			{
				this.current_monitor_index--;
				if (this.current_monitor_index < 0)
				{
					this.current_monitor_index = WindowAlign.AllMonitors.Count - 1;
				}
			}
			this.ShowMonitorName(this.current_monitor_index);
			this.SetMonitorName(WindowAlign.AllMonitors[this.current_monitor_index].DeviceFriendlyName());
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000108DC File Offset: 0x0000EADC
		private void alignBox_Checked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.AlignPlayers = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000108F4 File Offset: 0x0000EAF4
		private void alignBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.AlignPlayers = false;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0001090C File Offset: 0x0000EB0C
		private void mouseBox_Checked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.MouseEnabled = true;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00010924 File Offset: 0x0000EB24
		private void mouseBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.MouseEnabled = false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001093C File Offset: 0x0000EB3C
		private void graphBox_Checked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.NoGraphics = true;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00010954 File Offset: 0x0000EB54
		private void graphBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.NoGraphics = false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001096C File Offset: 0x0000EB6C
		private void setAllRules_Click(object sender, RoutedEventArgs e)
		{
			foreach (SteamRoute steamRoute in this.DGroutes)
			{
				steamRoute.blockServer();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000109C0 File Offset: 0x0000EBC0
		private void unsetAllRules_Click(object sender, RoutedEventArgs e)
		{
			foreach (SteamRoute steamRoute in this.DGroutes)
			{
				steamRoute.UnblockServer();
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00010A14 File Offset: 0x0000EC14
		private void lobbyAtTime_TextChanged(object sender, TextChangedEventArgs e)
		{
			string text = ((System.Windows.Controls.TextBox)sender).Text;
			int num;
			if (int.TryParse(text, out num))
			{
				Settings.GetInstance.LobbyAtTime = text;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00010A48 File Offset: 0x0000EC48
		private void CaseFasrmAtTime_TextChanged(object sender, TextChangedEventArgs e)
		{
			string text = ((System.Windows.Controls.TextBox)sender).Text;
			int num;
			if (int.TryParse(text, out num))
			{
				Settings.GetInstance.CaseFarmAtTime = text;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00010A7C File Offset: 0x0000EC7C
		private void inviteBox_Checked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.RecentInvite = true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00010A94 File Offset: 0x0000EC94
		private void inviteBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.RecentInvite = false;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00010AAC File Offset: 0x0000ECAC
		private void OverlayBox_Checked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.SuspendOverlay = true;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		private void OverlayBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.GetInstance.SuspendOverlay = false;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000247 RID: 583 RVA: 0x00010ADC File Offset: 0x0000ECDC
		// (remove) Token: 0x06000248 RID: 584 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000249 RID: 585 RVA: 0x00010B5C File Offset: 0x0000ED5C
		private void windowWidthBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.action_1 == 0)
			{
				this.action_1++;
				return;
			}
			Settings.GetInstance.Resolution = this.windowWidthBox.Text + "x" + this.windowHeightBox.Text;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00010BAC File Offset: 0x0000EDAC
		private void windowHeightBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.action_2 != 0)
			{
				Settings.GetInstance.Resolution = this.windowWidthBox.Text + "x" + this.windowHeightBox.Text;
				return;
			}
			this.action_2++;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00010BFC File Offset: 0x0000EDFC
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			string path = string.Empty;
			System.Windows.Forms.OpenFileDialog OPF = new System.Windows.Forms.OpenFileDialog();
			Exception threadEx = null;
			Thread thread = new Thread(delegate
			{
				try
				{
					OPF.Filter = "Файлы bat|*.bat";
					OPF.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Desktop\\";
					if (OPF.ShowDialog() == DialogResult.OK)
					{
						path = OPF.FileName;
					}
					else
					{
						path = "None";
					}
				}
				catch (Exception ex)
				{
					threadEx = ex;
					path = "None";
				}
				Console.WriteLine(path);
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			if ((!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path)) || path == "None")
			{
				if (!File.Exists(path))
				{
					CreateError.NewError("File does not exist", false);
					return;
				}
				Settings.GetInstance.IDLEPath = path;
				this.ReadyToSwap();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		private void CustomLaunchDelay_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.GetInstance.CustomLaunchDelay = this.CustomLaunchDelay.Text;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00010CDC File Offset: 0x0000EEDC
		private void DHookBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.GetInstance.DiscordHook = this.DHookBox.Text;
		}

		// Token: 0x040001EC RID: 492
		private string networkconfigURL = "https://panel.tedonstore.com/vb/routetool/network_config.json";

		// Token: 0x040001ED RID: 493
		private List<SteamRoute> DGroutes = new List<SteamRoute>();

		// Token: 0x040001EE RID: 494
		private int current_monitor_index;

		// Token: 0x040001EF RID: 495
		private int action_1;

		// Token: 0x040001F0 RID: 496
		private int action_2;
	}
}
