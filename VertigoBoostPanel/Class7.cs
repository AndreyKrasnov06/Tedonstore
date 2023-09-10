using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

// Token: 0x0200015E RID: 350
internal class Class7
{
	// Token: 0x06000795 RID: 1941 RVA: 0x0000490D File Offset: 0x00002B0D
	internal static object[] smethod_0()
	{
		return new object[1];
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0003431C File Offset: 0x0003251C
	internal static object[] smethod_1<T>(int int_1, object object_1, object object_2, ref T gparam_0)
	{
		Class7.Class13 @class = null;
		object obj = Class7.object_0;
		lock (obj)
		{
			if (!Class7.bool_0)
			{
				Class7.bool_0 = true;
				Class7.smethod_4();
			}
			if (Class7.class13_0[int_1] != null)
			{
				@class = Class7.class13_0[int_1];
			}
			else
			{
				Class7.binaryReader_0.BaseStream.Position = (long)Class7.int_0[int_1];
				@class = new Class7.Class13();
				Module module = typeof(Class7).Module;
				int num = Class7.smethod_6(Class7.binaryReader_0);
				int num2 = Class7.smethod_6(Class7.binaryReader_0);
				int num3 = Class7.smethod_6(Class7.binaryReader_0);
				int num4 = Class7.smethod_6(Class7.binaryReader_0);
				@class.object_0 = module.ResolveMethod(num);
				ParameterInfo[] parameters = @class.object_0.GetParameters();
				@class.class9_0 = new Class7.Class9[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					Type type = parameters[i].ParameterType;
					Class7.Class9 class2 = new Class7.Class9();
					class2.UhNjdqxgic = type.IsByRef;
					class2.int_0 = i;
					@class.class9_0[i] = class2;
					if (type.IsByRef)
					{
						type = type.GetElementType();
					}
					Class7.Enum1 @enum;
					if (type == typeof(string))
					{
						@enum = (Class7.Enum1)14;
					}
					else if (type == typeof(byte))
					{
						@enum = (Class7.Enum1)2;
					}
					else if (type == typeof(sbyte))
					{
						@enum = (Class7.Enum1)1;
					}
					else if (type == typeof(short))
					{
						@enum = (Class7.Enum1)3;
					}
					else if (type == typeof(ushort))
					{
						@enum = (Class7.Enum1)4;
					}
					else if (type == typeof(int))
					{
						@enum = (Class7.Enum1)5;
					}
					else if (type == typeof(uint))
					{
						@enum = (Class7.Enum1)6;
					}
					else if (type == typeof(long))
					{
						@enum = (Class7.Enum1)7;
					}
					else if (type == typeof(ulong))
					{
						@enum = (Class7.Enum1)8;
					}
					else if (type == typeof(float))
					{
						@enum = (Class7.Enum1)9;
					}
					else if (type == typeof(double))
					{
						@enum = (Class7.Enum1)10;
					}
					else if (type == typeof(bool))
					{
						@enum = (Class7.Enum1)11;
					}
					else if (type == typeof(IntPtr))
					{
						@enum = (Class7.Enum1)12;
					}
					else if (type == typeof(UIntPtr))
					{
						@enum = (Class7.Enum1)13;
					}
					else if (type == typeof(char))
					{
						@enum = (Class7.Enum1)15;
					}
					else
					{
						@enum = (Class7.Enum1)0;
					}
					class2.enum1_0 = @enum;
				}
				@class.fBcjhfhonR = new List<Class7.Class10>(num2);
				for (int j = 0; j < num2; j++)
				{
					int num5 = Class7.smethod_6(Class7.binaryReader_0);
					Class7.Class10 class3 = new Class7.Class10();
					class3.type_0 = null;
					if (num5 >= 0 && num5 < 50)
					{
						class3.enum1_0 = (Class7.Enum1)(num5 & 31);
						class3.bool_0 = (num5 & 32) > 0;
					}
					class3.int_0 = j;
					@class.fBcjhfhonR.Add(class3);
				}
				@class.list_1 = new List<Class7.Class11>(num3);
				for (int k = 0; k < num3; k++)
				{
					int num6 = Class7.smethod_6(Class7.binaryReader_0);
					int num7 = Class7.smethod_6(Class7.binaryReader_0);
					Class7.Class11 class4 = new Class7.Class11();
					class4.int_0 = num6;
					class4.int_1 = num7;
					Class7.Class12 class5 = new Class7.Class12();
					class4.class12_0 = class5;
					num6 = Class7.smethod_6(Class7.binaryReader_0);
					num7 = Class7.smethod_6(Class7.binaryReader_0);
					int num8 = Class7.smethod_6(Class7.binaryReader_0);
					class5.int_0 = num6;
					class5.int_1 = num7;
					class5.int_3 = num8;
					if (num8 == 0)
					{
						class5.type_0 = module.ResolveType(Class7.smethod_6(Class7.binaryReader_0));
					}
					else if (num8 == 1)
					{
						class5.int_2 = Class7.smethod_6(Class7.binaryReader_0);
					}
					else
					{
						Class7.smethod_6(Class7.binaryReader_0);
					}
					@class.list_1.Add(class4);
				}
				@class.list_1.Sort(new Comparison<Class7.Class11>(Class7.Class33<T>.<>9.method_0));
				@class.list_0 = new List<Class7.Class8>(num4);
				for (int l = 0; l < num4; l++)
				{
					Class7.Class8 class6 = new Class7.Class8();
					byte b = Class7.binaryReader_0.ReadByte();
					class6.enum3_0 = (Class7.Enum3)b;
					if (b >= 176)
					{
						throw new Exception();
					}
					int num9 = (int)Class7.byte_0[(int)b];
					if (num9 == 0)
					{
						class6.object_0 = null;
					}
					else
					{
						object obj2;
						switch (num9)
						{
						case 1:
							obj2 = Class7.smethod_6(Class7.binaryReader_0);
							break;
						case 2:
							obj2 = Class7.binaryReader_0.ReadInt64();
							break;
						case 3:
							obj2 = Class7.binaryReader_0.ReadSingle();
							break;
						case 4:
							obj2 = Class7.binaryReader_0.ReadDouble();
							break;
						case 5:
						{
							int num10 = Class7.smethod_6(Class7.binaryReader_0);
							int[] array = new int[num10];
							for (int m = 0; m < num10; m++)
							{
								array[m] = Class7.smethod_6(Class7.binaryReader_0);
							}
							obj2 = array;
							break;
						}
						default:
							throw new Exception();
						}
						class6.object_0 = obj2;
					}
					@class.list_0.Add(class6);
				}
				Class7.class13_0[int_1] = @class;
			}
		}
		Class7.Class16 class7 = new Class7.Class16();
		class7.class13_0 = @class;
		ParameterInfo[] parameters2 = @class.object_0.GetParameters();
		bool flag2 = false;
		int num11 = 0;
		if (@class.object_0 is MethodInfo && ((MethodInfo)@class.object_0).ReturnType != typeof(void))
		{
			flag2 = true;
		}
		if (@class.object_0.IsStatic)
		{
			class7.class18_0 = new Class7.Class18[parameters2.Length];
			for (int n = 0; n < parameters2.Length; n++)
			{
				Type parameterType = parameters2[n].ParameterType;
				class7.class18_0[n] = Class7.Class18.smethod_1(parameterType, object_1[n]);
				if (parameterType.IsByRef)
				{
					num11++;
				}
			}
		}
		else
		{
			class7.class18_0 = new Class7.Class18[parameters2.Length + 1];
			if (@class.object_0.DeclaringType.IsValueType)
			{
				class7.class18_0[0] = new Class7.Class29(new Class7.Class30(object_2), @class.object_0.DeclaringType);
			}
			else
			{
				class7.class18_0[0] = new Class7.Class30(object_2);
			}
			for (int num12 = 0; num12 < parameters2.Length; num12++)
			{
				Type parameterType2 = parameters2[num12].ParameterType;
				if (parameterType2.IsByRef)
				{
					class7.class18_0[num12 + 1] = Class7.Class18.smethod_1(parameterType2, object_1[num12]);
					num11++;
				}
				else
				{
					class7.class18_0[num12 + 1] = Class7.Class18.smethod_1(parameterType2, object_1[num12]);
				}
			}
		}
		class7.class18_1 = new Class7.Class18[@class.fBcjhfhonR.Count];
		for (int num13 = 0; num13 < @class.fBcjhfhonR.Count; num13++)
		{
			Class7.Class10 class8 = @class.fBcjhfhonR[num13];
			switch (class8.enum1_0)
			{
			case (Class7.Enum1)0:
				class7.class18_1[num13] = null;
				break;
			case (Class7.Enum1)1:
			case (Class7.Enum1)2:
			case (Class7.Enum1)3:
			case (Class7.Enum1)4:
			case (Class7.Enum1)5:
			case (Class7.Enum1)6:
			case (Class7.Enum1)11:
			case (Class7.Enum1)15:
				class7.class18_1[num13] = new Class7.Class20(0, class8.enum1_0);
				break;
			case (Class7.Enum1)7:
			case (Class7.Enum1)8:
				class7.class18_1[num13] = new Class7.Class21(0L, class8.enum1_0);
				break;
			case (Class7.Enum1)9:
			case (Class7.Enum1)10:
				class7.class18_1[num13] = new Class7.Class23(0.0, class8.enum1_0);
				break;
			case (Class7.Enum1)12:
				class7.class18_1[num13] = new Class7.Class22(IntPtr.Zero);
				break;
			case (Class7.Enum1)13:
				class7.class18_1[num13] = new Class7.Class22(UIntPtr.Zero);
				break;
			case (Class7.Enum1)14:
				class7.class18_1[num13] = null;
				break;
			case (Class7.Enum1)16:
				class7.class18_1[num13] = new Class7.Class30(null);
				break;
			}
		}
		try
		{
			class7.method_0();
		}
		finally
		{
			class7.method_1();
		}
		int num14 = 0;
		if (flag2)
		{
			num14 = 1;
		}
		num14 += num11;
		object[] array2 = new object[num14];
		if (flag2)
		{
			array2[0] = null;
		}
		if (@class.object_0 is MethodInfo)
		{
			MethodInfo methodInfo = (MethodInfo)@class.object_0;
			if (methodInfo.ReturnType != typeof(void) && class7.class18_2 != null)
			{
				array2[0] = class7.class18_2.vmethod_4(methodInfo.ReturnType);
			}
		}
		if (num11 > 0)
		{
			int num15 = 0;
			if (flag2)
			{
				num15++;
			}
			for (int num16 = 0; num16 < parameters2.Length; num16++)
			{
				Type type2 = parameters2[num16].ParameterType;
				if (type2.IsByRef)
				{
					type2 = type2.GetElementType();
					if (class7.class18_0[num16] != null)
					{
						if (@class.object_0.IsStatic)
						{
							array2[num15] = class7.class18_0[num16].vmethod_4(type2);
						}
						else
						{
							array2[num15] = class7.class18_0[num16 + 1].vmethod_4(type2);
						}
					}
					else
					{
						array2[num15] = null;
					}
					num15++;
				}
			}
		}
		if (!@class.object_0.IsStatic && @class.object_0.DeclaringType.IsValueType)
		{
			gparam_0 = (T)((object)class7.class18_0[0].vmethod_4(@class.object_0.DeclaringType));
		}
		return array2;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00034CFC File Offset: 0x00032EFC
	internal static object[] smethod_2(int int_1, object object_1, object object_2)
	{
		int num = 0;
		return Class7.smethod_1<int>(int_1, object_1, object_2, ref num);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00004915 File Offset: 0x00002B15
	internal static object[] smethod_3<T>(int int_1, object object_1, ref T gparam_0)
	{
		return Class7.smethod_1<T>(int_1, object_1, gparam_0, ref gparam_0);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00034D18 File Offset: 0x00032F18
	internal static void smethod_4()
	{
		if (Class7.int_0 == null)
		{
			BinaryReader binaryReader = new BinaryReader(typeof(Class7).Assembly.GetManifestResourceStream("BgdyAYGMU7YejBKTpl.KmLDv16bXNnBAg9JMy"));
			binaryReader.BaseStream.Position = 0L;
			byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
			binaryReader.Close();
			Class7.smethod_5(array);
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00034D80 File Offset: 0x00032F80
	internal static void smethod_5(byte[] byte_1)
	{
		Class7.binaryReader_0 = new BinaryReader(new MemoryStream(byte_1));
		Class7.byte_0 = new byte[255];
		int num = Class7.smethod_6(Class7.binaryReader_0);
		for (int i = 0; i < num; i++)
		{
			int num2 = (int)Class7.binaryReader_0.ReadByte();
			Class7.byte_0[num2] = Class7.binaryReader_0.ReadByte();
		}
		num = Class7.smethod_6(Class7.binaryReader_0);
		Class7.list_0 = new List<string>(num);
		for (int j = 0; j < num; j++)
		{
			Class7.list_0.Add(Encoding.Unicode.GetString(Class7.binaryReader_0.ReadBytes(Class7.smethod_6(Class7.binaryReader_0))));
		}
		num = Class7.smethod_6(Class7.binaryReader_0);
		Class7.class13_0 = new Class7.Class13[num];
		Class7.int_0 = new int[num];
		for (int k = 0; k < num; k++)
		{
			Class7.class13_0[k] = null;
			Class7.int_0[k] = Class7.smethod_6(Class7.binaryReader_0);
		}
		int num3 = (int)Class7.binaryReader_0.BaseStream.Position;
		for (int l = 0; l < num; l++)
		{
			int num4 = Class7.int_0[l];
			Class7.int_0[l] = num3;
			num3 += num4;
		}
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00034EC8 File Offset: 0x000330C8
	internal static int smethod_6(BinaryReader binaryReader_1)
	{
		bool flag = false;
		uint num = (uint)binaryReader_1.ReadByte();
		uint num2 = 0U | (num & 63U);
		if ((num & 64U) != 0U)
		{
			flag = true;
		}
		if (num >= 128U)
		{
			int num3 = 0;
			for (;;)
			{
				uint num4 = (uint)binaryReader_1.ReadByte();
				num2 |= (num4 & 127U) << 7 * num3 + 6;
				if (num4 < 128U)
				{
					break;
				}
				num3++;
			}
			if (flag)
			{
				return (int)(~(int)num2);
			}
			return (int)num2;
		}
		else
		{
			if (!flag)
			{
				return (int)num2;
			}
			return (int)(~(int)num2);
		}
	}

	// Token: 0x0400067B RID: 1659
	internal static Class7.Class13[] class13_0 = null;

	// Token: 0x0400067C RID: 1660
	internal static int[] int_0 = null;

	// Token: 0x0400067D RID: 1661
	internal static List<string> list_0;

	// Token: 0x0400067E RID: 1662
	private static BinaryReader binaryReader_0;

	// Token: 0x0400067F RID: 1663
	private static byte[] byte_0;

	// Token: 0x04000680 RID: 1664
	private static bool bool_0 = false;

	// Token: 0x04000681 RID: 1665
	private static object object_0 = new object();

	// Token: 0x0200015F RID: 351
	[StructLayout(LayoutKind.Explicit)]
	public struct Struct2
	{
		// Token: 0x04000682 RID: 1666
		[FieldOffset(0)]
		public byte byte_0;

		// Token: 0x04000683 RID: 1667
		[FieldOffset(0)]
		public sbyte sbyte_0;

		// Token: 0x04000684 RID: 1668
		[FieldOffset(0)]
		public ushort ushort_0;

		// Token: 0x04000685 RID: 1669
		[FieldOffset(0)]
		public short short_0;

		// Token: 0x04000686 RID: 1670
		[FieldOffset(0)]
		public uint uint_0;

		// Token: 0x04000687 RID: 1671
		[FieldOffset(0)]
		public int int_0;
	}

	// Token: 0x02000160 RID: 352
	private class Class20 : Class7.Class19
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x00034F70 File Offset: 0x00033170
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			this.struct2_0 = ((Class7.Class20)class18_0).struct2_0;
			this.enum1_0 = ((Class7.Class20)class18_0).enum1_0;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_9(class18_0);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00034FB4 File Offset: 0x000331B4
		public Class20(bool bool_0)
		{
			this.enum4_0 = (Class7.Enum4)1;
			if (!bool_0)
			{
				this.struct2_0.int_0 = 0;
			}
			else
			{
				this.struct2_0.int_0 = 1;
			}
			this.enum1_0 = (Class7.Enum1)11;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00034FF4 File Offset: 0x000331F4
		public Class20(Class7.Class20 class20_0)
		{
			this.enum4_0 = class20_0.enum4_0;
			this.struct2_0.int_0 = class20_0.struct2_0.int_0;
			this.enum1_0 = class20_0.enum1_0;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000492A File Offset: 0x00002B2A
		public override Class7.Class19 vmethod_71()
		{
			return new Class7.Class20(this);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00035038 File Offset: 0x00033238
		public Class20(int int_0)
		{
			this.enum4_0 = (Class7.Enum4)1;
			this.struct2_0.int_0 = int_0;
			this.enum1_0 = (Class7.Enum1)5;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00035068 File Offset: 0x00033268
		public Class20(uint uint_0)
		{
			this.enum4_0 = (Class7.Enum4)1;
			this.struct2_0.uint_0 = uint_0;
			this.enum1_0 = (Class7.Enum1)6;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00035098 File Offset: 0x00033298
		public Class20(int int_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)1;
			this.struct2_0.int_0 = int_0;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000350C8 File Offset: 0x000332C8
		public Class20(uint uint_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)1;
			this.struct2_0.uint_0 = uint_0;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000350F8 File Offset: 0x000332F8
		public override bool vmethod_10()
		{
			Class7.Enum1 @enum = this.enum1_0;
			switch (@enum)
			{
			case (Class7.Enum1)1:
			case (Class7.Enum1)3:
			case (Class7.Enum1)5:
			case (Class7.Enum1)7:
				goto IL_4A;
			case (Class7.Enum1)2:
			case (Class7.Enum1)4:
			case (Class7.Enum1)6:
				break;
			default:
				if (@enum == (Class7.Enum1)11)
				{
					goto IL_4A;
				}
				if (@enum == (Class7.Enum1)15)
				{
					goto IL_4A;
				}
				break;
			}
			return this.struct2_0.uint_0 == 0U;
			IL_4A:
			return this.struct2_0.int_0 == 0;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00004932 File Offset: 0x00002B32
		public override bool vmethod_11()
		{
			return !this.vmethod_10();
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00035160 File Offset: 0x00033360
		public override Class7.Class18 vmethod_12(Class7.Enum1 enum1_1)
		{
			switch (enum1_1)
			{
			case (Class7.Enum1)1:
				return this.vmethod_14();
			case (Class7.Enum1)2:
				return this.vmethod_15();
			case (Class7.Enum1)3:
				return this.vmethod_16();
			case (Class7.Enum1)4:
				return this.vmethod_17();
			case (Class7.Enum1)5:
				return this.vmethod_18();
			case (Class7.Enum1)6:
				return this.vmethod_19();
			case (Class7.Enum1)11:
				return this.vmethod_13();
			case (Class7.Enum1)15:
				return this.method_6();
			case (Class7.Enum1)16:
				return this.vmethod_71();
			}
			throw new Exception(((Class7.Enum5)4).ToString());
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003520C File Offset: 0x0003340C
		internal override object vmethod_4(Type type_0)
		{
			if (type_0 != null && type_0.IsByRef)
			{
				type_0 = type_0.GetElementType();
			}
			if (type_0 != null && Nullable.GetUnderlyingType(type_0) != null)
			{
				type_0 = Nullable.GetUnderlyingType(type_0);
			}
			if (type_0 == null || type_0 == typeof(object))
			{
				switch (this.enum1_0)
				{
				case (Class7.Enum1)1:
					return this.struct2_0.sbyte_0;
				case (Class7.Enum1)2:
					return this.struct2_0.byte_0;
				case (Class7.Enum1)3:
					return this.struct2_0.short_0;
				case (Class7.Enum1)4:
					return this.struct2_0.ushort_0;
				case (Class7.Enum1)5:
					return this.struct2_0.int_0;
				case (Class7.Enum1)6:
					return this.struct2_0.uint_0;
				case (Class7.Enum1)7:
					return (long)this.struct2_0.int_0;
				case (Class7.Enum1)8:
					return (ulong)this.struct2_0.uint_0;
				case (Class7.Enum1)11:
					return this.vmethod_11();
				case (Class7.Enum1)15:
					return (char)this.struct2_0.int_0;
				}
				return this.struct2_0.int_0;
			}
			if (type_0 == typeof(int))
			{
				return this.struct2_0.int_0;
			}
			if (type_0 == typeof(uint))
			{
				return this.struct2_0.uint_0;
			}
			if (type_0 == typeof(short))
			{
				return this.struct2_0.short_0;
			}
			if (type_0 == typeof(ushort))
			{
				return this.struct2_0.ushort_0;
			}
			if (type_0 == typeof(byte))
			{
				return this.struct2_0.byte_0;
			}
			if (type_0 == typeof(sbyte))
			{
				return this.struct2_0.sbyte_0;
			}
			if (type_0 == typeof(bool))
			{
				return !this.vmethod_10();
			}
			if (type_0 == typeof(long))
			{
				return (long)this.struct2_0.int_0;
			}
			if (type_0 == typeof(ulong))
			{
				return (ulong)this.struct2_0.uint_0;
			}
			if (type_0 == typeof(char))
			{
				return (char)this.struct2_0.int_0;
			}
			if (type_0 == typeof(IntPtr))
			{
				return new IntPtr(this.struct2_0.int_0);
			}
			if (type_0 == typeof(UIntPtr))
			{
				return new UIntPtr(this.struct2_0.uint_0);
			}
			if (type_0.IsEnum)
			{
				return this.method_5(type_0);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00035550 File Offset: 0x00033750
		internal object method_5(Type type_0)
		{
			Type underlyingType = Enum.GetUnderlyingType(type_0);
			if (underlyingType == typeof(int))
			{
				return Enum.ToObject(type_0, this.struct2_0.int_0);
			}
			if (underlyingType == typeof(uint))
			{
				return Enum.ToObject(type_0, this.struct2_0.uint_0);
			}
			if (underlyingType == typeof(short))
			{
				return Enum.ToObject(type_0, this.struct2_0.short_0);
			}
			if (underlyingType == typeof(ushort))
			{
				return Enum.ToObject(type_0, this.struct2_0.ushort_0);
			}
			if (underlyingType == typeof(byte))
			{
				return Enum.ToObject(type_0, this.struct2_0.byte_0);
			}
			if (underlyingType == typeof(sbyte))
			{
				return Enum.ToObject(type_0, this.struct2_0.sbyte_0);
			}
			if (underlyingType == typeof(long))
			{
				return Enum.ToObject(type_0, (long)this.struct2_0.int_0);
			}
			if (underlyingType == typeof(ulong))
			{
				return Enum.ToObject(type_0, (ulong)this.struct2_0.uint_0);
			}
			if (underlyingType == typeof(char))
			{
				return Enum.ToObject(type_0, (ushort)this.struct2_0.int_0);
			}
			return Enum.ToObject(type_0, this.struct2_0.int_0);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000356CC File Offset: 0x000338CC
		public override Class7.Class20 vmethod_13()
		{
			return new Class7.Class20(this.vmethod_10() ? 0 : 1);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000493D File Offset: 0x00002B3D
		internal override bool vmethod_6()
		{
			return this.vmethod_11();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00004945 File Offset: 0x00002B45
		public override Class7.Class20 vmethod_14()
		{
			return new Class7.Class20((int)this.struct2_0.sbyte_0, (Class7.Enum1)1);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00004958 File Offset: 0x00002B58
		public Class7.Class20 method_6()
		{
			return new Class7.Class20(this.struct2_0.int_0, (Class7.Enum1)15);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000496C File Offset: 0x00002B6C
		public override Class7.Class20 vmethod_15()
		{
			return new Class7.Class20((uint)this.struct2_0.byte_0, (Class7.Enum1)2);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000497F File Offset: 0x00002B7F
		public override Class7.Class20 vmethod_16()
		{
			return new Class7.Class20((int)this.struct2_0.short_0, (Class7.Enum1)3);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00004992 File Offset: 0x00002B92
		public override Class7.Class20 vmethod_17()
		{
			return new Class7.Class20((uint)this.struct2_0.ushort_0, (Class7.Enum1)4);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000049A5 File Offset: 0x00002BA5
		public override Class7.Class20 vmethod_18()
		{
			return new Class7.Class20(this.struct2_0.int_0, (Class7.Enum1)5);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000049B8 File Offset: 0x00002BB8
		public override Class7.Class20 vmethod_19()
		{
			return new Class7.Class20(this.struct2_0.uint_0, (Class7.Enum1)6);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000049CB File Offset: 0x00002BCB
		public override Class7.Class21 vmethod_20()
		{
			return new Class7.Class21((long)this.struct2_0.int_0, (Class7.Enum1)7);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000049DF File Offset: 0x00002BDF
		public override Class7.Class21 vmethod_21()
		{
			return new Class7.Class21((ulong)this.struct2_0.uint_0, (Class7.Enum1)8);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000049F3 File Offset: 0x00002BF3
		public override Class7.Class20 vmethod_22()
		{
			return this.vmethod_14();
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000049FB File Offset: 0x00002BFB
		public override Class7.Class20 vmethod_23()
		{
			return this.vmethod_16();
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00004A03 File Offset: 0x00002C03
		public override Class7.Class20 vmethod_24()
		{
			return this.vmethod_18();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00004A0B File Offset: 0x00002C0B
		public override Class7.Class21 vmethod_25()
		{
			return this.vmethod_20();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00004A13 File Offset: 0x00002C13
		public override Class7.Class20 vmethod_26()
		{
			return this.vmethod_15();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00004A1B File Offset: 0x00002C1B
		public override Class7.Class20 vmethod_27()
		{
			return this.vmethod_17();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00004A23 File Offset: 0x00002C23
		public override Class7.Class20 vmethod_28()
		{
			return this.vmethod_19();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00004A2B File Offset: 0x00002C2B
		public override Class7.Class21 vmethod_29()
		{
			return this.vmethod_21();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00004A33 File Offset: 0x00002C33
		public override Class7.Class20 vmethod_30()
		{
			return new Class7.Class20((int)(checked((sbyte)this.struct2_0.int_0)), (Class7.Enum1)1);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00004A47 File Offset: 0x00002C47
		public override Class7.Class20 vmethod_31()
		{
			return new Class7.Class20((int)(checked((sbyte)this.struct2_0.uint_0)), (Class7.Enum1)1);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00004A5B File Offset: 0x00002C5B
		public override Class7.Class20 vmethod_32()
		{
			return new Class7.Class20((int)(checked((short)this.struct2_0.int_0)), (Class7.Enum1)3);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00004A6F File Offset: 0x00002C6F
		public override Class7.Class20 usfdqHavse()
		{
			return new Class7.Class20((int)(checked((short)this.struct2_0.uint_0)), (Class7.Enum1)3);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000049A5 File Offset: 0x00002BA5
		public override Class7.Class20 vmethod_33()
		{
			return new Class7.Class20(this.struct2_0.int_0, (Class7.Enum1)5);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00004A83 File Offset: 0x00002C83
		public override Class7.Class20 vmethod_34()
		{
			return new Class7.Class20(checked((int)this.struct2_0.uint_0), (Class7.Enum1)5);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000049CB File Offset: 0x00002BCB
		public override Class7.Class21 vmethod_35()
		{
			return new Class7.Class21((long)this.struct2_0.int_0, (Class7.Enum1)7);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00004A97 File Offset: 0x00002C97
		public override Class7.Class21 vmethod_36()
		{
			return new Class7.Class21((long)((ulong)this.struct2_0.uint_0), (Class7.Enum1)7);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00004AAB File Offset: 0x00002CAB
		public override Class7.Class20 vmethod_37()
		{
			return new Class7.Class20((int)(checked((byte)this.struct2_0.int_0)), (Class7.Enum1)2);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00004ABF File Offset: 0x00002CBF
		public override Class7.Class20 vmethod_38()
		{
			return new Class7.Class20((int)(checked((byte)this.struct2_0.uint_0)), (Class7.Enum1)2);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00004AD3 File Offset: 0x00002CD3
		public override Class7.Class20 vmethod_39()
		{
			return new Class7.Class20((int)(checked((ushort)this.struct2_0.int_0)), (Class7.Enum1)4);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00004AE7 File Offset: 0x00002CE7
		public override Class7.Class20 vmethod_40()
		{
			return new Class7.Class20((int)(checked((ushort)this.struct2_0.uint_0)), (Class7.Enum1)4);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00004AFB File Offset: 0x00002CFB
		public override Class7.Class20 vmethod_41()
		{
			return new Class7.Class20(checked((uint)this.struct2_0.int_0), (Class7.Enum1)6);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000049B8 File Offset: 0x00002BB8
		public override Class7.Class20 vmethod_42()
		{
			return new Class7.Class20(this.struct2_0.uint_0, (Class7.Enum1)6);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00004B0F File Offset: 0x00002D0F
		public override Class7.Class21 vmethod_43()
		{
			return new Class7.Class21(checked((ulong)this.struct2_0.int_0), (Class7.Enum1)8);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000049DF File Offset: 0x00002BDF
		public override Class7.Class21 vmethod_44()
		{
			return new Class7.Class21((ulong)this.struct2_0.uint_0, (Class7.Enum1)8);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00004B23 File Offset: 0x00002D23
		public override Class7.Class23 vmethod_45()
		{
			return new Class7.Class23((float)this.struct2_0.int_0);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00004B36 File Offset: 0x00002D36
		public override Class7.Class23 vmethod_46()
		{
			return new Class7.Class23((double)this.struct2_0.int_0);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00004B49 File Offset: 0x00002D49
		public override Class7.Class23 vmethod_47()
		{
			return new Class7.Class23(this.struct2_0.uint_0);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000356EC File Offset: 0x000338EC
		public override Class7.Class22 vmethod_48()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_25().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_24().struct2_0.int_0);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00035730 File Offset: 0x00033930
		public override Class7.Class22 vmethod_49()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_29().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_28().struct2_0.uint_0);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00035774 File Offset: 0x00033974
		public override Class7.Class22 vmethod_50()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_35().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_33().struct2_0.int_0);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000357B8 File Offset: 0x000339B8
		public override Class7.Class22 vmethod_51()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_43().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_41().struct2_0.uint_0);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000357FC File Offset: 0x000339FC
		public override Class7.Class22 vmethod_52()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_36().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_34().struct2_0.int_0);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00035840 File Offset: 0x00033A40
		public override Class7.Class22 vmethod_53()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_44().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_42().struct2_0.uint_0);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00035884 File Offset: 0x00033A84
		public override Class7.Class18 vmethod_54()
		{
			Class7.Enum1 @enum = this.enum1_0;
			switch (@enum)
			{
			case (Class7.Enum1)1:
			case (Class7.Enum1)3:
			case (Class7.Enum1)5:
				goto IL_47;
			case (Class7.Enum1)2:
			case (Class7.Enum1)4:
				break;
			default:
				if (@enum == (Class7.Enum1)11)
				{
					goto IL_47;
				}
				if (@enum == (Class7.Enum1)15)
				{
					goto IL_47;
				}
				break;
			}
			return new Class7.Class20((int)(-(int)((ulong)this.struct2_0.uint_0)));
			IL_47:
			return new Class7.Class20(-this.struct2_0.int_0);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000358EC File Offset: 0x00033AEC
		public override Class7.Class18 Add(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 + ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).Add(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00035950 File Offset: 0x00033B50
		public override Class7.Class18 vmethod_55(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.int_0 + ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_55(this);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000359B4 File Offset: 0x00033BB4
		public override Class7.Class18 vmethod_56(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.uint_0 + ((Class7.Class20)class18_0).struct2_0.uint_0));
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_56(this);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00035A18 File Offset: 0x00033C18
		public override Class7.Class18 vmethod_57(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 - ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).method_7(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00035A7C File Offset: 0x00033C7C
		public override Class7.Class18 vmethod_58(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.int_0 - ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).method_8(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00035AE0 File Offset: 0x00033CE0
		public override Class7.Class18 vmethod_59(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.uint_0 - ((Class7.Class20)class18_0).struct2_0.uint_0));
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).method_9(this);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00035B44 File Offset: 0x00033D44
		public override Class7.Class18 vmethod_60(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 * ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).vmethod_60(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00035BA8 File Offset: 0x00033DA8
		public override Class7.Class18 vmethod_61(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.int_0 * ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_61(this);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00035C0C File Offset: 0x00033E0C
		public override Class7.Class18 vmethod_62(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(checked(this.struct2_0.uint_0 * ((Class7.Class20)class18_0).struct2_0.uint_0));
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_62(this);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00035C70 File Offset: 0x00033E70
		public override Class7.Class18 vmethod_63(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 / ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).method_10(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00035CD4 File Offset: 0x00033ED4
		public override Class7.Class18 vmethod_64(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.uint_0 / ((Class7.Class20)class18_0).struct2_0.uint_0);
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).method_11(this);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00035D38 File Offset: 0x00033F38
		public override Class7.Class18 vmethod_65(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 % ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).method_12(this);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00035D9C File Offset: 0x00033F9C
		public override Class7.Class18 vmethod_66(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.uint_0 % ((Class7.Class20)class18_0).struct2_0.uint_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).yIwByEsukh(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00035E00 File Offset: 0x00034000
		public override Class7.Class18 vmethod_67(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 & ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_67(this);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00035E64 File Offset: 0x00034064
		public override Class7.Class18 vmethod_68(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 | ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).vmethod_68(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00004B5D File Offset: 0x00002D5D
		public override Class7.Class18 vmethod_69()
		{
			return new Class7.Class20(~this.struct2_0.int_0);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00035EC8 File Offset: 0x000340C8
		public override Class7.Class18 vmethod_70(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 ^ ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).vmethod_70(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00035F2C File Offset: 0x0003412C
		public override Class7.Class18 vmethod_72(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 << ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).method_15(this);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00035F90 File Offset: 0x00034190
		public override Class7.Class18 vmethod_73(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.int_0 >> ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).method_14(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00035FF4 File Offset: 0x000341F4
		public override Class7.Class18 vmethod_74(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return new Class7.Class20(this.struct2_0.uint_0 >> ((Class7.Class20)class18_0).struct2_0.int_0);
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).method_13(this);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00036058 File Offset: 0x00034258
		public override string ToString()
		{
			Class7.Enum1 @enum = this.enum1_0;
			switch (@enum)
			{
			case (Class7.Enum1)1:
			case (Class7.Enum1)3:
			case (Class7.Enum1)5:
				goto IL_3E;
			case (Class7.Enum1)2:
			case (Class7.Enum1)4:
				break;
			default:
				if (@enum == (Class7.Enum1)11)
				{
					goto IL_3E;
				}
				break;
			}
			return this.struct2_0.uint_0.ToString();
			IL_3E:
			return this.struct2_0.int_0.ToString();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00004B70 File Offset: 0x00002D70
		internal override Class7.Class18 vmethod_7()
		{
			return this;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_8()
		{
			return true;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000360B4 File Offset: 0x000342B4
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return ((Class7.Class30)class18_0).vmethod_5(this);
			}
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).vmethod_5(this);
			}
			Class7.Class18 @class = class18_0.vmethod_7();
			if (!@class.vmethod_8())
			{
				return false;
			}
			if (@class.method_3())
			{
				return false;
			}
			if (!@class.method_1())
			{
				return ((Class7.Class22)@class).vmethod_5(this);
			}
			return this.struct2_0.int_0 == ((Class7.Class20)@class).struct2_0.int_0;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00036140 File Offset: 0x00034340
		private static Class7.Class19 smethod_4(Class7.Class18 class18_0)
		{
			Class7.Class19 @class = class18_0 as Class7.Class19;
			if (@class == null && class18_0.vmethod_0())
			{
				@class = class18_0.vmethod_7() as Class7.Class19;
			}
			return @class;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00036170 File Offset: 0x00034370
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).BeouTiljCp(this);
			}
			Class7.Class18 @class = class18_0.vmethod_7();
			if (!@class.vmethod_8())
			{
				return false;
			}
			if (@class.method_3())
			{
				return false;
			}
			if (@class.method_1())
			{
				return this.struct2_0.uint_0 != ((Class7.Class20)@class).struct2_0.uint_0;
			}
			return ((Class7.Class22)@class).BeouTiljCp(this);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000361F4 File Offset: 0x000343F4
		public override bool vmethod_75(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.int_0 >= ((Class7.Class20)class18_0).struct2_0.int_0;
			}
			if (class18_0.method_2())
			{
				return ((Class7.Class22)class18_0).vmethod_78(this);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00036254 File Offset: 0x00034454
		public override bool vmethod_76(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.uint_0 >= ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_79(this);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000362B4 File Offset: 0x000344B4
		public override bool vmethod_77(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.int_0 > ((Class7.Class20)class18_0).struct2_0.int_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_80(this);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00036314 File Offset: 0x00034514
		public override bool lwlumgaheq(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.uint_0 > ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_81(this);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00036374 File Offset: 0x00034574
		public override bool vmethod_78(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.int_0 <= ((Class7.Class20)class18_0).struct2_0.int_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_75(this);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000363D4 File Offset: 0x000345D4
		public override bool vmethod_79(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.uint_0 <= ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_76(this);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00036434 File Offset: 0x00034634
		public override bool vmethod_80(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.int_0 < ((Class7.Class20)class18_0).struct2_0.int_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).vmethod_77(this);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00036494 File Offset: 0x00034694
		public override bool vmethod_81(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				return this.struct2_0.uint_0 < ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			if (!class18_0.method_2())
			{
				throw new Class7.Exception1();
			}
			return ((Class7.Class22)class18_0).lwlumgaheq(this);
		}

		// Token: 0x04000688 RID: 1672
		public Class7.Struct2 struct2_0;

		// Token: 0x04000689 RID: 1673
		public Class7.Enum1 enum1_0;
	}

	// Token: 0x02000161 RID: 353
	[StructLayout(LayoutKind.Explicit)]
	private struct Struct3
	{
		// Token: 0x0400068A RID: 1674
		[FieldOffset(0)]
		public byte byte_0;

		// Token: 0x0400068B RID: 1675
		[FieldOffset(0)]
		public sbyte sbyte_0;

		// Token: 0x0400068C RID: 1676
		[FieldOffset(0)]
		public ushort ushort_0;

		// Token: 0x0400068D RID: 1677
		[FieldOffset(0)]
		public short short_0;

		// Token: 0x0400068E RID: 1678
		[FieldOffset(0)]
		public uint uint_0;

		// Token: 0x0400068F RID: 1679
		[FieldOffset(0)]
		public int int_0;

		// Token: 0x04000690 RID: 1680
		[FieldOffset(0)]
		public ulong ulong_0;

		// Token: 0x04000691 RID: 1681
		[FieldOffset(0)]
		public long long_0;
	}

	// Token: 0x02000162 RID: 354
	private class Class21 : Class7.Class19
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x000364F4 File Offset: 0x000346F4
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			this.struct3_0 = ((Class7.Class21)class18_0).struct3_0;
			this.enum1_0 = ((Class7.Class21)class18_0).enum1_0;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_9(class18_0);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00036524 File Offset: 0x00034724
		public Class21(long long_0)
		{
			this.enum4_0 = (Class7.Enum4)2;
			this.struct3_0.long_0 = long_0;
			this.enum1_0 = (Class7.Enum1)7;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00036554 File Offset: 0x00034754
		public Class21(Class7.Class21 class21_0)
		{
			this.enum4_0 = class21_0.enum4_0;
			this.struct3_0.long_0 = class21_0.struct3_0.long_0;
			this.enum1_0 = class21_0.enum1_0;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00004B73 File Offset: 0x00002D73
		public override Class7.Class19 vmethod_71()
		{
			return new Class7.Class21(this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00036598 File Offset: 0x00034798
		public Class21(long long_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)2;
			this.struct3_0.long_0 = long_0;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000365C8 File Offset: 0x000347C8
		public Class21(ulong ulong_0)
		{
			this.enum4_0 = (Class7.Enum4)2;
			this.struct3_0.ulong_0 = ulong_0;
			this.enum1_0 = (Class7.Enum1)8;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000365F8 File Offset: 0x000347F8
		public Class21(ulong ulong_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)2;
			this.struct3_0.ulong_0 = ulong_0;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00036628 File Offset: 0x00034828
		public override bool vmethod_10()
		{
			if (this.enum1_0 == (Class7.Enum1)7)
			{
				return this.struct3_0.long_0 == 0L;
			}
			return this.struct3_0.ulong_0 == 0UL;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00004932 File Offset: 0x00002B32
		public override bool vmethod_11()
		{
			return !this.vmethod_10();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0003666C File Offset: 0x0003486C
		public override Class7.Class18 vmethod_12(Class7.Enum1 enum1_1)
		{
			switch (enum1_1)
			{
			case (Class7.Enum1)1:
				return this.vmethod_14();
			case (Class7.Enum1)2:
				return this.vmethod_15();
			case (Class7.Enum1)3:
				return this.vmethod_16();
			case (Class7.Enum1)4:
				return this.vmethod_17();
			case (Class7.Enum1)5:
				return this.vmethod_18();
			case (Class7.Enum1)6:
				return this.vmethod_19();
			case (Class7.Enum1)7:
				return this.vmethod_20();
			case (Class7.Enum1)8:
				return this.vmethod_21();
			case (Class7.Enum1)11:
				return this.vmethod_13();
			case (Class7.Enum1)15:
				return this.method_6();
			case (Class7.Enum1)16:
				return this.vmethod_71();
			}
			throw new Exception(((Class7.Enum5)4).ToString());
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00036728 File Offset: 0x00034928
		internal override object vmethod_4(Type type_0)
		{
			if (type_0 != null && type_0.IsByRef)
			{
				type_0 = type_0.GetElementType();
			}
			if (type_0 == null || type_0 == typeof(object))
			{
				switch (this.enum1_0)
				{
				case (Class7.Enum1)1:
					return this.struct3_0.sbyte_0;
				case (Class7.Enum1)2:
					return this.struct3_0.byte_0;
				case (Class7.Enum1)3:
					return this.struct3_0.short_0;
				case (Class7.Enum1)4:
					return this.struct3_0.ushort_0;
				case (Class7.Enum1)5:
					return this.struct3_0.int_0;
				case (Class7.Enum1)6:
					return this.struct3_0.uint_0;
				case (Class7.Enum1)7:
					return this.struct3_0.long_0;
				case (Class7.Enum1)8:
					return this.struct3_0.ulong_0;
				case (Class7.Enum1)11:
					return this.vmethod_11();
				case (Class7.Enum1)15:
					return (char)this.struct3_0.int_0;
				}
				return this.struct3_0.long_0;
			}
			if (type_0 == typeof(int))
			{
				return this.struct3_0.int_0;
			}
			if (type_0 == typeof(uint))
			{
				return this.struct3_0.uint_0;
			}
			if (type_0 == typeof(short))
			{
				return this.struct3_0.short_0;
			}
			if (type_0 == typeof(ushort))
			{
				return this.struct3_0.ushort_0;
			}
			if (type_0 == typeof(byte))
			{
				return this.struct3_0.byte_0;
			}
			if (type_0 == typeof(sbyte))
			{
				return this.struct3_0.sbyte_0;
			}
			if (type_0 == typeof(bool))
			{
				return !this.vmethod_10();
			}
			if (type_0 == typeof(long))
			{
				return this.struct3_0.long_0;
			}
			if (type_0 == typeof(ulong))
			{
				return this.struct3_0.ulong_0;
			}
			if (type_0 == typeof(char))
			{
				return (char)this.struct3_0.long_0;
			}
			if (type_0.IsEnum)
			{
				return this.method_5(type_0);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000369F8 File Offset: 0x00034BF8
		internal object method_5(Type type_0)
		{
			Type underlyingType = Enum.GetUnderlyingType(type_0);
			if (underlyingType == typeof(int))
			{
				return Enum.ToObject(type_0, this.struct3_0.int_0);
			}
			if (underlyingType == typeof(uint))
			{
				return Enum.ToObject(type_0, this.struct3_0.uint_0);
			}
			if (underlyingType == typeof(short))
			{
				return Enum.ToObject(type_0, this.struct3_0.short_0);
			}
			if (underlyingType == typeof(ushort))
			{
				return Enum.ToObject(type_0, this.struct3_0.ushort_0);
			}
			if (underlyingType == typeof(byte))
			{
				return Enum.ToObject(type_0, this.struct3_0.byte_0);
			}
			if (underlyingType == typeof(sbyte))
			{
				return Enum.ToObject(type_0, this.struct3_0.sbyte_0);
			}
			if (underlyingType == typeof(long))
			{
				return Enum.ToObject(type_0, this.struct3_0.long_0);
			}
			if (underlyingType == typeof(ulong))
			{
				return Enum.ToObject(type_0, this.struct3_0.ulong_0);
			}
			if (!(underlyingType == typeof(char)))
			{
				return Enum.ToObject(type_0, this.struct3_0.long_0);
			}
			return Enum.ToObject(type_0, (ushort)this.struct3_0.int_0);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000356CC File Offset: 0x000338CC
		public override Class7.Class20 vmethod_13()
		{
			return new Class7.Class20(this.vmethod_10() ? 0 : 1);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000493D File Offset: 0x00002B3D
		internal override bool vmethod_6()
		{
			return this.vmethod_11();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00004B7B File Offset: 0x00002D7B
		public Class7.Class20 method_6()
		{
			return new Class7.Class20((int)this.struct3_0.sbyte_0, (Class7.Enum1)15);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00004B8F File Offset: 0x00002D8F
		public override Class7.Class20 vmethod_14()
		{
			return new Class7.Class20((int)this.struct3_0.sbyte_0, (Class7.Enum1)1);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00004BA2 File Offset: 0x00002DA2
		public override Class7.Class20 vmethod_15()
		{
			return new Class7.Class20((uint)this.struct3_0.byte_0, (Class7.Enum1)2);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public override Class7.Class20 vmethod_16()
		{
			return new Class7.Class20((int)this.struct3_0.short_0, (Class7.Enum1)3);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public override Class7.Class20 vmethod_17()
		{
			return new Class7.Class20((uint)this.struct3_0.ushort_0, (Class7.Enum1)4);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00004BDB File Offset: 0x00002DDB
		public override Class7.Class20 vmethod_18()
		{
			return new Class7.Class20(this.struct3_0.int_0, (Class7.Enum1)5);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00004BEE File Offset: 0x00002DEE
		public override Class7.Class20 vmethod_19()
		{
			return new Class7.Class20(this.struct3_0.uint_0, (Class7.Enum1)6);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00004C01 File Offset: 0x00002E01
		public override Class7.Class21 vmethod_20()
		{
			return new Class7.Class21(this.struct3_0.long_0, (Class7.Enum1)7);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00004C14 File Offset: 0x00002E14
		public override Class7.Class21 vmethod_21()
		{
			return new Class7.Class21(this.struct3_0.ulong_0, (Class7.Enum1)8);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000049F3 File Offset: 0x00002BF3
		public override Class7.Class20 vmethod_22()
		{
			return this.vmethod_14();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000049FB File Offset: 0x00002BFB
		public override Class7.Class20 vmethod_23()
		{
			return this.vmethod_16();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00004A03 File Offset: 0x00002C03
		public override Class7.Class20 vmethod_24()
		{
			return this.vmethod_18();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00004A0B File Offset: 0x00002C0B
		public override Class7.Class21 vmethod_25()
		{
			return this.vmethod_20();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00004A13 File Offset: 0x00002C13
		public override Class7.Class20 vmethod_26()
		{
			return this.vmethod_15();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00004A1B File Offset: 0x00002C1B
		public override Class7.Class20 vmethod_27()
		{
			return this.vmethod_17();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00004A23 File Offset: 0x00002C23
		public override Class7.Class20 vmethod_28()
		{
			return this.vmethod_19();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00004A2B File Offset: 0x00002C2B
		public override Class7.Class21 vmethod_29()
		{
			return this.vmethod_21();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00004C27 File Offset: 0x00002E27
		public override Class7.Class20 vmethod_30()
		{
			return new Class7.Class20((int)(checked((sbyte)this.struct3_0.long_0)), (Class7.Enum1)1);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00004C3B File Offset: 0x00002E3B
		public override Class7.Class20 vmethod_31()
		{
			return new Class7.Class20((int)(checked((sbyte)this.struct3_0.ulong_0)), (Class7.Enum1)1);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00004C4F File Offset: 0x00002E4F
		public override Class7.Class20 vmethod_32()
		{
			return new Class7.Class20((int)(checked((short)this.struct3_0.long_0)), (Class7.Enum1)3);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00004C63 File Offset: 0x00002E63
		public override Class7.Class20 usfdqHavse()
		{
			return new Class7.Class20((int)(checked((short)this.struct3_0.ulong_0)), (Class7.Enum1)3);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00004C77 File Offset: 0x00002E77
		public override Class7.Class20 vmethod_33()
		{
			return new Class7.Class20(checked((int)this.struct3_0.long_0), (Class7.Enum1)5);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00004C8B File Offset: 0x00002E8B
		public override Class7.Class20 vmethod_34()
		{
			return new Class7.Class20(checked((int)this.struct3_0.ulong_0), (Class7.Enum1)5);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00004C01 File Offset: 0x00002E01
		public override Class7.Class21 vmethod_35()
		{
			return new Class7.Class21(this.struct3_0.long_0, (Class7.Enum1)7);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00004C9F File Offset: 0x00002E9F
		public override Class7.Class21 vmethod_36()
		{
			return new Class7.Class21(checked((long)this.struct3_0.ulong_0), (Class7.Enum1)7);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00004CB3 File Offset: 0x00002EB3
		public override Class7.Class20 vmethod_37()
		{
			return new Class7.Class20((int)(checked((byte)this.struct3_0.long_0)), (Class7.Enum1)2);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public override Class7.Class20 vmethod_38()
		{
			return new Class7.Class20((int)(checked((byte)this.struct3_0.ulong_0)), (Class7.Enum1)2);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00004CDB File Offset: 0x00002EDB
		public override Class7.Class20 vmethod_39()
		{
			return new Class7.Class20((int)(checked((ushort)this.struct3_0.long_0)), (Class7.Enum1)4);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00004CEF File Offset: 0x00002EEF
		public override Class7.Class20 vmethod_40()
		{
			return new Class7.Class20((int)(checked((ushort)this.struct3_0.ulong_0)), (Class7.Enum1)4);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00004D03 File Offset: 0x00002F03
		public override Class7.Class20 vmethod_41()
		{
			return new Class7.Class20(checked((uint)this.struct3_0.long_0), (Class7.Enum1)6);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00004D17 File Offset: 0x00002F17
		public override Class7.Class20 vmethod_42()
		{
			return new Class7.Class20(checked((uint)this.struct3_0.ulong_0), (Class7.Enum1)6);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00004D2B File Offset: 0x00002F2B
		public override Class7.Class21 vmethod_43()
		{
			return new Class7.Class21(checked((ulong)this.struct3_0.long_0), (Class7.Enum1)8);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00004C14 File Offset: 0x00002E14
		public override Class7.Class21 vmethod_44()
		{
			return new Class7.Class21(this.struct3_0.ulong_0, (Class7.Enum1)8);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00004D3F File Offset: 0x00002F3F
		public override Class7.Class23 vmethod_45()
		{
			return new Class7.Class23((float)this.struct3_0.long_0);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00004D52 File Offset: 0x00002F52
		public override Class7.Class23 vmethod_46()
		{
			return new Class7.Class23((double)this.struct3_0.long_0);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00004D65 File Offset: 0x00002F65
		public override Class7.Class23 vmethod_47()
		{
			return new Class7.Class23(this.struct3_0.ulong_0);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000356EC File Offset: 0x000338EC
		public override Class7.Class22 vmethod_48()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_25().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_24().struct2_0.int_0);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00035730 File Offset: 0x00033930
		public override Class7.Class22 vmethod_49()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_29().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_28().struct2_0.uint_0);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00035774 File Offset: 0x00033974
		public override Class7.Class22 vmethod_50()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_35().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_33().struct2_0.int_0);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000357B8 File Offset: 0x000339B8
		public override Class7.Class22 vmethod_51()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_43().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_41().struct2_0.uint_0);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000357FC File Offset: 0x000339FC
		public override Class7.Class22 vmethod_52()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_36().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_34().struct2_0.int_0);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00036B78 File Offset: 0x00034D78
		public override Class7.Class22 vmethod_53()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)(checked((uint)this.struct3_0.ulong_0)));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00004D79 File Offset: 0x00002F79
		public override Class7.Class18 vmethod_54()
		{
			return new Class7.Class21(-this.struct3_0.long_0);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00036BB0 File Offset: 0x00034DB0
		public override Class7.Class18 Add(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 + ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00036BFC File Offset: 0x00034DFC
		public override Class7.Class18 vmethod_55(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.long_0 + ((Class7.Class21)class18_0).struct3_0.long_0));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00036C48 File Offset: 0x00034E48
		public override Class7.Class18 vmethod_56(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.ulong_0 + ((Class7.Class21)class18_0).struct3_0.ulong_0));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00036C94 File Offset: 0x00034E94
		public override Class7.Class18 vmethod_57(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 - ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00036CE0 File Offset: 0x00034EE0
		public override Class7.Class18 vmethod_58(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.long_0 - ((Class7.Class21)class18_0).struct3_0.long_0));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00036D2C File Offset: 0x00034F2C
		public override Class7.Class18 vmethod_59(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.ulong_0 - ((Class7.Class21)class18_0).struct3_0.ulong_0));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00036D78 File Offset: 0x00034F78
		public override Class7.Class18 vmethod_60(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 * ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public override Class7.Class18 vmethod_61(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.long_0 * ((Class7.Class21)class18_0).struct3_0.long_0));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00036E10 File Offset: 0x00035010
		public override Class7.Class18 vmethod_62(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(checked(this.struct3_0.ulong_0 * ((Class7.Class21)class18_0).struct3_0.ulong_0));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00036E5C File Offset: 0x0003505C
		public override Class7.Class18 vmethod_63(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 / ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00036EA8 File Offset: 0x000350A8
		public override Class7.Class18 vmethod_64(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.ulong_0 / ((Class7.Class21)class18_0).struct3_0.ulong_0);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00036EF4 File Offset: 0x000350F4
		public override Class7.Class18 vmethod_65(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 % ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00036F40 File Offset: 0x00035140
		public override Class7.Class18 vmethod_66(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.ulong_0 % ((Class7.Class21)class18_0).struct3_0.ulong_0);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00036F8C File Offset: 0x0003518C
		public override Class7.Class18 vmethod_67(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 & ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00036FD8 File Offset: 0x000351D8
		public override Class7.Class18 vmethod_68(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 | ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00004D8C File Offset: 0x00002F8C
		public override Class7.Class18 vmethod_69()
		{
			return new Class7.Class21(~this.struct3_0.long_0);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00037024 File Offset: 0x00035224
		public override Class7.Class18 vmethod_70(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 ^ ((Class7.Class21)class18_0).struct3_0.long_0);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00037070 File Offset: 0x00035270
		public override Class7.Class18 vmethod_72(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_3())
			{
				return new Class7.Class21(this.struct3_0.long_0 << ((Class7.Class21)class18_0).struct3_0.int_0);
			}
			if (!class18_0.vmethod_3())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class21(this.struct3_0.long_0 << ((Class7.Class19)class18_0).vmethod_18().struct2_0.int_0);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000370F4 File Offset: 0x000352F4
		public override Class7.Class18 vmethod_73(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_3())
			{
				return new Class7.Class21(this.struct3_0.long_0 >> ((Class7.Class21)class18_0).struct3_0.int_0);
			}
			if (class18_0.vmethod_3())
			{
				return new Class7.Class21(this.struct3_0.long_0 >> ((Class7.Class19)class18_0).vmethod_18().struct2_0.int_0);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00037178 File Offset: 0x00035378
		public override Class7.Class18 vmethod_74(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_3())
			{
				return new Class7.Class21(this.struct3_0.ulong_0 >> ((Class7.Class21)class18_0).struct3_0.int_0);
			}
			if (class18_0.vmethod_3())
			{
				return new Class7.Class21(this.struct3_0.ulong_0 >> ((Class7.Class19)class18_0).vmethod_18().struct2_0.int_0);
			}
			throw new Class7.Exception1();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000371FC File Offset: 0x000353FC
		public override string ToString()
		{
			if (this.enum1_0 == (Class7.Enum1)7)
			{
				return this.struct3_0.long_0.ToString();
			}
			return this.struct3_0.ulong_0.ToString();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00004B70 File Offset: 0x00002D70
		internal override Class7.Class18 vmethod_7()
		{
			return this;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_8()
		{
			return true;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00037234 File Offset: 0x00035434
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return ((Class7.Class30)class18_0).vmethod_5(this);
			}
			if (!class18_0.vmethod_0())
			{
				Class7.Class18 @class = class18_0.vmethod_7();
				return @class.method_3() && this.struct3_0.long_0 == ((Class7.Class21)@class).struct3_0.long_0;
			}
			return ((Class7.Class24)class18_0).vmethod_5(this);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00036140 File Offset: 0x00034340
		private static Class7.Class19 smethod_4(Class7.Class18 class18_0)
		{
			Class7.Class19 @class = class18_0 as Class7.Class19;
			if (@class == null && class18_0.vmethod_0())
			{
				@class = class18_0.vmethod_7() as Class7.Class19;
			}
			return @class;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0003729C File Offset: 0x0003549C
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (!class18_0.vmethod_0())
			{
				Class7.Class18 @class = class18_0.vmethod_7();
				return @class.method_3() && this.struct3_0.ulong_0 != ((Class7.Class21)@class).struct3_0.ulong_0;
			}
			return ((Class7.Class24)class18_0).BeouTiljCp(this);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000372FC File Offset: 0x000354FC
		public override bool vmethod_75(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.long_0 >= ((Class7.Class21)class18_0).struct3_0.long_0;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00037348 File Offset: 0x00035548
		public override bool vmethod_76(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.ulong_0 >= ((Class7.Class21)class18_0).struct3_0.ulong_0;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00037394 File Offset: 0x00035594
		public override bool vmethod_77(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.long_0 > ((Class7.Class21)class18_0).struct3_0.long_0;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000373DC File Offset: 0x000355DC
		public override bool lwlumgaheq(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.ulong_0 > ((Class7.Class21)class18_0).struct3_0.ulong_0;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00037424 File Offset: 0x00035624
		public override bool vmethod_78(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.long_0 <= ((Class7.Class21)class18_0).struct3_0.long_0;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00037470 File Offset: 0x00035670
		public override bool vmethod_79(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.ulong_0 <= ((Class7.Class21)class18_0).struct3_0.ulong_0;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000374BC File Offset: 0x000356BC
		public override bool vmethod_80(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.long_0 < ((Class7.Class21)class18_0).struct3_0.long_0;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00037504 File Offset: 0x00035704
		public override bool vmethod_81(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_3())
			{
				throw new Class7.Exception1();
			}
			return this.struct3_0.ulong_0 < ((Class7.Class21)class18_0).struct3_0.ulong_0;
		}

		// Token: 0x04000692 RID: 1682
		public Class7.Struct3 struct3_0;

		// Token: 0x04000693 RID: 1683
		public Class7.Enum1 enum1_0;
	}

	// Token: 0x02000163 RID: 355
	private class Class22 : Class7.Class19
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0003754C File Offset: 0x0003574C
		internal void method_5(Class7.Class18 class18_0)
		{
			if (class18_0.method_2())
			{
				this.object_0 = ((Class7.Class22)class18_0).object_0;
				this.enum1_0 = ((Class7.Class22)class18_0).enum1_0;
				return;
			}
			this.vmethod_9(class18_0);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0003758C File Offset: 0x0003578C
		internal unsafe override void vmethod_9(Class7.Class18 class18_0)
		{
			if (!class18_0.method_2())
			{
				object obj = class18_0.vmethod_4(null);
				if (obj == null)
				{
					return;
				}
				IntPtr intPtr;
				if (IntPtr.Size == 8)
				{
					intPtr = new IntPtr(((Class7.Class21)this.object_0).struct3_0.long_0);
				}
				else
				{
					intPtr = new IntPtr(((Class7.Class20)this.object_0).struct2_0.int_0);
				}
				Type type = obj.GetType();
				if (type == typeof(string))
				{
					return;
				}
				if (type == typeof(byte))
				{
					*(byte*)(void*)intPtr = (byte)obj;
					return;
				}
				if (type == typeof(sbyte))
				{
					*(byte*)(void*)intPtr = (byte)((sbyte)obj);
					return;
				}
				if (type == typeof(short))
				{
					*(short*)(void*)intPtr = (short)obj;
					return;
				}
				if (type == typeof(ushort))
				{
					*(short*)(void*)intPtr = (short)((ushort)obj);
					return;
				}
				if (type == typeof(int))
				{
					*(int*)(void*)intPtr = (int)obj;
					return;
				}
				if (type == typeof(uint))
				{
					*(int*)(void*)intPtr = (int)((uint)obj);
					return;
				}
				if (type == typeof(long))
				{
					*(long*)(void*)intPtr = (long)obj;
					return;
				}
				if (type == typeof(ulong))
				{
					*(long*)(void*)intPtr = (long)((ulong)obj);
					return;
				}
				if (type == typeof(float))
				{
					*(float*)(void*)intPtr = (float)obj;
					return;
				}
				if (type == typeof(double))
				{
					*(double*)(void*)intPtr = (double)obj;
					return;
				}
				if (type == typeof(bool))
				{
					*(byte*)(void*)intPtr = (((bool)obj) ? 1 : 0);
					return;
				}
				if (type == typeof(IntPtr))
				{
					*(IntPtr*)(void*)intPtr = (IntPtr)obj;
					return;
				}
				if (type == typeof(UIntPtr))
				{
					*(IntPtr*)(void*)intPtr = (IntPtr)((UIntPtr)obj);
					return;
				}
				if (!(type == typeof(char)))
				{
					throw new Class7.Exception1();
				}
				*(short*)(void*)intPtr = (short)((char)obj);
				return;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					IntPtr intPtr2 = new IntPtr(((Class7.Class21)this.object_0).struct3_0.long_0);
					IntPtr intPtr3 = new IntPtr(((Class7.Class21)((Class7.Class22)class18_0).object_0).struct3_0.long_0);
					*(long*)(void*)intPtr2 = intPtr3.ToInt64();
					return;
				}
				IntPtr intPtr4 = new IntPtr(((Class7.Class20)this.object_0).struct2_0.int_0);
				IntPtr intPtr5 = new IntPtr(((Class7.Class20)((Class7.Class22)class18_0).object_0).struct2_0.int_0);
				*(int*)(void*)intPtr4 = intPtr5.ToInt32();
				return;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_9(class18_0);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000378AC File Offset: 0x00035AAC
		public Class22(IntPtr intptr_0)
		{
			this.enum4_0 = (Class7.Enum4)3;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(intptr_0.ToInt64());
				this.enum1_0 = (Class7.Enum1)12;
				return;
			}
			this.object_0 = new Class7.Class20(intptr_0.ToInt32());
			this.enum1_0 = (Class7.Enum1)12;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00037904 File Offset: 0x00035B04
		public Class22(UIntPtr uintptr_0)
		{
			this.enum4_0 = (Class7.Enum4)3;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(uintptr_0.ToUInt64());
				this.enum1_0 = (Class7.Enum1)12;
				return;
			}
			this.object_0 = new Class7.Class20(uintptr_0.ToUInt32());
			this.enum1_0 = (Class7.Enum1)12;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0003795C File Offset: 0x00035B5C
		public Class22()
		{
			this.enum4_0 = (Class7.Enum4)3;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(0L);
				this.enum1_0 = (Class7.Enum1)12;
				return;
			}
			this.object_0 = new Class7.Class20(0);
			this.enum1_0 = (Class7.Enum1)12;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00004D9F File Offset: 0x00002F9F
		public override Class7.Class19 vmethod_71()
		{
			return new Class7.Class22
			{
				object_0 = this.object_0.vmethod_71(),
				enum1_0 = this.enum1_0
			};
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000379B0 File Offset: 0x00035BB0
		public Class22(long long_0)
		{
			this.enum4_0 = (Class7.Enum4)3;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(long_0);
				this.enum1_0 = (Class7.Enum1)12;
				return;
			}
			this.object_0 = new Class7.Class20((int)long_0);
			this.enum1_0 = (Class7.Enum1)12;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000379FC File Offset: 0x00035BFC
		public Class22(long long_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)3;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(long_0);
				this.enum1_0 = enum1_1;
				return;
			}
			this.object_0 = new Class7.Class20((int)long_0);
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00037A48 File Offset: 0x00035C48
		public Class22(ulong ulong_0)
		{
			this.enum4_0 = (Class7.Enum4)4;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(ulong_0);
				this.enum1_0 = (Class7.Enum1)13;
				return;
			}
			this.object_0 = new Class7.Class20((uint)ulong_0);
			this.enum1_0 = (Class7.Enum1)13;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00037A94 File Offset: 0x00035C94
		public Class22(ulong ulong_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)4;
			if (IntPtr.Size == 8)
			{
				this.object_0 = new Class7.Class21(ulong_0);
				this.enum1_0 = enum1_1;
				return;
			}
			this.object_0 = new Class7.Class20((uint)ulong_0);
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00004DC3 File Offset: 0x00002FC3
		public override bool vmethod_10()
		{
			return this.object_0.vmethod_10();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00004932 File Offset: 0x00002B32
		public override bool vmethod_11()
		{
			return !this.vmethod_10();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000493D File Offset: 0x00002B3D
		internal override bool vmethod_6()
		{
			return this.vmethod_11();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_1()
		{
			return true;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00037AE0 File Offset: 0x00035CE0
		public override Class7.Class18 vmethod_12(Class7.Enum1 enum1_1)
		{
			switch (enum1_1)
			{
			case (Class7.Enum1)1:
				return this.vmethod_14();
			case (Class7.Enum1)2:
				return this.vmethod_15();
			case (Class7.Enum1)3:
				return this.vmethod_16();
			case (Class7.Enum1)4:
				return this.vmethod_17();
			case (Class7.Enum1)5:
				return this.vmethod_18();
			case (Class7.Enum1)6:
				return this.vmethod_19();
			case (Class7.Enum1)7:
				return this.vmethod_20();
			case (Class7.Enum1)8:
				return this.vmethod_21();
			case (Class7.Enum1)11:
				return this.vmethod_13();
			case (Class7.Enum1)12:
				return this;
			case (Class7.Enum1)13:
				return this;
			case (Class7.Enum1)16:
				return this.vmethod_71();
			}
			throw new Exception(((Class7.Enum5)4).ToString());
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00037B98 File Offset: 0x00035D98
		internal IntPtr method_6()
		{
			if (IntPtr.Size == 8)
			{
				return new IntPtr(((Class7.Class21)this.object_0).struct3_0.long_0);
			}
			return new IntPtr(((Class7.Class20)this.object_0).struct2_0.int_0);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00037BE4 File Offset: 0x00035DE4
		internal override object vmethod_4(Type type_0)
		{
			if (type_0 != null && type_0.IsByRef)
			{
				type_0 = type_0.GetElementType();
			}
			if (!(type_0 == typeof(IntPtr)))
			{
				if (type_0 == typeof(UIntPtr))
				{
					if (IntPtr.Size == 8)
					{
						return new UIntPtr(((Class7.Class21)this.object_0).struct3_0.ulong_0);
					}
					return new UIntPtr(((Class7.Class20)this.object_0).struct2_0.uint_0);
				}
				else
				{
					if (!(type_0 == null) && !(type_0 == typeof(object)))
					{
						throw new Class7.Exception1();
					}
					if (IntPtr.Size == 8)
					{
						if (this.enum1_0 == (Class7.Enum1)12)
						{
							return new IntPtr(((Class7.Class21)this.object_0).struct3_0.long_0);
						}
						return new UIntPtr(((Class7.Class21)this.object_0).struct3_0.ulong_0);
					}
					else
					{
						if (this.enum1_0 == (Class7.Enum1)12)
						{
							return new IntPtr(((Class7.Class21)this.object_0).struct3_0.int_0);
						}
						return new UIntPtr(((Class7.Class20)this.object_0).struct2_0.uint_0);
					}
				}
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new IntPtr(((Class7.Class21)this.object_0).struct3_0.long_0);
				}
				return new IntPtr(((Class7.Class20)this.object_0).struct2_0.int_0);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public override Class7.Class20 vmethod_13()
		{
			return this.object_0.vmethod_13();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00004DDD File Offset: 0x00002FDD
		public override Class7.Class20 vmethod_14()
		{
			return this.object_0.vmethod_14();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00004DEA File Offset: 0x00002FEA
		public override Class7.Class20 vmethod_15()
		{
			return this.object_0.vmethod_15();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00004DF7 File Offset: 0x00002FF7
		public override Class7.Class20 vmethod_16()
		{
			return this.object_0.vmethod_16();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00004E04 File Offset: 0x00003004
		public override Class7.Class20 vmethod_17()
		{
			return this.object_0.vmethod_17();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00004E11 File Offset: 0x00003011
		public override Class7.Class20 vmethod_18()
		{
			return this.object_0.vmethod_18();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00004E1E File Offset: 0x0000301E
		public override Class7.Class20 vmethod_19()
		{
			return this.object_0.vmethod_19();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00004E2B File Offset: 0x0000302B
		public override Class7.Class21 vmethod_20()
		{
			return this.object_0.vmethod_20();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00004E38 File Offset: 0x00003038
		public override Class7.Class21 vmethod_21()
		{
			return this.object_0.vmethod_21();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000049F3 File Offset: 0x00002BF3
		public override Class7.Class20 vmethod_22()
		{
			return this.vmethod_14();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000049FB File Offset: 0x00002BFB
		public override Class7.Class20 vmethod_23()
		{
			return this.vmethod_16();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00004A03 File Offset: 0x00002C03
		public override Class7.Class20 vmethod_24()
		{
			return this.vmethod_18();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00004A0B File Offset: 0x00002C0B
		public override Class7.Class21 vmethod_25()
		{
			return this.vmethod_20();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00004A13 File Offset: 0x00002C13
		public override Class7.Class20 vmethod_26()
		{
			return this.vmethod_15();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00004A1B File Offset: 0x00002C1B
		public override Class7.Class20 vmethod_27()
		{
			return this.vmethod_17();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00004A23 File Offset: 0x00002C23
		public override Class7.Class20 vmethod_28()
		{
			return this.vmethod_19();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00004A2B File Offset: 0x00002C2B
		public override Class7.Class21 vmethod_29()
		{
			return this.vmethod_21();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00004E45 File Offset: 0x00003045
		public override Class7.Class20 vmethod_30()
		{
			return this.object_0.vmethod_30();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00004E52 File Offset: 0x00003052
		public override Class7.Class20 vmethod_31()
		{
			return this.object_0.vmethod_31();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00004E5F File Offset: 0x0000305F
		public override Class7.Class20 vmethod_32()
		{
			return this.object_0.vmethod_32();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00004E6C File Offset: 0x0000306C
		public override Class7.Class20 usfdqHavse()
		{
			return this.object_0.usfdqHavse();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00004E79 File Offset: 0x00003079
		public override Class7.Class20 vmethod_33()
		{
			return this.object_0.vmethod_33();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00004E86 File Offset: 0x00003086
		public override Class7.Class20 vmethod_34()
		{
			return this.object_0.vmethod_34();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00004E93 File Offset: 0x00003093
		public override Class7.Class21 vmethod_35()
		{
			return this.object_0.vmethod_35();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00004EA0 File Offset: 0x000030A0
		public override Class7.Class21 vmethod_36()
		{
			return this.object_0.vmethod_36();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00004EAD File Offset: 0x000030AD
		public override Class7.Class20 vmethod_37()
		{
			return this.object_0.vmethod_37();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00004EBA File Offset: 0x000030BA
		public override Class7.Class20 vmethod_38()
		{
			return this.object_0.vmethod_38();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00004EC7 File Offset: 0x000030C7
		public override Class7.Class20 vmethod_39()
		{
			return this.object_0.vmethod_39();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00004ED4 File Offset: 0x000030D4
		public override Class7.Class20 vmethod_40()
		{
			return this.object_0.vmethod_40();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00004EE1 File Offset: 0x000030E1
		public override Class7.Class20 vmethod_41()
		{
			return this.object_0.vmethod_41();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00004EEE File Offset: 0x000030EE
		public override Class7.Class20 vmethod_42()
		{
			return this.object_0.vmethod_42();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00004EFB File Offset: 0x000030FB
		public override Class7.Class21 vmethod_43()
		{
			return this.object_0.vmethod_43();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00004F08 File Offset: 0x00003108
		public override Class7.Class21 vmethod_44()
		{
			return this.object_0.vmethod_44();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00004F15 File Offset: 0x00003115
		public override Class7.Class23 vmethod_45()
		{
			return this.object_0.vmethod_45();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00004F22 File Offset: 0x00003122
		public override Class7.Class23 vmethod_46()
		{
			return this.object_0.vmethod_46();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00004F2F File Offset: 0x0000312F
		public override Class7.Class23 vmethod_47()
		{
			return this.object_0.vmethod_47();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000356EC File Offset: 0x000338EC
		public override Class7.Class22 vmethod_48()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_25().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_24().struct2_0.int_0);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00035730 File Offset: 0x00033930
		public override Class7.Class22 vmethod_49()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_29().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_28().struct2_0.uint_0);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00035774 File Offset: 0x00033974
		public override Class7.Class22 vmethod_50()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_35().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_33().struct2_0.int_0);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000357B8 File Offset: 0x000339B8
		public override Class7.Class22 vmethod_51()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_43().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_41().struct2_0.uint_0);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000357FC File Offset: 0x000339FC
		public override Class7.Class22 vmethod_52()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_36().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_34().struct2_0.int_0);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00035840 File Offset: 0x00033A40
		public override Class7.Class22 vmethod_53()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_44().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_42().struct2_0.uint_0);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00037D80 File Offset: 0x00035F80
		public override Class7.Class18 vmethod_54()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(-((Class7.Class21)this.object_0).struct3_0.long_0);
			}
			return new Class7.Class22((long)(-(long)((Class7.Class20)this.object_0).struct2_0.int_0));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00037DD0 File Offset: 0x00035FD0
		public override Class7.Class18 Add(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 + ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 + ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 + ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 + ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00037EC0 File Offset: 0x000360C0
		public override Class7.Class18 vmethod_55(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 + ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 + ((Class7.Class20)class18_0).struct2_0.int_0)));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 + ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 + ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0)));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00037FB0 File Offset: 0x000361B0
		public override Class7.Class18 vmethod_56(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.ulong_0 + ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0));
				}
				return new Class7.Class22((ulong)(checked(this.vmethod_18().struct2_0.uint_0 + ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0)));
			}
			else
			{
				checked
				{
					if (IntPtr.Size == 8)
					{
						return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 + unchecked((ulong)((Class7.Class20)class18_0).struct2_0.uint_0));
					}
				}
				return new Class7.Class22((long)((ulong)(checked(this.vmethod_18().struct2_0.uint_0 + ((Class7.Class20)class18_0).struct2_0.uint_0))));
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003809C File Offset: 0x0003629C
		public override Class7.Class18 vmethod_57(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 - ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 - ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 - ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 - ((Class7.Class20)class18_0).struct2_0.int_0));
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0003818C File Offset: 0x0003638C
		public Class7.Class18 method_7(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0 - this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class20)class18_0).struct2_0.int_0 - this.vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0 - this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0 - this.vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003827C File Offset: 0x0003647C
		public override Class7.Class18 vmethod_58(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 - ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 - ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0)));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 - ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 - ((Class7.Class20)class18_0).struct2_0.int_0)));
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0003836C File Offset: 0x0003656C
		public Class7.Class18 method_8(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0 - this.vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0 - this.vmethod_18().struct2_0.int_0)));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0 - this.vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(((Class7.Class20)class18_0).struct2_0.int_0 - this.vmethod_18().struct2_0.int_0)));
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0003845C File Offset: 0x0003665C
		public override Class7.Class18 vmethod_59(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.ulong_0 - ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0));
				}
				return new Class7.Class22((ulong)(checked(this.vmethod_18().struct2_0.uint_0 - ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0)));
			}
			else
			{
				checked
				{
					if (IntPtr.Size == 8)
					{
						return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 - unchecked((ulong)((Class7.Class20)class18_0).struct2_0.uint_0));
					}
				}
				return new Class7.Class22((long)((ulong)(checked(this.vmethod_18().struct2_0.uint_0 - ((Class7.Class20)class18_0).struct2_0.uint_0))));
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00038548 File Offset: 0x00036748
		public Class7.Class18 method_9(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0 - this.vmethod_20().struct3_0.ulong_0));
				}
				return new Class7.Class22((ulong)(checked(((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0 - this.vmethod_18().struct2_0.uint_0)));
			}
			else
			{
				checked
				{
					if (IntPtr.Size == 8)
					{
						return new Class7.Class22(unchecked((ulong)((Class7.Class20)class18_0).struct2_0.uint_0) - this.vmethod_20().struct3_0.ulong_0);
					}
				}
				return new Class7.Class22((long)((ulong)(checked(((Class7.Class20)class18_0).struct2_0.uint_0 - this.vmethod_18().struct2_0.uint_0))));
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00038634 File Offset: 0x00036834
		public override Class7.Class18 vmethod_60(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 * ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 * ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 * ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 * ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00038724 File Offset: 0x00036924
		public override Class7.Class18 vmethod_61(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 * ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 * ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0)));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.long_0 * ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0));
				}
				return new Class7.Class22((long)(checked(this.vmethod_18().struct2_0.int_0 * ((Class7.Class20)class18_0).struct2_0.int_0)));
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00038814 File Offset: 0x00036A14
		public override Class7.Class18 vmethod_62(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(checked(this.vmethod_20().struct3_0.ulong_0 * ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0));
				}
				return new Class7.Class22((ulong)(checked(this.vmethod_18().struct2_0.uint_0 * ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0)));
			}
			else
			{
				checked
				{
					if (IntPtr.Size == 8)
					{
						return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 * unchecked((ulong)((Class7.Class20)class18_0).struct2_0.uint_0));
					}
				}
				return new Class7.Class22((long)((ulong)(checked(this.vmethod_18().struct2_0.uint_0 * ((Class7.Class20)class18_0).struct2_0.uint_0))));
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00038900 File Offset: 0x00036B00
		public override Class7.Class18 vmethod_63(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 / ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 / ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 / ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 / ((Class7.Class20)class18_0).struct2_0.int_0));
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000389F0 File Offset: 0x00036BF0
		public Class7.Class18 method_10(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0 / this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class20)class18_0).struct2_0.int_0 / this.vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0 / this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0 / this.vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00038AE0 File Offset: 0x00036CE0
		public override Class7.Class18 vmethod_64(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 / ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((ulong)(this.vmethod_18().struct2_0.uint_0 / ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 / ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((long)((ulong)(this.vmethod_18().struct2_0.uint_0 / ((Class7.Class20)class18_0).struct2_0.uint_0)));
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00038BD0 File Offset: 0x00036DD0
		public Class7.Class18 method_11(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0 / this.vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((ulong)(((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0 / this.vmethod_18().struct2_0.uint_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0 / this.vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((long)((ulong)(((Class7.Class20)class18_0).struct2_0.uint_0 / this.vmethod_18().struct2_0.uint_0)));
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00038CC0 File Offset: 0x00036EC0
		public override Class7.Class18 vmethod_65(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 % ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 % ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 % ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 % ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00038DB0 File Offset: 0x00036FB0
		public Class7.Class18 method_12(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0 % this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0 % this.vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0 % this.vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(((Class7.Class20)class18_0).struct2_0.int_0 % this.vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00038EA0 File Offset: 0x000370A0
		public override Class7.Class18 vmethod_66(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 % ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((ulong)(this.vmethod_18().struct2_0.uint_0 % ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 % ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((long)((ulong)(this.vmethod_18().struct2_0.uint_0 % ((Class7.Class20)class18_0).struct2_0.uint_0)));
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00038F90 File Offset: 0x00037190
		public Class7.Class18 yIwByEsukh(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0 % this.vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((ulong)(((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0 % this.vmethod_18().struct2_0.uint_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0 % this.vmethod_20().struct3_0.ulong_0);
				}
				return new Class7.Class22((long)((ulong)(((Class7.Class20)class18_0).struct2_0.uint_0 % this.vmethod_18().struct2_0.uint_0)));
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00039080 File Offset: 0x00037280
		public override Class7.Class18 vmethod_67(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 & ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 & ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 & ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 & ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00039170 File Offset: 0x00037370
		public override Class7.Class18 vmethod_68(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 | ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 | ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 | ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 | ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00039260 File Offset: 0x00037460
		public override Class7.Class18 vmethod_69()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(~this.vmethod_20().struct3_0.long_0);
			}
			return new Class7.Class22((long)(~(long)this.vmethod_18().struct2_0.int_0));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000392A4 File Offset: 0x000374A4
		public override Class7.Class18 vmethod_70(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 ^ ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 ^ ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 ^ ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 ^ ((Class7.Class20)class18_0).struct2_0.int_0));
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00039394 File Offset: 0x00037594
		public override Class7.Class18 vmethod_72(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 << ((Class7.Class20)class18_0).struct2_0.int_0);
				}
				return new Class7.Class22((long)((long)this.vmethod_18().struct2_0.int_0 << ((Class7.Class20)class18_0).struct2_0.int_0));
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 << ((Class7.Class22)class18_0).vmethod_20().struct3_0.int_0);
				}
				return new Class7.Class22((long)((long)this.vmethod_18().struct2_0.int_0 << ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003948C File Offset: 0x0003768C
		public override Class7.Class18 vmethod_73(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 >> ((Class7.Class22)class18_0).vmethod_20().struct3_0.int_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 >> ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.long_0 >> ((Class7.Class20)class18_0).struct2_0.int_0);
				}
				return new Class7.Class22((long)(this.vmethod_18().struct2_0.int_0 >> ((Class7.Class20)class18_0).struct2_0.int_0));
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00039584 File Offset: 0x00037784
		public override Class7.Class18 vmethod_74(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 >> ((Class7.Class22)class18_0).vmethod_20().struct3_0.int_0);
				}
				return new Class7.Class22((long)((ulong)(this.vmethod_18().struct2_0.uint_0 >> ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0)));
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return new Class7.Class22(this.vmethod_20().struct3_0.ulong_0 >> ((Class7.Class20)class18_0).struct2_0.int_0);
				}
				return new Class7.Class22((long)((ulong)(this.vmethod_18().struct2_0.uint_0 >> ((Class7.Class20)class18_0).struct2_0.int_0)));
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00004F3C File Offset: 0x0000313C
		public Class7.Class18 method_13(Class7.Class20 class20_0)
		{
			return new Class7.Class22((long)((ulong)(class20_0.struct2_0.uint_0 >> this.vmethod_18().struct2_0.int_0)));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00004F63 File Offset: 0x00003163
		public Class7.Class18 method_14(Class7.Class20 class20_0)
		{
			return new Class7.Class22((long)(class20_0.struct2_0.int_0 >> this.vmethod_20().struct3_0.int_0));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00004F8A File Offset: 0x0000318A
		public Class7.Class18 method_15(Class7.Class20 class20_0)
		{
			return new Class7.Class22((long)((long)class20_0.struct2_0.int_0 << this.vmethod_20().struct3_0.int_0));
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00004FB1 File Offset: 0x000031B1
		public override string ToString()
		{
			return this.object_0.ToString();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00004B70 File Offset: 0x00002D70
		internal override Class7.Class18 vmethod_7()
		{
			return this;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_8()
		{
			return true;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0003967C File Offset: 0x0003787C
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).vmethod_5(this);
			}
			Class7.Class18 @class = class18_0.vmethod_7();
			if (!@class.vmethod_8())
			{
				return false;
			}
			if (!@class.method_1())
			{
				if (@class.method_2())
				{
					int size = IntPtr.Size;
					return this.vmethod_20().struct3_0.long_0 == ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0;
				}
				return false;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 == ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 == ((Class7.Class20)class18_0).struct2_0.int_0;
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00039758 File Offset: 0x00037958
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).BeouTiljCp(this);
			}
			Class7.Class18 @class = class18_0.vmethod_7();
			if (!@class.vmethod_8())
			{
				return false;
			}
			if (@class.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 != ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 != ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			else
			{
				if (!@class.method_2())
				{
					return false;
				}
				int size = IntPtr.Size;
				return this.vmethod_20().struct3_0.ulong_0 != ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0003983C File Offset: 0x00037A3C
		public override bool vmethod_75(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 >= ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 >= ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 >= ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 >= ((Class7.Class20)class18_0).struct2_0.int_0;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00039928 File Offset: 0x00037B28
		public override bool vmethod_76(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 >= ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 >= ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 >= ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 >= ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00039A14 File Offset: 0x00037C14
		public override bool vmethod_77(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 > ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 > ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 > ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 > ((Class7.Class20)class18_0).struct2_0.int_0;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00039AF4 File Offset: 0x00037CF4
		public override bool lwlumgaheq(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (class18_0.method_1())
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 > ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 > ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
			else
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 > ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 > ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0;
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public override bool vmethod_78(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 <= ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 <= ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 <= ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 <= ((Class7.Class20)class18_0).struct2_0.int_0;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00039CC0 File Offset: 0x00037EC0
		public override bool vmethod_79(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 <= ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 <= ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 <= ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 <= ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00039DAC File Offset: 0x00037FAC
		public override bool vmethod_80(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 < ((Class7.Class22)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 < ((Class7.Class22)class18_0).vmethod_18().struct2_0.int_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.long_0 < ((Class7.Class20)class18_0).vmethod_20().struct3_0.long_0;
				}
				return this.vmethod_18().struct2_0.int_0 < ((Class7.Class20)class18_0).struct2_0.int_0;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00039E8C File Offset: 0x0003808C
		public override bool vmethod_81(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_1())
			{
				if (!class18_0.method_2())
				{
					throw new Class7.Exception1();
				}
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 < ((Class7.Class22)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 < ((Class7.Class22)class18_0).vmethod_18().struct2_0.uint_0;
			}
			else
			{
				if (IntPtr.Size == 8)
				{
					return this.vmethod_20().struct3_0.ulong_0 < ((Class7.Class20)class18_0).vmethod_20().struct3_0.ulong_0;
				}
				return this.vmethod_18().struct2_0.uint_0 < ((Class7.Class20)class18_0).struct2_0.uint_0;
			}
		}

		// Token: 0x04000694 RID: 1684
		public object object_0;

		// Token: 0x04000695 RID: 1685
		public Class7.Enum1 enum1_0;
	}

	// Token: 0x02000164 RID: 356
	private abstract class Class19 : Class7.Class18
	{
		// Token: 0x060008BF RID: 2239
		public abstract bool vmethod_10();

		// Token: 0x060008C0 RID: 2240
		public abstract bool vmethod_11();

		// Token: 0x060008C1 RID: 2241
		public abstract Class7.Class18 vmethod_12(Class7.Enum1 enum1_0);

		// Token: 0x060008C2 RID: 2242
		public abstract Class7.Class20 vmethod_13();

		// Token: 0x060008C3 RID: 2243
		public abstract Class7.Class20 vmethod_14();

		// Token: 0x060008C4 RID: 2244
		public abstract Class7.Class20 vmethod_15();

		// Token: 0x060008C5 RID: 2245
		public abstract Class7.Class20 vmethod_16();

		// Token: 0x060008C6 RID: 2246
		public abstract Class7.Class20 vmethod_17();

		// Token: 0x060008C7 RID: 2247
		public abstract Class7.Class20 vmethod_18();

		// Token: 0x060008C8 RID: 2248
		public abstract Class7.Class20 vmethod_19();

		// Token: 0x060008C9 RID: 2249
		public abstract Class7.Class21 vmethod_20();

		// Token: 0x060008CA RID: 2250
		public abstract Class7.Class21 vmethod_21();

		// Token: 0x060008CB RID: 2251
		public abstract Class7.Class20 vmethod_22();

		// Token: 0x060008CC RID: 2252
		public abstract Class7.Class20 vmethod_23();

		// Token: 0x060008CD RID: 2253
		public abstract Class7.Class20 vmethod_24();

		// Token: 0x060008CE RID: 2254
		public abstract Class7.Class21 vmethod_25();

		// Token: 0x060008CF RID: 2255
		public abstract Class7.Class20 vmethod_26();

		// Token: 0x060008D0 RID: 2256
		public abstract Class7.Class20 vmethod_27();

		// Token: 0x060008D1 RID: 2257
		public abstract Class7.Class20 vmethod_28();

		// Token: 0x060008D2 RID: 2258
		public abstract Class7.Class21 vmethod_29();

		// Token: 0x060008D3 RID: 2259
		public abstract Class7.Class20 vmethod_30();

		// Token: 0x060008D4 RID: 2260
		public abstract Class7.Class20 vmethod_31();

		// Token: 0x060008D5 RID: 2261
		public abstract Class7.Class20 vmethod_32();

		// Token: 0x060008D6 RID: 2262
		public abstract Class7.Class20 usfdqHavse();

		// Token: 0x060008D7 RID: 2263
		public abstract Class7.Class20 vmethod_33();

		// Token: 0x060008D8 RID: 2264
		public abstract Class7.Class20 vmethod_34();

		// Token: 0x060008D9 RID: 2265
		public abstract Class7.Class21 vmethod_35();

		// Token: 0x060008DA RID: 2266
		public abstract Class7.Class21 vmethod_36();

		// Token: 0x060008DB RID: 2267
		public abstract Class7.Class20 vmethod_37();

		// Token: 0x060008DC RID: 2268
		public abstract Class7.Class20 vmethod_38();

		// Token: 0x060008DD RID: 2269
		public abstract Class7.Class20 vmethod_39();

		// Token: 0x060008DE RID: 2270
		public abstract Class7.Class20 vmethod_40();

		// Token: 0x060008DF RID: 2271
		public abstract Class7.Class20 vmethod_41();

		// Token: 0x060008E0 RID: 2272
		public abstract Class7.Class20 vmethod_42();

		// Token: 0x060008E1 RID: 2273
		public abstract Class7.Class21 vmethod_43();

		// Token: 0x060008E2 RID: 2274
		public abstract Class7.Class21 vmethod_44();

		// Token: 0x060008E3 RID: 2275
		public abstract Class7.Class23 vmethod_45();

		// Token: 0x060008E4 RID: 2276
		public abstract Class7.Class23 vmethod_46();

		// Token: 0x060008E5 RID: 2277
		public abstract Class7.Class23 vmethod_47();

		// Token: 0x060008E6 RID: 2278
		public abstract Class7.Class22 vmethod_48();

		// Token: 0x060008E7 RID: 2279
		public abstract Class7.Class22 vmethod_49();

		// Token: 0x060008E8 RID: 2280
		public abstract Class7.Class22 vmethod_50();

		// Token: 0x060008E9 RID: 2281
		public abstract Class7.Class22 vmethod_51();

		// Token: 0x060008EA RID: 2282
		public abstract Class7.Class22 vmethod_52();

		// Token: 0x060008EB RID: 2283
		public abstract Class7.Class22 vmethod_53();

		// Token: 0x060008EC RID: 2284
		public abstract Class7.Class18 vmethod_54();

		// Token: 0x060008ED RID: 2285
		public abstract Class7.Class18 Add(Class7.Class18 class18_0);

		// Token: 0x060008EE RID: 2286
		public abstract Class7.Class18 vmethod_55(Class7.Class18 class18_0);

		// Token: 0x060008EF RID: 2287
		public abstract Class7.Class18 vmethod_56(Class7.Class18 class18_0);

		// Token: 0x060008F0 RID: 2288
		public abstract Class7.Class18 vmethod_57(Class7.Class18 class18_0);

		// Token: 0x060008F1 RID: 2289
		public abstract Class7.Class18 vmethod_58(Class7.Class18 class18_0);

		// Token: 0x060008F2 RID: 2290
		public abstract Class7.Class18 vmethod_59(Class7.Class18 class18_0);

		// Token: 0x060008F3 RID: 2291
		public abstract Class7.Class18 vmethod_60(Class7.Class18 class18_0);

		// Token: 0x060008F4 RID: 2292
		public abstract Class7.Class18 vmethod_61(Class7.Class18 class18_0);

		// Token: 0x060008F5 RID: 2293
		public abstract Class7.Class18 vmethod_62(Class7.Class18 class18_0);

		// Token: 0x060008F6 RID: 2294
		public abstract Class7.Class18 vmethod_63(Class7.Class18 class18_0);

		// Token: 0x060008F7 RID: 2295
		public abstract Class7.Class18 vmethod_64(Class7.Class18 class18_0);

		// Token: 0x060008F8 RID: 2296
		public abstract Class7.Class18 vmethod_65(Class7.Class18 class18_0);

		// Token: 0x060008F9 RID: 2297
		public abstract Class7.Class18 vmethod_66(Class7.Class18 class18_0);

		// Token: 0x060008FA RID: 2298
		public abstract Class7.Class18 vmethod_67(Class7.Class18 class18_0);

		// Token: 0x060008FB RID: 2299
		public abstract Class7.Class18 vmethod_68(Class7.Class18 class18_0);

		// Token: 0x060008FC RID: 2300
		public abstract Class7.Class18 vmethod_69();

		// Token: 0x060008FD RID: 2301
		public abstract Class7.Class18 vmethod_70(Class7.Class18 class18_0);

		// Token: 0x060008FE RID: 2302
		public abstract Class7.Class19 vmethod_71();

		// Token: 0x060008FF RID: 2303
		public abstract Class7.Class18 vmethod_72(Class7.Class18 class18_0);

		// Token: 0x06000900 RID: 2304
		public abstract Class7.Class18 vmethod_73(Class7.Class18 class18_0);

		// Token: 0x06000901 RID: 2305
		public abstract Class7.Class18 vmethod_74(Class7.Class18 class18_0);

		// Token: 0x06000902 RID: 2306
		public abstract bool vmethod_75(Class7.Class18 class18_0);

		// Token: 0x06000903 RID: 2307
		public abstract bool vmethod_76(Class7.Class18 class18_0);

		// Token: 0x06000904 RID: 2308
		public abstract bool vmethod_77(Class7.Class18 class18_0);

		// Token: 0x06000905 RID: 2309
		public abstract bool lwlumgaheq(Class7.Class18 class18_0);

		// Token: 0x06000906 RID: 2310
		public abstract bool vmethod_78(Class7.Class18 class18_0);

		// Token: 0x06000907 RID: 2311
		public abstract bool vmethod_79(Class7.Class18 class18_0);

		// Token: 0x06000908 RID: 2312
		public abstract bool vmethod_80(Class7.Class18 class18_0);

		// Token: 0x06000909 RID: 2313
		public abstract bool vmethod_81(Class7.Class18 class18_0);

		// Token: 0x0600090A RID: 2314 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_3()
		{
			return true;
		}
	}

	// Token: 0x02000165 RID: 357
	private class Class23 : Class7.Class19
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00039F80 File Offset: 0x00038180
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			this.double_0 = ((Class7.Class23)class18_0).double_0;
			this.enum1_0 = ((Class7.Class23)class18_0).enum1_0;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_9(class18_0);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00039FB0 File Offset: 0x000381B0
		public Class23(double double_1)
		{
			this.enum4_0 = (Class7.Enum4)5;
			this.enum1_0 = (Class7.Enum1)10;
			this.double_0 = double_1;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00039FDC File Offset: 0x000381DC
		public Class23(Class7.Class23 class23_0)
		{
			this.enum4_0 = class23_0.enum4_0;
			this.enum1_0 = class23_0.enum1_0;
			this.double_0 = class23_0.double_0;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00004FBE File Offset: 0x000031BE
		public override Class7.Class19 vmethod_71()
		{
			return new Class7.Class23(this);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003A014 File Offset: 0x00038214
		public Class23(double double_1, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)5;
			this.double_0 = double_1;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0003A03C File Offset: 0x0003823C
		public Class23(float float_0)
		{
			this.enum4_0 = (Class7.Enum4)5;
			this.double_0 = (double)float_0;
			this.enum1_0 = (Class7.Enum1)9;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003A068 File Offset: 0x00038268
		public Class23(float float_0, Class7.Enum1 enum1_1)
		{
			this.enum4_0 = (Class7.Enum4)5;
			this.double_0 = (double)float_0;
			this.enum1_0 = enum1_1;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00004FC6 File Offset: 0x000031C6
		public override bool vmethod_10()
		{
			return this.double_0 == 0.0;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00004932 File Offset: 0x00002B32
		public override bool vmethod_11()
		{
			return !this.vmethod_10();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00004FD9 File Offset: 0x000031D9
		public override string ToString()
		{
			return this.double_0.ToString();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003A094 File Offset: 0x00038294
		public override Class7.Class18 vmethod_12(Class7.Enum1 enum1_1)
		{
			switch (enum1_1)
			{
			case (Class7.Enum1)1:
				return this.vmethod_14();
			case (Class7.Enum1)2:
				return this.vmethod_15();
			case (Class7.Enum1)3:
				return this.vmethod_16();
			case (Class7.Enum1)4:
				return this.vmethod_17();
			case (Class7.Enum1)5:
				return this.vmethod_18();
			case (Class7.Enum1)6:
				return this.vmethod_19();
			case (Class7.Enum1)7:
				return this.vmethod_20();
			case (Class7.Enum1)8:
				return this.vmethod_21();
			case (Class7.Enum1)9:
				return this.vmethod_45();
			case (Class7.Enum1)10:
				return this.vmethod_46();
			case (Class7.Enum1)11:
				return this.vmethod_13();
			default:
				throw new Exception(((Class7.Enum5)4).ToString());
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003A138 File Offset: 0x00038338
		internal override object vmethod_4(Type type_0)
		{
			if (type_0 != null && type_0.IsByRef)
			{
				type_0 = type_0.GetElementType();
			}
			if (type_0 == typeof(float))
			{
				return (float)this.double_0;
			}
			if (type_0 == typeof(double))
			{
				return this.double_0;
			}
			if ((type_0 == null || type_0 == typeof(object)) && this.enum1_0 == (Class7.Enum1)9)
			{
				return (float)this.double_0;
			}
			return this.double_0;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0003A1D8 File Offset: 0x000383D8
		public override Class7.Class20 vmethod_13()
		{
			return new Class7.Class20(this.vmethod_10() ? 1 : 0);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000493D File Offset: 0x00002B3D
		internal override bool vmethod_6()
		{
			return this.vmethod_11();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00004FE6 File Offset: 0x000031E6
		public override Class7.Class20 vmethod_14()
		{
			return new Class7.Class20((int)((sbyte)this.double_0), (Class7.Enum1)1);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00004FF5 File Offset: 0x000031F5
		public override Class7.Class20 vmethod_15()
		{
			return new Class7.Class20((uint)((byte)this.double_0), (Class7.Enum1)2);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00005004 File Offset: 0x00003204
		public override Class7.Class20 vmethod_16()
		{
			return new Class7.Class20((int)((short)this.double_0), (Class7.Enum1)3);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00005013 File Offset: 0x00003213
		public override Class7.Class20 vmethod_17()
		{
			return new Class7.Class20((uint)((ushort)this.double_0), (Class7.Enum1)4);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00005022 File Offset: 0x00003222
		public override Class7.Class20 vmethod_18()
		{
			return new Class7.Class20((int)this.double_0, (Class7.Enum1)5);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00005031 File Offset: 0x00003231
		public override Class7.Class20 vmethod_19()
		{
			return new Class7.Class20((uint)this.double_0, (Class7.Enum1)6);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00005040 File Offset: 0x00003240
		public override Class7.Class21 vmethod_20()
		{
			return new Class7.Class21((long)this.double_0, (Class7.Enum1)7);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000504F File Offset: 0x0000324F
		public override Class7.Class21 vmethod_21()
		{
			return new Class7.Class21((ulong)this.double_0, (Class7.Enum1)8);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000049F3 File Offset: 0x00002BF3
		public override Class7.Class20 vmethod_22()
		{
			return this.vmethod_14();
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000049FB File Offset: 0x00002BFB
		public override Class7.Class20 vmethod_23()
		{
			return this.vmethod_16();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00004A03 File Offset: 0x00002C03
		public override Class7.Class20 vmethod_24()
		{
			return this.vmethod_18();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00004A0B File Offset: 0x00002C0B
		public override Class7.Class21 vmethod_25()
		{
			return this.vmethod_20();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00004A13 File Offset: 0x00002C13
		public override Class7.Class20 vmethod_26()
		{
			return this.vmethod_15();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00004A1B File Offset: 0x00002C1B
		public override Class7.Class20 vmethod_27()
		{
			return this.vmethod_17();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00004A23 File Offset: 0x00002C23
		public override Class7.Class20 vmethod_28()
		{
			return this.vmethod_19();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00004A2B File Offset: 0x00002C2B
		public override Class7.Class21 vmethod_29()
		{
			return this.vmethod_21();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000505E File Offset: 0x0000325E
		public override Class7.Class20 vmethod_30()
		{
			return new Class7.Class20((int)(checked((sbyte)this.double_0)), (Class7.Enum1)1);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000505E File Offset: 0x0000325E
		public override Class7.Class20 vmethod_31()
		{
			return new Class7.Class20((int)(checked((sbyte)this.double_0)), (Class7.Enum1)1);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000506D File Offset: 0x0000326D
		public override Class7.Class20 vmethod_32()
		{
			return new Class7.Class20((int)(checked((short)this.double_0)), (Class7.Enum1)3);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0000506D File Offset: 0x0000326D
		public override Class7.Class20 usfdqHavse()
		{
			return new Class7.Class20((int)(checked((short)this.double_0)), (Class7.Enum1)3);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000507C File Offset: 0x0000327C
		public override Class7.Class20 vmethod_33()
		{
			return new Class7.Class20(checked((int)this.double_0), (Class7.Enum1)5);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000507C File Offset: 0x0000327C
		public override Class7.Class20 vmethod_34()
		{
			return new Class7.Class20(checked((int)this.double_0), (Class7.Enum1)5);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000508B File Offset: 0x0000328B
		public override Class7.Class21 vmethod_35()
		{
			return new Class7.Class21(checked((long)this.double_0), (Class7.Enum1)7);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000508B File Offset: 0x0000328B
		public override Class7.Class21 vmethod_36()
		{
			return new Class7.Class21(checked((long)this.double_0), (Class7.Enum1)7);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000509A File Offset: 0x0000329A
		public override Class7.Class20 vmethod_37()
		{
			return new Class7.Class20((int)(checked((byte)this.double_0)), (Class7.Enum1)2);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000509A File Offset: 0x0000329A
		public override Class7.Class20 vmethod_38()
		{
			return new Class7.Class20((int)(checked((byte)this.double_0)), (Class7.Enum1)2);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000050A9 File Offset: 0x000032A9
		public override Class7.Class20 vmethod_39()
		{
			return new Class7.Class20((int)(checked((ushort)this.double_0)), (Class7.Enum1)4);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000050A9 File Offset: 0x000032A9
		public override Class7.Class20 vmethod_40()
		{
			return new Class7.Class20((int)(checked((ushort)this.double_0)), (Class7.Enum1)4);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000050B8 File Offset: 0x000032B8
		public override Class7.Class20 vmethod_41()
		{
			return new Class7.Class20(checked((uint)this.double_0), (Class7.Enum1)6);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000050B8 File Offset: 0x000032B8
		public override Class7.Class20 vmethod_42()
		{
			return new Class7.Class20(checked((uint)this.double_0), (Class7.Enum1)6);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000050C7 File Offset: 0x000032C7
		public override Class7.Class21 vmethod_43()
		{
			return new Class7.Class21(checked((ulong)this.double_0), (Class7.Enum1)8);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000050C7 File Offset: 0x000032C7
		public override Class7.Class21 vmethod_44()
		{
			return new Class7.Class21(checked((ulong)this.double_0), (Class7.Enum1)8);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000050D6 File Offset: 0x000032D6
		public override Class7.Class23 vmethod_45()
		{
			return new Class7.Class23((float)this.double_0, (Class7.Enum1)9);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000050E6 File Offset: 0x000032E6
		public override Class7.Class23 vmethod_46()
		{
			return new Class7.Class23(this.double_0, (Class7.Enum1)10);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000050F6 File Offset: 0x000032F6
		public override Class7.Class23 vmethod_47()
		{
			return new Class7.Class23(this.double_0);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000356EC File Offset: 0x000338EC
		public override Class7.Class22 vmethod_48()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_25().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_24().struct2_0.int_0);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00035730 File Offset: 0x00033930
		public override Class7.Class22 vmethod_49()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_29().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_28().struct2_0.uint_0);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00035774 File Offset: 0x00033974
		public override Class7.Class22 vmethod_50()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_35().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_33().struct2_0.int_0);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000357B8 File Offset: 0x000339B8
		public override Class7.Class22 vmethod_51()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_43().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_41().struct2_0.uint_0);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000357FC File Offset: 0x000339FC
		public override Class7.Class22 vmethod_52()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_36().struct3_0.long_0);
			}
			return new Class7.Class22((long)this.vmethod_34().struct2_0.int_0);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00035840 File Offset: 0x00033A40
		public override Class7.Class22 vmethod_53()
		{
			if (IntPtr.Size == 8)
			{
				return new Class7.Class22(this.vmethod_44().struct3_0.ulong_0);
			}
			return new Class7.Class22((ulong)this.vmethod_42().struct2_0.uint_0);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0003A1F8 File Offset: 0x000383F8
		public override Class7.Class18 vmethod_54()
		{
			if (this.enum1_0 == (Class7.Enum1)9)
			{
				return new Class7.Class23((float)(-(float)this.double_0));
			}
			return new Class7.Class23(-this.double_0);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0003A22C File Offset: 0x0003842C
		public override Class7.Class18 Add(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 + ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0003A22C File Offset: 0x0003842C
		public override Class7.Class18 vmethod_55(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 + ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0003A22C File Offset: 0x0003842C
		public override Class7.Class18 vmethod_56(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 + ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0003A270 File Offset: 0x00038470
		public override Class7.Class18 vmethod_57(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 - ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003A270 File Offset: 0x00038470
		public override Class7.Class18 vmethod_58(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 - ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003A270 File Offset: 0x00038470
		public override Class7.Class18 vmethod_59(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 - ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0003A2B4 File Offset: 0x000384B4
		public override Class7.Class18 vmethod_60(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4() || !class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 * ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0003A300 File Offset: 0x00038500
		public override Class7.Class18 vmethod_61(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 * ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003A300 File Offset: 0x00038500
		public override Class7.Class18 vmethod_62(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 * ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003A344 File Offset: 0x00038544
		public override Class7.Class18 vmethod_63(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 / ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003A344 File Offset: 0x00038544
		public override Class7.Class18 vmethod_64(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 / ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003A388 File Offset: 0x00038588
		public override Class7.Class18 vmethod_65(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 % ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003A388 File Offset: 0x00038588
		public override Class7.Class18 vmethod_66(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return new Class7.Class23(this.double_0 % ((Class7.Class23)class18_0).double_0);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_67(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_68(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_69()
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_70(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_72(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_73(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00005104 File Offset: 0x00003304
		public override Class7.Class18 vmethod_74(Class7.Class18 class18_0)
		{
			throw new Class7.Exception1();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00004B70 File Offset: 0x00002D70
		internal override Class7.Class18 vmethod_7()
		{
			return this;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0003A3CC File Offset: 0x000385CC
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).vmethod_5(this);
			}
			Class7.Class18 @class = class18_0.vmethod_7();
			return @class.method_4() && this.double_0 == ((Class7.Class23)@class).double_0;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0003A420 File Offset: 0x00038620
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (class18_0.method_0())
			{
				return false;
			}
			if (!class18_0.vmethod_0())
			{
				Class7.Class18 @class = class18_0.vmethod_7();
				return @class.method_4() && this.double_0 != ((Class7.Class23)@class).double_0;
			}
			return ((Class7.Class24)class18_0).BeouTiljCp(this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0003A478 File Offset: 0x00038678
		public override bool vmethod_75(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 >= ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0003A478 File Offset: 0x00038678
		public override bool vmethod_76(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 >= ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0003A4BC File Offset: 0x000386BC
		public override bool vmethod_77(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 > ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0003A4BC File Offset: 0x000386BC
		public override bool lwlumgaheq(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 > ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003A4FC File Offset: 0x000386FC
		public override bool vmethod_78(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 <= ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0003A4FC File Offset: 0x000386FC
		public override bool vmethod_79(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 <= ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0003A540 File Offset: 0x00038740
		public override bool vmethod_80(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 < ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003A540 File Offset: 0x00038740
		public override bool vmethod_81(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				class18_0 = class18_0.vmethod_7();
			}
			if (!class18_0.method_4())
			{
				throw new Class7.Exception1();
			}
			return this.double_0 < ((Class7.Class23)class18_0).double_0;
		}

		// Token: 0x04000696 RID: 1686
		public double double_0;

		// Token: 0x04000697 RID: 1687
		public Class7.Enum1 enum1_0;
	}

	// Token: 0x02000166 RID: 358
	internal enum Enum1 : byte
	{

	}

	// Token: 0x02000167 RID: 359
	internal enum Enum2 : byte
	{

	}

	// Token: 0x02000168 RID: 360
	private class Exception0 : Exception
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x0003A580 File Offset: 0x00038780
		public Exception0(string string_0)
			: base(string_0)
		{
		}
	}

	// Token: 0x02000169 RID: 361
	private class Exception1 : Exception
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0003A594 File Offset: 0x00038794
		public Exception1()
		{
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0003A580 File Offset: 0x00038780
		public Exception1(string string_0)
			: base(string_0)
		{
		}
	}

	// Token: 0x0200016A RID: 362
	internal class Class8
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x0003A5A8 File Offset: 0x000387A8
		public override string ToString()
		{
			object obj = this.enum3_0;
			if (this.object_0 != null)
			{
				return obj.ToString() + 'H'.ToString() + this.object_0.ToString();
			}
			return obj.ToString();
		}

		// Token: 0x0400069A RID: 1690
		internal Class7.Enum3 enum3_0 = (Class7.Enum3)126;

		// Token: 0x0400069B RID: 1691
		internal object object_0;
	}

	// Token: 0x0200016B RID: 363
	internal abstract class Class24 : Class7.Class18
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00039F6C File Offset: 0x0003816C
		public Class24()
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_0()
		{
			return true;
		}

		// Token: 0x0600096B RID: 2411
		internal abstract IntPtr vmethod_10();

		// Token: 0x0600096C RID: 2412
		internal abstract void vmethod_11(Class7.Class18 class18_0);

		// Token: 0x0600096D RID: 2413 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal override bool vmethod_1()
		{
			return true;
		}
	}

	// Token: 0x0200016C RID: 364
	internal class Class25 : Class7.Class24
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0003A610 File Offset: 0x00038810
		public Class25(int int_1, Class7.Class16 class16_1)
		{
			this.class16_0 = class16_1;
			this.int_0 = int_1;
			this.enum4_0 = (Class7.Enum4)7;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0003A638 File Offset: 0x00038838
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			if (class18_0 is Class7.Class25)
			{
				this.class16_0 = ((Class7.Class25)class18_0).class16_0;
				this.int_0 = ((Class7.Class25)class18_0).int_0;
				return;
			}
			Class7.Class10 @class = this.class16_0.class13_0.fBcjhfhonR[this.int_0];
			if (class18_0 is Class7.Class24 && (@class.enum1_0 & (Class7.Enum1)226) > (Class7.Enum1)0)
			{
				Class7.Class18 class2 = (class18_0 as Class7.Class24).vmethod_7();
				this.vmethod_11(class2);
				return;
			}
			this.vmethod_11(class18_0);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0003A6C4 File Offset: 0x000388C4
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_11(class18_0);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000510B File Offset: 0x0000330B
		internal override IntPtr vmethod_10()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0003A6D8 File Offset: 0x000388D8
		internal override void vmethod_11(Class7.Class18 class18_0)
		{
			this.class16_0.class18_1[this.int_0] = class18_0;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0003A6F8 File Offset: 0x000388F8
		internal override object vmethod_4(Type type_0)
		{
			if (this.class16_0.class18_1[this.int_0] == null)
			{
				return null;
			}
			return this.vmethod_7().vmethod_4(type_0);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0003A728 File Offset: 0x00038928
		internal override Class7.Class18 vmethod_7()
		{
			if (this.class16_0.class18_1[this.int_0] != null)
			{
				return this.class16_0.class18_1[this.int_0].vmethod_7();
			}
			return new Class7.Class30(null);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00005112 File Offset: 0x00003312
		internal override bool vmethod_8()
		{
			return this.vmethod_7().vmethod_8();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0003A768 File Offset: 0x00038968
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			return class18_0.vmethod_0() && class18_0 is Class7.Class25 && ((Class7.Class25)class18_0).int_0 == this.int_0;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0003A7A0 File Offset: 0x000389A0
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			return !class18_0.vmethod_0() || !(class18_0 is Class7.Class25) || ((Class7.Class25)class18_0).int_0 != this.int_0;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool vmethod_6()
		{
			return this.vmethod_7().vmethod_6();
		}

		// Token: 0x0400069C RID: 1692
		private Class7.Class16 class16_0;

		// Token: 0x0400069D RID: 1693
		internal int int_0;
	}

	// Token: 0x0200016D RID: 365
	internal class Class26 : Class7.Class24
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0003A7D8 File Offset: 0x000389D8
		public Class26(int int_1, Array array_1)
		{
			this.array_0 = array_1;
			this.int_0 = int_1;
			this.enum4_0 = (Class7.Enum4)7;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000510B File Offset: 0x0000330B
		internal override IntPtr vmethod_10()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0003A800 File Offset: 0x00038A00
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			if (class18_0 is Class7.Class26)
			{
				this.array_0 = ((Class7.Class26)class18_0).array_0;
				this.int_0 = ((Class7.Class26)class18_0).int_0;
				return;
			}
			this.vmethod_11(class18_0);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0003A6C4 File Offset: 0x000388C4
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_11(class18_0);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0003A840 File Offset: 0x00038A40
		internal override void vmethod_11(Class7.Class18 class18_0)
		{
			this.array_0.SetValue(class18_0.vmethod_4(null), this.int_0);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000512C File Offset: 0x0000332C
		internal override object vmethod_4(Type type_0)
		{
			return this.vmethod_7().vmethod_4(type_0);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000513A File Offset: 0x0000333A
		internal override Class7.Class18 vmethod_7()
		{
			return Class7.Class18.smethod_1(this.array_0.GetType().GetElementType(), this.array_0.GetValue(this.int_0));
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00005112 File Offset: 0x00003312
		internal override bool vmethod_8()
		{
			return this.vmethod_7().vmethod_8();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0003A868 File Offset: 0x00038A68
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (!class18_0.vmethod_0())
			{
				return false;
			}
			if (!(class18_0 is Class7.Class26))
			{
				return false;
			}
			Class7.Class26 @class = (Class7.Class26)class18_0;
			return @class.int_0 == this.int_0 && @class.array_0 == this.array_0;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (!class18_0.vmethod_0())
			{
				return true;
			}
			if (class18_0 is Class7.Class26)
			{
				Class7.Class26 @class = (Class7.Class26)class18_0;
				return @class.int_0 != this.int_0 || @class.array_0 != this.array_0;
			}
			return true;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool vmethod_6()
		{
			return this.vmethod_7().vmethod_6();
		}

		// Token: 0x0400069E RID: 1694
		private Array array_0;

		// Token: 0x0400069F RID: 1695
		internal int int_0;
	}

	// Token: 0x0200016E RID: 366
	internal class Class27 : Class7.Class24
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0003A900 File Offset: 0x00038B00
		public Class27(FieldInfo fieldInfo_1, object object_1)
		{
			this.fieldInfo_0 = fieldInfo_1;
			this.object_0 = object_1;
			this.enum4_0 = (Class7.Enum4)7;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000510B File Offset: 0x0000330B
		internal override IntPtr vmethod_10()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003A928 File Offset: 0x00038B28
		internal override void vmethod_11(Class7.Class18 class18_0)
		{
			if (this.object_0 != null && this.object_0 is Class7.Class18)
			{
				this.fieldInfo_0.SetValue(((Class7.Class18)this.object_0).vmethod_4(null), class18_0.vmethod_4(null));
				return;
			}
			this.fieldInfo_0.SetValue(this.object_0, class18_0.vmethod_4(null));
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0003A988 File Offset: 0x00038B88
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			if (!(class18_0 is Class7.Class27))
			{
				this.vmethod_11(class18_0);
				return;
			}
			this.fieldInfo_0 = ((Class7.Class27)class18_0).fieldInfo_0;
			this.object_0 = ((Class7.Class27)class18_0).object_0;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0003A6C4 File Offset: 0x000388C4
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_11(class18_0);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000512C File Offset: 0x0000332C
		internal override object vmethod_4(Type type_0)
		{
			return this.vmethod_7().vmethod_4(type_0);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		internal override Class7.Class18 vmethod_7()
		{
			if (this.object_0 != null && this.object_0 is Class7.Class18)
			{
				return Class7.Class18.smethod_1(this.fieldInfo_0.FieldType, this.fieldInfo_0.GetValue(((Class7.Class18)this.object_0).vmethod_4(null)));
			}
			return Class7.Class18.smethod_1(this.fieldInfo_0.FieldType, this.fieldInfo_0.GetValue(this.object_0));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00005112 File Offset: 0x00003312
		internal override bool vmethod_8()
		{
			return this.vmethod_7().vmethod_8();
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0003AA38 File Offset: 0x00038C38
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (!class18_0.vmethod_0())
			{
				return false;
			}
			if (!(class18_0 is Class7.Class27))
			{
				return false;
			}
			Class7.Class27 @class = (Class7.Class27)class18_0;
			return !(@class.fieldInfo_0 != this.fieldInfo_0) && @class.object_0 == this.object_0;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0003AA8C File Offset: 0x00038C8C
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (!class18_0.vmethod_0())
			{
				return true;
			}
			if (class18_0 is Class7.Class27)
			{
				Class7.Class27 @class = (Class7.Class27)class18_0;
				return @class.fieldInfo_0 != this.fieldInfo_0 || @class.object_0 != this.object_0;
			}
			return true;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool vmethod_6()
		{
			return this.vmethod_7().vmethod_6();
		}

		// Token: 0x040006A0 RID: 1696
		internal FieldInfo fieldInfo_0;

		// Token: 0x040006A1 RID: 1697
		internal object object_0;
	}

	// Token: 0x0200016F RID: 367
	internal class Class28 : Class7.Class24
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		public Class28(int int_1, Class7.Class16 class16_0)
		{
			this.EqvjjieJlV = class16_0;
			this.int_0 = int_1;
			this.enum4_0 = (Class7.Enum4)7;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000510B File Offset: 0x0000330B
		internal override IntPtr vmethod_10()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003AB08 File Offset: 0x00038D08
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			if (class18_0 is Class7.Class28)
			{
				this.EqvjjieJlV = ((Class7.Class28)class18_0).EqvjjieJlV;
				this.int_0 = ((Class7.Class28)class18_0).int_0;
				return;
			}
			this.vmethod_11(class18_0);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0003A6C4 File Offset: 0x000388C4
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_11(class18_0);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0003AB48 File Offset: 0x00038D48
		internal override void vmethod_11(Class7.Class18 class18_0)
		{
			this.EqvjjieJlV.class18_0[this.int_0] = class18_0;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0003AB68 File Offset: 0x00038D68
		internal override object vmethod_4(Type type_0)
		{
			if (this.EqvjjieJlV.class18_0[this.int_0] == null)
			{
				return null;
			}
			return this.vmethod_7().vmethod_4(type_0);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0003AB98 File Offset: 0x00038D98
		internal override Class7.Class18 vmethod_7()
		{
			if (this.EqvjjieJlV.class18_0[this.int_0] != null)
			{
				return this.EqvjjieJlV.class18_0[this.int_0].vmethod_7();
			}
			return new Class7.Class30(null);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00005112 File Offset: 0x00003312
		internal override bool vmethod_8()
		{
			return this.vmethod_7().vmethod_8();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003ABD8 File Offset: 0x00038DD8
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			return class18_0.vmethod_0() && class18_0 is Class7.Class28 && ((Class7.Class28)class18_0).int_0 == this.int_0;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0003AC0C File Offset: 0x00038E0C
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			return !class18_0.vmethod_0() || !(class18_0 is Class7.Class28) || ((Class7.Class28)class18_0).int_0 != this.int_0;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool vmethod_6()
		{
			return this.vmethod_7().vmethod_6();
		}

		// Token: 0x040006A2 RID: 1698
		private Class7.Class16 EqvjjieJlV;

		// Token: 0x040006A3 RID: 1699
		internal int int_0;
	}

	// Token: 0x02000170 RID: 368
	internal class Class29 : Class7.Class24
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x0003AC44 File Offset: 0x00038E44
		public Class29(Class7.Class18 class18_1, Type type_1)
		{
			this.class18_0 = class18_1;
			this.type_0 = type_1;
			this.enum4_0 = (Class7.Enum4)7;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000510B File Offset: 0x0000330B
		internal override IntPtr vmethod_10()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0003AC6C File Offset: 0x00038E6C
		internal override void vmethod_9(Class7.Class18 class18_1)
		{
			if (!(class18_1 is Class7.Class29))
			{
				this.class18_0.vmethod_9(class18_1);
				return;
			}
			this.type_0 = ((Class7.Class29)class18_1).type_0;
			this.class18_0 = ((Class7.Class29)class18_1).class18_0;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0003A6C4 File Offset: 0x000388C4
		internal override void vmethod_2(Class7.Class18 class18_1)
		{
			this.vmethod_11(class18_1);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003ACB0 File Offset: 0x00038EB0
		internal override void vmethod_11(Class7.Class18 class18_1)
		{
			this.class18_0 = class18_1;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0003ACC4 File Offset: 0x00038EC4
		internal override object vmethod_4(Type type_1)
		{
			if (this.class18_0 == null)
			{
				return new Class7.Class30(null);
			}
			if (!(type_1 == null) && !(type_1 == typeof(object)))
			{
				return this.class18_0.vmethod_4(type_1);
			}
			return this.class18_0.vmethod_4(this.type_0);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003AD1C File Offset: 0x00038F1C
		internal override Class7.Class18 vmethod_7()
		{
			if (this.class18_0 == null)
			{
				return new Class7.Class30(null);
			}
			return this.class18_0.vmethod_7();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00005112 File Offset: 0x00003312
		internal override bool vmethod_8()
		{
			return this.vmethod_7().vmethod_8();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003AD44 File Offset: 0x00038F44
		internal override bool vmethod_5(Class7.Class18 class18_1)
		{
			if (!class18_1.vmethod_0())
			{
				return false;
			}
			if (!(class18_1 is Class7.Class29))
			{
				return false;
			}
			Class7.Class29 @class = (Class7.Class29)class18_1;
			if (@class.type_0 != this.type_0)
			{
				return false;
			}
			if (this.class18_0 == null)
			{
				return @class.class18_0 == null;
			}
			return this.class18_0.vmethod_5(@class.class18_0);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003ADAC File Offset: 0x00038FAC
		internal override bool BeouTiljCp(Class7.Class18 class18_1)
		{
			if (!class18_1.vmethod_0())
			{
				return true;
			}
			if (!(class18_1 is Class7.Class29))
			{
				return true;
			}
			Class7.Class29 @class = (Class7.Class29)class18_1;
			if (@class.type_0 != this.type_0)
			{
				return true;
			}
			if (this.class18_0 == null)
			{
				return @class.class18_0 != null;
			}
			return this.class18_0.BeouTiljCp(@class.class18_0);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool vmethod_6()
		{
			return this.vmethod_7().vmethod_6();
		}

		// Token: 0x040006A4 RID: 1700
		private Class7.Class18 class18_0;

		// Token: 0x040006A5 RID: 1701
		private Type type_0;
	}

	// Token: 0x02000171 RID: 369
	internal class Class9
	{
		// Token: 0x040006A6 RID: 1702
		public int int_0;

		// Token: 0x040006A7 RID: 1703
		public bool UhNjdqxgic;

		// Token: 0x040006A8 RID: 1704
		public Class7.Enum1 enum1_0;
	}

	// Token: 0x02000172 RID: 370
	internal class Class10
	{
		// Token: 0x040006A9 RID: 1705
		public int int_0;

		// Token: 0x040006AA RID: 1706
		public Class7.Enum1 enum1_0;

		// Token: 0x040006AB RID: 1707
		public bool bool_0;

		// Token: 0x040006AC RID: 1708
		public Type type_0 = typeof(object);
	}

	// Token: 0x02000173 RID: 371
	internal class Class11
	{
		// Token: 0x040006AD RID: 1709
		public int int_0;

		// Token: 0x040006AE RID: 1710
		public int int_1;

		// Token: 0x040006AF RID: 1711
		public Class7.Class12 class12_0;
	}

	// Token: 0x02000174 RID: 372
	internal class Class12
	{
		// Token: 0x040006B0 RID: 1712
		public int int_0;

		// Token: 0x040006B1 RID: 1713
		public int int_1;

		// Token: 0x040006B2 RID: 1714
		public byte byte_0;

		// Token: 0x040006B3 RID: 1715
		public Type type_0;

		// Token: 0x040006B4 RID: 1716
		public int int_2;

		// Token: 0x040006B5 RID: 1717
		public int int_3;
	}

	// Token: 0x02000175 RID: 373
	internal class Class13
	{
		// Token: 0x040006B6 RID: 1718
		internal object object_0;

		// Token: 0x040006B7 RID: 1719
		internal List<Class7.Class8> list_0;

		// Token: 0x040006B8 RID: 1720
		internal Class7.Class9[] class9_0;

		// Token: 0x040006B9 RID: 1721
		internal List<Class7.Class10> fBcjhfhonR;

		// Token: 0x040006BA RID: 1722
		internal List<Class7.Class11> list_1;
	}

	// Token: 0x02000176 RID: 374
	private class Class14
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x0003AE38 File Offset: 0x00039038
		public Class14(FieldInfo fieldInfo_0, int int_0)
		{
			this.object_0 = fieldInfo_0;
			this.ovMjpUjjfa = int_0;
		}

		// Token: 0x040006BB RID: 1723
		internal object object_0;

		// Token: 0x040006BC RID: 1724
		internal int ovMjpUjjfa;
	}

	// Token: 0x02000177 RID: 375
	private class Class15
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x00005162 File Offset: 0x00003362
		public Class15(MethodBase methodBase_1, List<Class7.Class14> list_1)
		{
			this.list_0 = list_1;
			this.methodBase_0 = methodBase_1;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003AE5C File Offset: 0x0003905C
		public Class15(MethodBase methodBase_1, Class7.Class14[] class14_0)
		{
			this.list_0.AddRange(class14_0);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003AE88 File Offset: 0x00039088
		public override bool Equals(object obj)
		{
			Class7.Class15 @class = obj as Class7.Class15;
			if (obj == null)
			{
				return false;
			}
			if (this.methodBase_0 != @class.methodBase_0)
			{
				return false;
			}
			if (this.list_0.Count != @class.list_0.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list_0.Count; i++)
			{
				if (this.list_0[i].object_0 != @class.list_0[i].object_0)
				{
					return false;
				}
				if (this.list_0[i].ovMjpUjjfa != @class.list_0[i].ovMjpUjjfa)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003AF50 File Offset: 0x00039150
		public override int GetHashCode()
		{
			int num = this.methodBase_0.GetHashCode();
			foreach (Class7.Class14 @class in this.list_0)
			{
				int num2 = @class.object_0.GetHashCode() + @class.ovMjpUjjfa;
				num = (num ^ num2) + num2;
			}
			return num;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0003AFD0 File Offset: 0x000391D0
		public Class7.Class14 method_0(int int_0)
		{
			foreach (Class7.Class14 @class in this.list_0)
			{
				if (@class.ovMjpUjjfa == int_0)
				{
					return @class;
				}
			}
			return null;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003B038 File Offset: 0x00039238
		public bool method_1(int int_0)
		{
			using (List<Class7.Class14>.Enumerator enumerator = this.list_0.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ovMjpUjjfa == int_0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040006BD RID: 1725
		private List<Class7.Class14> list_0 = new List<Class7.Class14>();

		// Token: 0x040006BE RID: 1726
		private MethodBase methodBase_0;
	}

	// Token: 0x02000178 RID: 376
	// (Invoke) Token: 0x060009B2 RID: 2482
	private delegate object Delegate10(object target, object[] paramters);

	// Token: 0x02000179 RID: 377
	// (Invoke) Token: 0x060009B6 RID: 2486
	private delegate object Delegate11(object target);

	// Token: 0x0200017A RID: 378
	// (Invoke) Token: 0x060009BA RID: 2490
	private delegate void Delegate12(IntPtr a, byte b, int c);

	// Token: 0x0200017B RID: 379
	// (Invoke) Token: 0x060009BE RID: 2494
	private delegate void Delegate13(IntPtr s, IntPtr t, uint c);

	// Token: 0x0200017C RID: 380
	internal class Class16
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0003B098 File Offset: 0x00039298
		internal void method_0()
		{
			bool flag = false;
			this.method_2(ref flag);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0003B0B0 File Offset: 0x000392B0
		internal void method_1()
		{
			this.class32_0.method_1();
			this.class18_1 = null;
			if (this.list_0 != null)
			{
				foreach (IntPtr intPtr in this.list_0)
				{
					try
					{
						Marshal.FreeHGlobal(intPtr);
					}
					catch
					{
					}
				}
				this.list_0.Clear();
				this.list_0 = null;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0003B144 File Offset: 0x00039344
		internal void method_2(ref bool bool_4)
		{
			while (this.int_0 > -2)
			{
				if (this.bool_0)
				{
					this.bool_0 = false;
					int num = this.int_1;
					int num2 = this.int_0;
					this.method_4(this.int_1, this.int_0);
					this.int_0 = num2;
					this.int_1 = num;
				}
				if (this.bool_2)
				{
					this.bool_2 = false;
					return;
				}
				if (!this.bool_1)
				{
					this.int_1 = this.int_0;
					Class7.Class8 @class = this.class13_0.list_0[this.int_0];
					this.object_0 = @class.object_0;
					try
					{
						this.method_7(@class);
					}
					catch (Exception innerException)
					{
						if (innerException is TargetInvocationException)
						{
							TargetInvocationException ex = (TargetInvocationException)innerException;
							if (ex.InnerException != null)
							{
								innerException = ex.InnerException;
							}
						}
						this.exception_0 = innerException;
						bool_4 = true;
						this.class32_0.method_1();
						int num3 = this.int_1;
						Class7.Class11 class2 = this.method_5(num3, innerException);
						List<Class7.Class11> list = this.method_6(num3, false);
						List<Class7.Class11> list2 = new List<Class7.Class11>();
						if (class2 != null)
						{
							list2.Add(class2);
						}
						if (list != null && list.Count > 0)
						{
							list2.AddRange(list);
						}
						list2.Sort(new Comparison<Class7.Class11>(Class7.Class16.Class17.<>9.method_0));
						Class7.Class11 class3 = null;
						foreach (Class7.Class11 class4 in list2)
						{
							if (class4.class12_0.int_3 != 0)
							{
								this.class32_0.method_2(new Class7.Class30(innerException));
								this.int_1 = class4.class12_0.int_2;
								this.int_0 = this.int_1;
								this.method_0();
								if (!this.bool_3)
								{
									continue;
								}
								this.bool_3 = false;
								class3 = class4;
							}
							else
							{
								class3 = class4;
							}
							break;
						}
						if (class3 != null)
						{
							this.int_2 = class3.class12_0.int_0;
							this.method_3(num3, class3.class12_0.int_0);
							if (this.int_2 >= 0)
							{
								this.class32_0.method_2(new Class7.Class30(innerException));
								this.int_1 = this.int_2;
								this.int_0 = this.int_1;
								this.int_2 = -1;
								this.method_0();
							}
							return;
						}
						throw innerException;
					}
					this.int_0++;
					continue;
				}
				this.bool_1 = false;
				return;
			}
			this.class32_0.method_1();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0003B404 File Offset: 0x00039604
		internal void method_3(int int_3, int int_4)
		{
			if (this.class13_0.list_1 != null)
			{
				foreach (Class7.Class11 @class in this.class13_0.list_1)
				{
					if ((@class.class12_0.int_3 == 4 || @class.class12_0.int_3 == 2) && @class.class12_0.int_0 >= int_3 && @class.class12_0.int_1 <= int_4)
					{
						this.int_1 = @class.class12_0.int_0;
						this.int_0 = this.int_1;
						bool flag = false;
						this.method_2(ref flag);
						if (flag)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0003B4D4 File Offset: 0x000396D4
		internal void method_4(int int_3, int int_4)
		{
			if (this.class13_0.list_1 != null)
			{
				foreach (Class7.Class11 @class in this.class13_0.list_1)
				{
					if (@class.class12_0.int_3 == 2 && @class.class12_0.int_0 >= int_3 && @class.class12_0.int_1 <= int_4)
					{
						this.int_1 = @class.class12_0.int_0;
						this.int_0 = this.int_1;
						bool flag = false;
						this.method_2(ref flag);
						if (flag)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0003B598 File Offset: 0x00039798
		internal Class7.Class11 method_5(int int_3, Exception exception_1)
		{
			Class7.Class11 @class = null;
			if (this.class13_0.list_1 != null)
			{
				foreach (Class7.Class11 class2 in this.class13_0.list_1)
				{
					if (class2.class12_0 != null && class2.class12_0.int_3 == 0 && (class2.class12_0.type_0 == exception_1.GetType() || (class2.class12_0.type_0 != null && (class2.class12_0.type_0.FullName == exception_1.GetType().FullName || class2.class12_0.type_0.FullName == typeof(object).FullName || class2.class12_0.type_0.FullName == typeof(Exception).FullName))) && int_3 >= class2.int_0 && int_3 <= class2.int_1)
					{
						if (@class == null)
						{
							@class = class2;
						}
						else if (class2.class12_0.int_0 < @class.class12_0.int_0)
						{
							@class = class2;
						}
					}
				}
			}
			return @class;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003B710 File Offset: 0x00039910
		internal List<Class7.Class11> method_6(int int_3, bool bool_4)
		{
			if (this.class13_0.list_1 == null)
			{
				return null;
			}
			List<Class7.Class11> list = new List<Class7.Class11>();
			foreach (Class7.Class11 @class in this.class13_0.list_1)
			{
				if ((@class.class12_0.int_3 & 1) == 1 && int_3 >= @class.int_0 && int_3 <= @class.int_1)
				{
					list.Add(@class);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0003B7AC File Offset: 0x000399AC
		private unsafe void method_7(Class7.Class8 class8_0)
		{
			switch (class8_0.enum3_0)
			{
			case (Class7.Enum3)0:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_76(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)1:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_47());
				return;
			}
			case (Class7.Enum3)2:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_46();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)3:
			{
				Class7.Class18 @class = this.class18_1[(int)this.object_0];
				this.class32_0.method_2(@class);
				return;
			}
			case (Class7.Enum3)4:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (!Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_77(@class))
				{
					this.class32_0.method_2(new Class7.Class20(0));
					return;
				}
				this.class32_0.method_2(new Class7.Class20(1));
				return;
			}
			case (Class7.Enum3)5:
			{
				int num = (int)this.object_0;
				MethodBase methodBase = typeof(Class7).Module.ResolveMethod(num);
				Type type = this.class32_0.method_4().vmethod_4(null).GetType();
				List<Type> list = new List<Type>();
				do
				{
					list.Add(type);
					type = type.BaseType;
				}
				while (type != null && type != methodBase.DeclaringType);
				list.Reverse();
				MethodBase methodBase2 = methodBase;
				foreach (Type type2 in list)
				{
					foreach (MethodInfo methodInfo in type2.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						if (methodInfo.GetBaseDefinition() == methodBase2)
						{
							methodBase2 = methodInfo;
							break;
						}
					}
				}
				this.class32_0.method_2(new Class7.Class22(methodBase2.MethodHandle.GetFunctionPointer()));
				return;
			}
			case (Class7.Enum3)6:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null && class3 != null)
				{
					this.class32_0.method_2(class2.vmethod_67(class3));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)7:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_50());
				return;
			}
			case (Class7.Enum3)8:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_27());
				return;
			}
			case (Class7.Enum3)9:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_61(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)10:
			{
				object obj = Class7.Class16.object_2;
				lock (obj)
				{
					object obj2 = this.class32_0.method_4().vmethod_4(null);
					Class7.Class18 @class = null;
					if (Class7.Class16.dictionary_1.TryGetValue(obj2, out @class))
					{
						this.class32_0.method_2(@class);
					}
					else
					{
						this.class32_0.method_2(new Class7.Class30(null));
					}
				}
				return;
			}
			case (Class7.Enum3)11:
			case (Class7.Enum3)30:
			case (Class7.Enum3)57:
			case (Class7.Enum3)67:
			case (Class7.Enum3)89:
			case (Class7.Enum3)120:
				return;
			case (Class7.Enum3)12:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_37());
				return;
			}
			case (Class7.Enum3)13:
				if (Class7.list_0.Count == 0)
				{
					Module module = typeof(Class7).Module;
					this.class32_0.method_2(new Class7.Class31(module.ResolveString((int)this.object_0 | 1879048192)));
					return;
				}
				this.class32_0.method_2(new Class7.Class31(Class7.list_0[(int)this.object_0]));
				return;
			case (Class7.Enum3)14:
				if (this.class32_0.method_4().BeouTiljCp(this.class32_0.method_4()))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			case (Class7.Enum3)15:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_42());
				return;
			}
			case (Class7.Enum3)16:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_46());
				return;
			}
			case (Class7.Enum3)17:
				this.class32_0.method_2(new Class7.Class20((int)this.object_0));
				return;
			case (Class7.Enum3)18:
			{
				int num = (int)this.object_0;
				Module module = typeof(Class7).Module;
				this.class32_0.method_2(new Class7.Class22(module.ResolveMethod(num).MethodHandle.GetFunctionPointer()));
				return;
			}
			case (Class7.Enum3)19:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_24();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)20:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				this.class32_0.method_2(Class7.Class18.smethod_1(fieldInfo.FieldType, fieldInfo.GetValue(null)));
				return;
			}
			case (Class7.Enum3)21:
			{
				Class7.Class19 class2 = this.class32_0.method_4() as Class7.Class19;
				Class7.Class19 class3 = this.class32_0.method_4() as Class7.Class19;
				IntPtr intPtr = Class7.Class16.bltXeOrHnB(this.class32_0.method_4());
				if (intPtr != IntPtr.Zero)
				{
					byte byte_ = class3.vmethod_15().struct2_0.byte_0;
					uint num2 = class2.vmethod_19().struct2_0.uint_0;
					Class7.Class16.smethod_9(intPtr, byte_, (int)num2);
				}
				return;
			}
			case (Class7.Enum3)22:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_48());
				return;
			}
			case (Class7.Enum3)23:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_49());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)24:
			case (Class7.Enum3)37:
			case (Class7.Enum3)41:
			case (Class7.Enum3)61:
			case (Class7.Enum3)91:
			case (Class7.Enum3)134:
			case (Class7.Enum3)146:
			case (Class7.Enum3)154:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Array array = (Array)this.class32_0.method_4().vmethod_4(null);
				Type type = array.GetType().GetElementType();
				array.SetValue(@class.vmethod_4(type), class2.vmethod_18().struct2_0.int_0);
				return;
			}
			case (Class7.Enum3)25:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_62(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)26:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_23());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)27:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_22();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)28:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_72(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)29:
			{
				Class7.Class19 class4 = Class7.Class16.smethod_1(this.class32_0.method_3());
				if (class4 != null)
				{
					Class7.Class23 class5 = class4 as Class7.Class23;
					if (class5 != null)
					{
						if (double.IsNaN(class5.double_0))
						{
							throw new OverflowException(((Class7.Enum5)2).ToString());
						}
						if (double.IsInfinity(class5.double_0))
						{
							throw new OverflowException(((Class7.Enum5)1).ToString());
						}
					}
					return;
				}
				throw new ArithmeticException(((Class7.Enum5)0).ToString());
			}
			case (Class7.Enum3)31:
				this.class32_0.method_2(this.class18_0[(int)this.object_0]);
				return;
			case (Class7.Enum3)32:
			case (Class7.Enum3)72:
			case (Class7.Enum3)74:
			case (Class7.Enum3)81:
			case (Class7.Enum3)109:
			case (Class7.Enum3)166:
				throw new Class7.Exception1();
			case (Class7.Enum3)33:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_43());
				return;
			}
			case (Class7.Enum3)34:
			{
				int num = (int)this.object_0;
				this.class18_1[num] = this.method_8(this.class32_0.method_4(), this.class13_0.fBcjhfhonR[num].enum1_0, this.class13_0.fBcjhfhonR[num].bool_0);
				return;
			}
			case (Class7.Enum3)35:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_23());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20((int)(*(short*)(void*)intPtr), (Class7.Enum1)3));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)36:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(double), obj));
				return;
			}
			case (Class7.Enum3)38:
				this.class32_0.method_2(new Class7.Class30(null));
				return;
			case (Class7.Enum3)39:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_36());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)40:
				this.bool_2 = true;
				return;
			case (Class7.Enum3)42:
			{
				Class7.Class18 class6 = this.class32_0.method_4();
				Class7.Class18 @class = this.class32_0.method_4();
				if (class6.vmethod_5(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)43:
				this.int_0 = (int)this.object_0 - 1;
				this.bool_0 = true;
				return;
			case (Class7.Enum3)44:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_48());
					return;
				}
				if (class2 == null || !class2.method_2())
				{
					throw new Class7.Exception1();
				}
				IntPtr intPtr = ((Class7.Class22)class2).method_6();
				if (IntPtr.Size == 8)
				{
					long num3 = *(long*)(void*)intPtr;
					this.class32_0.method_2(new Class7.Class22(num3, (Class7.Enum1)12));
					return;
				}
				int num = *(int*)(void*)intPtr;
				this.class32_0.method_2(new Class7.Class22((long)num, (Class7.Enum1)12));
				return;
			}
			case (Class7.Enum3)45:
			{
				Array array2 = (Array)this.class32_0.method_4().vmethod_4(null);
				this.class32_0.method_2(new Class7.Class20(array2.Length, (Class7.Enum1)5));
				return;
			}
			case (Class7.Enum3)46:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_25());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class21(*(long*)(void*)intPtr, (Class7.Enum1)7));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)47:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				Class7.Class18 class7 = this.class32_0.method_4();
				class7.vmethod_7();
				object obj = class7.vmethod_4(null);
				this.class32_0.method_2(new Class7.Class27(fieldInfo, obj));
				return;
			}
			case (Class7.Enum3)48:
			{
				int num = (int)this.object_0;
				Type type3 = typeof(Class7).Module.ResolveType(num);
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Array array2 = Array.CreateInstance(type3, class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(new Class7.Class30(array2));
				return;
			}
			case (Class7.Enum3)49:
			{
				Class7.Class19 class2 = this.class32_0.method_4() as Class7.Class19;
				IntPtr intPtr = Class7.Class16.bltXeOrHnB(this.class32_0.method_4());
				IntPtr intPtr2 = Class7.Class16.bltXeOrHnB(this.class32_0.method_4());
				if (intPtr != IntPtr.Zero && intPtr2 != IntPtr.Zero)
				{
					uint num2 = class2.vmethod_19().struct2_0.uint_0;
					Class7.Class16.smethod_10(intPtr2, intPtr, num2);
				}
				return;
			}
			case (Class7.Enum3)50:
				this.class32_0.method_2(new Class7.Class28((int)this.object_0, this));
				return;
			case (Class7.Enum3)51:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null && class3 != null)
				{
					this.class32_0.method_2(class2.vmethod_68(class3));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)52:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_28());
				return;
			}
			case (Class7.Enum3)53:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_33());
				return;
			}
			case (Class7.Enum3)54:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_73(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)55:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				Class7.Class18 class8 = this.class32_0.method_4();
				Class7.Class19 class3 = Class7.Class16.smethod_1(class8);
				if (class3 != null && class2 != null)
				{
					if (class3.lwlumgaheq(@class))
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					return;
				}
				if (@class.BeouTiljCp(class8))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)56:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_40());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)58:
				this.class32_0.method_4();
				return;
			case (Class7.Enum3)59:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)60:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(int), obj));
				return;
			}
			case (Class7.Enum3)62:
			{
				int num = (int)this.object_0;
				typeof(Class7).Module.ResolveType(num);
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Array array2 = (Array)this.class32_0.method_4().vmethod_4(null);
				this.class32_0.method_2(new Class7.Class26(class2.vmethod_18().struct2_0.int_0, array2));
				return;
			}
			case (Class7.Enum3)63:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_27());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20((int)(*(ushort*)(void*)intPtr), (Class7.Enum1)4));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)64:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_45();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)65:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class24 class9 = this.class32_0.method_4() as Class7.Class24;
				if (class9 == null)
				{
					throw new Class7.Exception1();
				}
				object obj = class9.vmethod_4(type);
				Class7.Class18 @class;
				if (obj == null)
				{
					if (type.IsValueType)
					{
						obj = Activator.CreateInstance(type);
						@class = Class7.Class18.smethod_1(type, obj);
					}
					else
					{
						@class = new Class7.Class30(null);
					}
				}
				else
				{
					if (type.IsValueType)
					{
						obj = Class7.Class16.smethod_8(obj);
					}
					@class = Class7.Class18.smethod_1(type, obj);
				}
				this.class32_0.method_2(@class);
				return;
			}
			case (Class7.Enum3)66:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_24());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20(*(int*)(void*)intPtr, (Class7.Enum1)5));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)68:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_38());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)69:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_51());
				return;
			}
			case (Class7.Enum3)70:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				((Array)this.class32_0.method_4().vmethod_4(null)).SetValue(@class.vmethod_4(type), class2.vmethod_18().struct2_0.int_0);
				return;
			}
			case (Class7.Enum3)71:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(ushort), obj));
				return;
			}
			case (Class7.Enum3)73:
			case (Class7.Enum3)147:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class18 @class = this.class32_0.method_4();
				object obj = @class.vmethod_4(type);
				if (obj == null)
				{
					if (!type.IsValueType)
					{
						@class = new Class7.Class30(null);
					}
					else
					{
						obj = Activator.CreateInstance(type);
						@class = Class7.Class18.smethod_1(type, obj);
					}
				}
				else
				{
					if (type.IsValueType)
					{
						obj = Class7.Class16.smethod_8(obj);
					}
					@class = Class7.Class18.smethod_1(type, obj);
				}
				Class7.Class24 class10 = this.class32_0.method_4() as Class7.Class24;
				if (class10 == null)
				{
					throw new Class7.Exception1();
				}
				class10.vmethod_9(@class);
				return;
			}
			case (Class7.Enum3)75:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_63(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)76:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_26());
				return;
			}
			case (Class7.Enum3)77:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				object obj = this.class32_0.method_4().vmethod_4(null);
				this.class32_0.method_2(Class7.Class18.smethod_1(fieldInfo.FieldType, fieldInfo.GetValue(obj)));
				return;
			}
			case (Class7.Enum3)78:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_28());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20(*(uint*)(void*)intPtr, (Class7.Enum1)6));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)79:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_81(@class))
				{
					this.class32_0.method_2(new Class7.Class20(1));
					return;
				}
				this.class32_0.method_2(new Class7.Class20(0));
				return;
			}
			case (Class7.Enum3)80:
			{
				int num = (int)this.object_0;
				if (this.class13_0.object_0.IsStatic)
				{
					this.class18_0[num] = this.method_8(this.class32_0.method_4(), this.class13_0.class9_0[num].enum1_0, false);
					return;
				}
				this.class18_0[num] = this.method_8(this.class32_0.method_4(), this.class13_0.class9_0[num - 1].enum1_0, false);
				return;
			}
			case (Class7.Enum3)82:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class != null && @class.vmethod_6())
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)83:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (!@class.vmethod_0())
				{
					throw new Class7.Exception1();
				}
				object obj = @class.vmethod_4(null);
				if (obj == null)
				{
					@class = new Class7.Class30(null);
				}
				else
				{
					@class = Class7.Class18.smethod_1(obj.GetType(), obj);
				}
				this.class32_0.method_2(@class);
				return;
			}
			case (Class7.Enum3)84:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (!Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_80(@class))
				{
					this.class32_0.method_2(new Class7.Class20(0));
					return;
				}
				this.class32_0.method_2(new Class7.Class20(1));
				return;
			}
			case (Class7.Enum3)85:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_53());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)86:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_57(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)87:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_24());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)88:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_35());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)90:
				this.class32_0.method_2(this.class32_0.method_4().vmethod_7());
				return;
			case (Class7.Enum3)92:
				this.class32_0.method_2(new Class7.Class23((float)this.object_0));
				return;
			case (Class7.Enum3)93:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_56(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)94:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_39());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)95:
				this.class32_0.method_2(new Class7.Class21((long)this.object_0));
				return;
			case (Class7.Enum3)96:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_25();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)97:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_23();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)98:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_41());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)99:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_80(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)100:
				this.class32_0.method_2(new Class7.Class23((double)this.object_0));
				return;
			case (Class7.Enum3)101:
			{
				int num = (int)this.object_0;
				uint num2 = (uint)Class7.Class16.smethod_0(typeof(Class7).Module.ResolveType(num));
				this.class32_0.method_2(new Class7.Class20(num2, (Class7.Enum1)6));
				return;
			}
			case (Class7.Enum3)102:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_26());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20((int)(*(byte*)(void*)intPtr), (Class7.Enum1)2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)103:
			{
				bool flag = false;
				Class7.Class18 @class = this.class32_0.method_4();
				flag = @class == null || !@class.vmethod_6();
				if (flag)
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)104:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_79(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)105:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_59(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)106:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null && class3 != null)
				{
					this.class32_0.method_2(class2.vmethod_70(class3));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)107:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (@class.vmethod_3())
				{
					@class = ((Class7.Class19)@class).vmethod_48();
				}
				this.class32_0.method_4().vmethod_2(@class);
				return;
			}
			case (Class7.Enum3)108:
			{
				IntPtr intPtr = Marshal.AllocHGlobal((this.class32_0.method_4() as Class7.Class19).vmethod_18().struct2_0.int_0);
				if (this.list_0 == null)
				{
					this.list_0 = new List<IntPtr>();
				}
				this.list_0.Add(intPtr);
				this.class32_0.method_2(new Class7.Class22(intPtr));
				return;
			}
			case (Class7.Enum3)110:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_55(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)111:
			{
				Class7.Class18 class11 = Class7.Class16.smethod_6(this.class32_0.method_4());
				Class7.Class18 @class = Class7.Class16.smethod_6(this.class32_0.method_4());
				if (class11.vmethod_5(@class))
				{
					this.class32_0.method_2(new Class7.Class20(1));
					return;
				}
				this.class32_0.method_2(new Class7.Class20(0));
				return;
			}
			case (Class7.Enum3)112:
				this.int_0 = -3;
				if (this.class32_0.method_0() > 0)
				{
					this.class18_2 = this.class32_0.method_4();
				}
				return;
			case (Class7.Enum3)113:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_78(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)114:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_22());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class20((int)(*(sbyte*)(void*)intPtr), (Class7.Enum1)1));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)115:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_77(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)116:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Array array3 = (Array)this.class32_0.method_4().vmethod_4(null);
				object obj = array3.GetValue(class2.vmethod_18().struct2_0.int_0);
				Type type = array3.GetType().GetElementType();
				this.class32_0.method_2(Class7.Class18.smethod_1(type, obj));
				return;
			}
			case (Class7.Enum3)117:
			{
				Type type = typeof(Class7).Module.ResolveType((int)this.object_0);
				object obj = this.class32_0.method_4().vmethod_4(type);
				if (obj == null)
				{
					obj = Activator.CreateInstance(type);
				}
				Class7.Class30 class12 = new Class7.Class30(Class7.Class18.smethod_1(type, Class7.Class16.smethod_8(obj)));
				this.class32_0.method_2(class12);
				return;
			}
			case (Class7.Enum3)118:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				object obj = this.class32_0.method_4().vmethod_7().vmethod_4(type);
				Class7.Class18 @class = Class7.Class18.smethod_1(type, obj);
				this.class32_0.method_2(@class);
				return;
			}
			case (Class7.Enum3)119:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(uint), obj));
				return;
			}
			case (Class7.Enum3)121:
				throw this.exception_0;
			case (Class7.Enum3)122:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class24 class13 = this.class32_0.method_4() as Class7.Class24;
				if (class13 == null)
				{
					throw new Class7.Exception1();
				}
				if (!type.IsValueType)
				{
					class13.vmethod_11(new Class7.Class30(null));
					return;
				}
				object obj = Activator.CreateInstance(type);
				class13.vmethod_11(Class7.Class18.smethod_1(type, obj));
				return;
			}
			case (Class7.Enum3)123:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				this.class32_0.method_2(new Class7.Class27(fieldInfo, null));
				return;
			}
			case (Class7.Enum3)124:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.usfdqHavse());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)125:
			{
				int[] array4 = (int[])this.object_0;
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				long num3 = class2.vmethod_20().struct3_0.long_0;
				if ((num3 < 0L || class2.method_4()) && IntPtr.Size == 4)
				{
					num3 = (long)((int)num3);
				}
				if (class2.method_1())
				{
					Class7.Class20 class14 = (Class7.Class20)class2;
					if (class14.enum1_0 == (Class7.Enum1)6)
					{
						num3 = (long)((ulong)class14.struct2_0.uint_0);
					}
				}
				if (num3 < (long)array4.Length && num3 >= 0L)
				{
					this.int_0 = array4[(int)(checked((IntPtr)num3))] - 1;
				}
				return;
			}
			case (Class7.Enum3)126:
				this.int_0 = (int)this.object_0 - 1;
				return;
			case (Class7.Enum3)127:
				this.bool_3 = (bool)this.class32_0.method_4().vmethod_4(typeof(bool));
				this.bool_1 = true;
				return;
			case (Class7.Enum3)128:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_60(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)129:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_65(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)130:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_25());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)131:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_69());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)132:
				this.method_12(true);
				return;
			case (Class7.Enum3)133:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				if (Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_75(@class))
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)135:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(IntPtr), obj));
				return;
			}
			case (Class7.Enum3)136:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = (Class7.Class19)this.class32_0.method_4();
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_58(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)137:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(byte), obj));
				return;
			}
			case (Class7.Enum3)138:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_46());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class23(*(double*)(void*)intPtr, (Class7.Enum1)10));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)139:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_22());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)140:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_29());
				return;
			}
			case (Class7.Enum3)141:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				if (@class != null && @class.vmethod_0() && class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_45());
					return;
				}
				if (class2 != null && class2.method_2())
				{
					IntPtr intPtr = ((Class7.Class22)class2).method_6();
					this.class32_0.method_2(new Class7.Class23(*(float*)(void*)intPtr, (Class7.Enum1)9));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)142:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_52());
				return;
			}
			case (Class7.Enum3)143:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_31());
				return;
			}
			case (Class7.Enum3)144:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_64(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)145:
				this.method_12(false);
				return;
			case (Class7.Enum3)148:
				this.class32_0.method_2(new Class7.Class25((int)this.object_0, this));
				return;
			case (Class7.Enum3)149:
			{
				object obj = Class7.Class16.object_2;
				lock (obj)
				{
					Class7.Class18 @class = this.class32_0.method_4();
					object obj2 = this.class32_0.method_4().vmethod_4(null);
					Class7.Class16.dictionary_1[obj2] = @class;
				}
				return;
			}
			case (Class7.Enum3)150:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(sbyte), obj));
				return;
			}
			case (Class7.Enum3)151:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_30());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)152:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_45());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)153:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_44());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)155:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.Add(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)156:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 == null)
				{
					throw new Class7.Exception1();
				}
				this.class32_0.method_2(class2.vmethod_34());
				return;
			}
			case (Class7.Enum3)157:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class2 != null)
				{
					this.class32_0.method_2(class2.vmethod_32());
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)158:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(short), obj));
				return;
			}
			case (Class7.Enum3)159:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				bool flag2 = Class7.Class16.smethod_1(this.class32_0.method_4()).vmethod_81(@class);
				if (!flag2)
				{
					this.class32_0.method_2(new Class7.Class20(0));
				}
				else
				{
					this.class32_0.method_2(new Class7.Class20(1));
				}
				if (flag2)
				{
					this.int_0 = (int)this.object_0 - 1;
				}
				return;
			}
			case (Class7.Enum3)160:
			{
				int num = (int)this.object_0;
				ConstructorInfo constructorInfo = (ConstructorInfo)typeof(Class7).Module.ResolveMethod(num);
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				object[] array5 = new object[parameters.Length];
				Class7.Class18[] array6 = new Class7.Class18[parameters.Length];
				List<Class7.Class14> list2 = null;
				Class7.Class15 class15 = null;
				for (int i = 0; i < parameters.Length; i++)
				{
					Class7.Class18 @class = this.class32_0.method_4();
					Type type = parameters[parameters.Length - 1 - i].ParameterType;
					object obj2 = null;
					bool flag = false;
					if (type.IsByRef)
					{
						Class7.Class27 class16 = @class as Class7.Class27;
						if (class16 != null)
						{
							if (list2 == null)
							{
								list2 = new List<Class7.Class14>();
							}
							list2.Add(new Class7.Class14(class16.fieldInfo_0, parameters.Length - 1 - i));
							obj2 = class16.object_0;
							if (!(obj2 is Class7.Class18))
							{
								flag = true;
							}
							else
							{
								@class = obj2 as Class7.Class18;
							}
						}
					}
					if (!flag)
					{
						if (@class != null)
						{
							obj2 = @class.vmethod_4(type);
						}
						if (obj2 == null)
						{
							if (type.IsByRef)
							{
								type = type.GetElementType();
							}
							if (type.IsValueType)
							{
								obj2 = Activator.CreateInstance(type);
								if (@class is Class7.Class25)
								{
									((Class7.Class24)@class).vmethod_11(Class7.Class18.smethod_1(type, obj2));
								}
							}
						}
					}
					array6[array5.Length - 1 - i] = @class;
					array5[array5.Length - 1 - i] = obj2;
				}
				Class7.Delegate10 @delegate = null;
				if (list2 != null)
				{
					class15 = new Class7.Class15(constructorInfo, list2);
					@delegate = Class7.Class16.smethod_4(constructorInfo, true, class15);
				}
				object obj = null;
				if (@delegate != null)
				{
					obj = @delegate(null, array5);
				}
				else
				{
					obj = constructorInfo.Invoke(array5);
				}
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].ParameterType.IsByRef && (class15 == null || !class15.method_1(j)))
					{
						if (array6[j].method_2())
						{
							((Class7.Class22)array6[j]).method_5(Class7.Class18.smethod_1(parameters[j].ParameterType, array5[j]));
						}
						else if (array6[j] is Class7.Class25)
						{
							array6[j].vmethod_9(Class7.Class18.smethod_1(parameters[j].ParameterType.GetElementType(), array5[j]));
						}
						else
						{
							array6[j].vmethod_9(Class7.Class18.smethod_1(parameters[j].ParameterType, array5[j]));
						}
					}
				}
				this.class32_0.method_2(Class7.Class18.smethod_1(constructorInfo.DeclaringType, obj));
				return;
			}
			case (Class7.Enum3)161:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				object obj = this.class32_0.method_4().vmethod_4(fieldInfo.FieldType);
				fieldInfo.SetValue(null, obj);
				return;
			}
			case (Class7.Enum3)162:
			{
				int num = (int)this.object_0;
				Module module = typeof(Class7).Module;
				object obj = null;
				try
				{
					obj = module.ResolveType(num);
				}
				catch
				{
					try
					{
						obj = module.ResolveMethod(num);
					}
					catch
					{
						try
						{
							obj = module.ResolveField(num);
						}
						catch
						{
							obj = module.ResolveMember(num);
						}
					}
				}
				this.class32_0.method_2(new Class7.Class30(obj));
				return;
			}
			case (Class7.Enum3)163:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_74(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)164:
				this.class32_0.method_2(((Class7.Class19)this.class32_0.method_4()).vmethod_54());
				return;
			case (Class7.Enum3)165:
			{
				int num = (int)this.object_0;
				FieldInfo fieldInfo = typeof(Class7).Module.ResolveField(num);
				object obj = this.class32_0.method_4().vmethod_4(fieldInfo.FieldType);
				Class7.Class18 @class = this.class32_0.method_4();
				object obj2 = @class.vmethod_4(null);
				if (obj2 == null)
				{
					Type type = fieldInfo.DeclaringType;
					if (type.IsByRef)
					{
						type = type.GetElementType();
					}
					if (!type.IsValueType)
					{
						throw new NullReferenceException();
					}
					obj2 = Activator.CreateInstance(type);
					if (@class is Class7.Class25)
					{
						((Class7.Class24)@class).vmethod_11(Class7.Class18.smethod_1(type, obj2));
					}
				}
				fieldInfo.SetValue(obj2, obj);
				return;
			}
			case (Class7.Enum3)167:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(type, obj));
				return;
			}
			case (Class7.Enum3)168:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(long), obj));
				return;
			}
			case (Class7.Enum3)169:
				this.class32_0.method_2(this.class32_0.method_3());
				return;
			case (Class7.Enum3)170:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				Class7.Class19 class3 = Class7.Class16.smethod_1(this.class32_0.method_4());
				if (class3 != null && class2 != null)
				{
					this.class32_0.method_2(class3.vmethod_66(class2));
					return;
				}
				throw new Class7.Exception1();
			}
			case (Class7.Enum3)171:
				throw (Exception)this.class32_0.method_4().vmethod_4(null);
			case (Class7.Enum3)172:
			{
				Class7.Class19 class2 = Class7.Class16.smethod_1(this.class32_0.method_4());
				object obj = ((Array)this.class32_0.method_4().vmethod_4(null)).GetValue(class2.vmethod_18().struct2_0.int_0);
				this.class32_0.method_2(Class7.Class18.smethod_1(typeof(float), obj));
				return;
			}
			case (Class7.Enum3)173:
			{
				int num = (int)this.object_0;
				Type type = typeof(Class7).Module.ResolveType(num);
				Class7.Class18 @class = this.class32_0.method_4();
				object obj = @class.vmethod_4(null);
				if (obj == null)
				{
					this.class32_0.method_2(new Class7.Class30(null));
					return;
				}
				if (!type.IsAssignableFrom(obj.GetType()))
				{
					this.class32_0.method_2(new Class7.Class30(null));
					return;
				}
				this.class32_0.method_2(@class);
				return;
			}
			case (Class7.Enum3)174:
				return;
			case (Class7.Enum3)175:
			{
				Class7.Class18 @class = this.class32_0.method_4();
				Class7.Class19 class2 = Class7.Class16.smethod_1(@class);
				Class7.Class18 class8 = this.class32_0.method_4();
				Class7.Class19 class3 = Class7.Class16.smethod_1(class8);
				if (class3 != null && class2 != null)
				{
					if (class3.lwlumgaheq(@class))
					{
						this.class32_0.method_2(new Class7.Class20(1));
						return;
					}
					this.class32_0.method_2(new Class7.Class20(0));
					return;
				}
				else
				{
					if (@class.BeouTiljCp(class8))
					{
						this.class32_0.method_2(new Class7.Class20(1));
						return;
					}
					this.class32_0.method_2(new Class7.Class20(0));
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0003E834 File Offset: 0x0003CA34
		private Class7.Class18 method_8(Class7.Class18 class18_3, Class7.Enum1 enum1_0, bool bool_4 = false)
		{
			if (!bool_4 && class18_3.vmethod_0())
			{
				class18_3 = class18_3.vmethod_7();
			}
			if (class18_3.method_1())
			{
				return ((Class7.Class20)class18_3).vmethod_12(enum1_0);
			}
			if (class18_3.method_3())
			{
				return ((Class7.Class21)class18_3).vmethod_12(enum1_0);
			}
			if (class18_3.method_4())
			{
				return ((Class7.Class23)class18_3).vmethod_12(enum1_0);
			}
			if (class18_3.method_2())
			{
				return ((Class7.Class22)class18_3).vmethod_12(enum1_0);
			}
			return class18_3;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00005183 File Offset: 0x00003383
		private Class7.Class18 method_9(int int_3)
		{
			return this.class18_1[int_3];
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003E8AC File Offset: 0x0003CAAC
		private void method_10(int int_3)
		{
			this.method_11(int_3, this.class32_0.method_4());
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003E8CC File Offset: 0x0003CACC
		private static int smethod_0(Type type_0)
		{
			object obj = Class7.Class16.object_1;
			int num2;
			lock (obj)
			{
				if (Class7.Class16.dictionary_0 == null)
				{
					Class7.Class16.dictionary_0 = new Dictionary<Type, int>();
				}
				try
				{
					int num = 0;
					if (!Class7.Class16.dictionary_0.TryGetValue(type_0, out num))
					{
						DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(int), Type.EmptyTypes, true);
						ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
						ilgenerator.Emit(OpCodes.Sizeof, type_0);
						ilgenerator.Emit(OpCodes.Ret);
						num = (int)dynamicMethod.Invoke(null, null);
						Class7.Class16.dictionary_0[type_0] = num;
						num2 = num;
					}
					else
					{
						num2 = num;
					}
				}
				catch
				{
					num2 = 0;
				}
			}
			return num2;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003E9A0 File Offset: 0x0003CBA0
		private void method_11(int int_3, Class7.Class18 class18_3)
		{
			this.class18_1[int_3] = this.method_8(class18_3, this.class13_0.fBcjhfhonR[int_3].enum1_0, this.class13_0.fBcjhfhonR[int_3].bool_0);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00036140 File Offset: 0x00034340
		private static Class7.Class19 smethod_1(Class7.Class18 class18_3)
		{
			Class7.Class19 @class = class18_3 as Class7.Class19;
			if (@class == null && class18_3.vmethod_0())
			{
				@class = class18_3.vmethod_7() as Class7.Class19;
			}
			return @class;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		private void method_12(bool bool_4)
		{
			int num = (int)this.object_0;
			MethodBase methodBase = typeof(Class7).Module.ResolveMethod(num);
			MethodInfo methodInfo = methodBase as MethodInfo;
			ParameterInfo[] parameters = methodBase.GetParameters();
			object[] array = new object[parameters.Length];
			Class7.Class18[] array2 = new Class7.Class18[parameters.Length];
			List<Class7.Class14> list = null;
			Class7.Class15 @class = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				Class7.Class18 class2 = this.class32_0.method_4();
				Type type = parameters[parameters.Length - 1 - i].ParameterType;
				object obj = null;
				bool flag = false;
				if (type.IsByRef)
				{
					Class7.Class27 class3 = class2 as Class7.Class27;
					if (class3 != null)
					{
						if (list == null)
						{
							list = new List<Class7.Class14>();
						}
						list.Add(new Class7.Class14(class3.fieldInfo_0, parameters.Length - 1 - i));
						obj = class3.object_0;
						if (!(obj is Class7.Class18))
						{
							flag = true;
							if (obj == null)
							{
								if (type.IsByRef)
								{
									type = type.GetElementType();
								}
								if (type.IsValueType)
								{
									if (!class3.fieldInfo_0.IsStatic)
									{
										obj = Activator.CreateInstance(type);
									}
									else
									{
										obj = class3.fieldInfo_0.GetValue(null);
									}
									if (class2 is Class7.Class25)
									{
										((Class7.Class24)class2).vmethod_11(Class7.Class18.smethod_1(type, obj));
									}
								}
							}
						}
						else
						{
							class2 = obj as Class7.Class18;
						}
					}
				}
				if (!flag)
				{
					if (class2 != null)
					{
						obj = class2.vmethod_4(type);
					}
					if (obj == null)
					{
						if (type.IsByRef)
						{
							type = type.GetElementType();
						}
						if (type.IsValueType)
						{
							obj = Activator.CreateInstance(type);
							if (class2 is Class7.Class25)
							{
								((Class7.Class24)class2).vmethod_11(Class7.Class18.smethod_1(type, obj));
							}
						}
					}
				}
				array2[array.Length - 1 - i] = class2;
				array[array.Length - 1 - i] = obj;
			}
			Class7.Delegate10 @delegate = null;
			if (list != null)
			{
				@class = new Class7.Class15(methodBase, list);
				@delegate = Class7.Class16.smethod_3(methodBase, bool_4, @class);
			}
			else if (methodInfo != null && methodInfo.ReturnType.IsByRef)
			{
				@delegate = Class7.Class16.smethod_2(methodBase, bool_4);
			}
			object obj2 = null;
			Class7.Class18 class4 = null;
			if (!methodBase.IsStatic)
			{
				class4 = this.class32_0.method_4();
				if (class4 != null)
				{
					obj2 = class4.vmethod_4(methodBase.DeclaringType);
				}
				if (obj2 == null)
				{
					Type type2 = methodBase.DeclaringType;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
					}
					if (!type2.IsValueType)
					{
						throw new NullReferenceException();
					}
					obj2 = Activator.CreateInstance(type2);
					if (obj2 == null && Nullable.GetUnderlyingType(type2) != null)
					{
						obj2 = FormatterServices.GetUninitializedObject(type2);
					}
					if (class4 is Class7.Class25)
					{
						((Class7.Class24)class4).vmethod_11(Class7.Class18.smethod_1(type2, obj2));
					}
				}
			}
			object obj3;
			if (methodBase is ConstructorInfo && Nullable.GetUnderlyingType(methodBase.DeclaringType) != null)
			{
				obj3 = array[0];
				if (class4 != null && class4 is Class7.Class25)
				{
					((Class7.Class24)class4).vmethod_11(Class7.Class18.smethod_1(Nullable.GetUnderlyingType(methodBase.DeclaringType), obj3));
				}
			}
			else if (@delegate != null)
			{
				obj3 = @delegate(obj2, array);
			}
			else
			{
				obj3 = methodBase.Invoke(obj2, array);
			}
			for (int j = 0; j < parameters.Length; j++)
			{
				if (parameters[j].ParameterType.IsByRef && (@class == null || !@class.method_1(j)))
				{
					if (!array2[j].method_2())
					{
						if (!(array2[j] is Class7.Class25))
						{
							array2[j].vmethod_9(Class7.Class18.smethod_1(parameters[j].ParameterType, array[j]));
						}
						else
						{
							array2[j].vmethod_9(Class7.Class18.smethod_1(parameters[j].ParameterType.GetElementType(), array[j]));
						}
					}
					else
					{
						((Class7.Class22)array2[j]).method_5(Class7.Class18.smethod_1(parameters[j].ParameterType, array[j]));
					}
				}
			}
			if (methodInfo != null && methodInfo.ReturnType != typeof(void))
			{
				this.class32_0.method_2(Class7.Class18.smethod_1(methodInfo.ReturnType, obj3));
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003EE18 File Offset: 0x0003D018
		private static Class7.Delegate10 smethod_2(object object_7, bool bool_4)
		{
			object obj = Class7.Class16.object_3;
			Class7.Delegate10 delegate3;
			lock (obj)
			{
				Class7.Delegate10 @delegate = null;
				if (bool_4)
				{
					if (Class7.Class16.dictionary_2.TryGetValue(object_7, out @delegate))
					{
						return @delegate;
					}
				}
				else if (Class7.Class16.dictionary_3.TryGetValue(object_7, out @delegate))
				{
					return @delegate;
				}
				MethodInfo methodInfo = object_7 as MethodInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
				{
					typeof(object),
					typeof(object[])
				}, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ParameterInfo[] parameters = object_7.GetParameters();
				Type[] array = new Type[parameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						array[i] = parameters[i].ParameterType;
					}
					else
					{
						array[i] = parameters[i].ParameterType.GetElementType();
					}
				}
				int num = array.Length;
				if (object_7.DeclaringType.IsValueType)
				{
					num++;
				}
				LocalBuilder[] array2 = new LocalBuilder[num];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = ilgenerator.DeclareLocal(array[j]);
				}
				if (object_7.DeclaringType.IsValueType)
				{
					array2[array2.Length - 1] = ilgenerator.DeclareLocal(object_7.DeclaringType.MakeByRefType());
				}
				for (int k = 0; k < array.Length; k++)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					Class7.Class16.smethod_5(ilgenerator, k);
					ilgenerator.Emit(OpCodes.Ldelem_Ref);
					if (array[k].IsValueType)
					{
						ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
					}
					else if (array[k] != typeof(object))
					{
						ilgenerator.Emit(OpCodes.Castclass, array[k]);
					}
					ilgenerator.Emit(OpCodes.Stloc, array2[k]);
				}
				if (!object_7.IsStatic)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
					if (object_7.DeclaringType.IsValueType)
					{
						ilgenerator.Emit(OpCodes.Unbox, object_7.DeclaringType);
						ilgenerator.Emit(OpCodes.Stloc, array2[array2.Length - 1]);
						ilgenerator.Emit(OpCodes.Ldloc_S, array2[array2.Length - 1]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Castclass, object_7.DeclaringType);
					}
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (parameters[l].ParameterType.IsByRef)
					{
						ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
					}
				}
				if (bool_4)
				{
					if (!(methodInfo != null))
					{
						ilgenerator.Emit(OpCodes.Call, object_7 as ConstructorInfo);
					}
					else
					{
						ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
					}
				}
				else if (!(methodInfo != null))
				{
					ilgenerator.Emit(OpCodes.Callvirt, object_7 as ConstructorInfo);
				}
				else
				{
					ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
				}
				if (!(methodInfo == null) && !(methodInfo.ReturnType == typeof(void)))
				{
					if (methodInfo.ReturnType.IsByRef)
					{
						Type elementType = methodInfo.ReturnType.GetElementType();
						if (!elementType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Ldind_Ref, elementType);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldobj, elementType);
						}
						if (elementType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Box, elementType);
						}
					}
					else if (methodInfo.ReturnType.IsValueType)
					{
						ilgenerator.Emit(OpCodes.Box, methodInfo.ReturnType);
					}
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (parameters[m].ParameterType.IsByRef)
					{
						ilgenerator.Emit(OpCodes.Ldarg_1);
						Class7.Class16.smethod_5(ilgenerator, m);
						ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
						if (array2[m].LocalType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
						}
						ilgenerator.Emit(OpCodes.Stelem_Ref);
					}
				}
				ilgenerator.Emit(OpCodes.Ret);
				Class7.Delegate10 delegate2 = (Class7.Delegate10)dynamicMethod.CreateDelegate(typeof(Class7.Delegate10));
				if (!bool_4)
				{
					Class7.Class16.dictionary_3.Add(object_7, delegate2);
				}
				else
				{
					Class7.Class16.dictionary_2.Add(object_7, delegate2);
				}
				delegate3 = delegate2;
			}
			return delegate3;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0003F2C8 File Offset: 0x0003D4C8
		private static Class7.Delegate10 smethod_3(object object_7, bool bool_4, Class7.Class15 class15_0)
		{
			object obj = Class7.Class16.object_4;
			Class7.Delegate10 delegate3;
			lock (obj)
			{
				Class7.Delegate10 @delegate = null;
				if (bool_4)
				{
					if (Class7.Class16.dictionary_4.TryGetValue(class15_0, out @delegate))
					{
						return @delegate;
					}
				}
				else if (Class7.Class16.dictionary_5.TryGetValue(class15_0, out @delegate))
				{
					return @delegate;
				}
				MethodInfo methodInfo = object_7 as MethodInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
				{
					typeof(object),
					typeof(object[])
				}, typeof(Class7), true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ParameterInfo[] parameters = object_7.GetParameters();
				Type[] array = new Type[parameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						array[i] = parameters[i].ParameterType;
					}
					else
					{
						array[i] = parameters[i].ParameterType.GetElementType();
					}
				}
				int num = array.Length;
				if (object_7.DeclaringType.IsValueType)
				{
					num++;
				}
				LocalBuilder[] array2 = new LocalBuilder[num];
				for (int j = 0; j < array.Length; j++)
				{
					if (!class15_0.method_1(j))
					{
						array2[j] = ilgenerator.DeclareLocal(array[j]);
					}
					else
					{
						array2[j] = ilgenerator.DeclareLocal(typeof(object));
					}
				}
				if (object_7.DeclaringType.IsValueType)
				{
					array2[array2.Length - 1] = ilgenerator.DeclareLocal(object_7.DeclaringType.MakeByRefType());
				}
				for (int k = 0; k < array.Length; k++)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					Class7.Class16.smethod_5(ilgenerator, k);
					ilgenerator.Emit(OpCodes.Ldelem_Ref);
					if (!class15_0.method_1(k))
					{
						if (array[k].IsValueType)
						{
							ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
						}
						else if (array[k] != typeof(object))
						{
							ilgenerator.Emit(OpCodes.Castclass, array[k]);
						}
					}
					ilgenerator.Emit(OpCodes.Stloc, array2[k]);
				}
				if (!object_7.IsStatic)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
					if (!object_7.DeclaringType.IsValueType)
					{
						ilgenerator.Emit(OpCodes.Castclass, object_7.DeclaringType);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Unbox, object_7.DeclaringType);
						ilgenerator.Emit(OpCodes.Stloc, array2[array2.Length - 1]);
						ilgenerator.Emit(OpCodes.Ldloc_S, array2[array2.Length - 1]);
					}
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (class15_0.method_1(l))
					{
						Class7.Class14 @class = class15_0.method_0(l);
						if (!@class.object_0.IsStatic)
						{
							if (!@class.object_0.DeclaringType.IsValueType)
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Castclass, @class.object_0.DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, @class.object_0);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Unbox, @class.object_0.DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, @class.object_0);
							}
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldsflda, @class.object_0);
						}
					}
					else if (parameters[l].ParameterType.IsByRef)
					{
						ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
					}
				}
				if (bool_4)
				{
					if (methodInfo != null)
					{
						ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Call, object_7 as ConstructorInfo);
					}
				}
				else if (methodInfo != null)
				{
					ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Callvirt, object_7 as ConstructorInfo);
				}
				if (!(methodInfo == null) && !(methodInfo.ReturnType == typeof(void)))
				{
					if (!methodInfo.ReturnType.IsByRef)
					{
						if (methodInfo.ReturnType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Box, methodInfo.ReturnType);
						}
					}
					else
					{
						Type elementType = methodInfo.ReturnType.GetElementType();
						if (!elementType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Ldind_Ref, elementType);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldobj, elementType);
						}
						if (elementType.IsValueType)
						{
							ilgenerator.Emit(OpCodes.Box, elementType);
						}
					}
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (parameters[m].ParameterType.IsByRef)
					{
						if (!class15_0.method_1(m))
						{
							ilgenerator.Emit(OpCodes.Ldarg_1);
							Class7.Class16.smethod_5(ilgenerator, m);
							ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
							if (array2[m].LocalType.IsValueType)
							{
								ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
							}
							ilgenerator.Emit(OpCodes.Stelem_Ref);
						}
						else
						{
							Class7.Class14 class2 = class15_0.method_0(m);
							if (class2.object_0.IsStatic)
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class7.Class16.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldsfld, class2.object_0);
								if (class2.object_0.FieldType.IsValueType)
								{
									ilgenerator.Emit(OpCodes.Box, class2.object_0.FieldType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class7.Class16.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
								if (array2[m].LocalType.IsValueType)
								{
									ilgenerator.Emit(OpCodes.Box, class2.object_0.FieldType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
						}
					}
				}
				ilgenerator.Emit(OpCodes.Ret);
				Class7.Delegate10 delegate2 = (Class7.Delegate10)dynamicMethod.CreateDelegate(typeof(Class7.Delegate10));
				if (!bool_4)
				{
					Class7.Class16.dictionary_5.Add(class15_0, delegate2);
				}
				else
				{
					Class7.Class16.dictionary_4.Add(class15_0, delegate2);
				}
				delegate3 = delegate2;
			}
			return delegate3;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0003F970 File Offset: 0x0003DB70
		private static Class7.Delegate10 smethod_4(object object_7, bool bool_4, Class7.Class15 class15_0)
		{
			object obj = Class7.Class16.object_5;
			Class7.Delegate10 delegate2;
			lock (obj)
			{
				Class7.Delegate10 @delegate = null;
				if (Class7.Class16.dictionary_6.TryGetValue(class15_0, out @delegate))
				{
					delegate2 = @delegate;
				}
				else
				{
					ConstructorInfo constructorInfo = object_7 as ConstructorInfo;
					DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
					{
						typeof(object),
						typeof(object[])
					}, typeof(Class7), true);
					ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
					ParameterInfo[] parameters = object_7.GetParameters();
					Type[] array = new Type[parameters.Length];
					for (int i = 0; i < array.Length; i++)
					{
						if (parameters[i].ParameterType.IsByRef)
						{
							array[i] = parameters[i].ParameterType.GetElementType();
						}
						else
						{
							array[i] = parameters[i].ParameterType;
						}
					}
					int num = array.Length;
					if (object_7.DeclaringType.IsValueType)
					{
						num++;
					}
					LocalBuilder[] array2 = new LocalBuilder[num];
					for (int j = 0; j < array.Length; j++)
					{
						if (!class15_0.method_1(j))
						{
							array2[j] = ilgenerator.DeclareLocal(array[j]);
						}
						else
						{
							array2[j] = ilgenerator.DeclareLocal(typeof(object));
						}
					}
					if (object_7.DeclaringType.IsValueType)
					{
						array2[array2.Length - 1] = ilgenerator.DeclareLocal(object_7.DeclaringType.MakeByRefType());
					}
					for (int k = 0; k < array.Length; k++)
					{
						ilgenerator.Emit(OpCodes.Ldarg_1);
						Class7.Class16.smethod_5(ilgenerator, k);
						ilgenerator.Emit(OpCodes.Ldelem_Ref);
						if (!class15_0.method_1(k))
						{
							if (!array[k].IsValueType)
							{
								if (array[k] != typeof(object))
								{
									ilgenerator.Emit(OpCodes.Castclass, array[k]);
								}
							}
							else
							{
								ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
							}
						}
						ilgenerator.Emit(OpCodes.Stloc, array2[k]);
					}
					for (int l = 0; l < array.Length; l++)
					{
						if (class15_0.method_1(l))
						{
							Class7.Class14 @class = class15_0.method_0(l);
							if (@class.object_0.IsStatic)
							{
								ilgenerator.Emit(OpCodes.Ldsflda, @class.object_0);
							}
							else if (!@class.object_0.DeclaringType.IsValueType)
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Castclass, @class.object_0.DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, @class.object_0);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Unbox, @class.object_0.DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, @class.object_0);
							}
						}
						else if (!parameters[l].ParameterType.IsByRef)
						{
							ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
						}
					}
					ilgenerator.Emit(OpCodes.Newobj, object_7 as ConstructorInfo);
					if (constructorInfo.DeclaringType.IsValueType)
					{
						ilgenerator.Emit(OpCodes.Box, constructorInfo.DeclaringType);
					}
					for (int m = 0; m < array.Length; m++)
					{
						if (parameters[m].ParameterType.IsByRef)
						{
							if (class15_0.method_1(m))
							{
								Class7.Class14 class2 = class15_0.method_0(m);
								if (class2.object_0.IsStatic)
								{
									ilgenerator.Emit(OpCodes.Ldarg_1);
									Class7.Class16.smethod_5(ilgenerator, m);
									ilgenerator.Emit(OpCodes.Ldsfld, class2.object_0);
									if (class2.object_0.FieldType.IsValueType)
									{
										ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
									}
									ilgenerator.Emit(OpCodes.Stelem_Ref);
								}
								else
								{
									ilgenerator.Emit(OpCodes.Ldarg_1);
									Class7.Class16.smethod_5(ilgenerator, m);
									ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
									if (array2[m].LocalType.IsValueType)
									{
										ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
									}
									ilgenerator.Emit(OpCodes.Stelem_Ref);
								}
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class7.Class16.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
								if (array2[m].LocalType.IsValueType)
								{
									ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
						}
					}
					ilgenerator.Emit(OpCodes.Ret);
					Class7.Delegate10 delegate3 = (Class7.Delegate10)dynamicMethod.CreateDelegate(typeof(Class7.Delegate10));
					Class7.Class16.dictionary_6.Add(class15_0, delegate3);
					delegate2 = delegate3;
				}
			}
			return delegate2;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0003FE9C File Offset: 0x0003E09C
		private static void smethod_5(ILGenerator ilgenerator_0, int int_3)
		{
			switch (int_3)
			{
			case -1:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				ilgenerator_0.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				if (int_3 > -129 && int_3 < 128)
				{
					ilgenerator_0.Emit(OpCodes.Ldc_I4_S, (sbyte)int_3);
					return;
				}
				ilgenerator_0.Emit(OpCodes.Ldc_I4, int_3);
				return;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0003FF7C File Offset: 0x0003E17C
		private static Class7.Class18 smethod_6(Class7.Class18 class18_3)
		{
			if (class18_3.vmethod_7().method_0())
			{
				object obj = class18_3.vmethod_4(null);
				if (obj != null && obj.GetType().IsEnum)
				{
					Type underlyingType = Enum.GetUnderlyingType(obj.GetType());
					object obj2 = Convert.ChangeType(obj, underlyingType);
					Class7.Class18 @class = Class7.Class16.smethod_7(Class7.Class18.smethod_1(underlyingType, obj2));
					if (@class != null)
					{
						return @class as Class7.Class19;
					}
				}
			}
			return class18_3;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		private static Class7.Class19 smethod_7(Class7.Class18 class18_3)
		{
			Class7.Class19 @class = class18_3 as Class7.Class19;
			if (@class == null && class18_3.vmethod_0())
			{
				@class = class18_3.vmethod_7() as Class7.Class19;
			}
			return @class;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00040018 File Offset: 0x0003E218
		private static IntPtr bltXeOrHnB(object object_7)
		{
			if (object_7 == null)
			{
				return IntPtr.Zero;
			}
			if (object_7.method_2())
			{
				return ((Class7.Class22)object_7).method_6();
			}
			if (object_7.vmethod_0())
			{
				Class7.Class24 @class = (Class7.Class24)object_7;
				IntPtr intPtr;
				try
				{
					intPtr = @class.vmethod_10();
				}
				catch
				{
					goto IL_38;
				}
				return intPtr;
			}
			IL_38:
			object obj = object_7.vmethod_4(typeof(IntPtr));
			if (obj != null && obj.GetType() == typeof(IntPtr))
			{
				return (IntPtr)obj;
			}
			throw new Class7.Exception1();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000400B0 File Offset: 0x0003E2B0
		private static object smethod_8(object object_7)
		{
			object obj = Class7.Class16.object_6;
			object obj2;
			lock (obj)
			{
				if (Class7.Class16.dictionary_7 == null)
				{
					Class7.Class16.dictionary_7 = new Dictionary<Type, Class7.Delegate11>();
				}
				if (object_7 != null)
				{
					try
					{
						Type type = object_7.GetType();
						Class7.Delegate11 @delegate;
						if (!Class7.Class16.dictionary_7.TryGetValue(type, out @delegate))
						{
							DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object) }, true);
							ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Unbox_Any, type);
							ilgenerator.Emit(OpCodes.Box, type);
							ilgenerator.Emit(OpCodes.Ret);
							Class7.Delegate11 delegate2 = (Class7.Delegate11)dynamicMethod.CreateDelegate(typeof(Class7.Delegate11));
							Class7.Class16.dictionary_7.Add(type, delegate2);
							return delegate2(object_7);
						}
						return @delegate(object_7);
					}
					catch
					{
						return null;
					}
				}
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000401D4 File Offset: 0x0003E3D4
		private static void smethod_9(IntPtr intptr_0, byte byte_0, int int_3)
		{
			object obj = Class7.Class16.object_6;
			lock (obj)
			{
				if (Class7.Class16.delegate12_0 == null)
				{
					DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
					{
						typeof(IntPtr),
						typeof(byte),
						typeof(int)
					}, typeof(Class7), true);
					ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldarg_2);
					ilgenerator.Emit(OpCodes.Initblk);
					ilgenerator.Emit(OpCodes.Ret);
					Class7.Class16.delegate12_0 = (Class7.Delegate12)dynamicMethod.CreateDelegate(typeof(Class7.Delegate12));
				}
				Class7.Class16.delegate12_0(intptr_0, byte_0, int_3);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000402CC File Offset: 0x0003E4CC
		private static void smethod_10(IntPtr intptr_0, IntPtr intptr_1, uint uint_0)
		{
			if (Class7.Class16.delegate13_0 == null)
			{
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
				{
					typeof(IntPtr),
					typeof(IntPtr),
					typeof(uint)
				}, typeof(Class7), true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Cpblk);
				ilgenerator.Emit(OpCodes.Ret);
				Class7.Class16.delegate13_0 = (Class7.Delegate13)dynamicMethod.CreateDelegate(typeof(Class7.Delegate13));
			}
			Class7.Class16.delegate13_0(intptr_0, intptr_1, uint_0);
		}

		// Token: 0x040006BF RID: 1727
		internal Class7.Class13 class13_0;

		// Token: 0x040006C0 RID: 1728
		internal Class7.Class18[] class18_0 = new Class7.Class18[0];

		// Token: 0x040006C1 RID: 1729
		internal Class7.Class18[] class18_1 = new Class7.Class18[0];

		// Token: 0x040006C2 RID: 1730
		internal Class7.Class32 class32_0 = new Class7.Class32();

		// Token: 0x040006C3 RID: 1731
		internal Class7.Class18 class18_2;

		// Token: 0x040006C4 RID: 1732
		internal Exception exception_0;

		// Token: 0x040006C5 RID: 1733
		internal List<IntPtr> list_0;

		// Token: 0x040006C6 RID: 1734
		private int int_0;

		// Token: 0x040006C7 RID: 1735
		private int int_1;

		// Token: 0x040006C8 RID: 1736
		private int int_2 = -1;

		// Token: 0x040006C9 RID: 1737
		private object object_0;

		// Token: 0x040006CA RID: 1738
		private bool bool_0;

		// Token: 0x040006CB RID: 1739
		private bool bool_1;

		// Token: 0x040006CC RID: 1740
		private bool bool_2;

		// Token: 0x040006CD RID: 1741
		private bool bool_3;

		// Token: 0x040006CE RID: 1742
		private static Dictionary<Type, int> dictionary_0;

		// Token: 0x040006CF RID: 1743
		private static object object_1 = new object();

		// Token: 0x040006D0 RID: 1744
		private static Dictionary<object, Class7.Class18> dictionary_1 = new Dictionary<object, Class7.Class18>();

		// Token: 0x040006D1 RID: 1745
		private static object object_2 = new object();

		// Token: 0x040006D2 RID: 1746
		private static Dictionary<MethodBase, Class7.Delegate10> dictionary_2 = new Dictionary<MethodBase, Class7.Delegate10>();

		// Token: 0x040006D3 RID: 1747
		private static Dictionary<MethodBase, Class7.Delegate10> dictionary_3 = new Dictionary<MethodBase, Class7.Delegate10>();

		// Token: 0x040006D4 RID: 1748
		private static object object_3 = new object();

		// Token: 0x040006D5 RID: 1749
		private static Dictionary<Class7.Class15, Class7.Delegate10> dictionary_4 = new Dictionary<Class7.Class15, Class7.Delegate10>();

		// Token: 0x040006D6 RID: 1750
		private static Dictionary<Class7.Class15, Class7.Delegate10> dictionary_5 = new Dictionary<Class7.Class15, Class7.Delegate10>();

		// Token: 0x040006D7 RID: 1751
		private static object object_4 = new object();

		// Token: 0x040006D8 RID: 1752
		private static Dictionary<Class7.Class15, Class7.Delegate10> dictionary_6 = new Dictionary<Class7.Class15, Class7.Delegate10>();

		// Token: 0x040006D9 RID: 1753
		private static object object_5 = new object();

		// Token: 0x040006DA RID: 1754
		private static Dictionary<Type, Class7.Delegate11> dictionary_7;

		// Token: 0x040006DB RID: 1755
		private static object object_6 = new object();

		// Token: 0x040006DC RID: 1756
		private static Class7.Delegate12 delegate12_0;

		// Token: 0x040006DD RID: 1757
		private static Class7.Delegate13 delegate13_0;

		// Token: 0x0200017D RID: 381
		[CompilerGenerated]
		[Serializable]
		private sealed class Class17
		{
			// Token: 0x060009DE RID: 2526 RVA: 0x0000518D File Offset: 0x0000338D
			internal int method_0(Class7.Class11 x, Class7.Class11 y)
			{
				return x.class12_0.int_0.CompareTo(y.class12_0.int_0);
			}

			// Token: 0x040006DE RID: 1758
			public static readonly Class7.Class16.Class17 <>9 = new Class7.Class16.Class17();

			// Token: 0x040006DF RID: 1759
			public static Comparison<Class7.Class11> <>9__12_0;
		}
	}

	// Token: 0x0200017E RID: 382
	internal enum Enum3 : byte
	{

	}

	// Token: 0x0200017F RID: 383
	internal enum Enum4 : byte
	{

	}

	// Token: 0x02000180 RID: 384
	internal abstract class Class18
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00005440 File Offset: 0x00003640
		public Class18()
		{
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000051AA File Offset: 0x000033AA
		internal bool method_0()
		{
			return this.enum4_0 == (Class7.Enum4)0;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000051B5 File Offset: 0x000033B5
		internal bool method_1()
		{
			return this.enum4_0 == (Class7.Enum4)1;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00040474 File Offset: 0x0003E674
		internal bool method_2()
		{
			return this.enum4_0 == (Class7.Enum4)3 || this.enum4_0 == (Class7.Enum4)4;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000051C0 File Offset: 0x000033C0
		internal bool method_3()
		{
			return this.enum4_0 == (Class7.Enum4)2;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000051CB File Offset: 0x000033CB
		internal bool method_4()
		{
			return this.enum4_0 == (Class7.Enum4)5;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000051D6 File Offset: 0x000033D6
		internal bool nGhvlaVjva()
		{
			return this.enum4_0 == (Class7.Enum4)6;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00003197 File Offset: 0x00001397
		internal virtual bool vmethod_0()
		{
			return false;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00003197 File Offset: 0x00001397
		internal virtual bool vmethod_1()
		{
			return false;
		}

		// Token: 0x060009E8 RID: 2536
		internal abstract void vmethod_2(Class7.Class18 class18_0);

		// Token: 0x060009E9 RID: 2537 RVA: 0x00003197 File Offset: 0x00001397
		internal virtual bool vmethod_3()
		{
			return false;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00040498 File Offset: 0x0003E698
		internal Class18(Class7.Enum4 enum4_1)
		{
			this.enum4_0 = enum4_1;
		}

		// Token: 0x060009EB RID: 2539
		internal abstract object vmethod_4(Type type_0);

		// Token: 0x060009EC RID: 2540
		internal abstract bool vmethod_5(Class7.Class18 class18_0);

		// Token: 0x060009ED RID: 2541
		internal abstract bool BeouTiljCp(Class7.Class18 class18_0);

		// Token: 0x060009EE RID: 2542
		internal abstract bool vmethod_6();

		// Token: 0x060009EF RID: 2543
		internal abstract Class7.Class18 vmethod_7();

		// Token: 0x060009F0 RID: 2544 RVA: 0x00003197 File Offset: 0x00001397
		internal virtual bool vmethod_8()
		{
			return false;
		}

		// Token: 0x060009F1 RID: 2545
		internal abstract void vmethod_9(Class7.Class18 class18_0);

		// Token: 0x060009F2 RID: 2546 RVA: 0x000404B4 File Offset: 0x0003E6B4
		internal static Class7.Enum2 smethod_0(Type type_0)
		{
			Type type = type_0;
			if (!(type != null))
			{
				return (Class7.Enum2)18;
			}
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			if (type != null && Nullable.GetUnderlyingType(type) != null)
			{
				type = Nullable.GetUnderlyingType(type);
			}
			if (type == typeof(string))
			{
				return (Class7.Enum2)14;
			}
			if (type == typeof(byte))
			{
				return (Class7.Enum2)2;
			}
			if (type == typeof(sbyte))
			{
				return (Class7.Enum2)1;
			}
			if (type == typeof(short))
			{
				return (Class7.Enum2)3;
			}
			if (type == typeof(ushort))
			{
				return (Class7.Enum2)4;
			}
			if (type == typeof(int))
			{
				return (Class7.Enum2)5;
			}
			if (type == typeof(uint))
			{
				return (Class7.Enum2)6;
			}
			if (type == typeof(long))
			{
				return (Class7.Enum2)7;
			}
			if (type == typeof(ulong))
			{
				return (Class7.Enum2)8;
			}
			if (type == typeof(float))
			{
				return (Class7.Enum2)9;
			}
			if (type == typeof(double))
			{
				return (Class7.Enum2)10;
			}
			if (type == typeof(bool))
			{
				return (Class7.Enum2)11;
			}
			if (type == typeof(IntPtr))
			{
				return (Class7.Enum2)12;
			}
			if (type == typeof(UIntPtr))
			{
				return (Class7.Enum2)13;
			}
			if (type == typeof(char))
			{
				return (Class7.Enum2)15;
			}
			if (type == typeof(object))
			{
				return (Class7.Enum2)0;
			}
			if (type.IsEnum)
			{
				return (Class7.Enum2)16;
			}
			return (Class7.Enum2)17;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0004067C File Offset: 0x0003E87C
		internal static Class7.Class18 smethod_1(Type type_0, object object_0)
		{
			Class7.Enum2 @enum = Class7.Class18.smethod_0(type_0);
			Class7.Enum2 enum2 = (Class7.Enum2)18;
			if (object_0 != null)
			{
				enum2 = Class7.Class18.smethod_0(object_0.GetType());
			}
			Class7.Class18 @class = null;
			switch (@enum)
			{
			case (Class7.Enum2)0:
				if (enum2 == (Class7.Enum2)15)
				{
					@class = new Class7.Class30(object_0);
				}
				else
				{
					@class = Class7.Class18.smethod_2(object_0);
				}
				break;
			case (Class7.Enum2)1:
				if (enum2 <= (Class7.Enum2)2)
				{
					if (enum2 == (Class7.Enum2)1)
					{
						@class = new Class7.Class20((int)((sbyte)object_0), (Class7.Enum1)1);
						break;
					}
					if (enum2 == (Class7.Enum2)2)
					{
						@class = new Class7.Class20((int)((sbyte)((byte)object_0)), (Class7.Enum1)1);
						break;
					}
				}
				else if (enum2 != (Class7.Enum2)11)
				{
					if (enum2 == (Class7.Enum2)15)
					{
						@class = new Class7.Class20((int)((sbyte)((char)object_0)), (Class7.Enum1)1);
						break;
					}
				}
				else
				{
					if ((bool)object_0)
					{
						@class = new Class7.Class20(1, (Class7.Enum1)1);
						break;
					}
					@class = new Class7.Class20(0, (Class7.Enum1)1);
					break;
				}
				throw new InvalidCastException();
			case (Class7.Enum2)2:
				if (enum2 <= (Class7.Enum2)2)
				{
					if (enum2 == (Class7.Enum2)1)
					{
						@class = new Class7.Class20((int)((byte)((sbyte)object_0)), (Class7.Enum1)2);
						break;
					}
					if (enum2 == (Class7.Enum2)2)
					{
						@class = new Class7.Class20((int)((byte)object_0), (Class7.Enum1)2);
						break;
					}
				}
				else if (enum2 != (Class7.Enum2)11)
				{
					if (enum2 == (Class7.Enum2)15)
					{
						@class = new Class7.Class20((int)((byte)((char)object_0)), (Class7.Enum1)2);
						break;
					}
				}
				else
				{
					if ((bool)object_0)
					{
						@class = new Class7.Class20(1, (Class7.Enum1)2);
						break;
					}
					@class = new Class7.Class20(0, (Class7.Enum1)2);
					break;
				}
				throw new InvalidCastException();
			case (Class7.Enum2)3:
				if (enum2 != (Class7.Enum2)3)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class20((int)((short)((char)object_0)), (Class7.Enum1)3);
					}
					else if (!(bool)object_0)
					{
						@class = new Class7.Class20(0, (Class7.Enum1)3);
					}
					else
					{
						@class = new Class7.Class20(1, (Class7.Enum1)3);
					}
				}
				else
				{
					@class = new Class7.Class20((int)((short)object_0), (Class7.Enum1)3);
				}
				break;
			case (Class7.Enum2)4:
				if (enum2 != (Class7.Enum2)4)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class20((int)((char)object_0), (Class7.Enum1)4);
					}
					else if (!(bool)object_0)
					{
						@class = new Class7.Class20(0, (Class7.Enum1)4);
					}
					else
					{
						@class = new Class7.Class20(1, (Class7.Enum1)4);
					}
				}
				else
				{
					@class = new Class7.Class20((int)((ushort)object_0), (Class7.Enum1)4);
				}
				break;
			case (Class7.Enum2)5:
				if (enum2 != (Class7.Enum2)5)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class20((int)((char)object_0), (Class7.Enum1)5);
					}
					else if ((bool)object_0)
					{
						@class = new Class7.Class20(1, (Class7.Enum1)5);
					}
					else
					{
						@class = new Class7.Class20(0, (Class7.Enum1)5);
					}
				}
				else
				{
					@class = new Class7.Class20((int)object_0, (Class7.Enum1)5);
				}
				break;
			case (Class7.Enum2)6:
				if (enum2 != (Class7.Enum2)6)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class20((uint)((char)object_0), (Class7.Enum1)6);
					}
					else if (!(bool)object_0)
					{
						@class = new Class7.Class20(0U, (Class7.Enum1)6);
					}
					else
					{
						@class = new Class7.Class20(1U, (Class7.Enum1)6);
					}
				}
				else
				{
					@class = new Class7.Class20((uint)object_0, (Class7.Enum1)6);
				}
				break;
			case (Class7.Enum2)7:
				if (enum2 != (Class7.Enum2)7)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class21((long)((ulong)((char)object_0)), (Class7.Enum1)7);
					}
					else if ((bool)object_0)
					{
						@class = new Class7.Class21(1L, (Class7.Enum1)7);
					}
					else
					{
						@class = new Class7.Class21(0L, (Class7.Enum1)7);
					}
				}
				else
				{
					@class = new Class7.Class21((long)object_0, (Class7.Enum1)7);
				}
				break;
			case (Class7.Enum2)8:
				if (enum2 != (Class7.Enum2)8)
				{
					if (enum2 != (Class7.Enum2)11)
					{
						if (enum2 != (Class7.Enum2)15)
						{
							throw new InvalidCastException();
						}
						@class = new Class7.Class21((ulong)((char)object_0), (Class7.Enum1)8);
					}
					else if ((bool)object_0)
					{
						@class = new Class7.Class21(1UL, (Class7.Enum1)8);
					}
					else
					{
						@class = new Class7.Class21(0UL, (Class7.Enum1)8);
					}
				}
				else
				{
					@class = new Class7.Class21((ulong)object_0, (Class7.Enum1)8);
				}
				break;
			case (Class7.Enum2)9:
				if (enum2 != (Class7.Enum2)9)
				{
					throw new InvalidCastException();
				}
				@class = new Class7.Class23((float)object_0);
				break;
			case (Class7.Enum2)10:
				if (enum2 != (Class7.Enum2)10)
				{
					throw new InvalidCastException();
				}
				@class = new Class7.Class23((double)object_0);
				break;
			case (Class7.Enum2)11:
				switch (enum2)
				{
				case (Class7.Enum2)1:
					@class = new Class7.Class20((sbyte)object_0 != 0);
					goto IL_67D;
				case (Class7.Enum2)2:
					@class = new Class7.Class20((byte)object_0 > 0);
					goto IL_67D;
				case (Class7.Enum2)3:
					@class = new Class7.Class20((short)object_0 != 0);
					goto IL_67D;
				case (Class7.Enum2)4:
					@class = new Class7.Class20((ushort)object_0 > 0);
					goto IL_67D;
				case (Class7.Enum2)5:
					@class = new Class7.Class20((int)object_0 != 0);
					goto IL_67D;
				case (Class7.Enum2)6:
					@class = new Class7.Class20((uint)object_0 > 0U);
					goto IL_67D;
				case (Class7.Enum2)7:
					@class = new Class7.Class20((long)object_0 != 0L);
					goto IL_67D;
				case (Class7.Enum2)8:
					@class = new Class7.Class20((ulong)object_0 > 0UL);
					goto IL_67D;
				case (Class7.Enum2)9:
				case (Class7.Enum2)10:
				case (Class7.Enum2)12:
				case (Class7.Enum2)13:
				case (Class7.Enum2)14:
				case (Class7.Enum2)15:
				case (Class7.Enum2)16:
					throw new InvalidCastException();
				case (Class7.Enum2)11:
					@class = new Class7.Class20((bool)object_0);
					goto IL_67D;
				case (Class7.Enum2)18:
					@class = new Class7.Class20(false);
					goto IL_67D;
				}
				@class = new Class7.Class20(object_0 != null);
				break;
			case (Class7.Enum2)12:
				if (enum2 != (Class7.Enum2)12)
				{
					throw new InvalidCastException();
				}
				@class = new Class7.Class22((IntPtr)object_0);
				break;
			case (Class7.Enum2)13:
				if (enum2 != (Class7.Enum2)13)
				{
					throw new InvalidCastException();
				}
				@class = new Class7.Class22((UIntPtr)object_0);
				break;
			case (Class7.Enum2)14:
				@class = new Class7.Class31(object_0 as string);
				break;
			case (Class7.Enum2)15:
				switch (enum2)
				{
				case (Class7.Enum2)1:
					@class = new Class7.Class20((int)((sbyte)object_0), (Class7.Enum1)15);
					break;
				case (Class7.Enum2)2:
					@class = new Class7.Class20((int)((byte)object_0), (Class7.Enum1)15);
					break;
				case (Class7.Enum2)3:
					@class = new Class7.Class20((int)((short)object_0), (Class7.Enum1)15);
					break;
				case (Class7.Enum2)4:
					@class = new Class7.Class20((int)((ushort)object_0), (Class7.Enum1)15);
					break;
				case (Class7.Enum2)5:
					@class = new Class7.Class20((int)object_0, (Class7.Enum1)15);
					break;
				case (Class7.Enum2)6:
					@class = new Class7.Class20((int)((uint)object_0), (Class7.Enum1)15);
					break;
				default:
					if (enum2 != (Class7.Enum2)15)
					{
						throw new InvalidCastException();
					}
					@class = new Class7.Class20((int)((char)object_0), (Class7.Enum1)15);
					break;
				}
				break;
			case (Class7.Enum2)16:
			case (Class7.Enum2)17:
				@class = Class7.Class18.smethod_2(object_0);
				break;
			case (Class7.Enum2)18:
				throw new InvalidCastException();
			}
			IL_67D:
			if (type_0.IsByRef)
			{
				@class = new Class7.Class29(@class, type_0.GetElementType());
			}
			return @class;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00040D28 File Offset: 0x0003EF28
		private static Class7.Class18 smethod_2(object object_0)
		{
			if (object_0 != null && object_0.GetType().IsEnum)
			{
				Type underlyingType = Enum.GetUnderlyingType(object_0.GetType());
				object obj = Convert.ChangeType(object_0, underlyingType);
				Class7.Class18 @class = Class7.Class18.smethod_3(Class7.Class18.smethod_1(underlyingType, obj));
				if (@class != null)
				{
					return @class as Class7.Class19;
				}
			}
			return new Class7.Class30(object_0);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		private static Class7.Class19 smethod_3(Class7.Class18 class18_0)
		{
			Class7.Class19 @class = class18_0 as Class7.Class19;
			if (@class == null && class18_0.vmethod_0())
			{
				@class = class18_0.vmethod_7() as Class7.Class19;
			}
			return @class;
		}

		// Token: 0x040006E2 RID: 1762
		internal Class7.Enum4 enum4_0;
	}

	// Token: 0x02000181 RID: 385
	private class Class30 : Class7.Class18
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00040D80 File Offset: 0x0003EF80
		public Class30()
			: this(null)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00040D94 File Offset: 0x0003EF94
		internal override void vmethod_9(Class7.Class18 class18_1)
		{
			if (!(class18_1 is Class7.Class30))
			{
				this.class18_0 = class18_1.vmethod_7();
				return;
			}
			this.class18_0 = ((Class7.Class30)class18_1).class18_0;
			this.jyuvQjLcik = ((Class7.Class30)class18_1).jyuvQjLcik;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_1)
		{
			this.vmethod_9(class18_1);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00040DD8 File Offset: 0x0003EFD8
		public Class30(object object_0)
			: base((Class7.Enum4)0)
		{
			this.class18_0 = object_0;
			this.jyuvQjLcik = null;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00040DFC File Offset: 0x0003EFFC
		public Class30(object object_0, Type type_0)
			: base((Class7.Enum4)0)
		{
			this.class18_0 = object_0;
			this.jyuvQjLcik = type_0;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00040E20 File Offset: 0x0003F020
		public override string ToString()
		{
			if (this.class18_0 == null)
			{
				return ((Class7.Enum5)5).ToString();
			}
			return this.class18_0.ToString();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00040E54 File Offset: 0x0003F054
		internal override object vmethod_4(Type type_0)
		{
			if (this.class18_0 == null)
			{
				return null;
			}
			if (type_0 != null && type_0.IsByRef)
			{
				type_0 = type_0.GetElementType();
			}
			if (!(this.class18_0 is Class7.Class18))
			{
				object obj = this.class18_0;
				if (obj != null && type_0 != null && obj.GetType() != type_0)
				{
					if (type_0 == typeof(RuntimeFieldHandle) && obj is FieldInfo)
					{
						obj = ((FieldInfo)obj).FieldHandle;
					}
					else if (type_0 == typeof(RuntimeTypeHandle) && obj is Type)
					{
						obj = ((Type)obj).TypeHandle;
					}
					else if (type_0 == typeof(RuntimeMethodHandle) && obj is MethodBase)
					{
						obj = ((MethodBase)obj).MethodHandle;
					}
				}
				return obj;
			}
			if (this.jyuvQjLcik != null)
			{
				return ((Class7.Class18)this.class18_0).vmethod_4(this.jyuvQjLcik);
			}
			object obj2 = ((Class7.Class18)this.class18_0).vmethod_4(type_0);
			if (obj2 != null && type_0 != null && obj2.GetType() != type_0)
			{
				if (type_0 == typeof(RuntimeFieldHandle) && obj2 is FieldInfo)
				{
					obj2 = ((FieldInfo)obj2).FieldHandle;
				}
				else if (type_0 == typeof(RuntimeTypeHandle) && obj2 is Type)
				{
					obj2 = ((Type)obj2).TypeHandle;
				}
				else if (type_0 == typeof(RuntimeMethodHandle) && obj2 is MethodBase)
				{
					obj2 = ((MethodBase)obj2).MethodHandle;
				}
			}
			return obj2;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00041044 File Offset: 0x0003F244
		internal override bool vmethod_5(Class7.Class18 class18_1)
		{
			if (!class18_1.vmethod_0())
			{
				object obj = this.vmethod_4(null);
				object obj2 = class18_1.vmethod_4(null);
				return obj == obj2;
			}
			return ((Class7.Class24)class18_1).vmethod_5(this);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0004107C File Offset: 0x0003F27C
		internal override bool BeouTiljCp(Class7.Class18 class18_1)
		{
			if (!class18_1.vmethod_0())
			{
				object obj = this.vmethod_4(null);
				object obj2 = class18_1.vmethod_4(null);
				return obj != obj2;
			}
			return ((Class7.Class24)class18_1).BeouTiljCp(this);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000410B8 File Offset: 0x0003F2B8
		internal override Class7.Class18 vmethod_7()
		{
			Class7.Class18 @class = this.class18_0 as Class7.Class18;
			if (@class != null)
			{
				return @class.vmethod_7();
			}
			return this;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000410E0 File Offset: 0x0003F2E0
		internal override bool vmethod_6()
		{
			if (this.class18_0 == null)
			{
				return false;
			}
			Class7.Class18 @class = this.class18_0 as Class7.Class18;
			return @class == null || @class.vmethod_4(null) != null;
		}

		// Token: 0x040006E3 RID: 1763
		public Class7.Class18 class18_0;

		// Token: 0x040006E4 RID: 1764
		public Type jyuvQjLcik;
	}

	// Token: 0x02000182 RID: 386
	private class Class31 : Class7.Class18
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x00041118 File Offset: 0x0003F318
		public Class31(string string_1)
			: base((Class7.Enum4)6)
		{
			this.string_0 = string_1;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00041134 File Offset: 0x0003F334
		internal override void vmethod_9(Class7.Class18 class18_0)
		{
			this.string_0 = ((Class7.Class31)class18_0).string_0;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00034FA0 File Offset: 0x000331A0
		internal override void vmethod_2(Class7.Class18 class18_0)
		{
			this.vmethod_9(class18_0);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00041154 File Offset: 0x0003F354
		public override string ToString()
		{
			if (this.string_0 == null)
			{
				return ((Class7.Enum5)5).ToString();
			}
			return '*'.ToString() + this.string_0 + '*'.ToString();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000051E1 File Offset: 0x000033E1
		internal override bool vmethod_6()
		{
			return this.string_0 != null;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000051EC File Offset: 0x000033EC
		internal override object vmethod_4(Type type_0)
		{
			return this.string_0;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0004119C File Offset: 0x0003F39C
		internal override bool vmethod_5(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).vmethod_5(this);
			}
			object obj = this.string_0;
			object obj2 = class18_0.vmethod_4(null);
			return obj == obj2;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000411D4 File Offset: 0x0003F3D4
		internal override bool BeouTiljCp(Class7.Class18 class18_0)
		{
			if (class18_0.vmethod_0())
			{
				return ((Class7.Class24)class18_0).BeouTiljCp(this);
			}
			object obj = this.string_0;
			object obj2 = class18_0.vmethod_4(null);
			return obj != obj2;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00004B70 File Offset: 0x00002D70
		internal override Class7.Class18 vmethod_7()
		{
			return this;
		}

		// Token: 0x040006E5 RID: 1765
		public string string_0;
	}

	// Token: 0x02000183 RID: 387
	internal class Class32
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x000051F4 File Offset: 0x000033F4
		public int method_0()
		{
			return this.list_0.Count;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0004120C File Offset: 0x0003F40C
		public void method_1()
		{
			this.list_0.Clear();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00041224 File Offset: 0x0003F424
		public void method_2(Class7.Class18 class18_0)
		{
			this.list_0.Add(class18_0);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00005201 File Offset: 0x00003401
		public Class7.Class18 method_3()
		{
			return this.list_0[this.list_0.Count - 1];
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000521B File Offset: 0x0000341B
		public Class7.Class18 method_4()
		{
			Class7.Class18 @class = this.method_3();
			if (this.list_0.Count != 0)
			{
				this.list_0.RemoveAt(this.list_0.Count - 1);
			}
			return @class;
		}

		// Token: 0x040006E6 RID: 1766
		private List<Class7.Class18> list_0 = new List<Class7.Class18>();
	}

	// Token: 0x02000184 RID: 388
	private struct d8DB968FE97D4703
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00041260 File Offset: 0x0003F460
		public d8DB968FE97D4703(int int_0, int int_1)
		{
			this.sb = new StringBuilder();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00041260 File Offset: 0x0003F460
		public d8DB968FE97D4703(int int_0, int int_1, IFormatProvider iformatProvider_0)
		{
			this.sb = new StringBuilder();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00041278 File Offset: 0x0003F478
		public void AppendLiteral(string string_0)
		{
			if (string_0 != null)
			{
				this.sb.Append(string_0);
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00005248 File Offset: 0x00003448
		public void AppendFormatted<T>(T value)
		{
			if (value != null)
			{
				this.sb.Append(value);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00005264 File Offset: 0x00003464
		public void AppendFormatted<T>(T value, string string_0)
		{
			if (string_0 != null)
			{
				this.sb.AppendFormat(string_0, value);
				return;
			}
			this.sb.Append(value);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00005248 File Offset: 0x00003448
		public void AppendFormatted<T>(T value, int int_0)
		{
			if (value != null)
			{
				this.sb.Append(value);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000528F File Offset: 0x0000348F
		public void AppendFormatted<T>(T value, int int_0, string string_0)
		{
			if (string_0 != null)
			{
				this.sb.AppendFormat(string_0, value);
				return;
			}
			this.sb.Append(value);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00041278 File Offset: 0x0003F478
		public void AppendFormatted(string string_0)
		{
			if (string_0 != null)
			{
				this.sb.Append(string_0);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00041298 File Offset: 0x0003F498
		public void AppendFormatted(string string_0, int int_0 = 0, string string_1 = null)
		{
			if (string_1 == null)
			{
				this.sb.Append(string_0);
				return;
			}
			this.sb.AppendFormat(string_1, string_0);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000412C4 File Offset: 0x0003F4C4
		public void AppendFormatted(object object_0, int int_0 = 0, string string_0 = null)
		{
			if (string_0 != null)
			{
				this.sb.AppendFormat(string_0, object_0);
				return;
			}
			this.sb.Append(object_0);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000052BA File Offset: 0x000034BA
		public string ToStringAndClear()
		{
			string text = this.sb.ToString();
			this.sb.Clear();
			return text;
		}

		// Token: 0x040006E7 RID: 1767
		private StringBuilder sb;
	}

	// Token: 0x02000185 RID: 389
	internal enum Enum5
	{

	}

	// Token: 0x02000186 RID: 390
	[CompilerGenerated]
	[Serializable]
	private sealed class Class33<T>
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x0000518D File Offset: 0x0000338D
		internal int method_0(Class7.Class11 x, Class7.Class11 y)
		{
			return x.class12_0.int_0.CompareTo(y.class12_0.int_0);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000052D3 File Offset: 0x000034D3
		internal static bool smethod_0()
		{
			return Class7.Class33<T>.object_0 == null;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000052DD File Offset: 0x000034DD
		internal static object smethod_1()
		{
			return Class7.Class33<T>.object_0;
		}

		// Token: 0x040006E9 RID: 1769
		public static readonly Class7.Class33<T> <>9 = new Class7.Class33<T>();

		// Token: 0x040006EA RID: 1770
		public static Comparison<Class7.Class11> <>9__46_0;

		// Token: 0x040006EB RID: 1771
		private static object object_0;
	}
}
