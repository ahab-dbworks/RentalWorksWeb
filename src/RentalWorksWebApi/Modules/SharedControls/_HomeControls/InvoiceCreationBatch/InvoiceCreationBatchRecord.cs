using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.InvoiceCreationBatch
{
    [FwSqlTable("invoicebatch")]
    public class InvoiceCreationBatchRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InvoiceCreationBatchId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? BatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string BatchType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
