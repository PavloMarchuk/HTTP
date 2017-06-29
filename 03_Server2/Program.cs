using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _03_Server2
{
	class Program
	{		
		static void Main(string[] args)
		{
			HttpListener listerner = new HttpListener();
			listerner.Prefixes.Add("http://localhost:8888/connection/");

			listerner.Start();

			Console.WriteLine("Server Started!!!!!");

			HttpListenerContext context = listerner.GetContext();
			HttpListenerRequest request = context.Request;

			string text;
			//string name = "unknown";

			Dictionary<string, string> parametrs= new Dictionary<string, string>();

			if(request.ContentLength64 > 0) //параметри через POST
			{
				using(var reader = new StreamReader(request.InputStream, request.ContentEncoding))
				{
					text = reader.ReadToEnd();

					string[] parametersPairs = text.Split('&');
					for(int i = 0; i < parametersPairs.Length; i++)
					{
						string [] pair = parametersPairs[i].Split('=');
						parametrs.Add(pair[0], pair[1]);						
					}
				}

			}
			else
			if(request.QueryString.Count > 0) //параметри через URL
			{
				string [] keys = request.QueryString.AllKeys;
				for(int i = 0; i < keys.Length; i++)
				{
					parametrs.Add(keys[i], request.QueryString[keys[i]]);
				}
			}
			else// дефолтне значення
			{
				parametrs.Add("name", "Guest");
				// home page
			}
			
			HttpListenerResponse response = context.Response;

			string html = "<html><head> <meta charset='UTF - 8'> <body> Hello! " + parametrs["name"] + " </body > </head></html>";

			byte[] data = Encoding.UTF8.GetBytes(html);
			response.ContentLength64 = data.Length;

			Stream writer = response.OutputStream;
			writer.Write(data, 0, data.Length);

			Console.Read();
			writer.Close();
			////////////////////////////
			listerner.Stop();
		}
	}
}
