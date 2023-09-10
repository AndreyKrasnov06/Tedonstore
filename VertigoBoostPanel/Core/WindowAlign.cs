using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000DD RID: 221
	internal class WindowAlign
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0001F67C File Offset: 0x0001D87C
		public static void InitMonitors()
		{
			foreach (Screen screen in Screen.AllScreens)
			{
				WindowAlign.AllMonitors.Add(screen);
			}
			WindowAlign.SplitSectors();
			if (!string.IsNullOrEmpty(Settings.GetInstance.StartMonitor) && !string.IsNullOrWhiteSpace(Settings.GetInstance.StartMonitor))
			{
				WindowAlign.WorkMonitor = Settings.GetInstance.StartMonitor;
				return;
			}
			WindowAlign.WorkMonitor = Screen.PrimaryScreen.DeviceFriendlyName();
			Settings.GetInstance.StartMonitor = WindowAlign.WorkMonitor;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001F70C File Offset: 0x0001D90C
		public static void SplitSectors()
		{
			int num;
			int num2;
			try
			{
				num = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[0]);
				num2 = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[1]);
			}
			catch
			{
				CreateError.NewError("Bad resolution", false);
				return;
			}
			if (num == 0 || num2 == 0)
			{
				CreateError.NewError("Bad resolution", false);
				return;
			}
			WindowAlign.Sectors.Clear();
			foreach (Screen screen in WindowAlign.AllMonitors)
			{
				string text = screen.DeviceFriendlyName();
				for (int i = 0; i < screen.WorkingArea.Height / (num2 + 27); i++)
				{
					for (int j = 0; j < screen.WorkingArea.Width / num; j++)
					{
						WindowAlign.Sectors.Add(new Sector(j * num + screen.WorkingArea.X, i * (num2 + 27) + screen.WorkingArea.Y, text));
					}
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001F880 File Offset: 0x0001DA80
		public static Sector GetFreeSector()
		{
			using (List<Sector>.Enumerator enumerator = WindowAlign.Sectors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Sector sector = enumerator.Current;
					if (!sector.IsUsed && sector.monitor == WindowAlign.WorkMonitor)
					{
						return sector;
					}
				}
				goto IL_56;
			}
			Sector sector2;
			return sector2;
			IL_56:
			return null;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		public static Sector GetRandomFreeSector()
		{
			using (List<Sector>.Enumerator enumerator = WindowAlign.Sectors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Sector sector = enumerator.Current;
					if (!sector.IsUsed)
					{
						return sector;
					}
				}
				goto IL_43;
			}
			Sector sector2;
			return sector2;
			IL_43:
			return null;
		}

		// Token: 0x04000453 RID: 1107
		public static string WorkMonitor = "";

		// Token: 0x04000454 RID: 1108
		private static List<Sector> Sectors = new List<Sector>();

		// Token: 0x04000455 RID: 1109
		public static List<Screen> AllMonitors = new List<Screen>();
	}
}
