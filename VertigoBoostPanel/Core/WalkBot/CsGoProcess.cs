using System;
using System.Collections.Generic;
using System.Diagnostics;
using VertigoBoostPanel.Model;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000136 RID: 310
	public class CsGoProcess
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x00030ACC File Offset: 0x0002ECCC
		public CsGoProcess(IntPtr hWnd)
		{
			this.LoadCSGO(hWnd);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00030AF4 File Offset: 0x0002ECF4
		public void LoadCSGO(IntPtr hWnd)
		{
			foreach (Process process in Process.GetProcessesByName("csgo"))
			{
				if (process.MainWindowHandle == hWnd)
				{
					foreach (object obj in process.Modules)
					{
						ProcessModule processModule = (ProcessModule)obj;
						if (processModule.ModuleName == "client.dll" && processModule.BaseAddress != IntPtr.Zero)
						{
							this.processModules.Add(processModule.ModuleName.Replace(".dll", string.Empty), new GameModule(processModule.BaseAddress, processModule.ModuleMemorySize));
						}
						else if (processModule.ModuleName == "engine.dll" && processModule.BaseAddress != IntPtr.Zero)
						{
							this.processModules.Add(processModule.ModuleName.Replace(".dll", string.Empty), new GameModule(processModule.BaseAddress, processModule.ModuleMemorySize));
						}
						else if (processModule.ModuleName == "materialsystem.dll" && processModule.BaseAddress != IntPtr.Zero)
						{
							this.processModules.Add(processModule.ModuleName.Replace(".dll", string.Empty), new GameModule(processModule.BaseAddress, processModule.ModuleMemorySize));
						}
					}
					this.handle = WinAPI.OpenProcess(2035711, false, process.Id);
				}
			}
		}

		// Token: 0x04000613 RID: 1555
		public IntPtr handle;

		// Token: 0x04000614 RID: 1556
		public Dictionary<string, GameModule> processModules = new Dictionary<string, GameModule>();
	}
}
