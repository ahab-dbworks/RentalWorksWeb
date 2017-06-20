using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Data.SqlServer
{
    public class FwSqlBool : FwSqlDataField
    {
        public FwSqlBool(string caption, string apiname, bool publishinapi) : base(caption, apiname, publishinapi)
        {

        }
        public bool Value { get; set; }
        public bool OldValue { get; set; }
    }
}
