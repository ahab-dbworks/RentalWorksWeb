using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.AdministratorControls.Person
{
    [FwSqlTable("webusersview")]
    public class PersonLoader : PersonBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ScheduleColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)]
        public string UserTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)]
        public string UserTitle { get; set; }
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
        [FwSqlDataField(column: "office", modeltype: FwDataTypes.Text)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text)]
        public string Cellular { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "home", modeltype: FwDataTypes.Text)]
        public string HomePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdepttype", modeltype: FwDataTypes.Text)]
        public string DefaultDepartmentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldepartmentid", modeltype: FwDataTypes.Text)]
        public string RentalDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldepartment", modeltype: FwDataTypes.Text)]
        public string RentalDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdepartmentid", modeltype: FwDataTypes.Text)]
        public string SalesDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdepartment", modeltype: FwDataTypes.Text)]
        public string SalesDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsdepartmentid", modeltype: FwDataTypes.Text)]
        public string PartsDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsdepartment", modeltype: FwDataTypes.Text)]
        public string PartsDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdepartmentid", modeltype: FwDataTypes.Text)]
        public string MiscDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdepartment", modeltype: FwDataTypes.Text)]
        public string MiscDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordepartmentid", modeltype: FwDataTypes.Text)]
        public string LaborDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordepartment", modeltype: FwDataTypes.Text)]
        public string LaborDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedepartmentid", modeltype: FwDataTypes.Text)]
        public string FacilityDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedepartment", modeltype: FwDataTypes.Text)]
        public string FacilityDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationdepartmentid", modeltype: FwDataTypes.Text)]
        public string TransportationDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationdepartment", modeltype: FwDataTypes.Text)]
        public string TransportationDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string RentalInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalinventorydepartment", modeltype: FwDataTypes.Text)]
        public string RentalInventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string SalesInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesinventorydepartment", modeltype: FwDataTypes.Text)]
        public string SalesInventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string PartsInventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsinventorydepartment", modeltype: FwDataTypes.Text)]
        public string PartsInventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string MiscTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscinventorydepartment", modeltype: FwDataTypes.Text)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string LaborTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborinventorydepartment", modeltype: FwDataTypes.Text)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceinventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceinventorydepartment", modeltype: FwDataTypes.Text)]
        public string FacilityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationinvdepartmentid", modeltype: FwDataTypes.Text)]
        public string TransportationTypeId { get; set; }
        //------------------------------------------------------------------------------------         
        [FwSqlDataField(column: "transportationinvdepartment", modeltype: FwDataTypes.Text)]
        public string TransportationType { get; set; }
        //------------------------------------------------------------------------------------         
        [FwSqlDataField(column: "nomisconquote", modeltype: FwDataTypes.Boolean)]
        public bool? NoMiscellaneousOnQuotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nomisconorder", modeltype: FwDataTypes.Boolean)]
        public bool? NoMiscellaneousOnOrders { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nomisconpo", modeltype: FwDataTypes.Boolean)]
        public bool? NoMiscellaneousOnPurchaseOrders { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitdw", modeltype: FwDataTypes.Boolean)]
        public bool? LimitDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwkfrom", modeltype: FwDataTypes.Decimal)]
        public decimal? MinimumDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcreditlimitoverride", modeltype: FwDataTypes.Boolean)]
        public bool? AllowCreditLimitOverride { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "cumulativediscountoverride", modeltype: FwDataTypes.Boolean)]
        //public bool? CumulativeDiscountOverride { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowcumulativediscount", modeltype: FwDataTypes.Boolean)]
        //public bool? LimitAllowCumulativeDiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal)]
        //public decimal? MaximumCumulativeDiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? LimitDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountto", modeltype: FwDataTypes.Decimal)]
        public decimal? MaximumDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? LimitSubDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subdiscountto", modeltype: FwDataTypes.Decimal)]
        public decimal? MaximumSubDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountrule", modeltype: FwDataTypes.Text)]
        public string DiscountRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alloworderedqty", modeltype: FwDataTypes.Boolean)]
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunavailable", modeltype: FwDataTypes.Boolean)]
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageonpendingpo", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcontractwithexceptions", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfPendingItemsExist { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageonunapprovedcredit", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoncreditlimit", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninsurancecoverage", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninvalidinsurance", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageoninvalidnontax", modeltype: FwDataTypes.Boolean)]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowreceivepositiveconflict", modeltype: FwDataTypes.Boolean)]
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunreservedconsigned", modeltype: FwDataTypes.Boolean)]
        public bool? AllowStagingOfUnreservedConsignedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowstageunapproveditem", modeltype: FwDataTypes.Boolean)]
        public bool? AllowStagingOfUnapprovedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowsubstitute", modeltype: FwDataTypes.Boolean)]
        public bool? AllowSubstitutesAtStaging { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "substitutedeletesoriginal", modeltype: FwDataTypes.Boolean)]
        public bool? DeleteOriginalOnSubstitution { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcancelcontract", modeltype: FwDataTypes.Boolean)]
        public bool? AllowCancelContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikactivityprintdollar", modeltype: FwDataTypes.Boolean)]
        public bool? QuikActivityAllowPrintDollarAmounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablecreatecontract", modeltype: FwDataTypes.Boolean)]
        public bool? QuikScanAllowCreateContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qsallowapplyallqtyitems", modeltype: FwDataTypes.Boolean)]
        public bool? QuikScanAllowApplyAll { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcrossicodeexchange", modeltype: FwDataTypes.Boolean)]
        public bool? AllowCrossICodeExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowcrossicodependingexchange", modeltype: FwDataTypes.Boolean)]
        public bool? AllowCrossICodePendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowchangeavailpriority", modeltype: FwDataTypes.Boolean)]
        public bool? AllowChangeAvailabilityPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "memo", modeltype: FwDataTypes.Text)]
        public string Memo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expiredays", modeltype: FwDataTypes.Integer)]
        public int? PasswordExpireDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pwupdated", modeltype: FwDataTypes.UTCDateTime)]
        public string PasswordUpdatedDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expireflg", modeltype: FwDataTypes.Boolean)]
        public bool? PasswordExpires { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mustchangepwflg", modeltype: FwDataTypes.Boolean)]
        public bool? UserMustChangePassword { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean)]
        public bool? AccountLocked { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
