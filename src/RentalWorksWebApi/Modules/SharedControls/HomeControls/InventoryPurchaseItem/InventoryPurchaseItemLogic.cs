using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.BarCodeHolding;
using WebApi.Modules.Utilities.InventoryPurchaseUtility;

namespace WebApi.Modules.HomeControls.InventoryPurchaseItem
{
    [FwLogic(Id: "TNVrRlKk7sfYc")]
    public class InventoryPurchaseItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BarCodeHoldingRecord inventoryPurchaseItem = new BarCodeHoldingRecord();
        InventoryPurchaseItemLoader inventoryPurchaseItemLoader = new InventoryPurchaseItemLoader();

        public InventoryPurchaseItemLogic()
        {
            dataRecords.Add(inventoryPurchaseItem);
            dataLoader = inventoryPurchaseItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "JrtnUpTg1coBE", IsPrimaryKey:true)]
        public int? InventoryPurchaseItemId { get { return inventoryPurchaseItem.BarCodeHoldingId; } set { inventoryPurchaseItem.BarCodeHoldingId = value; } }

        [FwLogicProperty(Id: "9XUAf7IQ8aMwA")]
        public string SessionId { get { return inventoryPurchaseItem.SessionId; } set { inventoryPurchaseItem.SessionId = value; } }

        [FwLogicProperty(Id: "8LEjUFHLYRVqj")]
        public string BarCode { get { return inventoryPurchaseItem.BarCode; } set { inventoryPurchaseItem.BarCode = value; } }

        [FwLogicProperty(Id: "g7SDIUx6hPZOG")]
        public string ManufactureDate { get { return inventoryPurchaseItem.ManufactureDate; } set { inventoryPurchaseItem.ManufactureDate = value; } }

        [FwLogicProperty(Id: "xgAc6zAY9EisB")]
        public int? PrintQuantity { get { return inventoryPurchaseItem.PrintQuantity; } set { inventoryPurchaseItem.PrintQuantity = value; } }

        [FwLogicProperty(Id: "9kzIjXSy6YPqL")]
        public string SerialNumber { get { return inventoryPurchaseItem.SerialNumber; } set { inventoryPurchaseItem.SerialNumber = value; } }

        [FwLogicProperty(Id: "ipfwiz7cVQkiJ")]
        public string RfId { get { return inventoryPurchaseItem.RfId; } set { inventoryPurchaseItem.RfId = value; } }

        [FwLogicProperty(Id: "qYkzExwdPlt9x", IsReadOnly:true)]
        public bool? SerialNumberIsMixedCase { get; set; }

        [FwLogicProperty(Id: "dmwT1s7Vustws")]
        public string DateStamp { get { return inventoryPurchaseItem.DateStamp; } set { inventoryPurchaseItem.DateStamp = value; } }

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
                    request.IgnoreId = InventoryPurchaseItemId.ToString();
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
                    request.IgnoreId = InventoryPurchaseItemId.ToString();
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
