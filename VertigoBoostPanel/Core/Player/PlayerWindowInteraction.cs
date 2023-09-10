using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VertigoBoostPanel.Core.GSI;
using VertigoBoostPanel.Core.Sessions;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Services.Files;

namespace VertigoBoostPanel.Core.Player
{
	// Token: 0x02000126 RID: 294
	public class PlayerWindowInteraction
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0002EC6C File Offset: 0x0002CE6C
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0002EC88 File Offset: 0x0002CE88
		public IntPtr HWND
		{
			get
			{
				this.method_0();
				return this.hWnd;
			}
			set
			{
				this.hWnd = value;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002EC9C File Offset: 0x0002CE9C
		private void method_0()
		{
			if (this.hWnd != IntPtr.Zero)
			{
				return;
			}
			foreach (Process process in Process.GetProcessesByName("csgo"))
			{
				if (process.MainWindowTitle == this.me.Login)
				{
					this.hWnd = process.MainWindowHandle;
				}
			}
		}

		// Token: 0x06000611 RID: 1553
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern void DustPath(float x1, float y1, float z1, float x2, float y2, float z2, PlayerWindowInteraction.ResponseDelegate response);

		// Token: 0x06000612 RID: 1554
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void DisableCsgoGraphics(IntPtr hWnd);

		// Token: 0x06000613 RID: 1555 RVA: 0x0002ED08 File Offset: 0x0002CF08
		public void DisableCsgoGraphics()
		{
			PlayerWindowInteraction.DisableCsgoGraphics(this.HWND);
		}

		// Token: 0x06000614 RID: 1556
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void GoAndKillBots(IntPtr hWnd, string token, string map_name);

		// Token: 0x06000615 RID: 1557 RVA: 0x0002ED20 File Offset: 0x0002CF20
		public void GoAndKillBots()
		{
			PlayerWindowInteraction.GoAndKillBots(this.HWND, "wtfhddddd", this.me.currentMap);
		}

		// Token: 0x06000616 RID: 1558
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void GoOut(IntPtr hWnd, string map_name);

		// Token: 0x06000617 RID: 1559 RVA: 0x0002ED48 File Offset: 0x0002CF48
		public void GoOut()
		{
			Task.Run(delegate
			{
				PlayerWindowInteraction.GoOut(this.HWND, this.me.currentMap);
			});
		}

		// Token: 0x06000618 RID: 1560
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void WatchToCorrentAngle(IntPtr hWnd, string map_name);

		// Token: 0x06000619 RID: 1561 RVA: 0x0002ED68 File Offset: 0x0002CF68
		public void WatchToCorrentAngle()
		{
			PlayerWindowInteraction.WatchToCorrentAngle(this.HWND, this.me.currentMap);
		}

		// Token: 0x0600061A RID: 1562
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void ExecConfig(IntPtr hWnd);

		// Token: 0x0600061B RID: 1563 RVA: 0x0002ED8C File Offset: 0x0002CF8C
		public void ExecConfig()
		{
			PlayerWindowInteraction.ExecConfig(this.HWND);
		}

		// Token: 0x0600061C RID: 1564
		[DllImport("lib\\Executor.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void CallvoteDizzy(IntPtr hWnd);

		// Token: 0x0600061D RID: 1565 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		public void CallvoteDizzy()
		{
			PlayerWindowInteraction.CallvoteDizzy(this.HWND);
		}

		// Token: 0x0600061E RID: 1566
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void InvitePlayerByCode(IntPtr hWnd, string token, string code);

		// Token: 0x0600061F RID: 1567 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public void InvitePlayerByCode(string code)
		{
			PlayerWindowInteraction.InvitePlayerByCode(this.HWND, Program.GetInstance.TOKEN, code);
		}

		// Token: 0x06000620 RID: 1568
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void InviteRecentTeam(IntPtr hWnd, int count_players);

		// Token: 0x06000621 RID: 1569 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		public void InviteRecentTeam(int count_players)
		{
			PlayerWindowInteraction.InviteRecentTeam(this.HWND, count_players);
		}

		// Token: 0x06000622 RID: 1570
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void AcceptInviteToLobby(IntPtr hWnd);

		// Token: 0x06000623 RID: 1571 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		public void AcceptInviteToLobby()
		{
			PlayerWindowInteraction.AcceptInviteToLobby(this.HWND);
		}

		// Token: 0x06000624 RID: 1572
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void StartGame(IntPtr hWnd, int lobbyState, bool menuClick);

		// Token: 0x06000625 RID: 1573 RVA: 0x0002EE14 File Offset: 0x0002D014
		public void StartGame(int lobbyState, bool menuClick)
		{
			PlayerWindowInteraction.StartGame(this.HWND, lobbyState, menuClick);
		}

		// Token: 0x06000626 RID: 1574
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void AcceptGame(IntPtr hWnd);

		// Token: 0x06000627 RID: 1575 RVA: 0x0002EE30 File Offset: 0x0002D030
		public void AcceptGame()
		{
			Session.SetForegroundWindow(this.HWND);
			Session.SetFocus(this.HWND);
			Session.SetActiveWindow(this.HWND);
			PlayerWindowInteraction.AcceptGame(this.HWND);
		}

		// Token: 0x06000628 RID: 1576
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void SelectShortGame(IntPtr hWnd);

		// Token: 0x06000629 RID: 1577 RVA: 0x0002EE6C File Offset: 0x0002D06C
		public void SelectShortGame()
		{
			PlayerWindowInteraction.SelectShortGame(this.HWND);
		}

		// Token: 0x0600062A RID: 1578
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void SelectLongGame(IntPtr hWnd);

		// Token: 0x0600062B RID: 1579 RVA: 0x0002EE84 File Offset: 0x0002D084
		public void SelectLongGame()
		{
			PlayerWindowInteraction.SelectLongGame(this.HWND);
		}

		// Token: 0x0600062C RID: 1580
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void LeaveLobby(IntPtr hWnd);

		// Token: 0x0600062D RID: 1581 RVA: 0x0002EE9C File Offset: 0x0002D09C
		public void LeaveLobby()
		{
			PlayerWindowInteraction.LeaveLobby(this.HWND);
		}

		// Token: 0x0600062E RID: 1582
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void Reconnect(IntPtr hWnd);

		// Token: 0x0600062F RID: 1583 RVA: 0x0002EEB4 File Offset: 0x0002D0B4
		public void Reconnect()
		{
			this.CheckAndCloseError();
			PlayerWindowInteraction.Reconnect(this.HWND);
		}

		// Token: 0x06000630 RID: 1584
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void Disconnect(IntPtr hWnd);

		// Token: 0x06000631 RID: 1585 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		public void Disconnect()
		{
			if (GSI.DisconnectTick)
			{
				PlayerWindowInteraction.Disconnect(this.HWND);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002EEF4 File Offset: 0x0002D0F4
		public Bitmap GetScreenshot()
		{
			User32.Rect rect = default(User32.Rect);
			User32.GetWindowRect(this.hWnd, ref rect);
			int num = rect.right - rect.left;
			int num2 = rect.bottom - rect.top;
			Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppRgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new System.Drawing.Size(num, num2), CopyPixelOperation.SourceCopy);
				PlayerWindowInteraction.PrintWindow(this.hWnd, graphics.GetHdc(), 0U);
				graphics.Dispose();
			}
			return bitmap;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		public void MoveWindowToCoordinate(int x, int y)
		{
			PlayerWindowInteraction.SetWindowPos(this.HWND, IntPtr.Zero, x, y, 0, 0, 5U);
		}

		// Token: 0x06000634 RID: 1588
		[DllImport("lib\\CPPHelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void ExecConsoleText(IntPtr hWnd, string console_text);

		// Token: 0x06000635 RID: 1589 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
		public void ExecConsoleText(string console_text)
		{
			PlayerWindowInteraction.ExecConsoleText(this.HWND, console_text);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002EFF4 File Offset: 0x0002D1F4
		public void CheckAndCloseError()
		{
			Bitmap screenshot = this.GetScreenshot();
			for (int i = 210; i > 120; i--)
			{
				if (this.ErrorBorderExists(screenshot.GetPixel(191, i)))
				{
					LogService.GetInstance.LogInformation("[INFO] : Found border on [" + this.me.Login + "]");
					for (int j = 180; j < screenshot.Width; j++)
					{
						LogService.GetInstance.LogInformation(string.Format("[INFO] : Coords - X: {0} | Y: {1}", j, i - 33));
						Player.ClickOnPoint(this.HWND, new System.Windows.Point((double)j, (double)(i - 33)));
						Thread.Sleep(1);
					}
					return;
				}
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		private bool OkErrorButtonExists(Color color)
		{
			return color.R > 28 && color.R < 37 && color.G > 28 && color.G < 37 && color.B > 28 && color.B < 37;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002F10C File Offset: 0x0002D30C
		private bool ErrorExists(Color color)
		{
			return color.R > 40 && color.R < 48 && color.G > 40 && color.G < 48 && color.B > 40 && color.B < 48;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0002F160 File Offset: 0x0002D360
		private bool ErrorBorderExists(Color color)
		{
			return color.R > 60 && color.R < 70 && color.G > 60 && color.G < 70 && color.B > 60 && color.B < 70;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		public static void sendKey(IntPtr hwnd, int keyCode, bool extended, bool up_down)
		{
			uint num = PlayerWindowInteraction.MapVirtualKey((uint)keyCode, 0U);
			uint num2 = 1U | (num << 16);
			if (extended)
			{
				num2 |= 16777216U;
			}
			if (!up_down)
			{
				PlayerWindowInteraction.PostMessage(hwnd, 257U, (IntPtr)keyCode, (IntPtr)((long)((ulong)num2)));
				return;
			}
			PlayerWindowInteraction.PostMessage(hwnd, 256U, (IntPtr)keyCode, (IntPtr)((long)((ulong)num2)));
		}

		// Token: 0x0600063B RID: 1595
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x0600063C RID: 1596
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600063D RID: 1597
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

		// Token: 0x0600063E RID: 1598
		[DllImport("user32.dll")]
		public static extern int SetWindowText(IntPtr hWnd, string text);

		// Token: 0x0600063F RID: 1599
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x06000640 RID: 1600
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

		// Token: 0x040005E6 RID: 1510
		public Player me;

		// Token: 0x040005E7 RID: 1511
		private IntPtr hWnd;

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000644 RID: 1604
		public delegate void ResponseDelegate(string s);
	}
}
