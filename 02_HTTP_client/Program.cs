using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _02_HTTP_client
{
	class Program
	{
		static void Main(string[] args)
		{
			GetRequest("http://www.google.com");			
			Console.WriteLine("\n\nConsole.ReadKey()  =============================================================================");
			Console.ReadKey();
			//Console.WriteLine("\n\nConsole.ReadKey()  =============================================================================");
			PostRequest("http://localhost:8888/connection");
			Console.ReadKey();
		}

		async static void GetRequest ( string uri)
		{
			using(HttpClient client = new HttpClient())
			{
				using(HttpResponseMessage respons = await client.GetAsync(uri))
				{
					using(HttpContent content = respons.Content)
					{
						string message =await content.ReadAsStringAsync();
						Console.WriteLine(message);
						Console.WriteLine("HEDER:");
						Console.WriteLine(content.Headers);
						Console.WriteLine();
						Console.WriteLine(message.Length);
					}
				}
			}
		}

		async static void PostRequest(string uri)
		{
			IEnumerable<KeyValuePair<String, string>> parameters = new List<KeyValuePair<String, string>>()
			{
				new KeyValuePair<string, string>("name", "igor"),
				new KeyValuePair<string, string>("age", "18")
			};
			HttpContent contentQery = new FormUrlEncodedContent(parameters);

			using(HttpClient client = new HttpClient())
			{
				using(HttpResponseMessage respons = await client.PostAsync(uri, contentQery))
				{
					Console.WriteLine(respons.StatusCode);
					if((int)respons.StatusCode == 404)
					{
						Console.WriteLine("СТОРІНКА НЕ ЗНАЙДЕНА (404)");
					}
					using(HttpContent content = respons.Content)
					{
						string message = await content.ReadAsStringAsync();
						Console.WriteLine(message);
						Console.WriteLine("HEDER:");
						Console.WriteLine(content.Headers);
						Console.WriteLine();
						Console.WriteLine(message.Length);						
					}
				}
			}
		}
	}
}
