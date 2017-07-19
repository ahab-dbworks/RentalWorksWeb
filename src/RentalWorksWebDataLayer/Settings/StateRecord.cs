using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("state")]
    public class StateRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "stateid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string StateId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "state", dataType: FwDataTypes.Text, length: 12)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "statecode", dataType: FwDataTypes.Text, length: 1)]
        public string StateCode { get; set; }
        //------------------------------------------------------------------------------------
        
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        //public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
