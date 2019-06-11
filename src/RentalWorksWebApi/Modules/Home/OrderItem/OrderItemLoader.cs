using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Logic;
using System.Collections.Generic;
using System.Text;
using WebApi.Modules.Home.InventoryAvailability;
using System;
using WebLibrary;

namespace WebApi.Modules.Home.OrderItem
{
    [FwSqlTable("orderitemwebview")]
    public class OrderItemLoader : AppDataLoadRecord
    {
        private bool _refreshAvailability = false;   // pupulated during SetBaseSelectQuery, reused later

        public OrderItemLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "row_number() over(partition by rectype order by primaryitemorder)", modeltype: FwDataTypes.Integer)]
        public int? RowNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "row_number() over(order by primaryitemorder)", modeltype: FwDataTypes.Integer)]
        public int? RowNumberCombined { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor
        {
            get { return getICodeColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromtime", modeltype: FwDataTypes.Text)]
        public string FromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttodate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttotime", modeltype: FwDataTypes.Text)]
        public string ToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableperiods", modeltype: FwDataTypes.Decimal)]
        public decimal? BillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poitemid", modeltype: FwDataTypes.Text)]
        public string SubPurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string SubQuantityColor
        {
            get { return getSubQuantityColor(SubPurchaseOrderItemId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Integer)]
        public int? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reservedrentalitems", modeltype: FwDataTypes.Integer)]
        public int? ReservedItemQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqty", modeltype: FwDataTypes.Decimal)]
        public decimal? AvailableQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string AvailableQuantityColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqtyallwh", modeltype: FwDataTypes.Decimal)]
        public decimal? AvailableAllWarehousesQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcolorallwh", modeltype: FwDataTypes.Integer)]
        public int? AvailableAllWarehousesQuantityColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflictdate", modeltype: FwDataTypes.Date)]
        public string ConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflictdateallwh", modeltype: FwDataTypes.Date)]
        public string ConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflictdateconsign", modeltype: FwDataTypes.Date)]
        public string ConflictDateConsignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "premiumpct", modeltype: FwDataTypes.Decimal)]
        public decimal? PremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewcontactid", modeltype: FwDataTypes.Text)]
        public string CrewContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewname", modeltype: FwDataTypes.Text)]
        public string CrewName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Decimal)]
        public decimal? Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.Decimal)]
        public decimal? HoursOvertime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.Decimal)]
        public decimal? HoursDoubletime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price2", modeltype: FwDataTypes.Decimal)]
        public decimal? Price2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price3", modeltype: FwDataTypes.Decimal)]
        public decimal? Price3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price4", modeltype: FwDataTypes.Decimal)]
        public decimal? Price4 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price5", modeltype: FwDataTypes.Decimal)]
        public decimal? Price5 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpctdisplay", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercentDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitextendednodisc", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitExtendedNoDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitdiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitextended", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyextendednodisc", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyExtendedNoDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklydiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklytotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklycostextended", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklytax", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeks1through3extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Weeks1Through3Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeks4plusextended", modeltype: FwDataTypes.Decimal)]
        public decimal? Weeks4PlusExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averageweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averageweeklyextendednodisc", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageWeeklyExtendedNoDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "episodes", modeltype: FwDataTypes.Integer)]
        public int? Episodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyextendednodisc", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyExtendedNoDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlydiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycostextended", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlytax", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlytotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodextendednodisc", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodExtendedNoDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodcostextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "perioddiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtax", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodvarianceextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodVarianceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "variancepct", modeltype: FwDataTypes.Decimal)]
        public decimal? VariancePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locked", modeltype: FwDataTypes.Boolean)]
        public bool? Locked { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowarehouseid", modeltype: FwDataTypes.Text)]
        public string ReturnToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowhcode", modeltype: FwDataTypes.Text)]
        public string ReturnToWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text)]
        public string RetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string PoSubOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string PoSubOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text)]
        public string PoSubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "ldorderid", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldmasteritemid", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderno", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 





        //[FwSqlDataField(column: "notesmasteritemid", modeltype: FwDataTypes.Text)]
        //public string NotesmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarymasteritemid", modeltype: FwDataTypes.Text)]
        //public string PrimarymasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orgmasteritemid", modeltype: FwDataTypes.Text)]
        //public string OrgmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        //public string NestedmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "packageitemid", modeltype: FwDataTypes.Text)]
        //public string PackageitemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        //public string CategoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        //public string Orderno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Boolean)]
        //public bool? SessionId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sessionno", modeltype: FwDataTypes.Boolean)]
        //public bool? Sessionno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sessionlocation", modeltype: FwDataTypes.Boolean)]
        //public bool? Sessionlocation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sessionroom", modeltype: FwDataTypes.Boolean)]
        //public bool? Sessionroom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sessionorderby", modeltype: FwDataTypes.Integer)]
        //public int? Sessionorderby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "issession", modeltype: FwDataTypes.Boolean)]
        //public bool? Issession { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Boolean)]
        //public bool? Rowtype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inlocationqty", modeltype: FwDataTypes.Integer)]
        //public int? InlocationQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "varyingdatestimes", modeltype: FwDataTypes.Boolean)]
        //public bool? Varyingdatestimes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "markuppctdisplay", modeltype: FwDataTypes.Decimal)]
        //public decimal? Markuppctdisplay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal)]
        //public decimal? Markuppct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "marginpctdisplay", modeltype: FwDataTypes.Decimal)]
        //public decimal? Marginpctdisplay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        //public decimal? Marginpct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "premiumpctdisplay", modeltype: FwDataTypes.Decimal)]
        //public decimal? Premiumpctdisplay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "premiumpct", modeltype: FwDataTypes.Decimal)]
        //public decimal? Premiumpct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "split", modeltype: FwDataTypes.Integer)]
        //public int? Split { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "whcodesummary", modeltype: FwDataTypes.Text)]
        //public string Whcodesummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returntowhcodesummary", modeltype: FwDataTypes.Text)]
        //public string Returntowhcodesummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "warehouseidsummary", modeltype: FwDataTypes.Text)]
        //public string Warehouseidsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returntowarehouseidsummary", modeltype: FwDataTypes.Text)]
        //public string Returntowarehouseidsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manualbillflg", modeltype: FwDataTypes.Boolean)]
        //public bool? Manualbillflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        //public string UnitId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unittype", modeltype: FwDataTypes.Text)]
        //public string Unittype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterclass", modeltype: FwDataTypes.Text)]
        //public string Masterclass { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterinactive", modeltype: FwDataTypes.Boolean)]
        //public bool? Masterinactive { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "parentmasterid", modeltype: FwDataTypes.Text)]
        //public string ParentmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "parentparentid", modeltype: FwDataTypes.Text)]
        //public string ParentparentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "candiscount", modeltype: FwDataTypes.Boolean)]
        //public bool? Candiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        //public bool? Optioncolor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldoutcontractid", modeltype: FwDataTypes.Text)]
        //public string LdoutcontractId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldpoid", modeltype: FwDataTypes.Text)]
        //public string LdpoId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldpoitemid", modeltype: FwDataTypes.Text)]
        //public string LdpoitemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        //public string PoId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "haspoitem", modeltype: FwDataTypes.Boolean)]
        //public bool? Haspoitem { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returnflg", modeltype: FwDataTypes.Boolean)]
        //public bool? Returnflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        //public string RepairId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean)]
        //public bool? Hastieredcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal)]
        //public decimal? Retail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manufacturerid", modeltype: FwDataTypes.Text)]
        //public string ManufacturerId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        //public string Manufacturer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        //public string Partnumber { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vendorpartno", modeltype: FwDataTypes.Text)]
        //public string Vendorpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vehicleno", modeltype: FwDataTypes.Text)]
        //public string Vehicleno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "taxrate1", modeltype: FwDataTypes.Decimal)]
        //public decimal? Taxrate1 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "taxrate2", modeltype: FwDataTypes.Decimal)]
        //public decimal? Taxrate2 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "recurringratetype", modeltype: FwDataTypes.Boolean)]
        //public bool? Recurringratetype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairorderno", modeltype: FwDataTypes.Text)]
        //public string Repairorderno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "mfgmodel", modeltype: FwDataTypes.Text)]
        //public string Mfgmodel { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "countryoforiginid", modeltype: FwDataTypes.Text)]
        //public string CountryoforiginId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        //public string Countryoforigin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "excludefromquikpaydiscount", modeltype: FwDataTypes.Boolean)]
        //public bool? Excludefromquikpaydiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availcolorsummary", modeltype: FwDataTypes.Integer)]
        //public int? Availcolorsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availcolorallwh", modeltype: FwDataTypes.Integer)]
        //public int? Availcolorallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availcolorconsign", modeltype: FwDataTypes.Integer)]
        //public int? Availcolorconsign { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availcolorconsignsummary", modeltype: FwDataTypes.Integer)]
        //public int? Availcolorconsignsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primaryitemorder", modeltype: FwDataTypes.Text)]
        //public string Primaryitemorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text)]
        //public string RatemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        //public bool? Noavail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
        //public bool? Availbyhour { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean)]
        //public bool? Availbyasset { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean)]
        //public bool? Availbydeal { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availcachedays", modeltype: FwDataTypes.Integer)]
        //public int? Availcachedays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availsequence", modeltype: FwDataTypes.Text)]
        //public string Availsequence { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflict", modeltype: FwDataTypes.Boolean)]
        //public bool? Conflict { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "forceconflictflg", modeltype: FwDataTypes.Boolean)]
        //public bool? Forceconflictflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "positiveconflict", modeltype: FwDataTypes.Boolean)]
        //public bool? Positiveconflict { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "issplit", modeltype: FwDataTypes.Boolean)]
        //public bool? Issplit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "isrecurring", modeltype: FwDataTypes.Boolean)]
        //public bool? Isrecurring { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ismultivendorinvoice", modeltype: FwDataTypes.Boolean)]
        //public bool? Ismultivendorinvoice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountoverride", modeltype: FwDataTypes.Text)]
        //public string Discountoverride { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Boolean)]
        //public bool? Availfrom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hasitemdiscountschedule", modeltype: FwDataTypes.Boolean)]
        //public bool? Hasitemdiscountschedule { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "linkedmasteritemid", modeltype: FwDataTypes.Text)]
        //public string LinkedmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "isprep", modeltype: FwDataTypes.Boolean)]
        //public bool? Isprep { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "displaywhenrateiszero", modeltype: FwDataTypes.Boolean)]
        //public bool? Displaywhenrateiszero { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        //public string Ordertype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availfromdatetime", modeltype: FwDataTypes.Date)]
        //public string Availfromdatetime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availtodatetime", modeltype: FwDataTypes.Date)]
        //public string Availtodatetime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "billedamount", modeltype: FwDataTypes.Decimal)]
        //public decimal? Billedamount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesmasterid", modeltype: FwDataTypes.Text)]
        //public string SalesmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text)]
        //public string Activity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "toomanystagedout", modeltype: FwDataTypes.Boolean)]
        //public bool? Toomanystagedout { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salescheckedin", modeltype: FwDataTypes.Boolean)]
        //public bool? Salescheckedin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        //public string Orderby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ispending", modeltype: FwDataTypes.Boolean)]
        //public bool? Ispending { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Quoteprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Orderprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Picklistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Contractoutprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Contractinprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Returnlistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Invoiceprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Poprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Contractreceiveprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Contractreturnprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Poreceivelistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean)]
        //public bool? Poreturnlistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "weeksanddays", modeltype: FwDataTypes.Text)]
        //public string Weeksanddays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "monthsanddays", modeltype: FwDataTypes.Text)]
        //public string Monthsanddays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "weeksanddaysexcluded", modeltype: FwDataTypes.Boolean)]
        //public bool? Weeksanddaysexcluded { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdate", modeltype: FwDataTypes.Date)]
        //public string Conflictdate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdatesummary", modeltype: FwDataTypes.Date)]
        //public string Conflictdatesummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdateallwh", modeltype: FwDataTypes.Date)]
        //public string Conflictdateallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdateconsign", modeltype: FwDataTypes.Date)]
        //public string Conflictdateconsign { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdateconsignsummary", modeltype: FwDataTypes.Date)]
        //public string Conflictdateconsignsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictdateconsignallwh", modeltype: FwDataTypes.Date)]
        //public string Conflictdateconsignallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availiscurrent", modeltype: FwDataTypes.Boolean)]
        //public bool? Availiscurrent { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availiscurrentallwh", modeltype: FwDataTypes.Boolean)]
        //public bool? Availiscurrentallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqtysummary", modeltype: FwDataTypes.Decimal)]
        //public decimal? AvailQuantitysummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqtyallwh", modeltype: FwDataTypes.Decimal)]
        //public decimal? AvailQuantityallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqtyconsign", modeltype: FwDataTypes.Integer)]
        //public int? AvailQuantityconsign { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqtyconsignsummary", modeltype: FwDataTypes.Integer)]
        //public int? AvailQuantityconsignsummary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqtyconsignallwh", modeltype: FwDataTypes.Integer)]
        //public int? AvailQuantityconsignallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldoutqty", modeltype: FwDataTypes.Integer)]
        //public int? LdoutQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ownedordertrans", modeltype: FwDataTypes.Integer)]
        //public int? Ownedordertrans { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inqty", modeltype: FwDataTypes.Decimal)]
        //public decimal? InQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rowsummarized", modeltype: FwDataTypes.Boolean)]
        //public bool? Rowsummarized { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "isprimary", modeltype: FwDataTypes.Boolean)]
        //public bool? Isprimary { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesqtyonhand", modeltype: FwDataTypes.Decimal)]
        //public decimal? SalesQuantityonhand { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "billedinfull", modeltype: FwDataTypes.Boolean)]
        //public bool? Billedinfull { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtyadjusted", modeltype: FwDataTypes.Boolean)]
        //public bool? Quantityadjusted { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "shortage", modeltype: FwDataTypes.Boolean)]
        //public bool? Shortage { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accratio", modeltype: FwDataTypes.Decimal)]
        //public decimal? Accratio { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text)]
        //public string SpacetypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "schedulestatusid", modeltype: FwDataTypes.Text)]
        //public string SchedulestatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "iteminactive", modeltype: FwDataTypes.Boolean)]
        //public bool? Iteminactive { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prorateweeks", modeltype: FwDataTypes.Boolean)]
        //public bool? Prorateweeks { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text)]
        //public string OriginalshowId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "proratemonths", modeltype: FwDataTypes.Boolean)]
        //public bool? Proratemonths { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "includeonpicklist", modeltype: FwDataTypes.Boolean)]
        //public bool? Includeonpicklist { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "totimeestimated", modeltype: FwDataTypes.Boolean)]
        //public bool? Totimeestimated { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ismaxdayloc", modeltype: FwDataTypes.Boolean)]
        //public bool? Ismaxdayloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ismaxloc", modeltype: FwDataTypes.Boolean)]
        //public bool? Ismaxloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "transactionno", modeltype: FwDataTypes.Text)]
        //public string Transactionno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sourcecode", modeltype: FwDataTypes.Text)]
        //public string Sourcecode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accountingcode", modeltype: FwDataTypes.Text)]
        //public string Accountingcode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "buyerid", modeltype: FwDataTypes.Text)]
        //public string BuyerId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "buyer", modeltype: FwDataTypes.Text)]
        //public string Buyer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "character", modeltype: FwDataTypes.Text)]
        //public string Character { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prepfees", modeltype: FwDataTypes.Decimal)]
        //public decimal? Prepfees { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "periodextendedincludesprep", modeltype: FwDataTypes.Boolean)]
        //public bool? Periodextendedincludesprep { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderactivity", modeltype: FwDataTypes.Text)]
        //public string Orderactivity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "issubstitute", modeltype: FwDataTypes.Boolean)]
        //public bool? Issubstitute { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantitystaged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantityout { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantityin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtyreceived", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantityreceived { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qtyreturned", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantityreturned { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string orderId = OrderId;
            bool summaryMode = false;
            bool subs = false;
            bool refreshAvailability = false;

            if (string.IsNullOrEmpty(orderId))
            {
                orderId = GetUniqueIdAsString("OrderId", request);
            }

            if (string.IsNullOrEmpty(orderId))
            {
                if (!string.IsNullOrEmpty(OrderItemId))
                {
                    string[] values = AppFunc.GetStringDataAsync(AppConfig, "masteritem", new string[] { "masteritemid" }, new string[] { OrderItemId }, new string[] { "orderid", "poorderid" }).Result;
                    orderId = values[0];
                    subs = (!values[1].Equals(""));
                }
            }

            if (string.IsNullOrEmpty(orderId))
            {
                orderId = "~xx~";
            }

            summaryMode = GetUniqueIdAsBoolean("Summary", request).GetValueOrDefault(false);

            if (!subs)
            {
                subs = GetUniqueIdAsBoolean("Subs", request).GetValueOrDefault(false);
            }

            refreshAvailability = GetUniqueIdAsBoolean("RefreshAvailability", request).GetValueOrDefault(false);

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            if (summaryMode)
            {
                StringBuilder summaryWhere = new StringBuilder();
                summaryWhere.Append(" (not                     ");
                summaryWhere.Append("     ( ");
                summaryWhere.Append("     (isnull(substring(itemclass, 2, 1), '') in ('I', 'O')) ");
                summaryWhere.Append("     and   displaywhenrateiszero <> 'T'");
                summaryWhere.Append("     and   price                 = 0  ");
                summaryWhere.Append("     and   parentid             <> ''");
                summaryWhere.Append("     )                        ");
                summaryWhere.Append(" )                        ");
                select.AddWhere(summaryWhere.ToString());
            }

            select.AddWhere("poorderid " + (subs ? ">" : "=") + "''");

            addFilterToSelect("RecType", "rectype", select, request);

            select.AddWhere("orderid = @orderid");
            select.AddParameter("@orderid", orderId);

            // saved here for AfterBrowse
            _refreshAvailability = refreshAvailability;

        }
        //------------------------------------------------------------------------------------ 
        private string getICodeColor(string itemClass)
        {
            return AppFunc.GetItemClassICodeColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getDescriptionColor(string itemClass)
        {
            return AppFunc.GetItemClassDescriptionColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getSubQuantityColor(string poItemId)
        {
            return (string.IsNullOrEmpty(poItemId) ? null : RwGlobals.SUB_COLOR);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                        string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                        DateTime fromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("FromDate")].ToString());   // not accurate
                        DateTime toDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("ToDate")].ToString());       // not accurate
                        availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDateTime, toDateTime));
                    }

                    TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems, _refreshAvailability).Result;

                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                        string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                        DateTime availFromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("FromDate")].ToString());  // not accurate
                        DateTime availToDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("ToDate")].ToString());      // not accurate
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                        TInventoryWarehouseAvailability availData = null;
                        row[dt.GetColumnNo("AvailableQuantityColor")] = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEEDRECALC);
                        if (availCache.TryGetValue(availKey, out availData))
                        {
                            row[dt.GetColumnNo("AvailableQuantityColor")] = null;
                            TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(availFromDateTime, availToDateTime);
                            row[dt.GetColumnNo("AvailableQuantity")] = minAvail.MinimumAvailable;
                            row[dt.GetColumnNo("AvailableQuantityColor")] = minAvail.Color;
                        }
                        row[dt.GetColumnNo("ICodeColor")] = getICodeColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("SubQuantityColor")] = getSubQuantityColor(row[dt.GetColumnNo("SubPurchaseOrderItemId")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}