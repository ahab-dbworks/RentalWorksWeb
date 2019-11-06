using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Reports.Shared.EmailHistory
{
    [FwSqlTable("emailreportwebview")]
    public class EmailHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailreportid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string EmailHistoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportdefid", modeltype: FwDataTypes.Text)]
        public string ReportId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromusersid", modeltype: FwDataTypes.Text)]
        public string FromUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromwebusersid", modeltype: FwDataTypes.Text)]
        public string FromWebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromuser", modeltype: FwDataTypes.Text)]
        public string FromUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "createdate", modeltype: FwDataTypes.Date)]
        public string EmailDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtext", modeltype: FwDataTypes.Text)]
        public string EmailText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailto", modeltype: FwDataTypes.Text)]
        public string EmailTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subject", modeltype: FwDataTypes.Text)]
        public string EmailSubject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailcc", modeltype: FwDataTypes.Text)]
        public string EmailCC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "title", modeltype: FwDataTypes.Text)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid", modeltype: FwDataTypes.Text)]
        public string RelatedToId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pdfattachment", modeltype: FwDataTypes.Unknown___varbinary)]
        //public Unknown___varbinary PdfAttachment { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("FromUserId", "fromusersid", select, request);
            addFilterToSelect("RelatedToId", "uniqueid", select, request);

            AddActiveViewFieldToSelect("FromUserId", "fromwebusersid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}
