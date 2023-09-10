using System;
using System.Collections.Generic;

namespace VertigoBoostPanel.Services.SteamRouteTool
{
	// Token: 0x02000082 RID: 130
	public class Route
	{
		// Token: 0x040002F1 RID: 753
		public string name;

		// Token: 0x040002F2 RID: 754
		public Dictionary<string, string> ranges;

		// Token: 0x040002F3 RID: 755
		public List<int> row_index;

		// Token: 0x040002F4 RID: 756
		public string desc;

		// Token: 0x040002F5 RID: 757
		public bool extended;

		// Token: 0x040002F6 RID: 758
		public bool pw;

		// Token: 0x040002F7 RID: 759
		public bool all_check;
	}
}
