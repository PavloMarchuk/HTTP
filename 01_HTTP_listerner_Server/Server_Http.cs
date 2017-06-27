using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _01_HTTP_listerner_Server
{
	class Server_Http
	{
		private HttpListenerContext context;
		public Server_Http(HttpListenerContext context_)
		{
			context = context_;
		}
		public  void Listen()
		{
			
			HttpListenerRequest request = context.Request;
			foreach(var item in request.QueryString)
			{
				Console.WriteLine();
			}
			string name = "unnown";
			if(request.QueryString["name"] != null)
			{
				name = request.QueryString["name"];
				Console.WriteLine("name=" + name);
			}

			if(request.QueryString["age"] != null)
				Console.WriteLine("age=" + request.QueryString["age"]);
			HttpListenerResponse response = context.Response;

			string html = "<html><head> <meta charset='UTF - 8'> <body> Hello! "+ name + " age= " +request.QueryString["age"] + " </body > </head></html>";

			byte[]  data;
			Stream writer;
			if(Int32.Parse(request.QueryString["age"]) < 21)
			{
				data = Young();
			}
			else
			{
				data = Encoding.UTF8.GetBytes(html);
			}
			response.ContentLength64 = data.Length;

			writer = response.OutputStream;
			writer.Write(data, 0, data.Length);

			Console.Read();
			writer.Close();		
		}

		private byte[] Young()
		{
			string html = "<html><head> <meta charset='UTF - 8'> <body>too Young! </body > </head></html>";
			return Encoding.UTF8.GetBytes(html);
		}

	}
}
