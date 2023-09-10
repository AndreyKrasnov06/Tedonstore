using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Model
{
	// Token: 0x02000021 RID: 33
	internal class Sector
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00008994 File Offset: 0x00006B94
		public Sector(int _x, int _y, string _monitor)
		{
			this.IsUsed = false;
			this.monitor = _monitor;
			this.x = _x;
			this.y = _y;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000089C4 File Offset: 0x00006BC4
		public void AlignWindow()
		{
			Process process = this.GetProcess();
			if (process == null)
			{
				return;
			}
			Thread.Sleep(2000);
			IntPtr mainWindowHandle = process.MainWindowHandle;
			Sector.SetForegroundWindow(mainWindowHandle);
			Sector.SetWindowPos(mainWindowHandle, IntPtr.Zero, this.x, this.y, 0, 0, 5U);
			Sector.SetWindowText(mainWindowHandle, this.login.ToLower());
			Player player = StaticData.GetInstance.Players.FirstOrDefault((Player x) => x.Login == this.login);
			player.HWND = mainWindowHandle;
			player.CsGoProcess = process;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008A54 File Offset: 0x00006C54
		private Process GetProcess()
		{
			foreach (Process process in Process.GetProcessesByName("csgo"))
			{
				if (process.MainWindowTitle == this.login.ToLower() + " - Direct3D 9")
				{
					return process;
				}
			}
			return null;
		}

		// Token: 0x060000EC RID: 236
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x060000ED RID: 237
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x060000EE RID: 238
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowText(IntPtr hwnd, string lpString);

		// Token: 0x040000A7 RID: 167
		private int x;

		// Token: 0x040000A8 RID: 168
		private int y;

		// Token: 0x040000A9 RID: 169
		public string monitor;

		// Token: 0x040000AA RID: 170
		public string login;

		// Token: 0x040000AB RID: 171
		public bool IsUsed;
	}
}
