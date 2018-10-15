using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebApi.Modules.Home.Tax;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    public class PurchaseOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord purchaseOrder = new DealOrderRecord();
        DealOrderDetailRecord purchaseOrderDetail = new DealOrderDetailRecord();
        TaxRecord tax = new TaxRecord();

        PurchaseOrderLoader purchaseOrderLoader = new PurchaseOrderLoader();
        PurchaseOrderBrowseLoader purchaseOrderBrowseLoader = new PurchaseOrderBrowseLoader();

        public PurchaseOrderLogic()
        {
            dataRecords.Add(purchaseOrder);
            dataRecords.Add(purchaseOrderDetail);
            dataRecords.Add(tax);
            dataLoader = purchaseOrderLoader;
            browseLoader = purchaseOrderBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
            purchaseOrder.BeforeSave += OnBeforeSavePurchaseOrder;
            purchaseOrder.AfterSave += OnAfterSavePurchaseOrder;
            tax.AfterSave += OnAfterSaveTax;

            Type = RwConstants.ORDER_TYPE_PURCHASE_ORDER;

        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PurchaseOrderId { get { return purchaseOrder.OrderId; } set { purchaseOrder.OrderId = value; purchaseOrderDetail.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PurchaseOrderNumber { get { return purchaseOrder.OrderNumber; } set { purchaseOrder.OrderNumber = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return purchaseOrder.Description; } set { purchaseOrder.Description = value; } }
        [JsonIgnore]
        public string Type { get { return purchaseOrder.Type; } set { purchaseOrder.Type = value; } }
        public string PurchaseOrderDate { get { return purchaseOrder.OrderDate; } set { purchaseOrder.OrderDate = value; } }
        public string RequisitionNumber { get { return purchaseOrder.RequisitionNumber; } set { purchaseOrder.RequisitionNumber = value; } }
        public string RequisitionDate { get { return purchaseOrder.RequisitionDate; } set { purchaseOrder.RequisitionDate = value; } }
        public string VendorId { get { return purchaseOrder.VendorId; } set { purchaseOrder.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        public string AgentId { get { return purchaseOrder.AgentId; } set { purchaseOrder.AgentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        public string Status { get { return purchaseOrder.Status; } set { purchaseOrder.Status = value; } }
        public string StatusDate { get { return purchaseOrder.StatusDate; } set { purchaseOrder.StatusDate = value; } }
        public string ReferenceNumber { get { return purchaseOrder.ReferenceNumber; } set { purchaseOrder.ReferenceNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NeedsApproval { get; set; }
        public string ApprovedByUserId { get { return purchaseOrder.ApprovedByUserId; } set { purchaseOrder.ApprovedByUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        //public string ApprovedBySecondUserId { get; set; }
        public string DepartmentId { get { return purchaseOrder.DepartmentId; } set { purchaseOrder.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string OfficeLocationId { get { return purchaseOrder.OfficeLocationId; } set { purchaseOrder.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string WarehouseId { get { return purchaseOrder.WarehouseId; } set { purchaseOrder.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityHolding { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityToBarCode { get; set; }
        public bool? Rental { get { return purchaseOrder.Rental; } set { purchaseOrder.Rental = value; } }
        public bool? Sales { get { return purchaseOrder.Sales; } set { purchaseOrder.Sales = value; } }
        public bool? Parts { get { return purchaseOrder.Parts; } set { purchaseOrder.Parts = value; } }
        public bool? Labor { get { return purchaseOrder.Labor; } set { purchaseOrder.Labor = value; } }
        public bool? Miscellaneous { get { return purchaseOrder.Miscellaneous; } set { purchaseOrder.Miscellaneous = value; } }
        public bool? Vehicle { get { return purchaseOrder.Vehicle; } set { purchaseOrder.Vehicle = value; } }
        public bool? SubRent { get { return purchaseOrder.SubRent; } set { purchaseOrder.SubRent = value; } }
        public bool? SubSale { get { return purchaseOrder.SubSale; } set { purchaseOrder.SubSale = value; } }
        public bool? SubLabor { get { return purchaseOrder.SubLabor; } set { purchaseOrder.SubLabor = value; } }
        public bool? SubMiscellaneous { get { return purchaseOrder.SubMiscellaneous; } set { purchaseOrder.SubMiscellaneous = value; } }
        public bool? SubVehicle { get { return purchaseOrder.SubVehicle; } set { purchaseOrder.SubVehicle = value; } }
        public bool? Repair { get { return purchaseOrder.Repair; } set { purchaseOrder.Repair = value; } }
        public bool? Consignment { get { return purchaseOrder.Consignment; } set { purchaseOrder.Consignment = value; } }
        public string ConsignorAgreementId { get { return purchaseOrder.ConsignorAgreementId; } set { purchaseOrder.ConsignorAgreementId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        public string RateType { get { return purchaseOrder.RateType; } set { purchaseOrder.RateType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DepartmentLocationRequiresApproval { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total { get; set; }
        public string PoTypeId { get { return purchaseOrder.OrderTypeId; } set { purchaseOrder.OrderTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoType { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string RequiredByDate { get; set; }
        public string PoClassificationId { get { return purchaseOrder.PoClassificationId; } set { purchaseOrder.PoClassificationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoClassification { get; set; }
        public string EstimatedStartDate { get { return purchaseOrder.EstimatedStartDate; } set { purchaseOrder.EstimatedStartDate = value; } }
        public string EstimatedStopDate { get { return purchaseOrder.EstimatedStopDate; } set { purchaseOrder.EstimatedStopDate = value; } }
        public string BillingStartDate { get { return purchaseOrder.BillingStartDate; } set { purchaseOrder.BillingStartDate = value; } }
        public string BillingEndDate { get { return purchaseOrder.BillingEndDate; } set { purchaseOrder.BillingEndDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoicedAmount { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? WeeklyExtended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatusId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatus { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatustype { get; set; }
        public string ProjectManagerId { get { return purchaseOrder.ProjectManagerId; } set { purchaseOrder.ProjectManagerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManager { get; set; }
        public string OutDeliveryId { get { return purchaseOrder.OutDeliveryId; } set { purchaseOrder.OutDeliveryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DropShip { get; set; }
        public string InDeliveryId { get { return purchaseOrder.InDeliveryId; } set { purchaseOrder.InDeliveryId = value; } }
        public string ProjectId { get { return purchaseOrder.ProjectId; } set { purchaseOrder.ProjectId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectDescription { get; set; }
        public string Location { get { return purchaseOrder.Location; } set { purchaseOrder.Location = value; } }
        public string CurrencyId { get { return purchaseOrder.CurrencyId; } set { purchaseOrder.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        public string BillingCycleId { get { return purchaseOrder.BillingCycleId; } set { purchaseOrder.BillingCycleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycle { get; set; }


        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }


        public string TaxId { get { return purchaseOrder.TaxId; } set { purchaseOrder.TaxId = value; } }
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }


        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasRentalItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasSalesItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasMiscellaneousItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasLaborItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasFacilitiesItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasLossAndDamageItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? RentalTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SalesTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PartsTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VehicleDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? VehicleTotalIncludesTax { get; set; }


        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MiscTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? LaborTotalIncludesTax { get; set; }










        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubRentalDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubRentalDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklySubRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlySubRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodSubRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklySubRentalTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlySubRentalTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodSubRentalTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubSalesDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubSalesTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SubSalesTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubVehicleDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubvehicleDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklySubVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlySubVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodSubVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklySubVehicleTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlySubVehicleTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodSubVehicleTotalIncludesTax { get; set; }


        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubMiscDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklySubMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlySubMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodSubMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklySubMiscTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlySubMiscTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodSubMiscTotalIncludesTax { get; set; }


        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubLaborDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklySubLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlySubLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodSubLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklySubLaborTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlySubLaborTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodSubLaborTotalIncludesTax { get; set; }


        //------------------------------------------------------------------------------------ 


        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
            }
            else
            {
                if (original != null)
                {
                    PurchaseOrderLogic lOrig = ((PurchaseOrderLogic)original);

                    if (isValid)
                    {
                        if (lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_SNAPSHOT) || lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_VOID))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------


        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.PURCHASE_ORDER_STATUS_NEW;
                if (string.IsNullOrEmpty(PurchaseOrderDate))
                {
                    PurchaseOrderDate = FwConvert.ToString(DateTime.Today);
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a new Quote/Order.  OutDeliveryId, InDeliveryId, and TaxId were not known at time of insert.  Need to re-update the data with the known ID's
                //purchaseOrder.OutDeliveryId = outDelivery.DeliveryId;
                //purchaseOrder.InDeliveryId = inDelivery.DeliveryId;
                purchaseOrder.TaxId = tax.TaxId;
                int i = purchaseOrder.SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSavePurchaseOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = purchaseOrder.SetNumber().Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocation(AppConfig, UserSession, OfficeLocationId, "taxoptionid").Result;
                }
            }
            else
            {
                if (e.Original != null)
                {
                    DealOrderRecord lOrig = ((DealOrderRecord)e.Original);

                    if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
                    {
                        tax.TaxId = lOrig.TaxId;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------

        public virtual void OnAfterSavePurchaseOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            //bool saved = false;
            //billToAddress.UniqueId1 = dealOrder.OrderId;
            //saved = dealOrder.SavePoASync(PoNumber, PoAmount).Result;

            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {

                    if (e.Original != null)
                    {
                        TaxId = ((DealOrderRecord)e.Original).TaxId;
                    }

                    if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                    {
                        bool b2 = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                    }
                }
            }

            bool b3 = purchaseOrder.UpdateOrderTotal().Result;
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTax(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
            {
                if ((TaxId == null) || (TaxId.Equals(string.Empty)))
                {
                    //PurchaseOrderLogic l2 = new PurchaseOrderLogic();
                    //l2.SetDependencies(this.AppConfig, this.UserSession);
                    //object[] pk = GetPrimaryKeys();
                    //bool b = l2.LoadAsync<PurchaseOrderLogic>(pk).Result;
                    //TaxId = l2.TaxId;
                }

                if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                {
                    bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDaysPerWeek(ApplyBottomLineDaysPerWeekRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineDaysPerWeek(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDiscountPercent(ApplyBottomLineDiscountPercentRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineDiscountPercent(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineTotal(ApplyBottomLineTotalRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineTotal(request);
            return success;
        }
        //------------------------------------------------------------------------------------

        public async Task<string> CreateReceiveContract()
        {
            string contractId = await purchaseOrder.CreateReceiveContract();
            return contractId;
        }
        //------------------------------------------------------------------------------------

        public async Task<string> CreateReturnContract()
        {
            string contractId = await purchaseOrder.CreateReturnContract();
            return contractId;
        }
        //------------------------------------------------------------------------------------

    }
}
