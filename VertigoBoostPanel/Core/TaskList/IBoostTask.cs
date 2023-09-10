using System;
using VertigoBoostPanel.Core.Sessions;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.Core.TaskList
{
	// Token: 0x020000EC RID: 236
	internal interface IBoostTask
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004FD RID: 1277
		Session Session { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004FE RID: 1278
		TaskListShow View { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004FF RID: 1279
		TaskOrderShow ViewInTaskList { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000500 RID: 1280
		TaskShow TaskShow_0 { get; }
	}
}
