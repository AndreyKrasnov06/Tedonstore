using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CSGSI.Nodes;
using ItemsTransfer.SteamAuth;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.WalkBot;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Model.enums;
using VertigoBoostPanel.Services.DataBase;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.Core.Player
{
	// Token: 0x02000116 RID: 278
	public class Player : PlayerWindowInteraction, INotifyPropertyChanged
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0002A0DC File Offset: 0x000282DC
		public Player(string login, string password, int pack, string st32, string st64, string code, int rank5x5, int rank2x2, int wins5x5, int wins2x2, int lvl, int mark, DateTime lastDrop)
		{
			this.me = this;
			this.Login = login.ToLower();
			this.Password = password;
			this.Prime = pack;
			this.Rank2x2 = rank2x2;
			this.Rank5x5 = rank5x5;
			this.Wins2x2 = wins2x2;
			this.Wins5x5 = wins5x5;
			this.Lvl = lvl;
			this.Mark = mark;
			this.LastDrop = lastDrop;
			this._playerShow = new AccountShow(this);
			this._playerShowInTask = new AccountShowInTask(this);
			this.string_1 = this.getSteamID64();
			this.string_0 = this.getSteamID32();
			this.StartStreamReading();
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000036CB File Offset: 0x000018CB
		public string AccountMark
		{
			get
			{
				return "/Resources/img/account_mark.png";
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00004093 File Offset: 0x00002293
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0002A20C File Offset: 0x0002840C
		public DateTime LastDrop
		{
			get
			{
				return this._lastDrop;
			}
			set
			{
				this._lastDrop = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000409B File Offset: 0x0000229B
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0002A220 File Offset: 0x00028420
		public int Mark
		{
			get
			{
				return this._mark;
			}
			set
			{
				this._mark = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000040A3 File Offset: 0x000022A3
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0002A234 File Offset: 0x00028434
		public int Prime
		{
			get
			{
				return this._prime;
			}
			set
			{
				this._prime = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000040AB File Offset: 0x000022AB
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0002A248 File Offset: 0x00028448
		public int Rank5x5
		{
			get
			{
				return this._rank5x5;
			}
			set
			{
				this._rank5x5 = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000040B3 File Offset: 0x000022B3
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0002A25C File Offset: 0x0002845C
		public int Rank2x2
		{
			get
			{
				return this._rank2x2;
			}
			set
			{
				this._rank2x2 = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000040BB File Offset: 0x000022BB
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0002A270 File Offset: 0x00028470
		public int Wins5x5
		{
			get
			{
				return this._wins5x5;
			}
			set
			{
				this._wins5x5 = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000040C3 File Offset: 0x000022C3
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0002A284 File Offset: 0x00028484
		public int Wins2x2
		{
			get
			{
				return this._wins2x2;
			}
			set
			{
				this._wins2x2 = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000040CB File Offset: 0x000022CB
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0002A298 File Offset: 0x00028498
		public int Lvl
		{
			get
			{
				return this._lvl;
			}
			set
			{
				this._lvl = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000040D3 File Offset: 0x000022D3
		public AccountShowInTask PlayerShowInTask
		{
			get
			{
				return this._playerShowInTask;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000040DB File Offset: 0x000022DB
		public AccountShow PlayerShow
		{
			get
			{
				return this._playerShow;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005B0 RID: 1456 RVA: 0x0002A2AC File Offset: 0x000284AC
		// (remove) Token: 0x060005B1 RID: 1457 RVA: 0x0002A2EC File Offset: 0x000284EC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0002A32C File Offset: 0x0002852C
		public bool HaveGuard
		{
			get
			{
				return File.Exists("maFiles\\" + this.string_1 + ".maFile") || File.Exists("maFiles\\" + this.Login + ".maFile");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000040E3 File Offset: 0x000022E3
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0002A374 File Offset: 0x00028574
		public bool HideAccountLogin
		{
			get
			{
				return this._hideAccountLogin;
			}
			set
			{
				if (value == this._hideAccountLogin)
				{
					return;
				}
				this._hideAccountLogin = value;
				if (value)
				{
					PlayerWindowInteraction.SetWindowText(base.HWND, string.Empty);
					return;
				}
				PlayerWindowInteraction.SetWindowText(base.HWND, this.Login);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000040EB File Offset: 0x000022EB
		public string InviteCode
		{
			get
			{
				return this._getInviteCode();
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002A3BC File Offset: 0x000285BC
		private string _getInviteCode()
		{
			if (!string.IsNullOrEmpty(this.string_1) && !string.IsNullOrWhiteSpace(this.string_1))
			{
				ulong num = ulong.Parse(this.string_1);
				ulong num2 = this.GetAccountId(num) | 4851299628236144640UL;
				byte[] array = MD5.Create().ComputeHash(BitConverter.GetBytes(num2));
				num2 = (ulong)BitConverter.ToUInt32(new byte[]
				{
					array[0],
					array[1],
					array[2],
					array[3]
				}, 0);
				ulong num3 = 0UL;
				for (int i = 0; i < 8; i++)
				{
					ulong num4 = this.Get(4 * i, 15U, num);
					ulong num5 = (num2 >> i) & 1UL;
					ulong num6 = (num3 << 4) | num4;
					num3 = (num3 >> 28 << 32) | num6;
					num3 = (num3 >> 31 << 32) | ((num6 << 1) | num5);
				}
				byte[] bytes = BitConverter.GetBytes(num3);
				Array.Reverse(bytes);
				num3 = BitConverter.ToUInt64(bytes, 0);
				string text = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
				string text2 = "";
				for (int j = 0; j < 13; j++)
				{
					if (j == 4 || j == 9)
					{
						text2 += "-";
					}
					text2 += text[int.Parse((ulong.Parse(num3.ToString()) & 31UL).ToString())].ToString();
					num3 >>= 5;
				}
				return text2.Substring(5);
			}
			return "";
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000040F3 File Offset: 0x000022F3
		private ulong Get(int bitOffset, uint valueMask, ulong id64)
		{
			return (id64 >> bitOffset) & (ulong)valueMask;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000040FE File Offset: 0x000022FE
		private ulong GetAccountId(ulong id64)
		{
			return this.Get(0, uint.MaxValue, id64);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002A550 File Offset: 0x00028750
		private string getSteamID32()
		{
			if (!(this.string_1 != string.Empty))
			{
				return string.Empty;
			}
			long num;
			if (!long.TryParse(this.string_1, out num))
			{
				return string.Empty;
			}
			return (num - 76561197960265728L).ToString();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public string getSteamID64()
		{
			string text;
			if (this.IsAccountLoggedIn(this.Login, out text))
			{
				return this.GetSteamId64(text, this.Login).ToString();
			}
			return string.Empty;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002A5D8 File Offset: 0x000287D8
		private string GetSteamId64(string content, string login)
		{
			string[] array = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			string text = "\t\t\"AccountName\"\t\t\"" + login + "\"";
			int num = Array.IndexOf<string>(array, text) - 2;
			return Regex.Match(array[num], "\"([^\"]+)").Groups[1].Value;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002A648 File Offset: 0x00028848
		private bool IsAccountLoggedIn(string login, out string content)
		{
			if (!string.IsNullOrEmpty(Settings.GetInstance.SteamPath) && File.Exists(Settings.GetInstance.SteamPath + "\\config\\loginusers.vdf"))
			{
				FileStream fileStream = new FileStream(Settings.GetInstance.SteamPath + "\\config\\loginusers.vdf", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				StreamReader streamReader = new StreamReader(fileStream);
				content = streamReader.ReadToEnd();
				fileStream.Close();
				streamReader.Close();
				return Regex.Match(content, "\t\t\"AccountName\"\t\t\"" + login + "\"").Value.Contains(login + "\"");
			}
			content = "";
			return false;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0002A6F4 File Offset: 0x000288F4
		public bool HasMaFile
		{
			get
			{
				foreach (string text in Directory.GetFiles("maFiles"))
				{
					if (text.EndsWith(".maFile"))
					{
						try
						{
							string text2 = string.Empty;
							using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
							{
								using (StreamReader streamReader = new StreamReader(fileStream))
								{
									text2 = streamReader.ReadToEnd();
								}
							}
							if ((string)JObject.Parse(text2)["account_name"] == this.Login)
							{
								return true;
							}
						}
						catch
						{
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002A7D8 File Offset: 0x000289D8
		private void GetDataFromMaFile()
		{
			foreach (string text in Directory.GetFiles("maFiles"))
			{
				if (text.EndsWith(".maFile"))
				{
					try
					{
						string text2 = string.Empty;
						using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
						{
							using (StreamReader streamReader = new StreamReader(fileStream))
							{
								text2 = streamReader.ReadToEnd();
							}
						}
						JObject jobject = JObject.Parse(text2);
						if ((string)jobject["account_name"] == this.Login)
						{
							this.steamID = jobject["Session"]["SteamID"].ToString();
							this.steamDeviceId = jobject["device_id"].ToString();
							this.steamIdentitySecret = jobject["identity_secret"].ToString();
							this.steamSessionID = jobject["Session"]["SessionID"].ToString();
							this.steamOAuthToken = jobject["Session"]["OAuthToken"].ToString();
							this.steamLogin = jobject["Session"]["SteamLogin"].ToString();
							this.steamLoginSecure = jobject["Session"]["SteamLoginSecure"].ToString();
							this.steamSharedSecret = jobject["shared_secret"].ToString();
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002A9CC File Offset: 0x00028BCC
		public void UpdateMaFile()
		{
			foreach (string text in Directory.GetFiles("maFiles"))
			{
				if (text.EndsWith(".maFile"))
				{
					try
					{
						string text2;
						using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
						{
							using (StreamReader streamReader = new StreamReader(fileStream))
							{
								text2 = streamReader.ReadToEnd();
							}
						}
						JObject jobject = JObject.Parse(text2);
						if ((string)jobject["account_name"] == this.Login)
						{
							jobject["Session"]["SessionID"] = this.steamSessionID;
							jobject["Session"]["SteamLogin"] = this.steamLogin;
							jobject["Session"]["SteamLoginSecure"] = this.steamLoginSecure;
							jobject["Session"]["OAuthToken"] = this.steamOAuthToken;
						}
						File.WriteAllText(text, jobject.ToString());
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0002AB3C File Offset: 0x00028D3C
		private void MoveMe()
		{
			Sector mySector = this.MySector;
			if (mySector == null)
			{
				return;
			}
			mySector.AlignWindow();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0002AB5C File Offset: 0x00028D5C
		private void StartStreamReading()
		{
			this.SR = new PlayerStreamReader(this);
			this.pipeName = this.SR.pipeName;
			this.threadSR = new Thread(new ThreadStart(this.SR.ReadingProcess));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		public async Task OnMessage(string message)
		{
			if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
			{
				Console.WriteLine("[" + this.Login + "] launcher: " + message);
				try
				{
					if (message == "loaded")
					{
						if (Program.GetInstance.LoginWindow != null)
						{
							this.WindowLoaded = true;
							this.MoveMe();
							this._playerShow.UpdateStat();
							if (this.IsCasefarm)
							{
								Thread.Sleep(5000);
								this._joinIdleServer();
							}
						}
					}
					else if (!message.Contains("vbp_ssfn$ "))
					{
						if (!message.Contains("lobby_st"))
						{
							if (message.Contains("steam "))
							{
								this.SteamProcess = Process.GetProcessesByName("steam_" + this.string_0).FirstOrDefault<Process>();
							}
							else if (message == "in_lobby")
							{
								this.PlayerState = PlayerState.IN_LOBBY;
								Task.Run(delegate
								{
									Thread.Sleep(5000);
									this.CheckAndCloseError();
								});
							}
							else if (!(message == "in_game"))
							{
								if (!(message == "in_loading"))
								{
									if (message.Contains("set_matchid "))
									{
										this.match_id = message.Split(new char[] { ' ' })[1];
										goto IL_6E0;
									}
									if (!message.Contains("update "))
									{
										goto IL_6E0;
									}
									if (this.IsPlayerLeader)
									{
										try
										{
											this.LobbyRanks[message.Split(new char[] { ' ' })[1]] = Convert.ToInt32(message.Split(new char[] { ' ' })[2]);
										}
										catch (Exception ex)
										{
											Console.WriteLine(ex);
										}
										try
										{
											Player player = StaticData.GetInstance.Players.FirstOrDefault((Player pl) => pl.string_1 == message.Split(new char[] { ' ' })[1]);
											if (player != null && player.Login != this.Login)
											{
												if (this.CompetitiveMode == 2)
												{
													player.Rank2x2 = Convert.ToInt32(message.Split(new char[] { ' ' })[2]);
													player.Wins2x2 = Convert.ToInt32(message.Split(new char[] { ' ' })[3]);
												}
												else
												{
													player.Rank5x5 = Convert.ToInt32(message.Split(new char[] { ' ' })[2]);
													player.Wins5x5 = Convert.ToInt32(message.Split(new char[] { ' ' })[3]);
												}
												player.Lvl = Convert.ToInt32(message.Split(new char[] { ' ' })[4]);
												player.Prime = Convert.ToInt32(message.Split(new char[] { ' ' })[5]);
												player.SaveInfo();
												int num = Convert.ToInt32(message.Split(new char[] { ' ' })[6].Replace("32768", ""));
												if (player.CurrentXP != -1)
												{
													if (num < player.CurrentXP)
													{
														player.GainedXP = 5000 - player.CurrentXP + num;
													}
													else
													{
														player.GainedXP = num - player.CurrentXP;
													}
												}
												player.CurrentXP = num;
											}
										}
										catch (Exception ex2)
										{
											Console.WriteLine(ex2);
										}
									}
									if (!(message.Split(new char[] { ' ' })[1] == this.string_1))
									{
										goto IL_6E0;
									}
									try
									{
										if (this.CompetitiveMode == 2)
										{
											this.Rank2x2 = Convert.ToInt32(message.Split(new char[] { ' ' })[2]);
											this.Wins2x2 = Convert.ToInt32(message.Split(new char[] { ' ' })[3]);
										}
										else
										{
											this.Rank5x5 = Convert.ToInt32(message.Split(new char[] { ' ' })[2]);
											this.Wins5x5 = Convert.ToInt32(message.Split(new char[] { ' ' })[3]);
										}
										this.Lvl = Convert.ToInt32(message.Split(new char[] { ' ' })[4]);
										this.Prime = Convert.ToInt32(message.Split(new char[] { ' ' })[5]);
										this.SaveInfo();
										int num2 = Convert.ToInt32(message.Split(new char[] { ' ' })[6].Replace("32768", ""));
										if (this.CurrentXP != -1)
										{
											if (num2 >= this.CurrentXP)
											{
												this.GainedXP = num2 - this.CurrentXP;
											}
											else
											{
												this.GainedXP = 5000 - this.CurrentXP + num2;
											}
										}
										this.CurrentXP = num2;
										goto IL_6E0;
									}
									catch (Exception ex3)
									{
										Console.WriteLine(ex3);
										goto IL_6E0;
									}
								}
								this.PlayerState = PlayerState.IN_LOADING;
							}
							else
							{
								this.PlayerState = PlayerState.IN_GAME;
							}
						}
						else
						{
							int.TryParse(message.Split(new char[] { ' ' })[1], out this.LobbyState);
						}
					}
					else
					{
						if (!Directory.Exists("data"))
						{
							Directory.CreateDirectory("data");
						}
						if (!Directory.Exists("data\\ssfn"))
						{
							Directory.CreateDirectory("data\\ssfn");
						}
						string text = message.Split(new char[] { ' ' })[1];
						string text2 = message.Replace("vbp_ssfn$ " + text + " ", "");
						if (!File.Exists("data\\ssfn\\" + this.Login + "\\" + text))
						{
							File.Create("data\\ssfn\\" + this.Login + "\\" + text).Close();
							using (FileStream fileStream = new FileStream("data\\ssfn\\" + this.Login + "\\" + text, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
							{
								using (StreamWriter streamWriter = new StreamWriter(fileStream))
								{
									streamWriter.Write(text2);
								}
							}
						}
					}
					IL_6E0:;
				}
				catch (Exception ex4)
				{
					Console.WriteLine(ex4);
					throw ex4;
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0002ABF0 File Offset: 0x00028DF0
		public string SteamGuardCode
		{
			get
			{
				string text = string.Empty;
				try
				{
					string text2 = string.Empty;
					if (File.Exists("maFiles\\" + this.string_1 + ".maFile"))
					{
						text2 = "maFiles\\" + this.string_1 + ".maFile";
					}
					else
					{
						if (!File.Exists("maFiles\\" + this.Login + ".maFile"))
						{
							return null;
						}
						text2 = "maFiles\\" + this.Login + ".maFile";
					}
					JObject jobject;
					using (StreamReader streamReader = File.OpenText(text2))
					{
						using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
						{
							jobject = (JObject)JToken.ReadFrom(jsonTextReader);
						}
					}
					text = (string)jobject["shared_secret"];
					goto IL_D6;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					goto IL_D6;
				}
				string text3;
				return text3;
				IL_D6:
				if (!string.IsNullOrEmpty(text))
				{
					return new SteamGuardAccount
					{
						SharedSecret = text
					}.GenerateSteamGuardCode();
				}
				CreateError.NewError("Failed to parse .maFile", false);
				return null;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002AD24 File Offset: 0x00028F24
		private void _startLauncher(int width, int height, bool bypass)
		{
			this.LauncherProcess = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Verb = "runas";
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.FileName = "lib\\client\\VertigoBoostClient.exe";
			processStartInfo.Arguments = string.Format("{0} {1} {2} {3} {4} \"{5}\" \"{6}\" 27020 {7} {8} {9} {10}", new object[]
			{
				this.pipeName,
				this.Login,
				this.Password,
				this.string_1,
				this.string_0,
				Settings.GetInstance.SteamPath.Replace("\\", "\\\\"),
				Settings.GetInstance.CsGoPath.Replace("\\", "\\\\"),
				0,
				0,
				width,
				height
			});
			if (!Settings.GetInstance.MouseEnabled)
			{
				ProcessStartInfo processStartInfo2 = processStartInfo;
				processStartInfo2.Arguments += " \"0\"";
			}
			else
			{
				ProcessStartInfo processStartInfo3 = processStartInfo;
				processStartInfo3.Arguments += " \"1\"";
			}
			ProcessStartInfo processStartInfo4 = processStartInfo;
			processStartInfo4.Arguments += " \"null\"";
			if (!bypass)
			{
				ProcessStartInfo processStartInfo5 = processStartInfo;
				processStartInfo5.Arguments += " \"no-bypass\"";
			}
			else
			{
				ProcessStartInfo processStartInfo6 = processStartInfo;
				processStartInfo6.Arguments += " \"bypass\"";
			}
			this.LauncherProcess.StartInfo = processStartInfo;
			this.LauncherProcess.Start();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0002AEB8 File Offset: 0x000290B8
		private void _joinIdleServer()
		{
			if (string.IsNullOrEmpty(Settings.GetInstance.server_id) || string.IsNullOrWhiteSpace(Settings.GetInstance.server_id))
			{
				CreateError.NewError("ServerIP not specified", false);
				return;
			}
			if (Process.GetProcessesByName("csgo").FirstOrDefault((Process process) => process.MainWindowTitle == this.Login) != null)
			{
				base.ExecConsoleText("connect " + Settings.GetInstance.server_id);
				base.ExecConsoleText("clear");
				this.CaseFarmJoinedServer = true;
				return;
			}
			CreateError.NewError("Could not find csgo process", false);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0002AF4C File Offset: 0x0002914C
		public string GetInternalID()
		{
			if (string.IsNullOrEmpty(this.string_1))
			{
				return string.Empty;
			}
			ulong num;
			if (ulong.TryParse(this.string_1, out num))
			{
				ulong num2 = num - 76561197960265728UL;
				uint num3 = (uint)(num2 & 1UL);
				uint num4 = (uint)(num2 >> 1);
				return string.Format("STEAM_{0}:{1}", num3, num4);
			}
			return string.Empty;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0002AFB8 File Offset: 0x000291B8
		public void StartStreamingPosition(CancellationToken token)
		{
			new Task(delegate
			{
				using (token.Register(new Action(Thread.CurrentThread.Abort)))
				{
					for (;;)
					{
						Thread.Sleep(5000);
						this.TakeKnife();
						this.ExecConsoleText("-left");
						this.ExecConsoleText("+attack2");
					}
				}
			}, token).Start();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002AFFC File Offset: 0x000291FC
		public void StartCollectInfo()
		{
			if (this.Started && this.WindowLoaded)
			{
				this.collectInfoThread = new Thread(delegate
				{
					if (string.IsNullOrEmpty(ExternalInfoCollector.TargetAccountID))
					{
						return;
					}
					this._process = new CsGoProcess(base.HWND);
					PatternScanner.FindPattern(this._process.handle, "8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01", this._process.processModules["engine"], false, 2, true, 0);
					PatternScanner.FindPattern(this._process.handle, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", this._process.processModules["engine"], true, 1, true, 0);
					for (;;)
					{
						Thread.Sleep(1);
						IntPtr handle = this._process.handle;
						IntPtr baseAddress = this._process.processModules["client"].BaseAddress;
						if (Player.<>o__120.<>p__2 == null)
						{
							Player.<>o__120.<>p__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
						}
						Func<CallSite, object, int> target = Player.<>o__120.<>p__2.Target;
						CallSite <>p__ = Player.<>o__120.<>p__2;
						if (Player.<>o__120.<>p__1 == null)
						{
							Player.<>o__120.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwLocalPlayer", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target2 = Player.<>o__120.<>p__1.Target;
						CallSite <>p__2 = Player.<>o__120.<>p__1;
						if (Player.<>o__120.<>p__0 == null)
						{
							Player.<>o__120.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						IntPtr intPtr = Memory.ReadFromProcess<IntPtr>(handle, baseAddress + target(<>p__, target2(<>p__2, Player.<>o__120.<>p__0.Target(Player.<>o__120.<>p__0, Offsets.hazedumper))));
						for (int i = 1; i < 64; i++)
						{
							IntPtr handle2 = this._process.handle;
							IntPtr baseAddress2 = this._process.processModules["client"].BaseAddress;
							if (Player.<>o__120.<>p__5 == null)
							{
								Player.<>o__120.<>p__5 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
							}
							Func<CallSite, object, int> target3 = Player.<>o__120.<>p__5.Target;
							CallSite <>p__3 = Player.<>o__120.<>p__5;
							if (Player.<>o__120.<>p__4 == null)
							{
								Player.<>o__120.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwEntityList", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target4 = Player.<>o__120.<>p__4.Target;
							CallSite <>p__4 = Player.<>o__120.<>p__4;
							if (Player.<>o__120.<>p__3 == null)
							{
								Player.<>o__120.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							IntPtr intPtr2 = Memory.ReadFromProcess<IntPtr>(handle2, baseAddress2 + target3(<>p__3, target4(<>p__4, Player.<>o__120.<>p__3.Target(Player.<>o__120.<>p__3, Offsets.hazedumper))) + (i - 1) * 16);
							if (!(intPtr2 == IntPtr.Zero))
							{
								IntPtr handle3 = this._process.handle;
								IntPtr intPtr3 = intPtr;
								if (Player.<>o__120.<>p__8 == null)
								{
									Player.<>o__120.<>p__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
								}
								Func<CallSite, object, int> target5 = Player.<>o__120.<>p__8.Target;
								CallSite <>p__5 = Player.<>o__120.<>p__8;
								if (Player.<>o__120.<>p__7 == null)
								{
									Player.<>o__120.<>p__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iTeamNum", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, object> target6 = Player.<>o__120.<>p__7.Target;
								CallSite <>p__6 = Player.<>o__120.<>p__7;
								if (Player.<>o__120.<>p__6 == null)
								{
									Player.<>o__120.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								int num = Memory.ReadFromProcess<int>(handle3, intPtr3 + target5(<>p__5, target6(<>p__6, Player.<>o__120.<>p__6.Target(Player.<>o__120.<>p__6, Offsets.hazedumper))));
								IntPtr handle4 = this._process.handle;
								IntPtr intPtr4 = intPtr2;
								if (Player.<>o__120.<>p__11 == null)
								{
									Player.<>o__120.<>p__11 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
								}
								Func<CallSite, object, int> target7 = Player.<>o__120.<>p__11.Target;
								CallSite <>p__7 = Player.<>o__120.<>p__11;
								if (Player.<>o__120.<>p__10 == null)
								{
									Player.<>o__120.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_iTeamNum", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, object> target8 = Player.<>o__120.<>p__10.Target;
								CallSite <>p__8 = Player.<>o__120.<>p__10;
								if (Player.<>o__120.<>p__9 == null)
								{
									Player.<>o__120.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								int num2 = Memory.ReadFromProcess<int>(handle4, intPtr4 + target7(<>p__7, target8(<>p__8, Player.<>o__120.<>p__9.Target(Player.<>o__120.<>p__9, Offsets.hazedumper))));
								if (num == num2)
								{
									IntPtr handle5 = this._process.handle;
									IntPtr baseAddress3 = this._process.processModules["engine"].BaseAddress;
									if (Player.<>o__120.<>p__14 == null)
									{
										Player.<>o__120.<>p__14 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target9 = Player.<>o__120.<>p__14.Target;
									CallSite <>p__9 = Player.<>o__120.<>p__14;
									if (Player.<>o__120.<>p__13 == null)
									{
										Player.<>o__120.<>p__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target10 = Player.<>o__120.<>p__13.Target;
									CallSite <>p__10 = Player.<>o__120.<>p__13;
									if (Player.<>o__120.<>p__12 == null)
									{
										Player.<>o__120.<>p__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr5 = Memory.ReadFromProcess<IntPtr>(handle5, baseAddress3 + target9(<>p__9, target10(<>p__10, Player.<>o__120.<>p__12.Target(Player.<>o__120.<>p__12, Offsets.hazedumper))));
									IntPtr handle6 = this._process.handle;
									IntPtr intPtr6 = intPtr5;
									if (Player.<>o__120.<>p__17 == null)
									{
										Player.<>o__120.<>p__17 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target11 = Player.<>o__120.<>p__17.Target;
									CallSite <>p__11 = Player.<>o__120.<>p__17;
									if (Player.<>o__120.<>p__16 == null)
									{
										Player.<>o__120.<>p__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_PlayerInfo", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target12 = Player.<>o__120.<>p__16.Target;
									CallSite <>p__12 = Player.<>o__120.<>p__16;
									if (Player.<>o__120.<>p__15 == null)
									{
										Player.<>o__120.<>p__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr7 = Memory.ReadFromProcess<IntPtr>(handle6, intPtr6 + target11(<>p__11, target12(<>p__12, Player.<>o__120.<>p__15.Target(Player.<>o__120.<>p__15, Offsets.hazedumper))));
									IntPtr intPtr8 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr7 + 64);
									IntPtr intPtr9 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr8 + 12);
									IntPtr intPtr10 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr9 + 40 + (i - 1) * 52);
									if (new string(Memory.ReadFromProcess<player_info_t>(this._process.handle, intPtr10).m_szSteamID).Contains(ExternalInfoCollector.TargetAccountID.Split(new char[] { ':' }).Last<string>()))
									{
										IntPtr handle7 = this._process.handle;
										IntPtr intPtr11 = intPtr2;
										if (Player.<>o__120.<>p__20 == null)
										{
											Player.<>o__120.<>p__20 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
										}
										Func<CallSite, object, int> target13 = Player.<>o__120.<>p__20.Target;
										CallSite <>p__13 = Player.<>o__120.<>p__20;
										if (Player.<>o__120.<>p__19 == null)
										{
											Player.<>o__120.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target14 = Player.<>o__120.<>p__19.Target;
										CallSite <>p__14 = Player.<>o__120.<>p__19;
										if (Player.<>o__120.<>p__18 == null)
										{
											Player.<>o__120.<>p__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										ExternalInfoCollector.TargetAccountPosition = Memory.ReadFromProcess<Vector3>(handle7, intPtr11 + target13(<>p__13, target14(<>p__14, Player.<>o__120.<>p__18.Target(Player.<>o__120.<>p__18, Offsets.hazedumper))));
										IntPtr handle8 = this._process.handle;
										IntPtr intPtr12 = intPtr2;
										if (Player.<>o__120.<>p__23 == null)
										{
											Player.<>o__120.<>p__23 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
										}
										Func<CallSite, object, int> target15 = Player.<>o__120.<>p__23.Target;
										CallSite <>p__15 = Player.<>o__120.<>p__23;
										if (Player.<>o__120.<>p__22 == null)
										{
											Player.<>o__120.<>p__22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_angEyeAnglesX", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target16 = Player.<>o__120.<>p__22.Target;
										CallSite <>p__16 = Player.<>o__120.<>p__22;
										if (Player.<>o__120.<>p__21 == null)
										{
											Player.<>o__120.<>p__21 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										ExternalInfoCollector.TargetAccountEyes = Memory.ReadFromProcess<Vector3>(handle8, intPtr12 + target15(<>p__15, target16(<>p__16, Player.<>o__120.<>p__21.Target(Player.<>o__120.<>p__21, Offsets.hazedumper)))).Y;
									}
								}
							}
						}
					}
				});
				this.collectInfoThread.Priority = ThreadPriority.Highest;
				this.collectInfoThread.Start();
				return;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002B048 File Offset: 0x00029248
		public void StopCollectInfo()
		{
			Thread thread = this.collectInfoThread;
			if (thread == null)
			{
				return;
			}
			thread.Abort();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002B068 File Offset: 0x00029268
		public void CallvoteMapChange(string map)
		{
			base.ExecConsoleText("callvote ChangeLevel " + map);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002B088 File Offset: 0x00029288
		public void ConfirmCallvote()
		{
			PlayerWindowInteraction.sendKey(base.HWND, 112, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, 112, false, false);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002B0B4 File Offset: 0x000292B4
		public void ResorePlayerWalk()
		{
			PlayerWindowInteraction.sendKey(base.HWND, 87, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, 87, false, false);
			Thread.Sleep(50);
			PlayerWindowInteraction.sendKey(base.HWND, 83, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, 83, false, false);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002B104 File Offset: 0x00029304
		public void KillAllEnemiesByKnife()
		{
			PlayerWindowInteraction.sendKey(base.HWND, 69, false, true);
			Thread.Sleep(300);
			PlayerWindowInteraction.sendKey(base.HWND, 69, false, false);
			PlayerWindowInteraction.sendKey(base.HWND, 51, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, 51, false, false);
			Thread.Sleep(200);
			for (int i = 0; i < 50; i++)
			{
				Player.RightMoveOnPoint(base.HWND, new System.Drawing.Point(100, 100));
				this.RightClickOnPoint(base.HWND, new System.Drawing.Point(100, 100));
				Thread.Sleep(200);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002B1A8 File Offset: 0x000293A8
		public void StartDustDMDieProcess(Player mainPlayer, CancellationToken token)
		{
			new Task(delegate
			{
				using (token.Register(new Action(Thread.CurrentThread.Abort)))
				{
					try
					{
						this._process = new CsGoProcess(this.HWND);
						PatternScanner.FindPattern(this._process.handle, "8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01", this._process.processModules["engine"], false, 2, true, 0);
						PatternScanner.FindPattern(this._process.handle, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", this._process.processModules["engine"], true, 1, true, 0);
						for (;;)
						{
							Thread.Sleep(5000);
							if (mainPlayer.currentMapPhase == MapPhase.Live && mainPlayer.currentRoundPhase == RoundPhase.Live && mainPlayer.currentPlayerActivity == PlayerActivity.Playing && this.currentPlayerActivity == PlayerActivity.Playing && this.currentPlayerTeam != PlayerTeam.Undefined && !string.IsNullOrEmpty(mainPlayer.GetInternalID()))
							{
								Console.WriteLine("3");
								IntPtr handle = this._process.handle;
								IntPtr baseAddress = this._process.processModules["client"].BaseAddress;
								if (Player.<>o__127.<>p__2 == null)
								{
									Player.<>o__127.<>p__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
								}
								Func<CallSite, object, int> target = Player.<>o__127.<>p__2.Target;
								CallSite <>p__ = Player.<>o__127.<>p__2;
								if (Player.<>o__127.<>p__1 == null)
								{
									Player.<>o__127.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwLocalPlayer", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, object> target2 = Player.<>o__127.<>p__1.Target;
								CallSite <>p__2 = Player.<>o__127.<>p__1;
								if (Player.<>o__127.<>p__0 == null)
								{
									Player.<>o__127.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Memory.ReadFromProcess<IntPtr>(handle, baseAddress + target(<>p__, target2(<>p__2, Player.<>o__127.<>p__0.Target(Player.<>o__127.<>p__0, Offsets.hazedumper))));
								Console.WriteLine("----------");
								for (int i = 0; i < 64; i++)
								{
									IntPtr handle2 = this._process.handle;
									IntPtr baseAddress2 = this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__5 == null)
									{
										Player.<>o__127.<>p__5 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target3 = Player.<>o__127.<>p__5.Target;
									CallSite <>p__3 = Player.<>o__127.<>p__5;
									if (Player.<>o__127.<>p__4 == null)
									{
										Player.<>o__127.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwEntityList", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target4 = Player.<>o__127.<>p__4.Target;
									CallSite <>p__4 = Player.<>o__127.<>p__4;
									if (Player.<>o__127.<>p__3 == null)
									{
										Player.<>o__127.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr = Memory.ReadFromProcess<IntPtr>(handle2, baseAddress2 + target3(<>p__3, target4(<>p__4, Player.<>o__127.<>p__3.Target(Player.<>o__127.<>p__3, Offsets.hazedumper))) + i * 16);
									IntPtr handle3 = this._process.handle;
									IntPtr baseAddress3 = this._process.processModules["engine"].BaseAddress;
									if (Player.<>o__127.<>p__8 == null)
									{
										Player.<>o__127.<>p__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target5 = Player.<>o__127.<>p__8.Target;
									CallSite <>p__5 = Player.<>o__127.<>p__8;
									if (Player.<>o__127.<>p__7 == null)
									{
										Player.<>o__127.<>p__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target6 = Player.<>o__127.<>p__7.Target;
									CallSite <>p__6 = Player.<>o__127.<>p__7;
									if (Player.<>o__127.<>p__6 == null)
									{
										Player.<>o__127.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr2 = Memory.ReadFromProcess<IntPtr>(handle3, baseAddress3 + target5(<>p__5, target6(<>p__6, Player.<>o__127.<>p__6.Target(Player.<>o__127.<>p__6, Offsets.hazedumper))));
									IntPtr handle4 = this._process.handle;
									IntPtr intPtr3 = intPtr2;
									if (Player.<>o__127.<>p__11 == null)
									{
										Player.<>o__127.<>p__11 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target7 = Player.<>o__127.<>p__11.Target;
									CallSite <>p__7 = Player.<>o__127.<>p__11;
									if (Player.<>o__127.<>p__10 == null)
									{
										Player.<>o__127.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_PlayerInfo", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target8 = Player.<>o__127.<>p__10.Target;
									CallSite <>p__8 = Player.<>o__127.<>p__10;
									if (Player.<>o__127.<>p__9 == null)
									{
										Player.<>o__127.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									IntPtr intPtr4 = Memory.ReadFromProcess<IntPtr>(handle4, intPtr3 + target7(<>p__7, target8(<>p__8, Player.<>o__127.<>p__9.Target(Player.<>o__127.<>p__9, Offsets.hazedumper))));
									IntPtr intPtr5 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr4 + 64);
									IntPtr intPtr6 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr5 + 12);
									IntPtr intPtr7 = Memory.ReadFromProcess<IntPtr>(this._process.handle, intPtr6 + 40 + i * 52);
									player_info_t player_info_t = Memory.ReadFromProcess<player_info_t>(this._process.handle, intPtr7);
									if (!(intPtr7 == IntPtr.Zero))
									{
										new string(player_info_t.m_szSteamID);
									}
									if (new string(player_info_t.m_szSteamID).Contains(mainPlayer.GetInternalID().Split(new char[] { ':' }).Last<string>()))
									{
										Player mainPlayer2 = mainPlayer;
										IntPtr handle5 = this._process.handle;
										IntPtr intPtr8 = intPtr;
										if (Player.<>o__127.<>p__14 == null)
										{
											Player.<>o__127.<>p__14 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
										}
										Func<CallSite, object, int> target9 = Player.<>o__127.<>p__14.Target;
										CallSite <>p__9 = Player.<>o__127.<>p__14;
										if (Player.<>o__127.<>p__13 == null)
										{
											Player.<>o__127.<>p__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target10 = Player.<>o__127.<>p__13.Target;
										CallSite <>p__10 = Player.<>o__127.<>p__13;
										if (Player.<>o__127.<>p__12 == null)
										{
											Player.<>o__127.<>p__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										mainPlayer2.Position = Memory.ReadFromProcess<Vector3>(handle5, intPtr8 + target9(<>p__9, target10(<>p__10, Player.<>o__127.<>p__12.Target(Player.<>o__127.<>p__12, Offsets.hazedumper))));
									}
								}
								if (mainPlayer.Position.X != 0f || mainPlayer.Position.Y != 0f)
								{
									CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
									Task task = new Task(delegate
									{
										this.GoAndDie(mainPlayer.Position, cancellationTokenSource.Token);
									}, cancellationTokenSource.Token);
									task.Start();
									cancellationTokenSource.CancelAfter(new TimeSpan(0, 0, 3, 0));
									while (!task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
									{
										if (!string.IsNullOrEmpty(this.currentMap) && this.currentMapPhase == MapPhase.Live && this.currentPlayerActivity == PlayerActivity.Playing && this.currentPlayerTeam != PlayerTeam.Undefined)
										{
											if (this.currentRoundPhase == RoundPhase.Live)
											{
												Thread.Sleep(500);
												continue;
											}
										}
										try
										{
											cancellationTokenSource.Cancel();
										}
										catch (Exception ex)
										{
											Console.WriteLine(ex);
										}
										break;
									}
									IntPtr handle6 = this._process.handle;
									int num = (int)this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__17 == null)
									{
										Player.<>o__127.<>p__17 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target11 = Player.<>o__127.<>p__17.Target;
									CallSite <>p__11 = Player.<>o__127.<>p__17;
									if (Player.<>o__127.<>p__16 == null)
									{
										Player.<>o__127.<>p__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwForceJump", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target12 = Player.<>o__127.<>p__16.Target;
									CallSite <>p__12 = Player.<>o__127.<>p__16;
									if (Player.<>o__127.<>p__15 == null)
									{
										Player.<>o__127.<>p__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Memory.Write<int>(handle6, num + target11(<>p__11, target12(<>p__12, Player.<>o__127.<>p__15.Target(Player.<>o__127.<>p__15, Offsets.hazedumper))), 4);
									IntPtr handle7 = this._process.handle;
									int num2 = (int)this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__20 == null)
									{
										Player.<>o__127.<>p__20 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target13 = Player.<>o__127.<>p__20.Target;
									CallSite <>p__13 = Player.<>o__127.<>p__20;
									if (Player.<>o__127.<>p__19 == null)
									{
										Player.<>o__127.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwForceForward", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target14 = Player.<>o__127.<>p__19.Target;
									CallSite <>p__14 = Player.<>o__127.<>p__19;
									if (Player.<>o__127.<>p__18 == null)
									{
										Player.<>o__127.<>p__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Memory.Write<int>(handle7, num2 + target13(<>p__13, target14(<>p__14, Player.<>o__127.<>p__18.Target(Player.<>o__127.<>p__18, Offsets.hazedumper))), 4);
									IntPtr handle8 = this._process.handle;
									int num3 = (int)this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__23 == null)
									{
										Player.<>o__127.<>p__23 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target15 = Player.<>o__127.<>p__23.Target;
									CallSite <>p__15 = Player.<>o__127.<>p__23;
									if (Player.<>o__127.<>p__22 == null)
									{
										Player.<>o__127.<>p__22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwForceBackward", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target16 = Player.<>o__127.<>p__22.Target;
									CallSite <>p__16 = Player.<>o__127.<>p__22;
									if (Player.<>o__127.<>p__21 == null)
									{
										Player.<>o__127.<>p__21 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Memory.Write<int>(handle8, num3 + target15(<>p__15, target16(<>p__16, Player.<>o__127.<>p__21.Target(Player.<>o__127.<>p__21, Offsets.hazedumper))), 4);
									IntPtr handle9 = this._process.handle;
									int num4 = (int)this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__26 == null)
									{
										Player.<>o__127.<>p__26 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target17 = Player.<>o__127.<>p__26.Target;
									CallSite <>p__17 = Player.<>o__127.<>p__26;
									if (Player.<>o__127.<>p__25 == null)
									{
										Player.<>o__127.<>p__25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwForceLeft", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target18 = Player.<>o__127.<>p__25.Target;
									CallSite <>p__18 = Player.<>o__127.<>p__25;
									if (Player.<>o__127.<>p__24 == null)
									{
										Player.<>o__127.<>p__24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Memory.Write<int>(handle9, num4 + target17(<>p__17, target18(<>p__18, Player.<>o__127.<>p__24.Target(Player.<>o__127.<>p__24, Offsets.hazedumper))), 4);
									IntPtr handle10 = this._process.handle;
									int num5 = (int)this._process.processModules["client"].BaseAddress;
									if (Player.<>o__127.<>p__29 == null)
									{
										Player.<>o__127.<>p__29 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target19 = Player.<>o__127.<>p__29.Target;
									CallSite <>p__19 = Player.<>o__127.<>p__29;
									if (Player.<>o__127.<>p__28 == null)
									{
										Player.<>o__127.<>p__28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwForceRight", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target20 = Player.<>o__127.<>p__28.Target;
									CallSite <>p__20 = Player.<>o__127.<>p__28;
									if (Player.<>o__127.<>p__27 == null)
									{
										Player.<>o__127.<>p__27 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Memory.Write<int>(handle10, num5 + target19(<>p__19, target20(<>p__20, Player.<>o__127.<>p__27.Target(Player.<>o__127.<>p__27, Offsets.hazedumper))), 4);
									GC.Collect();
									Thread.Sleep(3000);
								}
							}
						}
					}
					catch (Exception ex2)
					{
						Console.WriteLine(ex2);
					}
				}
			}, token).Start();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private bool GoAndDie(Vector3 targetPosition, CancellationToken token)
		{
			bool flag;
			using (token.Register(new Action(Thread.CurrentThread.Abort)))
			{
				try
				{
					if (this._process == null)
					{
						flag = false;
					}
					else
					{
						this.TakeKnife();
						int num = PatternScanner.FindPattern(this._process.handle, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", this._process.processModules["engine"], true, 1, true, 0);
						IntPtr intPtr = Memory.ReadFromProcess<IntPtr>(this._process.handle, this._process.processModules["engine"].BaseAddress + num);
						IntPtr handle = this._process.handle;
						IntPtr baseAddress = this._process.processModules["client"].BaseAddress;
						if (Player.<>o__128.<>p__2 == null)
						{
							Player.<>o__128.<>p__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
						}
						Func<CallSite, object, int> target = Player.<>o__128.<>p__2.Target;
						CallSite <>p__ = Player.<>o__128.<>p__2;
						if (Player.<>o__128.<>p__1 == null)
						{
							Player.<>o__128.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwLocalPlayer", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target2 = Player.<>o__128.<>p__1.Target;
						CallSite <>p__2 = Player.<>o__128.<>p__1;
						if (Player.<>o__128.<>p__0 == null)
						{
							Player.<>o__128.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						IntPtr intPtr2 = Memory.ReadFromProcess<IntPtr>(handle, baseAddress + target(<>p__, target2(<>p__2, Player.<>o__128.<>p__0.Target(Player.<>o__128.<>p__0, Offsets.hazedumper))));
						IntPtr handle2 = this._process.handle;
						IntPtr intPtr3 = intPtr2;
						if (Player.<>o__128.<>p__5 == null)
						{
							Player.<>o__128.<>p__5 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
						}
						Func<CallSite, object, int> target3 = Player.<>o__128.<>p__5.Target;
						CallSite <>p__3 = Player.<>o__128.<>p__5;
						if (Player.<>o__128.<>p__4 == null)
						{
							Player.<>o__128.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target4 = Player.<>o__128.<>p__4.Target;
						CallSite <>p__4 = Player.<>o__128.<>p__4;
						if (Player.<>o__128.<>p__3 == null)
						{
							Player.<>o__128.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Vector3 vector = Memory.ReadFromProcess<Vector3>(handle2, intPtr3 + target3(<>p__3, target4(<>p__4, Player.<>o__128.<>p__3.Target(Player.<>o__128.<>p__3, Offsets.hazedumper))));
						List<Vector3> list;
						try
						{
							string result = string.Empty;
							PlayerWindowInteraction.DustPath(vector.X, vector.Y, vector.Z, targetPosition.X, targetPosition.Y, targetPosition.Z, delegate(string x)
							{
								result = x;
							});
							list = JsonConvert.DeserializeObject<List<Vector3>>(result);
						}
						catch
						{
							return false;
						}
						if (list.Count > 1)
						{
							base.ExecConsoleText("-left");
							base.ExecConsoleText("-right");
							bool flag2 = false;
							foreach (Vector3 vector2 in list)
							{
								for (;;)
								{
									Thread.Sleep(2);
									IntPtr handle3 = this._process.handle;
									IntPtr intPtr4 = intPtr2;
									if (Player.<>o__128.<>p__8 == null)
									{
										Player.<>o__128.<>p__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target5 = Player.<>o__128.<>p__8.Target;
									CallSite <>p__5 = Player.<>o__128.<>p__8;
									if (Player.<>o__128.<>p__7 == null)
									{
										Player.<>o__128.<>p__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "m_vecOrigin", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target6 = Player.<>o__128.<>p__7.Target;
									CallSite <>p__6 = Player.<>o__128.<>p__7;
									if (Player.<>o__128.<>p__6 == null)
									{
										Player.<>o__128.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "netvars", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Vector3 vector3 = Memory.ReadFromProcess<Vector3>(handle3, intPtr4 + target5(<>p__5, target6(<>p__6, Player.<>o__128.<>p__6.Target(Player.<>o__128.<>p__6, Offsets.hazedumper))));
									if (Math.Sqrt(Math.Pow((double)(vector3.X - vector2.X), 2.0) + Math.Pow((double)(vector3.Y - vector2.Y), 2.0)) <= 15.0)
									{
										break;
									}
									Vector3 vector4 = vector2 - vector3;
									vector4.Z = 0f;
									float y = Player.CalcAngle(vector4, vector4.Length()).Y;
									IntPtr handle4 = this._process.handle;
									IntPtr intPtr5 = intPtr;
									if (Player.<>o__128.<>p__11 == null)
									{
										Player.<>o__128.<>p__11 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(int), typeof(Player)));
									}
									Func<CallSite, object, int> target7 = Player.<>o__128.<>p__11.Target;
									CallSite <>p__7 = Player.<>o__128.<>p__11;
									if (Player.<>o__128.<>p__10 == null)
									{
										Player.<>o__128.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dwClientState_ViewAngles", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target8 = Player.<>o__128.<>p__10.Target;
									CallSite <>p__8 = Player.<>o__128.<>p__10;
									if (Player.<>o__128.<>p__9 == null)
									{
										Player.<>o__128.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "signatures", typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									float num2 = y - Memory.ReadFromProcess<Vector3>(handle4, intPtr5 + target7(<>p__7, target8(<>p__8, Player.<>o__128.<>p__9.Target(Player.<>o__128.<>p__9, Offsets.hazedumper)))).Y;
									if (Math.Abs(num2) > 180f)
									{
										num2 = -num2;
									}
									if (num2 >= 3f)
									{
										base.ExecConsoleText("+left");
										base.ExecConsoleText("-left");
									}
									else if (num2 <= -3f)
									{
										base.ExecConsoleText("+right");
										base.ExecConsoleText("-right");
									}
									if (Math.Abs(num2) >= 10f)
									{
										if (flag2)
										{
											flag2 = false;
											base.ExecConsoleText("-forward");
										}
									}
									else if (!flag2)
									{
										flag2 = true;
										base.ExecConsoleText("+forward");
									}
								}
							}
							base.ExecConsoleText("-forward");
							base.ExecConsoleText("+left");
							base.ExecConsoleText("+right");
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002B900 File Offset: 0x00029B00
		public void GoGoDizzy()
		{
			int num = 81;
			int num2 = 69;
			int num3 = 87;
			int num4 = 68;
			bool flag = false;
			foreach (Vector3 vector in new Vector3[]
			{
				new Vector3(-551.727051f, -592.355957f, 8123.605f),
				new Vector3(-671.176453f, -225.801743f, 8256.094f),
				new Vector3(-892.350647f, -239.96875f, 8256.094f),
				new Vector3(-1269.633f, -222.977615f, 8256.094f),
				new Vector3(-1384.70422f, 111.4008f, 8256.094f),
				new Vector3(-1388.0625f, 999.363647f, 8096.09375f)
			})
			{
				for (;;)
				{
					Thread.Sleep(10);
					Vector3 targetAccountPosition = ExternalInfoCollector.TargetAccountPosition;
					targetAccountPosition.Z = 0f;
					Vector3 vector2 = vector;
					vector2.Z = 0f;
					Vector3 vector3 = targetAccountPosition - vector2;
					Vector3 vector4 = vector2 - targetAccountPosition;
					vector4.Z = 0f;
					if (vector3.Length() < 10f)
					{
						break;
					}
					ref Vector2 ptr = Player.CalcAngle(vector4, vector4.Length());
					float targetAccountEyes = ExternalInfoCollector.TargetAccountEyes;
					float num5 = ptr.Y - targetAccountEyes;
					if (Math.Abs(num5) >= 1f)
					{
						if (Math.Abs(num5) > 180f)
						{
							num5 = -num5;
						}
						if (num5 >= 1f)
						{
							if (num5 > 55f && !flag)
							{
								PlayerWindowInteraction.sendKey(base.HWND, num, false, true);
								Thread.Sleep(3);
								PlayerWindowInteraction.sendKey(base.HWND, num, false, false);
							}
							else
							{
								PlayerWindowInteraction.sendKey(base.HWND, num, false, true);
								PlayerWindowInteraction.sendKey(base.HWND, num, false, false);
							}
						}
						else if (num5 <= -1f)
						{
							if (num5 < -55f && !flag)
							{
								PlayerWindowInteraction.sendKey(base.HWND, num2, false, true);
								Thread.Sleep(3);
								PlayerWindowInteraction.sendKey(base.HWND, num2, false, false);
							}
							else
							{
								PlayerWindowInteraction.sendKey(base.HWND, num2, false, true);
								PlayerWindowInteraction.sendKey(base.HWND, num2, false, false);
							}
						}
					}
					if (Math.Abs(num5) >= 10f)
					{
						if (flag)
						{
							flag = false;
							PlayerWindowInteraction.sendKey(base.HWND, num3, false, false);
						}
					}
					else if (!flag)
					{
						flag = true;
						PlayerWindowInteraction.sendKey(base.HWND, num3, false, true);
					}
				}
			}
			PlayerWindowInteraction.sendKey(base.HWND, num3, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, num4, false, true);
			Thread.Sleep(3000);
			PlayerWindowInteraction.sendKey(base.HWND, num3, false, false);
			PlayerWindowInteraction.sendKey(base.HWND, num4, false, false);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0002BBCC File Offset: 0x00029DCC
		private Vector3 method_1(Vector3 src, Vector3 dst)
		{
			Vector3 vector = src - dst;
			return this.VectorAngles(vector);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002BBEC File Offset: 0x00029DEC
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
				if (vector.Y > 90f)
				{
					vector.Y -= 180f;
				}
				else if (vector.Y >= 90f)
				{
					if (vector.Y == 90f)
					{
						vector.Y = 0f;
					}
				}
				else
				{
					vector.Y += 180f;
				}
			}
			vector.Z = 0f;
			return vector;
		}

		// Token: 0x060005D3 RID: 1491
		[DllImport("msvcrt.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern double asin(double radians);

		// Token: 0x060005D4 RID: 1492 RVA: 0x00003DB4 File Offset: 0x00001FB4
		private static float RAD2DEG(float rad)
		{
			return 57.2957764f * rad;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00004109 File Offset: 0x00002309
		private static Vector2 CalcAngle(Vector3 posDelta, float posDeltaLength)
		{
			return new Vector2(Player.RAD2DEG((float)(-(float)Player.asin((double)(posDelta.Z / posDeltaLength)))), Player.RAD2DEG((float)Math.Atan2((double)posDelta.Y, (double)posDelta.X)));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002BCFC File Offset: 0x00029EFC
		public void TakeKnife()
		{
			PlayerWindowInteraction.sendKey(base.HWND, 51, false, true);
			PlayerWindowInteraction.sendKey(base.HWND, 51, false, false);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0002BD28 File Offset: 0x00029F28
		public void BeginClickingTeamSelect(int team)
		{
			if (this.Started && this.WindowLoaded)
			{
				this.teamClickingThread = new Thread(delegate
				{
					int num = -10;
					while (this.currentPlayerTeam == PlayerTeam.Undefined)
					{
						if (team == 1)
						{
							Player.MoveOnPoint(this.HWND, new System.Windows.Point(140.0, (double)(150 + num)));
							Thread.Sleep(50);
							Player.ClickOnPoint(this.HWND, new System.Windows.Point(140.0, (double)(150 + num)));
						}
						else
						{
							Player.MoveOnPoint(this.HWND, new System.Windows.Point(250.0, (double)(150 + num)));
							Thread.Sleep(50);
							Player.ClickOnPoint(this.HWND, new System.Windows.Point(250.0, (double)(150 + num)));
						}
						Thread.Sleep(1000);
						if (num == -10)
						{
							num = 0;
						}
						else
						{
							num = -10;
						}
					}
				});
				this.teamClickingThread.Start();
				return;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002BD80 File Offset: 0x00029F80
		public void StopClickingTeamSelect()
		{
			Thread thread = this.teamClickingThread;
			if (thread != null)
			{
				thread.Abort();
				return;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		public void BeginClickingMapChange()
		{
			if (this.Started && this.WindowLoaded)
			{
				this.mapClickingThread = new Thread(delegate
				{
					int num = -10;
					for (;;)
					{
						Player.MoveOnPoint(base.HWND, new System.Windows.Point(130.0, (double)(100 + num)));
						Thread.Sleep(50);
						Player.ClickOnPoint(base.HWND, new System.Windows.Point(130.0, (double)(100 + num)));
						Thread.Sleep(500);
						if (num == -10)
						{
							num = 0;
						}
						else
						{
							num = -10;
						}
					}
				});
				this.mapClickingThread.Start();
				return;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public void StopClickingMapChange()
		{
			Thread thread = this.mapClickingThread;
			if (thread != null)
			{
				thread.Abort();
				return;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002BE00 File Offset: 0x0002A000
		private void RightClickOnPoint(IntPtr hWnd, System.Drawing.Point clientPoint)
		{
			Player.SendMessage(hWnd, 512U, 2U, Player.MAKELPARAM(clientPoint.X, clientPoint.Y));
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002BE2C File Offset: 0x0002A02C
		public static void RightMoveOnPoint(IntPtr wndHandle, System.Drawing.Point clientPoint)
		{
			Player.SendMessage(wndHandle, 512U, 512U, Player.MAKELPARAM(clientPoint.X, clientPoint.Y));
			Thread.Sleep(50);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002BE64 File Offset: 0x0002A064
		public static void ClickOnPoint(IntPtr hWnd, System.Windows.Point clientpoint)
		{
			int num = Player.MAKELPARAM((int)clientpoint.X, (int)clientpoint.Y);
			Player.PostMessage(hWnd, 512U, (IntPtr)0, (IntPtr)num);
			Player.PostMessage(hWnd, 513U, (IntPtr)1, (IntPtr)num);
			Player.PostMessage(hWnd, 514U, (IntPtr)0, (IntPtr)num);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		private static void MoveOnPoint(IntPtr wndHandle, System.Windows.Point clientPoint)
		{
			Player.SendMessage(wndHandle, 512U, 512U, Player.MAKELPARAM((int)clientPoint.X, (int)clientPoint.Y));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000413E File Offset: 0x0000233E
		private static int MAKELPARAM(int p, int p_2)
		{
			return (p_2 << 16) | (p & 65535);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002BF08 File Offset: 0x0002A108
		public void SaveInfo()
		{
			DataBaseInteraction.SavePlayerInfo(this);
			this._playerShow.UpdateStat();
			this._playerShowInTask.UpdateStat();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002BF34 File Offset: 0x0002A134
		public void DeletePlayer()
		{
			Program.GetInstance.DeletePlayer(this);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002BF4C File Offset: 0x0002A14C
		public void StartPlayer(bool bypass = false)
		{
			if (!this.Started)
			{
				object obj = Player.launchObject;
				lock (obj)
				{
					Console.WriteLine("[" + this.Login + "] Starting...");
					if (!File.Exists("lib\\client\\VertigoBoostClient.exe"))
					{
						CreateError.NewError("Can't find VertigoBoostClient.exe!", false);
					}
					else
					{
						int num;
						int num2;
						try
						{
							num = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[0]);
							num2 = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[1]);
						}
						catch
						{
							CreateError.NewError("Bad resolution", false);
							return;
						}
						if (num != 0 && num2 != 0)
						{
							try
							{
								this.CheckEnterToSteam();
								if (this.FilesConfiguration())
								{
									this.MySector = WindowAlign.GetFreeSector();
									if (this.MySector == null || !Settings.GetInstance.AlignPlayers)
									{
										this.MySector = new Sector(0, 0, WindowAlign.WorkMonitor);
									}
									this.MySector.IsUsed = true;
									this.MySector.login = this.Login;
									this.LastStart = DateTime.Now;
									this.hWnd = IntPtr.Zero;
									this._startLauncher(num, num2, bypass);
									this.Started = true;
									this.threadSR.Start();
									this._playerShow.UpdateStat();
									return;
								}
								return;
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex);
								return;
							}
						}
						CreateError.NewError("Bad resolution", false);
					}
				}
				return;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002C128 File Offset: 0x0002A328
		public void StopPlayer()
		{
			if (!this.Started)
			{
				return;
			}
			Console.WriteLine("[" + this.Login + "] Stopping...");
			List<string> list = new List<string>();
			if (this.LauncherProcess != null)
			{
				list.Add(this.LauncherProcess.Id.ToString());
			}
			if (this.CsGoProcess != null)
			{
				list.Add(this.CsGoProcess.Id.ToString());
			}
			if (this.SteamProcess != null)
			{
				list.Add(this.SteamProcess.Id.ToString());
			}
			foreach (string text in list)
			{
				try
				{
					Process.Start(new ProcessStartInfo
					{
						FileName = "cmd.exe",
						Arguments = "/c taskkill /f /pid \"" + text + "\"",
						WindowStyle = ProcessWindowStyle.Hidden,
						Verb = "runas"
					});
				}
				catch
				{
				}
			}
			Process launcherProcess = this.LauncherProcess;
			if (launcherProcess != null)
			{
				launcherProcess.Dispose();
			}
			this.LauncherProcess = null;
			Process csGoProcess = this.CsGoProcess;
			if (csGoProcess != null)
			{
				csGoProcess.Dispose();
			}
			this.CsGoProcess = null;
			Process steamProcess = this.SteamProcess;
			if (steamProcess != null)
			{
				steamProcess.Dispose();
			}
			this.SteamProcess = null;
			if (this.MySector != null)
			{
				this.MySector.IsUsed = false;
				this.MySector.login = null;
				this.MySector = null;
			}
			this.SR.Disconnect();
			try
			{
				Thread thread = this.threadSR;
				if (thread != null)
				{
					thread.Abort();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			this.StartStreamReading();
			this.Started = false;
			this.WindowLoaded = false;
			this.CaseFarmJoinedServer = false;
			this._hideAccountLogin = false;
			this.IsCasefarm = false;
			base.HWND = IntPtr.Zero;
			AccountShow playerShow = this._playerShow;
			if (playerShow != null)
			{
				playerShow.UpdateStat();
				return;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0002C340 File Offset: 0x0002A540
		public void OpenSteam()
		{
			Console.WriteLine("[" + this.Login + "] Opening steam...");
			Process.Start("https://steamcommunity.com/profiles/" + this.string_1 + "/");
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002C384 File Offset: 0x0002A584
		public void OpenSteamClient()
		{
			new Process
			{
				StartInfo = 
				{
					FileName = Settings.GetInstance.SteamPath + "\\steam.exe",
					Arguments = string.Concat(new string[] { "-noreactlogin -login ", this.Login, " ", this.Password, " -console -vgui" }),
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden
				}
			}.Start();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002C41C File Offset: 0x0002A61C
		public bool FilesConfiguration()
		{
			object obj = Task.Run<ConfigurationResult>(async () => await SteamFileConfiguration.GetInstance.ConfigureSteamFiles(this));
			if (Player.<>o__151.<>p__0 == null)
			{
				Player.<>o__151.<>p__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Wait", null, typeof(Player), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Player.<>o__151.<>p__0.Target(Player.<>o__151.<>p__0, obj);
			return true;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002C490 File Offset: 0x0002A690
		public void CheckEnterToSteam()
		{
			if (string.IsNullOrEmpty(Settings.GetInstance.SteamPath) || string.IsNullOrWhiteSpace(Settings.GetInstance.SteamPath))
			{
				CreateError.NewError("Put steam path in settings", false);
				return;
			}
			if (string.IsNullOrEmpty(Settings.GetInstance.CsGoPath) || string.IsNullOrWhiteSpace(Settings.GetInstance.CsGoPath))
			{
				CreateError.NewError("Put CS:GO path in settings", false);
				return;
			}
			this.string_1 = this.getSteamID64();
			if (!string.IsNullOrEmpty(this.string_1))
			{
				this.string_1 = this.getSteamID64();
				this.string_0 = this.getSteamID32();
				return;
			}
			File.Copy(Settings.GetInstance.SteamPath + "\\steam.exe", Settings.GetInstance.SteamPath + "\\steam_" + this.Login.ToLower() + ".exe", true);
			Thread.Sleep(250);
			Process process = new Process();
			process.StartInfo.FileName = Settings.GetInstance.SteamPath + "\\steam_" + this.Login.ToLower() + ".exe";
			process.StartInfo.Arguments = string.Concat(new string[] { "-noreactlogin -login ", this.Login, " ", this.Password, " -vgui" });
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();
			do
			{
				Thread.Sleep(1000);
			}
			while (string.IsNullOrEmpty(this.getSteamID64()));
			process.Kill();
			Thread.Sleep(250);
			try
			{
				File.Delete(Settings.GetInstance.SteamPath + "\\steam_" + this.Login.ToLower() + ".exe");
			}
			catch
			{
			}
			this.string_1 = this.getSteamID64();
			this.string_0 = this.getSteamID32();
		}

		// Token: 0x060005E8 RID: 1512
		[DllImport("user32.dll")]
		private static extern int LoadKeyboardLayout(string pwszKLID, uint Flags);

		// Token: 0x060005E9 RID: 1513
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x060005EA RID: 1514
		[DllImport("user32.dll")]
		private static extern void SendMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);

		// Token: 0x060005EB RID: 1515
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

		// Token: 0x060005EC RID: 1516
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x060005ED RID: 1517
		[DllImport("user32.dll")]
		private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		// Token: 0x060005EE RID: 1518
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060005EF RID: 1519
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002C69C File Offset: 0x0002A89C
		private static void PressKey(IntPtr A_0, int A_1, bool A_2)
		{
			uint num = Player.MapVirtualKey((uint)A_1, 0U);
			uint num2 = 1U | (num << 16);
			if (A_2)
			{
				num2 |= 16777216U;
			}
			Player.PostMessage(A_0, 256U, (IntPtr)A_1, (IntPtr)((long)((ulong)num2)));
		}

		// Token: 0x04000549 RID: 1353
		private static object launchObject = new object();

		// Token: 0x0400054A RID: 1354
		public string pipeName;

		// Token: 0x0400054B RID: 1355
		public string Login;

		// Token: 0x0400054C RID: 1356
		public string Password;

		// Token: 0x0400054D RID: 1357
		public int _prime;

		// Token: 0x0400054E RID: 1358
		public string string_0;

		// Token: 0x0400054F RID: 1359
		public string string_1;

		// Token: 0x04000550 RID: 1360
		public int _rank5x5;

		// Token: 0x04000551 RID: 1361
		public int _rank2x2;

		// Token: 0x04000552 RID: 1362
		public int _wins5x5;

		// Token: 0x04000553 RID: 1363
		public int _wins2x2;

		// Token: 0x04000554 RID: 1364
		public int _lvl;

		// Token: 0x04000555 RID: 1365
		public int _mark;

		// Token: 0x04000556 RID: 1366
		public DateTime _lastDrop;

		// Token: 0x04000557 RID: 1367
		public int CurrentXP = -1;

		// Token: 0x04000558 RID: 1368
		public int GainedXP;

		// Token: 0x04000559 RID: 1369
		public DateTime LastStart;

		// Token: 0x0400055A RID: 1370
		public MapPhase currentMapPhase;

		// Token: 0x0400055B RID: 1371
		public RoundPhase currentRoundPhase;

		// Token: 0x0400055C RID: 1372
		public PlayerActivity currentPlayerActivity;

		// Token: 0x0400055D RID: 1373
		public PlayerTeam currentPlayerTeam;

		// Token: 0x0400055E RID: 1374
		public string ActiveWeapon = string.Empty;

		// Token: 0x0400055F RID: 1375
		public string currentMap = string.Empty;

		// Token: 0x04000560 RID: 1376
		public PlayerTeam Team;

		// Token: 0x04000561 RID: 1377
		public Dictionary<string, int> LobbyRanks = new Dictionary<string, int>();

		// Token: 0x04000562 RID: 1378
		public Process LauncherProcess;

		// Token: 0x04000563 RID: 1379
		public Process CsGoProcess;

		// Token: 0x04000564 RID: 1380
		public Process SteamProcess;

		// Token: 0x04000565 RID: 1381
		private PlayerStreamReader SR;

		// Token: 0x04000566 RID: 1382
		private Thread threadSR;

		// Token: 0x04000567 RID: 1383
		private IntPtr hWnd = IntPtr.Zero;

		// Token: 0x04000568 RID: 1384
		private Sector MySector;

		// Token: 0x04000569 RID: 1385
		public bool Started;

		// Token: 0x0400056A RID: 1386
		public bool WindowLoaded;

		// Token: 0x0400056B RID: 1387
		public bool CaseFarmJoinedServer;

		// Token: 0x0400056C RID: 1388
		public bool IsCasefarm;

		// Token: 0x0400056D RID: 1389
		public int CompetitiveMode;

		// Token: 0x0400056E RID: 1390
		public int LobbyState;

		// Token: 0x0400056F RID: 1391
		public string match_id;

		// Token: 0x04000570 RID: 1392
		public bool IsPlayerLeader;

		// Token: 0x04000571 RID: 1393
		public PlayerState PlayerState;

		// Token: 0x04000572 RID: 1394
		private AccountShow _playerShow;

		// Token: 0x04000573 RID: 1395
		private AccountShowInTask _playerShowInTask;

		// Token: 0x04000575 RID: 1397
		private bool _hideAccountLogin;

		// Token: 0x04000576 RID: 1398
		public string steamID = string.Empty;

		// Token: 0x04000577 RID: 1399
		public string steamDeviceId = string.Empty;

		// Token: 0x04000578 RID: 1400
		public string steamIdentitySecret = string.Empty;

		// Token: 0x04000579 RID: 1401
		public string steamSessionID = string.Empty;

		// Token: 0x0400057A RID: 1402
		public string steamOAuthToken = string.Empty;

		// Token: 0x0400057B RID: 1403
		public string steamLogin = string.Empty;

		// Token: 0x0400057C RID: 1404
		public string steamLoginSecure = string.Empty;

		// Token: 0x0400057D RID: 1405
		public string steamSharedSecret = string.Empty;

		// Token: 0x0400057E RID: 1406
		private CookieContainer cookies;

		// Token: 0x0400057F RID: 1407
		private Thread collectInfoThread;

		// Token: 0x04000580 RID: 1408
		private Thread clickKnifeThread;

		// Token: 0x04000581 RID: 1409
		private Thread teamClickingThread;

		// Token: 0x04000582 RID: 1410
		private Thread mapClickingThread;

		// Token: 0x04000583 RID: 1411
		private Thread positionStreamingThread;

		// Token: 0x04000584 RID: 1412
		private CsGoProcess _process;

		// Token: 0x04000585 RID: 1413
		public Vector3 Position;
	}
}
