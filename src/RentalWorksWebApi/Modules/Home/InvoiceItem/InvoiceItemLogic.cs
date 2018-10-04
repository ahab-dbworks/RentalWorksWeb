using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.InvoiceItem
{
    public class InvoiceItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceItemRecord invoiceItem = new InvoiceItemRecord();
        InvoiceItemLoader invoiceItemLoader = new InvoiceItemLoader();
        public InvoiceItemLogic()
        {
            dataRecords.Add(invoiceItem);
            dataLoader = invoiceItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InvoiceItemId { get { return invoiceItem.InvoiceItemId; } set { invoiceItem.InvoiceItemId = value; } }
        public string InvoiceId { get { return invoiceItem.InvoiceId; } set { invoiceItem.InvoiceId = value; } }
        public string ItemId { get { return invoiceItem.ItemId; } set { invoiceItem.ItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        public string InventoryId { get { return invoiceItem.InventoryId; } set { invoiceItem.InventoryId = value; } }
        public string OrderId { get { return invoiceItem.OrderId; } set { invoiceItem.OrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        public string RepairId { get { return invoiceItem.RepairId; } set { invoiceItem.RepairId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RepairNumber { get; set; }
        public string RecType { get { return invoiceItem.RecType; } set { invoiceItem.RecType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Bold { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OptionColor { get; set; }
        public string OrderItemId { get { return invoiceItem.OrderItemId; } set { invoiceItem.OrderItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        public bool? IsAdjusted { get { return invoiceItem.IsAdjusted; } set { invoiceItem.IsAdjusted = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsRecurring { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsManualBill { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VoidInvoiceItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsProfitCenter { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitCenterChargeCode1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitCenterChargeCode2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitCenterChargeCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Activity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ActivityExportCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        public string Description { get { return invoiceItem.Description; } set { invoiceItem.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        public string FromDate { get { return invoiceItem.FromDate; } set { invoiceItem.FromDate = value; } }
        public string FromTime { get { return invoiceItem.FromTime; } set { invoiceItem.FromTime = value; } }
        public string ToDate { get { return invoiceItem.ToDate; } set { invoiceItem.ToDate = value; } }
        public string ToTime { get { return invoiceItem.ToTime; } set { invoiceItem.ToTime = value; } }
        public decimal? Days { get { return invoiceItem.Days; } set { invoiceItem.Days = value; } }
        public decimal? Quantity { get { return invoiceItem.Quantity; } set { invoiceItem.Quantity = value; } }
        public decimal? Cost { get { return invoiceItem.Cost; } set { invoiceItem.Cost = value; } }
        public decimal? Rate { get { return invoiceItem.Rate; } set { invoiceItem.Rate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }
        public decimal? DaysPerWeek { get { return invoiceItem.DaysPerWeek; } set { invoiceItem.DaysPerWeek = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get { return invoiceItem.DiscountAmount; } set { invoiceItem.DiscountAmount = value; } }
        public int? Split { get { return invoiceItem.Split; } set { invoiceItem.Split = value; } }
        public decimal? Hours { get { return invoiceItem.Hours; } set { invoiceItem.Hours = value; } }
        public decimal? HoursOvertime { get { return invoiceItem.HoursOvertime; } set { invoiceItem.HoursOvertime = value; } }
        public decimal? HoursDoubletime { get { return invoiceItem.HoursDoubletime; } set { invoiceItem.HoursDoubletime = value; } }
        public bool? CrewActualHours { get { return invoiceItem.CrewActualHours; } set { invoiceItem.CrewActualHours = value; } }
        public decimal? MeterOut { get { return invoiceItem.MeterOut; } set { invoiceItem.MeterOut = value; } }
        public decimal? MeterIn { get { return invoiceItem.MeterIn; } set { invoiceItem.MeterIn = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Extended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LineTotal { get; set; }
        public bool? Taxable { get { return invoiceItem.Taxable; } set { invoiceItem.Taxable = value; } }
        public decimal? Adjustment { get { return invoiceItem.Adjustment; } set { invoiceItem.Adjustment = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RebateAmount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AdjustedRevenue { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuikPayExtended { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
