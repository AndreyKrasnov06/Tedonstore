using System;
using System.Collections.Generic;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.Core.BoostLogs
{
	// Token: 0x02000131 RID: 305
	public static class Logs
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0002FEB0 File Offset: 0x0002E0B0
		public static void AddNewLog(BoostLog boostLog)
		{
			Logs.logs.Insert(0, boostLog);
			Logs._updateLastLog();
			HomePage homePage = Program.GetInstance.HomePage;
			if (homePage != null)
			{
				homePage.Dispatcher.Invoke<HomePage>(delegate
				{
					HomePage homePage2 = Program.GetInstance.HomePage;
					if (homePage2 == null)
					{
						return null;
					}
					return homePage2.ReadyToSwap();
				});
				return;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002FF08 File Offset: 0x0002E108
		public static void ClearLogs()
		{
			Logs.logs.Clear();
			HomePage homePage = Program.GetInstance.HomePage;
			if (homePage != null)
			{
				homePage.Dispatcher.Invoke<HomePage>(delegate
				{
					HomePage homePage2 = Program.GetInstance.HomePage;
					if (homePage2 == null)
					{
						return null;
					}
					return homePage2.ReadyToSwap();
				});
				return;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002FF58 File Offset: 0x0002E158
		private static void _updateLastLog()
		{
			foreach (BoostLog boostLog in Logs.logs)
			{
				boostLog.SetLastLogLine(false);
			}
			if (Logs.logs.Count > 0)
			{
				Logs.logs[0].SetLastLogLine(true);
			}
		}

		// Token: 0x040005FB RID: 1531
		public static List<BoostLog> logs = new List<BoostLog>();
	}
}
