using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VertigoBoostPanel.Services.Api
{
	// Token: 0x020000AA RID: 170
	public class RESTClient
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public async Task<string> POST(string url, Dictionary<string, string> postData, Dictionary<string, string> customHeaders)
		{
			int num;
			TaskAwaiter<string> taskAwaiter3;
			while (num != 1)
			{
				RESTClient.client.Timeout = TimeSpan.FromSeconds(5.0);
				foreach (KeyValuePair<string, string> keyValuePair in customHeaders)
				{
					RESTClient.client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
				}
				TaskAwaiter<HttpResponseMessage> taskAwaiter = RESTClient.client.PostAsync(url, new FormUrlEncodedContent(postData)).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					num = 0;
					await taskAwaiter;
					TaskAwaiter<HttpResponseMessage> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<HttpResponseMessage>);
					num = -1;
				}
				taskAwaiter3 = taskAwaiter.GetResult().Content.ReadAsStringAsync().GetAwaiter();
				if (taskAwaiter3.IsCompleted)
				{
					IL_151:
					return taskAwaiter3.GetResult();
				}
				num = 1;
				await taskAwaiter3;
			}
			TaskAwaiter<string> taskAwaiter4;
			taskAwaiter3 = taskAwaiter4;
			taskAwaiter4 = default(TaskAwaiter<string>);
			num = -1;
			goto IL_151;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001CA24 File Offset: 0x0001AC24
		public async Task<T> GET<T>(string url, Dictionary<string, string> customHeaders)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					int num;
					TaskAwaiter<byte[]> taskAwaiter3;
					while (num != 1)
					{
						client.Timeout = TimeSpan.FromSeconds(30.0);
						foreach (KeyValuePair<string, string> keyValuePair in customHeaders)
						{
							client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
						}
						TaskAwaiter<HttpResponseMessage> taskAwaiter = client.GetAsync(url).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							await taskAwaiter;
							TaskAwaiter<HttpResponseMessage> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<HttpResponseMessage>);
							num = -1;
						}
						HttpResponseMessage result = taskAwaiter.GetResult();
						result.EnsureSuccessStatusCode();
						taskAwaiter3 = result.Content.ReadAsByteArrayAsync().GetAwaiter();
						if (taskAwaiter3.IsCompleted)
						{
							IL_168:
							byte[] result2 = taskAwaiter3.GetResult();
							return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(result2), new JsonSerializerSettings
							{
								TypeNameHandling = TypeNameHandling.Auto,
								NullValueHandling = NullValueHandling.Ignore
							});
						}
						num = 1;
						await taskAwaiter3;
					}
					TaskAwaiter<byte[]> taskAwaiter4;
					taskAwaiter3 = taskAwaiter4;
					taskAwaiter4 = default(TaskAwaiter<byte[]>);
					num = -1;
					goto IL_168;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Ошибка при выполнении запроса: {0}", ex));
				throw;
			}
			T t;
			return t;
		}

		// Token: 0x04000367 RID: 871
		private static readonly HttpClient client = new HttpClient();
	}
}
