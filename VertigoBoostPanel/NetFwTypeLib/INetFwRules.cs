using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
	// Token: 0x02000146 RID: 326
	[Guid("9C4C6277-5027-441E-AFAE-CA1F542DA009")]
	[CompilerGenerated]
	[TypeIdentifier]
	[ComImport]
	public interface INetFwRules : IEnumerable
	{
		// Token: 0x060006B9 RID: 1721
		void _VtblGap1_1();

		// Token: 0x060006BA RID: 1722
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Add([MarshalAs(UnmanagedType.Interface)] [In] INetFwRule rule);

		// Token: 0x060006BB RID: 1723
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Remove([MarshalAs(UnmanagedType.BStr)] [In] string Name);

		// Token: 0x060006BC RID: 1724
		void _VtblGap2_1();

		// Token: 0x060006BD RID: 1725
		[DispId(-4)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();
	}
}
