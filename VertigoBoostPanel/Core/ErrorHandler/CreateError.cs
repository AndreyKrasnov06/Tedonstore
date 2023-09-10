using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using VertigoBoostPanel.UI.Pages;

namespace VertigoBoostPanel.Core.ErrorHandler
{
	// Token: 0x0200012D RID: 301
	public static class CreateError
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0002FC54 File Offset: 0x0002DE54
		public static bool NewError(string errorText, bool wait = false)
		{
			if (Program.GetInstance.MainWindow != null)
			{
				Action <>9__1;
				TaskQueue.Queue.Add(new Task(delegate
				{
					MainWindow mainWindow = Program.GetInstance.MainWindow;
					if (mainWindow != null)
					{
						Dispatcher dispatcher = mainWindow.Dispatcher;
						Action action;
						if ((action = <>9__1) == null)
						{
							action = (<>9__1 = delegate
							{
								MainWindow mainWindow3 = Program.GetInstance.MainWindow;
								if (mainWindow3 == null)
								{
									return;
								}
								mainWindow3.CreateErrorMessage(errorText);
							});
						}
						dispatcher.Invoke(action);
					}
					Thread.Sleep(5000);
					if (!wait)
					{
						MainWindow mainWindow2 = Program.GetInstance.MainWindow;
						if (mainWindow2 == null)
						{
							return;
						}
						mainWindow2.Dispatcher.Invoke(delegate
						{
							MainWindow mainWindow4 = Program.GetInstance.MainWindow;
							if (mainWindow4 == null)
							{
								return;
							}
							mainWindow4.errorGrid.Children.Clear();
						});
					}
				}));
				return true;
			}
			return false;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
		public static bool NewNotification(string text, bool wait = false)
		{
			if (Program.GetInstance.MainWindow != null)
			{
				Action <>9__1;
				TaskQueue.Queue.Add(new Task(delegate
				{
					MainWindow mainWindow = Program.GetInstance.MainWindow;
					if (mainWindow != null)
					{
						Dispatcher dispatcher = mainWindow.Dispatcher;
						Action action;
						if ((action = <>9__1) == null)
						{
							action = (<>9__1 = delegate
							{
								MainWindow mainWindow3 = Program.GetInstance.MainWindow;
								if (mainWindow3 != null)
								{
									mainWindow3.CreateMessage(text);
									return;
								}
							});
						}
						dispatcher.Invoke(action);
					}
					Thread.Sleep(5000);
					if (!wait)
					{
						MainWindow mainWindow2 = Program.GetInstance.MainWindow;
						if (mainWindow2 == null)
						{
							return;
						}
						mainWindow2.Dispatcher.Invoke(delegate
						{
							MainWindow mainWindow4 = Program.GetInstance.MainWindow;
							if (mainWindow4 == null)
							{
								return;
							}
							mainWindow4.errorGrid.Children.Clear();
						});
					}
				}));
				return true;
			}
			return false;
		}
	}
}
