using System;
using System.Runtime.InteropServices;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x0200013C RID: 316
	public static class WinAPI
	{
		// Token: 0x0600068B RID: 1675
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern IntPtr RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

		// Token: 0x0600068C RID: 1676
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x0600068D RID: 1677
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x0600068E RID: 1678
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x0600068F RID: 1679
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtQueryInformationThread(IntPtr threadHandle, WinAPI.ThreadInfoClass threadInformationClass, ref WinAPI.THREAD_BASIC_INFORMATION threadInformation, int threadInformationLength, IntPtr returnLengthPtr);

		// Token: 0x06000690 RID: 1680
		[DllImport("kernel32.dll")]
		public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int nSize, uint flNewProtect, out uint lpflOldProtect);

		// Token: 0x06000691 RID: 1681
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06000692 RID: 1682
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

		// Token: 0x06000693 RID: 1683
		[DllImport("kernel32.dll")]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

		// Token: 0x06000694 RID: 1684
		[DllImport("kernel32.dll")]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x06000695 RID: 1685
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		// Token: 0x06000696 RID: 1686
		[DllImport("kernel32.dll")]
		public static extern void WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

		// Token: 0x06000697 RID: 1687
		[DllImport("kernel32.dll")]
		public static extern bool TerminateThread(IntPtr hHandle, uint dwExitCode);

		// Token: 0x06000698 RID: 1688
		[DllImport("kernel32.dll")]
		public static extern bool SetThreadPriority(IntPtr hHandle, int nPriority);

		// Token: 0x06000699 RID: 1689
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

		// Token: 0x0600069A RID: 1690
		[DllImport("kernel32.dll")]
		public static extern int GetThreadId(IntPtr hHandle);

		// Token: 0x0600069B RID: 1691
		[DllImport("ntdll.dll")]
		public static extern int RtlRemoteCall(IntPtr Process, IntPtr Thread, IntPtr CallSite, long ArgumentCount, uint Arguments, bool PassContext, bool AlreadySuspended);

		// Token: 0x0600069C RID: 1692
		[DllImport("kernel32.dll")]
		public static extern int SuspendThread(IntPtr hHandle);

		// Token: 0x0600069D RID: 1693
		[DllImport("kernel32.dll")]
		public static extern int ResumeThread(IntPtr handle);

		// Token: 0x0600069E RID: 1694
		[DllImport("kernel32.dll")]
		public static extern bool GetThreadContext(IntPtr hHandle, [Out] int[] context);

		// Token: 0x0600069F RID: 1695
		[DllImport("kernel32.dll")]
		public static extern bool SetThreadContext(IntPtr thread, [In] int[] context);

		// Token: 0x060006A0 RID: 1696
		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey);

		// Token: 0x060006A1 RID: 1697
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

		// Token: 0x04000625 RID: 1573
		public const int MEM_COMMIT = 4096;

		// Token: 0x04000626 RID: 1574
		public const int MEM_RELEASE = 2048;

		// Token: 0x04000627 RID: 1575
		public const int MEM_RESERVE = 8192;

		// Token: 0x04000628 RID: 1576
		public const int PAGE_READWRITE = 64;

		// Token: 0x04000629 RID: 1577
		public const int PROCESS_VM_OPERATION = 8;

		// Token: 0x0400062A RID: 1578
		public const int PROCESS_VM_READ = 16;

		// Token: 0x0400062B RID: 1579
		public const int PROCESS_VM_WRITE = 32;

		// Token: 0x0200013D RID: 317
		public struct CLIENT_ID
		{
			// Token: 0x0400062C RID: 1580
			public IntPtr UniqueProcess;

			// Token: 0x0400062D RID: 1581
			public IntPtr UniqueThread;
		}

		// Token: 0x0200013E RID: 318
		public struct THREAD_BASIC_INFORMATION
		{
			// Token: 0x0400062E RID: 1582
			public int ExitStatus;

			// Token: 0x0400062F RID: 1583
			public IntPtr TebBaseAdress;

			// Token: 0x04000630 RID: 1584
			public WinAPI.CLIENT_ID ClientId;

			// Token: 0x04000631 RID: 1585
			public IntPtr AffinityMask;

			// Token: 0x04000632 RID: 1586
			public int Priority;

			// Token: 0x04000633 RID: 1587
			public int BasePriority;
		}

		// Token: 0x0200013F RID: 319
		public enum Protection : uint
		{
			// Token: 0x04000635 RID: 1589
			PAGE_NOACCESS = 1U,
			// Token: 0x04000636 RID: 1590
			PAGE_READONLY,
			// Token: 0x04000637 RID: 1591
			PAGE_READWRITE = 4U,
			// Token: 0x04000638 RID: 1592
			PAGE_WRITECOPY = 8U,
			// Token: 0x04000639 RID: 1593
			PAGE_EXECUTE = 16U,
			// Token: 0x0400063A RID: 1594
			PAGE_EXECUTE_READ = 32U,
			// Token: 0x0400063B RID: 1595
			PAGE_EXECUTE_READWRITE = 64U,
			// Token: 0x0400063C RID: 1596
			PAGE_EXECUTE_WRITECOPY = 128U,
			// Token: 0x0400063D RID: 1597
			PAGE_GUARD = 256U,
			// Token: 0x0400063E RID: 1598
			PAGE_NOCACHE = 512U,
			// Token: 0x0400063F RID: 1599
			PAGE_WRITECOMBINE = 1024U
		}

		// Token: 0x02000140 RID: 320
		public enum ThreadInfoClass
		{
			// Token: 0x04000641 RID: 1601
			ThreadBasicInformation,
			// Token: 0x04000642 RID: 1602
			ThreadQuerySetWin32StartAddress = 9
		}
	}
}
