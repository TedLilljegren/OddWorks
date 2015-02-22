using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OddWorks;
using OddWorks.ApiControllers;
using System.Net;
using OddWorks.Models;
using System.Configuration;
using System.IO;
using System.Web.Http.Hosting;
using Newtonsoft.Json;

namespace OddWorks.Tests.Controllers
{
    [TestClass]
    public class MessagesControllerTest
    {
        private MessagesController _controller;
        
        public MessagesControllerTest()
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);

            _controller = new MessagesController();
            _controller.Request = new HttpRequestMessage();
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }
        
        [TestMethod]
        public void Post()
        {
            var message     = new Message();
            message.Content = "New test message";
            var response    = _controller.Post(message);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            response = _controller.Get();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(response.Content != null);
        }

        /*
         * Would like to write a failed test as well, but that requires a lot of work setting up
         * an mocked HttpContext
         */
        
    }
}
