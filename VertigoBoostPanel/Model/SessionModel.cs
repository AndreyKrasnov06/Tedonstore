using System;

namespace VertigoBoostPanel.Model
{
	// Token: 0x02000022 RID: 34
	public class SessionModel
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003448 File Offset: 0x00001648
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public int Id { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003450 File Offset: 0x00001650
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public string TypeOfSession { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public string Name { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00008AEC File Offset: 0x00006CEC
		public string GameMode { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003468 File Offset: 0x00001668
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00008B00 File Offset: 0x00006D00
		public string ClientInviteCode { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00008B14 File Offset: 0x00006D14
		public int NeedClientRank { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003478 File Offset: 0x00001678
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00008B28 File Offset: 0x00006D28
		public int CountGame { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003480 File Offset: 0x00001680
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00008B3C File Offset: 0x00006D3C
		public bool ShortGame { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003488 File Offset: 0x00001688
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00008B50 File Offset: 0x00006D50
		public bool MVP { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003490 File Offset: 0x00001690
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00008B64 File Offset: 0x00006D64
		public bool SwapLeaders { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00008B78 File Offset: 0x00006D78
		public string FirstTeam { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000034A0 File Offset: 0x000016A0
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00008B8C File Offset: 0x00006D8C
		public string SecondTeam { get; set; }
	}
}
