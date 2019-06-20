using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Settings.AvailabilityKeepFreshLog
{
    [FwSqlTable("availkeepfreshlog")]
    public class AvailabilityKeepFreshLogRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", isPrimaryKey: true, identity: true, modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchsize", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? BatchSize { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "startdatetime", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public DateTime? StartDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enddatetime", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public DateTime? EndDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
