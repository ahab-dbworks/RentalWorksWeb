using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InvoiceItem
{
    [FwLogic(Id:"K9CwMt5dnPp4")]
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
        [FwLogicProperty(Id:"t7Nt74q1FIYB", IsPrimaryKey:true)]
        public string InvoiceItemId { get { return invoiceItem.InvoiceItemId; } set { invoiceItem.InvoiceItemId = value; } }

        [FwLogicProperty(Id:"0NZoPqGArO3z")]
        public string InvoiceId { get { return invoiceItem.InvoiceId; } set { invoiceItem.InvoiceId = value; } }

        [FwLogicProperty(Id:"oWh9EM84SCuJ")]
        public string ItemId { get { return invoiceItem.ItemId; } set { invoiceItem.ItemId = value; } }

        [FwLogicProperty(Id:"8bJ4wT7zOIJ5", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"GdlQK243CGjf", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"jae7SrwoERba")]
        public string InventoryId { get { return invoiceItem.InventoryId; } set { invoiceItem.InventoryId = value; } }

        [FwLogicProperty(Id:"9lWqOnOkjV8l")]
        public string OrderId { get { return invoiceItem.OrderId; } set { invoiceItem.OrderId = value; } }

        [FwLogicProperty(Id:"1KkeYUhL2gTB", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"mk2g35Dfp4AN")]
        public string RepairId { get { return invoiceItem.RepairId; } set { invoiceItem.RepairId = value; } }

        [FwLogicProperty(Id:"adto4S6mElFe", IsReadOnly:true)]
        public string RepairNumber { get; set; }

        [FwLogicProperty(Id:"D3gWInKrmg9O")]
        public string RecType { get { return invoiceItem.RecType; } set { invoiceItem.RecType = value; } }

        [FwLogicProperty(Id: "ohz64hiDWe2BF", IsReadOnly: true)]
        public string AvailableFor { get; set; }

        [FwLogicProperty(Id:"Wx4VHmSLkZgB", IsReadOnly:true)]
        public bool? Bold { get; set; }

        [FwLogicProperty(Id:"lca58uXMipfh", IsReadOnly:true)]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"wqEatlxAjmLT")]
        public string OrderItemId { get { return invoiceItem.OrderItemId; } set { invoiceItem.OrderItemId = value; } }

        [FwLogicProperty(Id:"YdIxYTc3hVb4", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"Da82BM6TPUSn")]
        public bool? IsAdjusted { get { return invoiceItem.IsAdjusted; } set { invoiceItem.IsAdjusted = value; } }

        [FwLogicProperty(Id:"KO22BY8dnRcc", IsReadOnly:true)]
        public bool? IsRecurring { get; set; }

        [FwLogicProperty(Id:"ftpSvwwnHkkT", IsReadOnly:true)]
        public bool? IsManualBill { get; set; }

        [FwLogicProperty(Id:"CkIa16Wmyt8H", IsReadOnly:true)]
        public string VoidInvoiceItemId { get; set; }

        [FwLogicProperty(Id:"6YvxZn4AWB1y", IsReadOnly:true)]
        public bool? IsProfitCenter { get; set; }

        [FwLogicProperty(Id:"eeyruqARyzlM", IsReadOnly:true)]
        public string ProfitCenterChargeCode1 { get; set; }

        [FwLogicProperty(Id:"dct95Ra3DyR0", IsReadOnly:true)]
        public string ProfitCenterChargeCode2 { get; set; }

        [FwLogicProperty(Id:"yDTyxvISgDh1", IsReadOnly:true)]
        public string ProfitCenterChargeCode { get; set; }

        [FwLogicProperty(Id:"4nr6xjLJlSF7", IsReadOnly:true)]
        public string Activity { get; set; }

        [FwLogicProperty(Id:"rAlQuzrhBJ8D", IsReadOnly:true)]
        public string ActivityExportCode { get; set; }

        [FwLogicProperty(Id:"gXcvyflnlK6Q", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"JmLAGRBtlQvD", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"e9FEjuRcyuGE")]
        public string Description { get { return invoiceItem.Description; } set { invoiceItem.Description = value; } }

        [FwLogicProperty(Id:"IaLtmKaf1ac0", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"nU32XRj23qVe", IsReadOnly:true)]
        public string SerialNumber { get; set; }

        [FwLogicProperty(Id:"1qftIsh1CAoD")]
        public string FromDate { get { return invoiceItem.FromDate; } set { invoiceItem.FromDate = value; } }

        [FwLogicProperty(Id:"xBHACbFcYs9W")]
        public string FromTime { get { return invoiceItem.FromTime; } set { invoiceItem.FromTime = value; } }

        [FwLogicProperty(Id:"nI6m5P4FTc6z")]
        public string ToDate { get { return invoiceItem.ToDate; } set { invoiceItem.ToDate = value; } }

        [FwLogicProperty(Id:"Bwt4mkVhBxRn")]
        public string ToTime { get { return invoiceItem.ToTime; } set { invoiceItem.ToTime = value; } }

        [FwLogicProperty(Id:"M6y94SowuIzf")]
        public decimal? Days { get { return invoiceItem.Days; } set { invoiceItem.Days = value; } }

        [FwLogicProperty(Id:"8bZTzc9XWDjk")]
        public decimal? Quantity { get { return invoiceItem.Quantity; } set { invoiceItem.Quantity = value; } }

        [FwLogicProperty(Id:"cN3NHMLcWeO5")]
        public decimal? Cost { get { return invoiceItem.Cost; } set { invoiceItem.Cost = value; } }

        [FwLogicProperty(Id:"coTYsMaLOMBB")]
        public decimal? Rate { get { return invoiceItem.Rate; } set { invoiceItem.Rate = value; } }

        [FwLogicProperty(Id:"j6EOt7z9vJZa", IsReadOnly:true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id:"YuGazLnuo34w")]
        public decimal? DaysPerWeek { get { return invoiceItem.DaysPerWeek; } set { invoiceItem.DaysPerWeek = value; } }

        [FwLogicProperty(Id:"KfWOJvA7rwic", IsReadOnly:true)]
        public decimal? DiscountPercent { get; set; }

        [FwLogicProperty(Id:"oMyVexXMzbjp")]
        public decimal? DiscountAmount { get { return invoiceItem.DiscountAmount; } set { invoiceItem.DiscountAmount = value; } }

        [FwLogicProperty(Id:"N4GM0wK34Bvs")]
        public int? Split { get { return invoiceItem.Split; } set { invoiceItem.Split = value; } }

        [FwLogicProperty(Id:"p93NHNAcKSmI")]
        public decimal? Hours { get { return invoiceItem.Hours; } set { invoiceItem.Hours = value; } }

        [FwLogicProperty(Id:"XXPYNMLoBkt9")]
        public decimal? HoursOvertime { get { return invoiceItem.HoursOvertime; } set { invoiceItem.HoursOvertime = value; } }

        [FwLogicProperty(Id:"GzHAB7dP7VUg")]
        public decimal? HoursDoubletime { get { return invoiceItem.HoursDoubletime; } set { invoiceItem.HoursDoubletime = value; } }

        [FwLogicProperty(Id:"0rryhslt4BAT")]
        public bool? CrewActualHours { get { return invoiceItem.CrewActualHours; } set { invoiceItem.CrewActualHours = value; } }

        [FwLogicProperty(Id:"q3RwtcnBUbOH")]
        public decimal? MeterOut { get { return invoiceItem.MeterOut; } set { invoiceItem.MeterOut = value; } }

        [FwLogicProperty(Id:"YWkgmTTPwQr9")]
        public decimal? MeterIn { get { return invoiceItem.MeterIn; } set { invoiceItem.MeterIn = value; } }

        [FwLogicProperty(Id: "X8bPXdSBt0amt", IsReadOnly: true)]
        public decimal? LineTotalBeforeDiscount { get; set; }

        [FwLogicProperty(Id:"bh5uBQAZc6Db", IsReadOnly:true)]
        public decimal? Extended { get; set; }

        [FwLogicProperty(Id:"G3U9qxwVLKfb", IsReadOnly:true)]
        public decimal? LineTotal { get; set; }

        [FwLogicProperty(Id:"4bD2Ejj0dsVG")]
        public bool? Taxable { get { return invoiceItem.Taxable; } set { invoiceItem.Taxable = value; } }

        [FwLogicProperty(Id: "JIhwZoleX2ti8", IsReadOnly: true)]
        public decimal? Tax { get; set; }

        [FwLogicProperty(Id: "K4EkPdPU2v4Fb", IsReadOnly: true)]
        public decimal? LineTotalWithTax { get; set; }

        [FwLogicProperty(Id:"uMrfuVdo6XgL")]
        public decimal? Adjustment { get { return invoiceItem.Adjustment; } set { invoiceItem.Adjustment = value; } }

        [FwLogicProperty(Id:"226GyH6NXJOL", IsReadOnly:true)]
        public decimal? RebateAmount { get; set; }

        [FwLogicProperty(Id:"sdzpZxbwxQtC", IsReadOnly:true)]
        public decimal? AdjustedRevenue { get; set; }

        [FwLogicProperty(Id:"dcbXIxrGYZyK", IsReadOnly:true)]
        public decimal? QuikPayExtended { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
