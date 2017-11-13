using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Modules.Home.MasterWarehouse;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Settings.VehicleTypeWarehouse
{
    public class VehicleTypeWarehouseLoader : MasterWarehouseLoader 
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string VehicleTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(availfor='V')");
            addFilterToSelect("VehicleTypeId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}