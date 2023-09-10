using System;
using System.Runtime.Caching;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000AD RID: 173
	internal static class Cache<T>
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		static Cache()
		{
			if (Cache<T>.memoryCache == null)
			{
				Cache<T>.memoryCache = new MemoryCache("memCache", null);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00003B53 File Offset: 0x00001D53
		public static T Get(string key)
		{
			return (T)((object)Cache<T>.memoryCache.Get(key, null));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00003B66 File Offset: 0x00001D66
		public static void Set(string key, T value)
		{
			Cache<T>.memoryCache.Set(key, value, new CacheItemPolicy().AbsoluteExpiration, null);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00003B84 File Offset: 0x00001D84
		internal static bool smethod_0()
		{
			return Cache<T>.object_0 == null;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00003B8E File Offset: 0x00001D8E
		internal static object smethod_1()
		{
			return Cache<T>.object_0;
		}

		// Token: 0x04000376 RID: 886
		private static MemoryCache memoryCache;

		// Token: 0x04000377 RID: 887
		private static object object_0;
	}
}
