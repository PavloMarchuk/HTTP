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
						Console.WriteLine();
					}
				}
			}
		}

		async static void PostRequest(string uri)
		{ }

	}
}
