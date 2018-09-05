using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.RentalInventoryCatalogReport
{
    public class RentalInventoryCatalogReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RentalInventoryCatalogReportLoader rentalInventoryCatalogReportLoader = new RentalInventoryCatalogReportLoader();
        public RentalInventoryCatalogReportLogic()
        {
            dataLoader = rentalInventoryCatalogReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Classification { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ClassificationDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PackageId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DailyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReplacementCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Price { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Retail { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Rank { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CategoryOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ShippingWeightPounds { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ShippingWeightOunces { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PackagePrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubCategory { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubCategoryOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InventoryTypeOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubCategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsFixedAsset { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOwned { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WeightUS { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WeightMetric { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InventoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
