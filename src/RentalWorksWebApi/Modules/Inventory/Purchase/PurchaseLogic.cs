using WebApi.Logic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Inventory.Purchase
{
    [FwLogic(Id: "90MpwdSMOWb0d")]
    public class PurchaseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseRecord purchase = new PurchaseRecord();
        PurchaseLoader purchaseLoader = new PurchaseLoader();
        public PurchaseLogic()
        {
            dataRecords.Add(purchase);
            dataLoader = purchaseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "91jOR5fwCDtvJ", IsPrimaryKey: true)]
        public string PurchaseId { get { return purchase.PurchaseId; } set { purchase.PurchaseId = value; } }
        [FwLogicProperty(Id: "93sOpz9Z0nzhG")]
        public string Ownership { get { return purchase.Ownership; } set { purchase.Ownership = value; } }
        [FwLogicProperty(Id: "94Uu34C7NCX0b")]
        public string InventoryId { get { return purchase.InventoryId; } set { purchase.InventoryId = value; } }
        [FwLogicProperty(Id: "94ZxntlkFcBrA", IsReadOnly: true, IsRecordTitle: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "96VWu3NnBUlg8", IsReadOnly: true, IsRecordTitle: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "97kpFvVlXRP9E", IsReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwLogicProperty(Id: "9Bhw59R2blG4p")]
        public string WarehouseId { get { return purchase.WarehouseId; } set { purchase.WarehouseId = value; } }
        [FwLogicProperty(Id: "9btDjHkpwrLJ5", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "9bWp2Kaa7yNlM", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "9BXhWk2DG4dGN")]
        public int? Quantity { get { return purchase.Quantity; } set { purchase.Quantity = value; } }
        [FwLogicProperty(Id: "9bzfehu8Cp7dD", IsRecordTitle: true)]
        public DateTime? PurchaseDate { get { return purchase.PurchaseDate; } set { purchase.PurchaseDate = value; } }
        [FwLogicProperty(Id: "9dmdw3FaHpegB")]
        public DateTime? ReceiveDate { get { return purchase.ReceiveDate; } set { purchase.ReceiveDate = value; } }
        [FwLogicProperty(Id: "9ebLUq35fzHbK")]
        public string PurchasePoId { get { return purchase.PurchasePoId; } set { purchase.PurchasePoId = value; } }
        [FwLogicProperty(Id: "9egWAoPEXSBrD")]
        public string PurchasePoItemId { get { return purchase.PurchasePoItemId; } set { purchase.PurchasePoItemId = value; } }
        [FwLogicProperty(Id: "9fjtxlQJMrwUf")]
        public string OutsidePurchaseOrderNumber { get { return purchase.OutsidePurchaseOrderNumber; } set { purchase.OutsidePurchaseOrderNumber = value; } }
        [FwLogicProperty(Id: "9G6Gv3ncKOtQe", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "9hYrHRqxCXBdz", IsReadOnly: true)]
        public string PurchaseOrderDescription { get; set; }
        [FwLogicProperty(Id: "9ihJj0PIxaVN4", IsReadOnly: true)]
        public string PurchaseOrderDepartmentId { get; set; }
        [FwLogicProperty(Id: "9iMuew70A32D9", IsReadOnly: true)]
        public string PurchaseOrderDepartment { get; set; }
        [FwLogicProperty(Id: "9KaRIoRMoWzoP", IsReadOnly: true)]
        public string PurchaseOrderApproverId { get; set; }
        [FwLogicProperty(Id: "9kV7y0TkoNaVD", IsReadOnly: true)]
        public string PurchaseOrderAgentId { get; set; }
        [FwLogicProperty(Id: "9kvfjGJ7a1QHY")]
        public string VendorId { get { return purchase.VendorId; } set { purchase.VendorId = value; } }
        [FwLogicProperty(Id: "9MzdXZxSgZCl8")]
        public string ConsignorId { get { return purchase.ConsignorId; } set { purchase.ConsignorId = value; } }
        [FwLogicProperty(Id: "9OAScrx6osoOs")]
        public string ConsignorAgreementId { get { return purchase.ConsignorAgreementId; } set { purchase.ConsignorAgreementId = value; } }
        [FwLogicProperty(Id: "9OvzHDyK3Wjm3", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "9q3R3uRvTbPin")]
        public decimal? UnitCost { get { return purchase.UnitCost; } set { purchase.UnitCost = value; } }
        [FwLogicProperty(Id: "9rRsW9GEbnF6B")]
        public decimal? UnitCostWithTax { get { return purchase.UnitCostWithTax; } set { purchase.UnitCostWithTax = value; } }
        [FwLogicProperty(Id: "9rSBtKlGD1vAU", IsReadOnly: true)]
        public decimal? VendorInvoiceCost { get; set; }
        [FwLogicProperty(Id: "9sqRnd9xKurn3")]
        public string InventoryReceiptId { get { return purchase.InventoryReceiptId; } set { purchase.InventoryReceiptId = value; } }
        [FwLogicProperty(Id: "9SWjwp4YePDDo")]
        public string InventoryReceiptItemId { get { return purchase.InventoryReceiptItemId; } set { purchase.InventoryReceiptItemId = value; } }
        [FwLogicProperty(Id: "9SxWgeRmqh0Fc")]
        public string CurrencyId { get { return purchase.CurrencyId; } set { purchase.CurrencyId = value; } }
        [FwLogicProperty(Id: "9TahhOHj35Tok", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "9TPcTbxb55cXQ", IsReadOnly: true)]
        public string WarehouseDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "9U26KxsO8dLNl")]
        public string PhysicalInventoryId { get { return purchase.PhysicalInventoryId; } set { purchase.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "9U4Cbm9Yurmqz")]
        public int? PhysicalInventoryItemId { get { return purchase.PhysicalInventoryItemId; } set { purchase.PhysicalInventoryItemId = value; } }
        [FwLogicProperty(Id: "9uCzx1ZEAJjUL")]
        public string VendorPartNumber { get { return purchase.VendorPartNumber; } set { purchase.VendorPartNumber = value; } }
        [FwLogicProperty(Id: "9ujUhtD0IYItd")]
        public string LeaseVendorId { get { return purchase.LeaseVendorId; } set { purchase.LeaseVendorId = value; } }
        [FwLogicProperty(Id: "9urJvglKOYnPv")]
        public string LeasePurchasedate { get { return purchase.LeasePurchasedate; } set { purchase.LeasePurchasedate = value; } }
        [FwLogicProperty(Id: "9YZMyL95JCKii")]
        public string LeasePurchaseOrderId { get { return purchase.LeasePurchaseOrderId; } set { purchase.LeasePurchaseOrderId = value; } }
        [FwLogicProperty(Id: "9Z16wPm76to7a")]
        public string LeaseNumber { get { return purchase.LeaseNumber; } set { purchase.LeaseNumber = value; } }
        [FwLogicProperty(Id: "9zgtb4RXQXYUf")]
        public decimal? LeasePurchaseAmount { get { return purchase.LeasePurchaseAmount; } set { purchase.LeasePurchaseAmount = value; } }
        [FwLogicProperty(Id: "9ZOJdE4LX9UIE")]
        public string LeaseReceiveDate { get { return purchase.LeaseReceiveDate; } set { purchase.LeaseReceiveDate = value; } }
        [FwLogicProperty(Id: "9zQCqAqhRJnz6")]
        public string LeaseDate { get { return purchase.LeaseDate; } set { purchase.LeaseDate = value; } }
        [FwLogicProperty(Id: "A0w44zxJ0Embm")]
        public decimal? LeaseAmount { get { return purchase.LeaseAmount; } set { purchase.LeaseAmount = value; } }
        [FwLogicProperty(Id: "a22HNPsYaCPSq")]
        public string LeasePartNumber { get { return purchase.LeasePartNumber; } set { purchase.LeasePartNumber = value; } }
        [FwLogicProperty(Id: "a2ohDcXvpLiLa")]
        public string LeaseContact { get { return purchase.LeaseContact; } set { purchase.LeaseContact = value; } }
        [FwLogicProperty(Id: "A3pUSZIcZcUZS")]
        public string LeaseDocumentId { get { return purchase.LeaseDocumentId; } set { purchase.LeaseDocumentId = value; } }
        [FwLogicProperty(Id: "a40KVtjlT8o17")]
        public string LeaseOrderedPoId { get { return purchase.LeaseOrderedPoId; } set { purchase.LeaseOrderedPoId = value; } }
        [FwLogicProperty(Id: "a5DTjIAqeJsQ9")]
        public string LeaseOrderedVendorId { get { return purchase.LeaseOrderedVendorId; } set { purchase.LeaseOrderedVendorId = value; } }
        [FwLogicProperty(Id: "A5hRryZ4Q5Yjm")]
        public DateTime? InputDate { get { return purchase.InputDate; } set { purchase.InputDate = value; } }
        [FwLogicProperty(Id: "a5xbJoBQ9rfv5")]
        public string InputByUserId { get { return purchase.InputByUserId; } set { purchase.InputByUserId = value; } }
        [FwLogicProperty(Id: "A5YfvfxToGiMj")]
        public DateTime? ModifiedDate { get { return purchase.ModifiedDate; } set { purchase.ModifiedDate = value; } }
        [FwLogicProperty(Id: "a6VWaqmp62ymM")]
        public string ModifiedByUserId { get { return purchase.ModifiedByUserId; } set { purchase.ModifiedByUserId = value; } }
        [FwLogicProperty(Id: "a743xXEEpb8Os")]
        public string ReceiveContractId { get { return purchase.ReceiveContractId; } set { purchase.ReceiveContractId = value; } }
        [FwLogicProperty(Id: "A7yCzhU321rek")]
        public string SessionId { get { return purchase.SessionId; } set { purchase.SessionId = value; } }
        [FwLogicProperty(Id: "A84ZQbRvnftQj")]
        public string OriginalPurchaseId { get { return purchase.OriginalPurchaseId; } set { purchase.OriginalPurchaseId = value; } }
        [FwLogicProperty(Id: "a87J3Rqc3zC5L")]
        public string AdjustmentId { get { return purchase.AdjustmentId; } set { purchase.AdjustmentId = value; } }
        [FwLogicProperty(Id: "A8aykZ8eMZPhe")]
        public string PurchaseNotes { get { return purchase.PurchaseNotes; } set { purchase.PurchaseNotes = value; } }
        [FwLogicProperty(Id: "a958gqrhgM3bu")]
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
