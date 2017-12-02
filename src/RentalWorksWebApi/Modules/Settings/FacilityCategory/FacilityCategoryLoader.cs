﻿using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksWebApi.Modules.Settings.InventoryCategory;

namespace RentalWorksWebApi.Modules.Settings.FacilityCategory
{
    public class FacilityCategoryLoader: InventoryCategoryLoader
    {
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(rectype='SP')");
        }
        //------------------------------------------------------------------------------------
    }
}
