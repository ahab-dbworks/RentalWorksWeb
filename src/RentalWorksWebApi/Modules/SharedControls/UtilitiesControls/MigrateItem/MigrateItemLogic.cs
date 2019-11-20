using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.MigrateItem
{
    [FwLogic(Id:"JRRae5RkdESl")]
    public class MigrateItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MigrateItemLoader checkedOutItemLoader = new MigrateItemLoader();
        public MigrateItemLogic()
        {
            dataLoader = checkedOutItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"vhoe4FemCr6Q", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"lm53r0vlxeBd")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"wFhDwV7UH3HO", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"ZbPDiS892ED8", IsReadOnly:true)]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"ZbPDiS892ED8", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"ZbPDiS892ED8", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"zKdDcBIE9f9T", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"o5nY6TLqpcT6", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"o5nY6TLqpcT6", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"7EhDp9qZv2Uw", IsReadOnly:true)]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"kT5loDjrl9iX", IsReadOnly:true)]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"kT5loDjrl9iX", IsReadOnly:true)]
        public decimal? QuantityOut { get; set; }

        [FwLogicProperty(Id:"WkswQpSxO74W", IsReadOnly:true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"WkswQpSxO74W", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"WkswQpSxO74W", IsReadOnly:true)]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"ecsSflUhvafX", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"AN7a7BjNJ1CJ", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"AN7a7BjNJ1CJ", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"AN7a7BjNJ1CJ", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"N8li3jpRoegJ", IsReadOnly:true)]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"N8li3jpRoegJ", IsReadOnly:true)]
        public string PrimaryOrderItemId { get; set; }

        [FwLogicProperty(Id:"V6c0un8zx7bC", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"DYopKE9vM8Ea", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"Ev3j7sqr8BfU", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"bkXa1aShmiob", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"DVjCL4JDOMXD", IsReadOnly:true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"vmyACCJNxkmE", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"vmyACCJNxkmE", IsReadOnly:true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"vmyACCJNxkmE", IsReadOnly:true)]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id:"EEdKo2YBqwaM", IsReadOnly:true)]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"VxUgP4QgCo5p", IsReadOnly:true)]
        public string StagedByUserId { get; set; }

        [FwLogicProperty(Id:"VxUgP4QgCo5p", IsReadOnly:true)]
        public string StagedByUser { get; set; }

        [FwLogicProperty(Id:"h4rF3zumdmAl", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"pqBm5Q1AQiT8", IsReadOnly:true)]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"pjjef9viPrnH", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"Me5zBomuBPI2", IsReadOnly:true)]
        public string ContainerItemId { get; set; }

        [FwLogicProperty(Id:"wFhDwV7UH3HO", IsReadOnly:true)]
        public string ContainerBarCode { get; set; }

        [FwLogicProperty(Id:"pv80c66nZO6M", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"io6A9t8I3v9w", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

    }
}
