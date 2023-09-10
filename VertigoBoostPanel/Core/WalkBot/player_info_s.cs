using System;
using System.Runtime.InteropServices;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x0200013B RID: 315
	public struct player_info_s
	{
		// Token: 0x04000619 RID: 1561
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] __pad0;

		// Token: 0x0400061A RID: 1562
		public int m_nXuidLow;

		// Token: 0x0400061B RID: 1563
		public int m_nXuidHigh;

		// Token: 0x0400061C RID: 1564
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public char[] m_szPlayerName;

		// Token: 0x0400061D RID: 1565
		public int m_nUserID;

		// Token: 0x0400061E RID: 1566
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
		public char[] m_szSteamID;

		// Token: 0x0400061F RID: 1567
		public uint m_nSteam3ID;

		// Token: 0x04000620 RID: 1568
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public char[] m_szFriendsName;

		// Token: 0x04000621 RID: 1569
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bIsFakePlayer;

		// Token: 0x04000622 RID: 1570
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bIsHLTV;

		// Token: 0x04000623 RID: 1571
		public int[] m_dwCustomFiles;

		// Token: 0x04000624 RID: 1572
		public char m_FilesDownloaded;
	}
}
