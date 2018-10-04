using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi.Modules.Settings.OrderType;
using WebApi.Modules.Settings.OrderTypeFields;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.PoType
{
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

            poType.AfterSave += OnAfterSavePoType;
            repairOrderTypeFields.AfterSave += OnAfterSaveRepairFields;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoTypeId { get { return poType.OrderTypeId; } set { poType.OrderTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoType { get { return poType.OrderType; } set { poType.OrderType = value; } }

        public bool? ApprovalNeededByRequired { get { return poType.Poapprovebyrequired; } set { poType.Poapprovebyrequired = value; } }
        public bool? ImportanceRequired { get { return poType.Poimportancerequired; } set { poType.Poimportancerequired = value; } }
        public bool? PayTypeRequired { get { return poType.Popaytyperequired; } set { poType.Popaytyperequired = value; } }
        public bool? ProjectRequired { get { return poType.Poprojectrequired; } set { poType.Poprojectrequired = value; } }

        //sub rental fields
        [JsonIgnore]
        public string SubRentalOrderTypeFieldsId { get { return subRentalOrderTypeFields.OrderTypeFieldsId; } set { subRentalOrderTypeFields.OrderTypeFieldsId = value; } }
        public bool? SubRentalShowOrderNumber { get { return subRentalOrderTypeFields.ShowOrderNumber; } set { subRentalOrderTypeFields.ShowOrderNumber = value; } }
        public bool? SubRentalShowRepairOrderNumber { get { return subRentalOrderTypeFields.ShowRepairOrderNumber; } set { subRentalOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? SubRentalShowICode { get { return subRentalOrderTypeFields.ShowICode; } set { subRentalOrderTypeFields.ShowICode = value; } }
        public int? SubRentalICodeWidth { get { return subRentalOrderTypeFields.ICodeWidth; } set { subRentalOrderTypeFields.ICodeWidth = value; } }
        public bool? SubRentalShowDescription { get { return subRentalOrderTypeFields.ShowDescription; } set { subRentalOrderTypeFields.ShowDescription = value; } }
        public int? SubRentalDescriptionWidth { get { return subRentalOrderTypeFields.DescriptionWidth; } set { subRentalOrderTypeFields.DescriptionWidth = value; } }
        //public bool? SubRentalShowPickDate { get { return subRentalOrderTypeFields.ShowPickDate; } set { subRentalOrderTypeFields.ShowPickDate = value; } }
        public bool? SubRentalShowFromDate { get { return subRentalOrderTypeFields.ShowFromDate; } set { subRentalOrderTypeFields.ShowFromDate = value; } }
        public bool? SubRentalShowFromTime { get { return subRentalOrderTypeFields.ShowFromTime; } set { subRentalOrderTypeFields.ShowFromTime = value; } }
        public bool? SubRentalShowToDate { get { return subRentalOrderTypeFields.ShowToDate; } set { subRentalOrderTypeFields.ShowToDate = value; } }
        public bool? SubRentalShowToTime { get { return subRentalOrderTypeFields.ShowToTime; } set { subRentalOrderTypeFields.ShowToTime = value; } }
        public bool? SubRentalShowBillablePeriods { get { return subRentalOrderTypeFields.ShowBillablePeriods; } set { subRentalOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? SubRentalShowAvailableQuantity { get { return subRentalOrderTypeFields.ShowAvailableQuantity; } set { subRentalOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? SubRentalShowConflictDate { get { return subRentalOrderTypeFields.ShowConflictDate; } set { subRentalOrderTypeFields.ShowConflictDate = value; } }
        public bool? SubRentalShowRate { get { return subRentalOrderTypeFields.ShowRate; } set { subRentalOrderTypeFields.ShowRate = value; } }
        //public bool? SubRentalShowCost { get { return subRentalOrderTypeFields.ShowCost; } set { subRentalOrderTypeFields.ShowCost = value; } }
        //public bool? SubRentalShowWeeklyCostExtended { get { return subRentalOrderTypeFields.ShowWeeklyCostExtended; } set { subRentalOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? SubRentalShowMonthlyCostExtended { get { return subRentalOrderTypeFields.ShowMonthlyCostExtended; } set { subRentalOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? SubRentalShowPeriodCostExtended { get { return subRentalOrderTypeFields.ShowPeriodCostExtended; } set { subRentalOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool? SubRentalShowDaysPerWeek { get { return subRentalOrderTypeFields.ShowDaysPerWeek; } set { subRentalOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? SubRentalShowDiscountPercent { get { return subRentalOrderTypeFields.ShowDiscountPercent; } set { subRentalOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? SubRentalShowMarkupPercent { get { return subRentalOrderTypeFields.ShowMarkupPercent; } set { subRentalOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? SubRentalShowMarginPercent { get { return subRentalOrderTypeFields.ShowMarginPercent; } set { subRentalOrderTypeFields.ShowMarginPercent = value; } }
        public bool? SubRentalShowUnit { get { return subRentalOrderTypeFields.ShowUnit; } set { subRentalOrderTypeFields.ShowUnit = value; } }
        public bool? SubRentalShowUnitDiscountAmount { get { return subRentalOrderTypeFields.ShowUnitDiscountAmount; } set { subRentalOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? SubRentalShowUnitExtended { get { return subRentalOrderTypeFields.ShowUnitExtended; } set { subRentalOrderTypeFields.ShowUnitExtended = value; } }
        public bool? SubRentalShowWeeklyDiscountAmount { get { return subRentalOrderTypeFields.ShowWeeklyDiscountAmount; } set { subRentalOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool? SubRentalShowWeeklyExtended { get { return subRentalOrderTypeFields.ShowWeeklyExtended; } set { subRentalOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool? SubRentalShowMonthlyDiscountAmount { get { return subRentalOrderTypeFields.ShowMonthlyDiscountAmount; } set { subRentalOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool? SubRentalShowMonthlyExtended { get { return subRentalOrderTypeFields.ShowMonthlyExtended; } set { subRentalOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? SubRentalShowPeriodDiscountAmount { get { return subRentalOrderTypeFields.ShowPeriodDiscountAmount; } set { subRentalOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? SubRentalShowPeriodExtended { get { return subRentalOrderTypeFields.ShowPeriodExtended; } set { subRentalOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? SubRentalShowVariancePercent { get { return subRentalOrderTypeFields.ShowVariancePercent; } set { subRentalOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? SubRentalShowVarianceExtended { get { return subRentalOrderTypeFields.ShowVarianceExtended; } set { subRentalOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? SubRentalShowWarehouse { get { return subRentalOrderTypeFields.ShowWarehouse; } set { subRentalOrderTypeFields.ShowWarehouse = value; } }
        public bool? SubRentalShowTaxable { get { return subRentalOrderTypeFields.ShowTaxable; } set { subRentalOrderTypeFields.ShowTaxable = value; } }
        public bool? SubRentalShowNotes { get { return subRentalOrderTypeFields.ShowNotes; } set { subRentalOrderTypeFields.ShowNotes = value; } }
        //public bool? SubRentalShowReturnToWarehouse { get { return subRentalOrderTypeFields.ShowReturnToWarehouse; } set { subRentalOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? SubRentalShowVehicleNumber { get { return subRentalOrderTypeFields.ShowVehicleNumber; } set { subRentalOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? SubRentalShowBarCode { get { return subRentalOrderTypeFields.ShowBarCode; } set { subRentalOrderTypeFields.ShowBarCode = value; } }
        //public bool? SubRentalShowSerialNumber { get { return subRentalOrderTypeFields.ShowSerialNumber; } set { subRentalOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? SubRentalShowPickTime { get { return subRentalOrderTypeFields.ShowPickTime; } set { subRentalOrderTypeFields.ShowPickTime = value; } }
        //public bool? SubRentalShowAvailableQuantityAllWarehouses { get { return subRentalOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subRentalOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? SubRentalShowConflictDateAllWarehouses { get { return subRentalOrderTypeFields.ShowConflictDateAllWarehouses; } set { subRentalOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? SubRentalShowConsignmentAvailableQuantity { get { return subRentalOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subRentalOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? SubRentalShowConsignmentConflictDate { get { return subRentalOrderTypeFields.ShowConsignmentConflictDate; } set { subRentalOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? SubRentalShowConsignmentQuantity { get { return subRentalOrderTypeFields.ShowConsignmentQuantity; } set { subRentalOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? SubRentalShowInLocationQuantity { get { return subRentalOrderTypeFields.ShowInLocationQuantity; } set { subRentalOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? SubRentalShowReservedItems { get { return subRentalOrderTypeFields.ShowReservedItems; } set { subRentalOrderTypeFields.ShowReservedItems = value; } }
        //public bool? SubRentalShowWeeksAndDays { get { return subRentalOrderTypeFields.ShowWeeksAndDays; } set { subRentalOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? SubRentalShowMonthsAndDays { get { return subRentalOrderTypeFields.ShowMonthsAndDays; } set { subRentalOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? SubRentalShowPremiumPercent { get { return subRentalOrderTypeFields.ShowPremiumPercent; } set { subRentalOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? SubRentalShowDepartment { get { return subRentalOrderTypeFields.ShowDepartment; } set { subRentalOrderTypeFields.ShowDepartment = value; } }
        //public bool? SubRentalShowLocation { get { return subRentalOrderTypeFields.ShowLocation; } set { subRentalOrderTypeFields.ShowLocation = value; } }
        //public bool? SubRentalShowOrderActivity { get { return subRentalOrderTypeFields.ShowOrderActivity; } set { subRentalOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? SubRentalShowSubOrderNumber { get { return subRentalOrderTypeFields.ShowSubOrderNumber; } set { subRentalOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? SubRentalShowOrderStatus { get { return subRentalOrderTypeFields.ShowOrderStatus; } set { subRentalOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? SubRentalShowEpisodes { get { return subRentalOrderTypeFields.ShowEpisodes; } set { subRentalOrderTypeFields.ShowEpisodes = value; } }
        //public bool? SubRentalShowEpisodeExtended { get { return subRentalOrderTypeFields.ShowEpisodeExtended; } set { subRentalOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? SubRentalShowEpisodeDiscountAmount { get { return subRentalOrderTypeFields.ShowEpisodeDiscountAmount; } set { subRentalOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string SubRentalDateStamp { get { return subRentalOrderTypeFields.DateStamp; } set { subRentalOrderTypeFields.DateStamp = value; } }

        //sub sales fields
        [JsonIgnore]
        public string SubSaleOrderTypeFieldsId { get { return subSaleOrderTypeFields.OrderTypeFieldsId; } set { subSaleOrderTypeFields.OrderTypeFieldsId = value; } }
        public bool? SubSaleShowOrderNumber { get { return subSaleOrderTypeFields.ShowOrderNumber; } set { subSaleOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? SubSaleShowRepairOrderNumber { get { return subSaleOrderTypeFields.ShowRepairOrderNumber; } set { subSaleOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? SubSaleShowICode { get { return subSaleOrderTypeFields.ShowICode; } set { subSaleOrderTypeFields.ShowICode = value; } }
        public int? SubSaleICodeWidth { get { return subSaleOrderTypeFields.ICodeWidth; } set { subSaleOrderTypeFields.ICodeWidth = value; } }
        public bool? SubSaleShowDescription { get { return subSaleOrderTypeFields.ShowDescription; } set { subSaleOrderTypeFields.ShowDescription = value; } }
        public int? SubSaleDescriptionWidth { get { return subSaleOrderTypeFields.DescriptionWidth; } set { subSaleOrderTypeFields.DescriptionWidth = value; } }
        //public bool? SubSaleShowPickDate { get { return subSaleOrderTypeFields.ShowPickDate; } set { subSaleOrderTypeFields.ShowPickDate = value; } }
        //public bool? SubSaleShowFromDate { get { return subSaleOrderTypeFields.ShowFromDate; } set { subSaleOrderTypeFields.ShowFromDate = value; } }
        //public bool? SubSaleShowToDate { get { return subSaleOrderTypeFields.ShowToDate; } set { subSaleOrderTypeFields.ShowToDate = value; } }
        //public bool? SubSaleShowBillablePeriods { get { return subSaleOrderTypeFields.ShowBillablePeriods; } set { subSaleOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? SubSaleShowAvailableQuantity { get { return subSaleOrderTypeFields.ShowAvailableQuantity; } set { subSaleOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? SubSaleShowConflictDate { get { return subSaleOrderTypeFields.ShowConflictDate; } set { subSaleOrderTypeFields.ShowConflictDate = value; } }
        public bool? SubSaleShowRate { get { return subSaleOrderTypeFields.ShowRate; } set { subSaleOrderTypeFields.ShowRate = value; } }
        //public bool? SubSaleShowCost { get { return subSaleOrderTypeFields.ShowCost; } set { subSaleOrderTypeFields.ShowCost = value; } }
        //public bool? SubSaleShowWeeklyCostExtended { get { return subSaleOrderTypeFields.ShowWeeklyCostExtended; } set { subSaleOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? SubSaleShowMonthlyCostExtended { get { return subSaleOrderTypeFields.ShowMonthlyCostExtended; } set { subSaleOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? SubSaleShowPeriodCostExtended { get { return subSaleOrderTypeFields.ShowPeriodCostExtended; } set { subSaleOrderTypeFields.ShowPeriodCostExtended = value; } }
        public bool? SubSaleShowDiscountPercent { get { return subSaleOrderTypeFields.ShowDiscountPercent; } set { subSaleOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? SubSaleShowMarkupPercent { get { return subSaleOrderTypeFields.ShowMarkupPercent; } set { subSaleOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? SubSaleShowMarginPercent { get { return subSaleOrderTypeFields.ShowMarginPercent; } set { subSaleOrderTypeFields.ShowMarginPercent = value; } }
        public bool? SubSaleShowUnit { get { return subSaleOrderTypeFields.ShowUnit; } set { subSaleOrderTypeFields.ShowUnit = value; } }
        public bool? SubSaleShowUnitDiscountAmount { get { return subSaleOrderTypeFields.ShowUnitDiscountAmount; } set { subSaleOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? SubSaleShowUnitExtended { get { return subSaleOrderTypeFields.ShowUnitExtended; } set { subSaleOrderTypeFields.ShowUnitExtended = value; } }
        //public bool? SubSaleShowWeeklyDiscountAmount { get { return subSaleOrderTypeFields.ShowWeeklyDiscountAmount; } set { subSaleOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool? SubSaleShowWeeklyExtended { get { return subSaleOrderTypeFields.ShowWeeklyExtended; } set { subSaleOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool? SubSaleShowMonthlyDiscountAmount { get { return subSaleOrderTypeFields.ShowMonthlyDiscountAmount; } set { subSaleOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool? SubSaleShowMonthlyExtended { get { return subSaleOrderTypeFields.ShowMonthlyExtended; } set { subSaleOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? SubSaleShowPeriodDiscountAmount { get { return subSaleOrderTypeFields.ShowPeriodDiscountAmount; } set { subSaleOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? SubSaleShowPeriodExtended { get { return subSaleOrderTypeFields.ShowPeriodExtended; } set { subSaleOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? SubSaleShowVariancePercent { get { return subSaleOrderTypeFields.ShowVariancePercent; } set { subSaleOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? SubSaleShowVarianceExtended { get { return subSaleOrderTypeFields.ShowVarianceExtended; } set { subSaleOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? SubSaleShowWarehouse { get { return subSaleOrderTypeFields.ShowWarehouse; } set { subSaleOrderTypeFields.ShowWarehouse = value; } }
        public bool? SubSaleShowTaxable { get { return subSaleOrderTypeFields.ShowTaxable; } set { subSaleOrderTypeFields.ShowTaxable = value; } }
        public bool? SubSaleShowNotes { get { return subSaleOrderTypeFields.ShowNotes; } set { subSaleOrderTypeFields.ShowNotes = value; } }
        //public bool? SubSaleShowReturnToWarehouse { get { return subSaleOrderTypeFields.ShowReturnToWarehouse; } set { subSaleOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? SubSaleShowFromTime { get { return subSaleOrderTypeFields.ShowFromTime; } set { subSaleOrderTypeFields.ShowFromTime = value; } }
        //public bool? SubSaleShowToTime { get { return subSaleOrderTypeFields.ShowToTime; } set { subSaleOrderTypeFields.ShowToTime = value; } }
        //public bool? SubSaleShowVehicleNumber { get { return subSaleOrderTypeFields.ShowVehicleNumber; } set { subSaleOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? SubSaleShowPickTime { get { return subSaleOrderTypeFields.ShowPickTime; } set { subSaleOrderTypeFields.ShowPickTime = value; } }
        //public bool? SubSaleShowAvailableQuantityAllWarehouses { get { return subSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subSaleOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? SubSaleShowConflictDateAllWarehouses { get { return subSaleOrderTypeFields.ShowConflictDateAllWarehouses; } set { subSaleOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? SubSaleShowConsignmentAvailableQuantity { get { return subSaleOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subSaleOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? SubSaleShowConsignmentConflictDate { get { return subSaleOrderTypeFields.ShowConsignmentConflictDate; } set { subSaleOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? SubSaleShowConsignmentQuantity { get { return subSaleOrderTypeFields.ShowConsignmentQuantity; } set { subSaleOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? SubSaleShowInLocationQuantity { get { return subSaleOrderTypeFields.ShowInLocationQuantity; } set { subSaleOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? SubSaleShowReservedItems { get { return subSaleOrderTypeFields.ShowReservedItems; } set { subSaleOrderTypeFields.ShowReservedItems = value; } }
        //public bool? SubSaleShowWeeksAndDays { get { return subSaleOrderTypeFields.ShowWeeksAndDays; } set { subSaleOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? SubSaleShowMonthsAndDays { get { return subSaleOrderTypeFields.ShowMonthsAndDays; } set { subSaleOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? SubSaleShowPremiumPercent { get { return subSaleOrderTypeFields.ShowPremiumPercent; } set { subSaleOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? SubSaleShowDepartment { get { return subSaleOrderTypeFields.ShowDepartment; } set { subSaleOrderTypeFields.ShowDepartment = value; } }
        //public bool? SubSaleShowLocation { get { return subSaleOrderTypeFields.ShowLocation; } set { subSaleOrderTypeFields.ShowLocation = value; } }
        //public bool? SubSaleShowOrderActivity { get { return subSaleOrderTypeFields.ShowOrderActivity; } set { subSaleOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? SubSaleShowSubOrderNumber { get { return subSaleOrderTypeFields.ShowSubOrderNumber; } set { subSaleOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? SubSaleShowOrderStatus { get { return subSaleOrderTypeFields.ShowOrderStatus; } set { subSaleOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? SubSaleShowEpisodes { get { return subSaleOrderTypeFields.ShowEpisodes; } set { subSaleOrderTypeFields.ShowEpisodes = value; } }
        //public bool? SubSaleShowEpisodeExtended { get { return subSaleOrderTypeFields.ShowEpisodeExtended; } set { subSaleOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? SubSaleShowEpisodeDiscountAmount { get { return subSaleOrderTypeFields.ShowEpisodeDiscountAmount; } set { subSaleOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string SubSaleDateStamp { get { return subSaleOrderTypeFields.DateStamp; } set { subSaleOrderTypeFields.DateStamp = value; } }


        //purchase fields
        [JsonIgnore]
        public string PurchaseOrderTypeFieldsId { get { return purchaseOrderTypeFields.OrderTypeFieldsId; } set { purchaseOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool? PurchaseShowOrderNumber { get { return purchaseOrderTypeFields.ShowOrderNumber; } set { purchaseOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? PurchaseShowRepairOrderNumber { get { return purchaseOrderTypeFields.ShowRepairOrderNumber; } set { purchaseOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? PurchaseShowICode { get { return purchaseOrderTypeFields.ShowICode; } set { purchaseOrderTypeFields.ShowICode = value; } }
        public int? PurchaseICodeWidth { get { return purchaseOrderTypeFields.ICodeWidth; } set { purchaseOrderTypeFields.ICodeWidth = value; } }
        public bool? PurchaseShowDescription { get { return purchaseOrderTypeFields.ShowDescription; } set { purchaseOrderTypeFields.ShowDescription = value; } }
        public int? PurchaseDescriptionWidth { get { return purchaseOrderTypeFields.DescriptionWidth; } set { purchaseOrderTypeFields.DescriptionWidth = value; } }
        //public bool? PurchaseShowPickDate { get { return purchaseOrderTypeFields.ShowPickDate; } set { purchaseOrderTypeFields.ShowPickDate = value; } }
        //public bool? PurchaseShowFromDate { get { return purchaseOrderTypeFields.ShowFromDate; } set { purchaseOrderTypeFields.ShowFromDate = value; } }
        //public bool? PurchaseShowToDate { get { return purchaseOrderTypeFields.ShowToDate; } set { purchaseOrderTypeFields.ShowToDate = value; } }
        //public bool? PurchaseShowBillablePeriods { get { return purchaseOrderTypeFields.ShowBillablePeriods; } set { purchaseOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? PurchaseShowSubQuantity { get { return purchaseOrderTypeFields.ShowSubQuantity; } set { purchaseOrderTypeFields.ShowSubQuantity = value; } }
        //public bool? PurchaseShowAvailableQuantity { get { return purchaseOrderTypeFields.ShowAvailableQuantity; } set { purchaseOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? PurchaseShowConflictDate { get { return purchaseOrderTypeFields.ShowConflictDate; } set { purchaseOrderTypeFields.ShowConflictDate = value; } }
        public bool? PurchaseShowRate { get { return purchaseOrderTypeFields.ShowRate; } set { purchaseOrderTypeFields.ShowRate = value; } }
        //public bool? PurchaseShowCost { get { return purchaseOrderTypeFields.ShowCost; } set { purchaseOrderTypeFields.ShowCost = value; } }
        //public bool? PurchaseShowWeeklyCostExtended { get { return purchaseOrderTypeFields.ShowWeeklyCostExtended; } set { purchaseOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? PurchaseShowMonthlyCostExtended { get { return purchaseOrderTypeFields.ShowMonthlyCostExtended; } set { purchaseOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? PurchaseShowPeriodCostExtended { get { return purchaseOrderTypeFields.ShowPeriodCostExtended; } set { purchaseOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? PurchaseShowDaysPerWeek { get { return purchaseOrderTypeFields.ShowDaysPerWeek; } set { purchaseOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? PurchaseShowDiscountPercent { get { return purchaseOrderTypeFields.ShowDiscountPercent; } set { purchaseOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? PurchaseShowMarkupPercent { get { return purchaseOrderTypeFields.ShowMarkupPercent; } set { purchaseOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? PurchaseShowMarginPercent { get { return purchaseOrderTypeFields.ShowMarginPercent; } set { purchaseOrderTypeFields.ShowMarginPercent = value; } }
        //public bool? PurchaseShowSplit { get { return purchaseOrderTypeFields.ShowSplit; } set { purchaseOrderTypeFields.ShowSplit = value; } }
        public bool? PurchaseShowUnit { get { return purchaseOrderTypeFields.ShowUnit; } set { purchaseOrderTypeFields.ShowUnit = value; } }
        public bool? PurchaseShowUnitDiscountAmount { get { return purchaseOrderTypeFields.ShowUnitDiscountAmount; } set { purchaseOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? PurchaseShowUnitExtended { get { return purchaseOrderTypeFields.ShowUnitExtended; } set { purchaseOrderTypeFields.ShowUnitExtended = value; } }
        //public bool? PurchaseShowWeeklyDiscountAmount { get { return purchaseOrderTypeFields.ShowWeeklyDiscountAmount; } set { purchaseOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool? PurchaseShowWeeklyExtended { get { return purchaseOrderTypeFields.ShowWeeklyExtended; } set { purchaseOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool? PurchaseShowMonthlyDiscountAmount { get { return purchaseOrderTypeFields.ShowMonthlyDiscountAmount; } set { purchaseOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool? PurchaseShowMonthlyExtended { get { return purchaseOrderTypeFields.ShowMonthlyExtended; } set { purchaseOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? PurchaseShowPeriodDiscountAmount { get { return purchaseOrderTypeFields.ShowPeriodDiscountAmount; } set { purchaseOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? PurchaseShowPeriodExtended { get { return purchaseOrderTypeFields.ShowPeriodExtended; } set { purchaseOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? PurchaseShowVariancePercent { get { return purchaseOrderTypeFields.ShowVariancePercent; } set { purchaseOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? PurchaseShowVarianceExtended { get { return purchaseOrderTypeFields.ShowVarianceExtended; } set { purchaseOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? PurchaseShowCountryOfOrigin { get { return purchaseOrderTypeFields.ShowCountryOfOrigin; } set { purchaseOrderTypeFields.ShowCountryOfOrigin = value; } }
        public bool? PurchaseShowManufacturer { get { return purchaseOrderTypeFields.ShowManufacturer; } set { purchaseOrderTypeFields.ShowManufacturer = value; } }
        public bool? PurchaseShowManufacturerPartNumber { get { return purchaseOrderTypeFields.ShowManufacturerPartNumber; } set { purchaseOrderTypeFields.ShowManufacturerPartNumber = value; } }
        public int? PurchaseManufacturerPartNumberWidth { get { return purchaseOrderTypeFields.ManufacturerPartNumberWidth; } set { purchaseOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        public bool? PurchaseShowModelNumber { get { return purchaseOrderTypeFields.ShowModelNumber; } set { purchaseOrderTypeFields.ShowModelNumber = value; } }
        public bool? PurchaseShowVendorPartNumber { get { return purchaseOrderTypeFields.ShowVendorPartNumber; } set { purchaseOrderTypeFields.ShowVendorPartNumber = value; } }
        public bool? PurchaseShowWarehouse { get { return purchaseOrderTypeFields.ShowWarehouse; } set { purchaseOrderTypeFields.ShowWarehouse = value; } }
        public bool? PurchaseShowTaxable { get { return purchaseOrderTypeFields.ShowTaxable; } set { purchaseOrderTypeFields.ShowTaxable = value; } }
        public bool? PurchaseShowNotes { get { return purchaseOrderTypeFields.ShowNotes; } set { purchaseOrderTypeFields.ShowNotes = value; } }
        //public bool? PurchaseShowReturnToWarehouse { get { return purchaseOrderTypeFields.ShowReturnToWarehouse; } set { purchaseOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? PurchaseShowFromTime { get { return purchaseOrderTypeFields.ShowFromTime; } set { purchaseOrderTypeFields.ShowFromTime = value; } }
        //public bool? PurchaseShowToTime { get { return purchaseOrderTypeFields.ShowToTime; } set { purchaseOrderTypeFields.ShowToTime = value; } }
        //public bool? PurchaseShowVehicleNumber { get { return purchaseOrderTypeFields.ShowVehicleNumber; } set { purchaseOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? PurchaseShowBarCode { get { return purchaseOrderTypeFields.ShowBarCode; } set { purchaseOrderTypeFields.ShowBarCode = value; } }
        //public bool? PurchaseShowSerialNumber { get { return purchaseOrderTypeFields.ShowSerialNumber; } set { purchaseOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? PurchaseShowCrewName { get { return purchaseOrderTypeFields.ShowCrewName; } set { purchaseOrderTypeFields.ShowCrewName = value; } }
        //public bool? PurchaseShowHours { get { return purchaseOrderTypeFields.ShowHours; } set { purchaseOrderTypeFields.ShowHours = value; } }
        //public bool? PurchaseShowPickTime { get { return purchaseOrderTypeFields.ShowPickTime; } set { purchaseOrderTypeFields.ShowPickTime = value; } }
        //public bool? PurchaseShowAvailableQuantityAllWarehouses { get { return purchaseOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { purchaseOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? PurchaseShowConflictDateAllWarehouses { get { return purchaseOrderTypeFields.ShowConflictDateAllWarehouses; } set { purchaseOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? PurchaseShowConsignmentAvailableQuantity { get { return purchaseOrderTypeFields.ShowConsignmentAvailableQuantity; } set { purchaseOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? PurchaseShowConsignmentConflictDate { get { return purchaseOrderTypeFields.ShowConsignmentConflictDate; } set { purchaseOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? PurchaseShowConsignmentQuantity { get { return purchaseOrderTypeFields.ShowConsignmentQuantity; } set { purchaseOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? PurchaseShowInLocationQuantity { get { return purchaseOrderTypeFields.ShowInLocationQuantity; } set { purchaseOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? PurchaseShowReservedItems { get { return purchaseOrderTypeFields.ShowReservedItems; } set { purchaseOrderTypeFields.ShowReservedItems = value; } }
        //public bool? PurchaseShowWeeksAndDays { get { return purchaseOrderTypeFields.ShowWeeksAndDays; } set { purchaseOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? PurchaseShowMonthsAndDays { get { return purchaseOrderTypeFields.ShowMonthsAndDays; } set { purchaseOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? PurchaseShowPremiumPercent { get { return purchaseOrderTypeFields.ShowPremiumPercent; } set { purchaseOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? PurchaseShowDepartment { get { return purchaseOrderTypeFields.ShowDepartment; } set { purchaseOrderTypeFields.ShowDepartment = value; } }
        //public bool? PurchaseShowLocation { get { return purchaseOrderTypeFields.ShowLocation; } set { purchaseOrderTypeFields.ShowLocation = value; } }
        //public bool? PurchaseShowOrderActivity { get { return purchaseOrderTypeFields.ShowOrderActivity; } set { purchaseOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? PurchaseShowSubOrderNumber { get { return purchaseOrderTypeFields.ShowSubOrderNumber; } set { purchaseOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? PurchaseShowOrderStatus { get { return purchaseOrderTypeFields.ShowOrderStatus; } set { purchaseOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? PurchaseShowEpisodes { get { return purchaseOrderTypeFields.ShowEpisodes; } set { purchaseOrderTypeFields.ShowEpisodes = value; } }
        //public bool? PurchaseShowEpisodeExtended { get { return purchaseOrderTypeFields.ShowEpisodeExtended; } set { purchaseOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? PurchaseShowEpisodeDiscountAmount { get { return purchaseOrderTypeFields.ShowEpisodeDiscountAmount; } set { purchaseOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string PurchaseDateStamp { get { return purchaseOrderTypeFields.DateStamp; } set { purchaseOrderTypeFields.DateStamp = value; } }
        public string RentalPurchaseDefaultRate { get { return poType.Rentalpurchasedefaultrate; } set { poType.Rentalpurchasedefaultrate = value; } }
        public string SalesPurchaseDefaultRate { get { return poType.Salespurchasedefaultrate; } set { poType.Salespurchasedefaultrate = value; } }

        //labor/crew fields
        [JsonIgnore]
        public string LaborOrderTypeFieldsId { get { return laborOrderTypeFields.OrderTypeFieldsId; } set { laborOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool? LaborShowOrderNumber { get { return laborOrderTypeFields.ShowOrderNumber; } set { laborOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? LaborShowRepairOrderNumber { get { return laborOrderTypeFields.ShowRepairOrderNumber; } set { laborOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? LaborShowICode { get { return laborOrderTypeFields.ShowICode; } set { laborOrderTypeFields.ShowICode = value; } }
        public int? LaborICodeWidth { get { return laborOrderTypeFields.ICodeWidth; } set { laborOrderTypeFields.ICodeWidth = value; } }
        public bool? LaborShowDescription { get { return laborOrderTypeFields.ShowDescription; } set { laborOrderTypeFields.ShowDescription = value; } }
        public int? LaborDescriptionWidth { get { return laborOrderTypeFields.DescriptionWidth; } set { laborOrderTypeFields.DescriptionWidth = value; } }
        //public bool? LaborShowPickDate { get { return laborOrderTypeFields.ShowPickDate; } set { laborOrderTypeFields.ShowPickDate = value; } }
        public bool? LaborShowOrderActivity { get { return laborOrderTypeFields.ShowOrderActivity; } set { laborOrderTypeFields.ShowOrderActivity = value; } }
        public bool? LaborShowCrewName { get { return laborOrderTypeFields.ShowCrewName; } set { laborOrderTypeFields.ShowCrewName = value; } }
        public bool? LaborShowFromDate { get { return laborOrderTypeFields.ShowFromDate; } set { laborOrderTypeFields.ShowFromDate = value; } }
        public bool? LaborShowFromTime { get { return laborOrderTypeFields.ShowFromTime; } set { laborOrderTypeFields.ShowFromTime = value; } }
        public bool? LaborShowToDate { get { return laborOrderTypeFields.ShowToDate; } set { laborOrderTypeFields.ShowToDate = value; } }
        public bool? LaborShowToTime { get { return laborOrderTypeFields.ShowToTime; } set { laborOrderTypeFields.ShowToTime = value; } }
        public bool? LaborShowHours { get { return laborOrderTypeFields.ShowHours; } set { laborOrderTypeFields.ShowHours = value; } }
        public bool? LaborShowBillablePeriods { get { return laborOrderTypeFields.ShowBillablePeriods; } set { laborOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? LaborShowSubQuantity { get { return laborOrderTypeFields.ShowSubQuantity; } set { laborOrderTypeFields.ShowSubQuantity = value; } }
        //public bool? LaborShowAvailableQuantity { get { return laborOrderTypeFields.ShowAvailableQuantity; } set { laborOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? LaborShowConflictDate { get { return laborOrderTypeFields.ShowConflictDate; } set { laborOrderTypeFields.ShowConflictDate = value; } }
        public bool? LaborShowRate { get { return laborOrderTypeFields.ShowRate; } set { laborOrderTypeFields.ShowRate = value; } }
        //public bool? LaborShowCost { get { return laborOrderTypeFields.ShowCost; } set { laborOrderTypeFields.ShowCost = value; } }
        //public bool? LaborShowWeeklyCostExtended { get { return laborOrderTypeFields.ShowWeeklyCostExtended; } set { laborOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? LaborShowMonthlyCostExtended { get { return laborOrderTypeFields.ShowMonthlyCostExtended; } set { laborOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? LaborShowPeriodCostExtended { get { return laborOrderTypeFields.ShowPeriodCostExtended; } set { laborOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? LaborShowDaysPerWeek { get { return laborOrderTypeFields.ShowDaysPerWeek; } set { laborOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? LaborShowDiscountPercent { get { return laborOrderTypeFields.ShowDiscountPercent; } set { laborOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? LaborShowMarkupPercent { get { return laborOrderTypeFields.ShowMarkupPercent; } set { laborOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? LaborShowMarginPercent { get { return laborOrderTypeFields.ShowMarginPercent; } set { laborOrderTypeFields.ShowMarginPercent = value; } }
        public bool? LaborShowUnit { get { return laborOrderTypeFields.ShowUnit; } set { laborOrderTypeFields.ShowUnit = value; } }
        public bool? LaborShowUnitDiscountAmount { get { return laborOrderTypeFields.ShowUnitDiscountAmount; } set { laborOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? LaborShowUnitExtended { get { return laborOrderTypeFields.ShowUnitExtended; } set { laborOrderTypeFields.ShowUnitExtended = value; } }
        public bool? LaborShowWeeklyDiscountAmount { get { return laborOrderTypeFields.ShowWeeklyDiscountAmount; } set { laborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool? LaborShowWeeklyExtended { get { return laborOrderTypeFields.ShowWeeklyExtended; } set { laborOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool? LaborShowMonthlyDiscountAmount { get { return laborOrderTypeFields.ShowMonthlyDiscountAmount; } set { laborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool? LaborShowMonthlyExtended { get { return laborOrderTypeFields.ShowMonthlyExtended; } set { laborOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? LaborShowPeriodDiscountAmount { get { return laborOrderTypeFields.ShowPeriodDiscountAmount; } set { laborOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? LaborShowPeriodExtended { get { return laborOrderTypeFields.ShowPeriodExtended; } set { laborOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? LaborShowVariancePercent { get { return laborOrderTypeFields.ShowVariancePercent; } set { laborOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? LaborShowVarianceExtended { get { return laborOrderTypeFields.ShowVarianceExtended; } set { laborOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? LaborShowWarehouse { get { return laborOrderTypeFields.ShowWarehouse; } set { laborOrderTypeFields.ShowWarehouse = value; } }
        public bool? LaborShowTaxable { get { return laborOrderTypeFields.ShowTaxable; } set { laborOrderTypeFields.ShowTaxable = value; } }
        public bool? LaborShowNotes { get { return laborOrderTypeFields.ShowNotes; } set { laborOrderTypeFields.ShowNotes = value; } }
        //public bool? LaborShowReturnToWarehouse { get { return laborOrderTypeFields.ShowReturnToWarehouse; } set { laborOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? LaborShowPickTime { get { return laborOrderTypeFields.ShowPickTime; } set { laborOrderTypeFields.ShowPickTime = value; } }
        //public bool? LaborShowAvailableQuantityAllWarehouses { get { return laborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { laborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? LaborShowConflictDateAllWarehouses { get { return laborOrderTypeFields.ShowConflictDateAllWarehouses; } set { laborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? LaborShowConsignmentAvailableQuantity { get { return laborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { laborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? LaborShowConsignmentConflictDate { get { return laborOrderTypeFields.ShowConsignmentConflictDate; } set { laborOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? LaborShowConsignmentQuantity { get { return laborOrderTypeFields.ShowConsignmentQuantity; } set { laborOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? LaborShowInLocationQuantity { get { return laborOrderTypeFields.ShowInLocationQuantity; } set { laborOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? LaborShowReservedItems { get { return laborOrderTypeFields.ShowReservedItems; } set { laborOrderTypeFields.ShowReservedItems = value; } }
        //public bool? LaborShowWeeksAndDays { get { return laborOrderTypeFields.ShowWeeksAndDays; } set { laborOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? LaborShowMonthsAndDays { get { return laborOrderTypeFields.ShowMonthsAndDays; } set { laborOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? LaborShowPremiumPercent { get { return laborOrderTypeFields.ShowPremiumPercent; } set { laborOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? LaborShowDepartment { get { return laborOrderTypeFields.ShowDepartment; } set { laborOrderTypeFields.ShowDepartment = value; } }
        //public bool? LaborShowLocation { get { return laborOrderTypeFields.ShowLocation; } set { laborOrderTypeFields.ShowLocation = value; } }
        //public bool? LaborShowSubOrderNumber { get { return laborOrderTypeFields.ShowSubOrderNumber; } set { laborOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? LaborShowOrderStatus { get { return laborOrderTypeFields.ShowOrderStatus; } set { laborOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? LaborShowEpisodes { get { return laborOrderTypeFields.ShowEpisodes; } set { laborOrderTypeFields.ShowEpisodes = value; } }
        //public bool? LaborShowEpisodeExtended { get { return laborOrderTypeFields.ShowEpisodeExtended; } set { laborOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? LaborShowEpisodeDiscountAmount { get { return laborOrderTypeFields.ShowEpisodeDiscountAmount; } set { laborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string LaborDateStamp { get { return laborOrderTypeFields.DateStamp; } set { laborOrderTypeFields.DateStamp = value; } }
        public bool? HideCrewBreaks { get { return poType.Hidecrewbreaks; } set { poType.Hidecrewbreaks = value; } }
        public bool? Break1Paid { get { return poType.Break1paId; } set { poType.Break1paId = value; } }
        public bool? Break2Paid { get { return poType.Break2paId; } set { poType.Break2paId = value; } }
        public bool? Break3Paid { get { return poType.Break3paId; } set { poType.Break3paId = value; } }

        //misc fields
        [JsonIgnore]
        public string MiscOrderTypeFieldsId { get { return miscOrderTypeFields.OrderTypeFieldsId; } set { miscOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool? MiscShowOrderNumber { get { return miscOrderTypeFields.ShowOrderNumber; } set { miscOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? MiscShowRepairOrderNumber { get { return miscOrderTypeFields.ShowRepairOrderNumber; } set { miscOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? MiscShowICode { get { return miscOrderTypeFields.ShowICode; } set { miscOrderTypeFields.ShowICode = value; } }
        public int? MiscICodeWidth { get { return miscOrderTypeFields.ICodeWidth; } set { miscOrderTypeFields.ICodeWidth = value; } }
        public bool? MiscShowDescription { get { return miscOrderTypeFields.ShowDescription; } set { miscOrderTypeFields.ShowDescription = value; } }
        public int? MiscDescriptionWidth { get { return miscOrderTypeFields.DescriptionWidth; } set { miscOrderTypeFields.DescriptionWidth = value; } }
        //public bool? MiscShowPickDate { get { return miscOrderTypeFields.ShowPickDate; } set { miscOrderTypeFields.ShowPickDate = value; } }
        public bool? MiscShowFromDate { get { return miscOrderTypeFields.ShowFromDate; } set { miscOrderTypeFields.ShowFromDate = value; } }
        public bool? MiscShowToDate { get { return miscOrderTypeFields.ShowToDate; } set { miscOrderTypeFields.ShowToDate = value; } }
        public bool? MiscShowBillablePeriods { get { return miscOrderTypeFields.ShowBillablePeriods; } set { miscOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? MiscShowSubQuantity { get { return miscOrderTypeFields.ShowSubQuantity; } set { miscOrderTypeFields.ShowSubQuantity = value; } }
        //public bool? MiscShowAvailableQuantity { get { return miscOrderTypeFields.ShowAvailableQuantity; } set { miscOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? MiscShowConflictDate { get { return miscOrderTypeFields.ShowConflictDate; } set { miscOrderTypeFields.ShowConflictDate = value; } }
        public bool? MiscShowRate { get { return miscOrderTypeFields.ShowRate; } set { miscOrderTypeFields.ShowRate = value; } }
        //public bool? MiscShowCost { get { return miscOrderTypeFields.ShowCost; } set { miscOrderTypeFields.ShowCost = value; } }
        //public bool? MiscShowWeeklyCostExtended { get { return miscOrderTypeFields.ShowWeeklyCostExtended; } set { miscOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? MiscShowMonthlyCostExtended { get { return miscOrderTypeFields.ShowMonthlyCostExtended; } set { miscOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? MiscShowPeriodCostExtended { get { return miscOrderTypeFields.ShowPeriodCostExtended; } set { miscOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? MiscShowDaysPerWeek { get { return miscOrderTypeFields.ShowDaysPerWeek; } set { miscOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? MiscShowDiscountPercent { get { return miscOrderTypeFields.ShowDiscountPercent; } set { miscOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? MiscShowMarkupPercent { get { return miscOrderTypeFields.ShowMarkupPercent; } set { miscOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? MiscShowMarginPercent { get { return miscOrderTypeFields.ShowMarginPercent; } set { miscOrderTypeFields.ShowMarginPercent = value; } }
        public bool? MiscShowUnit { get { return miscOrderTypeFields.ShowUnit; } set { miscOrderTypeFields.ShowUnit = value; } }
        public bool? MiscShowUnitDiscountAmount { get { return miscOrderTypeFields.ShowUnitDiscountAmount; } set { miscOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? MiscShowUnitExtended { get { return miscOrderTypeFields.ShowUnitExtended; } set { miscOrderTypeFields.ShowUnitExtended = value; } }
        public bool? MiscShowWeeklyDiscountAmount { get { return miscOrderTypeFields.ShowWeeklyDiscountAmount; } set { miscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool? MiscShowWeeklyExtended { get { return miscOrderTypeFields.ShowWeeklyExtended; } set { miscOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool? MiscShowMonthlyDiscountAmount { get { return miscOrderTypeFields.ShowMonthlyDiscountAmount; } set { miscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool? MiscShowMonthlyExtended { get { return miscOrderTypeFields.ShowMonthlyExtended; } set { miscOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? MiscShowPeriodDiscountAmount { get { return miscOrderTypeFields.ShowPeriodDiscountAmount; } set { miscOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? MiscShowPeriodExtended { get { return miscOrderTypeFields.ShowPeriodExtended; } set { miscOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? MiscShowVariancePercent { get { return miscOrderTypeFields.ShowVariancePercent; } set { miscOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? MiscShowVarianceExtended { get { return miscOrderTypeFields.ShowVarianceExtended; } set { miscOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? MiscShowWarehouse { get { return miscOrderTypeFields.ShowWarehouse; } set { miscOrderTypeFields.ShowWarehouse = value; } }
        public bool? MiscShowTaxable { get { return miscOrderTypeFields.ShowTaxable; } set { miscOrderTypeFields.ShowTaxable = value; } }
        public bool? MiscShowNotes { get { return miscOrderTypeFields.ShowNotes; } set { miscOrderTypeFields.ShowNotes = value; } }
        //public bool? MiscShowReturnToWarehouse { get { return miscOrderTypeFields.ShowReturnToWarehouse; } set { miscOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? MiscShowFromTime { get { return miscOrderTypeFields.ShowFromTime; } set { miscOrderTypeFields.ShowFromTime = value; } }
        //public bool? MiscShowToTime { get { return miscOrderTypeFields.ShowToTime; } set { miscOrderTypeFields.ShowToTime = value; } }
        //public bool? MiscShowVehicleNumber { get { return miscOrderTypeFields.ShowVehicleNumber; } set { miscOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? MiscShowBarCode { get { return miscOrderTypeFields.ShowBarCode; } set { miscOrderTypeFields.ShowBarCode = value; } }
        //public bool? MiscShowSerialNumber { get { return miscOrderTypeFields.ShowSerialNumber; } set { miscOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? MiscShowCrewName { get { return miscOrderTypeFields.ShowCrewName; } set { miscOrderTypeFields.ShowCrewName = value; } }
        //public bool? MiscShowHours { get { return miscOrderTypeFields.ShowHours; } set { miscOrderTypeFields.ShowHours = value; } }
        //public bool? MiscShowPickTime { get { return miscOrderTypeFields.ShowPickTime; } set { miscOrderTypeFields.ShowPickTime = value; } }
        //public bool? MiscShowAvailableQuantityAllWarehouses { get { return miscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { miscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? MiscShowConflictDateAllWarehouses { get { return miscOrderTypeFields.ShowConflictDateAllWarehouses; } set { miscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? MiscShowConsignmentAvailableQuantity { get { return miscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { miscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? MiscShowConsignmentConflictDate { get { return miscOrderTypeFields.ShowConsignmentConflictDate; } set { miscOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? MiscShowConsignmentQuantity { get { return miscOrderTypeFields.ShowConsignmentQuantity; } set { miscOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? MiscShowInLocationQuantity { get { return miscOrderTypeFields.ShowInLocationQuantity; } set { miscOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? MiscShowReservedItems { get { return miscOrderTypeFields.ShowReservedItems; } set { miscOrderTypeFields.ShowReservedItems = value; } }
        public bool? MiscShowWeeksAndDays { get { return miscOrderTypeFields.ShowWeeksAndDays; } set { miscOrderTypeFields.ShowWeeksAndDays = value; } }
        public bool? MiscShowMonthsAndDays { get { return miscOrderTypeFields.ShowMonthsAndDays; } set { miscOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? MiscShowPremiumPercent { get { return miscOrderTypeFields.ShowPremiumPercent; } set { miscOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? MiscShowDepartment { get { return miscOrderTypeFields.ShowDepartment; } set { miscOrderTypeFields.ShowDepartment = value; } }
        //public bool? MiscShowLocation { get { return miscOrderTypeFields.ShowLocation; } set { miscOrderTypeFields.ShowLocation = value; } }
        //public bool? MiscShowOrderActivity { get { return miscOrderTypeFields.ShowOrderActivity; } set { miscOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? MiscShowSubOrderNumber { get { return miscOrderTypeFields.ShowSubOrderNumber; } set { miscOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? MiscShowOrderStatus { get { return miscOrderTypeFields.ShowOrderStatus; } set { miscOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? MiscShowEpisodes { get { return miscOrderTypeFields.ShowEpisodes; } set { miscOrderTypeFields.ShowEpisodes = value; } }
        //public bool? MiscShowEpisodeExtended { get { return miscOrderTypeFields.ShowEpisodeExtended; } set { miscOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? MiscShowEpisodeDiscountAmount { get { return miscOrderTypeFields.ShowEpisodeDiscountAmount; } set { miscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string MiscDateStamp { get { return miscOrderTypeFields.DateStamp; } set { miscOrderTypeFields.DateStamp = value; } }



        //sub-crew fields
        [JsonIgnore]
        public string SubLaborOrderTypeFieldsId { get { return subLaborOrderTypeFields.OrderTypeFieldsId; } set { subLaborOrderTypeFields.OrderTypeFieldsId = value; } }
        public bool? SubLaborShowOrderNumber { get { return subLaborOrderTypeFields.ShowOrderNumber; } set { subLaborOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? SubLaborShowRepairOrderNumber { get { return subLaborOrderTypeFields.ShowRepairOrderNumber; } set { subLaborOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? SubLaborShowICode { get { return subLaborOrderTypeFields.ShowICode; } set { subLaborOrderTypeFields.ShowICode = value; } }
        public int? SubLaborICodeWidth { get { return subLaborOrderTypeFields.ICodeWidth; } set { subLaborOrderTypeFields.ICodeWidth = value; } }
        public bool? SubLaborShowDescription { get { return subLaborOrderTypeFields.ShowDescription; } set { subLaborOrderTypeFields.ShowDescription = value; } }
        public int? SubLaborDescriptionWidth { get { return subLaborOrderTypeFields.DescriptionWidth; } set { subLaborOrderTypeFields.DescriptionWidth = value; } }
        //public bool? SubLaborShowPickDate { get { return subLaborOrderTypeFields.ShowPickDate; } set { subLaborOrderTypeFields.ShowPickDate = value; } }
        public bool? SubLaborShowFromDate { get { return subLaborOrderTypeFields.ShowFromDate; } set { subLaborOrderTypeFields.ShowFromDate = value; } }
        public bool? SubLaborShowFromTime { get { return subLaborOrderTypeFields.ShowFromTime; } set { subLaborOrderTypeFields.ShowFromTime = value; } }
        public bool? SubLaborShowToDate { get { return subLaborOrderTypeFields.ShowToDate; } set { subLaborOrderTypeFields.ShowToDate = value; } }
        public bool? SubLaborShowToTime { get { return subLaborOrderTypeFields.ShowToTime; } set { subLaborOrderTypeFields.ShowToTime = value; } }
        public bool? SubLaborShowHours { get { return subLaborOrderTypeFields.ShowHours; } set { subLaborOrderTypeFields.ShowHours = value; } }
        public bool? SubLaborShowBillablePeriods { get { return subLaborOrderTypeFields.ShowBillablePeriods; } set { subLaborOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? SubLaborShowAvailableQuantity { get { return subLaborOrderTypeFields.ShowAvailableQuantity; } set { subLaborOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? SubLaborShowConflictDate { get { return subLaborOrderTypeFields.ShowConflictDate; } set { subLaborOrderTypeFields.ShowConflictDate = value; } }
        public bool? SubLaborShowRate { get { return subLaborOrderTypeFields.ShowRate; } set { subLaborOrderTypeFields.ShowRate = value; } }
        //public bool? SubLaborShowCost { get { return subLaborOrderTypeFields.ShowCost; } set { subLaborOrderTypeFields.ShowCost = value; } }
        //public bool? SubLaborShowWeeklyCostExtended { get { return subLaborOrderTypeFields.ShowWeeklyCostExtended; } set { subLaborOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? SubLaborShowMonthlyCostExtended { get { return subLaborOrderTypeFields.ShowMonthlyCostExtended; } set { subLaborOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? SubLaborShowPeriodCostExtended { get { return subLaborOrderTypeFields.ShowPeriodCostExtended; } set { subLaborOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? SubLaborShowDaysPerWeek { get { return subLaborOrderTypeFields.ShowDaysPerWeek; } set { subLaborOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? SubLaborShowDiscountPercent { get { return subLaborOrderTypeFields.ShowDiscountPercent; } set { subLaborOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? SubLaborShowMarkupPercent { get { return subLaborOrderTypeFields.ShowMarkupPercent; } set { subLaborOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? SubLaborShowMarginPercent { get { return subLaborOrderTypeFields.ShowMarginPercent; } set { subLaborOrderTypeFields.ShowMarginPercent = value; } }
        public bool? SubLaborShowUnit { get { return subLaborOrderTypeFields.ShowUnit; } set { subLaborOrderTypeFields.ShowUnit = value; } }
        public bool? SubLaborShowUnitDiscountAmount { get { return subLaborOrderTypeFields.ShowUnitDiscountAmount; } set { subLaborOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? SubLaborShowUnitExtended { get { return subLaborOrderTypeFields.ShowUnitExtended; } set { subLaborOrderTypeFields.ShowUnitExtended = value; } }
        public bool? SubLaborShowWeeklyDiscountAmount { get { return subLaborOrderTypeFields.ShowWeeklyDiscountAmount; } set { subLaborOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool? SubLaborShowWeeklyExtended { get { return subLaborOrderTypeFields.ShowWeeklyExtended; } set { subLaborOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool? SubLaborShowMonthlyDiscountAmount { get { return subLaborOrderTypeFields.ShowMonthlyDiscountAmount; } set { subLaborOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool? SubLaborShowMonthlyExtended { get { return subLaborOrderTypeFields.ShowMonthlyExtended; } set { subLaborOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? SubLaborShowPeriodDiscountAmount { get { return subLaborOrderTypeFields.ShowPeriodDiscountAmount; } set { subLaborOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? SubLaborShowPeriodExtended { get { return subLaborOrderTypeFields.ShowPeriodExtended; } set { subLaborOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? SubLaborShowVariancePercent { get { return subLaborOrderTypeFields.ShowVariancePercent; } set { subLaborOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? SubLaborShowVarianceExtended { get { return subLaborOrderTypeFields.ShowVarianceExtended; } set { subLaborOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? SubLaborShowWarehouse { get { return subLaborOrderTypeFields.ShowWarehouse; } set { subLaborOrderTypeFields.ShowWarehouse = value; } }
        public bool? SubLaborShowTaxable { get { return subLaborOrderTypeFields.ShowTaxable; } set { subLaborOrderTypeFields.ShowTaxable = value; } }
        public bool? SubLaborShowNotes { get { return subLaborOrderTypeFields.ShowNotes; } set { subLaborOrderTypeFields.ShowNotes = value; } }
        //public bool? SubLaborShowReturnToWarehouse { get { return subLaborOrderTypeFields.ShowReturnToWarehouse; } set { subLaborOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? SubLaborShowVehicleNumber { get { return subLaborOrderTypeFields.ShowVehicleNumber; } set { subLaborOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? SubLaborShowBarCode { get { return subLaborOrderTypeFields.ShowBarCode; } set { subLaborOrderTypeFields.ShowBarCode = value; } }
        //public bool? SubLaborShowSerialNumber { get { return subLaborOrderTypeFields.ShowSerialNumber; } set { subLaborOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? SubLaborShowCrewName { get { return subLaborOrderTypeFields.ShowCrewName; } set { subLaborOrderTypeFields.ShowCrewName = value; } }
        //public bool? SubLaborShowPickTime { get { return subLaborOrderTypeFields.ShowPickTime; } set { subLaborOrderTypeFields.ShowPickTime = value; } }
        //public bool? SubLaborShowAvailableQuantityAllWarehouses { get { return subLaborOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subLaborOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? SubLaborShowConflictDateAllWarehouses { get { return subLaborOrderTypeFields.ShowConflictDateAllWarehouses; } set { subLaborOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? SubLaborShowConsignmentAvailableQuantity { get { return subLaborOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subLaborOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? SubLaborShowConsignmentConflictDate { get { return subLaborOrderTypeFields.ShowConsignmentConflictDate; } set { subLaborOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? SubLaborShowConsignmentQuantity { get { return subLaborOrderTypeFields.ShowConsignmentQuantity; } set { subLaborOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? SubLaborShowInLocationQuantity { get { return subLaborOrderTypeFields.ShowInLocationQuantity; } set { subLaborOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? SubLaborShowReservedItems { get { return subLaborOrderTypeFields.ShowReservedItems; } set { subLaborOrderTypeFields.ShowReservedItems = value; } }
        //public bool? SubLaborShowWeeksAndDays { get { return subLaborOrderTypeFields.ShowWeeksAndDays; } set { subLaborOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? SubLaborShowMonthsAndDays { get { return subLaborOrderTypeFields.ShowMonthsAndDays; } set { subLaborOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? SubLaborShowPremiumPercent { get { return subLaborOrderTypeFields.ShowPremiumPercent; } set { subLaborOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? SubLaborShowDepartment { get { return subLaborOrderTypeFields.ShowDepartment; } set { subLaborOrderTypeFields.ShowDepartment = value; } }
        //public bool? SubLaborShowLocation { get { return subLaborOrderTypeFields.ShowLocation; } set { subLaborOrderTypeFields.ShowLocation = value; } }
        //public bool? SubLaborShowOrderActivity { get { return subLaborOrderTypeFields.ShowOrderActivity; } set { subLaborOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? SubLaborShowSubOrderNumber { get { return subLaborOrderTypeFields.ShowSubOrderNumber; } set { subLaborOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? SubLaborShowOrderStatus { get { return subLaborOrderTypeFields.ShowOrderStatus; } set { subLaborOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? SubLaborShowEpisodes { get { return subLaborOrderTypeFields.ShowEpisodes; } set { subLaborOrderTypeFields.ShowEpisodes = value; } }
        //public bool? SubLaborShowEpisodeExtended { get { return subLaborOrderTypeFields.ShowEpisodeExtended; } set { subLaborOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? SubLaborShowEpisodeDiscountAmount { get { return subLaborOrderTypeFields.ShowEpisodeDiscountAmount; } set { subLaborOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string SubLaborDateStamp { get { return subLaborOrderTypeFields.DateStamp; } set { subLaborOrderTypeFields.DateStamp = value; } }


        //sub-misc fields
        [JsonIgnore]
        public string SubMiscOrderTypeFieldsId { get { return subMiscOrderTypeFields.OrderTypeFieldsId; } set { subMiscOrderTypeFields.OrderTypeFieldsId = value; } }
        public bool? SubMiscShowOrderNumber { get { return subMiscOrderTypeFields.ShowOrderNumber; } set { subMiscOrderTypeFields.ShowOrderNumber = value; } }
        //public bool? SubMiscShowRepairOrderNumber { get { return subMiscOrderTypeFields.ShowRepairOrderNumber; } set { subMiscOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? SubMiscShowICode { get { return subMiscOrderTypeFields.ShowICode; } set { subMiscOrderTypeFields.ShowICode = value; } }
        public int? SubMiscICodeWidth { get { return subMiscOrderTypeFields.ICodeWidth; } set { subMiscOrderTypeFields.ICodeWidth = value; } }
        public bool? SubMiscShowDescription { get { return subMiscOrderTypeFields.ShowDescription; } set { subMiscOrderTypeFields.ShowDescription = value; } }
        public int? SubMiscDescriptionWidth { get { return subMiscOrderTypeFields.DescriptionWidth; } set { subMiscOrderTypeFields.DescriptionWidth = value; } }
        //public bool? SubMiscShowPickDate { get { return subMiscOrderTypeFields.ShowPickDate; } set { subMiscOrderTypeFields.ShowPickDate = value; } }
        public bool? SubMiscShowFromDate { get { return subMiscOrderTypeFields.ShowFromDate; } set { subMiscOrderTypeFields.ShowFromDate = value; } }
        public bool? SubMiscShowToDate { get { return subMiscOrderTypeFields.ShowToDate; } set { subMiscOrderTypeFields.ShowToDate = value; } }
        public bool? SubMiscShowBillablePeriods { get { return subMiscOrderTypeFields.ShowBillablePeriods; } set { subMiscOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? SubMiscShowAvailableQuantity { get { return subMiscOrderTypeFields.ShowAvailableQuantity; } set { subMiscOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? SubMiscShowConflictDate { get { return subMiscOrderTypeFields.ShowConflictDate; } set { subMiscOrderTypeFields.ShowConflictDate = value; } }
        public bool? SubMiscShowRate { get { return subMiscOrderTypeFields.ShowRate; } set { subMiscOrderTypeFields.ShowRate = value; } }
        //public bool? SubMiscShowCost { get { return subMiscOrderTypeFields.ShowCost; } set { subMiscOrderTypeFields.ShowCost = value; } }
        //public bool? SubMiscShowWeeklyCostExtended { get { return subMiscOrderTypeFields.ShowWeeklyCostExtended; } set { subMiscOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? SubMiscShowMonthlyCostExtended { get { return subMiscOrderTypeFields.ShowMonthlyCostExtended; } set { subMiscOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? SubMiscShowPeriodCostExtended { get { return subMiscOrderTypeFields.ShowPeriodCostExtended; } set { subMiscOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? SubMiscShowDaysPerWeek { get { return subMiscOrderTypeFields.ShowDaysPerWeek; } set { subMiscOrderTypeFields.ShowDaysPerWeek = value; } }
        public bool? SubMiscShowDiscountPercent { get { return subMiscOrderTypeFields.ShowDiscountPercent; } set { subMiscOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? SubMiscShowMarkupPercent { get { return subMiscOrderTypeFields.ShowMarkupPercent; } set { subMiscOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? SubMiscShowMarginPercent { get { return subMiscOrderTypeFields.ShowMarginPercent; } set { subMiscOrderTypeFields.ShowMarginPercent = value; } }
        public bool? SubMiscShowUnit { get { return subMiscOrderTypeFields.ShowUnit; } set { subMiscOrderTypeFields.ShowUnit = value; } }
        public bool? SubMiscShowUnitDiscountAmount { get { return subMiscOrderTypeFields.ShowUnitDiscountAmount; } set { subMiscOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? SubMiscShowUnitExtended { get { return subMiscOrderTypeFields.ShowUnitExtended; } set { subMiscOrderTypeFields.ShowUnitExtended = value; } }
        public bool? SubMiscShowWeeklyDiscountAmount { get { return subMiscOrderTypeFields.ShowWeeklyDiscountAmount; } set { subMiscOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        public bool? SubMiscShowWeeklyExtended { get { return subMiscOrderTypeFields.ShowWeeklyExtended; } set { subMiscOrderTypeFields.ShowWeeklyExtended = value; } }
        public bool? SubMiscShowMonthlyDiscountAmount { get { return subMiscOrderTypeFields.ShowMonthlyDiscountAmount; } set { subMiscOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        public bool? SubMiscShowMonthlyExtended { get { return subMiscOrderTypeFields.ShowMonthlyExtended; } set { subMiscOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? SubMiscShowPeriodDiscountAmount { get { return subMiscOrderTypeFields.ShowPeriodDiscountAmount; } set { subMiscOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? SubMiscShowPeriodExtended { get { return subMiscOrderTypeFields.ShowPeriodExtended; } set { subMiscOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? SubMiscShowVariancePercent { get { return subMiscOrderTypeFields.ShowVariancePercent; } set { subMiscOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? SubMiscShowVarianceExtended { get { return subMiscOrderTypeFields.ShowVarianceExtended; } set { subMiscOrderTypeFields.ShowVarianceExtended = value; } }
        public bool? SubMiscShowWarehouse { get { return subMiscOrderTypeFields.ShowWarehouse; } set { subMiscOrderTypeFields.ShowWarehouse = value; } }
        public bool? SubMiscShowTaxable { get { return subMiscOrderTypeFields.ShowTaxable; } set { subMiscOrderTypeFields.ShowTaxable = value; } }
        public bool? SubMiscShowNotes { get { return subMiscOrderTypeFields.ShowNotes; } set { subMiscOrderTypeFields.ShowNotes = value; } }
        //public bool? SubMiscShowReturnToWarehouse { get { return subMiscOrderTypeFields.ShowReturnToWarehouse; } set { subMiscOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? SubMiscShowFromTime { get { return subMiscOrderTypeFields.ShowFromTime; } set { subMiscOrderTypeFields.ShowFromTime = value; } }
        //public bool? SubMiscShowToTime { get { return subMiscOrderTypeFields.ShowToTime; } set { subMiscOrderTypeFields.ShowToTime = value; } }
        //public bool? SubMiscShowVehicleNumber { get { return subMiscOrderTypeFields.ShowVehicleNumber; } set { subMiscOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? SubMiscShowBarCode { get { return subMiscOrderTypeFields.ShowBarCode; } set { subMiscOrderTypeFields.ShowBarCode = value; } }
        //public bool? SubMiscShowSerialNumber { get { return subMiscOrderTypeFields.ShowSerialNumber; } set { subMiscOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? SubMiscShowPickTime { get { return subMiscOrderTypeFields.ShowPickTime; } set { subMiscOrderTypeFields.ShowPickTime = value; } }
        //public bool? SubMiscShowAvailableQuantityAllWarehouses { get { return subMiscOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { subMiscOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? SubMiscShowConflictDateAllWarehouses { get { return subMiscOrderTypeFields.ShowConflictDateAllWarehouses; } set { subMiscOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? SubMiscShowConsignmentAvailableQuantity { get { return subMiscOrderTypeFields.ShowConsignmentAvailableQuantity; } set { subMiscOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? SubMiscShowConsignmentConflictDate { get { return subMiscOrderTypeFields.ShowConsignmentConflictDate; } set { subMiscOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? SubMiscShowConsignmentQuantity { get { return subMiscOrderTypeFields.ShowConsignmentQuantity; } set { subMiscOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? SubMiscShowInLocationQuantity { get { return subMiscOrderTypeFields.ShowInLocationQuantity; } set { subMiscOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? SubMiscShowReservedItems { get { return subMiscOrderTypeFields.ShowReservedItems; } set { subMiscOrderTypeFields.ShowReservedItems = value; } }
        //public bool? SubMiscShowWeeksAndDays { get { return subMiscOrderTypeFields.ShowWeeksAndDays; } set { subMiscOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? SubMiscShowMonthsAndDays { get { return subMiscOrderTypeFields.ShowMonthsAndDays; } set { subMiscOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? SubMiscShowPremiumPercent { get { return subMiscOrderTypeFields.ShowPremiumPercent; } set { subMiscOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? SubMiscShowDepartment { get { return subMiscOrderTypeFields.ShowDepartment; } set { subMiscOrderTypeFields.ShowDepartment = value; } }
        //public bool? SubMiscShowLocation { get { return subMiscOrderTypeFields.ShowLocation; } set { subMiscOrderTypeFields.ShowLocation = value; } }
        //public bool? SubMiscShowOrderActivity { get { return subMiscOrderTypeFields.ShowOrderActivity; } set { subMiscOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? SubMiscShowSubOrderNumber { get { return subMiscOrderTypeFields.ShowSubOrderNumber; } set { subMiscOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? SubMiscShowOrderStatus { get { return subMiscOrderTypeFields.ShowOrderStatus; } set { subMiscOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? SubMiscShowEpisodes { get { return subMiscOrderTypeFields.ShowEpisodes; } set { subMiscOrderTypeFields.ShowEpisodes = value; } }
        //public bool? SubMiscShowEpisodeExtended { get { return subMiscOrderTypeFields.ShowEpisodeExtended; } set { subMiscOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? SubMiscShowEpisodeDiscountAmount { get { return subMiscOrderTypeFields.ShowEpisodeDiscountAmount; } set { subMiscOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string SubMiscDateStamp { get { return subMiscOrderTypeFields.DateStamp; } set { subMiscOrderTypeFields.DateStamp = value; } }


        //repairfields
        [JsonIgnore]
        public string RepairOrderTypeFieldsId { get { return repairOrderTypeFields.OrderTypeFieldsId; } set { repairOrderTypeFields.OrderTypeFieldsId = value; } }
        //public bool? RepairShowOrderNumber { get { return repairOrderTypeFields.ShowOrderNumber; } set { repairOrderTypeFields.ShowOrderNumber = value; } }
        public bool? RepairShowRepairOrderNumber { get { return repairOrderTypeFields.ShowRepairOrderNumber; } set { repairOrderTypeFields.ShowRepairOrderNumber = value; } }
        public bool? RepairShowICode { get { return repairOrderTypeFields.ShowICode; } set { repairOrderTypeFields.ShowICode = value; } }
        public int? RepairICodeWidth { get { return repairOrderTypeFields.ICodeWidth; } set { repairOrderTypeFields.ICodeWidth = value; } }
        public bool? RepairShowDescription { get { return repairOrderTypeFields.ShowDescription; } set { repairOrderTypeFields.ShowDescription = value; } }
        public int? RepairDescriptionWidth { get { return repairOrderTypeFields.DescriptionWidth; } set { repairOrderTypeFields.DescriptionWidth = value; } }
        public bool? RepairShowPickDate { get { return repairOrderTypeFields.ShowPickDate; } set { repairOrderTypeFields.ShowPickDate = value; } }
        //public bool? RepairShowFromDate { get { return repairOrderTypeFields.ShowFromDate; } set { repairOrderTypeFields.ShowFromDate = value; } }
        //public bool? RepairShowToDate { get { return repairOrderTypeFields.ShowToDate; } set { repairOrderTypeFields.ShowToDate = value; } }
        //public bool? RepairShowBillablePeriods { get { return repairOrderTypeFields.ShowBillablePeriods; } set { repairOrderTypeFields.ShowBillablePeriods = value; } }
        //public bool? RepairShowSubQuantity { get { return repairOrderTypeFields.ShowSubQuantity; } set { repairOrderTypeFields.ShowSubQuantity = value; } }
        //public bool? RepairShowAvailableQuantity { get { return repairOrderTypeFields.ShowAvailableQuantity; } set { repairOrderTypeFields.ShowAvailableQuantity = value; } }
        //public bool? RepairShowConflictDate { get { return repairOrderTypeFields.ShowConflictDate; } set { repairOrderTypeFields.ShowConflictDate = value; } }
        public bool? RepairShowRate { get { return repairOrderTypeFields.ShowRate; } set { repairOrderTypeFields.ShowRate = value; } }
        //public bool? RepairShowCost { get { return repairOrderTypeFields.ShowCost; } set { repairOrderTypeFields.ShowCost = value; } }
        //public bool? RepairShowWeeklyCostExtended { get { return repairOrderTypeFields.ShowWeeklyCostExtended; } set { repairOrderTypeFields.ShowWeeklyCostExtended = value; } }
        //public bool? RepairShowMonthlyCostExtended { get { return repairOrderTypeFields.ShowMonthlyCostExtended; } set { repairOrderTypeFields.ShowMonthlyCostExtended = value; } }
        //public bool? RepairShowPeriodCostExtended { get { return repairOrderTypeFields.ShowPeriodCostExtended; } set { repairOrderTypeFields.ShowPeriodCostExtended = value; } }
        //public bool? RepairShowDaysPerWeek { get { return repairOrderTypeFields.ShowDaysPerWeek; } set { repairOrderTypeFields.ShowDaysPerWeek = value; } }
        //public bool? RepairShowDiscountPercent { get { return repairOrderTypeFields.ShowDiscountPercent; } set { repairOrderTypeFields.ShowDiscountPercent = value; } }
        //public bool? RepairShowMarkupPercent { get { return repairOrderTypeFields.ShowMarkupPercent; } set { repairOrderTypeFields.ShowMarkupPercent = value; } }
        //public bool? RepairShowMarginPercent { get { return repairOrderTypeFields.ShowMarginPercent; } set { repairOrderTypeFields.ShowMarginPercent = value; } }
        //public bool? RepairShowSplit { get { return repairOrderTypeFields.ShowSplit; } set { repairOrderTypeFields.ShowSplit = value; } }
        public bool? RepairShowUnit { get { return repairOrderTypeFields.ShowUnit; } set { repairOrderTypeFields.ShowUnit = value; } }
        public bool? RepairShowUnitDiscountAmount { get { return repairOrderTypeFields.ShowUnitDiscountAmount; } set { repairOrderTypeFields.ShowUnitDiscountAmount = value; } }
        public bool? RepairShowUnitExtended { get { return repairOrderTypeFields.ShowUnitExtended; } set { repairOrderTypeFields.ShowUnitExtended = value; } }
        //public bool? RepairShowWeeklyDiscountAmount { get { return repairOrderTypeFields.ShowWeeklyDiscountAmount; } set { repairOrderTypeFields.ShowWeeklyDiscountAmount = value; } }
        //public bool? RepairShowWeeklyExtended { get { return repairOrderTypeFields.ShowWeeklyExtended; } set { repairOrderTypeFields.ShowWeeklyExtended = value; } }
        //public bool? RepairShowMonthlyDiscountAmount { get { return repairOrderTypeFields.ShowMonthlyDiscountAmount; } set { repairOrderTypeFields.ShowMonthlyDiscountAmount = value; } }
        //public bool? RepairShowMonthlyExtended { get { return repairOrderTypeFields.ShowMonthlyExtended; } set { repairOrderTypeFields.ShowMonthlyExtended = value; } }
        public bool? RepairShowPeriodDiscountAmount { get { return repairOrderTypeFields.ShowPeriodDiscountAmount; } set { repairOrderTypeFields.ShowPeriodDiscountAmount = value; } }
        public bool? RepairShowPeriodExtended { get { return repairOrderTypeFields.ShowPeriodExtended; } set { repairOrderTypeFields.ShowPeriodExtended = value; } }
        //public bool? RepairShowVariancePercent { get { return repairOrderTypeFields.ShowVariancePercent; } set { repairOrderTypeFields.ShowVariancePercent = value; } }
        //public bool? RepairShowVarianceExtended { get { return repairOrderTypeFields.ShowVarianceExtended; } set { repairOrderTypeFields.ShowVarianceExtended = value; } }
        //public bool? RepairShowCountryOfOrigin { get { return repairOrderTypeFields.ShowCountryOfOrigin; } set { repairOrderTypeFields.ShowCountryOfOrigin = value; } }
        //public bool? RepairShowManufacturer { get { return repairOrderTypeFields.ShowManufacturer; } set { repairOrderTypeFields.ShowManufacturer = value; } }
        //public bool? RepairShowManufacturerPartNumber { get { return repairOrderTypeFields.ShowManufacturerPartNumber; } set { repairOrderTypeFields.ShowManufacturerPartNumber = value; } }
        //public int? RepairManufacturerPartNumberWidth { get { return repairOrderTypeFields.ManufacturerPartNumberWidth; } set { repairOrderTypeFields.ManufacturerPartNumberWidth = value; } }
        //public bool? RepairShowModelNumber { get { return repairOrderTypeFields.ShowModelNumber; } set { repairOrderTypeFields.ShowModelNumber = value; } }
        //public bool? RepairShowVendorPartNumber { get { return repairOrderTypeFields.ShowVendorPartNumber; } set { repairOrderTypeFields.ShowVendorPartNumber = value; } }
        public bool? RepairShowWarehouse { get { return repairOrderTypeFields.ShowWarehouse; } set { repairOrderTypeFields.ShowWarehouse = value; } }
        public bool? RepairShowTaxable { get { return repairOrderTypeFields.ShowTaxable; } set { repairOrderTypeFields.ShowTaxable = value; } }
        public bool? RepairShowNotes { get { return repairOrderTypeFields.ShowNotes; } set { repairOrderTypeFields.ShowNotes = value; } }
        //public bool? RepairShowReturnToWarehouse { get { return repairOrderTypeFields.ShowReturnToWarehouse; } set { repairOrderTypeFields.ShowReturnToWarehouse = value; } }
        //public bool? RepairShowFromTime { get { return repairOrderTypeFields.ShowFromTime; } set { repairOrderTypeFields.ShowFromTime = value; } }
        //public bool? RepairShowToTime { get { return repairOrderTypeFields.ShowToTime; } set { repairOrderTypeFields.ShowToTime = value; } }
        //public bool? RepairShowVehicleNumber { get { return repairOrderTypeFields.ShowVehicleNumber; } set { repairOrderTypeFields.ShowVehicleNumber = value; } }
        //public bool? RepairShowBarCode { get { return repairOrderTypeFields.ShowBarCode; } set { repairOrderTypeFields.ShowBarCode = value; } }
        //public bool? RepairShowSerialNumber { get { return repairOrderTypeFields.ShowSerialNumber; } set { repairOrderTypeFields.ShowSerialNumber = value; } }
        //public bool? RepairShowCrewName { get { return repairOrderTypeFields.ShowCrewName; } set { repairOrderTypeFields.ShowCrewName = value; } }
        //public bool? RepairShowHours { get { return repairOrderTypeFields.ShowHours; } set { repairOrderTypeFields.ShowHours = value; } }
        //public bool? RepairShowPickTime { get { return repairOrderTypeFields.ShowPickTime; } set { repairOrderTypeFields.ShowPickTime = value; } }
        //public bool? RepairShowAvailableQuantityAllWarehouses { get { return repairOrderTypeFields.ShowAvailableQuantityAllWarehouses; } set { repairOrderTypeFields.ShowAvailableQuantityAllWarehouses = value; } }
        //public bool? RepairShowConflictDateAllWarehouses { get { return repairOrderTypeFields.ShowConflictDateAllWarehouses; } set { repairOrderTypeFields.ShowConflictDateAllWarehouses = value; } }
        //public bool? RepairShowConsignmentAvailableQuantity { get { return repairOrderTypeFields.ShowConsignmentAvailableQuantity; } set { repairOrderTypeFields.ShowConsignmentAvailableQuantity = value; } }
        //public bool? RepairShowConsignmentConflictDate { get { return repairOrderTypeFields.ShowConsignmentConflictDate; } set { repairOrderTypeFields.ShowConsignmentConflictDate = value; } }
        //public bool? RepairShowConsignmentQuantity { get { return repairOrderTypeFields.ShowConsignmentQuantity; } set { repairOrderTypeFields.ShowConsignmentQuantity = value; } }
        //public bool? RepairShowInLocationQuantity { get { return repairOrderTypeFields.ShowInLocationQuantity; } set { repairOrderTypeFields.ShowInLocationQuantity = value; } }
        //public bool? RepairShowReservedItems { get { return repairOrderTypeFields.ShowReservedItems; } set { repairOrderTypeFields.ShowReservedItems = value; } }
        //public bool? RepairShowWeeksAndDays { get { return repairOrderTypeFields.ShowWeeksAndDays; } set { repairOrderTypeFields.ShowWeeksAndDays = value; } }
        //public bool? RepairShowMonthsAndDays { get { return repairOrderTypeFields.ShowMonthsAndDays; } set { repairOrderTypeFields.ShowMonthsAndDays = value; } }
        //public bool? RepairShowPremiumPercent { get { return repairOrderTypeFields.ShowPremiumPercent; } set { repairOrderTypeFields.ShowPremiumPercent = value; } }
        //public bool? RepairShowDepartment { get { return repairOrderTypeFields.ShowDepartment; } set { repairOrderTypeFields.ShowDepartment = value; } }
        //public bool? RepairShowLocation { get { return repairOrderTypeFields.ShowLocation; } set { repairOrderTypeFields.ShowLocation = value; } }
        //public bool? RepairShowOrderActivity { get { return repairOrderTypeFields.ShowOrderActivity; } set { repairOrderTypeFields.ShowOrderActivity = value; } }
        //public bool? RepairShowSubOrderNumber { get { return repairOrderTypeFields.ShowSubOrderNumber; } set { repairOrderTypeFields.ShowSubOrderNumber = value; } }
        //public bool? RepairShowOrderStatus { get { return repairOrderTypeFields.ShowOrderStatus; } set { repairOrderTypeFields.ShowOrderStatus = value; } }
        //public bool? RepairShowEpisodes { get { return repairOrderTypeFields.ShowEpisodes; } set { repairOrderTypeFields.ShowEpisodes = value; } }
        //public bool? RepairShowEpisodeExtended { get { return repairOrderTypeFields.ShowEpisodeExtended; } set { repairOrderTypeFields.ShowEpisodeExtended = value; } }
        //public bool? RepairShowEpisodeDiscountAmount { get { return repairOrderTypeFields.ShowEpisodeDiscountAmount; } set { repairOrderTypeFields.ShowEpisodeDiscountAmount = value; } }
        public string RepairDateStamp { get { return repairOrderTypeFields.DateStamp; } set { repairOrderTypeFields.DateStamp = value; } }

        public bool? RwNetDefaultRental { get { return poType.Rwnetrental; } set { poType.Rwnetrental = value; } }
        public bool? RwNetDefaultMisc { get { return poType.Rwnetmisc; } set { poType.Rwnetmisc = value; } }
        public bool? RwNetDefaultLabor { get { return poType.Rwnetlabor; } set { poType.Rwnetlabor = value; } }



        [JsonIgnore]
        public string OrdType { get { return poType.Ordtype; } set { poType.Ordtype = value; } }
        public decimal? OrderBy { get { return poType.Orderby; } set { poType.Orderby = value; } }
        public bool? Inactive { get { return poType.Inactive; } set { poType.Inactive = value; } }
        public string DateStamp { get { return poType.DateStamp; } set { poType.DateStamp = value; } }



        [FwBusinessLogicField(isReadOnly: true)]
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


        [FwBusinessLogicField(isReadOnly: true)]
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




        [FwBusinessLogicField(isReadOnly: true)]
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



        [FwBusinessLogicField(isReadOnly: true)]
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
                //if (SubLaborShowHours == true)
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


        [FwBusinessLogicField(isReadOnly: true)]
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




        [FwBusinessLogicField(isReadOnly: true)]
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



        [FwBusinessLogicField(isReadOnly: true)]
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
                //if (LaborShowHours == true) { showFields.Add("Hours"); }
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
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            OrdType = "PO";
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSavePoType(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (purchaseOrderTypeFields.OrderTypeFieldsId.Equals(string.Empty)))
            {
                PoTypeLogic l2 = new PoTypeLogic();
                l2.AppConfig = poType.AppConfig;
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<PoTypeLogic>(pk).Result;
                purchaseOrderTypeFields.OrderTypeFieldsId = l2.PurchaseOrderTypeFieldsId;
                subRentalOrderTypeFields.OrderTypeFieldsId = l2.SubRentalOrderTypeFieldsId;
                subSaleOrderTypeFields.OrderTypeFieldsId = l2.SubSaleOrderTypeFieldsId;
                laborOrderTypeFields.OrderTypeFieldsId = l2.LaborOrderTypeFieldsId;
                subLaborOrderTypeFields.OrderTypeFieldsId = l2.SubLaborOrderTypeFieldsId;
                miscOrderTypeFields.OrderTypeFieldsId = l2.MiscOrderTypeFieldsId;
                subMiscOrderTypeFields.OrderTypeFieldsId = l2.SubMiscOrderTypeFieldsId;
                repairOrderTypeFields.OrderTypeFieldsId = l2.RepairOrderTypeFieldsId;
            }
        }
        //------------------------------------------------------------------------------------   
        public void OnAfterSaveRepairFields(object sender, AfterSaveDataRecordEventArgs e)
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