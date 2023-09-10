using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ItemsTransfer.SteamAuth;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services
{
	// Token: 0x0200007B RID: 123
	public class SteamGuardAuthorizer
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001737C File Offset: 0x0001557C
		public static SteamGuardAuthorizer GetInstance
		{
			get
			{
				if (SteamGuardAuthorizer.Class == null)
				{
					object syncObject = SteamGuardAuthorizer.SyncObject;
					lock (syncObject)
					{
						if (SteamGuardAuthorizer.Class == null)
						{
							SteamGuardAuthorizer.Class = new SteamGuardAuthorizer();
						}
					}
				}
				return SteamGuardAuthorizer.Class;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000173E0 File Offset: 0x000155E0
		public void RunService()
		{
			this._worker = new BackgroundWorker();
			this._worker.RunWorkerAsync();
			this._worker.WorkerSupportsCancellation = true;
			this._worker.DoWork += this.WorkerDoWork;
			this._worker.RunWorkerCompleted += this.WorkerCompleted;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00017440 File Offset: 0x00015640
		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			Thread.Sleep(2000);
			try
			{
				List<KeyValuePair<IntPtr, ValueTuple<string, string>>> list = new List<KeyValuePair<IntPtr, ValueTuple<string, string>>>();
				list.AddRange((from x in this.GetOpenWindows()
					where x.Value.Item1.StartsWith("Steam Guard") && x.Value.Item2 != "steam.exe"
					select x).ToList<KeyValuePair<IntPtr, ValueTuple<string, string>>>());
				list.AddRange((from x in this.GetOpenWindows()
					where x.Value.Item1.StartsWith("Steam Sign In") && x.Value.Item2 != "steam.exe"
					select x).ToList<KeyValuePair<IntPtr, ValueTuple<string, string>>>());
				using (List<KeyValuePair<IntPtr, ValueTuple<string, string>>>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<IntPtr, ValueTuple<string, string>> window = enumerator.Current;
						if (window.Value.Item2.Contains("_"))
						{
							IntPtr key = window.Key;
							bool flag = false;
							Player player = StaticData.GetInstance.Players.FirstOrDefault((Player x) => x.string_0 == window.Value.Item2.Split(new char[] { '_' })[1]);
							if (player == null)
							{
								flag = true;
								player = StaticData.GetInstance.Players.FirstOrDefault((Player x) => x.Login == window.Value.Item2.Split(new char[] { '_' })[1]);
							}
							if (player != null)
							{
								string text = string.Empty;
								if (flag)
								{
									text = this.GetSteamGuardCodeByLogin(player.Login);
								}
								else
								{
									text = player.SteamGuardCode;
								}
								if (text == null)
								{
									break;
								}
								int num = SteamGuardAuthorizer.LoadKeyboardLayout("00000409", 1U);
								SteamGuardAuthorizer.PostMessage(key, 80U, (IntPtr)0, (IntPtr)num);
								SteamGuardAuthorizer.PostMessage(key, 81U, (IntPtr)0, (IntPtr)num);
								SteamGuardAuthorizer.ShowWindow(key, 9);
								SteamGuardAuthorizer.SwitchToThisWindow(key, true);
								SteamGuardAuthorizer.SendMessage(key, 274U, 61728U, 0);
								SteamGuardAuthorizer.SetForegroundWindow(key);
								SteamGuardAuthorizer.SetWindowPos(key, new IntPtr(-1), 0, 0, 0, 0, 3);
								Thread.Sleep(500);
								if (SteamGuardAuthorizer.GetForegroundWindow() != key)
								{
									SteamGuardAuthorizer.Rect rect = default(SteamGuardAuthorizer.Rect);
									SteamGuardAuthorizer.GetWindowRect(Process.GetCurrentProcess().MainWindowHandle, ref rect);
									int num2 = rect.Left + 100;
									int num3 = rect.Top + 15;
									SteamGuardAuthorizer.SetCursorPos(num2, num3);
									SteamGuardAuthorizer.mouse_event(2, num2, num3, 0, 0);
									SteamGuardAuthorizer.mouse_event(4, num2, num3, 0, 0);
									Thread.Sleep(500);
									SteamGuardAuthorizer.PostMessage(key, 80U, (IntPtr)0, (IntPtr)num);
									SteamGuardAuthorizer.PostMessage(key, 81U, (IntPtr)0, (IntPtr)num);
									SteamGuardAuthorizer.ShowWindow(key, 9);
									SteamGuardAuthorizer.SwitchToThisWindow(key, true);
									SteamGuardAuthorizer.SendMessage(key, 274U, 61728U, 0);
									SteamGuardAuthorizer.SetForegroundWindow(key);
									SteamGuardAuthorizer.SetWindowPos(key, new IntPtr(-1), 0, 0, 0, 0, 3);
								}
								string text2 = text;
								for (int i = 0; i < text2.Length; i++)
								{
									int num4 = (int)Convert.ToUInt32(text2[i]);
									uint num5 = SteamGuardAuthorizer.MapVirtualKey((uint)num4, 0U);
									num5 = 1U | (num5 << 16);
									num5 |= 16777216U;
									SteamGuardAuthorizer.PostMessage(key, 256U, (IntPtr)num4, (IntPtr)((long)((ulong)num5)));
								}
								Thread.Sleep(500);
								SteamGuardAuthorizer.SwitchToThisWindow(key, true);
								SteamGuardAuthorizer.SendMessage(key, 274U, 61728U, 0);
								Thread.Sleep(100);
								uint num6 = SteamGuardAuthorizer.MapVirtualKey(13U, 0U);
								num6 = 1U | (num6 << 16);
								SteamGuardAuthorizer.PostMessage(key, 256U, (IntPtr)13, (IntPtr)((long)((ulong)num6)));
								SteamGuardAuthorizer.PostMessage(key, 257U, (IntPtr)13, (IntPtr)((long)((ulong)num6)));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00017824 File Offset: 0x00015A24
		private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._worker.RunWorkerAsync();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00003948 File Offset: 0x00001B48
		private IDictionary<IntPtr, ValueTuple<string, string>> GetOpenWindows()
		{
			IntPtr shellWindow = SteamGuardAuthorizer.GetShellWindow();
			Dictionary<IntPtr, ValueTuple<string, string>> windows = new Dictionary<IntPtr, ValueTuple<string, string>>();
			SteamGuardAuthorizer.EnumWindows(delegate(IntPtr hWnd, int lParam)
			{
				if (hWnd == shellWindow)
				{
					return true;
				}
				if (!SteamGuardAuthorizer.IsWindowVisible(hWnd))
				{
					return true;
				}
				int windowTextLength = SteamGuardAuthorizer.GetWindowTextLength(hWnd);
				if (windowTextLength != 0)
				{
					StringBuilder stringBuilder = new StringBuilder(windowTextLength);
					SteamGuardAuthorizer.GetWindowText(hWnd, stringBuilder, windowTextLength + 1);
					uint pid;
					SteamGuardAuthorizer.GetWindowThreadProcessId(hWnd, out pid);
					Process process = Process.GetProcesses().FirstOrDefault((Process x) => (long)x.Id == (long)((ulong)pid));
					string text = ((process != null) ? process.ProcessName : null);
					windows[hWnd] = new ValueTuple<string, string>(stringBuilder.ToString(), text);
					return true;
				}
				return true;
			}, 0);
			return windows;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001783C File Offset: 0x00015A3C
		private string GetSteamGuardCodeByLogin(string login)
		{
			string text = string.Empty;
			foreach (string text2 in Directory.GetFiles("maFiles"))
			{
				if (text2.ToLower().EndsWith(".mafile"))
				{
					try
					{
						string text3 = string.Empty;
						using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
						{
							using (StreamReader streamReader = new StreamReader(fileStream))
							{
								text3 = streamReader.ReadToEnd();
							}
						}
						JObject jobject = JObject.Parse(text3);
						if ((string)jobject["account_name"] == login)
						{
							text = (string)jobject["shared_secret"];
							break;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return new SteamGuardAccount
				{
					SharedSecret = text
				}.GenerateSteamGuardCode();
			}
			return string.Empty;
		}

		// Token: 0x06000345 RID: 837
		[DllImport("user32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

		// Token: 0x06000346 RID: 838
		[DllImport("user32.dll")]
		public static extern long SetCursorPos(int x, int y);

		// Token: 0x06000347 RID: 839
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref SteamGuardAuthorizer.Rect rectangle);

		// Token: 0x06000348 RID: 840
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000349 RID: 841
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x0600034A RID: 842
		[DllImport("user32.dll")]
		private static extern int LoadKeyboardLayout(string pwszKLID, uint Flags);

		// Token: 0x0600034B RID: 843
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600034C RID: 844
		[DllImport("user32.dll")]
		private static extern void SendMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);

		// Token: 0x0600034D RID: 845
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x0600034E RID: 846
		[DllImport("user32.dll")]
		private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		// Token: 0x0600034F RID: 847
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000350 RID: 848
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x06000351 RID: 849
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

		// Token: 0x06000352 RID: 850
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000353 RID: 851
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x06000354 RID: 852
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000355 RID: 853
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(SteamGuardAuthorizer.EnumWindowsProc enumFunc, int lParam);

		// Token: 0x06000356 RID: 854
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumWindows(SteamGuardAuthorizer.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000357 RID: 855
		[DllImport("user32.dll")]
		private static extern IntPtr GetShellWindow();

		// Token: 0x040002E3 RID: 739
		private static volatile SteamGuardAuthorizer Class;

		// Token: 0x040002E4 RID: 740
		private static object SyncObject = new object();

		// Token: 0x040002E5 RID: 741
		private BackgroundWorker _worker;

		// Token: 0x0200007C RID: 124
		public struct Rect
		{
			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000397D File Offset: 0x00001B7D
			// (set) Token: 0x0600035B RID: 859 RVA: 0x00017978 File Offset: 0x00015B78
			public int Left { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x0600035C RID: 860 RVA: 0x00003985 File Offset: 0x00001B85
			// (set) Token: 0x0600035D RID: 861 RVA: 0x0001798C File Offset: 0x00015B8C
			public int Top { get; set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000398D File Offset: 0x00001B8D
			// (set) Token: 0x0600035F RID: 863 RVA: 0x000179A0 File Offset: 0x00015BA0
			public int Right { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000360 RID: 864 RVA: 0x00003995 File Offset: 0x00001B95
			// (set) Token: 0x06000361 RID: 865 RVA: 0x000179B4 File Offset: 0x00015BB4
			public int Bottom { get; set; }
		}

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000363 RID: 867
		private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
	}
}
