using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace FwStandard.Modules.Administrator.WebAlertLog
{
    [FwSqlTable("webalertlog")]
    public class WebAlertLogLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webalertlogid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? WebAlertLogId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "createdatetime", modeltype: FwDataTypes.DateTime)]
        public DateTime? CreateDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertsubject", modeltype: FwDataTypes.Text)]
        public string AlertSubject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertbody", modeltype: FwDataTypes.Text)]
        public string AlertBody { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertfrom", modeltype: FwDataTypes.Text)]
        public string AlertFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertto", modeltype: FwDataTypes.Text)]
        public string AlertTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errormessage", modeltype: FwDataTypes.Text)]
        public string ErrorMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("AlertId", "alertid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
