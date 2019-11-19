using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.OrderStatusDetail
{
    [FwLogic(Id:"nROby0s3w029")]
    public class OrderStatusDetailLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderStatusDetailLoader orderStatusDetailLoader = new OrderStatusDetailLoader();
        public OrderStatusDetailLogic()
        {
            dataLoader = orderStatusDetailLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"YXomqqULxDB4")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"CjyWWbz29n8V")]
        public string MasterItemId { get; set; }

        [FwLogicProperty(Id:"nErEEiMQrEvD")]
        public string PrimaryMasteritemId { get; set; }

        [FwLogicProperty(Id:"yeKKVfjix2tc")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"qFbkOJQEWJBF")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"hfBQntdnxfis")]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"fKzAT1WiK4oj")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"NLrXNVtr2Vlm")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"oCFkNjVtvq3C")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"ICIQLmFcjymk")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"pgCxkA39Aqvg")]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id:"MmrDlbjHgyFi")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"Ypolg8qcHGHx")]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id:"fShrbEspKER7")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"skuRGF86XWaj")]
        public string OutWarehouseId { get; set; }

        [FwLogicProperty(Id:"V9OkMFZZ0h80")]
        public string InWarehouseId { get; set; }

        [FwLogicProperty(Id:"taTHsQiWsdTJ")]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"82eQpf0nfpEe")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"zi09OfCjSfU1")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"h1dCJ4H08Xx1")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"2WvPkQYVNlmN")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"8wWvkVGnh3ZA")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"qmNSAb09p4vz")]
        public bool? Bold { get; set; }

        [FwLogicProperty(Id:"knibcKzg7eAj")]
        public string OutWarehouseCode { get; set; }

        [FwLogicProperty(Id:"QHvc84SzulAv")]
        public string OutWarehouse { get; set; }

        [FwLogicProperty(Id:"9uHF4iDBExwU")]
        public string InWarehouseCode { get; set; }

        [FwLogicProperty(Id:"8jDwVzROyzy7")]
        public string InWarehouse { get; set; }

        [FwLogicProperty(Id:"N6OypawOJNRq")]
        public string OutContractId { get; set; }

        [FwLogicProperty(Id:"Lyf9TrQYwS82")]
        public string OutContractNumber { get; set; }

        [FwLogicProperty(Id:"G4oUR4AJlFJY")]
        public bool? IsSuspendOut { get; set; }

        [FwLogicProperty(Id:"5UtC9wuJpBTx")]
        public string OutDateTime { get; set; }

        [FwLogicProperty(Id:"L6rqXkB5Gd2J")]
        public string OutDateTimeColor { get; set; }

        [FwLogicProperty(Id:"ke1di3Zzviwc")]
        public bool? IsExchangeOut { get; set; }

        [FwLogicProperty(Id:"lwQzedesiR3o")]
        public bool? IsTruckOut { get; set; }

        [FwLogicProperty(Id:"DsMyCiKwhKBn")]
        public string InContractId { get; set; }

        [FwLogicProperty(Id:"MqurFT8k9JPR")]
        public string InContractNumber { get; set; }

        [FwLogicProperty(Id:"BSKj1L2PrP9T")]
        public bool? IsSuspendIn { get; set; }

        [FwLogicProperty(Id:"Boy7H6uN9Mp5")]
        public string InDateTime { get; set; }

        [FwLogicProperty(Id:"ITYPzexjBH9J")]
        public string InDateTimeColor { get; set; }

        [FwLogicProperty(Id:"LbwAuLlUYejF")]
        public bool? IsExchangeIn { get; set; }

        [FwLogicProperty(Id:"tZ0DzyY9K388")]
        public bool? IsTruckIn { get; set; }

        [FwLogicProperty(Id:"xiQnwnVenN3n")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"emrMZcqFaBfB")]
        public string QuantityColor { get; set; }

        [FwLogicProperty(Id:"D6QvBJ5qUqgE")]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"iPH0iCJsHMoP")]
        public string BarCodeSerialRfid { get; set; }

        [FwLogicProperty(Id:"emqHza4qRieS")]
        public string BarCodeSerialRfidColor { get; set; }

        [FwLogicProperty(Id:"3hUtD6uHBmsC")]
        public string ManufacturerPartNumber { get; set; }

        [FwLogicProperty(Id:"YRQBrd4ho1t9")]
        public string ManufacturerPartNumberColor { get; set; }

        [FwLogicProperty(Id:"SBAhmUNQqJUK")]
        public int? ExchangeOrderTranId { get; set; }

        [FwLogicProperty(Id:"IBu0mx1tAr2N")]
        public string ExchangeInternalChar { get; set; }

        [FwLogicProperty(Id:"y9r2f9wcNGMI")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"4nid5b7DTB4K")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"heyF59HNsfOj")]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"q8eBU3iCR7aM")]
        public bool? Consignment { get; set; }

        [FwLogicProperty(Id:"ly4YLIs09jhq")]
        public string ItemStatus { get; set; }

        [FwLogicProperty(Id:"sN021tAzNK3O")]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"nLTMzZtWWLIE")]
        public string DepartmentId { get; set; }

        //[FwLogicProperty(Id:"dXZEahjDnsZu")]
        //public bool? Chkininclude { get; set; }

        [FwLogicProperty(Id:"6WcoP3PiG39Z")]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"wXmAXu6xyfyr")]
        public string OrderType { get; set; }

        //[FwLogicProperty(Id:"iG5q9igNfz8G")]
        //public string PoOrderId { get; set; }

        [FwLogicProperty(Id:"ZvxX8dJFd6pg")]
        public string NestedMasteritemId { get; set; }

        [FwLogicProperty(Id:"yXBUsr1LyNf8")]
        public bool? IsContainer { get; set; }

        [FwLogicProperty(Id:"ar1hQglCrTLG")]
        public bool? QcRequired { get; set; }

        [FwLogicProperty(Id:"WWGzh431Laep")]
        public string Location { get; set; }

        [FwLogicProperty(Id:"FwBxekyI8Pcz")]
        public int? OrderOrderby { get; set; }

    }
}
