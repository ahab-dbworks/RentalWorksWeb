using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.OrderType;
using WebApi.Modules.Settings.OrderTypeFields;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.EventType
{
    [FwLogic(Id:"IXvJH6dRpXWS")]
    public class EventTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord eventType = new OrderTypeRecord();
        OrderTypeFieldsRecord rentalOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord salesOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord laborOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord miscOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord spaceOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord lossAndDamageOrderTypeFields = new OrderTypeFieldsRecord();
        EventTypeLoader eventTypeLoader = new EventTypeLoader();
        EventTypeBrowseLoader eventTypeBrowseLoader = new EventTypeBrowseLoader();

        public EventTypeLogic()
        {
            dataRecords.Add(eventType);
            dataRecords.Add(rentalOrderTypeFields);
            dataRecords.Add(salesOrderTypeFields);
            dataRecords.Add(laborOrderTypeFields);
            dataRecords.Add(miscOrderTypeFields);
            dataRecords.Add(spaceOrderTypeFields);
            dataRecords.Add(lossAndDamageOrderTypeFields);
            dataLoader = eventTypeLoader;
            browseLoader = eventTypeBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"stvSwsmPFAiL", IsPrimaryKey:true)]
        public string EventTypeId { get { return eventType.OrderTypeId; } set { eventType.OrderTypeId = value; } }

        [FwLogicProperty(Id:"stvSwsmPFAiL", IsRecordTitle:true)]
        public string EventType { get { return eventType.OrderType; } set { eventType.OrderType = value; } }


        //rental fields
        [JsonIgnore]
        [FwLogicProperty(Id:"pquxmMyipqK")]
        public string RentalOrderTypeFieldsId { get { return rentalOrderTypeFields.OrderTypeFieldsId; } set { rentalOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"gCczVI3Lz3c")]
        //public bool? RentalShowOrderNumber { get { return rentalOrderTypeFields.ShowOrderNumber; } set { rentalOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"DJHBkyV6Yo9")]
        public bool? RentalShowICode { get { return rentalOrderTypeFields.ShowICode; } set { rentalOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"3Lep5CAalpP")]
        public int? RentalICodeWidth { get { return rentalOrderTypeFields.ICodeWidth; } set { rentalOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"VuPy3QYK7Pf")]
        public bool? RentalShowDescription { get { return rentalOrderTypeFields.ShowDescription; } set { rentalOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"StfcqAyXxyG")]
        public int? RentalDescriptionWidth { get { return rentalOrderTypeFields.DescriptionWidth; } set { rentalOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"KWRFuTrp4nq")]
        public bool? RentalShowPickDate { get { return rentalOrderTypeFields.ShowPickDate; } set { rentalOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"t6lYGRer6H1")]
        public bool? RentalShowPickTime { get { return rentalOrderTypeFields.ShowPickTime; } set { rentalOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"nwc0hkI6mNf")]
        public bool? RentalShowFromDate { get { return rentalOrderTypeFields.ShowFromDate; } set { rentalOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"6VhpdM2IZGb")]
        public bool? RentalShowFromTime { get { return rentalOrderTypeFields.ShowFromTime; } set { rentalOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"uoyiGGCgxiW")]
        public bool? RentalShowToDate { get { return rentalOrderTypeFields.ShowToDate; } set { rentalOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"u19jkNkMwhC")]
        public bool? RentalShowToTime { get { return rentalOrderTypeFields.ShowToTime; } set { rentalOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"m0QyQ3EtVpM")]
        public bool? RentalShowBillablePeriods { get { return rentalOrderTypeFields.ShowBillablePeriods; } set { rentalOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"24WBZz2BwbF")]
        public bool? RentalShowEpisodes { get { return rentalOrderTypeFields.ShowEpisodes; } set { rentalOrderTypeFields.ShowEpisodes = value; } }

        [FwLogicProperty(Id:"QU0fyNWqhq1")]
        public bool? RentalShowSubQuantity { get { return rentalOrderTypeFields.ShowSubQuantity; } set { rentalOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"Bg3G0kVJfKA")]
        public bool? RentalShowAvailableQuantity { get { return rentalOrderTypeFields.ShowAvailableQuantity; } set { rentalOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"QCOQySoSs6D")]
        public bool? RentalShowConflictDate { get { return rentalOrderTypeFields.ShowConflictDate; } set { rentalOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"6xkaXsybj8m")]
        public bool? RentalShowAvailableQuantityAllWarehouses { get { return rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { rentalOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        [FwLogicProperty(Id:"5GbVH8wO9RR")]
        public bool? RentalShowConflictDateAllWarehouses { get { return rentalOrderTypeFields.ShowConflictDateAllWarehouses; } set { rentalOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        [FwLogicProperty(Id:"Rh7PW9PtVFY")]
        public bool? RentalShowReservedItems { get { return rentalOrderTypeFields.ShowReservedItems; } set { rentalOrderTypeFields.ShowReservedItems = value; } }

        [FwLogicProperty(Id:"MLNTZoEKMUm")]
        public bool? RentalShowConsignmentQuantity { get { return rentalOrderTypeFields.ShowConsignmentQuantity; } set { rentalOrderTypeFields.ShowConsignmentQuantity = value; } }

        [FwLogicProperty(Id:"9EbsAQQd1ip")]
        public bool? RentalShowConsignmentAvailableQuantity { get { return rentalOrderTypeFields.ShowConsignmentAvailableQuantity; } set { rentalOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        [FwLogicProperty(Id:"QIvAV6Jwre8")]
        public bool? RentalShowConsignmentConflictDate { get { return rentalOrderTypeFields.ShowConsignmentConflictDate; } set { rentalOrderTypeFields.ShowConsignmentConflictDate = value; } }

        [FwLogicProperty(Id:"F5ANsDxLowW")]
        public bool? RentalShowRate { get { return rentalOrderTypeFields.ShowRate; } set { rentalOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"vbxMd06feaH")]
        public bool? RentalShowDaysPerWeek { get { return rentalOrderTypeFields.ShowDaysPerWeek; } set { rentalOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"9LdymAgck5T")]
        public bool? RentalShowPremiumPercent { get { return rentalOrderTypeFields.ShowPremiumPercent; } set { rentalOrderTypeFields.ShowPremiumPercent = value; } }

        [FwLogicProperty(Id:"RbQfGYzA2db")]
        public bool? RentalShowUnit { get { return rentalOrderTypeFields.ShowUnit; } set { rentalOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"ZrzFLZm33Xw")]
        public bool? RentalShowCost { get { return rentalOrderTypeFields.ShowCost; } set { rentalOrderTypeFields.ShowCost = value; } }


        //[FwLogicProperty(Id:"ohHaiXzDzNC")]
        //public bool? RentalShowWeeklyCostExtended { get { return rentalOrderTypeFields.ShowWeeklyCostExtended; } set { rentalOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"78VMVaZ4SXG")]
        //public bool? RentalShowMonthlyCostExtended { get { return rentalOrderTypeFields.ShowMonthlyCostExtended; } set { rentalOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"jrA6f7f565O")]
        //public bool? RentalShowPeriodCostExtended { get { return rentalOrderTypeFields.ShowPeriodCostExtended; } set { rentalOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"XgoxUbG4i8x")]
        public bool? RentalShowDiscountPercent { get { return rentalOrderTypeFields.ShowDiscountPercent; } set { rentalOrderTypeFields.ShowDiscountPercent = value; } }

        [FwLogicProperty(Id:"XFqcEwmlEcC")]
        public bool? RentalShowMarkupPercent { get { return rentalOrderTypeFields.ShowMarkupPercent; } set { rentalOrderTypeFields.ShowMarkupPercent = value; } }

        [FwLogicProperty(Id:"FVbXIwd3iKf")]
        public bool? RentalShowMarginPercent { get { return rentalOrderTypeFields.ShowMarginPercent; } set { rentalOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"bE84sCkUdq6")]
        public bool? RentalShowUnitDiscountAmount { get { return rentalOrderTypeFields.ShowUnitDiscountAmount; } set { rentalOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"RH0sWVtyQZ1")]
        public bool? RentalShowUnitExtended { get { return rentalOrderTypeFields.ShowUnitExtended; } set { rentalOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"GK9yimTr2U0")]
        public bool? RentalShowWeeklyDiscountAmount { get { return rentalOrderTypeFields.ShowWeeklyDiscountAmount; } set { rentalOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"rbUQPBB20Qo")]
        public bool? RentalShowWeeklyExtended { get { return rentalOrderTypeFields.ShowWeeklyExtended; } set { rentalOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"Kb8eEnGUChp")]
        public bool? RentalShowEpisodeExtended { get { return rentalOrderTypeFields.ShowEpisodeExtended; } set { rentalOrderTypeFields.ShowEpisodeExtended = value; } }

        [FwLogicProperty(Id:"biKgLvwhx3L")]
        public bool? RentalShowEpisodeDiscountAmount { get { return rentalOrderTypeFields.ShowEpisodeDiscountAmount; } set { rentalOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"QjjntW0wYaC")]
        public bool? RentalShowMonthlyDiscountAmount { get { return rentalOrderTypeFields.ShowMonthlyDiscountAmount; } set { rentalOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"H9VTMg2avgd")]
        public bool? RentalShowMonthlyExtended { get { return rentalOrderTypeFields.ShowMonthlyExtended; } set { rentalOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"LvObC3fSsPb")]
        public bool? RentalShowPeriodDiscountAmount { get { return rentalOrderTypeFields.ShowPeriodDiscountAmount; } set { rentalOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"RRplxVYmZD6")]
        public bool? RentalShowPeriodExtended { get { return rentalOrderTypeFields.ShowPeriodExtended; } set { rentalOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"ocxUoUHO1mq")]
        //public bool? RentalShowVariancePercent { get { return rentalOrderTypeFields.ShowVariancePercent; } set { rentalOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"vk98Qb08arF")]
        //public bool? RentalShowVarianceExtended { get { return rentalOrderTypeFields.ShowVarianceExtended; } set { rentalOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"XKuZHxpaewB")]
        public bool? RentalShowWarehouse { get { return rentalOrderTypeFields.ShowWarehouse; } set { rentalOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"Q8dq5ZX1CBt")]
        public bool? RentalShowTaxable { get { return rentalOrderTypeFields.ShowTaxable; } set { rentalOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"esZDQvDg3Sy")]
        public bool? RentalShowNotes { get { return rentalOrderTypeFields.ShowNotes; } set { rentalOrderTypeFields.ShowNotes = value; } }

        [FwLogicProperty(Id:"pWrdFtM4qRo")]
        public bool? RentalShowReturnToWarehouse { get { return rentalOrderTypeFields.ShowReturnToWarehouse; } set { rentalOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"I0qk9mM1uYW")]
        //public bool? RentalShowInLocationQuantity { get { return rentalOrderTypeFields.ShowInLocationQuantity; } set { rentalOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"4YTCpRruNW1")]
        //public bool? RentalShowWeeksAndDays { get { return rentalOrderTypeFields.ShowWeeksAndDays; } set { rentalOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"S9KM8cqeOsm")]
        //public bool? RentalShowMonthsAndDays { get { return rentalOrderTypeFields.ShowMonthsAndDays; } set { rentalOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"bZefnQ4T1y7")]
        //public bool? RentalShowDepartment { get { return rentalOrderTypeFields.ShowDepartment; } set { rentalOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"wAFGM5W6Qqz")]
        //public bool? RentalShowLocation { get { return rentalOrderTypeFields.ShowLocation; } set { rentalOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"aawQxIw4KFi")]
        //public bool? RentalShowOrderActivity { get { return rentalOrderTypeFields.ShowOrderActivity; } set { rentalOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"WUeUKXxtAzU")]
        //public bool? RentalShowSubOrderNumber { get { return rentalOrderTypeFields.ShowSubOrderNumber; } set { rentalOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"gnfdadicJVn")]
        //public bool? RentalShowOrderStatus { get { return rentalOrderTypeFields.ShowOrderStatus; } set { rentalOrderTypeFields.ShowOrderStatus = value; } }

        [FwLogicProperty(Id:"An73QmmI9Bq")]
        public string RentalDateStamp { get { return rentalOrderTypeFields.DateStamp; } set { rentalOrderTypeFields.DateStamp = value; } }



        //sales fields
        [JsonIgnore]
        [FwLogicProperty(Id:"hbLmJMvgNKF")]
        public string SalesOrderTypeFieldsId { get { return salesOrderTypeFields.OrderTypeFieldsId; } set { salesOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"hpKkaV0Weq6")]
        //public bool? SalesShowOrderNumber { get { return salesOrderTypeFields.ShowOrderNumber; } set { salesOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"bc1tokrKNmV")]
        public bool? SalesShowICode { get { return salesOrderTypeFields.ShowICode; } set { salesOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"faUMIxOkrt1")]
        public int? SalesICodeWidth { get { return salesOrderTypeFields.ICodeWidth; } set { salesOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"Ia7zxTCfXyj")]
        public bool? SalesShowDescription { get { return salesOrderTypeFields.ShowDescription; } set { salesOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"Vt7xumlFTf2")]
        public int? SalesDescriptionWidth { get { return salesOrderTypeFields.DescriptionWidth; } set { salesOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"6Pxk6vZSc7Y")]
        public bool? SalesShowManufacturerPartNumber { get { return salesOrderTypeFields.ShowManufacturerPartNumber; } set { salesOrderTypeFields.ShowManufacturerPartNumber = value; } }

        [FwLogicProperty(Id:"Vh9qP4k4WYp")]
        public int? SalesManufacturerPartNumberWidth { get { return salesOrderTypeFields.ManufacturerPartNumberWidth; } set { salesOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        [FwLogicProperty(Id:"u1nWe5cl86K")]
        public bool? SalesShowPickDate { get { return salesOrderTypeFields.ShowPickDate; } set { salesOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"aIXSbOzkLoj")]
        public bool? SalesShowPickTime { get { return salesOrderTypeFields.ShowPickTime; } set { salesOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"tJj1hHKC6yW")]
        //public bool? SalesShowFromDate { get { return salesOrderTypeFields.ShowFromDate; } set { salesOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"RU15BPc6UIz")]
        //public bool? SalesShowFromTime { get { return salesOrderTypeFields.ShowFromTime; } set { salesOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"6bbsAFtZVr8")]
        //public bool? SalesShowToDate { get { return salesOrderTypeFields.ShowToDate; } set { salesOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"xqdBsDe8tGr")]
        //public bool? SalesShowToTime { get { return salesOrderTypeFields.ShowToTime; } set { salesOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"kc1XlyxHgBe")]
        //public bool? SalesShowBillablePeriods { get { return salesOrderTypeFields.ShowBillablePeriods; } set { salesOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"2Psmb1NEHUa")]
        public bool? SalesShowSubQuantity { get { return salesOrderTypeFields.ShowSubQuantity; } set { salesOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"GOvy3vzTMkT")]
        public bool? SalesShowCost { get { return salesOrderTypeFields.ShowCost; } set { salesOrderTypeFields.ShowCost = value; } }

        [FwLogicProperty(Id:"1Rmwr4YC7UF")]
        public bool? SalesShowRate { get { return salesOrderTypeFields.ShowRate; } set { salesOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"YUCuifXbr8g")]
        public bool? SalesShowAvailableQuantity { get { return salesOrderTypeFields.ShowAvailableQuantity; } set { salesOrderTypeFields.ShowAvailableQuantity = value; } }

        [FwLogicProperty(Id:"RXFaohjxGsw")]
        public bool? SalesShowConflictDate { get { return salesOrderTypeFields.ShowConflictDate; } set { salesOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"u0DcYEKBpV9")]
        public bool? SalesShowAvailableQuantityAllWarehouses { get { return salesOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { salesOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        [FwLogicProperty(Id:"DFSwT298iVn")]
        public bool? SalesShowConflictDateAllWarehouses { get { return salesOrderTypeFields.ShowConflictDateAllWarehouses; } set { salesOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        [FwLogicProperty(Id:"gzozZSEmeaJ")]
        public bool? SalesShowMarkupPercent { get { return salesOrderTypeFields.ShowMarkupPercent; } set { salesOrderTypeFields.ShowMarkupPercent = value; } }

        [FwLogicProperty(Id:"qTz3jMjZARt")]
        public bool? SalesShowMarginPercent { get { return salesOrderTypeFields.ShowMarginPercent; } set { salesOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"xF8mGJmINDl")]
        public bool? SalesShowUnit { get { return salesOrderTypeFields.ShowUnit; } set { salesOrderTypeFields.ShowUnit = value; } }

        //[FwLogicProperty(Id:"RE8hzNd2IIm")]
        //public bool? SalesShowWeeklyCostExtended { get { return salesOrderTypeFields.ShowWeeklyCostExtended; } set { salesOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"WhAit6xinj5")]
        //public bool? SalesShowMonthlyCostExtended { get { return salesOrderTypeFields.ShowMonthlyCostExtended; } set { salesOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"gnnVtcHSa0K")]
        public bool? SalesShowPeriodCostExtended { get { return salesOrderTypeFields.ShowPeriodCostExtended; } set { salesOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"YngGZGVsZoC")]
        public bool? SalesShowDiscountPercent { get { return salesOrderTypeFields.ShowDiscountPercent; } set { salesOrderTypeFields.ShowDiscountPercent = value; } }

        [FwLogicProperty(Id:"XRBhkbSCp30")]
        public bool? SalesShowUnitDiscountAmount { get { return salesOrderTypeFields.ShowUnitDiscountAmount; } set { salesOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"zp3ulX0DQuo")]
        public bool? SalesShowUnitExtended { get { return salesOrderTypeFields.ShowUnitExtended; } set { salesOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"v9r2hEmlzy0")]
        //public bool? SalesShowWeeklyDiscountAmount { get { return salesOrderTypeFields.ShowWeeklyDiscountAmount; } set { salesOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"undUdZBSwji")]
        //public bool? SalesShowWeeklyExtended { get { return salesOrderTypeFields.ShowWeeklyExtended; } set { salesOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"YClpVqW3Z5J")]
        //public bool? SalesShowMonthlyDiscountAmount { get { return salesOrderTypeFields.ShowMonthlyDiscountAmount; } set { salesOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"hYcay2xIQAf")]
        //public bool? SalesShowMonthlyExtended { get { return salesOrderTypeFields.ShowMonthlyExtended; } set { salesOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"O1hBcwjEq5K")]
        public bool? SalesShowPeriodDiscountAmount { get { return salesOrderTypeFields.ShowPeriodDiscountAmount; } set { salesOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"Ax2cxXXCk2U")]
        public bool? SalesShowPeriodExtended { get { return salesOrderTypeFields.ShowPeriodExtended; } set { salesOrderTypeFields.ShowPeriodExtended = value; } }

        [FwLogicProperty(Id:"jc4JL5b2FH5")]
        public bool? SalesShowVariancePercent { get { return salesOrderTypeFields.ShowVariancePercent; } set { salesOrderTypeFields.ShowVariancePercent = value; } }

        [FwLogicProperty(Id:"9jAvZ30PFUh")]
        public bool? SalesShowVarianceExtended { get { return salesOrderTypeFields.ShowVarianceExtended; } set { salesOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"oOQqtIj1U6h")]
        public bool? SalesShowWarehouse { get { return salesOrderTypeFields.ShowWarehouse; } set { salesOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"dqGkZNmjEo6")]
        public bool? SalesShowTaxable { get { return salesOrderTypeFields.ShowTaxable; } set { salesOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"MLoBKUwrBQQ")]
        public bool? SalesShowNotes { get { return salesOrderTypeFields.ShowNotes; } set { salesOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"YoJ0TOBI2lO")]
        //public bool? SalesShowReturnToWarehouse { get { return salesOrderTypeFields.ShowReturnToWarehouse; } set { salesOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"PLcZmRsRrls")]
        //public bool? SalesShowConsigmentAvailableQuantity { get { return salesOrderTypeFields.ShowConsignmentAvailableQuantity; } set { salesOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"S4VSkflmoGV")]
        //public bool? SalesShowConsigmentConflictDate { get { return salesOrderTypeFields.ShowConsignmentConflictDate; } set { salesOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"gey6xSP0hI8")]
        //public bool? SalesShowConsignmentQuantity { get { return salesOrderTypeFields.ShowConsignmentQuantity; } set { salesOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"D2ClaxL7Gi3")]
        //public bool? SalesShowInLocationQuantity { get { return salesOrderTypeFields.ShowInLocationQuantity; } set { salesOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"Ck2nP4b0poW")]
        //public bool? SalesShowReservedItems { get { return salesOrderTypeFields.ShowReservedItems; } set { salesOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"puCvj1Yzn4zx")]
        //public bool? SalesShowWeeksAndDays { get { return salesOrderTypeFields.ShowWeeksAndDays; } set { salesOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"SCi5nUBJqDev")]
        //public bool? SalesShowMonthsAndDays { get { return salesOrderTypeFields.ShowMonthsAndDays; } set { salesOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"Wxm4wHIIpSVm")]
        //public bool? SalesShowPremiumPercent { get { return salesOrderTypeFields.ShowPremiumPercent; } set { salesOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"uzIvQ13MeMbj")]
        //public bool? SalesShowDepartment { get { return salesOrderTypeFields.ShowDepartment; } set { salesOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"FvTk4U63s0fe")]
        //public bool? SalesShowLocation { get { return salesOrderTypeFields.ShowLocation; } set { salesOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"gSrpAz4XRJU1")]
        //public bool? SalesShowOrderActivity { get { return salesOrderTypeFields.ShowOrderActivity; } set { salesOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"VZpKXRpDjK2V")]
        //public bool? SalesShowSubOrderNumber { get { return salesOrderTypeFields.ShowSubOrderNumber; } set { salesOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"gTy5OPtKF0M7")]
        //public bool? SalesShowOrderStatus { get { return salesOrderTypeFields.ShowOrderStatus; } set { salesOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"XCLnyFUf9dUu")]
        //public bool? SalesShowEpisodes { get { return salesOrderTypeFields.ShowEpisodes; } set { salesOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"IYn8cDyWM3Jz")]
        //public bool? SalesShowEpisodeExtended { get { return salesOrderTypeFields.ShowEpisodeExtended; } set { salesOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"OTNVSvlsqc7j")]
        //public bool? SalesShowEpisodeDiscountAmount { get { return salesOrderTypeFields.ShowEpisodeDiscountAmount; } set { salesOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"8lJbvbim0js6")]
        public string SalesDateStamp { get { return salesOrderTypeFields.DateStamp; } set { salesOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"YhfDygs0aG8C")]
        public string SalesInventoryPrice { get { return eventType.Selectsalesprice; } set { eventType.Selectsalesprice = value; } }

        [FwLogicProperty(Id:"kvRxUnvpLdyG")]
        public string SalesInventoryCost { get { return eventType.Selectsalescost; } set { eventType.Selectsalescost = value; } }


        //facilities fields
        [JsonIgnore]
        [FwLogicProperty(Id:"Ou6T1TswyEAD")]
        public string FacilityOrderTypeFieldsId { get { return spaceOrderTypeFields.OrderTypeFieldsId; } set { spaceOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"MQX4cm487Vk2")]
        //public bool? FacilityShowOrderNumber { get { return spaceOrderTypeFields.ShowOrderNumber; } set { spaceOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"0W0kdi7MnlAW")]
        //public bool? FacilityShowRepairOrderNumber { get { return spaceOrderTypeFields.ShowRepairOrderNumber; } set { spaceOrderTypeFields.ShowRepairOrderNumber = value; } }

        //[FwLogicProperty(Id:"nmgDO6ojSQpQ")]
        //public bool? FacilityShowICode { get { return spaceOrderTypeFields.ShowICode; } set { spaceOrderTypeFields.ShowICode = value; } }

        //[FwLogicProperty(Id:"WmsaEYbRgSII")]
        //public int? FacilityICodeWidth { get { return spaceOrderTypeFields.ICodeWidth; } set { spaceOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"tkTSkCSzBDt5")]
        public bool? FacilityShowDescription { get { return spaceOrderTypeFields.ShowDescription; } set { spaceOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"L1aIObCZiCjJ")]
        public int? FacilityDescriptionWidth { get { return spaceOrderTypeFields.DescriptionWidth; } set { spaceOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"Z1kxdWZc1nO6")]
        //public bool? FacilityShowPickDate { get { return spaceOrderTypeFields.ShowPickDate; } set { spaceOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"GROPKgXCPioR")]
        //public bool? FacilityShowPickTime { get { return spaceOrderTypeFields.ShowPickTime; } set { spaceOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"YRrs1sL22KrX")]
        public bool? FacilityShowFromDate { get { return spaceOrderTypeFields.ShowFromDate; } set { spaceOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"chEJUHPqmh5b")]
        public bool? FacilityShowFromTime { get { return spaceOrderTypeFields.ShowFromTime; } set { spaceOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"fBR8s6xeh0F2")]
        public bool? FacilityShowToDate { get { return spaceOrderTypeFields.ShowToDate; } set { spaceOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"wOnswvmfmmkN")]
        public bool? FacilityShowToTime { get { return spaceOrderTypeFields.ShowToTime; } set { spaceOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"kDfQOSdS71Q7")]
        public bool? FacilityShowWeeksAndDays { get { return spaceOrderTypeFields.ShowWeeksAndDays; } set { spaceOrderTypeFields.ShowWeeksAndDays = value; } }

        [FwLogicProperty(Id:"gtJQMX86Pxjh")]
        public bool? FacilityShowMonthsAndDays { get { return spaceOrderTypeFields.ShowMonthsAndDays; } set { spaceOrderTypeFields.ShowMonthsAndDays = value; } }

        [FwLogicProperty(Id:"Owk9cuhKWDBJ")]
        public bool? FacilityShowBillablePeriods { get { return spaceOrderTypeFields.ShowBillablePeriods; } set { spaceOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"FMk0K8zPxzZY")]
        //public bool? FacilityShowSubQuantity { get { return spaceOrderTypeFields.ShowSubQuantity; } set { spaceOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"pmTYKwxzqHrP")]
        //public bool? FacilityShowAvailableQuantity { get { return spaceOrderTypeFields.ShowAvailableQuantity; } set { spaceOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"ZzmpPnBt7wmU")]
        //public bool? FacilityShowConflictDate { get { return spaceOrderTypeFields.ShowConflictDate; } set { spaceOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"xsbcI35X1dJj")]
        public bool? FacilityShowRate { get { return spaceOrderTypeFields.ShowRate; } set { spaceOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"LEjecVqXYnY5")]
        //public bool? FacilityShowCost { get { return spaceOrderTypeFields.ShowCost; } set { spaceOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"GODcEKL1qyLy")]
        //public bool? FacilityShowWeeklyCostExtended { get { return spaceOrderTypeFields.ShowWeeklyCostExtended; } set { spaceOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"sJdtKlcn1oqw")]
        //public bool? FacilityShowMonthlyCostExtended { get { return spaceOrderTypeFields.ShowMonthlyCostExtended; } set { spaceOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"RMayonZ08eDF")]
        //public bool? FacilityShowPeriodCostExtended { get { return spaceOrderTypeFields.ShowPeriodCostExtended; } set { spaceOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"VFcAuSTiYZBH")]
        public bool? FacilityShowDaysPerWeek { get { return spaceOrderTypeFields.ShowDaysPerWeek; } set { spaceOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"6cURnqh7zJaX")]
        public bool? FacilityShowDiscountPercent { get { return spaceOrderTypeFields.ShowDiscountPercent; } set { spaceOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"EpAbPL8Wqlpr")]
        //public bool? FacilityShowMarkupPercent { get { return spaceOrderTypeFields.ShowMarkupPercent; } set { spaceOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"BNNNfYYWpTmf")]
        //public bool? FacilityShowMarginPercent { get { return spaceOrderTypeFields.ShowMarginPercent; } set { spaceOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"xLlUhV8WBy5V")]
        public bool? FacilityShowSplit { get { return spaceOrderTypeFields.ShowSplit; } set { spaceOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"Z45RiKNwQ0P6")]
        public bool? FacilityShowUnit { get { return spaceOrderTypeFields.ShowUnit; } set { spaceOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"kp4K62UJvdYa")]
        public bool? FacilityShowUnitDiscountAmount { get { return spaceOrderTypeFields.ShowUnitDiscountAmount; } set { spaceOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"QmldbTC897Vu")]
        public bool? FacilityShowUnitExtended { get { return spaceOrderTypeFields.ShowUnitExtended; } set { spaceOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"0hT1QiQhT2FY")]
        public bool? FacilityShowWeeklyDiscountAmount { get { return spaceOrderTypeFields.ShowWeeklyDiscountAmount; } set { spaceOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"TLhkJXARRoLo")]
        public bool? FacilityShowWeeklyExtended { get { return spaceOrderTypeFields.ShowWeeklyExtended; } set { spaceOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"RDxKBF82eJIE")]
        public bool? FacilityShowMonthlyDiscountAmount { get { return spaceOrderTypeFields.ShowMonthlyDiscountAmount; } set { spaceOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"cZN3S4FaHrl0")]
        public bool? FacilityShowMonthlyExtended { get { return spaceOrderTypeFields.ShowMonthlyExtended; } set { spaceOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"nstgIQKIP0Ud")]
        public bool? FacilityShowPeriodDiscountAmount { get { return spaceOrderTypeFields.ShowPeriodDiscountAmount; } set { spaceOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"VMC0yhMczRYD")]
        public bool? FacilityShowPeriodExtended { get { return spaceOrderTypeFields.ShowPeriodExtended; } set { spaceOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"SAQVvhYCIBd5")]
        //public bool? FacilityShowVariancePercent { get { return spaceOrderTypeFields.ShowVariancePercent; } set { spaceOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"Atn0fJXTQueR")]
        //public bool? FacilityShowVarianceExtended { get { return spaceOrderTypeFields.ShowVarianceExtended; } set { spaceOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"vDH5dK0Nh2OQ")]
        //public bool? FacilityShowCountryOfOrigin { get { return spaceOrderTypeFields.ShowCountryOfOrigin; } set { spaceOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"VQLOy6P8Qi4r")]
        //public bool? FacilityShowManufacturer { get { return spaceOrderTypeFields.ShowManufacturer; } set { spaceOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"1rd4oJGIkB8V")]
        //public bool? FacilityShowManufacturerPartNumber { get { return spaceOrderTypeFields.ShowManufacturerPartNumber; } set { spaceOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"xuKmOtYabayr")]
        //public int? FacilityManufacturerPartNumberWidth { get { return spaceOrderTypeFields.ManufacturerPartNumberWidth; } set { spaceOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"Yh7KULMeNic0")]
        //public bool? FacilityShowModelNumber { get { return spaceOrderTypeFields.ShowModelNumber; } set { spaceOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"TSfV7qxtgugW")]
        //public bool? FacilityShowVendorPartNumber { get { return spaceOrderTypeFields.ShowVendorPartNumber; } set { spaceOrderTypeFields.ShowVendorPartNumber = value; } }

        //[FwLogicProperty(Id:"nnniNqMt9ODw")]
        //public bool? FacilityShowWarehouse { get { return spaceOrderTypeFields.ShowWarehouse; } set { spaceOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"llBVSGPE48i2")]
        public bool? FacilityShowTaxable { get { return spaceOrderTypeFields.ShowTaxable; } set { spaceOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"7cAzmQELLzuI")]
        public bool? FacilityShowNotes { get { return spaceOrderTypeFields.ShowNotes; } set { spaceOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"P2ZesqGzsnFA")]
        //public bool? FacilityShowReturnToWarehouse { get { return spaceOrderTypeFields.ShowReturnToWarehouse; } set { spaceOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"ZnbeqieatDd4")]
        //public bool? FacilityShowVehicleNumber { get { return spaceOrderTypeFields.ShowVehicleNumber; } set { spaceOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"UPZQaRfV7OJA")]
        //public bool? FacilityShowBarCode { get { return spaceOrderTypeFields.ShowBarCode; } set { spaceOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"6eFJZHMynvlY")]
        //public bool? FacilityShowSerialNumber { get { return spaceOrderTypeFields.ShowSerialNumber; } set { spaceOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"4h5uloszvYOm")]
        //public bool? FacilityShowCrewName { get { return spaceOrderTypeFields.ShowCrewName; } set { spaceOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"stjk5amdoxdj")]
        //public bool? FacilityShowHours { get { return spaceOrderTypeFields.ShowHours; } set { spaceOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"59mRnQqj1Ocj")]
        //public bool? FacilityShowAvailableQuantityAllWarehouses { get { return spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { spaceOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"IC7P67wTu3D7")]
        //public bool? FacilityShowConflictDateAllWarehouses { get { return spaceOrderTypeFields.ShowConflictDateAllWarehouses; } set { spaceOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"dqOx6pPa2sII")]
        //public bool? FacilityShowConsignmentAvailableQuantity { get { return spaceOrderTypeFields.ShowConsignmentAvailableQuantity; } set { spaceOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"TPVSQNi7FMl0")]
        //public bool? FacilityShowConsignmentConflictDate { get { return spaceOrderTypeFields.ShowConsignmentConflictDate; } set { spaceOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"85cM4Gp4jSp8")]
        //public bool? FacilityShowConsignmentQuantity { get { return spaceOrderTypeFields.ShowConsignmentQuantity; } set { spaceOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"HH1HuDvN4jtd")]
        //public bool? FacilityShowInLocationQuantity { get { return spaceOrderTypeFields.ShowInLocationQuantity; } set { spaceOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"FfCYXVaEUpJS")]
        //public bool? FacilityShowReservedItems { get { return spaceOrderTypeFields.ShowReservedItems; } set { spaceOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"vFf5MxExMHqr")]
        //public bool? FacilityShowPremiumPercent { get { return spaceOrderTypeFields.ShowPremiumPercent; } set { spaceOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"dxRFkRnrkfP9")]
        //public bool? FacilityShowDepartment { get { return spaceOrderTypeFields.ShowDepartment; } set { spaceOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"vGaWKdNUXP3m")]
        //public bool? FacilityShowLocation { get { return spaceOrderTypeFields.ShowLocation; } set { spaceOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"wikIVoj1OJtp")]
        //public bool? FacilityShowOrderActivity { get { return spaceOrderTypeFields.ShowOrderActivity; } set { spaceOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"nnYfDf8x32X8")]
        //public bool? FacilityShowSubOrderNumber { get { return spaceOrderTypeFields.ShowSubOrderNumber; } set { spaceOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"MyBqgVugKaox")]
        //public bool? FacilityShowOrderStatus { get { return spaceOrderTypeFields.ShowOrderStatus; } set { spaceOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"bkOmZExKIp8H")]
        //public bool? FacilityShowEpisodes { get { return spaceOrderTypeFields.ShowEpisodes; } set { spaceOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"ExqIDJqZPTIk")]
        //public bool? FacilityShowEpisodeExtended { get { return spaceOrderTypeFields.ShowEpisodeExtended; } set { spaceOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"jMczeQ3yPOgs")]
        //public bool? FacilityShowEpisodeDiscountAmount { get { return spaceOrderTypeFields.ShowEpisodeDiscountAmount; } set { spaceOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"7KldaCl0FN64")]
        public string FacilityDateStamp { get { return spaceOrderTypeFields.DateStamp; } set { spaceOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"anhdzkaHgZgB")]
        public string FacilityDescription { get { return eventType.Spacedescription; } set { eventType.Spacedescription = value; } }



        //labor/crew fields
        [JsonIgnore]
        [FwLogicProperty(Id:"zfko4EafV6EV")]
        public string LaborOrderTypeFieldsId { get { return laborOrderTypeFields.OrderTypeFieldsId; } set { laborOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"uOOFMiYbWlRk")]
        //public bool? LaborShowOrderNumber { get { return laborOrderTypeFields.ShowOrderNumber; } set { laborOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"tGV7b4HuLjXT")]
        //public bool? LaborShowRepairOrderNumber { get { return laborOrderTypeFields.ShowRepairOrderNumber; } set { laborOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"jIJ7fKULEuqv")]
        public bool? LaborShowICode { get { return laborOrderTypeFields.ShowICode; } set { laborOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"G0RiaesePtV9")]
        public int? LaborICodeWidth { get { return laborOrderTypeFields.ICodeWidth; } set { laborOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"ivAhF2pvOonp")]
        public bool? LaborShowDescription { get { return laborOrderTypeFields.ShowDescription; } set { laborOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"ouS5Ie7bXukg")]
        public int? LaborDescriptionWidth { get { return laborOrderTypeFields.DescriptionWidth; } set { laborOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"54CJPskDuWHC")]
        public bool? LaborShowOrderActivity { get { return laborOrderTypeFields.ShowOrderActivity; } set { laborOrderTypeFields.ShowOrderActivity = value; } }

        [FwLogicProperty(Id:"GgVqSetakpy3")]
        public bool? LaborShowCrewName { get { return laborOrderTypeFields.ShowCrewName; } set { laborOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"MgHNZKerSBOG")]
        //public bool? LaborShowPickDate { get { return laborOrderTypeFields.ShowPickDate; } set { laborOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"52qUmr7UF2Xr")]
        //public bool? LaborShowPickTime { get { return laborOrderTypeFields.ShowPickTime; } set { laborOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"suvT2SDULm5d")]
        public bool? LaborShowFromDate { get { return laborOrderTypeFields.ShowFromDate; } set { laborOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"8IlshVkwpfcZ")]
        public bool? LaborShowFromTime { get { return laborOrderTypeFields.ShowFromTime; } set { laborOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"cUJP69m5JEy4")]
        public bool? LaborShowToDate { get { return laborOrderTypeFields.ShowToDate; } set { laborOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"o5fXdgjt5J8u")]
        public bool? LaborShowToTime { get { return laborOrderTypeFields.ShowToTime; } set { laborOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"BCzWEiuRkjcN")]
        public bool? LaborShowBillablePeriods { get { return laborOrderTypeFields.ShowBillablePeriods; } set { laborOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"2RqkOPJfEz4o")]
        public bool? LaborShowHours { get { return laborOrderTypeFields.ShowHours; } set { laborOrderTypeFields.ShowHours = value; } }

        [FwLogicProperty(Id:"DZFxlZHLfg9X")]
        public bool? LaborShowSubQuantity { get { return laborOrderTypeFields.ShowSubQuantity; } set { laborOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"qctZuDxPD19m")]
        //public bool? LaborShowAvailableQuantity { get { return laborOrderTypeFields.ShowAvailableQuantity; } set { laborOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"ZGSJTTC2NEjh")]
        //public bool? LaborShowConflictDate { get { return laborOrderTypeFields.ShowConflictDate; } set { laborOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"JlGdc06h682e")]
        public bool? LaborShowCost { get { return laborOrderTypeFields.ShowCost; } set { laborOrderTypeFields.ShowCost = value; } }

        [FwLogicProperty(Id:"1xfHqmcSIwi9")]
        public bool? LaborShowRate { get { return laborOrderTypeFields.ShowRate; } set { laborOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"mModVeAfGD1Y")]
        //public bool? LaborShowWeeklyCostExtended { get { return laborOrderTypeFields.ShowWeeklyCostExtended; } set { laborOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"3M60U8TJv9gO")]
        //public bool? LaborShowMonthlyCostExtended { get { return laborOrderTypeFields.ShowMonthlyCostExtended; } set { laborOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"u2iRq7Vzyf14")]
        public bool? LaborShowPeriodCostExtended { get { return laborOrderTypeFields.ShowPeriodCostExtended; } set { laborOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"bgh1snzW1vNR")]
        //public bool? LaborShowDaysPerWeek { get { return laborOrderTypeFields.ShowDaysPerWeek; } set { laborOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"TGsMFsMTbUXc")]
        public bool? LaborShowDiscountPercent { get { return laborOrderTypeFields.ShowDiscountPercent; } set { laborOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"QkPyAUUWVp1w")]
        //public bool? LaborShowMarkupPercent { get { return laborOrderTypeFields.ShowMarkupPercent; } set { laborOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"PGDQCc8mCSAC")]
        //public bool? LaborShowMarginPercent { get { return laborOrderTypeFields.ShowMarginPercent; } set { laborOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"xxvZPPE3njZv")]
        public bool? LaborShowUnit { get { return laborOrderTypeFields.ShowUnit; } set { laborOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"pYEhpHrts2pJ")]
        public bool? LaborShowUnitDiscountAmount { get { return laborOrderTypeFields.ShowUnitDiscountAmount; } set { laborOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"gsS2TZPPgnlW")]
        public bool? LaborShowUnitExtended { get { return laborOrderTypeFields.ShowUnitExtended; } set { laborOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"wGccqBE1bqKR")]
        public bool? LaborShowWeeklyDiscountAmount { get { return laborOrderTypeFields.ShowWeeklyDiscountAmount; } set { laborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"MNcrkRafx1aM")]
        public bool? LaborShowWeeklyExtended { get { return laborOrderTypeFields.ShowWeeklyExtended; } set { laborOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"lOXC7QkN3gLR")]
        public bool? LaborShowMonthlyDiscountAmount { get { return laborOrderTypeFields.ShowMonthlyDiscountAmount; } set { laborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"mvUHCqr1l091")]
        public bool? LaborShowMonthlyExtended { get { return laborOrderTypeFields.ShowMonthlyExtended; } set { laborOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"1liDFCl625jI")]
        public bool? LaborShowPeriodDiscountAmount { get { return laborOrderTypeFields.ShowPeriodDiscountAmount; } set { laborOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"yH73vFrAKo3r")]
        public bool? LaborShowPeriodExtended { get { return laborOrderTypeFields.ShowPeriodExtended; } set { laborOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"CkqoHNVKqFBL")]
        //public bool? LaborShowVariancePercent { get { return laborOrderTypeFields.ShowVariancePercent; } set { laborOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"JhnY3yrzzaik")]
        //public bool? LaborShowVarianceExtended { get { return laborOrderTypeFields.ShowVarianceExtended; } set { laborOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"Bkrrn5chcvIZ")]
        public bool? LaborShowWarehouse { get { return laborOrderTypeFields.ShowWarehouse; } set { laborOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"m3rCYXHvuqV8")]
        public bool? LaborShowTaxable { get { return laborOrderTypeFields.ShowTaxable; } set { laborOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"gaJFKivJ2OI5")]
        public bool? LaborShowNotes { get { return laborOrderTypeFields.ShowNotes; } set { laborOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"c046bePIAZAq")]
        //public bool? LaborShowReturnToWarehouse { get { return laborOrderTypeFields.ShowReturnToWarehouse; } set { laborOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"htgPtxxKDZG3")]
        //public bool? LaborShowAvailableQuantityAllWarehouses { get { return laborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { laborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"OCjomslg7tlU")]
        //public bool? LaborShowConflictDateAllWarehouses { get { return laborOrderTypeFields.ShowConflictDateAllWarehouses; } set { laborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"wTZAl9kupUlU")]
        //public bool? LaborShowConsignmentAvailableQuantity { get { return laborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { laborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"aL5mrasY6biE")]
        //public bool? LaborShowConsignmentConflictDate { get { return laborOrderTypeFields.ShowConsignmentConflictDate; } set { laborOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"gkVsvpArDKCn")]
        //public bool? LaborShowConsignmentQuantity { get { return laborOrderTypeFields.ShowConsignmentQuantity; } set { laborOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"j16cmptX4CNV")]
        //public bool? LaborShowInLocationQuantity { get { return laborOrderTypeFields.ShowInLocationQuantity; } set { laborOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"DxmjEyC2Lhwy")]
        //public bool? LaborShowReservedItems { get { return laborOrderTypeFields.ShowReservedItems; } set { laborOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"vqGRjBwSJV5p")]
        //public bool? LaborShowWeeksAndDays { get { return laborOrderTypeFields.ShowWeeksAndDays; } set { laborOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"FFZM1H9F2HjN")]
        //public bool? LaborShowMonthsAndDays { get { return laborOrderTypeFields.ShowMonthsAndDays; } set { laborOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"baYbm6mdYxdE")]
        //public bool? LaborShowPremiumPercent { get { return laborOrderTypeFields.ShowPremiumPercent; } set { laborOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"Osi1zq2iM4cI")]
        //public bool? LaborShowDepartment { get { return laborOrderTypeFields.ShowDepartment; } set { laborOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"pQiA3L2B4yiL")]
        //public bool? LaborShowLocation { get { return laborOrderTypeFields.ShowLocation; } set { laborOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"Es0JOAizb5Td")]
        //public bool? LaborShowSubOrderNumber { get { return laborOrderTypeFields.ShowSubOrderNumber; } set { laborOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"W9AWVVAvHATR")]
        //public bool? LaborShowOrderStatus { get { return laborOrderTypeFields.ShowOrderStatus; } set { laborOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"Jz0r6cwoX2j5")]
        //public bool? LaborShowEpisodes { get { return laborOrderTypeFields.ShowEpisodes; } set { laborOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"zzvJihYkh61C")]
        //public bool? LaborShowEpisodeExtended { get { return laborOrderTypeFields.ShowEpisodeExtended; } set { laborOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"iq6i8lXpzVfO")]
        //public bool? LaborShowEpisodeDiscountAmount { get { return laborOrderTypeFields.ShowEpisodeDiscountAmount; } set { laborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"IrBfEO7qFZ5E")]
        public string LaborDateStamp { get { return laborOrderTypeFields.DateStamp; } set { laborOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"K0oSiHZgfNKr")]
        public bool? HideCrewBreaks { get { return eventType.Hidecrewbreaks; } set { eventType.Hidecrewbreaks = value; } }

        [FwLogicProperty(Id:"3jNfyMiujeQ1")]
        public bool? Break1Paid { get { return eventType.Break1paId; } set { eventType.Break1paId = value; } }

        [FwLogicProperty(Id:"QeAdKgjndajZ")]
        public bool? Break2Paid { get { return eventType.Break2paId; } set { eventType.Break2paId = value; } }

        [FwLogicProperty(Id:"GCT3WM3LII35")]
        public bool? Break3Paid { get { return eventType.Break3paId; } set { eventType.Break3paId = value; } }



        //misc fields
        [JsonIgnore]
        [FwLogicProperty(Id:"IkUIJpMqXHSp")]
        public string MiscOrderTypeFieldsId { get { return miscOrderTypeFields.OrderTypeFieldsId; } set { miscOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"ONWK0RLslMqD")]
        //public bool? MiscShowOrderNumber { get { return miscOrderTypeFields.ShowOrderNumber; } set { miscOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"FpC0l5pMRDO0")]
        //public bool? MiscShowRepairOrderNumber { get { return miscOrderTypeFields.ShowRepairOrderNumber; } set { miscOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"UBORor84aFgQ")]
        public bool? MiscShowICode { get { return miscOrderTypeFields.ShowICode; } set { miscOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"VDq5vVL7HVVR")]
        public int? MiscICodeWidth { get { return miscOrderTypeFields.ICodeWidth; } set { miscOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"QemdaJSwtAb1")]
        public bool? MiscShowDescription { get { return miscOrderTypeFields.ShowDescription; } set { miscOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"SLCs4pHikkwD")]
        public int? MiscDescriptionWidth { get { return miscOrderTypeFields.DescriptionWidth; } set { miscOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"Bcuij7luSM6c")]
        //public bool? MiscShowPickDate { get { return miscOrderTypeFields.ShowPickDate; } set { miscOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"RfOKu3lSFWJ8")]
        //public bool? MiscShowPickTime { get { return miscOrderTypeFields.ShowPickTime; } set { miscOrderTypeFields.ShowPickTime = value; } }

        [FwLogicProperty(Id:"Iqi3OepcZalG")]
        public bool? MiscShowFromDate { get { return miscOrderTypeFields.ShowFromDate; } set { miscOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"vNKXln9pNHOx")]
        public bool? MiscShowFromTime { get { return miscOrderTypeFields.ShowFromTime; } set { miscOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"OdkDQi3inVzt")]
        public bool? MiscShowToDate { get { return miscOrderTypeFields.ShowToDate; } set { miscOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"GlPvlh9KvBKb")]
        public bool? MiscShowToTime { get { return miscOrderTypeFields.ShowToTime; } set { miscOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"9oVWxy6Cg86N")]
        public bool? MiscShowBillablePeriods { get { return miscOrderTypeFields.ShowBillablePeriods; } set { miscOrderTypeFields.ShowBillablePeriods = value; } }

        [FwLogicProperty(Id:"eFKbbPICo6we")]
        public bool? MiscShowSubQuantity { get { return miscOrderTypeFields.ShowSubQuantity; } set { miscOrderTypeFields.ShowSubQuantity = value; } }

        [FwLogicProperty(Id:"I6NU6COcpTcv")]
        public bool? MiscShowWeeksAndDays { get { return miscOrderTypeFields.ShowWeeksAndDays; } set { miscOrderTypeFields.ShowWeeksAndDays = value; } }

        [FwLogicProperty(Id:"Y1KxNadUqpkO")]
        public bool? MiscShowMonthsAndDays { get { return miscOrderTypeFields.ShowMonthsAndDays; } set { miscOrderTypeFields.ShowMonthsAndDays = value; } }


        //[FwLogicProperty(Id:"EkCPa9fcA4lu")]
        //public bool? MiscShowAvailableQuantity { get { return miscOrderTypeFields.ShowAvailableQuantity; } set { miscOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"hnu4O39bMdjU")]
        //public bool? MiscShowConflictDate { get { return miscOrderTypeFields.ShowConflictDate; } set { miscOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"YJes1MeMm7Si")]
        public bool? MiscShowUnit { get { return miscOrderTypeFields.ShowUnit; } set { miscOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"vQFmq4X7uaSp")]
        public bool? MiscShowRate { get { return miscOrderTypeFields.ShowRate; } set { miscOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"VozZwEy7pqQi")]
        public bool? MiscShowCost { get { return miscOrderTypeFields.ShowCost; } set { miscOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"L1cicFWaUJhN")]
        //public bool? MiscShowWeeklyCostExtended { get { return miscOrderTypeFields.ShowWeeklyCostExtended; } set { miscOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"8mhAJrXr9XcE")]
        //public bool? MiscShowMonthlyCostExtended { get { return miscOrderTypeFields.ShowMonthlyCostExtended; } set { miscOrderTypeFields.ShowMonthlyCostExtended = value; } }

        [FwLogicProperty(Id:"YRJFNK6vh5g5")]
        public bool? MiscShowPeriodCostExtended { get { return miscOrderTypeFields.ShowPeriodCostExtended; } set { miscOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"gYYONcuCW0mP")]
        //public bool? MiscShowDaysPerWeek { get { return miscOrderTypeFields.ShowDaysPerWeek; } set { miscOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"n1D22cxWGH3u")]
        public bool? MiscShowDiscountPercent { get { return miscOrderTypeFields.ShowDiscountPercent; } set { miscOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"BhMFmhFfzZeP")]
        //public bool? MiscShowMarkupPercent { get { return miscOrderTypeFields.ShowMarkupPercent; } set { miscOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"gv8XELiJOQ0J")]
        //public bool? MiscShowMarginPercent { get { return miscOrderTypeFields.ShowMarginPercent; } set { miscOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"zsipWDbsJn1g")]
        public bool? MiscShowUnitDiscountAmount { get { return miscOrderTypeFields.ShowUnitDiscountAmount; } set { miscOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"ZoRWgkeOGFRd")]
        public bool? MiscShowUnitExtended { get { return miscOrderTypeFields.ShowUnitExtended; } set { miscOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"wcAyNlIPylnI")]
        public bool? MiscShowWeeklyDiscountAmount { get { return miscOrderTypeFields.ShowWeeklyDiscountAmount; } set { miscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"YckK6Oq6E5Jv")]
        public bool? MiscShowWeeklyExtended { get { return miscOrderTypeFields.ShowWeeklyExtended; } set { miscOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"beqrhNMX9Zbs")]
        public bool? MiscShowMonthlyDiscountAmount { get { return miscOrderTypeFields.ShowMonthlyDiscountAmount; } set { miscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"FJl73BsF8Xer")]
        public bool? MiscShowMonthlyExtended { get { return miscOrderTypeFields.ShowMonthlyExtended; } set { miscOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"iy0v7zKPUyFN")]
        public bool? MiscShowPeriodDiscountAmount { get { return miscOrderTypeFields.ShowPeriodDiscountAmount; } set { miscOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"CvWxGFUqGEXE")]
        public bool? MiscShowPeriodExtended { get { return miscOrderTypeFields.ShowPeriodExtended; } set { miscOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"tocbxRLQlilx")]
        //public bool? MiscShowVariancePercent { get { return miscOrderTypeFields.ShowVariancePercent; } set { miscOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"jkPyrKvwRkqn")]
        //public bool? MiscShowVarianceExtended { get { return miscOrderTypeFields.ShowVarianceExtended; } set { miscOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"zOqPtTP3vv69")]
        public bool? MiscShowWarehouse { get { return miscOrderTypeFields.ShowWarehouse; } set { miscOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"4XRfp8KqqLbV")]
        public bool? MiscShowTaxable { get { return miscOrderTypeFields.ShowTaxable; } set { miscOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"NGmftwcAjnMi")]
        public bool? MiscShowNotes { get { return miscOrderTypeFields.ShowNotes; } set { miscOrderTypeFields.ShowNotes = value; } }

        [FwLogicProperty(Id:"CCGpncmntDqt")]
        public bool? MiscShowReturnToWarehouse { get { return miscOrderTypeFields.ShowReturnToWarehouse; } set { miscOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"TU19wvRbuUuk")]
        //public bool? MiscShowVehicleNumber { get { return miscOrderTypeFields.ShowVehicleNumber; } set { miscOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"PXzLx901Jrxp")]
        //public bool? MiscShowBarCode { get { return miscOrderTypeFields.ShowBarCode; } set { miscOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"kVqRDxwK99vi")]
        //public bool? MiscShowSerialNumber { get { return miscOrderTypeFields.ShowSerialNumber; } set { miscOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"M5oCsm1LH1j3")]
        //public bool? MiscShowCrewName { get { return miscOrderTypeFields.ShowCrewName; } set { miscOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"qSyUSxrPRu8L")]
        //public bool? MiscShowHours { get { return miscOrderTypeFields.ShowHours; } set { miscOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"Hx8jU2XI6Map")]
        //public bool? MiscShowAvailableQuantityAllWarehouses { get { return miscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { miscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"EPFjEO8hG1z3")]
        //public bool? MiscShowConflictDateAllWarehouses { get { return miscOrderTypeFields.ShowConflictDateAllWarehouses; } set { miscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"i6vm8HLgddrP")]
        //public bool? MiscShowConsignmentAvailableQuantity { get { return miscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { miscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"wTKx4U2llr4e")]
        //public bool? MiscShowConsignmentConflictDate { get { return miscOrderTypeFields.ShowConsignmentConflictDate; } set { miscOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"GiXPWDMxLeIc")]
        //public bool? MiscShowConsignmentQuantity { get { return miscOrderTypeFields.ShowConsignmentQuantity; } set { miscOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"up4jOxUkfPXD")]
        //public bool? MiscShowInLocationQuantity { get { return miscOrderTypeFields.ShowInLocationQuantity; } set { miscOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"OswftkC7yJnI")]
        //public bool? MiscShowReservedItems { get { return miscOrderTypeFields.ShowReservedItems; } set { miscOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"5Dtd5JSeWI9Y")]
        //public bool? MiscShowPremiumPercent { get { return miscOrderTypeFields.ShowPremiumPercent; } set { miscOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"tNtlPGO4RsSA")]
        //public bool? MiscShowDepartment { get { return miscOrderTypeFields.ShowDepartment; } set { miscOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"Skr3nT0Gs2Wn")]
        //public bool? MiscShowLocation { get { return miscOrderTypeFields.ShowLocation; } set { miscOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"T7UhO0y4t7K2")]
        //public bool? MiscShowOrderActivity { get { return miscOrderTypeFields.ShowOrderActivity; } set { miscOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"kxt66YxZj7ov")]
        //public bool? MiscShowSubOrderNumber { get { return miscOrderTypeFields.ShowSubOrderNumber; } set { miscOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"HAkpEo01044f")]
        //public bool? MiscShowOrderStatus { get { return miscOrderTypeFields.ShowOrderStatus; } set { miscOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"ZVTaMmGJsvPX")]
        //public bool? MiscShowEpisodes { get { return miscOrderTypeFields.ShowEpisodes; } set { miscOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"g7ffYEBrQxoT")]
        //public bool? MiscShowEpisodeExtended { get { return miscOrderTypeFields.ShowEpisodeExtended; } set { miscOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"Kazn0GscJG7h")]
        //public bool? MiscShowEpisodeDiscountAmount { get { return miscOrderTypeFields.ShowEpisodeDiscountAmount; } set { miscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"0qYLtC53xYu0")]
        public string MiscDateStamp { get { return miscOrderTypeFields.DateStamp; } set { miscOrderTypeFields.DateStamp = value; } }


        //finalld
        [JsonIgnore]
        [FwLogicProperty(Id:"usecIiGpd0kG")]
        public string LossAndDamageOrderTypeFieldsId { get { return lossAndDamageOrderTypeFields.OrderTypeFieldsId; } set { lossAndDamageOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"PXop9xJCyfML")]
        public bool? LossAndDamageShowOrderNumber { get { return lossAndDamageOrderTypeFields.ShowOrderNumber; } set { lossAndDamageOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"m2Uu59kS0vgo")]
        //public bool? LossAndDamageShowRepairOrderNumber { get { return lossAndDamageOrderTypeFields.ShowRepairOrderNumber; } set { lossAndDamageOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"aqlcTgetbsKn")]
        public bool? LossAndDamageShowBarCode { get { return lossAndDamageOrderTypeFields.ShowBarCode; } set { lossAndDamageOrderTypeFields.ShowBarCode = value; } }

        [FwLogicProperty(Id:"BVJcorhbeMLn")]
        public bool? LossAndDamageShowSerialNumber { get { return lossAndDamageOrderTypeFields.ShowSerialNumber; } set { lossAndDamageOrderTypeFields.ShowSerialNumber = value; } }

        [FwLogicProperty(Id:"FBiH1vT5ryUd")]
        public bool? LossAndDamageShowICode { get { return lossAndDamageOrderTypeFields.ShowICode; } set { lossAndDamageOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"jOpgXfs4Zj5R")]
        public int? LossAndDamageICodeWidth { get { return lossAndDamageOrderTypeFields.ICodeWidth; } set { lossAndDamageOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"94AAGfMW3KvS")]
        public bool? LossAndDamageShowDescription { get { return lossAndDamageOrderTypeFields.ShowDescription; } set { lossAndDamageOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"5vibQ1x57kMW")]
        public int? LossAndDamageDescriptionWidth { get { return lossAndDamageOrderTypeFields.DescriptionWidth; } set { lossAndDamageOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"t1knSncng4Jw")]
        //public bool? LossAndDamageShowPickDate { get { return lossAndDamageOrderTypeFields.ShowPickDate; } set { lossAndDamageOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"6RfPmVPtwx6C")]
        //public bool? LossAndDamageShowPickTime { get { return lossAndDamageOrderTypeFields.ShowPickTime; } set { lossAndDamageOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"fNgw17Wr77qG")]
        //public bool? LossAndDamageShowFromDate { get { return lossAndDamageOrderTypeFields.ShowFromDate; } set { lossAndDamageOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"D6gpbNDW3uH0")]
        //public bool? LossAndDamageShowFromTime { get { return lossAndDamageOrderTypeFields.ShowFromTime; } set { lossAndDamageOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"3fWWUR8OE01R")]
        //public bool? LossAndDamageShowToDate { get { return lossAndDamageOrderTypeFields.ShowToDate; } set { lossAndDamageOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"CLQbZ157PHp1")]
        //public bool? LossAndDamageShowToTime { get { return lossAndDamageOrderTypeFields.ShowToTime; } set { lossAndDamageOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"F8CT2YP1Y0sR")]
        //public bool? LossAndDamageShowBillablePeriods { get { return lossAndDamageOrderTypeFields.ShowBillablePeriods; } set { lossAndDamageOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"9qCzZ9VKeh0m")]
        ////public bool? LossAndDamageShowSubQuantity { get { return lossAndDamageOrderTypeFields.ShowSubQuantity; } set { lossAndDamageOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"A1qlGOxLk0Au")]
        //public bool? LossAndDamageShowAvailableQuantity { get { return lossAndDamageOrderTypeFields.ShowAvailableQuantity; } set { lossAndDamageOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"bcHOhcWsrHLR")]
        //public bool? LossAndDamageShowConflictDate { get { return lossAndDamageOrderTypeFields.ShowConflictDate; } set { lossAndDamageOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"isTGIWqGe8UU")]
        public bool? LossAndDamageShowUnit { get { return lossAndDamageOrderTypeFields.ShowUnit; } set { lossAndDamageOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"y167JD4koPUH")]
        public bool? LossAndDamageShowRate { get { return lossAndDamageOrderTypeFields.ShowRate; } set { lossAndDamageOrderTypeFields.ShowRate = value; } }

        [FwLogicProperty(Id:"DAyGAt9rwAWg")]
        public bool? LossAndDamageShowCost { get { return lossAndDamageOrderTypeFields.ShowCost; } set { lossAndDamageOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"CCJBHmdyNGIJ")]
        //public bool? LossAndDamageShowWeeklyCostExtended { get { return lossAndDamageOrderTypeFields.ShowWeeklyCostExtended; } set { lossAndDamageOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"lBbRqrSKRXWJ")]
        //public bool? LossAndDamageShowMonthlyCostExtended { get { return lossAndDamageOrderTypeFields.ShowMonthlyCostExtended; } set { lossAndDamageOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"CpKGxHIkCY79")]
        //public bool? LossAndDamageShowPeriodCostExtended { get { return lossAndDamageOrderTypeFields.ShowPeriodCostExtended; } set { lossAndDamageOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"YMshPNhSPRMC")]
        //public bool? LossAndDamageShowDaysPerWeek { get { return lossAndDamageOrderTypeFields.ShowDaysPerWeek; } set { lossAndDamageOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"MBm3lpaIDl7q")]
        public bool? LossAndDamageShowDiscountPercent { get { return lossAndDamageOrderTypeFields.ShowDiscountPercent; } set { lossAndDamageOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"C9jKrPUNif35")]
        //public bool? LossAndDamageShowMarkupPercent { get { return lossAndDamageOrderTypeFields.ShowMarkupPercent; } set { lossAndDamageOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"MHSq9xaedJzQ")]
        //public bool? LossAndDamageShowMarginPercent { get { return lossAndDamageOrderTypeFields.ShowMarginPercent; } set { lossAndDamageOrderTypeFields.ShowMarginPercent = value; } }

        //[FwLogicProperty(Id:"kqA2odY0oIGC")]
        //public bool? LossAndDamageShowSplit { get { return lossAndDamageOrderTypeFields.ShowSplit; } set { lossAndDamageOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"8hvwrbop19Kc")]
        public bool? LossAndDamageShowUnitDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowUnitDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"h98fg5TPt3go")]
        public bool? LossAndDamageShowUnitExtended { get { return lossAndDamageOrderTypeFields.ShowUnitExtended; } set { lossAndDamageOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"KJ48Q2GfDAav")]
        //public bool? LossAndDamageShowWeeklyDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowWeeklyDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"yFjrG7kmsvFO")]
        //public bool? LossAndDamageShowWeeklyExtended { get { return lossAndDamageOrderTypeFields.ShowWeeklyExtended; } set { lossAndDamageOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"TW6dFhw6z7X3")]
        //public bool? LossAndDamageShowMonthlyDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowMonthlyDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"fUoW26J8Ajfj")]
        //public bool? LossAndDamageShowMonthlyExtended { get { return lossAndDamageOrderTypeFields.ShowMonthlyExtended; } set { lossAndDamageOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"86dDUmhhw4Ay")]
        public bool? LossAndDamageShowPeriodDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowPeriodDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"TofoWwA6kXcI")]
        public bool? LossAndDamageShowPeriodExtended { get { return lossAndDamageOrderTypeFields.ShowPeriodExtended; } set { lossAndDamageOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"vfb8xa8QQSoA")]
        //public bool? LossAndDamageShowVariancePercent { get { return lossAndDamageOrderTypeFields.ShowVariancePercent; } set { lossAndDamageOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"sbYRE5qZleYo")]
        //public bool? LossAndDamageShowVarianceExtended { get { return lossAndDamageOrderTypeFields.ShowVarianceExtended; } set { lossAndDamageOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"D2VyYmXAWqYl")]
        //public bool? LossAndDamageShowCountryOfOrigin { get { return lossAndDamageOrderTypeFields.ShowCountryOfOrigin; } set { lossAndDamageOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"vpPAQt8bCl4M")]
        //public bool? LossAndDamageShowManufacturer { get { return lossAndDamageOrderTypeFields.ShowManufacturer; } set { lossAndDamageOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"TxBkyPCY75R1")]
        //public bool? LossAndDamageShowManufacturerPartNumber { get { return lossAndDamageOrderTypeFields.ShowManufacturerPartNumber; } set { lossAndDamageOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"Sq9fUX2YZ3dw")]
        //public int? LossAndDamageManufacturerPartNumberWidth { get { return lossAndDamageOrderTypeFields.ManufacturerPartNumberWidth; } set { lossAndDamageOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"ecDH9IjQEOD7")]
        //public bool? LossAndDamageShowModelNumber { get { return lossAndDamageOrderTypeFields.ShowModelNumber; } set { lossAndDamageOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"jToiyEzfXak8")]
        //public bool? LossAndDamageShowVendorPartNumber { get { return lossAndDamageOrderTypeFields.ShowVendorPartNumber; } set { lossAndDamageOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"azpQeFemHe1S")]
        public bool? LossAndDamageShowWarehouse { get { return lossAndDamageOrderTypeFields.ShowWarehouse; } set { lossAndDamageOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"7Pyz6tJXpaDX")]
        public bool? LossAndDamageShowTaxable { get { return lossAndDamageOrderTypeFields.ShowTaxable; } set { lossAndDamageOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"XNUlRmiywH27")]
        public bool? LossAndDamageShowNotes { get { return lossAndDamageOrderTypeFields.ShowNotes; } set { lossAndDamageOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"8YzpewAZptjB")]
        //public bool? LossAndDamageShowReturnToWarehouse { get { return lossAndDamageOrderTypeFields.ShowReturnToWarehouse; } set { lossAndDamageOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"zNYcgzeTV8g2")]
        //public bool? LossAndDamageShowVehicleNumber { get { return lossAndDamageOrderTypeFields.ShowVehicleNumber; } set { lossAndDamageOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"wdkYZrBq9Mel")]
        //public bool? LossAndDamageShowCrewName { get { return lossAndDamageOrderTypeFields.ShowCrewName; } set { lossAndDamageOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"IjRxiw7xDTeU")]
        //public bool? LossAndDamageShowHours { get { return lossAndDamageOrderTypeFields.ShowHours; } set { lossAndDamageOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"Qg6ds39s7icA")]
        //public bool? LossAndDamageShowAvailableQuantityAllWarehouses { get { return lossAndDamageOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { lossAndDamageOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"zgO1c7tFt2nx")]
        //public bool? LossAndDamageShowConflictDateAllWarehouses { get { return lossAndDamageOrderTypeFields.ShowConflictDateAllWarehouses; } set { lossAndDamageOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"j0RPvQWBy07G")]
        //public bool? LossAndDamageShowConsignmentAvailableQuantity { get { return lossAndDamageOrderTypeFields.ShowConsignmentAvailableQuantity; } set { lossAndDamageOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"HIZNMMMUDqMN")]
        //public bool? LossAndDamageShowConsignmentConflictDate { get { return lossAndDamageOrderTypeFields.ShowConsignmentConflictDate; } set { lossAndDamageOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"NULd2CP9n9pj")]
        //public bool? LossAndDamageShowConsignmentQuantity { get { return lossAndDamageOrderTypeFields.ShowConsignmentQuantity; } set { lossAndDamageOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"JfKjdLq42T9X")]
        //public bool? LossAndDamageShowInLocationQuantity { get { return lossAndDamageOrderTypeFields.ShowInLocationQuantity; } set { lossAndDamageOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"qqvCNgpuTgTi")]
        //public bool? LossAndDamageShowReservedItems { get { return lossAndDamageOrderTypeFields.ShowReservedItems; } set { lossAndDamageOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"awcurvfhTBky")]
        //public bool? LossAndDamageShowWeeksAndDays { get { return lossAndDamageOrderTypeFields.ShowWeeksAndDays; } set { lossAndDamageOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"fDAi55k2FiPP")]
        //public bool? LossAndDamageShowMonthsAndDays { get { return lossAndDamageOrderTypeFields.ShowMonthsAndDays; } set { lossAndDamageOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"RIsfRtnPlSXa")]
        //public bool? LossAndDamageShowPremiumPercent { get { return lossAndDamageOrderTypeFields.ShowPremiumPercent; } set { lossAndDamageOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"3bjlbIxClDPv")]
        //public bool? LossAndDamageShowDepartment { get { return lossAndDamageOrderTypeFields.ShowDepartment; } set { lossAndDamageOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"vknnc7papMN0")]
        //public bool? LossAndDamageShowLocation { get { return lossAndDamageOrderTypeFields.ShowLocation; } set { lossAndDamageOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"rC1U6235LJaf")]
        //public bool? LossAndDamageShowOrderActivity { get { return lossAndDamageOrderTypeFields.ShowOrderActivity; } set { lossAndDamageOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"lTIK0IzQvFdQ")]
        //public bool? LossAndDamageShowSubOrderNumber { get { return lossAndDamageOrderTypeFields.ShowSubOrderNumber; } set { lossAndDamageOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"3PWpytmHIySz")]
        //public bool? LossAndDamageShowOrderStatus { get { return lossAndDamageOrderTypeFields.ShowOrderStatus; } set { lossAndDamageOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"CycKju8OTDxC")]
        //public bool? LossAndDamageShowEpisodes { get { return lossAndDamageOrderTypeFields.ShowEpisodes; } set { lossAndDamageOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"MhX1kAvTbCLB")]
        //public bool? LossAndDamageShowEpisodeExtended { get { return lossAndDamageOrderTypeFields.ShowEpisodeExtended; } set { lossAndDamageOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"PbB9pYjG7iFV")]
        //public bool? LossAndDamageShowEpisodeDiscountAmount { get { return lossAndDamageOrderTypeFields.ShowEpisodeDiscountAmount; } set { lossAndDamageOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"SCuMJFbwgXVl")]
        public string LossAndDamageDateStamp { get { return lossAndDamageOrderTypeFields.DateStamp; } set { lossAndDamageOrderTypeFields.DateStamp = value; } }





        [JsonIgnore]
        [FwLogicProperty(Id:"MmK19jC9fBkv")]
        public string OrdType { get { return eventType.Ordtype; } set { eventType.Ordtype = value; } }

        [FwLogicProperty(Id:"P8xO4ZT4Val9")]
        public decimal? OrderBy { get { return eventType.Orderby; } set { eventType.Orderby = value; } }

        [FwLogicProperty(Id:"qWjmKk4voklu")]
        public bool? Inactive { get { return eventType.Inactive; } set { eventType.Inactive = value; } }

        [FwLogicProperty(Id:"t5ucTfMniDfJ")]
        public string DateStamp { get { return eventType.DateStamp; } set { eventType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            OrdType = "EVENT";
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    EventTypeLogic orig = ((EventTypeLogic)e.Original);
                    rentalOrderTypeFields.OrderTypeFieldsId = orig.RentalOrderTypeFieldsId;
                    salesOrderTypeFields.OrderTypeFieldsId = orig.SalesOrderTypeFieldsId;
                    laborOrderTypeFields.OrderTypeFieldsId = orig.LaborOrderTypeFieldsId;
                    miscOrderTypeFields.OrderTypeFieldsId = orig.MiscOrderTypeFieldsId;
                    spaceOrderTypeFields.OrderTypeFieldsId = orig.FacilityOrderTypeFieldsId;
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
                LossAndDamageOrderTypeFieldsId = lossAndDamageOrderTypeFields.OrderTypeFieldsId;
                int i = SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------   
    }
}
