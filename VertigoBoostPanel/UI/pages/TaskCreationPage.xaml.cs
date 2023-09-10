using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x0200005F RID: 95
	public partial class TaskCreationPage : Page
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00011190 File Offset: 0x0000F390
		public TaskCreationPage(BoostTask boostTask, string editType)
		{
			base.DataContext = this;
			this.InitializeComponent();
			this.backButton_Transfer.Visibility = Visibility.Hidden;
			this.saveButton_Transfer.Visibility = Visibility.Hidden;
			this.backButton.Visibility = Visibility.Visible;
			this.saveButton.Visibility = Visibility.Visible;
			if (boostTask.SessionType.ToLower() == "case farm")
			{
				this._currentTask = boostTask;
				this._editType = editType;
				Program.GetInstance.MainWindow.YESSS.Visibility = Visibility.Visible;
				this.sessionNameBoxCaseFarm.Text = this._currentTask.Name;
				this.teamsBoxCaseFarm.Visibility = Visibility.Visible;
				this.OptionsText.Visibility = Visibility.Hidden;
				this.teamsBoxDefault.Visibility = Visibility.Hidden;
				this.settingsBoxDefault.Visibility = Visibility.Hidden;
				this.settingsBoxCaseFarm.Visibility = Visibility.Visible;
				this.checkBoxes.Visibility = Visibility.Hidden;
				foreach (Player player in StaticData.GetInstance.Players)
				{
					if (this._currentTask.FirstTeam.Contains(player))
					{
						this.playersBoxCaseFarm.Items.Add(player.PlayerShowInTask);
						this.firstTeam.Add(player.Login);
					}
				}
				this.CreatePages();
				return;
			}
			if (boostTask.SessionType.ToLower() == "transfer")
			{
				this.backButton_Transfer.Visibility = Visibility.Visible;
				this.saveButton_Transfer.Visibility = Visibility.Visible;
				this.backButton.Visibility = Visibility.Hidden;
				this.saveButton.Visibility = Visibility.Hidden;
				this._currentTask = boostTask;
				this._editType = editType;
				Program.GetInstance.MainWindow.YESSS.Visibility = Visibility.Visible;
				this.checkBoxes.Visibility = Visibility.Hidden;
				this.settingsBoxDefault.Visibility = Visibility.Hidden;
				this.settingsBoxCaseFarm.Visibility = Visibility.Hidden;
				this.teamsBoxDefault.Visibility = Visibility.Hidden;
				this.TransferSettings.Visibility = Visibility.Visible;
				this.masterDataInfo.Visibility = Visibility.Visible;
				if (this._editType != "create")
				{
					this.TransferName.Text = this._currentTask.Name;
					this.TransferLogin.Text = ((this._currentTask.FirstTeam.Count == 1) ? this._currentTask.FirstTeam[0].Login : string.Empty);
					if (this.TransferLogin.Text == string.Empty && !string.IsNullOrEmpty(this._currentTask.GameMode))
					{
						this.TransferLogin.Text = this._currentTask.GameMode;
					}
					this.TransferToken.Text = this._currentTask.ClientInviteCode;
				}
				this.sessionNameBox.Text = this._currentTask.Name;
				this.CreatePages();
				return;
			}
			this.settingsBoxDefault.Visibility = Visibility.Visible;
			this.settingsBoxCaseFarm.Visibility = Visibility.Hidden;
			string text = "AFK 1 round";
			string text2 = "WalkBot";
			ResourceDictionary resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
			};
			if (resourceDictionary != null)
			{
				text2 = resourceDictionary["m_WalkBot"] as string;
				text = resourceDictionary["m_roundAFK"] as string;
			}
			this.BOTType.Content = text;
			if (Program.GetInstance.SubscriptionLevel == 3)
			{
				this.BOTType.Content = text2;
			}
			this._editType = editType;
			this._currentTask = boostTask;
			string text3 = boostTask.SessionType.ToLower();
			if (text3 == "win boost")
			{
				this.rankBoostOptions.Visibility = Visibility.Visible;
			}
			else if (text3 == "rank boost")
			{
				this.rankBoostOptions.Visibility = Visibility.Visible;
			}
			else if (text3 == "xp boost")
			{
				this.xpBoostOptions.Visibility = Visibility.Visible;
			}
			else if (!(text3 == "client boost"))
			{
				if (text3 == "fsm")
				{
					this.checkBoxes.Visibility = Visibility.Hidden;
					this.OptionsText.Visibility = Visibility.Hidden;
					this.OptTitle.Visibility = Visibility.Hidden;
				}
				else if (text3 == "dm")
				{
					this.checkBoxes.Visibility = Visibility.Hidden;
					this.OptionsText.Visibility = Visibility.Hidden;
					this.OptTitle.Visibility = Visibility.Hidden;
				}
			}
			else
			{
				this.clientBoostOptions.Visibility = Visibility.Visible;
			}
			Program.GetInstance.MainWindow.YESSS.Visibility = Visibility.Visible;
			this.sessionNameBox.Text = this._currentTask.Name;
			this.isShortGame.IsChecked = new bool?(this._currentTask.ShortGame);
			this.checkBox_0.IsChecked = new bool?(this._currentTask.MVP);
			this.isSwapLeaders.IsChecked = new bool?(this._currentTask.SwapLeaders);
			this.clientCode.Text = this._currentTask.ClientInviteCode;
			if (!(this._currentTask.SessionType.ToLower() == "win boost") && !(this._currentTask.SessionType.ToLower() == "rank boost"))
			{
				this.countGame_xpBoost.Text = this._currentTask.CountGame.ToString();
			}
			else
			{
				this.countGame_rankBoost.Text = this._currentTask.CountGame.ToString();
			}
			if (!string.IsNullOrEmpty(this._currentTask.GameMode))
			{
				text3 = this._currentTask.GameMode.ToLower();
				if (text3 == "team 1")
				{
					this.comboBox.SelectedIndex = 0;
				}
				else if (!(text3 == "team 2"))
				{
					if (text3 == "win/lose")
					{
						this.comboBox.SelectedIndex = 2;
					}
					else if (text3 == "2win/2lose")
					{
						this.comboBox.SelectedIndex = 3;
					}
				}
				else
				{
					this.comboBox.SelectedIndex = 1;
				}
			}
			this.CurrentRank = this._currentTask.NeedClientRank;
			this.UpdateRankImage();
			if (this._currentTask.FirstTeam != null)
			{
				foreach (Player player2 in this._currentTask.FirstTeam)
				{
					this.firstTeam.Add(player2.Login);
				}
			}
			if (this._currentTask.SecondTeam != null)
			{
				foreach (Player player3 in this._currentTask.SecondTeam)
				{
					this.secondTeam.Add(player3.Login);
				}
			}
			this.showFirstTeam();
			this.CreatePages();
			if (boostTask.SessionType.ToLower() == "calibration")
			{
				this.xpBoostOptions.Visibility = Visibility.Visible;
				if (editType == "create")
				{
					this.countGame_xpBoost.Text = "2";
					this.isSwapLeaders.IsChecked = new bool?(true);
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000377C File Offset: 0x0000197C
		public string NullSession
		{
			get
			{
				return "/Resources/img/null_session.png";
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000380F File Offset: 0x00001A0F
		public string NullSession1
		{
			get
			{
				return "/Resources/img/nullsession1.png";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003783 File Offset: 0x00001983
		public string YesSession
		{
			get
			{
				return "/Resources/img/yes_session.png";
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000036CB File Offset: 0x000018CB
		public string AccountMark
		{
			get
			{
				return "/Resources/img/account_mark.png";
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000036D2 File Offset: 0x000018D2
		public string PageLeft
		{
			get
			{
				return "/Resources/img/pageLeft.png";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000036D9 File Offset: 0x000018D9
		public string PageRight
		{
			get
			{
				return "/Resources/img/pageRight.png";
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00003816 File Offset: 0x00001A16
		public string Swap
		{
			get
			{
				return "/Resources/img/swap.png";
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000381D File Offset: 0x00001A1D
		public string BigTrash
		{
			get
			{
				return "/Resources/img/big_trash.png";
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00011930 File Offset: 0x0000FB30
		public void showFirstTeam()
		{
			this.activeTeam = 1;
			this.playersBox.Items.Clear();
			using (List<string>.Enumerator enumerator = this.firstTeam.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string login = enumerator.Current;
					this.playersBox.Items.Add(StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login).PlayerShowInTask);
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public void showSecondTeam()
		{
			this.activeTeam = 2;
			this.playersBox.Items.Clear();
			using (List<string>.Enumerator enumerator = this.secondTeam.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string login = enumerator.Current;
					this.playersBox.Items.Add(StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login).PlayerShowInTask);
				}
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00011A78 File Offset: 0x0000FC78
		private void activateSecondTeam_Click(object sender, RoutedEventArgs e)
		{
			this.activateSecondTeam.Visibility = Visibility.Hidden;
			this.activateFirstTeam.Visibility = Visibility.Visible;
			this.showSecondTeam();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		private void activateFirstTeam_Click(object sender, RoutedEventArgs e)
		{
			this.activateSecondTeam.Visibility = Visibility.Visible;
			this.activateFirstTeam.Visibility = Visibility.Hidden;
			this.showFirstTeam();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		private void sessionNameBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.toolTipSessionName.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00011AEC File Offset: 0x0000FCEC
		private void caseSessionNameBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.toolTipSessionNameCaseFarm.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00011B08 File Offset: 0x0000FD08
		private void deleteSelectedAccounts_Click(object sender, RoutedEventArgs e)
		{
			List<string> list = new List<string>();
			foreach (object obj in ((IEnumerable)this.playersBox.Items))
			{
				AccountShowInTask accountShowInTask = (AccountShowInTask)obj;
				if (accountShowInTask.isChecked)
				{
					list.Add(accountShowInTask.login);
				}
			}
			foreach (object obj2 in ((IEnumerable)this.playersBoxCaseFarm.Items))
			{
				AccountShowInTask accountShowInTask2 = (AccountShowInTask)obj2;
				if (accountShowInTask2.isChecked)
				{
					list.Add(accountShowInTask2.login);
				}
			}
			using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string login = enumerator2.Current;
					this.playersBox.Items.Remove(StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login).PlayerShowInTask);
					this.playersBoxCaseFarm.Items.Remove(StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login).PlayerShowInTask);
					if (this.activeTeam == 1)
					{
						this.firstTeam.Remove(login);
					}
					else if (this.activeTeam == 2)
					{
						this.secondTeam.Remove(login);
					}
				}
			}
			this.CreatePages();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00011CDC File Offset: 0x0000FEDC
		private void swapSelectedAccounts_Click(object sender, RoutedEventArgs e)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			using (List<string>.Enumerator enumerator = this.firstTeam.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string login2 = enumerator.Current;
					if (StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login2).PlayerShowInTask.isChecked)
					{
						list.Add(login2);
					}
				}
			}
			using (List<string>.Enumerator enumerator = this.secondTeam.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string login = enumerator.Current;
					if (StaticData.GetInstance.Players.FirstOrDefault((Player player) => player.Login == login).PlayerShowInTask.isChecked)
					{
						list2.Add(login);
					}
				}
			}
			this.playersBox.Items.Clear();
			if (this.secondTeam.Count - list2.Count + list.Count <= 5 && this.firstTeam.Count - list.Count + list2.Count <= 5)
			{
				foreach (string text in list)
				{
					this.firstTeam.Remove(text);
				}
				foreach (string text2 in list)
				{
					this.secondTeam.Add(text2);
				}
				foreach (string text3 in list2)
				{
					this.secondTeam.Remove(text3);
				}
				foreach (string text4 in list2)
				{
					this.firstTeam.Add(text4);
				}
				if (this.activeTeam == 1)
				{
					this.showFirstTeam();
					return;
				}
				if (this.activeTeam == 2)
				{
					this.showSecondTeam();
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00011F8C File Offset: 0x0001018C
		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			if (this._currentTask.SessionType.ToLower() == "transfer")
			{
				if (string.IsNullOrEmpty(this.TransferName.Text) || string.IsNullOrWhiteSpace(this.TransferName.Text))
				{
					CreateError.NewError("Please write task name", false);
					return;
				}
				if (string.IsNullOrEmpty(this.TransferToken.Text) || string.IsNullOrWhiteSpace(this.TransferToken.Text))
				{
					CreateError.NewError("Please write master token", false);
					return;
				}
				if (string.IsNullOrEmpty(this.TransferLogin.Text) || string.IsNullOrWhiteSpace(this.TransferLogin.Text))
				{
					CreateError.NewError("Please write master login", false);
					return;
				}
				this.AccountsBox.Items.Clear();
				this._currentTask.ClientInviteCode = this.TransferToken.Text;
				this._currentTask.Name = this.TransferName.Text;
				List<Player> list = StaticData.GetInstance.Players.Where((Player x) => x.Login == this.TransferLogin.Text).ToList<Player>();
				if (list.Count > 0)
				{
					this._currentTask.FirstTeam = list;
				}
				else
				{
					this._currentTask.GameMode = this.TransferLogin.Text;
				}
				this._currentTask.SecondTeam = StaticData.GetInstance.Players.Where((Player x) => x.PlayerShow.IsChecked).ToList<Player>();
			}
			else if (!(this._currentTask.SessionType.ToLower() != "case farm"))
			{
				if (string.IsNullOrEmpty(this.sessionNameBoxCaseFarm.Text) || string.IsNullOrWhiteSpace(this.sessionNameBoxCaseFarm.Text))
				{
					CreateError.NewError("Please write task name", false);
					return;
				}
				this.AccountsBox.Items.Clear();
				this._currentTask.Name = this.sessionNameBoxCaseFarm.Text;
				List<Player> list2 = new List<Player>();
				using (IEnumerator enumerator = ((IEnumerable)this.playersBoxCaseFarm.Items).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AccountShowInTask player = (AccountShowInTask)enumerator.Current;
						list2.Add(StaticData.GetInstance.Players.FirstOrDefault((Player acc) => acc.Login == player.login));
					}
				}
				this._currentTask.FirstTeam = list2;
				this.playersBoxCaseFarm.Items.Clear();
			}
			else
			{
				if (string.IsNullOrEmpty(this.sessionNameBox.Text) || string.IsNullOrWhiteSpace(this.sessionNameBox.Text))
				{
					CreateError.NewError("Please write task name", false);
					return;
				}
				int num = 0;
				int num2 = 0;
				if (this._currentTask.SessionType.ToLower() != "fsm" && this._currentTask.SessionType.ToLower() != "dm")
				{
					int.TryParse(this.countGame_rankBoost.Text, out num);
					int.TryParse(this.countGame_xpBoost.Text, out num2);
					if (num == 0 && num2 == 0 && this._currentTask.SessionType.ToLower() != "global boost" && this._currentTask.SessionType.ToLower() != "client boost")
					{
						CreateError.NewError("Please enter games more than 0", false);
						return;
					}
				}
				if (this.activeTeam == 1)
				{
					this.firstTeam.Clear();
					using (IEnumerator enumerator = ((IEnumerable)this.playersBox.Items).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							AccountShowInTask accountShowInTask = (AccountShowInTask)obj;
							this.firstTeam.Add(accountShowInTask.login);
						}
						goto IL_45B;
					}
				}
				if (this.activeTeam == 2)
				{
					this.secondTeam.Clear();
					foreach (object obj2 in ((IEnumerable)this.playersBox.Items))
					{
						AccountShowInTask accountShowInTask2 = (AccountShowInTask)obj2;
						this.secondTeam.Add(accountShowInTask2.login);
					}
				}
				IL_45B:
				this.playersBoxCaseFarm.Items.Clear();
				this.AccountsBox.Items.Clear();
				this.playersBox.Items.Clear();
				this._currentTask.Name = this.sessionNameBox.Text;
				List<Player> list3 = new List<Player>();
				using (List<string>.Enumerator enumerator2 = this.firstTeam.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string login2 = enumerator2.Current;
						list3.Add(StaticData.GetInstance.Players.FirstOrDefault((Player acc) => acc.Login == login2));
					}
				}
				this._currentTask.FirstTeam = list3;
				List<Player> list4 = new List<Player>();
				using (List<string>.Enumerator enumerator2 = this.secondTeam.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string login = enumerator2.Current;
						list4.Add(StaticData.GetInstance.Players.FirstOrDefault((Player acc) => acc.Login == login));
					}
				}
				this._currentTask.SecondTeam = list4;
				this._currentTask.ShortGame = this.isShortGame.IsChecked.Value;
				this._currentTask.MVP = this.checkBox_0.IsChecked.Value;
				this._currentTask.SwapLeaders = this.isSwapLeaders.IsChecked.Value;
				this._currentTask.ClientInviteCode = this.clientCode.Text;
				if (!(this._currentTask.SessionType.ToLower() == "win boost") && !(this._currentTask.SessionType.ToLower() == "rank boost"))
				{
					this._currentTask.CountGame = Convert.ToInt32(this.countGame_xpBoost.Text);
				}
				else
				{
					this._currentTask.CountGame = Convert.ToInt32(this.countGame_rankBoost.Text);
				}
				this._currentTask.GameMode = this.comboBox.Text.ToString();
				this._currentTask.NeedClientRank = this.CurrentRank;
			}
			Program.GetInstance.MainWindow.YESSS.Visibility = Visibility.Hidden;
			if (!(this._currentTask.SessionType.ToLower() != "transfer"))
			{
				((TransferPageViewModel)TransferPage.Instance.DataContext).ReadyToSwap();
				Program.GetInstance.MainWindow.frame.Content = TransferPage.Instance;
				Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.ConfigTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TransferTrigger.Visibility = Visibility.Visible;
			}
			else
			{
				Program.GetInstance.MainWindow.frame.Content = Program.GetInstance.TaskPage.ReadyToSwap();
				Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Visible;
				Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Visible;
			}
			if (Program.GetInstance.MainWindow.AccsButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.AccsButton.Opacity = 0.5;
			}
			if (Program.GetInstance.MainWindow.HomeButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.HomeButton.Opacity = 0.5;
			}
			if (Program.GetInstance.MainWindow.SettingsButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.SettingsButton.Opacity = 0.5;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00012938 File Offset: 0x00010B38
		private void AccountsBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = false;
			ListBox listBox = (ListBox)sender;
			this.dragSource = listBox;
			AccountShow accountShow = (AccountShow)TaskCreationPage.GetDataFromListBox(this.dragSource, e.GetPosition(listBox));
			if (accountShow != null)
			{
				DragDrop.DoDragDrop(listBox, accountShow, DragDropEffects.Move);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00012988 File Offset: 0x00010B88
		private static object GetDataFromListBox(object source, Point point)
		{
			UIElement uielement = source.InputHitTest(point) as UIElement;
			if (uielement != null)
			{
				object obj = DependencyProperty.UnsetValue;
				while (obj == DependencyProperty.UnsetValue)
				{
					obj = source.ItemContainerGenerator.ItemFromContainer(uielement);
					if (obj == DependencyProperty.UnsetValue)
					{
						uielement = VisualTreeHelper.GetParent(uielement) as UIElement;
					}
					if (uielement == source)
					{
						return null;
					}
				}
				if (obj != DependencyProperty.UnsetValue)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000129F8 File Offset: 0x00010BF8
		private void ListBox_Drop(object sender, DragEventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			if (this._currentTask.SessionType.ToLower() != "case farm" && this._currentTask.SessionType.ToLower() != "fsm" && this._currentTask.SessionType.ToLower() != "dm" && listBox.Items.Count >= 5)
			{
				return;
			}
			if (this._currentTask.SessionType.ToLower() == "fsm" && listBox.Items.Count >= 6)
			{
				return;
			}
			if (this._currentTask.SessionType.ToLower() == "dm" && listBox.Items.Count >= 8)
			{
				return;
			}
			AccountShow accountShow = (AccountShow)e.Data.GetData(typeof(AccountShow));
			this.AccountsBox.Items.Remove(accountShow);
			listBox.Items.Add(accountShow._currentPlayer.PlayerShowInTask);
			if (this.activeTeam == 1)
			{
				this.firstTeam.Add(accountShow._currentPlayer.Login);
			}
			else if (this.activeTeam == 2)
			{
				this.secondTeam.Add(accountShow._currentPlayer.Login);
			}
			int num = this.current_page;
			this.CreatePages();
			if (this.pages.Count - 1 < num)
			{
				num--;
			}
			this.current_page = num;
			this.NavigateToPage();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00012B88 File Offset: 0x00010D88
		private void rankImg_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta <= 0)
			{
				if (e.Delta < 0)
				{
					this.CurrentRank--;
					if (this.CurrentRank < 0)
					{
						this.CurrentRank = 18;
					}
					this.UpdateRankImage();
				}
				return;
			}
			this.CurrentRank++;
			if (this.CurrentRank > 18)
			{
				this.CurrentRank = 0;
			}
			this.UpdateRankImage();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private void UpdateRankImage()
		{
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri(string.Format("/Resources/img/ranks/{0}.png", this.CurrentRank), UriKind.Relative);
			bitmapImage.EndInit();
			this.rankImg.Stretch = Stretch.Fill;
			this.rankImg.Source = bitmapImage;
			this.rankImg.Width = 50.0;
			this.rankImg.Height = 20.0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00012C7C File Offset: 0x00010E7C
		private void backButton_Click(object sender, RoutedEventArgs e)
		{
			if (this._editType == "create")
			{
				Program.GetInstance.DeleteTask(this._currentTask);
			}
			ItemCollection items = this.AccountsBox.Items;
			if (items != null)
			{
				items.Clear();
			}
			ItemCollection items2 = this.playersBox.Items;
			if (items2 != null)
			{
				items2.Clear();
			}
			ItemCollection items3 = this.playersBoxCaseFarm.Items;
			if (items3 != null)
			{
				items3.Clear();
			}
			Program.GetInstance.MainWindow.YESSS.Visibility = Visibility.Hidden;
			if (this._currentTask.SessionType.ToLower() != "transfer")
			{
				Program.GetInstance.MainWindow.frame.Content = Program.GetInstance.TaskPage.ReadyToSwap();
				Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Visible;
				Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Visible;
			}
			else
			{
				((TransferPageViewModel)TransferPage.Instance.DataContext).ReadyToSwap();
				Program.GetInstance.MainWindow.frame.Content = TransferPage.Instance;
				Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.ConfigTrigger.Visibility = Visibility.Hidden;
				Program.GetInstance.MainWindow.TransferTrigger.Visibility = Visibility.Visible;
			}
			if (Program.GetInstance.MainWindow.AccsButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.AccsButton.Opacity = 0.5;
			}
			if (Program.GetInstance.MainWindow.HomeButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.HomeButton.Opacity = 0.5;
			}
			if (Program.GetInstance.MainWindow.SettingsButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.SettingsButton.Opacity = 0.5;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00012FA4 File Offset: 0x000111A4
		public void CreatePages()
		{
			this.pages.Clear();
			AccountPage accountPage = null;
			accountPage = new AccountPage();
			foreach (AccountShow accountShow in StaticData.GetInstance.Players.Select((Player player) => player.PlayerShow))
			{
				if (!this.firstTeam.Contains(accountShow._currentPlayer.Login) && !this.secondTeam.Contains(accountShow._currentPlayer.Login))
				{
					AccountShow accountShow2 = accountShow;
					if (this._currentTask.SessionType.ToLower() == "transfer" && this._currentTask.SecondTeam.Contains(accountShow._currentPlayer))
					{
						accountShow.checkBox_0.IsChecked = new bool?(true);
					}
					accountPage.players.Add(accountShow2);
					if (accountPage.players.Count == 16)
					{
						this.pages.Add(accountPage);
						accountPage = new AccountPage();
					}
				}
			}
			if (accountPage.players.Count != 0)
			{
				this.pages.Add(accountPage);
			}
			this.current_page = 0;
			this.NavigateToPage();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00013110 File Offset: 0x00011310
		public void NavigateToPage()
		{
			if (this.pages.Count != 0)
			{
				this.AccountsBox.Items.Clear();
				foreach (AccountShow accountShow in this.pages[this.current_page].players)
				{
					this.AccountsBox.Items.Add(accountShow);
				}
				this.n_page.Content = string.Format("{0}/{1}", this.current_page + 1, this.pages.Count);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000131D4 File Offset: 0x000113D4
		private void rightPage_Click(object sender, RoutedEventArgs e)
		{
			if (this.current_page == this.pages.Count - 1)
			{
				this.current_page = 0;
			}
			else
			{
				this.current_page++;
			}
			this.NavigateToPage();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00013214 File Offset: 0x00011414
		private void leftPage_Click(object sender, RoutedEventArgs e)
		{
			if (this.current_page == 0)
			{
				this.current_page = this.pages.Count - 1;
			}
			else
			{
				this.current_page--;
			}
			this.NavigateToPage();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00013254 File Offset: 0x00011454
		private void rankImg_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.CurrentRank++;
			if (this.CurrentRank > 18)
			{
				this.CurrentRank = 0;
			}
			this.UpdateRankImage();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00013288 File Offset: 0x00011488
		private void TextBox_Drop(object sender, DragEventArgs e)
		{
			AccountShow accountShow = (AccountShow)e.Data.GetData(typeof(AccountShow));
			if (accountShow != null)
			{
				((TextBox)sender).Text = ((Player)accountShow.DataContext).Login;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000132D4 File Offset: 0x000114D4
		private void ListBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			if (this.taskPropertyThree.Content.ToString() != "Master token")
			{
				return;
			}
			ListBox listBox = (ListBox)sender;
			this.dragSource = listBox;
			AccountShow accountShow = (AccountShow)TaskCreationPage.GetDataFromListBox(this.dragSource, e.GetPosition(listBox));
			if (accountShow != null)
			{
				DragDrop.DoDragDrop(listBox, accountShow, DragDropEffects.Move);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00013340 File Offset: 0x00011540
		private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00013354 File Offset: 0x00011554
		private void Checked(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				((AccountShow)obj).EditCheck(true);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000133B8 File Offset: 0x000115B8
		private void Unchecked(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				((AccountShow)obj).EditCheck(false);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001341C File Offset: 0x0001161C
		private void checkbox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.IsChecked.Value)
			{
				foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
				{
					((AccountShow)obj).EditCheck(true);
				}
				checkBox.IsChecked = new bool?(true);
				return;
			}
			foreach (object obj2 in ((IEnumerable)this.AccountsBox.Items))
			{
				((AccountShow)obj2).EditCheck(false);
			}
			checkBox.IsChecked = new bool?(false);
		}

		// Token: 0x04000224 RID: 548
		private BoostTask _currentTask;

		// Token: 0x04000225 RID: 549
		private string _editType;

		// Token: 0x04000226 RID: 550
		private List<string> firstTeam = new List<string>();

		// Token: 0x04000227 RID: 551
		private List<string> secondTeam = new List<string>();

		// Token: 0x04000228 RID: 552
		private int activeTeam = 1;

		// Token: 0x04000229 RID: 553
		private ListBox dragSource;

		// Token: 0x0400022A RID: 554
		private int CurrentRank;

		// Token: 0x0400022B RID: 555
		public List<AccountPage> pages = new List<AccountPage>();

		// Token: 0x0400022C RID: 556
		public int current_page;
	}
}
