using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Home.BarCodeHolding;

namespace WebApi.Modules.Home.PurchaseOrderReturnBarCode
{
    public class PurchaseOrderReturnBarCodeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BarCodeHoldingRecord purchaseOrderReturnBarCode = new BarCodeHoldingRecord();
        PurchaseOrderReturnBarCodeLoader purchaseOrderReturnBarCodeLoader = new PurchaseOrderReturnBarCodeLoader();

        public PurchaseOrderReturnBarCodeLogic()
        {
            dataRecords.Add(purchaseOrderReturnBarCode);
            dataLoader = purchaseOrderReturnBarCodeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public int? PurchaseOrderReturnBarCodeId { get { return purchaseOrderReturnBarCode.BarCodeHoldingId; } set { purchaseOrderReturnBarCode.BarCodeHoldingId = value; } }
        public string BarCode { get { return purchaseOrderReturnBarCode.BarCode; } set { purchaseOrderReturnBarCode.BarCode = value; } }
        public string InspectionNumber { get { return purchaseOrderReturnBarCode.InspectionNumber; } set { purchaseOrderReturnBarCode.InspectionNumber = value; } }
        public string InspectionVendorId { get { return purchaseOrderReturnBarCode.InspectionVendorId; } set { purchaseOrderReturnBarCode.InspectionVendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InspectionVendor { get; set; }
        public string ManufactureDate { get { return purchaseOrderReturnBarCode.ManufactureDate; } set { purchaseOrderReturnBarCode.ManufactureDate = value; } }
        public int? PrintQuantity { get { return purchaseOrderReturnBarCode.PrintQuantity; } set { purchaseOrderReturnBarCode.PrintQuantity = value; } }
        public string SerialNumber { get { return purchaseOrderReturnBarCode.SerialNumber; } set { purchaseOrderReturnBarCode.SerialNumber = value; } }
        public string RfId { get { return purchaseOrderReturnBarCode.RfId; } set { purchaseOrderReturnBarCode.RfId = value; } }
        public string PurchaseOrderItemId { get { return purchaseOrderReturnBarCode.OrderItemId; } set { purchaseOrderReturnBarCode.OrderItemId = value; } }
        public string PurchaseOrderId { get { return purchaseOrderReturnBarCode.OrderId; } set { purchaseOrderReturnBarCode.OrderId = value; } }
        public int? OrderTranId { get { return purchaseOrderReturnBarCode.OrderTranId; } set { purchaseOrderReturnBarCode.OrderTranId = value; } }
        public string InternalChar { get { return purchaseOrderReturnBarCode.InternalChar; } set { purchaseOrderReturnBarCode.InternalChar = value; } }
        public string ReturnContractId { get { return purchaseOrderReturnBarCode.ReturnContractId; } set { purchaseOrderReturnBarCode.ReturnContractId = value; } }
        public string InventoryId { get { return purchaseOrderReturnBarCode.InventoryId; } set { purchaseOrderReturnBarCode.InventoryId = value; } }
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
        public string DateStamp { get { return purchaseOrderReturnBarCode.DateStamp; } set { purchaseOrderReturnBarCode.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
