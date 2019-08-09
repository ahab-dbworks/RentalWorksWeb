using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi.Modules.Settings.OrderTypeFields;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.OrderType
{
    [FwLogic(Id:"j6CoqCXopPET")]
    public class OrderTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord orderType = new OrderTypeRecord();
        OrderTypeFieldsRecord rentalOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord salesOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord laborOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord miscOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord spaceOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord vehicleOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord rentalSaleOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord lossAndDamageOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeLoader orderTypeLoader = new OrderTypeLoader();
        OrderTypeBrowseLoader orderTypeBrowseLoader = new OrderTypeBrowseLoader();

        public OrderTypeLogic()
        {
            dataRecords.Add(orderType);
            dataRecords.Add(rentalOrderTypeFields);
            dataRecords.Add(salesOrderTypeFields);
            dataRecords.Add(laborOrderTypeFields);
            dataRecords.Add(miscOrderTypeFields);
            dataRecords.Add(spaceOrderTypeFields);
            dataRecords.Add(vehicleOrderTypeFields);
            dataRecords.Add(rentalSaleOrderTypeFields);
            dataRecords.Add(lossAndDamageOrderTypeFields);
            dataLoader = orderTypeLoader;
            browseLoader = orderTypeBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"PxY0D2RLNuDO", IsPrimaryKey:true)]
        public string OrderTypeId { get { return orderType.OrderTypeId; } set { orderType.OrderTypeId = value; } }

        [FwLogicProperty(Id:"PxY0D2RLNuDO", IsRecordTitle:true)]
        public string OrderType { get { return orderType.OrderType; } set { orderType.OrderType = value; } }

        [FwLogicProperty(Id:"feYHTd0dMt0f")]
        public string OrdType { get { return orderType.Ordtype; } set { orderType.Ordtype = value; } }

        [FwLogicProperty(Id:"idEMUXfVyeWr")]
        public string DefaultPickTime { get { return orderType.Picktime; } set { orderType.Picktime = value; } }

        [FwLogicProperty(Id:"XMi7px7i1ouI")]
        public string DefaultFromTime { get { return orderType.Fromtime; } set { orderType.Fromtime = value; } }

        [FwLogicProperty(Id:"luJQUyBDLYQh")]
        public string DefaultToTime { get { return orderType.Totime; } set { orderType.Totime = value; } }

        [FwLogicProperty(Id:"BorqyXzVmDd2")]
        public string DailyScheduleDefaultStartTime { get { return orderType.Defaultdaystarttime; } set { orderType.Defaultdaystarttime = value; } }

        [FwLogicProperty(Id:"OooTNOFHzKND")]
        public string DailyScheduleDefaultStopTime { get { return orderType.Defaultdaystoptime; } set { orderType.Defaultdaystoptime = value; } }

        [FwLogicProperty(Id:"imMYc95irIfL")]
        public bool? IsMasterSubOrderType { get { return orderType.Ismastersuborder; } set { orderType.Ismastersuborder = value; } }

        [FwLogicProperty(Id:"jpC5QlAPHP31")]
        public bool? CombineActivityTabs { get { return orderType.CombineActivityTabs; } set { orderType.CombineActivityTabs = value; } }




        //rental fields
        [JsonIgnore]
        [FwLogicProperty(Id:"9gUR7H9NQn5s")]
        public string RentalOrderTypeFieldsId { get { return rentalOrderTypeFields.OrderTypeFieldsId; } set { orderType.RentalOrderTypeFieldsId = value; rentalOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"CYM08oXtTmka")]
        //public bool? RentalShowOrderNumber { get { return rentalOrderTypeFields.ShowOrderNumber; } set { rentalOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"RVdKYMsqZntj")]
        public bool? RentalShowICode { get { return rentalOrderTypeFields.ShowICode; } set { rentalOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"f7lCpimK0HJg")]
        public int? RentalICodeWidth { get { return rentalOrderTypeFields.ICodeWidth; } set { rentalOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"wp2MA0dQwszP")]
        public bool? RentalShowDescription { get { return rentalOrderTypeFields.ShowDescription; } set { rentalOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"0hkz3ZKHBFKE")]
        public int? RentalDescriptionWidth { get { return rentalOrderTypeFields.DescriptionWidth; } set { rentalOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"ziajpkXyQKBS")]
        public bool? RentalShowPickDate { get { return rentalOrderTypeFields.ShowPickDate; } set { rentalOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"o4aUrEyuRqAG")]
        public bool? RentalShowPickTime { get { return rentalOrderTypeFields.ShowPickTime; } set { rentalOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"zZYKTrUSMNg0")]
        public bool? RentalShowFromDate { get { return rentalOrderTypeFields.ShowFromDate; } set { rentalOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"isSCdEhiNZeU")]
        public bool? RentalShowFromTime { get { return rentalOrderTypeFields.ShowFromTime; } set { rentalOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"DiwayP2P7tgl")]
        public bool? RentalShowToDate { get { return rentalOrderTypeFields.ShowToDate; } set { rentalOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"FCyF0IZIXYVm")]
        public bool? RentalShowToTime { get { return rentalOrderTypeFields.ShowToTime; } set { rentalOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"v0ofJhUUAruQ")]
        public bool? RentalShowBillablePeriods { get { return rentalOrderTypeFields.ShowBillablePeriods; } set { rentalOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"N8wFeqqLGT4e")]
        public bool? RentalShowEpisodes { get { return rentalOrderTypeFields.ShowEpisodes; } set { rentalOrderTypeFields.ShowEpisodes = value; } }

        [FwLogicProperty(Id:"2jQMAkhR1qZJ")]
        public bool? RentalShowSubQuantity { get { return rentalOrderTypeFields.ShowSubQuantity; } set { rentalOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"LQGjaI9nq7cy")]
        public bool? RentalShowAvailableQuantity { get { return rentalOrderTypeFields.ShowAvailableQuantity; } set { rentalOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"f2uwdTGV1ZOv")]
        public bool? RentalShowConflictDate { get { return rentalOrderTypeFields.ShowConflictDate; } set { rentalOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"KzX2c0iRzHFH")]
        public bool? RentalShowAvailableQuantityAllWarehouses { get { return rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        [FwLogicProperty(Id:"TNMlpuXhmA2P")]
        public bool? RentalShowConflictDateAllWarehouses { get { return rentalOrderTypeFields.ShowConflictDateAllWarehouses; } set { rentalOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        [FwLogicProperty(Id:"1l8xeNriytYB")]
        public bool? RentalShowReservedItems { get { return rentalOrderTypeFields.ShowReservedItems; } set { rentalOrderTypeFields.ShowReservedItems = value; } }

        [FwLogicProperty(Id:"UeGrwnoW3skg")]
        public bool? RentalShowConsignmentQuantity { get { return rentalOrderTypeFields.ShowConsignmentQuantity; } set { rentalOrderTypeFields.ShowConsignmentQuantity = value; } }

        [FwLogicProperty(Id:"dZM5pE5oKQVG")]
        public bool? RentalShowConsignmentAvailableQuantity { get { return rentalOrderTypeFields.ShowConsignmentAvailableQuantity; } set { rentalOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        [FwLogicProperty(Id:"5JKyfbUy7INE")]
        public bool? RentalShowConsignmentConflictDate { get { return rentalOrderTypeFields.ShowConsignmentConflictDate; } set { rentalOrderTypeFields.ShowConsignmentConflictDate = value; } }

        [FwLogicProperty(Id:"hC3qF3naO1dw")]
        public bool? RentalShowRate { get { return rentalOrderTypeFields.ShowRate; } set { rentalOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"8gHQoDRUUmaC")]
        public bool? RentalShowDaysPerWeek { get { return rentalOrderTypeFields.ShowDaysPerWeek; } set { rentalOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"AoUwNcLYwzyx")]
        public bool? RentalShowPremiumPercent { get { return rentalOrderTypeFields.ShowPremiumPercent; } set { rentalOrderTypeFields.ShowPremiumPercent = value; } }

        [FwLogicProperty(Id:"ueS6I2a8cVKE")]
        public bool? RentalShowUnit { get { return rentalOrderTypeFields.ShowUnit; } set { rentalOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"feY8WMRTwOP7")]
        public bool? RentalShowCost { get { return rentalOrderTypeFields.ShowCost; } set { rentalOrderTypeFields.ShowCost = value; } }


        //[FwLogicProperty(Id:"Liep3PdU6HiJ")]
        //public bool? RentalShowWeeklyCostExtended { get { return rentalOrderTypeFields.ShowWeeklyCostExtended; } set { rentalOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"LYN8AJG5zSia")]
        //public bool? RentalShowMonthlyCostExtended { get { return rentalOrderTypeFields.ShowMonthlyCostExtended; } set { rentalOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"7o70mG7UnKwj")]
        //public bool? RentalShowPeriodCostExtended { get { return rentalOrderTypeFields.ShowPeriodCostExtended; } set { rentalOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"aOePQPWqucTA")]
        public bool? RentalShowDiscountPercent { get { return rentalOrderTypeFields.ShowDiscountPercent; } set { rentalOrderTypeFields.ShowDiscountPercent = value; } }

        [FwLogicProperty(Id:"0CcPC94A5OSk")]
        public bool? RentalShowMarkupPercent { get { return rentalOrderTypeFields.ShowMarkupPercent; } set { rentalOrderTypeFields.ShowMarkupPercent = value; } }

        [FwLogicProperty(Id:"h0JxM9vZsuyY")]
        public bool? RentalShowMarginPercent { get { return rentalOrderTypeFields.ShowMarginPercent; } set { rentalOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"I4jadkipD488")]
        public bool? RentalShowUnitDiscountAmount { get { return rentalOrderTypeFields.ShowUnitDiscountAmount; } set { rentalOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"3WJFFkGi2Cbb")]
        public bool? RentalShowUnitExtended { get { return rentalOrderTypeFields.ShowUnitExtended; } set { rentalOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"mK89UBfX3Ehy")]
        public bool? RentalShowWeeklyDiscountAmount { get { return rentalOrderTypeFields.ShowWeeklyDiscountAmount; } set { rentalOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"d9oqM3ph1t2O")]
        public bool? RentalShowWeeklyExtended { get { return rentalOrderTypeFields.ShowWeeklyExtended; } set { rentalOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"pGusl4USV2Ci")]
        public bool? RentalShowEpisodeExtended { get { return rentalOrderTypeFields.ShowEpisodeExtended; } set { rentalOrderTypeFields.ShowEpisodeExtended = value; } }

        [FwLogicProperty(Id:"z2I7kSN8mobY")]
        public bool? RentalShowEpisodeDiscountAmount { get { return rentalOrderTypeFields.ShowEpisodeDiscountAmount; } set { rentalOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"rCMEVsWK4t4R")]
        public bool? RentalShowMonthlyDiscountAmount { get { return rentalOrderTypeFields.ShowMonthlyDiscountAmount; } set { rentalOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"njI2Tj6XZGAt")]
        public bool? RentalShowMonthlyExtended { get { return rentalOrderTypeFields.ShowMonthlyExtended; } set { rentalOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"cd37WDlqfleF")]
        public bool? RentalShowPeriodDiscountAmount { get { return rentalOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"ysMjnJb8PKbB")]
        public bool? RentalShowPeriodExtended { get { return rentalOrderTypeFields.ShowPeriodExtended; } set { rentalOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"Q27HeSyMLJ9m")]
        //public bool? RentalShowVariancePercent { get { return rentalOrderTypeFields.ShowVariancePercent; } set { rentalOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"rwTBOVrQv1HH")]
        //public bool? RentalShowVarianceExtended { get { return rentalOrderTypeFields.ShowVarianceExtended; } set { rentalOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"PaGkNS1QSP2p")]
        public bool? RentalShowWarehouse { get { return rentalOrderTypeFields.ShowWarehouse; } set { rentalOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"2SPibeVxKY3q")]
        public bool? RentalShowTaxable { get { return rentalOrderTypeFields.ShowTaxable; } set { rentalOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"PYkbZPSQlNNJ")]
        public bool? RentalShowNotes { get { return rentalOrderTypeFields.ShowNotes; } set { rentalOrderTypeFields.ShowNotes = value; } }

        [FwLogicProperty(Id:"rFxBfhJ8E0Iy")]
        public bool? RentalShowReturnToWarehouse { get { return rentalOrderTypeFields.ShowReturnToWarehouse; } set { rentalOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"LF1GV1iT2tKw")]
        //public bool? RentalShowInLocationQuantity { get { return rentalOrderTypeFields.ShowInLocationQuantity; } set { rentalOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"yrjT0CG3Ovhh")]
        //public bool? RentalShowWeeksAndDays { get { return rentalOrderTypeFields.ShowWeeksAndDays; } set { rentalOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"uTmEBpP8qIwB")]
        //public bool? RentalShowMonthsAndDays { get { return rentalOrderTypeFields.ShowMonthsAndDays; } set { rentalOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"kKX1SDwsTEdD")]
        //public bool? RentalShowDepartment { get { return rentalOrderTypeFields.ShowDepartment; } set { rentalOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"0FukkrgciFUq")]
        //public bool? RentalShowLocation { get { return rentalOrderTypeFields.ShowLocation; } set { rentalOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"wq6Ndc9RdoIE")]
        //public bool? RentalShowOrderActivity { get { return rentalOrderTypeFields.ShowOrderActivity; } set { rentalOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"MaQ2Gah3X2Kc")]
        //public bool? RentalShowSubOrderNumber { get { return rentalOrderTypeFields.ShowSubOrderNumber; } set { rentalOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"6N7dq7zwl88t")]
        //public bool? RentalShowOrderStatus { get { return rentalOrderTypeFields.ShowOrderStatus; } set { rentalOrderTypeFields.ShowOrderStatus = value; } }

        [FwLogicProperty(Id:"WXVY1URH85D8")]
        public string RentalDateStamp { get { return rentalOrderTypeFields.DateStamp; } set { rentalOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"7tEGfQjd2wEM")]
        public bool? AllowRoundTripRentals { get { return orderType.Roundtriprentals; } set { orderType.Roundtriprentals = value; } }


        //sales fields
        [JsonIgnore]
        [FwLogicProperty(Id:"yRduBcfF34iy")]
        public string SalesOrderTypeFieldsId { get { return salesOrderTypeFields.OrderTypeFieldsId; } set { orderType.SalesOrderTypeFieldsId = value; salesOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"P5VsqmA2F8CB")]
        //public bool? SalesShowOrderNumber { get { return salesOrderTypeFields.ShowOrderNumber; } set { salesOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"UdO2yud52yxc")]
        public bool? SalesShowICode { get { return salesOrderTypeFields.ShowICode; } set { salesOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"DTJINaiezEgZ")]
        public int? SalesICodeWidth { get { return salesOrderTypeFields.ICodeWidth; } set { salesOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"QsWwLZyvzlf6")]
        public bool? SalesShowDescription { get { return salesOrderTypeFields.ShowDescription; } set { salesOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"FS0ar3MudV8i")]
        public int? SalesDescriptionWidth { get { return salesOrderTypeFields.DescriptionWidth; } set { salesOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"rC3Gcy34A4uI")]
        public bool? SalesShowManufacturerPartNumber { get { return salesOrderTypeFields.ShowManufacturerPartNumber; } set { salesOrderTypeFields.ShowManufacturerPartNumber = value; } }

        [FwLogicProperty(Id:"wjz98JsG35Cw")]
        public int? SalesManufacturerPartNumberWidth { get { return salesOrderTypeFields.ManufacturerPartNumberWidth; } set { salesOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        [FwLogicProperty(Id:"nLZn8Af5nuia")]
        public bool? SalesShowPickDate { get { return salesOrderTypeFields.ShowPickDate; } set { salesOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"RnyXStRkpZNm")]
        public bool? SalesShowPickTime { get { return salesOrderTypeFields.ShowPickTime; } set { salesOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"4Uaoi4mtZtAd")]
        public bool? SalesShowFromDate { get { return salesOrderTypeFields.ShowFromDate; } set { salesOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"LAJPmo83Zw3f")]
        public bool? SalesShowFromTime { get { return salesOrderTypeFields.ShowFromTime; } set { salesOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"2TbeHipWbuLm")]
        //public bool? SalesShowToDate { get { return salesOrderTypeFields.ShowToDate; } set { salesOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"EnHJzDc09Vfi")]
        //public bool? SalesShowToTime { get { return salesOrderTypeFields.ShowToTime; } set { salesOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"kJwSY2GdQ7sV")]
        //public bool? SalesShowBillablePeriods { get { return salesOrderTypeFields.ShowBillablePeriods; } set { salesOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"0rrymhup62Wg")]
        public bool? SalesShowSubQuantity { get { return salesOrderTypeFields.ShowSubQuantity; } set { salesOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"Fss986MRrtv1")]
        public bool? SalesShowCost { get { return salesOrderTypeFields.ShowCost; } set { salesOrderTypeFields.ShowCost = value; } }

        [FwLogicProperty(Id:"WVfrirLYkvZa")]
        public bool? SalesShowRate { get { return salesOrderTypeFields.ShowRate; } set { salesOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"pZfMGupoUMOy")]
        public bool? SalesShowAvailableQuantity { get { return salesOrderTypeFields.ShowAvailableQuantity; } set { salesOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"NrZkBB6V4kR4")]
        public bool? SalesShowConflictDate { get { return salesOrderTypeFields.ShowConflictDate; } set { salesOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"SJjQEOmLCd62")]
        public bool? SalesShowAvailableQuantityAllWarehouses { get { return salesOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { salesOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        [FwLogicProperty(Id:"1ao7EmjTphfz")]
        public bool? SalesShowConflictDateAllWarehouses { get { return salesOrderTypeFields.ShowConflictDateAllWarehouses; } set { salesOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        [FwLogicProperty(Id:"oBEQXIaqxK64")]
        public bool? SalesShowMarkupPercent { get { return salesOrderTypeFields.ShowMarkupPercent; } set { salesOrderTypeFields.ShowMarkupPercent = value; } }

        [FwLogicProperty(Id:"xX55YH1BGzCK")]
        public bool? SalesShowMarginPercent { get { return salesOrderTypeFields.ShowMarginPercent; } set { salesOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"dEM2DaF2YOem")]
        public bool? SalesShowUnit { get { return salesOrderTypeFields.ShowUnit; } set { salesOrderTypeFields.ShowUnit = value; } }

        //[FwLogicProperty(Id:"mOJ8oXWdgFAl")]
        //public bool? SalesShowWeeklyCostExtended { get { return salesOrderTypeFields.ShowWeeklyCostExtended; } set { salesOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"xxjAnthwwVHo")]
        //public bool? SalesShowMonthlyCostExtended { get { return salesOrderTypeFields.ShowMonthlyCostExtended; } set { salesOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"rOa6kVgmm1TQ")]
        public bool? SalesShowPeriodCostExtended { get { return salesOrderTypeFields.ShowPeriodCostExtended; } set { salesOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"aMDVenViegc2")]
        public bool? SalesShowDiscountPercent { get { return salesOrderTypeFields.ShowDiscountPercent; } set { salesOrderTypeFields.ShowDiscountPercent = value; } }

        [FwLogicProperty(Id:"gl5XJBTRlJtg")]
        public bool? SalesShowUnitDiscountAmount { get { return salesOrderTypeFields.ShowUnitDiscountAmount; } set { salesOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"UswUhwbdsrK1")]
        public bool? SalesShowUnitExtended { get { return salesOrderTypeFields.ShowUnitExtended; } set { salesOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"RXQyGRD8evQY")]
        //public bool? SalesShowWeeklyDiscountAmount { get { return salesOrderTypeFields.ShowWeeklyDiscountAmount; } set { salesOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"n7YoZjo6f048")]
        //public bool? SalesShowWeeklyExtended { get { return salesOrderTypeFields.ShowWeeklyExtended; } set { salesOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"VLV8dnPpDaO5")]
        //public bool? SalesShowMonthlyDiscountAmount { get { return salesOrderTypeFields.ShowMonthlyDiscountAmount; } set { salesOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"v2HTG4PYTsEz")]
        //public bool? SalesShowMonthlyExtended { get { return salesOrderTypeFields.ShowMonthlyExtended; } set { salesOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"RmFGTqui0WJm")]
        public bool? SalesShowPeriodDiscountAmount { get { return salesOrderTypeFields.ShowPeriodDiscountAmount; } set { salesOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"QNOXqTiedyJV")]
        public bool? SalesShowPeriodExtended { get { return salesOrderTypeFields.ShowPeriodExtended; } set { salesOrderTypeFields.ShowPeriodExtended = value; } }

        [FwLogicProperty(Id:"fvykj43eCi9m")]
        public bool? SalesShowVariancePercent { get { return salesOrderTypeFields.ShowVariancePercent; } set { salesOrderTypeFields.ShowVariancePercent = value; } }

        [FwLogicProperty(Id:"yQEMj7TStFuX")]
        public bool? SalesShowVarianceExtended { get { return salesOrderTypeFields.ShowVarianceExtended; } set { salesOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"rEXWQFTmlrfT")]
        public bool? SalesShowWarehouse { get { return salesOrderTypeFields.ShowWarehouse; } set { salesOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"e7WXDyKaAB3Y")]
        public bool? SalesShowTaxable { get { return salesOrderTypeFields.ShowTaxable; } set { salesOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"lYrxdv0P8PAs")]
        public bool? SalesShowNotes { get { return salesOrderTypeFields.ShowNotes; } set { salesOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"CKzzsWIFc24g")]
        //public bool? SalesShowReturnToWarehouse { get { return salesOrderTypeFields.ShowReturnToWarehouse; } set { salesOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"ip1UtYxNVbG5")]
        //public bool? SalesShowConsigmentAvailableQuantity { get { return salesOrderTypeFields.ShowConsignmentAvailableQuantity; } set { salesOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"QORL8I8MF04S")]
        //public bool? SalesShowConsigmentConflictDate { get { return salesOrderTypeFields.ShowConsignmentConflictDate; } set { salesOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"YVpx4fslJCRs")]
        //public bool? SalesShowConsignmentQuantity { get { return salesOrderTypeFields.ShowConsignmentQuantity; } set { salesOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"sC4Q6kNr4xLW")]
        //public bool? SalesShowInLocationQuantity { get { return salesOrderTypeFields.ShowInLocationQuantity; } set { salesOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"eBLWsfhKW4Ld")]
        //public bool? SalesShowReservedItems { get { return salesOrderTypeFields.ShowReservedItems; } set { salesOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"jZ1pAO3tLICF")]
        //public bool? SalesShowWeeksAndDays { get { return salesOrderTypeFields.ShowWeeksAndDays; } set { salesOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"UVasjEfcFVk9")]
        //public bool? SalesShowMonthsAndDays { get { return salesOrderTypeFields.ShowMonthsAndDays; } set { salesOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"XrN5yBk3BvZm")]
        //public bool? SalesShowPremiumPercent { get { return salesOrderTypeFields.ShowPremiumPercent; } set { salesOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"B3wUOay6XDIR")]
        //public bool? SalesShowDepartment { get { return salesOrderTypeFields.ShowDepartment; } set { salesOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"DkJAp6LExar7")]
        //public bool? SalesShowLocation { get { return salesOrderTypeFields.ShowLocation; } set { salesOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"RRJqs5K2TfFA")]
        //public bool? SalesShowOrderActivity { get { return salesOrderTypeFields.ShowOrderActivity; } set { salesOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"KJ7fLuUMuVTu")]
        //public bool? SalesShowSubOrderNumber { get { return salesOrderTypeFields.ShowSubOrderNumber; } set { salesOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"tR70g8eFfRxe")]
        //public bool? SalesShowOrderStatus { get { return salesOrderTypeFields.ShowOrderStatus; } set { salesOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"6FN4dOMq9Atr")]
        //public bool? SalesShowEpisodes { get { return salesOrderTypeFields.ShowEpisodes; } set { salesOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"ImHwkAvC9hxe")]
        //public bool? SalesShowEpisodeExtended { get { return salesOrderTypeFields.ShowEpisodeExtended; } set { salesOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"XefSCH5S8y9t")]
        //public bool? SalesShowEpisodeDiscountAmount { get { return salesOrderTypeFields.ShowEpisodeDiscountAmount; } set { salesOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"IYTPiMnshg6n")]
        public string SalesDateStamp { get { return salesOrderTypeFields.DateStamp; } set { salesOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"odeoSr471A8Y")]
        public string SalesInventoryPrice { get { return orderType.Selectsalesprice; } set { orderType.Selectsalesprice = value; } }

        [FwLogicProperty(Id:"fVYzyaJ8KBLS")]
        public string SalesInventoryCost { get { return orderType.Selectsalescost; } set { orderType.Selectsalescost = value; } }


        //facilities fields
        [JsonIgnore]
        [FwLogicProperty(Id:"R0op6LOY6k5K")]
        public string FacilityOrderTypeFieldsId { get { return spaceOrderTypeFields.OrderTypeFieldsId; } set { orderType.FacilityOrderTypeFieldsId = value; spaceOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"d4L4ORr4ZngX")]
        //public bool? FacilityShowOrderNumber { get { return spaceOrderTypeFields.ShowOrderNumber; } set { spaceOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"9ZABJI7va7Ai")]
        //public bool? FacilityShowRepairOrderNumber { get { return spaceOrderTypeFields.ShowRepairOrderNumber; } set { spaceOrderTypeFields.ShowRepairOrderNumber = value; } }

        //[FwLogicProperty(Id:"cD3kXtqXiiFb")]
        //public bool? FacilityShowICode { get { return spaceOrderTypeFields.ShowICode; } set { spaceOrderTypeFields.ShowICode = value; } }

        //[FwLogicProperty(Id:"Q5RN5oB4iIyG")]
        //public int? FacilityICodeWidth { get { return spaceOrderTypeFields.ICodeWidth; } set { spaceOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"blwEjfiZ9vcD")]
        public bool? FacilityShowDescription { get { return spaceOrderTypeFields.ShowDescription; } set { spaceOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"l0tpqDz0tMes")]
        public int? FacilityDescriptionWidth { get { return spaceOrderTypeFields.DescriptionWidth; } set { spaceOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"aSbd2w8w90Ld")]
        //public bool? FacilityShowPickDate { get { return spaceOrderTypeFields.ShowPickDate; } set { spaceOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"QBGKCu9PEPr2")]
        //public bool? FacilityShowPickTime { get { return spaceOrderTypeFields.ShowPickTime; } set { spaceOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"N4GfaIWD0bYE")]
        public bool? FacilityShowFromDate { get { return spaceOrderTypeFields.ShowFromDate; } set { spaceOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"cgIZR85Wcj6T")]
        public bool? FacilityShowFromTime { get { return spaceOrderTypeFields.ShowFromTime; } set { spaceOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"OPaKv1G4UtJh")]
        public bool? FacilityShowToDate { get { return spaceOrderTypeFields.ShowToDate; } set { spaceOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"DnAdNBKjxNmY")]
        public bool? FacilityShowToTime { get { return spaceOrderTypeFields.ShowToTime; } set { spaceOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"JlGww0bUNufx")]
        public bool? FacilityShowWeeksAndDays { get { return spaceOrderTypeFields.ShowWeeksAndDays; } set { spaceOrderTypeFields.ShowWeeksAndDays = value; } }

        [FwLogicProperty(Id:"kvnikk1UBsxP")]
        public bool? FacilityShowMonthsAndDays { get { return spaceOrderTypeFields.ShowMonthsAndDays; } set { spaceOrderTypeFields.ShowMonthsAndDays = value; } }

        [FwLogicProperty(Id:"Jbuk4cDkkzCs")]
        public bool? FacilityShowBillablePeriods { get { return spaceOrderTypeFields.ShowBillablePeriods; } set { spaceOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"VKlRTsBXGLfi")]
        //public bool? FacilityShowSubQuantity { get { return spaceOrderTypeFields.ShowSubQuantity; } set { spaceOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"VZLa3ku47Uki")]
        //public bool? FacilityShowAvailableQuantity { get { return spaceOrderTypeFields.ShowAvailableQuantity; } set { spaceOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"0dvgZgbAZZjT")]
        //public bool? FacilityShowConflictDate { get { return spaceOrderTypeFields.ShowConflictDate; } set { spaceOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"c5caBjYJofkc")]
        public bool? FacilityShowRate { get { return spaceOrderTypeFields.ShowRate; } set { spaceOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"vLIFF7KZw2mD")]
        //public bool? FacilityShowCost { get { return spaceOrderTypeFields.ShowCost; } set { spaceOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"Bwh47nLTiRdW")]
        //public bool? FacilityShowWeeklyCostExtended { get { return spaceOrderTypeFields.ShowWeeklyCostExtended; } set { spaceOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"wZanPGzO5QQ2")]
        //public bool? FacilityShowMonthlyCostExtended { get { return spaceOrderTypeFields.ShowMonthlyCostExtended; } set { spaceOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"JYdjzrn22SCG")]
        //public bool? FacilityShowPeriodCostExtended { get { return spaceOrderTypeFields.ShowPeriodCostExtended; } set { spaceOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"oCvLe3Ldof0H")]
        public bool? FacilityShowDaysPerWeek { get { return spaceOrderTypeFields.ShowDaysPerWeek; } set { spaceOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"gq0SG685JPaG")]
        public bool? FacilityShowDiscountPercent { get { return spaceOrderTypeFields.ShowDiscountPercent; } set { spaceOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"ZCZNmGTVsuIn")]
        //public bool? FacilityShowMarkupPercent { get { return spaceOrderTypeFields.ShowMarkupPercent; } set { spaceOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"Jmg1dDHjDdi0")]
        //public bool? FacilityShowMarginPercent { get { return spaceOrderTypeFields.ShowMarginPercent; } set { spaceOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"5qurr0cDSvBG")]
        public bool? FacilityShowSplit { get { return spaceOrderTypeFields.ShowSplit; } set { spaceOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"pLt8sOdoh75W")]
        public bool? FacilityShowUnit { get { return spaceOrderTypeFields.ShowUnit; } set { spaceOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"ednVgRWe11kT")]
        public bool? FacilityShowUnitDiscountAmount { get { return spaceOrderTypeFields.ShowUnitDiscountAmount; } set { spaceOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"QAhsdhY80kNh")]
        public bool? FacilityShowUnitExtended { get { return spaceOrderTypeFields.ShowUnitExtended; } set { spaceOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"tRR44Ztuly4n")]
        public bool? FacilityShowWeeklyDiscountAmount { get { return spaceOrderTypeFields.ShowWeeklyDiscountAmount; } set { spaceOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"4JYyELZZ7XLo")]
        public bool? FacilityShowWeeklyExtended { get { return spaceOrderTypeFields.ShowWeeklyExtended; } set { spaceOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"9PKNC7iq1WkA")]
        public bool? FacilityShowMonthlyDiscountAmount { get { return spaceOrderTypeFields.ShowMonthlyDiscountAmount; } set { spaceOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"AEDttG9AgcyT")]
        public bool? FacilityShowMonthlyExtended { get { return spaceOrderTypeFields.ShowMonthlyExtended; } set { spaceOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"GB3eEITkihDj")]
        public bool? FacilityShowPeriodDiscountAmount { get { return spaceOrderTypeFields.ShowPeriodDiscountAmount; } set { spaceOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"1rGXHe6wxJb5")]
        public bool? FacilityShowPeriodExtended { get { return spaceOrderTypeFields.ShowPeriodExtended; } set { spaceOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"nCZU7JxCQZzB")]
        //public bool? FacilityShowVariancePercent { get { return spaceOrderTypeFields.ShowVariancePercent; } set { spaceOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"e6yXhqWXCcAc")]
        //public bool? FacilityShowVarianceExtended { get { return spaceOrderTypeFields.ShowVarianceExtended; } set { spaceOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"Y8vsaLyGmGHG")]
        //public bool? FacilityShowCountryOfOrigin { get { return spaceOrderTypeFields.ShowCountryOfOrigin; } set { spaceOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"F6KbHo08dJxX")]
        //public bool? FacilityShowManufacturer { get { return spaceOrderTypeFields.ShowManufacturer; } set { spaceOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"PVV5UT1kpBTq")]
        //public bool? FacilityShowManufacturerPartNumber { get { return spaceOrderTypeFields.ShowManufacturerPartNumber; } set { spaceOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"ZMHac5rKx0pC")]
        //public int? FacilityManufacturerPartNumberWidth { get { return spaceOrderTypeFields.ManufacturerPartNumberWidth; } set { spaceOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"qEy4is85iPJE")]
        //public bool? FacilityShowModelNumber { get { return spaceOrderTypeFields.ShowModelNumber; } set { spaceOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"mDKWgtR7DRN5")]
        //public bool? FacilityShowVendorPartNumber { get { return spaceOrderTypeFields.ShowVendorPartNumber; } set { spaceOrderTypeFields.ShowVendorPartNumber = value; } }

        //[FwLogicProperty(Id:"McKHhC8AVIQd")]
        //public bool? FacilityShowWarehouse { get { return spaceOrderTypeFields.ShowWarehouse; } set { spaceOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"DKmW60fQ5yPu")]
        public bool? FacilityShowTaxable { get { return spaceOrderTypeFields.ShowTaxable; } set { spaceOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"U0hZeymTiLFK")]
        public bool? FacilityShowNotes { get { return spaceOrderTypeFields.ShowNotes; } set { spaceOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"Tnn8XamnujhS")]
        //public bool? FacilityShowReturnToWarehouse { get { return spaceOrderTypeFields.ShowReturnToWarehouse; } set { spaceOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"8UYkIZFXvFEJ")]
        //public bool? FacilityShowVehicleNumber { get { return spaceOrderTypeFields.ShowVehicleNumber; } set { spaceOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"4JJq7qCVrkvz")]
        //public bool? FacilityShowBarCode { get { return spaceOrderTypeFields.ShowBarCode; } set { spaceOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"nsaEDPQJ125b")]
        //public bool? FacilityShowSerialNumber { get { return spaceOrderTypeFields.ShowSerialNumber; } set { spaceOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"ikqb3op9KfPz")]
        //public bool? FacilityShowCrewName { get { return spaceOrderTypeFields.ShowCrewName; } set { spaceOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"VEiGHNnJRLfn")]
        //public bool? FacilityShowHours { get { return spaceOrderTypeFields.ShowHours; } set { spaceOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"wTAxr0DE9NJx")]
        //public bool? FacilityShowAvailableQuantityAllWarehouses { get { return spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"WLg1z2lKrxHN")]
        //public bool? FacilityShowConflictDateAllWarehouses { get { return spaceOrderTypeFields.ShowConflictDateAllWarehouses; } set { spaceOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"BLyyrNqqDNQz")]
        //public bool? FacilityShowConsignmentAvailableQuantity { get { return spaceOrderTypeFields.ShowConsignmentAvailableQuantity; } set { spaceOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"rdOa19AMXEEh")]
        //public bool? FacilityShowConsignmentConflictDate { get { return spaceOrderTypeFields.ShowConsignmentConflictDate; } set { spaceOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"XgzrDDgFtfdI")]
        //public bool? FacilityShowConsignmentQuantity { get { return spaceOrderTypeFields.ShowConsignmentQuantity; } set { spaceOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"W8HvrxTfWlnV")]
        //public bool? FacilityShowInLocationQuantity { get { return spaceOrderTypeFields.ShowInLocationQuantity; } set { spaceOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"wxqx83wDx1wh")]
        //public bool? FacilityShowReservedItems { get { return spaceOrderTypeFields.ShowReservedItems; } set { spaceOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"Cv7XeYYuX9KF")]
        //public bool? FacilityShowPremiumPercent { get { return spaceOrderTypeFields.ShowPremiumPercent; } set { spaceOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"sC4MQZp3qKvI")]
        //public bool? FacilityShowDepartment { get { return spaceOrderTypeFields.ShowDepartment; } set { spaceOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"vX8oxVTwhWFY")]
        //public bool? FacilityShowLocation { get { return spaceOrderTypeFields.ShowLocation; } set { spaceOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"gYBZONHILX4y")]
        //public bool? FacilityShowOrderActivity { get { return spaceOrderTypeFields.ShowOrderActivity; } set { spaceOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"g986ljvu2qNc")]
        //public bool? FacilityShowSubOrderNumber { get { return spaceOrderTypeFields.ShowSubOrderNumber; } set { spaceOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"UKb9G2J50p9V")]
        //public bool? FacilityShowOrderStatus { get { return spaceOrderTypeFields.ShowOrderStatus; } set { spaceOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"7g7zZ4JYuRGk")]
        //public bool? FacilityShowEpisodes { get { return spaceOrderTypeFields.ShowEpisodes; } set { spaceOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"sieFIMkWuNS2")]
        //public bool? FacilityShowEpisodeExtended { get { return spaceOrderTypeFields.ShowEpisodeExtended; } set { spaceOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"nFqSA96xrWL2")]
        //public bool? FacilityShowEpisodeDiscountAmount { get { return spaceOrderTypeFields.ShowEpisodeDiscountAmount; } set { spaceOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"JX4SwTaOSq8Q")]
        public string FacilityDateStamp { get { return spaceOrderTypeFields.DateStamp; } set { spaceOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"LBuGn3jOsC3S")]
        public string FacilityDescription { get { return orderType.Spacedescription; } set { orderType.Spacedescription = value; } }


        //transportation fields
        [JsonIgnore]
        [FwLogicProperty(Id:"xysrvoAxbZDw")]
        public string VehicleOrderTypeFieldsId { get { return vehicleOrderTypeFields.OrderTypeFieldsId; } set { orderType.VehicleOrderTypeFieldsId = value; vehicleOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"Zug7urwxGTeZ")]
        //public bool? VehicleShowOrderNumber { get { return vehicleOrderTypeFields.ShowOrderNumber; } set { vehicleOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"0rxCLjjjaaPS")]
        //public bool? VehicleShowRepairOrderNumber { get { return vehicleOrderTypeFields.ShowRepairOrderNumber; } set { vehicleOrderTypeFields.ShowRepairOrderNumber = value; } }

        //[FwLogicProperty(Id:"RMT9ILnYDqOt")]
        //public bool? VehicleShowICode { get { return vehicleOrderTypeFields.ShowICode; } set { vehicleOrderTypeFields.ShowICode = value; } }

        //[FwLogicProperty(Id:"X3QRFCYpZERL")]
        //public int? VehicleICodeWidth { get { return vehicleOrderTypeFields.ICodeWidth; } set { vehicleOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"AnIiyON4MnlQ")]
        public bool? VehicleShowDescription { get { return vehicleOrderTypeFields.ShowDescription; } set { vehicleOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"o4YlkQlvdc8Q")]
        public int? VehicleDescriptionWidth { get { return vehicleOrderTypeFields.DescriptionWidth; } set { vehicleOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"t9cRW1axEw0L")]
        public bool? VehicleShowVehicleNumber { get { return vehicleOrderTypeFields.ShowVehicleNumber; } set { vehicleOrderTypeFields.ShowVehicleNumber = value; } }

        [FwLogicProperty(Id:"3vAOh6zi248Q")]
        public bool? VehicleShowPickDate { get { return vehicleOrderTypeFields.ShowPickDate; } set { vehicleOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"xFPd7YxcC4Ga")]
        public bool? VehicleShowPickTime { get { return vehicleOrderTypeFields.ShowPickTime; } set { vehicleOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"ZZs46Yw8cZmK")]
        public bool? VehicleShowFromDate { get { return vehicleOrderTypeFields.ShowFromDate; } set { vehicleOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"Mi4SIx506xkw")]
        public bool? VehicleShowFromTime { get { return vehicleOrderTypeFields.ShowFromTime; } set { vehicleOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"krLyv9fzY6Dg")]
        public bool? VehicleShowToDate { get { return vehicleOrderTypeFields.ShowToDate; } set { vehicleOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"AmZY8c7omrS1")]
        public bool? VehicleShowToTime { get { return vehicleOrderTypeFields.ShowToTime; } set { vehicleOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"X94uZSO77mTv")]
        public bool? VehicleShowBillablePeriods { get { return vehicleOrderTypeFields.ShowBillablePeriods; } set { vehicleOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"EOd4c42owBfQ")]
        public bool? VehicleShowSubQuantity { get { return vehicleOrderTypeFields.ShowSubQuantity; } set { vehicleOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"UOqEVqcHtulb")]
        public bool? VehicleShowAvailableQuantity { get { return vehicleOrderTypeFields.ShowAvailableQuantity; } set { vehicleOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"51jg8cQr110v")]
        public bool? VehicleShowConflictDate { get { return vehicleOrderTypeFields.ShowConflictDate; } set { vehicleOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"m7s1txW6fI6O")]
        public bool? VehicleShowUnit { get { return vehicleOrderTypeFields.ShowUnit; } set { vehicleOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"CFnaZ5KYNske")]
        public bool? VehicleShowRate { get { return vehicleOrderTypeFields.ShowRate; } set { vehicleOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"WjI0OiIb3vCt")]
        public bool? VehicleShowDaysPerWeek { get { return vehicleOrderTypeFields.ShowDaysPerWeek; } set { vehicleOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"31D4lhEXl25f")]
        public bool? VehicleShowCost { get { return vehicleOrderTypeFields.ShowCost; } set { vehicleOrderTypeFields.ShowCost = value; } }

        [FwLogicProperty(Id:"peJlCynfux3d")]
        public bool? VehicleShowWeeklyCostExtended { get { return vehicleOrderTypeFields.ShowWeeklyCostExtended; } set { vehicleOrderTypeFields.ShowWeeklyCostExtended = value; } }

        [FwLogicProperty(Id:"qfU3VKcn8FeZ")]
        public bool? VehicleShowMonthlyCostExtended { get { return vehicleOrderTypeFields.ShowMonthlyCostExtended; } set { vehicleOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"lnqTn7cSKxGj")]
        public bool? VehicleShowPeriodCostExtended { get { return vehicleOrderTypeFields.ShowPeriodCostExtended; } set { vehicleOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"hqSfxzSH3NRj")]
        public bool? VehicleShowDiscountPercent { get { return vehicleOrderTypeFields.ShowDiscountPercent; } set { vehicleOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"X4E2S3RCySc1")]
        //public bool? VehicleShowMarkupPercent { get { return vehicleOrderTypeFields.ShowMarkupPercent; } set { vehicleOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"n3d3BRUXS9UA")]
        //public bool? VehicleShowMarginPercent { get { return vehicleOrderTypeFields.ShowMarginPercent; } set { vehicleOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"fApD9PKIvFZI")]
        public bool? VehicleShowUnitDiscountAmount { get { return vehicleOrderTypeFields.ShowUnitDiscountAmount; } set { vehicleOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"zFwufZxAmTNy")]
        public bool? VehicleShowUnitExtended { get { return vehicleOrderTypeFields.ShowUnitExtended; } set { vehicleOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"TXAaqFMkszKh")]
        public bool? VehicleShowWeeklyDiscountAmount { get { return vehicleOrderTypeFields.ShowWeeklyDiscountAmount; } set { vehicleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"3SLxjakRrWtX")]
        public bool? VehicleShowWeeklyExtended { get { return vehicleOrderTypeFields.ShowWeeklyExtended; } set { vehicleOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"ooBgHpYYGuvB")]
        public bool? VehicleShowMonthlyDiscountAmount { get { return vehicleOrderTypeFields.ShowMonthlyDiscountAmount; } set { vehicleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"XAx2QQ1JTLjn")]
        public bool? VehicleShowMonthlyExtended { get { return vehicleOrderTypeFields.ShowMonthlyExtended; } set { vehicleOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"0271gvpbDni7")]
        public bool? VehicleShowPeriodDiscountAmount { get { return vehicleOrderTypeFields.ShowPeriodDiscountAmount; } set { vehicleOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"qAGepnJAqP7G")]
        public bool? VehicleShowPeriodExtended { get { return vehicleOrderTypeFields.ShowPeriodExtended; } set { vehicleOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"3VboLI6xJC6I")]
        //public bool? VehicleShowVariancePercent { get { return vehicleOrderTypeFields.ShowVariancePercent; } set { vehicleOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"7rpvbUpW4dj2")]
        //public bool? VehicleShowVarianceExtended { get { return vehicleOrderTypeFields.ShowVarianceExtended; } set { vehicleOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"hDNwHQyQ35e0")]
        //public bool? VehicleShowCountryOfOrigin { get { return vehicleOrderTypeFields.ShowCountryOfOrigin; } set { vehicleOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"ocoBIrViVtiK")]
        //public bool? VehicleShowManufacturer { get { return vehicleOrderTypeFields.ShowManufacturer; } set { vehicleOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"9gd6Z3YcF3IE")]
        //public bool? VehicleShowManufacturerPartNumber { get { return vehicleOrderTypeFields.ShowManufacturerPartNumber; } set { vehicleOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"K7V1LaFbHSkO")]
        //public int? VehicleManufacturerPartNumberWidth { get { return vehicleOrderTypeFields.ManufacturerPartNumberWidth; } set { vehicleOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"nZHguf1nlLy1")]
        //public bool? VehicleShowModelNumber { get { return vehicleOrderTypeFields.ShowModelNumber; } set { vehicleOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"dExg1DKEM5rh")]
        //public bool? VehicleShowVendorPartNumber { get { return vehicleOrderTypeFields.ShowVendorPartNumber; } set { vehicleOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"m0gKDmOcQARJ")]
        public bool? VehicleShowWarehouse { get { return vehicleOrderTypeFields.ShowWarehouse; } set { vehicleOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"205yP0jjkq4O")]
        public bool? VehicleShowReturnToWarehouse { get { return vehicleOrderTypeFields.ShowReturnToWarehouse; } set { vehicleOrderTypeFields.ShowReturnToWarehouse = value; } }

        [FwLogicProperty(Id:"hJA0yrT1Xox9")]
        public bool? VehicleShowTaxable { get { return vehicleOrderTypeFields.ShowTaxable; } set { vehicleOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"1GW2gszJ6JoC")]
        public bool? VehicleShowNotes { get { return vehicleOrderTypeFields.ShowNotes; } set { vehicleOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"61pKKRQDTU7r")]
        //public bool? VehicleShowBarCode { get { return vehicleOrderTypeFields.ShowBarCode; } set { vehicleOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"Nc9AcDcohvQr")]
        //public bool? VehicleShowSerialNumber { get { return vehicleOrderTypeFields.ShowSerialNumber; } set { vehicleOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"7n3NsvIsn4Rh")]
        //public bool? VehicleShowCrewName { get { return vehicleOrderTypeFields.ShowCrewName; } set { vehicleOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"JntkGkYOCRRH")]
        //public bool? VehicleShowHours { get { return vehicleOrderTypeFields.ShowHours; } set { vehicleOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"uVN0UXqbnZIO")]
        //public bool? VehicleShowAvailableQuantityAllWarehouses { get { return vehicleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { vehicleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"KqX4GUidosir")]
        //public bool? VehicleShowConflictDateAllWarehouses { get { return vehicleOrderTypeFields.ShowConflictDateAllWarehouses; } set { vehicleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"iEyToRVtD0w8")]
        //public bool? VehicleShowConsignmentAvailableQuantity { get { return vehicleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { vehicleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"hDC90DlQEhg0")]
        //public bool? VehicleShowConsignmentConflictDate { get { return vehicleOrderTypeFields.ShowConsignmentConflictDate; } set { vehicleOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"Y9U9umBtgXE1")]
        //public bool? VehicleShowConsignmentQuantity { get { return vehicleOrderTypeFields.ShowConsignmentQuantity; } set { vehicleOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"PtiL0ams237K")]
        //public bool? VehicleShowInLocationQuantity { get { return vehicleOrderTypeFields.ShowInLocationQuantity; } set { vehicleOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"qYJ4K9AikID3")]
        //public bool? VehicleShowReservedItems { get { return vehicleOrderTypeFields.ShowReservedItems; } set { vehicleOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"4QKrGIIhxZKz")]
        //public bool? VehicleShowWeeksAndDays { get { return vehicleOrderTypeFields.ShowWeeksAndDays; } set { vehicleOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"YM5mvaUa8i34")]
        //public bool? VehicleShowMonthsAndDays { get { return vehicleOrderTypeFields.ShowMonthsAndDays; } set { vehicleOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"hrxfSVY4HnIg")]
        //public bool? VehicleShowPremiumPercent { get { return vehicleOrderTypeFields.ShowPremiumPercent; } set { vehicleOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"l8Dn8CtOFLeB")]
        //public bool? VehicleShowDepartment { get { return vehicleOrderTypeFields.ShowDepartment; } set { vehicleOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"ICC5wbAaUzXO")]
        //public bool? VehicleShowLocation { get { return vehicleOrderTypeFields.ShowLocation; } set { vehicleOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"JQ46JC7NmyxE")]
        //public bool? VehicleShowOrderActivity { get { return vehicleOrderTypeFields.ShowOrderActivity; } set { vehicleOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"Ss2Dr2wUZg2i")]
        //public bool? VehicleShowSubOrderNumber { get { return vehicleOrderTypeFields.ShowSubOrderNumber; } set { vehicleOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"lY89d7k8O0sb")]
        //public bool? VehicleShowOrderStatus { get { return vehicleOrderTypeFields.ShowOrderStatus; } set { vehicleOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"Y9yE7VmWv4gJ")]
        //public bool? VehicleShowEpisodes { get { return vehicleOrderTypeFields.ShowEpisodes; } set { vehicleOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"ltRRIJbp6baY")]
        //public bool? VehicleShowEpisodeExtended { get { return vehicleOrderTypeFields.ShowEpisodeExtended; } set { vehicleOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"0xyf2wejI5jY")]
        //public bool? VehicleShowEpisodeDiscountAmount { get { return vehicleOrderTypeFields.ShowEpisodeDiscountAmount; } set { vehicleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"Sz39wId9APMM")]
        public string VehicleDateStamp { get { return vehicleOrderTypeFields.DateStamp; } set { vehicleOrderTypeFields.DateStamp = value; } }


        //labor/crew fields
        [JsonIgnore]
        [FwLogicProperty(Id:"YTiB26LaZjJ1")]
        public string LaborOrderTypeFieldsId { get { return laborOrderTypeFields.OrderTypeFieldsId; } set { orderType.LaborOrderTypeFieldsId = value; laborOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"DdQ0agSLRqyY")]
        //public bool? LaborShowOrderNumber { get { return laborOrderTypeFields.ShowOrderNumber; } set { laborOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"HpWHnMxQnObc")]
        //public bool? LaborShowRepairOrderNumber { get { return laborOrderTypeFields.ShowRepairOrderNumber; } set { laborOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"KY3v84T84i7H")]
        public bool? LaborShowICode { get { return laborOrderTypeFields.ShowICode; } set { laborOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"exgOA8Uflbrp")]
        public int? LaborICodeWidth { get { return laborOrderTypeFields.ICodeWidth; } set { laborOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"o3Kc8YUzZHnD")]
        public bool? LaborShowDescription { get { return laborOrderTypeFields.ShowDescription; } set { laborOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"qX7B7cl6I6EH")]
        public int? LaborDescriptionWidth { get { return laborOrderTypeFields.DescriptionWidth; } set { laborOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"xbMpJmjD5EUk")]
        public bool? LaborShowOrderActivity { get { return laborOrderTypeFields.ShowOrderActivity; } set { laborOrderTypeFields.ShowOrderActivity = value; } }

        [FwLogicProperty(Id:"dBU7nFR6iHsM")]
        public bool? LaborShowCrewName { get { return laborOrderTypeFields.ShowCrewName; } set { laborOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"P3ilAC5lWTMC")]
        //public bool? LaborShowPickDate { get { return laborOrderTypeFields.ShowPickDate; } set { laborOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"nUKeik4taeNC")]
        //public bool? LaborShowPickTime { get { return laborOrderTypeFields.ShowPickTime; } set { laborOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"WDFFKJOCspTh")]
        public bool? LaborShowFromDate { get { return laborOrderTypeFields.ShowFromDate; } set { laborOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"v6RvvwqeGZpR")]
        public bool? LaborShowFromTime { get { return laborOrderTypeFields.ShowFromTime; } set { laborOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"6gx2nrNqfoLh")]
        public bool? LaborShowToDate { get { return laborOrderTypeFields.ShowToDate; } set { laborOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"rQBGfD2SwpPt")]
        public bool? LaborShowToTime { get { return laborOrderTypeFields.ShowToTime; } set { laborOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"dtPYMQtBcD22")]
        public bool? LaborShowBillablePeriods { get { return laborOrderTypeFields.ShowBillablePeriods; } set { laborOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"w0cka3Bp4Yvw")]
        public bool? LaborShowHours { get { return laborOrderTypeFields.ShowHours; } set { laborOrderTypeFields.ShowHours = value; } }

        [FwLogicProperty(Id:"cYc8O2crEbQ4")]
        public bool? LaborShowSubQuantity { get { return laborOrderTypeFields.ShowSubQuantity; } set { laborOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"Lr7WnqqXx6vT")]
        //public bool? LaborShowAvailableQuantity { get { return laborOrderTypeFields.ShowAvailableQuantity; } set { laborOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"bdY9P0z5eHvW")]
        //public bool? LaborShowConflictDate { get { return laborOrderTypeFields.ShowConflictDate; } set { laborOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"djxZEKMbfqez")]
        public bool? LaborShowCost { get { return laborOrderTypeFields.ShowCost; } set { laborOrderTypeFields.ShowCost = value; } }

        [FwLogicProperty(Id:"vDMcdpJdPUrA")]
        public bool? LaborShowRate { get { return laborOrderTypeFields.ShowRate; } set { laborOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"dyDPSb4sqoej")]
        //public bool? LaborShowWeeklyCostExtended { get { return laborOrderTypeFields.ShowWeeklyCostExtended; } set { laborOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"sbEr8fotecxN")]
        //public bool? LaborShowMonthlyCostExtended { get { return laborOrderTypeFields.ShowMonthlyCostExtended; } set { laborOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"ZaLByVxw1hpT")]
        public bool? LaborShowPeriodCostExtended { get { return laborOrderTypeFields.ShowPeriodCostExtended; } set { laborOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"639ktTzFIR1K")]
        //public bool? LaborShowDaysPerWeek { get { return laborOrderTypeFields.ShowDaysPerWeek; } set { laborOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"rSAnUIwMEovP")]
        public bool? LaborShowDiscountPercent { get { return laborOrderTypeFields.ShowDiscountPercent; } set { laborOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"izWZdJtnTYMn")]
        //public bool? LaborShowMarkupPercent { get { return laborOrderTypeFields.ShowMarkupPercent; } set { laborOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"kUSL6NOg9DPy")]
        //public bool? LaborShowMarginPercent { get { return laborOrderTypeFields.ShowMarginPercent; } set { laborOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"BngfIWuUPv7O")]
        public bool? LaborShowUnit { get { return laborOrderTypeFields.ShowUnit; } set { laborOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"rET5odPYS5GH")]
        public bool? LaborShowUnitDiscountAmount { get { return laborOrderTypeFields.ShowUnitDiscountAmount; } set { laborOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"R9BiXuafEqaO")]
        public bool? LaborShowUnitExtended { get { return laborOrderTypeFields.ShowUnitExtended; } set { laborOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"hnio9DVcQg1R")]
        public bool? LaborShowWeeklyDiscountAmount { get { return laborOrderTypeFields.ShowWeeklyDiscountAmount; } set { laborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"UR0xHq5CqQ8W")]
        public bool? LaborShowWeeklyExtended { get { return laborOrderTypeFields.ShowWeeklyExtended; } set { laborOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"UBQ6nMMTtRO1")]
        public bool? LaborShowMonthlyDiscountAmount { get { return laborOrderTypeFields.ShowMonthlyDiscountAmount; } set { laborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"9Cbj9wc4CBe5")]
        public bool? LaborShowMonthlyExtended { get { return laborOrderTypeFields.ShowMonthlyExtended; } set { laborOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"uCIY2OTEkjP9")]
        public bool? LaborShowPeriodDiscountAmount { get { return laborOrderTypeFields.ShowPeriodDiscountAmount; } set { laborOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"SAdmnoj2I1RN")]
        public bool? LaborShowPeriodExtended { get { return laborOrderTypeFields.ShowPeriodExtended; } set { laborOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"QIWIwJa0qUhI")]
        //public bool? LaborShowVariancePercent { get { return laborOrderTypeFields.ShowVariancePercent; } set { laborOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"pmNcJbpRL85F")]
        //public bool? LaborShowVarianceExtended { get { return laborOrderTypeFields.ShowVarianceExtended; } set { laborOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"iU7aRAZGOUOo")]
        public bool? LaborShowWarehouse { get { return laborOrderTypeFields.ShowWarehouse; } set { laborOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"qgQ8ZmUnmttU")]
        public bool? LaborShowTaxable { get { return laborOrderTypeFields.ShowTaxable; } set { laborOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"drshMSvo69aq")]
        public bool? LaborShowNotes { get { return laborOrderTypeFields.ShowNotes; } set { laborOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"OKgPp4KBOadk")]
        //public bool? LaborShowReturnToWarehouse { get { return laborOrderTypeFields.ShowReturnToWarehouse; } set { laborOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"nfnXlxOy0uVB")]
        //public bool? LaborShowAvailableQuantityAllWarehouses { get { return laborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { laborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"K8xTrk3He59O")]
        //public bool? LaborShowConflictDateAllWarehouses { get { return laborOrderTypeFields.ShowConflictDateAllWarehouses; } set { laborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"bQnBCqwURQkv")]
        //public bool? LaborShowConsignmentAvailableQuantity { get { return laborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { laborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"hvy36xDd3e1c")]
        //public bool? LaborShowConsignmentConflictDate { get { return laborOrderTypeFields.ShowConsignmentConflictDate; } set { laborOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"yhE9seCWjpG3")]
        //public bool? LaborShowConsignmentQuantity { get { return laborOrderTypeFields.ShowConsignmentQuantity; } set { laborOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"iOIsJadc2XdE")]
        //public bool? LaborShowInLocationQuantity { get { return laborOrderTypeFields.ShowInLocationQuantity; } set { laborOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"4eqpmMWn7CMn")]
        //public bool? LaborShowReservedItems { get { return laborOrderTypeFields.ShowReservedItems; } set { laborOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"pYQyqMHsocxM")]
        //public bool? LaborShowWeeksAndDays { get { return laborOrderTypeFields.ShowWeeksAndDays; } set { laborOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"3FP8GyHGu74O")]
        //public bool? LaborShowMonthsAndDays { get { return laborOrderTypeFields.ShowMonthsAndDays; } set { laborOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"yi2jOqhQkqhj")]
        //public bool? LaborShowPremiumPercent { get { return laborOrderTypeFields.ShowPremiumPercent; } set { laborOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"hSSJL1UuMHss")]
        //public bool? LaborShowDepartment { get { return laborOrderTypeFields.ShowDepartment; } set { laborOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"DDqO87ihoqsA")]
        //public bool? LaborShowLocation { get { return laborOrderTypeFields.ShowLocation; } set { laborOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"44tP70xKJIqV")]
        //public bool? LaborShowSubOrderNumber { get { return laborOrderTypeFields.ShowSubOrderNumber; } set { laborOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"lN428nu4O0VS")]
        //public bool? LaborShowOrderStatus { get { return laborOrderTypeFields.ShowOrderStatus; } set { laborOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"gZmkYJ3JpQWJ")]
        //public bool? LaborShowEpisodes { get { return laborOrderTypeFields.ShowEpisodes; } set { laborOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"AFiSc10G9Kar")]
        //public bool? LaborShowEpisodeExtended { get { return laborOrderTypeFields.ShowEpisodeExtended; } set { laborOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"f9DvxlXSSYmY")]
        //public bool? LaborShowEpisodeDiscountAmount { get { return laborOrderTypeFields.ShowEpisodeDiscountAmount; } set { laborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"0Q7C2fo3ayf6")]
        public string LaborDateStamp { get { return laborOrderTypeFields.DateStamp; } set { laborOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"kBB9RmAg28db")]
        public bool? HideCrewBreaks { get { return orderType.Hidecrewbreaks; } set { orderType.Hidecrewbreaks = value; } }

        [FwLogicProperty(Id:"cERFo3bouBrn")]
        public bool? Break1Paid { get { return orderType.Break1paId; } set { orderType.Break1paId = value; } }

        [FwLogicProperty(Id:"tLUf5SvUVtu4")]
        public bool? Break2Paid { get { return orderType.Break2paId; } set { orderType.Break2paId = value; } }

        [FwLogicProperty(Id:"Wh16sIScgFAO")]
        public bool? Break3Paid { get { return orderType.Break3paId; } set { orderType.Break3paId = value; } }



        //misc fields
        [JsonIgnore]
        [FwLogicProperty(Id:"AEHmD1OEBAvx")]
        public string MiscOrderTypeFieldsId { get { return miscOrderTypeFields.OrderTypeFieldsId; } set { orderType.MiscOrderTypeFieldsId = value; miscOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"nl67Zhy9Rdnj")]
        //public bool? MiscShowOrderNumber { get { return miscOrderTypeFields.ShowOrderNumber; } set { miscOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"4QKV2cYK34ag")]
        //public bool? MiscShowRepairOrderNumber { get { return miscOrderTypeFields.ShowRepairOrderNumber; } set { miscOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"b6nrtqvuZnjQ")]
        public bool? MiscShowICode { get { return miscOrderTypeFields.ShowICode; } set { miscOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"8mMPwuxorDKF")]
        public int? MiscICodeWidth { get { return miscOrderTypeFields.ICodeWidth; } set { miscOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"n9hOv9njngD7")]
        public bool? MiscShowDescription { get { return miscOrderTypeFields.ShowDescription; } set { miscOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"WilkAUdXsbGv")]
        public int? MiscDescriptionWidth { get { return miscOrderTypeFields.DescriptionWidth; } set { miscOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"YbF1AC9xPmrl")]
        //public bool? MiscShowPickDate { get { return miscOrderTypeFields.ShowPickDate; } set { miscOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"7prW2gZ7Nv7R")]
        //public bool? MiscShowPickTime { get { return miscOrderTypeFields.ShowPickTime; } set { miscOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"p69c2JrBiwGX")]
        public bool? MiscShowFromDate { get { return miscOrderTypeFields.ShowFromDate; } set { miscOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"teASUDh9D9EY")]
        public bool? MiscShowFromTime { get { return miscOrderTypeFields.ShowFromTime; } set { miscOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"TqA7RFogLSrX")]
        public bool? MiscShowToDate { get { return miscOrderTypeFields.ShowToDate; } set { miscOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"E1JJj6QUcoND")]
        public bool? MiscShowToTime { get { return miscOrderTypeFields.ShowToTime; } set { miscOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"s311ionqp8lL")]
        public bool? MiscShowBillablePeriods { get { return miscOrderTypeFields.ShowBillablePeriods; } set { miscOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"hsgITd5Qw60z")]
        public bool? MiscShowSubQuantity { get { return miscOrderTypeFields.ShowSubQuantity; } set { miscOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"MYxh6xuAjqzZ")]
        public bool? MiscShowWeeksAndDays { get { return miscOrderTypeFields.ShowWeeksAndDays; } set { miscOrderTypeFields.ShowWeeksAndDays = value; } }

        [FwLogicProperty(Id:"UCS8EjnLQ4Hk")]
        public bool? MiscShowMonthsAndDays { get { return miscOrderTypeFields.ShowMonthsAndDays; } set { miscOrderTypeFields.ShowMonthsAndDays = value; } }


        //[FwLogicProperty(Id:"NQgji63mU2Pu")]
        //public bool? MiscShowAvailableQuantity { get { return miscOrderTypeFields.ShowAvailableQuantity; } set { miscOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"e8lj7WdVG2Ck")]
        //public bool? MiscShowConflictDate { get { return miscOrderTypeFields.ShowConflictDate; } set { miscOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"ZhxEnUHwO8xn")]
        public bool? MiscShowUnit { get { return miscOrderTypeFields.ShowUnit; } set { miscOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"mgcdaUfX8oDq")]
        public bool? MiscShowRate { get { return miscOrderTypeFields.ShowRate; } set { miscOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"6ZBS6VP1nUo1")]
        public bool? MiscShowCost { get { return miscOrderTypeFields.ShowCost; } set { miscOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"RjmYJcb6Xa69")]
        //public bool? MiscShowWeeklyCostExtended { get { return miscOrderTypeFields.ShowWeeklyCostExtended; } set { miscOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"q8xMdLmqzxmZ")]
        //public bool? MiscShowMonthlyCostExtended { get { return miscOrderTypeFields.ShowMonthlyCostExtended; } set { miscOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"4Z1i4C6fvh19")]
        public bool? MiscShowPeriodCostExtended { get { return miscOrderTypeFields.ShowPeriodCostExtended; } set { miscOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"5nkCCvvBLwzG")]
        //public bool? MiscShowDaysPerWeek { get { return miscOrderTypeFields.ShowDaysPerWeek; } set { miscOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"xyERKV5LAWPs")]
        public bool? MiscShowDiscountPercent { get { return miscOrderTypeFields.ShowDiscountPercent; } set { miscOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"VFI2hFlxb43o")]
        //public bool? MiscShowMarkupPercent { get { return miscOrderTypeFields.ShowMarkupPercent; } set { miscOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"ecjdXzKqJDv1")]
        //public bool? MiscShowMarginPercent { get { return miscOrderTypeFields.ShowMarginPercent; } set { miscOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"bAn7WYqVH7QP")]
        public bool? MiscShowUnitDiscountAmount { get { return miscOrderTypeFields.ShowUnitDiscountAmount; } set { miscOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"zSckvVxju4vn")]
        public bool? MiscShowUnitExtended { get { return miscOrderTypeFields.ShowUnitExtended; } set { miscOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"8oGWjdOWWr5T")]
        public bool? MiscShowWeeklyDiscountAmount { get { return miscOrderTypeFields.ShowWeeklyDiscountAmount; } set { miscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"N1njLln2TqWS")]
        public bool? MiscShowWeeklyExtended { get { return miscOrderTypeFields.ShowWeeklyExtended; } set { miscOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"nuU62EZuZNYu")]
        public bool? MiscShowMonthlyDiscountAmount { get { return miscOrderTypeFields.ShowMonthlyDiscountAmount; } set { miscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"FNETfvp4m8mf")]
        public bool? MiscShowMonthlyExtended { get { return miscOrderTypeFields.ShowMonthlyExtended; } set { miscOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"WYRVcbogatvb")]
        public bool? MiscShowPeriodDiscountAmount { get { return miscOrderTypeFields.ShowPeriodDiscountAmount; } set { miscOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"hgS2tqSXg2Hh")]
        public bool? MiscShowPeriodExtended { get { return miscOrderTypeFields.ShowPeriodExtended; } set { miscOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"lcAXq5P4ZpcI")]
        //public bool? MiscShowVariancePercent { get { return miscOrderTypeFields.ShowVariancePercent; } set { miscOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"EjGOgJmpO8S5")]
        //public bool? MiscShowVarianceExtended { get { return miscOrderTypeFields.ShowVarianceExtended; } set { miscOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"oUZstTTeUrTl")]
        public bool? MiscShowWarehouse { get { return miscOrderTypeFields.ShowWarehouse; } set { miscOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"73wixRX0bq4q")]
        public bool? MiscShowTaxable { get { return miscOrderTypeFields.ShowTaxable; } set { miscOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"WKws81CZN3yS")]
        public bool? MiscShowNotes { get { return miscOrderTypeFields.ShowNotes; } set { miscOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"drYG1wAd3khI")]
        //public bool? MiscShowReturnToWarehouse { get { return miscOrderTypeFields.ShowReturnToWarehouse; } set { miscOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"ETkOGh0C2vhQ")]
        //public bool? MiscShowVehicleNumber { get { return miscOrderTypeFields.ShowVehicleNumber; } set { miscOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"cLSr4L0gbt0K")]
        //public bool? MiscShowBarCode { get { return miscOrderTypeFields.ShowBarCode; } set { miscOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"VQOfLqmLaSRe")]
        //public bool? MiscShowSerialNumber { get { return miscOrderTypeFields.ShowSerialNumber; } set { miscOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"nUee6ouMhFpP")]
        //public bool? MiscShowCrewName { get { return miscOrderTypeFields.ShowCrewName; } set { miscOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"rECMMuK6pp4H")]
        //public bool? MiscShowHours { get { return miscOrderTypeFields.ShowHours; } set { miscOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"G4xmaF6Npqsv")]
        //public bool? MiscShowAvailableQuantityAllWarehouses { get { return miscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { miscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"xUSclT1tLi54")]
        //public bool? MiscShowConflictDateAllWarehouses { get { return miscOrderTypeFields.ShowConflictDateAllWarehouses; } set { miscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"6lhJd79lyq2R")]
        //public bool? MiscShowConsignmentAvailableQuantity { get { return miscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { miscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"mgaCPrhWhQ64")]
        //public bool? MiscShowConsignmentConflictDate { get { return miscOrderTypeFields.ShowConsignmentConflictDate; } set { miscOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"69678O39WWBp")]
        //public bool? MiscShowConsignmentQuantity { get { return miscOrderTypeFields.ShowConsignmentQuantity; } set { miscOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"Wi0Us4u43gCx")]
        //public bool? MiscShowInLocationQuantity { get { return miscOrderTypeFields.ShowInLocationQuantity; } set { miscOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"mDiYsaHOrNPT")]
        //public bool? MiscShowReservedItems { get { return miscOrderTypeFields.ShowReservedItems; } set { miscOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"c1w7ti3mdiSE")]
        //public bool? MiscShowPremiumPercent { get { return miscOrderTypeFields.ShowPremiumPercent; } set { miscOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"cgIPn97Znl82")]
        //public bool? MiscShowDepartment { get { return miscOrderTypeFields.ShowDepartment; } set { miscOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"RXfWcRlm3VkD")]
        //public bool? MiscShowLocation { get { return miscOrderTypeFields.ShowLocation; } set { miscOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"9IgGgYgfIVJq")]
        //public bool? MiscShowOrderActivity { get { return miscOrderTypeFields.ShowOrderActivity; } set { miscOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"BJrVWcLhnCvq")]
        //public bool? MiscShowSubOrderNumber { get { return miscOrderTypeFields.ShowSubOrderNumber; } set { miscOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"qPXkyZXAnUdo")]
        //public bool? MiscShowOrderStatus { get { return miscOrderTypeFields.ShowOrderStatus; } set { miscOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"xOjgfdfbAPPb")]
        //public bool? MiscShowEpisodes { get { return miscOrderTypeFields.ShowEpisodes; } set { miscOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"JzC2gjYS9lXp")]
        //public bool? MiscShowEpisodeExtended { get { return miscOrderTypeFields.ShowEpisodeExtended; } set { miscOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"z3Q7MxKr7uCC")]
        //public bool? MiscShowEpisodeDiscountAmount { get { return miscOrderTypeFields.ShowEpisodeDiscountAmount; } set { miscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"L1sV2XgrsMKw")]
        public string MiscDateStamp { get { return miscOrderTypeFields.DateStamp; } set { miscOrderTypeFields.DateStamp = value; } }



        //rental sale
        [JsonIgnore]
        [FwLogicProperty(Id:"ujYvikPckB41")]
        public string RentalSaleOrderTypeFieldsId { get { return rentalSaleOrderTypeFields.OrderTypeFieldsId; } set { orderType.RentalSaleOrderTypeFieldsId = value; rentalSaleOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"tOipelsKD1EC")]
        //public bool? RentalSaleShowOrderNumber { get { return rentalSaleOrderTypeFields.ShowOrderNumber; } set { rentalSaleOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"9nFwzvxmUhDX")]
        //public bool? RentalSaleShowRepairOrderNumber { get { return rentalSaleOrderTypeFields.ShowRepairOrderNumber; } set { rentalSaleOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"L5OPkoLsdamz")]
        public bool? RentalSaleShowBarCode { get { return rentalSaleOrderTypeFields.ShowBarCode; } set { rentalSaleOrderTypeFields.ShowBarCode = value; } }

        [FwLogicProperty(Id:"rwyTTdxjbb1U")]
        public bool? RentalSaleShowSerialNumber { get { return rentalSaleOrderTypeFields.ShowSerialNumber; } set { rentalSaleOrderTypeFields.ShowSerialNumber = value; } }

        [FwLogicProperty(Id:"dQE3uD3baIkX")]
        public bool? RentalSaleShowICode { get { return rentalSaleOrderTypeFields.ShowICode; } set { rentalSaleOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"2OJ3hKAuftO6")]
        public int? RentalSaleICodeWidth { get { return rentalSaleOrderTypeFields.ICodeWidth; } set { rentalSaleOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"hNxmHmmhZC5R")]
        public bool? RentalSaleShowDescription { get { return rentalSaleOrderTypeFields.ShowDescription; } set { rentalSaleOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"MrlXLILSBMWp")]
        public int? RentalSaleDescriptionWidth { get { return rentalSaleOrderTypeFields.DescriptionWidth; } set { rentalSaleOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"fpz12wDWoGOo")]
        public bool? RentalSaleShowPickDate { get { return rentalSaleOrderTypeFields.ShowPickDate; } set { rentalSaleOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"9JK5cAm2cWKK")]
        public bool? RentalSaleShowPickTime { get { return rentalSaleOrderTypeFields.ShowPickTime; } set { rentalSaleOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"ZxYfzu4RvVpi")]
        //public bool? RentalSaleShowFromDate { get { return rentalSaleOrderTypeFields.ShowFromDate; } set { rentalSaleOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"PbrEROlegO0e")]
        //public bool? RentalSaleShowFromTime { get { return rentalSaleOrderTypeFields.ShowFromTime; } set { rentalSaleOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"Itp8O20UEy1X")]
        //public bool? RentalSaleShowToDate { get { return rentalSaleOrderTypeFields.ShowToDate; } set { rentalSaleOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"dgqJmWXzEZiz")]
        //public bool? RentalSaleShowToTime { get { return rentalSaleOrderTypeFields.ShowToTime; } set { rentalSaleOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"gzwax09Qa1XS")]
        //public bool? RentalSaleShowBillablePeriods { get { return rentalSaleOrderTypeFields.ShowBillablePeriods; } set { rentalSaleOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"P4R40WJC3H5k")]
        //public bool? RentalSaleShowSubQuantity { get { return rentalSaleOrderTypeFields.ShowSubQuantity; } set { rentalSaleOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"RrusRvu8gIQm")]
        public bool? RentalSaleShowAvailableQuantity { get { return rentalSaleOrderTypeFields.ShowAvailableQuantity; } set { rentalSaleOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"VJzT8X2rUZTV")]
        public bool? RentalSaleShowConflictDate { get { return rentalSaleOrderTypeFields.ShowConflictDate; } set { rentalSaleOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"NzVVyQDObZrQ")]
        public bool? RentalSaleShowUnit { get { return rentalSaleOrderTypeFields.ShowUnit; } set { rentalSaleOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"VP6s7xkLAztW")]
        public bool? RentalSaleShowRate { get { return rentalSaleOrderTypeFields.ShowRate; } set { rentalSaleOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"l3iHReO2LAOz")]
        public bool? RentalSaleShowCost { get { return rentalSaleOrderTypeFields.ShowCost; } set { rentalSaleOrderTypeFields.ShowCost = value; } }



        //[FwLogicProperty(Id:"pnA3QBY9C2jN")]
        //public bool? RentalSaleShowWeeklyCostExtended { get { return rentalSaleOrderTypeFields.ShowWeeklyCostExtended; } set { rentalSaleOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"FT79HMe1b7hv")]
        //public bool? RentalSaleShowMonthlyCostExtended { get { return rentalSaleOrderTypeFields.ShowMonthlyCostExtended; } set { rentalSaleOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"HOzoILrcWWLI")]
        //public bool? RentalSaleShowPeriodCostExtended { get { return rentalSaleOrderTypeFields.ShowPeriodCostExtended; } set { rentalSaleOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"TVcYL9mIH8Wg")]
        //public bool? RentalSaleShowDaysPerWeek { get { return rentalSaleOrderTypeFields.ShowDaysPerWeek; } set { rentalSaleOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"PH0Ud3HwRqA9")]
        public bool? RentalSaleShowDiscountPercent { get { return rentalSaleOrderTypeFields.ShowDiscountPercent; } set { rentalSaleOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"SV9mT2d3PPxo")]
        //public bool? RentalSaleShowMarkupPercent { get { return rentalSaleOrderTypeFields.ShowMarkupPercent; } set { rentalSaleOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"KhXJtdrKJGuf")]
        //public bool? RentalSaleShowMarginPercent { get { return rentalSaleOrderTypeFields.ShowMarginPercent; } set { rentalSaleOrderTypeFields.ShowMarginPercent = value; } }

        //[FwLogicProperty(Id:"H7BmdUTgyDKH")]
        //public bool? RentalSaleShowSplit { get { return rentalSaleOrderTypeFields.ShowSplit; } set { rentalSaleOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"EKiXo64gt0on")]
        public bool? RentalSaleShowUnitDiscountAmount { get { return rentalSaleOrderTypeFields.ShowUnitDiscountAmount; } set { rentalSaleOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"8zLRxAx7qNxJ")]
        public bool? RentalSaleShowUnitExtended { get { return rentalSaleOrderTypeFields.ShowUnitExtended; } set { rentalSaleOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"dEoAvq6j1e7q")]
        //public bool? RentalSaleShowWeeklyDiscountAmount { get { return rentalSaleOrderTypeFields.ShowWeeklyDiscountAmount; } set { rentalSaleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"4AnTbIMNqqLP")]
        //public bool? RentalSaleShowWeeklyExtended { get { return rentalSaleOrderTypeFields.ShowWeeklyExtended; } set { rentalSaleOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"s1Oguz6OR3NZ")]
        //public bool? RentalSaleShowMonthlyDiscountAmount { get { return rentalSaleOrderTypeFields.ShowMonthlyDiscountAmount; } set { rentalSaleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"CAv8U2ORmboY")]
        //public bool? RentalSaleShowMonthlyExtended { get { return rentalSaleOrderTypeFields.ShowMonthlyExtended; } set { rentalSaleOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"edfUySniLLgC")]
        public bool? RentalSaleShowPeriodDiscountAmount { get { return rentalSaleOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalSaleOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"0yTt8Fx4Y7dO")]
        public bool? RentalSaleShowPeriodExtended { get { return rentalSaleOrderTypeFields.ShowPeriodExtended; } set { rentalSaleOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"p5S5cevHOE15")]
        //public bool? RentalSaleShowVariancePercent { get { return rentalSaleOrderTypeFields.ShowVariancePercent; } set { rentalSaleOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"8t9sbLzlBVNk")]
        //public bool? RentalSaleShowVarianceExtended { get { return rentalSaleOrderTypeFields.ShowVariancePercent; } set { rentalSaleOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"IcYKv9jxmqHl")]
        //public bool? RentalSaleShowCountryOfOrigin { get { return rentalSaleOrderTypeFields.ShowCountryOfOrigin; } set { rentalSaleOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"7DfEytPZBwBz")]
        //public bool? RentalSaleShowManufacturer { get { return rentalSaleOrderTypeFields.ShowManufacturer; } set { rentalSaleOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"6mjYFfzYYUnj")]
        //public bool? RentalSaleShowManufacturerPartNumber { get { return rentalSaleOrderTypeFields.ShowManufacturerPartNumber; } set { rentalSaleOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"kne1xv4CwKJf")]
        //public int? RentalSaleManufacturerPartNumberWidth { get { return rentalSaleOrderTypeFields.ManufacturerPartNumberWidth; } set { rentalSaleOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"akD09nOjqyQj")]
        //public bool? RentalSaleShowModelNumber { get { return rentalSaleOrderTypeFields.ShowModelNumber; } set { rentalSaleOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"fiWuBELp3moX")]
        //public bool? RentalSaleShowVendorPartNumber { get { return rentalSaleOrderTypeFields.ShowVendorPartNumber; } set { rentalSaleOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"qTcQzzKN8NBK")]
        public bool? RentalSaleShowWarehouse { get { return rentalSaleOrderTypeFields.ShowWarehouse; } set { rentalSaleOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"VohHiKSo7n68")]
        public bool? RentalSaleShowTaxable { get { return rentalSaleOrderTypeFields.ShowTaxable; } set { rentalSaleOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"lX3TdpwsQB3l")]
        public bool? RentalSaleShowNotes { get { return rentalSaleOrderTypeFields.ShowNotes; } set { rentalSaleOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"fl4yHIfsGsoc")]
        //public bool? RentalSaleShowReturnToWarehouse { get { return rentalSaleOrderTypeFields.ShowNotes; } set { rentalSaleOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"SlNN9AxKvYeX")]
        //public bool? RentalSaleShowVehicleNumber { get { return rentalSaleOrderTypeFields.ShowToTime; } set { rentalSaleOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"PVIjb4kQAYqG")]
        //public bool? RentalSaleShowCrewName { get { return rentalSaleOrderTypeFields.ShowCrewName; } set { rentalSaleOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"iEANCKVM9PqY")]
        //public bool? RentalSaleShowHours { get { return rentalSaleOrderTypeFields.ShowHours; } set { rentalSaleOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"573FGoDFfbAN")]
        //public bool? RentalSaleShowAvailableQuantityAllWarehouses { get { return rentalSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { rentalSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"oDnintJjSXuZ")]
        //public bool? RentalSaleShowConflictDateAllWarehouses { get { return rentalSaleOrderTypeFields.ShowConflictDateAllWarehouses; } set { rentalSaleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"YY9m6JgvSz3G")]
        //public bool? RentalSaleShowConsignmentAvailableQuantity { get { return rentalSaleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { rentalSaleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"XGV9hYcBTYzR")]
        //public bool? RentalSaleShowConsignmentConflictDate { get { return rentalSaleOrderTypeFields.ShowConsignmentConflictDate; } set { rentalSaleOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"6MLcsS1BpDLL")]
        //public bool? RentalSaleShowConsignmentQuantity { get { return rentalSaleOrderTypeFields.ShowConsignmentQuantity; } set { rentalSaleOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"HzrpJeiL15hl")]
        //public bool? RentalSaleShowInLocationQuantity { get { return rentalSaleOrderTypeFields.ShowInLocationQuantity; } set { rentalSaleOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"PcVFDQrmp04v")]
        //public bool? RentalSaleShowReservedItems { get { return rentalSaleOrderTypeFields.ShowReservedItems; } set { rentalSaleOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"Qm1EB9SYwwiD")]
        //public bool? RentalSaleShowWeeksAndDays { get { return rentalSaleOrderTypeFields.ShowWeeksAndDays; } set { rentalSaleOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"70QDBS4Sde4P")]
        //public bool? RentalSaleShowMonthsAndDays { get { return rentalSaleOrderTypeFields.ShowMonthsAndDays; } set { rentalSaleOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"56HbOQ8GNSBj")]
        //public bool? RentalSaleShowPremiumPercent { get { return rentalSaleOrderTypeFields.ShowPremiumPercent; } set { rentalSaleOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"o8ta24rjgvBK")]
        //public bool? RentalSaleShowDepartment { get { return rentalSaleOrderTypeFields.ShowDepartment; } set { rentalSaleOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"aJmTKpPjlDnu")]
        //public bool? RentalSaleShowLocation { get { return rentalSaleOrderTypeFields.ShowLocation; } set { rentalSaleOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"rSdmVGlJ5LCE")]
        //public bool? RentalSaleShowOrderActivity { get { return rentalSaleOrderTypeFields.ShowOrderActivity; } set { rentalSaleOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"fKFqSex67EeI")]
        //public bool? RentalSaleShowSubOrderNumber { get { return rentalSaleOrderTypeFields.ShowSubOrderNumber; } set { rentalSaleOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"PooMjMRIXjrC")]
        //public bool? RentalSaleShowOrderStatus { get { return rentalSaleOrderTypeFields.ShowOrderStatus; } set { rentalSaleOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"XM2OLgweQChM")]
        //public bool? RentalSaleShowEpisodes { get { return rentalSaleOrderTypeFields.ShowEpisodes; } set { rentalSaleOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"FNCiREOJUZIW")]
        //public bool? RentalSaleShowEpisodeExtended { get { return rentalSaleOrderTypeFields.ShowEpisodeExtended; } set { rentalSaleOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"lrTy1x0igwj9")]
        //public bool? RentalSaleShowEpisodeDiscountAmount { get { return rentalSaleOrderTypeFields.ShowEpisodeDiscountAmount; } set { rentalSaleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"3hmf6PFXy90M")]
        public string RentalSaleDateStamp { get { return rentalSaleOrderTypeFields.DateStamp; } set { rentalSaleOrderTypeFields.DateStamp = value; } }



        //finalld
        [JsonIgnore]
        [FwLogicProperty(Id:"g72O7P9YepFm")]
        public string LossAndDamageOrderTypeFieldsId { get { return lossAndDamageOrderTypeFields.OrderTypeFieldsId; } set { orderType.LossAndDamageOrderTypeFieldsId = value; lossAndDamageOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"VOLDwQp0FKy1")]
        public bool? LossAndDamageShowOrderNumber { get { return lossAndDamageOrderTypeFields.ShowOrderNumber; } set { lossAndDamageOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"ahT2ldShg440")]
        //public bool? LossAndDamageShowRepairOrderNumber { get { return lossAndDamageOrderTypeFields.ShowRepairOrderNumber; } set { lossAndDamageOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"1OGxzGvlLHml")]
        public bool? LossAndDamageShowBarCode { get { return lossAndDamageOrderTypeFields.ShowBarCode; } set { lossAndDamageOrderTypeFields.ShowBarCode = value; } }

        [FwLogicProperty(Id:"Ws7m9X80zQkt")]
        public bool? LossAndDamageShowSerialNumber { get { return lossAndDamageOrderTypeFields.ShowSerialNumber; } set { lossAndDamageOrderTypeFields.ShowSerialNumber = value; } }

        [FwLogicProperty(Id:"LWKYxXDjBh53")]
        public bool? LossAndDamageShowICode { get { return lossAndDamageOrderTypeFields.ShowICode; } set { lossAndDamageOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"VAwyFJuvDSbG")]
        public int? LossAndDamageICodeWidth { get { return lossAndDamageOrderTypeFields.ICodeWidth; } set { lossAndDamageOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"lC8GqeMRp7qG")]
        public bool? LossAndDamageShowDescription { get { return lossAndDamageOrderTypeFields.ShowDescription; } set { lossAndDamageOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"3wNVqmCiZyzu")]
        public int? LossAndDamageDescriptionWidth { get { return lossAndDamageOrderTypeFields.DescriptionWidth; } set { lossAndDamageOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"lKF1jItxg6RP")]
        //public bool? LossAndDamageShowPickDate { get { return lossAndDamageOrderTypeFields.ShowPickDate; } set { lossAndDamageOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"5fdKQzZTTnvj")]
        //public bool? LossAndDamageShowPickTime { get { return lossAndDamageOrderTypeFields.ShowPickTime; } set { lossAndDamageOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"cNOvdILQ6uiu")]
        //public bool? LossAndDamageShowFromDate { get { return lossAndDamageOrderTypeFields.ShowFromDate; } set { lossAndDamageOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"rjc91XEsAZ5p")]
        //public bool? LossAndDamageShowFromTime { get { return lossAndDamageOrderTypeFields.ShowFromTime; } set { lossAndDamageOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"E9YAwe8opzFy")]
        //public bool? LossAndDamageShowToDate { get { return lossAndDamageOrderTypeFields.ShowToDate; } set { lossAndDamageOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"Q8kt9KUDTRoq")]
        //public bool? LossAndDamageShowToTime { get { return lossAndDamageOrderTypeFields.ShowToTime; } set { lossAndDamageOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"yFG0OcMb5Z4c")]
        //public bool? LossAndDamageShowBillablePeriods { get { return lossAndDamageOrderTypeFields.ShowBillablePeriods; } set { lossAndDamageOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"cHtJvHgrzHHh")]
        ////public bool? LossAndDamageShowSubQuantity { get { return lossAndDamageOrderTypeFields.ShowSubQuantity; } set { lossAndDamageOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"PzbSCe3LldED")]
        //public bool? LossAndDamageShowAvailableQuantity { get { return lossAndDamageOrderTypeFields.ShowAvailableQuantity; } set { lossAndDamageOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"i07ZWWHBYtPD")]
        //public bool? LossAndDamageShowConflictDate { get { return lossAndDamageOrderTypeFields.ShowConflictDate; } set { lossAndDamageOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"Oe2uyYPUS9oj")]
        public bool? LossAndDamageShowUnit { get { return lossAndDamageOrderTypeFields.ShowUnit; } set { lossAndDamageOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"t76dvyTy1Lfm")]
        public bool? LossAndDamageShowRate { get { return lossAndDamageOrderTypeFields.ShowRate; } set { lossAndDamageOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"mV527YoPDexT")]
        public bool? LossAndDamageShowCost { get { return lossAndDamageOrderTypeFields.ShowCost; } set { lossAndDamageOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"akewUThDgmAM")]
        //public bool? LossAndDamageShowWeeklyCostExtended { get { return lossAndDamageOrderTypeFields.ShowWeeklyCostExtended; } set { lossAndDamageOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"uk4M0YXTCIoh")]
        //public bool? LossAndDamageShowMonthlyCostExtended { get { return lossAndDamageOrderTypeFields.ShowMonthlyCostExtended; } set { lossAndDamageOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"MVRTo6XArrlL")]
        //public bool? LossAndDamageShowPeriodCostExtended { get { return lossAndDamageOrderTypeFields.ShowPeriodCostExtended; } set { lossAndDamageOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"9SQs4xCheiDt")]
        //public bool? LossAndDamageShowDaysPerWeek { get { return lossAndDamageOrderTypeFields.ShowDaysPerWeek; } set { lossAndDamageOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"Hj00B0YwwW0w")]
        public bool? LossAndDamageShowDiscountPercent { get { return lossAndDamageOrderTypeFields.ShowDiscountPercent; } set { lossAndDamageOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"BoMSMLhUnMMg")]
        //public bool? LossAndDamageShowMarkupPercent { get { return lossAndDamageOrderTypeFields.ShowMarkupPercent; } set { lossAndDamageOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"eMQwYwsP0OcD")]
        //public bool? LossAndDamageShowMarginPercent { get { return lossAndDamageOrderTypeFields.ShowMarginPercent; } set { lossAndDamageOrderTypeFields.ShowMarginPercent = value; } }

        //[FwLogicProperty(Id:"yzbkq9AWiqWV")]
        //public bool? LossAndDamageShowSplit { get { return lossAndDamageOrderTypeFields.ShowSplit; } set { lossAndDamageOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"AiM2eb0PNtNA")]
        public bool? LossAndDamageShowUnitDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowUnitDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"Ugg3vgbmFk55")]
        public bool? LossAndDamageShowUnitExtended { get { return lossAndDamageOrderTypeFields.ShowUnitExtended; } set { lossAndDamageOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"lj5vU6f3hbok")]
        //public bool? LossAndDamageShowWeeklyDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowWeeklyDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"GyBbfHMuJlMs")]
        //public bool? LossAndDamageShowWeeklyExtended { get { return lossAndDamageOrderTypeFields.ShowWeeklyExtended; } set { lossAndDamageOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"puLEyuVW2mvq")]
        //public bool? LossAndDamageShowMonthlyDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowMonthlyDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"UKJ5IB9pAXC9")]
        //public bool? LossAndDamageShowMonthlyExtended { get { return lossAndDamageOrderTypeFields.ShowMonthlyExtended; } set { lossAndDamageOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"D15NmwSbVHEb")]
        public bool? LossAndDamageShowPeriodDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowPeriodDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"RFWahReRTi0V")]
        public bool? LossAndDamageShowPeriodExtended { get { return lossAndDamageOrderTypeFields.ShowPeriodExtended; } set { lossAndDamageOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"yq4OE6gq5UR5")]
        //public bool? LossAndDamageShowVariancePercent { get { return lossAndDamageOrderTypeFields.ShowVariancePercent; } set { lossAndDamageOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"RTGqiQFrqPT1")]
        //public bool? LossAndDamageShowVarianceExtended { get { return lossAndDamageOrderTypeFields.ShowVarianceExtended; } set { lossAndDamageOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"K98jucMvTBMf")]
        //public bool? LossAndDamageShowCountryOfOrigin { get { return lossAndDamageOrderTypeFields.ShowCountryOfOrigin; } set { lossAndDamageOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"MyaQuNQrY3a4")]
        //public bool? LossAndDamageShowManufacturer { get { return lossAndDamageOrderTypeFields.ShowManufacturer; } set { lossAndDamageOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"XmnTFJS9yIDc")]
        //public bool? LossAndDamageShowManufacturerPartNumber { get { return lossAndDamageOrderTypeFields.ShowManufacturerPartNumber; } set { lossAndDamageOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"YkksT2ocBBmt")]
        //public int? LossAndDamageManufacturerPartNumberWidth { get { return lossAndDamageOrderTypeFields.ManufacturerPartNumberWidth; } set { lossAndDamageOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"fGkeBJWkWgIL")]
        //public bool? LossAndDamageShowModelNumber { get { return lossAndDamageOrderTypeFields.ShowModelNumber; } set { lossAndDamageOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"gHv9UOrVfd9Z")]
        //public bool? LossAndDamageShowVendorPartNumber { get { return lossAndDamageOrderTypeFields.ShowVendorPartNumber; } set { lossAndDamageOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"c77Cch8RmueN")]
        public bool? LossAndDamageShowWarehouse { get { return lossAndDamageOrderTypeFields.ShowWarehouse; } set { lossAndDamageOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"xLAM00VujB5Q")]
        public bool? LossAndDamageShowTaxable { get { return lossAndDamageOrderTypeFields.ShowTaxable; } set { lossAndDamageOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"MquzVNuARWbL")]
        public bool? LossAndDamageShowNotes { get { return lossAndDamageOrderTypeFields.ShowNotes; } set { lossAndDamageOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"L6Iy25UycGVS")]
        //public bool? LossAndDamageShowReturnToWarehouse { get { return lossAndDamageOrderTypeFields.ShowReturnToWarehouse; } set { lossAndDamageOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"7toET2O0nCnr")]
        //public bool? LossAndDamageShowVehicleNumber { get { return lossAndDamageOrderTypeFields.ShowVehicleNumber; } set { lossAndDamageOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"mlq835a13wVP")]
        //public bool? LossAndDamageShowCrewName { get { return lossAndDamageOrderTypeFields.ShowCrewName; } set { lossAndDamageOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"fj9WSsXhENpq")]
        //public bool? LossAndDamageShowHours { get { return lossAndDamageOrderTypeFields.ShowHours; } set { lossAndDamageOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"Eeyfm5xEoaoo")]
        //public bool? LossAndDamageShowAvailableQuantityAllWarehouses { get { return lossAndDamageOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { lossAndDamageOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"SnuyLK2oWnzC")]
        //public bool? LossAndDamageShowConflictDateAllWarehouses { get { return lossAndDamageOrderTypeFields.ShowConflictDateAllWarehouses; } set { lossAndDamageOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"9RBmoCJRCTiP")]
        //public bool? LossAndDamageShowConsignmentAvailableQuantity { get { return lossAndDamageOrderTypeFields.ShowConsignmentAvailableQuantity; } set { lossAndDamageOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"eWYQxvjoPUGP")]
        //public bool? LossAndDamageShowConsignmentConflictDate { get { return lossAndDamageOrderTypeFields.ShowConsignmentConflictDate; } set { lossAndDamageOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"LEcDaN5k1XzU")]
        //public bool? LossAndDamageShowConsignmentQuantity { get { return lossAndDamageOrderTypeFields.ShowConsignmentQuantity; } set { lossAndDamageOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"ifP5fJ5K0cka")]
        //public bool? LossAndDamageShowInLocationQuantity { get { return lossAndDamageOrderTypeFields.ShowInLocationQuantity; } set { lossAndDamageOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"oyi6syPw8f6q")]
        //public bool? LossAndDamageShowReservedItems { get { return lossAndDamageOrderTypeFields.ShowReservedItems; } set { lossAndDamageOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"mRqO7XEdgU9a")]
        //public bool? LossAndDamageShowWeeksAndDays { get { return lossAndDamageOrderTypeFields.ShowWeeksAndDays; } set { lossAndDamageOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"WuGCdhw95Brc")]
        //public bool? LossAndDamageShowMonthsAndDays { get { return lossAndDamageOrderTypeFields.ShowMonthsAndDays; } set { lossAndDamageOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"k3OXI09MwhhR")]
        //public bool? LossAndDamageShowPremiumPercent { get { return lossAndDamageOrderTypeFields.ShowPremiumPercent; } set { lossAndDamageOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"gZ3w91tK2Arw")]
        //public bool? LossAndDamageShowDepartment { get { return lossAndDamageOrderTypeFields.ShowDepartment; } set { lossAndDamageOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"Z4CrQVt6b1YI")]
        //public bool? LossAndDamageShowLocation { get { return lossAndDamageOrderTypeFields.ShowLocation; } set { lossAndDamageOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"ThBnCOTL1E06")]
        //public bool? LossAndDamageShowOrderActivity { get { return lossAndDamageOrderTypeFields.ShowOrderActivity; } set { lossAndDamageOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"iGTUjfUodUEl")]
        //public bool? LossAndDamageShowSubOrderNumber { get { return lossAndDamageOrderTypeFields.ShowSubOrderNumber; } set { lossAndDamageOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"jyjiCs0JX8VC")]
        //public bool? LossAndDamageShowOrderStatus { get { return lossAndDamageOrderTypeFields.ShowOrderStatus; } set { lossAndDamageOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"g1dUcSeUyC95")]
        //public bool? LossAndDamageShowEpisodes { get { return lossAndDamageOrderTypeFields.ShowEpisodes; } set { lossAndDamageOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"5HV9Orr6bFt0")]
        //public bool? LossAndDamageShowEpisodeExtended { get { return lossAndDamageOrderTypeFields.ShowEpisodeExtended; } set { lossAndDamageOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"HpDbetUqCSIZ")]
        //public bool? LossAndDamageShowEpisodeDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowEpisodeDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"5AdrwuvzYS5v")]
        public string LossAndDamageDateStamp { get { return lossAndDamageOrderTypeFields.DateStamp; } set { lossAndDamageOrderTypeFields.DateStamp = value; } }




        [FwLogicProperty(Id:"MDrbMnRohmY7")]
        public bool? AddInstallationAndStrikeFee { get { return orderType.Installstrikefee; } set { orderType.Installstrikefee = value; } }

        [FwLogicProperty(Id:"hCEWDWbL7D9n")]
        public string InstallationAndStrikeFeeRateId { get { return orderType.InstallstrikemasterId; } set { orderType.InstallstrikemasterId = value; } }

        [FwLogicProperty(Id:"7TDs9sxPsU6d", IsReadOnly:true)]
        public string InstallationAndStrikeFeeICode { get; set; }

        [FwLogicProperty(Id:"e6mnv1VCzF7o", IsReadOnly:true)]
        public string InstallationAndStrikeFeeDescription { get; set; }

        [FwLogicProperty(Id:"HjE0pXZheJGg")]
        public decimal? InstallationAndStrikeFeePercent { get { return orderType.Installstrikepct; } set { orderType.Installstrikepct = value; } }

        [FwLogicProperty(Id:"r4gvR6G3RZj5")]
        public string InstallationAndStrikeFeeBasedOn { get { return orderType.Installstrikebasedon; } set { orderType.Installstrikebasedon = value; } }

        [FwLogicProperty(Id:"ySgmr54NejZF")]
        public bool? AddManagementAndServiceFee { get { return orderType.Managementservicefee; } set { orderType.Managementservicefee = value; } }

        [FwLogicProperty(Id:"4DjjdXBnxZXN")]
        public string ManagementAndServiceFeeRateId { get { return orderType.ManagementservicemasterId; } set { orderType.ManagementservicemasterId = value; } }

        [FwLogicProperty(Id:"7vMU1GrQY9zM", IsReadOnly:true)]
        public string ManagementAndServiceFeeICode { get; set; }

        [FwLogicProperty(Id:"bJblpTr8NOon", IsReadOnly:true)]
        public string ManagementAndServiceFeeDescription { get; set; }

        [FwLogicProperty(Id:"3XrXeex8Qoqo")]
        public decimal? ManagementAndServiceFeePercent { get { return orderType.Managementservicepct; } set { orderType.Managementservicepct = value; } }

        [FwLogicProperty(Id:"WbTu86Hn2E6G")]
        public string ManagementAndServiceFeeBasedOn { get { return orderType.Managementservicebasedon; } set { orderType.Managementservicebasedon = value; } }



        [FwLogicProperty(Id:"lfbvkk44e1D5")]
        public string DefaultUsedSalePrice { get { return orderType.Selectrentalsaleprice; } set { orderType.Selectrentalsaleprice = value; } }



        [FwLogicProperty(Id:"gbyKbR0lf3pd")]
        public bool? QuikPayDiscount { get { return orderType.Quikpaydiscount; } set { orderType.Quikpaydiscount = value; } }

        [FwLogicProperty(Id:"MY5zA9J4Bsqv")]
        public string QuikPayDiscountType { get { return orderType.Quikpaydiscounttype; } set { orderType.Quikpaydiscounttype = value; } }

        [FwLogicProperty(Id:"O5oQN1cfNl9K")]
        public int? QuikPayDiscountDays { get { return orderType.Quikpaydiscountdays; } set { orderType.Quikpaydiscountdays = value; } }

        [FwLogicProperty(Id:"AFLYkzEZ2tfD")]
        public decimal? QuikPayDiscountPercent { get { return orderType.Quikpaydiscountpct; } set { orderType.Quikpaydiscountpct = value; } }

        [FwLogicProperty(Id:"4GW33zBLGMD9")]
        public bool? QuikPayDiscountExcludeSubs { get { return orderType.Quikpayexcludesubs; } set { orderType.Quikpayexcludesubs = value; } }



        [FwLogicProperty(Id:"nTUWBU1ezMP4")]
        public bool? QuikConfirmDiscount { get { return orderType.Quikconfirmdiscount; } set { orderType.Quikconfirmdiscount = value; } }

        [FwLogicProperty(Id:"jAWHEuKORldG")]
        public decimal? QuikConfirmDiscountPercent { get { return orderType.Quikconfirmdiscountpct; } set { orderType.Quikconfirmdiscountpct = value; } }

        [FwLogicProperty(Id:"vXzalPIUgvtF")]
        public int? QuikConfirmDiscountDays { get { return orderType.Quikconfirmdiscountdays; } set { orderType.Quikconfirmdiscountdays = value; } }



        [FwLogicProperty(Id:"9T7Wz9Zj31M5")]
        public bool? DisableCostGl { get { return orderType.Disablecostgl; } set { orderType.Disablecostgl = value; } }

        [FwLogicProperty(Id:"8yvxG1AmrNtt")]
        public bool? ExcludeFromTopSalesDashboard { get { return orderType.Excludefromtopsales; } set { orderType.Excludefromtopsales = value; } }



        //[FwLogicProperty(Id:"nnzudHvvwtDD")]
        //public bool? Combineactivitytabs { get { return orderType.Combineactivitytabs; } set { orderType.Combineactivitytabs = value; } }

        //[FwLogicProperty(Id:"bFrfu2D6QzKJ")]
        //public bool? Combinetabseparateitems { get { return orderType.Combinetabseparateitems; } set { orderType.Combinetabseparateitems = value; } }

        //[FwLogicProperty(Id:"4M38vLo5LXY0")]
        //public string Suborderbillby { get { return orderType.Suborderbillby; } set { orderType.Suborderbillby = value; } }

        //[FwLogicProperty(Id:"sNAj9nksViCx")]
        //public string Suborderavailabilityrule { get { return orderType.Suborderavailabilityrule; } set { orderType.Suborderavailabilityrule = value; } }

        //[FwLogicProperty(Id:"kSDFfowqTfJw")]
        //public string Suborderorderqty { get { return orderType.Suborderorderqty; } set { orderType.Suborderorderqty = value; } }

        //[FwLogicProperty(Id:"D4HURKUIV0I3")]
        //public string SuborderdefaultordertypeId { get { return orderType.SuborderdefaultordertypeId; } set { orderType.SuborderdefaultordertypeId = value; } }

        //[FwLogicProperty(Id:"Ecc7GmN9vU50")]
        //public string SuborderordertypefieldsId { get { return orderType.SuborderordertypefieldsId; } set { orderType.SuborderordertypefieldsId = value; } }

        //public string Invoiceclass { get { return orderType.Invoiceclass; } set { orderType.Invoiceclass = value; } }

        [FwLogicProperty(Id:"PtPRM2dusvuE")]
        public decimal? Orderby { get { return orderType.Orderby; } set { orderType.Orderby = value; } }

        [FwLogicProperty(Id:"L9SI4yklZawn")]
        public bool? Inactive { get { return orderType.Inactive; } set { orderType.Inactive = value; } }



        [FwLogicProperty(Id:"1gfcbRBawDCX", IsReadOnly:true)]
        public List<string> CombinedShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (CombineActivityTabs == true)
                {
                    //showFields.Add("RecTypeDisplay");
                    //if (RentalShowICode == true || RentalSaleShowICode == true || MiscShowICode == true || LaborShowICode == true) { showFields.Add("ICode"); }
                    //if (RentalShowDescription == true || SalesShowDescription == true || MiscShowDescription == true || LaborShowDescription == true) { showFields.Add("Description"); }
                    //if ((!(RentalShowICode == true || RentalSaleShowICode == true || MiscShowICode == true || LaborShowICode == true)) && (!(RentalShowDescription == true || SalesShowDescription == true || MiscShowDescription == true || LaborShowDescription == true))) { showFields.Add("ICode"); }
                    //showFields.Add("QuantityOrdered");
                    //if (RentalShowPickDate == true || SalesShowPickDate == true) { showFields.Add("PickDate"); }
                    //if (RentalShowFromDate == true || MiscShowFromDate == true || LaborShowFromDate == true) { showFields.Add("FromDate"); }
                    //if (RentalShowToDate == true || MiscShowToDate == true || LaborShowToDate == true) { showFields.Add("ToDate"); }
                    //if (RentalShowBillablePeriods == true || MiscShowBillablePeriods == true || LaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                    //if (RentalShowSubQuantity == true || SalesShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                    //if (RentalShowAvailableQuantity == true || SalesShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                    //if (RentalShowRate == true || SalesShowRate == true || MiscShowRate == true || LaborShowRate == true) { showFields.Add("Rate"); }
                    //if (RentalShowDaysPerWeek == true) { showFields.Add("DaysPerWeek"); }
                    //if (RentalShowDiscountPercent == true || SalesShowDiscountPercent == true || MiscShowDiscountPercent == true || LaborShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                    //if (RentalShowPeriodDiscountAmount == true || SalesShowPeriodDiscountAmount == true || MiscShowPeriodDiscountAmount == true || LaborShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                    //if (RentalShowPeriodExtended == true || SalesShowPeriodExtended == true || MiscShowPeriodExtended == true || LaborShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                    //if (RentalShowTaxable == true || SalesShowTaxable == true || MiscShowTaxable == true || LaborShowTaxable == true) { showFields.Add("Taxable"); }
                    //if (RentalShowWarehouse == true || SalesShowWarehouse == true || MiscShowWarehouse == true || LaborShowWarehouse == true) { showFields.Add("Warehouse"); }
                    //if (RentalShowReturnToWarehouse == true) { showFields.Add("ReturnToWarehouse"); }
                    //if (RentalShowNotes == true || SalesShowNotes == true || MiscShowNotes == true || LaborShowNotes == true) { showFields.Add("Notes"); }


                    showFields.Add("RecTypeDisplay");
                    if (RentalShowICode == true || RentalSaleShowICode == true || MiscShowICode == true || LaborShowICode == true) { showFields.Add("ICode"); }
                    if (RentalShowDescription == true || SalesShowDescription == true || MiscShowDescription == true || LaborShowDescription == true) { showFields.Add("Description"); }
                    if ((!(RentalShowICode == true || RentalSaleShowICode == true || MiscShowICode == true || LaborShowICode == true)) && (!(RentalShowDescription == true || SalesShowDescription == true || MiscShowDescription == true || LaborShowDescription == true))) { showFields.Add("ICode"); }
                    showFields.Add("QuantityOrdered");
                    if (RentalShowPickDate == true || SalesShowPickDate == true) { showFields.Add("PickDate"); }
                    if (RentalShowPickTime == true || SalesShowPickTime == true) { showFields.Add("PickTime"); }
                    if (RentalShowFromDate == true || MiscShowFromDate == true || LaborShowFromDate == true) { showFields.Add("FromDate"); }
                    if (RentalShowFromTime == true || MiscShowFromTime == true || LaborShowFromTime == true) { showFields.Add("FromTime"); }
                    if (RentalShowToDate == true || MiscShowToDate == true || LaborShowToDate == true) { showFields.Add("ToDate"); }
                    if (RentalShowToTime == true || MiscShowToTime == true || LaborShowToTime == true) { showFields.Add("ToTime"); }
                    if (RentalShowBillablePeriods == true || MiscShowBillablePeriods == true || LaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                    if (RentalShowSubQuantity == true || SalesShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                    if (RentalShowConsignmentQuantity == true) { showFields.Add("ConsignQuantity"); }
                    if (RentalShowReservedItems == true) { showFields.Add("ReservedItemQuantity"); }
                    if (RentalShowAvailableQuantity == true || SalesShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                    if (RentalShowAvailableQuantityAllWarehouses == true || SalesShowAvailableQuantityAllWarehouses == true) { showFields.Add("AvailableAllWarehousesQuantity"); }
                    if (RentalShowConflictDate == true || SalesShowConflictDate == true) { showFields.Add("ConflictDate"); }
                    if (RentalShowConflictDateAllWarehouses == true || SalesShowConflictDateAllWarehouses == true) { showFields.Add("ConflictDateAllWarehouses"); }
                    if (RentalShowConsignmentConflictDate == true) { showFields.Add("ConflictDateConsignment"); }
                    if (RentalShowUnit == true || SalesShowUnit == true || MiscShowUnit == true || LaborShowUnit == true) { showFields.Add("Unit"); }
                    if (RentalShowMarkupPercent == true || SalesShowMarkupPercent == true) { showFields.Add("MarkupPercent"); }
                    if (RentalShowMarginPercent == true || SalesShowMarginPercent == true) { showFields.Add("MarginPercent"); }
                    if (RentalShowRate == true || SalesShowRate == true || MiscShowRate == true || LaborShowRate == true) { showFields.Add("Rate"); }
                    if (RentalShowPremiumPercent == true) { showFields.Add("PremiumPercent"); }
                    if (RentalShowDaysPerWeek == true) { showFields.Add("DaysPerWeek"); }
                    if (RentalShowDiscountPercent == true || SalesShowDiscountPercent == true || MiscShowDiscountPercent == true || LaborShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                    if (RentalShowUnitDiscountAmount == true || SalesShowUnitDiscountAmount == true || MiscShowUnitDiscountAmount == true || LaborShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                    if (RentalShowUnitExtended == true || SalesShowUnitExtended == true || MiscShowUnitExtended == true || LaborShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                    if (RentalShowWeeklyDiscountAmount == true || MiscShowWeeklyDiscountAmount == true || LaborShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                    if (RentalShowWeeklyExtended == true || MiscShowWeeklyExtended == true || LaborShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                    if (RentalShowMonthlyDiscountAmount == true || MiscShowMonthlyDiscountAmount == true || LaborShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                    if (RentalShowMonthlyExtended == true || MiscShowMonthlyExtended == true || LaborShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                    if (RentalShowPeriodDiscountAmount == true || SalesShowPeriodDiscountAmount == true || MiscShowPeriodDiscountAmount == true || LaborShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                    if (RentalShowPeriodExtended == true || SalesShowPeriodExtended == true || MiscShowPeriodExtended == true || LaborShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                    if (RentalShowTaxable == true || SalesShowTaxable == true || MiscShowTaxable == true || LaborShowTaxable == true) { showFields.Add("Taxable"); }
                    if (RentalShowWarehouse == true || SalesShowWarehouse == true || MiscShowWarehouse == true || LaborShowWarehouse == true) { showFields.Add("Warehouse"); }
                    if (RentalShowReturnToWarehouse == true) { showFields.Add("ReturnToWarehouse"); }
                    if (RentalShowNotes == true || SalesShowNotes == true || MiscShowNotes == true || LaborShowNotes == true) { showFields.Add("Notes"); }
                }

                return showFields;
            }
            set { }
        }


        [FwLogicProperty(Id:"qpMMNO0ka6IY", IsReadOnly:true)]
        public List<string> RentalShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                //if (RentalShowICode == true) { showFields.Add("ICode"); }
                //if (RentalShowDescription == true) { showFields.Add("Description"); }
                //if ((!RentalShowICode == true) && (!RentalShowDescription == true)) { showFields.Add("ICode"); }
                //showFields.Add("QuantityOrdered");
                //if (RentalShowPickDate == true) { showFields.Add("PickDate"); }
                //if (RentalShowFromDate == true) { showFields.Add("FromDate"); }
                //if (RentalShowToDate == true) { showFields.Add("ToDate"); }
                //if (RentalShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                //if (RentalShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                //if (RentalShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                //if (RentalShowRate == true) { showFields.Add("Rate"); }
                //if (RentalShowDaysPerWeek == true) { showFields.Add("DaysPerWeek"); }
                //if (RentalShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                //if (RentalShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                //if (RentalShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                //if (RentalShowTaxable == true) { showFields.Add("Taxable"); }
                //if (RentalShowWarehouse == true) { showFields.Add("Warehouse"); }
                //if (RentalShowReturnToWarehouse == true) { showFields.Add("ReturnToWarehouse"); }
                //if (RentalShowNotes == true) { showFields.Add("Notes"); }


                if (RentalShowICode == true) { showFields.Add("ICode"); }
                if (RentalShowDescription == true) { showFields.Add("Description"); }
                if ((!(RentalShowICode == true)) && (!(RentalShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (RentalShowPickDate == true) { showFields.Add("PickDate"); }
                if (RentalShowPickTime == true) { showFields.Add("PickTime"); }
                if (RentalShowFromDate == true) { showFields.Add("FromDate"); }
                if (RentalShowFromTime == true) { showFields.Add("FromTime"); }
                if (RentalShowToDate == true) { showFields.Add("ToDate"); }
                if (RentalShowToTime == true) { showFields.Add("ToTime"); }
                if (RentalShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (RentalShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                if (RentalShowConsignmentQuantity == true) { showFields.Add("ConsignQuantity"); }
                if (RentalShowReservedItems == true) { showFields.Add("ReservedItemQuantity"); }
                if (RentalShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                if (RentalShowAvailableQuantityAllWarehouses == true) { showFields.Add("AvailableAllWarehousesQuantity"); }
                if (RentalShowConflictDate == true) { showFields.Add("ConflictDate"); }
                if (RentalShowConflictDateAllWarehouses == true) { showFields.Add("ConflictDateAllWarehouses"); }
                if (RentalShowConsignmentConflictDate == true) { showFields.Add("ConflictDateConsignment"); }
                if (RentalShowUnit == true) { showFields.Add("Unit"); }
                if (RentalShowMarkupPercent == true) { showFields.Add("MarkupPercent"); }
                if (RentalShowMarginPercent == true) { showFields.Add("MarginPercent"); }
                if (RentalShowRate == true) { showFields.Add("Rate"); }
                if (RentalShowPremiumPercent == true) { showFields.Add("PremiumPercent"); }
                if (RentalShowDaysPerWeek == true) { showFields.Add("DaysPerWeek"); }
                if (RentalShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (RentalShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (RentalShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (RentalShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (RentalShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (RentalShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (RentalShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (RentalShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (RentalShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (RentalShowTaxable == true) { showFields.Add("Taxable"); }
                if (RentalShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (RentalShowReturnToWarehouse == true) { showFields.Add("ReturnToWarehouse"); }
                if (RentalShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }

        [FwLogicProperty(Id:"8VFAulksW0gN", IsReadOnly:true)]
        public List<string> SalesShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                //if (SalesShowICode == true) { showFields.Add("ICode"); }
                //if (SalesShowDescription == true) { showFields.Add("Description"); }
                //if ((!SalesShowICode == true) && (!SalesShowDescription == true)) { showFields.Add("ICode"); }
                //showFields.Add("QuantityOrdered");
                //if (SalesShowPickDate == true) { showFields.Add("PickDate"); }
                //if (SalesShowFromDate == true) { showFields.Add("FromDate"); }
                //if (SalesShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                //if (SalesShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                //if (SalesShowRate == true) { showFields.Add("Rate"); }
                //if (SalesShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                //if (SalesShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                //if (SalesShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                //if (SalesShowTaxable == true) { showFields.Add("Taxable"); }
                //if (SalesShowWarehouse == true) { showFields.Add("Warehouse"); }
                //if (SalesShowNotes == true) { showFields.Add("Notes"); }


                if (SalesShowICode == true) { showFields.Add("ICode"); }
                if (SalesShowDescription == true) { showFields.Add("Description"); }
                if ((!(SalesShowICode == true)) && (!(SalesShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (SalesShowPickDate == true) { showFields.Add("PickDate"); }
                if (SalesShowPickTime == true) { showFields.Add("PickTime"); }
                if (SalesShowFromDate == true) { showFields.Add("FromDate"); }
                if (SalesShowFromTime == true) { showFields.Add("FromTime"); }
                if (SalesShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                if (SalesShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                if (SalesShowAvailableQuantityAllWarehouses == true) { showFields.Add("AvailableAllWarehousesQuantity"); }
                if (SalesShowConflictDate == true) { showFields.Add("ConflictDate"); }
                if (SalesShowConflictDateAllWarehouses == true) { showFields.Add("ConflictDateAllWarehouses"); }
                if (SalesShowUnit == true) { showFields.Add("Unit"); }
                if (SalesShowMarkupPercent == true) { showFields.Add("MarkupPercent"); }
                if (SalesShowMarginPercent == true) { showFields.Add("MarginPercent"); }
                if (SalesShowRate == true) { showFields.Add("Rate"); }
                if (SalesShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (SalesShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (SalesShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (SalesShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (SalesShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (SalesShowTaxable == true) { showFields.Add("Taxable"); }
                if (SalesShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (SalesShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }


        [FwLogicProperty(Id:"ADQi362J3DTY", IsReadOnly:true)]
        public List<string> MiscShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                //if (MiscShowICode == true) { showFields.Add("ICode"); }
                //if (MiscShowDescription == true) { showFields.Add("Description"); }
                //if ((!MiscShowICode == true) && (!MiscShowDescription == true)) { showFields.Add("ICode"); }
                //showFields.Add("QuantityOrdered");
                //if (MiscShowFromDate == true) { showFields.Add("FromDate"); }
                //if (MiscShowToDate == true) { showFields.Add("ToDate"); }
                //if (MiscShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                //if (MiscShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                //if (MiscShowRate == true) { showFields.Add("Rate"); }
                //if (MiscShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                //if (MiscShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                //if (MiscShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                //if (MiscShowTaxable == true) { showFields.Add("Taxable"); }
                //if (MiscShowWarehouse == true) { showFields.Add("Warehouse"); }
                //if (MiscShowNotes == true) { showFields.Add("Notes"); }


                if (MiscShowICode == true) { showFields.Add("ICode"); }
                if (MiscShowDescription == true) { showFields.Add("Description"); }
                if ((!(MiscShowICode == true)) && (!(MiscShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (MiscShowFromDate == true) { showFields.Add("FromDate"); }
                if (MiscShowFromTime == true) { showFields.Add("FromTime"); }
                if (MiscShowToDate == true) { showFields.Add("ToDate"); }
                if (MiscShowToTime == true) { showFields.Add("ToTime"); }
                if (MiscShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (MiscShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                if (MiscShowUnit == true) { showFields.Add("Unit"); }
                if (MiscShowRate == true) { showFields.Add("Rate"); }
                if (MiscShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (MiscShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (MiscShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (MiscShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (MiscShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (MiscShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (MiscShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (MiscShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (MiscShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (MiscShowTaxable == true) { showFields.Add("Taxable"); }
                if (MiscShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (MiscShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }



        [FwLogicProperty(Id:"8kxYYOb0Prm8", IsReadOnly:true)]
        public List<string> LaborShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                //if (LaborShowICode == true) { showFields.Add("ICode"); }
                //if (LaborShowDescription == true) { showFields.Add("Description"); }
                //if ((!LaborShowICode == true) && (!LaborShowDescription == true)) { showFields.Add("ICode"); }
                //showFields.Add("QuantityOrdered");
                //if (LaborShowFromDate == true) { showFields.Add("FromDate"); }
                //if (LaborShowToDate == true) { showFields.Add("ToDate"); }
                //if (LaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                //if (LaborShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                //if (LaborShowRate == true) { showFields.Add("Rate"); }
                //if (LaborShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                //if (LaborShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                //if (LaborShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                //if (LaborShowTaxable == true) { showFields.Add("Taxable"); }
                //if (LaborShowWarehouse == true) { showFields.Add("Warehouse"); }
                //if (LaborShowNotes == true) { showFields.Add("Notes"); }


                if (LaborShowICode == true) { showFields.Add("ICode"); }
                if (LaborShowDescription == true) { showFields.Add("Description"); }
                if ((!(LaborShowICode == true)) && (!(LaborShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (LaborShowFromDate == true) { showFields.Add("FromDate"); }
                if (LaborShowFromTime == true) { showFields.Add("FromTime"); }
                if (LaborShowToDate == true) { showFields.Add("ToDate"); }
                if (LaborShowToTime == true) { showFields.Add("ToTime"); }
                if (LaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (LaborShowSubQuantity == true) { showFields.Add("SubQuantity"); }
                if (LaborShowUnit == true) { showFields.Add("Unit"); }
                if (LaborShowRate == true) { showFields.Add("Rate"); }
                if (LaborShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (LaborShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (LaborShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (LaborShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (LaborShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (LaborShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (LaborShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (LaborShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (LaborShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (LaborShowTaxable == true) { showFields.Add("Taxable"); }
                if (LaborShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (LaborShowNotes == true) { showFields.Add("Notes"); }

                return showFields;
            }
            set { }
        }

        [FwLogicProperty(Id:"5TunYFwvQv4x", IsReadOnly:true)]
        public List<string> RentalSaleShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (RentalSaleShowBarCode == true) { showFields.Add("BarCode"); }
                if (RentalSaleShowSerialNumber == true) { showFields.Add("SerialNumber"); }
                if (RentalSaleShowICode == true) { showFields.Add("ICode"); }
                if (RentalSaleShowDescription == true) { showFields.Add("Description"); }
                if ((!(RentalSaleShowICode == true)) && (!(RentalSaleShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (RentalSaleShowPickDate == true) { showFields.Add("PickDate"); }
                if (RentalSaleShowPickTime == true) { showFields.Add("PickTime"); }
                if (RentalSaleShowAvailableQuantity == true) { showFields.Add("AvailableQuantity"); }
                if (RentalSaleShowConflictDate == true) { showFields.Add("ConflictDate"); }
                if (RentalSaleShowUnit == true) { showFields.Add("Unit"); }
                if (RentalSaleShowRate == true) { showFields.Add("Rate"); }
                if (RentalSaleShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (RentalSaleShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (RentalSaleShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (RentalSaleShowCost == true) { showFields.Add("UnitCost"); }
                if (RentalSaleShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (RentalSaleShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (RentalSaleShowTaxable == true) { showFields.Add("Taxable"); }
                if (RentalSaleShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (RentalSaleShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }



        [FwLogicProperty(Id:"wfMjZkUZbbgi", IsReadOnly:true)]
        public List<string> LossAndDamageShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (LossAndDamageShowOrderNumber == true) { showFields.Add("LossAndDamageOrderNumber"); }
                if (LossAndDamageShowBarCode == true) { showFields.Add("BarCode"); }
                if (LossAndDamageShowSerialNumber == true) { showFields.Add("SerialNumber"); }
                if (LossAndDamageShowICode == true) { showFields.Add("ICode"); }
                if (LossAndDamageShowDescription == true) { showFields.Add("Description"); }
                if ((!(LossAndDamageShowICode == true)) && (!(LossAndDamageShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (LossAndDamageShowUnit == true) { showFields.Add("Unit"); }
                if (LossAndDamageShowRate == true) { showFields.Add("Rate"); }
                if (LossAndDamageShowCost == true) { showFields.Add("Cost"); }
                if (LossAndDamageShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (LossAndDamageShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (LossAndDamageShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (LossAndDamageShowCost == true) { showFields.Add("UnitCost"); }
                if (LossAndDamageShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (LossAndDamageShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                showFields.Add("RetiredReason");
                if (LossAndDamageShowTaxable == true) { showFields.Add("Taxable"); }
                if (LossAndDamageShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (LossAndDamageShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }

        [FwLogicProperty(Id:"CLrP8W8Oh6hk")]
        public string DateStamp { get { return orderType.DateStamp; } set { orderType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    OrderTypeLogic orig = ((OrderTypeLogic)e.Original);
                    rentalOrderTypeFields.OrderTypeFieldsId = orig.RentalOrderTypeFieldsId;
                    salesOrderTypeFields.OrderTypeFieldsId = orig.SalesOrderTypeFieldsId;
                    laborOrderTypeFields.OrderTypeFieldsId = orig.LaborOrderTypeFieldsId;
                    miscOrderTypeFields.OrderTypeFieldsId = orig.MiscOrderTypeFieldsId;
                    spaceOrderTypeFields.OrderTypeFieldsId = orig.FacilityOrderTypeFieldsId;
                    vehicleOrderTypeFields.OrderTypeFieldsId = orig.VehicleOrderTypeFieldsId;
                    rentalSaleOrderTypeFields.OrderTypeFieldsId = orig.RentalSaleOrderTypeFieldsId;
                    lossAndDamageOrderTypeFields.OrderTypeFieldsId = orig.LossAndDamageOrderTypeFieldsId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                RentalOrderTypeFieldsId = rentalOrderTypeFields.OrderTypeFieldsId;
                SalesOrderTypeFieldsId = salesOrderTypeFields.OrderTypeFieldsId;
                LaborOrderTypeFieldsId = laborOrderTypeFields.OrderTypeFieldsId;
                MiscOrderTypeFieldsId = miscOrderTypeFields.OrderTypeFieldsId;
                FacilityOrderTypeFieldsId = spaceOrderTypeFields.OrderTypeFieldsId;
                VehicleOrderTypeFieldsId = vehicleOrderTypeFields.OrderTypeFieldsId;
                RentalSaleOrderTypeFieldsId = rentalSaleOrderTypeFields.OrderTypeFieldsId;
                LossAndDamageOrderTypeFieldsId = lossAndDamageOrderTypeFields.OrderTypeFieldsId;
                int i = SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------   
    }
}
