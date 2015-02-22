using OddWorks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OddWorks.ConsoleApplication
{
    public class ConsoleApplication
    {
        public static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Type a message");
                string choice = Console.ReadLine();

                if (choice.CompareTo("exit") == 0)
                {
                    run = false;
                }
                else
                {
                    var success = PostMessage(choice);
                    if (success)
                    {
                        Console.WriteLine("Message posted");
                    }
                    else
                    {
                        Console.WriteLine("Failure when posting message");
                    }
                }
            }
            
        }

        private static bool PostMessage(string input)
        {
            const string url    = "http://localhost:63607/api/messages";
            var message         = new Message();
            message.Date        = DateTime.Now;
            message.Content     = input;

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "POST";

            var json = "{\"Content\" : \"" + message.Content + "\", \"Date\" : \"" + message.Date + "\"}"; //A json helper function should be used here, but I don't want to introduce a framework like newtonsoft
            ASCIIEncoding encoding = new ASCIIEncoding ();
            byte[] bytes = encoding.GetBytes(json);

            // Set the content type of the data being posted.
            webrequest.ContentType = "application/json";
            // Set the content length of the string being posted.
            webrequest.ContentLength = bytes.Length;

            using (Stream contentStream = webrequest.GetRequestStream ()) 
            {
                contentStream.Write (bytes, 0, bytes.Length);
            }

            var success = false;
            try
            {
                using (var webresponse = (HttpWebResponse)webrequest.GetResponse())
                {
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    success = webresponse.StatusCode == HttpStatusCode.Created;
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            return success;
        }

    }
}
