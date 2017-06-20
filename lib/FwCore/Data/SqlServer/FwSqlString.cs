using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Data.SqlServer
{
    public class FwSqlString : FwSqlDataField
    {
        public FwSqlString(string caption, string apiname, bool publishinapi) : base(caption, apiname, publishinapi)
        {

        }

        public string Value { get; set; }
        public string OldValue { get; set; }
    }
}
