using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Reports.Shared.EmailHistory
{
    [FwSqlTable("emailreport")]
    public class EmailHistoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailreportid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string EmailHistoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportdefid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReportId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FromUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "createdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string EmailDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtext", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string EmailText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailto", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string EmailTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string EmailSubject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailcc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string EmailCC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "title", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string RelatedToId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pdfattachment", modeltype: FwDataTypes.Unknown___varbinary, sqltype: "varbinary")]
        //public Unknown___varbinary PdfAttachment { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
