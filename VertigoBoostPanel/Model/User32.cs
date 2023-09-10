using System;
using System.Runtime.InteropServices;

namespace VertigoBoostPanel.Model
{
	// Token: 0x02000023 RID: 35
	public class User32
	{
		// Token: 0x06000109 RID: 265
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowRect(IntPtr hWnd, ref User32.Rect rect);

		// Token: 0x02000024 RID: 36
		public struct Rect
		{
			// Token: 0x040000B8 RID: 184
			public int left;

			// Token: 0x040000B9 RID: 185
			public int top;

			// Token: 0x040000BA RID: 186
			public int right;

			// Token: 0x040000BB RID: 187
			public int bottom;
		}
	}
}
