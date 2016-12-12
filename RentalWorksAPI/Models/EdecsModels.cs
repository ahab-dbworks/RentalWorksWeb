using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RentalWorksAPI.Models
{
    public class EdecsMessage
    {                       
        public string tpy { get;set; }
        public int rdr { get;set; }
        public int ant { get;set; }
        public string epc { get;set; }
        public string ts { get;set; }
        public int rss { get;set; }
        public int phs { get;set; }
        public int dop { get;set; }
        public int pc { get;set; }
        public int ch { get;set; }
        public int dir { get;set; }
        public int cnf { get;set; }
        public string lnm { get;set; }
        public string usr { get;set; }
        public string pwd { get;set; }
    }

    public class EdecsSubscriberNotification
    {
        public const string TYPE_COUNT = "count";
        public const string TYPE_BATCH = "batch";

        public string type { get;set; }
        public int count { get;set; }
        public List<EdecsMessage> epcs { get;set; }

        public string ToJsonString()
        {
            string jsonmessage = JsonConvert.SerializeObject(this);
            return jsonmessage;
        }
    }
}
