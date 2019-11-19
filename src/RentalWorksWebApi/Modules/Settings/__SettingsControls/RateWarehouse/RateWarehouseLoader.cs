using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.HomeControls.MasterWarehouse;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.RateWarehouse
{
    public class RateWarehouseLoader : MasterWarehouseLoader 
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(availfor in ('M', 'L', 'SP'))");
            select.AddWhere("warehouseinactive <> 'T'");
            addFilterToSelect("RateId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}