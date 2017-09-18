using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mobilecenterpush.Helpers
{
	public static class HttpClientHelpers
	{
		
		async public static Task<T> PostAsync<T>(this HttpClient client, string url, object body)
		{
			try
			{
				var jsonIn = JsonConvert.SerializeObject(body);
				Debug.WriteLine($"URL:\n{url}");
				Debug.WriteLine($"INPUT:\n{jsonIn}");

				var content = new StringContent(jsonIn, Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, content);

				var jsonOut = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine($"OUTPUT:\n{jsonOut}");

				var returnValue = JsonConvert.DeserializeObject<T>(jsonOut);
				return returnValue;
			}
			catch (Exception e)
			{
				//TODO: log error
				Debug.WriteLine(e);
				throw;
			}
		}

		async public static Task<T> PutAsync<T>(this HttpClient client, string url, object body)
		{
			try
			{
				var jsonIn = JsonConvert.SerializeObject(body);
				Debug.WriteLine($"URL:\n{url}");
				Debug.WriteLine($"INPUT:\n{jsonIn}");

				var content = new StringContent(jsonIn, Encoding.UTF8, "application/json");
				var resp = await client.PutAsync(url, content);

				var jsonOut = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine($"OUTPUT:\n{jsonOut}");

				var returnValue = JsonConvert.DeserializeObject<T>(jsonOut);
				return returnValue;
			}
			catch (Exception e)
			{
				//TODO: log error
				Debug.WriteLine(e);
				throw;
			}
		}
	}
}
