using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.MasterItem;

namespace WebApi.Modules.Home.OrderItem
{
    public class OrderItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterItemRecord orderItem = new MasterItemRecord();
        OrderItemLoader orderItemLoader = new OrderItemLoader();
        public OrderItemLogic()
        {
            dataRecords.Add(orderItem);
            dataLoader = orderItemLoader;
            orderItem.AfterSave += OnAfterSaveOrderItem;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderItemId { get { return orderItem.MasterItemId; } set { orderItem.MasterItemId = value; } }
        public string OrderId { get { return orderItem.OrderId; } set { orderItem.OrderId = value; } }
        public string RecType { get { return orderItem.RecType; } set { orderItem.RecType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? RowNumber { get; set; }
        public string InventoryId { get { return orderItem.InventoryId; } set { orderItem.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        public string ICodeColor { get; set; }
        public string Description { get { return orderItem.Description; } set { orderItem.Description = value; } }
        public string DescriptionColor { get; set; }
        public string PickDate { get { return orderItem.PickDate; } set { orderItem.PickDate = value; } }
        public string PickTime { get { return orderItem.PickTime; } set { orderItem.PickTime = value; } }
        public string FromDate { get { return orderItem.FromDate; } set { orderItem.FromDate = value; } }
        public string FromTime { get { return orderItem.FromTime; } set { orderItem.FromTime = value; } }
        public string ToDate { get { return orderItem.ToDate; } set { orderItem.ToDate = value; } }
        public string ToTime { get { return orderItem.ToTime; } set { orderItem.ToTime = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? BillablePeriods { get; set; }
        public decimal? QuantityOrdered { get { return orderItem.QuantityOrdered; } set { orderItem.QuantityOrdered = value; } }
        public decimal? SubQuantity { get { return orderItem.SubQuantity; } set { orderItem.SubQuantity = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AvailableQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? AvailableQuantityColor { get; set; }
        public decimal? Price { get { return orderItem.Price; } set { orderItem.Price = value; } }
        public decimal? Price2 { get { return orderItem.Price2; } set { orderItem.Price2 = value; } }
        public decimal? Price3 { get { return orderItem.Price3; } set { orderItem.Price3 = value; } }
        public decimal? Price4 { get { return orderItem.Price4; } set { orderItem.Price4 = value; } }
        public decimal? Price5 { get { return orderItem.Price5; } set { orderItem.Price5 = value; } }
        public decimal? DaysPerWeek { get { return orderItem.DaysPerWeek; } set { orderItem.DaysPerWeek = value; } }
        public decimal? DiscountPercent { get { return orderItem.DiscountPercent; } set { orderItem.DiscountPercent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DiscountPercentDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodDiscountAmount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodExtended { get; set; }
        public bool? Taxable { get { return orderItem.Taxable; } set { orderItem.Taxable = value; } }

        public string WarehouseId { get { return orderItem.WarehouseId; } set { orderItem.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        public string ReturnToWarehouseId { get { return orderItem.ReturnToWarehouseId; } set { orderItem.ReturnToWarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReturnToWarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }


        //[FwBusinessLogicField(isReadOnly: true)]
        //public string NotesmasteritemId { get; set; }
        //public string PrimarymasteritemId { get { return orderItem.PrimarymasteritemId; } set { orderItem.PrimarymasteritemId = value; } }
        //public string OrgmasteritemId { get { return orderItem.OrgmasteritemId; } set { orderItem.OrgmasteritemId = value; } }
        //public string NestedmasteritemId { get { return orderItem.NestedmasteritemId; } set { orderItem.NestedmasteritemId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PackageitemId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string CategoryId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Orderno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? SessionId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Sessionno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Sessionlocation { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Sessionroom { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Sessionorderby { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Issession { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Rowtype { get; set; }
        //public string Mfgpartno { get { return orderItem.Mfgpartno; } set { orderItem.Mfgpartno = value; } }
        //public decimal? SubQuantity { get { return orderItem.SubQuantity; } set { orderItem.SubQuantity = value; } }
        //public int? ConsignQuantity { get { return orderItem.ConsignQuantity; } set { orderItem.ConsignQuantity = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? InlocationQuantity { get; set; }
        //public decimal? Price { get { return orderItem.Price; } set { orderItem.Price = value; } }
        //public decimal? Price2 { get { return orderItem.Price2; } set { orderItem.Price2 = value; } }
        //public decimal? Price3 { get { return orderItem.Price3; } set { orderItem.Price3 = value; } }
        //public decimal? Price4 { get { return orderItem.Price4; } set { orderItem.Price4 = value; } }
        //public decimal? Price5 { get { return orderItem.Price5; } set { orderItem.Price5 = value; } }
        //public decimal? Cost { get { return orderItem.Cost; } set { orderItem.Cost = value; } }
        //public decimal? Daysinwk { get { return orderItem.Daysinwk; } set { orderItem.Daysinwk = value; } }
        //public decimal? Hours { get { return orderItem.Hours; } set { orderItem.Hours = value; } }
        //public decimal? Hoursot { get { return orderItem.Hoursot; } set { orderItem.Hoursot = value; } }
        //public decimal? Hoursdt { get { return orderItem.Hoursdt; } set { orderItem.Hoursdt = value; } }
        //public string Pickdate { get { return orderItem.Pickdate; } set { orderItem.Pickdate = value; } }
        //public string Picktime { get { return orderItem.Picktime; } set { orderItem.Picktime = value; } }
        //public string Rentfromdate { get { return orderItem.Rentfromdate; } set { orderItem.Rentfromdate = value; } }
        //public string Rentfromtime { get { return orderItem.Rentfromtime; } set { orderItem.Rentfromtime = value; } }
        //public string Renttodate { get { return orderItem.Renttodate; } set { orderItem.Renttodate = value; } }
        //public string Renttotime { get { return orderItem.Renttotime; } set { orderItem.Renttotime = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Varyingdatestimes { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Discountpctdisplay { get; set; }
        //public decimal? Discountpct { get { return orderItem.Discountpct; } set { orderItem.Discountpct = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Markuppctdisplay { get; set; }
        //public decimal? Markuppct { get { return orderItem.Markuppct; } set { orderItem.Markuppct = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Marginpctdisplay { get; set; }
        //public decimal? Marginpct { get { return orderItem.Marginpct; } set { orderItem.Marginpct = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Premiumpctdisplay { get; set; }
        //public decimal? Premiumpct { get { return orderItem.Premiumpct; } set { orderItem.Premiumpct = value; } }
        //public int? Split { get { return orderItem.Split; } set { orderItem.Split = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Whcodesummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Returntowhcode { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Returntowhcodesummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Warehouseidsummary { get; set; }
        //public string ReturntowarehouseId { get { return orderItem.ReturntowarehouseId; } set { orderItem.ReturntowarehouseId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Returntowarehouseidsummary { get; set; }
        //public bool? Locked { get { return orderItem.Locked; } set { orderItem.Locked = value; } }
        //public bool? Taxable { get { return orderItem.Taxable; } set { orderItem.Taxable = value; } }
        //public bool? Manualbillflg { get { return orderItem.Manualbillflg; } set { orderItem.Manualbillflg = value; } }
        //public string UnitId { get { return orderItem.UnitId; } set { orderItem.UnitId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Unit { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Unittype { get; set; }
        //public string ParentId { get { return orderItem.ParentId; } set { orderItem.ParentId = value; } }
        //public string Itemclass { get { return orderItem.Itemclass; } set { orderItem.Itemclass = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Masterclass { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Masterinactive { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ParentmasterId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ParentparentId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Candiscount { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Optioncolor { get; set; }
        //public string LdorderId { get { return orderItem.LdorderId; } set { orderItem.LdorderId = value; } }
        //public string LdmasteritemId { get { return orderItem.LdmasteritemId; } set { orderItem.LdmasteritemId = value; } }
        //public string LdoutcontractId { get { return orderItem.LdoutcontractId; } set { orderItem.LdoutcontractId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string LdpoId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string LdpoitemId { get; set; }
        //public string PoorderId { get { return orderItem.PoorderId; } set { orderItem.PoorderId = value; } }
        //public string PomasteritemId { get { return orderItem.PomasteritemId; } set { orderItem.PomasteritemId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoitemId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Haspoitem { get; set; }
        //public bool? Returnflg { get { return orderItem.Returnflg; } set { orderItem.Returnflg = value; } }
        //public string RepairId { get { return orderItem.RepairId; } set { orderItem.RepairId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Hastieredcost { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Retail { get; set; }
        //public string ManufacturerId { get { return orderItem.ManufacturerId; } set { orderItem.ManufacturerId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Manufacturer { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Partnumber { get; set; }
        //public string Vendorpartno { get { return orderItem.Vendorpartno; } set { orderItem.Vendorpartno = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Vehicleno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Barcode { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Serialno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Taxrate1 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Taxrate2 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Recurringratetype { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Ldorderno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Poorderno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Repairorderno { get; set; }
        //public string Mfgmodel { get { return orderItem.Mfgmodel; } set { orderItem.Mfgmodel = value; } }
        //public string CountryoforiginId { get { return orderItem.CountryoforiginId; } set { orderItem.CountryoforiginId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Countryoforigin { get; set; }
        //public bool? Bold { get { return orderItem.Bold; } set { orderItem.Bold = value; } }
        //public bool? Excludefromquikpaydiscount { get { return orderItem.Excludefromquikpaydiscount; } set { orderItem.Excludefromquikpaydiscount = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcolor { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcolorsummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcolorallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcolorconsign { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcolorconsignsummary { get; set; }
        //public string Rectype { get { return orderItem.Rectype; } set { orderItem.Rectype = value; } }
        //public string Itemorder { get { return orderItem.Itemorder; } set { orderItem.Itemorder = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Primaryitemorder { get; set; }
        //public string RatemasterId { get { return orderItem.RatemasterId; } set { orderItem.RatemasterId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Noavail { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availbyhour { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availbyasset { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availbydeal { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Availcachedays { get; set; }
        //public string Availsequence { get { return orderItem.Availsequence; } set { orderItem.Availsequence = value; } }
        //public bool? Conflict { get { return orderItem.Conflict; } set { orderItem.Conflict = value; } }
        //public bool? Forceconflictflg { get { return orderItem.Forceconflictflg; } set { orderItem.Forceconflictflg = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Positiveconflict { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Issplit { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Isrecurring { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Ismultivendorinvoice { get; set; }
        //public string RentalitemId { get { return orderItem.RentalitemId; } set { orderItem.RentalitemId = value; } }
        //public string CrewcontactId { get { return orderItem.CrewcontactId; } set { orderItem.CrewcontactId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Crewname { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Discountoverride { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availfrom { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Hasitemdiscountschedule { get; set; }
        //public string LinkedmasteritemId { get { return orderItem.LinkedmasteritemId; } set { orderItem.LinkedmasteritemId = value; } }
        //public string RetiredreasonId { get { return orderItem.RetiredreasonId; } set { orderItem.RetiredreasonId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Retiredreason { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Isprep { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Displaywhenrateiszero { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Ordertype { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Availfromdatetime { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Availtodatetime { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Billedamount { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string SalesmasterId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Activity { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Toomanystagedout { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Salescheckedin { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Orderby { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Ispending { get; set; }
        //public bool? Quoteprint { get { return orderItem.Quoteprint; } set { orderItem.Quoteprint = value; } }
        //public bool? Orderprint { get { return orderItem.Orderprint; } set { orderItem.Orderprint = value; } }
        //public bool? Picklistprint { get { return orderItem.Picklistprint; } set { orderItem.Picklistprint = value; } }
        //public bool? Contractoutprint { get { return orderItem.Contractoutprint; } set { orderItem.Contractoutprint = value; } }
        //public bool? Contractinprint { get { return orderItem.Contractinprint; } set { orderItem.Contractinprint = value; } }
        //public bool? Returnlistprint { get { return orderItem.Returnlistprint; } set { orderItem.Returnlistprint = value; } }
        //public bool? Invoiceprint { get { return orderItem.Invoiceprint; } set { orderItem.Invoiceprint = value; } }
        //public bool? Poprint { get { return orderItem.Poprint; } set { orderItem.Poprint = value; } }
        //public bool? Contractreceiveprint { get { return orderItem.Contractreceiveprint; } set { orderItem.Contractreceiveprint = value; } }
        //public bool? Contractreturnprint { get { return orderItem.Contractreturnprint; } set { orderItem.Contractreturnprint = value; } }
        //public bool? Poreceivelistprint { get { return orderItem.Poreceivelistprint; } set { orderItem.Poreceivelistprint = value; } }
        //public bool? Poreturnlistprint { get { return orderItem.Poreturnlistprint; } set { orderItem.Poreturnlistprint = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Billableperiods { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Weeksanddays { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Monthsanddays { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Weeksanddaysexcluded { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Unitextendednodisc { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Unitdiscountamt { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Unitextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeklyextendednodisc { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeklydiscountamt { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeklyextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeklycostextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Week2extended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Week3extended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeks1through3extended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Weeks4plusextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Week4extended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Averageweekly { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Averageweeklyextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Averageweeklyextendednodisc { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Episodes { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Episodediscountamt { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Episodeextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Monthlyextendednodisc { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Monthlydiscountamt { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Monthlyextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Monthlycostextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Periodextendednodisc { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Perioddiscountamt { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Periodextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Periodcostextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Periodvarianceextended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Variancepct { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdate { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdatesummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdateallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdateconsign { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdateconsignsummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Conflictdateconsignallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availiscurrent { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Availiscurrentallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? AvailQuantity { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? AvailQuantitysummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? AvailQuantityallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? AvailQuantityconsign { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? AvailQuantityconsignsummary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? AvailQuantityconsignallwh { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? LdoutQuantity { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Notes { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Ownedordertrans { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? InQuantity { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Rowsummarized { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Isprimary { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? SalesQuantityonhand { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Billedinfull { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Quantityadjusted { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Shortage { get; set; }
        //public decimal? Accratio { get { return orderItem.Accratio; } set { orderItem.Accratio = value; } }
        //public string SpacetypeId { get { return orderItem.SpacetypeId; } set { orderItem.SpacetypeId = value; } }
        //public string SchedulestatusId { get { return orderItem.SchedulestatusId; } set { orderItem.SchedulestatusId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Iteminactive { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Reservedrentalitems { get; set; }
        //public bool? Prorateweeks { get { return orderItem.Prorateweeks; } set { orderItem.Prorateweeks = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string OriginalshowId { get; set; }
        //public bool? Proratemonths { get { return orderItem.Proratemonths; } set { orderItem.Proratemonths = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Includeonpicklist { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Totimeestimated { get; set; }
        //public bool? Ismaxdayloc { get { return orderItem.Ismaxdayloc; } set { orderItem.Ismaxdayloc = value; } }
        //public bool? Ismaxloc { get { return orderItem.Ismaxloc; } set { orderItem.Ismaxloc = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Transactionno { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Sourcecode { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Accountingcode { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string BuyerId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Buyer { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Character { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Prepfees { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Periodextendedincludesprep { get; set; }
        //public string Orderactivity { get { return orderItem.Orderactivity; } set { orderItem.Orderactivity = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Issubstitute { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Quantitystaged { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Quantityout { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Quantityin { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Quantityreceived { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Quantityreturned { get; set; }
        public string DateStamp { get { return orderItem.DateStamp; } set { orderItem.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveOrderItem(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = orderItem.SaveNoteASync(Notes).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}