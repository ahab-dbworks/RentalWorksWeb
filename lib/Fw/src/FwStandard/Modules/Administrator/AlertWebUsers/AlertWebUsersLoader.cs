using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace FwStandard.Modules.Administrator.AlertWebUsers
{
    [FwSqlTable("webalertwebusersview")]
    public class AlertWebUsersLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webalertwebusersid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string AlertWebUserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
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
            addFilterToSelect("AlertId", "alertid", select, request);
            addFilterToSelect("WebUserId", "webusersid", select, request);
            addFilterToSelect("UserId", "usersid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
