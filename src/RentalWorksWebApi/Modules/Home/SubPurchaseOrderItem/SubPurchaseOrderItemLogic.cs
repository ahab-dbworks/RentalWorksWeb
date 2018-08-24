using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.SubPurchaseOrderItem
{
    public class SubPurchaseOrderItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SubPurchaseOrderItemRecord subPurchaseOrderItem = new SubPurchaseOrderItemRecord();
        SubPurchaseOrderItemLoader subPurchaseOrderItemLoader = new SubPurchaseOrderItemLoader();
        public SubPurchaseOrderItemLogic()
        {
            dataRecords.Add(subPurchaseOrderItem);
            dataLoader = subPurchaseOrderItemLoader;

            ReloadOnSave = false;

        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SessionId { get { return subPurchaseOrderItem.SessionId; } set { subPurchaseOrderItem.SessionId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return subPurchaseOrderItem.OrderId; } set { subPurchaseOrderItem.OrderId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderItemId { get { return subPurchaseOrderItem.OrderItemId; } set { subPurchaseOrderItem.OrderItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NonDiscountable { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsRecurring { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ProrateWeeks { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ProrateMonths { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Prorate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProrateMonthsBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? RecurringRateType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsCrewPositionHourly { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FromDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ToDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Hours { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? OverTimeHours { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DoubleTimeHours { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        public decimal? QuantityOrdered { get { return subPurchaseOrderItem.QuantityOrdered; } set { subPurchaseOrderItem.QuantityOrdered = value; } }
        public decimal? VendorRate { get { return subPurchaseOrderItem.VendorRate; } set { subPurchaseOrderItem.VendorRate = value; } }
        public decimal? VendorDaysPerWeek { get { return subPurchaseOrderItem.VendorDaysPerWeek; } set { subPurchaseOrderItem.VendorDaysPerWeek = value; } }
        public decimal? VendorDiscountPercent { get { return subPurchaseOrderItem.VendorDiscountPercent; } set { subPurchaseOrderItem.VendorDiscountPercent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorDiscountPercentDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorBillablePeriods { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorWeeklySubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorWeeklyDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorWeeklyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorMonthlySubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorMonthlyDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorMonthlyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorPeriodSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorPeriodDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VendorPeriodExtended { get; set; }
        public decimal? DealRate { get { return subPurchaseOrderItem.DealRate; } set { subPurchaseOrderItem.DealRate = value; } }
        public decimal? DealDaysPerWeek { get { return subPurchaseOrderItem.DealDaysPerWeek; } set { subPurchaseOrderItem.DealDaysPerWeek = value; } }
        public decimal? DealDiscountPercent { get { return subPurchaseOrderItem.DealDiscountPercent; } set { subPurchaseOrderItem.DealDiscountPercent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealDiscountPercentDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealBillablePeriods { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealWeeklySubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealWeeklyDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealWeeklyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealMonthlySubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealMonthlyDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealMonthlyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealPeriodSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealPeriodDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealPeriodExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Variance { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MarkupPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MarginPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? OptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Taxable { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UnitId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AccessoryRatio { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CurrencyExchangeRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CurrencyConvertedRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CurrencyConvertedWeeklyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CurrencyConvertedMonthlyExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CurrencyConvertedPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
    }
}
