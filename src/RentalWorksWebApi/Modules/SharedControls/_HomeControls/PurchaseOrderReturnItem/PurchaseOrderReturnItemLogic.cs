using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.Home.PurchaseOrderReturnItem
{
    [FwLogic(Id:"GD7v28tgnuVJb")]
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
        [FwLogicProperty(Id:"QxABu0NsuSko")]
        public string ContractId { get; set; }//{ get { return purchaseOrderReturnItem.ContractId; } set { purchaseOrderReturnItem.ContractId = value; } }

        [FwLogicProperty(Id:"65kxUiLqbeT7")]
        public string PurchaseOrderId { get; set; }//{ get { return purchaseOrderReturnItem.PurchaseOrderId; } set { purchaseOrderReturnItem.PurchaseOrderId = value; } }

        [FwLogicProperty(Id:"LBj4YEg8IAq2")]
        public string PurchaseOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.PurchaseOrderItemId; } set { purchaseOrderReturnItem.PurchaseOrderItemId = value; } }

        [FwLogicProperty(Id:"j1JluHvD5CyD")]
        public string LineType { get; set; }//{ get { return purchaseOrderReturnItem.LineType; } set { purchaseOrderReturnItem.LineType = value; } }

        [FwLogicProperty(Id:"5j73KWFzWdnK")]
        public string InventoryId { get; set; }//{ get { return purchaseOrderReturnItem.InventoryId; } set { purchaseOrderReturnItem.InventoryId = value; } }

        [FwLogicProperty(Id:"oE3dO9n5msX9")]
        public string WarehouseId { get; set; }//{ get { return purchaseOrderReturnItem.WarehouseId; } set { purchaseOrderReturnItem.WarehouseId = value; } }

        [FwLogicProperty(Id:"pY49ZkopAuTo")]
        public string WarehouseCode { get; set; }//{ get { return purchaseOrderReturnItem.WarehouseCode; } set { purchaseOrderReturnItem.WarehouseCode = value; } }

        [FwLogicProperty(Id:"qiIvA9nhTzC5")]
        public string Warehouse { get; set; }//{ get { return purchaseOrderReturnItem.Warehouse; } set { purchaseOrderReturnItem.Warehouse = value; } }

        [FwLogicProperty(Id:"EVY16ixpkJF0")]
        public string SubOrderId { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderId; } set { purchaseOrderReturnItem.SubOrderId = value; } }

        [FwLogicProperty(Id:"GOLDZ2v52xmx")]
        public string SubOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderItemId; } set { purchaseOrderReturnItem.SubOrderItemId = value; } }

        [FwLogicProperty(Id:"tupHWpMgwVIJ")]
        public string SubOrderNumber { get; set; }//{ get { return purchaseOrderReturnItem.SubOrderNumber; } set { purchaseOrderReturnItem.SubOrderNumber = value; } }

        [FwLogicProperty(Id:"2EBQumEqF2mV")]
        public string ICode { get; set; }//{ get { return purchaseOrderReturnItem.ICode; } set { purchaseOrderReturnItem.ICode = value; } }

        [FwLogicProperty(Id:"W6MYpBVgE1Sx")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"xLpFw35QoNk8")]
        public string ICodeDisplay { get; set; }//{ get { return purchaseOrderReturnItem.ICodeDisplay; } set { purchaseOrderReturnItem.ICodeDisplay = value; } }

        [FwLogicProperty(Id:"0rm3cQZLmaYe")]
        public string TrackedBy { get; set; }//{ get { return purchaseOrderReturnItem.TrackedBy; } set { purchaseOrderReturnItem.TrackedBy = value; } }

        [FwLogicProperty(Id:"ynKx0UH2d3zI")]
        public string Description { get; set; }//{ get { return purchaseOrderReturnItem.Description; } set { purchaseOrderReturnItem.Description = value; } }

        [FwLogicProperty(Id:"i1rfRj1gjLWS")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"az6Sw1gSY5KA")]
        public decimal? QuantityOrdered { get; set; }//{ get { return purchaseOrderReturnItem.QuantityOrdered; } set { purchaseOrderReturnItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id:"KovVT6RTweaC")]
        public decimal? QuantityReturnd { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturnd; } set { purchaseOrderReturnItem.QuantityReturnd = value; } }

        [FwLogicProperty(Id:"f4oyBcwM2rau")]
        public decimal? QuantityReturned { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturned; } set { purchaseOrderReturnItem.QuantityReturned = value; } }

        [FwLogicProperty(Id:"Rl5ihzgQ0dcw")]
        public decimal? QuantityReturnable { get; set; }//{ get { return purchaseOrderReturnItem.QuantityReturnable; } set { purchaseOrderReturnItem.QuantityReturnable = value; } }

        [FwLogicProperty(Id:"jVfm3JPWE4fB")]
        public decimal? Quantity { get; set; }//{ get { return purchaseOrderReturnItem.Quantity; } set { purchaseOrderReturnItem.Quantity = value; } }

        [FwLogicProperty(Id:"HSbagMGODiRF")]
        public decimal? QuantityNeedBarCode { get; set; }//{ get { return purchaseOrderReturnItem.QuantityNeedBarCode; } set { purchaseOrderReturnItem.QuantityNeedBarCode = value; } }

        [FwLogicProperty(Id:"NQKaU3oH8Bv1")]
        public string ItemOrder { get; set; }//{ get { return purchaseOrderReturnItem.ItemOrder; } set { purchaseOrderReturnItem.ItemOrder = value; } }

        [FwLogicProperty(Id:"OyvV8jpnDr7T")]
        public string RecType { get; set; }//{ get { return purchaseOrderReturnItem.RecType; } set { purchaseOrderReturnItem.RecType = value; } }

        [FwLogicProperty(Id:"R7rvidMbsXfU")]
        public string ItemClass { get; set; }//{ get { return purchaseOrderReturnItem.ItemClass; } set { purchaseOrderReturnItem.ItemClass = value; } }

        [FwLogicProperty(Id:"ZuGzBpqxCMAK")]
        public bool? OptionColor { get; set; }//{ get { return purchaseOrderReturnItem.OptionColor; } set { purchaseOrderReturnItem.OptionColor = value; } }

        [FwLogicProperty(Id:"KspP0vm8cuU3")]
        public string NestedOrderItemId { get; set; }//{ get { return purchaseOrderReturnItem.NestedOrderItemId; } set { purchaseOrderReturnItem.NestedOrderItemId = value; } }

        [FwLogicProperty(Id:"sua5caySNjak")]
        public int? BarCodeCount { get; set; }//{ get { return purchaseOrderReturnItem.BarCodeCount; } set { purchaseOrderReturnItem.BarCodeCount = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
