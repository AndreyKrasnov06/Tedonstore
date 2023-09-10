using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace VertigoBoostPanel.Services.Optimization
{
	// Token: 0x02000097 RID: 151
	public class WebHelperSlayer
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001A818 File Offset: 0x00018A18
		public static WebHelperSlayer GetInstance
		{
			get
			{
				if (WebHelperSlayer.Class == null)
				{
					object syncObject = WebHelperSlayer.SyncObject;
					lock (syncObject)
					{
						if (WebHelperSlayer.Class == null)
						{
							WebHelperSlayer.Class = new WebHelperSlayer();
						}
					}
				}
				return WebHelperSlayer.Class;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001A87C File Offset: 0x00018A7C
		public void RunService()
		{
			this._worker = new BackgroundWorker();
			this._worker.RunWorkerAsync();
			this._worker.WorkerSupportsCancellation = true;
			this._worker.DoWork += this.WorkerDoWork;
			this._worker.RunWorkerCompleted += this.WorkerCompleted;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001A8DC File Offset: 0x00018ADC
		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			Thread.Sleep(10000);
			foreach (KeyValuePair<IntPtr, string> keyValuePair in WebHelperSlayer.GetOpenWindows())
			{
				try
				{
					uint num;
					WebHelperSlayer.GetWindowThreadProcessId(keyValuePair.Key, out num);
					if (Process.GetProcessById((int)num).ProcessName == "steamwebhelper")
					{
						WebHelperSlayer.ShowWindow(keyValuePair.Key, 0);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public static IDictionary<IntPtr, string> GetOpenWindows()
		{
			IntPtr shellWindow = WebHelperSlayer.GetShellWindow();
			Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();
			WebHelperSlayer.EnumWindows(delegate(IntPtr hWnd, int lParam)
			{
				if (hWnd == shellWindow)
				{
					return true;
				}
				if (!WebHelperSlayer.IsWindowVisible(hWnd))
				{
					return true;
				}
				int windowTextLength = WebHelperSlayer.GetWindowTextLength(hWnd);
				if (windowTextLength == 0)
				{
					return true;
				}
				StringBuilder stringBuilder = new StringBuilder(windowTextLength);
				WebHelperSlayer.GetWindowText(hWnd, stringBuilder, windowTextLength + 1);
				windows[hWnd] = stringBuilder.ToString();
				return true;
			}, 0);
			return windows;
		}

		// Token: 0x060003C2 RID: 962
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(WebHelperSlayer.EnumWindowsProc enumFunc, int lParam);

		// Token: 0x060003C3 RID: 963
		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x060003C4 RID: 964
		[DllImport("user32.dll")]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x060003C5 RID: 965
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x060003C6 RID: 966
		[DllImport("user32.dll")]
		private static extern IntPtr GetShellWindow();

		// Token: 0x060003C7 RID: 967
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x060003C8 RID: 968
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

		// Token: 0x060003C9 RID: 969 RVA: 0x0001A978 File Offset: 0x00018B78
		private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._worker.RunWorkerAsync();
		}

		// Token: 0x04000329 RID: 809
		private static volatile WebHelperSlayer Class;

		// Token: 0x0400032A RID: 810
		private static object SyncObject = new object();

		// Token: 0x0400032B RID: 811
		private BackgroundWorker _worker;

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x060003CD RID: 973
		private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
	}
}
