using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Model;

namespace VertigoBoostPanel.Services.DataBase
{
	// Token: 0x02000089 RID: 137
	internal class DataBaseContext : DbContext
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00018F8C File Offset: 0x0001718C
		public DataBaseContext()
		{
			if (!DataBaseContext._created)
			{
				DataBaseContext._created = true;
				this.Database.EnsureCreated();
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00018FB8 File Offset: 0x000171B8
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			try
			{
				Directory.CreateDirectory("data");
				options.UseSqlite("Data Source=data/data", null);
			}
			catch (Exception ex)
			{
				Console.WriteLine("pizdec");
				CreateError.NewError("Something went wront", false);
				throw ex;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00003A87 File Offset: 0x00001C87
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00003A8F File Offset: 0x00001C8F
		public DbSet<Account> Players { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00003A98 File Offset: 0x00001C98
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public DbSet<SessionModel> Sessions { get; set; }

		// Token: 0x04000302 RID: 770
		private static bool _created;
	}
}
