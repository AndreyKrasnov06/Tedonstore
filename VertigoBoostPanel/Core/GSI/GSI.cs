using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CSGSI;
using CSGSI.Nodes;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Model.GSI;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Core.GSI
{
	// Token: 0x02000128 RID: 296
	public class GSI
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0002F23C File Offset: 0x0002D43C
		public static void StartGSI()
		{
			GameStateListener gameStateListener = new GameStateListener(3000);
			gameStateListener.NewGameState += GSI.OnNewGameState;
			if (!gameStateListener.Start())
			{
				MessageBox.Show("Failed to start GSI. You launched the 2nd copy of panel.", "VertigoBoostPanel");
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00004184 File Offset: 0x00002384
		public static bool DisconnectTick
		{
			get
			{
				return Settings.GetInstance.PanelOffset != 0L;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002F288 File Offset: 0x0002D488
		private static void OnNewGameState(GameState gs)
		{
			try
			{
				Player player11 = StaticData.GetInstance.Players.FirstOrDefault(delegate(Player player)
				{
					string string_ = player.string_1;
					PlayerNode player12 = gs.Player;
					return string_ == ((player12 != null) ? player12.SteamID : null);
				});
				if (player11 != null)
				{
					Player player2 = player11;
					PlayerNode player3 = gs.Player;
					player2.ActiveWeapon = ((player3 != null) ? player3.Weapons.ActiveWeapon.Name : null);
					Player player4 = player11;
					PlayerNode player5 = gs.Player;
					player4.Team = ((player5 != null) ? player5.Team : PlayerTeam.Undefined);
					player11.currentMap = gs.Map.Name;
					player11.currentPlayerActivity = gs.Player.Activity;
					player11.currentMapPhase = gs.Map.Phase;
					player11.currentRoundPhase = gs.Round.Phase;
					player11.currentPlayerTeam = gs.Player.Team;
				}
				using (List<LobbyGSI>.Enumerator enumerator = GSI.ActiveLobbies.GetEnumerator())
				{
					Func<Player, bool> <>9__4;
					while (enumerator.MoveNext())
					{
						LobbyGSI _lobby = enumerator.Current;
						if (_lobby.first_team.All((Player pl) => pl.WindowLoaded))
						{
							if (_lobby.second_team.All((Player pl) => pl.WindowLoaded))
							{
								if (_lobby.AccountsToDisconnect.Count<Player>() > 0)
								{
									ParallelQuery<Player> parallelQuery = _lobby.AccountsToDisconnect.AsParallel<Player>();
									Func<Player, bool> func;
									if ((func = <>9__4) == null)
									{
										func = (<>9__4 = delegate(Player player)
										{
											string string_2 = player.string_1;
											PlayerNode player13 = gs.Player;
											return string_2 == ((player13 == null) ? null : player13.SteamID);
										});
									}
									Player player6 = parallelQuery.FirstOrDefault(func);
									if (player6 != null && GSI.RoundStarted(gs) && GSI.CheckPlayerActivity(gs) && GSI.CheckMapPhase(gs))
									{
										try
										{
											ValueTuple<int, int> valueTuple = new ValueTuple<int, int>(gs.Map.TeamCT.Score, gs.Map.TeamT.Score);
											ValueTuple<int, int> valueTuple2 = valueTuple;
											int num = _lobby.HalfRoundsCount;
											if (valueTuple2.Item1 != num || valueTuple2.Item2 != 0)
											{
												valueTuple2 = valueTuple;
												num = _lobby.HalfRoundsCount;
												if (valueTuple2.Item1 != 0)
												{
													goto IL_361;
												}
												if (valueTuple2.Item2 != num)
												{
													goto IL_361;
												}
											}
											if (_lobby.IsReconnected)
											{
												goto IL_383;
											}
											_lobby.ValueDisconnectWorker = false;
											_lobby.AccountsToDisconnect.Clear();
											_lobby.IsReconnected = true;
											if (_lobby.newMethod)
											{
												goto IL_383;
											}
											foreach (Player player7 in _lobby.first_team)
											{
												player7.Reconnect();
											}
											using (List<Player>.Enumerator enumerator2 = _lobby.second_team.GetEnumerator())
											{
												while (enumerator2.MoveNext())
												{
													Player player8 = enumerator2.Current;
													player8.Reconnect();
												}
												goto IL_383;
											}
											IL_361:
											Thread.Sleep(50);
											player6.Disconnect();
											_lobby.AccountsToDisconnect.Remove(player6);
											IL_383:;
										}
										catch (Exception ex)
										{
											throw ex;
										}
									}
								}
								if (gs.Player.SteamID.ToString() == _lobby.leaderID)
								{
									_lobby.leaderTeam = gs.Player.Team;
								}
								if (_lobby.SteamIds.Contains(gs.Player.SteamID.ToString()) && gs.Player.SteamID.ToString() != _lobby.leaderID)
								{
									_lobby.RoundPhase = gs.Round.Phase;
									if (!string.IsNullOrEmpty(gs.Map.Name))
									{
										_lobby.MapName = gs.Map.Name;
									}
									_lobby.xhase = gs.Map.Phase;
									_lobby.CountRoundsMatch = gs.Map.Round;
									_lobby.ScoreCT = gs.Map.TeamCT.Score;
									_lobby.ScoreT = gs.Map.TeamT.Score;
									if ((gs.Map.TeamCT.Score != _lobby.HalfRoundsCount && gs.Map.TeamT.Score != _lobby.HalfRoundsCount) || _lobby.IsReconnected)
									{
										goto IL_694;
									}
									_lobby.IsReconnected = true;
									_lobby.ValueDisconnectWorker = false;
									_lobby.AccountsToDisconnect.Clear();
									if (_lobby.newMethod)
									{
										goto IL_694;
									}
									if (_lobby.WhereDisconnect == 1)
									{
										using (List<Player>.Enumerator enumerator2 = _lobby.first_team.GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												Player player9 = enumerator2.Current;
												player9.Reconnect();
											}
											goto IL_694;
										}
									}
									if (_lobby.WhereDisconnect != 2)
									{
										goto IL_694;
									}
									using (List<Player>.Enumerator enumerator2 = _lobby.second_team.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											Player player10 = enumerator2.Current;
											player10.Reconnect();
										}
										goto IL_694;
									}
									IL_626:
									if (!_lobby.IsGameOver)
									{
										_lobby.IsGameOver = true;
										_lobby.IsPlanted = false;
										Task.Run(delegate
										{
											Thread.Sleep(2000);
											foreach (Player player14 in _lobby.first_team)
											{
												player14.Disconnect();
											}
											foreach (Player player15 in _lobby.second_team)
											{
												player15.Disconnect();
											}
										});
										_lobby.IsReconnected = false;
										_lobby.AccountsToDisconnect.Clear();
										_lobby.ValueDisconnectWorker = false;
										continue;
									}
									continue;
									IL_694:
									if (gs.Map.Phase == MapPhase.GameOver)
									{
										goto IL_626;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				LogService.GetInstance.LogInformation(ex2.ToString());
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002FA18 File Offset: 0x0002DC18
		private static bool RoundStarted(GameState gameState)
		{
			return gameState.Round.Phase == RoundPhase.FreezeTime || gameState.Round.Phase == RoundPhase.Live;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002FA44 File Offset: 0x0002DC44
		private static bool CheckPlayerActivity(GameState gameState)
		{
			return gameState.Player.Activity == PlayerActivity.Playing && gameState.Player.State.Health == 100;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002FA78 File Offset: 0x0002DC78
		private static bool CheckMapPhase(GameState gameState)
		{
			return gameState.Map.Phase == MapPhase.Live || gameState.Map.Phase == MapPhase.GameOver;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
		public static async Task<double> GameInterval(DateTime[] dates)
		{
			return (dates[0] - dates[1]).TotalSeconds;
		}

		// Token: 0x040005E8 RID: 1512
		public static List<LobbyGSI> ActiveLobbies = new List<LobbyGSI>();
	}
}
