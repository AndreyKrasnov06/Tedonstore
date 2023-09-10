using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace VertigoBoostPanel.Services.Optimization
{
	// Token: 0x0200009A RID: 154
	public class OverlayKiller
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001AA08 File Offset: 0x00018C08
		public static OverlayKiller GetInstance
		{
			get
			{
				if (OverlayKiller.Class == null)
				{
					object syncObject = OverlayKiller.SyncObject;
					lock (syncObject)
					{
						if (OverlayKiller.Class == null)
						{
							OverlayKiller.Class = new OverlayKiller();
						}
					}
				}
				return OverlayKiller.Class;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public int OptimizeTiming(bool elapsed)
		{
			if (!elapsed)
			{
				return 0;
			}
			return 5234;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001AA84 File Offset: 0x00018C84
		private void _startSuspend()
		{
			if (this.Started)
			{
				return;
			}
			this._task = new Thread(delegate
			{
				for (;;)
				{
					Thread.Sleep(1000);
					foreach (Process process in Process.GetProcesses())
					{
						if ((process.ProcessName.ToLower() == "gameoverlayui" || process.ProcessName.ToLower() == "steamwebhelper") && !this._suspendedProcesses.Contains((uint)process.Id))
						{
							this._suspendedProcesses.Add((uint)process.Id);
							OverlayKiller.DebugActiveProcess((uint)process.Id);
						}
					}
				}
			});
			this._task.Start();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001AABC File Offset: 0x00018CBC
		private void _stopSuspend()
		{
			if (this.Started)
			{
				Thread task = this._task;
				if (task != null)
				{
					task.Abort();
				}
				foreach (uint num in this._suspendedProcesses)
				{
					OverlayKiller.DebugActiveProcessStop(num);
				}
				this._suspendedProcesses.Clear();
				return;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00003B1A File Offset: 0x00001D1A
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001AB34 File Offset: 0x00018D34
		public bool Started
		{
			get
			{
				return this._started;
			}
			set
			{
				if (!value)
				{
					this._stopSuspend();
				}
				else
				{
					this._startSuspend();
				}
				this._started = value;
			}
		}

		// Token: 0x060003D8 RID: 984
		[DllImport("kernel32.dll")]
		private static extern bool DebugActiveProcess(uint dwProcessId);

		// Token: 0x060003D9 RID: 985
		[DllImport("kernel32.dll")]
		private static extern bool DebugActiveProcessStop(uint dwProcessId);

		// Token: 0x0400032E RID: 814
		private static volatile OverlayKiller Class;

		// Token: 0x0400032F RID: 815
		private static object SyncObject = new object();

		// Token: 0x04000330 RID: 816
		private List<uint> _suspendedProcesses = new List<uint>();

		// Token: 0x04000331 RID: 817
		private Thread _task;

		// Token: 0x04000332 RID: 818
		private bool _started;
	}
}
