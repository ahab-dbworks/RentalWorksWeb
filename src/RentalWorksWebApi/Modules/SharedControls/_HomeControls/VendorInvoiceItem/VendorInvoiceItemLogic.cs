using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.HomeControls.VendorInvoiceItem
{
    [FwLogic(Id: "6LEAX0jUecG")]
    public class VendorInvoiceItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceItemRecord vendorInvoiceItem = new VendorInvoiceItemRecord();
        VendorInvoiceItemLoader vendorInvoiceItemLoader = new VendorInvoiceItemLoader();
        public VendorInvoiceItemLogic()
        {
            dataRecords.Add(vendorInvoiceItem);
            dataLoader = vendorInvoiceItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "stuciorVagL", IsPrimaryKey: true)]
        public string VendorInvoiceItemId { get { return vendorInvoiceItem.VendorInvoiceItemId; } set { vendorInvoiceItem.VendorInvoiceItemId = value; } }

        [FwLogicProperty(Id: "MUo18FXBqyB")]
        public string PurchaseOrderId { get { return vendorInvoiceItem.PurchaseOrderId; } set { vendorInvoiceItem.PurchaseOrderId = value; } }

        [FwLogicProperty(Id: "myI8T0ywwoG")]
        public string OrderItemId { get { return vendorInvoiceItem.OrderItemId; } set { vendorInvoiceItem.OrderItemId = value; } }

        [FwLogicProperty(Id: "s1AWG9Ol0gU", IsReadOnly: true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id: "ji0t7AxVEyc")]
        public string InventoryId { get { return vendorInvoiceItem.InventoryId; } set { vendorInvoiceItem.InventoryId = value; } }

        [FwLogicProperty(Id: "OQ2oxmDFUe2", IsReadOnly: true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "3aPrWgxrSjQ", IsReadOnly: true)]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id: "JEqTbwni7NP", IsReadOnly: true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id: "UiAlpEWj7Qj", IsReadOnly: true)]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id: "LNRxcEoyy0E")]
        public string Description { get { return vendorInvoiceItem.Description; } set { vendorInvoiceItem.Description = value; } }

        [FwLogicProperty(Id: "qiIE8SBIIuA", IsReadOnly: true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id: "JTCA9XizihT")]
        public string FromDate { get { return vendorInvoiceItem.FromDate; } set { vendorInvoiceItem.FromDate = value; } }

        [FwLogicProperty(Id: "ERf3S5ar7Qw")]
        public string ToDate { get { return vendorInvoiceItem.ToDate; } set { vendorInvoiceItem.ToDate = value; } }

        [FwLogicProperty(Id: "f14pEJM3f4N")]
        public decimal? Quantity { get { return vendorInvoiceItem.Quantity; } set { vendorInvoiceItem.Quantity = value; } }

        [FwLogicProperty(Id: "1VBgxIqmH7X", IsReadOnly: true)]
        public decimal? QuantityApproved { get; set; }

        [FwLogicProperty(Id: "47DX5llx9B2", IsReadOnly: true)]
        public decimal? AmountApproved { get; set; }

        [FwLogicProperty(Id: "t4mm5hB24Pb", IsReadOnly: true)]
        public string UnitId { get; set; }

        [FwLogicProperty(Id: "uK8ncIS7THd", IsReadOnly: true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id: "SnmnctPO7BV")]
        public decimal? Cost { get { return vendorInvoiceItem.Cost; } set { vendorInvoiceItem.Cost = value; } }

        [FwLogicProperty(Id: "dqd18dY7X9C", IsReadOnly: true)]
        public decimal? Extended { get; set; }

        [FwLogicProperty(Id: "zSFC1jFx2VG")]
        public decimal? Adjustment { get { return vendorInvoiceItem.Adjustment; } set { vendorInvoiceItem.Adjustment = value; } }

        [FwLogicProperty(Id: "5CmmnVqFqbj", IsReadOnly: true)]
        public decimal? LineTotal { get; set; }

        [FwLogicProperty(Id: "0f9ARl8wJ1T", IsReadOnly: true)]
        public decimal? LineTotalPerQuantity { get; set; }

        [FwLogicProperty(Id: "AxmCorl3u7F", IsReadOnly: true)]
        public decimal? TaxableExtended { get; set; }

        [FwLogicProperty(Id: "34agXtGDipQ")]
        public bool? Taxable { get { return vendorInvoiceItem.Taxable; } set { vendorInvoiceItem.Taxable = value; } }

        [FwLogicProperty(Id: "cR1D9BT0CXklD", IsReadOnly: true)]
        public decimal? Tax { get; set; }

        [FwLogicProperty(Id: "YuX1GJAoNJZxe", IsReadOnly: true)]
        public decimal? LineTotalWithTax { get; set; }

        [FwLogicProperty(Id: "kXFTc6vx0B9")]
        public string Note { get { return vendorInvoiceItem.Note; } set { vendorInvoiceItem.Note = value; } }

        [FwLogicProperty(Id: "8YvJ0pgIVm1")]
        public string ItemClass { get { return vendorInvoiceItem.ItemClass; } set { vendorInvoiceItem.ItemClass = value; } }

        [FwLogicProperty(Id: "exXkaY5oxqK")]
        public string RecType { get { return vendorInvoiceItem.RecType; } set { vendorInvoiceItem.RecType = value; } }

        [FwLogicProperty(Id: "Mesu7KoBYLI", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id: "JgcJ7hw4Wgn", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }

        [FwLogicProperty(Id: "uutjzTOgUqP", IsReadOnly: true)]
        public string InvoiceType { get; set; }

        [FwLogicProperty(Id: "UgVksECWpP8", IsReadOnly: true)]
        public string InvoiceDate { get; set; }

        [FwLogicProperty(Id: "jQi9csWuXuA", IsReadOnly: true)]
        public string StatusDate { get; set; }

        [FwLogicProperty(Id: "H7lxYLBiImE", IsReadOnly: true)]
        public int? VendorInvoiceNumber { get; set; }

        [FwLogicProperty(Id: "WTqPD9Gf9Wy", IsReadOnly: true)]
        public string InputDate { get; set; }

        [FwLogicProperty(Id: "Z4CGbSbegXO")]
        public string GlAccountId { get { return vendorInvoiceItem.GlAccountId; } set { vendorInvoiceItem.GlAccountId = value; } }

        [FwLogicProperty(Id: "Zq24XT8UZFJ", IsReadOnly: true)]
        public string GlAccountNo { get; set; }

        [FwLogicProperty(Id: "BJIG0rBWxK5", IsReadOnly: true)]
        public string IncomeAccountId { get; set; }

        [FwLogicProperty(Id: "8d0MlERR7x9", IsReadOnly: true)]
        public decimal? DealBilledQuantity { get; set; }

        [FwLogicProperty(Id: "fAcKfZGzVRA", IsReadOnly: true)]
        public decimal? DealBilledExtended { get; set; }

        [FwLogicProperty(Id: "D3fdBPUW1Mp", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "o0DrrB8ByfN", IsReadOnly: true)]
        public string OrderOrderItemId { get; set; }

        [FwLogicProperty(Id: "CBfBuK4AUqL", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id: "OIkJBscOKHx", IsReadOnly: true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "qhBhvJIY27E", IsReadOnly: true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id: "UGmIypQhgal", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "Jx3anlu3YF8", IsReadOnly: true)]
        public string DealTypeId { get; set; }

        [FwLogicProperty(Id: "aKAKBStfBs9", IsReadOnly: true)]
        public string OrderTypeId { get; set; }

        [FwLogicProperty(Id: "jXMR7AGCb4I", IsReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id: "rnGnOYIzTAk", IsReadOnly: true)]
        public decimal? QuantityReceived { get; set; }

        [FwLogicProperty(Id: "VEk5MdpYpbz", IsReadOnly: true)]
        public decimal? QuantityCanceled { get; set; }

        [FwLogicProperty(Id: "VfCSCWSMxgh", IsReadOnly: true)]
        public decimal? QuantityReturned { get; set; }

        [FwLogicProperty(Id: "WBpRI49eQPA", IsReadOnly: true)]
        public decimal? PurchaseOrderRate { get; set; }

        [FwLogicProperty(Id: "b3QZZTvyCsc", IsReadOnly: true)]
        public decimal? PurchaseOrderDiscountPercent { get; set; }

        [FwLogicProperty(Id: "CMrMHvNSdUw", IsReadOnly: true)]
        public decimal? PurchaseOrderDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "gfJqeQmWM8z", IsReadOnly: true)]
        public decimal? PurchaseOrderPeriodExtended { get; set; }

        [FwLogicProperty(Id: "iEHTArnpN9D", IsReadOnly: true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id: "nJOB2nFzWGC", IsReadOnly: true)]
        public string LineType { get; set; }

        [FwLogicProperty(Id: "4zF26ClTntC", IsReadOnly: true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id: "RWyGFnBb5Pm", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "qCGYmLVWoFa", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "Dn9ktvrm5hE", IsReadOnly: true)]
        public string ChargeType { get; set; }

        [FwLogicProperty(Id: "AsvuOnFf6zW", IsReadOnly: true)]
        public string PoItemBillingStartDate { get; set; }

        [FwLogicProperty(Id: "4CfoI1oHwyp", IsReadOnly: true)]
        public string PoItemBillingEndDate { get; set; }

        [FwLogicProperty(Id: "HltCLNfSZfa")]
        public string VendorInvoiceId { get { return vendorInvoiceItem.VendorInvoiceId; } set { vendorInvoiceItem.VendorInvoiceId = value; } }
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
