using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Settings.Category;
using WebApi;

namespace WebApi.Modules.Settings.InventorySettings.SalesCategory
{
    public class SalesCategoryLoader: CategoryLoader
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(rectype='" + RwConstants.INVENTORY_AVAILABLE_FOR_SALE + "')");
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}
