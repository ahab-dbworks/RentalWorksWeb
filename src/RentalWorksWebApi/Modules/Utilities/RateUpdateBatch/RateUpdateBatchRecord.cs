using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Utilities.RateUpdateBatch
{
    [FwSqlTable("rateupdatebatch")]
    public class RateUpdateBatchRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateupdatebatchid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true)]
        public int? RateUpdateBatchId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateupdatebatch", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string RateUpdateBatch { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public DateTime? Applied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
