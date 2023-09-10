using System;
using System.Collections.Generic;
using CSGSI.Nodes;
using VertigoBoostPanel.Core.Player;

namespace VertigoBoostPanel.Model.GSI
{
	// Token: 0x02000026 RID: 38
	public class LobbyGSI
	{
		// Token: 0x040000C1 RID: 193
		public int HalfRoundsCount;

		// Token: 0x040000C2 RID: 194
		public bool IsPlanted;

		// Token: 0x040000C3 RID: 195
		public string id_bomb_player = "";

		// Token: 0x040000C4 RID: 196
		public int TypeGame;

		// Token: 0x040000C5 RID: 197
		public List<Player> AccountsToDisconnect = new List<Player>();

		// Token: 0x040000C6 RID: 198
		public bool IsReconnected;

		// Token: 0x040000C7 RID: 199
		public bool ValueDisconnectWorker;

		// Token: 0x040000C8 RID: 200
		public List<Player> first_team = new List<Player>();

		// Token: 0x040000C9 RID: 201
		public List<Player> second_team = new List<Player>();

		// Token: 0x040000CA RID: 202
		public string leaderID = "1";

		// Token: 0x040000CB RID: 203
		public int ScoreCT;

		// Token: 0x040000CC RID: 204
		public int ScoreT;

		// Token: 0x040000CD RID: 205
		public int WhereDisconnect;

		// Token: 0x040000CE RID: 206
		public RoundPhase RoundPhase;

		// Token: 0x040000CF RID: 207
		public string MapName;

		// Token: 0x040000D0 RID: 208
		public MapPhase xhase;

		// Token: 0x040000D1 RID: 209
		public PlayerTeam leaderTeam;

		// Token: 0x040000D2 RID: 210
		public List<string> SteamIds = new List<string>();

		// Token: 0x040000D3 RID: 211
		public int CountRoundsMatch;

		// Token: 0x040000D4 RID: 212
		public bool IsGameOver;

		// Token: 0x040000D5 RID: 213
		public string title = "";

		// Token: 0x040000D6 RID: 214
		public bool newMethod;
	}
}
