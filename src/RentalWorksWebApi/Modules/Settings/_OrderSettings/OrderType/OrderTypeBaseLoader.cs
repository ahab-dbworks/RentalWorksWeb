    using FwStandard.Data; 
    using FwStandard.Models; 
    using FwStandard.SqlServer; 
    using FwStandard.SqlServer.Attributes; 
    using WebApi.Data; 
    using System.Collections.Generic;
namespace WebApi.Modules.Settings.OrderSettings.OrderType
{
    public class OrderTypeBaseLoader : OrderTypeBaseBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikefee", modeltype: FwDataTypes.Boolean)]
        public bool? AddInstallationAndStrikeFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikemasterid", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikemasterno", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikemaster", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikepct", modeltype: FwDataTypes.Decimal)]
        public decimal? InstallationAndStrikeFeePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikebasedon", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeBasedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicefee", modeltype: FwDataTypes.Boolean)]
        public bool? AddManagementAndServiceFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicemasterid", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicemasterno", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicemaster", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicepct", modeltype: FwDataTypes.Decimal)]
        public decimal? ManagementAndServiceFeePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicebasedon", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeBasedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combinetabseparateitems", modeltype: FwDataTypes.Boolean)]
        public bool? Combinetabseparateitems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? QuikConfirmDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikConfirmDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountdays", modeltype: FwDataTypes.Integer)]
        public int? QuikConfirmDiscountDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismastersuborder", modeltype: FwDataTypes.Boolean)]
        public bool? IsMasterSubOrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderbillby", modeltype: FwDataTypes.Text)]
        public string Suborderbillby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderavailabilityrule", modeltype: FwDataTypes.Text)]
        public string Suborderavailabilityrule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderorderqty", modeltype: FwDataTypes.Text)]
        public string Suborderorderqty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidecrewbreaks", modeltype: FwDataTypes.Boolean)]
        public bool? HideCrewBreaks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break1paid", modeltype: FwDataTypes.Boolean)]
        public bool? Break1Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break2paid", modeltype: FwDataTypes.Boolean)]
        public bool? Break2Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break3paid", modeltype: FwDataTypes.Boolean)]
        public bool? Break3Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordtype", modeltype: FwDataTypes.Text)]
        public string OrdType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? Orderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalesprice", modeltype: FwDataTypes.Text)]
        public string SalesInventoryPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disablecostgl", modeltype: FwDataTypes.Boolean)]
        public bool? DisableCostGl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text)]
        public string Invoiceclass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalescost", modeltype: FwDataTypes.Text)]
        public string SalesInventoryCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscount", modeltype: FwDataTypes.Boolean)]
        public bool? QuikPayDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscounttype", modeltype: FwDataTypes.Text)]
        public string QuikPayDiscountType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscountdays", modeltype: FwDataTypes.Integer)]
        public int? QuikPayDiscountDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikPayDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayexcludesubs", modeltype: FwDataTypes.Boolean)]
        public bool? QuikPayDiscountExcludeSubs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromtime", modeltype: FwDataTypes.Text)]
        public string DefaultFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string DefaultPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totime", modeltype: FwDataTypes.Text)]
        public string DefaultToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdaystarttime", modeltype: FwDataTypes.Text)]
        public string DailyScheduleDefaultStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdaystoptime", modeltype: FwDataTypes.Text)]
        public string DailyScheduleDefaultStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludefromtopsales", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeFromTopSalesDashboard { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovebyrequired", modeltype: FwDataTypes.Boolean)]
        public bool? ApprovalNeededByRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poimportancerequired", modeltype: FwDataTypes.Boolean)]
        public bool? ImportanceRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popaytyperequired", modeltype: FwDataTypes.Boolean)]
        public bool? PayTypeRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprojectrequired", modeltype: FwDataTypes.Boolean)]
        public bool? ProjectRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetrental", modeltype: FwDataTypes.Boolean)]
        public bool? RwNetDefaultRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetmisc", modeltype: FwDataTypes.Boolean)]
        public bool? RwNetDefaultMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetlabor", modeltype: FwDataTypes.Boolean)]
        public bool? RwNetDefaultLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalpurchasedefaultrate", modeltype: FwDataTypes.Text)]
        public string RentalPurchaseDefaultRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salespurchasedefaultrate", modeltype: FwDataTypes.Text)]
        public string SalesPurchaseDefaultRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedescription", modeltype: FwDataTypes.Text)]
        public string FacilityDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "roundtriprentals", modeltype: FwDataTypes.Boolean)]
        public bool? AllowRoundTripRentals { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectrentalsaleprice", modeltype: FwDataTypes.Text)]
        public string DefaultUsedSalePrice { get; set; }
        //------------------------------------------------------------------------------------ 





        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RentalOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalicodewidth", modeltype: FwDataTypes.Integer)]
        public int? RentalICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldescwidth", modeltype: FwDataTypes.Integer)]
        public int? RentalDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string RentalDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SalesOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesicodewidth", modeltype: FwDataTypes.Integer)]
        public int? SalesICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdescwidth", modeltype: FwDataTypes.Integer)]
        public int? SalesDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesmfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? SalesManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SalesShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string SalesDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string LaborOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? LaborShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? LaborShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? LaborShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laboricodewidth", modeltype: FwDataTypes.Integer)]
        public int? LaborICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordescwidth", modeltype: FwDataTypes.Integer)]
        public int? LaborDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? Laborshowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? Laborshowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowconsignAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowconsignConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LaborshowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string LaborDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string MiscOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscicodewidth", modeltype: FwDataTypes.Integer)]
        public int? MiscICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdescwidth", modeltype: FwDataTypes.Integer)]
        public int? MiscDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? MiscShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string MiscDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseicodewidth", modeltype: FwDataTypes.Integer)]
        public int? PurchaseICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedescwidth", modeltype: FwDataTypes.Integer)]
        public int? PurchaseDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasemfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? PurchaseManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string PurchaseDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string FacilityOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowrepairno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceicodewidth", modeltype: FwDataTypes.Integer)]
        public int? FacilityICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedescwidth", modeltype: FwDataTypes.Integer)]
        public int? FacilityDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacemfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? FacilityManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? FacilityShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string FacilityDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubRentalOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalicodewidth", modeltype: FwDataTypes.Integer)]
        public int? SubRentalICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentaldescwidth", modeltype: FwDataTypes.Integer)]
        public int? SubRentalDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubRentalShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentaldatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string SubRentalDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubSaleOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesicodewidth", modeltype: FwDataTypes.Integer)]
        public int? SubSaleICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesdescwidth", modeltype: FwDataTypes.Integer)]
        public int? SubSaleDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubSaleShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesdatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string SubSaleDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubLaborOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaboricodewidth", modeltype: FwDataTypes.Integer)]
        public int? SubLaborICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabordescwidth", modeltype: FwDataTypes.Integer)]
        public int? SubLaborDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabordatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string SublaborDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubMiscOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscicodewidth", modeltype: FwDataTypes.Integer)]
        public int? SubMiscICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscdescwidth", modeltype: FwDataTypes.Integer)]
        public int? SubMiscDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscdatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string SubmiscDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RepairOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairicodewidth", modeltype: FwDataTypes.Integer)]
        public int? RepairICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdescwidth", modeltype: FwDataTypes.Integer)]
        public int? RepairDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairmfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? RepairManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RepairShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string RepairDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string VehicleOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowVehicleNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleicodewidth", modeltype: FwDataTypes.Integer)]
        public int? VehicleICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicledescwidth", modeltype: FwDataTypes.Integer)]
        public int? VehicleDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehiclemfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? VehicleManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowvehicleno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowvehicleno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowcrewname", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowcrewname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowhours", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicledatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string VehicleDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RentalSaleOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleicodewidth", modeltype: FwDataTypes.Integer)]
        public int? RentalSaleICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaledescwidth", modeltype: FwDataTypes.Integer)]
        public int? RentalSaleDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsalemfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? RentalSaleManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleshowvehicleno", modeltype: FwDataTypes.Boolean)]
        //public bool? RentalSaleShowvehicleno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleshowbarcode", modeltype: FwDataTypes.Boolean)]
        //public bool? RentalSaleShowbarcode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleshowserialno", modeltype: FwDataTypes.Boolean)]
        //public bool? RentalSaleShowserialno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleshowcrewname", modeltype: FwDataTypes.Boolean)]
        //public bool? RentalSaleShowcrewname { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleshowhours", modeltype: FwDataTypes.Boolean)]
        //public bool? RentalSaleShowhours { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaledatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string RentalsaleDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshoworderno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowrepairno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldicodewidth", modeltype: FwDataTypes.Integer)]
        public int? LossAndDamageICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowdescription", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lddescwidth", modeltype: FwDataTypes.Integer)]
        public int? LossAndDamageDescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowserialno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowpickdate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowfromdate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowtodate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowbillableperiods", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowsubqty", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowrate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowcost", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowweeklycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmonthlycostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowperiodcostextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowdw", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowdiscountpercent", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmarkuppercent", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmarginpercent", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowsplit", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowunit", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowunitdiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowunitextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowweeklydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowweeklyextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmonthlydiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmonthlyextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowperioddiscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowperiodextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowvariancepercent", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowvarianceextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowcountryoforigin", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmanufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmfgpartno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldmfgpartnowidth", modeltype: FwDataTypes.Integer)]
        public int? LossAndDamageManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmodelno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowvendorpartno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshownotes", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowreturntowarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowfromtime", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowtotime", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldshowvehicleno", modeltype: FwDataTypes.Boolean)]
        //public bool? LossAndDamageShowvehicleno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldshowbarcode", modeltype: FwDataTypes.Boolean)]
        //public bool? LossAndDamageShowbarcode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldshowserialno", modeltype: FwDataTypes.Boolean)]
        //public bool? LossAndDamageShowserialno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldshowcrewname", modeltype: FwDataTypes.Boolean)]
        //public bool? LossAndDamageShowcrewname { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldshowhours", modeltype: FwDataTypes.Boolean)]
        //public bool? LossAndDamageShowhours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowpicktime", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowavailqtyallwh", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowconflictdateallwh", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowconsignavailqty", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowconsignconflictdate", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowconsignqty", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowinlocationqty", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowreservedrentalitems", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowweeksanddays", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowmonthsanddays", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowpremiumpct", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowdepartment", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowlocation", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshoworderactivity", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowsuborderno", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshoworderstatus", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowepisodes", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowepisodeextended", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldshowepisodediscountamount", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lddatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string LossAndDamageDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 



        [FwSqlDataField(column: "suborderdefaultordertypeid", modeltype: FwDataTypes.Text)]
        public string SuborderdefaultordertypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubOrderOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 

        //[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        //public bool? Inactive { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 



        public List<string> CombinedShowFields { get; set; }
        public List<string> RentalShowFields { get; set; }
        public List<string> SalesShowFields { get; set; }
        public List<string> MiscShowFields { get; set; }
        public List<string> LaborShowFields { get; set; }
        public List<string> RentalSaleShowFields { get; set; }
        public List<string> LossAndDamageShowFields { get; set; }


    }
}