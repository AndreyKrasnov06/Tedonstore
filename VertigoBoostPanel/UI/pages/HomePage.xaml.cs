using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.BoostLogs;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x02000051 RID: 81
	public partial class HomePage : Page, INotifyPropertyChanged
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public HomePage()
		{
			base.DataContext = this;
			this.InitializeComponent();
			Program.GetInstance.HomePage = this;
			this._getAndAlignTasks();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00003746 File Offset: 0x00001946
		public string Right
		{
			get
			{
				return "/Resources/img/right.png";
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000374D File Offset: 0x0000194D
		public string Left
		{
			get
			{
				return "/Resources/img/left.png";
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Trash
		{
			get
			{
				return "/Resources/img/trash.png";
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000034D2 File Offset: 0x000016D2
		public string SettingsLogo
		{
			get
			{
				return "/Resources/img/settings.png";
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000034BD File Offset: 0x000016BD
		public string Home
		{
			get
			{
				return "/Resources/img/home.png";
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public string countFinishedTasks
		{
			get
			{
				return Settings.GetInstance.CompletedSessions.ToString();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public string countTasksInQueue
		{
			get
			{
				return BoostTaskQueue.TaskQueue.Count.ToString();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000EC40 File Offset: 0x0000CE40
		public string countAllTasks
		{
			get
			{
				return StaticData.GetInstance.BoostTasks.Count.ToString();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public string TotalAccounts
		{
			get
			{
				return StaticData.GetInstance.Players.Count.ToString();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public string PrimeAccounts
		{
			get
			{
				return StaticData.GetInstance.Players.Where((Player pl) => pl.Prime == 1).Count<Player>().ToString();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public string NonPrimeAccounts
		{
			get
			{
				return StaticData.GetInstance.Players.Where((Player pl) => pl.Prime == 0).Count<Player>().ToString();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000ED28 File Offset: 0x0000CF28
		public string RankedAccounts
		{
			get
			{
				return StaticData.GetInstance.Players.Where((Player pl) => pl.Rank2x2 != 0 || pl.Rank5x5 != 0).Count<Player>().ToString();
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600020D RID: 525 RVA: 0x0000ED74 File Offset: 0x0000CF74
		// (remove) Token: 0x0600020E RID: 526 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600020F RID: 527 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public HomePage ReadyToSwap()
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs("TotalAccounts"));
			}
			PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
			if (propertyChanged2 != null)
			{
				propertyChanged2(this, new PropertyChangedEventArgs("PrimeAccounts"));
			}
			PropertyChangedEventHandler propertyChanged3 = this.PropertyChanged;
			if (propertyChanged3 != null)
			{
				propertyChanged3(this, new PropertyChangedEventArgs("NonPrimeAccounts"));
			}
			PropertyChangedEventHandler propertyChanged4 = this.PropertyChanged;
			if (propertyChanged4 != null)
			{
				propertyChanged4(this, new PropertyChangedEventArgs("RankedAccounts"));
			}
			PropertyChangedEventHandler propertyChanged5 = this.PropertyChanged;
			if (propertyChanged5 != null)
			{
				propertyChanged5(this, new PropertyChangedEventArgs("countFinishedTasks"));
			}
			PropertyChangedEventHandler propertyChanged6 = this.PropertyChanged;
			if (propertyChanged6 != null)
			{
				propertyChanged6(this, new PropertyChangedEventArgs("countTasksInQueue"));
			}
			PropertyChangedEventHandler propertyChanged7 = this.PropertyChanged;
			if (propertyChanged7 != null)
			{
				propertyChanged7(this, new PropertyChangedEventArgs("countAllTasks"));
			}
			this._getAndAlignTasks();
			this.boostLogsBox.Items.Clear();
			foreach (BoostLog boostLog in Logs.logs)
			{
				this.boostLogsBox.Items.Add(boostLog.View);
			}
			return this;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D244 File Offset: 0x0000B444
		private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000EF38 File Offset: 0x0000D138
		private void DeleteButton_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?((double)1f);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.ClearNotification.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		private void DeleteButton_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?((double)1f);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.ClearNotification.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F068 File Offset: 0x0000D268
		private void SetupButton_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?((double)1f);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.SetupNotification.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000F100 File Offset: 0x0000D300
		private void SetupButton_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?((double)1f);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.SetupNotification.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000F198 File Offset: 0x0000D398
		private void TaskPage_Click(object sender, RoutedEventArgs e)
		{
			if (Program.GetInstance.MainWindow.tasksPage == null)
			{
				Program.GetInstance.MainWindow.tasksPage = new TasksPage();
			}
			Program.GetInstance.MainWindow.frame.Content = Program.GetInstance.MainWindow.tasksPage.ReadyToSwap();
			Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Visible;
			Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Visible;
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

		// Token: 0x06000217 RID: 535 RVA: 0x0000F350 File Offset: 0x0000D550
		private void _moveTaskToPosition(int position, BoostTask boostTask)
		{
			this.taskListGrid.Children.Add(boostTask.View);
			if (position == 1)
			{
				boostTask.View.Margin = new Thickness(10.0, 38.0, 423.0, 10.0);
				return;
			}
			if (position == 2)
			{
				boostTask.View.Margin = new Thickness(216.0, 38.0, 217.0, 10.0);
				return;
			}
			if (position == 3)
			{
				boostTask.View.Margin = new Thickness(423.0, 38.0, 10.0, 10.0);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000F420 File Offset: 0x0000D620
		private void _getAndAlignTasks()
		{
			this._tasks = BoostTaskQueue.TaskQueue;
			this.taskListGrid.Children.Clear();
			if (this._tasks.Count - 1 >= this._currentIndexes.Item1)
			{
				this._moveTaskToPosition(1, this._tasks[this._currentIndexes.Item1]);
			}
			if (this._tasks.Count - 1 >= this._currentIndexes.Item2)
			{
				this._moveTaskToPosition(2, this._tasks[this._currentIndexes.Item2]);
			}
			if (this._tasks.Count - 1 >= this._currentIndexes.Item3)
			{
				this._moveTaskToPosition(3, this._tasks[this._currentIndexes.Item3]);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void add_accounts_Copy6_Click(object sender, RoutedEventArgs e)
		{
			if (this._tasks.Count - 2 >= this._currentIndexes.Item3)
			{
				this._currentIndexes.Item1 = this._currentIndexes.Item1 + 1;
				this._currentIndexes.Item2 = this._currentIndexes.Item2 + 1;
				this._currentIndexes.Item3 = this._currentIndexes.Item3 + 1;
			}
			this._getAndAlignTasks();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000F550 File Offset: 0x0000D750
		private void add_accounts_Copy7_Click(object sender, RoutedEventArgs e)
		{
			if (this._currentIndexes.Item1 > 0)
			{
				this._currentIndexes.Item1 = this._currentIndexes.Item1 - 1;
				this._currentIndexes.Item2 = this._currentIndexes.Item2 - 1;
				this._currentIndexes.Item3 = this._currentIndexes.Item3 - 1;
			}
			this._getAndAlignTasks();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		private void removeFinishedTasks_Click(object sender, RoutedEventArgs e)
		{
			List<BoostTask> list = new List<BoostTask>();
			foreach (BoostTask boostTask in BoostTaskQueue.TaskQueue)
			{
				if (boostTask.Finished)
				{
					list.Add(boostTask);
				}
			}
			foreach (BoostTask boostTask2 in list)
			{
				BoostTaskQueue.TaskQueue.Remove(boostTask2);
			}
			this.ReadyToSwap();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00003754 File Offset: 0x00001954
		private void removeLogs_Click(object sender, RoutedEventArgs e)
		{
			Logs.ClearLogs();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D244 File Offset: 0x0000B444
		private void removeLogs_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void removeLogs_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x040001C7 RID: 455
		private ValueTuple<int, int, int> _currentIndexes = new ValueTuple<int, int, int>(0, 1, 2);

		// Token: 0x040001C8 RID: 456
		private List<BoostTask> _tasks;
	}
}
