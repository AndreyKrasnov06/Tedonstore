using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200014C RID: 332
internal class Class1
{
	// Token: 0x060006C5 RID: 1733 RVA: 0x0003121C File Offset: 0x0002F41C
	static Class1()
	{
		try
		{
			RSACryptoServiceProvider.UseMachineKeyStore = true;
		}
		catch
		{
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00003148 File Offset: 0x00001348
	private void method_0()
	{
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00031398 File Offset: 0x0002F598
	internal static byte[] smethod_0(object object_3)
	{
		uint[] array = new uint[16];
		uint num = (uint)((448 - object_3.Length * 8 % 512 + 512) % 512);
		if (num == 0U)
		{
			num = 512U;
		}
		uint num2 = (uint)((long)object_3.Length + (long)((ulong)(num / 8U)) + 8L);
		ulong num3 = (ulong)((long)object_3.Length * 8L);
		byte[] array2 = new byte[num2];
		for (int i = 0; i < object_3.Length; i++)
		{
			array2[i] = object_3[i];
		}
		byte[] array3 = array2;
		int num4 = object_3.Length;
		array3[num4] |= 128;
		for (int j = 8; j > 0; j--)
		{
			array2[(int)(checked((IntPtr)(unchecked((ulong)num2 - (ulong)((long)j)))))] = (byte)((num3 >> (8 - j) * 8) & 255UL);
		}
		uint num5 = (uint)(array2.Length * 8 / 32);
		uint num6 = 1732584193U;
		uint num7 = 4023233417U;
		uint num8 = 2562383102U;
		uint num9 = 271733878U;
		for (uint num10 = 0U; num10 < num5 / 16U; num10 += 1U)
		{
			uint num11 = num10 << 6;
			for (uint num12 = 0U; num12 < 61U; num12 += 4U)
			{
				array[(int)(num12 >> 2)] = (uint)(((int)array2[(int)(num11 + (num12 + 3U))] << 24) | ((int)array2[(int)(num11 + (num12 + 2U))] << 16) | ((int)array2[(int)(num11 + (num12 + 1U))] << 8) | (int)array2[(int)(num11 + num12)]);
			}
			uint num13 = num6;
			uint num14 = num7;
			uint num15 = num8;
			uint num16 = num9;
			Class1.ReZxSxiJZ(ref num6, num7, num8, num9, 0U, 7, 1U, array);
			Class1.ReZxSxiJZ(ref num9, num6, num7, num8, 1U, 12, 2U, array);
			Class1.ReZxSxiJZ(ref num8, num9, num6, num7, 2U, 17, 3U, array);
			Class1.ReZxSxiJZ(ref num7, num8, num9, num6, 3U, 22, 4U, array);
			Class1.ReZxSxiJZ(ref num6, num7, num8, num9, 4U, 7, 5U, array);
			Class1.ReZxSxiJZ(ref num9, num6, num7, num8, 5U, 12, 6U, array);
			Class1.ReZxSxiJZ(ref num8, num9, num6, num7, 6U, 17, 7U, array);
			Class1.ReZxSxiJZ(ref num7, num8, num9, num6, 7U, 22, 8U, array);
			Class1.ReZxSxiJZ(ref num6, num7, num8, num9, 8U, 7, 9U, array);
			Class1.ReZxSxiJZ(ref num9, num6, num7, num8, 9U, 12, 10U, array);
			Class1.ReZxSxiJZ(ref num8, num9, num6, num7, 10U, 17, 11U, array);
			Class1.ReZxSxiJZ(ref num7, num8, num9, num6, 11U, 22, 12U, array);
			Class1.ReZxSxiJZ(ref num6, num7, num8, num9, 12U, 7, 13U, array);
			Class1.ReZxSxiJZ(ref num9, num6, num7, num8, 13U, 12, 14U, array);
			Class1.ReZxSxiJZ(ref num8, num9, num6, num7, 14U, 17, 15U, array);
			Class1.ReZxSxiJZ(ref num7, num8, num9, num6, 15U, 22, 16U, array);
			Class1.smethod_1(ref num6, num7, num8, num9, 1U, 5, 17U, array);
			Class1.smethod_1(ref num9, num6, num7, num8, 6U, 9, 18U, array);
			Class1.smethod_1(ref num8, num9, num6, num7, 11U, 14, 19U, array);
			Class1.smethod_1(ref num7, num8, num9, num6, 0U, 20, 20U, array);
			Class1.smethod_1(ref num6, num7, num8, num9, 5U, 5, 21U, array);
			Class1.smethod_1(ref num9, num6, num7, num8, 10U, 9, 22U, array);
			Class1.smethod_1(ref num8, num9, num6, num7, 15U, 14, 23U, array);
			Class1.smethod_1(ref num7, num8, num9, num6, 4U, 20, 24U, array);
			Class1.smethod_1(ref num6, num7, num8, num9, 9U, 5, 25U, array);
			Class1.smethod_1(ref num9, num6, num7, num8, 14U, 9, 26U, array);
			Class1.smethod_1(ref num8, num9, num6, num7, 3U, 14, 27U, array);
			Class1.smethod_1(ref num7, num8, num9, num6, 8U, 20, 28U, array);
			Class1.smethod_1(ref num6, num7, num8, num9, 13U, 5, 29U, array);
			Class1.smethod_1(ref num9, num6, num7, num8, 2U, 9, 30U, array);
			Class1.smethod_1(ref num8, num9, num6, num7, 7U, 14, 31U, array);
			Class1.smethod_1(ref num7, num8, num9, num6, 12U, 20, 32U, array);
			Class1.smethod_2(ref num6, num7, num8, num9, 5U, 4, 33U, array);
			Class1.smethod_2(ref num9, num6, num7, num8, 8U, 11, 34U, array);
			Class1.smethod_2(ref num8, num9, num6, num7, 11U, 16, 35U, array);
			Class1.smethod_2(ref num7, num8, num9, num6, 14U, 23, 36U, array);
			Class1.smethod_2(ref num6, num7, num8, num9, 1U, 4, 37U, array);
			Class1.smethod_2(ref num9, num6, num7, num8, 4U, 11, 38U, array);
			Class1.smethod_2(ref num8, num9, num6, num7, 7U, 16, 39U, array);
			Class1.smethod_2(ref num7, num8, num9, num6, 10U, 23, 40U, array);
			Class1.smethod_2(ref num6, num7, num8, num9, 13U, 4, 41U, array);
			Class1.smethod_2(ref num9, num6, num7, num8, 0U, 11, 42U, array);
			Class1.smethod_2(ref num8, num9, num6, num7, 3U, 16, 43U, array);
			Class1.smethod_2(ref num7, num8, num9, num6, 6U, 23, 44U, array);
			Class1.smethod_2(ref num6, num7, num8, num9, 9U, 4, 45U, array);
			Class1.smethod_2(ref num9, num6, num7, num8, 12U, 11, 46U, array);
			Class1.smethod_2(ref num8, num9, num6, num7, 15U, 16, 47U, array);
			Class1.smethod_2(ref num7, num8, num9, num6, 2U, 23, 48U, array);
			Class1.smethod_3(ref num6, num7, num8, num9, 0U, 6, 49U, array);
			Class1.smethod_3(ref num9, num6, num7, num8, 7U, 10, 50U, array);
			Class1.smethod_3(ref num8, num9, num6, num7, 14U, 15, 51U, array);
			Class1.smethod_3(ref num7, num8, num9, num6, 5U, 21, 52U, array);
			Class1.smethod_3(ref num6, num7, num8, num9, 12U, 6, 53U, array);
			Class1.smethod_3(ref num9, num6, num7, num8, 3U, 10, 54U, array);
			Class1.smethod_3(ref num8, num9, num6, num7, 10U, 15, 55U, array);
			Class1.smethod_3(ref num7, num8, num9, num6, 1U, 21, 56U, array);
			Class1.smethod_3(ref num6, num7, num8, num9, 8U, 6, 57U, array);
			Class1.smethod_3(ref num9, num6, num7, num8, 15U, 10, 58U, array);
			Class1.smethod_3(ref num8, num9, num6, num7, 6U, 15, 59U, array);
			Class1.smethod_3(ref num7, num8, num9, num6, 13U, 21, 60U, array);
			Class1.smethod_3(ref num6, num7, num8, num9, 4U, 6, 61U, array);
			Class1.smethod_3(ref num9, num6, num7, num8, 11U, 10, 62U, array);
			Class1.smethod_3(ref num8, num9, num6, num7, 2U, 15, 63U, array);
			Class1.smethod_3(ref num7, num8, num9, num6, 9U, 21, 64U, array);
			num6 += num13;
			num7 += num14;
			num8 += num15;
			num9 += num16;
		}
		byte[] array4 = new byte[16];
		Array.Copy(BitConverter.GetBytes(num6), 0, array4, 0, 4);
		Array.Copy(BitConverter.GetBytes(num7), 0, array4, 4, 4);
		Array.Copy(BitConverter.GetBytes(num8), 0, array4, 8, 4);
		Array.Copy(BitConverter.GetBytes(num9), 0, array4, 12, 4);
		return array4;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00004257 File Offset: 0x00002457
	private static void ReZxSxiJZ(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_3)
	{
		uint_1 = uint_2 + Class1.smethod_4(uint_1 + ((uint_2 & uint_3) | (~uint_2 & uint_4)) + object_3[(int)uint_5] + Class1.uint_0[(int)(uint_6 - 1U)], ushort_0);
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00004280 File Offset: 0x00002480
	private static void smethod_1(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_3)
	{
		uint_1 = uint_2 + Class1.smethod_4(uint_1 + ((uint_2 & uint_4) | (uint_3 & ~uint_4)) + object_3[(int)uint_5] + Class1.uint_0[(int)(uint_6 - 1U)], ushort_0);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x000042A9 File Offset: 0x000024A9
	private static void smethod_2(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_3)
	{
		uint_1 = uint_2 + Class1.smethod_4(uint_1 + (uint_2 ^ uint_3 ^ uint_4) + object_3[(int)uint_5] + Class1.uint_0[(int)(uint_6 - 1U)], ushort_0);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x000042CF File Offset: 0x000024CF
	private static void smethod_3(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_3)
	{
		uint_1 = uint_2 + Class1.smethod_4(uint_1 + (uint_3 ^ (uint_2 | ~uint_4)) + object_3[(int)uint_5] + Class1.uint_0[(int)(uint_6 - 1U)], ushort_0);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x000042F6 File Offset: 0x000024F6
	private static uint smethod_4(uint uint_1, ushort ushort_0)
	{
		return (uint_1 >> (int)(32 - ushort_0)) | (uint_1 << (int)ushort_0);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00004308 File Offset: 0x00002508
	internal static bool smethod_5()
	{
		if (!Class1.bool_1)
		{
			Class1.smethod_7();
			Class1.bool_1 = true;
		}
		return Class1.bool_4;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00004321 File Offset: 0x00002521
	internal Class1()
	{
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x000319F8 File Offset: 0x0002FBF8
	private void method_1(byte[] byte_2, byte[] byte_3, byte[] byte_4)
	{
		int num = byte_4.Length % 4;
		int num2 = byte_4.Length / 4;
		byte[] array = new byte[byte_4.Length];
		int num3 = byte_2.Length / 4;
		uint num4 = 0U;
		if (num > 0)
		{
			num2++;
		}
		for (int i = 0; i < num2; i++)
		{
			int num5 = i % num3;
			int num6 = i * 4;
			uint num7 = (uint)(num5 * 4);
			uint num8 = (uint)(((int)byte_2[(int)(num7 + 3U)] << 24) | ((int)byte_2[(int)(num7 + 2U)] << 16) | ((int)byte_2[(int)(num7 + 1U)] << 8) | (int)byte_2[(int)num7]);
			uint num9 = 255U;
			int num10 = 0;
			uint num11;
			if (i == num2 - 1 && num > 0)
			{
				num11 = 0U;
				num4 += num8;
				for (int j = 0; j < num; j++)
				{
					if (j > 0)
					{
						num11 <<= 8;
					}
					num11 |= (uint)byte_4[byte_4.Length - (1 + j)];
				}
			}
			else
			{
				num4 += num8;
				num7 = (uint)num6;
				num11 = (uint)(((int)byte_4[(int)(num7 + 3U)] << 24) | ((int)byte_4[(int)(num7 + 2U)] << 16) | ((int)byte_4[(int)(num7 + 1U)] << 8) | (int)byte_4[(int)num7]);
			}
			uint num13;
			uint num12 = (num13 = num4);
			num13 += 744984710U;
			uint num14 = num13 ^ 1209970465U ^ num13;
			uint num15 = num13 + 1351004559U - 694324608U;
			uint num16 = num13 / 1863877164U + 1863877164U;
			uint num17 = (num13 + num13) * num16 + num13;
			num13 ^= num13 >> 15;
			num13 += num14;
			num13 ^= num13 << 5;
			num13 += num15;
			num13 ^= num13 >> 4;
			num13 += num17;
			num13 = (((num14 << 21) - num14) ^ num14) + num13;
			num4 = num12 + (uint)num13;
			if (i == num2 - 1 && num > 0)
			{
				uint num18 = num4 ^ num11;
				for (int k = 0; k < num; k++)
				{
					if (k > 0)
					{
						num9 <<= 8;
						num10 += 8;
					}
					array[num6 + k] = (byte)((num18 & num9) >> num10);
				}
			}
			else
			{
				uint num19 = num4 ^ num11;
				array[num6] = (byte)(num19 & 255U);
				array[num6 + 1] = (byte)((num19 & 65280U) >> 8);
				array[num6 + 2] = (byte)((num19 & 16711680U) >> 16);
				array[num6 + 3] = (byte)((num19 & 4278190080U) >> 24);
			}
		}
		Class1.byte_0 = array;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00031C9C File Offset: 0x0002FE9C
	internal static SymmetricAlgorithm smethod_6()
	{
		SymmetricAlgorithm symmetricAlgorithm = null;
		if (Class1.smethod_5())
		{
			symmetricAlgorithm = new AesCryptoServiceProvider();
		}
		else
		{
			try
			{
				symmetricAlgorithm = new RijndaelManaged();
			}
			catch
			{
				try
				{
					symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Security.Cryptography.AesCryptoServiceProvider").Unwrap();
				}
				catch
				{
					symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Security.Cryptography.AesCryptoServiceProvider").Unwrap();
				}
			}
		}
		return symmetricAlgorithm;
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00031D1C File Offset: 0x0002FF1C
	internal static void smethod_7()
	{
		try
		{
			new MD5CryptoServiceProvider();
		}
		catch
		{
			Class1.bool_4 = true;
			return;
		}
		try
		{
			Class1.bool_4 = CryptoConfig.AllowOnlyFipsAlgorithms;
		}
		catch
		{
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00004329 File Offset: 0x00002529
	internal static byte[] smethod_8(byte[] byte_2)
	{
		if (!Class1.smethod_5())
		{
			return new MD5CryptoServiceProvider().ComputeHash(byte_2);
		}
		return Class1.smethod_0(byte_2);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00031D68 File Offset: 0x0002FF68
	internal static void smethod_9(HashAlgorithm hashAlgorithm_0, Stream stream_0, uint uint_1, byte[] byte_2)
	{
		while (uint_1 > 0U)
		{
			int num = ((uint_1 > (uint)byte_2.Length) ? byte_2.Length : ((int)uint_1));
			stream_0.Read(byte_2, 0, num);
			Class1.VeZjNyNqf(hashAlgorithm_0, byte_2, 0, num);
			uint_1 -= (uint)num;
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00004344 File Offset: 0x00002544
	internal static void VeZjNyNqf(HashAlgorithm hashAlgorithm_0, byte[] byte_2, int int_6, int int_7)
	{
		hashAlgorithm_0.TransformBlock(byte_2, int_6, int_7, byte_2, int_6);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00031DA4 File Offset: 0x0002FFA4
	internal static uint smethod_10(uint uint_1, int int_6, long long_2, BinaryReader binaryReader_0)
	{
		for (int i = 0; i < int_6; i++)
		{
			binaryReader_0.BaseStream.Position = long_2 + (long)(i * 40 + 8);
			uint num = binaryReader_0.ReadUInt32();
			uint num2 = binaryReader_0.ReadUInt32();
			binaryReader_0.ReadUInt32();
			uint num3 = binaryReader_0.ReadUInt32();
			if (num2 <= uint_1 && uint_1 < num2 + num)
			{
				return num3 + uint_1 - num2;
			}
		}
		return 0U;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00031E00 File Offset: 0x00030000
	private static void smethod_11(object object_3, int int_6)
	{
		Class7.smethod_2(0, new object[] { object_3, int_6 }, null);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00031E40 File Offset: 0x00030040
	internal static string smethod_12(string string_1)
	{
		"{11111-22222-50001-00000}".Trim();
		byte[] array = Convert.FromBase64String(string_1);
		return Encoding.Unicode.GetString(array, 0, array.Length);
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00031E70 File Offset: 0x00030070
	internal static uint smethod_13(IntPtr intptr_4, IntPtr intptr_5, IntPtr intptr_6, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_7, ref uint uint_2)
	{
		IntPtr intPtr = intptr_6;
		if (Class1.bool_3)
		{
			intPtr = intptr_5;
		}
		long num;
		if (IntPtr.Size == 4)
		{
			num = (long)Marshal.ReadInt32(intPtr, IntPtr.Size * 2);
		}
		else
		{
			num = Marshal.ReadInt64(intPtr, IntPtr.Size * 2);
		}
		object obj = Class1.hashtable_0[num];
		if (obj == null)
		{
			return Class1.delegate2_0(intptr_4, intptr_5, intptr_6, uint_1, intptr_7, ref uint_2);
		}
		Class1.Struct1 @struct = (Class1.Struct1)obj;
		IntPtr intPtr2 = Marshal.AllocCoTaskMem(@struct.byte_0.Length);
		Marshal.Copy(@struct.byte_0, 0, intPtr2, @struct.byte_0.Length);
		if (@struct.bool_0)
		{
			intptr_7 = intPtr2;
			uint_2 = (uint)@struct.byte_0.Length;
			Class1.smethod_22(intptr_7, @struct.byte_0.Length, 64, ref Class1.int_1);
			return 0U;
		}
		Marshal.WriteIntPtr(intPtr, IntPtr.Size * 2, intPtr2);
		Marshal.WriteInt32(intPtr, IntPtr.Size * 3, @struct.byte_0.Length);
		uint num2 = 0U;
		if (uint_1 == 216669565U && !Class1.firstrundone)
		{
			Class1.firstrundone = true;
		}
		else
		{
			num2 = Class1.delegate2_0(intptr_4, intptr_5, intptr_6, uint_1, intptr_7, ref uint_2);
			Marshal.WriteIntPtr(intPtr, IntPtr.Size * 2, IntPtr.Zero);
		}
		return num2;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00004352 File Offset: 0x00002552
	private static int smethod_14()
	{
		return 5;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00031FA8 File Offset: 0x000301A8
	private static void smethod_15()
	{
		try
		{
			RSACryptoServiceProvider.UseMachineKeyStore = true;
		}
		catch
		{
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00031FD0 File Offset: 0x000301D0
	private static Delegate smethod_16(IntPtr intptr_4, Type type_0)
	{
		return (Delegate)typeof(Marshal).GetMethod("GetDelegateForFunctionPointer", new Type[]
		{
			typeof(IntPtr),
			typeof(Type)
		}).Invoke(null, new object[] { intptr_4, type_0 });
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00032030 File Offset: 0x00030230
	internal unsafe static void smethod_17()
	{
		if (!Class1.usfLnHavse)
		{
			Class1.usfLnHavse = true;
			long num = 0L;
			Marshal.ReadIntPtr(new IntPtr((void*)(&num)), 0);
			Marshal.ReadInt32(new IntPtr((void*)(&num)), 0);
			Marshal.ReadInt64(new IntPtr((void*)(&num)), 0);
			Marshal.WriteIntPtr(new IntPtr((void*)(&num)), 0, IntPtr.Zero);
			Marshal.WriteInt32(new IntPtr((void*)(&num)), 0, 0);
			Marshal.WriteInt64(new IntPtr((void*)(&num)), 0, 0L);
			Marshal.Copy(new byte[1], 0, Marshal.AllocCoTaskMem(8), 1);
			Class1.smethod_15();
			if (!(Class1.smethod_19(Process.GetCurrentProcess().MainModule.BaseAddress, "__", 10U) != IntPtr.Zero))
			{
				return;
			}
			if (IntPtr.Size == 4 && Type.GetType("System.Reflection.ReflectionContext", false) != null)
			{
				foreach (object obj in Process.GetCurrentProcess().Modules)
				{
					ProcessModule processModule = (ProcessModule)obj;
					if (processModule.ModuleName.ToLower() == "clrjit.dll")
					{
						Version version = new Version(processModule.FileVersionInfo.ProductMajorPart, processModule.FileVersionInfo.ProductMinorPart, processModule.FileVersionInfo.ProductBuildPart, processModule.FileVersionInfo.ProductPrivatePart);
						Version version2 = new Version(4, 0, 30319, 17020);
						Version version3 = new Version(4, 0, 30319, 17921);
						if (version >= version2 && version < version3)
						{
							Class1.bool_3 = true;
							break;
						}
					}
				}
			}
			Class1.Class4 @class = new Class1.Class4(Class1.assembly_0.GetManifestResourceStream("KegUuyYuHP5EmI1LYL.84uOv4nTZvHLFRB3U6"));
			@class.method_0().Position = 0L;
			byte[] array = @class.method_1((int)@class.method_0().Length);
			byte[] array2 = new byte[32];
			array2[0] = 94;
			array2[0] = 216;
			array2[0] = 113;
			array2[0] = 99;
			array2[0] = 182;
			array2[1] = 142;
			array2[1] = 124;
			array2[1] = 155;
			array2[1] = 100;
			array2[1] = 38;
			array2[2] = 140;
			array2[2] = 49;
			array2[2] = 130;
			array2[2] = 174;
			array2[2] = 161;
			array2[2] = 234;
			array2[3] = 35;
			array2[3] = 120;
			array2[3] = 157;
			array2[3] = 88;
			array2[3] = 162;
			array2[4] = 172;
			array2[4] = 96;
			array2[4] = 75;
			array2[5] = 161;
			array2[5] = 158;
			array2[5] = 151;
			array2[5] = 146;
			array2[5] = 88;
			array2[6] = 99;
			array2[6] = 162;
			array2[6] = 155;
			array2[6] = 21;
			array2[7] = 166;
			array2[7] = 92;
			array2[7] = 235;
			array2[7] = 251;
			array2[8] = 166;
			array2[8] = 129;
			array2[8] = 217;
			array2[9] = 104;
			array2[9] = 112;
			array2[9] = 89;
			array2[9] = 128;
			array2[9] = 105;
			array2[10] = 168;
			array2[10] = 160;
			array2[10] = 59;
			array2[10] = 105;
			array2[10] = 104;
			array2[11] = 150;
			array2[11] = 161;
			array2[11] = 112;
			array2[11] = 100;
			array2[11] = 155;
			array2[11] = 164;
			array2[12] = 114;
			array2[12] = 123;
			array2[12] = 160;
			array2[12] = 104;
			array2[13] = 84;
			array2[13] = 108;
			array2[13] = 139;
			array2[13] = 114;
			array2[13] = 195;
			array2[14] = 87;
			array2[14] = 109;
			array2[14] = 117;
			array2[14] = 153;
			array2[15] = 161;
			array2[15] = 162;
			array2[15] = 114;
			array2[15] = 136;
			array2[15] = 20;
			array2[16] = 129;
			array2[16] = 74;
			array2[16] = 191;
			array2[16] = 40;
			array2[16] = 106;
			array2[16] = 23;
			array2[17] = 123;
			array2[17] = 117;
			array2[17] = 178;
			array2[17] = 19;
			array2[17] = 15;
			array2[18] = 88;
			array2[18] = 94;
			array2[18] = 162;
			array2[18] = 99;
			array2[18] = 191;
			array2[18] = 245;
			array2[19] = 150;
			array2[19] = 22;
			array2[19] = 121;
			array2[20] = 86;
			array2[20] = 94;
			array2[20] = 145;
			array2[20] = 80;
			array2[20] = 74;
			array2[21] = 70;
			array2[21] = 124;
			array2[21] = 35;
			array2[22] = 91;
			array2[22] = 122;
			array2[22] = 200;
			array2[23] = 90;
			array2[23] = 149;
			array2[23] = 170;
			array2[23] = 131;
			array2[23] = 78;
			array2[24] = 45;
			array2[24] = 128;
			array2[24] = 95;
			array2[25] = 147;
			array2[25] = 90;
			array2[25] = 85;
			array2[25] = 136;
			array2[25] = 159;
			array2[25] = 188;
			array2[26] = 118;
			array2[26] = 163;
			array2[26] = 63;
			array2[27] = 100;
			array2[27] = 92;
			array2[27] = 86;
			array2[28] = 128;
			array2[28] = 155;
			array2[28] = 108;
			array2[28] = 76;
			array2[29] = 108;
			array2[29] = 88;
			array2[29] = 194;
			array2[30] = 91;
			array2[30] = 36;
			array2[30] = 89;
			array2[31] = 206;
			array2[31] = 137;
			array2[31] = 29;
			byte[] array3 = array2;
			byte[] array4 = new byte[16];
			array4[0] = 88;
			array4[0] = 166;
			array4[0] = 113;
			array4[0] = 136;
			array4[1] = 165;
			array4[1] = 124;
			array4[1] = 104;
			array4[1] = 189;
			array4[2] = 72;
			array4[2] = 152;
			array4[2] = 147;
			array4[2] = 208;
			array4[2] = 250;
			array4[3] = 94;
			array4[3] = 140;
			array4[3] = 97;
			array4[3] = 120;
			array4[3] = 96;
			array4[4] = 74;
			array4[4] = 122;
			array4[4] = 135;
			array4[4] = 119;
			array4[4] = 96;
			array4[4] = 111;
			array4[5] = 140;
			array4[5] = 143;
			array4[5] = 154;
			array4[5] = 100;
			array4[5] = 198;
			array4[6] = 130;
			array4[6] = 137;
			array4[6] = 146;
			array4[6] = 14;
			array4[7] = 161;
			array4[7] = 12;
			array4[7] = 141;
			array4[7] = 60;
			array4[8] = 129;
			array4[8] = 162;
			array4[8] = 101;
			array4[9] = 117;
			array4[9] = 122;
			array4[9] = 128;
			array4[9] = 124;
			array4[9] = 234;
			array4[9] = 27;
			array4[10] = 113;
			array4[10] = 174;
			array4[10] = 116;
			array4[10] = 102;
			array4[11] = 112;
			array4[11] = 152;
			array4[11] = 100;
			array4[11] = 119;
			array4[11] = 161;
			array4[11] = 247;
			array4[12] = 174;
			array4[12] = 68;
			array4[12] = 118;
			array4[12] = 142;
			array4[12] = 156;
			array4[12] = 193;
			array4[13] = 136;
			array4[13] = 109;
			array4[13] = 117;
			array4[13] = 171;
			array4[13] = 148;
			array4[14] = 114;
			array4[14] = 183;
			array4[14] = 167;
			array4[14] = 153;
			array4[15] = 222;
			array4[15] = 156;
			array4[15] = 99;
			array4[15] = 106;
			array4[15] = 78;
			byte[] array5 = array4;
			Array.Reverse(array5);
			byte[] publicKeyToken = Class1.assembly_0.GetName().GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken.Length != 0)
			{
				array5[1] = publicKeyToken[0];
				array5[3] = publicKeyToken[1];
				array5[5] = publicKeyToken[2];
				array5[7] = publicKeyToken[3];
				array5[9] = publicKeyToken[4];
				array5[11] = publicKeyToken[5];
				array5[13] = publicKeyToken[6];
				array5[15] = publicKeyToken[7];
				Array.Clear(publicKeyToken, 0, publicKeyToken.Length);
			}
			for (int i = 0; i < array5.Length; i++)
			{
				array3[i] ^= array5[i];
			}
			byte[] array6 = array;
			int num2 = array6.Length % 4;
			int num3 = array6.Length / 4;
			byte[] array7 = new byte[array6.Length];
			int num4 = array3.Length / 4;
			uint num5 = 0U;
			if (num2 > 0)
			{
				num3++;
			}
			for (int j = 0; j < num3; j++)
			{
				int num6 = j % num4;
				int num7 = j * 4;
				uint num8 = (uint)(num6 * 4);
				uint num9 = (uint)(((int)array3[(int)(num8 + 3U)] << 24) | ((int)array3[(int)(num8 + 2U)] << 16) | ((int)array3[(int)(num8 + 1U)] << 8) | (int)array3[(int)num8]);
				uint num10 = 255U;
				int num11 = 0;
				uint num12;
				if (j == num3 - 1 && num2 > 0)
				{
					num5 += num9;
					num12 = 0U;
					for (int k = 0; k < num2; k++)
					{
						if (k > 0)
						{
							num12 <<= 8;
						}
						num12 |= (uint)array6[array6.Length - (1 + k)];
					}
				}
				else
				{
					num8 = (uint)num7;
					num5 += num9;
					num12 = (uint)(((int)array6[(int)(num8 + 3U)] << 24) | ((int)array6[(int)(num8 + 2U)] << 16) | ((int)array6[(int)(num8 + 1U)] << 8) | (int)array6[(int)num8]);
				}
				num5 = num5;
				uint num13 = num5;
				uint num14 = num5;
				num14 += 744984710U;
				uint num15 = num14 ^ 1209970465U ^ num14;
				uint num16 = num14 + 1351004559U - 694324608U;
				uint num17 = num14 / 1863877164U + 1863877164U;
				uint num18 = (num14 + num14) * num17 + num14;
				num14 ^= num14 >> 15;
				num14 += num15;
				num14 ^= num14 << 5;
				num14 += num16;
				num14 ^= num14 >> 4;
				num14 += num18;
				num14 = (((num15 << 21) - num15) ^ num15) + num14;
				num5 = num13 + (uint)num14;
				if (j == num3 - 1 && num2 > 0)
				{
					uint num19 = num5 ^ num12;
					for (int l = 0; l < num2; l++)
					{
						if (l > 0)
						{
							num10 <<= 8;
							num11 += 8;
						}
						array7[num7 + l] = (byte)((num19 & num10) >> num11);
					}
				}
				else
				{
					uint num20 = num5 ^ num12;
					array7[num7] = (byte)(num20 & 255U);
					array7[num7 + 1] = (byte)((num20 & 65280U) >> 8);
					array7[num7 + 2] = (byte)((num20 & 16711680U) >> 16);
					array7[num7 + 3] = (byte)((num20 & 4278190080U) >> 24);
				}
			}
			byte[] array8 = array7;
			int num21 = array8.Length / 8;
			byte[] array9;
			byte* ptr;
			if ((array9 = array8) != null && array9.Length != 0)
			{
				ptr = &array9[0];
			}
			else
			{
				ptr = null;
			}
			for (int m = 0; m < num21; m++)
			{
				*(long*)(ptr + m * 8) ^= 261477489L;
			}
			array9 = null;
			@class = new Class1.Class4(new MemoryStream(array8));
			@class.method_0().Position = 0L;
			long num22 = Marshal.GetHINSTANCE(Class1.assembly_0.GetModules()[0]).ToInt64();
			int num23 = 0;
			int num24 = 0;
			if (Class1.assembly_0.Location == null || Class1.assembly_0.Location.Length == 0)
			{
				num24 = 7680;
			}
			@class.method_3();
			@class.method_3();
			@class.method_3();
			@class.method_3();
			int num25 = @class.method_3();
			int num26 = @class.method_3();
			if (num26 == 4)
			{
				SymmetricAlgorithm symmetricAlgorithm = Class1.smethod_6();
				symmetricAlgorithm.Mode = CipherMode.CBC;
				ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor(array3, array5);
				Array.Clear(array3, 0, array3.Length);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				array8 = memoryStream.ToArray();
				Array.Clear(array5, 0, array5.Length);
				memoryStream.Close();
				cryptoStream.Close();
				@class.method_4();
				num25 = @class.method_3();
				num26 = @class.method_3();
			}
			if (num26 == 1)
			{
				IntPtr intPtr = IntPtr.Zero;
				intPtr = Class1.smethod_23(56U, 1, (uint)Process.GetCurrentProcess().Id);
				if (IntPtr.Size == 4)
				{
					Class1.int_0 = Marshal.GetHINSTANCE(Class1.assembly_0.GetModules()[0]).ToInt32();
				}
				Class1.long_1 = Marshal.GetHINSTANCE(Class1.assembly_0.GetModules()[0]).ToInt64();
				IntPtr zero = IntPtr.Zero;
				for (int n = 0; n < num25; n++)
				{
					IntPtr intPtr2 = new IntPtr(Class1.long_1 + (long)@class.method_3() - (long)num24);
					if (Class1.smethod_22(intPtr2, 4, 4, ref num23) == 0)
					{
						Class1.smethod_22(intPtr2, 4, 8, ref num23);
					}
					if (IntPtr.Size == 4)
					{
						Class1.smethod_21(intPtr, intPtr2, BitConverter.GetBytes(@class.method_3()), 4U, out zero);
					}
					else
					{
						Class1.smethod_21(intPtr, intPtr2, BitConverter.GetBytes(@class.method_3()), 4U, out zero);
					}
					Class1.smethod_22(intPtr2, 4, num23, ref num23);
				}
				while (@class.method_0().Position < @class.method_0().Length - 1L)
				{
					int num27 = @class.method_3();
					IntPtr intPtr3 = new IntPtr(Class1.long_1 + (long)num27 - (long)num24);
					int num28 = @class.method_3();
					if (Class1.smethod_22(intPtr3, num28 * 4, 4, ref num23) == 0)
					{
						Class1.smethod_22(intPtr3, num28 * 4, 8, ref num23);
					}
					for (int num29 = 0; num29 < num28; num29++)
					{
						Marshal.WriteInt32(new IntPtr(intPtr3.ToInt64() + (long)(num29 * 4)), @class.method_3());
					}
					Class1.smethod_22(intPtr3, num28 * 4, num23, ref num23);
				}
				Class1.smethod_24(intPtr);
				return;
			}
			for (int num30 = 0; num30 < num25; num30++)
			{
				IntPtr intPtr4 = new IntPtr(num22 + (long)@class.method_3() - (long)num24);
				if (Class1.smethod_22(intPtr4, 4, 4, ref num23) == 0)
				{
					Class1.smethod_22(intPtr4, 4, 8, ref num23);
				}
				Marshal.WriteInt32(intPtr4, @class.method_3());
				Class1.smethod_22(intPtr4, 4, num23, ref num23);
			}
			Class1.hashtable_0 = new Hashtable(@class.method_3() + 1);
			Class1.Struct1 @struct = default(Class1.Struct1);
			@struct.byte_0 = new byte[] { 42 };
			@struct.bool_0 = false;
			Class1.hashtable_0.Add(0L, @struct);
			while (@class.method_0().Position < @class.method_0().Length - 1L)
			{
				int num31 = @class.method_3() - num24;
				int num32 = @class.method_3();
				bool flag = false;
				if (num32 >= 1879048192)
				{
					flag = true;
				}
				int num33 = @class.method_3();
				byte[] array10 = @class.method_1(num33);
				Class1.Struct1 struct2 = default(Class1.Struct1);
				struct2.byte_0 = array10;
				struct2.bool_0 = flag;
				Class1.hashtable_0.Add(num22 + (long)num31, struct2);
			}
			Class1.long_0 = Marshal.GetHINSTANCE(typeof(Class1).Assembly.GetModules()[0]).ToInt64();
			if (IntPtr.Size == 4)
			{
				Class1.int_2 = Convert.ToInt32(Class1.long_0);
			}
			byte[] array11 = new byte[]
			{
				109, 115, 99, 111, 114, 106, 105, 116, 46, 100,
				108, 108
			};
			string text = Encoding.UTF8.GetString(array11);
			IntPtr intPtr5 = IntPtr.Zero;
			if (intPtr5 == IntPtr.Zero)
			{
				array11 = new byte[] { 99, 108, 114, 106, 105, 116, 46, 100, 108, 108 };
				text = Encoding.UTF8.GetString(array11);
				intPtr5 = Class1.LoadLibrary(text);
			}
			byte[] array12 = new byte[] { 103, 101, 116, 74, 105, 116 };
			string @string = Encoding.UTF8.GetString(array12);
			IntPtr intPtr6 = ((Class1.Delegate3)Class1.smethod_16(Class1.GetProcAddress(intPtr5, @string), typeof(Class1.Delegate3)))();
			long num34 = 0L;
			if (IntPtr.Size == 4)
			{
				num34 = (long)Marshal.ReadInt32(intPtr6);
			}
			else
			{
				num34 = Marshal.ReadInt64(intPtr6);
			}
			Marshal.ReadIntPtr(intPtr6, 0);
			Class1.delegate2_1 = new Class1.Delegate2(Class1.smethod_13);
			IntPtr intPtr7 = IntPtr.Zero;
			intPtr7 = Marshal.GetFunctionPointerForDelegate(Class1.delegate2_1);
			long num35 = 0L;
			if (IntPtr.Size == 4)
			{
				num35 = (long)Marshal.ReadInt32(new IntPtr(num34));
			}
			else
			{
				num35 = Marshal.ReadInt64(new IntPtr(num34));
			}
			Process currentProcess = Process.GetCurrentProcess();
			try
			{
				foreach (object obj2 in currentProcess.Modules)
				{
					ProcessModule processModule2 = (ProcessModule)obj2;
					if (processModule2.ModuleName == text && (num35 < processModule2.BaseAddress.ToInt64() || num35 > processModule2.BaseAddress.ToInt64() + (long)processModule2.ModuleMemorySize) && typeof(Class1).Assembly.EntryPoint != null)
					{
						return;
					}
				}
			}
			catch
			{
			}
			try
			{
				using (IEnumerator enumerator = currentProcess.Modules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((ProcessModule)enumerator.Current).BaseAddress.ToInt64() == Class1.long_0)
						{
							break;
						}
					}
				}
			}
			catch
			{
			}
			Class1.delegate2_0 = null;
			try
			{
				Class1.delegate2_0 = (Class1.Delegate2)Class1.smethod_16(new IntPtr(num35), typeof(Class1.Delegate2));
			}
			catch
			{
				try
				{
					Delegate @delegate = Class1.smethod_16(new IntPtr(num35), typeof(Class1.Delegate2));
					Class1.delegate2_0 = (Class1.Delegate2)Delegate.CreateDelegate(typeof(Class1.Delegate2), @delegate.Method);
				}
				catch
				{
				}
			}
			int num36 = 0;
			if (typeof(Class1).Assembly.EntryPoint != null && typeof(Class1).Assembly.EntryPoint.GetParameters().Length == 2 && typeof(Class1).Assembly.Location != null && typeof(Class1).Assembly.Location.Length > 0)
			{
				return;
			}
			try
			{
				object value = typeof(Class1).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(typeof(Class1).Assembly.ManifestModule.ModuleHandle);
				if (value is IntPtr)
				{
					Class1.intptr_3 = (IntPtr)value;
				}
				if (value.GetType().ToString() == "System.Reflection.RuntimeModule")
				{
					Class1.intptr_3 = (IntPtr)value.GetType().GetField("m_pData", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(value);
				}
				MemoryStream memoryStream2 = new MemoryStream();
				memoryStream2.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
				if (IntPtr.Size == 4)
				{
					memoryStream2.Write(BitConverter.GetBytes(Class1.intptr_3.ToInt32()), 0, 4);
				}
				else
				{
					memoryStream2.Write(BitConverter.GetBytes(Class1.intptr_3.ToInt64()), 0, 8);
				}
				memoryStream2.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
				memoryStream2.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
				memoryStream2.Position = 0L;
				byte[] array13 = memoryStream2.ToArray();
				memoryStream2.Close();
				uint num37 = 0U;
				try
				{
					byte* ptr2;
					if ((array9 = array13) != null && array9.Length != 0)
					{
						ptr2 = &array9[0];
					}
					else
					{
						ptr2 = null;
					}
					Class1.delegate2_1(new IntPtr((void*)ptr2), new IntPtr((void*)ptr2), new IntPtr((void*)ptr2), 216669565U, new IntPtr((void*)ptr2), ref num37);
				}
				finally
				{
					array9 = null;
				}
			}
			catch
			{
			}
			RuntimeHelpers.PrepareDelegate(Class1.delegate2_0);
			RuntimeHelpers.PrepareMethod(Class1.delegate2_0.Method.MethodHandle);
			RuntimeHelpers.PrepareDelegate(Class1.delegate2_1);
			RuntimeHelpers.PrepareMethod(Class1.delegate2_1.Method.MethodHandle);
			byte[] array14;
			if (IntPtr.Size != 4)
			{
				array14 = new byte[]
				{
					72, 184, 0, 0, 0, 0, 0, 0, 0, 0,
					73, 57, 64, 8, 116, 12, 72, 184, 0, 0,
					0, 0, 0, 0, 0, 0, byte.MaxValue, 224, 72, 184,
					0, 0, 0, 0, 0, 0, 0, 0, byte.MaxValue, 224
				};
			}
			else
			{
				array14 = new byte[]
				{
					85, 139, 236, 139, 69, 16, 129, 120, 4, 125,
					29, 234, 12, 116, 7, 184, 182, 177, 74, 6,
					235, 5, 184, 182, 146, 64, 12, 93, byte.MaxValue, 224
				};
			}
			IntPtr intPtr8 = Class1.smethod_20(IntPtr.Zero, (uint)array14.Length, 4096U, 64U);
			byte[] array15 = array14;
			byte[] array16;
			byte[] array17;
			byte[] array18;
			if (IntPtr.Size == 4)
			{
				array16 = BitConverter.GetBytes(Class1.intptr_3.ToInt32());
				array17 = BitConverter.GetBytes(intPtr7.ToInt32());
				array18 = BitConverter.GetBytes(Convert.ToInt32(num35));
			}
			else
			{
				array16 = BitConverter.GetBytes(Class1.intptr_3.ToInt64());
				array17 = BitConverter.GetBytes(intPtr7.ToInt64());
				array18 = BitConverter.GetBytes(num35);
			}
			if (IntPtr.Size == 4)
			{
				array15[9] = array16[0];
				array15[10] = array16[1];
				array15[11] = array16[2];
				array15[12] = array16[3];
				array15[16] = array18[0];
				array15[17] = array18[1];
				array15[18] = array18[2];
				array15[19] = array18[3];
				array15[23] = array17[0];
				array15[24] = array17[1];
				array15[25] = array17[2];
				array15[26] = array17[3];
			}
			else
			{
				array15[2] = array16[0];
				array15[3] = array16[1];
				array15[4] = array16[2];
				array15[5] = array16[3];
				array15[6] = array16[4];
				array15[7] = array16[5];
				array15[8] = array16[6];
				array15[9] = array16[7];
				array15[18] = array18[0];
				array15[19] = array18[1];
				array15[20] = array18[2];
				array15[21] = array18[3];
				array15[22] = array18[4];
				array15[23] = array18[5];
				array15[24] = array18[6];
				array15[25] = array18[7];
				array15[30] = array17[0];
				array15[31] = array17[1];
				array15[32] = array17[2];
				array15[33] = array17[3];
				array15[34] = array17[4];
				array15[35] = array17[5];
				array15[36] = array17[6];
				array15[37] = array17[7];
			}
			Marshal.Copy(array15, 0, intPtr8, array15.Length);
			Class1.bool_2 = false;
			Class1.smethod_22(new IntPtr(num34), IntPtr.Size, 64, ref num36);
			Marshal.WriteIntPtr(new IntPtr(num34), intPtr8);
			Class1.smethod_22(new IntPtr(num34), IntPtr.Size, num36, ref num36);
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00033E48 File Offset: 0x00032048
	internal static object smethod_18(Assembly assembly_1)
	{
		try
		{
			if (File.Exists(((Assembly)assembly_1).Location))
			{
				return ((Assembly)assembly_1).Location;
			}
		}
		catch
		{
		}
		try
		{
			if (File.Exists(((Assembly)assembly_1).GetName().CodeBase.ToString().Replace("file:///", "")))
			{
				return ((Assembly)assembly_1).GetName().CodeBase.ToString().Replace("file:///", "");
			}
		}
		catch
		{
		}
		try
		{
			if (File.Exists(assembly_1.GetType().GetProperty("Location").GetValue(assembly_1, new object[0])
				.ToString()))
			{
				return assembly_1.GetType().GetProperty("Location").GetValue(assembly_1, new object[0])
					.ToString();
			}
		}
		catch
		{
		}
		return "";
	}

	// Token: 0x060006DE RID: 1758
	[DllImport("kernel32")]
	public static extern IntPtr LoadLibrary(string string_1);

	// Token: 0x060006DF RID: 1759
	[DllImport("kernel32", CharSet = CharSet.Ansi)]
	public static extern IntPtr GetProcAddress(IntPtr intptr_4, string string_1);

	// Token: 0x060006E0 RID: 1760 RVA: 0x00033F58 File Offset: 0x00032158
	private static IntPtr smethod_19(IntPtr intptr_4, string string_1, uint uint_1)
	{
		if (Class1.delegate4_0 == null)
		{
			Class1.delegate4_0 = (Class1.Delegate4)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Find ".Trim() + "ResourceA"), typeof(Class1.Delegate4));
		}
		return Class1.delegate4_0(intptr_4, string_1, uint_1);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00033FB0 File Offset: 0x000321B0
	private static IntPtr smethod_20(IntPtr intptr_4, uint uint_1, uint uint_2, uint uint_3)
	{
		if (Class1.delegate5_0 == null)
		{
			Class1.delegate5_0 = (Class1.Delegate5)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Virtual ".Trim() + "Alloc"), typeof(Class1.Delegate5));
		}
		return Class1.delegate5_0(intptr_4, uint_1, uint_2, uint_3);
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0003400C File Offset: 0x0003220C
	private static int smethod_21(IntPtr intptr_4, IntPtr intptr_5, [In] [Out] byte[] byte_2, uint uint_1, out IntPtr intptr_6)
	{
		if (Class1.delegate6_0 == null)
		{
			Class1.delegate6_0 = (Class1.Delegate6)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Write ".Trim() + "Process ".Trim() + "Memory"), typeof(Class1.Delegate6));
		}
		return Class1.delegate6_0(intptr_4, intptr_5, byte_2, uint_1, out intptr_6);
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00034074 File Offset: 0x00032274
	private static int smethod_22(IntPtr intptr_4, int int_6, int int_7, ref int int_8)
	{
		if (Class1.delegate7_0 == null)
		{
			Class1.delegate7_0 = (Class1.Delegate7)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Virtual ".Trim() + "Protect"), typeof(Class1.Delegate7));
		}
		return Class1.delegate7_0(intptr_4, int_6, int_7, ref int_8);
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x000340D0 File Offset: 0x000322D0
	private static IntPtr smethod_23(uint uint_1, int int_6, uint uint_2)
	{
		if (Class1.delegate8_0 == null)
		{
			Class1.delegate8_0 = (Class1.Delegate8)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Open ".Trim() + "Process"), typeof(Class1.Delegate8));
		}
		return Class1.delegate8_0(uint_1, int_6, uint_2);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00034128 File Offset: 0x00032328
	private static int smethod_24(IntPtr intptr_4)
	{
		if (Class1.delegate9_0 == null)
		{
			Class1.delegate9_0 = (Class1.Delegate9)Marshal.GetDelegateForFunctionPointer(Class1.GetProcAddress(Class1.smethod_25(), "Close ".Trim() + "Handle"), typeof(Class1.Delegate9));
		}
		return Class1.delegate9_0(intptr_4);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00004355 File Offset: 0x00002555
	private static IntPtr smethod_25()
	{
		if (Class1.intptr_1 == IntPtr.Zero)
		{
			Class1.intptr_1 = Class1.LoadLibrary("kernel ".Trim() + "32.dll");
		}
		return Class1.intptr_1;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00034180 File Offset: 0x00032380
	private static byte[] smethod_26(string string_1)
	{
		byte[] array;
		using (FileStream fileStream = new FileStream(string_1, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			int num = 0;
			int i = (int)fileStream.Length;
			array = new byte[i];
			while (i > 0)
			{
				int num2 = fileStream.Read(array, num, i);
				num += num2;
				i -= num2;
			}
		}
		return array;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0000438B File Offset: 0x0000258B
	internal static byte[] smethod_27(MemoryStream memoryStream_0)
	{
		return ((MemoryStream)memoryStream_0).ToArray();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000341E0 File Offset: 0x000323E0
	private static byte[] smethod_28(byte[] byte_2)
	{
		Stream stream = new MemoryStream();
		SymmetricAlgorithm symmetricAlgorithm = Class1.smethod_6();
		symmetricAlgorithm.Key = new byte[]
		{
			139, 228, 93, 4, 206, 236, 211, 161, 44, 165,
			248, 205, 200, 244, 28, 219, 6, 19, 79, 214,
			40, 128, 203, 31, 73, 44, 206, 88, 208, 133,
			248, 93
		};
		symmetricAlgorithm.IV = new byte[]
		{
			100, 120, 43, 160, 65, 195, 205, 97, 69, 239,
			218, 157, 219, 83, 203, 156
		};
		CryptoStream cryptoStream = new CryptoStream(stream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(byte_2, 0, byte_2.Length);
		cryptoStream.Close();
		return Class1.smethod_27(stream);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0003424C File Offset: 0x0003244C
	private byte[] method_2()
	{
		return null;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0003424C File Offset: 0x0003244C
	private byte[] method_3()
	{
		return null;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00004398 File Offset: 0x00002598
	private byte[] method_4()
	{
		int length = "{11111-22222-20001-00001}".Length;
		return new byte[] { 1, 2 };
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000043B3 File Offset: 0x000025B3
	private byte[] method_5()
	{
		int length = "{11111-22222-20001-00002}".Length;
		return new byte[] { 1, 2 };
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0003425C File Offset: 0x0003245C
	private byte[] method_6()
	{
		return null;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0003425C File Offset: 0x0003245C
	private byte[] method_7()
	{
		return null;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x000043CE File Offset: 0x000025CE
	internal byte[] method_8()
	{
		int length = "{11111-22222-40001-00001}".Length;
		return new byte[] { 1, 2 };
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x000043E9 File Offset: 0x000025E9
	internal byte[] method_9()
	{
		int length = "{11111-22222-40001-00002}".Length;
		return new byte[] { 1, 2 };
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0003425C File Offset: 0x0003245C
	internal byte[] method_10()
	{
		return null;
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0003425C File Offset: 0x0003245C
	internal byte[] method_11()
	{
		return null;
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00004404 File Offset: 0x00002604
	internal static object smethod_29(Class1.Class4 class4_0)
	{
		return class4_0.method_0();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0000440F File Offset: 0x0000260F
	internal static void smethod_30(Stream stream_0, long long_2)
	{
		stream_0.Position = long_2;
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0000441E File Offset: 0x0000261E
	internal static long smethod_31(Stream stream_0)
	{
		return stream_0.Length;
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00004429 File Offset: 0x00002629
	internal static object smethod_32(Class1.Class4 class4_0, int int_6)
	{
		return class4_0.method_1(int_6);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00004438 File Offset: 0x00002638
	internal static void smethod_33(Class1.Class4 class4_0)
	{
		class4_0.method_4();
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00004443 File Offset: 0x00002643
	internal static void smethod_34(Array array_0)
	{
		Array.Reverse(array_0);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0000444E File Offset: 0x0000264E
	internal static object smethod_35(Assembly assembly_1)
	{
		return assembly_1.GetName();
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00004459 File Offset: 0x00002659
	internal static object smethod_36(AssemblyName assemblyName_0)
	{
		return assemblyName_0.GetPublicKeyToken();
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00004464 File Offset: 0x00002664
	internal static object smethod_37()
	{
		return Class1.smethod_6();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0000446B File Offset: 0x0000266B
	internal static void smethod_38(SymmetricAlgorithm symmetricAlgorithm_0, CipherMode cipherMode_0)
	{
		symmetricAlgorithm_0.Mode = cipherMode_0;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0000447A File Offset: 0x0000267A
	internal static object smethod_39(SymmetricAlgorithm symmetricAlgorithm_0, byte[] byte_2, byte[] byte_3)
	{
		return symmetricAlgorithm_0.CreateDecryptor(byte_2, byte_3);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0000448D File Offset: 0x0000268D
	internal static object smethod_40()
	{
		return new MemoryStream();
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00004494 File Offset: 0x00002694
	internal static void smethod_41(Stream stream_0, byte[] byte_2, int int_6, int int_7)
	{
		stream_0.Write(byte_2, int_6, int_7);
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x000044AB File Offset: 0x000026AB
	internal static void smethod_42(CryptoStream cryptoStream_0)
	{
		cryptoStream_0.FlushFinalBlock();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000044B6 File Offset: 0x000026B6
	internal static object smethod_43(MemoryStream memoryStream_0)
	{
		return Class1.smethod_27(memoryStream_0);
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x000044C1 File Offset: 0x000026C1
	internal static void smethod_44(Stream stream_0)
	{
		stream_0.Close();
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x000044CC File Offset: 0x000026CC
	internal static object smethod_45(Assembly assembly_1)
	{
		return assembly_1.EntryPoint;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x000044D7 File Offset: 0x000026D7
	internal static bool smethod_46(MethodInfo methodInfo_0, MethodInfo methodInfo_1)
	{
		return methodInfo_0 == methodInfo_1;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x000044E6 File Offset: 0x000026E6
	internal static bool smethod_47()
	{
		return null == null;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00003B50 File Offset: 0x00001D50
	internal static object smethod_48()
	{
		return null;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00003B4D File Offset: 0x00001D4D
	static int smethod_49()
	{
		return 1;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x000044EC File Offset: 0x000026EC
	internal static IntPtr smethod_50(IntPtr intptr_4, int int_6)
	{
		return Marshal.ReadIntPtr(intptr_4, int_6);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x000044FB File Offset: 0x000026FB
	internal static int smethod_51(IntPtr intptr_4, int int_6)
	{
		return Marshal.ReadInt32(intptr_4, int_6);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0000450A File Offset: 0x0000270A
	internal static long smethod_52(IntPtr intptr_4, int int_6)
	{
		return Marshal.ReadInt64(intptr_4, int_6);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00004519 File Offset: 0x00002719
	internal static void smethod_53(IntPtr intptr_4, int int_6, IntPtr intptr_5)
	{
		Marshal.WriteIntPtr(intptr_4, int_6, intptr_5);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0000452C File Offset: 0x0000272C
	internal static void smethod_54(IntPtr intptr_4, int int_6, int int_7)
	{
		Marshal.WriteInt32(intptr_4, int_6, int_7);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0000453F File Offset: 0x0000273F
	internal static void smethod_55(IntPtr intptr_4, int int_6, long long_2)
	{
		Marshal.WriteInt64(intptr_4, int_6, long_2);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00004552 File Offset: 0x00002752
	internal static IntPtr smethod_56(int int_6)
	{
		return Marshal.AllocCoTaskMem(int_6);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0000455D File Offset: 0x0000275D
	internal static void smethod_57(byte[] byte_2, int int_6, IntPtr intptr_4, int int_7)
	{
		Marshal.Copy(byte_2, int_6, intptr_4, int_7);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00004574 File Offset: 0x00002774
	internal static void smethod_58()
	{
		Class1.smethod_15();
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0000457B File Offset: 0x0000277B
	internal static object smethod_59()
	{
		return Process.GetCurrentProcess();
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00004582 File Offset: 0x00002782
	internal static object smethod_60(Process process_0)
	{
		return process_0.MainModule;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0000458D File Offset: 0x0000278D
	internal static IntPtr smethod_61(ProcessModule processModule_0)
	{
		return processModule_0.BaseAddress;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x00004598 File Offset: 0x00002798
	internal static IntPtr smethod_62(IntPtr intptr_4, string string_1, uint uint_1)
	{
		return Class1.smethod_19(intptr_4, string_1, uint_1);
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x000045AB File Offset: 0x000027AB
	internal static bool smethod_63(IntPtr intptr_4, IntPtr intptr_5)
	{
		return intptr_4 != intptr_5;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x000045BA File Offset: 0x000027BA
	internal static int smethod_65()
	{
		return IntPtr.Size;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x000045C1 File Offset: 0x000027C1
	internal static Type smethod_66(string string_1, bool bool_5)
	{
		return Type.GetType(string_1, bool_5);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000045D0 File Offset: 0x000027D0
	internal static bool smethod_67(Type type_0, Type type_1)
	{
		return type_0 != type_1;
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000045DF File Offset: 0x000027DF
	internal static object smethod_68(Process process_0)
	{
		return process_0.Modules;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x000045EA File Offset: 0x000027EA
	internal static object smethod_69(ReadOnlyCollectionBase readOnlyCollectionBase_0)
	{
		return readOnlyCollectionBase_0.GetEnumerator();
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x000045F5 File Offset: 0x000027F5
	internal static object smethod_70(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00004600 File Offset: 0x00002800
	internal static object smethod_71(ProcessModule processModule_0)
	{
		return processModule_0.ModuleName;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0000460B File Offset: 0x0000280B
	internal static object smethod_72(string string_1)
	{
		return string_1.ToLower();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00004616 File Offset: 0x00002816
	internal static bool smethod_73(string string_1, string string_2)
	{
		return string_1 == string_2;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00004625 File Offset: 0x00002825
	internal static object smethod_74(ProcessModule processModule_0)
	{
		return processModule_0.FileVersionInfo;
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00004630 File Offset: 0x00002830
	internal static int smethod_75(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.ProductMajorPart;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0000463B File Offset: 0x0000283B
	internal static int smethod_76(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.ProductMinorPart;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00004646 File Offset: 0x00002846
	internal static int smethod_77(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.ProductBuildPart;
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00004651 File Offset: 0x00002851
	internal static int smethod_78(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.ProductPrivatePart;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0000465C File Offset: 0x0000285C
	internal static bool smethod_79(Version version_0, Version version_1)
	{
		return version_0 >= version_1;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0000466B File Offset: 0x0000286B
	internal static bool smethod_80(Version version_0, Version version_1)
	{
		return version_0 < version_1;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0000467A File Offset: 0x0000287A
	internal static bool smethod_81(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00004685 File Offset: 0x00002885
	internal static void smethod_82(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00004690 File Offset: 0x00002890
	internal static object smethod_83(Assembly assembly_1, string string_1)
	{
		return assembly_1.GetManifestResourceStream(string_1);
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00004404 File Offset: 0x00002604
	internal static object smethod_84(Class1.Class4 class4_0)
	{
		return class4_0.method_0();
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0000440F File Offset: 0x0000260F
	internal static void smethod_85(Stream stream_0, long long_2)
	{
		stream_0.Position = long_2;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0000441E File Offset: 0x0000261E
	internal static long smethod_86(Stream stream_0)
	{
		return stream_0.Length;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00004429 File Offset: 0x00002629
	internal static object smethod_87(Class1.Class4 class4_0, int int_6)
	{
		return class4_0.method_1(int_6);
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00004443 File Offset: 0x00002643
	internal static void smethod_88(Array array_0)
	{
		Array.Reverse(array_0);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0000444E File Offset: 0x0000264E
	internal static object smethod_89(Assembly assembly_1)
	{
		return assembly_1.GetName();
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00004459 File Offset: 0x00002659
	internal static object smethod_90(AssemblyName assemblyName_0)
	{
		return assemblyName_0.GetPublicKeyToken();
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0000469F File Offset: 0x0000289F
	internal static void smethod_91(Array array_0, int int_6, int int_7)
	{
		Array.Clear(array_0, int_6, int_7);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x000046B2 File Offset: 0x000028B2
	internal static object smethod_92(Assembly assembly_1)
	{
		return assembly_1.GetModules();
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000046BD File Offset: 0x000028BD
	internal static IntPtr smethod_93(Module module_0)
	{
		return Marshal.GetHINSTANCE(module_0);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x000046C8 File Offset: 0x000028C8
	internal static object smethod_94(Assembly assembly_1)
	{
		return assembly_1.Location;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x000046D3 File Offset: 0x000028D3
	internal static int smethod_95(string string_1)
	{
		return string_1.Length;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000046DE File Offset: 0x000028DE
	internal static int smethod_96(Class1.Class4 class4_0)
	{
		return class4_0.method_3();
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00004464 File Offset: 0x00002664
	internal static object smethod_97()
	{
		return Class1.smethod_6();
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0000446B File Offset: 0x0000266B
	internal static void smethod_98(SymmetricAlgorithm symmetricAlgorithm_0, CipherMode cipherMode_0)
	{
		symmetricAlgorithm_0.Mode = cipherMode_0;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0000447A File Offset: 0x0000267A
	internal static object smethod_99(SymmetricAlgorithm symmetricAlgorithm_0, byte[] byte_2, byte[] byte_3)
	{
		return symmetricAlgorithm_0.CreateDecryptor(byte_2, byte_3);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00004494 File Offset: 0x00002694
	internal static void smethod_100(Stream stream_0, byte[] byte_2, int int_6, int int_7)
	{
		stream_0.Write(byte_2, int_6, int_7);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000044AB File Offset: 0x000026AB
	internal static void smethod_101(CryptoStream cryptoStream_0)
	{
		cryptoStream_0.FlushFinalBlock();
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x000046E9 File Offset: 0x000028E9
	internal static object smethod_102(MemoryStream memoryStream_0)
	{
		return memoryStream_0.ToArray();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x000044C1 File Offset: 0x000026C1
	internal static void smethod_103(Stream stream_0)
	{
		stream_0.Close();
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00004438 File Offset: 0x00002638
	internal static void smethod_104(Class1.Class4 class4_0)
	{
		class4_0.method_4();
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x000046F4 File Offset: 0x000028F4
	internal static int smethod_105(Process process_0)
	{
		return process_0.Id;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x000046FF File Offset: 0x000028FF
	internal static IntPtr smethod_106(uint uint_1, int int_6, uint uint_2)
	{
		return Class1.smethod_23(uint_1, int_6, uint_2);
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00004712 File Offset: 0x00002912
	internal static object smethod_107(int int_6)
	{
		return BitConverter.GetBytes(int_6);
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0000471D File Offset: 0x0000291D
	internal static long smethod_108(Stream stream_0)
	{
		return stream_0.Position;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00004728 File Offset: 0x00002928
	internal static void smethod_109(IntPtr intptr_4, int int_6)
	{
		Marshal.WriteInt32(intptr_4, int_6);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00004737 File Offset: 0x00002937
	internal static int smethod_110(IntPtr intptr_4)
	{
		return Class1.smethod_24(intptr_4);
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00004742 File Offset: 0x00002942
	internal static void smethod_111(Hashtable hashtable_1, object object_3, object object_4)
	{
		hashtable_1.Add(object_3, object_4);
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00004755 File Offset: 0x00002955
	internal static Type smethod_112(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00004760 File Offset: 0x00002960
	internal static int smethod_113(long long_2)
	{
		return Convert.ToInt32(long_2);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0000476B File Offset: 0x0000296B
	internal static object smethod_114()
	{
		return Encoding.UTF8;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00004772 File Offset: 0x00002972
	internal static object smethod_115(Encoding encoding_0, byte[] byte_2)
	{
		return encoding_0.GetString(byte_2);
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00004781 File Offset: 0x00002981
	internal static bool smethod_116(IntPtr intptr_4, IntPtr intptr_5)
	{
		return intptr_4 == intptr_5;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00004790 File Offset: 0x00002990
	internal static object smethod_117(IntPtr intptr_4, Type type_0)
	{
		return Class1.smethod_16(intptr_4, type_0);
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0000479F File Offset: 0x0000299F
	internal static IntPtr smethod_118(Class1.Delegate3 delegate3_0)
	{
		return delegate3_0();
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000047AA File Offset: 0x000029AA
	internal static int smethod_119(IntPtr intptr_4)
	{
		return Marshal.ReadInt32(intptr_4);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000047B5 File Offset: 0x000029B5
	internal static long smethod_120(IntPtr intptr_4)
	{
		return Marshal.ReadInt64(intptr_4);
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x000047C0 File Offset: 0x000029C0
	internal static IntPtr smethod_121(Delegate delegate_0)
	{
		return Marshal.GetFunctionPointerForDelegate(delegate_0);
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x000047CB File Offset: 0x000029CB
	internal static int smethod_122(ProcessModule processModule_0)
	{
		return processModule_0.ModuleMemorySize;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000044CC File Offset: 0x000026CC
	internal static object smethod_123(Assembly assembly_1)
	{
		return assembly_1.EntryPoint;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x000047D6 File Offset: 0x000029D6
	internal static bool smethod_124(MethodInfo methodInfo_0, MethodInfo methodInfo_1)
	{
		return methodInfo_0 != methodInfo_1;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x000047E5 File Offset: 0x000029E5
	internal static object smethod_125(Delegate delegate_0)
	{
		return delegate_0.Method;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000047F0 File Offset: 0x000029F0
	internal static object smethod_126(Type type_0, MethodInfo methodInfo_0)
	{
		return Delegate.CreateDelegate(type_0, methodInfo_0);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x000047FF File Offset: 0x000029FF
	internal static object smethod_127(MethodBase methodBase_0)
	{
		return methodBase_0.GetParameters();
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0000480A File Offset: 0x00002A0A
	internal static object smethod_128(Assembly assembly_1)
	{
		return assembly_1.ManifestModule;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00004815 File Offset: 0x00002A15
	internal static ModuleHandle smethod_129(Module module_0)
	{
		return module_0.ModuleHandle;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00004820 File Offset: 0x00002A20
	internal static Type smethod_130(object object_3)
	{
		return object_3.GetType();
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0000482B File Offset: 0x00002A2B
	internal static object smethod_131(FieldInfo fieldInfo_0, object object_3)
	{
		return fieldInfo_0.GetValue(object_3);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0000483A File Offset: 0x00002A3A
	internal static object smethod_132(long long_2)
	{
		return BitConverter.GetBytes(long_2);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00004845 File Offset: 0x00002A45
	internal static void smethod_133(Delegate delegate_0)
	{
		RuntimeHelpers.PrepareDelegate(delegate_0);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00004850 File Offset: 0x00002A50
	internal static RuntimeMethodHandle smethod_134(MethodBase methodBase_0)
	{
		return methodBase_0.MethodHandle;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0000485B File Offset: 0x00002A5B
	internal static void smethod_135(RuntimeMethodHandle runtimeMethodHandle_0)
	{
		RuntimeHelpers.PrepareMethod(runtimeMethodHandle_0);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00004866 File Offset: 0x00002A66
	internal static void smethod_136(Array array_0, RuntimeFieldHandle runtimeFieldHandle_0)
	{
		RuntimeHelpers.InitializeArray(array_0, runtimeFieldHandle_0);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00004875 File Offset: 0x00002A75
	internal static IntPtr smethod_137(IntPtr intptr_4, uint uint_1, uint uint_2, uint uint_3)
	{
		return Class1.smethod_20(intptr_4, uint_1, uint_2, uint_3);
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0000488C File Offset: 0x00002A8C
	internal static void smethod_138(IntPtr intptr_4, IntPtr intptr_5)
	{
		Marshal.WriteIntPtr(intptr_4, intptr_5);
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000044E6 File Offset: 0x000026E6
	internal static bool smethod_139()
	{
		return null == null;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00003B50 File Offset: 0x00001D50
	internal static object smethod_140()
	{
		return null;
	}

	// Token: 0x0400064D RID: 1613
	private static uint[] uint_0 = new uint[]
	{
		3614090360U, 3905402710U, 606105819U, 3250441966U, 4118548399U, 1200080426U, 2821735955U, 4249261313U, 1770035416U, 2336552879U,
		4294925233U, 2304563134U, 1804603682U, 4254626195U, 2792965006U, 1236535329U, 4129170786U, 3225465664U, 643717713U, 3921069994U,
		3593408605U, 38016083U, 3634488961U, 3889429448U, 568446438U, 3275163606U, 4107603335U, 1163531501U, 2850285829U, 4243563512U,
		1735328473U, 2368359562U, 4294588738U, 2272392833U, 1839030562U, 4259657740U, 2763975236U, 1272893353U, 4139469664U, 3200236656U,
		681279174U, 3936430074U, 3572445317U, 76029189U, 3654602809U, 3873151461U, 530742520U, 3299628645U, 4096336452U, 1126891415U,
		2878612391U, 4237533241U, 1700485571U, 2399980690U, 4293915773U, 2240044497U, 1873313359U, 4264355552U, 2734768916U, 1309151649U,
		4149444226U, 3174756917U, 718787259U, 3951481745U
	};

	// Token: 0x0400064E RID: 1614
	private static List<string> list_0 = null;

	// Token: 0x0400064F RID: 1615
	private static int int_0 = 0;

	// Token: 0x04000650 RID: 1616
	internal static Class1.Delegate2 delegate2_0 = null;

	// Token: 0x04000651 RID: 1617
	internal static Class1.Delegate2 delegate2_1 = null;

	// Token: 0x04000652 RID: 1618
	[Class1.Attribute0(typeof(Class1.Attribute0.Class2<object>[]))]
	private static bool firstrundone = false;

	// Token: 0x04000653 RID: 1619
	private static Class1.Delegate8 delegate8_0 = null;

	// Token: 0x04000654 RID: 1620
	private static string[] string_0 = new string[0];

	// Token: 0x04000655 RID: 1621
	private static SortedList sortedList_0 = new SortedList();

	// Token: 0x04000656 RID: 1622
	private static bool usfLnHavse = false;

	// Token: 0x04000657 RID: 1623
	private static bool bool_0 = false;

	// Token: 0x04000658 RID: 1624
	private static bool bool_1 = false;

	// Token: 0x04000659 RID: 1625
	private static int int_1 = 0;

	// Token: 0x0400065A RID: 1626
	private static object object_0 = new object();

	// Token: 0x0400065B RID: 1627
	private static List<int> list_1 = null;

	// Token: 0x0400065C RID: 1628
	private static bool bool_2 = false;

	// Token: 0x0400065D RID: 1629
	private static long long_0 = 0L;

	// Token: 0x0400065E RID: 1630
	private static Class1.Delegate6 delegate6_0 = null;

	// Token: 0x0400065F RID: 1631
	internal static Hashtable hashtable_0 = new Hashtable();

	// Token: 0x04000660 RID: 1632
	private static bool bool_3 = false;

	// Token: 0x04000661 RID: 1633
	private static int int_2 = 0;

	// Token: 0x04000662 RID: 1634
	private static object object_1 = new object();

	// Token: 0x04000663 RID: 1635
	private static byte[] byte_0 = new byte[0];

	// Token: 0x04000664 RID: 1636
	private static Dictionary<int, int> dictionary_0 = null;

	// Token: 0x04000665 RID: 1637
	private static int int_3 = 0;

	// Token: 0x04000666 RID: 1638
	private static bool bool_4 = false;

	// Token: 0x04000667 RID: 1639
	private static Class1.Delegate4 delegate4_0 = null;

	// Token: 0x04000668 RID: 1640
	private static Class1.Delegate9 delegate9_0 = null;

	// Token: 0x04000669 RID: 1641
	private static byte[] byte_1 = new byte[0];

	// Token: 0x0400066A RID: 1642
	internal static Assembly assembly_0 = typeof(Class1).Assembly;

	// Token: 0x0400066B RID: 1643
	private static int[] int_4 = new int[0];

	// Token: 0x0400066C RID: 1644
	internal static object object_2 = null;

	// Token: 0x0400066D RID: 1645
	private static long long_1 = 0L;

	// Token: 0x0400066E RID: 1646
	private static IntPtr intptr_0 = IntPtr.Zero;

	// Token: 0x0400066F RID: 1647
	private static Class1.Delegate7 delegate7_0 = null;

	// Token: 0x04000670 RID: 1648
	private static int int_5 = 1;

	// Token: 0x04000671 RID: 1649
	private static IntPtr intptr_1 = IntPtr.Zero;

	// Token: 0x04000672 RID: 1650
	private static Class1.Delegate5 delegate5_0 = null;

	// Token: 0x04000673 RID: 1651
	private static IntPtr intptr_2 = IntPtr.Zero;

	// Token: 0x04000674 RID: 1652
	private static IntPtr intptr_3 = IntPtr.Zero;

	// Token: 0x0200014D RID: 333
	// (Invoke) Token: 0x06000764 RID: 1892
	private delegate void Delegate1(object o);

	// Token: 0x0200014E RID: 334
	internal class Attribute0 : Attribute
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0000489B File Offset: 0x00002A9B
		public Attribute0(object object_0)
		{
		}

		// Token: 0x0200014F RID: 335
		internal class Class2<T>
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x000048A3 File Offset: 0x00002AA3
			internal static bool smethod_0()
			{
				return Class1.Attribute0.Class2<T>.object_0 == null;
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x000048AD File Offset: 0x00002AAD
			internal static object smethod_1()
			{
				return Class1.Attribute0.Class2<T>.object_0;
			}

			// Token: 0x04000675 RID: 1653
			internal static object object_0;
		}
	}

	// Token: 0x02000150 RID: 336
	internal class Class3
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0003426C File Offset: 0x0003246C
		internal static string smethod_0(string string_0, string string_1)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(string_0);
			byte[] array = new byte[]
			{
				82, 102, 104, 110, 32, 77, 24, 34, 118, 181,
				51, 17, 18, 51, 12, 109, 10, 32, 77, 24,
				34, 158, 161, 41, 97, 28, 118, 181, 5, 25,
				1, 88
			};
			byte[] array2 = Class1.smethod_8(Encoding.Unicode.GetBytes(string_1));
			MemoryStream memoryStream = new MemoryStream();
			SymmetricAlgorithm symmetricAlgorithm = Class1.smethod_6();
			symmetricAlgorithm.Key = array;
			symmetricAlgorithm.IV = array2;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.Close();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}

	// Token: 0x02000151 RID: 337
	// (Invoke) Token: 0x0600076E RID: 1902
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate uint Delegate2(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

	// Token: 0x02000152 RID: 338
	// (Invoke) Token: 0x06000772 RID: 1906
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate IntPtr Delegate3();

	// Token: 0x02000153 RID: 339
	internal struct Struct1
	{
		// Token: 0x04000676 RID: 1654
		internal bool bool_0;

		// Token: 0x04000677 RID: 1655
		internal byte[] byte_0;
	}

	// Token: 0x02000154 RID: 340
	internal class Class4
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x000048B4 File Offset: 0x00002AB4
		public Class4(Stream stream_0)
		{
			this.binaryReader_0 = new BinaryReader(stream_0);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal Stream method_0()
		{
			return this.binaryReader_0.BaseStream;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000048D5 File Offset: 0x00002AD5
		internal byte[] method_1(int int_0)
		{
			return this.binaryReader_0.ReadBytes(int_0);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000048E3 File Offset: 0x00002AE3
		internal int method_2(byte[] byte_0, int int_0, int int_1)
		{
			return this.binaryReader_0.Read(byte_0, int_0, int_1);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000048F3 File Offset: 0x00002AF3
		internal int method_3()
		{
			return this.binaryReader_0.ReadInt32();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00004900 File Offset: 0x00002B00
		internal void method_4()
		{
			this.binaryReader_0.Close();
		}

		// Token: 0x04000678 RID: 1656
		private BinaryReader binaryReader_0;
	}

	// Token: 0x02000155 RID: 341
	// (Invoke) Token: 0x0600077C RID: 1916
	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private delegate IntPtr Delegate4(IntPtr hModule, string lpName, uint lpType);

	// Token: 0x02000156 RID: 342
	// (Invoke) Token: 0x06000780 RID: 1920
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate IntPtr Delegate5(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

	// Token: 0x02000157 RID: 343
	// (Invoke) Token: 0x06000784 RID: 1924
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate int Delegate6(IntPtr hProcess, IntPtr lpBaseAddress, [In] [Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);

	// Token: 0x02000158 RID: 344
	// (Invoke) Token: 0x06000788 RID: 1928
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate int Delegate7(IntPtr lpAddress, int dwSize, int flNewProtect, ref int lpflOldProtect);

	// Token: 0x02000159 RID: 345
	// (Invoke) Token: 0x0600078C RID: 1932
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate IntPtr Delegate8(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

	// Token: 0x0200015A RID: 346
	// (Invoke) Token: 0x06000790 RID: 1936
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate int Delegate9(IntPtr ptr);

	// Token: 0x0200015B RID: 347
	[Flags]
	private enum Enum0
	{

	}
}
