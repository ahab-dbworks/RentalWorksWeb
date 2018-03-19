using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.Home.MasterWarehouse;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.InventoryWarehouse
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
            select.AddWhere("(availfor in ('" + RwConstants.INVENTORY_AVAILABLE_FOR_RENT + "', '" + RwConstants.INVENTORY_AVAILABLE_FOR_SALE + "', '" + RwConstants.INVENTORY_AVAILABLE_FOR_PARTS + "', '" + RwConstants.INVENTORY_AVAILABLE_FOR_VEHICLE + "'))");
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}