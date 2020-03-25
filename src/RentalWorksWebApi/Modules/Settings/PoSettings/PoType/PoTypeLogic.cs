using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi.Modules.Settings.OrderSettings.OrderType;
using WebApi.Modules.Settings.OrderTypeFields;

namespace WebApi.Modules.Settings.PoSettings.PoType
{
    [FwLogic(Id:"ZDZ8gE8Nvt4sA")]
    public class PoTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord poType = new OrderTypeRecord();
        OrderTypeFieldsRecord purchaseOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord subRentalOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord subSaleOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord laborOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord subLaborOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord miscOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord subMiscOrderTypeFields = new OrderTypeFieldsRecord();
        OrderTypeFieldsRecord repairOrderTypeFields = new OrderTypeFieldsRecord();
        PoTypeLoader poTypeLoader = new PoTypeLoader();
        PoTypeBrowseLoader poTypeBrowseLoader = new PoTypeBrowseLoader();

        public PoTypeLogic()
        {
            dataRecords.Add(poType);
            dataRecords.Add(purchaseOrderTypeFields);
            dataRecords.Add(subRentalOrderTypeFields);
            dataRecords.Add(subSaleOrderTypeFields);
            dataRecords.Add(laborOrderTypeFields);
            dataRecords.Add(subLaborOrderTypeFields);
            dataRecords.Add(miscOrderTypeFields);
            dataRecords.Add(subMiscOrderTypeFields);
            dataRecords.Add(repairOrderTypeFields);
            dataLoader = poTypeLoader;
            browseLoader = poTypeBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"lwWCT8bfJGbUS", IsPrimaryKey:true)]
        public string PoTypeId { get { return poType.OrderTypeId; } set { poType.OrderTypeId = value; } }

        [FwLogicProperty(Id:"lwWCT8bfJGbUS", IsRecordTitle:true)]
        public string PoType { get { return poType.OrderType; } set { poType.OrderType = value; } }


        [FwLogicProperty(Id:"lnNT0p1AgT0N")]
        public bool? ApprovalNeededByRequired { get { return poType.Poapprovebyrequired; } set { poType.Poapprovebyrequired = value; } }

        [FwLogicProperty(Id:"wfV0miGx2Pcu")]
        public bool? ImportanceRequired { get { return poType.Poimportancerequired; } set { poType.Poimportancerequired = value; } }

        [FwLogicProperty(Id:"FY8X4lv5kkv4")]
        public bool? PayTypeRequired { get { return poType.Popaytyperequired; } set { poType.Popaytyperequired = value; } }

        [FwLogicProperty(Id:"2ff2sn1G7HWY")]
        public bool? ProjectRequired { get { return poType.Poprojectrequired; } set { poType.Poprojectrequired = value; } }


        //sub rental fields
        [JsonIgnore]
        [FwLogicProperty(Id:"BckcxH3tgp9V")]
        public string SubRentalOrderTypeFieldsId { get { return subRentalOrderTypeFields.OrderTypeFieldsId; } set { poType.SubrentalordertypefieldsId = value; subRentalOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"vYVG4whEsJ6y")]
        public bool? SubRentalShowOrderNumber { get { return subRentalOrderTypeFields.ShowOrderNumber; } set { subRentalOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"hjhekNwYC86c")]
        public bool? SubRentalShowRepairOrderNumber { get { return subRentalOrderTypeFields.ShowRepairOrderNumber; } set { subRentalOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"PxfDfAfAO5k6")]
        public bool? SubRentalShowICode { get { return subRentalOrderTypeFields.ShowICode; } set { subRentalOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"MApFRwEvpkgh")]
        public int? SubRentalICodeWidth { get { return subRentalOrderTypeFields.ICodeWidth; } set { subRentalOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"QTrMcDYoAxet")]
        public bool? SubRentalShowDescription { get { return subRentalOrderTypeFields.ShowDescription; } set { subRentalOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"Bnd9fqgY9lqp")]
        public int? SubRentalDescriptionWidth { get { return subRentalOrderTypeFields.DescriptionWidth; } set { subRentalOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"TfQvdtegZk4e")]
        //public bool? SubRentalShowPickDate { get { return subRentalOrderTypeFields.ShowPickDate; } set { subRentalOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"nzg6IP4Qz5w7")]
        public bool? SubRentalShowFromDate { get { return subRentalOrderTypeFields.ShowFromDate; } set { subRentalOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"RgCIAT8mQ4rr")]
        public bool? SubRentalShowFromTime { get { return subRentalOrderTypeFields.ShowFromTime; } set { subRentalOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"sTvcWA1WvAY4")]
        public bool? SubRentalShowToDate { get { return subRentalOrderTypeFields.ShowToDate; } set { subRentalOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"Y7vybe6gXaAA")]
        public bool? SubRentalShowToTime { get { return subRentalOrderTypeFields.ShowToTime; } set { subRentalOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"B0quWguTVXC5")]
        public bool? SubRentalShowBillablePeriods { get { return subRentalOrderTypeFields.ShowBillablePeriods; } set { subRentalOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"wcgYPUNNlH3R")]
        //public bool? SubRentalShowAvailableQuantity { get { return subRentalOrderTypeFields.ShowAvailableQuantity; } set { subRentalOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"vSLKsm2y4Wzp")]
        //public bool? SubRentalShowConflictDate { get { return subRentalOrderTypeFields.ShowConflictDate; } set { subRentalOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"OPomHoIuOOYt")]
        public bool? SubRentalShowRate { get { return subRentalOrderTypeFields.ShowRate; } set { subRentalOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"mKug1bvllnwJ")]
        //public bool? SubRentalShowCost { get { return subRentalOrderTypeFields.ShowCost; } set { subRentalOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"8YMsfZnTyKDK")]
        //public bool? SubRentalShowWeeklyCostExtended { get { return subRentalOrderTypeFields.ShowWeeklyCostExtended; } set { subRentalOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"GNgJowiACsHC")]
        //public bool? SubRentalShowMonthlyCostExtended { get { return subRentalOrderTypeFields.ShowMonthlyCostExtended; } set { subRentalOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"q1N4X9TqID9F")]
        //public bool? SubRentalShowPeriodCostExtended { get { return subRentalOrderTypeFields.ShowPeriodCostExtended; } set { subRentalOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"1aFyMDhT2rVR")]
        public bool? SubRentalShowDaysPerWeek { get { return subRentalOrderTypeFields.ShowDaysPerWeek; } set { subRentalOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"MfHmLKVDn52c")]
        public bool? SubRentalShowDiscountPercent { get { return subRentalOrderTypeFields.ShowDiscountPercent; } set { subRentalOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"BfOKmbJlfoBj")]
        //public bool? SubRentalShowMarkupPercent { get { return subRentalOrderTypeFields.ShowMarkupPercent; } set { subRentalOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"nFSYRCGv7fYh")]
        //public bool? SubRentalShowMarginPercent { get { return subRentalOrderTypeFields.ShowMarginPercent; } set { subRentalOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"3Skt8BiserKj")]
        public bool? SubRentalShowUnit { get { return subRentalOrderTypeFields.ShowUnit; } set { subRentalOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"3QBI8GObsLxK")]
        public bool? SubRentalShowUnitDiscountAmount { get { return subRentalOrderTypeFields.ShowUnitDiscountAmount; } set { subRentalOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"JmzkrsjmC8ap")]
        public bool? SubRentalShowUnitExtended { get { return subRentalOrderTypeFields.ShowUnitExtended; } set { subRentalOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"GYYKVToWxtTh")]
        public bool? SubRentalShowWeeklyDiscountAmount { get { return subRentalOrderTypeFields.ShowWeeklyDiscountAmount; } set { subRentalOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"vkgMI2iESkwx")]
        public bool? SubRentalShowWeeklyExtended { get { return subRentalOrderTypeFields.ShowWeeklyExtended; } set { subRentalOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"f9XrCOpGukFz")]
        public bool? SubRentalShowMonthlyDiscountAmount { get { return subRentalOrderTypeFields.ShowMonthlyDiscountAmount; } set { subRentalOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"vXvEJccAg6oP")]
        public bool? SubRentalShowMonthlyExtended { get { return subRentalOrderTypeFields.ShowMonthlyExtended; } set { subRentalOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"oUv3y8KPrr8w")]
        public bool? SubRentalShowPeriodDiscountAmount { get { return subRentalOrderTypeFields.ShowPeriodDiscountAmount; } set { subRentalOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"DFnAA7aiOFFs")]
        public bool? SubRentalShowPeriodExtended { get { return subRentalOrderTypeFields.ShowPeriodExtended; } set { subRentalOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"LYBED1ayZH3G")]
        //public bool? SubRentalShowVariancePercent { get { return subRentalOrderTypeFields.ShowVariancePercent; } set { subRentalOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"EseAbXsZ8Gct")]
        //public bool? SubRentalShowVarianceExtended { get { return subRentalOrderTypeFields.ShowVarianceExtended; } set { subRentalOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"ptNOVcvUtgX7")]
        public bool? SubRentalShowWarehouse { get { return subRentalOrderTypeFields.ShowWarehouse; } set { subRentalOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"p6ufHaQvys6v")]
        public bool? SubRentalShowTaxable { get { return subRentalOrderTypeFields.ShowTaxable; } set { subRentalOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"joV4LBFXJmOW")]
        public bool? SubRentalShowNotes { get { return subRentalOrderTypeFields.ShowNotes; } set { subRentalOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"46Hz6khawhR7")]
        //public bool? SubRentalShowReturnToWarehouse { get { return subRentalOrderTypeFields.ShowReturnToWarehouse; } set { subRentalOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"278Ua1WtmJ5l")]
        //public bool? SubRentalShowVehicleNumber { get { return subRentalOrderTypeFields.ShowVehicleNumber; } set { subRentalOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"Qd7NdbIAAYrv")]
        //public bool? SubRentalShowBarCode { get { return subRentalOrderTypeFields.ShowBarCode; } set { subRentalOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"j5svC7gaOhzs")]
        //public bool? SubRentalShowSerialNumber { get { return subRentalOrderTypeFields.ShowSerialNumber; } set { subRentalOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"C1CofVoCvvsE")]
        //public bool? SubRentalShowPickTime { get { return subRentalOrderTypeFields.ShowPickTime; } set { subRentalOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"WodbdOiBQjCc")]
        //public bool? SubRentalShowAvailableQuantityAllWarehouses { get { return subRentalOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subRentalOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"hnZEiF54n4a5")]
        //public bool? SubRentalShowConflictDateAllWarehouses { get { return subRentalOrderTypeFields.ShowConflictDateAllWarehouses; } set { subRentalOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"Kl0qSWIGtgTx")]
        //public bool? SubRentalShowConsignmentAvailableQuantity { get { return subRentalOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subRentalOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"zTEEOXuVlmTP")]
        //public bool? SubRentalShowConsignmentConflictDate { get { return subRentalOrderTypeFields.ShowConsignmentConflictDate; } set { subRentalOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"Mkbb7Fb5sc6R")]
        //public bool? SubRentalShowConsignmentQuantity { get { return subRentalOrderTypeFields.ShowConsignmentQuantity; } set { subRentalOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"sNUM6RP1rL55")]
        //public bool? SubRentalShowInLocationQuantity { get { return subRentalOrderTypeFields.ShowInLocationQuantity; } set { subRentalOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"lRFnP23iVdy5")]
        //public bool? SubRentalShowReservedItems { get { return subRentalOrderTypeFields.ShowReservedItems; } set { subRentalOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"Cx7KgbuiOgJw")]
        //public bool? SubRentalShowWeeksAndDays { get { return subRentalOrderTypeFields.ShowWeeksAndDays; } set { subRentalOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"v7XERmCglHbO")]
        //public bool? SubRentalShowMonthsAndDays { get { return subRentalOrderTypeFields.ShowMonthsAndDays; } set { subRentalOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"gszf9wMA3mz0")]
        //public bool? SubRentalShowPremiumPercent { get { return subRentalOrderTypeFields.ShowPremiumPercent; } set { subRentalOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"l4p3SzUfYtHh")]
        //public bool? SubRentalShowDepartment { get { return subRentalOrderTypeFields.ShowDepartment; } set { subRentalOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"LVhaibSz78be")]
        //public bool? SubRentalShowLocation { get { return subRentalOrderTypeFields.ShowLocation; } set { subRentalOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"fB8DlJkSKvRf")]
        //public bool? SubRentalShowOrderActivity { get { return subRentalOrderTypeFields.ShowOrderActivity; } set { subRentalOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"qNalcHOeh9fN")]
        //public bool? SubRentalShowSubOrderNumber { get { return subRentalOrderTypeFields.ShowSubOrderNumber; } set { subRentalOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"uGdAIXfeUyF6")]
        //public bool? SubRentalShowOrderStatus { get { return subRentalOrderTypeFields.ShowOrderStatus; } set { subRentalOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"LTkFnjWURqZL")]
        //public bool? SubRentalShowEpisodes { get { return subRentalOrderTypeFields.ShowEpisodes; } set { subRentalOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"5veGM6TE6SN7")]
        //public bool? SubRentalShowEpisodeExtended { get { return subRentalOrderTypeFields.ShowEpisodeExtended; } set { subRentalOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"gULHd1legfzL")]
        //public bool? SubRentalShowEpisodeDiscountAmount { get { return subRentalOrderTypeFields.ShowEpisodeDiscountAmount; } set { subRentalOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"IvdVf7lWzX6G")]
        public string SubRentalDateStamp { get { return subRentalOrderTypeFields.DateStamp; } set { subRentalOrderTypeFields.DateStamp = value; } }


        //sub sales fields
        [JsonIgnore]
        [FwLogicProperty(Id:"5XFqtOCAgXLu")]
        public string SubSaleOrderTypeFieldsId { get { return subSaleOrderTypeFields.OrderTypeFieldsId; } set { poType.SubsalesordertypefieldsId = value; subSaleOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"9QFiMsKUvGGk")]
        public bool? SubSaleShowOrderNumber { get { return subSaleOrderTypeFields.ShowOrderNumber; } set { subSaleOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"HMeOjJBgom10")]
        //public bool? SubSaleShowRepairOrderNumber { get { return subSaleOrderTypeFields.ShowRepairOrderNumber; } set { subSaleOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"7pFwMW9seiIS")]
        public bool? SubSaleShowICode { get { return subSaleOrderTypeFields.ShowICode; } set { subSaleOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"9nq2e7HXM3PE")]
        public int? SubSaleICodeWidth { get { return subSaleOrderTypeFields.ICodeWidth; } set { subSaleOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"gsPOkADPr3sg")]
        public bool? SubSaleShowDescription { get { return subSaleOrderTypeFields.ShowDescription; } set { subSaleOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"Z5qMTIl2vimA")]
        public int? SubSaleDescriptionWidth { get { return subSaleOrderTypeFields.DescriptionWidth; } set { subSaleOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"c3Nm7W01kTtl")]
        //public bool? SubSaleShowPickDate { get { return subSaleOrderTypeFields.ShowPickDate; } set { subSaleOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"WK9p1tG0Wza3")]
        //public bool? SubSaleShowFromDate { get { return subSaleOrderTypeFields.ShowFromDate; } set { subSaleOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"1hP48kRVH2ar")]
        //public bool? SubSaleShowToDate { get { return subSaleOrderTypeFields.ShowToDate; } set { subSaleOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"LR1r9IabVW6s")]
        //public bool? SubSaleShowBillablePeriods { get { return subSaleOrderTypeFields.ShowBillablePeriods; } set { subSaleOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"OTCYUxaF2WQV")]
        //public bool? SubSaleShowAvailableQuantity { get { return subSaleOrderTypeFields.ShowAvailableQuantity; } set { subSaleOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"HGYGiOUphohQ")]
        //public bool? SubSaleShowConflictDate { get { return subSaleOrderTypeFields.ShowConflictDate; } set { subSaleOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"jg6GchuEuhDe")]
        public bool? SubSaleShowRate { get { return subSaleOrderTypeFields.ShowRate; } set { subSaleOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"iylxhBsQciO2")]
        //public bool? SubSaleShowCost { get { return subSaleOrderTypeFields.ShowCost; } set { subSaleOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"Qv99YWIJNHBS")]
        //public bool? SubSaleShowWeeklyCostExtended { get { return subSaleOrderTypeFields.ShowWeeklyCostExtended; } set { subSaleOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"Lx1JJOmtd66G")]
        //public bool? SubSaleShowMonthlyCostExtended { get { return subSaleOrderTypeFields.ShowMonthlyCostExtended; } set { subSaleOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"pOvO9OcU55bc")]
        //public bool? SubSaleShowPeriodCostExtended { get { return subSaleOrderTypeFields.ShowPeriodCostExtended; } set { subSaleOrderTypeFields.ShowPeriodCostExtended = value; } }

        [FwLogicProperty(Id:"u6BWNdYIEoVp")]
        public bool? SubSaleShowDiscountPercent { get { return subSaleOrderTypeFields.ShowDiscountPercent; } set { subSaleOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"sbE51GEHC1Vs")]
        //public bool? SubSaleShowMarkupPercent { get { return subSaleOrderTypeFields.ShowMarkupPercent; } set { subSaleOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"kyC9r20sjBLQ")]
        //public bool? SubSaleShowMarginPercent { get { return subSaleOrderTypeFields.ShowMarginPercent; } set { subSaleOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"tdzuFpF8xXBH")]
        public bool? SubSaleShowUnit { get { return subSaleOrderTypeFields.ShowUnit; } set { subSaleOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"y5sGYjV398mW")]
        public bool? SubSaleShowUnitDiscountAmount { get { return subSaleOrderTypeFields.ShowUnitDiscountAmount; } set { subSaleOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"r5uKgfGELWpz")]
        public bool? SubSaleShowUnitExtended { get { return subSaleOrderTypeFields.ShowUnitExtended; } set { subSaleOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"zIfhk12JOwUp")]
        //public bool? SubSaleShowWeeklyDiscountAmount { get { return subSaleOrderTypeFields.ShowWeeklyDiscountAmount; } set { subSaleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"MIJpGT2Mq6sY")]
        //public bool? SubSaleShowWeeklyExtended { get { return subSaleOrderTypeFields.ShowWeeklyExtended; } set { subSaleOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"nVpLtrwyyCI9")]
        //public bool? SubSaleShowMonthlyDiscountAmount { get { return subSaleOrderTypeFields.ShowMonthlyDiscountAmount; } set { subSaleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"T9Z9MKYSoE7g")]
        //public bool? SubSaleShowMonthlyExtended { get { return subSaleOrderTypeFields.ShowMonthlyExtended; } set { subSaleOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"vCTJ127yxUi0")]
        public bool? SubSaleShowPeriodDiscountAmount { get { return subSaleOrderTypeFields.ShowPeriodDiscountAmount; } set { subSaleOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"SQ0jkWbokz40")]
        public bool? SubSaleShowPeriodExtended { get { return subSaleOrderTypeFields.ShowPeriodExtended; } set { subSaleOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"6iOBZv5BiLPU")]
        //public bool? SubSaleShowVariancePercent { get { return subSaleOrderTypeFields.ShowVariancePercent; } set { subSaleOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"Iazv3zaXbEvo")]
        //public bool? SubSaleShowVarianceExtended { get { return subSaleOrderTypeFields.ShowVarianceExtended; } set { subSaleOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"yVMSaORuyLYH")]
        public bool? SubSaleShowWarehouse { get { return subSaleOrderTypeFields.ShowWarehouse; } set { subSaleOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"faFbhBE1d3od")]
        public bool? SubSaleShowTaxable { get { return subSaleOrderTypeFields.ShowTaxable; } set { subSaleOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"3L3OdVmY8CWC")]
        public bool? SubSaleShowNotes { get { return subSaleOrderTypeFields.ShowNotes; } set { subSaleOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"x97xeE5xUdDh")]
        //public bool? SubSaleShowReturnToWarehouse { get { return subSaleOrderTypeFields.ShowReturnToWarehouse; } set { subSaleOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"GzW8poviEHNd")]
        //public bool? SubSaleShowFromTime { get { return subSaleOrderTypeFields.ShowFromTime; } set { subSaleOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"CeJODKxVawIx")]
        //public bool? SubSaleShowToTime { get { return subSaleOrderTypeFields.ShowToTime; } set { subSaleOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"pJBuPu3S98CD")]
        //public bool? SubSaleShowVehicleNumber { get { return subSaleOrderTypeFields.ShowVehicleNumber; } set { subSaleOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"dzFhQpYGZBKH")]
        //public bool? SubSaleShowPickTime { get { return subSaleOrderTypeFields.ShowPickTime; } set { subSaleOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"uEtSsQbLnVqu")]
        //public bool? SubSaleShowAvailableQuantityAllWarehouses { get { return subSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"HITOE3FLn2GX")]
        //public bool? SubSaleShowConflictDateAllWarehouses { get { return subSaleOrderTypeFields.ShowConflictDateAllWarehouses; } set { subSaleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"2Lq5nQy7xexl")]
        //public bool? SubSaleShowConsignmentAvailableQuantity { get { return subSaleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subSaleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"R7RYxSuJt2Qg")]
        //public bool? SubSaleShowConsignmentConflictDate { get { return subSaleOrderTypeFields.ShowConsignmentConflictDate; } set { subSaleOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"T4hY3kI6HK5K")]
        //public bool? SubSaleShowConsignmentQuantity { get { return subSaleOrderTypeFields.ShowConsignmentQuantity; } set { subSaleOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"y7gCTV5Ih75V")]
        //public bool? SubSaleShowInLocationQuantity { get { return subSaleOrderTypeFields.ShowInLocationQuantity; } set { subSaleOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"DVo8PL7frFbI")]
        //public bool? SubSaleShowReservedItems { get { return subSaleOrderTypeFields.ShowReservedItems; } set { subSaleOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"zZ8BkpSreIGE")]
        //public bool? SubSaleShowWeeksAndDays { get { return subSaleOrderTypeFields.ShowWeeksAndDays; } set { subSaleOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"68liHcBZcfnn")]
        //public bool? SubSaleShowMonthsAndDays { get { return subSaleOrderTypeFields.ShowMonthsAndDays; } set { subSaleOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"vwnMYZ5MA44S")]
        //public bool? SubSaleShowPremiumPercent { get { return subSaleOrderTypeFields.ShowPremiumPercent; } set { subSaleOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"EYVX4PpBlmKD")]
        //public bool? SubSaleShowDepartment { get { return subSaleOrderTypeFields.ShowDepartment; } set { subSaleOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"50Qy4E4qEdsA")]
        //public bool? SubSaleShowLocation { get { return subSaleOrderTypeFields.ShowLocation; } set { subSaleOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"Uc5pBlLGlEsi")]
        //public bool? SubSaleShowOrderActivity { get { return subSaleOrderTypeFields.ShowOrderActivity; } set { subSaleOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"KJgHoceEljpl")]
        //public bool? SubSaleShowSubOrderNumber { get { return subSaleOrderTypeFields.ShowSubOrderNumber; } set { subSaleOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"p9po5azlXhYk")]
        //public bool? SubSaleShowOrderStatus { get { return subSaleOrderTypeFields.ShowOrderStatus; } set { subSaleOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"ohTG9Mp74dWI")]
        //public bool? SubSaleShowEpisodes { get { return subSaleOrderTypeFields.ShowEpisodes; } set { subSaleOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"6V9tOQs1guBD")]
        //public bool? SubSaleShowEpisodeExtended { get { return subSaleOrderTypeFields.ShowEpisodeExtended; } set { subSaleOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"04ZM6x5CuEkQ")]
        //public bool? SubSaleShowEpisodeDiscountAmount { get { return subSaleOrderTypeFields.ShowEpisodeDiscountAmount; } set { subSaleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"Hvkzbn8a4nm3")]
        public string SubSaleDateStamp { get { return subSaleOrderTypeFields.DateStamp; } set { subSaleOrderTypeFields.DateStamp = value; } }



        //purchase fields
        [JsonIgnore]
        [FwLogicProperty(Id:"xJEg5wjMfA18")]
        public string PurchaseOrderTypeFieldsId { get { return purchaseOrderTypeFields.OrderTypeFieldsId; } set { poType.PurchaseordertypefieldsId = value; purchaseOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"FGCC1mra7al5")]
        //public bool? PurchaseShowOrderNumber { get { return purchaseOrderTypeFields.ShowOrderNumber; } set { purchaseOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"JZUiJk48fwpg")]
        //public bool? PurchaseShowRepairOrderNumber { get { return purchaseOrderTypeFields.ShowRepairOrderNumber; } set { purchaseOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"dFXzxQW9wWKJ")]
        public bool? PurchaseShowICode { get { return purchaseOrderTypeFields.ShowICode; } set { purchaseOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"xde38JRyKrZV")]
        public int? PurchaseICodeWidth { get { return purchaseOrderTypeFields.ICodeWidth; } set { purchaseOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"EJU5qRlTiTHR")]
        public bool? PurchaseShowDescription { get { return purchaseOrderTypeFields.ShowDescription; } set { purchaseOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"qmZognLkACaT")]
        public int? PurchaseDescriptionWidth { get { return purchaseOrderTypeFields.DescriptionWidth; } set { purchaseOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"uZUuoUCKQArY")]
        //public bool? PurchaseShowPickDate { get { return purchaseOrderTypeFields.ShowPickDate; } set { purchaseOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"AQvWeV8Gcu4z")]
        //public bool? PurchaseShowFromDate { get { return purchaseOrderTypeFields.ShowFromDate; } set { purchaseOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"vzgHYiLNMGkt")]
        //public bool? PurchaseShowToDate { get { return purchaseOrderTypeFields.ShowToDate; } set { purchaseOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"lnkjG6ajvCdG")]
        //public bool? PurchaseShowBillablePeriods { get { return purchaseOrderTypeFields.ShowBillablePeriods; } set { purchaseOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"hbrzfKMD95cL")]
        //public bool? PurchaseShowSubQuantity { get { return purchaseOrderTypeFields.ShowSubQuantity; } set { purchaseOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"Gr4AH1EJwKKb")]
        //public bool? PurchaseShowAvailableQuantity { get { return purchaseOrderTypeFields.ShowAvailableQuantity; } set { purchaseOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"Zinf84QQpPeR")]
        //public bool? PurchaseShowConflictDate { get { return purchaseOrderTypeFields.ShowConflictDate; } set { purchaseOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"uDPDBRX43fn7")]
        public bool? PurchaseShowRate { get { return purchaseOrderTypeFields.ShowRate; } set { purchaseOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"H7RkRVTjCGnJ")]
        //public bool? PurchaseShowCost { get { return purchaseOrderTypeFields.ShowCost; } set { purchaseOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"9RlOYAs8srs7")]
        //public bool? PurchaseShowWeeklyCostExtended { get { return purchaseOrderTypeFields.ShowWeeklyCostExtended; } set { purchaseOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"4r7TC77MIq3c")]
        //public bool? PurchaseShowMonthlyCostExtended { get { return purchaseOrderTypeFields.ShowMonthlyCostExtended; } set { purchaseOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"C0VkJ4md4KTZ")]
        //public bool? PurchaseShowPeriodCostExtended { get { return purchaseOrderTypeFields.ShowPeriodCostExtended; } set { purchaseOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"CYn2yv776zZC")]
        //public bool? PurchaseShowDaysPerWeek { get { return purchaseOrderTypeFields.ShowDaysPerWeek; } set { purchaseOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"JAXWTbyeHInd")]
        public bool? PurchaseShowDiscountPercent { get { return purchaseOrderTypeFields.ShowDiscountPercent; } set { purchaseOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"mW3BDI47ipGX")]
        //public bool? PurchaseShowMarkupPercent { get { return purchaseOrderTypeFields.ShowMarkupPercent; } set { purchaseOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"1rLHDMygK65W")]
        //public bool? PurchaseShowMarginPercent { get { return purchaseOrderTypeFields.ShowMarginPercent; } set { purchaseOrderTypeFields.ShowMarginPercent = value; } }

        //[FwLogicProperty(Id:"idLqHQBYe1n1")]
        //public bool? PurchaseShowSplit { get { return purchaseOrderTypeFields.ShowSplit; } set { purchaseOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"qtPUS45EvZfj")]
        public bool? PurchaseShowUnit { get { return purchaseOrderTypeFields.ShowUnit; } set { purchaseOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"Qfzdxip8ImD9")]
        public bool? PurchaseShowUnitDiscountAmount { get { return purchaseOrderTypeFields.ShowUnitDiscountAmount; } set { purchaseOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"UWSrZCciSEFl")]
        public bool? PurchaseShowUnitExtended { get { return purchaseOrderTypeFields.ShowUnitExtended; } set { purchaseOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"SVCoTjHim5CD")]
        //public bool? PurchaseShowWeeklyDiscountAmount { get { return purchaseOrderTypeFields.ShowWeeklyDiscountAmount; } set { purchaseOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"A23ZHb3HF49O")]
        //public bool? PurchaseShowWeeklyExtended { get { return purchaseOrderTypeFields.ShowWeeklyExtended; } set { purchaseOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"QCrAQ6MdlgJl")]
        //public bool? PurchaseShowMonthlyDiscountAmount { get { return purchaseOrderTypeFields.ShowMonthlyDiscountAmount; } set { purchaseOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"IY3nGpNGkJoD")]
        //public bool? PurchaseShowMonthlyExtended { get { return purchaseOrderTypeFields.ShowMonthlyExtended; } set { purchaseOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"DfYrzdV5SByT")]
        public bool? PurchaseShowPeriodDiscountAmount { get { return purchaseOrderTypeFields.ShowPeriodDiscountAmount; } set { purchaseOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"Vbh8BjTCsCN6")]
        public bool? PurchaseShowPeriodExtended { get { return purchaseOrderTypeFields.ShowPeriodExtended; } set { purchaseOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"b8XxYF69QeIr")]
        //public bool? PurchaseShowVariancePercent { get { return purchaseOrderTypeFields.ShowVariancePercent; } set { purchaseOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"Z7sFvjfzPgI6")]
        //public bool? PurchaseShowVarianceExtended { get { return purchaseOrderTypeFields.ShowVarianceExtended; } set { purchaseOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"zEYzFLXJCoLx")]
        public bool? PurchaseShowCountryOfOrigin { get { return purchaseOrderTypeFields.ShowCountryOfOrigin; } set { purchaseOrderTypeFields.ShowCountryOfOrigin = value; } }

        [FwLogicProperty(Id:"fDM0Ls5Y4maw")]
        public bool? PurchaseShowManufacturer { get { return purchaseOrderTypeFields.ShowManufacturer; } set { purchaseOrderTypeFields.ShowManufacturer = value; } }

        [FwLogicProperty(Id:"F9SrRfjxfLeg")]
        public bool? PurchaseShowManufacturerPartNumber { get { return purchaseOrderTypeFields.ShowManufacturerPartNumber; } set { purchaseOrderTypeFields.ShowManufacturerPartNumber = value; } }

        [FwLogicProperty(Id:"7JitKlJyPyHS")]
        public int? PurchaseManufacturerPartNumberWidth { get { return purchaseOrderTypeFields.ManufacturerPartNumberWidth; } set { purchaseOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        [FwLogicProperty(Id:"2LKgR76OAIU9")]
        public bool? PurchaseShowModelNumber { get { return purchaseOrderTypeFields.ShowModelNumber; } set { purchaseOrderTypeFields.ShowModelNumber = value; } }

        [FwLogicProperty(Id:"f967Q9EQGXmu")]
        public bool? PurchaseShowVendorPartNumber { get { return purchaseOrderTypeFields.ShowVendorPartNumber; } set { purchaseOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"NeIW1eZ6a1CE")]
        public bool? PurchaseShowWarehouse { get { return purchaseOrderTypeFields.ShowWarehouse; } set { purchaseOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"LX8oOftR6pco")]
        public bool? PurchaseShowTaxable { get { return purchaseOrderTypeFields.ShowTaxable; } set { purchaseOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"IF4DHtNO8hLp")]
        public bool? PurchaseShowNotes { get { return purchaseOrderTypeFields.ShowNotes; } set { purchaseOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"BXmZswqCNetK")]
        //public bool? PurchaseShowReturnToWarehouse { get { return purchaseOrderTypeFields.ShowReturnToWarehouse; } set { purchaseOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"dXZP6dtt5H5W")]
        //public bool? PurchaseShowFromTime { get { return purchaseOrderTypeFields.ShowFromTime; } set { purchaseOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"jDtWCB7fPHSZ")]
        //public bool? PurchaseShowToTime { get { return purchaseOrderTypeFields.ShowToTime; } set { purchaseOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"8zUER1iD1nP6")]
        //public bool? PurchaseShowVehicleNumber { get { return purchaseOrderTypeFields.ShowVehicleNumber; } set { purchaseOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"PDiqEqgzC7T2")]
        //public bool? PurchaseShowBarCode { get { return purchaseOrderTypeFields.ShowBarCode; } set { purchaseOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"EfEepOoJFmRP")]
        //public bool? PurchaseShowSerialNumber { get { return purchaseOrderTypeFields.ShowSerialNumber; } set { purchaseOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"kwJHZOGX7sGg")]
        //public bool? PurchaseShowCrewName { get { return purchaseOrderTypeFields.ShowCrewName; } set { purchaseOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"kGmtpeWhj280")]
        //public bool? PurchaseShowHours { get { return purchaseOrderTypeFields.ShowHours; } set { purchaseOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"ERYKfAjImj2R")]
        //public bool? PurchaseShowPickTime { get { return purchaseOrderTypeFields.ShowPickTime; } set { purchaseOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"dtsAnSexgXFS")]
        //public bool? PurchaseShowAvailableQuantityAllWarehouses { get { return purchaseOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { purchaseOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"Mq7uJ2csyDmm")]
        //public bool? PurchaseShowConflictDateAllWarehouses { get { return purchaseOrderTypeFields.ShowConflictDateAllWarehouses; } set { purchaseOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"HTuyeQBkFDwC")]
        //public bool? PurchaseShowConsignmentAvailableQuantity { get { return purchaseOrderTypeFields.ShowConsignmentAvailableQuantity; } set { purchaseOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"6yC806KDqZNt")]
        //public bool? PurchaseShowConsignmentConflictDate { get { return purchaseOrderTypeFields.ShowConsignmentConflictDate; } set { purchaseOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"sQtHP8pSfyr4")]
        //public bool? PurchaseShowConsignmentQuantity { get { return purchaseOrderTypeFields.ShowConsignmentQuantity; } set { purchaseOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"1o5JH3quZ1uN")]
        //public bool? PurchaseShowInLocationQuantity { get { return purchaseOrderTypeFields.ShowInLocationQuantity; } set { purchaseOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"ERPdPkDSx8uI")]
        //public bool? PurchaseShowReservedItems { get { return purchaseOrderTypeFields.ShowReservedItems; } set { purchaseOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"uEImPE0PirSs")]
        //public bool? PurchaseShowWeeksAndDays { get { return purchaseOrderTypeFields.ShowWeeksAndDays; } set { purchaseOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"IBB1QYYE5e5b")]
        //public bool? PurchaseShowMonthsAndDays { get { return purchaseOrderTypeFields.ShowMonthsAndDays; } set { purchaseOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"1qFfVrWM3UQL")]
        //public bool? PurchaseShowPremiumPercent { get { return purchaseOrderTypeFields.ShowPremiumPercent; } set { purchaseOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"oQsGKV6md4nN")]
        //public bool? PurchaseShowDepartment { get { return purchaseOrderTypeFields.ShowDepartment; } set { purchaseOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"WaSFP3pahE37")]
        //public bool? PurchaseShowLocation { get { return purchaseOrderTypeFields.ShowLocation; } set { purchaseOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"jwjILkVkRMDx")]
        //public bool? PurchaseShowOrderActivity { get { return purchaseOrderTypeFields.ShowOrderActivity; } set { purchaseOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"SUjxzdiByXLR")]
        //public bool? PurchaseShowSubOrderNumber { get { return purchaseOrderTypeFields.ShowSubOrderNumber; } set { purchaseOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"6MfyTbKCphV2")]
        //public bool? PurchaseShowOrderStatus { get { return purchaseOrderTypeFields.ShowOrderStatus; } set { purchaseOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"KkjMH9yFoJ8d")]
        //public bool? PurchaseShowEpisodes { get { return purchaseOrderTypeFields.ShowEpisodes; } set { purchaseOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"6thL9Ve17ch8")]
        //public bool? PurchaseShowEpisodeExtended { get { return purchaseOrderTypeFields.ShowEpisodeExtended; } set { purchaseOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"3zJh5rTzZBK5")]
        //public bool? PurchaseShowEpisodeDiscountAmount { get { return purchaseOrderTypeFields.ShowEpisodeDiscountAmount; } set { purchaseOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"pg2SMa5IRk1f")]
        public string PurchaseDateStamp { get { return purchaseOrderTypeFields.DateStamp; } set { purchaseOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"SmTCgv5S6dIJ")]
        public string RentalPurchaseDefaultRate { get { return poType.Rentalpurchasedefaultrate; } set { poType.Rentalpurchasedefaultrate = value; } }

        [FwLogicProperty(Id:"djhtv2ioA3xT")]
        public string SalesPurchaseDefaultRate { get { return poType.Salespurchasedefaultrate; } set { poType.Salespurchasedefaultrate = value; } }


        //labor/crew fields
        [JsonIgnore]
        [FwLogicProperty(Id:"UOAioR3JMapn")]
        public string LaborOrderTypeFieldsId { get { return laborOrderTypeFields.OrderTypeFieldsId; } set { poType.LaborOrderTypeFieldsId = value; laborOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"g0IqSvft7Pyf")]
        //public bool? LaborShowOrderNumber { get { return laborOrderTypeFields.ShowOrderNumber; } set { laborOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"rP5j66Uavrys")]
        //public bool? LaborShowRepairOrderNumber { get { return laborOrderTypeFields.ShowRepairOrderNumber; } set { laborOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"DKIKfYM5Vsv4")]
        public bool? LaborShowICode { get { return laborOrderTypeFields.ShowICode; } set { laborOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"a0oGbs05Izso")]
        public int? LaborICodeWidth { get { return laborOrderTypeFields.ICodeWidth; } set { laborOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"c5wGcXBJTLun")]
        public bool? LaborShowDescription { get { return laborOrderTypeFields.ShowDescription; } set { laborOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"gViYohey9OZB")]
        public int? LaborDescriptionWidth { get { return laborOrderTypeFields.DescriptionWidth; } set { laborOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"7I4AQwTZh762")]
        //public bool? LaborShowPickDate { get { return laborOrderTypeFields.ShowPickDate; } set { laborOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"SSeStE2pPDjj")]
        public bool? LaborShowOrderActivity { get { return laborOrderTypeFields.ShowOrderActivity; } set { laborOrderTypeFields.ShowOrderActivity = value; } }

        [FwLogicProperty(Id:"5kVqeZiPkjcA")]
        public bool? LaborShowCrewName { get { return laborOrderTypeFields.ShowCrewName; } set { laborOrderTypeFields.ShowCrewName = value; } }

        [FwLogicProperty(Id:"pq4p6RMvQw0Y")]
        public bool? LaborShowFromDate { get { return laborOrderTypeFields.ShowFromDate; } set { laborOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"tuxcpFn7psik")]
        public bool? LaborShowFromTime { get { return laborOrderTypeFields.ShowFromTime; } set { laborOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"aeaH6AVKzhUR")]
        public bool? LaborShowToDate { get { return laborOrderTypeFields.ShowToDate; } set { laborOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"XERZWEeuVZJG")]
        public bool? LaborShowToTime { get { return laborOrderTypeFields.ShowToTime; } set { laborOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"HNWIz0KpbCwW")]
        public bool? LaborShowHours { get { return laborOrderTypeFields.ShowHours; } set { laborOrderTypeFields.ShowHours = value; } }

        [FwLogicProperty(Id:"0AHdiD3SVbW5")]
        public bool? LaborShowBillablePeriods { get { return laborOrderTypeFields.ShowBillablePeriods; } set { laborOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"DQMkFIJSMA6e")]
        //public bool? LaborShowSubQuantity { get { return laborOrderTypeFields.ShowSubQuantity; } set { laborOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"BkELE5pC3Bl2")]
        //public bool? LaborShowAvailableQuantity { get { return laborOrderTypeFields.ShowAvailableQuantity; } set { laborOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"2pBBrW96I6mA")]
        //public bool? LaborShowConflictDate { get { return laborOrderTypeFields.ShowConflictDate; } set { laborOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"vCSlX56WNU8a")]
        public bool? LaborShowRate { get { return laborOrderTypeFields.ShowRate; } set { laborOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"FBI40GMEamiz")]
        //public bool? LaborShowCost { get { return laborOrderTypeFields.ShowCost; } set { laborOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"ZmDcmeVJYckE")]
        //public bool? LaborShowWeeklyCostExtended { get { return laborOrderTypeFields.ShowWeeklyCostExtended; } set { laborOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"Edk2HXM61UXy")]
        //public bool? LaborShowMonthlyCostExtended { get { return laborOrderTypeFields.ShowMonthlyCostExtended; } set { laborOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"kRlqFOgEmZEz")]
        //public bool? LaborShowPeriodCostExtended { get { return laborOrderTypeFields.ShowPeriodCostExtended; } set { laborOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"jx3TnrhZ9skE")]
        //public bool? LaborShowDaysPerWeek { get { return laborOrderTypeFields.ShowDaysPerWeek; } set { laborOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"BIN7hETGLrQn")]
        public bool? LaborShowDiscountPercent { get { return laborOrderTypeFields.ShowDiscountPercent; } set { laborOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"k6su68OZKcuZ")]
        //public bool? LaborShowMarkupPercent { get { return laborOrderTypeFields.ShowMarkupPercent; } set { laborOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"vFPiDizPOaJ8")]
        //public bool? LaborShowMarginPercent { get { return laborOrderTypeFields.ShowMarginPercent; } set { laborOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"ed37kzCns5rJ")]
        public bool? LaborShowUnit { get { return laborOrderTypeFields.ShowUnit; } set { laborOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"Mb4ov3K6zgLx")]
        public bool? LaborShowUnitDiscountAmount { get { return laborOrderTypeFields.ShowUnitDiscountAmount; } set { laborOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"zgsgTrRMhdRJ")]
        public bool? LaborShowUnitExtended { get { return laborOrderTypeFields.ShowUnitExtended; } set { laborOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"nLr7caVzaJ1h")]
        public bool? LaborShowWeeklyDiscountAmount { get { return laborOrderTypeFields.ShowWeeklyDiscountAmount; } set { laborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"6N4NZLzJ44rn")]
        public bool? LaborShowWeeklyExtended { get { return laborOrderTypeFields.ShowWeeklyExtended; } set { laborOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"Md6arAZP875L")]
        public bool? LaborShowMonthlyDiscountAmount { get { return laborOrderTypeFields.ShowMonthlyDiscountAmount; } set { laborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"YB2jeguRmwfS")]
        public bool? LaborShowMonthlyExtended { get { return laborOrderTypeFields.ShowMonthlyExtended; } set { laborOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"RiX4rDBJdMJY")]
        public bool? LaborShowPeriodDiscountAmount { get { return laborOrderTypeFields.ShowPeriodDiscountAmount; } set { laborOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"Y6VgCna0oYt4")]
        public bool? LaborShowPeriodExtended { get { return laborOrderTypeFields.ShowPeriodExtended; } set { laborOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"ASlZbyBoHrPB")]
        //public bool? LaborShowVariancePercent { get { return laborOrderTypeFields.ShowVariancePercent; } set { laborOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"byR6F9yCPuWt")]
        //public bool? LaborShowVarianceExtended { get { return laborOrderTypeFields.ShowVarianceExtended; } set { laborOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"h0CpEwu89Ii7")]
        public bool? LaborShowWarehouse { get { return laborOrderTypeFields.ShowWarehouse; } set { laborOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"zDCQU3zAw7Xh")]
        public bool? LaborShowTaxable { get { return laborOrderTypeFields.ShowTaxable; } set { laborOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"oLW4xxzJ8Wz3")]
        public bool? LaborShowNotes { get { return laborOrderTypeFields.ShowNotes; } set { laborOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"RzeKtXM1fctz")]
        //public bool? LaborShowReturnToWarehouse { get { return laborOrderTypeFields.ShowReturnToWarehouse; } set { laborOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"Mlran4flHr7e")]
        //public bool? LaborShowPickTime { get { return laborOrderTypeFields.ShowPickTime; } set { laborOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"bALhWZad96UO")]
        //public bool? LaborShowAvailableQuantityAllWarehouses { get { return laborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { laborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"pFC5OPkhwB5c")]
        //public bool? LaborShowConflictDateAllWarehouses { get { return laborOrderTypeFields.ShowConflictDateAllWarehouses; } set { laborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"SGYrMU72FEcU")]
        //public bool? LaborShowConsignmentAvailableQuantity { get { return laborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { laborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"T1ejhLXaYJiH")]
        //public bool? LaborShowConsignmentConflictDate { get { return laborOrderTypeFields.ShowConsignmentConflictDate; } set { laborOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"sQiurr6Gbd8M")]
        //public bool? LaborShowConsignmentQuantity { get { return laborOrderTypeFields.ShowConsignmentQuantity; } set { laborOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"XWWTyKKe2kc3")]
        //public bool? LaborShowInLocationQuantity { get { return laborOrderTypeFields.ShowInLocationQuantity; } set { laborOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"xQnigMUREHt9")]
        //public bool? LaborShowReservedItems { get { return laborOrderTypeFields.ShowReservedItems; } set { laborOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"hRAze2FFL5Fy")]
        //public bool? LaborShowWeeksAndDays { get { return laborOrderTypeFields.ShowWeeksAndDays; } set { laborOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"eu8FX2ck4Wtp")]
        //public bool? LaborShowMonthsAndDays { get { return laborOrderTypeFields.ShowMonthsAndDays; } set { laborOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"I44LpOpB6bR7")]
        //public bool? LaborShowPremiumPercent { get { return laborOrderTypeFields.ShowPremiumPercent; } set { laborOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"3CCI88tIyNlB")]
        //public bool? LaborShowDepartment { get { return laborOrderTypeFields.ShowDepartment; } set { laborOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"mEGjcvvbym6D")]
        //public bool? LaborShowLocation { get { return laborOrderTypeFields.ShowLocation; } set { laborOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"zyCtSlkarsOA")]
        //public bool? LaborShowSubOrderNumber { get { return laborOrderTypeFields.ShowSubOrderNumber; } set { laborOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"nrI7Qo1T5UG2")]
        //public bool? LaborShowOrderStatus { get { return laborOrderTypeFields.ShowOrderStatus; } set { laborOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"EY4PBGvGXxhk")]
        //public bool? LaborShowEpisodes { get { return laborOrderTypeFields.ShowEpisodes; } set { laborOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"Ac71yV4C1AuX")]
        //public bool? LaborShowEpisodeExtended { get { return laborOrderTypeFields.ShowEpisodeExtended; } set { laborOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"Wa8JiwPvcTYK")]
        //public bool? LaborShowEpisodeDiscountAmount { get { return laborOrderTypeFields.ShowEpisodeDiscountAmount; } set { laborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"RL0uNucpStu0")]
        public string LaborDateStamp { get { return laborOrderTypeFields.DateStamp; } set { laborOrderTypeFields.DateStamp = value; } }

        [FwLogicProperty(Id:"qt9KgZtwB9eQ")]
        public bool? HideCrewBreaks { get { return poType.Hidecrewbreaks; } set { poType.Hidecrewbreaks = value; } }

        [FwLogicProperty(Id:"BcMqVKLejUkK")]
        public bool? Break1Paid { get { return poType.Break1paId; } set { poType.Break1paId = value; } }

        [FwLogicProperty(Id:"s1aUwWi81hsu")]
        public bool? Break2Paid { get { return poType.Break2paId; } set { poType.Break2paId = value; } }

        [FwLogicProperty(Id:"5foFze23xG9o")]
        public bool? Break3Paid { get { return poType.Break3paId; } set { poType.Break3paId = value; } }


        //misc fields
        [JsonIgnore]
        [FwLogicProperty(Id:"QV7w1TUEjy6R")]
        public string MiscOrderTypeFieldsId { get { return miscOrderTypeFields.OrderTypeFieldsId; } set { poType.MiscOrderTypeFieldsId = value; miscOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"ns6nkjIcqIYK")]
        //public bool? MiscShowOrderNumber { get { return miscOrderTypeFields.ShowOrderNumber; } set { miscOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"yl0etc4TejM1")]
        //public bool? MiscShowRepairOrderNumber { get { return miscOrderTypeFields.ShowRepairOrderNumber; } set { miscOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"kLpMuZnf9ywB")]
        public bool? MiscShowICode { get { return miscOrderTypeFields.ShowICode; } set { miscOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"WoDXyqUfloCy")]
        public int? MiscICodeWidth { get { return miscOrderTypeFields.ICodeWidth; } set { miscOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"FDn0rQnLUCNb")]
        public bool? MiscShowDescription { get { return miscOrderTypeFields.ShowDescription; } set { miscOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"wSITpinm2yxi")]
        public int? MiscDescriptionWidth { get { return miscOrderTypeFields.DescriptionWidth; } set { miscOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"W8SpXSYMxMXC")]
        //public bool? MiscShowPickDate { get { return miscOrderTypeFields.ShowPickDate; } set { miscOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"MOVyTJJuupu2")]
        public bool? MiscShowFromDate { get { return miscOrderTypeFields.ShowFromDate; } set { miscOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"2bye5MCY92Bf")]
        public bool? MiscShowToDate { get { return miscOrderTypeFields.ShowToDate; } set { miscOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"jwXybJA1GRjh")]
        public bool? MiscShowBillablePeriods { get { return miscOrderTypeFields.ShowBillablePeriods; } set { miscOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"GHcXY3082REz")]
        //public bool? MiscShowSubQuantity { get { return miscOrderTypeFields.ShowSubQuantity; } set { miscOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"QtFQwaq1qvqn")]
        //public bool? MiscShowAvailableQuantity { get { return miscOrderTypeFields.ShowAvailableQuantity; } set { miscOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"EWvGvMkpXSWI")]
        //public bool? MiscShowConflictDate { get { return miscOrderTypeFields.ShowConflictDate; } set { miscOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"1pKqaCEejsOx")]
        public bool? MiscShowRate { get { return miscOrderTypeFields.ShowRate; } set { miscOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"DT0Q792GRUeD")]
        //public bool? MiscShowCost { get { return miscOrderTypeFields.ShowCost; } set { miscOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"2jnVJSzT9KNm")]
        //public bool? MiscShowWeeklyCostExtended { get { return miscOrderTypeFields.ShowWeeklyCostExtended; } set { miscOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"EGYeUrk4SIhM")]
        //public bool? MiscShowMonthlyCostExtended { get { return miscOrderTypeFields.ShowMonthlyCostExtended; } set { miscOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"d4wHJzZoRzDJ")]
        //public bool? MiscShowPeriodCostExtended { get { return miscOrderTypeFields.ShowPeriodCostExtended; } set { miscOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"Jzmcml2gezWD")]
        //public bool? MiscShowDaysPerWeek { get { return miscOrderTypeFields.ShowDaysPerWeek; } set { miscOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"XcZ1W6zWV0SZ")]
        public bool? MiscShowDiscountPercent { get { return miscOrderTypeFields.ShowDiscountPercent; } set { miscOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"BhPmUb1oF8Jr")]
        //public bool? MiscShowMarkupPercent { get { return miscOrderTypeFields.ShowMarkupPercent; } set { miscOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"CmubVPfqe23v")]
        //public bool? MiscShowMarginPercent { get { return miscOrderTypeFields.ShowMarginPercent; } set { miscOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"fj9Cu31QVz5g")]
        public bool? MiscShowUnit { get { return miscOrderTypeFields.ShowUnit; } set { miscOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"pUKxbkCnpFGQ")]
        public bool? MiscShowUnitDiscountAmount { get { return miscOrderTypeFields.ShowUnitDiscountAmount; } set { miscOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"kSJ7pDWRkFjD")]
        public bool? MiscShowUnitExtended { get { return miscOrderTypeFields.ShowUnitExtended; } set { miscOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"xZNrXb7pRraX")]
        public bool? MiscShowWeeklyDiscountAmount { get { return miscOrderTypeFields.ShowWeeklyDiscountAmount; } set { miscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"w9j8owirFzRM")]
        public bool? MiscShowWeeklyExtended { get { return miscOrderTypeFields.ShowWeeklyExtended; } set { miscOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"wvzl97jsu1jd")]
        public bool? MiscShowMonthlyDiscountAmount { get { return miscOrderTypeFields.ShowMonthlyDiscountAmount; } set { miscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"s44iW8pdZO6o")]
        public bool? MiscShowMonthlyExtended { get { return miscOrderTypeFields.ShowMonthlyExtended; } set { miscOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"Z8qkGnxBDBnZ")]
        public bool? MiscShowPeriodDiscountAmount { get { return miscOrderTypeFields.ShowPeriodDiscountAmount; } set { miscOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"jxch4XBAlula")]
        public bool? MiscShowPeriodExtended { get { return miscOrderTypeFields.ShowPeriodExtended; } set { miscOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"XfETPjBLmqhX")]
        //public bool? MiscShowVariancePercent { get { return miscOrderTypeFields.ShowVariancePercent; } set { miscOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"sxyYO1nqr1MA")]
        //public bool? MiscShowVarianceExtended { get { return miscOrderTypeFields.ShowVarianceExtended; } set { miscOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"KHfFpvzR316F")]
        public bool? MiscShowWarehouse { get { return miscOrderTypeFields.ShowWarehouse; } set { miscOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"OFSkLTZGbf9v")]
        public bool? MiscShowTaxable { get { return miscOrderTypeFields.ShowTaxable; } set { miscOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"XrbdOXYNmSew")]
        public bool? MiscShowNotes { get { return miscOrderTypeFields.ShowNotes; } set { miscOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"HpUcnREgkLjK")]
        //public bool? MiscShowReturnToWarehouse { get { return miscOrderTypeFields.ShowReturnToWarehouse; } set { miscOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"SE6drl8iBbJ2")]
        //public bool? MiscShowFromTime { get { return miscOrderTypeFields.ShowFromTime; } set { miscOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"4JEq7dDzIEIe")]
        //public bool? MiscShowToTime { get { return miscOrderTypeFields.ShowToTime; } set { miscOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"Ge04kFs2YKGu")]
        //public bool? MiscShowVehicleNumber { get { return miscOrderTypeFields.ShowVehicleNumber; } set { miscOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"ZOdJIA1lUEUs")]
        //public bool? MiscShowBarCode { get { return miscOrderTypeFields.ShowBarCode; } set { miscOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"MDnMszk2hrZL")]
        //public bool? MiscShowSerialNumber { get { return miscOrderTypeFields.ShowSerialNumber; } set { miscOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"65gqK3Xhn9qx")]
        //public bool? MiscShowCrewName { get { return miscOrderTypeFields.ShowCrewName; } set { miscOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"g7n2BObdlYd4")]
        //public bool? MiscShowHours { get { return miscOrderTypeFields.ShowHours; } set { miscOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"EpKAH3OttHq4")]
        //public bool? MiscShowPickTime { get { return miscOrderTypeFields.ShowPickTime; } set { miscOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"Yc5mOCL5IefZ")]
        //public bool? MiscShowAvailableQuantityAllWarehouses { get { return miscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { miscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"ut1U780c4CUv")]
        //public bool? MiscShowConflictDateAllWarehouses { get { return miscOrderTypeFields.ShowConflictDateAllWarehouses; } set { miscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"aHxSkUrBOCpe")]
        //public bool? MiscShowConsignmentAvailableQuantity { get { return miscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { miscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"k8A92Vp9vICs")]
        //public bool? MiscShowConsignmentConflictDate { get { return miscOrderTypeFields.ShowConsignmentConflictDate; } set { miscOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"Ehvx7aEwfWsW")]
        //public bool? MiscShowConsignmentQuantity { get { return miscOrderTypeFields.ShowConsignmentQuantity; } set { miscOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"7ORK0OlvuO0a")]
        //public bool? MiscShowInLocationQuantity { get { return miscOrderTypeFields.ShowInLocationQuantity; } set { miscOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"xacyOhfalVhh")]
        //public bool? MiscShowReservedItems { get { return miscOrderTypeFields.ShowReservedItems; } set { miscOrderTypeFields.ShowReservedItems = value; } }

        [FwLogicProperty(Id:"n1XkPn8K9DSK")]
        public bool? MiscShowWeeksAndDays { get { return miscOrderTypeFields.ShowWeeksAndDays; } set { miscOrderTypeFields.ShowWeeksAndDays = value; } }

        [FwLogicProperty(Id:"czcX8chz7tRK")]
        public bool? MiscShowMonthsAndDays { get { return miscOrderTypeFields.ShowMonthsAndDays; } set { miscOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"Afps1bRw4IMA")]
        //public bool? MiscShowPremiumPercent { get { return miscOrderTypeFields.ShowPremiumPercent; } set { miscOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"uziGVcq6ID23")]
        //public bool? MiscShowDepartment { get { return miscOrderTypeFields.ShowDepartment; } set { miscOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"XGmPKYpVvJ9B")]
        //public bool? MiscShowLocation { get { return miscOrderTypeFields.ShowLocation; } set { miscOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"EmMREuxXUFjB")]
        //public bool? MiscShowOrderActivity { get { return miscOrderTypeFields.ShowOrderActivity; } set { miscOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"qpaS4whdvoKP")]
        //public bool? MiscShowSubOrderNumber { get { return miscOrderTypeFields.ShowSubOrderNumber; } set { miscOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"90rDUjPUtYxr")]
        //public bool? MiscShowOrderStatus { get { return miscOrderTypeFields.ShowOrderStatus; } set { miscOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"yuOhwWHCd8g1")]
        //public bool? MiscShowEpisodes { get { return miscOrderTypeFields.ShowEpisodes; } set { miscOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"JhPRWESiPYla")]
        //public bool? MiscShowEpisodeExtended { get { return miscOrderTypeFields.ShowEpisodeExtended; } set { miscOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"u5p4teVvGHbQ")]
        //public bool? MiscShowEpisodeDiscountAmount { get { return miscOrderTypeFields.ShowEpisodeDiscountAmount; } set { miscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"xdl5htNFNpXq")]
        public string MiscDateStamp { get { return miscOrderTypeFields.DateStamp; } set { miscOrderTypeFields.DateStamp = value; } }




        //sub-crew fields
        [JsonIgnore]
        [FwLogicProperty(Id:"qimgekhI2KS2")]
        public string SubLaborOrderTypeFieldsId { get { return subLaborOrderTypeFields.OrderTypeFieldsId; } set { poType.SublaborordertypefieldsId = value; subLaborOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"34VI7iBQMCKP")]
        public bool? SubLaborShowOrderNumber { get { return subLaborOrderTypeFields.ShowOrderNumber; } set { subLaborOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"2RGQddadDMUU")]
        //public bool? SubLaborShowRepairOrderNumber { get { return subLaborOrderTypeFields.ShowRepairOrderNumber; } set { subLaborOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"8RU5aMWdjPsT")]
        public bool? SubLaborShowICode { get { return subLaborOrderTypeFields.ShowICode; } set { subLaborOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"sPyXHj97g1TC")]
        public int? SubLaborICodeWidth { get { return subLaborOrderTypeFields.ICodeWidth; } set { subLaborOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"jhE5ZqZRjyb4")]
        public bool? SubLaborShowDescription { get { return subLaborOrderTypeFields.ShowDescription; } set { subLaborOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"Z3PLMPtLeTkS")]
        public int? SubLaborDescriptionWidth { get { return subLaborOrderTypeFields.DescriptionWidth; } set { subLaborOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"B8kGKcssw8xJ")]
        //public bool? SubLaborShowPickDate { get { return subLaborOrderTypeFields.ShowPickDate; } set { subLaborOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"SW6ku2KZ6oKZ")]
        public bool? SubLaborShowFromDate { get { return subLaborOrderTypeFields.ShowFromDate; } set { subLaborOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"C2fat6p30dBU")]
        public bool? SubLaborShowFromTime { get { return subLaborOrderTypeFields.ShowFromTime; } set { subLaborOrderTypeFields.ShowFromTime = value; } }

        [FwLogicProperty(Id:"IF2SOrNh1wjD")]
        public bool? SubLaborShowToDate { get { return subLaborOrderTypeFields.ShowToDate; } set { subLaborOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"ObLSmtoRxl0b")]
        public bool? SubLaborShowToTime { get { return subLaborOrderTypeFields.ShowToTime; } set { subLaborOrderTypeFields.ShowToTime = value; } }

        [FwLogicProperty(Id:"x9H3NpCyEEgi")]
        public bool? SubLaborShowHours { get { return subLaborOrderTypeFields.ShowHours; } set { subLaborOrderTypeFields.ShowHours = value; } }

        [FwLogicProperty(Id:"AiGp9iT0UWOL")]
        public bool? SubLaborShowBillablePeriods { get { return subLaborOrderTypeFields.ShowBillablePeriods; } set { subLaborOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"2KorTA8pAEVd")]
        //public bool? SubLaborShowAvailableQuantity { get { return subLaborOrderTypeFields.ShowAvailableQuantity; } set { subLaborOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"JdJRdB0QdeBF")]
        //public bool? SubLaborShowConflictDate { get { return subLaborOrderTypeFields.ShowConflictDate; } set { subLaborOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"d9YnUj8MS8iX")]
        public bool? SubLaborShowRate { get { return subLaborOrderTypeFields.ShowRate; } set { subLaborOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"Dnlkm1l6mlSc")]
        //public bool? SubLaborShowCost { get { return subLaborOrderTypeFields.ShowCost; } set { subLaborOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"UCXbL6NHi1AT")]
        //public bool? SubLaborShowWeeklyCostExtended { get { return subLaborOrderTypeFields.ShowWeeklyCostExtended; } set { subLaborOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"YzHq39MzU9mD")]
        //public bool? SubLaborShowMonthlyCostExtended { get { return subLaborOrderTypeFields.ShowMonthlyCostExtended; } set { subLaborOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"DgHxycKLXY12")]
        //public bool? SubLaborShowPeriodCostExtended { get { return subLaborOrderTypeFields.ShowPeriodCostExtended; } set { subLaborOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"KdKUvQNUD9Re")]
        //public bool? SubLaborShowDaysPerWeek { get { return subLaborOrderTypeFields.ShowDaysPerWeek; } set { subLaborOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"945cijqA8J9e")]
        public bool? SubLaborShowDiscountPercent { get { return subLaborOrderTypeFields.ShowDiscountPercent; } set { subLaborOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"OBUKlERwfrMK")]
        //public bool? SubLaborShowMarkupPercent { get { return subLaborOrderTypeFields.ShowMarkupPercent; } set { subLaborOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"6lWR9WbeOZDT")]
        //public bool? SubLaborShowMarginPercent { get { return subLaborOrderTypeFields.ShowMarginPercent; } set { subLaborOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"ZGYONDwpt6KJ")]
        public bool? SubLaborShowUnit { get { return subLaborOrderTypeFields.ShowUnit; } set { subLaborOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"NvkOwS9B7wxP")]
        public bool? SubLaborShowUnitDiscountAmount { get { return subLaborOrderTypeFields.ShowUnitDiscountAmount; } set { subLaborOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"de8vzbKOwwdO")]
        public bool? SubLaborShowUnitExtended { get { return subLaborOrderTypeFields.ShowUnitExtended; } set { subLaborOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"9O6AfLehwnUg")]
        public bool? SubLaborShowWeeklyDiscountAmount { get { return subLaborOrderTypeFields.ShowWeeklyDiscountAmount; } set { subLaborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"BNGnCz992U6d")]
        public bool? SubLaborShowWeeklyExtended { get { return subLaborOrderTypeFields.ShowWeeklyExtended; } set { subLaborOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"1DaXkWxHverx")]
        public bool? SubLaborShowMonthlyDiscountAmount { get { return subLaborOrderTypeFields.ShowMonthlyDiscountAmount; } set { subLaborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"nImQ1ulyOBPV")]
        public bool? SubLaborShowMonthlyExtended { get { return subLaborOrderTypeFields.ShowMonthlyExtended; } set { subLaborOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"75w6wI6dj7Vr")]
        public bool? SubLaborShowPeriodDiscountAmount { get { return subLaborOrderTypeFields.ShowPeriodDiscountAmount; } set { subLaborOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"TEUB7O8ox0pq")]
        public bool? SubLaborShowPeriodExtended { get { return subLaborOrderTypeFields.ShowPeriodExtended; } set { subLaborOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"RvSjlBM6V4Gz")]
        //public bool? SubLaborShowVariancePercent { get { return subLaborOrderTypeFields.ShowVariancePercent; } set { subLaborOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"fY8nu0iF1B01")]
        //public bool? SubLaborShowVarianceExtended { get { return subLaborOrderTypeFields.ShowVarianceExtended; } set { subLaborOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"oMfV6sznxGky")]
        public bool? SubLaborShowWarehouse { get { return subLaborOrderTypeFields.ShowWarehouse; } set { subLaborOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"2YV54WVPyCar")]
        public bool? SubLaborShowTaxable { get { return subLaborOrderTypeFields.ShowTaxable; } set { subLaborOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"kiuTsYcGmkHa")]
        public bool? SubLaborShowNotes { get { return subLaborOrderTypeFields.ShowNotes; } set { subLaborOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"mXnhR5zu3P8E")]
        //public bool? SubLaborShowReturnToWarehouse { get { return subLaborOrderTypeFields.ShowReturnToWarehouse; } set { subLaborOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"52qXYyyaUJa8")]
        //public bool? SubLaborShowVehicleNumber { get { return subLaborOrderTypeFields.ShowVehicleNumber; } set { subLaborOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"EZq7nOra3Hot")]
        //public bool? SubLaborShowBarCode { get { return subLaborOrderTypeFields.ShowBarCode; } set { subLaborOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"fUHe3vLjsvMm")]
        //public bool? SubLaborShowSerialNumber { get { return subLaborOrderTypeFields.ShowSerialNumber; } set { subLaborOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"ieuqpuI2cws0")]
        //public bool? SubLaborShowCrewName { get { return subLaborOrderTypeFields.ShowCrewName; } set { subLaborOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"HfGpbWmbxusF")]
        //public bool? SubLaborShowPickTime { get { return subLaborOrderTypeFields.ShowPickTime; } set { subLaborOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"6T4LrsFNjkSL")]
        //public bool? SubLaborShowAvailableQuantityAllWarehouses { get { return subLaborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subLaborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"RhQvg5SYnYzO")]
        //public bool? SubLaborShowConflictDateAllWarehouses { get { return subLaborOrderTypeFields.ShowConflictDateAllWarehouses; } set { subLaborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"7snPsiWbONK6")]
        //public bool? SubLaborShowConsignmentAvailableQuantity { get { return subLaborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subLaborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"5dVnNB7P3jhq")]
        //public bool? SubLaborShowConsignmentConflictDate { get { return subLaborOrderTypeFields.ShowConsignmentConflictDate; } set { subLaborOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"5K8Z4HuAwFqE")]
        //public bool? SubLaborShowConsignmentQuantity { get { return subLaborOrderTypeFields.ShowConsignmentQuantity; } set { subLaborOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"BjMHwQImj28R")]
        //public bool? SubLaborShowInLocationQuantity { get { return subLaborOrderTypeFields.ShowInLocationQuantity; } set { subLaborOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"YikxekgMyoMm")]
        //public bool? SubLaborShowReservedItems { get { return subLaborOrderTypeFields.ShowReservedItems; } set { subLaborOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"82iFT3e9tnCt")]
        //public bool? SubLaborShowWeeksAndDays { get { return subLaborOrderTypeFields.ShowWeeksAndDays; } set { subLaborOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"NxiOjzwMwb1b")]
        //public bool? SubLaborShowMonthsAndDays { get { return subLaborOrderTypeFields.ShowMonthsAndDays; } set { subLaborOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"XTlYknl9oANq")]
        //public bool? SubLaborShowPremiumPercent { get { return subLaborOrderTypeFields.ShowPremiumPercent; } set { subLaborOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"UBC7WMu2e0I5")]
        //public bool? SubLaborShowDepartment { get { return subLaborOrderTypeFields.ShowDepartment; } set { subLaborOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"A6kt62gWh2CN")]
        //public bool? SubLaborShowLocation { get { return subLaborOrderTypeFields.ShowLocation; } set { subLaborOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"EoIVnPJU1eGk")]
        //public bool? SubLaborShowOrderActivity { get { return subLaborOrderTypeFields.ShowOrderActivity; } set { subLaborOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"KNOk6waAHiD3")]
        //public bool? SubLaborShowSubOrderNumber { get { return subLaborOrderTypeFields.ShowSubOrderNumber; } set { subLaborOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"DLn6u0Ph07v3")]
        //public bool? SubLaborShowOrderStatus { get { return subLaborOrderTypeFields.ShowOrderStatus; } set { subLaborOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"VOVt9JFWIlgl")]
        //public bool? SubLaborShowEpisodes { get { return subLaborOrderTypeFields.ShowEpisodes; } set { subLaborOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"RUyuu0ofhTev")]
        //public bool? SubLaborShowEpisodeExtended { get { return subLaborOrderTypeFields.ShowEpisodeExtended; } set { subLaborOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"6Wsk45BDrGZT")]
        //public bool? SubLaborShowEpisodeDiscountAmount { get { return subLaborOrderTypeFields.ShowEpisodeDiscountAmount; } set { subLaborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"N1G99C8ia3aK")]
        public string SubLaborDateStamp { get { return subLaborOrderTypeFields.DateStamp; } set { subLaborOrderTypeFields.DateStamp = value; } }



        //sub-misc fields
        [JsonIgnore]
        [FwLogicProperty(Id:"bYJMqcajrzOH")]
        public string SubMiscOrderTypeFieldsId { get { return subMiscOrderTypeFields.OrderTypeFieldsId; } set { poType.SubmiscordertypefieldsId = value; subMiscOrderTypeFields.OrderTypeFieldsId = value; } }

        [FwLogicProperty(Id:"Ai1gTQgtpOtm")]
        public bool? SubMiscShowOrderNumber { get { return subMiscOrderTypeFields.ShowOrderNumber; } set { subMiscOrderTypeFields.ShowOrderNumber = value; } }

        //[FwLogicProperty(Id:"k5xv75WAr1wA")]
        //public bool? SubMiscShowRepairOrderNumber { get { return subMiscOrderTypeFields.ShowRepairOrderNumber; } set { subMiscOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"3CmrFNmu88tB")]
        public bool? SubMiscShowICode { get { return subMiscOrderTypeFields.ShowICode; } set { subMiscOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"7uwgdZbYQWja")]
        public int? SubMiscICodeWidth { get { return subMiscOrderTypeFields.ICodeWidth; } set { subMiscOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"5F6qdnSXPhfO")]
        public bool? SubMiscShowDescription { get { return subMiscOrderTypeFields.ShowDescription; } set { subMiscOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"e2LZNDgomV6A")]
        public int? SubMiscDescriptionWidth { get { return subMiscOrderTypeFields.DescriptionWidth; } set { subMiscOrderTypeFields.DescriptionWidth = value; } }

        //[FwLogicProperty(Id:"wk6ahdI8uabV")]
        //public bool? SubMiscShowPickDate { get { return subMiscOrderTypeFields.ShowPickDate; } set { subMiscOrderTypeFields.ShowPickDate = value; } }

        [FwLogicProperty(Id:"UVjMFSKTdPwf")]
        public bool? SubMiscShowFromDate { get { return subMiscOrderTypeFields.ShowFromDate; } set { subMiscOrderTypeFields.ShowFromDate = value; } }

        [FwLogicProperty(Id:"9UoFOdnT116M")]
        public bool? SubMiscShowToDate { get { return subMiscOrderTypeFields.ShowToDate; } set { subMiscOrderTypeFields.ShowToDate = value; } }

        [FwLogicProperty(Id:"xBvaZ8OZaLrj")]
        public bool? SubMiscShowBillablePeriods { get { return subMiscOrderTypeFields.ShowBillablePeriods; } set { subMiscOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"vncByydgS0CT")]
        //public bool? SubMiscShowAvailableQuantity { get { return subMiscOrderTypeFields.ShowAvailableQuantity; } set { subMiscOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"kfdP0ZSfga7x")]
        //public bool? SubMiscShowConflictDate { get { return subMiscOrderTypeFields.ShowConflictDate; } set { subMiscOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"rAusqPmB9eIL")]
        public bool? SubMiscShowRate { get { return subMiscOrderTypeFields.ShowRate; } set { subMiscOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"mtis8DlicsaB")]
        //public bool? SubMiscShowCost { get { return subMiscOrderTypeFields.ShowCost; } set { subMiscOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"GhRYVqzMcbh4")]
        //public bool? SubMiscShowWeeklyCostExtended { get { return subMiscOrderTypeFields.ShowWeeklyCostExtended; } set { subMiscOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"bDmmVF5YoujC")]
        //public bool? SubMiscShowMonthlyCostExtended { get { return subMiscOrderTypeFields.ShowMonthlyCostExtended; } set { subMiscOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"bCRSdkE834C8")]
        //public bool? SubMiscShowPeriodCostExtended { get { return subMiscOrderTypeFields.ShowPeriodCostExtended; } set { subMiscOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"1y8grZicTUoK")]
        //public bool? SubMiscShowDaysPerWeek { get { return subMiscOrderTypeFields.ShowDaysPerWeek; } set { subMiscOrderTypeFields.ShowDaysPerWeek = value; } }

        [FwLogicProperty(Id:"FSjMTHbUyd61")]
        public bool? SubMiscShowDiscountPercent { get { return subMiscOrderTypeFields.ShowDiscountPercent; } set { subMiscOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"WpGRT5OeA053")]
        //public bool? SubMiscShowMarkupPercent { get { return subMiscOrderTypeFields.ShowMarkupPercent; } set { subMiscOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"akumcivToprZ")]
        //public bool? SubMiscShowMarginPercent { get { return subMiscOrderTypeFields.ShowMarginPercent; } set { subMiscOrderTypeFields.ShowMarginPercent = value; } }

        [FwLogicProperty(Id:"31mH17P5AS1a")]
        public bool? SubMiscShowUnit { get { return subMiscOrderTypeFields.ShowUnit; } set { subMiscOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"iavPInMNejIL")]
        public bool? SubMiscShowUnitDiscountAmount { get { return subMiscOrderTypeFields.ShowUnitDiscountAmount; } set { subMiscOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"RsTf5FJZr3Ch")]
        public bool? SubMiscShowUnitExtended { get { return subMiscOrderTypeFields.ShowUnitExtended; } set { subMiscOrderTypeFields.ShowUnitExtended = value; } }

        [FwLogicProperty(Id:"CVdftmbujbS2")]
        public bool? SubMiscShowWeeklyDiscountAmount { get { return subMiscOrderTypeFields.ShowWeeklyDiscountAmount; } set { subMiscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        [FwLogicProperty(Id:"DBKZ4L6TcawG")]
        public bool? SubMiscShowWeeklyExtended { get { return subMiscOrderTypeFields.ShowWeeklyExtended; } set { subMiscOrderTypeFields.ShowWeeklyExtended = value; } }

        [FwLogicProperty(Id:"CrF92WRDN7T3")]
        public bool? SubMiscShowMonthlyDiscountAmount { get { return subMiscOrderTypeFields.ShowMonthlyDiscountAmount; } set { subMiscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        [FwLogicProperty(Id:"f9E8D59M2Lo0")]
        public bool? SubMiscShowMonthlyExtended { get { return subMiscOrderTypeFields.ShowMonthlyExtended; } set { subMiscOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"wZTxFYAUW0Ip")]
        public bool? SubMiscShowPeriodDiscountAmount { get { return subMiscOrderTypeFields.ShowPeriodDiscountAmount; } set { subMiscOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"U9Fm0Ocd2f9N")]
        public bool? SubMiscShowPeriodExtended { get { return subMiscOrderTypeFields.ShowPeriodExtended; } set { subMiscOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"akdvrn7SubaN")]
        //public bool? SubMiscShowVariancePercent { get { return subMiscOrderTypeFields.ShowVariancePercent; } set { subMiscOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"D6Yy8J62SOrd")]
        //public bool? SubMiscShowVarianceExtended { get { return subMiscOrderTypeFields.ShowVarianceExtended; } set { subMiscOrderTypeFields.ShowVarianceExtended = value; } }

        [FwLogicProperty(Id:"eJkOg5KvCl4G")]
        public bool? SubMiscShowWarehouse { get { return subMiscOrderTypeFields.ShowWarehouse; } set { subMiscOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"nGilQDtMjSSp")]
        public bool? SubMiscShowTaxable { get { return subMiscOrderTypeFields.ShowTaxable; } set { subMiscOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"bHtqfIU9GXw1")]
        public bool? SubMiscShowNotes { get { return subMiscOrderTypeFields.ShowNotes; } set { subMiscOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"3kkP9i7lBnsY")]
        //public bool? SubMiscShowReturnToWarehouse { get { return subMiscOrderTypeFields.ShowReturnToWarehouse; } set { subMiscOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"Tl51U0Mvugqp")]
        //public bool? SubMiscShowFromTime { get { return subMiscOrderTypeFields.ShowFromTime; } set { subMiscOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"9Z6v4VJr1ddi")]
        //public bool? SubMiscShowToTime { get { return subMiscOrderTypeFields.ShowToTime; } set { subMiscOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"8zgBVaGUMwbu")]
        //public bool? SubMiscShowVehicleNumber { get { return subMiscOrderTypeFields.ShowVehicleNumber; } set { subMiscOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"dsObyGNKlMnY")]
        //public bool? SubMiscShowBarCode { get { return subMiscOrderTypeFields.ShowBarCode; } set { subMiscOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"cA1QL170t7LI")]
        //public bool? SubMiscShowSerialNumber { get { return subMiscOrderTypeFields.ShowSerialNumber; } set { subMiscOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"hg68B5onBaiP")]
        //public bool? SubMiscShowPickTime { get { return subMiscOrderTypeFields.ShowPickTime; } set { subMiscOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"2skj5cPbFtp2")]
        //public bool? SubMiscShowAvailableQuantityAllWarehouses { get { return subMiscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subMiscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"5TbktZAz1fXu")]
        //public bool? SubMiscShowConflictDateAllWarehouses { get { return subMiscOrderTypeFields.ShowConflictDateAllWarehouses; } set { subMiscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"bvPn6Ws6UBxS")]
        //public bool? SubMiscShowConsignmentAvailableQuantity { get { return subMiscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subMiscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"Kd8iyGC8onrX")]
        //public bool? SubMiscShowConsignmentConflictDate { get { return subMiscOrderTypeFields.ShowConsignmentConflictDate; } set { subMiscOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"Hz7ffr4h4rg0")]
        //public bool? SubMiscShowConsignmentQuantity { get { return subMiscOrderTypeFields.ShowConsignmentQuantity; } set { subMiscOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"wsrufvmwkV4X")]
        //public bool? SubMiscShowInLocationQuantity { get { return subMiscOrderTypeFields.ShowInLocationQuantity; } set { subMiscOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"rbJrkIsHf0Sg")]
        //public bool? SubMiscShowReservedItems { get { return subMiscOrderTypeFields.ShowReservedItems; } set { subMiscOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"I4HGuGGr24II")]
        //public bool? SubMiscShowWeeksAndDays { get { return subMiscOrderTypeFields.ShowWeeksAndDays; } set { subMiscOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"1IzsyAnZHpuW")]
        //public bool? SubMiscShowMonthsAndDays { get { return subMiscOrderTypeFields.ShowMonthsAndDays; } set { subMiscOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"g5QslpQ06dU1")]
        //public bool? SubMiscShowPremiumPercent { get { return subMiscOrderTypeFields.ShowPremiumPercent; } set { subMiscOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"z7BjVuf2d6NW")]
        //public bool? SubMiscShowDepartment { get { return subMiscOrderTypeFields.ShowDepartment; } set { subMiscOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"WMeaLEIgXNkW")]
        //public bool? SubMiscShowLocation { get { return subMiscOrderTypeFields.ShowLocation; } set { subMiscOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"weC41hhiLA0C")]
        //public bool? SubMiscShowOrderActivity { get { return subMiscOrderTypeFields.ShowOrderActivity; } set { subMiscOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"NsjV8AMCNHbj")]
        //public bool? SubMiscShowSubOrderNumber { get { return subMiscOrderTypeFields.ShowSubOrderNumber; } set { subMiscOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"fSj3yOUTLQto")]
        //public bool? SubMiscShowOrderStatus { get { return subMiscOrderTypeFields.ShowOrderStatus; } set { subMiscOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"rBkLN752kWRM")]
        //public bool? SubMiscShowEpisodes { get { return subMiscOrderTypeFields.ShowEpisodes; } set { subMiscOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"8EWZrOvKISUp")]
        //public bool? SubMiscShowEpisodeExtended { get { return subMiscOrderTypeFields.ShowEpisodeExtended; } set { subMiscOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"GLU1GfIa1pap")]
        //public bool? SubMiscShowEpisodeDiscountAmount { get { return subMiscOrderTypeFields.ShowEpisodeDiscountAmount; } set { subMiscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"TaPBpmX7azBV")]
        public string SubMiscDateStamp { get { return subMiscOrderTypeFields.DateStamp; } set { subMiscOrderTypeFields.DateStamp = value; } }



        //repairfields
        [JsonIgnore]
        [FwLogicProperty(Id:"Ad5ho9X6IDQ2")]
        public string RepairOrderTypeFieldsId { get { return repairOrderTypeFields.OrderTypeFieldsId; } set { poType.RepairordertypefieldsId = value; repairOrderTypeFields.OrderTypeFieldsId = value; } }

        //[FwLogicProperty(Id:"UpQ5AKtVUPOA")]
        //public bool? RepairShowOrderNumber { get { return repairOrderTypeFields.ShowOrderNumber; } set { repairOrderTypeFields.ShowOrderNumber = value; } }

        [FwLogicProperty(Id:"DNgIAad8DHqv")]
        public bool? RepairShowRepairOrderNumber { get { return repairOrderTypeFields.ShowRepairOrderNumber; } set { repairOrderTypeFields.ShowRepairOrderNumber = value; } }

        [FwLogicProperty(Id:"SISpvHvcog4q")]
        public bool? RepairShowICode { get { return repairOrderTypeFields.ShowICode; } set { repairOrderTypeFields.ShowICode = value; } }

        [FwLogicProperty(Id:"5AsQK2Svp5YN")]
        public int? RepairICodeWidth { get { return repairOrderTypeFields.ICodeWidth; } set { repairOrderTypeFields.ICodeWidth = value; } }

        [FwLogicProperty(Id:"fCSIdpvSxufF")]
        public bool? RepairShowDescription { get { return repairOrderTypeFields.ShowDescription; } set { repairOrderTypeFields.ShowDescription = value; } }

        [FwLogicProperty(Id:"9wh5Z3RtTVZG")]
        public int? RepairDescriptionWidth { get { return repairOrderTypeFields.DescriptionWidth; } set { repairOrderTypeFields.DescriptionWidth = value; } }

        [FwLogicProperty(Id:"pTE9byBZxVFT")]
        public bool? RepairShowPickDate { get { return repairOrderTypeFields.ShowPickDate; } set { repairOrderTypeFields.ShowPickDate = value; } }

        //[FwLogicProperty(Id:"pqoQmqoI9NuP")]
        //public bool? RepairShowFromDate { get { return repairOrderTypeFields.ShowFromDate; } set { repairOrderTypeFields.ShowFromDate = value; } }

        //[FwLogicProperty(Id:"WNEdST6346LT")]
        //public bool? RepairShowToDate { get { return repairOrderTypeFields.ShowToDate; } set { repairOrderTypeFields.ShowToDate = value; } }

        //[FwLogicProperty(Id:"OhOQ55on43eh")]
        //public bool? RepairShowBillablePeriods { get { return repairOrderTypeFields.ShowBillablePeriods; } set { repairOrderTypeFields.ShowBillablePeriods = value; } }

        //[FwLogicProperty(Id:"pS58M4fh8KAt")]
        //public bool? RepairShowSubQuantity { get { return repairOrderTypeFields.ShowSubQuantity; } set { repairOrderTypeFields.ShowSubQuantity = value; } }

        //[FwLogicProperty(Id:"0HkbkbtX2vOH")]
        //public bool? RepairShowAvailableQuantity { get { return repairOrderTypeFields.ShowAvailableQuantity; } set { repairOrderTypeFields.ShowAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"h9LitloViBRM")]
        //public bool? RepairShowConflictDate { get { return repairOrderTypeFields.ShowConflictDate; } set { repairOrderTypeFields.ShowConflictDate = value; } }

        [FwLogicProperty(Id:"uGzEvXtjAf72")]
        public bool? RepairShowRate { get { return repairOrderTypeFields.ShowRate; } set { repairOrderTypeFields.ShowRate = value; } }

        //[FwLogicProperty(Id:"nrL4pVSq4Xmt")]
        //public bool? RepairShowCost { get { return repairOrderTypeFields.ShowCost; } set { repairOrderTypeFields.ShowCost = value; } }

        //[FwLogicProperty(Id:"Gq1cIlxF61mF")]
        //public bool? RepairShowWeeklyCostExtended { get { return repairOrderTypeFields.ShowWeeklyCostExtended; } set { repairOrderTypeFields.ShowWeeklyCostExtended = value; } }

        //[FwLogicProperty(Id:"uHBLyA4DWzCV")]
        //public bool? RepairShowMonthlyCostExtended { get { return repairOrderTypeFields.ShowMonthlyCostExtended; } set { repairOrderTypeFields.ShowMonthlyCostExtended = value; } }

        //[FwLogicProperty(Id:"8JMmKkpXEisH")]
        //public bool? RepairShowPeriodCostExtended { get { return repairOrderTypeFields.ShowPeriodCostExtended; } set { repairOrderTypeFields.ShowPeriodCostExtended = value; } }

        //[FwLogicProperty(Id:"2G2VmViIkXHK")]
        //public bool? RepairShowDaysPerWeek { get { return repairOrderTypeFields.ShowDaysPerWeek; } set { repairOrderTypeFields.ShowDaysPerWeek = value; } }

        //[FwLogicProperty(Id:"KPkAoLbWfni5")]
        //public bool? RepairShowDiscountPercent { get { return repairOrderTypeFields.ShowDiscountPercent; } set { repairOrderTypeFields.ShowDiscountPercent = value; } }

        //[FwLogicProperty(Id:"eTuQhOMga1Ff")]
        //public bool? RepairShowMarkupPercent { get { return repairOrderTypeFields.ShowMarkupPercent; } set { repairOrderTypeFields.ShowMarkupPercent = value; } }

        //[FwLogicProperty(Id:"W1JmVPw1W0di")]
        //public bool? RepairShowMarginPercent { get { return repairOrderTypeFields.ShowMarginPercent; } set { repairOrderTypeFields.ShowMarginPercent = value; } }

        //[FwLogicProperty(Id:"q4xgV0AUVNKa")]
        //public bool? RepairShowSplit { get { return repairOrderTypeFields.ShowSplit; } set { repairOrderTypeFields.ShowSplit = value; } }

        [FwLogicProperty(Id:"uEmUuERppeTJ")]
        public bool? RepairShowUnit { get { return repairOrderTypeFields.ShowUnit; } set { repairOrderTypeFields.ShowUnit = value; } }

        [FwLogicProperty(Id:"DvuwKl7NwIIx")]
        public bool? RepairShowUnitDiscountAmount { get { return repairOrderTypeFields.ShowUnitDiscountAmount; } set { repairOrderTypeFields.ShowUnitDiscountAmount = value; } }

        [FwLogicProperty(Id:"kzjYtMy2SytV")]
        public bool? RepairShowUnitExtended { get { return repairOrderTypeFields.ShowUnitExtended; } set { repairOrderTypeFields.ShowUnitExtended = value; } }

        //[FwLogicProperty(Id:"YhktI1ugfkAO")]
        //public bool? RepairShowWeeklyDiscountAmount { get { return repairOrderTypeFields.ShowWeeklyDiscountAmount; } set { repairOrderTypeFields.ShowWeeklyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"Sa3ehQb197v9")]
        //public bool? RepairShowWeeklyExtended { get { return repairOrderTypeFields.ShowWeeklyExtended; } set { repairOrderTypeFields.ShowWeeklyExtended = value; } }

        //[FwLogicProperty(Id:"nLr1EqoPFjP0")]
        //public bool? RepairShowMonthlyDiscountAmount { get { return repairOrderTypeFields.ShowMonthlyDiscountAmount; } set { repairOrderTypeFields.ShowMonthlyDiscountAmount = value; } }

        //[FwLogicProperty(Id:"Hl9evGlbfy7e")]
        //public bool? RepairShowMonthlyExtended { get { return repairOrderTypeFields.ShowMonthlyExtended; } set { repairOrderTypeFields.ShowMonthlyExtended = value; } }

        [FwLogicProperty(Id:"BVrnzPx9mSWy")]
        public bool? RepairShowPeriodDiscountAmount { get { return repairOrderTypeFields.ShowPeriodDiscountAmount; } set { repairOrderTypeFields.ShowPeriodDiscountAmount = value; } }

        [FwLogicProperty(Id:"iQpFmZ2W6Btr")]
        public bool? RepairShowPeriodExtended { get { return repairOrderTypeFields.ShowPeriodExtended; } set { repairOrderTypeFields.ShowPeriodExtended = value; } }

        //[FwLogicProperty(Id:"knFsnw4WL5uX")]
        //public bool? RepairShowVariancePercent { get { return repairOrderTypeFields.ShowVariancePercent; } set { repairOrderTypeFields.ShowVariancePercent = value; } }

        //[FwLogicProperty(Id:"SnoiMWd21xUS")]
        //public bool? RepairShowVarianceExtended { get { return repairOrderTypeFields.ShowVarianceExtended; } set { repairOrderTypeFields.ShowVarianceExtended = value; } }

        //[FwLogicProperty(Id:"fNBlvUxIrTx9")]
        //public bool? RepairShowCountryOfOrigin { get { return repairOrderTypeFields.ShowCountryOfOrigin; } set { repairOrderTypeFields.ShowCountryOfOrigin = value; } }

        //[FwLogicProperty(Id:"9TfG8pZhSRHZ")]
        //public bool? RepairShowManufacturer { get { return repairOrderTypeFields.ShowManufacturer; } set { repairOrderTypeFields.ShowManufacturer = value; } }

        //[FwLogicProperty(Id:"RTM4THOPUhvU")]
        //public bool? RepairShowManufacturerPartNumber { get { return repairOrderTypeFields.ShowManufacturerPartNumber; } set { repairOrderTypeFields.ShowManufacturerPartNumber = value; } }

        //[FwLogicProperty(Id:"3Bp8aOpi1Tp5")]
        //public int? RepairManufacturerPartNumberWidth { get { return repairOrderTypeFields.ManufacturerPartNumberWidth; } set { repairOrderTypeFields.ManufacturerPartNumberWidth = value; } }

        //[FwLogicProperty(Id:"8lYvVmItMY0N")]
        //public bool? RepairShowModelNumber { get { return repairOrderTypeFields.ShowModelNumber; } set { repairOrderTypeFields.ShowModelNumber = value; } }

        //[FwLogicProperty(Id:"tokNVp8FE9lF")]
        //public bool? RepairShowVendorPartNumber { get { return repairOrderTypeFields.ShowVendorPartNumber; } set { repairOrderTypeFields.ShowVendorPartNumber = value; } }

        [FwLogicProperty(Id:"Re4P7xb3P9j3")]
        public bool? RepairShowWarehouse { get { return repairOrderTypeFields.ShowWarehouse; } set { repairOrderTypeFields.ShowWarehouse = value; } }

        [FwLogicProperty(Id:"5RtKMf3Ys9GS")]
        public bool? RepairShowTaxable { get { return repairOrderTypeFields.ShowTaxable; } set { repairOrderTypeFields.ShowTaxable = value; } }

        [FwLogicProperty(Id:"SmfJtCoUQgOp")]
        public bool? RepairShowNotes { get { return repairOrderTypeFields.ShowNotes; } set { repairOrderTypeFields.ShowNotes = value; } }

        //[FwLogicProperty(Id:"s4xqe7OQQ0q5")]
        //public bool? RepairShowReturnToWarehouse { get { return repairOrderTypeFields.ShowReturnToWarehouse; } set { repairOrderTypeFields.ShowReturnToWarehouse = value; } }

        //[FwLogicProperty(Id:"j8WPge6qLQIV")]
        //public bool? RepairShowFromTime { get { return repairOrderTypeFields.ShowFromTime; } set { repairOrderTypeFields.ShowFromTime = value; } }

        //[FwLogicProperty(Id:"jJbOa0crfZbu")]
        //public bool? RepairShowToTime { get { return repairOrderTypeFields.ShowToTime; } set { repairOrderTypeFields.ShowToTime = value; } }

        //[FwLogicProperty(Id:"yGsJz2Zw3pHX")]
        //public bool? RepairShowVehicleNumber { get { return repairOrderTypeFields.ShowVehicleNumber; } set { repairOrderTypeFields.ShowVehicleNumber = value; } }

        //[FwLogicProperty(Id:"QofTnmQ18ZwZ")]
        //public bool? RepairShowBarCode { get { return repairOrderTypeFields.ShowBarCode; } set { repairOrderTypeFields.ShowBarCode = value; } }

        //[FwLogicProperty(Id:"Gwh5cQmrMwo1")]
        //public bool? RepairShowSerialNumber { get { return repairOrderTypeFields.ShowSerialNumber; } set { repairOrderTypeFields.ShowSerialNumber = value; } }

        //[FwLogicProperty(Id:"2eSxV7V5gy14")]
        //public bool? RepairShowCrewName { get { return repairOrderTypeFields.ShowCrewName; } set { repairOrderTypeFields.ShowCrewName = value; } }

        //[FwLogicProperty(Id:"rEyVAlcuNbyN")]
        //public bool? RepairShowHours { get { return repairOrderTypeFields.ShowHours; } set { repairOrderTypeFields.ShowHours = value; } }

        //[FwLogicProperty(Id:"ZM02FAEPldlj")]
        //public bool? RepairShowPickTime { get { return repairOrderTypeFields.ShowPickTime; } set { repairOrderTypeFields.ShowPickTime = value; } }

        //[FwLogicProperty(Id:"C0uNuJ6G3TJq")]
        //public bool? RepairShowAvailableQuantityAllWarehouses { get { return repairOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { repairOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }

        //[FwLogicProperty(Id:"pndv8DHTnE3Y")]
        //public bool? RepairShowConflictDateAllWarehouses { get { return repairOrderTypeFields.ShowConflictDateAllWarehouses; } set { repairOrderTypeFields.ShowConflictDateAllWarehouses = value; } }

        //[FwLogicProperty(Id:"AckdIeuip3DW")]
        //public bool? RepairShowConsignmentAvailableQuantity { get { return repairOrderTypeFields.ShowConsignmentAvailableQuantity; } set { repairOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }

        //[FwLogicProperty(Id:"MWVGbpZOknh1")]
        //public bool? RepairShowConsignmentConflictDate { get { return repairOrderTypeFields.ShowConsignmentConflictDate; } set { repairOrderTypeFields.ShowConsignmentConflictDate = value; } }

        //[FwLogicProperty(Id:"hihCcVINcDkw")]
        //public bool? RepairShowConsignmentQuantity { get { return repairOrderTypeFields.ShowConsignmentQuantity; } set { repairOrderTypeFields.ShowConsignmentQuantity = value; } }

        //[FwLogicProperty(Id:"WJcHYeXdKT8m")]
        //public bool? RepairShowInLocationQuantity { get { return repairOrderTypeFields.ShowInLocationQuantity; } set { repairOrderTypeFields.ShowInLocationQuantity = value; } }

        //[FwLogicProperty(Id:"qSng1KrtP760")]
        //public bool? RepairShowReservedItems { get { return repairOrderTypeFields.ShowReservedItems; } set { repairOrderTypeFields.ShowReservedItems = value; } }

        //[FwLogicProperty(Id:"Npw0sJNEnXJf")]
        //public bool? RepairShowWeeksAndDays { get { return repairOrderTypeFields.ShowWeeksAndDays; } set { repairOrderTypeFields.ShowWeeksAndDays = value; } }

        //[FwLogicProperty(Id:"S2B6deyMdVYd")]
        //public bool? RepairShowMonthsAndDays { get { return repairOrderTypeFields.ShowMonthsAndDays; } set { repairOrderTypeFields.ShowMonthsAndDays = value; } }

        //[FwLogicProperty(Id:"JtDmZYzqYaoy")]
        //public bool? RepairShowPremiumPercent { get { return repairOrderTypeFields.ShowPremiumPercent; } set { repairOrderTypeFields.ShowPremiumPercent = value; } }

        //[FwLogicProperty(Id:"dHzf2iVTdE4A")]
        //public bool? RepairShowDepartment { get { return repairOrderTypeFields.ShowDepartment; } set { repairOrderTypeFields.ShowDepartment = value; } }

        //[FwLogicProperty(Id:"hyGzU7O2Kwrj")]
        //public bool? RepairShowLocation { get { return repairOrderTypeFields.ShowLocation; } set { repairOrderTypeFields.ShowLocation = value; } }

        //[FwLogicProperty(Id:"hsWqY3tTh5OJ")]
        //public bool? RepairShowOrderActivity { get { return repairOrderTypeFields.ShowOrderActivity; } set { repairOrderTypeFields.ShowOrderActivity = value; } }

        //[FwLogicProperty(Id:"Gqv5vqCXjNsB")]
        //public bool? RepairShowSubOrderNumber { get { return repairOrderTypeFields.ShowSubOrderNumber; } set { repairOrderTypeFields.ShowSubOrderNumber = value; } }

        //[FwLogicProperty(Id:"2XokWKkJ7paZ")]
        //public bool? RepairShowOrderStatus { get { return repairOrderTypeFields.ShowOrderStatus; } set { repairOrderTypeFields.ShowOrderStatus = value; } }

        //[FwLogicProperty(Id:"zquvpKI8LI4z")]
        //public bool? RepairShowEpisodes { get { return repairOrderTypeFields.ShowEpisodes; } set { repairOrderTypeFields.ShowEpisodes = value; } }

        //[FwLogicProperty(Id:"9aMbsmlD5kgN")]
        //public bool? RepairShowEpisodeExtended { get { return repairOrderTypeFields.ShowEpisodeExtended; } set { repairOrderTypeFields.ShowEpisodeExtended = value; } }

        //[FwLogicProperty(Id:"Sf3KIlPVUOLj")]
        //public bool? RepairShowEpisodeDiscountAmount { get { return repairOrderTypeFields.ShowEpisodeDiscountAmount; } set { repairOrderTypeFields.ShowEpisodeDiscountAmount = value; } }

        [FwLogicProperty(Id:"62hgwGw13vD9")]
        public string RepairDateStamp { get { return repairOrderTypeFields.DateStamp; } set { repairOrderTypeFields.DateStamp = value; } }


        [FwLogicProperty(Id:"pj8v92bGa0vz")]
        public bool? RwNetDefaultRental { get { return poType.Rwnetrental; } set { poType.Rwnetrental = value; } }

        [FwLogicProperty(Id:"07hNdhCUnq4c")]
        public bool? RwNetDefaultMisc { get { return poType.Rwnetmisc; } set { poType.Rwnetmisc = value; } }

        [FwLogicProperty(Id:"w4X84GEwVsWA")]
        public bool? RwNetDefaultLabor { get { return poType.Rwnetlabor; } set { poType.Rwnetlabor = value; } }




        [JsonIgnore]
        [FwLogicProperty(Id:"ucH79MHnDyFr")]
        public string OrdType { get { return poType.Ordtype; } set { poType.Ordtype = value; } }

        [FwLogicProperty(Id:"TYnhsId960OD")]
        public decimal? OrderBy { get { return poType.Orderby; } set { poType.Orderby = value; } }

        [FwLogicProperty(Id:"BpVfNU0Ng6dn")]
        public bool? Inactive { get { return poType.Inactive; } set { poType.Inactive = value; } }

        [FwLogicProperty(Id:"8PIu6r8Yu20P")]
        public string DateStamp { get { return poType.DateStamp; } set { poType.DateStamp = value; } }




        [FwLogicProperty(Id:"KKHsFIjLSomQ1", IsReadOnly:true)]
        public List<string> SubRentalShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (SubRentalShowOrderNumber == true) { showFields.Add("PoSubOrderNumber"); }
                if (SubRentalShowICode == true) { showFields.Add("ICode"); }
                if (SubRentalShowDescription == true) { showFields.Add("Description"); }
                if ((!(SubRentalShowICode == true)) && (!(SubRentalShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (SubRentalShowFromDate == true) { showFields.Add("FromDate"); }
                if (SubRentalShowFromTime == true) { showFields.Add("FromTime"); }
                if (SubRentalShowToDate == true) { showFields.Add("ToDate"); }
                if (SubRentalShowToTime == true) { showFields.Add("ToTime"); }
                if (SubRentalShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (SubRentalShowUnit == true) { showFields.Add("Unit"); }
                if (SubRentalShowRate == true) { showFields.Add("Rate"); }
                if (SubRentalShowDaysPerWeek == true) { showFields.Add("DaysPerWeek"); }
                if (SubRentalShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (SubRentalShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (SubRentalShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (SubRentalShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (SubRentalShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (SubRentalShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (SubRentalShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (SubRentalShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (SubRentalShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (SubRentalShowTaxable == true) { showFields.Add("Taxable"); }
                if (SubRentalShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (SubRentalShowNotes == true) { showFields.Add("Notes"); }

                return showFields;
            }
            set { }
        }


        [FwLogicProperty(Id:"qs6tuj2U3RlUR", IsReadOnly:true)]
        public List<string> SubSaleShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (SubSaleShowOrderNumber == true) { showFields.Add("PoSubOrderNumber"); }
                if (SubSaleShowICode == true) { showFields.Add("ICode"); }
                if (SubSaleShowDescription == true) { showFields.Add("Description"); }
                if ((!(SubSaleShowICode == true)) && (!(SubSaleShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                //if (SubSaleShowFromDate == true) { showFields.Add("FromDate"); }
                //if (SubSaleShowFromTime == true) { showFields.Add("FromTime"); }
                if (SubSaleShowUnit == true) { showFields.Add("Unit"); }
                if (SubSaleShowRate == true) { showFields.Add("Rate"); }
                if (SubSaleShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (SubSaleShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (SubSaleShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (SubSaleShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (SubSaleShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (SubSaleShowTaxable == true) { showFields.Add("Taxable"); }
                if (SubSaleShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (SubSaleShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }




        [FwLogicProperty(Id:"0iayZPZhhPsEp", IsReadOnly:true)]
        public List<string> SubMiscShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (SubMiscShowOrderNumber == true) { showFields.Add("PoSubOrderNumber"); }
                if (SubMiscShowICode == true) { showFields.Add("ICode"); }
                if (SubMiscShowDescription == true) { showFields.Add("Description"); }
                if ((!(SubMiscShowICode == true)) && (!(SubMiscShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (SubMiscShowFromDate == true) { showFields.Add("FromDate"); }
                //if (SubMiscShowFromTime == true) { showFields.Add("FromTime"); }
                if (SubMiscShowToDate == true) { showFields.Add("ToDate"); }
                //if (SubMiscShowToTime == true) { showFields.Add("ToTime"); }
                if (SubMiscShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (SubMiscShowUnit == true) { showFields.Add("Unit"); }
                if (SubMiscShowRate == true) { showFields.Add("Rate"); }
                if (SubMiscShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (SubMiscShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (SubMiscShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (SubMiscShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (SubMiscShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (SubMiscShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (SubMiscShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (SubMiscShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (SubMiscShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (SubMiscShowTaxable == true) { showFields.Add("Taxable"); }
                if (SubMiscShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (SubMiscShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }



        [FwLogicProperty(Id:"AGzMkZ9u03Cuy", IsReadOnly:true)]
        public List<string> SubLaborShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (SubLaborShowOrderNumber == true) { showFields.Add("PoSubOrderNumber"); }
                if (SubLaborShowICode == true) { showFields.Add("ICode"); }
                if (SubLaborShowDescription == true) { showFields.Add("Description"); }
                if ((!(SubLaborShowICode == true)) && (!(SubLaborShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (SubLaborShowFromDate == true) { showFields.Add("FromDate"); }
                if (SubLaborShowFromTime == true) { showFields.Add("FromTime"); }
                if (SubLaborShowToDate == true) { showFields.Add("ToDate"); }
                if (SubLaborShowToTime == true) { showFields.Add("ToTime"); }
                if (SubLaborShowHours == true) { showFields.Add("Hours"); }
                if (SubLaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (SubLaborShowUnit == true) { showFields.Add("Unit"); }
                if (SubLaborShowRate == true) { showFields.Add("Rate"); }
                if (SubLaborShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (SubLaborShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (SubLaborShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (SubLaborShowWeeklyDiscountAmount == true) { showFields.Add("WeeklyDiscountAmount"); }
                if (SubLaborShowWeeklyExtended == true) { showFields.Add("WeeklyExtended"); }
                if (SubLaborShowMonthlyDiscountAmount == true) { showFields.Add("MonthlyDiscountAmount"); }
                if (SubLaborShowMonthlyExtended == true) { showFields.Add("MonthlyExtended"); }
                if (SubLaborShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (SubLaborShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (SubLaborShowTaxable == true) { showFields.Add("Taxable"); }
                if (SubLaborShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (SubLaborShowNotes == true) { showFields.Add("Notes"); }

                return showFields;
            }
            set { }
        }


        [FwLogicProperty(Id:"tsSpebZH3Lx2o", IsReadOnly:true)]
        public List<string> PurchaseShowFields

        {
            get
            {
                List<string> showFields = new List<string>();


                if (PurchaseShowICode == true) { showFields.Add("ICode"); }
                if (PurchaseShowDescription == true) { showFields.Add("Description"); }
                if ((!(PurchaseShowICode == true)) && (!(PurchaseShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (PurchaseShowUnit == true) { showFields.Add("Unit"); }
                if (PurchaseShowRate == true) { showFields.Add("Rate"); }
                if (PurchaseShowDiscountPercent == true) { showFields.Add("DiscountPercent"); }
                if (PurchaseShowUnitDiscountAmount == true) { showFields.Add("UnitDiscountAmount"); }
                if (PurchaseShowUnitExtended == true) { showFields.Add("UnitExtended"); }
                if (PurchaseShowPeriodDiscountAmount == true) { showFields.Add("PeriodDiscountAmount"); }
                if (PurchaseShowPeriodExtended == true) { showFields.Add("PeriodExtended"); }
                if (PurchaseShowManufacturerPartNumber == true) { showFields.Add("ManufacturerPartNumber"); }
                if (PurchaseShowTaxable == true) { showFields.Add("Taxable"); }
                if (PurchaseShowWarehouse == true) { showFields.Add("Warehouse"); }
                if (PurchaseShowNotes == true) { showFields.Add("Notes"); }


                return showFields;
            }
            set { }
        }




        [FwLogicProperty(Id:"0iayZPZhhPsEp", IsReadOnly:true)]
        public List<string> MiscShowFields

        {
            get
            {
                List<string> showFields = new List<string>();

                if (MiscShowICode == true) { showFields.Add("ICode"); }
                if (MiscShowDescription == true) { showFields.Add("Description"); }
                if ((!(MiscShowICode == true)) && (!(MiscShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (MiscShowFromDate == true) { showFields.Add("FromDate"); }
                //if (MiscShowFromTime == true) { showFields.Add("FromTime"); }
                if (MiscShowToDate == true) { showFields.Add("ToDate"); }
                //if (MiscShowToTime == true) { showFields.Add("ToTime"); }
                if (MiscShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
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



        [FwLogicProperty(Id:"AGzMkZ9u03Cuy", IsReadOnly:true)]
        public List<string> LaborShowFields

        {
            get
            {
                List<string> showFields = new List<string>();
                
                if (LaborShowICode == true) { showFields.Add("ICode"); }
                if (LaborShowDescription == true) { showFields.Add("Description"); }
                if ((!(LaborShowICode == true)) && (!(LaborShowDescription == true))) { showFields.Add("ICode"); }
                showFields.Add("QuantityOrdered");
                if (LaborShowFromDate == true) { showFields.Add("FromDate"); }
                if (LaborShowFromTime == true) { showFields.Add("FromTime"); }
                if (LaborShowToDate == true) { showFields.Add("ToDate"); }
                if (LaborShowToTime == true) { showFields.Add("ToTime"); }
                if (LaborShowBillablePeriods == true) { showFields.Add("BillablePeriods"); }
                if (LaborShowUnit == true) { showFields.Add("Unit"); }
                if (LaborShowRate == true) { showFields.Add("Rate"); }
                if (LaborShowHours == true) { showFields.Add("Hours"); }
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


        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            OrdType = "PO";
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    PoTypeLogic orig = ((PoTypeLogic)e.Original);
                    purchaseOrderTypeFields.OrderTypeFieldsId = orig.PurchaseOrderTypeFieldsId;
                    subRentalOrderTypeFields.OrderTypeFieldsId = orig.SubRentalOrderTypeFieldsId;
                    subSaleOrderTypeFields.OrderTypeFieldsId = orig.SubSaleOrderTypeFieldsId;
                    laborOrderTypeFields.OrderTypeFieldsId = orig.LaborOrderTypeFieldsId;
                    subLaborOrderTypeFields.OrderTypeFieldsId = orig.SubLaborOrderTypeFieldsId;
                    miscOrderTypeFields.OrderTypeFieldsId = orig.MiscOrderTypeFieldsId;
                    subMiscOrderTypeFields.OrderTypeFieldsId = orig.SubMiscOrderTypeFieldsId;
                    repairOrderTypeFields.OrderTypeFieldsId = orig.RepairOrderTypeFieldsId;
                }
            }
        }
        //------------------------------------------------------------------------------------   
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                PurchaseOrderTypeFieldsId = purchaseOrderTypeFields.OrderTypeFieldsId;
                SubRentalOrderTypeFieldsId = subRentalOrderTypeFields.OrderTypeFieldsId;
                SubSaleOrderTypeFieldsId = subSaleOrderTypeFields.OrderTypeFieldsId;
                LaborOrderTypeFieldsId = laborOrderTypeFields.OrderTypeFieldsId;
                SubLaborOrderTypeFieldsId = subLaborOrderTypeFields.OrderTypeFieldsId;
                MiscOrderTypeFieldsId = miscOrderTypeFields.OrderTypeFieldsId;
                SubMiscOrderTypeFieldsId = subMiscOrderTypeFields.OrderTypeFieldsId;
                RepairOrderTypeFieldsId = repairOrderTypeFields.OrderTypeFieldsId;
                int i = SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------   
    }
}
