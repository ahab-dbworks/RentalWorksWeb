using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.PurchaseOrderPaymentSchedule
{
    [FwLogic(Id: "NjengPKyouohA")]
    public class PurchaseOrderPaymentScheduleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseOrderPaymentScheduleLoader purchaseOrderPaymentScheduleLoader = new PurchaseOrderPaymentScheduleLoader();
        public PurchaseOrderPaymentScheduleLogic()
        {
            dataLoader = purchaseOrderPaymentScheduleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "njvpc3X1VA0Xa", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "NkA65teTH3n9D", IsReadOnly: true)]
        public string EpisodeId { get; set; }
        [FwLogicProperty(Id: "nKCcGsNFhspDm", IsReadOnly: true)]
        public int? EpisodeNumber { get; set; }
        [FwLogicProperty(Id: "nkeAf2un8dH7N", IsReadOnly: true)]
        public string FromDate { get; set; }
        [FwLogicProperty(Id: "nKlzqonTv1J3D", IsReadOnly: true)]
        public string ToDate { get; set; }
        [FwLogicProperty(Id: "NkSlqrvbevxO5", IsReadOnly: true)]
        public bool? BillWeekends { get; set; }
        [FwLogicProperty(Id: "NKzayjhkPsayY", IsReadOnly: true)]
        public bool? BillHolidays { get; set; }
        [FwLogicProperty(Id: "nKZmN6SLKA9ic", IsReadOnly: true)]
        public decimal? BillableDays { get; set; }
        [FwLogicProperty(Id: "Nl1HvyY4OGz26", IsReadOnly: true)]
        public bool? IsHiatus { get; set; }
        [FwLogicProperty(Id: "nl5AztvdAYSuc", IsReadOnly: true)]
        public decimal? HiatusDiscountPercent { get; set; }
        [FwLogicProperty(Id: "nldTF7zoT7qxf", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        [FwLogicProperty(Id: "NLMPa6u0yL4Wb", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "NmjLRQOEj0b2X", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "NmLZVhlotmQdw", IsReadOnly: true)]
        public decimal? TotalBeforeOverride { get; set; }
        [FwLogicProperty(Id: "NMmTGiroDR0tm", IsReadOnly: true)]
        public decimal? GrossTotal { get; set; }
        [FwLogicProperty(Id: "nnAj23QPJQ7kq", IsReadOnly: true)]
        public decimal? DiscountAmount { get; set; }
        [FwLogicProperty(Id: "nNaKiXDsAMzAC", IsReadOnly: true)]
        public decimal? SubTotal { get; set; }
        [FwLogicProperty(Id: "NNAQEJ4TXuxHr", IsReadOnly: true)]
        public decimal? Tax1 { get; set; }
        [FwLogicProperty(Id: "nNB4emhXyc00J", IsReadOnly: true)]
        public decimal? Tax2 { get; set; }
        [FwLogicProperty(Id: "nncKfa1YkzhRW", IsReadOnly: true)]
        public decimal? TaxTotal { get; set; }
        [FwLogicProperty(Id: "NOdNFvE5Obp8Z", IsReadOnly: true)]
        public decimal? Total { get; set; }
        [FwLogicProperty(Id: "nohOSMrLHlo1X", IsReadOnly: true)]
        public decimal? MeterSubTotal { get; set; }
        [FwLogicProperty(Id: "NPeUBmVJDzl7L", IsReadOnly: true)]
        public decimal? MeterDiscountAmount { get; set; }
        [FwLogicProperty(Id: "NQCeDktjOrkp9", IsReadOnly: true)]
        public decimal? MeterTax { get; set; }
        [FwLogicProperty(Id: "NrB927P2fbVJ5", IsReadOnly: true)]
        public decimal? MeterTotal { get; set; }
        [FwLogicProperty(Id: "nRdvyUGLzsodY", IsReadOnly: true)]
        public int? PeriodNumber { get; set; }
        [FwLogicProperty(Id: "NRL3DGwVjh1Z4", IsReadOnly: true)]
        public decimal? BillPercent { get; set; }
        [FwLogicProperty(Id: "nRyl0xIghsMhz", IsReadOnly: true)]
        public decimal? AdjustedBillTotal { get; set; }
        [FwLogicProperty(Id: "Nsds44UJ1IZUW", IsReadOnly: true)]
        public bool? CreateRebateInvoice { get; set; }
        [FwLogicProperty(Id: "nsh8M0xR1G7JQ", IsReadOnly: true)]
        public decimal? RebatePercent { get; set; }
        [FwLogicProperty(Id: "nT0DwVZ5cOQAD", IsReadOnly: true)]
        public decimal? RebateAmount { get; set; }
        [FwLogicProperty(Id: "NTSzRyYhCcmFo", IsReadOnly: true)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
