using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderSettings.OrderType
{
    [FwSqlTable("ordertype")]
    public class OrderTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, required: true)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikefee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Installstrikefee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InstallstrikemasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? Installstrikepct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "installstrikebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Installstrikebasedon { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicefee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Managementservicefee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ManagementservicemasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? Managementservicepct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "managementservicebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Managementservicebasedon { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combineactivitytabs", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CombineActivityTabs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combinetabseparateitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Combinetabseparateitems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Quikconfirmdiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 9)]
        public decimal? Quikconfirmdiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikconfirmdiscountdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Quikconfirmdiscountdays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismastersuborder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Ismastersuborder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderbillby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Suborderbillby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderavailabilityrule", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Suborderavailabilityrule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderorderqty", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Suborderorderqty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderdefaultordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SuborderdefaultordertypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suborderordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SuborderordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidecrewbreaks", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Hidecrewbreaks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break1paid", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Break1paId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break2paid", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Break2paId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "break3paid", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Break3paId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string Ordtype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 3, scale: 1)]
        public decimal? Orderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalesprice", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string Selectsalesprice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disablecostgl", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Disablecostgl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Invoiceclass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectsalescost", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string Selectsalescost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Quikpaydiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscountdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Quikpaydiscountdays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 9)]
        public decimal? Quikpaydiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscounttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Quikpaydiscounttype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayexcludesubs", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Quikpayexcludesubs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrentalordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SubrentalordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsalesordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SubsalesordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublaborordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SublaborordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submiscordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SubmiscordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RepairordertypefieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Fromtime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Picktime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Totime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdaystarttime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Defaultdaystarttime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdaystoptime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Defaultdaystoptime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludefromtopsales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Excludefromtopsales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovebyrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Poapprovebyrequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poimportancerequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Poimportancerequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popaytyperequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Popaytyperequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprojectrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Poprojectrequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetrental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rwnetrental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetmisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rwnetmisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rwnetlabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rwnetlabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalpurchasedefaultrate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Rentalpurchasedefaultrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salespurchasedefaultrate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Salespurchasedefaultrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedescription", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Spacedescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "roundtriprentals", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Roundtriprentals { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectrentalsaleprice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string SelectRentalSalePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LaborOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string MiscOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FacilityOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VehicleOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalSaleOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LossAndDamageOrderTypeFieldsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultmanualsort", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DefaultManualSort { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DetermineQuantitiesToBillBasedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}