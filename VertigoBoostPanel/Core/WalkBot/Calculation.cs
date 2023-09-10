using System;
using System.Numerics;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000135 RID: 309
	public static class Calculation
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x00030A10 File Offset: 0x0002EC10
		public static Vector3 ToVector3(string str)
		{
			return new Vector3(float.Parse(str.Split(new char[] { ' ' })[0]), float.Parse(str.Split(new char[] { ' ' })[1]), float.Parse(str.Split(new char[] { ' ' })[2]));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000041E3 File Offset: 0x000023E3
		public static Vector3 CalculateAngle(Vector3 localPosition, Vector3 enemyPosition, Vector3 viewAngles)
		{
			return Calculation.ToAngle(enemyPosition - localPosition) - viewAngles;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00030A6C File Offset: 0x0002EC6C
		private static Vector3 ToAngle(Vector3 vector)
		{
			return new Vector3((float)(Math.Atan2((double)(-(double)vector.Z), Calculation.hypo((double)vector.X, (double)vector.Y)) * 57.295779513082323), (float)(Math.Atan2((double)vector.Y, (double)vector.X) * 57.295779513082323), 0f);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000041F7 File Offset: 0x000023F7
		private static double hypo(double x, double y)
		{
			return Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2.0));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000421D File Offset: 0x0000241D
		public static float Length2D(this Vector3 source)
		{
			return (float)Math.Sqrt((double)(source.X * source.X + source.Y * source.Y));
		}

		// Token: 0x04000612 RID: 1554
		public static string TOKEN;
	}
}
