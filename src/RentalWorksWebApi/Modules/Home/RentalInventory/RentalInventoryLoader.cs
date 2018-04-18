using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.RentalInventory
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