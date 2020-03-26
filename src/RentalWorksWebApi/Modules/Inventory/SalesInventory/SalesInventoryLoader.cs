using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;

namespace WebApi.Modules.Inventory.SalesInventory
{
    [FwSqlTable("inventoryview")]
    public class SalesInventoryLoader : InventoryLoader
    {

        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 

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