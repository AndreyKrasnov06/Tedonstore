using System;
using System.Reflection;

// Token: 0x0200014A RID: 330
internal class Class0
{
	// Token: 0x060006BE RID: 1726 RVA: 0x0003117C File Offset: 0x0002F37C
	internal static void smethod_0(int typemdt)
	{
		Type type = Class0.module_0.ResolveType(33554432 + typemdt);
		foreach (FieldInfo fieldInfo in type.GetFields())
		{
			MethodInfo methodInfo = (MethodInfo)Class0.module_0.ResolveMethod(fieldInfo.MetadataToken + 100663296);
			fieldInfo.SetValue(null, (MulticastDelegate)Delegate.CreateDelegate(type, methodInfo));
		}
	}

	// Token: 0x0400064C RID: 1612
	internal static Module module_0 = typeof(Class0).Assembly.ManifestModule;

	// Token: 0x0200014B RID: 331
	// (Invoke) Token: 0x060006C2 RID: 1730
	internal delegate void Delegate0(object o);
}
