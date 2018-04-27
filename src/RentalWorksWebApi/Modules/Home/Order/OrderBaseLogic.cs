using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebApi.Modules.Home.Address;
using WebLibrary;
using System.Threading.Tasks;
using WebApi.Modules.Home.Quote;
using WebApi.Modules.Home.Tax;
using System;
using FwStandard.SqlServer;

namespace WebApi.Modules.Home.Order
{
    public class OrderBaseLogic : AppBusinessLogic
    {
        protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        protected AddressRecord billToAddress = new AddressRecord();
        protected TaxRecord tax = new TaxRecord();

        private string tmpTaxId = "";

        //------------------------------------------------------------------------------------
        public OrderBaseLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
            dataRecords.Add(billToAddress);
            dataRecords.Add(tax);
            dealOrder.BeforeSave += OnBeforeSaveDealOrder;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
            billToAddress.BeforeSave += OnBeforeSaveBillToAddress;


            billToAddress.UniqueId1 = dealOrder.OrderId;
            billToAddress.UniqueId2 = RwConstants.ADDRESS_TYPE_BILLING;


            tax.AssignPrimaryKeys += TaxAssignPrimaryKeys;
            tax.AfterSave += OnAfterSaveTax;

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
        public string ProjectManagerId { get { return dealOrder.ProjectManagerId; } set { dealOrder.ProjectManagerId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManager { get; set; }
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

        public bool? LockBillingDates { get { return dealOrderDetail.LockBillingDates; } set { dealOrderDetail.LockBillingDates = value; } }
        //------------------------------------------------------------------------------------
        public string DelayBillingSearchUntil { get { return dealOrderDetail.DelayBillingSearchUntil; } set { dealOrderDetail.DelayBillingSearchUntil = value; } }
        public bool? IncludePrepFeesInRentalRate { get { return dealOrderDetail.IncludePrepFeesInRentalRate; } set { dealOrderDetail.IncludePrepFeesInRentalRate = value; } }
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

        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }


        public string TaxId { get { return dealOrder.TaxId; } set { dealOrder.TaxId = value; } }
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }


        public bool? NoCharge { get { return dealOrder.NoCharge; } set { dealOrder.NoCharge = value; } }
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




        public bool? BillToAddressDifferentFromIssuedToAddress { get { return dealOrderDetail.BillToAddressDifferentFromIssuedToAddress; } set { dealOrderDetail.BillToAddressDifferentFromIssuedToAddress = value; } }
        public string BillToAddressId { get { return billToAddress.AddressId; } set { billToAddress.AddressId = value; } }
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


        public bool? RequireContactConfirmation { get { return dealOrderDetail.RequireContactConfirmation; } set { dealOrderDetail.RequireContactConfirmation = value; } }


        public bool? IncludeInBillingAnalysis { get { return dealOrder.IncludeInBillingAnalysis; } set { dealOrder.IncludeInBillingAnalysis = value; } }

        public string HiatusDiscountFrom { get { return dealOrder.HiatusDiscountFrom; } set { dealOrder.HiatusDiscountFrom = value; } }
        public bool? RoundTripRentals { get { return dealOrderDetail.RoundTripRentals; } set { dealOrderDetail.RoundTripRentals = value; } }

        public bool? InGroup { get { return dealOrder.InGroup; } set { dealOrder.InGroup = value; } }
        public int GroupNumber { get { return dealOrder.GroupNumber; } set { dealOrder.GroupNumber = value; } }


