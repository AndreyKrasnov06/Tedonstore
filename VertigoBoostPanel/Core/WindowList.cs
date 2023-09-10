using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000DE RID: 222
	public static class WindowList
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0001F984 File Offset: 0x0001DB84
		public static List<WindowInformation> GetAllWindows()
		{
			IntPtr desktopWindow = WindowList.GetDesktopWindow();
			List<WindowInformation> list = new List<WindowInformation>();
			list.Add(WindowList.winInfoGet(desktopWindow));
			foreach (IntPtr intPtr in WindowList.getChildWindows(desktopWindow))
			{
				try
				{
					list.Add(WindowList.winInfoGet(intPtr));
				}
				catch (Exception)
				{
				}
			}
			return list;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00003C13 File Offset: 0x00001E13
		public static List<WindowInformation> GetAllWindowsExtendedInfo()
		{
			return WindowList.winInfoExtendedInfoProcess(WindowList.GetAllWindowsTree());
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00003C1F File Offset: 0x00001E1F
		public static WindowInformation GetAllWindowsTree()
		{
			WindowInformation windowInformation = WindowList.winInfoGet(WindowList.GetDesktopWindow());
			windowInformation.ChildWindows = WindowList.getChildWindowsInfo(windowInformation);
			return windowInformation;
		}

		// Token: 0x06000492 RID: 1170
		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr window, WindowList.EnumWindowProc callback, IntPtr i);

		// Token: 0x06000493 RID: 1171
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		// Token: 0x06000494 RID: 1172
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06000495 RID: 1173
		[DllImport("user32.dll")]
		private static extern IntPtr GetDesktopWindow();

		// Token: 0x06000496 RID: 1174
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindow(IntPtr hWnd, WindowList.GetWindow_Cmd uCmd);

		// Token: 0x06000497 RID: 1175
		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		// Token: 0x06000498 RID: 1176
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hwnd, WindowList.WMConstants wmConstant, int wParam, StringBuilder sb);

		// Token: 0x06000499 RID: 1177
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hwnd, WindowList.WMConstants wmConstant, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600049A RID: 1178 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private static bool enumWindow(IntPtr handle, IntPtr pointer)
		{
			List<IntPtr> list = GCHandle.FromIntPtr(pointer).Target as List<IntPtr>;
			if (list == null)
			{
				throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
			}
			list.Add(handle);
			return true;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001FA08 File Offset: 0x0001DC08
		private static List<IntPtr> getChildWindows(IntPtr parent)
		{
			List<IntPtr> list = new List<IntPtr>();
			GCHandle gchandle = GCHandle.Alloc(list);
			try
			{
				WindowList.EnumWindowProc enumWindowProc = new WindowList.EnumWindowProc(WindowList.enumWindow);
				WindowList.EnumChildWindows(parent, enumWindowProc, GCHandle.ToIntPtr(gchandle));
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

		// Token: 0x0600049C RID: 1180 RVA: 0x0001FA64 File Offset: 0x0001DC64
		private static List<WindowInformation> getChildWindowsInfo(WindowInformation parent)
		{
			List<WindowInformation> list = new List<WindowInformation>();
			IntPtr intPtr = WindowList.GetWindow(parent.Handle, WindowList.GetWindow_Cmd.GW_CHILD);
			while (intPtr != IntPtr.Zero)
			{
				WindowInformation windowInformation = WindowList.winInfoGet(intPtr);
				windowInformation.Parent = parent;
				windowInformation.ChildWindows = WindowList.getChildWindowsInfo(windowInformation);
				list.Add(windowInformation);
				intPtr = WindowList.FindWindowEx(parent.Handle, intPtr, null, null);
			}
			foreach (WindowInformation windowInformation2 in list)
			{
				windowInformation2.SiblingWindows.AddRange(list);
				windowInformation2.SiblingWindows.Remove(windowInformation2);
			}
			return list;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001FB1C File Offset: 0x0001DD1C
		public static bool Verify
		{
			get
			{
				object userName = Environment.UserName;
				if (WindowList.<>o__18.<>p__0 == null)
				{
					WindowList.<>o__18.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToLower", null, typeof(WindowList), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				object obj = WindowList.<>o__18.<>p__0.Target(WindowList.<>o__18.<>p__0, userName);
				if (WindowList.<>o__18.<>p__2 == null)
				{
					WindowList.<>o__18.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(WindowList)));
				}
				Func<CallSite, object, bool> target = WindowList.<>o__18.<>p__2.Target;
				CallSite <>p__ = WindowList.<>o__18.<>p__2;
				if (WindowList.<>o__18.<>p__1 == null)
				{
					WindowList.<>o__18.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Contains", null, typeof(WindowList), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				return target(<>p__, WindowList.<>o__18.<>p__1.Target(WindowList.<>o__18.<>p__1, obj, "a"));
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		private static List<WindowInformation> winInfoExtendedInfoProcess(List<WindowInformation> winInfo)
		{
			List<WindowInformation> list = new List<WindowInformation>();
			list.Add(winInfo);
			foreach (WindowInformation windowInformation in winInfo.ChildWindows)
			{
				list.AddRange(WindowList.winInfoExtendedInfoProcess(windowInformation));
			}
			return list;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001FC84 File Offset: 0x0001DE84
		private static WindowInformation winInfoGet(IntPtr hWnd)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			StringBuilder stringBuilder2 = new StringBuilder(1024);
			WindowList.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
			WindowList.GetClassName(hWnd, stringBuilder2, stringBuilder2.Capacity);
			WindowInformation windowInformation = new WindowInformation();
			windowInformation.Handle = hWnd;
			windowInformation.Class = stringBuilder2.ToString();
			if (stringBuilder.ToString() != string.Empty)
			{
				windowInformation.Caption = stringBuilder.ToString();
			}
			else
			{
				stringBuilder = new StringBuilder(Convert.ToInt32(WindowList.SendMessage(windowInformation.Handle, WindowList.WMConstants.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero)) + 1);
				WindowList.SendMessage(windowInformation.Handle, WindowList.WMConstants.WM_GETTEXT, stringBuilder.Capacity, stringBuilder);
				windowInformation.Caption = stringBuilder.ToString();
			}
			return windowInformation;
		}

		// Token: 0x020000DF RID: 223
		// (Invoke) Token: 0x060004A1 RID: 1185
		private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

		// Token: 0x020000E0 RID: 224
		private enum GetWindow_Cmd : uint
		{
			// Token: 0x04000457 RID: 1111
			GW_HWNDFIRST,
			// Token: 0x04000458 RID: 1112
			GW_HWNDLAST,
			// Token: 0x04000459 RID: 1113
			GW_HWNDNEXT,
			// Token: 0x0400045A RID: 1114
			GW_HWNDPREV,
			// Token: 0x0400045B RID: 1115
			GW_OWNER,
			// Token: 0x0400045C RID: 1116
			GW_CHILD,
			// Token: 0x0400045D RID: 1117
			GW_ENABLEDPOPUP
		}

		// Token: 0x020000E1 RID: 225
		private enum WMConstants
		{
			// Token: 0x0400045F RID: 1119
			WM_GETTEXT = 13,
			// Token: 0x04000460 RID: 1120
			WM_GETTEXTLENGTH
		}
	}
}
