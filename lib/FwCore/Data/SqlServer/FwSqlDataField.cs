using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Data.SqlServer
{
    public class FwSqlDataField
    {
        public FwSqlDataField(string caption, string apiname, bool publishinapi)
        {
            this.Caption = caption;
            this.ApiName = apiname;
            this.PublishInApi = publishinapi;
        }
        public string Caption { get; set; }
        public string ApiName { get; set; }
        public bool PublishInApi { get; set; }
    }
}
