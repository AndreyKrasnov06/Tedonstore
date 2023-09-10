using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using ItemsTransfer.SteamAuth;
using Microsoft.CSharp.RuntimeBinder;
using NetFwTypeLib;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.GSI;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Core.WalkBot;
using VertigoBoostPanel.Services;
using VertigoBoostPanel.Services.Api;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Services.Optimization;
using VertigoBoostPanel.Services.RegistryInteraction;
using VertigoBoostPanel.Services.Steam;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;
using VertigoBoostPanel.UI.pages;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.UI.Pages
{
	// Token: 0x0200002A RID: 42
	public class MainWindow : Window, IComponentConnector
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00008C40 File Offset: 0x00006E40
		public MainWindow()
		{
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += this.CheckAlive;
			timer.Interval = 240000.0;
			timer.Start();
			if (string.IsNullOrEmpty(Settings.GetInstance.Resolution))
			{
				Settings.GetInstance.Resolution = "383x280";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.CustomLaunchDelay))
			{
				Settings.GetInstance.CustomLaunchDelay = "7000";
			}
			this.notifyIcon = new NotifyIcon();
			StreamResourceInfo resourceStream = System.Windows.Application.GetResourceStream(new Uri("/gt_logo_app.ico", UriKind.Relative));
			System.Drawing.Size size = new System.Drawing.Size(128, 128);
			this.notifyIcon.Icon = new Icon(resourceStream.Stream, size);
			this.notifyIcon.Visible = true;
			this.notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
			this.notifyIcon.ContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("Exit", new EventHandler(this.noitifyIcon_ExitClick)));
			this.notifyIcon.MouseClick += this.noitifyIcon_Click;
			base.DataContext = this;
			WindowAlign.InitMonitors();
			this.settingsPage = new SettingsPage();
			this.homePage = new HomePage();
			this.accsPage = new AccountsPage();
			this.tasksPage = new TasksPage();
			this.InitializeComponent();
			if (Program.GetInstance.SubscriptionLevel < 2)
			{
				this.TransferButton.Visibility = Visibility.Hidden;
			}
			if (Program.GetInstance.LoginWindow == null)
			{
				throw new ArgumentNullException("Stop! ABOBA.");
			}
			Program.GetInstance.MainWindow = this;
			Program.GetInstance.GetInfoFromDataBase();
			TaskQueue.StartTaskQueue();
			GSI.StartGSI();
			if (!File.Exists("data/webhook.txt"))
			{
				File.Create("data/webhook.txt").Close();
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.Arguments))
			{
				Settings.GetInstance.Arguments = "-swapcores -noqueuedload -d3d9ex -disable_d3d9_hacks -dxlevel 90 -vrdisable -windowed -nopreload -limitvsconst -softparticlesdefaultoff -nohltv -noaafonts -nosound -novid -nojoy +violence_hblood 0 +sethdmodels 0 +mat_disable_fancy_blending 1 -panorama_nosinglecontext +r_dynamic 0 +exec autoexec.cfg -w 640 -h 480 -x 0 -y 0";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.MapToPlay5x5))
			{
				Settings.GetInstance.MapToPlay5x5 = "mg_de_vertigo";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.MapToPlay2x2))
			{
				Settings.GetInstance.MapToPlay2x2 = "mg_de_vertigo";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.CompetitiveMode))
			{
				Settings.GetInstance.CompetitiveMode = "Ranked";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.DefaultLaunchTab))
			{
				Settings.GetInstance.DefaultLaunchTab = "5x5";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.DiscordLogins))
			{
				Settings.GetInstance.DiscordLogins = "login1,login2,login3";
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.CaseFarmAtTime))
			{
				Settings.GetInstance.CaseFarmAtTime = "10";
			}
			SteamFileConfiguration.GetInstance.Init();
			if (Settings.GetInstance.SuspendOverlay)
			{
				OverlayKiller.GetInstance.Started = true;
			}
			SteamGuardAuthorizer.GetInstance.RunService();
			WebHelperSlayer.GetInstance.RunService();
			Task.Run(async delegate
			{
				await SocketBackend.GetInstance.Initialize();
			}).Wait();
			IdleDropChecker.GetInstance.RunService();
			Offsets.Load();
			uint num = 0U;
			try
			{
				if (MainWindow.SystemParametersInfo(8192U, 0U, ref num, 0U) && num > 0U)
				{
					MainWindow.SystemParametersInfo_1(8193U, 0U, 0U, 0U);
				}
			}
			catch (Exception ex)
			{
				LogService.GetInstance.LogInformation(ex.ToString());
			}
			if (Program.GetInstance.SubscriptionLevel == 3)
			{
				base.Title = "AutoFarm Full";
				return;
			}
			if (Program.GetInstance.SubscriptionLevel == 2)
			{
				base.Title = "AutoFarm Lite";
				return;
			}
			base.Title = "VB Panel";
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008FE8 File Offset: 0x000071E8
		public void ShowCaseFarmConfirmation(BoostTask task)
		{
			this.currentCaseFarmChoice = task;
			this.CaseFarmDialogGrid.Visibility = Visibility.Visible;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009008 File Offset: 0x00007208
		private async void CancelCaseFarm(object sender, RoutedEventArgs e)
		{
			this.CaseFarmDialogGrid.Visibility = Visibility.Hidden;
			if (this.currentCaseFarmChoice != null)
			{
				this.currentCaseFarmChoice.CaseFarmType = 0;
				this.currentCaseFarmChoice = null;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009040 File Offset: 0x00007240
		private async void RunAllCaseFarm(object sender, RoutedEventArgs e)
		{
			this.CaseFarmDialogGrid.Visibility = Visibility.Hidden;
			if (this.currentCaseFarmChoice != null)
			{
				this.currentCaseFarmChoice.CaseFarmType = 2;
				StaticData.GetInstance.taskNamesInQueue.Add(this.currentCaseFarmChoice.Name);
				BoostTaskQueue.AddTaskToQueue(this.currentCaseFarmChoice);
				TasksPage taskPage = Program.GetInstance.TaskPage;
				if (taskPage != null)
				{
					taskPage.ReadyToSwap();
				}
				this.currentCaseFarmChoice = null;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009078 File Offset: 0x00007278
		private async void RunCleanCaseFarm(object sender, RoutedEventArgs e)
		{
			this.CaseFarmDialogGrid.Visibility = Visibility.Hidden;
			if (this.currentCaseFarmChoice != null)
			{
				this.currentCaseFarmChoice.CaseFarmType = 1;
				StaticData.GetInstance.taskNamesInQueue.Add(this.currentCaseFarmChoice.Name);
				BoostTaskQueue.AddTaskToQueue(this.currentCaseFarmChoice);
				TasksPage taskPage = Program.GetInstance.TaskPage;
				if (taskPage != null)
				{
					taskPage.ReadyToSwap();
				}
				this.currentCaseFarmChoice = null;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000090B0 File Offset: 0x000072B0
		private async void CheckAlive(object sender, ElapsedEventArgs e)
		{
			object obj = Task.Run(async delegate
			{
				await PanelApiService.GetInstance.CheckIsAlive();
			});
			if (MainWindow.<>o__7.<>p__0 == null)
			{
				MainWindow.<>o__7.<>p__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(MainWindow), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			MainWindow.<>o__7.<>p__0.Target(MainWindow.<>o__7.<>p__0, obj);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000034AF File Offset: 0x000016AF
		public string ConfigLogo
		{
			get
			{
				return "/Resources/img/config.png";
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000034B6 File Offset: 0x000016B6
		public string Logo
		{
			get
			{
				return "/Resources/img/logo.png";
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000034BD File Offset: 0x000016BD
		public string Home
		{
			get
			{
				return "/Resources/img/home.png";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000034C4 File Offset: 0x000016C4
		public string Accounts
		{
			get
			{
				return "/Resources/img/accounts.png";
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000034CB File Offset: 0x000016CB
		public string Sessions
		{
			get
			{
				return "/Resources/img/sessions.png";
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000034D2 File Offset: 0x000016D2
		public string SettingsLogo
		{
			get
			{
				return "/Resources/img/settings.png";
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000034D9 File Offset: 0x000016D9
		public string Faq
		{
			get
			{
				return "/Resources/img/faq.png";
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000034E0 File Offset: 0x000016E0
		public string Exit
		{
			get
			{
				return "/Resources/img/exit.png";
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000090E0 File Offset: 0x000072E0
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.frame.Content = this.homePage.ReadyToSwap();
			this.homepage_hui.Visibility = Visibility.Visible;
			if (!Directory.Exists("maFiles"))
			{
				Directory.CreateDirectory("maFiles");
			}
			MainWindow.checkNewServersFix();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009118 File Offset: 0x00007318
		private async void Drag_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Program.GetInstance.AccountsPage != null)
			{
				Program.GetInstance.AccountsPage.markChoiceMenu.Visibility = Visibility.Hidden;
			}
			if (e.ChangedButton == MouseButton.Left)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009158 File Offset: 0x00007358
		private async void ButtonTopPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Shape shape = (System.Windows.Shapes.Rectangle)((System.Windows.Controls.Button)sender).Template.FindName("Line", (System.Windows.Controls.Button)sender);
			System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)((System.Windows.Controls.Button)sender).Template.FindName("SecondLine", (System.Windows.Controls.Button)sender);
			BrushConverter brushConverter = new BrushConverter();
			shape.Fill = (System.Windows.Media.Brush)brushConverter.ConvertFromString("#FF99999B");
			if (rectangle != null)
			{
				rectangle.Fill = (System.Windows.Media.Brush)brushConverter.ConvertFromString("#FF99999B");
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009190 File Offset: 0x00007390
		private async void ButtonTopPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Shape shape = (System.Windows.Shapes.Rectangle)((System.Windows.Controls.Button)sender).Template.FindName("Line", (System.Windows.Controls.Button)sender);
			System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)((System.Windows.Controls.Button)sender).Template.FindName("SecondLine", (System.Windows.Controls.Button)sender);
			shape.Fill = System.Windows.Media.Brushes.White;
			if (rectangle != null)
			{
				rectangle.Fill = System.Windows.Media.Brushes.White;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000091C8 File Offset: 0x000073C8
		private async void Close_Button_Click(object sender, RoutedEventArgs e)
		{
			this.ClosePanel();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009200 File Offset: 0x00007400
		private async void noitifyIcon_ExitClick(object sender, EventArgs e)
		{
			this.ClosePanel();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009238 File Offset: 0x00007438
		private void ClosePanel()
		{
			try
			{
				foreach (Process process in Process.GetProcesses())
				{
					if (process.ProcessName.Contains("csgo") || process.ProcessName.ToLower().Contains("steam") || process.ProcessName == "VertigoBoostClient" || process.ProcessName.ToLower() == "gameoverlayui")
					{
						process.Kill();
					}
				}
				this.notifyIcon.Visible = false;
				this.notifyIcon.Dispose();
			}
			catch
			{
			}
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000092FC File Offset: 0x000074FC
		private async void Minimize_Button_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009334 File Offset: 0x00007534
		private async void MenuButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000936C File Offset: 0x0000756C
		private async void MenuButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
			if (this.frame.Content == this.accsPage)
			{
				if (this.AccsButton.Opacity == 0.5)
				{
					this.AccsButton.Opacity = 1.0;
				}
			}
			else if (this.frame.Content == this.homePage)
			{
				if (this.HomeButton.Opacity == 0.5)
				{
					this.HomeButton.Opacity = 1.0;
				}
			}
			else if (this.frame.Content == this.tasksPage)
			{
				if (this.TasksButton.Opacity == 0.5)
				{
					this.TasksButton.Opacity = 1.0;
				}
			}
			else if (this.frame.Content == this.settingsPage)
			{
				if (this.SettingsButton.Opacity == 0.5)
				{
					this.SettingsButton.Opacity = 1.0;
				}
			}
			else if (this.frame.Content == this.configPage)
			{
				if (this.ConfigButton.Opacity == 0.5)
				{
					this.ConfigButton.Opacity = 1.0;
				}
			}
			else if (this.frame.Content == this.transferPage && this.TransferButton.Opacity == 0.5)
			{
				this.TransferButton.Opacity = 1.0;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000093AC File Offset: 0x000075AC
		private async void AccountsPage_Click(object sender, RoutedEventArgs e)
		{
			Thread.Sleep(OverlayKiller.GetInstance.OptimizeTiming(MainWindow.ElapsedTimer));
			if (this.accsPage == null)
			{
				this.accsPage = new AccountsPage();
			}
			this.frame.Content = this.accsPage.ReadyToSwap();
			this.AccsButton.Opacity = 1.0;
			this.HomeButton.Opacity = 0.5;
			this.TasksButton.Opacity = 0.5;
			this.SettingsButton.Opacity = 0.5;
			this.ConfigButton.Opacity = 0.5;
			this.TransferButton.Opacity = 0.5;
			this.AccsTrigger.Visibility = Visibility.Visible;
			this.HomeTrigger.Visibility = Visibility.Hidden;
			this.SettingsTrigger.Visibility = Visibility.Hidden;
			this.TasksTrigger.Visibility = Visibility.Hidden;
			this.ConfigTrigger.Visibility = Visibility.Hidden;
			this.TransferTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000093E4 File Offset: 0x000075E4
		private async void HomePage_Click(object sender, RoutedEventArgs e)
		{
			AccountsPage accountsPage = this.accsPage;
			if (accountsPage != null)
			{
				accountsPage.ReadyToLeave();
			}
			this.frame.Content = this.homePage.ReadyToSwap();
			this.HomeButton.Opacity = 1.0;
			this.AccsButton.Opacity = 0.5;
			this.TasksButton.Opacity = 0.5;
			this.SettingsButton.Opacity = 0.5;
			this.ConfigButton.Opacity = 0.5;
			this.TransferButton.Opacity = 0.5;
			this.HomeTrigger.Visibility = Visibility.Visible;
			this.AccsTrigger.Visibility = Visibility.Hidden;
			this.SettingsTrigger.Visibility = Visibility.Hidden;
			this.TasksTrigger.Visibility = Visibility.Hidden;
			this.ConfigTrigger.Visibility = Visibility.Hidden;
			this.TransferTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000941C File Offset: 0x0000761C
		private async void TasksPage_Click(object sender, RoutedEventArgs e)
		{
			Util.UsePreCompiling(MainWindow.ElapsedTimer);
			if (this.tasksPage == null)
			{
				this.tasksPage = new TasksPage();
			}
			AccountsPage accountsPage = this.accsPage;
			if (accountsPage != null)
			{
				accountsPage.ReadyToLeave();
			}
			this.frame.Content = this.tasksPage.ReadyToSwap();
			this.TasksButton.Opacity = 1.0;
			this.AccsButton.Opacity = 0.5;
			this.HomeButton.Opacity = 0.5;
			this.SettingsButton.Opacity = 0.5;
			this.ConfigButton.Opacity = 0.5;
			this.TransferButton.Opacity = 0.5;
			this.TasksTrigger.Visibility = Visibility.Visible;
			this.AccsTrigger.Visibility = Visibility.Hidden;
			this.HomeTrigger.Visibility = Visibility.Hidden;
			this.SettingsTrigger.Visibility = Visibility.Hidden;
			this.ConfigTrigger.Visibility = Visibility.Hidden;
			this.TransferTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009454 File Offset: 0x00007654
		private async void SettingsPage_Click(object sender, RoutedEventArgs e)
		{
			Util.UsePreCompiling(MainWindow.ElapsedTimer);
			if (this.settingsPage == null)
			{
				this.settingsPage = new SettingsPage();
			}
			AccountsPage accountsPage = this.accsPage;
			if (accountsPage != null)
			{
				accountsPage.ReadyToLeave();
			}
			this.frame.Content = this.settingsPage.ReadyToSwap();
			this.SettingsButton.Opacity = 1.0;
			this.AccsButton.Opacity = 0.5;
			this.HomeButton.Opacity = 0.5;
			this.TasksButton.Opacity = 0.5;
			this.ConfigButton.Opacity = 0.5;
			this.TransferButton.Opacity = 0.5;
			this.SettingsTrigger.Visibility = Visibility.Visible;
			this.AccsTrigger.Visibility = Visibility.Hidden;
			this.HomeTrigger.Visibility = Visibility.Hidden;
			this.TasksTrigger.Visibility = Visibility.Hidden;
			this.ConfigTrigger.Visibility = Visibility.Hidden;
			this.TransferTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000948C File Offset: 0x0000768C
		private async void ConfigButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.configPage == null)
			{
				this.configPage = new ConfigPage();
			}
			AccountsPage accountsPage = this.accsPage;
			if (accountsPage != null)
			{
				accountsPage.ReadyToLeave();
			}
			this.frame.Content = this.configPage;
			this.ConfigButton.Opacity = 1.0;
			this.AccsButton.Opacity = 0.5;
			this.HomeButton.Opacity = 0.5;
			this.TasksButton.Opacity = 0.5;
			this.SettingsButton.Opacity = 0.5;
			this.TransferButton.Opacity = 0.5;
			this.ConfigTrigger.Visibility = Visibility.Visible;
			this.AccsTrigger.Visibility = Visibility.Hidden;
			this.HomeTrigger.Visibility = Visibility.Hidden;
			this.SettingsTrigger.Visibility = Visibility.Hidden;
			this.TasksTrigger.Visibility = Visibility.Hidden;
			this.TransferTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000094C4 File Offset: 0x000076C4
		private async void TransferButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.transferPage == null)
			{
				this.transferPage = new TransferPage();
			}
			AccountsPage accountsPage = this.accsPage;
			if (accountsPage != null)
			{
				accountsPage.ReadyToLeave();
			}
			((TransferPageViewModel)this.transferPage.DataContext).ReadyToSwap();
			this.frame.Content = this.transferPage;
			this.TransferButton.Opacity = 1.0;
			this.ConfigButton.Opacity = 0.5;
			this.AccsButton.Opacity = 0.5;
			this.HomeButton.Opacity = 0.5;
			this.TasksButton.Opacity = 0.5;
			this.SettingsButton.Opacity = 0.5;
			this.TransferTrigger.Visibility = Visibility.Visible;
			this.ConfigTrigger.Visibility = Visibility.Hidden;
			this.AccsTrigger.Visibility = Visibility.Hidden;
			this.HomeTrigger.Visibility = Visibility.Hidden;
			this.SettingsTrigger.Visibility = Visibility.Hidden;
			this.TasksTrigger.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000094FC File Offset: 0x000076FC
		private async void TroubleshootButton_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://tedonstore.com/troubleshoot");
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000952C File Offset: 0x0000772C
		private async void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			RegistryService.Login = "";
			RegistryService.Password = "";
			Process.Start(Directory.GetCurrentDirectory() + "\\VertigoBoostPanel.exe");
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000955C File Offset: 0x0000775C
		public void CreateErrorMessage(string errorText)
		{
			ErrorNotification errorNotification = new ErrorNotification(errorText);
			errorNotification.Margin = new Thickness(522.0, 0.0, 51.0, 38.0);
			errorNotification.Opacity = 0.0;
			this.errorGrid.Children.Add(errorNotification);
			ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
			thicknessAnimation.From = new Thickness?(new Thickness(522.0, -3.0, 51.0, 37.0));
			thicknessAnimation.To = new Thickness?(new Thickness(522.0, 7.0, 51.0, 37.0));
			thicknessAnimation.Duration = TimeSpan.FromSeconds(0.1);
			Storyboard.SetTarget(thicknessAnimation, errorNotification);
			Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.To = new double?((double)1f);
			doubleAnimation.Duration = TimeSpan.FromSeconds(1.0);
			Storyboard.SetTarget(doubleAnimation, errorNotification);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(UIElement.OpacityProperty));
			new Storyboard
			{
				Children = new TimelineCollection { thicknessAnimation, doubleAnimation }
			}.Begin();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000096DC File Offset: 0x000078DC
		public void CreateMessage(string errorText)
		{
			Notification notification = new Notification(errorText);
			notification.Margin = new Thickness(522.0, 0.0, 51.0, 38.0);
			notification.Opacity = 0.0;
			this.errorGrid.Children.Add(notification);
			ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
			thicknessAnimation.From = new Thickness?(new Thickness(522.0, -3.0, 51.0, 37.0));
			thicknessAnimation.To = new Thickness?(new Thickness(522.0, 7.0, 51.0, 37.0));
			thicknessAnimation.Duration = TimeSpan.FromSeconds(0.1);
			Storyboard.SetTarget(thicknessAnimation, notification);
			Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.To = new double?((double)1f);
			doubleAnimation.Duration = TimeSpan.FromSeconds(1.0);
			Storyboard.SetTarget(doubleAnimation, notification);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(UIElement.OpacityProperty));
			new Storyboard
			{
				Children = new TimelineCollection { thicknessAnimation, doubleAnimation }
			}.Begin();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000985C File Offset: 0x00007A5C
		private async void noitifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (base.Visibility == Visibility.Collapsed)
				{
					this.CenterWindowOnScreen();
				}
				base.Visibility = Visibility.Visible;
				if (base.WindowState == WindowState.Minimized)
				{
					base.WindowState = WindowState.Normal;
					base.Topmost = true;
					base.Topmost = false;
					this.CenterWindowOnScreen();
				}
				base.Topmost = true;
				base.Topmost = false;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000989C File Offset: 0x00007A9C
		private void CenterWindowOnScreen()
		{
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			double width = base.Width;
			double height = base.Height;
			base.Left = primaryScreenWidth / 2.0 - width / 2.0;
			base.Top = primaryScreenHeight / 2.0 - height / 2.0;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009908 File Offset: 0x00007B08
		private async void image_Copy_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				int num = (from p in StaticData.GetInstance.Players
					where p.Started
					where p.HideAccountLogin
					select p).Count<Player>();
				int num2 = (from p in StaticData.GetInstance.Players
					where p.Started
					where !p.HideAccountLogin
					select p).Count<Player>();
				if (num > num2)
				{
					using (IEnumerator<Player> enumerator = StaticData.GetInstance.Players.Where((Player p) => p.HideAccountLogin && p.Started).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Player player = enumerator.Current;
							player.HideAccountLogin = false;
						}
						return;
					}
				}
				using (IEnumerator<Player> enumerator = StaticData.GetInstance.Players.Where((Player p) => !p.HideAccountLogin && p.Started).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player2 = enumerator.Current;
						player2.HideAccountLogin = true;
					}
					return;
				}
			}
			if (e.RightButton == MouseButtonState.Pressed)
			{
				foreach (Player player3 in StaticData.GetInstance.Players)
				{
					if (player3.WindowLoaded)
					{
						player3.ConfirmCallvote();
					}
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009940 File Offset: 0x00007B40
		private static void checkNewServersFix()
		{
			string text = "155.133.248.36-155.133.248.41";
			if (!MainWindow.checkIfRuleExists(text))
			{
				MainWindow.blockServer(text);
			}
			text = "166.38.20.82";
			if (!MainWindow.checkIfRuleExists(text))
			{
				MainWindow.blockServer(text);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000997C File Offset: 0x00007B7C
		private static void blockServer(string ip)
		{
			INetFwRule netFwRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
			netFwRule.Enabled = true;
			netFwRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
			netFwRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
			netFwRule.RemoteAddresses = ip;
			netFwRule.Protocol = 17;
			netFwRule.RemotePorts = "27015-27068";
			netFwRule.Name = "TDN-block-" + ip;
			((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Add(netFwRule);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009A08 File Offset: 0x00007C08
		private static bool checkIfRuleExists(string ip)
		{
			using (IEnumerator enumerator = ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((INetFwRule)enumerator.Current).Name == "TDN-block-" + ip)
					{
						return true;
					}
				}
				return false;
			}
			bool flag;
			return flag;
		}

		// Token: 0x06000138 RID: 312
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SystemParametersInfo(uint action, uint param, ref uint vParam, uint init);

		// Token: 0x06000139 RID: 313
		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
		private static extern bool SystemParametersInfo_1(uint action, uint param, uint vParam, uint init);

		// Token: 0x0600013A RID: 314 RVA: 0x00009A94 File Offset: 0x00007C94
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/VertigoBoostPanel;component/ui/windows/mainwindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, uri);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009AC8 File Offset: 0x00007CC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MainWindow)target).Loaded += this.Window_Loaded;
				return;
			case 2:
				this.LayoutRoot = (Grid)target;
				return;
			case 3:
				((Border)target).MouseDown += this.Drag_MouseDown;
				return;
			case 4:
				this.button_Copy1 = (System.Windows.Controls.Button)target;
				this.button_Copy1.MouseLeave += this.ButtonTopPanel_MouseLeave;
				this.button_Copy1.MouseEnter += this.ButtonTopPanel_MouseEnter;
				this.button_Copy1.Click += this.Close_Button_Click;
				return;
			case 5:
				((System.Windows.Controls.Button)target).MouseLeave += this.ButtonTopPanel_MouseLeave;
				((System.Windows.Controls.Button)target).MouseEnter += this.ButtonTopPanel_MouseEnter;
				((System.Windows.Controls.Button)target).Click += this.Minimize_Button_Click;
				return;
			case 6:
				this.image_Copy = (System.Windows.Controls.Image)target;
				this.image_Copy.MouseDown += this.image_Copy_MouseDown;
				return;
			case 7:
				this.HomeButton = (System.Windows.Controls.Button)target;
				this.HomeButton.Click += this.HomePage_Click;
				this.HomeButton.MouseEnter += this.MenuButton_MouseEnter;
				this.HomeButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 8:
				this.AccsButton = (System.Windows.Controls.Button)target;
				this.AccsButton.Click += this.AccountsPage_Click;
				this.AccsButton.MouseEnter += this.MenuButton_MouseEnter;
				this.AccsButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 9:
				this.TasksButton = (System.Windows.Controls.Button)target;
				this.TasksButton.Click += this.TasksPage_Click;
				this.TasksButton.MouseEnter += this.MenuButton_MouseEnter;
				this.TasksButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 10:
				this.SettingsButton = (System.Windows.Controls.Button)target;
				this.SettingsButton.Click += this.SettingsPage_Click;
				this.SettingsButton.MouseEnter += this.MenuButton_MouseEnter;
				this.SettingsButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 11:
				this.ConfigButton = (System.Windows.Controls.Button)target;
				this.ConfigButton.Click += this.ConfigButton_Click;
				this.ConfigButton.MouseEnter += this.MenuButton_MouseEnter;
				this.ConfigButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 12:
				this.TransferButton = (System.Windows.Controls.Button)target;
				this.TransferButton.Click += this.TransferButton_Click;
				this.TransferButton.MouseEnter += this.MenuButton_MouseEnter;
				this.TransferButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 13:
				this.TroubleshootButton = (System.Windows.Controls.Button)target;
				this.TroubleshootButton.Click += this.TroubleshootButton_Click;
				this.TroubleshootButton.MouseEnter += this.MenuButton_MouseEnter;
				this.TroubleshootButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 14:
				this.ExitButton = (System.Windows.Controls.Button)target;
				this.ExitButton.Click += this.ExitButton_Click;
				this.ExitButton.MouseEnter += this.MenuButton_MouseEnter;
				this.ExitButton.MouseLeave += this.MenuButton_MouseLeave;
				return;
			case 15:
				this.HomeTrigger = (Border)target;
				return;
			case 16:
				this.frame = (Frame)target;
				return;
			case 17:
				this.AccsTrigger = (Border)target;
				return;
			case 18:
				this.TasksTrigger = (Border)target;
				return;
			case 19:
				this.SettingsTrigger = (Border)target;
				return;
			case 20:
				this.ConfigTrigger = (Border)target;
				return;
			case 21:
				this.TransferTrigger = (Border)target;
				return;
			case 22:
				this.homepage_hui = (Grid)target;
				return;
			case 23:
				this.accpage_hui = (Grid)target;
				return;
			case 24:
				this.settingspage_hui = (Grid)target;
				return;
			case 25:
				this.taskspage_hui = (Grid)target;
				return;
			case 26:
				this.YESSS = (System.Windows.Shapes.Rectangle)target;
				return;
			case 27:
				this.errorGrid = (Grid)target;
				return;
			case 28:
				this.CaseFarmDialogGrid = (Grid)target;
				return;
			case 29:
				((System.Windows.Controls.Button)target).Click += this.CancelCaseFarm;
				return;
			case 30:
				((System.Windows.Controls.Button)target).Click += this.RunCleanCaseFarm;
				return;
			case 31:
				((System.Windows.Controls.Button)target).Click += this.RunAllCaseFarm;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040000E0 RID: 224
		private NotifyIcon notifyIcon;

		// Token: 0x040000E1 RID: 225
		private BoostTask currentCaseFarmChoice;

		// Token: 0x040000E2 RID: 226
		public HomePage homePage;

		// Token: 0x040000E3 RID: 227
		public AccountsPage accsPage;

		// Token: 0x040000E4 RID: 228
		public SettingsPage settingsPage;

		// Token: 0x040000E5 RID: 229
		public TasksPage tasksPage;

		// Token: 0x040000E6 RID: 230
		public ConfigPage configPage;

		// Token: 0x040000E7 RID: 231
		public TransferPage transferPage;

		// Token: 0x040000E8 RID: 232
		public static bool ElapsedTimer = true;

		// Token: 0x040000E9 RID: 233
		internal Grid LayoutRoot;

		// Token: 0x040000EA RID: 234
		internal System.Windows.Controls.Button button_Copy1;

		// Token: 0x040000EB RID: 235
		internal System.Windows.Controls.Image image_Copy;

		// Token: 0x040000EC RID: 236
		internal System.Windows.Controls.Button HomeButton;

		// Token: 0x040000ED RID: 237
		internal System.Windows.Controls.Button AccsButton;

		// Token: 0x040000EE RID: 238
		internal System.Windows.Controls.Button TasksButton;

		// Token: 0x040000EF RID: 239
		internal System.Windows.Controls.Button SettingsButton;

		// Token: 0x040000F0 RID: 240
		internal System.Windows.Controls.Button ConfigButton;

		// Token: 0x040000F1 RID: 241
		internal System.Windows.Controls.Button TransferButton;

		// Token: 0x040000F2 RID: 242
		internal System.Windows.Controls.Button TroubleshootButton;

		// Token: 0x040000F3 RID: 243
		internal System.Windows.Controls.Button ExitButton;

		// Token: 0x040000F4 RID: 244
		internal Border HomeTrigger;

		// Token: 0x040000F5 RID: 245
		internal Frame frame;

		// Token: 0x040000F6 RID: 246
		internal Border AccsTrigger;

		// Token: 0x040000F7 RID: 247
		internal Border TasksTrigger;

		// Token: 0x040000F8 RID: 248
		internal Border SettingsTrigger;

		// Token: 0x040000F9 RID: 249
		internal Border ConfigTrigger;

		// Token: 0x040000FA RID: 250
		internal Border TransferTrigger;

		// Token: 0x040000FB RID: 251
		internal Grid homepage_hui;

		// Token: 0x040000FC RID: 252
		internal Grid accpage_hui;

		// Token: 0x040000FD RID: 253
		internal Grid settingspage_hui;

		// Token: 0x040000FE RID: 254
		internal Grid taskspage_hui;

		// Token: 0x040000FF RID: 255
		internal System.Windows.Shapes.Rectangle YESSS;

		// Token: 0x04000100 RID: 256
		internal Grid errorGrid;

		// Token: 0x04000101 RID: 257
		internal Grid CaseFarmDialogGrid;

		// Token: 0x04000102 RID: 258
		private bool _contentLoaded;
	}
}
