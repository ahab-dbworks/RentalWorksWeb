using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Home.BarCodeHolding;

namespace WebApi.Modules.Home.PurchaseOrderReceiveBarCode
{
    public class PurchaseOrderReceiveBarCodeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BarCodeHoldingRecord purchaseOrderReceiveBarCode = new BarCodeHoldingRecord();
        PurchaseOrderReceiveBarCodeLoader purchaseOrderReceiveBarCodeLoader = new PurchaseOrderReceiveBarCodeLoader();

        public PurchaseOrderReceiveBarCodeLogic()
        {
            dataRecords.Add(purchaseOrderReceiveBarCode);
            dataLoader = purchaseOrderReceiveBarCodeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public int? PurchaseOrderReceiveBarCodeId { get { return purchaseOrderReceiveBarCode.BarCodeHoldingId; } set { purchaseOrderReceiveBarCode.BarCodeHoldingId = value; } }
        public string BarCode { get { return purchaseOrderReceiveBarCode.BarCode; } set { purchaseOrderReceiveBarCode.BarCode = value; } }
        public string InspectionNumber { get { return purchaseOrderReceiveBarCode.InspectionNumber; } set { purchaseOrderReceiveBarCode.InspectionNumber = value; } }
        public string InspectionVendorId { get { return purchaseOrderReceiveBarCode.InspectionVendorId; } set { purchaseOrderReceiveBarCode.InspectionVendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InspectionVendor { get; set; }
        public string ManufactureDate { get { return purchaseOrderReceiveBarCode.ManufactureDate; } set { purchaseOrderReceiveBarCode.ManufactureDate = value; } }
        public int? PrintQuantity { get { return purchaseOrderReceiveBarCode.PrintQuantity; } set { purchaseOrderReceiveBarCode.PrintQuantity = value; } }
        public string SerialNumber { get { return purchaseOrderReceiveBarCode.SerialNumber; } set { purchaseOrderReceiveBarCode.SerialNumber = value; } }
        public string RfId { get { return purchaseOrderReceiveBarCode.RfId; } set { purchaseOrderReceiveBarCode.RfId = value; } }
        public string PurchaseOrderItemId { get { return purchaseOrderReceiveBarCode.OrderItemId; } set { purchaseOrderReceiveBarCode.OrderItemId = value; } }
        public string PurchaseOrderId { get { return purchaseOrderReceiveBarCode.OrderId; } set { purchaseOrderReceiveBarCode.OrderId = value; } }
        public int? OrderTranId { get { return purchaseOrderReceiveBarCode.OrderTranId; } set { purchaseOrderReceiveBarCode.OrderTranId = value; } }
        public string InternalChar { get { return purchaseOrderReceiveBarCode.InternalChar; } set { purchaseOrderReceiveBarCode.InternalChar = value; } }
        public string ReceiveContractId { get { return purchaseOrderReceiveBarCode.ReceiveContractId; } set { purchaseOrderReceiveBarCode.ReceiveContractId = value; } }
        public string ReturnContractId { get { return purchaseOrderReceiveBarCode.ReturnContractId; } set { purchaseOrderReceiveBarCode.ReturnContractId = value; } }
        public string InventoryId { get { return purchaseOrderReceiveBarCode.InventoryId; } set { purchaseOrderReceiveBarCode.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ScannableBarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PackingSlipNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailableForDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SerialNumberIsMixedCase { get; set; }
        public string DateStamp { get { return purchaseOrderReceiveBarCode.DateStamp; } set { purchaseOrderReceiveBarCode.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
