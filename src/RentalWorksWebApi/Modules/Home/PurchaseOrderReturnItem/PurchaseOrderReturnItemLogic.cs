using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.PurchaseOrderReturnItem
{
    public class PurchaseOrderReturnItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        //PurchaseOrderReturnItemRecord purchaseOrderReturnItem = new PurchaseOrderReturnItemRecord();
        PurchaseOrderReturnItemLoader purchaseOrderReturnItemLoader = new PurchaseOrderReturnItemLoader();
        public PurchaseOrderReturnItemLogic()
        {
            //dataRecords.Add(purchaseOrderReturnItem);
            dataLoader = purchaseOrderReturnItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ContractId { get; set; }//{ get { return purchaseOrderReturnItem.ContractId; } set { purchaseOrderReturnItem.ContractId = value; } }
        public string PurchaseOrderId { get; set; }//{ get { return purchaseOrderReturnItem.PurchaseOrderId; } set { purchaseOrderReturnItem.PurchaseOrderId = value; } }
        public string PurchaseOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.PurchaseOrderItemId; } set { purchaseOrderReturnItem.PurchaseOrderItemId = value; } }
        public string LineType { get; set; }//{ get { return purchaseOrderReturnItem.LineType; } set { purchaseOrderReturnItem.LineType = value; } }
        public string InventoryId { get; set; }//{ get { return purchaseOrderReturnItem.InventoryId; } set { purchaseOrderReturnItem.InventoryId = value; } }
        public string WarehouseId { get; set; }//{ get { return purchaseOrderReturnItem.WarehouseId; } set { purchaseOrderReturnItem.WarehouseId = value; } }
        public string WarehouseCode { get; set; }//{ get { return purchaseOrderReturnItem.WarehouseCode; } set { purchaseOrderReturnItem.WarehouseCode = value; } }
        public string Warehouse { get; set; }//{ get { return purchaseOrderReturnItem.Warehouse; } set { purchaseOrderReturnItem.Warehouse = value; } }
        public string SubOrderId { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderId; } set { purchaseOrderReturnItem.SubOrderId = value; } }
        public string SubOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderItemId; } set { purchaseOrderReturnItem.SubOrderItemId = value; } }
        public string SubOrderNumber { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderNumber; } set { purchaseOrderReturnItem.SubOrderNumber = value; } }
        public string ICode { get; set; }//{ get { return purchaseOrderReturnItem.ICode; } set { purchaseOrderReturnItem.ICode = value; } }
        public string ICodeColor { get; set; }
        public string ICodeDisplay { get; set; }//{ get { return purchaseOrderReturnItem.ICodeDisplay; } set { purchaseOrderReturnItem.ICodeDisplay = value; } }
        public string TrackedBy { get; set; }//{ get { return purchaseOrderReturnItem.TrackedBy; } set { purchaseOrderReturnItem.TrackedBy = value; } }
        public string Description { get; set; }//{ get { return purchaseOrderReturnItem.Description; } set { purchaseOrderReturnItem.Description = value; } }
        public string DescriptionColor { get; set; }
        public decimal? QuantityOrdered { get; set; }//{ get { return purchaseOrderReturnItem.QuantityOrdered; } set { purchaseOrderReturnItem.QuantityOrdered = value; } }
        public decimal? QuantityReturnd { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturnd; } set { purchaseOrderReturnItem.QuantityReturnd = value; } }
        public decimal? QuantityReturned { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturned; } set { purchaseOrderReturnItem.QuantityReturned = value; } }
        public decimal? QuantityReturnable { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturnable; } set { purchaseOrderReturnItem.QuantityReturnable = value; } }
        public decimal? Quantity { get; set; }//{ get { return purchaseOrderReturnItem.Quantity; } set { purchaseOrderReturnItem.Quantity = value; } }
        public decimal? QuantityNeedBarCode { get; set; }//{ get { return purchaseOrderReturnItem.QuantityNeedBarCode; } set { purchaseOrderReturnItem.QuantityNeedBarCode = value; } }
        public string ItemOrder { get; set; }//{ get { return purchaseOrderReturnItem.ItemOrder; } set { purchaseOrderReturnItem.ItemOrder = value; } }
        public string RecType { get; set; }//{ get { return purchaseOrderReturnItem.RecType; } set { purchaseOrderReturnItem.RecType = value; } }
        public string ItemClass { get; set; }//{ get { return purchaseOrderReturnItem.ItemClass; } set { purchaseOrderReturnItem.ItemClass = value; } }
        public bool? OptionColor { get; set; }//{ get { return purchaseOrderReturnItem.OptionColor; } set { purchaseOrderReturnItem.OptionColor = value; } }
        public string NestedOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.NestedOrderItemId; } set { purchaseOrderReturnItem.NestedOrderItemId = value; } }
        public int? BarCodeCount { get; set; }//{ get { return purchaseOrderReturnItem.BarCodeCount; } set { purchaseOrderReturnItem.BarCodeCount = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
