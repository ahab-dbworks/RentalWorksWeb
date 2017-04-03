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
using Fw.Json.ValueTypes;
using Newtonsoft.Json;
using RentalWorksAPI.Models;

namespace RentalWorksAPI
{
    public sealed class EdecsPublisher
    {
        // singleton code
        static readonly EdecsPublisher instance = new EdecsPublisher();
        static EdecsPublisher() { } // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        public static EdecsPublisher Instance { get { return instance; } }

        // private members
        private List<EdecsSubscriber> _subscribers = new List<EdecsSubscriber>();
        private static object subscriberslock = new object();


        EdecsPublisher()
        {
            //Log("EdecsPublisher.EdecsPublisher()");
        }

        //public void Log(string message)
        //{
        //    if (HttpContext.Current != null)
        //    {
        //        string path = HttpContext.Current.Server.MapPath("~/App_Data/EdecsPublisherLog.txt");
        //        if (File.Exists(path))
        //        {
        //            File.AppendAllText(path, message + Environment.NewLine);
        //        }
        //    }
        //}

        public bool HasSubscriber(EdecsSubscriber subscriber)
        {
            bool result = _subscribers.Contains(subscriber);
            return result;
        }

        public EdecsSubscriber AddSubscriber(AspNetWebSocketContext context)
        {
            //Log("begin EdecsPublisher.AddSubscriber()");
            EdecsSubscriber subscriber = new EdecsSubscriber(context);
            lock(subscriberslock)
            {
                _subscribers.Add(subscriber);
            }
            //Log("end EdecsPublisher.AddSubscriber()");
            return subscriber;
        }

        public void RemoveSubscriber(EdecsSubscriber subscriber)
        {
            //Log("begin EdecsPublisher.RemoveSubscriber()");
            lock (subscriberslock)
            {
                _subscribers.Remove(subscriber);
            }
            //Log("end EdecsPublisher.RemoveSubscriber()");
        }

        public void NotifySubscribers(EdecsMessage message)
        {
            //Log("begin EdecsPublisher.NotifySubscribers()");
            try
            {
                lock (subscriberslock)
                {
                    //Log("EdecsPublisher.NotifySubscribers(): SubscriberCount: " + _subscribers.Count.ToString());
                    //Log("EdecsPublisher.NotifySubscribers(): Message: " + JsonConvert.SerializeObject(message));
                    foreach (EdecsSubscriber subscriber in _subscribers)
                    {
                        subscriber.Notify(message);
                    }
                }
            }
            catch
            {
                //Log(ex.Message);
            }
            //Log("end EdecsPublisher.NotifySubscribers()");
        }
    }
}
