using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.HomeControls.Inventory;
using WebLibrary;

namespace WebApi.Modules.Inventory.RentalInventory
{
    [FwSqlTable("inventoryview")]
    public class RentalInventoryLoader : InventoryLoader
    {
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            select.AddWhere("(availfor='" + RwConstants.INVENTORY_AVAILABLE_FOR_RENT + "')");
        }
        //------------------------------------------------------------------------------------
    }
}