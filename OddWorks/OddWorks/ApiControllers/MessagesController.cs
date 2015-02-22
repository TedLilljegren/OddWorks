using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OddWorks.Models;
using System.Web;
using OddWorks.Repositories;
using OddWorks.Attributes;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace OddWorks.ApiControllers
{
    //There should be some sort of authenthication mechanism that makes sure that the calls to the API
    //is authenthic, like OAuth or OAuth 2.0, but that seems to be beyond the scope
    [ExceptionHandlingAttribute]
    public class MessagesController : ApiController
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController()
        {
            //Here we really should use dependency injection to resolve _messageRepository, but it feels pretty overkill
            //to introduce a dependency injection framework for such a small project
            _messageRepository = new MessageRepository();
        }
        
        // GET api/messages
        //Gets all the messages from the db, sorted by their timestamp
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var messages = _messageRepository.GetMessages();

            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

        // POST api/messages
        // Inserts a new Message into the db
        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage Post(Message message)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var created = _messageRepository.InsertMessage(message);
            var response = new {created};

            if (created)
            {
                statusCode = HttpStatusCode.Created;
            }

            return Request.CreateResponse(statusCode, response);
        }
    }
}