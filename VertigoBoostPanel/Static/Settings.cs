using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Services.Optimization;
using VertigoBoostPanel.Services.RegistryInteraction;

namespace VertigoBoostPanel.Static
{
	// Token: 0x02000010 RID: 16
	public class Settings
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00006810 File Offset: 0x00004A10
		public static Settings GetInstance
		{
			get
			{
				if (Settings.Class == null)
				{
					object syncObject = Settings.SyncObject;
					lock (syncObject)
					{
						if (Settings.Class == null)
						{
							Settings.Class = new Settings();
						}
					}
				}
				return Settings.Class;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00006874 File Offset: 0x00004A74
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000068D8 File Offset: 0x00004AD8
		public string DiscordHook
		{
			get
			{
				if (!File.Exists("data\\webhook.txt"))
				{
					return string.Empty;
				}
				string text = File.ReadAllText("data\\webhook.txt").Replace("\n", string.Empty).Replace("\r", string.Empty);
				if (!string.IsNullOrEmpty(text) && text.Length > 0)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				try
				{
					File.WriteAllText("data\\webhook.txt", value);
				}
				catch
				{
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003289 File Offset: 0x00001489
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00006908 File Offset: 0x00004B08
		public bool SendNotifications
		{
			get
			{
				return this._sendNotifications;
			}
			set
			{
				this._sendNotifications = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003291 File Offset: 0x00001491
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000691C File Offset: 0x00004B1C
		public bool Boolean_0
		{
			get
			{
				return this._srtDisabled;
			}
			set
			{
				this._srtDisabled = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00006930 File Offset: 0x00004B30
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00006964 File Offset: 0x00004B64
		public int CompletedSessions
		{
			get
			{
				string universalKey = RegistryService.getUniversalKey("completedSessions");
				if (!string.IsNullOrEmpty(universalKey) && !string.IsNullOrWhiteSpace(universalKey))
				{
					return Convert.ToInt32(universalKey);
				}
				return 0;
			}
			set
			{
				RegistryService.setUniversalKey("completedSessions", value.ToString());
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003299 File Offset: 0x00001499
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00006984 File Offset: 0x00004B84
		public string Language
		{
			get
			{
				return RegistryService.getUniversalKey("Language");
			}
			set
			{
				RegistryService.setUniversalKey("Language", value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000032A5 File Offset: 0x000014A5
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000699C File Offset: 0x00004B9C
		public string SteamPath
		{
			get
			{
				return RegistryService.getUniversalKey("SteamPath");
			}
			set
			{
				RegistryService.setUniversalKey("SteamPath", value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000032B1 File Offset: 0x000014B1
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000069B4 File Offset: 0x00004BB4
		public string CsGoPath
		{
			get
			{
				return RegistryService.getUniversalKey("CsGoPath");
			}
			set
			{
				RegistryService.setUniversalKey("CsGoPath", value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000032BD File Offset: 0x000014BD
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000069CC File Offset: 0x00004BCC
		public string IDLEPath
		{
			get
			{
				return RegistryService.getUniversalKey("IDLEPath");
			}
			set
			{
				RegistryService.setUniversalKey("IDLEPath", value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000069E4 File Offset: 0x00004BE4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00006A08 File Offset: 0x00004C08
		public string Version
		{
			get
			{
				LogService.GetInstance.startVersionControl();
				return RegistryService.getUniversalKey("Version");
			}
			set
			{
				RegistryService.setUniversalKey("Version", value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000032C9 File Offset: 0x000014C9
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00006A20 File Offset: 0x00004C20
		public string TelegramId
		{
			get
			{
				return RegistryService.getUniversalKey("TelegramId");
			}
			set
			{
				RegistryService.setUniversalKey("TelegramId", value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000032D5 File Offset: 0x000014D5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00006A38 File Offset: 0x00004C38
		public string LobbyAtTime
		{
			get
			{
				return RegistryService.getUniversalKey("LobbyAtTime");
			}
			set
			{
				RegistryService.setUniversalKey("LobbyAtTime", value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000032E1 File Offset: 0x000014E1
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00006A50 File Offset: 0x00004C50
		public string Arguments
		{
			get
			{
				return RegistryService.getUniversalKey("Arguments");
			}
			set
			{
				RegistryService.setUniversalKey("Arguments", value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00006A68 File Offset: 0x00004C68
		public string MapToPlay5x5
		{
			get
			{
				return RegistryService.getUniversalKey("MapToPlay5x5");
			}
			set
			{
				RegistryService.setUniversalKey("MapToPlay5x5", value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000032F9 File Offset: 0x000014F9
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00006A80 File Offset: 0x00004C80
		public string MapToPlay2x2
		{
			get
			{
				return RegistryService.getUniversalKey("MapToPlay2x2");
			}
			set
			{
				RegistryService.setUniversalKey("MapToPlay2x2", value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003305 File Offset: 0x00001505
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00006A98 File Offset: 0x00004C98
		public string CompetitiveMode
		{
			get
			{
				return RegistryService.getUniversalKey("CompetitiveMode");
			}
			set
			{
				RegistryService.setUniversalKey("CompetitiveMode", value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003311 File Offset: 0x00001511
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public string StartMonitor
		{
			get
			{
				return RegistryService.getUniversalKey("StartMonitor");
			}
			set
			{
				RegistryService.setUniversalKey("StartMonitor", value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000331D File Offset: 0x0000151D
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public string server_id
		{
			get
			{
				return RegistryService.getUniversalKey("server_id");
			}
			set
			{
				RegistryService.setUniversalKey("server_id", value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003329 File Offset: 0x00001529
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public string Resolution
		{
			get
			{
				return RegistryService.getUniversalKey("Resolution");
			}
			set
			{
				RegistryService.setUniversalKey("Resolution", value);
				WindowAlign.SplitSectors();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003335 File Offset: 0x00001535
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00006B00 File Offset: 0x00004D00
		public string DiscordLogins
		{
			get
			{
				return RegistryService.getUniversalKey("DiscordLogins");
			}
			set
			{
				RegistryService.setUniversalKey("DiscordLogins", value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003341 File Offset: 0x00001541
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00006B18 File Offset: 0x00004D18
		public string CaseFarmAtTime
		{
			get
			{
				return RegistryService.getUniversalKey("CaseFarmAtTime");
			}
			set
			{
				RegistryService.setUniversalKey("CaseFarmAtTime", value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000334D File Offset: 0x0000154D
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00006B30 File Offset: 0x00004D30
		public string DefaultLaunchTab
		{
			get
			{
				return RegistryService.getUniversalKey("DefaultLaunchTab");
			}
			set
			{
				RegistryService.setUniversalKey("DefaultLaunchTab", value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003359 File Offset: 0x00001559
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00006B48 File Offset: 0x00004D48
		public string CustomLaunchDelay
		{
			get
			{
				return RegistryService.getUniversalKey("CustomLaunchDelay");
			}
			set
			{
				RegistryService.setUniversalKey("CustomLaunchDelay", value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00006B60 File Offset: 0x00004D60
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00006B88 File Offset: 0x00004D88
		public bool SuspendOverlay
		{
			get
			{
				return RegistryService.getUniversalKey("SuspendOverlay") == "1";
			}
			set
			{
				if (!value)
				{
					OverlayKiller.GetInstance.Started = false;
					RegistryService.setUniversalKey("SuspendOverlay", "0");
					return;
				}
				OverlayKiller.GetInstance.Started = true;
				RegistryService.setUniversalKey("SuspendOverlay", "1");
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public string BlockedProcess()
		{
			object obj = null;
			foreach (string text in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
			{
				string text2 = "\\";
				string text3 = text.Split(new char[] { text2.ToCharArray()[0] }).Last<string>().ToLower();
				if (text3.Length == 5 && text3.Contains("d") && text3.Contains("ns") && text3.Contains("py"))
				{
					return text3 + ".exe";
				}
			}
			if (Settings.<>o__75.<>p__1 == null)
			{
				Settings.<>o__75.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Settings), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Func<CallSite, object, bool> target = Settings.<>o__75.<>p__1.Target;
			CallSite <>p__ = Settings.<>o__75.<>p__1;
			object obj2 = obj;
			object obj3;
			if (obj2 != null)
			{
				if (Settings.<>o__75.<>p__0 == null)
				{
					Settings.<>o__75.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Contains", null, typeof(Settings), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				obj3 = Settings.<>o__75.<>p__0.Target(Settings.<>o__75.<>p__0, obj2, ".");
			}
			else
			{
				obj3 = null;
			}
			if (!target(<>p__, obj3))
			{
				if (Settings.<>o__75.<>p__3 == null)
				{
					Settings.<>o__75.<>p__3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Settings)));
				}
				return Settings.<>o__75.<>p__3.Target(Settings.<>o__75.<>p__3, obj);
			}
			if (Settings.<>o__75.<>p__2 == null)
			{
				Settings.<>o__75.<>p__2 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Settings)));
			}
			return Settings.<>o__75.<>p__2.Target(Settings.<>o__75.<>p__2, obj);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00006DA8 File Offset: 0x00004FA8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public bool AlignPlayers
		{
			get
			{
				return RegistryService.getUniversalKey("AlignPlayers") == "1";
			}
			set
			{
				if (value)
				{
					RegistryService.setUniversalKey("AlignPlayers", "1");
					return;
				}
				RegistryService.setUniversalKey("AlignPlayers", "0");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00006E00 File Offset: 0x00005000
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00006E28 File Offset: 0x00005028
		public bool Boolean_1
		{
			get
			{
				return RegistryService.getUniversalKey("DelayedSRT") == "1";
			}
			set
			{
				if (value)
				{
					RegistryService.setUniversalKey("DelayedSRT", "1");
					return;
				}
				RegistryService.setUniversalKey("DelayedSRT", "0");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006E58 File Offset: 0x00005058
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00006E80 File Offset: 0x00005080
		public bool RecentInvite
		{
			get
			{
				return RegistryService.getUniversalKey("RecentInvite") == "1";
			}
			set
			{
				if (value)
				{
					RegistryService.setUniversalKey("RecentInvite", "1");
					return;
				}
				RegistryService.setUniversalKey("RecentInvite", "0");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006EB0 File Offset: 0x000050B0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00006ED8 File Offset: 0x000050D8
		public bool NoGraphics
		{
			get
			{
				return RegistryService.getUniversalKey("NoGraphics") == "1";
			}
			set
			{
				if (!value)
				{
					RegistryService.setUniversalKey("NoGraphics", "0");
					return;
				}
				RegistryService.setUniversalKey("NoGraphics", "1");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006F08 File Offset: 0x00005108
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00006F30 File Offset: 0x00005130
		public bool MouseEnabled
		{
			get
			{
				return RegistryService.getUniversalKey("MouseEnabled") == "1";
			}
			set
			{
				if (value)
				{
					RegistryService.setUniversalKey("MouseEnabled", "1");
					return;
				}
				RegistryService.setUniversalKey("MouseEnabled", "0");
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006F60 File Offset: 0x00005160
		public async Task<bool> ValidateDates(DateTime _1, DateTime _2)
		{
			Settings.<>c__DisplayClass91_0 CS$<>8__locals1 = new Settings.<>c__DisplayClass91_0();
			CS$<>8__locals1._1 = _1;
			CS$<>8__locals1._2 = _2;
			object obj = Task.Run<double>(delegate
			{
				Settings.<>c__DisplayClass91_0.<<ValidateDates>b__0>d <<ValidateDates>b__0>d;
				<<ValidateDates>b__0>d.<>t__builder = AsyncTaskMethodBuilder<double>.Create();
				<<ValidateDates>b__0>d.<>4__this = CS$<>8__locals1;
				<<ValidateDates>b__0>d.<>1__state = -1;
				<<ValidateDates>b__0>d.<>t__builder.Start<Settings.<>c__DisplayClass91_0.<<ValidateDates>b__0>d>(ref <<ValidateDates>b__0>d);
				return <<ValidateDates>b__0>d.<>t__builder.Task;
			});
			if (Settings.<>o__91.<>p__1 == null)
			{
				Settings.<>o__91.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Settings)));
			}
			Func<CallSite, object, int> target = Settings.<>o__91.<>p__1.Target;
			CallSite <>p__ = Settings.<>o__91.<>p__1;
			if (Settings.<>o__91.<>p__0 == null)
			{
				Settings.<>o__91.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Result", typeof(Settings), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			int num = target(<>p__, Settings.<>o__91.<>p__0.Target(Settings.<>o__91.<>p__0, obj));
			Console.WriteLine("interval: " + num.ToString());
			return num > 60;
		}

		// Token: 0x04000038 RID: 56
		private static volatile Settings Class;

		// Token: 0x04000039 RID: 57
		private static object SyncObject = new object();

		// Token: 0x0400003A RID: 58
		private bool _sendNotifications = true;

		// Token: 0x0400003B RID: 59
		private bool _srtDisabled;

		// Token: 0x0400003C RID: 60
		public long PanelOffset;
	}
}
