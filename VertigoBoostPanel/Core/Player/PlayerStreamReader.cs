using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VertigoBoostPanel.Services.Files;

namespace VertigoBoostPanel.Core.Player
{
	// Token: 0x02000123 RID: 291
	public class PlayerStreamReader
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0002EA0C File Offset: 0x0002CC0C
		public PlayerStreamReader(Player player)
		{
			this.pipeName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToLower();
			this.current_player = player;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002EA54 File Offset: 0x0002CC54
		public void ReadingProcess()
		{
			this.server = new NamedPipeServerStream(this.pipeName);
			this.server.WaitForConnection();
			StreamReader streamReader = new StreamReader(this.server);
			for (;;)
			{
				PlayerStreamReader.<>c__DisplayClass4_0 CS$<>8__locals1 = new PlayerStreamReader.<>c__DisplayClass4_0();
				CS$<>8__locals1.<>4__this = this;
				Thread.Sleep(5);
				CS$<>8__locals1.line = streamReader.ReadLine();
				if (!string.IsNullOrEmpty(CS$<>8__locals1.line))
				{
					Task.Run(delegate
					{
						PlayerStreamReader.<>c__DisplayClass4_0.<<ReadingProcess>b__0>d <<ReadingProcess>b__0>d;
						<<ReadingProcess>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ReadingProcess>b__0>d.<>4__this = CS$<>8__locals1;
						<<ReadingProcess>b__0>d.<>1__state = -1;
						<<ReadingProcess>b__0>d.<>t__builder.Start<PlayerStreamReader.<>c__DisplayClass4_0.<<ReadingProcess>b__0>d>(ref <<ReadingProcess>b__0>d);
						return <<ReadingProcess>b__0>d.<>t__builder.Task;
					});
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		public void Disconnect()
		{
			try
			{
				NamedPipeServerStream namedPipeServerStream = this.server;
				if (namedPipeServerStream != null)
				{
					namedPipeServerStream.WaitForPipeDrain();
				}
				NamedPipeServerStream namedPipeServerStream2 = this.server;
				if (namedPipeServerStream2 != null)
				{
					namedPipeServerStream2.Disconnect();
				}
				NamedPipeServerStream namedPipeServerStream3 = this.server;
				if (namedPipeServerStream3 != null)
				{
					namedPipeServerStream3.Dispose();
				}
			}
			catch (Exception ex)
			{
				LogService.GetInstance.LogInformation(ex.ToString());
			}
		}

		// Token: 0x040005DD RID: 1501
		private Player current_player;

		// Token: 0x040005DE RID: 1502
		public NamedPipeServerStream server;

		// Token: 0x040005DF RID: 1503
		public string pipeName;
	}
}
