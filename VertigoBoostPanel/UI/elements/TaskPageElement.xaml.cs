using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000074 RID: 116
	public partial class TaskPageElement : UserControl, INotifyPropertyChanged
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00015EB4 File Offset: 0x000140B4
		public TaskPageElement(string title)
		{
			base.DataContext = this;
			this.InitializeComponent();
			this.Title = title;
			ResourceDictionary resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
			};
			if (resourceDictionary != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(title);
				if (num <= 1679612730U)
				{
					if (num > 1201894158U)
					{
						if (num != 1292627586U)
						{
							if (num == 1679612730U)
							{
								if (title == "dm")
								{
									this.UITitle = "DM";
								}
							}
						}
						else if (title == "XP Boost")
						{
							this.UITitle = resourceDictionary["m_XPBoost"] as string;
						}
					}
					else if (num != 2289049U)
					{
						if (num == 1201894158U)
						{
							if (title == "Win Boost")
							{
								this.UITitle = resourceDictionary["m_WinBoost"] as string;
							}
						}
					}
					else if (title == "Client Boost")
					{
						this.UITitle = resourceDictionary["m_ClientBoost"] as string;
					}
				}
				else if (num <= 2252935020U)
				{
					if (num != 1751856583U)
					{
						if (num == 2252935020U)
						{
							if (title == "Rank Boost")
							{
								this.UITitle = resourceDictionary["m_RankBoost"] as string;
							}
						}
					}
					else if (title == "Calibration")
					{
						this.UITitle = resourceDictionary["m_Calibration"] as string;
					}
				}
				else if (num != 2907478575U)
				{
					if (num != 3419774793U)
					{
						if (num == 3800030249U)
						{
							if (title == "Global Boost")
							{
								this.UITitle = resourceDictionary["m_GlobalBoost"] as string;
							}
						}
					}
					else if (title == "fsm")
					{
						this.UITitle = "FSM";
					}
				}
				else if (title == "Case Farm")
				{
					this.UITitle = resourceDictionary["m_CaseFarm"] as string;
				}
			}
			if (title == "Case Farm")
			{
				this.AutoCreateSessions.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003914 File Offset: 0x00001B14
		public string Add
		{
			get
			{
				return "/Resources/img/add.png";
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000374D File Offset: 0x0000194D
		public string Left
		{
			get
			{
				return "/Resources/img/left.png";
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00003746 File Offset: 0x00001946
		public string Right
		{
			get
			{
				return "/Resources/img/right.png";
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600030F RID: 783 RVA: 0x00016120 File Offset: 0x00014320
		// (remove) Token: 0x06000310 RID: 784 RVA: 0x00016160 File Offset: 0x00014360
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000391B File Offset: 0x00001B1B
		// (set) Token: 0x06000312 RID: 786 RVA: 0x000161A0 File Offset: 0x000143A0
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00003923 File Offset: 0x00001B23
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000161D0 File Offset: 0x000143D0
		public string UITitle
		{
			get
			{
				return this._uiTitle;
			}
			set
			{
				this._uiTitle = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("UITitle"));
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00016200 File Offset: 0x00014400
		private void Button_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = sender as Button;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 0.75;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00016238 File Offset: 0x00014438
		private void Button_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = sender as Button;
			if (button.Opacity == 0.75)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00016270 File Offset: 0x00014470
		private void createSession_Click(object sender, RoutedEventArgs e)
		{
			BoostTask boostTask = new BoostTask(new SessionModel
			{
				TypeOfSession = this.Title
			});
			Program.GetInstance.AddNewTask(boostTask);
			boostTask.OpenEditTask("create");
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000162B0 File Offset: 0x000144B0
		private void swapRight_Click(object sender, RoutedEventArgs e)
		{
			if (this._tasksBoost.ToList<IBoostTask>().Count - 2 >= this._currentBoostIndexes.Item4)
			{
				this._currentBoostIndexes.Item1 = this._currentBoostIndexes.Item1 + 1;
				this._currentBoostIndexes.Item2 = this._currentBoostIndexes.Item2 + 1;
				this._currentBoostIndexes.Item3 = this._currentBoostIndexes.Item3 + 1;
				this._currentBoostIndexes.Item4 = this._currentBoostIndexes.Item4 + 1;
			}
			this.GetAndAlignTasks();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00016324 File Offset: 0x00014524
		private void swapLeft_Click(object sender, RoutedEventArgs e)
		{
			if (this._currentBoostIndexes.Item1 > 0)
			{
				this._currentBoostIndexes.Item1 = this._currentBoostIndexes.Item1 - 1;
				this._currentBoostIndexes.Item2 = this._currentBoostIndexes.Item2 - 1;
				this._currentBoostIndexes.Item3 = this._currentBoostIndexes.Item3 - 1;
				this._currentBoostIndexes.Item4 = this._currentBoostIndexes.Item4 - 1;
			}
			this.GetAndAlignTasks();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00016388 File Offset: 0x00014588
		private void _moveRankTaskToPosition(int position, IBoostTask boostTask)
		{
			this.tasksGrid.Children.Add(boostTask.TaskShow_0);
			if (position == 1)
			{
				boostTask.TaskShow_0.Margin = new Thickness(10.0, 0.0, 471.0, 0.0);
				return;
			}
			if (position == 2)
			{
				boostTask.TaskShow_0.Margin = new Thickness(163.0, 0.0, 318.0, 0.0);
				return;
			}
			if (position == 3)
			{
				boostTask.TaskShow_0.Margin = new Thickness(316.0, 0.0, 165.0, 0.0);
				return;
			}
			if (position == 4)
			{
				boostTask.TaskShow_0.Margin = new Thickness(469.0, 0.0, 13.0, 0.0);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00016490 File Offset: 0x00014690
		public TaskPageElement GetAndAlignTasks()
		{
			this._tasksBoost = StaticData.GetInstance.BoostTasks.Where((BoostTask task) => task.SessionType.ToLower() == this.Title.ToLower()).ToList<BoostTask>();
			this.tasksGrid.Children.Clear();
			if (this._tasksBoost.ToList<IBoostTask>().Count - 1 >= this._currentBoostIndexes.Item1)
			{
				this._moveRankTaskToPosition(1, this._tasksBoost.ToList<IBoostTask>()[this._currentBoostIndexes.Item1]);
			}
			if (this._tasksBoost.ToList<IBoostTask>().Count - 1 >= this._currentBoostIndexes.Item2)
			{
				this._moveRankTaskToPosition(2, this._tasksBoost.ToList<IBoostTask>()[this._currentBoostIndexes.Item2]);
			}
			if (this._tasksBoost.ToList<IBoostTask>().Count - 1 >= this._currentBoostIndexes.Item3)
			{
				this._moveRankTaskToPosition(3, this._tasksBoost.ToList<IBoostTask>()[this._currentBoostIndexes.Item3]);
			}
			if (this._tasksBoost.ToList<IBoostTask>().Count - 1 >= this._currentBoostIndexes.Item4)
			{
				this._moveRankTaskToPosition(4, this._tasksBoost.ToList<IBoostTask>()[this._currentBoostIndexes.Item4]);
			}
			return this;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000165D8 File Offset: 0x000147D8
		private void AutoCreateSessions_Click(object sender, RoutedEventArgs e)
		{
			int num;
			if (!int.TryParse(Interaction.InputBox("How many accounts in each session?", "VertigoBoostPanel", "", -1, -1), out num))
			{
				CreateError.NewError("Wrong count", false);
				return;
			}
			int num2 = 1;
			List<Player> list = new List<Player>();
			foreach (Player player in StaticData.GetInstance.Players)
			{
				if (list.Count == num)
				{
					BoostTask boostTask = new BoostTask(new SessionModel
					{
						TypeOfSession = this.Title
					});
					boostTask.Name = string.Format("#{0}", num2);
					num2++;
					Program.GetInstance.AddNewTask(boostTask);
					boostTask.FirstTeam = list;
					list = new List<Player>();
				}
				list.Add(player);
			}
			if (list.Count == num)
			{
				BoostTask boostTask2 = new BoostTask(new SessionModel
				{
					TypeOfSession = this.Title
				});
				boostTask2.Name = string.Format("#{0}", num2);
				num2++;
				Program.GetInstance.AddNewTask(boostTask2);
				boostTask2.FirstTeam = list;
				list.Clear();
			}
			Program.GetInstance.TaskPage.ReadyToSwap();
		}

		// Token: 0x040002C2 RID: 706
		private string _title;

		// Token: 0x040002C3 RID: 707
		private string _uiTitle;

		// Token: 0x040002C4 RID: 708
		private ValueTuple<int, int, int, int> _currentBoostIndexes = new ValueTuple<int, int, int, int>(0, 1, 2, 3);

		// Token: 0x040002C5 RID: 709
		private IEnumerable<IBoostTask> _tasksBoost;
	}
}
