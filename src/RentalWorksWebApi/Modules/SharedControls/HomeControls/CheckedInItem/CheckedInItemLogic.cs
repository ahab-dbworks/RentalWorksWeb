using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.CheckedInItem
{
    [FwLogic(Id:"utdKVDQt0zc")]
    public class CheckedInItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckedInItemLoader checkedInItemLoader = new CheckedInItemLoader();
        public CheckedInItemLogic()
        {
            dataLoader = checkedInItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"eRYUffFWHxQ")]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"PWqggU3Rbjl")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"4wPO9dwbEnU")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"KhP6xwvhN4j")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"froRlPTQWUM")]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"bRYdlIXUFQ3")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"P6PKwWKWKzz")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"IXLBIclLDy3")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"IxaMyyleVWH")]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"ogZlPC4DiTo")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"DdvUbv3Mvxe")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"2cIdxWQyqKW")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"sK8PDRbCbx7")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"T9h2RM4FLlR")]
        public string CheckedInDateTime { get; set; }

        [FwLogicProperty(Id:"YRHQZaDKLSw")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"6rKBa3SeXD4")]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"GW5FhErai4x")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"fisetxLVcvz")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"pud6NP6IqWk")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"im2ibQS3prs")]
        public bool? QuotePrint { get; set; }

        [FwLogicProperty(Id:"S0cN8uCvpmK")]
        public bool? OrderPrint { get; set; }

        [FwLogicProperty(Id:"sBJbbSzbLCu")]
        public bool? PickListPrint { get; set; }

        [FwLogicProperty(Id:"QQDnIJC1fyx")]
        public bool? ContractOutPrint { get; set; }

        [FwLogicProperty(Id:"gmqSpzf2Kfo")]
        public bool? ReturnListPrint { get; set; }

        [FwLogicProperty(Id:"3GL6cfDRBIx")]
        public bool? InvoicePrint { get; set; }

        [FwLogicProperty(Id:"pLnJVHZqp1u")]
        public bool? ContractInPrint { get; set; }

        [FwLogicProperty(Id:"lJuGLy9t6r4")]
        public bool? PurchaseOrderPrint { get; set; }

        [FwLogicProperty(Id:"uGpbZrcJCv7")]
        public bool? ContractReceivePrint { get; set; }

        [FwLogicProperty(Id:"ubXqii09h7v")]
        public bool? ContractReturnPrint { get; set; }

        [FwLogicProperty(Id:"br1yjY4oOO6")]
        public bool? PurchaseOrderReceivelistPrint { get; set; }

        [FwLogicProperty(Id:"1xcMLQo6ko6")]
        public bool? PurchaseOrderReturnlistPrint { get; set; }

        [FwLogicProperty(Id:"sELQIkBa727")]
        public string CheckedInUsersId { get; set; }

        [FwLogicProperty(Id:"Ed3EOzVWbxs")]
        public string CheckedInUser { get; set; }

        [FwLogicProperty(Id:"DoFfd3irzwQ")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"xSdzUerr0Te")]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"7ta2gJuAtUp")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"IRtuINfEPwi")]
        public string BarCodeColor { get; set; }

        [FwLogicProperty(Id:"jWPJbnRVPtl")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"tKDiIWnToNW")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"dyNKI7JwWNR")]
        public bool? Bold { get; set; }

        [FwLogicProperty(Id:"8h9jZh6gCC2")]
        public bool? IsSwap { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
