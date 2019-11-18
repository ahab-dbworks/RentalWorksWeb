using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckedOutItem
{
    [FwLogic(Id:"vUvDyYlwWpaz")]
    public class CheckedOutItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckedOutItemLoader checkedOutItemLoader = new CheckedOutItemLoader();
        public CheckedOutItemLogic()
        {
            dataLoader = checkedOutItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"QaxJKofXMpRt", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"EnLF1lB1wUbi", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"wg0ROIMz8Sdu", IsReadOnly:true)]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"wg0ROIMz8Sdu", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"wg0ROIMz8Sdu", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"49OFrW4c2ZyF", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"FruXVQ6TNrAQ", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"FruXVQ6TNrAQ", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"Fmab9J4Tz04s", IsReadOnly:true)]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"g11ZwwVtNxtX", IsReadOnly:true)]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"pDot8uMwyJJm", IsReadOnly:true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"pDot8uMwyJJm", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"pDot8uMwyJJm", IsReadOnly:true)]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"ZDYuBQAfiGpM", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"QeJCGHu5B7wJ", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"QeJCGHu5B7wJ", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"QeJCGHu5B7wJ", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"apDfTzDFpDV4", IsReadOnly:true)]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"apDfTzDFpDV4", IsReadOnly:true)]
        public string PrimaryOrderItemId { get; set; }

        [FwLogicProperty(Id:"lzzU0wdWaNRE", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"FylOY6Ll5Hxm", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"kHpTN428cbYX", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"d9IQbDC25NQr", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"aoVOdgiPrEI2", IsReadOnly:true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"6L0SZrtTeZ0b", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"6L0SZrtTeZ0b", IsReadOnly:true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"6L0SZrtTeZ0b", IsReadOnly:true)]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id:"Uzlq6GJk9rSk", IsReadOnly:true)]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"ufycjTA4CiIA", IsReadOnly:true)]
        public string StagedByUserId { get; set; }

        [FwLogicProperty(Id:"ufycjTA4CiIA", IsReadOnly:true)]
        public string StagedByUser { get; set; }

        [FwLogicProperty(Id:"AOhrak8e7fpO", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"b7ndP9eqCzoP", IsReadOnly:true)]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"GRvH2VjtGw6h", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"UMdGRJZ4UnNs", IsReadOnly:true)]
        public string ContainerItemId { get; set; }

        [FwLogicProperty(Id:"EnLF1lB1wUbi", IsReadOnly:true)]
        public string ContainerBarCode { get; set; }

        [FwLogicProperty(Id:"VhrDreFTAQw1", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"KGFHGqvT3PFx", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

    }
}
