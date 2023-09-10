using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;

namespace VertigoBoostPanel.Core.RuntimeCompiler
{
	// Token: 0x02000114 RID: 276
	public static class Compiler
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x00029FB0 File Offset: 0x000281B0
		public static void CompileAndRunCheckCode(string code)
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00029FC0 File Offset: 0x000281C0
		public static void CompileAndRunCheckThread(string code)
		{
			ICodeCompiler codeCompiler = new CSharpCodeProvider().CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters
			{
				GenerateExecutable = false,
				GenerateInMemory = true,
				IncludeDebugInformation = false
			};
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, code);
			foreach (object obj in compilerResults.Errors)
			{
				Console.WriteLine(((CompilerError)obj).ErrorText);
			}
			try
			{
				Type type = compilerResults.CompiledAssembly.GetType("C", true, true);
				object obj2 = Activator.CreateInstance(type);
				MethodBase method = type.GetMethod("M");
				object obj3 = obj2;
				object[] array = new string[] { Directory.GetCurrentDirectory() };
				method.Invoke(obj3, array);
				Compiler.ClearTemp();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002A0B4 File Offset: 0x000282B4
		private static void ClearTemp()
		{
		}
	}
}
