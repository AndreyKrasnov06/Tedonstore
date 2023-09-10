using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.Core.TaskList
{
	// Token: 0x020000EA RID: 234
	public static class BoostTaskQueue
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00003D4C File Offset: 0x00001F4C
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00020B00 File Offset: 0x0001ED00
		public static bool Started
		{
			get
			{
				return BoostTaskQueue._started;
			}
			set
			{
				BoostTaskQueue._started = value;
				if (!BoostTaskQueue._started)
				{
					BoostTaskQueue._stopBoostingQueue();
					return;
				}
				BoostTaskQueue._startBoostingQueue();
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00003D53 File Offset: 0x00001F53
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00003D5A File Offset: 0x00001F5A
		public static List<BoostTask> TaskQueue
		{
			get
			{
				return BoostTaskQueue._taskQueue;
			}
			set
			{
				BoostTaskQueue._taskQueue = value;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00020B28 File Offset: 0x0001ED28
		public static void AddTaskToQueue(BoostTask boostTask)
		{
			BoostTaskQueue._taskQueue.Add(boostTask);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00020B40 File Offset: 0x0001ED40
		public static void RemoveFromQueue(BoostTask boostTask)
		{
			BoostTaskQueue._taskQueue.Remove(boostTask);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00020B5C File Offset: 0x0001ED5C
		private static void _startBoostingQueue()
		{
			BoostTaskQueue._boostingQueueThread = new Thread(delegate
			{
				for (;;)
				{
					Thread.Sleep(1000);
					int num;
					try
					{
						num = Convert.ToInt32(Settings.GetInstance.LobbyAtTime);
						goto IL_95;
					}
					catch
					{
						num = 1;
						goto IL_95;
					}
					IL_24:
					if (BoostTaskQueue._currentBoostTasks.Count == num)
					{
						continue;
					}
					if (!BoostTaskQueue._taskQueue.All((BoostTask t) => t.Status == "Working"))
					{
						new Task(delegate
						{
							BoostTask boostTask = null;
							foreach (BoostTask boostTask2 in BoostTaskQueue._taskQueue)
							{
								if (boostTask2.Status != "Working")
								{
									boostTask = boostTask2;
									break;
								}
							}
							BoostTaskQueue._currentBoostTasks.Add(boostTask);
							boostTask.StartSession();
							boostTask.Session.MainTask.Wait();
							boostTask.Session.ClosePlayers();
							StaticData.GetInstance.taskNamesInQueue.Remove(boostTask.Name);
							Program.GetInstance.MainWindow.Dispatcher.Invoke(delegate
							{
								TasksPage taskPage = Program.GetInstance.TaskPage;
								if (taskPage != null)
								{
									taskPage.ReadyToSwap();
								}
								HomePage homePage = Program.GetInstance.HomePage;
								if (homePage == null)
								{
									return;
								}
								homePage.ReadyToSwap();
							});
							Thread.Sleep(10000);
							BoostTaskQueue._currentBoostTasks.Remove(boostTask);
							BoostTaskQueue._taskQueue.Remove(boostTask);
							Program.GetInstance.MainWindow.Dispatcher.Invoke(delegate
							{
								Program.GetInstance.HomePage.ReadyToSwap();
								Program.GetInstance.TaskPage.ReadyToSwap();
								if (BoostTaskQueue._taskQueue.Count == 0)
								{
									BoostTaskQueue.Started = false;
								}
							});
							GC.Collect();
						}).Start();
						Thread.Sleep(60000);
						continue;
					}
					continue;
					IL_95:
					if (BoostTaskQueue._taskQueue.Count<BoostTask>() != 0)
					{
						goto IL_24;
					}
				}
			});
			BoostTaskQueue._boostingQueueThread.Start();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00020B9C File Offset: 0x0001ED9C
		private static void _stopBoostingQueue()
		{
			foreach (BoostTask boostTask in BoostTaskQueue._currentBoostTasks)
			{
				if (boostTask != null)
				{
					boostTask.StopSession();
				}
			}
			BoostTaskQueue._currentBoostTasks.Clear();
			Thread boostingQueueThread = BoostTaskQueue._boostingQueueThread;
			if (boostingQueueThread == null)
			{
				return;
			}
			boostingQueueThread.Abort();
		}

		// Token: 0x04000481 RID: 1153
		private static Thread _boostingQueueThread;

		// Token: 0x04000482 RID: 1154
		private static List<BoostTask> _currentBoostTasks = new List<BoostTask>();

		// Token: 0x04000483 RID: 1155
		private static List<BoostTask> _taskQueue = new List<BoostTask>();

		// Token: 0x04000484 RID: 1156
		private static bool _started = false;
	}
}
