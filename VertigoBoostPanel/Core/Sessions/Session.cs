using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CSGSI.Nodes;
using ItemsTransfer.SteamAuth;
using ItemsTransfer.Trade;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core.BoostLogs;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.GSI;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Core.WalkBot;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Model.enums;
using VertigoBoostPanel.Model.GSI;
using VertigoBoostPanel.Services.Api;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.Core.Sessions
{
	// Token: 0x020000ED RID: 237
	public class Session
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public Session(BoostTask boostTask)
		{
			this._boostTask = boostTask;
			this.gsi = new LobbyGSI();
			this.gsi.title = this._boostTask.Name;
			foreach (Player player in this._boostTask.FirstTeam)
			{
				this.gsi.SteamIds.Add(player.string_1);
			}
			foreach (Player player2 in this._boostTask.SecondTeam)
			{
				this.gsi.SteamIds.Add(player2.string_1);
			}
			GSI.ActiveLobbies.Add(this.gsi);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00003D74 File Offset: 0x00001F74
		private List<Player> firstTeam
		{
			get
			{
				return this._boostTask.FirstTeam;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00003D81 File Offset: 0x00001F81
		private List<Player> secondTeam
		{
			get
			{
				return this._boostTask.SecondTeam;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00003D8E File Offset: 0x00001F8E
		private Player firstTeamLeader
		{
			get
			{
				return this._boostTask.FirstTeam[0];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00003DA1 File Offset: 0x00001FA1
		private Player secondTeamLeader
		{
			get
			{
				return this._boostTask.SecondTeam[0];
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00020FE8 File Offset: 0x0001F1E8
		private async Task CollectLobby(int gameCountFactor = 1, bool client_boost = false, int TeamToInviteClient = 0)
		{
			Session.<>c__DisplayClass25_0 CS$<>8__locals1 = new Session.<>c__DisplayClass25_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.client_boost = client_boost;
			CS$<>8__locals1.TeamToInviteClient = TeamToInviteClient;
			try
			{
				ResourceDictionary resourceDictionary = new ResourceDictionary
				{
					Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
				};
				if (resourceDictionary != null)
				{
					string text = resourceDictionary["m_Message_Inviting"] as string;
					string text2 = resourceDictionary["m_ClientBoost"] as string;
					if (CS$<>8__locals1.client_boost)
					{
						Logs.AddNewLog(new BoostLog(this._boostTask.Name, text + " (" + text2 + ")"));
					}
					else
					{
						Logs.AddNewLog(new BoostLog(this._boostTask.Name, string.Format("{0} ({1}/{2})", text, this.PlayedGames + 1, this._boostTask.CountGame * gameCountFactor)));
					}
				}
				this.gsi.first_team = this.firstTeam;
				this.gsi.second_team = this.secondTeam;
				Thread.Sleep(1000);
				this.firstTeamLeader.LobbyState = 0;
				this.secondTeamLeader.LobbyState = 0;
				foreach (Player player in this.firstTeam)
				{
					player.ExecConfig();
				}
				foreach (Player player2 in this.secondTeam)
				{
					player2.ExecConfig();
				}
				foreach (Player player3 in this.firstTeam)
				{
					player3.CheckAndCloseError();
				}
				foreach (Player player4 in this.secondTeam)
				{
					player4.CheckAndCloseError();
				}
				if (this.PlayedGames > 0 && !this._boostTask.SwapLeaders && !CS$<>8__locals1.client_boost && Settings.GetInstance.RecentInvite)
				{
					Console.WriteLine("fast invite");
					this.firstTeamLeader.Disconnect();
					Task task = new Task(delegate
					{
						Thread.Sleep(500);
						CS$<>8__locals1.<>4__this.firstTeamLeader.InviteRecentTeam(CS$<>8__locals1.<>4__this.firstTeam.Count - 1);
					}, TaskCreationOptions.AttachedToParent);
					this.secondTeamLeader.Disconnect();
					Task task2 = new Task(delegate
					{
						Thread.Sleep(500);
						CS$<>8__locals1.<>4__this.secondTeamLeader.InviteRecentTeam(CS$<>8__locals1.<>4__this.secondTeam.Count - 1);
					}, TaskCreationOptions.AttachedToParent);
					task.Start();
					task2.Start();
					task.Wait();
					task2.Wait();
				}
				else
				{
					this.firstTeamLeader.Disconnect();
					Task task3 = new Task(delegate
					{
						Thread.Sleep(500);
						foreach (Player player7 in CS$<>8__locals1.<>4__this.firstTeam)
						{
							if (player7.Login != CS$<>8__locals1.<>4__this.firstTeamLeader.Login)
							{
								player7.Disconnect();
								CS$<>8__locals1.<>4__this.firstTeamLeader.InvitePlayerByCode(player7.InviteCode);
								Thread.Sleep(2000);
							}
						}
						if (CS$<>8__locals1.client_boost && CS$<>8__locals1.TeamToInviteClient == 1)
						{
							CS$<>8__locals1.<>4__this.firstTeamLeader.InvitePlayerByCode(CS$<>8__locals1.<>4__this._boostTask.ClientInviteCode);
						}
					}, TaskCreationOptions.AttachedToParent);
					this.secondTeamLeader.Disconnect();
					Task task4 = new Task(delegate
					{
						Thread.Sleep(500);
						foreach (Player player8 in CS$<>8__locals1.<>4__this.secondTeam)
						{
							if (player8.Login != CS$<>8__locals1.<>4__this.secondTeamLeader.Login)
							{
								player8.Disconnect();
								CS$<>8__locals1.<>4__this.secondTeamLeader.InvitePlayerByCode(player8.InviteCode);
								Thread.Sleep(2000);
							}
						}
						if (CS$<>8__locals1.client_boost && CS$<>8__locals1.TeamToInviteClient == 2)
						{
							CS$<>8__locals1.<>4__this.secondTeamLeader.InvitePlayerByCode(CS$<>8__locals1.<>4__this._boostTask.ClientInviteCode);
						}
					}, TaskCreationOptions.AttachedToParent);
					task3.Start();
					task4.Start();
					task3.Wait();
					task4.Wait();
				}
				Thread.Sleep(2000);
				Task task5 = new Task(delegate
				{
					IEnumerable<Player> firstTeam = CS$<>8__locals1.<>4__this.firstTeam;
					Func<Player, bool> func;
					if ((func = CS$<>8__locals1.<>9__7) == null)
					{
						func = (CS$<>8__locals1.<>9__7 = (Player pl) => pl.Login != CS$<>8__locals1.<>4__this.firstTeamLeader.Login);
					}
					foreach (Player player9 in firstTeam.Where(func))
					{
						string login = player9.Login;
						IEnumerable<Player> firstTeam2 = CS$<>8__locals1.<>4__this.firstTeam;
						Func<Player, bool> func2;
						if ((func2 = CS$<>8__locals1.<>9__8) == null)
						{
							func2 = (CS$<>8__locals1.<>9__8 = (Player pl) => pl.Login != CS$<>8__locals1.<>4__this.firstTeamLeader.Login);
						}
						if (login == firstTeam2.Where(func2).Last<Player>().Login)
						{
							Thread.Sleep(2250);
						}
						player9.CheckAndCloseError();
						player9.AcceptInviteToLobby();
					}
				}, TaskCreationOptions.AttachedToParent);
				Task task6 = new Task(delegate
				{
					IEnumerable<Player> secondTeam = CS$<>8__locals1.<>4__this.secondTeam;
					Func<Player, bool> func3;
					if ((func3 = CS$<>8__locals1.<>9__9) == null)
					{
						func3 = (CS$<>8__locals1.<>9__9 = (Player pl) => pl.Login != CS$<>8__locals1.<>4__this.secondTeamLeader.Login);
					}
					foreach (Player player10 in secondTeam.Where(func3))
					{
						string login2 = player10.Login;
						IEnumerable<Player> secondTeam2 = CS$<>8__locals1.<>4__this.secondTeam;
						Func<Player, bool> func4;
						if ((func4 = CS$<>8__locals1.<>9__10) == null)
						{
							func4 = (CS$<>8__locals1.<>9__10 = (Player pl) => pl.Login != CS$<>8__locals1.<>4__this.secondTeamLeader.Login);
						}
						if (login2 == secondTeam2.Where(func4).Last<Player>().Login)
						{
							Thread.Sleep(2250);
						}
						player10.CheckAndCloseError();
						player10.AcceptInviteToLobby();
					}
				}, TaskCreationOptions.AttachedToParent);
				task5.Start();
				task6.Start();
				task5.Wait();
				task6.Wait();
				if (!CS$<>8__locals1.client_boost)
				{
					Thread.Sleep(7000);
					if (this.firstTeamLeader.LobbyState >= this.firstTeam.Count && this.secondTeamLeader.LobbyState >= this.secondTeam.Count)
					{
						Console.WriteLine("Lobby Collected !");
					}
					else
					{
						Thread.Sleep(1000);
						foreach (Player player5 in this.firstTeam)
						{
							Thread.Sleep(500);
							if (player5.Login != this.firstTeamLeader.Login)
							{
								player5.LeaveLobby();
							}
						}
						foreach (Player player6 in this.secondTeam)
						{
							Thread.Sleep(500);
							if (player6.Login != this.secondTeamLeader.Login)
							{
								player6.LeaveLobby();
							}
						}
						Thread.Sleep(3000);
						this.CollectLobby(1, false, 0).Wait();
					}
				}
				else
				{
					int num;
					do
					{
						Thread.Sleep(200);
						num = new int[]
						{
							this.firstTeam.Count<Player>(),
							this.secondTeam.Count<Player>()
						}.Max();
					}
					while (this.firstTeamLeader.LobbyState != num || this.secondTeamLeader.LobbyState != num);
					Console.WriteLine("Lobby Collected !");
				}
				object obj = Task.Run(delegate
				{
					Session.<>c__DisplayClass25_0.<<CollectLobby>b__2>d <<CollectLobby>b__2>d;
					<<CollectLobby>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<CollectLobby>b__2>d.<>4__this = CS$<>8__locals1;
					<<CollectLobby>b__2>d.<>1__state = -1;
					<<CollectLobby>b__2>d.<>t__builder.Start<Session.<>c__DisplayClass25_0.<<CollectLobby>b__2>d>(ref <<CollectLobby>b__2>d);
					return <<CollectLobby>b__2>d.<>t__builder.Task;
				});
				if (Session.<>o__25.<>p__0 == null)
				{
					Session.<>o__25.<>p__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Session.<>o__25.<>p__0.Target(Session.<>o__25.<>p__0, obj);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00021044 File Offset: 0x0001F244
		private async Task UpdateStatistic()
		{
			Session.<>c__DisplayClass26_0 CS$<>8__locals1 = new Session.<>c__DisplayClass26_0();
			CS$<>8__locals1.<>4__this = this;
			if (!string.IsNullOrEmpty(this.LastGameLogID))
			{
				List<List<PlayerStats>> list = new List<List<PlayerStats>>();
				List<PlayerStats> list2 = new List<PlayerStats>();
				List<PlayerStats> list3 = new List<PlayerStats>();
				using (List<Player>.Enumerator enumerator = this.firstTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Session.<>c__DisplayClass26_1 CS$<>8__locals2 = new Session.<>c__DisplayClass26_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.player = enumerator.Current;
						int num = CS$<>8__locals2.player.GainedXP;
						if (num < 0)
						{
							num += 5000;
						}
						list2.Add(new PlayerStats
						{
							ProfileName = "",
							Login = CS$<>8__locals2.player.Login,
							SteamID = CS$<>8__locals2.player.string_1,
							XPGained = num,
							XPCurrentLVL = CS$<>8__locals2.player.Lvl,
							XPUntilNextLevel = ((5000 - CS$<>8__locals2.player.CurrentXP > 0) ? (5000 - CS$<>8__locals2.player.CurrentXP) : (5000 - CS$<>8__locals2.player.CurrentXP + 5000)),
							CurrentMatchmakingRank = this.ToFriedlyMatchmakingRank(CS$<>8__locals2.player.Rank5x5),
							CurrentMatchmakingWins = CS$<>8__locals2.player.Wins5x5
						});
						try
						{
							if (!string.IsNullOrEmpty(Settings.GetInstance.DiscordLogins))
							{
								if (Settings.GetInstance.DiscordLogins.Contains(","))
								{
									if ((from x in Settings.GetInstance.DiscordLogins.Split(new char[] { ',' })
										select x.Replace(" ", "")).Contains(CS$<>8__locals2.player.Login))
									{
										object obj = Task.Run(delegate
										{
											Session.<>c__DisplayClass26_1.<<UpdateStatistic>b__3>d <<UpdateStatistic>b__3>d;
											<<UpdateStatistic>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
											<<UpdateStatistic>b__3>d.<>4__this = CS$<>8__locals2;
											<<UpdateStatistic>b__3>d.<>1__state = -1;
											<<UpdateStatistic>b__3>d.<>t__builder.Start<Session.<>c__DisplayClass26_1.<<UpdateStatistic>b__3>d>(ref <<UpdateStatistic>b__3>d);
											return <<UpdateStatistic>b__3>d.<>t__builder.Task;
										});
										if (Session.<>o__26.<>p__0 == null)
										{
											Session.<>o__26.<>p__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Session.<>o__26.<>p__0.Target(Session.<>o__26.<>p__0, obj);
									}
								}
								else if (Settings.GetInstance.DiscordLogins.Length > 0 && Settings.GetInstance.DiscordLogins.Replace(" ", "") == CS$<>8__locals2.player.Login)
								{
									object obj2 = Task.Run(delegate
									{
										Session.<>c__DisplayClass26_1.<<UpdateStatistic>b__4>d <<UpdateStatistic>b__4>d;
										<<UpdateStatistic>b__4>d.<>t__builder = AsyncTaskMethodBuilder.Create();
										<<UpdateStatistic>b__4>d.<>4__this = CS$<>8__locals2;
										<<UpdateStatistic>b__4>d.<>1__state = -1;
										<<UpdateStatistic>b__4>d.<>t__builder.Start<Session.<>c__DisplayClass26_1.<<UpdateStatistic>b__4>d>(ref <<UpdateStatistic>b__4>d);
										return <<UpdateStatistic>b__4>d.<>t__builder.Task;
									});
									if (Session.<>o__26.<>p__1 == null)
									{
										Session.<>o__26.<>p__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Session.<>o__26.<>p__1.Target(Session.<>o__26.<>p__1, obj2);
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex);
						}
					}
				}
				using (List<Player>.Enumerator enumerator = this.secondTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Session.<>c__DisplayClass26_2 CS$<>8__locals3 = new Session.<>c__DisplayClass26_2();
						CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals1;
						CS$<>8__locals3.player = enumerator.Current;
						int num2 = CS$<>8__locals3.player.GainedXP;
						if (num2 < 0)
						{
							num2 += 5000;
						}
						list3.Add(new PlayerStats
						{
							ProfileName = "",
							Login = CS$<>8__locals3.player.Login,
							SteamID = CS$<>8__locals3.player.string_1,
							XPGained = num2,
							XPCurrentLVL = CS$<>8__locals3.player.Lvl,
							XPUntilNextLevel = ((5000 - CS$<>8__locals3.player.CurrentXP <= 0) ? (5000 - CS$<>8__locals3.player.CurrentXP + 5000) : (5000 - CS$<>8__locals3.player.CurrentXP)),
							CurrentMatchmakingRank = this.ToFriedlyMatchmakingRank(CS$<>8__locals3.player.Rank5x5),
							CurrentMatchmakingWins = CS$<>8__locals3.player.Wins5x5
						});
						try
						{
							if (!string.IsNullOrEmpty(Settings.GetInstance.DiscordLogins) && Settings.GetInstance.DiscordLogins.Contains(","))
							{
								if (!(from x in Settings.GetInstance.DiscordLogins.Split(new char[] { ',' })
									select x.Replace(" ", "")).Contains(CS$<>8__locals3.player.Login))
								{
									if (Settings.GetInstance.DiscordLogins.Length > 0 && Settings.GetInstance.DiscordLogins.Replace(" ", "") == CS$<>8__locals3.player.Login)
									{
										object obj3 = Task.Run(delegate
										{
											Session.<>c__DisplayClass26_2.<<UpdateStatistic>b__7>d <<UpdateStatistic>b__7>d;
											<<UpdateStatistic>b__7>d.<>t__builder = AsyncTaskMethodBuilder.Create();
											<<UpdateStatistic>b__7>d.<>4__this = CS$<>8__locals3;
											<<UpdateStatistic>b__7>d.<>1__state = -1;
											<<UpdateStatistic>b__7>d.<>t__builder.Start<Session.<>c__DisplayClass26_2.<<UpdateStatistic>b__7>d>(ref <<UpdateStatistic>b__7>d);
											return <<UpdateStatistic>b__7>d.<>t__builder.Task;
										});
										if (Session.<>o__26.<>p__3 == null)
										{
											Session.<>o__26.<>p__3 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Session.<>o__26.<>p__3.Target(Session.<>o__26.<>p__3, obj3);
									}
								}
								else
								{
									object obj4 = Task.Run(delegate
									{
										Session.<>c__DisplayClass26_2.<<UpdateStatistic>b__6>d <<UpdateStatistic>b__6>d;
										<<UpdateStatistic>b__6>d.<>t__builder = AsyncTaskMethodBuilder.Create();
										<<UpdateStatistic>b__6>d.<>4__this = CS$<>8__locals3;
										<<UpdateStatistic>b__6>d.<>1__state = -1;
										<<UpdateStatistic>b__6>d.<>t__builder.Start<Session.<>c__DisplayClass26_2.<<UpdateStatistic>b__6>d>(ref <<UpdateStatistic>b__6>d);
										return <<UpdateStatistic>b__6>d.<>t__builder.Task;
									});
									if (Session.<>o__26.<>p__2 == null)
									{
										Session.<>o__26.<>p__2 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Session.<>o__26.<>p__2.Target(Session.<>o__26.<>p__2, obj4);
								}
							}
						}
						catch (Exception ex2)
						{
							Console.WriteLine(ex2);
						}
					}
				}
				if (list2.Count >= 0)
				{
					list.Add(list2);
				}
				if (list3.Count >= 0)
				{
					list.Add(list3);
				}
				CS$<>8__locals1.json = JsonConvert.SerializeObject(list);
				object obj5 = Task.Run<string>(delegate
				{
					Session.<>c__DisplayClass26_0.<<UpdateStatistic>b__0>d <<UpdateStatistic>b__0>d;
					<<UpdateStatistic>b__0>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
					<<UpdateStatistic>b__0>d.<>4__this = CS$<>8__locals1;
					<<UpdateStatistic>b__0>d.<>1__state = -1;
					<<UpdateStatistic>b__0>d.<>t__builder.Start<Session.<>c__DisplayClass26_0.<<UpdateStatistic>b__0>d>(ref <<UpdateStatistic>b__0>d);
					return <<UpdateStatistic>b__0>d.<>t__builder.Task;
				});
				Session.<>c__DisplayClass26_0 CS$<>8__locals4 = CS$<>8__locals1;
				if (Session.<>o__26.<>p__5 == null)
				{
					Session.<>o__26.<>p__5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Session)));
				}
				Func<CallSite, object, string> target = Session.<>o__26.<>p__5.Target;
				CallSite <>p__ = Session.<>o__26.<>p__5;
				if (Session.<>o__26.<>p__4 == null)
				{
					Session.<>o__26.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Result", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				CS$<>8__locals4.encryptedJson = target(<>p__, Session.<>o__26.<>p__4.Target(Session.<>o__26.<>p__4, obj5));
				object obj6 = Task.Run(delegate
				{
					Session.<>c__DisplayClass26_0.<<UpdateStatistic>b__1>d <<UpdateStatistic>b__1>d;
					<<UpdateStatistic>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<UpdateStatistic>b__1>d.<>4__this = CS$<>8__locals1;
					<<UpdateStatistic>b__1>d.<>1__state = -1;
					<<UpdateStatistic>b__1>d.<>t__builder.Start<Session.<>c__DisplayClass26_0.<<UpdateStatistic>b__1>d>(ref <<UpdateStatistic>b__1>d);
					return <<UpdateStatistic>b__1>d.<>t__builder.Task;
				});
				if (Session.<>o__26.<>p__6 == null)
				{
					Session.<>o__26.<>p__6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Session.<>o__26.<>p__6.Target(Session.<>o__26.<>p__6, obj6);
				this.LastGameLogID = string.Empty;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00021088 File Offset: 0x0001F288
		private string ToFriedlyMatchmakingRank(int rank)
		{
			switch (rank)
			{
			case 0:
				return "Unranked";
			case 1:
				return "Silver I";
			case 2:
				return "Silver II";
			case 3:
				return "Silver III";
			case 4:
				return "Silver IV";
			case 5:
				return "Silver Elite";
			case 6:
				return "Silver Elite Master";
			case 7:
				return "Gold Nova I";
			case 8:
				return "Gold Nova II";
			case 9:
				return "Gold Nova III";
			case 10:
				return "Gold Nova Master";
			case 11:
				return "Master Guardian I";
			case 12:
				return "Master Guardian II";
			case 13:
				return "Master Guardian Elite";
			case 14:
				return "Distinguished Master Guardian";
			case 15:
				return "Legendary Eagle";
			case 16:
				return "Legendary Eagle Master";
			case 17:
				return "Supreme Master First Class";
			case 18:
				return "The Global Elite";
			default:
				return "Unknown";
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00021160 File Offset: 0x0001F360
		private async Task MatchProcess()
		{
			if (this._boostTask.SessionType.ToLower() == "rank boost")
			{
				Thread.Sleep(40000);
				if (this.MyGameMode == "Team 1")
				{
					foreach (Player player39 in this.firstTeam)
					{
						player39.Disconnect();
					}
				}
				if (this.MyGameMode == "Team 2")
				{
					foreach (Player player2 in this.secondTeam)
					{
						player2.Disconnect();
					}
				}
			}
			int halfRoundsCount = this.gsi.HalfRoundsCount;
			while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
			{
				Thread.Sleep(100);
			}
			this.gsi.IsGameOver = false;
			this.gsi.IsReconnected = false;
			this.gsi.ValueDisconnectWorker = true;
			if (this._boostTask.MVP && this._boostTask.SessionType.ToLower() != "client boost" && this._boostTask.SessionType.ToLower() != "xp boost" && this._boostTask.SessionType.ToLower() != "global boost")
			{
				if (this.gsi.MapName == "lobby_mapveto")
				{
					Thread.Sleep(60000);
					while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
					{
						Thread.Sleep(100);
					}
				}
				if (Program.GetInstance.SubscriptionLevel == 3)
				{
					Console.WriteLine("walkbot working");
					try
					{
						while (this.gsi.RoundPhase != RoundPhase.Live)
						{
							Thread.Sleep(50);
						}
						if (this._boostTask.GameMode == "Team 1")
						{
							foreach (Player player3 in this.secondTeam)
							{
								player3.WatchToCorrentAngle();
							}
							foreach (Player player4 in this.firstTeam.Where((Player pl) => pl.Login != this.firstTeamLeader.Login))
							{
								player4.GoOut();
							}
							this.firstTeamLeader.GoAndKillBots();
						}
						else
						{
							foreach (Player player5 in this.firstTeam)
							{
								player5.WatchToCorrentAngle();
							}
							foreach (Player player6 in this.secondTeam.Where((Player pl) => pl.Login != this.secondTeamLeader.Login))
							{
								player6.GoOut();
							}
							this.secondTeamLeader.GoAndKillBots();
						}
						Console.WriteLine("don");
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(50);
						}
						goto IL_3D5;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						CreateError.NewError("Something went wront", false);
						throw ex;
					}
				}
				if (Program.GetInstance.SubscriptionLevel != 1)
				{
					if (Program.GetInstance.SubscriptionLevel != 2)
					{
						return;
					}
				}
				while (this.gsi.RoundPhase != RoundPhase.Live)
				{
					Thread.Sleep(100);
				}
				Thread.Sleep(20000);
				while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
				{
					Thread.Sleep(100);
				}
			}
			IL_3D5:
			if (this._boostTask.SessionType.ToLower() == "client boost")
			{
				while (this.gsi.RoundPhase != RoundPhase.Live)
				{
					Thread.Sleep(100);
				}
				Thread.Sleep(20000);
				while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
				{
					Thread.Sleep(100);
				}
			}
			if (!(this._boostTask.SessionType.ToLower() == "global boost"))
			{
				if (this._boostTask.SessionType.ToLower() == "xp boost" && Program.GetInstance.SubscriptionLevel == 3 && this._boostTask.MVP)
				{
					while (this.gsi.RoundPhase != RoundPhase.Live)
					{
						Thread.Sleep(50);
					}
					if (!this._boostTask.MVP)
					{
						while (this.gsi.ScoreT + this.gsi.ScoreCT != 1)
						{
						}
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(100);
						}
					}
					else
					{
						Thread.Sleep(2500);
						foreach (Player player7 in this.firstTeam)
						{
							player7.ExecConsoleText("+duck");
						}
						this.WalkBot();
						while (this.gsi.ScoreT + this.gsi.ScoreCT != 1)
						{
						}
						GC.Collect();
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(100);
						}
						foreach (Player player8 in this.firstTeam)
						{
							player8.ExecConsoleText("+left");
							player8.ExecConsoleText("-attack2");
						}
					}
					this.gsi.ValueDisconnectWorker = true;
					bool flag = false;
					if (!(this._boostTask.SessionType.ToLower() == "rank boost"))
					{
						foreach (Player player9 in this.secondTeam)
						{
							player9.Disconnect();
						}
						Thread.Sleep(1500);
					}
					else
					{
						flag = true;
						this.secondTeam[0].Disconnect();
						this.gsi.AccountsToDisconnect.Add(this.secondTeam[0]);
						Thread.Sleep(1500);
						this.secondTeam[0].Reconnect();
						Thread.Sleep(1500);
						foreach (Player player10 in this.secondTeam.Where((Player player) => player40.Login != this.secondTeam[0].Login))
						{
							player10.Disconnect();
						}
					}
					bool flag2 = true;
					while (flag2)
					{
						if (this.gsi.ScoreCT == halfRoundsCount || this.gsi.ScoreT == halfRoundsCount || !this.gsi.ValueDisconnectWorker)
						{
							flag2 = false;
							IL_84D:
							this.gsi.AccountsToDisconnect.Clear();
							foreach (Player player11 in this.secondTeam)
							{
								player11.Reconnect();
							}
							Console.WriteLine("FIRST HALF ENDED.");
							while (this.gsi.RoundPhase != RoundPhase.Live)
							{
								Thread.Sleep(50);
							}
							this.gsi.ValueDisconnectWorker = true;
							foreach (Player player12 in this.secondTeam)
							{
								player12.Disconnect();
							}
							this.gsi.AccountsToDisconnect.Clear();
							goto IL_1937;
						}
						foreach (Player player13 in this.secondTeam)
						{
							if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
							{
								if (flag)
								{
									flag = false;
									this.gsi.AccountsToDisconnect.Add(player13);
									Thread.Sleep(6500);
									continue;
								}
								player13.Reconnect();
								this.gsi.AccountsToDisconnect.Add(player13);
								if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
								{
									if (this._boostTask.SessionType.ToLower() == "rank boost")
									{
										Thread.Sleep(6500);
										continue;
									}
									Thread.Sleep(10000);
									continue;
								}
								else
								{
									flag2 = false;
								}
							}
							else
							{
								flag2 = false;
							}
							break;
						}
					}
					goto IL_84D;
				}
				if (this.MyGameMode == "Draw")
				{
					if (this._boostTask.SessionType.ToLower() == "xp boost")
					{
						while (this.gsi.ScoreT + this.gsi.ScoreCT != 2)
						{
						}
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(100);
						}
					}
					this.gsi.WhereDisconnect = 2;
					this.gsi.ValueDisconnectWorker = true;
					foreach (Player player14 in this.secondTeam)
					{
						player14.Disconnect();
					}
					Thread.Sleep(1000);
					bool flag3 = true;
					while (flag3)
					{
						if (this.gsi.ScoreCT == halfRoundsCount || this.gsi.ScoreT == halfRoundsCount || !this.gsi.ValueDisconnectWorker)
						{
							flag3 = false;
							IL_1276:
							this.gsi.AccountsToDisconnect.Clear();
							foreach (Player player15 in this.secondTeam)
							{
								player15.Reconnect();
							}
							Thread.Sleep(20000);
							Thread.Sleep(2100);
							this.gsi.ValueDisconnectWorker = true;
							this.gsi.leaderID = this.secondTeamLeader.string_1;
							this.gsi.ValueDisconnectWorker = true;
							this.gsi.WhereDisconnect = 1;
							foreach (Player player16 in this.firstTeam)
							{
								player16.Disconnect();
							}
							Thread.Sleep(1000);
							flag3 = true;
							while (flag3)
							{
								if ((this.gsi.ScoreCT == halfRoundsCount && this.gsi.ScoreT == halfRoundsCount) || !this.gsi.ValueDisconnectWorker)
								{
									flag3 = false;
									break;
								}
								foreach (Player player17 in this.firstTeam)
								{
									if ((this.gsi.ScoreCT != halfRoundsCount || this.gsi.ScoreT != halfRoundsCount) && this.gsi.ValueDisconnectWorker)
									{
										player17.Reconnect();
										this.gsi.AccountsToDisconnect.Add(player17);
										if ((this.gsi.ScoreCT != halfRoundsCount || this.gsi.ScoreT != halfRoundsCount) && this.gsi.ValueDisconnectWorker)
										{
											Thread.Sleep(10000);
											continue;
										}
										flag3 = false;
									}
									else
									{
										flag3 = false;
									}
									break;
								}
							}
							goto IL_1937;
						}
						foreach (Player player18 in this.secondTeam)
						{
							if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
							{
								player18.Reconnect();
								this.gsi.AccountsToDisconnect.Add(player18);
								if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
								{
									Thread.Sleep(10000);
									continue;
								}
								flag3 = false;
							}
							else
							{
								flag3 = false;
							}
							break;
						}
					}
					goto IL_1276;
				}
				if (this.MyGameMode == "Team 2")
				{
					if (this._boostTask.SessionType.ToLower() == "xp boost")
					{
						while (this.gsi.ScoreT + this.gsi.ScoreCT != 1)
						{
						}
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(100);
						}
					}
					this.gsi.ValueDisconnectWorker = true;
					bool flag4 = false;
					if (this._boostTask.SessionType.ToLower() == "rank boost")
					{
						flag4 = true;
						this.firstTeam[0].Disconnect();
						this.gsi.AccountsToDisconnect.Add(this.firstTeam[0]);
						Thread.Sleep(1500);
						this.firstTeam[0].Reconnect();
						Thread.Sleep(1500);
						using (IEnumerator<Player> enumerator2 = this.firstTeam.Where((Player player) => player40.Login != this.firstTeam[0].Login).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Player player19 = enumerator2.Current;
								player19.Disconnect();
							}
							goto IL_AAF;
						}
					}
					foreach (Player player20 in this.firstTeam)
					{
						player20.Disconnect();
					}
					Thread.Sleep(1500);
					IL_AAF:
					bool flag5 = true;
					while (flag5)
					{
						if (this.gsi.ScoreCT == halfRoundsCount || this.gsi.ScoreT == halfRoundsCount || !this.gsi.ValueDisconnectWorker)
						{
							flag5 = false;
							break;
						}
						foreach (Player player21 in this.firstTeam)
						{
							if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
							{
								if (flag4)
								{
									flag4 = false;
									this.gsi.AccountsToDisconnect.Add(player21);
									Thread.Sleep(6500);
									continue;
								}
								player21.Reconnect();
								this.gsi.AccountsToDisconnect.Add(player21);
								if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
								{
									if (!(this._boostTask.SessionType.ToLower() == "rank boost"))
									{
										Thread.Sleep(10000);
										continue;
									}
									Thread.Sleep(6500);
									continue;
								}
								else
								{
									flag5 = false;
								}
							}
							else
							{
								flag5 = false;
							}
							break;
						}
					}
					this.gsi.AccountsToDisconnect.Clear();
					foreach (Player player22 in this.firstTeam)
					{
						player22.Reconnect();
					}
					Console.WriteLine("FIRST HALF ENDED.");
					while (this.gsi.RoundPhase != RoundPhase.Live)
					{
						Thread.Sleep(50);
					}
					this.gsi.ValueDisconnectWorker = true;
					this.gsi.ValueDisconnectWorker = true;
					foreach (Player player23 in this.firstTeam)
					{
						player23.Disconnect();
					}
					this.gsi.AccountsToDisconnect.Clear();
				}
				else if (this.MyGameMode == "Team 1")
				{
					if (this._boostTask.SessionType.ToLower() == "xp boost")
					{
						while (this.gsi.ScoreT + this.gsi.ScoreCT != 1)
						{
						}
						while (this.gsi.RoundPhase != RoundPhase.FreezeTime)
						{
							Thread.Sleep(100);
						}
					}
					this.gsi.ValueDisconnectWorker = true;
					bool flag6 = false;
					if (this._boostTask.SessionType.ToLower() == "rank boost")
					{
						flag6 = true;
						this.secondTeam[0].Disconnect();
						this.gsi.AccountsToDisconnect.Add(this.secondTeam[0]);
						Thread.Sleep(1500);
						this.secondTeam[0].Reconnect();
						Thread.Sleep(1500);
						using (IEnumerator<Player> enumerator2 = this.secondTeam.Where((Player player) => player40.Login != this.secondTeam[0].Login).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Player player24 = enumerator2.Current;
								player24.Disconnect();
							}
							goto IL_E73;
						}
					}
					foreach (Player player25 in this.secondTeam)
					{
						player25.Disconnect();
					}
					Thread.Sleep(1500);
					IL_E73:
					bool flag7 = true;
					while (flag7)
					{
						if (this.gsi.ScoreCT == halfRoundsCount || this.gsi.ScoreT == halfRoundsCount || !this.gsi.ValueDisconnectWorker)
						{
							flag7 = false;
							break;
						}
						foreach (Player player26 in this.secondTeam)
						{
							if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
							{
								if (flag6)
								{
									flag6 = false;
									this.gsi.AccountsToDisconnect.Add(player26);
									Thread.Sleep(6500);
									continue;
								}
								player26.Reconnect();
								this.gsi.AccountsToDisconnect.Add(player26);
								if (this.gsi.ScoreCT != halfRoundsCount && this.gsi.ScoreT != halfRoundsCount && this.gsi.ValueDisconnectWorker)
								{
									if (this._boostTask.SessionType.ToLower() == "rank boost")
									{
										Thread.Sleep(6500);
										continue;
									}
									Thread.Sleep(10000);
									continue;
								}
								else
								{
									flag7 = false;
								}
							}
							else
							{
								flag7 = false;
							}
							break;
						}
					}
					this.gsi.AccountsToDisconnect.Clear();
					foreach (Player player27 in this.secondTeam)
					{
						player27.Reconnect();
					}
					Console.WriteLine("FIRST HALF ENDED.");
					while (this.gsi.RoundPhase != RoundPhase.Live)
					{
						Thread.Sleep(50);
					}
					this.gsi.ValueDisconnectWorker = true;
					foreach (Player player28 in this.secondTeam)
					{
						player28.Disconnect();
					}
					this.gsi.AccountsToDisconnect.Clear();
				}
			}
			else
			{
				if (this.MyGameMode == "Team 1")
				{
					Player _40_kills_player2 = this.firstTeam[new Random().Next(this.firstTeam.Count)];
					Func<Player, bool> <>9__3;
					for (int i = 0; i < 8; i++)
					{
						while (this.gsi.RoundPhase != RoundPhase.Live)
						{
							Thread.Sleep(50);
						}
						foreach (Player player29 in this.secondTeam)
						{
							player29.WatchToCorrentAngle();
						}
						IEnumerable<Player> firstTeam = this.firstTeam;
						Func<Player, bool> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (Player pl) => pl.Login != _40_kills_player2.Login);
						}
						foreach (Player player30 in firstTeam.Where(func))
						{
							player30.GoOut();
						}
						_40_kills_player2.GoAndKillBots();
					}
					IEnumerable<Player> firstTeam2 = this.firstTeam;
					Func<Player, bool> func2;
					Func<Player, bool> <>9__4;
					if ((func2 = <>9__4) == null)
					{
						func2 = (<>9__4 = (Player pl) => pl.Login != _40_kills_player2.Login);
					}
					using (IEnumerator<Player> enumerator2 = firstTeam2.Where(func2).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Player player40 = enumerator2.Current;
							Func<Player, bool> <>9__5;
							for (int j = 0; j < 2; j++)
							{
								while (this.gsi.RoundPhase != RoundPhase.Live)
								{
									Thread.Sleep(50);
								}
								foreach (Player player31 in this.secondTeam)
								{
									player31.WatchToCorrentAngle();
								}
								IEnumerable<Player> firstTeam3 = this.firstTeam;
								Func<Player, bool> func3;
								if ((func3 = <>9__5) == null)
								{
									func3 = (<>9__5 = (Player pp) => pp.Login != player40.Login);
								}
								foreach (Player player32 in firstTeam3.Where(func3))
								{
									player32.GoOut();
								}
								player40.GoAndKillBots();
							}
						}
						goto IL_1937;
					}
				}
				if (this.MyGameMode == "Team 2")
				{
					Player _40_kills_player = this.secondTeam[new Random().Next(this.secondTeam.Count)];
					Func<Player, bool> <>9__6;
					for (int k = 0; k < 8; k++)
					{
						while (this.gsi.RoundPhase != RoundPhase.Live)
						{
							Thread.Sleep(50);
						}
						foreach (Player player33 in this.firstTeam)
						{
							player33.WatchToCorrentAngle();
						}
						IEnumerable<Player> secondTeam = this.secondTeam;
						Func<Player, bool> func4;
						if ((func4 = <>9__6) == null)
						{
							func4 = (<>9__6 = (Player pl) => pl.Login != _40_kills_player.Login);
						}
						foreach (Player player34 in secondTeam.Where(func4))
						{
							player34.GoOut();
						}
						_40_kills_player.GoAndKillBots();
					}
					IEnumerable<Player> secondTeam2 = this.secondTeam;
					Func<Player, bool> func5;
					Func<Player, bool> <>9__7;
					if ((func5 = <>9__7) == null)
					{
						func5 = (<>9__7 = (Player pl) => pl.Login != _40_kills_player.Login);
					}
					using (IEnumerator<Player> enumerator2 = secondTeam2.Where(func5).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Player player41 = enumerator2.Current;
							Func<Player, bool> <>9__8;
							for (int l = 0; l < 2; l++)
							{
								while (this.gsi.RoundPhase != RoundPhase.Live)
								{
									Thread.Sleep(50);
								}
								foreach (Player player35 in this.firstTeam)
								{
									player35.WatchToCorrentAngle();
								}
								IEnumerable<Player> secondTeam3 = this.secondTeam;
								Func<Player, bool> func6;
								if ((func6 = <>9__8) == null)
								{
									func6 = (<>9__8 = (Player pp) => pp.Login != player41.Login);
								}
								foreach (Player player36 in secondTeam3.Where(func6))
								{
									player36.GoOut();
								}
								player41.GoAndKillBots();
							}
						}
					}
				}
			}
			IL_1937:
			Thread.Sleep(7000);
			if (this._boostTask.SwapLeaders && this._boostTask.SessionType.ToLower() != "client boost" && this._boostTask.SessionType.ToLower() != "global boost")
			{
				List<Player> list = new List<Player>();
				using (List<Player>.Enumerator enumerator = this.firstTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player42 = enumerator.Current;
						list.Add(StaticData.GetInstance.Players.FirstOrDefault((Player p) => p.Login == player42.Login));
					}
				}
				List<Player> list2 = new List<Player>();
				using (List<Player>.Enumerator enumerator = this.secondTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player = enumerator.Current;
						list2.Add(StaticData.GetInstance.Players.FirstOrDefault((Player p) => p.Login == player.Login));
					}
				}
				Player player37 = list[0];
				Player player38 = list2[0];
				list.Remove(player37);
				list2.Remove(player38);
				list.Insert(0, player38);
				list2.Insert(0, player37);
				this.gsi.first_team = list;
				this.gsi.second_team = list2;
				this._boostTask.FirstTeam = list;
				this._boostTask.SecondTeam = list2;
			}
			string result = Task.Run<string>(async () => await PanelApiService.GetInstance.RegisterGame(this._boostTask.SessionType, this._boostTask.GameMode, this.gsi.MapName)).Result;
			if (!string.IsNullOrEmpty(result))
			{
				JObject jobject = JObject.Parse(result);
				if (!(jobject["status"].ToString() == "ok"))
				{
					this.LastGameLogID = string.Empty;
				}
				else
				{
					this.LastGameLogID = jobject["game_id"].ToString();
				}
			}
			ResourceDictionary resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
			};
			if (resourceDictionary != null)
			{
				string text = resourceDictionary["m_Message_GameOver"] as string;
				Logs.AddNewLog(new BoostLog(this._boostTask.Name, text + " [" + this.LastGameLogID + "]"));
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000211A4 File Offset: 0x0001F3A4
		private void WalkBot()
		{
			List<Vector3> enemyPositions = new List<Vector3>();
			List<string> enemyLogins = new List<string>();
			int j;
			Action <>9__0;
			int k;
			for (j = 0; j < this.secondTeam.Count; j = k + 1)
			{
				Action action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate
					{
						using (this.cancellationToken.Register(new Action(Thread.CurrentThread.Abort)))
						{
							int index = j;
							Console.WriteLine(string.Format("new thread with index : {0}", index));
							Player player = this.secondTeam[index];
							player.ExecConsoleText("-left");
							player.ExecConsoleText("-right");
							List<Vector3> list = new List<Vector3>();
							foreach (Vector3 vector in Maps.getPathByMapName(this.firstTeamLeader.currentMap))
							{
								list.Add(vector);
							}
							bool flag = false;
							CsGoProcess csGoProcess = new CsGoProcess(player.HWND);
							int num = PatternScanner.FindPattern(csGoProcess.handle, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", csGoProcess.processModules["engine"], true, 1, true, 0);
							IntPtr intPtr = Memory.ReadFromProcess<IntPtr>(csGoProcess.handle, csGoProcess.processModules["engine"].BaseAddress + num);
							IntPtr handle = csGoProcess.handle;
							IntPtr baseAddress = csGoProcess.processModules["client"].BaseAddress;
							if (Session.<>o__29.<>p__2 == null)
							{
								Session.<>o__29.<>p__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
							}
							Func<CallSite, object, int> target = Session.<>o__29.<>p__2.Target;
							CallSite <>p__ = Session.<>o__29.<>p__2;
							if (Session.<>o__29.<>p__1 == null)
							{
								Session.<>o__29.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwLocalPlayer", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target2 = Session.<>o__29.<>p__1.Target;
							CallSite <>p__2 = Session.<>o__29.<>p__1;
							if (Session.<>o__29.<>p__0 == null)
							{
								Session.<>o__29.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							IntPtr intPtr2 = Memory.ReadFromProcess<IntPtr>(handle, baseAddress + target(<>p__, target2(<>p__2, Session.<>o__29.<>p__0.Target(Session.<>o__29.<>p__0, Offsets.hazedumper))));
							IntPtr handle2 = csGoProcess.handle;
							IntPtr intPtr3 = intPtr2;
							if (Session.<>o__29.<>p__5 == null)
							{
								Session.<>o__29.<>p__5 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
							}
							Func<CallSite, object, int> target3 = Session.<>o__29.<>p__5.Target;
							CallSite <>p__3 = Session.<>o__29.<>p__5;
							if (Session.<>o__29.<>p__4 == null)
							{
								Session.<>o__29.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iTeamNum", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target4 = Session.<>o__29.<>p__4.Target;
							CallSite <>p__4 = Session.<>o__29.<>p__4;
							if (Session.<>o__29.<>p__3 == null)
							{
								Session.<>o__29.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							if (Memory.ReadFromProcess<int>(handle2, intPtr3 + target3(<>p__3, target4(<>p__4, Session.<>o__29.<>p__3.Target(Session.<>o__29.<>p__3, Offsets.hazedumper)))) == 3)
							{
								list.Reverse();
							}
							Session.SetForegroundWindow(player.HWND);
							Session.SetFocus(player.HWND);
							Session.SetActiveWindow(player.HWND);
							Thread.Sleep(500);
							Session.SetForegroundWindow(this.firstTeamLeader.HWND);
							Session.SetFocus(this.firstTeamLeader.HWND);
							Session.SetActiveWindow(this.firstTeamLeader.HWND);
							Thread.Sleep(500);
							foreach (Vector3 vector2 in list)
							{
								for (;;)
								{
									Thread.Sleep(2);
									IntPtr handle3 = csGoProcess.handle;
									IntPtr intPtr4 = intPtr2;
									if (Session.<>o__29.<>p__8 == null)
									{
										Session.<>o__29.<>p__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
									}
									Func<CallSite, object, int> target5 = Session.<>o__29.<>p__8.Target;
									CallSite <>p__5 = Session.<>o__29.<>p__8;
									if (Session.<>o__29.<>p__7 == null)
									{
										Session.<>o__29.<>p__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target6 = Session.<>o__29.<>p__7.Target;
									CallSite <>p__6 = Session.<>o__29.<>p__7;
									if (Session.<>o__29.<>p__6 == null)
									{
										Session.<>o__29.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Vector3 vector3 = Memory.ReadFromProcess<Vector3>(handle3, intPtr4 + target5(<>p__5, target6(<>p__6, Session.<>o__29.<>p__6.Target(Session.<>o__29.<>p__6, Offsets.hazedumper))));
									if (Math.Sqrt(Math.Pow((double)(vector3.X - vector2.X), 2.0) + Math.Pow((double)(vector3.Y - vector2.Y), 2.0)) <= 15.0)
									{
										break;
									}
									Vector3 vector4 = vector2 - vector3;
									vector4.Z = 0f;
									float y = this.CalcAngle(vector4, vector4.Length()).Y;
									IntPtr handle4 = csGoProcess.handle;
									IntPtr intPtr5 = intPtr;
									if (Session.<>o__29.<>p__11 == null)
									{
										Session.<>o__29.<>p__11 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
									}
									Func<CallSite, object, int> target7 = Session.<>o__29.<>p__11.Target;
									CallSite <>p__7 = Session.<>o__29.<>p__11;
									if (Session.<>o__29.<>p__10 == null)
									{
										Session.<>o__29.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_ViewAngles", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target8 = Session.<>o__29.<>p__10.Target;
									CallSite <>p__8 = Session.<>o__29.<>p__10;
									if (Session.<>o__29.<>p__9 == null)
									{
										Session.<>o__29.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									float num2 = y - Memory.ReadFromProcess<Vector3>(handle4, intPtr5 + target7(<>p__7, target8(<>p__8, Session.<>o__29.<>p__9.Target(Session.<>o__29.<>p__9, Offsets.hazedumper)))).Y;
									if (Math.Abs(num2) > 180f)
									{
										num2 = -num2;
									}
									if (num2 >= 3f)
									{
										player.ExecConsoleText("+left");
										player.ExecConsoleText("-left");
									}
									else if (num2 <= -3f)
									{
										player.ExecConsoleText("+right");
										player.ExecConsoleText("-right");
									}
									if (Math.Abs(num2) >= 10f)
									{
										if (flag)
										{
											flag = false;
											player.ExecConsoleText("-forward");
										}
									}
									else if (!flag)
									{
										flag = true;
										player.ExecConsoleText("+forward");
									}
								}
							}
							player.ExecConsoleText("-forward");
							if (enemyPositions.Count == 0)
							{
								for (int i = 0; i < 64; i++)
								{
									IntPtr handle5 = csGoProcess.handle;
									IntPtr baseAddress2 = csGoProcess.processModules["client"].BaseAddress;
									if (Session.<>o__29.<>p__14 == null)
									{
										Session.<>o__29.<>p__14 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
									}
									Func<CallSite, object, int> target9 = Session.<>o__29.<>p__14.Target;
									CallSite <>p__9 = Session.<>o__29.<>p__14;
									if (Session.<>o__29.<>p__13 == null)
									{
										Session.<>o__29.<>p__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwEntityList", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target10 = Session.<>o__29.<>p__13.Target;
									CallSite <>p__10 = Session.<>o__29.<>p__13;
									if (Session.<>o__29.<>p__12 == null)
									{
										Session.<>o__29.<>p__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr6 = Memory.ReadFromProcess<IntPtr>(handle5, baseAddress2 + target9(<>p__9, target10(<>p__10, Session.<>o__29.<>p__12.Target(Session.<>o__29.<>p__12, Offsets.hazedumper))) + i * 16);
									if (!(intPtr6 == IntPtr.Zero))
									{
										IntPtr handle6 = csGoProcess.handle;
										IntPtr intPtr7 = intPtr2;
										if (Session.<>o__29.<>p__17 == null)
										{
											Session.<>o__29.<>p__17 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
										}
										Func<CallSite, object, int> target11 = Session.<>o__29.<>p__17.Target;
										CallSite <>p__11 = Session.<>o__29.<>p__17;
										if (Session.<>o__29.<>p__16 == null)
										{
											Session.<>o__29.<>p__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iTeamNum", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target12 = Session.<>o__29.<>p__16.Target;
										CallSite <>p__12 = Session.<>o__29.<>p__16;
										if (Session.<>o__29.<>p__15 == null)
										{
											Session.<>o__29.<>p__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										int num3 = Memory.ReadFromProcess<int>(handle6, intPtr7 + target11(<>p__11, target12(<>p__12, Session.<>o__29.<>p__15.Target(Session.<>o__29.<>p__15, Offsets.hazedumper))));
										IntPtr handle7 = csGoProcess.handle;
										IntPtr intPtr8 = intPtr6;
										if (Session.<>o__29.<>p__20 == null)
										{
											Session.<>o__29.<>p__20 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
										}
										Func<CallSite, object, int> target13 = Session.<>o__29.<>p__20.Target;
										CallSite <>p__13 = Session.<>o__29.<>p__20;
										if (Session.<>o__29.<>p__19 == null)
										{
											Session.<>o__29.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iTeamNum", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target14 = Session.<>o__29.<>p__19.Target;
										CallSite <>p__14 = Session.<>o__29.<>p__19;
										if (Session.<>o__29.<>p__18 == null)
										{
											Session.<>o__29.<>p__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										int num4 = Memory.ReadFromProcess<int>(handle7, intPtr8 + target13(<>p__13, target14(<>p__14, Session.<>o__29.<>p__18.Target(Session.<>o__29.<>p__18, Offsets.hazedumper))));
										if (num3 != num4 && num4 != 0)
										{
											Console.WriteLine("scanned yes");
											IntPtr handle8 = csGoProcess.handle;
											IntPtr intPtr9 = intPtr;
											if (Session.<>o__29.<>p__23 == null)
											{
												Session.<>o__29.<>p__23 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
											}
											Func<CallSite, object, int> target15 = Session.<>o__29.<>p__23.Target;
											CallSite <>p__15 = Session.<>o__29.<>p__23;
											if (Session.<>o__29.<>p__22 == null)
											{
												Session.<>o__29.<>p__22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_PlayerInfo", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
											}
											Func<CallSite, object, object> target16 = Session.<>o__29.<>p__22.Target;
											CallSite <>p__16 = Session.<>o__29.<>p__22;
											if (Session.<>o__29.<>p__21 == null)
											{
												Session.<>o__29.<>p__21 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
											}
											IntPtr intPtr10 = Memory.ReadFromProcess<IntPtr>(handle8, intPtr9 + target15(<>p__15, target16(<>p__16, Session.<>o__29.<>p__21.Target(Session.<>o__29.<>p__21, Offsets.hazedumper))));
											IntPtr intPtr11 = Memory.ReadFromProcess<IntPtr>(csGoProcess.handle, intPtr10 + 64);
											IntPtr intPtr12 = Memory.ReadFromProcess<IntPtr>(csGoProcess.handle, intPtr11 + 12);
											IntPtr intPtr13 = Memory.ReadFromProcess<IntPtr>(csGoProcess.handle, intPtr12 + 40 + i * 52);
											player_info_t player_info_t = Memory.ReadFromProcess<player_info_t>(csGoProcess.handle, intPtr13);
											foreach (Player player2 in this.firstTeam)
											{
												if (new string(player_info_t.m_szSteamID).Contains(player2.GetInternalID().Split(new char[] { ':' }).Last<string>()))
												{
													enemyLogins.Add(player2.Login);
													break;
												}
											}
											IntPtr handle9 = csGoProcess.handle;
											IntPtr intPtr14 = intPtr6;
											if (Session.<>o__29.<>p__26 == null)
											{
												Session.<>o__29.<>p__26 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
											}
											Func<CallSite, object, int> target17 = Session.<>o__29.<>p__26.Target;
											CallSite <>p__17 = Session.<>o__29.<>p__26;
											if (Session.<>o__29.<>p__25 == null)
											{
												Session.<>o__29.<>p__25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
											}
											Func<CallSite, object, object> target18 = Session.<>o__29.<>p__25.Target;
											CallSite <>p__18 = Session.<>o__29.<>p__25;
											if (Session.<>o__29.<>p__24 == null)
											{
												Session.<>o__29.<>p__24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
											}
											Vector3 vector5 = Memory.ReadFromProcess<Vector3>(handle9, intPtr14 + target17(<>p__17, target18(<>p__18, Session.<>o__29.<>p__24.Target(Session.<>o__29.<>p__24, Offsets.hazedumper))));
											enemyPositions.Add(vector5);
										}
									}
								}
								Console.WriteLine(string.Format("Scanned players: {0}/{1}", enemyPositions.Count, enemyLogins.Count));
							}
							Console.WriteLine(string.Format("HERE index : {0}", index));
							Player player3 = this.firstTeam.FirstOrDefault((Player x) => x.Login == enemyLogins[index]);
							if (player3 != null)
							{
								Console.WriteLine("Win login: " + player3.Login);
								player3.TakeKnife();
								player3.ExecConsoleText("-duck");
								player3.ExecConsoleText("-left");
								player3.ExecConsoleText("+attack2");
								Vector3 vector6 = enemyPositions[index];
								for (;;)
								{
									IntPtr handle10 = csGoProcess.handle;
									IntPtr intPtr15 = intPtr2;
									if (Session.<>o__29.<>p__29 == null)
									{
										Session.<>o__29.<>p__29 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
									}
									Func<CallSite, object, int> target19 = Session.<>o__29.<>p__29.Target;
									CallSite <>p__19 = Session.<>o__29.<>p__29;
									if (Session.<>o__29.<>p__28 == null)
									{
										Session.<>o__29.<>p__28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iHealth", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target20 = Session.<>o__29.<>p__28.Target;
									CallSite <>p__20 = Session.<>o__29.<>p__28;
									if (Session.<>o__29.<>p__27 == null)
									{
										Session.<>o__29.<>p__27 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									if (Memory.ReadFromProcess<int>(handle10, intPtr15 + target19(<>p__19, target20(<>p__20, Session.<>o__29.<>p__27.Target(Session.<>o__29.<>p__27, Offsets.hazedumper)))) <= 3)
									{
										break;
									}
									Thread.Sleep(2);
									IntPtr handle11 = csGoProcess.handle;
									IntPtr intPtr16 = intPtr2;
									if (Session.<>o__29.<>p__32 == null)
									{
										Session.<>o__29.<>p__32 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
									}
									Func<CallSite, object, int> target21 = Session.<>o__29.<>p__32.Target;
									CallSite <>p__21 = Session.<>o__29.<>p__32;
									if (Session.<>o__29.<>p__31 == null)
									{
										Session.<>o__29.<>p__31 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target22 = Session.<>o__29.<>p__31.Target;
									CallSite <>p__22 = Session.<>o__29.<>p__31;
									if (Session.<>o__29.<>p__30 == null)
									{
										Session.<>o__29.<>p__30 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Vector3 vector7 = Memory.ReadFromProcess<Vector3>(handle11, intPtr16 + target21(<>p__21, target22(<>p__22, Session.<>o__29.<>p__30.Target(Session.<>o__29.<>p__30, Offsets.hazedumper))));
									if (Math.Sqrt(Math.Pow((double)(vector7.X - vector6.X), 2.0) + Math.Pow((double)(vector7.Y - vector6.Y), 2.0)) > 5.0)
									{
										Vector3 vector8 = vector6 - vector7;
										vector8.Z = 0f;
										float y2 = this.CalcAngle(vector8, vector8.Length()).Y;
										IntPtr handle12 = csGoProcess.handle;
										IntPtr intPtr17 = intPtr;
										if (Session.<>o__29.<>p__35 == null)
										{
											Session.<>o__29.<>p__35 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Session)));
										}
										Func<CallSite, object, int> target23 = Session.<>o__29.<>p__35.Target;
										CallSite <>p__23 = Session.<>o__29.<>p__35;
										if (Session.<>o__29.<>p__34 == null)
										{
											Session.<>o__29.<>p__34 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_ViewAngles", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target24 = Session.<>o__29.<>p__34.Target;
										CallSite <>p__24 = Session.<>o__29.<>p__34;
										if (Session.<>o__29.<>p__33 == null)
										{
											Session.<>o__29.<>p__33 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										float num5 = y2 - Memory.ReadFromProcess<Vector3>(handle12, intPtr17 + target23(<>p__23, target24(<>p__24, Session.<>o__29.<>p__33.Target(Session.<>o__29.<>p__33, Offsets.hazedumper)))).Y;
										if (Math.Abs(num5) > 180f)
										{
											num5 = -num5;
										}
										if (num5 >= 3f)
										{
											player.ExecConsoleText("+left");
											player.ExecConsoleText("-left");
										}
										else if (num5 <= -3f)
										{
											player.ExecConsoleText("+right");
											player.ExecConsoleText("-right");
										}
										if (Math.Abs(num5) >= 10f)
										{
											if (flag)
											{
												flag = false;
												player.ExecConsoleText("-forward");
											}
										}
										else if (!flag)
										{
											flag = true;
											player.ExecConsoleText("+forward");
										}
									}
								}
								player.ExecConsoleText("-forward");
								player.ExecConsoleText("+left");
								player.ExecConsoleText("+right");
								player3.ExecConsoleText("+left");
								player3.ExecConsoleText("-attack2");
							}
						}
					});
				}
				new Task(action).Start();
				Thread.Sleep(5000);
				k = j;
			}
			Console.WriteLine("запустили всех, ждем");
		}

		// Token: 0x0600050B RID: 1291
		[DllImport("msvcrt.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern double asin(double radians);

		// Token: 0x0600050C RID: 1292 RVA: 0x00003DB4 File Offset: 0x00001FB4
		private static float RAD2DEG(float rad)
		{
			return 57.2957764f * rad;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00003DBD File Offset: 0x00001FBD
		public Vector2 CalcAngle(Vector3 posDelta, float posDeltaLength)
		{
			return new Vector2(Session.RAD2DEG((float)(-(float)Session.asin((double)(posDelta.Z / posDeltaLength)))), Session.RAD2DEG((float)Math.Atan2((double)posDelta.Y, (double)posDelta.X)));
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002124C File Offset: 0x0001F44C
		private Vector3 method_0(Vector3 src, Vector3 dst)
		{
			Vector3 vector = src - dst;
			return this.VectorAngles(vector);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002126C File Offset: 0x0001F46C
		private Vector3 VectorAngles(Vector3 forward)
		{
			float num = 3.14159274f;
			Vector3 vector;
			if (forward.Y == 0f && forward.X == 0f)
			{
				vector.X = ((forward.Z > 0f) ? 270f : 90f);
				vector.Y = 0f;
			}
			else
			{
				vector.X = (float)Math.Atan2((double)(-(double)forward.Z), (double)forward.Length2D()) * -180f / num;
				vector.Y = (float)Math.Atan2((double)forward.Y, (double)forward.X) * 180f / num;
				if (vector.Y <= 90f)
				{
					if (vector.Y < 90f)
					{
						vector.Y += 180f;
					}
					else if (vector.Y == 90f)
					{
						vector.Y = 0f;
					}
				}
				else
				{
					vector.Y -= 180f;
				}
			}
			vector.Z = 0f;
			return vector;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002137C File Offset: 0x0001F57C
		private async Task StartGameSearching()
		{
			try
			{
				ResourceDictionary resourceDictionary = new ResourceDictionary
				{
					Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
				};
				if (resourceDictionary != null)
				{
					string text = resourceDictionary["m_Message_Searching"] as string;
					Logs.AddNewLog(new BoostLog(this._boostTask.Name, text));
				}
			}
			catch
			{
			}
			this.NeedAccept = true;
			this.Accepted = false;
			this.firstTeamLeader.StartGame(this.firstTeamLeader.LobbyState, true);
			this.secondTeamLeader.StartGame(this.secondTeamLeader.LobbyState, true);
			Thread.Sleep(5000);
			for (;;)
			{
				Thread.Sleep(250);
				if (this.Accepted)
				{
					break;
				}
				if (this.CanSearch)
				{
					try
					{
						Bitmap screenshot = this.firstTeamLeader.GetScreenshot();
						if (screenshot.GetPixel(300, 290) == Color.FromArgb(Convert.ToInt32("-16759505")))
						{
							this.firstTeamLeader.StartGame(this.firstTeamLeader.LobbyState, false);
						}
						Bitmap screenshot2 = this.secondTeamLeader.GetScreenshot();
						if (screenshot2.GetPixel(300, 290) == Color.FromArgb(Convert.ToInt32("-16759505")))
						{
							this.secondTeamLeader.StartGame(this.secondTeamLeader.LobbyState, false);
						}
						screenshot.Dispose();
						screenshot2.Dispose();
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
					}
					Thread.Sleep(2000);
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000213C0 File Offset: 0x0001F5C0
		private void MatchAccept_worker()
		{
			Thread.Sleep(5000);
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				Player player11 = this.firstTeam.Where((Player player) => !string.IsNullOrEmpty(player.match_id)).FirstOrDefault<Player>();
				if (player11 != null)
				{
					text = player11.match_id;
				}
				Player player2 = this.secondTeam.Where((Player player) => !string.IsNullOrEmpty(player.match_id)).FirstOrDefault<Player>();
				if (player2 != null)
				{
					text2 = player2.match_id;
				}
				if (text == text2 && !string.IsNullOrEmpty(text))
				{
					ResourceDictionary resourceDictionary = new ResourceDictionary
					{
						Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
					};
					if (resourceDictionary != null)
					{
						string text3 = resourceDictionary["m_Message_GameFound"] as string;
						Logs.AddNewLog(new BoostLog(this._boostTask.Name, text3));
					}
					foreach (Player player3 in this.firstTeam)
					{
						player3.match_id = string.Empty;
					}
					foreach (Player player4 in this.secondTeam)
					{
						player4.match_id = string.Empty;
					}
					try
					{
						if (File.Exists(Directory.GetCurrentDirectory() + "\\cfg\\music\\1.wav"))
						{
							new SoundPlayer(Directory.GetCurrentDirectory() + "\\cfg\\music\\1.wav").Play();
						}
						goto IL_607;
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
						goto IL_607;
					}
					goto IL_1C9;
					IL_2F5:
					if (Session.<>o__38.<>p__3 == null)
					{
						Session.<>o__38.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj;
					if (Session.<>o__38.<>p__3.Target(Session.<>o__38.<>p__3, obj))
					{
						foreach (Player player5 in this.firstTeam)
						{
							player5.AcceptGame();
						}
						foreach (Player player6 in this.secondTeam)
						{
							player6.AcceptGame();
						}
					}
					this.Accepted = true;
					this.NeedAccept = false;
					this.CanSearch = true;
					this.failGameCounter = 0;
					goto IL_3C6;
					IL_1E9:
					object obj2 = new Random();
					if (Session.<>o__38.<>p__2 == null)
					{
						Session.<>o__38.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Session), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target = Session.<>o__38.<>p__2.Target;
					CallSite <>p__ = Session.<>o__38.<>p__2;
					if (Session.<>o__38.<>p__1 == null)
					{
						Session.<>o__38.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(Session), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, int, object> target2 = Session.<>o__38.<>p__1.Target;
					CallSite <>p__2 = Session.<>o__38.<>p__1;
					if (Session.<>o__38.<>p__0 == null)
					{
						Session.<>o__38.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Next", null, typeof(Session), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					if (target(<>p__, target2(<>p__2, Session.<>o__38.<>p__0.Target(Session.<>o__38.<>p__0, obj2, 5), 3)))
					{
						obj = false;
						goto IL_2F5;
					}
					obj = true;
					goto IL_2F5;
					IL_607:
					Thread.Sleep(7000);
					if (string.IsNullOrEmpty(Program.GetInstance.TOKEN))
					{
						goto IL_1E9;
					}
					IL_1C9:
					if (Program.GetInstance.TOKEN.Length == 48)
					{
						obj = true;
						goto IL_2F5;
					}
					goto IL_1E9;
				}
				IL_3C6:
				if (text != text2 && string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					if (this.failGameCounter == 0)
					{
						this.failGameCounter++;
						Thread.Sleep(2000);
						continue;
					}
					this.CanSearch = false;
					this.firstTeamLeader.StartGame(this.firstTeamLeader.LobbyState, false);
					foreach (Player player7 in this.firstTeam)
					{
						player7.match_id = string.Empty;
					}
					foreach (Player player8 in this.secondTeam)
					{
						player8.match_id = string.Empty;
					}
					Thread.Sleep(7000);
					this.secondTeamLeader.AcceptGame();
					Thread.Sleep(new Random().Next(90, 200) * 1000);
					this.CanSearch = true;
				}
				else if (text != text2 && !string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
				{
					if (this.failGameCounter == 0)
					{
						this.failGameCounter++;
						Thread.Sleep(2000);
						continue;
					}
					this.CanSearch = false;
					this.secondTeamLeader.StartGame(this.secondTeamLeader.LobbyState, false);
					foreach (Player player9 in this.firstTeam)
					{
						player9.match_id = string.Empty;
					}
					foreach (Player player10 in this.secondTeam)
					{
						player10.match_id = string.Empty;
					}
					Thread.Sleep(7000);
					this.firstTeamLeader.AcceptGame();
					Thread.Sleep(new Random().Next(90, 200) * 1000);
					this.CanSearch = true;
				}
				Thread.Sleep(2000);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00021A68 File Offset: 0x0001FC68
		public void StartBoosting()
		{
			try
			{
				CancellationTokenSource cancellationTokenSource = this.source;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			catch (Exception ex)
			{
				LogService.GetInstance.LogInformation(ex.ToString());
			}
			this.source = new CancellationTokenSource();
			this.cancellationToken = this.source.Token;
			this.source.Token.Register(new Action(this.OnCancelled));
			if (this._boostTask.SessionType.ToLower() != "case farm" && this._boostTask.SessionType.ToLower() != "transfer" && this._boostTask.SessionType.ToLower() != "fsm")
			{
				Thread acceptTask = this.AcceptTask;
				if (acceptTask != null)
				{
					acceptTask.Abort();
				}
				this.AcceptTask = new Thread(delegate
				{
					using (this.source.Token.Register(new Action(Thread.CurrentThread.Abort)))
					{
						this.MatchAccept_worker();
					}
				});
				this.AcceptTask.Start();
				this.firstTeamLeader.IsPlayerLeader = true;
				this.secondTeamLeader.IsPlayerLeader = true;
			}
			this.MainTask = new Task(delegate
			{
				using (this.source.Token.Register(new Action(Thread.CurrentThread.Abort)))
				{
					try
					{
						if (this._boostTask.SessionType.ToLower() != "case farm" && this._boostTask.SessionType.ToLower() != "transfer")
						{
							if (!(this.firstTeam.All((Player player) => player.WindowLoaded) & this.secondTeam.All((Player player) => player.WindowLoaded)))
							{
								this.RunPlayers();
								Thread.Sleep(10000);
							}
							Thread.Sleep(5000);
							if (this._boostTask.SessionType.ToLower() != "fsm" && this._boostTask.SessionType.ToLower() != "dm")
							{
								if (this._boostTask.ShortGame)
								{
									this.firstTeamLeader.SelectShortGame();
									this.secondTeamLeader.SelectShortGame();
								}
								else
								{
									this.firstTeamLeader.SelectLongGame();
									this.secondTeamLeader.SelectLongGame();
								}
								int num = 15;
								if (this._boostTask.ShortGame)
								{
									num = 8;
								}
								this.gsi.HalfRoundsCount = num;
								int num2 = new int[]
								{
									this.firstTeam.Count<Player>(),
									this.secondTeam.Count<Player>()
								}.Max();
								foreach (Player player17 in this.firstTeam.Concat(this.secondTeam))
								{
									player17.CompetitiveMode = num2;
								}
							}
							this.PlayedGames = 0;
							this.BoostWinTeam = 0;
						}
						if (!(this._boostTask.SessionType.ToLower() == "win boost") && !(this._boostTask.SessionType.ToLower() == "rank boost") && !(this._boostTask.SessionType.ToLower() == "calibration") && !(this._boostTask.SessionType.ToLower() == "global boost"))
						{
							if (this._boostTask.SessionType.ToLower() == "client boost")
							{
								if (this._boostTask.NeedClientRank != 0)
								{
									for (;;)
									{
										try
										{
											if (this.firstTeam.Count < this.secondTeam.Count)
											{
												this.CollectLobby(1, true, 1).Wait();
												this.BoostWinTeam = 1;
												this.MyGameMode = "Team 1";
											}
											else
											{
												if (this.secondTeam.Count >= this.firstTeam.Count)
												{
													Logs.AddNewLog(new BoostLog(this._boostTask.Name, "Something is wrong with accounts"));
													return;
												}
												this.CollectLobby(1, true, 2).Wait();
												this.BoostWinTeam = 2;
												this.MyGameMode = "Team 2";
											}
											Thread.Sleep(3000);
											Player player2 = null;
											if (this.BoostWinTeam == 1)
											{
												player2 = this.firstTeamLeader;
											}
											else if (this.BoostWinTeam == 2)
											{
												player2 = this.secondTeamLeader;
											}
											foreach (KeyValuePair<string, int> keyValuePair in player2.LobbyRanks)
											{
												if (!this.gsi.SteamIds.Contains(keyValuePair.Key))
												{
													Console.WriteLine("========================================");
													Console.WriteLine(string.Format("CLIENT DETECTOR 3000 : {0}", keyValuePair.Value));
													Console.WriteLine("========================================");
													this.NowClientRank = keyValuePair.Value;
												}
											}
											if (this._boostTask.NeedClientRank <= this.NowClientRank)
											{
												Logs.AddNewLog(new BoostLog(this._boostTask.Name, "Completed"));
												foreach (Player player3 in this.firstTeam.Where((Player pl) => pl.Login != this.firstTeamLeader.Login))
												{
													player3.LeaveLobby();
												}
												foreach (Player player4 in this.secondTeam.Where((Player pl) => pl.Login != this.secondTeamLeader.Login))
												{
													player4.LeaveLobby();
												}
												return;
											}
											ResourceDictionary resourceDictionary = new ResourceDictionary
											{
												Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
											};
											if (resourceDictionary != null)
											{
												string text = resourceDictionary["m_Message_BoostingClient"] as string;
												Logs.AddNewLog(new BoostLog(this._boostTask.Name, text));
											}
											this.StartGameSearching().Wait();
											this.MatchProcess().Wait();
											Thread.Sleep(5000);
											this.SwapClient();
										}
										catch (Exception ex2)
										{
											Console.WriteLine(ex2);
										}
									}
								}
								else
								{
									for (;;)
									{
										try
										{
											if (this.firstTeam.Count < this.secondTeam.Count)
											{
												this.CollectLobby(1, true, 1).Wait();
												this.BoostWinTeam = 1;
												this.MyGameMode = "Team 1";
											}
											else
											{
												if (this.secondTeam.Count >= this.firstTeam.Count)
												{
													Logs.AddNewLog(new BoostLog(this._boostTask.Name, "Something is wrong with accounts"));
													return;
												}
												this.CollectLobby(1, true, 2).Wait();
												this.BoostWinTeam = 2;
												this.MyGameMode = "Team 2";
											}
											Thread.Sleep(3000);
											Player player5 = null;
											if (this.BoostWinTeam == 1)
											{
												player5 = this.firstTeamLeader;
											}
											else if (this.BoostWinTeam == 2)
											{
												player5 = this.secondTeamLeader;
											}
											foreach (KeyValuePair<string, int> keyValuePair2 in player5.LobbyRanks)
											{
												if (!this.gsi.SteamIds.Contains(keyValuePair2.Key))
												{
													Console.WriteLine("========================================");
													Console.WriteLine(string.Format("CLIENT DETECTOR 3000 : {0}", keyValuePair2.Value));
													Console.WriteLine("========================================");
													this.NowClientRank = keyValuePair2.Value;
												}
											}
											ResourceDictionary resourceDictionary2 = new ResourceDictionary
											{
												Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative)
											};
											if (resourceDictionary2 != null)
											{
												string text2 = resourceDictionary2["m_Message_BoostingClient"] as string;
												Logs.AddNewLog(new BoostLog(this._boostTask.Name, text2));
											}
											this.StartGameSearching().Wait();
											this.MatchProcess().Wait();
											Thread.Sleep(5000);
											this.SwapClient();
										}
										catch (Exception ex3)
										{
											Console.WriteLine(ex3);
										}
									}
								}
							}
							else if (!(this._boostTask.SessionType.ToLower() == "xp boost"))
							{
								if (this._boostTask.SessionType.ToLower() == "case farm")
								{
									this.farmedAccounts.Clear();
									if (!string.IsNullOrEmpty(Settings.GetInstance.CsGoPath))
									{
										if (!File.Exists(Settings.GetInstance.CsGoPath + "\\csgo\\maps\\achievement_boost.bsp"))
										{
											try
											{
												using (WebClient webClient = new WebClient())
												{
													webClient.DownloadFile("https://vbpanel.com/panel/download/maps/achievement_boost.bsp", Settings.GetInstance.CsGoPath + "\\csgo\\maps\\achievement_boost.bsp");
												}
											}
											catch (Exception ex4)
											{
												LogService.GetInstance.LogInformation(ex4.ToString());
											}
										}
										if (!File.Exists(Settings.GetInstance.CsGoPath + "\\csgo\\maps\\achievement_boost.bsp.ztmp"))
										{
											try
											{
												using (WebClient webClient2 = new WebClient())
												{
													webClient2.DownloadFile("https://vbpanel.com/panel/download/maps/achievement_boost.bsp.ztmp", Settings.GetInstance.CsGoPath + "\\csgo\\maps\\achievement_boost.bsp.ztmp");
												}
											}
											catch (Exception ex5)
											{
												LogService.GetInstance.LogInformation(ex5.ToString());
											}
										}
									}
									if (!string.IsNullOrEmpty(Settings.GetInstance.IDLEPath) && File.Exists(Settings.GetInstance.IDLEPath) && Process.GetProcessesByName("srcds").Length == 0)
									{
										new Process
										{
											StartInfo = new ProcessStartInfo
											{
												WindowStyle = ProcessWindowStyle.Hidden,
												FileName = "cmd",
												Arguments = "/c \"" + Settings.GetInstance.IDLEPath + "\"",
												WorkingDirectory = Path.GetDirectoryName(Settings.GetInstance.IDLEPath),
												Verb = "runas"
											}
										}.Start();
										Thread.Sleep(2000);
										Process process = Process.GetProcessesByName("srcds").FirstOrDefault<Process>();
										if (process != null)
										{
											Session.ShowWindow(process.MainWindowHandle, 6);
										}
									}
									for (;;)
									{
										foreach (Player player6 in this.firstTeam)
										{
											if (player6.Started && player6.WindowLoaded && (DateTime.Now - player6.LastStart).TotalSeconds > 600.0 && player6.PlayerState == PlayerState.IN_LOBBY)
											{
												this.farmedAccounts.Add(player6.Login);
												player6.StopPlayer();
												player6.LastDrop = DateTime.Now;
												player6.SaveInfo();
											}
										}
										int num3;
										if (int.TryParse(Settings.GetInstance.CaseFarmAtTime, out num3) && num3 != 0)
										{
											int num4 = this.firstTeam.Count((Player x) => x.Started);
											int num5 = this.firstTeam.Count((Player x) => x.WindowLoaded);
											if (num4 == num5 && num5 < num3)
											{
												IEnumerable<Player> enumerable = this.firstTeam.Where((Player x) => !x.Started && !this.farmedAccounts.Contains(x.Login));
												if (this._boostTask.CaseFarmType == 1)
												{
													enumerable = from x in this.firstTeam
														where (DateTime.Now - x.LastDrop).TotalDays > 7.0
														where !x.Started && !this.farmedAccounts.Contains(x.Login)
														select x;
												}
												if (enumerable.Count<Player>() > 0)
												{
													Player player7 = enumerable.ToList<Player>()[0];
													player7.IsCasefarm = true;
													player7.StartPlayer(false);
													int num6;
													if (!int.TryParse(Settings.GetInstance.CustomLaunchDelay, out num6))
													{
														num6 = 7000;
													}
													Thread.Sleep(num6);
												}
												else if (num5 == 0)
												{
													break;
												}
											}
											Thread.Sleep(1000);
										}
									}
								}
								else if (!(this._boostTask.SessionType.ToLower() == "transfer"))
								{
									if (this._boostTask.SessionType.ToLower() == "fsm")
									{
										Console.WriteLine("ждем");
										for (;;)
										{
											if (this.firstTeam.Concat(this.secondTeam).All((Player x) => !string.IsNullOrEmpty(x.currentMap)))
											{
												break;
											}
											Thread.Sleep(50);
										}
										Console.WriteLine("дождались");
										Thread.Sleep(35000);
										Console.WriteLine("waiting game start");
										for (;;)
										{
											if (this.firstTeamLeader.currentMapPhase == MapPhase.Live)
											{
												if (this.firstTeamLeader.currentRoundPhase == RoundPhase.Live)
												{
													break;
												}
											}
											Thread.Sleep(50);
										}
										Thread.Sleep(2000);
										this.firstTeam[new Random().Next(0, this.firstTeam.Count - 1)].CallvoteMapChange("ar_dizzy");
										Console.WriteLine("callvote");
										Thread.Sleep(2000);
										this.firstTeam.Concat(this.secondTeam).ToList<Player>().ForEach(delegate(Player x)
										{
											x.ConfirmCallvote();
										});
										for (;;)
										{
											Thread.Sleep(10000);
											Console.WriteLine("selecting");
											foreach (Player player8 in this.firstTeam)
											{
												player8.BeginClickingTeamSelect(1);
											}
											foreach (Player player9 in this.secondTeam)
											{
												player9.BeginClickingTeamSelect(2);
											}
											Console.WriteLine("waiting game start");
											for (;;)
											{
												if (this.firstTeamLeader.currentMapPhase == MapPhase.Live)
												{
													if (this.firstTeamLeader.currentRoundPhase == RoundPhase.Live)
													{
														break;
													}
												}
												Thread.Sleep(50);
											}
											Console.WriteLine("yesss");
											this.firstTeam.ForEach(delegate(Player x)
											{
												x.StopClickingTeamSelect();
											});
											this.secondTeam.ForEach(delegate(Player x)
											{
												x.StopClickingTeamSelect();
											});
											Console.WriteLine("checking map = " + this.firstTeamLeader.currentMap);
											if (this.firstTeamLeader.currentMap.Contains("dizzy"))
											{
												Console.WriteLine("ABOBUS ИГРАЕМ ЕПТА");
												ExternalInfoCollector.SetupTargetPlayer(this.firstTeamLeader);
												this.firstTeam[1].StartCollectInfo();
												for (int i = 0; i < 8; i++)
												{
													Thread.Sleep(1000);
													while (this.firstTeamLeader.currentRoundPhase != RoundPhase.Live)
													{
														Thread.Sleep(50);
													}
													Console.WriteLine("beget");
													foreach (Player player10 in this.firstTeam)
													{
														player10.ResorePlayerWalk();
													}
													Thread.Sleep(100);
													foreach (Player player11 in this.secondTeam)
													{
														player11.ExecConsoleText("+moveleft");
													}
													this.firstTeamLeader.ExecConsoleText("-left; -right");
													this.firstTeamLeader.ExecConsoleText("bind q +left");
													this.firstTeamLeader.ExecConsoleText("bind e +right");
													Thread.Sleep(500);
													this.firstTeamLeader.GoGoDizzy();
													foreach (Player player12 in this.secondTeam)
													{
														player12.ExecConsoleText("-moveleft");
													}
													this.firstTeamLeader.KillAllEnemiesByKnife();
													this.firstTeamLeader.ExecConsoleText("+left; +right");
													while (this.firstTeamLeader.currentRoundPhase == RoundPhase.Live)
													{
														Thread.Sleep(50);
													}
													Console.WriteLine("stopped");
													Thread.Sleep(2000);
												}
												this.firstTeam[1].StopCollectInfo();
												Console.WriteLine("clicking map");
												foreach (Player player13 in this.secondTeam)
												{
													player13.BeginClickingMapChange();
												}
												Thread.Sleep(5000);
												Console.WriteLine("stop clicking map");
												using (List<Player>.Enumerator enumerator3 = this.secondTeam.GetEnumerator())
												{
													while (enumerator3.MoveNext())
													{
														Player player14 = enumerator3.Current;
														player14.StopClickingMapChange();
													}
													continue;
												}
											}
											Thread.Sleep(2000);
											this.firstTeam.Concat(this.secondTeam).ToList<Player>()[new Random().Next(0, (this.firstTeam.Count - 1) * 2)].CallvoteMapChange("ar_dizzy");
											Thread.Sleep(2000);
											this.firstTeam.Concat(this.secondTeam).ToList<Player>().ForEach(delegate(Player x)
											{
												x.ConfirmCallvote();
											});
										}
									}
									else if (this._boostTask.SessionType.ToLower() == "dm")
									{
										Console.WriteLine("ждем");
										for (;;)
										{
											if (this.firstTeam.Concat(this.secondTeam).All((Player x) => x.currentMap.ToLower().Contains("dust")))
											{
												break;
											}
											Thread.Sleep(50);
										}
										Console.WriteLine("дождались");
										Thread.Sleep(25000);
										Console.WriteLine("Master started spinning");
										this.firstTeamLeader.StartStreamingPosition(this.source.Token);
										Thread.Sleep(1000);
										Console.WriteLine("lessssss go");
										foreach (Player player15 in this.firstTeam)
										{
											if (player15.Login != this.firstTeamLeader.Login)
											{
												player15.StartDustDMDieProcess(this.firstTeamLeader, this.source.Token);
												Thread.Sleep(1000);
											}
										}
										foreach (Player player16 in this.secondTeam)
										{
											player16.StartDustDMDieProcess(this.firstTeamLeader, this.source.Token);
											Thread.Sleep(1000);
										}
										for (;;)
										{
											Thread.Sleep(1000);
										}
									}
								}
								else
								{
									string text3 = string.Format("{0}\\maFiles\\", Environment.CurrentDirectory);
									Dictionary<string, SteamGuardAccount> dictionary = new Dictionary<string, SteamGuardAccount>();
									List<SteamGuardAccount> list = new List<SteamGuardAccount>();
									if (!string.IsNullOrEmpty(text3) && Directory.Exists(text3))
									{
										foreach (string text4 in from t in Directory.GetFiles(text3)
											where Path.GetExtension(t) == ".maFile"
											select t)
										{
											SteamGuardAccount guard = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(text4));
											if (list.FirstOrDefault((SteamGuardAccount t) => t.AccountName == guard.AccountName) == null)
											{
												list.Add(guard);
											}
											else
											{
												SteamGuardAccount steamGuardAccount = list.FirstOrDefault((SteamGuardAccount t) => t.AccountName == guard.AccountName);
												if (steamGuardAccount.Session.RefreshToken == null)
												{
													list.Remove(steamGuardAccount);
													list.Add(guard);
												}
											}
										}
										dictionary = list.ToDictionary((SteamGuardAccount t) => t.AccountName, (SteamGuardAccount t) => t);
									}
									bool flag = false;
									try
									{
										List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>();
										SteamGuardAccount masterGuard = new SteamGuardAccount();
										Player master_account = ((this._boostTask.FirstTeam.Count == 1) ? this._boostTask.FirstTeam[0] : null);
										if (master_account == null)
										{
											string gameMode = this._boostTask.GameMode;
											string clientInviteCode = this._boostTask.ClientInviteCode;
											if (string.IsNullOrEmpty(gameMode) || string.IsNullOrWhiteSpace(gameMode) || string.IsNullOrEmpty(clientInviteCode) || string.IsNullOrWhiteSpace(clientInviteCode))
											{
												throw new Exception("Master is empty!");
											}
											flag = true;
											masterGuard.Session = new SessionData();
											if (gameMode.StartsWith("765") && gameMode.Length == 17)
											{
												masterGuard.Session.SteamID = Convert.ToUInt64(gameMode);
											}
											else
											{
												if (!gameMode.All(new Func<char, bool>(char.IsDigit)))
												{
													TransferPageViewModel.Instance.AddNewLog("Master Error", gameMode);
													Session.TransferLogToFile("Master Error", gameMode);
													return;
												}
												masterGuard.Session.SteamID = (ulong)(long.Parse(gameMode) + long.Parse("76561197960265728"));
											}
										}
										if (!flag)
										{
											if (dictionary.TryGetValue(master_account.Login, out masterGuard))
											{
												if (masterGuard.Session.RefreshToken == null)
												{
													if (!AsyncHelpers.RunSync<bool>(() => masterGuard.RefreshSession(masterGuard, master_account.Password)))
													{
														TransferPageViewModel.Instance.AddNewLog("Login Error", masterGuard.AccountName);
														Session.TransferLogToFile("Login Error", masterGuard.AccountName);
													}
													else
													{
														string text5 = JsonConvert.SerializeObject(masterGuard);
														File.WriteAllText(string.Format("{0}\\maFiles\\{1}.maFile", Environment.CurrentDirectory, masterGuard.Session.SteamID), text5);
														dictionary[masterGuard.AccountName] = masterGuard;
														TransferPageViewModel.Instance.AddNewLog("Session Updated", masterGuard.AccountName);
														Session.TransferLogToFile("Session Updated", masterGuard.AccountName);
													}
												}
												if (!masterGuard.Session.IsAccessTokenExpired() || string.IsNullOrEmpty(masterGuard.Session.RefreshToken))
												{
													goto IL_15FF;
												}
												try
												{
													AsyncHelpers.RunSync(() => masterGuard.Session.RefreshAccessToken());
													goto IL_15FF;
												}
												catch (Exception)
												{
													TransferPageViewModel.Instance.AddNewLog("Login Error", masterGuard.AccountName);
													return;
												}
											}
											throw new Exception("Master don't have maFile");
										}
										IL_15FF:
										Thread.Sleep(1000);
										using (List<Player>.Enumerator enumerator3 = this._boostTask.SecondTeam.GetEnumerator())
										{
											while (enumerator3.MoveNext())
											{
												Player account = enumerator3.Current;
												if (master_account == null || !(account.Login == master_account.Login))
												{
													try
													{
														TransferPageViewModel.Instance.AddNewLog("Checking", account.Login);
														Session.TransferLogToFile("Checking", account.Login);
														if (account.HasMaFile)
														{
															SteamGuardAccount sga = new SteamGuardAccount();
															if (!dictionary.TryGetValue(account.Login, out sga))
															{
																throw new Exception("Account don't have maFile");
															}
															if (sga.Session.RefreshToken == null)
															{
																if (!AsyncHelpers.RunSync<bool>(() => sga.RefreshSession(sga, account.Password)))
																{
																	TransferPageViewModel.Instance.AddNewLog("Login Error", sga.AccountName);
																	Session.TransferLogToFile("Login Error", sga.AccountName);
																	continue;
																}
																string text6 = JsonConvert.SerializeObject(sga);
																File.WriteAllText(string.Format("{0}\\maFiles\\{1}.maFile", Environment.CurrentDirectory, sga.Session.SteamID), text6);
																dictionary[sga.AccountName] = sga;
																TransferPageViewModel.Instance.AddNewLog("Session Updated", sga.AccountName);
																Session.TransferLogToFile("Session Updated", sga.AccountName);
															}
															if (sga.Session.IsAccessTokenExpired() && !string.IsNullOrEmpty(sga.Session.RefreshToken))
															{
																try
																{
																	AsyncHelpers.RunSync(() => sga.Session.RefreshAccessToken());
																}
																catch (Exception)
																{
																	TransferPageViewModel.Instance.AddNewLog("Login Error", sga.AccountName);
																	continue;
																}
															}
															CookieContainer cookies = sga.Session.GetCookies();
															Thread.Sleep(1000);
															JObject jobject = AsyncHelpers.RunSync<JObject>(() => TradeLogic.GetInventory(sga.Session.SteamID.ToString(), cookies));
															if (!(bool)jobject["success"])
															{
																Regex regex = new Regex("\\(\\d+\\)");
																if (jobject["Error"] == null)
																{
																	TransferPageViewModel.Instance.AddNewLog("Inventory empty", sga.AccountName);
																	Session.TransferLogToFile("Inventory empty", sga.AccountName);
																	continue;
																}
																if (jobject["Error"].ToString() == "429 Too Many Requests.")
																{
																	TransferPageViewModel.Instance.AddNewLog("Error 429", sga.AccountName);
																	Session.TransferLogToFile("Error 429", sga.AccountName);
																	continue;
																}
																if (jobject["Error"].ToString() == "403 Forbidden.")
																{
																	TransferPageViewModel.Instance.AddNewLog("Error 403", sga.AccountName);
																	Session.TransferLogToFile("Error 403", sga.AccountName);
																	continue;
																}
																if (jobject["Error"].ToString() == "500 Internal Server Error.")
																{
																	TransferPageViewModel.Instance.AddNewLog("Error 500", sga.AccountName);
																	Session.TransferLogToFile("Error 500", sga.AccountName);
																	continue;
																}
																if (regex.Matches(jobject["Error"].ToString()).Count > 0)
																{
																	string value = regex.Match(jobject["Error"].ToString()).Value;
																	TransferPageViewModel.Instance.AddNewLog(string.Format("Error {0}", value), sga.AccountName);
																	Session.TransferLogToFile(string.Format("Error {0}", value), sga.AccountName);
																	continue;
																}
															}
															List<Asset> itemsToSend = new List<Asset>();
															if (jobject["rgInventory"].ToString().Length == 2)
															{
																TransferPageViewModel.Instance.AddNewLog("No items to trade", account.Login);
																Session.TransferLogToFile("No items to trade", account.Login);
															}
															else
															{
																foreach (KeyValuePair<string, JObject> keyValuePair3 in JsonConvert.DeserializeObject<Dictionary<string, JObject>>(jobject["rgInventory"].ToString()))
																{
																	itemsToSend.Add(new Asset
																	{
																		appid = 730,
																		contextid = "2",
																		amount = keyValuePair3.Value["amount"].ToString(),
																		assetid = keyValuePair3.Value["id"].ToString()
																	});
																}
																if (itemsToSend.Count != 0)
																{
																	Thread.Sleep(1000);
																	JObject jobject2 = AsyncHelpers.RunSync<JObject>(() => TradeLogic.SendTrade(cookies, masterGuard.Session.SteamID.ToString(), "", itemsToSend, this._boostTask.ClientInviteCode));
																	if (jobject2.ToString().Contains("Error"))
																	{
																		if (jobject2["Error"].ToString() == "429 Too Many Requests.")
																		{
																			TransferPageViewModel.Instance.AddNewLog("Error 429", sga.AccountName);
																			Session.TransferLogToFile("Error 429", sga.AccountName);
																			continue;
																		}
																		if (jobject2["Error"].ToString() == "403 Forbidden.")
																		{
																			TransferPageViewModel.Instance.AddNewLog("Error 403", sga.AccountName);
																			Session.TransferLogToFile("Error 403", sga.AccountName);
																			continue;
																		}
																		if (jobject2["Error"].ToString() == "500 Internal Server Error.")
																		{
																			TransferPageViewModel.Instance.AddNewLog("Error Too Many Trades", sga.AccountName);
																			Session.TransferLogToFile("Error 500", sga.AccountName);
																			continue;
																		}
																	}
																	else if (!jobject2.ToString().Contains("tradeofferid"))
																	{
																		TransferPageViewModel.Instance.AddNewLog("Failed", account.Login);
																		Session.TransferLogToFile("Failed", account.Login);
																		continue;
																	}
																	Thread.Sleep(1000);
																	Confirmation[] array = AsyncHelpers.RunSync<Confirmation[]>(() => sga.FetchConfirmationsAsync());
																	for (int j = 0; j < array.Length; j++)
																	{
																		Confirmation conf = array[j];
																		if (conf.Creator.ToString() == jobject2["tradeofferid"].ToString())
																		{
																			if (AsyncHelpers.RunSync<bool>(() => sga.AcceptConfirmation(conf)))
																			{
																				TransferPageViewModel.Instance.AddNewLog("Sent", account.Login);
																				Session.TransferLogToFile("Sent", account.Login);
																				list2.Add(new ValueTuple<string, string>(sga.Session.SteamID.ToString(), jobject2["tradeofferid"].ToString()));
																			}
																			else
																			{
																				TransferPageViewModel.Instance.AddNewLog("Failed", account.Login);
																				Session.TransferLogToFile("Failed", account.Login);
																			}
																		}
																	}
																	Thread.Sleep(1000);
																}
																else
																{
																	TransferPageViewModel.Instance.AddNewLog("No items to trade", account.Login);
																	Session.TransferLogToFile("No items to trade", account.Login);
																}
															}
														}
														else
														{
															TransferPageViewModel.Instance.AddNewLog("Failed 2FA", account.Login);
															Session.TransferLogToFile("Failed 2FA", account.Login);
														}
													}
													catch (Exception ex6)
													{
														Console.WriteLine(ex6);
														TransferPageViewModel.Instance.AddNewLog("Error", account.Login);
														Session.TransferLogToFile("Error", account.Login);
													}
												}
											}
										}
										Thread.Sleep(2000);
										if (!flag)
										{
											CookieContainer mcookies = masterGuard.Session.GetCookies();
											mcookies.Add(new Cookie("steamid", masterGuard.Session.SteamID.ToString(), "/", ".steamcommunity.com"));
											mcookies.Add(new Cookie("steamLogin", master_account.Login, "/", ".steamcommunity.com")
											{
												HttpOnly = true
											});
											mcookies.Add(new Cookie("Steam_Language", "english", "/", ".steamcommunity.com"));
											mcookies.Add(new Cookie("steamCurrencyId", "1", "/", ".steamcommunity.com"));
											int num7 = 1;
											int count = list2.Count;
											using (List<ValueTuple<string, string>>.Enumerator enumerator6 = list2.GetEnumerator())
											{
												while (enumerator6.MoveNext())
												{
													ValueTuple<string, string> trade = enumerator6.Current;
													AsyncHelpers.RunSync<JObject>(() => TradeLogic.AcceptTrade(mcookies, masterGuard.Session.SessionID, trade.Item1, trade.Item2));
													if (count > 10 && num7 % 10 == 0)
													{
														TransferPageViewModel.Instance.AddNewLog(string.Format("Accepted {0}/{1}", num7, count), master_account.Login);
														Session.TransferLogToFile(string.Format("Accepted {0}/{1}", num7, count), master_account.Login);
													}
													if (count < 10)
													{
														TransferPageViewModel.Instance.AddNewLog(string.Format("Accepted {0}/{1}", num7, count), master_account.Login);
														Session.TransferLogToFile(string.Format("Accepted {0}/{1}", num7, count), master_account.Login);
													}
													num7++;
													Thread.Sleep(1000);
												}
											}
											TransferPageViewModel.Instance.AddNewLog("Got items", master_account.Login);
											Session.TransferLogToFile("Got items", master_account.Login);
										}
										TransferPageViewModel.Instance.AddNewLog("Done", this._boostTask.Name);
										Session.TransferLogToFile("Done", this._boostTask.Name);
									}
									catch (Exception ex7)
									{
										System.Windows.Forms.MessageBox.Show(ex7.ToString(), "VertigoBoostPanel");
									}
									this._boostTask.StopSession();
								}
							}
							else
							{
								for (int k = 0; k < this._boostTask.CountGame; k++)
								{
									try
									{
										this.MyGameMode = "Draw";
										this.CollectLobby(1, false, 0).Wait();
										Thread.Sleep(2000);
										this.StartGameSearching().Wait();
										this.MatchProcess().Wait();
										Thread.Sleep(5000);
										this.PlayedGames++;
									}
									catch (Exception ex8)
									{
										Console.WriteLine(ex8);
									}
								}
							}
						}
						else if (this._boostTask.GameMode == "2Win/2Lose")
						{
							for (int l = 0; l < this._boostTask.CountGame; l++)
							{
								for (int m = 0; m < 2; m++)
								{
									try
									{
										this.MyGameMode = "Team 1";
										this.CollectLobby(4, false, 0).Wait();
										Thread.Sleep(2000);
										this.StartGameSearching().Wait();
										this.MatchProcess().Wait();
										Thread.Sleep(5000);
										this.PlayedGames++;
										goto IL_2365;
									}
									catch (Exception ex9)
									{
										Console.WriteLine(ex9);
										goto IL_2365;
									}
									break;
									IL_2365:;
								}
								for (int n = 0; n < 2; n++)
								{
									try
									{
										this.MyGameMode = "Team 2";
										this.CollectLobby(4, false, 0).Wait();
										Thread.Sleep(2000);
										this.StartGameSearching().Wait();
										this.MatchProcess().Wait();
										Thread.Sleep(5000);
										this.PlayedGames++;
									}
									catch (Exception ex10)
									{
										Console.WriteLine(ex10);
									}
								}
							}
						}
						else if (!(this._boostTask.GameMode == "Win/Lose") && !(this._boostTask.SessionType.ToLower() == "global boost"))
						{
							if (!(this._boostTask.SessionType.ToLower() == "calibration"))
							{
								this.MyGameMode = this._boostTask.GameMode;
							}
							else
							{
								this.MyGameMode = "Team 1";
							}
							for (int num8 = 0; num8 < this._boostTask.CountGame; num8++)
							{
								try
								{
									this.CollectLobby(1, false, 0).Wait();
									Thread.Sleep(2000);
									this.StartGameSearching().Wait();
									this.MatchProcess().Wait();
									Thread.Sleep(5000);
									this.PlayedGames++;
								}
								catch (Exception ex11)
								{
									LogService.GetInstance.LogInformation(ex11.ToString());
								}
							}
						}
						else
						{
							if (this._boostTask.SessionType.ToLower() == "global boost")
							{
								this._boostTask.CountGame = 21;
							}
							this.PlayedGames = 0;
							int num9 = 0;
							while (num9 < this._boostTask.CountGame)
							{
								try
								{
									this.MyGameMode = "Team 1";
									this.CollectLobby(2, false, 0).Wait();
									Thread.Sleep(2000);
									this.StartGameSearching().Wait();
									this.MatchProcess().Wait();
									Thread.Sleep(5000);
									this.PlayedGames++;
								}
								catch (Exception ex12)
								{
									Console.WriteLine(ex12);
								}
								try
								{
									IL_24FE:
									this.MyGameMode = "Team 2";
									this.CollectLobby(2, false, 0).Wait();
									Thread.Sleep(2000);
									this.StartGameSearching().Wait();
									this.MatchProcess().Wait();
									Thread.Sleep(5000);
									this.PlayedGames++;
								}
								catch (Exception ex13)
								{
									Console.WriteLine(ex13);
								}
								num9++;
								continue;
								goto IL_24FE;
							}
						}
					}
					catch (Exception ex14)
					{
						Console.WriteLine(ex14);
					}
					Thread acceptTask2 = this.AcceptTask;
					if (acceptTask2 != null)
					{
						acceptTask2.Abort();
					}
				}
			}, this.cancellationToken, TaskCreationOptions.LongRunning);
			this.MainTask.Start();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		private static void TransferLogToFile(object message, object account)
		{
			string text = string.Format("[{0}][{1}] => [{2}]", DateTime.Now, account, message);
			string text2 = string.Format("{0}\\data\\transfer.log", Environment.CurrentDirectory);
			if (!File.Exists(text2))
			{
				File.Create(text2).Close();
			}
			File.AppendAllText(text2, text + "\n");
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00021C18 File Offset: 0x0001FE18
		private void SwapClient()
		{
			if (Program.GetInstance.SubscriptionLevel != 0)
			{
				List<Player> list = new List<Player>();
				using (List<Player>.Enumerator enumerator = this.firstTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player3 = enumerator.Current;
						list.Add(StaticData.GetInstance.Players.FirstOrDefault((Player p) => p.Login == player3.Login));
					}
				}
				List<Player> list2 = new List<Player>();
				using (List<Player>.Enumerator enumerator = this.secondTeam.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player = enumerator.Current;
						list2.Add(StaticData.GetInstance.Players.FirstOrDefault((Player p) => p.Login == player.Login));
					}
				}
				if (list.Count < list2.Count)
				{
					Player player2 = list2[4];
					list2.Remove(player2);
					list.Add(player2);
				}
				else if (this.secondTeam.Count < list.Count)
				{
					Player player2 = list[4];
					list.Remove(player2);
					list2.Add(player2);
				}
				this.gsi.first_team = list;
				this.gsi.second_team = list2;
				this._boostTask.FirstTeam = list;
				this._boostTask.SecondTeam = list2;
				return;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00021DB8 File Offset: 0x0001FFB8
		private void RunPlayers()
		{
			object runPlayerLock = Session._runPlayerLock;
			lock (runPlayerLock)
			{
				foreach (Player player3 in this.firstTeam.Concat(this.secondTeam))
				{
					player3.CheckEnterToSteam();
				}
				foreach (Player player2 in this.firstTeam.Concat(this.secondTeam))
				{
					player2.StartPlayer(false);
					if (this._boostTask.SessionType.ToLower() == "case farm")
					{
						player2.IsCasefarm = true;
						while (!player2.CaseFarmJoinedServer)
						{
							Thread.Sleep(50);
						}
					}
					int num;
					if (!int.TryParse(Settings.GetInstance.CustomLaunchDelay, out num))
					{
						num = 7000;
					}
					Thread.Sleep(num);
				}
				for (;;)
				{
					if (this.firstTeam.Concat(this.secondTeam).All((Player player) => player.WindowLoaded))
					{
						break;
					}
					Thread.Sleep(50);
				}
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00021F4C File Offset: 0x0002014C
		public void ClosePlayers()
		{
			Console.WriteLine("stop started");
			foreach (Player player in this.firstTeam)
			{
				player.StopPlayer();
			}
			foreach (Player player2 in this.secondTeam)
			{
				player2.StopPlayer();
			}
			Console.WriteLine("stop ended");
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00021FF4 File Offset: 0x000201F4
		public void StopBoosting()
		{
			try
			{
				foreach (Player player in this.firstTeam)
				{
					player.Disconnect();
				}
				foreach (Player player2 in this.secondTeam)
				{
					player2.Disconnect();
				}
				GC.Collect();
			}
			catch (Exception ex)
			{
				LogService.GetInstance.LogInformation(ex.ToString());
			}
			Thread acceptTask = this.AcceptTask;
			if (acceptTask != null)
			{
				acceptTask.Abort();
			}
			CancellationTokenSource cancellationTokenSource = this.source;
			if (cancellationTokenSource == null)
			{
				return;
			}
			cancellationTokenSource.Cancel();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000220D4 File Offset: 0x000202D4
		private void OnCancelled()
		{
			BoostTask boostTask = this._boostTask;
			if (boostTask != null)
			{
				boostTask.SetStatus(VertigoBoostPanel.Model.enums.TaskStatus.COMPLETED);
				return;
			}
		}

		// Token: 0x06000519 RID: 1305
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x0600051A RID: 1306
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

		// Token: 0x0600051B RID: 1307
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetCursorPos(int X, int Y);

		// Token: 0x0600051C RID: 1308
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600051D RID: 1309
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		// Token: 0x0600051E RID: 1310
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		// Token: 0x0600051F RID: 1311
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hwnd, out Session.RECT lpRect);

		// Token: 0x0400048B RID: 1163
		private bool GameCoordinator;

		// Token: 0x0400048C RID: 1164
		private string LastGameLogID;

		// Token: 0x0400048D RID: 1165
		private bool newMethod;

		// Token: 0x0400048E RID: 1166
		private string MyGameMode = string.Empty;

		// Token: 0x0400048F RID: 1167
		private int NowClientRank;

		// Token: 0x04000490 RID: 1168
		private int BoostWinTeam;

		// Token: 0x04000491 RID: 1169
		public int PlayedGames;

		// Token: 0x04000492 RID: 1170
		private bool NeedAccept;

		// Token: 0x04000493 RID: 1171
		private bool Accepted;

		// Token: 0x04000494 RID: 1172
		private CancellationToken cancellationToken;

		// Token: 0x04000495 RID: 1173
		private CancellationTokenSource source;

		// Token: 0x04000496 RID: 1174
		public Task MainTask;

		// Token: 0x04000497 RID: 1175
		private Thread AcceptTask;

		// Token: 0x04000498 RID: 1176
		private BoostTask _boostTask;

		// Token: 0x04000499 RID: 1177
		private LobbyGSI gsi;

		// Token: 0x0400049A RID: 1178
		private List<string> farmedAccounts = new List<string>();

		// Token: 0x0400049B RID: 1179
		private bool CanSearch = true;

		// Token: 0x0400049C RID: 1180
		private int failGameCounter;

		// Token: 0x0400049D RID: 1181
		private static object _runPlayerLock = new object();

		// Token: 0x020000EE RID: 238
		public struct RECT
		{
			// Token: 0x0400049E RID: 1182
			public int Left;

			// Token: 0x0400049F RID: 1183
			public int Top;

			// Token: 0x040004A0 RID: 1184
			public int Right;

			// Token: 0x040004A1 RID: 1185
			public int Bottom;
		}
	}
}
