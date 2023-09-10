using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ItemsTransfer.SteamAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core;

namespace VertigoBoostPanel.Services.Files
{
	// Token: 0x020000A6 RID: 166
	public static class SteamGuardService
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0001C59C File Offset: 0x0001A79C
		public static void SetSteamCodeToWindow(string login, string steam_id_64, string steam_id_32)
		{
			TaskQueue.Queue.Add(new Task(delegate
			{
				for (;;)
				{
					Thread.Sleep(100);
					try
					{
						Process[] processesByName = Process.GetProcessesByName("steam_" + steam_id_32);
						if (processesByName.Length != 0)
						{
							IntPtr childSteam = SteamGuardService.GetChildSteam(processesByName[0]);
							if (!(childSteam == IntPtr.Zero))
							{
								string text = "maFiles\\" + steam_id_64 + ".maFile";
								if (!File.Exists(text))
								{
									text = "maFiles\\" + login + ".maFile";
								}
								if (!File.Exists(text))
								{
									break;
								}
								JObject.Parse(File.ReadAllText(text));
								JObject jobject;
								using (StreamReader streamReader = File.OpenText(text))
								{
									using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
									{
										jobject = (JObject)JToken.ReadFrom(jsonTextReader);
									}
								}
								string text2 = (string)jobject["shared_secret"];
								if (!string.IsNullOrEmpty(text2))
								{
									string text3 = new SteamGuardAccount
									{
										SharedSecret = text2
									}.GenerateSteamGuardCode();
									Console.WriteLine(text3);
									SteamGuardService.SendTextToActiveWindow(childSteam, text3);
									break;
								}
								break;
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						break;
					}
				}
			}));
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00003B2F File Offset: 0x00001D2F
		public static int CurrentProcessId
		{
			get
			{
				return Process.GetCurrentProcess().Id;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		private static void SendTextToActiveWindow(IntPtr hwnd, string text)
		{
			SteamGuardService.SetForegroundWindow(hwnd);
			Thread.Sleep(500);
			foreach (char c in text)
			{
				SteamGuardService.PostMessage(hwnd, 258, (IntPtr)((int)c), IntPtr.Zero);
			}
			Thread.Sleep(250);
			SteamGuardService.PostMessage(hwnd, 256, (IntPtr)13, IntPtr.Zero);
			SteamGuardService.PostMessage(hwnd, 257, (IntPtr)13, IntPtr.Zero);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001C678 File Offset: 0x0001A878
		private static IntPtr GetChildSteam(Process steam)
		{
			try
			{
				if (steam != null)
				{
					return SteamGuardService.GetRootWindowsOfProcess(steam.Id).FirstOrDefault((IntPtr x) => SteamGuardService.GetWindowText(x) == "Steam Guard - Computer Authorization Required");
				}
			}
			catch
			{
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		private static string GetWindowText(IntPtr hWnd)
		{
			int windowTextLength = SteamGuardService.GetWindowTextLength(hWnd);
			if (windowTextLength > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
				SteamGuardService.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001C720 File Offset: 0x0001A920
		private static List<IntPtr> GetRootWindowsOfProcess(int pid)
		{
			List<IntPtr> childWindows = SteamGuardService.GetChildWindows(IntPtr.Zero);
			List<IntPtr> list = new List<IntPtr>();
			foreach (IntPtr intPtr in childWindows)
			{
				uint num;
				SteamGuardService.GetWindowThreadProcessId(intPtr, out num);
				if ((ulong)num == (ulong)((long)pid))
				{
					list.Add(intPtr);
				}
			}
			return list;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001C790 File Offset: 0x0001A990
		private static List<IntPtr> GetChildWindows(IntPtr parent)
		{
			List<IntPtr> list = new List<IntPtr>();
			GCHandle gchandle = GCHandle.Alloc(list);
			try
			{
				SteamGuardService.Win32Callback win32Callback = new SteamGuardService.Win32Callback(SteamGuardService.EnumWindow);
				SteamGuardService.EnumChildWindows(parent, win32Callback, GCHandle.ToIntPtr(gchandle));
			}
			catch
			{
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return list;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private static bool EnumWindow(IntPtr handle, IntPtr pointer)
		{
			List<IntPtr> list = GCHandle.FromIntPtr(pointer).Target as List<IntPtr>;
			if (list == null)
			{
				throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
			}
			list.Add(handle);
			return true;
		}

		// Token: 0x06000409 RID: 1033
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600040A RID: 1034
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

		// Token: 0x0600040B RID: 1035
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x0600040C RID: 1036
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr parentHandle, SteamGuardService.Win32Callback callback, IntPtr lParam);

		// Token: 0x0600040D RID: 1037
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600040E RID: 1038
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x06000410 RID: 1040
		private delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);
	}
}
