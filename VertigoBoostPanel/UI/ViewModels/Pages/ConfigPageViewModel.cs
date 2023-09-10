using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using Microsoft.Win32;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Services.Relay;
using VertigoBoostPanel.Services.Windows;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.pages;
using VertigoBoostPanel.UI.windows;

namespace VertigoBoostPanel.UI.ViewModels.Pages
{
	// Token: 0x02000046 RID: 70
	public class ConfigPageViewModel : INotifyPropertyChanged
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public ConfigPageViewModel(Page page)
		{
			this._page = (ConfigPage)page;
			this._page.InitializeComponent();
			string[] array = new string[] { "mg_de_mirage", "mg_de_vertigo", "mg_de_inferno", "mg_de_overpass", "mg_de_nuke", "mg_de_ancient", "mg_de_dust2", "mg_lobby_mapveto", "mg_cs_agency", "mg_cs_office" };
			string[] array2 = new string[] { "mg_de_crete", "mg_de_hive", "mg_de_vertigo", "mg_de_overpass", "mg_de_cbble", "mg_de_train", "mg_de_shortnuke", "mg_de_shortdust", "mg_de_lake" };
			foreach (string text in array)
			{
				this._page.ComboBox_MapChoice_5x5.Items.Add(text);
			}
			foreach (string text2 in array2)
			{
				this._page.ComboBox_MapChoice_2x2.Items.Add(text2);
			}
			this._page.ComboBox_MapChoice_5x5.SelectedItem = Settings.GetInstance.MapToPlay5x5;
			this._page.ComboBox_MapChoice_2x2.SelectedItem = Settings.GetInstance.MapToPlay2x2;
			foreach (string text3 in new string[] { "Ranked", "Unranked" })
			{
				this._page.ComboBox_CompetitiveMode.Items.Add(text3);
			}
			this._page.ComboBox_CompetitiveMode.SelectedItem = Settings.GetInstance.CompetitiveMode;
			foreach (string text4 in new string[] { "5x5", "2x2" })
			{
				this._page.ComboBox_DefaultTab.Items.Add(text4);
			}
			this._page.ComboBox_DefaultTab.SelectedItem = Settings.GetInstance.DefaultLaunchTab;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600017A RID: 378 RVA: 0x0000BD10 File Offset: 0x00009F10
		// (remove) Token: 0x0600017B RID: 379 RVA: 0x0000BD50 File Offset: 0x00009F50
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003502 File Offset: 0x00001702
		public bool SteamOverlay_OnButtonState
		{
			get
			{
				return this.SteamOverlayState;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000350A File Offset: 0x0000170A
		public bool SteamOverlay_OffButtonState
		{
			get
			{
				return !this.SteamOverlayState;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003515 File Offset: 0x00001715
		public bool SteamWebHelper_OnButtonState
		{
			get
			{
				return this.SteamWebHelperState;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000351D File Offset: 0x0000171D
		public bool SteamWebHelper_OffButtonState
		{
			get
			{
				return !this.SteamWebHelperState;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000BD90 File Offset: 0x00009F90
		public bool CsGoNews_OnButtonState
		{
			get
			{
				return !HostsWriter.GetInstance.IsHostsFileHasText("0.0.0.0 store.steampowered.com") && !HostsWriter.GetInstance.IsHostsFileHasText("127.0.0.1 store.steampowered.com");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public bool CsGoNews_OffButtonState
		{
			get
			{
				return HostsWriter.GetInstance.IsHostsFileHasText("0.0.0.0 store.steampowered.com") || HostsWriter.GetInstance.IsHostsFileHasText("127.0.0.1 store.steampowered.com");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public Command InstallOldSteam
		{
			get
			{
				return new Command(delegate(object obj)
				{
					if (string.IsNullOrEmpty(Settings.GetInstance.SteamPath))
					{
						MessageBox.Show("Steam path is empty !", "VertigoBoostPanel");
						return;
					}
					new SteamInstallation().Show();
					Program.GetInstance.MainWindow.Hide();
				}, null);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003528 File Offset: 0x00001728
		public Command Map_5x5_Changed
		{
			get
			{
				return new Command(delegate(object obj)
				{
					Settings.GetInstance.MapToPlay5x5 = this._page.ComboBox_MapChoice_5x5.SelectedItem.ToString();
				}, null);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000353C File Offset: 0x0000173C
		public Command Map_2x2_Changed
		{
			get
			{
				return new Command(delegate(object obj)
				{
					Settings.GetInstance.MapToPlay2x2 = this._page.ComboBox_MapChoice_2x2.SelectedItem.ToString();
				}, null);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003550 File Offset: 0x00001750
		public Command CompetitiveMode_Changed
		{
			get
			{
				return new Command(delegate(object obj)
				{
					Settings.GetInstance.CompetitiveMode = this._page.ComboBox_CompetitiveMode.SelectedItem.ToString();
				}, null);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003564 File Offset: 0x00001764
		public Command DefaultTab_Changed
		{
			get
			{
				return new Command(delegate(object obj)
				{
					Settings.GetInstance.DefaultLaunchTab = this._page.ComboBox_DefaultTab.SelectedItem.ToString();
				}, null);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003578 File Offset: 0x00001778
		public Command SteamOverlay_On
		{
			get
			{
				return new Command(delegate(object obj)
				{
					this.SteamOverlayState = true;
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("SteamOverlay_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 != null)
					{
						propertyChanged2(this, new PropertyChangedEventArgs("SteamOverlay_OffButtonState"));
						return;
					}
				}, null);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000358C File Offset: 0x0000178C
		public Command SteamOverlay_Off
		{
			get
			{
				return new Command(delegate(object obj)
				{
					this.SteamOverlayState = false;
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("SteamOverlay_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 != null)
					{
						propertyChanged2(this, new PropertyChangedEventArgs("SteamOverlay_OffButtonState"));
						return;
					}
				}, null);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000035A0 File Offset: 0x000017A0
		public Command SteamWebHelper_On
		{
			get
			{
				return new Command(delegate(object obj)
				{
					this.SteamWebHelperState = true;
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("SteamWebHelper_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 != null)
					{
						propertyChanged2(this, new PropertyChangedEventArgs("SteamWebHelper_OffButtonState"));
						return;
					}
				}, null);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000035B4 File Offset: 0x000017B4
		public Command SteamWebHelper_Off
		{
			get
			{
				return new Command(delegate(object obj)
				{
					this.SteamWebHelperState = false;
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("SteamWebHelper_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 == null)
					{
						return;
					}
					propertyChanged2(this, new PropertyChangedEventArgs("SteamWebHelper_OffButtonState"));
				}, null);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000035C8 File Offset: 0x000017C8
		public Command CsGoNews_On
		{
			get
			{
				return new Command(delegate(object obj)
				{
					HostsWriter.GetInstance.DeleteTextFromHosts("0.0.0.0 store.steampowered.com");
					HostsWriter.GetInstance.DeleteTextFromHosts("127.0.0.1 store.steampowered.com");
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("CsGoNews_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 == null)
					{
						return;
					}
					propertyChanged2(this, new PropertyChangedEventArgs("CsGoNews_OffButtonState"));
				}, null);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000035DC File Offset: 0x000017DC
		public Command CsGoNews_Off
		{
			get
			{
				return new Command(delegate(object obj)
				{
					HostsWriter.GetInstance.AddTextToHostsFile("0.0.0.0 store.steampowered.com");
					PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
					if (propertyChanged != null)
					{
						propertyChanged(this, new PropertyChangedEventArgs("CsGoNews_OnButtonState"));
					}
					PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
					if (propertyChanged2 == null)
					{
						return;
					}
					propertyChanged2(this, new PropertyChangedEventArgs("CsGoNews_OffButtonState"));
				}, null);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000BE28 File Offset: 0x0000A028
		public Command AddAdapter
		{
			get
			{
				return new Command(delegate(object obj)
				{
					NetworkAdapterWrapper.GetInstance.CreateAdapter();
				}, null);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public Command RemoveAdapters
		{
			get
			{
				return new Command(delegate(object obj)
				{
					NetworkAdapterWrapper.GetInstance.RemoveAdapters();
				}, null);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000035F0 File Offset: 0x000017F0
		public Command ClearUserdata
		{
			get
			{
				return new Command(delegate(object obj)
				{
					if (!string.IsNullOrEmpty(Settings.GetInstance.SteamPath) && !string.IsNullOrEmpty(Settings.GetInstance.CsGoPath))
					{
						ConfigPageViewModel.DeleteFiles(Settings.GetInstance.SteamPath, "steam_*.exe");
						ConfigPageViewModel.DeleteFiles(Settings.GetInstance.SteamPath, "Steam_*.exe");
						this.DeleteFolders(Settings.GetInstance.SteamPath + "\\userdata\\", "*");
						this.DeleteFolders(Settings.GetInstance.CsGoPath, "csgo_*");
						this.DeleteFolders(Settings.GetInstance.CsGoPath, "Csgo_*");
						return;
					}
				}, null);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000BE90 File Offset: 0x0000A090
		public static void DeleteFiles(string path, string mask)
		{
			if (Directory.Exists(path))
			{
				try
				{
					Directory.GetFiles(path, mask, SearchOption.AllDirectories).AsParallel<string>().ForAll(delegate(string x)
					{
						try
						{
							File.SetAttributes(x, File.GetAttributes(x) & ~FileAttributes.ReadOnly);
						}
						catch (Exception ex2)
						{
							LogService.GetInstance.LogInformation(ex2.ToString());
						}
						try
						{
							File.Delete(x);
						}
						catch (Exception ex3)
						{
							LogService.GetInstance.LogInformation(ex3.ToString());
						}
					});
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000BF00 File Offset: 0x0000A100
		private void DeleteFolders(string path, string mask)
		{
			if (Directory.Exists(path))
			{
				try
				{
					Directory.GetDirectories(path, mask, SearchOption.AllDirectories).AsParallel<string>().ForAll(delegate(string x)
					{
						try
						{
							ConfigPageViewModel.DeleteFiles(x, mask);
						}
						catch (Exception ex2)
						{
							LogService.GetInstance.LogInformation(ex2.ToString());
						}
						try
						{
							Directory.Delete(x, true);
						}
						catch (Exception ex3)
						{
							LogService.GetInstance.LogInformation(ex3.ToString());
						}
					});
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000BF74 File Offset: 0x0000A174
		private bool SteamOverlayState
		{
			get
			{
				return Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\gameoverlayui.exe") == null;
			}
			set
			{
				try
				{
					if (value)
					{
						Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\gameoverlayui.exe");
					}
					else
					{
						using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\gameoverlayui.exe"))
						{
							registryKey.SetValue("Debugger", "C:\\Windows\\system32\\systray.exe", RegistryValueKind.String);
						}
					}
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003618 File Offset: 0x00001818
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		private bool SteamWebHelperState
		{
			get
			{
				return Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\steamwebhelper.exe") == null;
			}
			set
			{
				try
				{
					if (value)
					{
						Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\steamwebhelper.exe");
					}
					else
					{
						using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\steamwebhelper.exe"))
						{
							registryKey.SetValue("Debugger", "C:\\Windows\\system32\\systray.exe", RegistryValueKind.String);
						}
					}
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
		}

		// Token: 0x04000158 RID: 344
		private ConfigPage _page;
	}
}
