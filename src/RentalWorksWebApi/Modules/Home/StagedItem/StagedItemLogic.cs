using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.StagedItem
{
    [FwLogic(Id:"smLpcnXJTXfAd")]
    public class StagedItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StagedItemLoader stagedItemLoader = new StagedItemLoader();
        public StagedItemLogic()
        {
            dataLoader = stagedItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"MmInhgIvscUD")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"T6hU6ffMgMZL")]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"qVqvVI0Dd0WD")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"Z6nqVSeIbE8E")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"tEuhTBajwhQ2")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"yWTsj9Z1Qtye")]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"lA1CQscWO0uj")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"s9yUkiWs0xDz")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"FKbpNZyAf2b4")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"N1JnLDrKFF4G")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"GgSmHlj6MnxQ")]
        public string StagedDateTime { get; set; }

        [FwLogicProperty(Id:"6nzkWTsSaB5B")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"yrIuHzfr5M3h")]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"t0HGJdjpABMO")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"TJa515EdThvA")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"XW2o8qjrKRWR")]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id:"wYrm1u6gd3RX")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"1NH9VVV8nXu8")]
        public bool? QuotePrint { get; set; }

        [FwLogicProperty(Id:"fhxyiHR6WcRo")]
        public bool? OrderPrint { get; set; }

        [FwLogicProperty(Id:"JKQGi1AJwBri")]
        public bool? PickListPrint { get; set; }

        [FwLogicProperty(Id:"fr9hCMGrTNOi")]
        public bool? ContractOutPrint { get; set; }

        [FwLogicProperty(Id:"RiQuxF4WAJws")]
        public bool? ReturnListPrint { get; set; }

        [FwLogicProperty(Id:"lkjw5Zybn1ou")]
        public bool? InvoicePrint { get; set; }

        [FwLogicProperty(Id:"cgRXRcIb3ODl")]
        public bool? ContractInPrint { get; set; }

        [FwLogicProperty(Id:"y19KoRUnl464")]
        public bool? PurchaseOrderPrint { get; set; }

        [FwLogicProperty(Id:"TYIzyoU7Hzq8")]
        public bool? ContractReceivePrint { get; set; }

        [FwLogicProperty(Id:"QIDcybb8ubvp")]
        public bool? ContractReturnPrint { get; set; }

        [FwLogicProperty(Id:"hvqmK25h5yMB")]
        public bool? PurchaseOrderReceivelistPrint { get; set; }

        [FwLogicProperty(Id:"BeQjzU8uxOqr")]
        public bool? PurchaseOrderReturnlistPrint { get; set; }

        [FwLogicProperty(Id:"gYeepmckwOtq")]
        public string StagedUsersId { get; set; }

        [FwLogicProperty(Id:"6znOshReUrbl")]
        public string StagedUser { get; set; }

        [FwLogicProperty(Id:"iX08I4AhJn6S")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"7zB0eOXWRGyl")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"I4QqMwanMy8k")]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"ejnaaqtId2on")]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"ui0xA5bW5lVp")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"GmvDUMQeWA8W")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"jdaBS1qiIox2")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"C6OrBBPNLMXL")]
        public bool? Bold { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
