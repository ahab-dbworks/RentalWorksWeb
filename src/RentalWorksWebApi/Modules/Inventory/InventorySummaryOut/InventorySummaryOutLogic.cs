using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventorySummaryOut
{
    [FwLogic(Id: "189C7xI3aJOGw")]
    public class InventorySummaryOutLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventorySummaryOutLoader inventorySummaryOutLoader = new InventorySummaryOutLoader();
        public InventorySummaryOutLogic()
        {
            dataLoader = inventorySummaryOutLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1BOBPUcsHjGv1", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "1KwCtgdm1IGFN", IsReadOnly: true)]
        public string ItemDescription { get; set; }
        [FwLogicProperty(Id: "1oEuEYUmB6gSD", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "1PQLl6PrbYlJ4", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "1s45RUbI77PHM", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "1UoV2rzyaka8T", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "1wBt2YoKwdPcj", IsReadOnly: true)]
        public string Orderdate { get; set; }
        [FwLogicProperty(Id: "26kEanXTNMVYI", IsReadOnly: true)]
        public string Ordertype { get; set; }
        [FwLogicProperty(Id: "2BY5FZ4jecZRL", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "2iTZM7TJQvso9", IsReadOnly: true)]
        public string InventoryStatus { get; set; }
        [FwLogicProperty(Id: "2ssXNMsgErnXN", IsReadOnly: true)]
        public string InventoryStatusType { get; set; }
        [FwLogicProperty(Id: "2Z2B6xRU74nwJ", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "35RRfzdmpT2oo", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "3BavkySA0bXum", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwLogicProperty(Id: "3bOVmruHWsBvd", IsReadOnly: true)]
        public string EstimatedStartDate { get; set; }
        [FwLogicProperty(Id: "3dj43R4J1kyDp", IsReadOnly: true)]
        public string EstimatedEndDate { get; set; }
        [FwLogicProperty(Id: "3h3mJNGs3qksP", IsReadOnly: true)]
        public string PoId { get; set; }
        [FwLogicProperty(Id: "3LWp318qpbTAH", IsReadOnly: true)]
        public string Pono { get; set; }
        [FwLogicProperty(Id: "3Vj4HjrYL67fU", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "3zENtyX6nKdNl", IsReadOnly: true)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
