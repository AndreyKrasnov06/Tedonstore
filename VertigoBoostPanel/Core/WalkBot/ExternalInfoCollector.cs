using System;
using System.Numerics;
using VertigoBoostPanel.Core.Player;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000137 RID: 311
	public static class ExternalInfoCollector
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00030CD0 File Offset: 0x0002EED0
		public static bool SetupTargetPlayer(Player player)
		{
			if (!string.IsNullOrEmpty(player.string_1))
			{
				ExternalInfoCollector.TargetAccountID = player.GetInternalID();
				return true;
			}
			return false;
		}

		// Token: 0x04000615 RID: 1557
		public static string TargetAccountID = string.Empty;

		// Token: 0x04000616 RID: 1558
		public static Vector3 TargetAccountPosition = default(Vector3);

		// Token: 0x04000617 RID: 1559
		public static float TargetAccountEyes = 0f;
	}
}
