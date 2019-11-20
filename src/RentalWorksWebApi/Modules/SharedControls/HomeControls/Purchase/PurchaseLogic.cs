using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.Purchase
{
    [FwLogic(Id: "uWA4rDdby6XhG")]
    public class PurchaseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseRecord purchase = new PurchaseRecord();
        public PurchaseLogic()
        {
            dataRecords.Add(purchase);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "nR5RZ5KXSgaFs", IsPrimaryKey: true)]
        public string PurchaseId { get { return purchase.PurchaseId; } set { purchase.PurchaseId = value; } }
        [FwLogicProperty(Id: "OUsBaYj8Rrgvk")]
        public string PurchasePurchaseOrderId { get { return purchase.PurchasePurchaseOrderId; } set { purchase.PurchasePurchaseOrderId = value; } }
        [FwLogicProperty(Id: "AwSDWqoq3D5qm")]
        public string WarehouseId { get { return purchase.WarehouseId; } set { purchase.WarehouseId = value; } }
        [FwLogicProperty(Id: "4PLx0tp7NhGzi")]
        public int? PhysicalItemId { get { return purchase.PhysicalItemId; } set { purchase.PhysicalItemId = value; } }
        [FwLogicProperty(Id: "38RdOmOI2dd5o")]
        public string PhysicalInventoryId { get { return purchase.PhysicalInventoryId; } set { purchase.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "sW0DaebhtMgYL")]
        public string PurchaseDate { get { return purchase.PurchaseDate; } set { purchase.PurchaseDate = value; } }
        [FwLogicProperty(Id: "FAvnR9CARxvIH")]
        public decimal? PurchaseAmount { get { return purchase.PurchaseAmount; } set { purchase.PurchaseAmount = value; } }
        [FwLogicProperty(Id: "3ZFQ2nB1IlgPc")]
        public string VendorPartNumber { get { return purchase.VendorPartNumber; } set { purchase.VendorPartNumber = value; } }
        [FwLogicProperty(Id: "4Fm5FxAFO5z7Q")]
        public string ReceiveDate { get { return purchase.ReceiveDate; } set { purchase.ReceiveDate = value; } }
        [FwLogicProperty(Id: "JHF5ypYHgd4Kd")]
        public string PurchaseVendorId { get { return purchase.PurchaseVendorId; } set { purchase.PurchaseVendorId = value; } }
        [FwLogicProperty(Id: "RUBoLwtOL7EDu")]
        public string Ownership { get { return purchase.Ownership; } set { purchase.Ownership = value; } }
        [FwLogicProperty(Id: "e1YN4IdXwuI1m")]
        public string LeaseVendorId { get { return purchase.LeaseVendorId; } set { purchase.LeaseVendorId = value; } }
        [FwLogicProperty(Id: "Px6yGrevXUMzg")]
        public string LeasePurchaseDate { get { return purchase.LeasePurchaseDate; } set { purchase.LeasePurchaseDate = value; } }
        [FwLogicProperty(Id: "rQBpCVAaT3vAS")]
        public string LeasePurchaseOrderId { get { return purchase.LeasePurchaseOrderId; } set { purchase.LeasePurchaseOrderId = value; } }
        [FwLogicProperty(Id: "wnBLMc3WRfFLJ")]
        public string LeaseNumber { get { return purchase.LeaseNumber; } set { purchase.LeaseNumber = value; } }
        [FwLogicProperty(Id: "ZY7DUXC7YBfgL")]
        public decimal? LeasePurchaseAmount { get { return purchase.LeasePurchaseAmount; } set { purchase.LeasePurchaseAmount = value; } }
        [FwLogicProperty(Id: "xxMIWfjBUODCT")]
        public string LeaseReceiveDate { get { return purchase.LeaseReceiveDate; } set { purchase.LeaseReceiveDate = value; } }
        [FwLogicProperty(Id: "eSCkDyuMHt4DO")]
        public string LeaseDate { get { return purchase.LeaseDate; } set { purchase.LeaseDate = value; } }
        [FwLogicProperty(Id: "Kf6ifAiauhe6o")]
        public decimal? LeaseAmount { get { return purchase.LeaseAmount; } set { purchase.LeaseAmount = value; } }
        [FwLogicProperty(Id: "8kd75czAf2Tbt")]
        public string LeasePartNumber { get { return purchase.LeasePartNumber; } set { purchase.LeasePartNumber = value; } }
        [FwLogicProperty(Id: "FIjxTi75gPi5Y")]
        public string InputByUserId { get { return purchase.InputByUserId; } set { purchase.InputByUserId = value; } }
        [FwLogicProperty(Id: "mG0n4gD9AUXRS")]
        public string ModifiedByUserId { get { return purchase.ModifiedByUserId; } set { purchase.ModifiedByUserId = value; } }
        [FwLogicProperty(Id: "Jfz0ZD1uRmKVg")]
        public string LeaseContact { get { return purchase.LeaseContact; } set { purchase.LeaseContact = value; } }
        [FwLogicProperty(Id: "bXZtzMqKjfEVC")]
        public string LeaseDocumentId { get { return purchase.LeaseDocumentId; } set { purchase.LeaseDocumentId = value; } }
        [FwLogicProperty(Id: "TpQyBnRYhPU9z")]
        public string InputDate { get { return purchase.InputDate; } set { purchase.InputDate = value; } }
        [FwLogicProperty(Id: "JbgrDp8a02o4W")]
        public string ModifiedDate { get { return purchase.ModifiedDate; } set { purchase.ModifiedDate = value; } }
        [FwLogicProperty(Id: "mhp95DOMJrPOr")]
        public string PurchaseNotes { get { return purchase.PurchaseNotes; } set { purchase.PurchaseNotes = value; } }
        [FwLogicProperty(Id: "g18zxVuLkqLXn")]
        public string OutsidePurchaseOrderNumber { get { return purchase.OutsidePurchaseOrderNumber; } set { purchase.OutsidePurchaseOrderNumber = value; } }
        [FwLogicProperty(Id: "Z7YVDtkzi2C0l")]
        public string LeaseOrderedPurchaseOrderId { get { return purchase.LeaseOrderedPurchaseOrderId; } set { purchase.LeaseOrderedPurchaseOrderId = value; } }
        [FwLogicProperty(Id: "DDcCYpfq3tf9I")]
        public string LeaseOrderedVendorId { get { return purchase.LeaseOrderedVendorId; } set { purchase.LeaseOrderedVendorId = value; } }
        [FwLogicProperty(Id: "JtNcCGcPba2dZ")]
        public string InventoryId { get { return purchase.InventoryId; } set { purchase.InventoryId = value; } }
        [FwLogicProperty(Id: "UFWoLhe3Y9kLl")]
        public int? Quantity { get { return purchase.Quantity; } set { purchase.Quantity = value; } }
        [FwLogicProperty(Id: "Zhcn6nVmFyuHZ")]
        public string ReceiveContractId { get { return purchase.ReceiveContractId; } set { purchase.ReceiveContractId = value; } }
        [FwLogicProperty(Id: "Ztofn17W6PiDg")]
        public string PurchasePurchaseOrderItemId { get { return purchase.PurchasePurchaseOrderItemId; } set { purchase.PurchasePurchaseOrderItemId = value; } }
        [FwLogicProperty(Id: "jkPbG7ZnwR6pj")]
        public string InventoryReceiptId { get { return purchase.InventoryReceiptId; } set { purchase.InventoryReceiptId = value; } }
        [FwLogicProperty(Id: "VcvBheYjVNsXK")]
        public string InventoryReceiptItemId { get { return purchase.InventoryReceiptItemId; } set { purchase.InventoryReceiptItemId = value; } }
        [FwLogicProperty(Id: "PxJPxNq9OHh5q")]
        public decimal? PurchaseAmountWithTax { get { return purchase.PurchaseAmountWithTax; } set { purchase.PurchaseAmountWithTax = value; } }
        [FwLogicProperty(Id: "pwaVm0j6ioGZZ")]
        public string ConsignorAgreementId { get { return purchase.ConsignorAgreementId; } set { purchase.ConsignorAgreementId = value; } }
        [FwLogicProperty(Id: "B2yt2sk8P4oXP")]
        public string ConsignorId { get { return purchase.ConsignorId; } set { purchase.ConsignorId = value; } }
        [FwLogicProperty(Id: "SIxisk5HMwDyT")]
        public string SessionId { get { return purchase.SessionId; } set { purchase.SessionId = value; } }
        [FwLogicProperty(Id: "NbmTTHvMTLFkj")]
        public string CurrencyId { get { return purchase.CurrencyId; } set { purchase.CurrencyId = value; } }
        [FwLogicProperty(Id: "OwXUNlVaDY4qR")]
        public string OriginalPurchaseId { get { return purchase.OriginalPurchaseId; } set { purchase.OriginalPurchaseId = value; } }
        [FwLogicProperty(Id: "9X6iXzm1EK68Q")]
        public string InventoryAdjustmentId { get { return purchase.InventoryAdjustmentId; } set { purchase.InventoryAdjustmentId = value; } }
        [FwLogicProperty(Id: "Nv9scOQbewY0S")]
        public string DateStamp { get { return purchase.DateStamp; } set { purchase.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
