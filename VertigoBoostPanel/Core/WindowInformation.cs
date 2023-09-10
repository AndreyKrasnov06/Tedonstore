using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000E3 RID: 227
	public class WindowInformation
	{
		// Token: 0x060004A5 RID: 1189
		[DllImport("user32")]
		private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001FD98 File Offset: 0x0001DF98
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Window ",
				this.Handle.ToString(),
				" \"",
				this.Caption,
				"\" ",
				this.Class
			});
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		public List<IntPtr> ChildWindowHandles
		{
			get
			{
				List<IntPtr> list;
				try
				{
					list = (from c in this.ChildWindows.AsEnumerable<WindowInformation>()
						select c.Handle).ToList<IntPtr>();
				}
				catch (Exception)
				{
					list = null;
				}
				return list;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00003C37 File Offset: 0x00001E37
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001FE44 File Offset: 0x0001E044
		public WindowInformation Parent { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001FE58 File Offset: 0x0001E058
		public IntPtr ParentHandle
		{
			get
			{
				if (this.Parent == null)
				{
					return IntPtr.Zero;
				}
				return this.Parent.Handle;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001FE80 File Offset: 0x0001E080
		public Process Process
		{
			get
			{
				Process process;
				try
				{
					int num = 0;
					WindowInformation.GetWindowThreadProcessId(this.Handle, out num);
					process = Process.GetProcessById(num);
				}
				catch (Exception)
				{
					process = null;
				}
				return process;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
		public List<IntPtr> SiblingWindowHandles
		{
			get
			{
				List<IntPtr> list;
				try
				{
					list = (from s in this.SiblingWindows.AsEnumerable<WindowInformation>()
						select s.Handle).ToList<IntPtr>();
				}
				catch (Exception)
				{
					list = null;
				}
				return list;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001FF20 File Offset: 0x0001E120
		public int ThreadID
		{
			get
			{
				int num2;
				try
				{
					int num = 0;
					num2 = WindowInformation.GetWindowThreadProcessId(this.Handle, out num);
				}
				catch (Exception)
				{
					num2 = -1;
				}
				return num2;
			}
		}

		// Token: 0x04000464 RID: 1124
		public string Caption = string.Empty;

		// Token: 0x04000465 RID: 1125
		public string Class = string.Empty;

		// Token: 0x04000466 RID: 1126
		public List<WindowInformation> ChildWindows = new List<WindowInformation>();

		// Token: 0x04000467 RID: 1127
		public IntPtr Handle;

		// Token: 0x04000469 RID: 1129
		public List<WindowInformation> SiblingWindows = new List<WindowInformation>();
	}
}
