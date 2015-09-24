using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace TelegramBot
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var api = new Api();
		}
	}

	public class Api
	{
		public const string AccessToken = @"";
		public string Uri = string.Format(@"https://api.telegram.org/bot{0}/", AccessToken);

		public string GetUpdates()
		{
			var getRequest = new Request().SendGetRequest(Uri + "getUpdates");
			var response = new Response().GetResponse(getRequest);
			return response;
		}

		public void SendMessage(int userId, string message)
		{
			var dict = new Dictionary<string, string> () 
			{ 
				{"chat_id", userId.ToString()},
				{"text", message},
			};
			var request = new Request().SendPostRequest(Uri + "sendMessage", dict);
			var response = new Response().GetResponse(request);

		}
	}

	public class Request
	{
		public HttpWebRequest SendGetRequest(string url)
		{
			return (HttpWebRequest)WebRequest.Create(url);
		}

		public HttpWebRequest SendPostRequest(string url, Dictionary<string, string> pairs)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);

			string post = "";

			foreach (var pair in pairs)
			{
				post += pair.Key + "=" + pair.Value + "&";
			}

			post = post.Remove(post.Length - 1);

			var data = Encoding.ASCII.GetBytes(post);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			return request;
		}
	}

	public class Response
	{
		public string GetResponse(HttpWebRequest request)
		{
			var response = (HttpWebResponse)request.GetResponse();
			return new StreamReader(response.GetResponseStream()).ReadToEnd();
		}
	}
}
