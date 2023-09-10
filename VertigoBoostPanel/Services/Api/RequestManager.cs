using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VertigoBoostPanel.UI.Pages;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000AE RID: 174
	public class RequestManager
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00003B95 File Offset: 0x00001D95
		public RequestManager(Dictionary<string, string> customHeaders = null)
		{
			this.CustomHeaders = customHeaders;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001CF18 File Offset: 0x0001B118
		public async Task<string> POST(string url, Dictionary<string, string> data, string contentType = "application/json")
		{
			return await this.ConstructAndMakeRequest(url, HttpMethod.Post, contentType, data);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001CF74 File Offset: 0x0001B174
		public async Task<string> GET(string url)
		{
			return await this.ConstructAndMakeRequest(url, HttpMethod.Get, string.Empty, null);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public async Task<string> ConstructAndMakeRequest(string url, HttpMethod method, string contentType, Dictionary<string, string> postData)
		{
			string text = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				if (this.CustomHeaders != null && this.CustomHeaders.Count > 0)
				{
					foreach (KeyValuePair<string, string> keyValuePair in this.CustomHeaders)
					{
						client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				if (method == HttpMethod.Post)
				{
					MainWindow.ElapsedTimer = false;
					TaskAwaiter<HttpResponseMessage> taskAwaiter = client.PostAsync(url, new FormUrlEncodedContent(postData)).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<HttpResponseMessage> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<HttpResponseMessage>);
					}
					text = await taskAwaiter.GetResult().Content.ReadAsStringAsync();
				}
				else
				{
					if (!(method == HttpMethod.Get))
					{
						throw new ArgumentException("Method not supported");
					}
					TaskAwaiter<string> taskAwaiter3 = client.GetStringAsync(url).GetAwaiter();
					if (!taskAwaiter3.IsCompleted)
					{
						await taskAwaiter3;
						TaskAwaiter<string> taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter<string>);
					}
					text = taskAwaiter3.GetResult();
				}
			}
			HttpClient client = null;
			if (!string.IsNullOrEmpty(text) && Cache<string>.Get(url) == null)
			{
				Cache<string>.Set(url, text);
			}
			return text;
		}

		// Token: 0x04000378 RID: 888
		private Dictionary<string, string> CustomHeaders;
	}
}
