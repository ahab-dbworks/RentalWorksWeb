using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Home.Deal
{
    [FwSqlTable("deal")]
    public class DealRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DealId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string Deal { get; set; }
            //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone800 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PhoneOther { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csrid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CsrId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultagentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultprojectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 

        /*
                    [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Statusasof { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscert", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Inscert { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "expwrapdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Expwrapdate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string BillperiodId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "insvalidthru", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Insvalidthru { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string PaytermsId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string PaytypeId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "creditcardtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string CreditcardtypeId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "poreq", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Poreq { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crappthru", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Crappthru { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "potype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Potype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crlimitdeal", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Crlimitdeal { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crapponfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Crapponfile { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "responsibilityparty", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 35)]
                    public string Responsibilityparty { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crresponfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Crresponfile { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
                    public string Billtocity { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                    public string Billtostate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
                    public string Billtozip { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crver", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Crver { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billdiscrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
                    public decimal Billdiscrate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
                    public string Crby { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "cron", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Cron { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crclimit", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Crclimit { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crcno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                    public string Crcno { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crcname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Crcname { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipcity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
                    public string Shipcity { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crcauthonfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Crcauthonfile { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipzip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
                    public string Shipzip { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string ShipcountryId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscovliab", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Inscovliab { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscovliabded", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Inscovliabded { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscovprop", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Inscovprop { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscovpropded", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Inscovpropded { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscompagent", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Inscompagent { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Taxable { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "taxfedno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                    public string Taxfedno { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Inputdate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Moddate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Inputby { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "modby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Modby { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string CreditstatusId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string VendorId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "prodtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string ProdtypeId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string DealstatusId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "stateofinc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
                    public string Stateofinc { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "custinsurance", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Custinsurance { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "custcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Custcredit { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "unlimitedcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Unlimitedcredit { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "thresholdamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
                    public decimal Thresholdamt { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "thresholdpercent", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Thresholdpercent { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inscertification", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Inscertification { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string Ratetype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "printlaborcomponent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Printlaborcomponent { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "laborcomponentpct", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Laborcomponentpct { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Color { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "enablewebreports", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Enablewebreports { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Textcolor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtoadd", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
                    public string Billtoadd { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string BilltocountryId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipadd", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
                    public string Shipadd { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipstate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                    public string Shipstate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "spacebillweekends", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Spacebillweekends { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "balance", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
                    public decimal Balance { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "commissionincludesvendoritems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Commissionincludesvendoritems { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "commissionrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
                    public decimal Commissionrate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string SalesrepresentativeId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "usecustomerdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Usecustomerdiscount { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "budocs", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Budocs { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "invformatid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string InvformatId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "onlot", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Onlot { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
                    public string Orbitsapchgdeal { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgdetail { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgmajor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgset { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
                    public string Orbitsapchgsub { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
                    public int Ownedrebaterate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string RebatecustomerId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Rebaterentalflg { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "revdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Revdate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
                    public int Vendorrebaterate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "enablewebquoterequest", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Enablewebquoterequest { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
                    public string Indeliverytype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
                    public string Outdeliverytype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "dealclassificationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string DealclassificationId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapaccountno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
                    public string Sapaccountno { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapcopytoorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Sapcopytoorder { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapcostobject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 24)]
                    public string Sapcostobject { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "saptype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Saptype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "usecustomertax", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Usecustomertax { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "maxrequestequipmentdays", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int Maxrequestequipmentdays { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "maxrequestequipmenthours", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int Maxrequestequipmenthours { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "nontaxcertificateno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Nontaxcertificateno { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "nontaxcertificateonfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Nontaxcertificateonfile { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "nontaxvalidthrough", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Nontaxvalidthrough { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "nontaxyear", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int Nontaxyear { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "financechargeflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Financechargeflg { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "vehiclerentalagreementcomplete", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Vehiclerentalagreementcomplete { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "vehicleinsurancecertification", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Vehicleinsurancecertification { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtoatt2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Billtoatt2 { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtoatt", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
                    public string Billtoatt { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipatt", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Shipatt { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Billtoadd1 { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Billtoadd2 { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipadd1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Shipadd1 { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "shipadd2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
                    public string Shipadd2 { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "allowbillschedoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Allowbillschedoverride { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "allowrebatecredits", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Allowrebatecredits { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crcexpmonth", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int Crcexpmonth { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "crcexpyear", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int Crcexpyear { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "playpreview", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Playpreview { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "playopen", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Playopen { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "usediscounttemplate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Usediscounttemplate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string DiscounttemplateId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "enableactivityoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Enableactivityoverride { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablerental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablerental { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablesubrental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablesubrental { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablesales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablesales { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablesubsales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablesubsales { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablefacilities", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablefacilities { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disabletransportation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disabletransportation { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablelabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablelabor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablesublabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablesublabor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablemisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablemisc { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablesubmisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablesubmisc { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "disablerentalsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool Disablerentalsale { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "lastorderdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Lastorderdate { get; set; }
             */
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}