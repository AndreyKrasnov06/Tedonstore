using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VertigoBoostPanel.Services.Files;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000DB RID: 219
	public class TaskQueue
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x0001F57C File Offset: 0x0001D77C
		public static void StartTaskQueue()
		{
			new Thread(delegate
			{
				for (;;)
				{
					foreach (Task task in TaskQueue.Queue)
					{
						try
						{
							task.Start();
							task.Wait();
							TaskQueue.Queue.Remove(task);
							break;
						}
						catch (Exception ex)
						{
							LogService.GetInstance.LogInformation(ex.ToString());
						}
					}
					Thread.Sleep(50);
				}
			}).Start();
		}

		// Token: 0x04000450 RID: 1104
		public static List<Task> Queue = new List<Task>();
	}
}
