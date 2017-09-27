using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Modules.Home.Inventory;
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.SalesInventory
{
    public class SalesInventoryLoader : InventoryLoader 
    {
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(availfor='S')");
        }
        //------------------------------------------------------------------------------------
    }
}