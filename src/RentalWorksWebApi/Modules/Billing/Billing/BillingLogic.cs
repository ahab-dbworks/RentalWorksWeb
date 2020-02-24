using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Billing.Billing
{
    [FwLogic(Id: "lOnX2rpi2bgT")]
    public class BillingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BillingLoader billingLoader = new BillingLoader();
        public BillingLogic()
        {
            dataLoader = billingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "kr6anlutt9oa", IsReadOnly: true)]
        public string SessionId { get; set; }
        [FwLogicProperty(Id: "6P3IGDJsossi", IsReadOnly: true)]
        public int? BillingId { get; set; }
        [FwLogicProperty(Id: "VH2g1TTjyb2A", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "mpAYmwyuTnCaV", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "b57uo1ETeErbu", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "B8XfHEYKyyEf", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "BTQXYXi9mMRO", IsReadOnly: true)]
        public string FlatPoId { get; set; }
        [FwLogicProperty(Id: "v5RTDvs5pSHr", IsReadOnly: true)]
        public string FlatPoBillingScheduleId { get; set; }
        [FwLogicProperty(Id: "cjR61ytB1EsI", IsReadOnly: true)]
        public string SupplementalPoId { get; set; }
        [FwLogicProperty(Id: "MJCwl9ZX93Ge", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "k6rjo5ooE5z2", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "PaBytHYEWAg0", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "SeEirRpBtvcf", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "WKFU1lTHQNXD", IsReadOnly: true)]
        public string OrderDate { get; set; }
        [FwLogicProperty(Id: "HIPsISkhskiBB", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "kDKIXMEoUnbB", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "wypl39lhefmr", IsReadOnly: true)]
        public string OrderTypeId { get; set; }
        [FwLogicProperty(Id: "6mpKOQYI3znF", IsReadOnly: true)]
        public string OrderTypeType { get; set; }
        [FwLogicProperty(Id: "SWMTTQP4LiUS", IsReadOnly: true)]
        public string BillingCycleId { get; set; }
        [FwLogicProperty(Id: "mEdqS0VBF562", IsReadOnly: true)]
        public string BillingCycle { get; set; }
        [FwLogicProperty(Id: "F9yXrthyFt8T", IsReadOnly: true)]
        public string BillingCycleType { get; set; }
        [FwLogicProperty(Id: "CpFETq4AKDvB", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "DNWfPd0iEc09i", IsReadOnly: true)]
        public string BillingStartDate { get; set; }
        [FwLogicProperty(Id: "6svCe94oZKUF", IsReadOnly: true)]
        public string BillingStopDate { get; set; }
        [FwLogicProperty(Id: "8PFUJfaLik8t", IsReadOnly: true)]
        public string BillAsOfDate { get; set; }
        [FwLogicProperty(Id: "ZObLdfxi2ZAEo", IsReadOnly: true)]
        public bool? IsNoCharge { get; set; }
        [FwLogicProperty(Id: "YZghYCRjTrHJ", IsReadOnly: true)]
        public bool? IsRepair { get; set; }
        [FwLogicProperty(Id: "hj1FZ051zdUE", IsReadOnly: true)]
        public bool? IsFlatPo { get; set; }
        [FwLogicProperty(Id: "paaMchFcNvev0", IsReadOnly: true)]
        public bool? PendingPo { get; set; }
        [FwLogicProperty(Id: "MwrMbX08hF13f", IsReadOnly: true)]
        public string PoNumber { get; set; }
        [FwLogicProperty(Id: "8QgwkDBpUW984", IsReadOnly: true)]
        public decimal? PoAmount { get; set; }
        [FwLogicProperty(Id: "jqjFZshUEBKN", IsReadOnly: true)]
        public string BillingPeriodStartDate { get; set; }
        [FwLogicProperty(Id: "vUETJRg0Y2VtT", IsReadOnly: true)]
        public string BillingPeriodEndDate { get; set; }
        [FwLogicProperty(Id: "tZlcdAfsYTmVY", IsReadOnly: true)]
        public bool? Validchargeno { get; set; }
        [FwLogicProperty(Id: "rT90acK4WVkjo", IsReadOnly: true)]
        public string Orbitsapchgmajor { get; set; }
        [FwLogicProperty(Id: "AIoz76BgGWXJ1", IsReadOnly: true)]
        public string Orbitsapchgsub { get; set; }
        [FwLogicProperty(Id: "EactunBNXGp0", IsReadOnly: true)]
        public string Orbitsapchgdetail { get; set; }
        [FwLogicProperty(Id: "CE5JPeztNz1U", IsReadOnly: true)]
        public string Orbitsapchgdeal { get; set; }
        [FwLogicProperty(Id: "Xm51Dfxxnh5H", IsReadOnly: true)]
        public string Orbitsapchgset { get; set; }
        [FwLogicProperty(Id: "k2WNQVz5YwK7", IsReadOnly: true)]
        public bool? BillingNotes { get; set; }
        [FwLogicProperty(Id: "2aMgX8Bld1Bw", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "mEMzNJjy6VMF", IsReadOnly: true)]
        public string WorksheetId { get; set; }
        [FwLogicProperty(Id: "tXpl2cEkwhcp", IsReadOnly: true)]
        public string BillingCycleEvent { get; set; }
        [FwLogicProperty(Id: "KwK51bhXyI0N", IsReadOnly: true)]
        public int? BillingCycleEventOrder { get; set; }
        [FwLogicProperty(Id: "DmqiZWVhzvOP", IsReadOnly: true)]
        public int? SummaryInvoiceGroup { get; set; }
        [FwLogicProperty(Id: "2qPAr9zA6068", IsReadOnly: true)]
        public bool? DoNotInvoice { get; set; }
        [FwLogicProperty(Id: "VZpDM3tQ1KVG", IsReadOnly: true)]
        public string AgentId { get; set; }
        [FwLogicProperty(Id: "vhvbbP9B13du", IsReadOnly: true)]
        public string Agent { get; set; }
        [FwLogicProperty(Id: "5KoG79cBiraM", IsReadOnly: true)]
        public int? EpisodeNumber { get; set; }
        [FwLogicProperty(Id: "5WPdoraKYQQs", IsReadOnly: true)]
        public bool? IsFinalLossAndDamage { get; set; }
        [FwLogicProperty(Id: "sb93TV1ornOK", IsReadOnly: true)]
        public decimal? BillingTotal { get; set; }
        [FwLogicProperty(Id: "jPfF3SvZLvXgq", IsReadOnly: true)]
        public bool? HasRecurring { get; set; }
        public string ContractId { get; set; }
        [FwLogicProperty(Id: "SQBOplq4iY7I", IsReadOnly: true)]
        public string ReferenceNumber { get; set; }
        [FwLogicProperty(Id: "SJ1flPIBMhpgt", IsReadOnly: true)]
        public bool? BilledHiatus { get; set; }
        [FwLogicProperty(Id: "MeeFWYzcvHGDV", IsReadOnly: true)]
        public bool? MissingCrewBreakTime { get; set; }
        [FwLogicProperty(Id: "TelZxdsEaj2xl", IsReadOnly: true)]
        public bool? MissingCrewWorkTime { get; set; }
        [FwLogicProperty(Id: "LadKW3tZYtRZ", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "AJaODabpRknh", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "KIAJkKCEWagaK", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "ikmVHI6K9Milm", IsReadOnly: true)]
        public string ProjectManagerId { get; set; }
        [FwLogicProperty(Id: "qwtJ2S6quPOfV", IsReadOnly: true)]
        public string ProjectManager { get; set; }
        [FwLogicProperty(Id: "RhpgIBPng2FrM", IsReadOnly: true)]
        public string OutsideSalesRepresentativeId { get; set; }
        [FwLogicProperty(Id: "FljWrG1LF0R2y", IsReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }


        //colors

        [FwLogicProperty(Id: "doqkwuvY4pK", IsReadOnly: true)]
        public string OrderNumberColor { get; set; }
        [FwLogicProperty(Id: "ByhexPRANAd", IsReadOnly: true)]
        public string DescriptionColor { get; set; }
        [FwLogicProperty(Id: "Rv1NsrtwODD", IsReadOnly: true)]
        public string BillingStopDateColor { get; set; }
        [FwLogicProperty(Id: "2ZzGyL00jt9", IsReadOnly: true)]
        public string OrderDateColor { get; set; }
        [FwLogicProperty(Id: "r6hltu0uCfn", IsReadOnly: true)]
        public string PurchaseOrderNumberColor { get; set; }
        [FwLogicProperty(Id: "OeUrQVPvUdU", IsReadOnly: true)]
        public string TotalColor { get; set; }

        //------------------------------------------------------------------------------------ 



        //------------------------------------------------------------------------------------ 
    }
}
