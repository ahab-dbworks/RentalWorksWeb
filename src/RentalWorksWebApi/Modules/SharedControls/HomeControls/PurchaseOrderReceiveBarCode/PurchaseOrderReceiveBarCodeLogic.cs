using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.BarCodeHolding;
using WebApi.Modules.Utilities.InventoryPurchaseUtility;

namespace WebApi.Modules.HomeControls.PurchaseOrderReceiveBarCode
{
    [FwLogic(Id:"d4Cc2GVnfJPfP")]
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
        [FwLogicProperty(Id:"hVYkRuJwWyLxp", IsPrimaryKey:true)]
        public int? PurchaseOrderReceiveBarCodeId { get { return purchaseOrderReceiveBarCode.BarCodeHoldingId; } set { purchaseOrderReceiveBarCode.BarCodeHoldingId = value; } }

        [FwLogicProperty(Id:"Jee1g7pgpf16")]
        public string BarCode { get { return purchaseOrderReceiveBarCode.BarCode; } set { purchaseOrderReceiveBarCode.BarCode = value; } }

        [FwLogicProperty(Id:"RUwNSAkEwBu9")]
        public string InspectionNumber { get { return purchaseOrderReceiveBarCode.InspectionNumber; } set { purchaseOrderReceiveBarCode.InspectionNumber = value; } }

        [FwLogicProperty(Id:"9sPuubFdLoqU")]
        public string InspectionVendorId { get { return purchaseOrderReceiveBarCode.InspectionVendorId; } set { purchaseOrderReceiveBarCode.InspectionVendorId = value; } }

        [FwLogicProperty(Id:"Vau9PKQcfPhDJ", IsReadOnly:true)]
        public string InspectionVendor { get; set; }

        [FwLogicProperty(Id:"Gc0zPNADq6FL")]
        public string ManufactureDate { get { return purchaseOrderReceiveBarCode.ManufactureDate; } set { purchaseOrderReceiveBarCode.ManufactureDate = value; } }

        [FwLogicProperty(Id:"F4pWLqS5Zlba")]
        public int? PrintQuantity { get { return purchaseOrderReceiveBarCode.PrintQuantity; } set { purchaseOrderReceiveBarCode.PrintQuantity = value; } }

        [FwLogicProperty(Id:"wG42adzH2yvT")]
        public string SerialNumber { get { return purchaseOrderReceiveBarCode.SerialNumber; } set { purchaseOrderReceiveBarCode.SerialNumber = value; } }

        [FwLogicProperty(Id:"2pKCUJbFzzQP")]
        public string RfId { get { return purchaseOrderReceiveBarCode.RfId; } set { purchaseOrderReceiveBarCode.RfId = value; } }

        [FwLogicProperty(Id:"3XGac4oY1olL")]
        public string PurchaseOrderItemId { get { return purchaseOrderReceiveBarCode.OrderItemId; } set { purchaseOrderReceiveBarCode.OrderItemId = value; } }

        [FwLogicProperty(Id:"pOJ2ycEEWike")]
        public string PurchaseOrderId { get { return purchaseOrderReceiveBarCode.OrderId; } set { purchaseOrderReceiveBarCode.OrderId = value; } }

        [FwLogicProperty(Id:"lbLOvXAIrsyz")]
        public int? OrderTranId { get { return purchaseOrderReceiveBarCode.OrderTranId; } set { purchaseOrderReceiveBarCode.OrderTranId = value; } }

        [FwLogicProperty(Id:"ImBwEaxq9Ose")]
        public string InternalChar { get { return purchaseOrderReceiveBarCode.InternalChar; } set { purchaseOrderReceiveBarCode.InternalChar = value; } }

        [FwLogicProperty(Id:"dWs8yAj2KrjS")]
        public string ReceiveContractId { get { return purchaseOrderReceiveBarCode.ReceiveContractId; } set { purchaseOrderReceiveBarCode.ReceiveContractId = value; } }

        [FwLogicProperty(Id: "GgkzhPhvSgtwM", IsReadOnly: true)]
        public string ReceiveContractNumber { get; set; }

        [FwLogicProperty(Id:"kqgDtcxfKJo7")]
        public string ReturnContractId { get { return purchaseOrderReceiveBarCode.ReturnContractId; } set { purchaseOrderReceiveBarCode.ReturnContractId = value; } }

        [FwLogicProperty(Id:"A6VuT3EsErPY")]
        public string InventoryId { get { return purchaseOrderReceiveBarCode.InventoryId; } set { purchaseOrderReceiveBarCode.InventoryId = value; } }

        [FwLogicProperty(Id:"hVYkRuJwWyLxp", IsReadOnly:true)]
        public string ScannableBarCode { get; set; }

        [FwLogicProperty(Id:"3wKfricc6KWGF", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"5ecqNc2PNhc5R", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"5ecqNc2PNhc5R", IsReadOnly:true)]
        public string ICodeDescription { get; set; }

        [FwLogicProperty(Id:"YIkUndeEgrDvH", IsReadOnly:true)]
        public string PackingSlipNumber { get; set; }

        [FwLogicProperty(Id:"uev4L6ASmmq4E", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"cPoFM5ee88vsX", IsReadOnly:true)]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"1ponoYMaw2WQ3", IsReadOnly:true)]
        public string PurchaseId { get; set; }

        [FwLogicProperty(Id:"6X9W0sLkgh8uA", IsReadOnly:true)]
        public string AvailableFor { get; set; }

        [FwLogicProperty(Id:"6X9W0sLkgh8uA", IsReadOnly:true)]
        public string AvailableForDisplay { get; set; }

        [FwLogicProperty(Id:"SNF7v69ejgi0m", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"f0cY35o3uZJWL", IsReadOnly:true)]
        public bool? SerialNumberIsMixedCase { get; set; }

        [FwLogicProperty(Id:"IEPwB38ojBPV")]
        public string DateStamp { get { return purchaseOrderReceiveBarCode.DateStamp; } set { purchaseOrderReceiveBarCode.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                if (!string.IsNullOrEmpty(BarCode))
                {
                    CodeExistsRequest request = new CodeExistsRequest();
                    request.Code = BarCode;
                    request.IgnoreId = PurchaseOrderReceiveBarCodeId.ToString();
                    CodeExistsResponse response = InventoryPurchaseUtilityFunc.CodeExists(AppConfig, UserSession, request).Result;
                    isValid = !response.Exists;
                    if (!isValid)
                    {
                        validateMsg = $"Bar Code {BarCode} already exists in {response.DefinedIn}";
                    }
                }
            }


            if (isValid)
            {
                if (!string.IsNullOrEmpty(SerialNumber))
                {
                    CodeExistsRequest request = new CodeExistsRequest();
                    request.Code = SerialNumber;
                    request.IgnoreId = PurchaseOrderReceiveBarCodeId.ToString();
                    CodeExistsResponse response = InventoryPurchaseUtilityFunc.CodeExists(AppConfig, UserSession, request).Result;
                    isValid = !response.Exists;
                    if (!isValid)
                    {
                        validateMsg = $"Serial Number {SerialNumber} already exists in {response.DefinedIn}";
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
