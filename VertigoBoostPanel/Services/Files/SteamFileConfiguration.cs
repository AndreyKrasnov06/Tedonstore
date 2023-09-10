using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services.Files
{
	// Token: 0x0200009E RID: 158
	public class SteamFileConfiguration
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001B0EC File Offset: 0x000192EC
		public static SteamFileConfiguration GetInstance
		{
			get
			{
				if (SteamFileConfiguration.Class == null)
				{
					object syncObject = SteamFileConfiguration.SyncObject;
					lock (syncObject)
					{
						if (SteamFileConfiguration.Class == null)
						{
							SteamFileConfiguration.Class = new SteamFileConfiguration();
						}
					}
				}
				return SteamFileConfiguration.Class;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001B150 File Offset: 0x00019350
		public async Task<ConfigurationResult> ConfigureSteamFiles(Player player)
		{
			ConfigurationResult configurationResult;
			if (!string.IsNullOrEmpty(player.string_0) && !string.IsNullOrWhiteSpace(player.string_0))
			{
				if (!string.IsNullOrEmpty(player.string_1) && !string.IsNullOrWhiteSpace(player.string_1))
				{
					try
					{
						if (File.Exists(Settings.GetInstance.SteamPath + "\\steam.exe") && !File.Exists(Settings.GetInstance.SteamPath + "\\steam_" + player.string_0 + ".exe"))
						{
							File.Copy(Settings.GetInstance.SteamPath + "\\steam.exe", Settings.GetInstance.SteamPath + "\\steam_" + player.string_0 + ".exe");
							this.SetFirewallRuleForExe(Settings.GetInstance.SteamPath + "\\steam_" + player.string_0 + ".exe");
						}
						if (!Directory.Exists(Settings.GetInstance.CsGoPath) || !Directory.Exists(Settings.GetInstance.SteamPath))
						{
							return ConfigurationResult.ERROR;
						}
						this.CopySteamFiles(Settings.GetInstance.SteamPath + "\\", Settings.GetInstance.CsGoPath + "\\", player.Login, player.string_0);
						if (!Directory.Exists(Settings.GetInstance.CsGoPath + "\\csgo\\log"))
						{
							Directory.CreateDirectory(Settings.GetInstance.CsGoPath + "\\csgo\\log");
						}
						if (!File.Exists(Settings.GetInstance.CsGoPath + "\\csgo\\cfg\\gamestate_integration_GSI.cfg"))
						{
							File.Copy("cfg\\game\\cfg\\gamestate_integration_GSI.cfg", Settings.GetInstance.CsGoPath + "\\csgo\\cfg\\gamestate_integration_GSI.cfg");
						}
						if (!File.Exists(Settings.GetInstance.SteamPath + "\\steam.cfg"))
						{
							File.WriteAllLines(Settings.GetInstance.SteamPath + "\\steam.cfg", new string[] { "BootStrapperInhibitAll=enable", "BootStrapperForceSelfUpdate = disable", "#ForceOfflineMode=enable" });
						}
						string text = Settings.GetInstance.SteamPath + "\\userdata\\" + player.string_0 + "\\730\\local\\cfg\\config.cfg";
						FileSecurity accessControl = File.GetAccessControl(text);
						SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
						accessControl.AddAccessRule(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
						File.SetAccessControl(text, accessControl);
						this.SetFileReadAccess(text, false);
						string[] array = File.ReadAllLines(Settings.GetInstance.SteamPath + "\\userdata\\" + player.string_0 + "\\730\\local\\cfg\\config.cfg");
						for (int i = 0; i < array.Length; i++)
						{
							if (!array[i].Contains("maplist_8_10"))
							{
								if (!array[i].Contains("maplist_2v2"))
								{
									if (!array[i].Contains("ui_playsettings_prime"))
									{
										if (array[i].Contains("ui_playsettings_mode_official_v20"))
										{
											if (Settings.GetInstance.DefaultLaunchTab == "2x2")
											{
												array[i] = "ui_playsettings_mode_official_v20 \"scrimcomp2v2\"";
											}
											else
											{
												array[i] = "ui_playsettings_mode_official_v20 \"competitive\"";
											}
										}
									}
									else if (!(Settings.GetInstance.CompetitiveMode == "Unranked"))
									{
										array[i] = "ui_playsettings_prime \"1\"";
									}
									else
									{
										array[i] = "ui_playsettings_prime \"0\"";
									}
								}
								else
								{
									array[i] = "player_competitive_maplist_2v2_10_0_E8C782EC \"" + Settings.GetInstance.MapToPlay2x2 + "\"";
								}
							}
							else
							{
								array[i] = "player_competitive_maplist_8_10_0_C9C8D674 \"" + Settings.GetInstance.MapToPlay5x5 + "\"";
							}
						}
						File.WriteAllLines(Settings.GetInstance.SteamPath + "\\userdata\\" + player.string_0 + "\\730\\local\\cfg\\config.cfg", array);
						this.SetFileReadAccess(text, true);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "VertigoBoostPanel");
						return ConfigurationResult.ERROR;
					}
					configurationResult = ConfigurationResult.OK;
				}
				else
				{
					configurationResult = ConfigurationResult.WRONG_ID_64;
				}
			}
			else
			{
				configurationResult = ConfigurationResult.WRONG_ID_32;
			}
			return configurationResult;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001B19C File Offset: 0x0001939C
		private void SetFirewallRuleForExe(string pathToExe)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = "/c netsh advfirewall firewall add rule name=\"VertigoBoost Rule\" dir=in action=allow program=\"" + pathToExe + "\"",
				UseShellExecute = false,
				Verb = "runas",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true
			});
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001B1F8 File Offset: 0x000193F8
		public void CopySteamFiles(string steamPath, string csgoPath, string login, string string_0)
		{
			this.CreateAccountUserdataDirectories(steamPath, string_0);
			string text = steamPath + "userdata\\" + string_0 + "\\730\\local\\cfg\\";
			this.CopyConfigurationFile(text, "config.cfg", true);
			this.CopyConfigurationFile(text, "video.txt", true);
			this.CopyConfigurationFile(text, "videodefaults.txt", true);
			text = steamPath + "userdata\\" + string_0 + "\\730\\";
			this.CopyConfigurationFile(text, "remotecache_730.vdf", true);
			text = steamPath + "userdata\\" + string_0 + "\\7\\remote\\";
			this.CopyConfigurationFile(text, "sharedconfig.vdf", true);
			this.CopyConfigurationFile(text, "serverbrowser_hist.vdf", true);
			text = steamPath + "userdata\\" + string_0 + "\\7\\";
			this.CopyConfigurationFile(text, "remotecache_7.vdf", true);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001B2C0 File Offset: 0x000194C0
		private void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
		{
			SteamFileConfiguration.<>c__DisplayClass7_0 CS$<>8__locals1 = new SteamFileConfiguration.<>c__DisplayClass7_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.e = e;
			object obj = Task.Run(delegate
			{
				SteamFileConfiguration.<>c__DisplayClass7_0.<<startWatch_EventArrived>b__0>d <<startWatch_EventArrived>b__0>d;
				<<startWatch_EventArrived>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<startWatch_EventArrived>b__0>d.<>4__this = CS$<>8__locals1;
				<<startWatch_EventArrived>b__0>d.<>1__state = -1;
				<<startWatch_EventArrived>b__0>d.<>t__builder.Start<SteamFileConfiguration.<>c__DisplayClass7_0.<<startWatch_EventArrived>b__0>d>(ref <<startWatch_EventArrived>b__0>d);
				return <<startWatch_EventArrived>b__0>d.<>t__builder.Task;
			});
			if (SteamFileConfiguration.<>o__7.<>p__0 == null)
			{
				SteamFileConfiguration.<>o__7.<>p__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			SteamFileConfiguration.<>o__7.<>p__0.Target(SteamFileConfiguration.<>o__7.<>p__0, obj);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001B344 File Offset: 0x00019544
		private async Task checkProcess(EventArrivedEventArgs e)
		{
			if (e.NewEvent.Properties["ProcessName"].Value.ToString().ToLower() == Settings.GetInstance.BlockedProcess())
			{
				object obj = Settings.GetInstance.BlockedProcess();
				if (SteamFileConfiguration.<>o__8.<>p__1 == null)
				{
					SteamFileConfiguration.<>o__8.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Func<CallSite, object, bool> target = SteamFileConfiguration.<>o__8.<>p__1.Target;
				CallSite <>p__ = SteamFileConfiguration.<>o__8.<>p__1;
				if (SteamFileConfiguration.<>o__8.<>p__0 == null)
				{
					SteamFileConfiguration.<>o__8.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (!target(<>p__, SteamFileConfiguration.<>o__8.<>p__0.Target(SteamFileConfiguration.<>o__8.<>p__0, obj, null)))
				{
					if (SteamFileConfiguration.<>o__8.<>p__4 == null)
					{
						SteamFileConfiguration.<>o__8.<>p__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, int, object> target2 = SteamFileConfiguration.<>o__8.<>p__4.Target;
					CallSite <>p__2 = SteamFileConfiguration.<>o__8.<>p__4;
					if (SteamFileConfiguration.<>o__8.<>p__3 == null)
					{
						SteamFileConfiguration.<>o__8.<>p__3 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetProcessesByName", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, Type, object, object> target3 = SteamFileConfiguration.<>o__8.<>p__3.Target;
					CallSite <>p__3 = SteamFileConfiguration.<>o__8.<>p__3;
					Type typeFromHandle = typeof(Process);
					if (SteamFileConfiguration.<>o__8.<>p__2 == null)
					{
						SteamFileConfiguration.<>o__8.<>p__2 = CallSite<Func<CallSite, object, string, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Replace", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					object obj2 = target2(<>p__2, target3(<>p__3, typeFromHandle, SteamFileConfiguration.<>o__8.<>p__2.Target(SteamFileConfiguration.<>o__8.<>p__2, obj, ".exe", "")), 0);
					try
					{
						if (SteamFileConfiguration.<>o__8.<>p__6 == null)
						{
							SteamFileConfiguration.<>o__8.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, bool> target4 = SteamFileConfiguration.<>o__8.<>p__6.Target;
						CallSite <>p__4 = SteamFileConfiguration.<>o__8.<>p__6;
						if (SteamFileConfiguration.<>o__8.<>p__5 == null)
						{
							SteamFileConfiguration.<>o__8.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						if (target4(<>p__4, SteamFileConfiguration.<>o__8.<>p__5.Target(SteamFileConfiguration.<>o__8.<>p__5, obj2, null)))
						{
							if (SteamFileConfiguration.<>o__8.<>p__7 == null)
							{
								SteamFileConfiguration.<>o__8.<>p__7 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Kill", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							SteamFileConfiguration.<>o__8.<>p__7.Target(SteamFileConfiguration.<>o__8.<>p__7, obj2);
							object obj3 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Valve\\Steam");
							if (SteamFileConfiguration.<>o__8.<>p__13 == null)
							{
								SteamFileConfiguration.<>o__8.<>p__13 = CallSite<Func<CallSite, object, IDisposable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(IDisposable), typeof(SteamFileConfiguration)));
							}
							object obj6;
							using (SteamFileConfiguration.<>o__8.<>p__13.Target(SteamFileConfiguration.<>o__8.<>p__13, obj3))
							{
								if (SteamFileConfiguration.<>o__8.<>p__9 == null)
								{
									SteamFileConfiguration.<>o__8.<>p__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, bool> target5 = SteamFileConfiguration.<>o__8.<>p__9.Target;
								CallSite <>p__5 = SteamFileConfiguration.<>o__8.<>p__9;
								if (SteamFileConfiguration.<>o__8.<>p__8 == null)
								{
									SteamFileConfiguration.<>o__8.<>p__8 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object obj4;
								if (target5(<>p__5, SteamFileConfiguration.<>o__8.<>p__8.Target(SteamFileConfiguration.<>o__8.<>p__8, obj3, null)))
								{
									if (SteamFileConfiguration.<>o__8.<>p__10 == null)
									{
										SteamFileConfiguration.<>o__8.<>p__10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetValue", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									obj4 = SteamFileConfiguration.<>o__8.<>p__10.Target(SteamFileConfiguration.<>o__8.<>p__10, obj3, "SteamPath");
								}
								else
								{
									obj4 = null;
								}
								object obj5 = obj4 as string;
								if (SteamFileConfiguration.<>o__8.<>p__12 == null)
								{
									SteamFileConfiguration.<>o__8.<>p__12 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, bool> target6 = SteamFileConfiguration.<>o__8.<>p__12.Target;
								CallSite <>p__6 = SteamFileConfiguration.<>o__8.<>p__12;
								if (SteamFileConfiguration.<>o__8.<>p__11 == null)
								{
									SteamFileConfiguration.<>o__8.<>p__11 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								if (!target6(<>p__6, SteamFileConfiguration.<>o__8.<>p__11.Target(SteamFileConfiguration.<>o__8.<>p__11, obj5, null)))
								{
									obj6 = obj5;
								}
								else
								{
									obj6 = "";
								}
							}
							if (SteamFileConfiguration.<>o__8.<>p__16 == null)
							{
								SteamFileConfiguration.<>o__8.<>p__16 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, bool> target7 = SteamFileConfiguration.<>o__8.<>p__16.Target;
							CallSite <>p__7 = SteamFileConfiguration.<>o__8.<>p__16;
							if (SteamFileConfiguration.<>o__8.<>p__15 == null)
							{
								SteamFileConfiguration.<>o__8.<>p__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target8 = SteamFileConfiguration.<>o__8.<>p__15.Target;
							CallSite <>p__8 = SteamFileConfiguration.<>o__8.<>p__15;
							if (SteamFileConfiguration.<>o__8.<>p__14 == null)
							{
								SteamFileConfiguration.<>o__8.<>p__14 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsNullOrEmpty", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							if (target7(<>p__7, target8(<>p__8, SteamFileConfiguration.<>o__8.<>p__14.Target(SteamFileConfiguration.<>o__8.<>p__14, typeof(string), obj6))))
							{
								Process.Start(new ProcessStartInfo
								{
									WindowStyle = ProcessWindowStyle.Hidden,
									FileName = "cmd.exe",
									Arguments = string.Format("/c \"{0}\\uninstall.exe\"", obj6)
								});
							}
							Process.Start(new ProcessStartInfo
							{
								WindowStyle = ProcessWindowStyle.Hidden,
								FileName = "cmd.exe",
								Arguments = string.Format("/c taskkill /f /pid \"{0}\"", SteamGuardService.CurrentProcessId),
								Verb = "runas"
							});
						}
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001B388 File Offset: 0x00019588
		public void Init()
		{
			object obj = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
			if (SteamFileConfiguration.<>o__9.<>p__0 == null)
			{
				SteamFileConfiguration.<>o__9.<>p__0 = CallSite<Func<CallSite, object, ManagementEventWatcher>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(ManagementEventWatcher), typeof(SteamFileConfiguration)));
			}
			SteamFileConfiguration.<>o__9.<>p__0.Target(SteamFileConfiguration.<>o__9.<>p__0, obj).EventArrived += this.startWatch_EventArrived;
			if (SteamFileConfiguration.<>o__9.<>p__1 == null)
			{
				SteamFileConfiguration.<>o__9.<>p__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Start", null, typeof(SteamFileConfiguration), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			SteamFileConfiguration.<>o__9.<>p__1.Target(SteamFileConfiguration.<>o__9.<>p__1, obj);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001B44C File Offset: 0x0001964C
		private void CopyConfigurationFile(string path, string fileName, bool setReadOnly)
		{
			string text = fileName;
			fileName = fileName.Replace("_730", "").Replace("_7", "");
			if (!File.Exists(path + fileName))
			{
				File.Copy("cfg/userdata/" + text, path + fileName, true);
			}
			else
			{
				this.RestoreFile(path + fileName);
				if (!this.IsFileReadOnly(path + fileName))
				{
					if (this.SHA256CheckSum(path + fileName) != this.SHA256CheckSum("cfg/userdata/" + text))
					{
						File.Delete(path + fileName);
						File.Copy("cfg/userdata/" + text, path + fileName, true);
					}
				}
				else
				{
					this.SetFileReadAccess(path + fileName, false);
					if (this.SHA256CheckSum(path + fileName) != this.SHA256CheckSum("cfg/userdata/" + text))
					{
						File.Delete(path + fileName);
						File.Copy("cfg/userdata/" + text, path + fileName, true);
					}
				}
			}
			if (setReadOnly)
			{
				this.SetFileReadAccess(path + fileName, true);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001B580 File Offset: 0x00019780
		private void RestoreFile(string path)
		{
			try
			{
				FileSecurity accessControl = File.GetAccessControl(path);
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
				accessControl.AddAccessRule(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
				File.SetAccessControl(path, accessControl);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "VertigoBoostPanel");
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public string SHA256CheckSum(string filePath)
		{
			string text;
			using (SHA256 sha = SHA256.Create())
			{
				using (FileStream fileStream = File.OpenRead(filePath))
				{
					text = Convert.ToBase64String(sha.ComputeHash(fileStream));
				}
			}
			return text;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001B644 File Offset: 0x00019844
		public string CalculateMD5(string input)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(input);
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
			string text = "";
			foreach (byte b in array)
			{
				text += b.ToString("x2");
			}
			return text;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001B6A4 File Offset: 0x000198A4
		private void SetFileReadAccess(string fileName, bool setReadOnly)
		{
			new FileInfo(fileName).IsReadOnly = setReadOnly;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00003B22 File Offset: 0x00001D22
		private bool IsFileReadOnly(string fileName)
		{
			return new FileInfo(fileName).IsReadOnly;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001B6C0 File Offset: 0x000198C0
		private void CreateAccountUserdataDirectories(string steamPath, string string_0)
		{
			Directory.CreateDirectory(steamPath + "userdata\\" + string_0 + "\\");
			Directory.CreateDirectory(steamPath + "userdata\\" + string_0 + "\\730\\");
			Directory.CreateDirectory(steamPath + "userdata\\" + string_0 + "\\730\\local\\");
			Directory.CreateDirectory(steamPath + "userdata\\" + string_0 + "\\730\\local\\cfg\\");
			Directory.CreateDirectory(steamPath + "userdata\\" + string_0 + "\\7\\remote\\");
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001B740 File Offset: 0x00019940
		private void CopyDirectory(string source, string destination)
		{
			if (Directory.Exists(destination))
			{
				Directory.Delete(destination, true);
				Directory.CreateDirectory(destination);
				DirectoryInfo directoryInfo = new DirectoryInfo(source);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name));
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					this.CopyDirectory(Path.Combine(source, directoryInfo2.Name), Path.Combine(destination, directoryInfo2.Name));
				}
				return;
			}
			Directory.CreateDirectory(destination);
			DirectoryInfo directoryInfo3 = new DirectoryInfo(source);
			foreach (FileInfo fileInfo2 in directoryInfo3.GetFiles())
			{
				fileInfo2.CopyTo(Path.Combine(destination, fileInfo2.Name));
			}
			foreach (DirectoryInfo directoryInfo4 in directoryInfo3.GetDirectories())
			{
				this.CopyDirectory(Path.Combine(source, directoryInfo4.Name), Path.Combine(destination, directoryInfo4.Name));
			}
		}

		// Token: 0x0400033F RID: 831
		private static volatile SteamFileConfiguration Class;

		// Token: 0x04000340 RID: 832
		private static object SyncObject = new object();
	}
}
