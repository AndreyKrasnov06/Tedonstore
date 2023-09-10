using System;
using System.Collections.Generic;
using System.Globalization;
using VertigoBoostPanel.Model;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x0200013A RID: 314
	public static class PatternScanner
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x00004241 File Offset: 0x00002441
		public static int FindPattern(IntPtr _handle, string pattern_str, GameModule module, bool sub, int offset = 0, bool read = false, int startAddress = 0)
		{
			int num = PatternScanner.FindPatternEx(_handle, pattern_str, module, sub, offset, read, startAddress);
			GC.Collect();
			return num;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		public static int FindPatternEx(IntPtr _handle, string pattern_str, GameModule module, bool sub, int offset = 0, bool read = false, int startAddress = 0)
		{
			List<byte> list = new List<byte>();
			string text = "";
			foreach (string text2 in pattern_str.Split(new char[] { ' ' }))
			{
				if (!(text2 == "?") && !(text2 == "00"))
				{
					list.Add((byte)int.Parse(text2, NumberStyles.HexNumber));
					text += "x";
				}
				else
				{
					list.Add(0);
					text += "?";
				}
			}
			byte[] array2 = list.ToArray();
			int num = module.Size;
			IntPtr baseAddress = module.BaseAddress;
			if (startAddress != 0)
			{
				num = (int)baseAddress + num - startAddress;
				baseAddress = new IntPtr(startAddress);
			}
			if (num != 0)
			{
				byte[] array3 = new byte[num];
				int num2;
				if (WinAPI.ReadProcessMemory(_handle, baseAddress, array3, num, out num2))
				{
					for (int j = 0; j < num; j++)
					{
						bool flag = true;
						int num3 = 0;
						while (num3 < text.Length && (flag = text[num3] == '?' || array3[num3 + j] == array2[num3]))
						{
							num3++;
						}
						if (flag)
						{
							j += (int)baseAddress;
							if (read)
							{
								j = Memory.ReadFromProcess<int>(_handle, new IntPtr(j + offset));
							}
							if (sub)
							{
								j -= (int)baseAddress;
							}
							return j;
						}
					}
				}
				return 0;
			}
			throw new Exception(string.Format("Size of module {0} is INVALID.", module));
		}
	}
}
