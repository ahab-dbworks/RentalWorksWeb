using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Settings.OrderType
{
    [FwSqlTable("ordertypeview")]
    public class OrderTypeBaseLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikefee", modeltype: FwDataTypes.Boolean)]
        public bool AddInstallationAndStrikeFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikemasterid", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikepct", modeltype: FwDataTypes.Decimal)]
        public decimal? InstallationAndStrikeFeePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikebasedon", modeltype: FwDataTypes.Text)]
        public string InstallationAndStrikeFeeBasedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicefee", modeltype: FwDataTypes.Boolean)]
        public bool AddManagementAndServiceFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicemasterid", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicepct", modeltype: FwDataTypes.Decimal)]
        public decimal? ManagementAndServiceFeePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicebasedon", modeltype: FwDataTypes.Text)]
        public string ManagementAndServiceFeeBasedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combineactivitytabs", modeltype: FwDataTypes.Boolean)]
        public bool Combineactivitytabs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combinetabseparateitems", modeltype: FwDataTypes.Boolean)]
        public bool Combinetabseparateitems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscount", modeltype: FwDataTypes.Boolean)]
        public bool QuikConfirmDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikConfirmDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountdays", modeltype: FwDataTypes.Integer)]
        public int? QuikConfirmDiscountDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismastersuborder", modeltype: FwDataTypes.Boolean)]
        public bool IsMasterSubOrderType { get; set; }
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
        [FwSqlDataField(column: "suborderdefaultordertypeid", modeltype: FwDataTypes.Text)]
        public string SuborderdefaultordertypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SuborderordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidecrewbreaks", modeltype: FwDataTypes.Boolean)]
        public bool HideCrewBreaks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break1paid", modeltype: FwDataTypes.Boolean)]
        public bool Break1Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break2paid", modeltype: FwDataTypes.Boolean)]
        public bool Break2Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break3paid", modeltype: FwDataTypes.Boolean)]
        public bool Break3Paid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordtype", modeltype: FwDataTypes.Text)]
        public string OrdType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? Orderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalesprice", modeltype: FwDataTypes.Boolean)]
        public bool SalesInventoryPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disablecostgl", modeltype: FwDataTypes.Boolean)]
        public bool DisableCostGl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text)]
        public string Invoiceclass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalescost", modeltype: FwDataTypes.Boolean)]
        public bool SalesInventoryCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscount", modeltype: FwDataTypes.Boolean)]
        public bool QuikPayDiscount { get; set; }
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
        public bool QuikPayDiscountExcludeSubs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RentalordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SalesordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SpaceordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubrentalordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubsalesordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string PurchaseordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string LaborordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SublaborordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string MiscordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string SubmiscordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RepairordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string VehicleordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string RentalsaleordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldordertypefieldsid", modeltype: FwDataTypes.Text)]
        public string LdordertypefieldsId { get; set; }
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
        public bool ExcludeFromTopSalesDashboard { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovebyrequired", modeltype: FwDataTypes.Boolean)]
        public bool ApprovalNeededByRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poimportancerequired", modeltype: FwDataTypes.Boolean)]
        public bool ImportanceRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popaytyperequired", modeltype: FwDataTypes.Boolean)]
        public bool PayTypeRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprojectrequired", modeltype: FwDataTypes.Boolean)]
        public bool ProjectRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetrental", modeltype: FwDataTypes.Boolean)]
        public bool RwNetDefaultRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetmisc", modeltype: FwDataTypes.Boolean)]
        public bool RwNetDefaultMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetlabor", modeltype: FwDataTypes.Boolean)]
        public bool RwNetDefaultLabor { get; set; }
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
        public bool AllowRoundTripRentals { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectrentalsaleprice", modeltype: FwDataTypes.Text)]
        public string DefaultUsedSalePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        
    }
}