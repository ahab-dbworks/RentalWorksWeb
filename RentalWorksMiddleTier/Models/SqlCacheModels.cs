using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace RentalWorksMiddleTier.Models
{
    public class SqlCacheModels
    {
        public class GetDataRequest
        {
            public string table { get;set; } = string.Empty;
            public List<string> uniqueids { get; set; } = new List<string>();
            public List<string> uniqueidvalues { get; set; } = new List<string>();
            public List<string> columns { get; set; } = new List<string>();
        }

        public class GetDataResponse
        {
            public List<Dictionary<string, string>> rows { get; set; } = new List<Dictionary<string, string>>();
            public string msgid { get; set; } = string.Empty;
            public string msg { get; set; } = string.Empty;

            public GetDataResponse() : base()
            {

            }

            public GetDataResponse(string msgid, string msg) : this()
            {
                this.msgid = msgid;
                this.msg   = msg;
            } 
        }

        public class GetCountRequest
        {
            public string table { get;set; } = string.Empty;
            public bool includeinactive { get;set; } = false;
        }

        public class GetCountResponse
        {
            public int count { get;set; } = 0;
            public string msgid { get; set; } = string.Empty;
            public string msg { get; set; } = string.Empty;

            public GetCountResponse() : base()
            {

            }

            public GetCountResponse(string msgid, string msg) : this()
            {
                this.msgid = msgid;
                this.msg   = msg;
            } 
        }

        public class RefreshRequest
        {
            public string table { get; set; } = String.Empty;
        }
    }
}