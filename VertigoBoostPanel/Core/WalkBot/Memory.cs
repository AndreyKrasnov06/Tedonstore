using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000138 RID: 312
	public static class Memory
	{
		// Token: 0x0600067A RID: 1658
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x0600067B RID: 1659
		[DllImport("kernel32.dll")]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

		// Token: 0x0600067C RID: 1660
		[DllImport("kernel32.dll")]
		internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);

		// Token: 0x0600067D RID: 1661
		[DllImport("kernel32.dll")]
		internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, ref uint lpNumberOfBytesWritten);

		// Token: 0x0600067E RID: 1662
		[DllImport("kernel32.dll")]
		internal static extern bool CloseHandle(IntPtr hProcess);

		// Token: 0x0600067F RID: 1663 RVA: 0x00030D24 File Offset: 0x0002EF24
		public static void CloseProcess(IntPtr phandle)
		{
			Memory.CloseHandle(phandle);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00030D38 File Offset: 0x0002EF38
		public static T GetStructure<T>(byte[] bytes)
		{
			GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T t = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
			gchandle.Free();
			return t;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00030D70 File Offset: 0x0002EF70
		public static byte[] GetStructBytes<T>(T str)
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<T>(str, intPtr, true);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00030DB4 File Offset: 0x0002EFB4
		public static void Write<T>(IntPtr handle, int address, T value)
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<T>(value, intPtr, true);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			uint num2 = 0U;
			Memory.WriteProcessMemory(handle, (IntPtr)address, array, (IntPtr)num, ref num2);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00030E10 File Offset: 0x0002F010
		public static void WriteBytes(IntPtr handle, int address, byte[] value)
		{
			uint num = 0U;
			Memory.WriteProcessMemory(handle, (IntPtr)address, value, (IntPtr)value.Length, ref num);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00030E38 File Offset: 0x0002F038
		public static string ReadString(IntPtr handle, int address, int bufferSize, Encoding enc)
		{
			byte[] array = new byte[bufferSize];
			uint num = 0U;
			Memory.ReadProcessMemory(handle, (IntPtr)address, array, (uint)bufferSize, ref num);
			string text = enc.GetString(array);
			if (text.Contains("\0"))
			{
				text = text.Substring(0, text.IndexOf('\0'));
			}
			return text;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00030E90 File Offset: 0x0002F090
		public static T ReadFromProcess<T>(IntPtr handle, IntPtr address)
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(T))];
			IntPtr intPtr;
			Memory.ReadProcessMemory(handle, address, array, array.Length, out intPtr);
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			T t = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
			gchandle.Free();
			return t;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00030EEC File Offset: 0x0002F0EC
		public static byte[] ReadBytes(IntPtr handle, int address, int length)
		{
			byte[] array = new byte[length];
			uint num = 0U;
			Memory.ReadProcessMemory(handle, (IntPtr)address, array, (uint)length, ref num);
			return array;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00030F18 File Offset: 0x0002F118
		public static T Read<T>(IntPtr handle, int address)
		{
			int num = Marshal.SizeOf(typeof(T));
			if (typeof(T) == typeof(bool))
			{
				num = 1;
			}
			byte[] array = new byte[num];
			uint num2 = 0U;
			Memory.ReadProcessMemory(handle, (IntPtr)address, array, (uint)num, ref num2);
			return Memory.GetStructure<T>(array);
		}
	}
}
