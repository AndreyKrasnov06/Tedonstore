using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Services.Relay;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.UI.ViewModels.Pages
{
	// Token: 0x02000049 RID: 73
	public class TransferPageViewModel
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000C52C File Offset: 0x0000A72C
		public TransferPageViewModel(Page page)
		{
			this._view = (TransferPage)page;
			TransferPageViewModel.Instance = this;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000362C File Offset: 0x0000182C
		public TransferPage View
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00003634 File Offset: 0x00001834
		public List<BoostTask> TransferTasks
		{
			get
			{
				return StaticData.GetInstance.BoostTasks.Where((BoostTask task) => task.SessionType.ToLower() == "transfer").ToList<BoostTask>();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003669 File Offset: 0x00001869
		public Command pageLeft
		{
			get
			{
				return new Command(delegate(object obj)
				{
					if (this._currentTaskIndexes.Item1 > 0)
					{
						this._currentTaskIndexes.Item1 = this._currentTaskIndexes.Item1 - 1;
						this._currentTaskIndexes.Item2 = this._currentTaskIndexes.Item2 - 1;
						this._currentTaskIndexes.Item3 = this._currentTaskIndexes.Item3 - 1;
						this._currentTaskIndexes.Item4 = this._currentTaskIndexes.Item4 - 1;
						this._currentTaskIndexes.Item5 = this._currentTaskIndexes.Item5 - 1;
						this._currentTaskIndexes.Item6 = this._currentTaskIndexes.Item6 - 1;
						this._currentTaskIndexes.Item7 = this._currentTaskIndexes.Item7 - 1;
						this._currentTaskIndexes.Rest.Item1 = this._currentTaskIndexes.Rest.Item1 - 1;
					}
					this.GetAndAlignTasks();
				}, null);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000367D File Offset: 0x0000187D
		public Command pageRight
		{
			get
			{
				return new Command(delegate(object obj)
				{
					if (this.TransferTasks.Count - 2 >= this._currentTaskIndexes.Rest.Item1)
					{
						this._currentTaskIndexes.Item1 = this._currentTaskIndexes.Item1 + 1;
						this._currentTaskIndexes.Item2 = this._currentTaskIndexes.Item2 + 1;
						this._currentTaskIndexes.Item3 = this._currentTaskIndexes.Item3 + 1;
						this._currentTaskIndexes.Item4 = this._currentTaskIndexes.Item4 + 1;
						this._currentTaskIndexes.Item5 = this._currentTaskIndexes.Item5 + 1;
						this._currentTaskIndexes.Item6 = this._currentTaskIndexes.Item6 + 1;
						this._currentTaskIndexes.Item7 = this._currentTaskIndexes.Item7 + 1;
						this._currentTaskIndexes.Rest.Item1 = this._currentTaskIndexes.Rest.Item1 + 1;
					}
					this.GetAndAlignTasks();
				}, null);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000C554 File Offset: 0x0000A754
		public Command CreateTask
		{
			get
			{
				return new Command(delegate(object obj)
				{
					BoostTask boostTask = new BoostTask(new SessionModel
					{
						TypeOfSession = "transfer"
					});
					if (Program.GetInstance.AddNewTask(boostTask))
					{
						boostTask.OpenEditTask("create");
					}
				}, null);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C588 File Offset: 0x0000A788
		private void GetAndAlignTasks()
		{
			this.View.TasksGrid.Children.Clear();
			int num = this._currentTaskIndexes.Item1;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(12.0, 31.0, 471.0, 62.0);
			}
			num = this._currentTaskIndexes.Item2;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(166.0, 31.0, 317.0, 62.0);
			}
			num = this._currentTaskIndexes.Item3;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(319.0, 31.0, 164.0, 62.0);
			}
			num = this._currentTaskIndexes.Item4;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(473.0, 31.0, 10.0, 62.0);
			}
			num = this._currentTaskIndexes.Item5;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(12.0, 82.0, 471.0, 11.0);
			}
			num = this._currentTaskIndexes.Item6;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(166.0, 82.0, 317.0, 11.0);
			}
			num = this._currentTaskIndexes.Item7;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(319.0, 82.0, 164.0, 11.0);
			}
			num = this._currentTaskIndexes.Rest.Item1;
			if (this.TransferTasks.Count >= num + 1)
			{
				this.View.TasksGrid.Children.Add(this.TransferTasks[num].ViewInTaskList);
				this.TransferTasks[num].ViewInTaskList.Margin = new Thickness(473.0, 82.0, 10.0, 11.0);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public void ReadyToSwap()
		{
			this._currentTaskIndexes = new ValueTuple<int, int, int, int, int, int, int, ValueTuple<int>>(0, 1, 2, 3, 4, 5, 6, new ValueTuple<int>(7));
			this.GetAndAlignTasks();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		public void AddNewLog(string name, string login)
		{
			this._view.Dispatcher.Invoke(delegate
			{
				this._view.transferLogsBox.Items.Insert(0, new TransferLogView(name, login));
			});
		}

		// Token: 0x04000160 RID: 352
		private TransferPage _view;

		// Token: 0x04000161 RID: 353
		public static TransferPageViewModel Instance;

		// Token: 0x04000162 RID: 354
		private ValueTuple<int, int, int, int, int, int, int, ValueTuple<int>> _currentTaskIndexes;
	}
}
