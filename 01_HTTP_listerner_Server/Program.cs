using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace _01_HTTP_listerner_Server
{
	
	class Program
	{
		//static Server server;
		static Thread serverThread;
		static void Main(string[] args)
		{
			HttpListener listerner = new HttpListener();
			listerner.Prefixes.Add("http://localhost:8888/connection/");
			listerner.Start();

			Console.WriteLine("Server Started!!!!!");
			try
			{
				// while
				while(true)
				{
					HttpListenerContext context = listerner.GetContext();
					// запоточити все далі, передавши контекст
					Server_Http server = new Server_Http(context);
					serverThread = new Thread(new ThreadStart(server.Listen));
					serverThread.Start();
				}
			}
			//HttpListenerRequest request = context.Request;
			//foreach(var item in request.QueryString)
			//{
			//	Console.WriteLine();
			//}
			//string name = "unnown";
			//if(request.QueryString["name"] != null)
			//{
			//	 name =  request.QueryString["name"];
			//	Console.WriteLine("name=" + name);
			//}
			//if(request.QueryString["age"] != null)
			//	Console.WriteLine("age=" + request.QueryString["age"]);
			//HttpListenerResponse response = context.Response;

			//string html = "<html><head> <meta charset='UTF - 8'> <body> Hello! "+ name + " </body > </head></html>";

			//byte[]  data = Encoding.UTF8.GetBytes(html);
			//response.ContentLength64 = data.Length;

			//Stream writer = response.OutputStream;
			//writer.Write(data, 0, data.Length);

			//Console.Read();
			//writer.Close();
			////////////////////////////
			catch(Exception)
			{
				listerner.Stop();
			}
			
		}
	}
}
