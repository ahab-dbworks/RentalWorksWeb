using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Billing.OrderBillingSchedule
{
    [FwLogic(Id: "6MSyes7DUrOP")]
    public class OrderBillingScheduleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderBillingScheduleLoader orderBillingScheduleLoader = new OrderBillingScheduleLoader();
        public OrderBillingScheduleLogic()
        {
            dataLoader = orderBillingScheduleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "P05ygVEVeyUZ", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "bRrGShpi2hd3", IsReadOnly: true)]
        public string EpisodeId { get; set; }
        [FwLogicProperty(Id: "oLWXJNREZ5UA", IsReadOnly: true)]
        public int? EpisodeNumber { get; set; }
        [FwLogicProperty(Id: "RsegBz2Gof81", IsReadOnly: true)]
        public string FromDate { get; set; }
        [FwLogicProperty(Id: "223o7x2L2RpR", IsReadOnly: true)]
        public string ToDate { get; set; }
        [FwLogicProperty(Id: "8eXafiO8TQOw", IsReadOnly: true)]
        public bool? BillWeekends { get; set; }
        [FwLogicProperty(Id: "g6tGYJmnFT6Z", IsReadOnly: true)]
        public bool? BillHolidays { get; set; }
        [FwLogicProperty(Id: "t0Jl4GQszmZC", IsReadOnly: true)]
        public decimal? BillableDays { get; set; }
        [FwLogicProperty(Id: "zo4hSgnDKT9h", IsReadOnly: true)]
        public bool? IsHiatus { get; set; }
        [FwLogicProperty(Id: "7tQVrqu4NM2n", IsReadOnly: true)]
        public decimal? HiatusDiscountPercent { get; set; }
        [FwLogicProperty(Id: "sd9RV9Z3yDmN", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        [FwLogicProperty(Id: "w7SZhTXV3Yvs", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "bRDIenx1afPo", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "ZpIsqGlBoW93", IsReadOnly: true)]
        public decimal? TotalBeforeOverride { get; set; }
        [FwLogicProperty(Id: "54B7mw0UTaKb", IsReadOnly: true)]
        public decimal? GrossTotal { get; set; }
        [FwLogicProperty(Id: "HNHzIJqKJJnx", IsReadOnly: true)]
        public decimal? DiscountAmount { get; set; }
        [FwLogicProperty(Id: "dRrTdc4y1iQu", IsReadOnly: true)]
        public decimal? SubTotal { get; set; }
        [FwLogicProperty(Id: "PHcKfQKm8IfS", IsReadOnly: true)]
        public decimal? Tax1 { get; set; }
        [FwLogicProperty(Id: "jdzmbJJ7AJJS", IsReadOnly: true)]
        public decimal? Tax2 { get; set; }
        [FwLogicProperty(Id: "qYI92akaLvbE", IsReadOnly: true)]
        public decimal? TaxTotal { get; set; }
        [FwLogicProperty(Id: "aAdn8izKihVP", IsReadOnly: true)]
        public decimal? Total { get; set; }
        [FwLogicProperty(Id: "HHIaqWWwS0fb", IsReadOnly: true)]
        public decimal? MeterSubTotal { get; set; }
        [FwLogicProperty(Id: "C2QESBZcVY8i", IsReadOnly: true)]
        public decimal? MeterDiscountAmount { get; set; }
        [FwLogicProperty(Id: "kYT1KARr0H2J", IsReadOnly: true)]
        public decimal? MeterTax { get; set; }
        [FwLogicProperty(Id: "FKDczqXhv6Nx", IsReadOnly: true)]
        public decimal? MeterTotal { get; set; }
        [FwLogicProperty(Id: "R1S7cdKzthry", IsReadOnly: true)]
        public int? PeriodNumber { get; set; }
        [FwLogicProperty(Id: "jjXKNLA5gXng", IsReadOnly: true)]
        public decimal? BillPercent { get; set; }
        [FwLogicProperty(Id: "CjElEWDpybbB", IsReadOnly: true)]
        public decimal? AdjustedBillTotal { get; set; }
        [FwLogicProperty(Id: "iyDbqiC286w5", IsReadOnly: true)]
        public bool? CreateRebateInvoice { get; set; }
        [FwLogicProperty(Id: "9HvRBxqzU14K", IsReadOnly: true)]
        public decimal? RebatePercent { get; set; }
        [FwLogicProperty(Id: "xzlJCA8teeyd", IsReadOnly: true)]
        public decimal? RebateAmount { get; set; }
        [FwLogicProperty(Id: "X6ctB067myrG", IsReadOnly: true)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
