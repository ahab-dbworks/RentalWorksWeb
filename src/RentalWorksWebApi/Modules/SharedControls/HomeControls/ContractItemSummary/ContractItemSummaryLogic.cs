using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.ContractItemSummary
{
    [FwLogic(Id:"PXI33gcozMwX")]
    public class ContractItemSummaryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractItemSummaryLoader contractItemSummaryLoader = new ContractItemSummaryLoader();
        public ContractItemSummaryLogic()
        {
            dataLoader = contractItemSummaryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"EOOdMl9wmtyx")]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"6bezKtqihXek")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"Shfnsp0N9ilD")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"Ei63kY5akzOT")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"1qdcpmO0vAuQ")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"9wpe0L3Uck6d")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"1GHEYHAahMvY")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"1NKVOJ4D126e")]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"xIsrXknoAJMm")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"nYBVeX3mZpsu")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"qWaLP9edo9FG")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"9ni72CkV9ity")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"HX9xe2Yj2hmL")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"rmGAQsL6GWxe")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"lu7cmDTksRVs")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"k8C6DfIoLH19")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"KYYaYGufqB5u")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"yIisrJdJRQwH")]
        public string PrimaryOrderItemId { get; set; }

        [FwLogicProperty(Id:"3QJfTJpLOHeu")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"C75zJmXcIKr3")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"LZsZ2HwjpSEu")]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"pUGDlpYv9FXI")]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"3YXw8DQ8y0u5")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"dwgvNdF9ivxN")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"vE2HUGi9myV3")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"JnN0Ajc14MkX")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"gAEDf284ygRQ")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"dPoIhnenENTc")]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"iCSnnLib6N7y")]
        public string NestedOrderItemId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
