using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;

namespace VertigoBoostPanel.Services.Crypto
{
	// Token: 0x0200008F RID: 143
	public class Encryption
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001995C File Offset: 0x00017B5C
		public static Encryption GetInstance
		{
			get
			{
				if (Encryption.Class == null)
				{
					object syncObject = Encryption.SyncObject;
					lock (syncObject)
					{
						if (Encryption.Class == null)
						{
							Encryption.Class = new Encryption();
						}
					}
				}
				return Encryption.Class;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000199C0 File Offset: 0x00017BC0
		public async Task<string> EncryptString(string text, string password)
		{
			byte[] array = new byte[16];
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			byte[] array2;
			using (SHA256 sha = SHA256.Create())
			{
				array2 = sha.ComputeHash(bytes);
			}
			array2 = array2.Take(32).ToArray<byte>();
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = array2;
			aes.IV = array;
			object obj = new MemoryStream();
			object obj2 = aes.CreateEncryptor();
			if (Encryption.<>o__4.<>p__0 == null)
			{
				Encryption.<>o__4.<>p__0 = CallSite<Func<CallSite, Type, object, object, CryptoStreamMode, CryptoStream>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof(Encryption), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			object obj3 = Encryption.<>o__4.<>p__0.Target(Encryption.<>o__4.<>p__0, typeof(CryptoStream), obj, obj2, CryptoStreamMode.Write);
			byte[] bytes2 = Encoding.ASCII.GetBytes(text);
			if (Encryption.<>o__4.<>p__1 == null)
			{
				Encryption.<>o__4.<>p__1 = CallSite<Action<CallSite, object, byte[], int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(Encryption), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Encryption.<>o__4.<>p__1.Target(Encryption.<>o__4.<>p__1, obj3, bytes2, 0, bytes2.Length);
			if (Encryption.<>o__4.<>p__2 == null)
			{
				Encryption.<>o__4.<>p__2 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "FlushFinalBlock", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Encryption.<>o__4.<>p__2.Target(Encryption.<>o__4.<>p__2, obj3);
			if (Encryption.<>o__4.<>p__4 == null)
			{
				Encryption.<>o__4.<>p__4 = CallSite<Func<CallSite, object, byte[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(byte[]), typeof(Encryption)));
			}
			Func<CallSite, object, byte[]> target = Encryption.<>o__4.<>p__4.Target;
			CallSite <>p__ = Encryption.<>o__4.<>p__4;
			if (Encryption.<>o__4.<>p__3 == null)
			{
				Encryption.<>o__4.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToArray", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			byte[] array3 = target(<>p__, Encryption.<>o__4.<>p__3.Target(Encryption.<>o__4.<>p__3, obj));
			if (Encryption.<>o__4.<>p__5 == null)
			{
				Encryption.<>o__4.<>p__5 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Encryption.<>o__4.<>p__5.Target(Encryption.<>o__4.<>p__5, obj);
			if (Encryption.<>o__4.<>p__6 == null)
			{
				Encryption.<>o__4.<>p__6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Encryption.<>o__4.<>p__6.Target(Encryption.<>o__4.<>p__6, obj3);
			return Convert.ToBase64String(array3, 0, array3.Length);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00019A0C File Offset: 0x00017C0C
		public async Task<string> DecryptString(string text, string password)
		{
			byte[] array = new byte[16];
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			byte[] array2;
			using (SHA256 sha = SHA256.Create())
			{
				array2 = sha.ComputeHash(bytes);
			}
			array2 = array2.Take(32).ToArray<byte>();
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = array2;
			aes.IV = array;
			object obj = new MemoryStream();
			object obj2 = aes.CreateDecryptor();
			if (Encryption.<>o__5.<>p__0 == null)
			{
				Encryption.<>o__5.<>p__0 = CallSite<Func<CallSite, Type, object, object, CryptoStreamMode, CryptoStream>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof(Encryption), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			object obj3 = Encryption.<>o__5.<>p__0.Target(Encryption.<>o__5.<>p__0, typeof(CryptoStream), obj, obj2, CryptoStreamMode.Write);
			string text2 = string.Empty;
			try
			{
				byte[] array3 = Convert.FromBase64String(text);
				if (Encryption.<>o__5.<>p__1 == null)
				{
					Encryption.<>o__5.<>p__1 = CallSite<Action<CallSite, object, byte[], int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(Encryption), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				Encryption.<>o__5.<>p__1.Target(Encryption.<>o__5.<>p__1, obj3, array3, 0, array3.Length);
				if (Encryption.<>o__5.<>p__2 == null)
				{
					Encryption.<>o__5.<>p__2 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "FlushFinalBlock", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Encryption.<>o__5.<>p__2.Target(Encryption.<>o__5.<>p__2, obj3);
				if (Encryption.<>o__5.<>p__4 == null)
				{
					Encryption.<>o__5.<>p__4 = CallSite<Func<CallSite, object, byte[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(byte[]), typeof(Encryption)));
				}
				Func<CallSite, object, byte[]> target = Encryption.<>o__5.<>p__4.Target;
				CallSite <>p__ = Encryption.<>o__5.<>p__4;
				if (Encryption.<>o__5.<>p__3 == null)
				{
					Encryption.<>o__5.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToArray", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				byte[] array4 = target(<>p__, Encryption.<>o__5.<>p__3.Target(Encryption.<>o__5.<>p__3, obj));
				text2 = Encoding.ASCII.GetString(array4, 0, array4.Length);
			}
			finally
			{
				if (Encryption.<>o__5.<>p__5 == null)
				{
					Encryption.<>o__5.<>p__5 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Encryption.<>o__5.<>p__5.Target(Encryption.<>o__5.<>p__5, obj);
				if (Encryption.<>o__5.<>p__6 == null)
				{
					Encryption.<>o__5.<>p__6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(Encryption), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Encryption.<>o__5.<>p__6.Target(Encryption.<>o__5.<>p__6, obj3);
			}
			return text2;
		}

		// Token: 0x04000309 RID: 777
		private static volatile Encryption Class;

		// Token: 0x0400030A RID: 778
		private static object SyncObject = new object();
	}
}
