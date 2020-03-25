using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.DataHealth
{
    [FwSqlTable("datahealthview")]
    public class DataHealthLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datahealthid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DataHealthId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datahealthtype", modeltype: FwDataTypes.Text)]
        public string DataHealthType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "capturedatetime", modeltype: FwDataTypes.DateTime)]
        public string CaptureDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "resolved", modeltype: FwDataTypes.Boolean)]
        public bool? Resolved { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("BaseForm", "baseform", select, request);
            //addFilterToSelect("WebUserId", "webusersid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
