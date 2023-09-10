using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x02000069 RID: 105
	public partial class TasksPage : Page
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00013B5C File Offset: 0x00011D5C
		public TasksPage()
		{
			base.DataContext = this;
			Program.GetInstance.TaskPage = this;
			this.InitializeComponent();
			if (Program.GetInstance.SubscriptionLevel < 2)
			{
				this.startQueueButton.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000038E6 File Offset: 0x00001AE6
		public string Start
		{
			get
			{
				return "/Resources/img/start.png";
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000034CB File Offset: 0x000016CB
		public string Sessions
		{
			get
			{
				return "/Resources/img/sessions.png";
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003746 File Offset: 0x00001946
		public string Right
		{
			get
			{
				return "/Resources/img/right.png";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000374D File Offset: 0x0000194D
		public string Left
		{
			get
			{
				return "/Resources/img/left.png";
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Trash
		{
			get
			{
				return "/Resources/img/trash.png";
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000038ED File Offset: 0x00001AED
		public string Stop
		{
			get
			{
				return "/Resources/img/stop.png";
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00013BBC File Offset: 0x00011DBC
		public TasksPage ReadyToSwap()
		{
			this._getAndAlignTasks();
			if (this.tasksElements.Count == 0)
			{
				this.tasksElements.Add(new TaskPageElement("Win Boost"));
				this.tasksElements.Add(new TaskPageElement("XP Boost"));
				if (Program.GetInstance.SubscriptionLevel >= 2)
				{
					this.tasksElements.Add(new TaskPageElement("Client Boost"));
				}
				if (Program.GetInstance.SubscriptionLevel == 3)
				{
					this.tasksElements.Add(new TaskPageElement("Rank Boost"));
					this.tasksElements.Add(new TaskPageElement("fsm"));
					this.tasksElements.Add(new TaskPageElement("dm"));
				}
				this.tasksBox.Items.Clear();
				foreach (TaskPageElement taskPageElement in this.tasksElements)
				{
					this.tasksBox.Items.Add(taskPageElement);
				}
			}
			foreach (TaskPageElement taskPageElement2 in this.tasksElements)
			{
				taskPageElement2.GetAndAlignTasks();
			}
			return this;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D244 File Offset: 0x0000B444
		private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00013D20 File Offset: 0x00011F20
		private void _moveTaskToPosition(int position, BoostTask boostTask)
		{
			this.taskListGrid.Children.Add(boostTask.ViewInTaskList);
			if (position == 1)
			{
				boostTask.ViewInTaskList.Margin = new Thickness(29.0, 388.0, 490.0, 29.0);
				return;
			}
			if (position == 2)
			{
				boostTask.ViewInTaskList.Margin = new Thickness(182.0, 388.0, 338.0, 29.0);
				return;
			}
			if (position == 3)
			{
				boostTask.ViewInTaskList.Margin = new Thickness(335.0, 388.0, 184.0, 29.0);
				return;
			}
			if (position == 4)
			{
				boostTask.ViewInTaskList.Margin = new Thickness(488.0, 388.0, 32.0, 29.0);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00013E28 File Offset: 0x00012028
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
			if (this._tasks.Count - 1 >= this._currentIndexes.Item4)
			{
				this._moveTaskToPosition(4, this._tasks[this._currentIndexes.Item4]);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00013F2C File Offset: 0x0001212C
		private void add_accounts_Copy10_Click(object sender, RoutedEventArgs e)
		{
			if (this._currentIndexes.Item1 > 0)
			{
				this._currentIndexes.Item1 = this._currentIndexes.Item1 - 1;
				this._currentIndexes.Item2 = this._currentIndexes.Item2 - 1;
				this._currentIndexes.Item3 = this._currentIndexes.Item3 - 1;
				this._currentIndexes.Item4 = this._currentIndexes.Item4 - 1;
			}
			this._getAndAlignTasks();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00013F90 File Offset: 0x00012190
		private void add_accounts_Copy9_Click(object sender, RoutedEventArgs e)
		{
			if (this._tasks.Count - 2 >= this._currentIndexes.Item4)
			{
				this._currentIndexes.Item1 = this._currentIndexes.Item1 + 1;
				this._currentIndexes.Item2 = this._currentIndexes.Item2 + 1;
				this._currentIndexes.Item3 = this._currentIndexes.Item3 + 1;
				this._currentIndexes.Item4 = this._currentIndexes.Item4 + 1;
			}
			this._getAndAlignTasks();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00014000 File Offset: 0x00012200
		private void startQueueButton_Click(object sender, RoutedEventArgs e)
		{
			this.startQueueButton.Visibility = Visibility.Hidden;
			this.stopQueueButton.Visibility = Visibility.Visible;
			BoostTaskQueue.Started = true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001402C File Offset: 0x0001222C
		private void stopQueueButton_Click(object sender, RoutedEventArgs e)
		{
			this.startQueueButton.Visibility = Visibility.Visible;
			this.stopQueueButton.Visibility = Visibility.Hidden;
			BoostTaskQueue.Started = false;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00014058 File Offset: 0x00012258
		private void deleteCompletedTasks_Click(object sender, RoutedEventArgs e)
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

		// Token: 0x060002B7 RID: 695 RVA: 0x00014110 File Offset: 0x00012310
		public void StartQueue()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.startQueueButton.Visibility = Visibility.Hidden;
				this.stopQueueButton.Visibility = Visibility.Visible;
				BoostTaskQueue.Started = true;
			});
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00014134 File Offset: 0x00012334
		public void StopQueue()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.startQueueButton.Visibility = Visibility.Visible;
				this.stopQueueButton.Visibility = Visibility.Hidden;
				BoostTaskQueue.Started = false;
			});
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00014158 File Offset: 0x00012358
		public void ClearQueue()
		{
			base.Dispatcher.Invoke(delegate
			{
				foreach (BoostTask boostTask in StaticData.GetInstance.BoostTasks)
				{
					StaticData.GetInstance.taskNamesInQueue.Remove(boostTask.Name);
					BoostTaskQueue.RemoveFromQueue(boostTask);
					this.ReadyToSwap();
					HomePage homePage = Program.GetInstance.HomePage;
					if (homePage != null)
					{
						homePage.ReadyToSwap();
					}
				}
			});
		}

		// Token: 0x04000273 RID: 627
		private List<TaskPageElement> tasksElements = new List<TaskPageElement>();

		// Token: 0x04000274 RID: 628
		private ValueTuple<int, int, int, int> _currentIndexes = new ValueTuple<int, int, int, int>(0, 1, 2, 3);

		// Token: 0x04000275 RID: 629
		private List<BoostTask> _tasks;
	}
}
