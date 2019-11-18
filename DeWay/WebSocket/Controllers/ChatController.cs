using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Web.WebSockets;

namespace WebSocket.Controllers
{
    public class ChatController : ApiController
    {

        public HttpResponseMessage Get(string  username)
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(username));

            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }


        class ChatWebSocketHandler:WebSocketHandler
        {
            string _username;
            static WebSocketCollection _chatClient = new WebSocketCollection();


            public ChatWebSocketHandler(string username)
            {
                _username = username;
            }


            public override void OnOpen()
            {
                _chatClient.Add(this);
            }

            public override void OnMessage(string message)
            {
                _chatClient.Broadcast(_username + "說"+message);
            }
        }

       

    }
}
