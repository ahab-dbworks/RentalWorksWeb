using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.ExchangeItem
{
    [FwLogic(Id:"qSc0F5FySu4")]
    public class ExchangeItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ExchangeItemLoader exchangeItemLoader = new ExchangeItemLoader();
        public ExchangeItemLogic()
        {
            dataLoader = exchangeItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"R855ZJVuDsrH")]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"wGyNV3w9hS37")]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"145WLEyH6mjr")]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id:"OIvGXN9bF2Eg")]
        public string LocationId { get; set; }

        [FwLogicProperty(Id:"yocbbGkfkw8v")]
        public string Direction { get; set; }

        [FwLogicProperty(Id:"13rvowErVRDi")]
        public string DirectionDisplay { get; set; }

        [FwLogicProperty(Id:"6Uu73o1QZn2H")]
        public int? OrderTranId { get; set; }

        [FwLogicProperty(Id:"lwNIYD6LxwJH")]
        public bool? InternalChar { get; set; }

        [FwLogicProperty(Id:"LxlXxuHRtyfY")]
        public bool? ItemStatus { get; set; }

        [FwLogicProperty(Id:"rF8rBuPrvIbZ")]
        public string OutContractId { get; set; }

        [FwLogicProperty(Id:"x2hYDvVUhcHV")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"uAaSrBqePsb2")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"HtU50MblOESO")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"kejxj46peCnj")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"KVJBlQjjNGQj")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"j4AvYoxvaoap")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"HyXF12vtuYhx")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"a3IRDIgqVfP3")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"805AJwR3xawy")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"RRUAmkRy0WXr")]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"GCKAjlVizcI6")]
        public int? Quantity { get; set; }

        [FwLogicProperty(Id:"Dd8HqELAsm2f")]
        public int? ContractQuantity { get; set; }

        [FwLogicProperty(Id:"QxgYPAlj0JQc")]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"XjXlZRpSVIdQ")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"Su0kM2W1MnMz")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"ZNGJX4sxjJtG")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"jxPs9XOoh2Lg")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"tAgCQxTdVRmj")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"Pm85apP7mpfY")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"JEamCn3lsCFk")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"GCJLSZagewzg")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"pnHKaN6LtjX7")]
        public string ScannedByUserId { get; set; }

        [FwLogicProperty(Id:"yu1CjghIXsdq")]
        public string ScannedByUserName { get; set; }

        [FwLogicProperty(Id:"kSOEJLUJ6y00")]
        public string ScannedDate { get; set; }

        [FwLogicProperty(Id:"gkTOPUH7pCUs")]
        public bool? ToRepair { get; set; }

        [FwLogicProperty(Id:"tU98EZmD6THs")]
        public bool? IsMultiWarehouse { get; set; }

        [FwLogicProperty(Id:"q1YA9OHo2IS9")]
        public decimal? OrderBy { get; set; }

        [FwLogicProperty(Id:"AKfiWldD2LeC")]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"l48SLISYKZZ7")]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"GP92XSP6xZS1")]
        public string ConsignorAgreementId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
