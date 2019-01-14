using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Reports.ReturnOnAssetPeriod
{
    [FwSqlTable("dbo.dwreturnonassetperiod()")]
    public class ReturnOnAssetPeriodLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Period", modeltype: FwDataTypes.Text)]
        public string Period { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Label", modeltype: FwDataTypes.Text)]
        public string Label { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "OrderNbr", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        public override FwSqlConnection GetDatabaseConnection()
        {
            return new FwSqlConnection(AppConfig.DataWarehouseDatabaseSettings.ConnectionString);
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
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
