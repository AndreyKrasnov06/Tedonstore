using System;

namespace VertigoBoostPanel.Model
{
	// Token: 0x0200001F RID: 31
	public struct GameModule
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000088D8 File Offset: 0x00006AD8
		public GameModule(IntPtr baseaddress, int size)
		{
			this.BaseAddress = baseaddress;
			this.Size = size;
		}

		// Token: 0x0400009D RID: 157
		public IntPtr BaseAddress;

		// Token: 0x0400009E RID: 158
		public int Size;
	}
}
