using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Home.Inventory;
using WebLibrary;

namespace WebApi.Modules.Inventory.SalesInventory
{
    [FwSqlTable("inventoryview")]
    public class SalesInventoryLoader : InventoryLoader
    {
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            select.AddWhere("(availfor='" + RwConstants.INVENTORY_AVAILABLE_FOR_SALE + "')");
        }
        //------------------------------------------------------------------------------------
    }
}