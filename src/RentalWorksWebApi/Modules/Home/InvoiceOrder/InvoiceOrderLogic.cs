using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.InvoiceOrder
{
    [FwLogic(Id: "ecOHiUtFuiLS")]
    public class InvoiceOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceOrderLoader invoiceOrderLoader = new InvoiceOrderLoader();
        public InvoiceOrderLogic()
        {
            dataLoader = invoiceOrderLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "k4tDWPdQxsZ6", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "8y9YjLOMCoC6", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "OyBT0U0tSoXj", IsReadOnly: true)]
        public string ReferenceNumber { get; set; }
        [FwLogicProperty(Id: "8CgTHkvDY0HI", IsReadOnly: true)]
        public string OrderLocation { get; set; }
        [FwLogicProperty(Id: "Bfb3BGvL9RFM", IsReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwLogicProperty(Id: "iByDVnCph2iMe", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "enIRUsj7LawYG", IsReadOnly: true)]
        public string Agent { get; set; }
        [FwLogicProperty(Id: "EaBnbUHq3TRpb", IsReadOnly: true)]
        public string ProjectManager { get; set; }
        [FwLogicProperty(Id: "LwYHJGoUCup4L", IsReadOnly: true)]
        public string BillingStartDate { get; set; }
        [FwLogicProperty(Id: "OZaqD80qyVhv", IsReadOnly: true)]
        public string BillingEndDate { get; set; }
        [FwLogicProperty(Id: "YJkmtkK0m3Gm", IsReadOnly: true)]
        public string PoNumber { get; set; }
        [FwLogicProperty(Id: "LcFu22KourA0", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "2CYIfkx3Khqk", IsReadOnly: true)]
        public string OrderInvoiceId { get; set; }
        [FwLogicProperty(Id: "AInT8qsytCkg", IsReadOnly: true)]
        public string FlatPoId { get; set; }
        [FwLogicProperty(Id: "CU5PWxMG431G", IsReadOnly: true)]
        public bool? ExcludeFromFlatPO { get; set; }
        [FwLogicProperty(Id: "IJez6hnhsfEA3", IsReadOnly: true)]
        public bool? BillableFlat { get; set; }
        [FwLogicProperty(Id: "6mMihkwPFlZP", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "Qcz9sH24IHzbc", IsReadOnly: true)]
        public decimal? SummaryInvoiceOrderBy { get; set; }
        [FwLogicProperty(Id: "ucJyiUZNk1C3", IsReadOnly: true)]
        public decimal? RentalSubTotal { get; set; }
        [FwLogicProperty(Id: "kYdQQfzmDcoF", IsReadOnly: true)]
        public decimal? LaborSubTotal { get; set; }
        [FwLogicProperty(Id: "OqsvrIKlWRe3", IsReadOnly: true)]
        public decimal? NonRentalSubTotal { get; set; }
        [FwLogicProperty(Id: "zirSJ563dZ1", IsReadOnly: true)]
        public decimal? OrderInvoiceSubTotal { get; set; }
        [FwLogicProperty(Id: "WelaHDlHeViZG", IsReadOnly: true)]
        public decimal? OrderInvoiceTax { get; set; }
        [FwLogicProperty(Id: "eJtlPVI0A3qe", IsReadOnly: true)]
        public decimal? OrderInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
