using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.AssignedCustomForm
{
    [FwSqlTable("dbo.funcassignedwebform(@webusersid)")]
    public class AssignedCustomFormLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webformid", modeltype: FwDataTypes.Text)]
        public string CustomFormId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "baseform", modeltype: FwDataTypes.Text)]
        public string BaseForm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "html", modeltype: FwDataTypes.Text)]
        public string Html { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string webUserId = GetUniqueIdAsString("WebUserId", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            select.AddParameter("@webusersid", webUserId); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
