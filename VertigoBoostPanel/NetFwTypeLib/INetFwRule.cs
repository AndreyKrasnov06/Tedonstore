using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
	// Token: 0x02000144 RID: 324
	[Guid("AF230D27-BABA-4E42-ACED-F524F22CFCE2")]
	[CompilerGenerated]
	[TypeIdentifier]
	[ComImport]
	public interface INetFwRule
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006A5 RID: 1701
		// (set) Token: 0x060006A6 RID: 1702
		[DispId(1)]
		string Name
		{
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x060006A7 RID: 1703
		void _VtblGap1_6();

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006A8 RID: 1704
		// (set) Token: 0x060006A9 RID: 1705
		[DispId(5)]
		int Protocol
		{
			[DispId(5)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(5)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060006AA RID: 1706
		void _VtblGap2_2();

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006AB RID: 1707
		// (set) Token: 0x060006AC RID: 1708
		[DispId(7)]
		string RemotePorts
		{
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x060006AD RID: 1709
		void _VtblGap3_2();

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006AE RID: 1710
		// (set) Token: 0x060006AF RID: 1711
		[DispId(9)]
		string RemoteAddresses
		{
			[DispId(9)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(9)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x060006B0 RID: 1712
		void _VtblGap4_2();

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006B1 RID: 1713
		// (set) Token: 0x060006B2 RID: 1714
		[DispId(11)]
		NET_FW_RULE_DIRECTION_ Direction
		{
			[DispId(11)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(11)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060006B3 RID: 1715
		void _VtblGap5_4();

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006B4 RID: 1716
		// (set) Token: 0x060006B5 RID: 1717
		[DispId(14)]
		bool Enabled
		{
			[DispId(14)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(14)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060006B6 RID: 1718
		void _VtblGap6_6();

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006B7 RID: 1719
		// (set) Token: 0x060006B8 RID: 1720
		[DispId(18)]
		NET_FW_ACTION_ Action
		{
			[DispId(18)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(18)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}
	}
}
