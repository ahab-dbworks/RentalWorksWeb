using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Modules.Home.MasterWarehouse;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Home.InventoryWarehouse
{
    public class InventoryWarehouseLoader : MasterWarehouseLoader 
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(availfor in ('R', 'S', 'P', 'V'))");
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}