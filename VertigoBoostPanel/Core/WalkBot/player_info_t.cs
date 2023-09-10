using System;
using System.Runtime.InteropServices;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000134 RID: 308
	public struct player_info_t
	{
		// Token: 0x04000606 RID: 1542
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] __pad0;

		// Token: 0x04000607 RID: 1543
		public int m_nXuidLow;

		// Token: 0x04000608 RID: 1544
		public int m_nXuidHigh;

		// Token: 0x04000609 RID: 1545
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public char[] m_szPlayerName;

		// Token: 0x0400060A RID: 1546
		public int m_nUserID;

		// Token: 0x0400060B RID: 1547
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
		public char[] m_szSteamID;

		// Token: 0x0400060C RID: 1548
		public uint m_nSteam3ID;

		// Token: 0x0400060D RID: 1549
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public char[] m_szFriendsName;

		// Token: 0x0400060E RID: 1550
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bIsFakePlayer;

		// Token: 0x0400060F RID: 1551
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bIsHLTV;

		// Token: 0x04000610 RID: 1552
		public int[] m_dwCustomFiles;

		// Token: 0x04000611 RID: 1553
		public char m_FilesDownloaded;
	}
}
