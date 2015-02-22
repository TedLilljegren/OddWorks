using OddWorks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace OddWorks.WebControllers
{
    public class MessagesController : Controller
    {
        public ActionResult Index()
        {
            //It is pretty overkill to get the messages from the API instead of just using the 
            //message repository, but the point is to demonstrate a web page that uses an API

            var messages = this.GetMessagesFromApi();

            return View(messages);
        }

        private List<Message> GetMessagesFromApi()
        {
            var list = new List<Message>();
            const string url = "http://localhost:63607/api/messages";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            
            using (var webresponse = (HttpWebResponse)webrequest.GetResponse())
            {
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                if (webresponse.StatusCode == HttpStatusCode.OK)
                {
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<Message>>(result);
                }
                
            }

            return list;
        }
    }
}