        //------------------------------------------------------------------------------------
        public string CoverLetterId { get { return dealOrderDetail.CoverLetterId; } set { dealOrderDetail.CoverLetterId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string CoverLetter { get; set; }
        //------------------------------------------------------------------------------------
        public string TermsConditionsId { get { return dealOrder.TermsConditionsId; } set { dealOrder.TermsConditionsId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string TermsConditions { get; set; }
        //------------------------------------------------------------------------------------
        public string SalesRepresentativeId { get { return dealOrderDetail.SalesRepresentativeId; } set { dealOrderDetail.SalesRepresentativeId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------

        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketType { get; set; }
        //------------------------------------------------------------------------------------
        public string MarketSegmentId { get { return dealOrderDetail.MarketSegmentId; } set { dealOrderDetail.MarketSegmentId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketSegment { get; set; }
        //------------------------------------------------------------------------------------
        public string MarketSegmentJobId { get { return dealOrderDetail.MarketSegmentJobId; } set { dealOrderDetail.MarketSegmentJobId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketSegmentJob { get; set; }
        //------------------------------------------------------------------------------------



        //------------------------------------------------------------------------------------


        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void TaxAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((TaxRecord)sender).TaxId = tmpTaxId;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveDealOrder(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = dealOrder.SetNumber().Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocation(AppConfig, UserSession, OfficeLocationId, "taxoptionid").Result;
                }
                tmpTaxId = AppFunc.GetNextIdAsync(AppConfig).Result;
                TaxId = tmpTaxId;
            }
            else
            {
                if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
                {
                    OrderBaseLogic l2 = null;
                    if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                    {
                        l2 = new QuoteLogic();
                    }
                    else
                    {
                        l2 = new OrderLogic();
                    }
                    l2.SetDependencies(this.AppConfig, this.UserSession);
                    object[] pk = GetPrimaryKeys();
                    if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                    {
                        bool b = l2.LoadAsync<QuoteLogic>(pk).Result;
                    }
                    else 
                    {
                        bool b = l2.LoadAsync<OrderLogic>(pk).Result;
                    }
                    tax.TaxId = l2.TaxId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSaveDealOrder(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                billToAddress.UniqueId1 = dealOrder.OrderId;
                saved = dealOrder.SavePoASync(PoNumber, PoAmount).Result;

                if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
                {
                    if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                    {
                        OrderBaseLogic l2 = null;
                        if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                        {
                            l2 = new QuoteLogic();
                        }
                        else
                        {
                            l2 = new OrderLogic();
                        }
                        l2.SetDependencies(this.AppConfig, this.UserSession);
                        object[] pk = GetPrimaryKeys();
                        if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                        {
                            bool b = l2.LoadAsync<QuoteLogic>(pk).Result;
                        }
                        else
                        {
                            bool b = l2.LoadAsync<OrderLogic>(pk).Result;
                        }
                        TaxId = l2.TaxId;

                        if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                        {
                            bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                        }
                    }
                }


            }
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveBillToAddress(object sender, BeforeSaveEventArgs e)
        {
            if (BillToAddressId.Equals(string.Empty))
            {
                e.PerformSave = false;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTax(object sender, AfterSaveEventArgs e)
        {
            if (e.SavePerformed)
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {
                    if ((TaxId == null) || (TaxId.Equals(string.Empty)))
                    {
                        OrderBaseLogic l2 = new OrderBaseLogic();
                        l2.SetDependencies(this.AppConfig, this.UserSession);
                        object[] pk = GetPrimaryKeys();
                        bool b = l2.LoadAsync<OrderBaseLogic>(pk).Result;
                        TaxId = l2.TaxId;
                    }

                    if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                    {
                        bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<OrderBaseLogic> CopyAsync<T>(QuoteOrderCopyRequest copyRequest)
        {
            string newQuoteId = await dealOrder.Copy(copyRequest);
            string[] keys = { newQuoteId };

            OrderBaseLogic lCopy = null;
            if (copyRequest.CopyToType.Equals(RwConstants.ORDER_TYPE_QUOTE))
            {
                lCopy = new QuoteLogic();
                lCopy.AppConfig = AppConfig;
                lCopy.UserSession = UserSession;
                bool x = await lCopy.LoadAsync<QuoteLogic>(keys);
            }
            else
            {
                lCopy = new OrderLogic();
                lCopy.AppConfig = AppConfig;
                lCopy.UserSession = UserSession;
                bool x = await lCopy.LoadAsync<OrderLogic>(keys);
            }
            return lCopy;

        }
        //------------------------------------------------------------------------------------    
    }
}
