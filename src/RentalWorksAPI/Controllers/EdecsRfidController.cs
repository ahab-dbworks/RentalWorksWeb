using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RentalWorksAPI.Models;

namespace RentalWorksAPI.Controllers
{
    /// <summary>
    /// Receives messages from EDECS and broadcasts the data to connected web socket clients.
    /// </summary>
    public class EdecsRfidController : ApiController
    {
        /// <summary>
        /// Open a web socket connection to the server to listen for EDECS messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("edecsrfid/openwebsocket")]
        public HttpResponseMessage Get()
        {
            //EdecsPublisher.Instance.Log("begin EdecsRfidController.Get()");
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest || currentContext.IsWebSocketRequestUpgrading)
            {
                currentContext.AcceptWebSocketRequest(ProcessWebSocketSession);
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception("This method only supports web socket connections."));
            }
            //EdecsPublisher.Instance.Log("end EdecsRfidController.Get()");
        }

        /// <summary>
        /// POST messages from EDECS.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("edecsrfid/edecsmessage")]
        //public void Post()
        //{
        //    byte[] body = Request.Content.ReadAsByteArrayAsync().Result;
        //    string json = System.Text.Encoding.ASCII.GetString(body);
        //    dynamic request = JsonConvert.DeserializeObject<ExpandoObject>(json);
        //}
        public HttpResponseMessage EdecsMessage([FromBody]EdecsMessage message)
        {
            if (message != null)
            {
                EdecsPublisher.Instance.NotifySubscribers(message);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private async Task ProcessWebSocketSession(AspNetWebSocketContext context)
        {
            EdecsSubscriber subscriber = null;
            WebSocket socket;
            ArraySegment<byte> receivebuffer;
            WebSocketReceiveResult receiveResult;
            string jsonrequest;
            dynamic request;
            //CancellationTokenSource listenClientCancellationToken;
            try
            {
                using (var listenClientCancellationToken = new CancellationTokenSource())
                {
                    //EdecsPublisher.Instance.Log("begin EdecsRfidController.ProcessWebSocketSession()");
                    subscriber = EdecsPublisher.Instance.AddSubscriber(context);
                    socket        = context.WebSocket;
                    receivebuffer = new ArraySegment<byte>(new byte[4096]);
                    receiveResult = await socket.ReceiveAsync(receivebuffer, CancellationToken.None);
                    jsonrequest = Encoding.UTF8.GetString(receivebuffer.Array, 0, receiveResult.Count);
                    request = JsonConvert.DeserializeObject<ExpandoObject>(jsonrequest);
                    switch ((string)request.command)
                    {
                        case "startlistening":
                            Task listenClientTask = Task.Run(async ()=> {
                                while (socket.State == WebSocketState.Open || socket.State == WebSocketState.CloseSent)
                                {
                                    if ((socket.State == WebSocketState.CloseSent) || listenClientCancellationToken.IsCancellationRequested)
                                    {
                                        break;
                                    }
                                    receiveResult = await socket.ReceiveAsync(receivebuffer, CancellationToken.None);
                                    jsonrequest = Encoding.UTF8.GetString(receivebuffer.Array, 0, receiveResult.Count);
                                    request = JsonConvert.DeserializeObject<ExpandoObject>(jsonrequest);
                                    switch ((string)request.command)
                                    {
                                        case "stoplistening":
                                        subscriber.StopListening();
                                        break;
                                    }
                                }
                            }, listenClientCancellationToken.Token);
                            subscriber.StartListening((string)request.portal, (int)request.batchtimeout);
                            listenClientCancellationToken.Cancel();
                            break;
                    }
                    EdecsPublisher.Instance.RemoveSubscriber(subscriber);
                }
            }
            catch //(Exception ex)
            {
                //EdecsPublisher.Instance.Log(ex.Message);
                if (subscriber != null)
                {
                    EdecsPublisher.Instance.RemoveSubscriber(subscriber);
                }
            }
            //EdecsPublisher.Instance.Log("end EdecsRfidController.ProcessWebSocketSession()");
        }
    } 
}