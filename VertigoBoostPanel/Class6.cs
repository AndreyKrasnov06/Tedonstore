using System;
using System.Reflection;
using System.Windows.Forms;

// Token: 0x0200015D RID: 349
internal static class Class6
{
	// Token: 0x06000794 RID: 1940 RVA: 0x000342EC File Offset: 0x000324EC
	internal static string smethod_0(Assembly assembly)
	{
		if (assembly == typeof(Class6).Assembly)
		{
			return Application.ExecutablePath;
		}
		return assembly.Location;
	}
}
