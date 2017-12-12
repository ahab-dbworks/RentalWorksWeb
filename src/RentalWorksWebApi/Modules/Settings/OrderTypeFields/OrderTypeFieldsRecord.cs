using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderTypeFields
{
    [FwSqlTable("ordertypefields")]
    public class OrderTypeFieldsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypefieldsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeFieldsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showorderno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showrepairno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowRepairOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmasterno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icodewidth", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ICodeWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showdescription", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descwidth", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? DescriptionWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showpickdate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showfromdate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showtodate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showbillableperiods", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showsubqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowSubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showavailqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showconflictdate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showrate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showweeklycostextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowWeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmonthlycostextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showperiodcostextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showdw", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showdiscountpercent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmarkuppercent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmarginpercent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showsplit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowSplit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showunit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showunitdiscountamount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowUnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showunitextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowUnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showweeklydiscountamount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowWeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showweeklyextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmonthlydiscountamount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmonthlyextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showperioddiscountamount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showperiodextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showvariancepercent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowVariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showvarianceextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcountryoforigin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowCountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmanufacturer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowManufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmfgpartno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartnowidth", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ManufacturerPartNumberWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmodelno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showvendorpartno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowVendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showwarehouse", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showtaxable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shownotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showreturntowarehouse", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showfromtime", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showtotime", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showvehicleno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowVehicleNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showbarcode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showserialno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcrewname", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowCrewName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showhours", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showpicktime", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showavailqtyallwh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowAvailableQuantityAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showconflictdateallwh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showconsignavailqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowConsignmentAvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showconsignconflictdate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowConsignmentConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showconsignqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowConsignmentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showinlocationqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowInLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showreservedrentalitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showweeksanddays", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowWeeksAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showmonthsanddays", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowMonthsAndDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showpremiumpct", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowPremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showdepartment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showlocation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showorderactivity", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowOrderActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showsuborderno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showorderstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showepisodes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowEpisodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showepisodeextended", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowEpisodeExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showepisodediscountamount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowEpisodeDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}