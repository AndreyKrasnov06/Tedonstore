using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
	// Token: 0x02000143 RID: 323
	[CompilerGenerated]
	[Guid("98325047-C671-4174-8D81-DEFCD3F03186")]
	[TypeIdentifier]
	[ComImport]
	public interface INetFwPolicy2
	{
		// Token: 0x060006A3 RID: 1699
		void _VtblGap1_11();

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006A4 RID: 1700
		[DispId(7)]
		INetFwRules Rules
		{
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
