using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.PurchaseOrderReceiveItem
{
    [FwLogic(Id:"cveSVyb2hsFHg")]
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
        [FwLogicProperty(Id:"BRZZkUHaaW4K")]
        public string ContractId { get; set; }//{ get { return purchaseOrderReceiveItem.ContractId; } set { purchaseOrderReceiveItem.ContractId = value; } }

        [FwLogicProperty(Id:"coYqskBJsKWX")]
        public string PurchaseOrderId { get; set; }//{ get { return purchaseOrderReceiveItem.PurchaseOrderId; } set { purchaseOrderReceiveItem.PurchaseOrderId = value; } }

        [FwLogicProperty(Id:"g1rgTAgG9ZlY")]
        public string PurchaseOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.PurchaseOrderItemId; } set { purchaseOrderReceiveItem.PurchaseOrderItemId = value; } }

        [FwLogicProperty(Id:"TbhQBvyCZXjD")]
        public string LineType { get; set; }//{ get { return purchaseOrderReceiveItem.LineType; } set { purchaseOrderReceiveItem.LineType = value; } }

        [FwLogicProperty(Id:"WXSn5zzpcLVC")]
        public string InventoryId { get; set; }//{ get { return purchaseOrderReceiveItem.InventoryId; } set { purchaseOrderReceiveItem.InventoryId = value; } }

        [FwLogicProperty(Id:"2rprVRdgIlXG")]
        public string WarehouseId { get; set; }//{ get { return purchaseOrderReceiveItem.WarehouseId; } set { purchaseOrderReceiveItem.WarehouseId = value; } }

        [FwLogicProperty(Id:"MS3dhPquyGk7")]
        public string WarehouseCode { get; set; }//{ get { return purchaseOrderReceiveItem.WarehouseCode; } set { purchaseOrderReceiveItem.WarehouseCode = value; } }

        [FwLogicProperty(Id:"oxfBl6lGwEVF")]
        public string Warehouse { get; set; }//{ get { return purchaseOrderReceiveItem.Warehouse; } set { purchaseOrderReceiveItem.Warehouse = value; } }

        [FwLogicProperty(Id:"vuNSHiUH30qs")]
        public string SubOrderId { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderId; } set { purchaseOrderReceiveItem.SubOrderId = value; } }

        [FwLogicProperty(Id:"kMYKTtJjOm7G")]
        public string SubOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderItemId; } set { purchaseOrderReceiveItem.SubOrderItemId = value; } }

        [FwLogicProperty(Id:"YJbSPsqzRAHt")]
        public string SubOrderNumber { get; set; }//{ get { return purchaseOrderReceiveItem.SubOrderNumber; } set { purchaseOrderReceiveItem.SubOrderNumber = value; } }

        [FwLogicProperty(Id:"Qzsfi2kpQucX")]
        public string ICode { get; set; }//{ get { return purchaseOrderReceiveItem.ICode; } set { purchaseOrderReceiveItem.ICode = value; } }

        [FwLogicProperty(Id:"fFvZk4B2ABbj")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"Hf4KP6RDOME1")]
        public string ICodeDisplay { get; set; }//{ get { return purchaseOrderReceiveItem.ICodeDisplay; } set { purchaseOrderReceiveItem.ICodeDisplay = value; } }

        [FwLogicProperty(Id:"VXCH3NuJJCVM")]
        public string TrackedBy { get; set; }//{ get { return purchaseOrderReceiveItem.TrackedBy; } set { purchaseOrderReceiveItem.TrackedBy = value; } }

        [FwLogicProperty(Id:"Lp3BBlktDYXW")]
        public string Description { get; set; }//{ get { return purchaseOrderReceiveItem.Description; } set { purchaseOrderReceiveItem.Description = value; } }

        [FwLogicProperty(Id:"lHKp4Xc5E8Su")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"ascVBoACUqOo")]
        public decimal? QuantityOrdered { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityOrdered; } set { purchaseOrderReceiveItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id:"m36sgI81hNo5")]
        public decimal? QuantityReceived { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityReceived; } set { purchaseOrderReceiveItem.QuantityReceived = value; } }

        [FwLogicProperty(Id:"IPNRdVtJFMiU")]
        public decimal? QuantityRemaining { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityRemaining; } set { purchaseOrderReceiveItem.QuantityRemaining = value; } }

        [FwLogicProperty(Id:"UP4gz5MT5GQR")]
        public decimal? QuantityReturned { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityReturned; } set { purchaseOrderReceiveItem.QuantityReturned = value; } }

        [FwLogicProperty(Id:"cnwrjMXe6JQl")]
        public decimal? Quantity { get; set; }//{ get { return purchaseOrderReceiveItem.Quantity; } set { purchaseOrderReceiveItem.Quantity = value; } }

        [FwLogicProperty(Id:"s0eGtC0gtd8S")]
        public string QuantityColor { get; set; }

        [FwLogicProperty(Id:"5C5Ttl3wK9HK")]
        public decimal? QuantityNeedBarCode { get; set; }//{ get { return purchaseOrderReceiveItem.QuantityNeedBarCode; } set { purchaseOrderReceiveItem.QuantityNeedBarCode = value; } }

        [FwLogicProperty(Id:"jlNkvdiGgKWA")]
        public string ItemOrder { get; set; }//{ get { return purchaseOrderReceiveItem.ItemOrder; } set { purchaseOrderReceiveItem.ItemOrder = value; } }

        [FwLogicProperty(Id:"13K62JxCM9Da")]
        public string RecType { get; set; }//{ get { return purchaseOrderReceiveItem.RecType; } set { purchaseOrderReceiveItem.RecType = value; } }

        [FwLogicProperty(Id:"64pAm8926blU")]
        public string ItemClass { get; set; }//{ get { return purchaseOrderReceiveItem.ItemClass; } set { purchaseOrderReceiveItem.ItemClass = value; } }

        [FwLogicProperty(Id:"MwxpvmBe1rwS")]
        public bool? OptionColor { get; set; }//{ get { return purchaseOrderReceiveItem.OptionColor; } set { purchaseOrderReceiveItem.OptionColor = value; } }

        [FwLogicProperty(Id:"JbcOm7lPFRtx")]
        public string NestedOrderItemId { get; set; }//{ get { return purchaseOrderReceiveItem.NestedOrderItemId; } set { purchaseOrderReceiveItem.NestedOrderItemId = value; } }

        [FwLogicProperty(Id:"AtO35KYP2uzg")]
        public int? BarCodeCount { get; set; }//{ get { return purchaseOrderReceiveItem.BarCodeCount; } set { purchaseOrderReceiveItem.BarCodeCount = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
