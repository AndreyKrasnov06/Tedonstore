using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Discord;
using Discord.Webhook;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Crypto;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000BA RID: 186
	public class PanelApiService
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001E120 File Offset: 0x0001C320
		public static PanelApiService GetInstance
		{
			get
			{
				if (PanelApiService.Class == null)
				{
					object syncObject = PanelApiService.SyncObject;
					lock (syncObject)
					{
						if (PanelApiService.Class == null)
						{
							PanelApiService.Class = new PanelApiService();
						}
					}
				}
				return PanelApiService.Class;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00003BCA File Offset: 0x00001DCA
		public string Password
		{
			get
			{
				return Hardware.HWID;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00003BD1 File Offset: 0x00001DD1
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0001E184 File Offset: 0x0001C384
		public bool Alive
		{
			get
			{
				return this._alive;
			}
			set
			{
				this._alive = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001E198 File Offset: 0x0001C398
		public Dictionary<string, string> UserAgent
		{
			get
			{
				string text = string.Empty;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					text = currentProcess.ProcessName.Replace("ertigo", string.Empty).Replace("oost", string.Empty).Replace("anel", string.Empty);
				}
				return new Dictionary<string, string> { { "user-agent", text } };
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001E214 File Offset: 0x0001C414
		public async Task<string> GetRequest(string line)
		{
			string text = string.Empty;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://panel.tedonstore.com/" + line);
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
			text = streamReader.ReadToEnd();
			httpWebResponse.Close();
			streamReader.Close();
			return text;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001E258 File Offset: 0x0001C458
		public void SendDiscordNotificationMessage(string text)
		{
			DiscordWebhook discordWebhook = new DiscordWebhook();
			discordWebhook.Url = Settings.GetInstance.DiscordHook;
			DiscordMessage discordMessage = default(DiscordMessage);
			discordMessage.Content = string.Empty;
			discordMessage.TTS = false;
			discordMessage.Username = "tedonstore.com";
			discordMessage.AvatarUrl = "https://vbpanel.com/resources/bot_logo.jpg";
			DiscordEmbed discordEmbed = default(DiscordEmbed);
			discordEmbed.Title = text;
			discordEmbed.Description = string.Empty;
			discordEmbed.Timestamp = new DateTime?(DateTime.Now);
			discordEmbed.Color = new Color?(Color.Blue);
			discordEmbed.Footer = new EmbedFooter?(new EmbedFooter
			{
				Text = "tedonstore.com",
				IconUrl = "https://vbpanel.com/resources/logddo.png"
			});
			discordMessage.Embeds = new List<DiscordEmbed>();
			discordMessage.Embeds.Add(discordEmbed);
			discordWebhook.Send(discordMessage, null);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001E33C File Offset: 0x0001C53C
		public async Task CheckIsAlive()
		{
			try
			{
				Console.WriteLine("request");
				TaskAwaiter<string> taskAwaiter = this.GetRequest("vb/checkOnline.php?action=update&token=" + Program.GetInstance.TOKEN).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<string> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<string>);
				}
				Console.WriteLine(taskAwaiter.GetResult());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001E380 File Offset: 0x0001C580
		public async Task<string> RegisterGame(string session_type, string game_mode, string map)
		{
			string text;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Add("user-agent", "VBP");
					webClient.Encoding = Encoding.UTF8;
					webClient.QueryString.Add("token", Program.GetInstance.TOKEN);
					webClient.QueryString.Add("key", "register");
					webClient.QueryString.Add("session_type", session_type);
					webClient.QueryString.Add("game_mode", game_mode);
					webClient.QueryString.Add("map", map);
					byte[] array = webClient.UploadValues("https://panel.tedonstore.com/vb/gamestats.php", webClient.QueryString);
					text = Encoding.UTF8.GetString(array);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		public async Task UpdateGame(string game_id, string players_statistic)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Add("user-agent", "VBP");
					webClient.Encoding = Encoding.UTF8;
					webClient.QueryString.Add("token", Program.GetInstance.TOKEN);
					webClient.QueryString.Add("key", "update");
					webClient.QueryString.Add("game_id", game_id);
					webClient.QueryString.Add("players_statistic", players_statistic);
					byte[] array = webClient.UploadValues("https://panel.tedonstore.com/vb/gamestats.php", webClient.QueryString);
					Console.WriteLine("[UpdateGame] " + Encoding.UTF8.GetString(array));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001E420 File Offset: 0x0001C620
		public async Task SendPlayerStatistic(string login, string steamID, string XPGained, string XPCurrentLVL, string XPUntilNextLevel, string CurrentMatchmakingRank, string CurrentMatchmakingWins)
		{
			try
			{
				string text = File.ReadAllText("data/webhook.txt").Replace("\n", "").Replace(" ", "");
				if (!string.IsNullOrEmpty(text) && text.Contains("https://discord.com/api/webhooks/"))
				{
					string text2 = "Unknown";
					string text3 = string.Empty;
					try
					{
						using (WebClient webClient = new WebClient())
						{
							webClient.Headers.Clear();
							webClient.Headers.Add("user-agent", "VBP");
							JObject jobject = JObject.Parse(webClient.DownloadString("https://panel.tedonstore.com/vb/steamavatar.php?steam_id=" + steamID));
							text2 = jobject["name"].ToString();
							text3 = jobject["avatar"].ToString();
						}
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
					}
					ResourceDictionary resourceDictionary = new ResourceDictionary();
					resourceDictionary.Source = new Uri("/VertigoBoostPanel;component/Resources/languages/lang." + Settings.GetInstance.Language + ".xaml", UriKind.Relative);
					string text4 = resourceDictionary["m_Stats"] as string;
					string text5 = resourceDictionary["m_XPGainedPerGame"] as string;
					string text6 = resourceDictionary["m_CurrentLevel"] as string;
					string text7 = resourceDictionary["m_XPUntilNextLevel"] as string;
					string text8 = resourceDictionary["m_CurrentRank5x5"] as string;
					string text9 = resourceDictionary["m_CurrentWins5x5"] as string;
					string text10 = resourceDictionary["m_Profile"] as string;
					DiscordWebhook discordWebhook = new DiscordWebhook();
					discordWebhook.Url = text;
					DiscordMessage discordMessage = default(DiscordMessage);
					discordMessage.Content = "";
					discordMessage.TTS = false;
					discordMessage.Username = "tedonstore.com";
					discordMessage.AvatarUrl = "https://vbpanel.com/resources/bot_logo.jpg";
					DiscordEmbed discordEmbed = default(DiscordEmbed);
					discordEmbed.Title = string.Concat(new string[] { text4, " **", text2, "**  |  **", login, "**" });
					discordEmbed.Description = string.Concat(new string[] { "[`", text10, "`](https://steamcommunity.com/profiles/", steamID, "/)" });
					discordEmbed.Timestamp = new DateTime?(DateTime.Now);
					discordEmbed.Color = new Color?(Color.Red);
					discordEmbed.Thumbnail = new EmbedMedia?(new EmbedMedia
					{
						Url = text3,
						Width = new int?(150),
						Height = new int?(150)
					});
					discordEmbed.Footer = new EmbedFooter?(new EmbedFooter
					{
						Text = "tedonstore.com",
						IconUrl = "https://vbpanel.com/resources/logddo.png"
					});
					discordEmbed.Fields = new List<EmbedField>();
					discordEmbed.Fields.Add(new EmbedField
					{
						Name = "**_" + text5 + "_**",
						Value = "```" + XPGained + "xp```",
						InLine = true
					});
					discordEmbed.Fields.Add(new EmbedField
					{
						Name = "**_" + text6 + "_**   ",
						Value = "```" + XPCurrentLVL + "```",
						InLine = true
					});
					discordEmbed.Fields.Add(new EmbedField
					{
						Name = "**_" + text7 + "_**",
						Value = "```" + XPUntilNextLevel + "xp```",
						InLine = true
					});
					discordEmbed.Fields.Add(new EmbedField
					{
						Name = "**_" + text8 + "_**",
						Value = "```" + CurrentMatchmakingRank + "```",
						InLine = true
					});
					discordEmbed.Fields.Add(new EmbedField
					{
						Name = "**_" + text9 + "_**",
						Value = "```" + CurrentMatchmakingWins + "```",
						InLine = true
					});
					discordMessage.Embeds = new List<DiscordEmbed>();
					discordMessage.Embeds.Add(discordEmbed);
					discordWebhook.Send(discordMessage, null);
				}
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2);
			}
		}

		// Token: 0x040003A8 RID: 936
		private static volatile PanelApiService Class;

		// Token: 0x040003A9 RID: 937
		private static object SyncObject = new object();

		// Token: 0x040003AA RID: 938
		private bool _alive = true;
	}
}
