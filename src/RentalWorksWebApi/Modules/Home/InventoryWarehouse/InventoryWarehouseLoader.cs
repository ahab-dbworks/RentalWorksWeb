using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Modules.Home.MasterWarehouse;
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
            select.AddWhere("warehouseinactive <> 'T'"); //justin 04/01/2019 #359  this is still just a work-around.  Need to be able to add filters to grids to let user "View All, Active, or Inactive"

            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}