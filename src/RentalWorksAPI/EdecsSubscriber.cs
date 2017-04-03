using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;
using Newtonsoft.Json;
using RentalWorksAPI.Models;

namespace RentalWorksAPI
{
    public class EdecsSubscriber
    {
        private int _batchTimeout = 10;
        private string _portal = string.Empty;
        private AspNetWebSocketContext _context = null;
        private bool _isListening = false;
        private ConcurrentQueue<EdecsMessage> _quickNotifyQueue = new ConcurrentQueue<EdecsMessage>();
        private ConcurrentQueue<EdecsMessage> _batchQueue = new ConcurrentQueue<EdecsMessage>();
        private ConcurrentDictionary<string, EdecsMessage> _batchEpcs = new ConcurrentDictionary<string, EdecsMessage>();
        private DateTime _batchExpires = DateTime.MaxValue; // initially the batch doesn't expire until we get tags in the quickNotifyQueue
        public bool stopListening = false;

        public EdecsSubscriber(AspNetWebSocketContext context)
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.EdecsSubscriber()");
            _context = context;
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.EdecsSubscriber()");
        }

        public void ResetBatchTimer()
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.ResetBatchTimer()");
            _batchExpires = DateTime.Now.AddSeconds(_batchTimeout);
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.ResetBatchTimer()");
        }

        public void StartListening(string portal, int batchtimeout)
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.StartListening()");
            _portal = portal;
            _batchTimeout = batchtimeout;
            _isListening = true;
            //ResetBatchTimer();
            while (_isListening && EdecsPublisher.Instance.HasSubscriber(this))
            {
                // provides immediate notification to the websocket subscriber about scans
                if (_quickNotifyQueue.Count > 0)
                {
                    List<EdecsMessage> epcs = new List<EdecsMessage>();
                    while (_quickNotifyQueue.Count > 0)
                    {
                        EdecsMessage epc = new EdecsMessage();
                        bool success = _quickNotifyQueue.TryDequeue(out epc);
                        if (success)
                        {
                            epcs.Add(epc);
                            ResetBatchTimer();
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (epcs.Count > 0)
                    {
                        EdecsSubscriberNotification countMessage = new EdecsSubscriberNotification() { type=EdecsSubscriberNotification.TYPE_COUNT, count=_batchEpcs.Count, epcs=epcs };
                        SendNotification(countMessage);
                        //EdecsPublisher.Instance.Log("EdecsSubscriber.StartListening(): Sent websocket count notification");
                    }
                }

                // batch expired
                if (_batchExpires < DateTime.Now)
                {
                    StopListening();

                    List<EdecsMessage> epcs = new List<EdecsMessage>();
                    while (_batchQueue.Count > 0)
                    {
                        EdecsMessage epc = new EdecsMessage();
                        bool success = _batchQueue.TryDequeue(out epc);
                        if (success)
                        {
                            epcs.Add(epc);
                        }
                        else
                        {
                            break;
                        }
                    }
                    EdecsSubscriberNotification countMessage = new EdecsSubscriberNotification() { type=EdecsSubscriberNotification.TYPE_BATCH, count=_batchEpcs.Count, epcs=epcs };
                    SendNotification(countMessage);
                    //EdecsPublisher.Instance.Log("EdecsSubscriber.StartListening(): Sent websocket batch expired notification");
                    //bool timedout = Task.Run(async () => {
                    //    try
                    //    {
                    //        await _context.WebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Batch timeout expired.", CancellationToken.None);
                    //    }
                    //    catch { }
                    //}).Wait(1000);
                }
                if (stopListening)
                {
                    _isListening = false;
                    stopListening = false;
                    break;
                }
                Thread.Sleep(250);
            }
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.StartListening()");
        }

        public void StopListening()
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.StopListening()");
            //_isListening = false;
            _batchExpires = DateTime.Now.AddMilliseconds(-1);
            stopListening = true;
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.StopListening()");
        }

        public void Notify(EdecsMessage message)
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.Notify()");
            switch (message.tpy)
            {
                case "REG": // an epc message
                    //EdecsPublisher.Instance.Log("EdecsSubscriber.Notify(): REG");
                    if ((_isListening) && (message.lnm == this._portal))
                    {
                        string[] epcs = message.epc.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string epc in epcs)
                        {
                            EdecsMessage message2 = new EdecsMessage() {};
                            message2.epc = epc;
                            if (!_batchEpcs.ContainsKey(epc))
                            {
                                _batchQueue.Enqueue(message2);
                                _batchEpcs[epc] = message2;
                                _quickNotifyQueue.Enqueue(message2);
                                //EdecsPublisher.Instance.Log("EdecsSubscriber.Notify(): REG-queued");
                            }
                        }
                    }
                    break;
                case "ANT_SHIFT_EVT":
                case "ALIVE_EVT":
                default:
                    break;
            }
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.Notify()");
        }

        private void SendNotification(EdecsSubscriberNotification message)
        {
            //EdecsPublisher.Instance.Log("begin EdecsSubscriber.SendNotification()");
            ArraySegment<byte> sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message.ToJsonString()));
            try
            {
                Task.Run(async () => {
                    bool sent = false;
                    int attemptsleft = 5;
                    bool fail = false;
                    do
                    {
                        try
                        {
                            await _context.WebSocket.SendAsync(sendbuffer, WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);
                            sent = true;
                        }
                        catch(AggregateException)
                        {

                        }
                        catch
                        {
                            fail = true;
                        }
                        attemptsleft--;
                        Thread.Sleep(50);
                    } while (fail == false && sent == false && attemptsleft > 0);
                }).Wait();
            }
            catch
            {

            }
            //EdecsPublisher.Instance.Log("end EdecsSubscriber.SendNotification()");
        }
    }
}
