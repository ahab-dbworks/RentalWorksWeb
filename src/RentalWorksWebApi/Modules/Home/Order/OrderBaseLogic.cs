using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebApi.Modules.Home.Address;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.Order
{
    public abstract class OrderBaseLogic : AppBusinessLogic
    {
        protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        protected AddressRecord billToAddress = new AddressRecord();
        //------------------------------------------------------------------------------------
        public OrderBaseLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
            dataRecords.Add(billToAddress);
            dealOrder.BeforeSave += OnBeforeSaveDealOrder;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return dealOrder.Description; } set { dealOrder.Description = value; } }
        //------------------------------------------------------------------------------------
        public string OfficeLocationId { get { return dealOrder.OfficeLocationId; } set { dealOrder.OfficeLocationId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        public string WarehouseId { get { return dealOrder.WarehouseId; } set { dealOrder.WarehouseId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        public string DepartmentId { get { return dealOrder.DepartmentId; } set { dealOrder.DepartmentId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        public string DealId { get { return dealOrder.DealId; } set { dealOrder.DealId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------
        public string RateType { get { return dealOrder.RateType; } set { dealOrder.RateType = value; } }
        //------------------------------------------------------------------------------------
        public string AgentId { get { return dealOrder.AgentId; } set { dealOrder.AgentId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------

        public bool? Rental { get { return dealOrder.Rental; } set { dealOrder.Rental = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Sales { get { return dealOrder.Sales; } set { dealOrder.Sales = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Miscellaneous { get { return dealOrder.Miscellaneous; } set { dealOrder.Miscellaneous = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Labor { get { return dealOrder.Labor; } set { dealOrder.Labor = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Facilities { get { return dealOrder.Facilities; } set { dealOrder.Facilities = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Transportation { get { return dealOrder.Transportation; } set { dealOrder.Transportation = value; } }
        //------------------------------------------------------------------------------------ 


        public string PickDate { get { return dealOrder.PickDate; } set { dealOrder.PickDate = value; } }
        //------------------------------------------------------------------------------------
        public string PickTime { get { return dealOrder.PickTime; } set { dealOrder.PickTime = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStartDate { get { return dealOrder.EstimatedStartDate; } set { dealOrder.EstimatedStartDate = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStartTime { get { return dealOrder.EstimatedStartTime; } set { dealOrder.EstimatedStartTime = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStopDate { get { return dealOrder.EstimatedStopDate; } set { dealOrder.EstimatedStopDate = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStopTime { get { return dealOrder.EstimatedStopTime; } set { dealOrder.EstimatedStopTime = value; } }
        //------------------------------------------------------------------------------------
        public string OrderTypeId { get { return dealOrder.OrderTypeId; } set { dealOrder.OrderTypeId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------
        public bool? FlatPo { get { return dealOrder.FlatPo; } set { dealOrder.FlatPo = value; } }
        //------------------------------------------------------------------------------------
        public bool? PendingPo { get { return dealOrder.PendingPo; } set { dealOrder.PendingPo = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PoAmount { get; set; }
        //------------------------------------------------------------------------------------
        public string Location { get { return dealOrder.Location; } set { dealOrder.Location = value; } }
        //------------------------------------------------------------------------------------
        public string ReferenceNumber { get { return dealOrder.ReferenceNumber; } set { dealOrder.ReferenceNumber = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        public string Status { get { return dealOrder.Status; } set { dealOrder.Status = value; } }
        //------------------------------------------------------------------------------------
        public string StatusDate { get { return dealOrder.StatusDate; } set { dealOrder.StatusDate = value; } }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public string Type { get { return dealOrder.Type; } set { dealOrder.Type = value; } }
        //------------------------------------------------------------------------------------
        public decimal? MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }
        //------------------------------------------------------------------------------------
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }
        //------------------------------------------------------------------------------------

        public bool LockBillingDates { get { return dealOrderDetail.LockBillingDates; } set { dealOrderDetail.LockBillingDates = value; } }
        //------------------------------------------------------------------------------------
        public string DelayBillingSearchUntil { get { return dealOrderDetail.DelayBillingSearchUntil; } set { dealOrderDetail.DelayBillingSearchUntil = value; } }
        public bool IncludePrepFeesInRentalRate { get { return dealOrderDetail.IncludePrepFeesInRentalRate; } set { dealOrderDetail.IncludePrepFeesInRentalRate = value; } }
        public string BillingStartDate { get { return dealOrder.BillingStartDate; } set { dealOrder.BillingStartDate = value; } }
        public string BillingEndDate { get { return dealOrder.BillingEndDate; } set { dealOrder.BillingEndDate = value; } }
        public string DetermineQuantitiesToBillBasedOn { get { return dealOrder.DetermineQuantitiesToBillBasedOn; } set { dealOrder.DetermineQuantitiesToBillBasedOn = value; } }
        public string BillingCycleId { get { return dealOrder.BillingCycleId; } set { dealOrder.BillingCycleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycle { get; set; }
        public string PaymentTermsId { get { return dealOrder.PaymentTermsId; } set { dealOrder.PaymentTermsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public string PaymentTypeId { get { return dealOrder.PaymentTypeId; } set { dealOrder.PaymentTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentType { get; set; }
        public string CurrencyId { get { return dealOrder.CurrencyId; } set { dealOrder.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOptionId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }


        public string TaxId { get { return dealOrder.TaxId; } set { dealOrder.TaxId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate2 { get; set; }

        public bool NoCharge { get { return dealOrder.NoCharge; } set { dealOrder.NoCharge = value; } }
        public string NoChargeReason { get { return dealOrder.NoChargeReason; } set { dealOrder.NoChargeReason = value; } }
        //------------------------------------------------------------------------------------


        public string PrintIssuedToAddressFrom { get { return dealOrder.PrintIssuedToAddressFrom; } set { dealOrder.PrintIssuedToAddressFrom = value; } }
        public string IssuedToName { get { return dealOrder.IssuedToName; } set { dealOrder.IssuedToName = value; } }
        public string IssuedToAttention { get { return dealOrder.IssuedToAttention; } set { dealOrder.IssuedToAttention = value; } }
        public string IssuedToAttention2 { get { return dealOrder.IssuedToAttention2; } set { dealOrder.IssuedToAttention2 = value; } }
        public string IssuedToAddress1 { get { return dealOrder.IssuedToAddress1; } set { dealOrder.IssuedToAddress1 = value; } }
        public string IssuedToAddress2 { get { return dealOrder.IssuedToAddress2; } set { dealOrder.IssuedToAddress2 = value; } }
        public string IssuedToCity { get { return dealOrder.IssuedToCity; } set { dealOrder.IssuedToCity = value; } }
        public string IssuedToState { get { return dealOrder.IssuedToState; } set { dealOrder.IssuedToState = value; } }
        public string IssuedToZipCode { get { return dealOrder.IssuedToZipCode; } set { dealOrder.IssuedToZipCode = value; } }
        public string IssuedToCountryId { get { return dealOrder.IssuedToCountryId; } set { dealOrder.IssuedToCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string IssuedToCountry { get; set; }


        

        public bool BillToAddressDifferentFromIssuedToAddress { get { return dealOrderDetail.BillToAddressDifferentFromIssuedToAddress; } set { dealOrderDetail.BillToAddressDifferentFromIssuedToAddress = value; } }
        public string BillToName { get { return billToAddress.Name; } set { billToAddress.Name = value; } }
        public string BillToAttention { get { return billToAddress.Attention; } set { billToAddress.Attention = value; } }
        public string BillToAttention2 { get { return billToAddress.Attention2; } set { billToAddress.Attention2 = value; } }
        public string BillToAddress1 { get { return billToAddress.Address1; } set { billToAddress.Address1 = value; } }
        public string BillToAddress2 { get { return billToAddress.Address2; } set { billToAddress.Address2 = value; } }
        public string BillToCity { get { return billToAddress.City; } set { billToAddress.City = value; } }
        public string BillToState { get { return billToAddress.State; } set { billToAddress.State = value; } }
        public string BillToZipCode { get { return billToAddress.ZipCode; } set { billToAddress.ZipCode = value; } }
        public string BillToCountryId { get { return billToAddress.CountryId; } set { billToAddress.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToCountry { get; set; }




        public string DiscountReasonId { get { return dealOrderDetail.DiscountReasonId; } set { dealOrderDetail.DiscountReasonId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DiscountReason { get; set; }


        public bool RequireContactConfirmation { get { return dealOrderDetail.RequireContactConfirmation; } set { dealOrderDetail.RequireContactConfirmation = value; } }


        public bool IncludeInBillingAnalysis { get { return dealOrder.IncludeInBillingAnalysis; } set { dealOrder.IncludeInBillingAnalysis = value; } }

        public string HiatusDiscountFrom { get { return dealOrder.HiatusDiscountFrom; } set { dealOrder.HiatusDiscountFrom = value; } }
        public bool RoundTripRentals { get { return dealOrderDetail.RoundTripRentals; } set { dealOrderDetail.RoundTripRentals = value; } }

        public bool InGroup { get { return dealOrder.InGroup; } set { dealOrder.InGroup = value; } }
        public int GroupNumber { get { return dealOrder.GroupNumber; } set { dealOrder.GroupNumber = value; } }


        //------------------------------------------------------------------------------------


        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveDealOrder(object sender, BeforeSaveEventArgs e)
        {
            bool saved = false;
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                saved = dealOrder.SetNumber().Result;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveDealOrder(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = dealOrder.SavePoASync(PoNumber, PoAmount).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
