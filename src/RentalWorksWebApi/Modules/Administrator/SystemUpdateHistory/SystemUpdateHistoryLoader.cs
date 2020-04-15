using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Administrator.SystemUpdateHistory
{
    [FwSqlTable("systemupdatehistoryview")]
    public class SystemUpdateHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistoryid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? SystemUpdateHistoryId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "updatedatetime", modeltype: FwDataTypes.DateTime)]
        public DateTime UpdateDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromversion", modeltype: FwDataTypes.Text)]
        public string FromVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toversion", modeltype: FwDataTypes.Text)]
        public string ToVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errormsg", modeltype: FwDataTypes.Text)]
        public string ErrorMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
