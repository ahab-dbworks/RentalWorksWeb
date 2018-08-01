using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.PurchaseOrderReceiveItem
{
    public class PurchaseOrderReceiveItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        //PurchaseOrderReceiveItemRecord purchaseOrderReceiveItem = new PurchaseOrderReceiveItemRecord();
        PurchaseOrderReceiveItemLoader purchaseOrderReceiveItemLoader = new PurchaseOrderReceiveItemLoader();
        public PurchaseOrderReceiveItemLogic()
        {
            //dataRecords.Add(purchaseOrderReceiveItem);
            dataLoader = purchaseOrderReceiveItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ContractId { get; set; }//{ get { return purchaseOrderReceiveItem.ContractId; } set { purchaseOrderReceiveItem.ContractId = value; } }
        public string PurchaseOrderId { get; set; }//{ get { return purchaseOrderReceiveItem.PurchaseOrderId; } set { purchaseOrderReceiveItem.PurchaseOrderId = value; } }
        public string PurchaseOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.PurchaseOrderItemId; } set { purchaseOrderReceiveItem.PurchaseOrderItemId = value; } }
        public string LineType { get; set; }//{ get { return purchaseOrderReceiveItem.LineType; } set { purchaseOrderReceiveItem.LineType = value; } }
        public string InventoryId { get; set; }//{ get { return purchaseOrderReceiveItem.InventoryId; } set { purchaseOrderReceiveItem.InventoryId = value; } }
        public string WarehouseId { get; set; }//{ get { return purchaseOrderReceiveItem.WarehouseId; } set { purchaseOrderReceiveItem.WarehouseId = value; } }
        public string WarehouseCode { get; set; }//{ get { return purchaseOrderReceiveItem.WarehouseCode; } set { purchaseOrderReceiveItem.WarehouseCode = value; } }
        public string Warehouse { get; set; }//{ get { return purchaseOrderReceiveItem.Warehouse; } set { purchaseOrderReceiveItem.Warehouse = value; } }
        public string SubOrderId { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderId; } set { purchaseOrderReceiveItem.SubOrderId = value; } }
        public string SubOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderItemId; } set { purchaseOrderReceiveItem.SubOrderItemId = value; } }
        public string SubOrderNumber { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderNumber; } set { purchaseOrderReceiveItem.SubOrderNumber = value; } }
        public string ICode { get; set; }//{ get { return purchaseOrderReceiveItem.ICode; } set { purchaseOrderReceiveItem.ICode = value; } }
        public string ICodeColor { get; set; }
        public string ICodeDisplay { get; set; }//{ get { return purchaseOrderReceiveItem.ICodeDisplay; } set { purchaseOrderReceiveItem.ICodeDisplay = value; } }
        public string TrackedBy { get; set; }//{ get { return purchaseOrderReceiveItem.TrackedBy; } set { purchaseOrderReceiveItem.TrackedBy = value; } }
        public string Description { get; set; }//{ get { return purchaseOrderReceiveItem.Description; } set { purchaseOrderReceiveItem.Description = value; } }
        public string DescriptionColor { get; set; }
        public decimal? QuantityOrdered { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityOrdered; } set { purchaseOrderReceiveItem.QuantityOrdered = value; } }
        public decimal? QuantityReceived { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityReceived; } set { purchaseOrderReceiveItem.QuantityReceived = value; } }
        public decimal? QuantityRemaining { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityRemaining; } set { purchaseOrderReceiveItem.QuantityRemaining = value; } }
        public decimal? QuantityReturned { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityReturned; } set { purchaseOrderReceiveItem.QuantityReturned = value; } }
        public decimal? Quantity { get; set; }//{ get { return purchaseOrderReceiveItem.Quantity; } set { purchaseOrderReceiveItem.Quantity = value; } }
        public string QuantityColor { get; set; }
        public decimal? QuantityNeedBarCode { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityNeedBarCode; } set { purchaseOrderReceiveItem.QuantityNeedBarCode = value; } }
        public string ItemOrder { get; set; }//{ get { return purchaseOrderReceiveItem.ItemOrder; } set { purchaseOrderReceiveItem.ItemOrder = value; } }
        public string RecType { get; set; }//{ get { return purchaseOrderReceiveItem.RecType; } set { purchaseOrderReceiveItem.RecType = value; } }
        public string ItemClass { get; set; }//{ get { return purchaseOrderReceiveItem.ItemClass; } set { purchaseOrderReceiveItem.ItemClass = value; } }
        public bool? OptionColor { get; set; }//{ get { return purchaseOrderReceiveItem.OptionColor; } set { purchaseOrderReceiveItem.OptionColor = value; } }
        public string NestedOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.NestedOrderItemId; } set { purchaseOrderReceiveItem.NestedOrderItemId = value; } }
        public int? BarCodeCount { get; set; }//{ get { return purchaseOrderReceiveItem.BarCodeCount; } set { purchaseOrderReceiveItem.BarCodeCount = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
