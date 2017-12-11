using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Administrator.User
{
    [FwSqlTable("users")]
    public class UserRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loginname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Loginname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "password", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Password { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "middleinitial", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GroupsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "office", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "title", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string PhoneExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "home", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Home { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Cellular { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "memo", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Memo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscellaneous", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string NameFirstMiddleLast { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expiredays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? PasswordExpireDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pwupdated", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string PasswordUpdatedDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expireflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PasswordExpires { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pwignore", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Pwignore { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mustchangepwflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UserMustChangePassword { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "searchpref", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? SearchPreference { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "userstitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string UsersTitleId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usersdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string UsersDepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "room", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Room { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string InputById { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ModifiedById { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SalesDepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoprintcontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoprintcontract { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultdepttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        //public string Defaultdepttype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "partsdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PartsdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LocationId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spacedepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SpacedepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "alloworderedqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Alloworderedqty { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcancelcontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcancelcontract { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageonunapprovedcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageonunapprovedcredit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageoncreditlimit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageoncreditlimit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageoninvalidinsurance", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageoninvalidinsurance { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageoninsurancecoverage", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageoninsurancecoverage { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sso", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Sso { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "daysinwkfrom", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 4, scale: 2)]
        //public decimal? Daysinwkfrom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountto", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 2)]
        //public decimal? Discountto { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcreditlimitoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcreditlimitoverride { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcrosseditlocation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcrosseditlocation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageonpendingpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageonpendingpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageunavailable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageunavailable { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "labordepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LabordepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "laborinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LaborinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "miscdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string MiscdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "miscinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string MiscinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "partsinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PartsinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarylocationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PrimarylocationId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarywarehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PrimarywarehouseId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentaldepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RentaldepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RentalinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SalesinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "showalllocations", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Showalllocations { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spaceinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SpaceinventorydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qedisbaledblclickinc", modeltype: FwDataTypes.Boolean, sqltype: "varchar")]
        //public bool? Qedisbaledblclickinc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaulttogrid", modeltype: FwDataTypes.Boolean, sqltype: "varchar")]
        //public bool? DefaulttogrId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "printbyuserdeal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Printbyuserdeal { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "soundpatherror", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Soundpatherror { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "soundpathsuccess", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Soundpathsuccess { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "soundpathswap", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Soundpathswap { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "soundpathwarning", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Soundpathwarning { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "activedirectorydomain", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Activedirectorydomain { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "authenticatetype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Authenticatetype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "emailapp", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        //public string Emailapp { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "quikactivityprintdollar", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Quikactivityprintdollar { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "showallinventory", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Showallinventory { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ccmyself", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Ccmyself { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ccagent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Ccagent { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "alwaysshowsplitdetails", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Alwaysshowsplitdetails { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcontractwithexceptions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcontractwithexceptions { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowdeptpane", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeshowdeptpane { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "applanguageid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ApplanguageId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowimagepane", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeshowimagepane { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "transportationdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string TransportationdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "transportationinvdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string TransportationinvdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Lockaccount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageoninvalidnontax", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageoninvalidnontax { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountrule", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        //public string Discountrule { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "limitdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Limitdiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "limitdw", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Limitdw { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "limitsubdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Limitsubdiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subdiscountto", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 2)]
        //public decimal? Subdiscountto { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcumulativediscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcumulativediscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "cumulativediscountoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Cumulativediscountoverride { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Maxcumulativediscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dashboardaccess", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Dashboardaccess { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "basedataapplanguageid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string BasedataapplanguageId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeusecompanydepartment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeusecompanydepartment { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "summarybydefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Summarybydefault { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nomisconquote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Nomisconquote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nomisconorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Nomisconorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcrossicodeexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcrossicodeexchange { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hidebilleditems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Hidebilleditems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "topsalesbypmagent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Topsalesbypmagent { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "customerrevenuedetail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Customerrevenuedetail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "customerrevenuebyyear", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Customerrevenuebyyear { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dailyactivity", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Dailyactivity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maximizescrollers", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Maximizescrollers { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maximizeupdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Maximizeupdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "openupdateineditmode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Openupdateineditmode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowreceivepositiveconflict", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowreceivepositiveconflict { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "enablecrosshairorderactivity", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Enablecrosshairorderactivity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablesound", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablesound { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "enablecreatecontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Enablecreatecontract { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowchangeavailpriority", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowchangeavailpriority { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcrossicodependingexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowcrossicodependingexchange { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "crosshairhorizontal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Crosshairhorizontal { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "crosshairvertical", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Crosshairvertical { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qsallowapplyallqtyitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qsallowapplyallqtyitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "soundpathduplicatescan", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Soundpathduplicatescan { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webcatalogid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string WebcatalogId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PoordertypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageunreservedconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageunreservedconsigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nomisconpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Nomisconpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowinstallhotfixes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowinstallhotfixes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowstageunapproveditem", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowstageunapproveditem { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "schedulecolor", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Schedulecolor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowsubstitute", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowsubstitute { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "substitutedeletesoriginal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Substitutedeletesoriginal { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenmode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenmode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenquote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenquote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenrepairorders", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenrepairorders { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenactivityscroller", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenactivityscroller { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoopenavailabilityconflicts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoopenavailabilityconflicts { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prodschedfulldetails", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Prodschedfulldetails { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prodschedshowcrewname", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Prodschedshowcrewname { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availshowallwh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availshowallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availpreference", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Availpreference { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availscheduleviewmode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availscheduleviewmode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        //    [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        //    public string DateStamp { get; set; }
        //    //------------------------------------------------------------------------------------ 
    }
}