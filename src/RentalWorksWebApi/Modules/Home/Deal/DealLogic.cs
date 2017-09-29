using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.Deal
{
    public class DealLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealRecord deal = new DealRecord();
        DealLoader dealLoader = new DealLoader();
        public DealLogic()
        {
            dataRecords.Add(deal);
            dataLoader = dealLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealId { get { return deal.DealId; } set { deal.DealId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Deal { get { return deal.Deal; } set { deal.Deal = value; } }
        public string DealNumber { get { return deal.DealNumber; } set { deal.DealNumber = value; } }
        /*
                [FwBusinessLogicField(isReadOnly: true)]
                public string Dealtype { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Customer { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string CusttypeId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Department { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Dealstatus { get; set; }
                public string Statusasof { get { return deal.Statusasof; } set { deal.Statusasof = value; } }
                public string LocationId { get { return deal.LocationId; } set { deal.LocationId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Location { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Prodtype { get; set; }
                public string Add1 { get { return deal.Add1; } set { deal.Add1 = value; } }
                public string Add2 { get { return deal.Add2; } set { deal.Add2 = value; } }
                public string City { get { return deal.City; } set { deal.City = value; } }
                public string State { get { return deal.State; } set { deal.State = value; } }
                public string Zip { get { return deal.Zip; } set { deal.Zip = value; } }
                public string Phone { get { return deal.Phone; } set { deal.Phone = value; } }
                public string Fax { get { return deal.Fax; } set { deal.Fax = value; } }
                public string Phone800 { get { return deal.Phone800; } set { deal.Phone800 = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Prcontact { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Prphone { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Billperiod { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Porequired { get; set; }
                public string Billtoadd1 { get { return deal.Billtoadd1; } set { deal.Billtoadd1 = value; } }
                public string Billtoadd2 { get { return deal.Billtoadd2; } set { deal.Billtoadd2 = value; } }
                public string Billtocity { get { return deal.Billtocity; } set { deal.Billtocity = value; } }
                public string Billtostate { get { return deal.Billtostate; } set { deal.Billtostate = value; } }
                public string Billtozip { get { return deal.Billtozip; } set { deal.Billtozip = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Creditstatus { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Usecustomercredit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string DealcreditstatusId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Dealcrappthru { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Dealcreditlimit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Dealunlimitedcredit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string CustcreditstatusId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Custcrappthru { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Custcreditlimit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Custunlimitedcredit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Usecustomerinsurance { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Dealinscertification { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Dealinsvalidthru { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Custinscertification { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Custinsvalidthru { get; set; }
                public string CreditstatusId { get { return deal.CreditstatusId; } set { deal.CreditstatusId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Creditapponfile { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Creditvalidthru { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Creditlimit { get; set; }
                public bool Unlimitedcredit { get { return deal.Unlimitedcredit; } set { deal.Unlimitedcredit = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Orderallow { get; set; }
                public bool Inscertification { get { return deal.Inscertification; } set { deal.Inscertification = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Insurancevalidthru { get; set; }
                public int Inscovliab { get { return deal.Inscovliab; } set { deal.Inscovliab = value; } }
                public int Inscovprop { get { return deal.Inscovprop; } set { deal.Inscovprop = value; } }
                public string Taxable { get { return deal.Taxable; } set { deal.Taxable = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Carrier { get; set; }
                public string Shipadd1 { get { return deal.Shipadd1; } set { deal.Shipadd1 = value; } }
                public string Shipadd2 { get { return deal.Shipadd2; } set { deal.Shipadd2 = value; } }
                public string Shipcity { get { return deal.Shipcity; } set { deal.Shipcity = value; } }
                public string Shipstate { get { return deal.Shipstate; } set { deal.Shipstate = value; } }
                public string Shipzip { get { return deal.Shipzip; } set { deal.Shipzip = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Shipcountry { get; set; }
                public string BillperiodId { get { return deal.BillperiodId; } set { deal.BillperiodId = value; } }
                public string PaytermsId { get { return deal.PaytermsId; } set { deal.PaytermsId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Payterms { get; set; }
                public string PaytypeId { get { return deal.PaytypeId; } set { deal.PaytypeId = value; } }
                public string BilltocountryId { get { return deal.BilltocountryId; } set { deal.BilltocountryId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Dealinactive { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Statustype { get; set; }
                public string CustomerId { get { return deal.CustomerId; } set { deal.CustomerId = value; } }
                public string DepartmentId { get { return deal.DepartmentId; } set { deal.DepartmentId = value; } }
                public string Ratetype { get { return deal.Ratetype; } set { deal.Ratetype = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Periodtype { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Csr { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Projectmanager { get; set; }
                public string Expwrapdate { get { return deal.Expwrapdate; } set { deal.Expwrapdate = value; } }
                public string SalesrepresentativeId { get { return deal.SalesrepresentativeId; } set { deal.SalesrepresentativeId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Salesrepresentative { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Splitrentalflg { get; set; }
                public bool Rebaterentalflg { get { return deal.Rebaterentalflg; } set { deal.Rebaterentalflg = value; } }
                public int Ownedrebaterate { get { return deal.Ownedrebaterate; } set { deal.Ownedrebaterate = value; } }
                public int Vendorrebaterate { get { return deal.Vendorrebaterate; } set { deal.Vendorrebaterate = value; } }
                public string RebatecustomerId { get { return deal.RebatecustomerId; } set { deal.RebatecustomerId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Rebatecustomer { get; set; }
                public string DealtypeId { get { return deal.DealtypeId; } set { deal.DealtypeId = value; } }
                public string DealstatusId { get { return deal.DealstatusId; } set { deal.DealstatusId = value; } }
                public string CountryId { get { return deal.CountryId; } set { deal.CountryId = value; } }
                public string CsrId { get { return deal.CsrId; } set { deal.CsrId = value; } }
                public bool Enablewebquoterequest { get { return deal.Enablewebquoterequest; } set { deal.Enablewebquoterequest = value; } }
                public bool Enablewebreports { get { return deal.Enablewebreports; } set { deal.Enablewebreports = value; } }
                public string Inputdate { get { return deal.Inputdate; } set { deal.Inputdate = value; } }
                public string Moddate { get { return deal.Moddate; } set { deal.Moddate = value; } }
                public bool Usecustomertax { get { return deal.Usecustomertax; } set { deal.Usecustomertax = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Locdisablecredit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Locdisablecreditthroughdate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Locdisableinsurance { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Locdisableinsurancethroughdate { get; set; }
                public string Lastorderdate { get { return deal.Lastorderdate; } set { deal.Lastorderdate = value; } }
                public decimal Balance { get { return deal.Balance; } set { deal.Balance = value; } }
                public string DealclassificationId { get { return deal.DealclassificationId; } set { deal.DealclassificationId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Dealclassification { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Inactive { get; set; }
                //------------------------------------------------------------------------------------ 
                */
    }
}