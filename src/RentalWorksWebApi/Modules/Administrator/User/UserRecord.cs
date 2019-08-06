using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Administrator.User
{
    [FwSqlTable("users")]
    public class UserRecord : AppDataReadWriteRecord
    {
        public UserRecord() : base()
        {
            BeforeSave += OnBeforeSaveUser;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loginname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string LoginName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "password", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Password { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarylocationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PrimaryOfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarywarehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PrimaryWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UserTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15, required: true)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "middleinitial", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, required: true)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string GroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "office", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "title", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "home", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string HomePhone { get; set; }
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
        [FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AccountLocked { get; set; }
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
        //[FwSqlDataField(column: "autoprintcontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoprintcontract { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdepttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string DefaultDepartmentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PartsDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string MiscDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LaborDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FacilityDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TransportationDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PartsInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string MiscTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LaborTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceinventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationinvdepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TransportationTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nomisconquote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? NoMiscellaneousOnQuotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nomisconorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? NoMiscellaneousOnOrders { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nomisconpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? NoMiscellaneousOnPurchaseOrders { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitdw", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? LimitDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwkfrom", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 4, scale: 2)]
        public decimal? MinimumDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcreditlimitoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowCreditLimitOverride { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "cumulativediscountoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? CumulativeDiscountOverride { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcumulativediscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? LimitAllowCumulativeDiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? MaximumCumulativeDiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? LimitDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountto", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 2)]
        public decimal? MaximumDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? LimitSubDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subdiscountto", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 2)]
        public decimal? MaximumSubDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountrule", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string DiscountRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alloworderedqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunavailable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageonpendingpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcontractwithexceptions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfPendingItemsExist { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageonunapprovedcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoncreditlimit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninsurancecoverage", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninvalidinsurance", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninvalidnontax", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowreceivepositiveconflict", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunreservedconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowStagingOfUnreservedConsignedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunapproveditem", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowStagingOfUnapprovedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowsubstitute", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowSubstitutesAtStaging { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "substitutedeletesoriginal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DeleteOriginalOnSubstitution { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcancelcontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? AllowCancelContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikactivityprintdollar", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikActivityAllowPrintDollarAmounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablecreatecontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikScanAllowCreateContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qsallowapplyallqtyitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikScanAllowApplyAll { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcrossicodeexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowCrossICodeExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcrossicodependingexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowCrossICodePendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowchangeavailpriority", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowChangeAvailabilityPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulecolor", modeltype: FwDataTypes.OleToHtmlColor, sqltype: "int")]
        public string ScheduleColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcrosseditlocation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowCrossLocationEditAndDelete { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode == TDataRecordSaveMode.smInsert)
            {
                string primaryDepartmentId = string.Empty;
                switch (DefaultDepartmentType)
                {
                    case RwConstants.DEPARTMENT_TYPE_RENTAL:
                        primaryDepartmentId = RentalDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_SALES:
                        primaryDepartmentId = SalesDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_PARTS:
                        primaryDepartmentId = PartsDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_TRANSPORTATION:
                        primaryDepartmentId = TransportationDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_FACILITIES:
                        primaryDepartmentId = FacilityDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_MISC:
                        primaryDepartmentId = MiscDepartmentId;
                        break;
                    case RwConstants.DEPARTMENT_TYPE_LABOR:
                        primaryDepartmentId = LaborDepartmentId;
                        break;
                    default:
                        primaryDepartmentId = "";
                        break;
                }
                if (primaryDepartmentId.Equals(string.Empty))
                {
                    isValid = false;
                    validateMsg = "Primary Department is required.";
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveUser(object sender, BeforeSaveDataRecordEventArgs e)
        {
            PrimaryOfficeLocationId = OfficeLocationId;
            PrimaryWarehouseId = WarehouseId;

            if (Password != null)
            {
                //Password = AppFunc.EncryptAsync(AppConfig, Password).Result;
                Password = AppFunc.EncryptAsync(AppConfig, Password.ToUpper()).Result;  //justin 11/25/2018 CAS-24166-YQWC
            }
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}