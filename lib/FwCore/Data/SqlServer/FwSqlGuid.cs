using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Data.SqlServer
{
    public class FwSqlGuid : FwSqlDataField
    {
        public FwSqlGuid(string caption, string apiname, bool publishinapi) : base(caption, apiname, publishinapi)
        {

        }

        public DateTime Value { get; set; }
        public DateTime OldValue { get; set; }
    }
}
