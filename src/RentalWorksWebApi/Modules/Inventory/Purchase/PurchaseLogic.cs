using WebApi.Logic;
using FwStandard.AppManager;
using System;
using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

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

            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "91jOR5fwCDtvJ", IsPrimaryKey: true)]
        public string PurchaseId { get { return purchase.PurchaseId; } set { purchase.PurchaseId = value; } }
        [FwLogicProperty(Id: "93sOpz9Z0nzhG", DisableDirectAssign: true, DisableDirectModify: true)]
        public string Ownership { get { return purchase.Ownership; } set { purchase.Ownership = value; } }
        [FwLogicProperty(Id: "94Uu34C7NCX0b", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InventoryId { get { return purchase.InventoryId; } set { purchase.InventoryId = value; } }
        [FwLogicProperty(Id: "94ZxntlkFcBrA", IsReadOnly: true, IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "96VWu3NnBUlg8", IsReadOnly: true, IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "97kpFvVlXRP9E", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string TrackedBy { get; set; }
        [FwLogicProperty(Id: "zu0C3TdN7iLA", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "jfidmL2vIYN4W", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public bool? FixedAsset { get; set; }
        [FwLogicProperty(Id: "9Bhw59R2blG4p", DisableDirectAssign: true, DisableDirectModify: true)]
        public string WarehouseId { get { return purchase.WarehouseId; } set { purchase.WarehouseId = value; } }
        [FwLogicProperty(Id: "9btDjHkpwrLJ5", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "9bWp2Kaa7yNlM", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "9BXhWk2DG4dGN", DisableDirectAssign: true, DisableDirectModify: true)]
        public int? Quantity { get { return purchase.Quantity; } set { purchase.Quantity = value; } }
        [FwLogicProperty(Id: "9bzfehu8Cp7dD", IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public DateTime? PurchaseDate { get { return purchase.PurchaseDate; } set { purchase.PurchaseDate = value; } }
        [FwLogicProperty(Id: "9dmdw3FaHpegB", DisableDirectAssign: true, DisableDirectModify: true)]
        public DateTime? ReceiveDate { get { return purchase.ReceiveDate; } set { purchase.ReceiveDate = value; } }
        [FwLogicProperty(Id: "jNSSjhIPTIsni", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseDateString { get; set; }
        [FwLogicProperty(Id: "P640wPnn50hWw", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string ReceiveDateString { get; set; }
        [FwLogicProperty(Id: "9ebLUq35fzHbK", DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchasePoId { get { return purchase.PurchasePoId; } set { purchase.PurchasePoId = value; } }
        [FwLogicProperty(Id: "9egWAoPEXSBrD", DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchasePoItemId { get { return purchase.PurchasePoItemId; } set { purchase.PurchasePoItemId = value; } }
        [FwLogicProperty(Id: "9fjtxlQJMrwUf", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OutsidePurchaseOrderNumber { get { return purchase.OutsidePurchaseOrderNumber; } set { purchase.OutsidePurchaseOrderNumber = value; } }
        [FwLogicProperty(Id: "9G6Gv3ncKOtQe", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "9hYrHRqxCXBdz", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderDescription { get; set; }
        [FwLogicProperty(Id: "9ihJj0PIxaVN4", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderDepartmentId { get; set; }
        [FwLogicProperty(Id: "9iMuew70A32D9", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderDepartment { get; set; }
        [FwLogicProperty(Id: "9KaRIoRMoWzoP", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderApproverId { get; set; }
        [FwLogicProperty(Id: "9kV7y0TkoNaVD", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string PurchaseOrderAgentId { get; set; }
        [FwLogicProperty(Id: "9kvfjGJ7a1QHY", DisableDirectAssign: true, DisableDirectModify: true)]
        public string VendorId { get { return purchase.VendorId; } set { purchase.VendorId = value; } }
        [FwLogicProperty(Id: "9MzdXZxSgZCl8", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ConsignorId { get { return purchase.ConsignorId; } set { purchase.ConsignorId = value; } }
        [FwLogicProperty(Id: "9OAScrx6osoOs", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ConsignorAgreementId { get { return purchase.ConsignorAgreementId; } set { purchase.ConsignorAgreementId = value; } }
        [FwLogicProperty(Id: "9OvzHDyK3Wjm3", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "9q3R3uRvTbPin", DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? UnitCost { get { return purchase.UnitCost; } set { purchase.UnitCost = value; } }
        [FwLogicProperty(Id: "Ha3LtGylP2DKE", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? CostExtended { get; set; }
        [FwLogicProperty(Id: "9rRsW9GEbnF6B", DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? UnitCostWithTax { get { return purchase.UnitCostWithTax; } set { purchase.UnitCostWithTax = value; } }
        [FwLogicProperty(Id: "7Jb8fuhsdQeo", DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? OriginalEquipmentCost { get { return purchase.OriginalEquipmentCost; } set { purchase.OriginalEquipmentCost = value; } }
        [FwLogicProperty(Id: "z76rgA0RhX7of", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? CostWithTaxExtended { get; set; }
        [FwLogicProperty(Id: "Q4iHVXmTjzPdu", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? UnitCostCurrencyConverted { get; set; }
        [FwLogicProperty(Id: "TczHsziIfzZf6", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? CurrencyExchangeRate { get { return purchase.CurrencyExchangeRate; } set { purchase.CurrencyExchangeRate = value; } }
        [FwLogicProperty(Id: "zr6AwpOtrejS8", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? CostCurrencyConvertedExtended { get; set; }
        [FwLogicProperty(Id: "76vjGUY4KvMYd", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? UnitCostWithTaxCurrencyConverted { get; set; }
        [FwLogicProperty(Id: "AL8Z1pJRUUYoy", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? CostWithTaxCurrencyConvertedExtended { get; set; }
        [FwLogicProperty(Id: "VeXAlZ0WsIRVU", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? TotalDepreciation { get; set; }
        [FwLogicProperty(Id: "jkXHVEPwjYXCI", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? TotalBookValue { get; set; }
        [FwLogicProperty(Id: "qlwC3b4TYJYXi", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? SalvageValue { get; set; }
        [FwLogicProperty(Id: "000pZayYThmOC", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? SalvageValueExtended { get; set; }
        [FwLogicProperty(Id: "qkxj4m6zWWJBR", IsReadOnly: true)]
        public int? DepreciationMonths { get; set; }
        [FwLogicProperty(Id: "9rSBtKlGD1vAU", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? VendorInvoiceCost { get; set; }
        [FwLogicProperty(Id: "9sqRnd9xKurn3", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InventoryReceiptId { get { return purchase.InventoryReceiptId; } set { purchase.InventoryReceiptId = value; } }
        [FwLogicProperty(Id: "9SWjwp4YePDDo", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InventoryReceiptItemId { get { return purchase.InventoryReceiptItemId; } set { purchase.InventoryReceiptItemId = value; } }
        [FwLogicProperty(Id: "9SxWgeRmqh0Fc", DisableDirectAssign: true, DisableDirectModify: true)]
        public string CurrencyId { get { return purchase.CurrencyId; } set { purchase.CurrencyId = value; } }
        [FwLogicProperty(Id: "9TahhOHj35Tok", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "HsUjvvyNT6Rqu", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "9TPcTbxb55cXQ", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string WarehouseDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "9TahhOHj35Tok", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string WarehouseDefaultCurrencyCode { get; set; }
        [FwLogicProperty(Id: "HsUjvvyNT6Rqu", IsReadOnly: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string WarehouseDefaultCurrencySymbol { get; set; }
        [FwLogicProperty(Id: "9U26KxsO8dLNl", DisableDirectAssign: true, DisableDirectModify: true)]
        public string PhysicalInventoryId { get { return purchase.PhysicalInventoryId; } set { purchase.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "9U4Cbm9Yurmqz", DisableDirectAssign: true, DisableDirectModify: true)]
        public int? PhysicalInventoryItemId { get { return purchase.PhysicalInventoryItemId; } set { purchase.PhysicalInventoryItemId = value; } }
        [FwLogicProperty(Id: "9uCzx1ZEAJjUL", DisableDirectAssign: true, DisableDirectModify: true)]
        public string VendorPartNumber { get { return purchase.VendorPartNumber; } set { purchase.VendorPartNumber = value; } }
        [FwLogicProperty(Id: "9ujUhtD0IYItd", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseVendorId { get { return purchase.LeaseVendorId; } set { purchase.LeaseVendorId = value; } }
        [FwLogicProperty(Id: "9urJvglKOYnPv", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeasePurchasedate { get { return purchase.LeasePurchasedate; } set { purchase.LeasePurchasedate = value; } }
        [FwLogicProperty(Id: "9YZMyL95JCKii", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeasePurchaseOrderId { get { return purchase.LeasePurchaseOrderId; } set { purchase.LeasePurchaseOrderId = value; } }
        [FwLogicProperty(Id: "9Z16wPm76to7a", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseNumber { get { return purchase.LeaseNumber; } set { purchase.LeaseNumber = value; } }
        [FwLogicProperty(Id: "9zgtb4RXQXYUf", DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? LeasePurchaseAmount { get { return purchase.LeasePurchaseAmount; } set { purchase.LeasePurchaseAmount = value; } }
        [FwLogicProperty(Id: "9ZOJdE4LX9UIE", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseReceiveDate { get { return purchase.LeaseReceiveDate; } set { purchase.LeaseReceiveDate = value; } }
        [FwLogicProperty(Id: "9zQCqAqhRJnz6", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseDate { get { return purchase.LeaseDate; } set { purchase.LeaseDate = value; } }
        [FwLogicProperty(Id: "A0w44zxJ0Embm", DisableDirectAssign: true, DisableDirectModify: true)]
        public decimal? LeaseAmount { get { return purchase.LeaseAmount; } set { purchase.LeaseAmount = value; } }
        [FwLogicProperty(Id: "a22HNPsYaCPSq", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeasePartNumber { get { return purchase.LeasePartNumber; } set { purchase.LeasePartNumber = value; } }
        [FwLogicProperty(Id: "a2ohDcXvpLiLa", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseContact { get { return purchase.LeaseContact; } set { purchase.LeaseContact = value; } }
        [FwLogicProperty(Id: "A3pUSZIcZcUZS", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseDocumentId { get { return purchase.LeaseDocumentId; } set { purchase.LeaseDocumentId = value; } }
        [FwLogicProperty(Id: "a40KVtjlT8o17", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseOrderedPoId { get { return purchase.LeaseOrderedPoId; } set { purchase.LeaseOrderedPoId = value; } }
        [FwLogicProperty(Id: "a5DTjIAqeJsQ9", DisableDirectAssign: true, DisableDirectModify: true)]
        public string LeaseOrderedVendorId { get { return purchase.LeaseOrderedVendorId; } set { purchase.LeaseOrderedVendorId = value; } }
        [FwLogicProperty(Id: "A5hRryZ4Q5Yjm", DisableDirectAssign: true, DisableDirectModify: true)]
        public DateTime? InputDate { get { return purchase.InputDate; } set { purchase.InputDate = value; } }
        [FwLogicProperty(Id: "a5xbJoBQ9rfv5", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InputByUserId { get { return purchase.InputByUserId; } set { purchase.InputByUserId = value; } }
        [FwLogicProperty(Id: "A5YfvfxToGiMj", DisableDirectAssign: true, DisableDirectModify: true)]
        public DateTime? ModifiedDate { get { return purchase.ModifiedDate; } set { purchase.ModifiedDate = value; } }
        [FwLogicProperty(Id: "a6VWaqmp62ymM", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ModifiedByUserId { get { return purchase.ModifiedByUserId; } set { purchase.ModifiedByUserId = value; } }
        [FwLogicProperty(Id: "a743xXEEpb8Os", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ReceiveContractId { get { return purchase.ReceiveContractId; } set { purchase.ReceiveContractId = value; } }
        [FwLogicProperty(Id: "A7yCzhU321rek", DisableDirectAssign: true, DisableDirectModify: true)]
        public string SessionId { get { return purchase.SessionId; } set { purchase.SessionId = value; } }
        [FwLogicProperty(Id: "A84ZQbRvnftQj", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OriginalPurchaseId { get { return purchase.OriginalPurchaseId; } set { purchase.OriginalPurchaseId = value; } }
        [FwLogicProperty(Id: "a87J3Rqc3zC5L", DisableDirectAssign: true, DisableDirectModify: true)]
        public string AdjustmentId { get { return purchase.AdjustmentId; } set { purchase.AdjustmentId = value; } }
        [FwLogicProperty(Id: "A8aykZ8eMZPhe")]
        public string PurchaseNotes { get { return purchase.PurchaseNotes; } set { purchase.PurchaseNotes = value; } }
        [FwLogicProperty(Id: "CLT5RRDZB2Nr", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ItemId { get; set; }
        [FwLogicProperty(Id: "2pROfEOT6Uqc", DisableDirectAssign: true, DisableDirectModify: true)]
        public string BarCode { get; set; }
        [FwLogicProperty(Id: "F9srj8yqNSNZ", DisableDirectAssign: true, DisableDirectModify: true)]
        public string SerialNumber { get; set; }
        [FwLogicProperty(Id: "BK2TqagSXUfq", DisableDirectAssign: true, DisableDirectModify: true)]
        public string Rfid { get; set; }
        [FwLogicProperty(Id: "a958gqrhgM3bu", DisableDirectAssign: true, DisableDirectModify: true)]
        public string DateStamp { get { return purchase.DateStamp; } set { purchase.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                CurrencyExchangeRate = 1;
                WarehouseLogic warehouse = new WarehouseLogic();
                warehouse.SetDependencies(AppConfig, UserSession);
                warehouse.WarehouseId = WarehouseId;
                if (warehouse.LoadAsync<WarehouseLogic>().Result)
                {
                    if (!CurrencyId.Equals(warehouse.CurrencyId))
                    {
                        CurrencyExchangeRate = AppFunc.GetCurrencyExchangeRate(AppConfig, CurrencyId, warehouse.CurrencyId).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
