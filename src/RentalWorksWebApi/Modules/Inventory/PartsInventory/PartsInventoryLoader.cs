using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.HomeControls.Inventory;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Inventory.PartsInventory
{
    [FwSqlTable("inventoryview")]
    public class PartsInventoryLoader : InventoryLoader
    {
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            select.AddWhere("(availfor='" + RwConstants.INVENTORY_AVAILABLE_FOR_PARTS + "')");
        }
        //------------------------------------------------------------------------------------
    }
}