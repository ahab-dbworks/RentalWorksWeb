using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Home.BarCodeHolding;

namespace WebApi.Modules.Home.PurchaseOrderReturnBarCode
{
    [FwLogic(Id:"npmP9HG9tOU4z")]
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
        [FwLogicProperty(Id:"8vKdWDBIahcpu", IsPrimaryKey:true)]
        public int? PurchaseOrderReturnBarCodeId { get { return purchaseOrderReturnBarCode.BarCodeHoldingId; } set { purchaseOrderReturnBarCode.BarCodeHoldingId = value; } }

        [FwLogicProperty(Id:"oYd178YgMqDQ")]
        public string BarCode { get { return purchaseOrderReturnBarCode.BarCode; } set { purchaseOrderReturnBarCode.BarCode = value; } }

        [FwLogicProperty(Id:"Jj4xFFs98jAC")]
        public string InspectionNumber { get { return purchaseOrderReturnBarCode.InspectionNumber; } set { purchaseOrderReturnBarCode.InspectionNumber = value; } }

        [FwLogicProperty(Id:"71RWq9EtEPge")]
        public string InspectionVendorId { get { return purchaseOrderReturnBarCode.InspectionVendorId; } set { purchaseOrderReturnBarCode.InspectionVendorId = value; } }

        [FwLogicProperty(Id:"OOPxvtOcSsM5G", IsReadOnly:true)]
        public string InspectionVendor { get; set; }

        [FwLogicProperty(Id:"1b5z9906Gf2t")]
        public string ManufactureDate { get { return purchaseOrderReturnBarCode.ManufactureDate; } set { purchaseOrderReturnBarCode.ManufactureDate = value; } }

        [FwLogicProperty(Id:"nS47odbxN1U4")]
        public int? PrintQuantity { get { return purchaseOrderReturnBarCode.PrintQuantity; } set { purchaseOrderReturnBarCode.PrintQuantity = value; } }

        [FwLogicProperty(Id:"BmUsU7KBZimy")]
        public string SerialNumber { get { return purchaseOrderReturnBarCode.SerialNumber; } set { purchaseOrderReturnBarCode.SerialNumber = value; } }

        [FwLogicProperty(Id:"PQuLnQcCkChj")]
        public string RfId { get { return purchaseOrderReturnBarCode.RfId; } set { purchaseOrderReturnBarCode.RfId = value; } }

        [FwLogicProperty(Id:"y084Qdj27T2j")]
        public string PurchaseOrderItemId { get { return purchaseOrderReturnBarCode.OrderItemId; } set { purchaseOrderReturnBarCode.OrderItemId = value; } }

        [FwLogicProperty(Id:"adQ3mqDt0tQG")]
        public string PurchaseOrderId { get { return purchaseOrderReturnBarCode.OrderId; } set { purchaseOrderReturnBarCode.OrderId = value; } }

        [FwLogicProperty(Id:"B1cmTGS6sP6t")]
        public int? OrderTranId { get { return purchaseOrderReturnBarCode.OrderTranId; } set { purchaseOrderReturnBarCode.OrderTranId = value; } }

        [FwLogicProperty(Id:"JhyY7gdjio6S")]
        public string InternalChar { get { return purchaseOrderReturnBarCode.InternalChar; } set { purchaseOrderReturnBarCode.InternalChar = value; } }

        [FwLogicProperty(Id:"u9CbyZCe0ddL")]
        public string ReturnContractId { get { return purchaseOrderReturnBarCode.ReturnContractId; } set { purchaseOrderReturnBarCode.ReturnContractId = value; } }

        [FwLogicProperty(Id:"wvS3xk2iAQnP")]
        public string InventoryId { get { return purchaseOrderReturnBarCode.InventoryId; } set { purchaseOrderReturnBarCode.InventoryId = value; } }

        [FwLogicProperty(Id:"8vKdWDBIahcpu", IsReadOnly:true)]
        public string ScannableBarCode { get; set; }

        [FwLogicProperty(Id:"WpIPc4aPFiRWo", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"ePgSN2LHspGbN", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"ePgSN2LHspGbN", IsReadOnly:true)]
        public string ICodeDescription { get; set; }

        [FwLogicProperty(Id:"r3GZuqFl8Enhq", IsReadOnly:true)]
        public string PackingSlipNumber { get; set; }

        [FwLogicProperty(Id:"6HtkPKUmns4j3", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"ZUAQJKkPzBCwH", IsReadOnly:true)]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"KHcTp3MOaypDs", IsReadOnly:true)]
        public string PurchaseId { get; set; }

        [FwLogicProperty(Id:"ctGvR72voWJ5E", IsReadOnly:true)]
        public string AvailableFor { get; set; }

        [FwLogicProperty(Id:"ctGvR72voWJ5E", IsReadOnly:true)]
        public string AvailableForDisplay { get; set; }

        [FwLogicProperty(Id:"QW9NbKj2WnbkT", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"VGEoUgnEDwhOj", IsReadOnly:true)]
        public bool? SerialNumberIsMixedCase { get; set; }

        [FwLogicProperty(Id:"y3ZsM17fROaF")]
        public string DateStamp { get { return purchaseOrderReturnBarCode.DateStamp; } set { purchaseOrderReturnBarCode.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
