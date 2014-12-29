using System;
using System.Net;
using System.Web;
using System.IO;

namespace WebApplicationRouter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("WebApp Router");

			var listener = new HttpListener ();

			listener.Prefixes.Add ("http://localhost:8080/");

			Console.WriteLine ("Listening..");

			listener.Start ();

			while (listener.IsListening) {
				Console.WriteLine (listener.GetContext ());

				var context = listener.GetContext ();

				var response = context.Response;

				//const string responseString = "<html><body>Hello world</body></html>";
				String responseString = new WebClient ().DownloadString ("http://localhost:8888/hello/");
				var buffer = System.Text.Encoding.UTF8.GetBytes (responseString);
				response.ContentLength64 = buffer.Length;

				var output = response.OutputStream;

				output.Write (buffer, 0, buffer.Length);

				Console.WriteLine (output);

				output.Close ();
			}

			listener.Stop ();

			Console.ReadKey ();
		}
		/*
		protected static String forwardRequest (HttpListenerContext context)
		{
			HttpListenerRequest original = context.Request;
			HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create ("http://localhost:8888/hello/");

			newRequest.ContentType = original.ContentType;
			newRequest.Method = original.HttpMethod;
			newRequest.UserAgent = original.UserAgent;

			byte[] originalStream = System.Text.Encoding.UTF8.GetBytes (original.InputStream, 1024);

			Stream reqStream = newRequest.GetRequestStream ();
			reqStream.Write (originalStream, 0, originalStream.Length);
			reqStream.Close ();


			return newRequest.GetResponse ();
		}
		*/
	}
}
