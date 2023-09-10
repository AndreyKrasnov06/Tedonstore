using System;
using System.Net;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000139 RID: 313
	public static class Offsets
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00030F74 File Offset: 0x0002F174
		public static void Load()
		{
			WebClient webClient = new WebClient();
			Offsets.hazedumper = JsonConvert.DeserializeObject(webClient.DownloadString("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.json"));
			webClient.Dispose();
		}

		// Token: 0x04000618 RID: 1560
		[Dynamic]
		public static dynamic hazedumper;
	}
}
