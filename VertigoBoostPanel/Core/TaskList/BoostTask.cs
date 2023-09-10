using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.Sessions;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Model.enums;
using VertigoBoostPanel.Services.DataBase;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.Core.TaskList
{
	// Token: 0x020000E5 RID: 229
	public class BoostTask : IBoostTask, INotifyPropertyChanged
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001FF74 File Offset: 0x0001E174
		public BoostTask(SessionModel sessionModel)
		{
			this._currentModel = sessionModel;
			this.TimeRunning = "00:00:00";
			this._currentTaskListShow = new TaskListShow(this);
			this._currentTaskShow = new TaskShow(this);
			this._currentTaskOrderShow = new TaskOrderShow(this);
			this.SetStatus(TaskStatus.AFK);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000034D2 File Offset: 0x000016D2
		public string Settings
		{
			get
			{
				return "/Resources/img/settings.png";
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		public string XPBoost
		{
			get
			{
				if (this.SessionType.ToLower() == "xp boost")
				{
					return "/Resources/img/xpboost.png";
				}
				if (this.SessionType.ToLower() == "case farm")
				{
					return "/Resources/img/back12.png";
				}
				if (this.SessionType.ToLower() == "client boost")
				{
					return "/Resources/img/clientboost.png";
				}
				if (this.SessionType.ToLower() == "win boost")
				{
					return "/Resources/img/idle.png";
				}
				if (this.SessionType.ToLower() == "calibration")
				{
					return "/Resources/img/rankwinboost.png";
				}
				if (!(this.SessionType.ToLower() == "global boost"))
				{
					return "/Resources/img/xpboost.png";
				}
				return "/Resources/img/globalboost.png";
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00003914 File Offset: 0x00001B14
		public string Add
		{
			get
			{
				return "/Resources/img/add.png";
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00003C47 File Offset: 0x00001E47
		public string Cancel
		{
			get
			{
				return "/Resources/img/cancel.png";
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Trash
		{
			get
			{
				return "/Resources/img/trash.png";
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00003C4E File Offset: 0x00001E4E
		public string Back1
		{
			get
			{
				return "/Resources/img/back1.png";
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00003C55 File Offset: 0x00001E55
		public string Start
		{
			get
			{
				return "/Resources/img/playdefault.png";
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00003C5C File Offset: 0x00001E5C
		public string Stop
		{
			get
			{
				return "/Resources/img/stopdefault.png";
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00003C63 File Offset: 0x00001E63
		public string SessionType
		{
			get
			{
				return this._currentModel.TypeOfSession;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00003C70 File Offset: 0x00001E70
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00020088 File Offset: 0x0001E288
		public string Name
		{
			get
			{
				return this._currentModel.Name;
			}
			set
			{
				this._currentModel.Name = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("Name"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00003C7D File Offset: 0x00001E7D
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000200C4 File Offset: 0x0001E2C4
		public string GameMode
		{
			get
			{
				return this._currentModel.GameMode;
			}
			set
			{
				this._currentModel.GameMode = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("GameMode"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00003C8A File Offset: 0x00001E8A
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00020100 File Offset: 0x0001E300
		public string ClientInviteCode
		{
			get
			{
				return this._currentModel.ClientInviteCode;
			}
			set
			{
				this._currentModel.ClientInviteCode = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("ClientInviteCode"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00003C97 File Offset: 0x00001E97
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0002013C File Offset: 0x0001E33C
		public int NeedClientRank
		{
			get
			{
				return this._currentModel.NeedClientRank;
			}
			set
			{
				this._currentModel.NeedClientRank = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("NeedClientRank"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00020178 File Offset: 0x0001E378
		public int CountGame
		{
			get
			{
				return this._currentModel.CountGame;
			}
			set
			{
				this._currentModel.CountGame = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("CountGame"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00003CB1 File Offset: 0x00001EB1
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x000201B4 File Offset: 0x0001E3B4
		public bool ShortGame
		{
			get
			{
				return this._currentModel.ShortGame;
			}
			set
			{
				this._currentModel.ShortGame = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("ShortGame"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00003CBE File Offset: 0x00001EBE
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x000201F0 File Offset: 0x0001E3F0
		public bool MVP
		{
			get
			{
				return this._currentModel.MVP;
			}
			set
			{
				this._currentModel.MVP = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("MVP"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00003CCB File Offset: 0x00001ECB
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x0002022C File Offset: 0x0001E42C
		public bool SwapLeaders
		{
			get
			{
				return this._currentModel.SwapLeaders;
			}
			set
			{
				this._currentModel.SwapLeaders = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("SwapLeaders"));
				}
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00003CD8 File Offset: 0x00001ED8
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00020268 File Offset: 0x0001E468
		public string TimeRunning
		{
			get
			{
				return this._timeRunning;
			}
			set
			{
				this._timeRunning = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("TimeRunning"));
					return;
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00020298 File Offset: 0x0001E498
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00020340 File Offset: 0x0001E540
		public List<Player> FirstTeam
		{
			get
			{
				bool flag = false;
				List<Player> list = new List<Player>();
				if (this._currentModel.FirstTeam != null)
				{
					string[] array = this._currentModel.FirstTeam.Split(new char[] { ',' });
					for (int i = 0; i < array.Length; i++)
					{
						string login = array[i];
						if (login.Length > 0)
						{
							Player player = StaticData.GetInstance.Players.FirstOrDefault((Player acc) => acc.Login == login);
							if (player == null)
							{
								flag = true;
							}
							else
							{
								list.Add(player);
							}
						}
					}
				}
				if (flag)
				{
					this.FirstTeam = list;
				}
				return list;
			}
			set
			{
				this._currentModel.FirstTeam = string.Join(",", value.Select((Player acc) => acc.Login).ToArray<string>());
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00020394 File Offset: 0x0001E594
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0002043C File Offset: 0x0001E63C
		public List<Player> SecondTeam
		{
			get
			{
				bool flag = false;
				List<Player> list = new List<Player>();
				if (this._currentModel.SecondTeam != null)
				{
					string[] array = this._currentModel.SecondTeam.Split(new char[] { ',' });
					for (int i = 0; i < array.Length; i++)
					{
						string login = array[i];
						if (login.Length > 0)
						{
							Player player = StaticData.GetInstance.Players.FirstOrDefault((Player acc) => acc.Login == login);
							if (player == null)
							{
								flag = true;
							}
							else
							{
								list.Add(player);
							}
						}
					}
				}
				if (flag)
				{
					this.SecondTeam = list;
				}
				return list;
			}
			set
			{
				this._currentModel.SecondTeam = string.Join(",", value.Select((Player acc) => acc.Login).ToArray<string>());
				this.SaveTaskToDataBase();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public SessionModel TaskModel
		{
			get
			{
				return this._currentModel;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00020490 File Offset: 0x0001E690
		public string Status
		{
			get
			{
				if (this._status == TaskStatus.AFK)
				{
					return "Waiting";
				}
				if (this._status == TaskStatus.RUNNING)
				{
					return "Working";
				}
				if (this._status == TaskStatus.COMPLETED)
				{
					return "Finished";
				}
				return "undefinded";
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public bool SettingsAviable
		{
			get
			{
				return this._status != TaskStatus.RUNNING;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00003CF6 File Offset: 0x00001EF6
		public Session Session
		{
			get
			{
				return this._currentSession;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00003CFE File Offset: 0x00001EFE
		public TaskListShow View
		{
			get
			{
				return this._currentTaskListShow;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00003D06 File Offset: 0x00001F06
		public TaskOrderShow ViewInTaskList
		{
			get
			{
				return this._currentTaskOrderShow;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00003D0E File Offset: 0x00001F0E
		public TaskShow TaskShow_0
		{
			get
			{
				return this._currentTaskShow;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004D9 RID: 1241 RVA: 0x000204D0 File Offset: 0x0001E6D0
		// (remove) Token: 0x060004DA RID: 1242 RVA: 0x00020510 File Offset: 0x0001E710
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004DB RID: 1243 RVA: 0x00020550 File Offset: 0x0001E750
		public void StartSession()
		{
			this._currentSession = new Session(this);
			this._currentSession.StartBoosting();
			this.SetStatus(TaskStatus.RUNNING);
			this._timeStarted = DateTime.Now;
			TimerCallback timerCallback = new TimerCallback(this._timeTick);
			this.timer = new Timer(timerCallback, null, 0, 1000);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000205A8 File Offset: 0x0001E7A8
		private void _timeTick(object state)
		{
			TimeSpan timeSpan = DateTime.Now - this._timeStarted;
			string text = timeSpan.Hours.ToString();
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			string text2 = timeSpan.Minutes.ToString();
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			string text3 = timeSpan.Seconds.ToString();
			if (text3.Length == 1)
			{
				text3 = "0" + text3;
			}
			this.TimeRunning = string.Concat(new string[] { text, ":", text2, ":", text3 });
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00020678 File Offset: 0x0001E878
		public void StopSession()
		{
			Timer timer = this.timer;
			if (timer != null)
			{
				timer.Dispose();
			}
			Session currentSession = this._currentSession;
			if (currentSession != null)
			{
				currentSession.StopBoosting();
			}
			this.SetStatus(TaskStatus.AFK);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000206B0 File Offset: 0x0001E8B0
		public void AddTaskToQueue()
		{
			if (StaticData.GetInstance.taskNamesInQueue.Contains(this.Name))
			{
				CreateError.NewError("Task already added", false);
				return;
			}
			if (this.SessionType.ToLower() == "case farm")
			{
				if (this.FirstTeam.Any((Player x) => (DateTime.Now - x.LastDrop).TotalDays < 7.0))
				{
					Program.GetInstance.MainWindow.ShowCaseFarmConfirmation(this);
					return;
				}
			}
			StaticData.GetInstance.taskNamesInQueue.Add(this.Name);
			BoostTaskQueue.AddTaskToQueue(this);
			TasksPage taskPage = Program.GetInstance.TaskPage;
			if (taskPage == null)
			{
				return;
			}
			taskPage.ReadyToSwap();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00020768 File Offset: 0x0001E968
		public void OpenEditTask(string type)
		{
			Program.GetInstance.MainWindow.frame.Content = new TaskCreationPage(this, type);
			Program.GetInstance.MainWindow.accpage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.homepage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.settingspage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.taskspage_hui.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.AccsTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.HomeTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.SettingsTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.TasksTrigger.Visibility = Visibility.Hidden;
			Program.GetInstance.MainWindow.TransferTrigger.Visibility = Visibility.Hidden;
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
			if (Program.GetInstance.MainWindow.TasksButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.TasksButton.Opacity = 0.5;
			}
			if (Program.GetInstance.MainWindow.TransferButton.Opacity == 1.0)
			{
				Program.GetInstance.MainWindow.TransferButton.Opacity = 0.5;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00003D16 File Offset: 0x00001F16
		private void SaveTaskToDataBase()
		{
			DataBaseInteraction.UpdateTask(this);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0002097C File Offset: 0x0001EB7C
		public void SetStatus(TaskStatus status)
		{
			this._status = status;
			if (status == TaskStatus.COMPLETED)
			{
				try
				{
					this.StopSession();
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
			Program.GetInstance.MainWindow.Dispatcher.Invoke(delegate
			{
				if (status == TaskStatus.AFK)
				{
					this._currentTaskListShow.StatusBorder.BorderBrush = Brushes.Gray;
				}
				if (status == TaskStatus.RUNNING)
				{
					this._currentTaskListShow.StatusBorder.BorderBrush = Brushes.Yellow;
				}
				if (status == TaskStatus.COMPLETED)
				{
					this._currentTaskListShow.StatusBorder.BorderBrush = Brushes.Green;
				}
			});
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs("Status"));
			}
			PropertyChangedEventHandler propertyChanged2 = this.PropertyChanged;
			if (propertyChanged2 != null)
			{
				propertyChanged2(this, new PropertyChangedEventArgs("SettingsAviable"));
				return;
			}
		}

		// Token: 0x0400046D RID: 1133
		private SessionModel _currentModel;

		// Token: 0x0400046E RID: 1134
		public int CaseFarmType;

		// Token: 0x0400046F RID: 1135
		private string _timeRunning;

		// Token: 0x04000470 RID: 1136
		public bool Finished;

		// Token: 0x04000472 RID: 1138
		private Session _currentSession;

		// Token: 0x04000473 RID: 1139
		private TaskOrderShow _currentTaskOrderShow;

		// Token: 0x04000474 RID: 1140
		private TaskShow _currentTaskShow;

		// Token: 0x04000475 RID: 1141
		private TaskListShow _currentTaskListShow;

		// Token: 0x04000476 RID: 1142
		private TaskStatus _status;

		// Token: 0x04000477 RID: 1143
		private Timer timer;

		// Token: 0x04000478 RID: 1144
		private DateTime _timeStarted;
	}
}
