using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.OrderTypeFields;

namespace RentalWorksWebApi.Modules.Settings.OrderType
{
    public class OrderTypeLogic : RwBusinessLogic
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
        OrderTypeFieldsRecord finalLandDOrderTypeFields = new OrderTypeFieldsRecord();



        OrderTypeLoader orderTypeLoader = new OrderTypeLoader();
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
            dataRecords.Add(finalLandDOrderTypeFields);
            dataLoader = orderTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeId { get { return orderType.OrderTypeId; } set { orderType.OrderTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrderType { get { return orderType.OrderType; } set { orderType.OrderType = value; } }
        public string OrdType { get { return orderType.Ordtype; } set { orderType.Ordtype = value; } }
        public string DefaultPickTime { get { return orderType.Picktime; } set { orderType.Picktime = value; } }
        public string DefaultFromTime { get { return orderType.Fromtime; } set { orderType.Fromtime = value; } }
        public string DefaultToTime { get { return orderType.Totime; } set { orderType.Totime = value; } }
        public string DailyScheduleDefaultStartTime { get { return orderType.Defaultdaystarttime; } set { orderType.Defaultdaystarttime = value; } }
        public string DailyScheduleDefaultStopTime { get { return orderType.Defaultdaystoptime; } set { orderType.Defaultdaystoptime = value; } }
        public bool IsMasterSubOrderType { get { return orderType.Ismastersuborder; } set { orderType.Ismastersuborder = value; } }


        //rental fields
        public string RentalOrderTypeFieldsId { get { return rentalOrderTypeFields.OrderTypeFieldsId; } set { rentalOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool RentalShowOrderNumber { get { return rentalOrderTypeFields.ShowOrderNumber; } set { rentalOrderTypeFields.ShowOrderNumber = value; } }
        public bool RentalShowICode { get { return rentalOrderTypeFields.ShowICode; } set { rentalOrderTypeFields.ShowICode = value; } }
        public int? RentalICodeWidth { get { return rentalOrderTypeFields.ICodeWidth; } set { rentalOrderTypeFields.ICodeWidth = value; } }
        public bool RentalShowDescription { get { return rentalOrderTypeFields.ShowDescription; } set { rentalOrderTypeFields.ShowDescription = value; } }
        public int? RentalDescriptionWidth { get { return rentalOrderTypeFields.DescriptionWidth; } set { rentalOrderTypeFields.DescriptionWidth = value; } }
        public bool RentalShowPickDate { get { return rentalOrderTypeFields.ShowPickDate; } set { rentalOrderTypeFields.ShowPickDate = value; } }
        public bool RentalShowPickTime { get { return rentalOrderTypeFields.ShowPickTime; } set { rentalOrderTypeFields.ShowPickTime = value; } }
        public bool RentalShowFromDate { get { return rentalOrderTypeFields.ShowFromDate; } set { rentalOrderTypeFields.ShowFromDate = value; } }
        public bool RentalShowFromTime { get { return rentalOrderTypeFields.ShowFromTime; } set { rentalOrderTypeFields.ShowFromTime = value; } }
        public bool RentalShowToDate { get { return rentalOrderTypeFields.ShowToDate; } set { rentalOrderTypeFields.ShowToDate = value; } }
        public bool RentalShowToTime { get { return rentalOrderTypeFields.ShowToTime; } set { rentalOrderTypeFields.ShowToTime = value; } }
        public bool RentalShowBillablePeriods { get { return rentalOrderTypeFields.ShowBillablePeriods; } set { rentalOrderTypeFields.ShowBillablePeriods = value; } }
        public bool RentalShowEpisodes { get { return rentalOrderTypeFields.ShowEpisodes; } set { rentalOrderTypeFields.ShowEpisodes = value; } }
        public bool RentalShowSubQuantity { get { return rentalOrderTypeFields.ShowSubQuantity; } set { rentalOrderTypeFields.ShowSubQuantity = value; } }
        public bool RentalShowAvailableQuantity { get { return rentalOrderTypeFields.ShowAvailableQuantity; } set { rentalOrderTypeFields.ShowAvailableQuantity = value; } }
        public bool RentalShowConflictDate { get { return rentalOrderTypeFields.ShowConflictDate; } set { rentalOrderTypeFields.ShowConflictDate = value; } }
        public bool RentalShowAvailableQuantityAllWarehouses { get { return rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        public bool RentalShowConflictDateAllWarehouses { get { return rentalOrderTypeFields.ShowConflictDateAllWarehouses; } set { rentalOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        public bool RentalShowReservedItems { get { return rentalOrderTypeFields.ShowReservedItems; } set { rentalOrderTypeFields.ShowReservedItems = value; } }
        public bool RentalShowConsignmentQuantity { get { return rentalOrderTypeFields.ShowConsignmentQuantity; } set { rentalOrderTypeFields.ShowConsignmentQuantity = value; } }
        public bool RentalShowConsigmentAvailableQuantity { get { return rentalOrderTypeFields.ShowConsignmentAvailableQuantity; } set { rentalOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        public bool RentalShowConsigmentConflictDate { get { return rentalOrderTypeFields.ShowConsignmentConflictDate; } set { rentalOrderTypeFields.ShowConsignmentConflictDate = value; } }
        public bool RentalShowRate { get { return rentalOrderTypeFields.ShowRate; } set { rentalOrderTypeFields.ShowRate = value; } }
        public bool RentalShowDaysPerWeek { get { return rentalOrderTypeFields.ShowDaysPerWeek; } set { rentalOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool RentalShowPremiumPercent { get { return rentalOrderTypeFields.ShowPremiumPercent; } set { rentalOrderTypeFields.ShowPremiumPercent = value; } }
        public bool RentalShowUnit { get { return rentalOrderTypeFields.ShowUnit; } set { rentalOrderTypeFields.ShowUnit = value; } }
        public bool RentalShowCost { get { return rentalOrderTypeFields.ShowCost; } set { rentalOrderTypeFields.ShowCost = value; } }

        //public bool RentalShowWeeklyCostExtended { get { return rentalOrderTypeFields.ShowWeeklyCostExtended; } set { rentalOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool RentalShowMonthlyCostExtended { get { return rentalOrderTypeFields.ShowMonthlyCostExtended; } set { rentalOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool RentalShowPeriodCostExtended { get { return rentalOrderTypeFields.ShowPeriodCostExtended; } set { rentalOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool RentalShowDiscountPercent { get { return rentalOrderTypeFields.ShowDiscountPercent; } set { rentalOrderTypeFields.ShowDiscountPercent = value; } }
        public bool RentalShowMarkupPercent { get { return rentalOrderTypeFields.ShowMarkupPercent; } set { rentalOrderTypeFields.ShowMarkupPercent = value; } }
        public bool RentalShowMarginPercent { get { return rentalOrderTypeFields.ShowMarginPercent; } set { rentalOrderTypeFields.ShowMarginPercent = value; } }
        public bool RentalShowUnitDiscountAmount { get { return rentalOrderTypeFields.ShowUnitDiscountAmount; } set { rentalOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool RentalShowUnitExtended { get { return rentalOrderTypeFields.ShowUnitExtended; } set { rentalOrderTypeFields.ShowUnitExtended = value; } }
        public bool RentalShowWeeklyDiscountAmount { get { return rentalOrderTypeFields.ShowWeeklyDiscountAmount; } set { rentalOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool RentalShowWeeklyExtended { get { return rentalOrderTypeFields.ShowWeeklyExtended; } set { rentalOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool RentalShowEpisodeExtended { get { return rentalOrderTypeFields.ShowEpisodeExtended; } set { rentalOrderTypeFields.ShowEpisodeExtended = value; } }
        public bool RentalShowEpisodeDiscountAmount { get { return rentalOrderTypeFields.ShowEpisodeDiscountAmount; } set { rentalOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public bool RentalShowMonthlyDiscountAmount { get { return rentalOrderTypeFields.ShowMonthlyDiscountAmount; } set { rentalOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool RentalShowMonthlyExtended { get { return rentalOrderTypeFields.ShowMonthlyExtended; } set { rentalOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool RentalShowPeriodDiscountAmount { get { return rentalOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool RentalShowPeriodExtended { get { return rentalOrderTypeFields.ShowPeriodExtended; } set { rentalOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool RentalShowVariancePercent { get { return rentalOrderTypeFields.ShowVariancePercent; } set { rentalOrderTypeFields.ShowVariancePercent = value; } }
        //public bool RentalShowVarianceExtended { get { return rentalOrderTypeFields.ShowVarianceExtended; } set { rentalOrderTypeFields.ShowVarianceExtended = value; } }
        public bool RentalShowWarehouse { get { return rentalOrderTypeFields.ShowWarehouse; } set { rentalOrderTypeFields.ShowWarehouse = value; } }
        public bool RentalShowTaxable { get { return rentalOrderTypeFields.ShowTaxable; } set { rentalOrderTypeFields.ShowTaxable = value; } }
        public bool RentalShowNotes { get { return rentalOrderTypeFields.ShowNotes; } set { rentalOrderTypeFields.ShowNotes = value; } }
        public bool RentalShowReturnToWarehouse { get { return rentalOrderTypeFields.ShowReturnToWarehouse; } set { rentalOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool RentalShowInLocationQuantity { get { return rentalOrderTypeFields.ShowInLocationQuantity; } set { rentalOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool RentalShowWeeksAndDays { get { return rentalOrderTypeFields.ShowWeeksAndDays; } set { rentalOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool RentalShowMonthsAndDays { get { return rentalOrderTypeFields.ShowMonthsAndDays; } set { rentalOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool RentalShowDepartment { get { return rentalOrderTypeFields.ShowDepartment; } set { rentalOrderTypeFields.ShowDepartment = value; } }
        //public bool RentalShowLocation { get { return rentalOrderTypeFields.ShowLocation; } set { rentalOrderTypeFields.ShowLocation = value; } }
        //public bool RentalShowOrderActivity { get { return rentalOrderTypeFields.ShowOrderActivity; } set { rentalOrderTypeFields.ShowOrderActivity = value; } }
        //public bool RentalShowSubOrderNumber { get { return rentalOrderTypeFields.ShowSubOrderNumber; } set { rentalOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool RentalShowOrderStatus { get { return rentalOrderTypeFields.ShowOrderStatus; } set { rentalOrderTypeFields.ShowOrderStatus = value; } }
        public string RentalDateStamp { get { return rentalOrderTypeFields.DateStamp; } set { rentalOrderTypeFields.DateStamp = value; } }
        public bool AllowRoundTripRentals { get { return orderType.Roundtriprentals; } set { orderType.Roundtriprentals = value; } }

        //sales fields
        public string SalesOrderTypeFieldsId { get { return salesOrderTypeFields.OrderTypeFieldsId; } set { salesOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool SalesShowOrderNumber { get { return salesOrderTypeFields.ShowOrderNumber; } set { salesOrderTypeFields.ShowOrderNumber = value; } }
        public bool SalesShowICode { get { return salesOrderTypeFields.ShowICode; } set { salesOrderTypeFields.ShowICode = value; } }
        public int? SalesICodeWidth { get { return salesOrderTypeFields.ICodeWidth; } set { salesOrderTypeFields.ICodeWidth = value; } }
        public bool SalesShowDescription { get { return salesOrderTypeFields.ShowDescription; } set { salesOrderTypeFields.ShowDescription = value; } }
        public int? SalesDescriptionWidth { get { return salesOrderTypeFields.DescriptionWidth; } set { salesOrderTypeFields.DescriptionWidth = value; } }
        public bool SalesShowPickDate { get { return salesOrderTypeFields.ShowPickDate; } set { salesOrderTypeFields.ShowPickDate = value; } }
        public bool SalesShowPickTime { get { return salesOrderTypeFields.ShowPickTime; } set { salesOrderTypeFields.ShowPickTime = value; } }
        //public bool SalesShowFromDate { get { return salesOrderTypeFields.ShowFromDate; } set { salesOrderTypeFields.ShowFromDate = value; } }
        //public bool SalesShowFromTime { get { return salesOrderTypeFields.ShowFromTime; } set { salesOrderTypeFields.ShowFromTime = value; } }
        //public bool SalesShowToDate { get { return salesOrderTypeFields.ShowToDate; } set { salesOrderTypeFields.ShowToDate = value; } }
        //public bool SalesShowToTime { get { return salesOrderTypeFields.ShowToTime; } set { salesOrderTypeFields.ShowToTime = value; } }
        //public bool SalesShowBillablePeriods { get { return salesOrderTypeFields.ShowBillablePeriods; } set { salesOrderTypeFields.ShowBillablePeriods = value; } }
        public bool SalesShowSubQuantity { get { return salesOrderTypeFields.ShowSubQuantity; } set { salesOrderTypeFields.ShowSubQuantity = value; } }
        public bool SalesShowCost { get { return salesOrderTypeFields.ShowCost; } set { salesOrderTypeFields.ShowCost = value; } }
        public bool SalesShowRate { get { return salesOrderTypeFields.ShowRate; } set { salesOrderTypeFields.ShowRate = value; } }
        public bool SalesShowAvailableQuantity { get { return salesOrderTypeFields.ShowAvailableQuantity; } set { salesOrderTypeFields.ShowAvailableQuantity = value; } }
        public bool SalesShowConflictDate { get { return salesOrderTypeFields.ShowConflictDate; } set { salesOrderTypeFields.ShowConflictDate = value; } }
        public bool SalesShowAvailableQuantityAllWarehouses { get { return salesOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { salesOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        public bool SalesShowConflictDateAllWarehouses { get { return salesOrderTypeFields.ShowConflictDateAllWarehouses; } set { salesOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        public bool SalesShowMarkupPercent { get { return salesOrderTypeFields.ShowMarkupPercent; } set { salesOrderTypeFields.ShowMarkupPercent = value; } }
        public bool SalesShowMarginPercent { get { return salesOrderTypeFields.ShowMarginPercent; } set { salesOrderTypeFields.ShowMarginPercent = value; } }
        public bool SalesShowUnit { get { return salesOrderTypeFields.ShowUnit; } set { salesOrderTypeFields.ShowUnit = value; } }
        //public bool SalesShowWeeklyCostExtended { get { return salesOrderTypeFields.ShowWeeklyCostExtended; } set { salesOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool SalesShowMonthlyCostExtended { get { return salesOrderTypeFields.ShowMonthlyCostExtended; } set { salesOrderTypeFields.ShowMonthlyCostExtended = value; } }
        public bool SalesShowPeriodCostExtended { get { return salesOrderTypeFields.ShowPeriodCostExtended; } set { salesOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool SalesShowDiscountPercent { get { return salesOrderTypeFields.ShowDiscountPercent; } set { salesOrderTypeFields.ShowDiscountPercent = value; } }
        public bool SalesShowUnitDiscountAmount { get { return salesOrderTypeFields.ShowUnitDiscountAmount; } set { salesOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool SalesShowUnitExtended { get { return salesOrderTypeFields.ShowUnitExtended; } set { salesOrderTypeFields.ShowUnitExtended = value; } }
        //public bool SalesShowWeeklyDiscountAmount { get { return salesOrderTypeFields.ShowWeeklyDiscountAmount; } set { salesOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool SalesShowWeeklyExtended { get { return salesOrderTypeFields.ShowWeeklyExtended; } set { salesOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool SalesShowMonthlyDiscountAmount { get { return salesOrderTypeFields.ShowMonthlyDiscountAmount; } set { salesOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool SalesShowMonthlyExtended { get { return salesOrderTypeFields.ShowMonthlyExtended; } set { salesOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool SalesShowPeriodDiscountAmount { get { return salesOrderTypeFields.ShowPeriodDiscountAmount; } set { salesOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool SalesShowPeriodExtended { get { return salesOrderTypeFields.ShowPeriodExtended; } set { salesOrderTypeFields.ShowPeriodExtended = value; } }
        public bool SalesShowVariancePercent { get { return salesOrderTypeFields.ShowVariancePercent; } set { salesOrderTypeFields.ShowVariancePercent = value; } }
        public bool SalesShowVarianceExtended { get { return salesOrderTypeFields.ShowVarianceExtended; } set { salesOrderTypeFields.ShowVarianceExtended = value; } }
        public bool SalesShowWarehouse { get { return salesOrderTypeFields.ShowWarehouse; } set { salesOrderTypeFields.ShowWarehouse = value; } }
        public bool SalesShowTaxable { get { return salesOrderTypeFields.ShowTaxable; } set { salesOrderTypeFields.ShowTaxable = value; } }
        public bool SalesShowNotes { get { return salesOrderTypeFields.ShowNotes; } set { salesOrderTypeFields.ShowNotes = value; } }
        //public bool SalesShowReturnToWarehouse { get { return salesOrderTypeFields.ShowReturnToWarehouse; } set { salesOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool SalesShowConsigmentAvailableQuantity { get { return salesOrderTypeFields.ShowConsignmentAvailableQuantity; } set { salesOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool SalesShowConsigmentConflictDate { get { return salesOrderTypeFields.ShowConsignmentConflictDate; } set { salesOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool SalesShowConsignmentQuantity { get { return salesOrderTypeFields.ShowConsignmentQuantity; } set { salesOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool SalesShowInLocationQuantity { get { return salesOrderTypeFields.ShowInLocationQuantity; } set { salesOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool SalesShowReservedItems { get { return salesOrderTypeFields.ShowReservedItems; } set { salesOrderTypeFields.ShowReservedItems = value; } }
        //public bool SalesShowWeeksAndDays { get { return salesOrderTypeFields.ShowWeeksAndDays; } set { salesOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool SalesShowMonthsAndDays { get { return salesOrderTypeFields.ShowMonthsAndDays; } set { salesOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool SalesShowPremiumPercent { get { return salesOrderTypeFields.ShowPremiumPercent; } set { salesOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool SalesShowDepartment { get { return salesOrderTypeFields.ShowDepartment; } set { salesOrderTypeFields.ShowDepartment = value; } }
        //public bool SalesShowLocation { get { return salesOrderTypeFields.ShowLocation; } set { salesOrderTypeFields.ShowLocation = value; } }
        //public bool SalesShowOrderActivity { get { return salesOrderTypeFields.ShowOrderActivity; } set { salesOrderTypeFields.ShowOrderActivity = value; } }
        //public bool SalesShowSubOrderNumber { get { return salesOrderTypeFields.ShowSubOrderNumber; } set { salesOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool SalesShowOrderStatus { get { return salesOrderTypeFields.ShowOrderStatus; } set { salesOrderTypeFields.ShowOrderStatus = value; } }
        //public bool SalesShowEpisodes { get { return salesOrderTypeFields.ShowEpisodes; } set { salesOrderTypeFields.ShowEpisodes = value; } }
        //public bool SalesShowEpisodeExtended { get { return salesOrderTypeFields.ShowEpisodeExtended; } set { salesOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool SalesShowEpisodeDiscountAmount { get { return salesOrderTypeFields.ShowEpisodeDiscountAmount; } set { salesOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string SalesDateStamp { get { return salesOrderTypeFields.DateStamp; } set { salesOrderTypeFields.DateStamp = value; } }
        public bool SalesInventoryPrice { get { return orderType.Selectsalesprice; } set { orderType.Selectsalesprice = value; } }
        public bool SalesInventoryCost { get { return orderType.Selectsalescost; } set { orderType.Selectsalescost = value; } }

        //facilities fields
        public string FacilityOrderTypeFieldsId { get { return spaceOrderTypeFields.OrderTypeFieldsId; } set { spaceOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool FacilityShowOrderNumber { get { return spaceOrderTypeFields.ShowOrderNumber; } set { spaceOrderTypeFields.ShowOrderNumber = value; } }
        //public bool FacilityShowRepairOrderNumber { get { return spaceOrderTypeFields.ShowRepairOrderNumber; } set { spaceOrderTypeFields.ShowRepairOrderNumber = value; } }
        //public bool FacilityShowICode { get { return spaceOrderTypeFields.ShowICode; } set { spaceOrderTypeFields.ShowICode = value; } }
        //public int? FacilityICodeWidth { get { return spaceOrderTypeFields.ICodeWidth; } set { spaceOrderTypeFields.ICodeWidth = value; } }
        public bool FacilityShowDescription { get { return spaceOrderTypeFields.ShowDescription; } set { spaceOrderTypeFields.ShowDescription = value; } }
        public int? FacilityDescriptionWidth { get { return spaceOrderTypeFields.DescriptionWidth; } set { spaceOrderTypeFields.DescriptionWidth = value; } }
        //public bool FacilityShowPickDate { get { return spaceOrderTypeFields.ShowPickDate; } set { spaceOrderTypeFields.ShowPickDate = value; } }
        //public bool FacilityShowPickTime { get { return spaceOrderTypeFields.ShowPickTime; } set { spaceOrderTypeFields.ShowPickTime = value; } }
        public bool FacilityShowFromDate { get { return spaceOrderTypeFields.ShowFromDate; } set { spaceOrderTypeFields.ShowFromDate = value; } }
        public bool FacilityShowFromTime { get { return spaceOrderTypeFields.ShowFromTime; } set { spaceOrderTypeFields.ShowFromTime = value; } }
        public bool FacilityShowToDate { get { return spaceOrderTypeFields.ShowToDate; } set { spaceOrderTypeFields.ShowToDate = value; } }
        public bool FacilityShowToTime { get { return spaceOrderTypeFields.ShowToTime; } set { spaceOrderTypeFields.ShowToTime = value; } }
        public bool FacilityShowWeeksAndDays { get { return spaceOrderTypeFields.ShowWeeksAndDays; } set { spaceOrderTypeFields.ShowWeeksAndDays = value; } }
        public bool FacilityShowMonthsAndDays { get { return spaceOrderTypeFields.ShowMonthsAndDays; } set { spaceOrderTypeFields.ShowMonthsAndDays = value; } }
        public bool FacilityShowBillablePeriods { get { return spaceOrderTypeFields.ShowBillablePeriods; } set { spaceOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool FacilityShowSubQuantity { get { return spaceOrderTypeFields.ShowSubQuantity; } set { spaceOrderTypeFields.ShowSubQuantity = value; } }
        //public bool FacilityShowAvailableQuantity { get { return spaceOrderTypeFields.ShowAvailableQuantity; } set { spaceOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool FacilityShowConflictDate { get { return spaceOrderTypeFields.ShowConflictDate; } set { spaceOrderTypeFields.ShowConflictDate = value; } }
        public bool FacilityShowRate { get { return spaceOrderTypeFields.ShowRate; } set { spaceOrderTypeFields.ShowRate = value; } }
        //public bool FacilityShowCost { get { return spaceOrderTypeFields.ShowCost; } set { spaceOrderTypeFields.ShowCost = value; } }
        //public bool FacilityShowWeeklyCostExtended { get { return spaceOrderTypeFields.ShowWeeklyCostExtended; } set { spaceOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool FacilityShowMonthlyCostExtended { get { return spaceOrderTypeFields.ShowMonthlyCostExtended; } set { spaceOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool FacilityShowPeriodCostExtended { get { return spaceOrderTypeFields.ShowPeriodCostExtended; } set { spaceOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool FacilityShowDaysPerWeek { get { return spaceOrderTypeFields.ShowDaysPerWeek; } set { spaceOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool FacilityShowDiscountPercent { get { return spaceOrderTypeFields.ShowDiscountPercent; } set { spaceOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool FacilityShowMarkupPercent { get { return spaceOrderTypeFields.ShowMarkupPercent; } set { spaceOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool FacilityShowMarginPercent { get { return spaceOrderTypeFields.ShowMarginPercent; } set { spaceOrderTypeFields.ShowMarginPercent = value; } }
        public bool FacilityShowSplit { get { return spaceOrderTypeFields.ShowSplit; } set { spaceOrderTypeFields.ShowSplit = value; } }
        public bool FacilityShowUnit { get { return spaceOrderTypeFields.ShowUnit; } set { spaceOrderTypeFields.ShowUnit = value; } }
        public bool FacilityShowUnitDiscountAmount { get { return spaceOrderTypeFields.ShowUnitDiscountAmount; } set { spaceOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool FacilityShowUnitExtended { get { return spaceOrderTypeFields.ShowUnitExtended; } set { spaceOrderTypeFields.ShowUnitExtended = value; } }
        public bool FacilityShowWeeklyDiscountAmount { get { return spaceOrderTypeFields.ShowWeeklyDiscountAmount; } set { spaceOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool FacilityShowWeeklyExtended { get { return spaceOrderTypeFields.ShowWeeklyExtended; } set { spaceOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool FacilityShowMonthlyDiscountAmount { get { return spaceOrderTypeFields.ShowMonthlyDiscountAmount; } set { spaceOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool FacilityShowMonthlyExtended { get { return spaceOrderTypeFields.ShowMonthlyExtended; } set { spaceOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool FacilityShowPeriodDiscountAmount { get { return spaceOrderTypeFields.ShowPeriodDiscountAmount; } set { spaceOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool FacilityShowPeriodExtended { get { return spaceOrderTypeFields.ShowPeriodExtended; } set { spaceOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool FacilityShowVariancePercent { get { return spaceOrderTypeFields.ShowVariancePercent; } set { spaceOrderTypeFields.ShowVariancePercent = value; } }
        //public bool FacilityShowVarianceExtended { get { return spaceOrderTypeFields.ShowVarianceExtended; } set { spaceOrderTypeFields.ShowVarianceExtended = value; } }
        //public bool FacilityShowCountryOfOrigin { get { return spaceOrderTypeFields.ShowCountryOfOrigin; } set { spaceOrderTypeFields.ShowCountryOfOrigin = value; } }
        //public bool FacilityShowManufacturer { get { return spaceOrderTypeFields.ShowManufacturer; } set { spaceOrderTypeFields.ShowManufacturer = value; } }
        //public bool FacilityShowManufacturerPartNumber { get { return spaceOrderTypeFields.ShowManufacturerPartNumber; } set { spaceOrderTypeFields.ShowManufacturerPartNumber = value; } }
        //public int? FacilityManufacturerPartNumberWidth { get { return spaceOrderTypeFields.ManufacturerPartNumberWidth; } set { spaceOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        //public bool FacilityShowModelNumber { get { return spaceOrderTypeFields.ShowModelNumber; } set { spaceOrderTypeFields.ShowModelNumber = value; } }
        //public bool FacilityShowVendorPartNumber { get { return spaceOrderTypeFields.ShowVendorPartNumber; } set { spaceOrderTypeFields.ShowVendorPartNumber = value; } }
        //public bool FacilityShowWarehouse { get { return spaceOrderTypeFields.ShowWarehouse; } set { spaceOrderTypeFields.ShowWarehouse = value; } }
        public bool FacilityShowTaxable { get { return spaceOrderTypeFields.ShowTaxable; } set { spaceOrderTypeFields.ShowTaxable = value; } }
        public bool FacilityShowNotes { get { return spaceOrderTypeFields.ShowNotes; } set { spaceOrderTypeFields.ShowNotes = value; } }
        //public bool FacilityShowReturnToWarehouse { get { return spaceOrderTypeFields.ShowReturnToWarehouse; } set { spaceOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool FacilityShowVehicleNumber { get { return spaceOrderTypeFields.ShowVehicleNumber; } set { spaceOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool FacilityShowBarCode { get { return spaceOrderTypeFields.ShowBarCode; } set { spaceOrderTypeFields.ShowBarCode = value; } }
        //public bool FacilityShowSerialNumber { get { return spaceOrderTypeFields.ShowSerialNumber; } set { spaceOrderTypeFields.ShowSerialNumber = value; } }
        //public bool FacilityShowCrewName { get { return spaceOrderTypeFields.ShowCrewName; } set { spaceOrderTypeFields.ShowCrewName = value; } }
        //public bool FacilityShowHours { get { return spaceOrderTypeFields.ShowHours; } set { spaceOrderTypeFields.ShowHours = value; } }
        //public bool FacilityShowAvailableQuantityAllWarehouses { get { return spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool FacilityShowConflictDateAllWarehouses { get { return spaceOrderTypeFields.ShowConflictDateAllWarehouses; } set { spaceOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool FacilityShowConsignmentAvailableQuantity { get { return spaceOrderTypeFields.ShowConsignmentAvailableQuantity; } set { spaceOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool FacilityShowConsignmentConflictDate { get { return spaceOrderTypeFields.ShowConsignmentConflictDate; } set { spaceOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool FacilityShowConsignmentQuantity { get { return spaceOrderTypeFields.ShowConsignmentQuantity; } set { spaceOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool FacilityShowInLocationQuantity { get { return spaceOrderTypeFields.ShowInLocationQuantity; } set { spaceOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool FacilityShowReservedItems { get { return spaceOrderTypeFields.ShowReservedItems; } set { spaceOrderTypeFields.ShowReservedItems = value; } }
        //public bool FacilityShowPremiumPercent { get { return spaceOrderTypeFields.ShowPremiumPercent; } set { spaceOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool FacilityShowDepartment { get { return spaceOrderTypeFields.ShowDepartment; } set { spaceOrderTypeFields.ShowDepartment = value; } }
        //public bool FacilityShowLocation { get { return spaceOrderTypeFields.ShowLocation; } set { spaceOrderTypeFields.ShowLocation = value; } }
        //public bool FacilityShowOrderActivity { get { return spaceOrderTypeFields.ShowOrderActivity; } set { spaceOrderTypeFields.ShowOrderActivity = value; } }
        //public bool FacilityShowSubOrderNumber { get { return spaceOrderTypeFields.ShowSubOrderNumber; } set { spaceOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool FacilityShowOrderStatus { get { return spaceOrderTypeFields.ShowOrderStatus; } set { spaceOrderTypeFields.ShowOrderStatus = value; } }
        //public bool FacilityShowEpisodes { get { return spaceOrderTypeFields.ShowEpisodes; } set { spaceOrderTypeFields.ShowEpisodes = value; } }
        //public bool FacilityShowEpisodeExtended { get { return spaceOrderTypeFields.ShowEpisodeExtended; } set { spaceOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool FacilityShowEpisodeDiscountAmount { get { return spaceOrderTypeFields.ShowEpisodeDiscountAmount; } set { spaceOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string FacilityDateStamp { get { return spaceOrderTypeFields.DateStamp; } set { spaceOrderTypeFields.DateStamp = value; } }
        public string FacilityDescription { get { return orderType.Spacedescription; } set { orderType.Spacedescription = value; } }

        //transportation fields
        public string VehicleOrderTypeFieldsId { get { return vehicleOrderTypeFields.OrderTypeFieldsId; } set { vehicleOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool VehicleShowOrderNumber { get { return vehicleOrderTypeFields.ShowOrderNumber; } set { vehicleOrderTypeFields.ShowOrderNumber = value; } }
        //public bool VehicleShowRepairOrderNumber { get { return vehicleOrderTypeFields.ShowRepairOrderNumber; } set { vehicleOrderTypeFields.ShowRepairOrderNumber = value; } }
        //public bool VehicleShowICode { get { return vehicleOrderTypeFields.ShowICode; } set { vehicleOrderTypeFields.ShowICode = value; } }
        //public int? VehicleICodeWidth { get { return vehicleOrderTypeFields.ICodeWidth; } set { vehicleOrderTypeFields.ICodeWidth = value; } }
        public bool VehicleShowDescription { get { return vehicleOrderTypeFields.ShowDescription; } set { vehicleOrderTypeFields.ShowDescription = value; } }
        public int? VehicleDescriptionWidth { get { return vehicleOrderTypeFields.DescriptionWidth; } set { vehicleOrderTypeFields.DescriptionWidth = value; } }
        public bool VehicleShowVehicleNumber { get { return vehicleOrderTypeFields.ShowVehicleNumber; } set { vehicleOrderTypeFields.ShowVehicleNumber = value; } }
        public bool VehicleShowPickDate { get { return vehicleOrderTypeFields.ShowPickDate; } set { vehicleOrderTypeFields.ShowPickDate = value; } }
        public bool VehicleShowPickTime { get { return vehicleOrderTypeFields.ShowPickTime; } set { vehicleOrderTypeFields.ShowPickTime = value; } }
        public bool VehicleShowFromDate { get { return vehicleOrderTypeFields.ShowFromDate; } set { vehicleOrderTypeFields.ShowFromDate = value; } }
        public bool VehicleShowFromTime { get { return vehicleOrderTypeFields.ShowFromTime; } set { vehicleOrderTypeFields.ShowFromTime = value; } }
        public bool VehicleShowToDate { get { return vehicleOrderTypeFields.ShowToDate; } set { vehicleOrderTypeFields.ShowToDate = value; } }
        public bool VehicleShowToTime { get { return vehicleOrderTypeFields.ShowToTime; } set { vehicleOrderTypeFields.ShowToTime = value; } }
        public bool VehicleShowBillablePeriods { get { return vehicleOrderTypeFields.ShowBillablePeriods; } set { vehicleOrderTypeFields.ShowBillablePeriods = value; } }
        public bool VehicleShowSubQuantity { get { return vehicleOrderTypeFields.ShowSubQuantity; } set { vehicleOrderTypeFields.ShowSubQuantity = value; } }
        public bool VehicleShowAvailableQuantity { get { return vehicleOrderTypeFields.ShowAvailableQuantity; } set { vehicleOrderTypeFields.ShowAvailableQuantity = value; } }
        public bool VehicleShowConflictDate { get { return vehicleOrderTypeFields.ShowConflictDate; } set { vehicleOrderTypeFields.ShowConflictDate = value; } }
        public bool VehicleShowUnit { get { return vehicleOrderTypeFields.ShowUnit; } set { vehicleOrderTypeFields.ShowUnit = value; } }
        public bool VehicleShowRate { get { return vehicleOrderTypeFields.ShowRate; } set { vehicleOrderTypeFields.ShowRate = value; } }
        public bool VehicleShowDaysPerWeek { get { return vehicleOrderTypeFields.ShowDaysPerWeek; } set { vehicleOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool VehicleShowCost { get { return vehicleOrderTypeFields.ShowCost; } set { vehicleOrderTypeFields.ShowCost = value; } }
        public bool VehicleShowWeeklyCostExtended { get { return vehicleOrderTypeFields.ShowWeeklyCostExtended; } set { vehicleOrderTypeFields.ShowWeeklyCostExtended = value; } }
        public bool VehicleShowMonthlyCostExtended { get { return vehicleOrderTypeFields.ShowMonthlyCostExtended; } set { vehicleOrderTypeFields.ShowMonthlyCostExtended = value; } }
        public bool VehicleShowPeriodCostExtended { get { return vehicleOrderTypeFields.ShowPeriodCostExtended; } set { vehicleOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool VehicleShowDiscountPercent { get { return vehicleOrderTypeFields.ShowDiscountPercent; } set { vehicleOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool VehicleShowMarkupPercent { get { return vehicleOrderTypeFields.ShowMarkupPercent; } set { vehicleOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool VehicleShowMarginPercent { get { return vehicleOrderTypeFields.ShowMarginPercent; } set { vehicleOrderTypeFields.ShowMarginPercent = value; } }
        public bool VehicleShowUnitDiscountAmount { get { return vehicleOrderTypeFields.ShowUnitDiscountAmount; } set { vehicleOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool VehicleShowUnitExtended { get { return vehicleOrderTypeFields.ShowUnitExtended; } set { vehicleOrderTypeFields.ShowUnitExtended = value; } }
        public bool VehicleShowWeeklyDiscountAmount { get { return vehicleOrderTypeFields.ShowWeeklyDiscountAmount; } set { vehicleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool VehicleShowWeeklyExtended { get { return vehicleOrderTypeFields.ShowWeeklyExtended; } set { vehicleOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool VehicleShowMonthlyDiscountAmount { get { return vehicleOrderTypeFields.ShowMonthlyDiscountAmount; } set { vehicleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool VehicleShowMonthlyExtended { get { return vehicleOrderTypeFields.ShowMonthlyExtended; } set { vehicleOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool VehicleShowPeriodDiscountAmount { get { return vehicleOrderTypeFields.ShowPeriodDiscountAmount; } set { vehicleOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool VehicleShowPeriodExtended { get { return vehicleOrderTypeFields.ShowPeriodDiscountAmount; } set { vehicleOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        //public bool VehicleShowVariancePercent { get { return vehicleOrderTypeFields.ShowVariancePercent; } set { vehicleOrderTypeFields.ShowVariancePercent = value; } }
        //public bool VehicleShowVarianceExtended { get { return vehicleOrderTypeFields.ShowVarianceExtended; } set { vehicleOrderTypeFields.ShowVarianceExtended = value; } }
        //public bool VehicleShowCountryOfOrigin { get { return vehicleOrderTypeFields.ShowCountryOfOrigin; } set { vehicleOrderTypeFields.ShowCountryOfOrigin = value; } }
        //public bool VehicleShowManufacturer { get { return vehicleOrderTypeFields.ShowManufacturer; } set { vehicleOrderTypeFields.ShowManufacturer = value; } }
        //public bool VehicleShowManufacturerPartNumber { get { return vehicleOrderTypeFields.ShowManufacturerPartNumber; } set { vehicleOrderTypeFields.ShowManufacturerPartNumber = value; } }
        //public int? VehicleManufacturerPartNumberWidth { get { return vehicleOrderTypeFields.ManufacturerPartNumberWidth; } set { vehicleOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        //public bool VehicleShowModelNumber { get { return vehicleOrderTypeFields.ShowModelNumber; } set { vehicleOrderTypeFields.ShowModelNumber = value; } }
        //public bool VehicleShowVendorPartNumber { get { return vehicleOrderTypeFields.ShowVendorPartNumber; } set { vehicleOrderTypeFields.ShowVendorPartNumber = value; } }
        public bool VehicleShowWarehouse { get { return vehicleOrderTypeFields.ShowWarehouse; } set { vehicleOrderTypeFields.ShowWarehouse = value; } }
        public bool VehicleShowReturnToWarehouse { get { return vehicleOrderTypeFields.ShowReturnToWarehouse; } set { vehicleOrderTypeFields.ShowReturnToWarehouse = value; } }
        public bool VehicleShowTaxable { get { return vehicleOrderTypeFields.ShowTaxable; } set { vehicleOrderTypeFields.ShowTaxable = value; } }
        public bool VehicleShowNotes { get { return vehicleOrderTypeFields.ShowNotes; } set { vehicleOrderTypeFields.ShowNotes = value; } }
        //public bool VehicleShowBarCode { get { return vehicleOrderTypeFields.ShowBarCode; } set { vehicleOrderTypeFields.ShowBarCode = value; } }
        //public bool VehicleShowSerialNumber { get { return vehicleOrderTypeFields.ShowSerialNumber; } set { vehicleOrderTypeFields.ShowSerialNumber = value; } }
        //public bool VehicleShowCrewName { get { return vehicleOrderTypeFields.ShowCrewName; } set { vehicleOrderTypeFields.ShowCrewName = value; } }
        //public bool VehicleShowHours { get { return vehicleOrderTypeFields.ShowHours; } set { vehicleOrderTypeFields.ShowHours = value; } }
        //public bool VehicleShowAvailableQuantityAllWarehouses { get { return vehicleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { vehicleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool VehicleShowConflictDateAllWarehouses { get { return vehicleOrderTypeFields.ShowConflictDateAllWarehouses; } set { vehicleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool VehicleShowConsignmentAvailableQuantity { get { return vehicleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { vehicleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool VehicleShowConsignmentConflictDate { get { return vehicleOrderTypeFields.ShowConsignmentConflictDate; } set { vehicleOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool VehicleShowConsignmentQuantity { get { return vehicleOrderTypeFields.ShowConsignmentQuantity; } set { vehicleOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool VehicleShowInLocationQuantity { get { return vehicleOrderTypeFields.ShowInLocationQuantity; } set { vehicleOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool VehicleShowReservedItems { get { return vehicleOrderTypeFields.ShowReservedItems; } set { vehicleOrderTypeFields.ShowReservedItems = value; } }
        //public bool VehicleShowWeeksAndDays { get { return vehicleOrderTypeFields.ShowWeeksAndDays; } set { vehicleOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool VehicleShowMonthsAndDays { get { return vehicleOrderTypeFields.ShowMonthsAndDays; } set { vehicleOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool VehicleShowPremiumPercent { get { return vehicleOrderTypeFields.ShowPremiumPercent; } set { vehicleOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool VehicleShowDepartment { get { return vehicleOrderTypeFields.ShowDepartment; } set { vehicleOrderTypeFields.ShowDepartment = value; } }
        //public bool VehicleShowLocation { get { return vehicleOrderTypeFields.ShowLocation; } set { vehicleOrderTypeFields.ShowLocation = value; } }
        //public bool VehicleShowOrderActivity { get { return vehicleOrderTypeFields.ShowOrderActivity; } set { vehicleOrderTypeFields.ShowOrderActivity = value; } }
        //public bool VehicleShowSubOrderNumber { get { return vehicleOrderTypeFields.ShowSubOrderNumber; } set { vehicleOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool VehicleShowOrderStatus { get { return vehicleOrderTypeFields.ShowOrderStatus; } set { vehicleOrderTypeFields.ShowOrderStatus = value; } }
        //public bool VehicleShowEpisodes { get { return vehicleOrderTypeFields.ShowEpisodes; } set { vehicleOrderTypeFields.ShowEpisodes = value; } }
        //public bool VehicleShowEpisodeExtended { get { return vehicleOrderTypeFields.ShowEpisodeExtended; } set { vehicleOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool VehicleShowEpisodeDiscountAmount { get { return vehicleOrderTypeFields.ShowEpisodeDiscountAmount; } set { vehicleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string VehicleDateStamp { get { return vehicleOrderTypeFields.DateStamp; } set { vehicleOrderTypeFields.DateStamp = value; } }

        //labor/crew fields
        public string LaborOrderTypeFieldsId { get { return laborOrderTypeFields.OrderTypeFieldsId; } set { laborOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool LaborShowOrderNumber { get { return laborOrderTypeFields.ShowOrderNumber; } set { laborOrderTypeFields.ShowOrderNumber = value; } }
        //public bool LaborShowRepairOrderNumber { get { return laborOrderTypeFields.ShowRepairOrderNumber; } set { laborOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool LaborShowICode { get { return laborOrderTypeFields.ShowICode; } set { laborOrderTypeFields.ShowICode = value; } }
        public int? LaborICodeWidth { get { return laborOrderTypeFields.ICodeWidth; } set { laborOrderTypeFields.ICodeWidth = value; } }
        public bool LaborShowDescription { get { return laborOrderTypeFields.ShowDescription; } set { laborOrderTypeFields.ShowDescription = value; } }
        public int? LaborDescriptionWidth { get { return laborOrderTypeFields.DescriptionWidth; } set { laborOrderTypeFields.DescriptionWidth = value; } }
        public bool LaborShowOrderActivity { get { return laborOrderTypeFields.ShowOrderActivity; } set { laborOrderTypeFields.ShowOrderActivity = value; } }
        public bool LaborShowCrewName { get { return laborOrderTypeFields.ShowCrewName; } set { laborOrderTypeFields.ShowCrewName = value; } }
        //public bool LaborShowPickDate { get { return laborOrderTypeFields.ShowPickDate; } set { laborOrderTypeFields.ShowPickDate = value; } }
        //public bool LaborShowPickTime { get { return laborOrderTypeFields.ShowPickTime; } set { laborOrderTypeFields.ShowPickTime = value; } }
        public bool LaborShowFromDate { get { return laborOrderTypeFields.ShowFromDate; } set { laborOrderTypeFields.ShowFromDate = value; } }
        public bool LaborShowFromTime { get { return laborOrderTypeFields.ShowFromTime; } set { laborOrderTypeFields.ShowFromTime = value; } }
        public bool LaborShowToDate { get { return laborOrderTypeFields.ShowToDate; } set { laborOrderTypeFields.ShowToDate = value; } }
        public bool LaborShowToTime { get { return laborOrderTypeFields.ShowToTime; } set { laborOrderTypeFields.ShowToTime = value; } }
        public bool LaborShowBillablePeriods { get { return laborOrderTypeFields.ShowBillablePeriods; } set { laborOrderTypeFields.ShowBillablePeriods = value; } }
        public bool LaborShowHours { get { return laborOrderTypeFields.ShowHours; } set { laborOrderTypeFields.ShowHours = value; } }
        public bool LaborShowSubQuantity { get { return laborOrderTypeFields.ShowSubQuantity; } set { laborOrderTypeFields.ShowSubQuantity = value; } }
        //public bool LaborShowAvailableQuantity { get { return laborOrderTypeFields.ShowAvailableQuantity; } set { laborOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool LaborShowConflictDate { get { return laborOrderTypeFields.ShowConflictDate; } set { laborOrderTypeFields.ShowConflictDate = value; } }
        public bool LaborShowCost { get { return laborOrderTypeFields.ShowCost; } set { laborOrderTypeFields.ShowCost = value; } }
        public bool LaborShowRate { get { return laborOrderTypeFields.ShowRate; } set { laborOrderTypeFields.ShowRate = value; } }
        //public bool LaborShowWeeklyCostExtended { get { return laborOrderTypeFields.ShowWeeklyCostExtended; } set { laborOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool LaborShowMonthlyCostExtended { get { return laborOrderTypeFields.ShowMonthlyCostExtended; } set { laborOrderTypeFields.ShowMonthlyCostExtended = value; } }
        public bool LaborShowPeriodCostExtended { get { return laborOrderTypeFields.ShowPeriodCostExtended; } set { laborOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool LaborShowDaysPerWeek { get { return laborOrderTypeFields.ShowDaysPerWeek; } set { laborOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool LaborShowDiscountPercent { get { return laborOrderTypeFields.ShowDiscountPercent; } set { laborOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool LaborShowMarkupPercent { get { return laborOrderTypeFields.ShowMarkupPercent; } set { laborOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool LaborShowMarginPercent { get { return laborOrderTypeFields.ShowMarginPercent; } set { laborOrderTypeFields.ShowMarginPercent = value; } }
        public bool LaborShowUnit { get { return laborOrderTypeFields.ShowUnit; } set { laborOrderTypeFields.ShowUnit = value; } }
        public bool LaborShowUnitDiscountAmount { get { return laborOrderTypeFields.ShowUnitDiscountAmount; } set { laborOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool LaborShowUnitExtended { get { return laborOrderTypeFields.ShowUnitExtended; } set { laborOrderTypeFields.ShowUnitExtended = value; } }
        public bool LaborShowWeeklyDiscountAmount { get { return laborOrderTypeFields.ShowWeeklyDiscountAmount; } set { laborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool LaborShowWeeklyExtended { get { return laborOrderTypeFields.ShowWeeklyExtended; } set { laborOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool LaborShowMonthlyDiscountAmount { get { return laborOrderTypeFields.ShowMonthlyDiscountAmount; } set { laborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool LaborShowMonthlyExtended { get { return laborOrderTypeFields.ShowMonthlyExtended; } set { laborOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool LaborShowPeriodDiscountAmount { get { return laborOrderTypeFields.ShowPeriodDiscountAmount; } set { laborOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool LaborShowPeriodExtended { get { return laborOrderTypeFields.ShowPeriodExtended; } set { laborOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool LaborShowVariancePercent { get { return laborOrderTypeFields.ShowVariancePercent; } set { laborOrderTypeFields.ShowVariancePercent = value; } }
        //public bool LaborShowVarianceExtended { get { return laborOrderTypeFields.ShowVarianceExtended; } set { laborOrderTypeFields.ShowVarianceExtended = value; } }
        public bool LaborShowWarehouse { get { return laborOrderTypeFields.ShowWarehouse; } set { laborOrderTypeFields.ShowWarehouse = value; } }
        public bool LaborShowTaxable { get { return laborOrderTypeFields.ShowTaxable; } set { laborOrderTypeFields.ShowTaxable = value; } }
        public bool LaborShowNotes { get { return laborOrderTypeFields.ShowNotes; } set { laborOrderTypeFields.ShowNotes = value; } }
        //public bool LaborShowReturnToWarehouse { get { return laborOrderTypeFields.ShowReturnToWarehouse; } set { laborOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool LaborShowAvailableQuantityAllWarehouses { get { return laborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { laborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool LaborShowConflictDateAllWarehouses { get { return laborOrderTypeFields.ShowConflictDateAllWarehouses; } set { laborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool LaborShowConsignmentAvailableQuantity { get { return laborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { laborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool LaborShowConsignmentConflictDate { get { return laborOrderTypeFields.ShowConsignmentConflictDate; } set { laborOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool LaborShowConsignmentQuantity { get { return laborOrderTypeFields.ShowConsignmentQuantity; } set { laborOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool LaborShowInLocationQuantity { get { return laborOrderTypeFields.ShowInLocationQuantity; } set { laborOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool LaborShowReservedItems { get { return laborOrderTypeFields.ShowReservedItems; } set { laborOrderTypeFields.ShowReservedItems = value; } }
        //public bool LaborShowWeeksAndDays { get { return laborOrderTypeFields.ShowWeeksAndDays; } set { laborOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool LaborShowMonthsAndDays { get { return laborOrderTypeFields.ShowMonthsAndDays; } set { laborOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool LaborShowPremiumPercent { get { return laborOrderTypeFields.ShowPremiumPercent; } set { laborOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool LaborShowDepartment { get { return laborOrderTypeFields.ShowDepartment; } set { laborOrderTypeFields.ShowDepartment = value; } }
        //public bool LaborShowLocation { get { return laborOrderTypeFields.ShowLocation; } set { laborOrderTypeFields.ShowLocation = value; } }
        //public bool LaborShowSubOrderNumber { get { return laborOrderTypeFields.ShowSubOrderNumber; } set { laborOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool LaborShowOrderStatus { get { return laborOrderTypeFields.ShowOrderStatus; } set { laborOrderTypeFields.ShowOrderStatus = value; } }
        //public bool LaborShowEpisodes { get { return laborOrderTypeFields.ShowEpisodes; } set { laborOrderTypeFields.ShowEpisodes = value; } }
        //public bool LaborShowEpisodeExtended { get { return laborOrderTypeFields.ShowEpisodeExtended; } set { laborOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool LaborShowEpisodeDiscountAmount { get { return laborOrderTypeFields.ShowEpisodeDiscountAmount; } set { laborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string LaborDateStamp { get { return laborOrderTypeFields.DateStamp; } set { laborOrderTypeFields.DateStamp = value; } }
        public bool HideCrewBreaks { get { return orderType.Hidecrewbreaks; } set { orderType.Hidecrewbreaks = value; } }
        public bool Break1Paid { get { return orderType.Break1paId; } set { orderType.Break1paId = value; } }
        public bool Break2Paid { get { return orderType.Break2paId; } set { orderType.Break2paId = value; } }
        public bool Break3Paid { get { return orderType.Break3paId; } set { orderType.Break3paId = value; } }


        //misc fields
        public string MiscOrderTypeFieldsId { get { return miscOrderTypeFields.OrderTypeFieldsId; } set { miscOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool MiscShowOrderNumber { get { return miscOrderTypeFields.ShowOrderNumber; } set { miscOrderTypeFields.ShowOrderNumber = value; } }
        //public bool MiscShowRepairOrderNumber { get { return miscOrderTypeFields.ShowRepairOrderNumber; } set { miscOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool MiscShowICode { get { return miscOrderTypeFields.ShowICode; } set { miscOrderTypeFields.ShowICode = value; } }
        public int? MiscICodeWidth { get { return miscOrderTypeFields.ICodeWidth; } set { miscOrderTypeFields.ICodeWidth = value; } }
        public bool MiscShowDescription { get { return miscOrderTypeFields.ShowDescription; } set { miscOrderTypeFields.ShowDescription = value; } }
        public int? MiscDescriptionWidth { get { return miscOrderTypeFields.DescriptionWidth; } set { miscOrderTypeFields.DescriptionWidth = value; } }
        //public bool MiscShowPickDate { get { return miscOrderTypeFields.ShowPickDate; } set { miscOrderTypeFields.ShowPickDate = value; } }
        //public bool MiscShowPickTime { get { return miscOrderTypeFields.ShowPickTime; } set { miscOrderTypeFields.ShowPickTime = value; } }
        public bool MiscShowFromDate { get { return miscOrderTypeFields.ShowFromDate; } set { miscOrderTypeFields.ShowFromDate = value; } }
        public bool MiscShowFromTime { get { return miscOrderTypeFields.ShowFromTime; } set { miscOrderTypeFields.ShowFromTime = value; } }
        public bool MiscShowToDate { get { return miscOrderTypeFields.ShowToDate; } set { miscOrderTypeFields.ShowToDate = value; } }
        public bool MiscShowToTime { get { return miscOrderTypeFields.ShowToTime; } set { miscOrderTypeFields.ShowToTime = value; } }
        public bool MiscShowBillablePeriods { get { return miscOrderTypeFields.ShowBillablePeriods; } set { miscOrderTypeFields.ShowBillablePeriods = value; } }
        public bool MiscShowSubQuantity { get { return miscOrderTypeFields.ShowSubQuantity; } set { miscOrderTypeFields.ShowSubQuantity = value; } }
        public bool MiscShowWeeksAndDays { get { return miscOrderTypeFields.ShowWeeksAndDays; } set { miscOrderTypeFields.ShowWeeksAndDays = value; } }
        public bool MiscShowMonthsAndDays { get { return miscOrderTypeFields.ShowMonthsAndDays; } set { miscOrderTypeFields.ShowMonthsAndDays = value; } }

        //public bool MiscShowAvailableQuantity { get { return miscOrderTypeFields.ShowAvailableQuantity; } set { miscOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool MiscShowConflictDate { get { return miscOrderTypeFields.ShowConflictDate; } set { miscOrderTypeFields.ShowConflictDate = value; } }
        public bool MiscShowUnit { get { return miscOrderTypeFields.ShowUnit; } set { miscOrderTypeFields.ShowUnit = value; } }
        public bool MiscShowRate { get { return miscOrderTypeFields.ShowRate; } set { miscOrderTypeFields.ShowRate = value; } }
        public bool MiscShowCost { get { return miscOrderTypeFields.ShowCost; } set { miscOrderTypeFields.ShowCost = value; } }
        //public bool MiscShowWeeklyCostExtended { get { return miscOrderTypeFields.ShowWeeklyCostExtended; } set { miscOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool MiscShowMonthlyCostExtended { get { return miscOrderTypeFields.ShowMonthlyCostExtended; } set { miscOrderTypeFields.ShowMonthlyCostExtended = value; } }
        public bool MiscShowPeriodCostExtended { get { return miscOrderTypeFields.ShowPeriodCostExtended; } set { miscOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool MiscShowDaysPerWeek { get { return miscOrderTypeFields.ShowDaysPerWeek; } set { miscOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool MiscShowDiscountPercent { get { return miscOrderTypeFields.ShowDiscountPercent; } set { miscOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool MiscShowMarkupPercent { get { return miscOrderTypeFields.ShowMarkupPercent; } set { miscOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool MiscShowMarginPercent { get { return miscOrderTypeFields.ShowMarginPercent; } set { miscOrderTypeFields.ShowMarginPercent = value; } }
        public bool MiscShowUnitDiscountAmount { get { return miscOrderTypeFields.ShowUnitDiscountAmount; } set { miscOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool MiscShowUnitExtended { get { return miscOrderTypeFields.ShowUnitExtended; } set { miscOrderTypeFields.ShowUnitExtended = value; } }
        public bool MiscShowWeeklyDiscountAmount { get { return miscOrderTypeFields.ShowWeeklyDiscountAmount; } set { miscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool MiscShowWeeklyExtended { get { return miscOrderTypeFields.ShowWeeklyExtended; } set { miscOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool MiscShowMonthlyDiscountAmount { get { return miscOrderTypeFields.ShowMonthlyDiscountAmount; } set { miscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool MiscShowMonthlyExtended { get { return miscOrderTypeFields.ShowMonthlyExtended; } set { miscOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool MiscShowPeriodDiscountAmount { get { return miscOrderTypeFields.ShowPeriodDiscountAmount; } set { miscOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool MiscShowPeriodExtended { get { return miscOrderTypeFields.ShowPeriodExtended; } set { miscOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool MiscShowVariancePercent { get { return miscOrderTypeFields.ShowVariancePercent; } set { miscOrderTypeFields.ShowVariancePercent = value; } }
        //public bool MiscShowVarianceExtended { get { return miscOrderTypeFields.ShowVarianceExtended; } set { miscOrderTypeFields.ShowVarianceExtended = value; } }
        public bool MiscShowWarehouse { get { return miscOrderTypeFields.ShowWarehouse; } set { miscOrderTypeFields.ShowWarehouse = value; } }
        public bool MiscShowTaxable { get { return miscOrderTypeFields.ShowTaxable; } set { miscOrderTypeFields.ShowTaxable = value; } }
        public bool MiscShowNotes { get { return miscOrderTypeFields.ShowNotes; } set { miscOrderTypeFields.ShowNotes = value; } }
        public bool MiscShowReturnToWarehouse { get { return miscOrderTypeFields.ShowReturnToWarehouse; } set { miscOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool MiscShowVehicleNumber { get { return miscOrderTypeFields.ShowVehicleNumber; } set { miscOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool MiscShowBarCode { get { return miscOrderTypeFields.ShowBarCode; } set { miscOrderTypeFields.ShowBarCode = value; } }
        //public bool MiscShowSerialNumber { get { return miscOrderTypeFields.ShowSerialNumber; } set { miscOrderTypeFields.ShowSerialNumber = value; } }
        //public bool MiscShowCrewName { get { return miscOrderTypeFields.ShowCrewName; } set { miscOrderTypeFields.ShowCrewName = value; } }
        //public bool MiscShowHours { get { return miscOrderTypeFields.ShowHours; } set { miscOrderTypeFields.ShowHours = value; } }
        //public bool MiscShowAvailableQuantityAllWarehouses { get { return miscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { miscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool MiscShowConflictDateAllWarehouses { get { return miscOrderTypeFields.ShowConflictDateAllWarehouses; } set { miscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool MiscShowConsignmentAvailableQuantity { get { return miscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { miscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool MiscShowConsignmentConflictDate { get { return miscOrderTypeFields.ShowConsignmentConflictDate; } set { miscOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool MiscShowConsignmentQuantity { get { return miscOrderTypeFields.ShowConsignmentQuantity; } set { miscOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool MiscShowInLocationQuantity { get { return miscOrderTypeFields.ShowInLocationQuantity; } set { miscOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool MiscShowReservedItems { get { return miscOrderTypeFields.ShowReservedItems; } set { miscOrderTypeFields.ShowReservedItems = value; } }
        //public bool MiscShowPremiumPercent { get { return miscOrderTypeFields.ShowPremiumPercent; } set { miscOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool MiscShowDepartment { get { return miscOrderTypeFields.ShowDepartment; } set { miscOrderTypeFields.ShowDepartment = value; } }
        //public bool MiscShowLocation { get { return miscOrderTypeFields.ShowLocation; } set { miscOrderTypeFields.ShowLocation = value; } }
        //public bool MiscShowOrderActivity { get { return miscOrderTypeFields.ShowOrderActivity; } set { miscOrderTypeFields.ShowOrderActivity = value; } }
        //public bool MiscShowSubOrderNumber { get { return miscOrderTypeFields.ShowSubOrderNumber; } set { miscOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool MiscShowOrderStatus { get { return miscOrderTypeFields.ShowOrderStatus; } set { miscOrderTypeFields.ShowOrderStatus = value; } }
        //public bool MiscShowEpisodes { get { return miscOrderTypeFields.ShowEpisodes; } set { miscOrderTypeFields.ShowEpisodes = value; } }
        //public bool MiscShowEpisodeExtended { get { return miscOrderTypeFields.ShowEpisodeExtended; } set { miscOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool MiscShowEpisodeDiscountAmount { get { return miscOrderTypeFields.ShowEpisodeDiscountAmount; } set { miscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string MiscDateStamp { get { return miscOrderTypeFields.DateStamp; } set { miscOrderTypeFields.DateStamp = value; } }


        //rental sale
        public string RentalSaleOrderTypeFieldsId { get { return rentalSaleOrderTypeFields.OrderTypeFieldsId; } set { rentalSaleOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool RentalSaleShowOrderNumber { get { return rentalSaleOrderTypeFields.ShowOrderNumber; } set { rentalSaleOrderTypeFields.ShowOrderNumber = value; } }
        //public bool RentalSaleShowRepairOrderNumber { get { return rentalSaleOrderTypeFields.ShowRepairOrderNumber; } set { rentalSaleOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool RentalSaleShowBarCode { get { return rentalSaleOrderTypeFields.ShowBarCode; } set { rentalSaleOrderTypeFields.ShowBarCode = value; } }
        public bool RentalSaleShowSerialNumber { get { return rentalSaleOrderTypeFields.ShowSerialNumber; } set { rentalSaleOrderTypeFields.ShowSerialNumber = value; } }
        public bool RentalSaleShowICode { get { return rentalSaleOrderTypeFields.ShowICode; } set { rentalSaleOrderTypeFields.ShowICode = value; } }
        public int? RentalSaleICodeWidth { get { return rentalSaleOrderTypeFields.ICodeWidth; } set { rentalSaleOrderTypeFields.ICodeWidth = value; } }
        public bool RentalSaleShowDescription { get { return rentalSaleOrderTypeFields.ShowDescription; } set { rentalSaleOrderTypeFields.ShowDescription = value; } }
        public int? RentalSaleDescriptionWidth { get { return rentalSaleOrderTypeFields.DescriptionWidth; } set { rentalSaleOrderTypeFields.DescriptionWidth = value; } }
        public bool RentalSaleShowPickDate { get { return rentalSaleOrderTypeFields.ShowPickDate; } set { rentalSaleOrderTypeFields.ShowPickDate = value; } }
        public bool RentalSaleShowPickTime { get { return rentalSaleOrderTypeFields.ShowPickTime; } set { rentalSaleOrderTypeFields.ShowPickTime = value; } }
        //public bool RentalSaleShowFromDate { get { return rentalSaleOrderTypeFields.ShowFromDate; } set { rentalSaleOrderTypeFields.ShowFromDate = value; } }
        //public bool RentalSaleShowFromTime { get { return rentalSaleOrderTypeFields.ShowFromTime; } set { rentalSaleOrderTypeFields.ShowFromTime = value; } }
        //public bool RentalSaleShowToDate { get { return rentalSaleOrderTypeFields.ShowToDate; } set { rentalSaleOrderTypeFields.ShowToDate = value; } }
        //public bool RentalSaleShowToTime { get { return rentalSaleOrderTypeFields.ShowToTime; } set { rentalSaleOrderTypeFields.ShowToTime = value; } }
        //public bool RentalSaleShowBillablePeriods { get { return rentalSaleOrderTypeFields.ShowBillablePeriods; } set { rentalSaleOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool RentalSaleShowSubQuantity { get { return rentalSaleOrderTypeFields.ShowSubQuantity; } set { rentalSaleOrderTypeFields.ShowSubQuantity = value; } }
        public bool RentalSaleShowAvailableQuantity { get { return rentalSaleOrderTypeFields.ShowAvailableQuantity; } set { rentalSaleOrderTypeFields.ShowAvailableQuantity = value; } }
        public bool RentalSaleShowConflictDate { get { return rentalSaleOrderTypeFields.ShowConflictDate; } set { rentalSaleOrderTypeFields.ShowConflictDate = value; } }
        public bool RentalSaleShowUnit { get { return rentalSaleOrderTypeFields.ShowUnit; } set { rentalSaleOrderTypeFields.ShowUnit = value; } }
        public bool RentalSaleShowRate { get { return rentalSaleOrderTypeFields.ShowRate; } set { rentalSaleOrderTypeFields.ShowRate = value; } }
        public bool RentalSaleShowCost { get { return rentalSaleOrderTypeFields.ShowCost; } set { rentalSaleOrderTypeFields.ShowCost = value; } }


        //public bool RentalSaleShowWeeklyCostExtended { get { return rentalSaleOrderTypeFields.ShowWeeklyCostExtended; } set { rentalSaleOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool RentalSaleShowMonthlyCostExtended { get { return rentalSaleOrderTypeFields.ShowMonthlyCostExtended; } set { rentalSaleOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool RentalSaleShowPeriodCostExtended { get { return rentalSaleOrderTypeFields.ShowPeriodCostExtended; } set { rentalSaleOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool RentalSaleShowDaysPerWeek { get { return rentalSaleOrderTypeFields.ShowDaysPerWeek; } set { rentalSaleOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool RentalSaleShowDiscountPercent { get { return rentalSaleOrderTypeFields.ShowDiscountPercent; } set { rentalSaleOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool RentalSaleShowMarkupPercent { get { return rentalSaleOrderTypeFields.ShowMarkupPercent; } set { rentalSaleOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool RentalSaleShowMarginPercent { get { return rentalSaleOrderTypeFields.ShowMarginPercent; } set { rentalSaleOrderTypeFields.ShowMarginPercent = value; } }
        //public bool RentalSaleShowSplit { get { return rentalSaleOrderTypeFields.ShowSplit; } set { rentalSaleOrderTypeFields.ShowSplit = value; } }
        public bool RentalSaleShowUnitDiscountAmount { get { return rentalSaleOrderTypeFields.ShowUnitDiscountAmount; } set { rentalSaleOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool RentalSaleShowUnitExtended { get { return rentalSaleOrderTypeFields.ShowUnitExtended; } set { rentalSaleOrderTypeFields.ShowUnitExtended = value; } }
        //public bool RentalSaleShowWeeklyDiscountAmount { get { return rentalSaleOrderTypeFields.ShowWeeklyDiscountAmount; } set { rentalSaleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool RentalSaleShowWeeklyExtended { get { return rentalSaleOrderTypeFields.ShowWeeklyExtended; } set { rentalSaleOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool RentalSaleShowMonthlyDiscountAmount { get { return rentalSaleOrderTypeFields.ShowMonthlyDiscountAmount; } set { rentalSaleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool RentalSaleShowMonthlyExtended { get { return rentalSaleOrderTypeFields.ShowMonthlyExtended; } set { rentalSaleOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool RentalSaleShowPeriodDiscountAmount { get { return rentalSaleOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalSaleOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool RentalSaleShowPeriodExtended { get { return rentalSaleOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalSaleOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        //public bool RentalSaleShowVariancePercent { get { return rentalSaleOrderTypeFields.ShowVariancePercent; } set { rentalSaleOrderTypeFields.ShowVariancePercent = value; } }
        //public bool RentalSaleShowVarianceExtended { get { return rentalSaleOrderTypeFields.ShowVariancePercent; } set { rentalSaleOrderTypeFields.ShowVariancePercent = value; } }
        //public bool RentalSaleShowCountryOfOrigin { get { return rentalSaleOrderTypeFields.ShowCountryOfOrigin; } set { rentalSaleOrderTypeFields.ShowCountryOfOrigin = value; } }
        //public bool RentalSaleShowManufacturer { get { return rentalSaleOrderTypeFields.ShowManufacturer; } set { rentalSaleOrderTypeFields.ShowManufacturer = value; } }
        //public bool RentalSaleShowManufacturerPartNumber { get { return rentalSaleOrderTypeFields.ShowManufacturerPartNumber; } set { rentalSaleOrderTypeFields.ShowManufacturerPartNumber = value; } }
        //public int? RentalSaleManufacturerPartNumberWidth { get { return rentalSaleOrderTypeFields.ManufacturerPartNumberWidth; } set { rentalSaleOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        //public bool RentalSaleShowModelNumber { get { return rentalSaleOrderTypeFields.ShowModelNumber; } set { rentalSaleOrderTypeFields.ShowModelNumber = value; } }
        //public bool RentalSaleShowVendorPartNumber { get { return rentalSaleOrderTypeFields.ShowVendorPartNumber; } set { rentalSaleOrderTypeFields.ShowVendorPartNumber = value; } }
        public bool RentalSaleShowWarehouse { get { return rentalSaleOrderTypeFields.ShowWarehouse; } set { rentalSaleOrderTypeFields.ShowWarehouse = value; } }
        public bool RentalSaleShowTaxable { get { return rentalSaleOrderTypeFields.ShowTaxable; } set { rentalSaleOrderTypeFields.ShowTaxable = value; } }
        public bool RentalSaleShowNotes { get { return rentalSaleOrderTypeFields.ShowNotes; } set { rentalSaleOrderTypeFields.ShowNotes = value; } }
        //public bool RentalSaleShowReturnToWarehouse { get { return rentalSaleOrderTypeFields.ShowNotes; } set { rentalSaleOrderTypeFields.ShowNotes = value; } }
        //public bool RentalSaleShowVehicleNumber { get { return rentalSaleOrderTypeFields.ShowToTime; } set { rentalSaleOrderTypeFields.ShowToTime = value; } }
        //public bool RentalSaleShowCrewName { get { return rentalSaleOrderTypeFields.ShowCrewName; } set { rentalSaleOrderTypeFields.ShowCrewName = value; } }
        //public bool RentalSaleShowHours { get { return rentalSaleOrderTypeFields.ShowHours; } set { rentalSaleOrderTypeFields.ShowHours = value; } }
        //public bool RentalSaleShowAvailableQuantityAllWarehouses { get { return rentalSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { rentalSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool RentalSaleShowConflictDateAllWarehouses { get { return rentalSaleOrderTypeFields.ShowConflictDateAllWarehouses; } set { rentalSaleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool RentalSaleShowConsignmentAvailableQuantity { get { return rentalSaleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { rentalSaleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool RentalSaleShowConsignmentConflictDate { get { return rentalSaleOrderTypeFields.ShowConsignmentConflictDate; } set { rentalSaleOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool RentalSaleShowConsignmentQuantity { get { return rentalSaleOrderTypeFields.ShowConsignmentQuantity; } set { rentalSaleOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool RentalSaleShowInLocationQuantity { get { return rentalSaleOrderTypeFields.ShowInLocationQuantity; } set { rentalSaleOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool RentalSaleShowReservedItems { get { return rentalSaleOrderTypeFields.ShowReservedItems; } set { rentalSaleOrderTypeFields.ShowReservedItems = value; } }
        //public bool RentalSaleShowWeeksAndDays { get { return rentalSaleOrderTypeFields.ShowWeeksAndDays; } set { rentalSaleOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool RentalSaleShowMonthsAndDays { get { return rentalSaleOrderTypeFields.ShowMonthsAndDays; } set { rentalSaleOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool RentalSaleShowPremiumPercent { get { return rentalSaleOrderTypeFields.ShowPremiumPercent; } set { rentalSaleOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool RentalSaleShowDepartment { get { return rentalSaleOrderTypeFields.ShowDepartment; } set { rentalSaleOrderTypeFields.ShowDepartment = value; } }
        //public bool RentalSaleShowLocation { get { return rentalSaleOrderTypeFields.ShowLocation; } set { rentalSaleOrderTypeFields.ShowLocation = value; } }
        //public bool RentalSaleShowOrderActivity { get { return rentalSaleOrderTypeFields.ShowOrderActivity; } set { rentalSaleOrderTypeFields.ShowOrderActivity = value; } }
        //public bool RentalSaleShowSubOrderNumber { get { return rentalSaleOrderTypeFields.ShowSubOrderNumber; } set { rentalSaleOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool RentalSaleShowOrderStatus { get { return rentalSaleOrderTypeFields.ShowOrderStatus; } set { rentalSaleOrderTypeFields.ShowOrderStatus = value; } }
        //public bool RentalSaleShowEpisodes { get { return rentalSaleOrderTypeFields.ShowEpisodes; } set { rentalSaleOrderTypeFields.ShowEpisodes = value; } }
        //public bool RentalSaleShowEpisodeExtended { get { return rentalSaleOrderTypeFields.ShowEpisodeExtended; } set { rentalSaleOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool RentalSaleShowEpisodeDiscountAmount { get { return rentalSaleOrderTypeFields.ShowEpisodeDiscountAmount; } set { rentalSaleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string RentalSaleDateStamp { get { return rentalSaleOrderTypeFields.DateStamp; } set { rentalSaleOrderTypeFields.DateStamp = value; } }


        //finalld
        public string FinalLandDOrderTypeFieldsId { get { return finalLandDOrderTypeFields.OrderTypeFieldsId; } set { finalLandDOrderTypeFields.OrderTypeFieldsId = value; } }
        public bool FinalLandDShowOrderNumber { get { return finalLandDOrderTypeFields.ShowOrderNumber; } set { finalLandDOrderTypeFields.ShowOrderNumber = value; } }
        //public bool FinalLandDShowRepairOrderNumber { get { return finalLandDOrderTypeFields.ShowRepairOrderNumber; } set { finalLandDOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool FinalLandDShowBarCode { get { return finalLandDOrderTypeFields.ShowBarCode; } set { finalLandDOrderTypeFields.ShowBarCode = value; } }
        public bool FinalLandDShowSerialNumber { get { return finalLandDOrderTypeFields.ShowSerialNumber; } set { finalLandDOrderTypeFields.ShowSerialNumber = value; } }
        public bool FinalLandDShowICode { get { return finalLandDOrderTypeFields.ShowICode; } set { finalLandDOrderTypeFields.ShowICode = value; } }
        public int? FinalLandDICodeWidth { get { return finalLandDOrderTypeFields.ICodeWidth; } set { finalLandDOrderTypeFields.ICodeWidth = value; } }
        public bool FinalLandDShowDescription { get { return finalLandDOrderTypeFields.ShowDescription; } set { finalLandDOrderTypeFields.ShowDescription = value; } }
        public int? FinalLandDDescriptionWidth { get { return finalLandDOrderTypeFields.DescriptionWidth; } set { finalLandDOrderTypeFields.DescriptionWidth = value; } }
        //public bool FinalLandDShowPickDate { get { return finalLandDOrderTypeFields.ShowPickDate; } set { finalLandDOrderTypeFields.ShowPickDate = value; } }
        //public bool FinalLandDShowPickTime { get { return finalLandDOrderTypeFields.ShowPickTime; } set { finalLandDOrderTypeFields.ShowPickTime = value; } }
        //public bool FinalLandDShowFromDate { get { return finalLandDOrderTypeFields.ShowFromDate; } set { finalLandDOrderTypeFields.ShowFromDate = value; } }
        //public bool FinalLandDShowFromTime { get { return finalLandDOrderTypeFields.ShowFromTime; } set { finalLandDOrderTypeFields.ShowFromTime = value; } }
        //public bool FinalLandDShowToDate { get { return finalLandDOrderTypeFields.ShowToDate; } set { finalLandDOrderTypeFields.ShowToDate = value; } }
        //public bool FinalLandDShowToTime { get { return finalLandDOrderTypeFields.ShowToTime; } set { finalLandDOrderTypeFields.ShowToTime = value; } }
        //public bool FinalLandDShowBillablePeriods { get { return finalLandDOrderTypeFields.ShowBillablePeriods; } set { finalLandDOrderTypeFields.ShowBillablePeriods = value; } }
        ////public bool FinalLandDShowSubQuantity { get { return finalLandDOrderTypeFields.ShowSubQuantity; } set { finalLandDOrderTypeFields.ShowSubQuantity = value; } }
        //public bool FinalLandDShowAvailableQuantity { get { return finalLandDOrderTypeFields.ShowAvailableQuantity; } set { finalLandDOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool FinalLandDShowConflictDate { get { return finalLandDOrderTypeFields.ShowConflictDate; } set { finalLandDOrderTypeFields.ShowConflictDate = value; } }
        public bool FinalLandDShowUnit { get { return finalLandDOrderTypeFields.ShowUnit; } set { finalLandDOrderTypeFields.ShowUnit = value; } }
        public bool FinalLandDShowRate { get { return finalLandDOrderTypeFields.ShowRate; } set { finalLandDOrderTypeFields.ShowRate = value; } }
        public bool FinalLandDShowCost { get { return finalLandDOrderTypeFields.ShowCost; } set { finalLandDOrderTypeFields.ShowCost = value; } }
        //public bool FinalLandDShowWeeklyCostExtended { get { return finalLandDOrderTypeFields.ShowWeeklyCostExtended; } set { finalLandDOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool FinalLandDShowMonthlyCostExtended { get { return finalLandDOrderTypeFields.ShowMonthlyCostExtended; } set { finalLandDOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool FinalLandDShowPeriodCostExtended { get { return finalLandDOrderTypeFields.ShowPeriodCostExtended; } set { finalLandDOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool FinalLandDShowDaysPerWeek { get { return finalLandDOrderTypeFields.ShowDaysPerWeek; } set { finalLandDOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool FinalLandDShowDiscountPercent { get { return finalLandDOrderTypeFields.ShowDiscountPercent; } set { finalLandDOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool FinalLandDShowMarkupPercent { get { return finalLandDOrderTypeFields.ShowMarkupPercent; } set { finalLandDOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool FinalLandDShowMarginPercent { get { return finalLandDOrderTypeFields.ShowMarginPercent; } set { finalLandDOrderTypeFields.ShowMarginPercent = value; } }
        //public bool FinalLandDShowSplit { get { return finalLandDOrderTypeFields.ShowSplit; } set { finalLandDOrderTypeFields.ShowSplit = value; } }
        public bool FinalLandDShowUnitDiscountAmount { get { return finalLandDOrderTypeFields.ShowUnitDiscountAmount; } set { finalLandDOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool FinalLandDShowUnitExtended { get { return finalLandDOrderTypeFields.ShowUnitExtended; } set { finalLandDOrderTypeFields.ShowUnitExtended = value; } }
        //public bool FinalLandDShowWeeklyDiscountAmount { get { return finalLandDOrderTypeFields.ShowWeeklyDiscountAmount; } set { finalLandDOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool FinalLandDShowWeeklyExtended { get { return finalLandDOrderTypeFields.ShowWeeklyExtended; } set { finalLandDOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool FinalLandDShowMonthlyDiscountAmount { get { return finalLandDOrderTypeFields.ShowMonthlyDiscountAmount; } set { finalLandDOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool FinalLandDShowMonthlyExtended { get { return finalLandDOrderTypeFields.ShowMonthlyExtended; } set { finalLandDOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool FinalLandDShowPeriodDiscountAmount { get { return finalLandDOrderTypeFields.ShowPeriodDiscountAmount; } set { finalLandDOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool FinalLandDShowPeriodExtended { get { return finalLandDOrderTypeFields.ShowPeriodExtended; } set { finalLandDOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool FinalLandDShowVariancePercent { get { return finalLandDOrderTypeFields.ShowVariancePercent; } set { finalLandDOrderTypeFields.ShowVariancePercent = value; } }
        //public bool FinalLandDShowVarianceExtended { get { return finalLandDOrderTypeFields.ShowVarianceExtended; } set { finalLandDOrderTypeFields.ShowVarianceExtended = value; } }
        //public bool FinalLandDShowCountryOfOrigin { get { return finalLandDOrderTypeFields.ShowCountryOfOrigin; } set { finalLandDOrderTypeFields.ShowCountryOfOrigin = value; } }
        //public bool FinalLandDShowManufacturer { get { return finalLandDOrderTypeFields.ShowManufacturer; } set { finalLandDOrderTypeFields.ShowManufacturer = value; } }
        //public bool FinalLandDShowManufacturerPartNumber { get { return finalLandDOrderTypeFields.ShowManufacturerPartNumber; } set { finalLandDOrderTypeFields.ShowManufacturerPartNumber = value; } }
        //public int? FinalLandDManufacturerPartNumberWidth { get { return finalLandDOrderTypeFields.ManufacturerPartNumberWidth; } set { finalLandDOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        //public bool FinalLandDShowModelNumber { get { return finalLandDOrderTypeFields.ShowModelNumber; } set { finalLandDOrderTypeFields.ShowModelNumber = value; } }
        //public bool FinalLandDShowVendorPartNumber { get { return finalLandDOrderTypeFields.ShowVendorPartNumber; } set { finalLandDOrderTypeFields.ShowVendorPartNumber = value; } }
        public bool FinalLandDShowWarehouse { get { return finalLandDOrderTypeFields.ShowWarehouse; } set { finalLandDOrderTypeFields.ShowWarehouse = value; } }
        public bool FinalLandDShowTaxable { get { return finalLandDOrderTypeFields.ShowTaxable; } set { finalLandDOrderTypeFields.ShowTaxable = value; } }
        public bool FinalLandDShowNotes { get { return finalLandDOrderTypeFields.ShowNotes; } set { finalLandDOrderTypeFields.ShowNotes = value; } }
        //public bool FinalLandDShowReturnToWarehouse { get { return finalLandDOrderTypeFields.ShowReturnToWarehouse; } set { finalLandDOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool FinalLandDShowVehicleNumber { get { return finalLandDOrderTypeFields.ShowVehicleNumber; } set { finalLandDOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool FinalLandDShowCrewName { get { return finalLandDOrderTypeFields.ShowCrewName; } set { finalLandDOrderTypeFields.ShowCrewName = value; } }
        //public bool FinalLandDShowHours { get { return finalLandDOrderTypeFields.ShowHours; } set { finalLandDOrderTypeFields.ShowHours = value; } }
        //public bool FinalLandDShowAvailableQuantityAllWarehouses { get { return finalLandDOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { finalLandDOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool FinalLandDShowConflictDateAllWarehouses { get { return finalLandDOrderTypeFields.ShowConflictDateAllWarehouses; } set { finalLandDOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool FinalLandDShowConsignmentAvailableQuantity { get { return finalLandDOrderTypeFields.ShowConsignmentAvailableQuantity; } set { finalLandDOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool FinalLandDShowConsignmentConflictDate { get { return finalLandDOrderTypeFields.ShowConsignmentConflictDate; } set { finalLandDOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool FinalLandDShowConsignmentQuantity { get { return finalLandDOrderTypeFields.ShowConsignmentQuantity; } set { finalLandDOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool FinalLandDShowInLocationQuantity { get { return finalLandDOrderTypeFields.ShowInLocationQuantity; } set { finalLandDOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool FinalLandDShowReservedItems { get { return finalLandDOrderTypeFields.ShowReservedItems; } set { finalLandDOrderTypeFields.ShowReservedItems = value; } }
        //public bool FinalLandDShowWeeksAndDays { get { return finalLandDOrderTypeFields.ShowWeeksAndDays; } set { finalLandDOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool FinalLandDShowMonthsAndDays { get { return finalLandDOrderTypeFields.ShowMonthsAndDays; } set { finalLandDOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool FinalLandDShowPremiumPercent { get { return finalLandDOrderTypeFields.ShowPremiumPercent; } set { finalLandDOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool FinalLandDShowDepartment { get { return finalLandDOrderTypeFields.ShowDepartment; } set { finalLandDOrderTypeFields.ShowDepartment = value; } }
        //public bool FinalLandDShowLocation { get { return finalLandDOrderTypeFields.ShowLocation; } set { finalLandDOrderTypeFields.ShowLocation = value; } }
        //public bool FinalLandDShowOrderActivity { get { return finalLandDOrderTypeFields.ShowOrderActivity; } set { finalLandDOrderTypeFields.ShowOrderActivity = value; } }
        //public bool FinalLandDShowSubOrderNumber { get { return finalLandDOrderTypeFields.ShowSubOrderNumber; } set { finalLandDOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool FinalLandDShowOrderStatus { get { return finalLandDOrderTypeFields.ShowOrderStatus; } set { finalLandDOrderTypeFields.ShowOrderStatus = value; } }
        //public bool FinalLandDShowEpisodes { get { return finalLandDOrderTypeFields.ShowEpisodes; } set { finalLandDOrderTypeFields.ShowEpisodes = value; } }
        //public bool FinalLandDShowEpisodeExtended { get { return finalLandDOrderTypeFields.ShowEpisodeExtended; } set { finalLandDOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool FinalLandDShowEpisodeDiscountAmount { get { return finalLandDOrderTypeFields.ShowEpisodeDiscountAmount; } set { finalLandDOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string FinalLandDDateStamp { get { return finalLandDOrderTypeFields.DateStamp; } set { finalLandDOrderTypeFields.DateStamp = value; } }



        public bool AddInstallationAndStrikeFee { get { return orderType.Installstrikefee; } set { orderType.Installstrikefee = value; } }
        public string InstallationAndStrikeFeeRateId { get { return orderType.InstallstrikemasterId; } set { orderType.InstallstrikemasterId = value; } }
        public decimal? InstallationAndStrikeFeePercent { get { return orderType.Installstrikepct; } set { orderType.Installstrikepct = value; } }
        public string InstallationAndStrikeFeeBasedOn { get { return orderType.Installstrikebasedon; } set { orderType.Installstrikebasedon = value; } }
        public bool AddManagementAndServiceFee { get { return orderType.Managementservicefee; } set { orderType.Managementservicefee = value; } }
        public string ManagementAndServiceFeeRateId { get { return orderType.ManagementservicemasterId; } set { orderType.ManagementservicemasterId = value; } }
        public decimal? ManagementAndServiceFeePercent { get { return orderType.Managementservicepct; } set { orderType.Managementservicepct = value; } }
        public string ManagementAndServiceFeeBasedOn { get { return orderType.Managementservicebasedon; } set { orderType.Managementservicebasedon = value; } }


        public string DefaultUsedSalePrice { get { return orderType.Selectrentalsaleprice; } set { orderType.Selectrentalsaleprice = value; } }


        public bool QuikPayDiscount { get { return orderType.Quikpaydiscount; } set { orderType.Quikpaydiscount = value; } }
        public string QuikPayDiscountType { get { return orderType.Quikpaydiscounttype; } set { orderType.Quikpaydiscounttype = value; } }
        public int? QuikPayDiscountDays { get { return orderType.Quikpaydiscountdays; } set { orderType.Quikpaydiscountdays = value; } }
        public decimal? QuikPayDiscountPercent { get { return orderType.Quikpaydiscountpct; } set { orderType.Quikpaydiscountpct = value; } }
        public bool QuikPayDiscountExcludeSubs { get { return orderType.Quikpayexcludesubs; } set { orderType.Quikpayexcludesubs = value; } }


        public bool QuikConfirmDiscount { get { return orderType.Quikconfirmdiscount; } set { orderType.Quikconfirmdiscount = value; } }
        public decimal? QuikConfirmDiscountPercent { get { return orderType.Quikconfirmdiscountpct; } set { orderType.Quikconfirmdiscountpct = value; } }
        public int? QuikConfirmDiscountDays { get { return orderType.Quikconfirmdiscountdays; } set { orderType.Quikconfirmdiscountdays = value; } }


        public bool DisableCostGl { get { return orderType.Disablecostgl; } set { orderType.Disablecostgl = value; } }
        public bool ExcludeFromTopSalesDashboard { get { return orderType.Excludefromtopsales; } set { orderType.Excludefromtopsales = value; } }


        //public bool Combineactivitytabs { get { return orderType.Combineactivitytabs; } set { orderType.Combineactivitytabs = value; } }
        //public bool Combinetabseparateitems { get { return orderType.Combinetabseparateitems; } set { orderType.Combinetabseparateitems = value; } }
        //public string Suborderbillby { get { return orderType.Suborderbillby; } set { orderType.Suborderbillby = value; } }
        //public string Suborderavailabilityrule { get { return orderType.Suborderavailabilityrule; } set { orderType.Suborderavailabilityrule = value; } }
        //public string Suborderorderqty { get { return orderType.Suborderorderqty; } set { orderType.Suborderorderqty = value; } }
        //public string SuborderdefaultordertypeId { get { return orderType.SuborderdefaultordertypeId; } set { orderType.SuborderdefaultordertypeId = value; } }
        //public string SuborderordertypefieldsId { get { return orderType.SuborderordertypefieldsId; } set { orderType.SuborderordertypefieldsId = value; } }
        //public string Invoiceclass { get { return orderType.Invoiceclass; } set { orderType.Invoiceclass = value; } }

        public decimal? Orderby { get { return orderType.Orderby; } set { orderType.Orderby = value; } }
        public bool Inactive { get { return orderType.Inactive; } set { orderType.Inactive = value; } }
        public string DateStamp { get { return orderType.DateStamp; } set { orderType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}