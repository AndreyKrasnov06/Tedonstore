using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace VertigoBoostPanel.Properties
{
	// Token: 0x02000009 RID: 9
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000031E0 File Offset: 0x000013E0
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000031E7 File Offset: 0x000013E7
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00005AA0 File Offset: 0x00003CA0
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("en-US")]
		public CultureInfo DefaultLanguage
		{
			get
			{
				return (CultureInfo)this["DefaultLanguage"];
			}
			set
			{
				this["DefaultLanguage"] = value;
			}
		}

		// Token: 0x04000013 RID: 19
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
