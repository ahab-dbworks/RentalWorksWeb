using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.Deal
{
    [FwSqlTable("dealview")]
    public class DealLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealtypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string Dealtype { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text)]
        public string Phone800 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text)]
        public string PhoneOther { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csrid", modeltype: FwDataTypes.Text)]
        public string CsrId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csr", modeltype: FwDataTypes.Text)]
        public string Csr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultprojectmanagerid", modeltype: FwDataTypes.Text)]
        public string DefaultProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string DefaultProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultagentid", modeltype: FwDataTypes.Text)]
        public string DefaultAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string DefaultAgent { get; set; }
        //------------------------------------------------------------------------------------ 

        /*
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
                public string CusttypeId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
                public string Dealstatus { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.UTCDateTime)]
                public string Statusasof { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "prodtype", modeltype: FwDataTypes.Text)]
                public string Prodtype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "prcontact", modeltype: FwDataTypes.Text)]
                public string Prcontact { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "prphone", modeltype: FwDataTypes.Text)]
                public string Prphone { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
                public string Billperiod { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "porequired", modeltype: FwDataTypes.Boolean)]
                public bool Porequired { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text)]
                public string Billtoadd1 { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text)]
                public string Billtoadd2 { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text)]
                public string Billtocity { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text)]
                public string Billtostate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
                public string Billtozip { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text)]
                public string Creditstatus { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "usecustomercredit", modeltype: FwDataTypes.Boolean)]
                public bool Usecustomercredit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealcreditstatusid", modeltype: FwDataTypes.Text)]
                public string DealcreditstatusId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealcrappthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Dealcrappthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealcreditlimit", modeltype: FwDataTypes.Integer)]
                public int? Dealcreditlimit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealunlimitedcredit", modeltype: FwDataTypes.Boolean)]
                public bool Dealunlimitedcredit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custcreditstatusid", modeltype: FwDataTypes.Text)]
                public string CustcreditstatusId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custcrappthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Custcrappthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custcreditlimit", modeltype: FwDataTypes.Integer)]
                public int? Custcreditlimit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custunlimitedcredit", modeltype: FwDataTypes.Boolean)]
                public bool Custunlimitedcredit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "usecustomerinsurance", modeltype: FwDataTypes.Boolean)]
                public bool Usecustomerinsurance { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealinscertification", modeltype: FwDataTypes.Boolean)]
                public bool Dealinscertification { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealinsvalidthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Dealinsvalidthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custinscertification", modeltype: FwDataTypes.Boolean)]
                public bool Custinscertification { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "custinsvalidthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Custinsvalidthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text)]
                public string CreditstatusId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "creditapponfile", modeltype: FwDataTypes.Boolean)]
                public bool Creditapponfile { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "creditvalidthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Creditvalidthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "creditlimit", modeltype: FwDataTypes.Integer)]
                public int? Creditlimit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "unlimitedcredit", modeltype: FwDataTypes.Boolean)]
                public bool Unlimitedcredit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "orderallow", modeltype: FwDataTypes.Boolean)]
                public bool Orderallow { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inscertification", modeltype: FwDataTypes.Boolean)]
                public bool Inscertification { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "insurancevalidthru", modeltype: FwDataTypes.UTCDateTime)]
                public string Insurancevalidthru { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inscovliab", modeltype: FwDataTypes.Integer)]
                public int? Inscovliab { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inscovprop", modeltype: FwDataTypes.Integer)]
                public int? Inscovprop { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Text)]
                public string Taxable { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
                public string Carrier { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipadd1", modeltype: FwDataTypes.Text)]
                public string Shipadd1 { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipadd2", modeltype: FwDataTypes.Text)]
                public string Shipadd2 { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipcity", modeltype: FwDataTypes.Text)]
                public string Shipcity { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipstate", modeltype: FwDataTypes.Text)]
                public string Shipstate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipzip", modeltype: FwDataTypes.Text)]
                public string Shipzip { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipcountry", modeltype: FwDataTypes.Text)]
                public string Shipcountry { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
                public string BillperiodId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
                public string PaytermsId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
                public string Payterms { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
                public string PaytypeId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text)]
                public string BilltocountryId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealinactive", modeltype: FwDataTypes.Boolean)]
                public bool Dealinactive { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Boolean)]
                public bool Statustype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
                public string Ratetype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
                public string Periodtype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "expwrapdate", modeltype: FwDataTypes.UTCDateTime)]
                public string Expwrapdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text)]
                public string SalesrepresentativeId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
                public string Salesrepresentative { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "splitrentalflg", modeltype: FwDataTypes.Boolean)]
                public bool Splitrentalflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean)]
                public bool Rebaterentalflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer)]
                public int? Ownedrebaterate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer)]
                public int? Vendorrebaterate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text)]
                public string RebatecustomerId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "rebatecustomer", modeltype: FwDataTypes.Text)]
                public string Rebatecustomer { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
                public string DealstatusId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "enablewebquoterequest", modeltype: FwDataTypes.Boolean)]
                public bool Enablewebquoterequest { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "enablewebreports", modeltype: FwDataTypes.Boolean)]
                public bool Enablewebreports { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime)]
                public string Inputdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime)]
                public string Moddate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "usecustomertax", modeltype: FwDataTypes.Boolean)]
                public bool Usecustomertax { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "locdisablecredit", modeltype: FwDataTypes.Boolean)]
                public bool Locdisablecredit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "locdisablecreditthroughdate", modeltype: FwDataTypes.Boolean)]
                public bool Locdisablecreditthroughdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "locdisableinsurance", modeltype: FwDataTypes.Boolean)]
                public bool Locdisableinsurance { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "locdisableinsurancethroughdate", modeltype: FwDataTypes.Boolean)]
                public bool Locdisableinsurancethroughdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lastorderdate", modeltype: FwDataTypes.UTCDateTime)]
                public string Lastorderdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "balance", modeltype: FwDataTypes.Decimal)]
                public decimal? Balance { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealclassificationid", modeltype: FwDataTypes.Text)]
                public string DealclassificationId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dealclassification", modeltype: FwDataTypes.Text)]
                public string Dealclassification { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
                public bool Inactive { get; set; }
                //------------------------------------------------------------------------------------ 
        */
    }
}