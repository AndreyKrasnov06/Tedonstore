using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Static;

namespace VertigoBoostPanel.Services.DataBase
{
	// Token: 0x0200008A RID: 138
	public static class DataBaseInteraction
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00019008 File Offset: 0x00017208
		public static void SavePlayerInfo(Player player)
		{
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				foreach (Account account in ((IEnumerable<Account>)dataBaseContext.Players))
				{
					if (!(account.Login != player.Login))
					{
						account.Prime = player.Prime;
						account.String_0 = player.string_0;
						account.String_1 = player.string_1;
						account.InviteCode = player.InviteCode;
						account.Rank5x5 = player.Rank5x5;
						account.Rank2x2 = player.Rank2x2;
						account.Wins5x5 = player.Wins5x5;
						account.Wins2x2 = player.Wins2x2;
						account.Lvl = player.Lvl;
						account.Mark = player.Mark;
						account.LastDrop = player.LastDrop;
					}
				}
				dataBaseContext.SaveChanges();
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001912C File Offset: 0x0001732C
		public static bool AddPlayer(Player player)
		{
			Account account = new Account
			{
				Login = player.Login,
				Password = player.Password,
				Prime = player.Prime,
				String_0 = player.string_0,
				String_1 = player.string_1,
				InviteCode = player.InviteCode,
				Rank5x5 = player.Rank5x5,
				Rank2x2 = player.Rank2x2,
				Wins5x5 = player.Wins5x5,
				Wins2x2 = player.Wins2x2,
				Lvl = player.Lvl,
				Mark = player.Mark,
				LastDrop = player.LastDrop
			};
			bool flag;
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				if (dataBaseContext.Players.FirstOrDefault((Account a) => a.Login == account.Login) != null)
				{
					StaticData.GetInstance.Players.FirstOrDefault((Player p) => p.Login == account.Login).Password = account.Password;
					foreach (Account account2 in ((IEnumerable<Account>)dataBaseContext.Players))
					{
						if (account2.Login == account.Login)
						{
							account2.Password = account.Password;
						}
					}
					dataBaseContext.SaveChanges();
					flag = false;
				}
				else
				{
					dataBaseContext.Players.Add(account);
					dataBaseContext.SaveChanges();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019370 File Offset: 0x00017570
		public static void RemovePlayer(Player player)
		{
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				Account account = dataBaseContext.Players.FirstOrDefault((Account acc) => acc.Login == player.Login);
				if (account != null)
				{
					dataBaseContext.Players.Remove(account);
				}
				dataBaseContext.SaveChanges();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001944C File Offset: 0x0001764C
		public static List<Account> GetAllAccounts()
		{
			List<Account> list = new List<Account>();
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				foreach (Account account in ((IEnumerable<Account>)dataBaseContext.Players))
				{
					list.Add(account);
				}
			}
			return list;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000194C0 File Offset: 0x000176C0
		public static List<Player> GetAllPlayers()
		{
			List<Player> list2;
			try
			{
				List<Player> list = new List<Player>();
				using (DataBaseContext dataBaseContext = new DataBaseContext())
				{
					foreach (Account account in ((IEnumerable<Account>)dataBaseContext.Players))
					{
						Player player = new Player(account.Login, account.Password, account.Prime, account.String_0, account.String_1, account.InviteCode, account.Rank5x5, account.Rank2x2, account.Wins5x5, account.Wins2x2, account.Lvl, account.Mark, account.LastDrop);
						list.Add(player);
					}
					dataBaseContext.SaveChanges();
				}
				list2 = list;
			}
			catch (SqliteException ex)
			{
				MessageBox.Show("The \"data\" file is corrupted or incompatible with the current version of the panel.", "VertigoBoostPanel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
				throw ex;
			}
			return list2;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000195C0 File Offset: 0x000177C0
		public static void RemoveTask(BoostTask boostTask)
		{
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				SessionModel sessionModel = dataBaseContext.Sessions.FirstOrDefault((SessionModel acc) => acc.Id == boostTask.TaskModel.Id);
				if (sessionModel != null)
				{
					dataBaseContext.Sessions.Remove(sessionModel);
				}
				dataBaseContext.SaveChanges();
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000196B4 File Offset: 0x000178B4
		public static bool AddTask(BoostTask boostTask)
		{
			bool flag;
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				if (dataBaseContext.Sessions.FirstOrDefault((SessionModel t) => t.Name == boostTask.TaskModel.Name) != null)
				{
					flag = false;
				}
				else
				{
					dataBaseContext.Sessions.Add(boostTask.TaskModel);
					dataBaseContext.SaveChanges();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000197B8 File Offset: 0x000179B8
		public static void UpdateTask(BoostTask boostTask)
		{
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				SessionModel taskModel = boostTask.TaskModel;
				foreach (SessionModel sessionModel in ((IEnumerable<SessionModel>)dataBaseContext.Sessions))
				{
					if (sessionModel.Id == taskModel.Id)
					{
						sessionModel.Name = taskModel.Name;
						sessionModel.MVP = taskModel.MVP;
						sessionModel.ShortGame = taskModel.ShortGame;
						sessionModel.SwapLeaders = taskModel.SwapLeaders;
						sessionModel.FirstTeam = taskModel.FirstTeam;
						sessionModel.SecondTeam = taskModel.SecondTeam;
						sessionModel.ClientInviteCode = taskModel.ClientInviteCode;
						sessionModel.NeedClientRank = taskModel.NeedClientRank;
						sessionModel.CountGame = taskModel.CountGame;
						sessionModel.GameMode = taskModel.GameMode;
					}
				}
				dataBaseContext.SaveChanges();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000198DC File Offset: 0x00017ADC
		public static List<BoostTask> GetAllTasks()
		{
			List<BoostTask> list = new List<BoostTask>();
			using (DataBaseContext dataBaseContext = new DataBaseContext())
			{
				foreach (SessionModel sessionModel in ((IEnumerable<SessionModel>)dataBaseContext.Sessions))
				{
					list.Add(new BoostTask(sessionModel));
				}
				dataBaseContext.SaveChanges();
			}
			return list;
		}
	}
}
