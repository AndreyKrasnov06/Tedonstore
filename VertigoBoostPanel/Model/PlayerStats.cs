using System;

namespace VertigoBoostPanel.Model
{
	// Token: 0x02000020 RID: 32
	public sealed class PlayerStats
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000033F5 File Offset: 0x000015F5
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000088F4 File Offset: 0x00006AF4
		public string ProfileName { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00008908 File Offset: 0x00006B08
		public string Login { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003405 File Offset: 0x00001605
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000891C File Offset: 0x00006B1C
		public string SteamID { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000340D File Offset: 0x0000160D
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00008930 File Offset: 0x00006B30
		public int XPGained { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003415 File Offset: 0x00001615
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00008944 File Offset: 0x00006B44
		public int XPCurrentLVL { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000341D File Offset: 0x0000161D
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00008958 File Offset: 0x00006B58
		public int XPUntilNextLevel { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000896C File Offset: 0x00006B6C
		public string CurrentMatchmakingRank { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000342D File Offset: 0x0000162D
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00008980 File Offset: 0x00006B80
		public int CurrentMatchmakingWins { get; set; }
	}
}
