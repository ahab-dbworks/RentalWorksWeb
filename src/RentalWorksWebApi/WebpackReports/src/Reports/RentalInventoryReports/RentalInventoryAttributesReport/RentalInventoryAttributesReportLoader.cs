using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using FwStandard.SqlServer.Attributes;
using FwStandard.SqlServer;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryAttributesReport
{
    public class RentalInventoryAttributesReportLoader : InventoryAttributesReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryAttributesReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DefaultDepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevaluepct", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSalvageValuePercent { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
