﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Pages;
using VertigoBoostPanel.Services.DataBase;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.Pages;
using VertigoBoostPanel.UI.pages;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000C0 RID: 192
	public class Program
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		public static Program GetInstance
		{
			get
			{
				if (Program.Class == null)
				{
					object syncObject = Program.SyncObject;
					lock (syncObject)
					{
						if (Program.Class == null)
						{
							Program.Class = new Program();
						}
					}
				}
				return Program.Class;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00003BD9 File Offset: 0x00001DD9
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001EF50 File Offset: 0x0001D150
		public string TOKEN
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001EF64 File Offset: 0x0001D164
		public int Width
		{
			get
			{
				int num;
				try
				{
					num = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[0]);
				}
				catch
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public int Height
		{
			get
			{
				int num;
				try
				{
					num = Convert.ToInt32(Settings.GetInstance.Resolution.Split(new char[] { 'x' })[1]);
				}
				catch
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		public void SetupNewAccount(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
			{
				return;
			}
			if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
			{
				Player player = new Player(login, password, 0, null, null, null, 0, 0, 0, 0, 0, 0, DateTime.MinValue);
				if (DataBaseInteraction.AddPlayer(player))
				{
					StaticData.GetInstance.Players.Add(player);
				}
				return;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001F060 File Offset: 0x0001D260
		public void DeletePlayer(Player player)
		{
			StaticData.GetInstance.Players.Remove(player);
			DataBaseInteraction.RemovePlayer(player);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00003BE1 File Offset: 0x00001DE1
		public bool AddNewTask(BoostTask task)
		{
			bool flag = DataBaseInteraction.AddTask(task);
			if (flag)
			{
				StaticData.GetInstance.BoostTasks.Add(task);
			}
			return flag;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001F084 File Offset: 0x0001D284
		public void DeleteTask(BoostTask task)
		{
			string sessionType = task.SessionType;
			DataBaseInteraction.RemoveTask(task);
			StaticData.GetInstance.BoostTasks.Remove(task);
			if (sessionType == "transfer")
			{
				((TransferPageViewModel)TransferPage.Instance.DataContext).ReadyToSwap();
				return;
			}
			TasksPage taskPage = this.TaskPage;
			if (taskPage != null)
			{
				taskPage.ReadyToSwap();
				return;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public void GetInfoFromDataBase()
		{
			foreach (Player player in DataBaseInteraction.GetAllPlayers())
			{
				StaticData.GetInstance.Players.Add(player);
			}
			foreach (BoostTask boostTask in DataBaseInteraction.GetAllTasks())
			{
				StaticData.GetInstance.BoostTasks.Add(boostTask);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001F190 File Offset: 0x0001D390
		public async Task<string> DecryptLoginCode()
		{
			string text = string.Empty;
			StringReader stringReader = new StringReader(Encoding.Unicode.GetString(this.privateRSAKey));
			RSAParameters rsaparameters = (RSAParameters)new XmlSerializer(typeof(RSAParameters)).Deserialize(stringReader);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			string[] loginCode = StaticData.GetInstance.LoginCode;
			for (int i = 0; i < loginCode.Length; i++)
			{
				byte[] array = Convert.FromBase64String(loginCode[i]);
				byte[] array2 = rsacryptoServiceProvider.Decrypt(array, false);
				text += Encoding.Unicode.GetString(array2);
			}
			rsacryptoServiceProvider.Dispose();
			return text;
		}

		// Token: 0x040003C4 RID: 964
		private static volatile Program Class;

		// Token: 0x040003C5 RID: 965
		private static object SyncObject = new object();

		// Token: 0x040003C6 RID: 966
		private string _token = string.Empty;

		// Token: 0x040003C7 RID: 967
		public int SubscriptionLevel;

		// Token: 0x040003C8 RID: 968
		public LoginPage LoginWindow;

		// Token: 0x040003C9 RID: 969
		public TasksPage TaskPage;

		// Token: 0x040003CA RID: 970
		public HomePage HomePage;

		// Token: 0x040003CB RID: 971
		public AccountsPage AccountsPage;

		// Token: 0x040003CC RID: 972
		public bool accPageActive;

		// Token: 0x040003CD RID: 973
		public MainWindow MainWindow;

		// Token: 0x040003CE RID: 974
		public byte[] privateRSAKey = new byte[]
		{
			60, 0, 63, 0, 120, 0, 109, 0, 108, 0,
			32, 0, 118, 0, 101, 0, 114, 0, 115, 0,
			105, 0, 111, 0, 110, 0, 61, 0, 34, 0,
			49, 0, 46, 0, 48, 0, 34, 0, 32, 0,
			101, 0, 110, 0, 99, 0, 111, 0, 100, 0,
			105, 0, 110, 0, 103, 0, 61, 0, 34, 0,
			117, 0, 116, 0, 102, 0, 45, 0, 49, 0,
			54, 0, 34, 0, 63, 0, 62, 0, 13, 0,
			10, 0, 60, 0, 82, 0, 83, 0, 65, 0,
			80, 0, 97, 0, 114, 0, 97, 0, 109, 0,
			101, 0, 116, 0, 101, 0, 114, 0, 115, 0,
			32, 0, 120, 0, 109, 0, 108, 0, 110, 0,
			115, 0, 58, 0, 120, 0, 115, 0, 105, 0,
			61, 0, 34, 0, 104, 0, 116, 0, 116, 0,
			112, 0, 58, 0, 47, 0, 47, 0, 119, 0,
			119, 0, 119, 0, 46, 0, 119, 0, 51, 0,
			46, 0, 111, 0, 114, 0, 103, 0, 47, 0,
			50, 0, 48, 0, 48, 0, 49, 0, 47, 0,
			88, 0, 77, 0, 76, 0, 83, 0, 99, 0,
			104, 0, 101, 0, 109, 0, 97, 0, 45, 0,
			105, 0, 110, 0, 115, 0, 116, 0, 97, 0,
			110, 0, 99, 0, 101, 0, 34, 0, 32, 0,
			120, 0, 109, 0, 108, 0, 110, 0, 115, 0,
			58, 0, 120, 0, 115, 0, 100, 0, 61, 0,
			34, 0, 104, 0, 116, 0, 116, 0, 112, 0,
			58, 0, 47, 0, 47, 0, 119, 0, 119, 0,
			119, 0, 46, 0, 119, 0, 51, 0, 46, 0,
			111, 0, 114, 0, 103, 0, 47, 0, 50, 0,
			48, 0, 48, 0, 49, 0, 47, 0, 88, 0,
			77, 0, 76, 0, 83, 0, 99, 0, 104, 0,
			101, 0, 109, 0, 97, 0, 34, 0, 62, 0,
			13, 0, 10, 0, 32, 0, 32, 0, 60, 0,
			69, 0, 120, 0, 112, 0, 111, 0, 110, 0,
			101, 0, 110, 0, 116, 0, 62, 0, 65, 0,
			81, 0, 65, 0, 66, 0, 60, 0, 47, 0,
			69, 0, 120, 0, 112, 0, 111, 0, 110, 0,
			101, 0, 110, 0, 116, 0, 62, 0, 13, 0,
			10, 0, 32, 0, 32, 0, 60, 0, 77, 0,
			111, 0, 100, 0, 117, 0, 108, 0, 117, 0,
			115, 0, 62, 0, 112, 0, 51, 0, 119, 0,
			107, 0, 122, 0, 48, 0, 121, 0, 86, 0,
			56, 0, 69, 0, 67, 0, 70, 0, 88, 0,
			100, 0, 66, 0, 111, 0, 116, 0, 85, 0,
			77, 0, 52, 0, 90, 0, 100, 0, 51, 0,
			70, 0, 89, 0, 121, 0, 116, 0, 86, 0,
			77, 0, 99, 0, 50, 0, 77, 0, 68, 0,
			102, 0, 72, 0, 71, 0, 68, 0, 119, 0,
			66, 0, 78, 0, 97, 0, 108, 0, 105, 0,
			76, 0, 66, 0, 54, 0, 80, 0, 51, 0,
			116, 0, 119, 0, 88, 0, 114, 0, 68, 0,
			100, 0, 77, 0, 74, 0, 108, 0, 102, 0,
			82, 0, 80, 0, 110, 0, 74, 0, 102, 0,
			122, 0, 110, 0, 112, 0, 70, 0, 56, 0,
			84, 0, 97, 0, 113, 0, 49, 0, 67, 0,
			109, 0, 80, 0, 43, 0, 71, 0, 43, 0,
			76, 0, 104, 0, 97, 0, 55, 0, 105, 0,
			122, 0, 55, 0, 51, 0, 88, 0, 53, 0,
			83, 0, 117, 0, 114, 0, 81, 0, 112, 0,
			73, 0, 121, 0, 98, 0, 74, 0, 119, 0,
			110, 0, 85, 0, 118, 0, 122, 0, 116, 0,
			102, 0, 55, 0, 120, 0, 99, 0, 75, 0,
			99, 0, 57, 0, 67, 0, 102, 0, 112, 0,
			78, 0, 69, 0, 80, 0, 122, 0, 78, 0,
			74, 0, 105, 0, 81, 0, 122, 0, 99, 0,
			55, 0, 116, 0, 49, 0, 122, 0, 55, 0,
			101, 0, 118, 0, 107, 0, 48, 0, 99, 0,
			54, 0, 55, 0, 56, 0, 72, 0, 81, 0,
			99, 0, 85, 0, 97, 0, 107, 0, 121, 0,
			74, 0, 66, 0, 115, 0, 57, 0, 51, 0,
			56, 0, 43, 0, 121, 0, 101, 0, 55, 0,
			80, 0, 49, 0, 66, 0, 78, 0, 99, 0,
			69, 0, 118, 0, 106, 0, 90, 0, 103, 0,
			99, 0, 72, 0, 105, 0, 66, 0, 57, 0,
			87, 0, 120, 0, 65, 0, 55, 0, 77, 0,
			49, 0, 114, 0, 52, 0, 108, 0, 77, 0,
			72, 0, 106, 0, 78, 0, 65, 0, 66, 0,
			103, 0, 100, 0, 52, 0, 70, 0, 114, 0,
			97, 0, 99, 0, 77, 0, 51, 0, 109, 0,
			105, 0, 118, 0, 74, 0, 78, 0, 50, 0,
			103, 0, 75, 0, 75, 0, 99, 0, 87, 0,
			118, 0, 70, 0, 122, 0, 101, 0, 74, 0,
			107, 0, 120, 0, 85, 0, 105, 0, 83, 0,
			72, 0, 48, 0, 88, 0, 67, 0, 103, 0,
			90, 0, 106, 0, 69, 0, 73, 0, 68, 0,
			83, 0, 115, 0, 113, 0, 77, 0, 100, 0,
			118, 0, 109, 0, 85, 0, 43, 0, 71, 0,
			76, 0, 53, 0, 49, 0, 81, 0, 49, 0,
			117, 0, 84, 0, 117, 0, 78, 0, 78, 0,
			67, 0, 71, 0, 70, 0, 80, 0, 90, 0,
			112, 0, 102, 0, 55, 0, 87, 0, 107, 0,
			43, 0, 102, 0, 76, 0, 103, 0, 77, 0,
			71, 0, 51, 0, 114, 0, 98, 0, 71, 0,
			49, 0, 85, 0, 43, 0, 83, 0, 118, 0,
			81, 0, 68, 0, 50, 0, 70, 0, 116, 0,
			122, 0, 106, 0, 109, 0, 101, 0, 72, 0,
			106, 0, 79, 0, 97, 0, 51, 0, 88, 0,
			83, 0, 51, 0, 89, 0, 88, 0, 68, 0,
			73, 0, 100, 0, 99, 0, 83, 0, 100, 0,
			98, 0, 72, 0, 121, 0, 76, 0, 97, 0,
			70, 0, 75, 0, 69, 0, 112, 0, 106, 0,
			116, 0, 85, 0, 105, 0, 119, 0, 57, 0,
			89, 0, 87, 0, 112, 0, 74, 0, 65, 0,
			75, 0, 85, 0, 119, 0, 81, 0, 74, 0,
			84, 0, 47, 0, 97, 0, 108, 0, 57, 0,
			109, 0, 76, 0, 50, 0, 90, 0, 112, 0,
			73, 0, 118, 0, 50, 0, 86, 0, 102, 0,
			82, 0, 70, 0, 43, 0, 77, 0, 111, 0,
			69, 0, 71, 0, 74, 0, 81, 0, 61, 0,
			61, 0, 60, 0, 47, 0, 77, 0, 111, 0,
			100, 0, 117, 0, 108, 0, 117, 0, 115, 0,
			62, 0, 13, 0, 10, 0, 32, 0, 32, 0,
			60, 0, 80, 0, 62, 0, 122, 0, 65, 0,
			112, 0, 117, 0, 109, 0, 119, 0, 90, 0,
			55, 0, 81, 0, 79, 0, 53, 0, 90, 0,
			80, 0, 76, 0, 87, 0, 50, 0, 86, 0,
			110, 0, 74, 0, 65, 0, 70, 0, 89, 0,
			119, 0, 122, 0, 50, 0, 107, 0, 82, 0,
			101, 0, 73, 0, 68, 0, 114, 0, 49, 0,
			56, 0, 87, 0, 88, 0, 98, 0, 112, 0,
			69, 0, 50, 0, 116, 0, 86, 0, 119, 0,
			117, 0, 115, 0, 102, 0, 54, 0, 72, 0,
			54, 0, 76, 0, 97, 0, 115, 0, 112, 0,
			116, 0, 54, 0, 47, 0, 76, 0, 82, 0,
			117, 0, 56, 0, 118, 0, 119, 0, 90, 0,
			116, 0, 99, 0, 49, 0, 98, 0, 78, 0,
			82, 0, 113, 0, 66, 0, 77, 0, 68, 0,
			121, 0, 48, 0, 120, 0, 54, 0, 76, 0,
			114, 0, 74, 0, 68, 0, 117, 0, 82, 0,
			114, 0, 50, 0, 56, 0, 66, 0, 102, 0,
			97, 0, 115, 0, 81, 0, 70, 0, 78, 0,
			101, 0, 88, 0, 76, 0, 122, 0, 101, 0,
			53, 0, 98, 0, 80, 0, 90, 0, 69, 0,
			55, 0, 43, 0, 71, 0, 102, 0, 82, 0,
			104, 0, 66, 0, 98, 0, 48, 0, 76, 0,
			74, 0, 104, 0, 98, 0, 56, 0, 97, 0,
			88, 0, 82, 0, 116, 0, 79, 0, 51, 0,
			76, 0, 82, 0, 85, 0, 116, 0, 72, 0,
			67, 0, 122, 0, 104, 0, 121, 0, 101, 0,
			87, 0, 78, 0, 52, 0, 111, 0, 80, 0,
			50, 0, 74, 0, 79, 0, 112, 0, 74, 0,
			43, 0, 76, 0, 84, 0, 89, 0, 118, 0,
			90, 0, 104, 0, 55, 0, 98, 0, 87, 0,
			102, 0, 50, 0, 77, 0, 77, 0, 103, 0,
			78, 0, 105, 0, 51, 0, 106, 0, 49, 0,
			97, 0, 76, 0, 107, 0, 47, 0, 48, 0,
			67, 0, 109, 0, 74, 0, 99, 0, 61, 0,
			60, 0, 47, 0, 80, 0, 62, 0, 13, 0,
			10, 0, 32, 0, 32, 0, 60, 0, 81, 0,
			62, 0, 48, 0, 105, 0, 75, 0, 99, 0,
			99, 0, 107, 0, 99, 0, 79, 0, 80, 0,
			122, 0, 103, 0, 115, 0, 81, 0, 117, 0,
			102, 0, 102, 0, 115, 0, 72, 0, 50, 0,
			109, 0, 51, 0, 117, 0, 115, 0, 84, 0,
			102, 0, 98, 0, 56, 0, 85, 0, 99, 0,
			107, 0, 65, 0, 47, 0, 90, 0, 73, 0,
			68, 0, 107, 0, 54, 0, 71, 0, 116, 0,
			83, 0, 108, 0, 70, 0, 106, 0, 88, 0,
			76, 0, 85, 0, 80, 0, 51, 0, 79, 0,
			85, 0, 67, 0, 99, 0, 81, 0, 110, 0,
			102, 0, 49, 0, 117, 0, 79, 0, 98, 0,
			43, 0, 105, 0, 52, 0, 87, 0, 112, 0,
			99, 0, 106, 0, 71, 0, 112, 0, 104, 0,
			81, 0, 79, 0, 97, 0, 50, 0, 81, 0,
			66, 0, 48, 0, 118, 0, 117, 0, 55, 0,
			102, 0, 79, 0, 67, 0, 43, 0, 90, 0,
			86, 0, 81, 0, 90, 0, 70, 0, 80, 0,
			114, 0, 113, 0, 84, 0, 118, 0, 79, 0,
			106, 0, 115, 0, 120, 0, 99, 0, 77, 0,
			81, 0, 112, 0, 83, 0, 117, 0, 108, 0,
			79, 0, 84, 0, 66, 0, 98, 0, 68, 0,
			114, 0, 120, 0, 119, 0, 73, 0, 49, 0,
			118, 0, 100, 0, 76, 0, 104, 0, 88, 0,
			110, 0, 115, 0, 73, 0, 105, 0, 106, 0,
			112, 0, 119, 0, 83, 0, 69, 0, 85, 0,
			115, 0, 72, 0, 85, 0, 52, 0, 90, 0,
			52, 0, 72, 0, 100, 0, 109, 0, 72, 0,
			56, 0, 79, 0, 70, 0, 72, 0, 102, 0,
			77, 0, 77, 0, 114, 0, 54, 0, 68, 0,
			105, 0, 51, 0, 75, 0, 90, 0, 73, 0,
			104, 0, 43, 0, 103, 0, 110, 0, 88, 0,
			43, 0, 55, 0, 83, 0, 83, 0, 54, 0,
			103, 0, 79, 0, 83, 0, 67, 0, 48, 0,
			113, 0, 77, 0, 61, 0, 60, 0, 47, 0,
			81, 0, 62, 0, 13, 0, 10, 0, 32, 0,
			32, 0, 60, 0, 68, 0, 80, 0, 62, 0,
			105, 0, 88, 0, 49, 0, 75, 0, 70, 0,
			74, 0, 112, 0, 107, 0, 101, 0, 86, 0,
			100, 0, 105, 0, 117, 0, 70, 0, 55, 0,
			49, 0, 116, 0, 65, 0, 67, 0, 101, 0,
			111, 0, 67, 0, 90, 0, 117, 0, 86, 0,
			108, 0, 105, 0, 86, 0, 87, 0, 73, 0,
			67, 0, 67, 0, 99, 0, 53, 0, 121, 0,
			103, 0, 83, 0, 88, 0, 71, 0, 121, 0,
			75, 0, 83, 0, 75, 0, 76, 0, 112, 0,
			83, 0, 70, 0, 47, 0, 108, 0, 69, 0,
			66, 0, 118, 0, 66, 0, 119, 0, 67, 0,
			108, 0, 75, 0, 72, 0, 74, 0, 75, 0,
			75, 0, 116, 0, 114, 0, 71, 0, 122, 0,
			112, 0, 86, 0, 98, 0, 111, 0, 55, 0,
			98, 0, 85, 0, 97, 0, 113, 0, 120, 0,
			111, 0, 57, 0, 73, 0, 104, 0, 89, 0,
			122, 0, 67, 0, 87, 0, 89, 0, 87, 0,
			105, 0, 97, 0, 111, 0, 122, 0, 88, 0,
			100, 0, 69, 0, 80, 0, 108, 0, 56, 0,
			119, 0, 74, 0, 73, 0, 112, 0, 49, 0,
			121, 0, 51, 0, 68, 0, 84, 0, 70, 0,
			113, 0, 103, 0, 117, 0, 114, 0, 74, 0,
			65, 0, 55, 0, 76, 0, 47, 0, 112, 0,
			122, 0, 77, 0, 90, 0, 103, 0, 114, 0,
			100, 0, 116, 0, 67, 0, 105, 0, 114, 0,
			110, 0, 122, 0, 56, 0, 71, 0, 75, 0,
			106, 0, 43, 0, 74, 0, 111, 0, 72, 0,
			107, 0, 101, 0, 118, 0, 109, 0, 67, 0,
			90, 0, 114, 0, 109, 0, 47, 0, 81, 0,
			52, 0, 122, 0, 71, 0, 69, 0, 119, 0,
			89, 0, 111, 0, 79, 0, 89, 0, 83, 0,
			68, 0, 121, 0, 49, 0, 113, 0, 57, 0,
			76, 0, 104, 0, 65, 0, 121, 0, 85, 0,
			53, 0, 69, 0, 56, 0, 113, 0, 53, 0,
			99, 0, 61, 0, 60, 0, 47, 0, 68, 0,
			80, 0, 62, 0, 13, 0, 10, 0, 32, 0,
			32, 0, 60, 0, 68, 0, 81, 0, 62, 0,
			98, 0, 111, 0, 65, 0, 121, 0, 52, 0,
			115, 0, 108, 0, 73, 0, 122, 0, 97, 0,
			102, 0, 100, 0, 90, 0, 113, 0, 87, 0,
			71, 0, 100, 0, 77, 0, 110, 0, 117, 0,
			109, 0, 87, 0, 84, 0, 109, 0, 48, 0,
			65, 0, 90, 0, 79, 0, 87, 0, 122, 0,
			73, 0, 55, 0, 52, 0, 55, 0, 56, 0,
			65, 0, 73, 0, 78, 0, 82, 0, 110, 0,
			68, 0, 89, 0, 109, 0, 87, 0, 82, 0,
			99, 0, 66, 0, 113, 0, 51, 0, 104, 0,
			109, 0, 67, 0, 48, 0, 108, 0, 86, 0,
			65, 0, 121, 0, 121, 0, 88, 0, 49, 0,
			106, 0, 121, 0, 115, 0, 80, 0, 105, 0,
			118, 0, 82, 0, 103, 0, 110, 0, 52, 0,
			106, 0, 103, 0, 119, 0, 101, 0, 74, 0,
			67, 0, 103, 0, 106, 0, 69, 0, 100, 0,
			65, 0, 117, 0, 47, 0, 77, 0, 52, 0,
			118, 0, 65, 0, 48, 0, 77, 0, 54, 0,
			78, 0, 114, 0, 107, 0, 122, 0, 105, 0,
			116, 0, 89, 0, 101, 0, 87, 0, 77, 0,
			43, 0, 68, 0, 53, 0, 50, 0, 87, 0,
			82, 0, 100, 0, 55, 0, 109, 0, 115, 0,
			107, 0, 117, 0, 56, 0, 118, 0, 107, 0,
			89, 0, 50, 0, 50, 0, 89, 0, 55, 0,
			101, 0, 120, 0, 75, 0, 118, 0, 100, 0,
			71, 0, 71, 0, 107, 0, 114, 0, 57, 0,
			119, 0, 114, 0, 57, 0, 83, 0, 84, 0,
			106, 0, 54, 0, 98, 0, 88, 0, 110, 0,
			75, 0, 122, 0, 113, 0, 113, 0, 99, 0,
			110, 0, 56, 0, 97, 0, 100, 0, 97, 0,
			66, 0, 81, 0, 119, 0, 81, 0, 77, 0,
			104, 0, 43, 0, 55, 0, 114, 0, 79, 0,
			47, 0, 113, 0, 101, 0, 69, 0, 120, 0,
			56, 0, 106, 0, 105, 0, 110, 0, 112, 0,
			56, 0, 61, 0, 60, 0, 47, 0, 68, 0,
			81, 0, 62, 0, 13, 0, 10, 0, 32, 0,
			32, 0, 60, 0, 73, 0, 110, 0, 118, 0,
			101, 0, 114, 0, 115, 0, 101, 0, 81, 0,
			62, 0, 120, 0, 122, 0, 72, 0, 77, 0,
			111, 0, 86, 0, 113, 0, 86, 0, 53, 0,
			71, 0, 110, 0, 75, 0, 104, 0, 120, 0,
			77, 0, 101, 0, 74, 0, 79, 0, 71, 0,
			100, 0, 78, 0, 74, 0, 81, 0, 69, 0,
			82, 0, 119, 0, 69, 0, 71, 0, 112, 0,
			117, 0, 108, 0, 55, 0, 79, 0, 50, 0,
			106, 0, 102, 0, 102, 0, 112, 0, 111, 0,
			89, 0, 109, 0, 89, 0, 86, 0, 110, 0,
			83, 0, 82, 0, 51, 0, 98, 0, 69, 0,
			118, 0, 113, 0, 70, 0, 105, 0, 107, 0,
			119, 0, 82, 0, 115, 0, 108, 0, 54, 0,
			122, 0, 67, 0, 65, 0, 87, 0, 71, 0,
			111, 0, 48, 0, 73, 0, 104, 0, 103, 0,
			108, 0, 117, 0, 68, 0, 111, 0, 90, 0,
			114, 0, 101, 0, 76, 0, 83, 0, 51, 0,
			118, 0, 66, 0, 103, 0, 51, 0, 89, 0,
			104, 0, 108, 0, 48, 0, 110, 0, 83, 0,
			99, 0, 71, 0, 55, 0, 51, 0, 55, 0,
			86, 0, 108, 0, 101, 0, 81, 0, 82, 0,
			116, 0, 71, 0, 56, 0, 55, 0, 103, 0,
			88, 0, 70, 0, 122, 0, 90, 0, 51, 0,
			80, 0, 107, 0, 67, 0, 70, 0, 119, 0,
			54, 0, 69, 0, 70, 0, 110, 0, 69, 0,
			115, 0, 73, 0, 97, 0, 88, 0, 105, 0,
			74, 0, 74, 0, 81, 0, 65, 0, 55, 0,
			51, 0, 55, 0, 49, 0, 100, 0, 53, 0,
			109, 0, 108, 0, 122, 0, 100, 0, 43, 0,
			65, 0, 109, 0, 100, 0, 119, 0, 98, 0,
			121, 0, 108, 0, 72, 0, 54, 0, 67, 0,
			77, 0, 120, 0, 87, 0, 122, 0, 80, 0,
			115, 0, 81, 0, 84, 0, 122, 0, 98, 0,
			112, 0, 49, 0, 109, 0, 75, 0, 87, 0,
			51, 0, 118, 0, 103, 0, 49, 0, 82, 0,
			79, 0, 89, 0, 61, 0, 60, 0, 47, 0,
			73, 0, 110, 0, 118, 0, 101, 0, 114, 0,
			115, 0, 101, 0, 81, 0, 62, 0, 13, 0,
			10, 0, 32, 0, 32, 0, 60, 0, 68, 0,
			62, 0, 99, 0, 52, 0, 104, 0, 88, 0,
			102, 0, 65, 0, 104, 0, 65, 0, 104, 0,
			102, 0, 74, 0, 56, 0, 68, 0, 84, 0,
			76, 0, 117, 0, 104, 0, 57, 0, 116, 0,
			79, 0, 49, 0, 68, 0, 89, 0, 102, 0,
			75, 0, 48, 0, 71, 0, 66, 0, 67, 0,
			49, 0, 72, 0, 88, 0, 65, 0, 117, 0,
			100, 0, 56, 0, 48, 0, 71, 0, 120, 0,
			77, 0, 43, 0, 115, 0, 99, 0, 74, 0,
			89, 0, 77, 0, 80, 0, 105, 0, 53, 0,
			81, 0, 88, 0, 89, 0, 70, 0, 122, 0,
			107, 0, 48, 0, 113, 0, 53, 0, 82, 0,
			85, 0, 78, 0, 112, 0, 107, 0, 55, 0,
			72, 0, 71, 0, 56, 0, 80, 0, 97, 0,
			67, 0, 81, 0, 82, 0, 114, 0, 112, 0,
			69, 0, 55, 0, 103, 0, 78, 0, 69, 0,
			73, 0, 51, 0, 116, 0, 52, 0, 83, 0,
			103, 0, 87, 0, 84, 0, 73, 0, 67, 0,
			114, 0, 51, 0, 103, 0, 72, 0, 121, 0,
			73, 0, 47, 0, 66, 0, 76, 0, 98, 0,
			114, 0, 89, 0, 116, 0, 55, 0, 54, 0,
			86, 0, 108, 0, 116, 0, 50, 0, 103, 0,
			108, 0, 50, 0, 118, 0, 81, 0, 109, 0,
			112, 0, 85, 0, 107, 0, 57, 0, 50, 0,
			115, 0, 109, 0, 117, 0, 70, 0, 71, 0,
			43, 0, 68, 0, 51, 0, 84, 0, 51, 0,
			65, 0, 104, 0, 55, 0, 47, 0, 106, 0,
			68, 0, 47, 0, 67, 0, 75, 0, 99, 0,
			82, 0, 108, 0, 105, 0, 73, 0, 108, 0,
			103, 0, 43, 0, 85, 0, 112, 0, 119, 0,
			99, 0, 71, 0, 47, 0, 47, 0, 52, 0,
			114, 0, 84, 0, 113, 0, 78, 0, 107, 0,
			83, 0, 106, 0, 73, 0, 73, 0, 90, 0,
			73, 0, 48, 0, 56, 0, 87, 0, 75, 0,
			102, 0, 72, 0, 121, 0, 114, 0, 54, 0,
			65, 0, 66, 0, 108, 0, 75, 0, 100, 0,
			52, 0, 55, 0, 74, 0, 117, 0, 79, 0,
			55, 0, 56, 0, 82, 0, 74, 0, 69, 0,
			121, 0, 97, 0, 49, 0, 121, 0, 83, 0,
			80, 0, 56, 0, 85, 0, 118, 0, 78, 0,
			74, 0, 69, 0, 56, 0, 110, 0, 80, 0,
			80, 0, 72, 0, 100, 0, 89, 0, 66, 0,
			76, 0, 103, 0, 78, 0, 110, 0, 85, 0,
			85, 0, 51, 0, 121, 0, 112, 0, 119, 0,
			80, 0, 77, 0, 117, 0, 99, 0, 81, 0,
			54, 0, 80, 0, 48, 0, 66, 0, 49, 0,
			71, 0, 88, 0, 82, 0, 52, 0, 52, 0,
			47, 0, 81, 0, 47, 0, 65, 0, 70, 0,
			84, 0, 121, 0, 121, 0, 67, 0, 119, 0,
			109, 0, 78, 0, 87, 0, 78, 0, 87, 0,
			101, 0, 121, 0, 50, 0, 82, 0, 100, 0,
			108, 0, 121, 0, 103, 0, 105, 0, 100, 0,
			72, 0, 88, 0, 97, 0, 43, 0, 78, 0,
			90, 0, 108, 0, 106, 0, 110, 0, 49, 0,
			114, 0, 107, 0, 50, 0, 77, 0, 106, 0,
			83, 0, 98, 0, 53, 0, 71, 0, 43, 0,
			84, 0, 100, 0, 84, 0, 66, 0, 50, 0,
			67, 0, 110, 0, 101, 0, 112, 0, 117, 0,
			109, 0, 71, 0, 49, 0, 87, 0, 83, 0,
			48, 0, 108, 0, 119, 0, 108, 0, 119, 0,
			88, 0, 103, 0, 68, 0, 108, 0, 104, 0,
			71, 0, 43, 0, 117, 0, 80, 0, 81, 0,
			80, 0, 117, 0, 106, 0, 57, 0, 118, 0,
			49, 0, 117, 0, 76, 0, 79, 0, 109, 0,
			87, 0, 101, 0, 81, 0, 80, 0, 112, 0,
			113, 0, 66, 0, 86, 0, 52, 0, 77, 0,
			79, 0, 54, 0, 54, 0, 101, 0, 86, 0,
			112, 0, 118, 0, 48, 0, 56, 0, 85, 0,
			56, 0, 73, 0, 81, 0, 61, 0, 61, 0,
			60, 0, 47, 0, 68, 0, 62, 0, 13, 0,
			10, 0, 60, 0, 47, 0, 82, 0, 83, 0,
			65, 0, 80, 0, 97, 0, 114, 0, 97, 0,
			109, 0, 101, 0, 116, 0, 101, 0, 114, 0,
			115, 0, 62, 0
		};
	}
}