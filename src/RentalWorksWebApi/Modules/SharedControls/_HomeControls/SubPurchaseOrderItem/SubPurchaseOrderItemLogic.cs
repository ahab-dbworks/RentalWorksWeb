using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.SubPurchaseOrderItem
{
    [FwLogic(Id:"KmFrofZhOs5nE")]
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
            LoadOriginalBeforeSaving = false;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"aW7iIxiD5eSQs", IsPrimaryKey:true)]
        public string SessionId { get { return subPurchaseOrderItem.SessionId; } set { subPurchaseOrderItem.SessionId = value; } }

        [FwLogicProperty(Id:"wF4lz8g2uG6w9", IsPrimaryKey:true)]
        public string OrderId { get { return subPurchaseOrderItem.OrderId; } set { subPurchaseOrderItem.OrderId = value; } }

        [FwLogicProperty(Id:"TgPBfvVlj1ldB", IsPrimaryKey:true)]
        public string OrderItemId { get { return subPurchaseOrderItem.OrderItemId; } set { subPurchaseOrderItem.OrderItemId = value; } }

        [FwLogicProperty(Id:"aQXvJq1Xz86Er", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"qTY0Tn5CvouOt", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"hc78sY6xvU75V", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"hc78sY6xvU75V", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"ebzFSzo2FuB13", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"ebzFSzo2FuB13", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"SoSVrMlfnJyEn", IsReadOnly:true)]
        public bool? NonDiscountable { get; set; }

        [FwLogicProperty(Id:"hVMP4nWDKQTcw", IsReadOnly:true)]
        public bool? IsRecurring { get; set; }

        [FwLogicProperty(Id:"U1Zd12INnEmBZ", IsReadOnly:true)]
        public bool? ProrateWeeks { get; set; }

        [FwLogicProperty(Id:"U1Zd12INnEmBZ", IsReadOnly:true)]
        public bool? ProrateMonths { get; set; }

        [FwLogicProperty(Id:"U1Zd12INnEmBZ", IsReadOnly:true)]
        public bool? Prorate { get; set; }

        [FwLogicProperty(Id:"U1Zd12INnEmBZ", IsReadOnly:true)]
        public string ProrateMonthsBy { get; set; }

        [FwLogicProperty(Id:"rllDf22gRKsHY", IsReadOnly:true)]
        public bool? RecurringRateType { get; set; }

        [FwLogicProperty(Id:"flkBW97yW8x4I", IsReadOnly:true)]
        public bool? IsCrewPositionHourly { get; set; }

        [FwLogicProperty(Id:"tSa5lWPtsxPP4", IsReadOnly:true)]
        public string FromDate { get; set; }

        [FwLogicProperty(Id:"mzUj2TkzuFdgb", IsReadOnly:true)]
        public string ToDate { get; set; }

        [FwLogicProperty(Id:"ZC27u0WKxzVbn", IsReadOnly:true)]
        public decimal? Hours { get; set; }

        [FwLogicProperty(Id:"ZC27u0WKxzVbn", IsReadOnly:true)]
        public decimal? OverTimeHours { get; set; }

        [FwLogicProperty(Id:"n7UtJCfZGA9Px", IsReadOnly:true)]
        public decimal? DoubleTimeHours { get; set; }

        [FwLogicProperty(Id:"V7fDyHGfNI774", IsReadOnly:true)]
        public decimal? SubQuantity { get; set; }

        [FwLogicProperty(Id:"JWpoOXNvXQoZ")]
        public decimal? QuantityOrdered { get { return subPurchaseOrderItem.QuantityOrdered; } set { subPurchaseOrderItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id:"nqRLX6Cc2lzM")]
        public decimal? VendorRate { get { return subPurchaseOrderItem.VendorRate; } set { subPurchaseOrderItem.VendorRate = value; } }

        [FwLogicProperty(Id:"AZuYnfSFONdw")]
        public decimal? VendorDaysPerWeek { get { return subPurchaseOrderItem.VendorDaysPerWeek; } set { subPurchaseOrderItem.VendorDaysPerWeek = value; } }

        [FwLogicProperty(Id:"q7e0PD00KwdQ")]
        public decimal? VendorDiscountPercent { get { return subPurchaseOrderItem.VendorDiscountPercent; } set { subPurchaseOrderItem.VendorDiscountPercent = value; } }

        [FwLogicProperty(Id:"cJ3X6hzSbd8tE", IsReadOnly:true)]
        public decimal? VendorDiscountPercentDisplay { get; set; }

        [FwLogicProperty(Id:"RlxAwRHvauiI3", IsReadOnly:true)]
        public decimal? VendorBillablePeriods { get; set; }

        [FwLogicProperty(Id:"dg0R6e9n1prKu", IsReadOnly:true)]
        public decimal? VendorWeeklySubTotal { get; set; }

        [FwLogicProperty(Id:"jlXMdeZ2Q869f", IsReadOnly:true)]
        public decimal? VendorWeeklyDiscount { get; set; }

        [FwLogicProperty(Id:"Z4GWzwW4dAf4p", IsReadOnly:true)]
        public decimal? VendorWeeklyExtended { get; set; }

        [FwLogicProperty(Id: "f95VSrblr5PWA", IsReadOnly: true)]
        public decimal? VendorWeeklyTax { get; set; }

        [FwLogicProperty(Id: "zzskd4E5CDaR9", IsReadOnly: true)]
        public decimal? VendorWeeklyTotal { get; set; }

        [FwLogicProperty(Id:"kBI5bzXVJQwqW", IsReadOnly:true)]
        public decimal? VendorMonthlySubTotal { get; set; }

        [FwLogicProperty(Id:"07pjtGbOazuuZ", IsReadOnly:true)]
        public decimal? VendorMonthlyDiscount { get; set; }

        [FwLogicProperty(Id:"pbNFh7nIlfxeR", IsReadOnly:true)]
        public decimal? VendorMonthlyExtended { get; set; }

        [FwLogicProperty(Id: "FLrMEn8zlvmhN", IsReadOnly: true)]
        public decimal? VendorMonthlyTax { get; set; }

        [FwLogicProperty(Id: "jpF5uBkhmHsrb", IsReadOnly: true)]
        public decimal? VendorMonthlyTotal { get; set; }

        [FwLogicProperty(Id:"glYybVTk0BmBw", IsReadOnly:true)]
        public decimal? VendorPeriodSubTotal { get; set; }

        [FwLogicProperty(Id:"q2CW9Cn7zVFuu", IsReadOnly:true)]
        public decimal? VendorPeriodDiscount { get; set; }

        [FwLogicProperty(Id:"CYhNXwTQBnZVy", IsReadOnly:true)]
        public decimal? VendorPeriodExtended { get; set; }

        [FwLogicProperty(Id: "uC0JOzESI4VQk", IsReadOnly: true)]
        public decimal? VendorPeriodTax { get; set; }

        [FwLogicProperty(Id: "usgHE7vopwksr", IsReadOnly: true)]
        public decimal? VendorPeriodTotal { get; set; }

        [FwLogicProperty(Id:"eJExRXZQL8fa")]
        public decimal? DealRate { get { return subPurchaseOrderItem.DealRate; } set { subPurchaseOrderItem.DealRate = value; } }

        [FwLogicProperty(Id:"WpmgCdYMsz2u")]
        public decimal? DealDaysPerWeek { get { return subPurchaseOrderItem.DealDaysPerWeek; } set { subPurchaseOrderItem.DealDaysPerWeek = value; } }

        [FwLogicProperty(Id:"QQKdQNhH0OZV")]
        public decimal? DealDiscountPercent { get { return subPurchaseOrderItem.DealDiscountPercent; } set { subPurchaseOrderItem.DealDiscountPercent = value; } }

        [FwLogicProperty(Id:"57kzl8CbqJN4t", IsReadOnly:true)]
        public decimal? DealDiscountPercentDisplay { get; set; }

        [FwLogicProperty(Id:"HT0H3dLnkXIVu", IsReadOnly:true)]
        public decimal? DealBillablePeriods { get; set; }

        [FwLogicProperty(Id:"o4e8K4hA82dUm", IsReadOnly:true)]
        public decimal? DealWeeklySubTotal { get; set; }

        [FwLogicProperty(Id:"CB9rwYpdSb52A", IsReadOnly:true)]
        public decimal? DealWeeklyDiscount { get; set; }

        [FwLogicProperty(Id:"eXBUskX8Xa3Gf", IsReadOnly:true)]
        public decimal? DealWeeklyExtended { get; set; }

        [FwLogicProperty(Id:"v7I13LH8OmYaW", IsReadOnly:true)]
        public decimal? DealMonthlySubTotal { get; set; }

        [FwLogicProperty(Id:"LKAj1KLWV5wfw", IsReadOnly:true)]
        public decimal? DealMonthlyDiscount { get; set; }

        [FwLogicProperty(Id:"1rW6bZmgo0vLT", IsReadOnly:true)]
        public decimal? DealMonthlyExtended { get; set; }

        [FwLogicProperty(Id:"wJX66gDvGS2Kt", IsReadOnly:true)]
        public decimal? DealPeriodSubTotal { get; set; }

        [FwLogicProperty(Id:"KEoj2poNOC2Q3", IsReadOnly:true)]
        public decimal? DealPeriodDiscount { get; set; }

        [FwLogicProperty(Id:"JEEkWmFASrBEK", IsReadOnly:true)]
        public decimal? DealPeriodExtended { get; set; }

        [FwLogicProperty(Id:"JfFcpQCvOMLnT", IsReadOnly:true)]
        public decimal? Variance { get; set; }

        [FwLogicProperty(Id:"JfFcpQCvOMLnT", IsReadOnly:true)]
        public string VarianceColor { get; set; }

        [FwLogicProperty(Id:"ATPMGBeXhnOkj", IsReadOnly:true)]
        public decimal? MarkupPercent { get; set; }

        [FwLogicProperty(Id:"7m8EfT4AgXCud", IsReadOnly:true)]
        public decimal? MarginPercent { get; set; }

        [FwLogicProperty(Id:"SiKsIPLQMSX4v", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"rMjwO4wnJFkNn", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"5KaLkLCEvwA0s", IsReadOnly:true)]
        public bool? OptionColor { get; set; }

        [FwLogicProperty(Id:"sNVLlwKCFIxS8", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"nSidenj6kGrIh", IsReadOnly:true)]
        public bool? Taxable { get; set; }

        [FwLogicProperty(Id:"5oMEzEr2jcjQr", IsReadOnly:true)]
        public string UnitId { get; set; }

        [FwLogicProperty(Id:"oDAuRLU2Jm7Cs", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"tXUBZ8QSVj0sL", IsReadOnly:true)]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"Xik4Xg7MkV9S9", IsReadOnly:true)]
        public string VendorCurrencyId { get; set; }

        [FwLogicProperty(Id:"sm7qIeOTpBPd9", IsReadOnly:true)]
        public string DealCurrencyId { get; set; }

        [FwLogicProperty(Id:"zJ4lzlnIA7bo2", IsReadOnly:true)]
        public decimal? CurrencyExchangeRate { get; set; }

        [FwLogicProperty(Id:"GejfrJdaupzjp", IsReadOnly:true)]
        public decimal? CurrencyConvertedRate { get; set; }

        [FwLogicProperty(Id:"XvxNHKBCPukrQ", IsReadOnly:true)]
        public decimal? CurrencyConvertedWeeklyExtended { get; set; }

        [FwLogicProperty(Id:"q8WkDqoQvi0ba", IsReadOnly:true)]
        public decimal? CurrencyConvertedMonthlyExtended { get; set; }

        [FwLogicProperty(Id:"G5XNEg4E7XhA3", IsReadOnly:true)]
        public decimal? CurrencyConvertedPeriodExtended { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
