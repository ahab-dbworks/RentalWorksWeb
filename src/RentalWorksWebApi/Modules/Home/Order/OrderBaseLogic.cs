﻿using FwStandard.BusinessLogic;
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
using WebApi.Modules.Home.Delivery;

namespace WebApi.Modules.Home.Order
{
    public class OrderBaseLogic : AppBusinessLogic
    {
        protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        protected AddressRecord billToAddress = new AddressRecord();
        protected TaxRecord tax = new TaxRecord();
        protected DeliveryRecord outDelivery = new DeliveryRecord();
        protected DeliveryRecord inDelivery = new DeliveryRecord();
        //------------------------------------------------------------------------------------
        public OrderBaseLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
            dataRecords.Add(billToAddress);
            dataRecords.Add(tax);
            dataRecords.Add(outDelivery);
            dataRecords.Add(inDelivery);

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;

            dealOrder.BeforeSave += OnBeforeSaveDealOrder;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
            billToAddress.BeforeSave += OnBeforeSaveBillToAddress;
            billToAddress.UniqueId1 = dealOrder.OrderId;
            billToAddress.UniqueId2 = RwConstants.ADDRESS_TYPE_BILLING;
            tax.AfterSave += OnAfterSaveTax;
        }
        //------------------------------------------------------------------------------------
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
        public bool? RentalSale { get { return dealOrder.RentalSale; } set { dealOrder.RentalSale = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? LossAndDamage { get { return dealOrder.LossAndDamage; } set { dealOrder.LossAndDamage = value; } }
        //------------------------------------------------------------------------------------ 
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
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? OrderTypeCombineActivityTabs { get; set; }
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
        //public decimal? MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }
        //------------------------------------------------------------------------------------
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }
        //------------------------------------------------------------------------------------

        public bool? LockBillingDates { get { return dealOrderDetail.LockBillingDates; } set { dealOrderDetail.LockBillingDates = value; } }
        //------------------------------------------------------------------------------------
        public string DelayBillingSearchUntil { get { return dealOrderDetail.DelayBillingSearchUntil; } set { dealOrderDetail.DelayBillingSearchUntil = value; } }
        public bool? IncludePrepFeesInRentalRate { get { return dealOrderDetail.IncludePrepFeesInRentalRate; } set { dealOrderDetail.IncludePrepFeesInRentalRate = value; } }
        public string BillingStartDate { get { return dealOrder.BillingStartDate; } set { dealOrder.BillingStartDate = value; } }
        public string BillingEndDate { get { return dealOrder.BillingEndDate; } set { dealOrder.BillingEndDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? BillingWeeks { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? BillingMonths { get; set; }
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


        public string TaxId { get { return dealOrder.TaxId; } set { dealOrder.TaxId = value; tax.TaxId = value; } }
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
        public int? GroupNumber { get { return dealOrder.GroupNumber; } set { dealOrder.GroupNumber = value; } }


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
        public string OutsideSalesRepresentativeId { get { return dealOrderDetail.OutsideSalesRepresentativeId; } set { dealOrderDetail.OutsideSalesRepresentativeId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }
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








        public string OutDeliveryId { get { return dealOrder.OutDeliveryId; } set { dealOrder.OutDeliveryId = value; outDelivery.DeliveryId = value; } }
        public string OutDeliveryDeliveryType { get { return outDelivery.DeliveryType; } set { outDelivery.DeliveryType = value; } }
        public string OutDeliveryRequiredDate { get { return outDelivery.RequiredDate; } set { outDelivery.RequiredDate = value; } }
        public string OutDeliveryRequiredTime { get { return outDelivery.RequiredTime; } set { outDelivery.RequiredTime = value; } }
        public string OutDeliveryTargetShipDate { get { return outDelivery.TargetShipDate; } set { outDelivery.TargetShipDate = value; } }
        public string OutDeliveryTargetShipTime { get { return outDelivery.TargetShipTime; } set { outDelivery.TargetShipTime = value; } }
        public string OutDeliveryDirection { get { return outDelivery.Direction; } set { outDelivery.Direction = value; } }
        public string OutDeliveryAddressType { get { return outDelivery.AddressType; } set { outDelivery.AddressType = value; } }
        public string OutDeliveryFromLocation { get { return outDelivery.FromLocation; } set { outDelivery.FromLocation = value; } }
        public string OutDeliveryFromContact { get { return outDelivery.FromContact; } set { outDelivery.FromContact = value; } }
        public string OutDeliveryFromContactPhone { get { return outDelivery.FromContactPhone; } set { outDelivery.FromContactPhone = value; } }
        public string OutDeliveryFromAlternateContact { get { return outDelivery.FromAlternateContact; } set { outDelivery.FromAlternateContact = value; } }
        public string OutDeliveryFromAlternateContactPhone { get { return outDelivery.FromAlternateContactPhone; } set { outDelivery.FromAlternateContactPhone = value; } }
        public string OutDeliveryFromAttention { get { return outDelivery.FromAttention; } set { outDelivery.FromAttention = value; } }
        public string OutDeliveryFromAddress1 { get { return outDelivery.FromAddress1; } set { outDelivery.FromAddress1 = value; } }
        public string OutDeliveryFromAdd2ress { get { return outDelivery.FromAdd2ress; } set { outDelivery.FromAdd2ress = value; } }
        public string OutDeliveryFromCity { get { return outDelivery.FromCity; } set { outDelivery.FromCity = value; } }
        public string OutDeliveryFromState { get { return outDelivery.FromState; } set { outDelivery.FromState = value; } }
        public string OutDeliveryFromZipCode { get { return outDelivery.FromZipCode; } set { outDelivery.FromZipCode = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutDeliveryFromCountry { get; set; }
        public string OutDeliveryFromCountryId { get { return outDelivery.FromCountryId; } set { outDelivery.FromCountryId = value; } }
        public string OutDeliveryFromCrossStreets { get { return outDelivery.FromCrossStreets; } set { outDelivery.FromCrossStreets = value; } }
        public string OutDeliveryToLocation { get { return outDelivery.Location; } set { outDelivery.Location = value; } }
        public string OutDeliveryToContact { get { return outDelivery.Contact; } set { outDelivery.Contact = value; } }
        public string OutDeliveryToContactPhone { get { return outDelivery.ContactPhone; } set { outDelivery.ContactPhone = value; } }
        public string OutDeliveryToAlternateContact { get { return outDelivery.AlternateContact; } set { outDelivery.AlternateContact = value; } }
        public string OutDeliveryToAlternateContactPhone { get { return outDelivery.AlternateContactPhone; } set { outDelivery.AlternateContactPhone = value; } }
        public string OutDeliveryToAttention { get { return outDelivery.Attention; } set { outDelivery.Attention = value; } }
        public string OutDeliveryToAddress1 { get { return outDelivery.Address1; } set { outDelivery.Address1 = value; } }
        public string OutDeliveryToAddress2 { get { return outDelivery.Address2; } set { outDelivery.Address2 = value; } }
        public string OutDeliveryToCity { get { return outDelivery.City; } set { outDelivery.City = value; } }
        public string OutDeliveryToState { get { return outDelivery.State; } set { outDelivery.State = value; } }
        public string OutDeliveryToZipCode { get { return outDelivery.ZipCode; } set { outDelivery.ZipCode = value; } }
        public string OutDeliveryToCountryId { get { return outDelivery.CountryId; } set { outDelivery.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutDeliveryToCountry { get; set; }
        public string OutDeliveryToContactFax { get { return outDelivery.ContactFax; } set { outDelivery.ContactFax = value; } }
        public string OutDeliveryToCrossStreets { get { return outDelivery.CrossStreets; } set { outDelivery.CrossStreets = value; } }
        public string OutDeliveryDeliveryNotes { get { return outDelivery.DeliveryNotes; } set { outDelivery.DeliveryNotes = value; } }
        public string OutDeliveryCarrierId { get { return outDelivery.CarrierId; } set { outDelivery.CarrierId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutDeliveryCarrier { get; set; }
        public string OutDeliveryCarrierAccount { get { return outDelivery.CarrierAccount; } set { outDelivery.CarrierAccount = value; } }
        public string OutDeliveryShipViaId { get { return outDelivery.ShipViaId; } set { outDelivery.ShipViaId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutDeliveryShipVia { get; set; }
        public string OutDeliveryInvoiceId { get { return outDelivery.InvoiceId; } set { outDelivery.InvoiceId = value; } }
        public string OutDeliveryVendorInvoiceId { get { return outDelivery.VendorInvoiceId; } set { outDelivery.VendorInvoiceId = value; } }
        public decimal? OutDeliveryEstimatedFreight { get { return outDelivery.EstimatedFreight; } set { outDelivery.EstimatedFreight = value; } }
        public decimal? OutDeliveryFreightInvoiceAmount { get { return outDelivery.FreightInvoiceAmount; } set { outDelivery.FreightInvoiceAmount = value; } }
        public string OutDeliveryChargeType { get { return outDelivery.ChargeType; } set { outDelivery.ChargeType = value; } }
        public string OutDeliveryFreightTrackingNumber { get { return outDelivery.FreightTrackingNumber; } set { outDelivery.FreightTrackingNumber = value; } }
        public bool? OutDeliveryDropShip { get { return outDelivery.DropShip; } set { outDelivery.DropShip = value; } }
        public string OutDeliveryPackageCode { get { return outDelivery.PackageCode; } set { outDelivery.PackageCode = value; } }
        public bool? OutDeliveryBillPoFreightOnOrder { get { return outDelivery.BillPoFreightOnOrder; } set { outDelivery.BillPoFreightOnOrder = value; } }
        public string OutDeliveryOnlineOrderNumber { get { return outDelivery.OnlineOrderNumber; } set { outDelivery.OnlineOrderNumber = value; } }
        public string OutDeliveryOnlineOrderStatus { get { return outDelivery.OnlineOrderStatus; } set { outDelivery.OnlineOrderStatus = value; } }
        public string OutDeliveryDateStamp { get { return outDelivery.DateStamp; } set { outDelivery.DateStamp = value; } }









        public string InDeliveryId { get { return dealOrder.InDeliveryId; } set { dealOrder.InDeliveryId = value; inDelivery.DeliveryId = value; } }
        public string InDeliveryDeliveryType { get { return inDelivery.DeliveryType; } set { inDelivery.DeliveryType = value; } }
        public string InDeliveryRequiredDate { get { return inDelivery.RequiredDate; } set { inDelivery.RequiredDate = value; } }
        public string InDeliveryRequiredTime { get { return inDelivery.RequiredTime; } set { inDelivery.RequiredTime = value; } }
        public string InDeliveryTargetShipDate { get { return inDelivery.TargetShipDate; } set { inDelivery.TargetShipDate = value; } }
        public string InDeliveryTargetShipTime { get { return inDelivery.TargetShipTime; } set { inDelivery.TargetShipTime = value; } }
        public string InDeliveryDirection { get { return inDelivery.Direction; } set { inDelivery.Direction = value; } }
        public string InDeliveryAddressType { get { return inDelivery.AddressType; } set { inDelivery.AddressType = value; } }
        public string InDeliveryFromLocation { get { return inDelivery.FromLocation; } set { inDelivery.FromLocation = value; } }
        public string InDeliveryFromContact { get { return inDelivery.FromContact; } set { inDelivery.FromContact = value; } }
        public string InDeliveryFromContactPhone { get { return inDelivery.FromContactPhone; } set { inDelivery.FromContactPhone = value; } }
        public string InDeliveryFromAlternateContact { get { return inDelivery.FromAlternateContact; } set { inDelivery.FromAlternateContact = value; } }
        public string InDeliveryFromAlternateContactPhone { get { return inDelivery.FromAlternateContactPhone; } set { inDelivery.FromAlternateContactPhone = value; } }
        public string InDeliveryFromAttention { get { return inDelivery.FromAttention; } set { inDelivery.FromAttention = value; } }
        public string InDeliveryFromAddress1 { get { return inDelivery.FromAddress1; } set { inDelivery.FromAddress1 = value; } }
        public string InDeliveryFromAdd2ress { get { return inDelivery.FromAdd2ress; } set { inDelivery.FromAdd2ress = value; } }
        public string InDeliveryFromCity { get { return inDelivery.FromCity; } set { inDelivery.FromCity = value; } }
        public string InDeliveryFromState { get { return inDelivery.FromState; } set { inDelivery.FromState = value; } }
        public string InDeliveryFromZipCode { get { return inDelivery.FromZipCode; } set { inDelivery.FromZipCode = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InDeliveryFromCountry { get; set; }
        public string InDeliveryFromCountryId { get { return inDelivery.FromCountryId; } set { inDelivery.FromCountryId = value; } }
        public string InDeliveryFromCrossStreets { get { return inDelivery.FromCrossStreets; } set { inDelivery.FromCrossStreets = value; } }
        public string InDeliveryToLocation { get { return inDelivery.Location; } set { inDelivery.Location = value; } }
        public string InDeliveryToContact { get { return inDelivery.Contact; } set { inDelivery.Contact = value; } }
        public string InDeliveryToContactPhone { get { return inDelivery.ContactPhone; } set { inDelivery.ContactPhone = value; } }
        public string InDeliveryToAlternateContact { get { return inDelivery.AlternateContact; } set { inDelivery.AlternateContact = value; } }
        public string InDeliveryToAlternateContactPhone { get { return inDelivery.AlternateContactPhone; } set { inDelivery.AlternateContactPhone = value; } }
        public string InDeliveryToAttention { get { return inDelivery.Attention; } set { inDelivery.Attention = value; } }
        public string InDeliveryToAddress1 { get { return inDelivery.Address1; } set { inDelivery.Address1 = value; } }
        public string InDeliveryToAddress2 { get { return inDelivery.Address2; } set { inDelivery.Address2 = value; } }
        public string InDeliveryToCity { get { return inDelivery.City; } set { inDelivery.City = value; } }
        public string InDeliveryToState { get { return inDelivery.State; } set { inDelivery.State = value; } }
        public string InDeliveryToZipCode { get { return inDelivery.ZipCode; } set { inDelivery.ZipCode = value; } }
        public string InDeliveryToCountryId { get { return inDelivery.CountryId; } set { inDelivery.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InDeliveryToCountry { get; set; }
        public string InDeliveryToContactFax { get { return inDelivery.ContactFax; } set { inDelivery.ContactFax = value; } }
        public string InDeliveryToCrossStreets { get { return inDelivery.CrossStreets; } set { inDelivery.CrossStreets = value; } }
        public string InDeliveryDeliveryNotes { get { return inDelivery.DeliveryNotes; } set { inDelivery.DeliveryNotes = value; } }
        public string InDeliveryCarrierId { get { return inDelivery.CarrierId; } set { inDelivery.CarrierId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InDeliveryCarrier { get; set; }
        public string InDeliveryCarrierAccount { get { return inDelivery.CarrierAccount; } set { inDelivery.CarrierAccount = value; } }
        public string InDeliveryShipViaId { get { return inDelivery.ShipViaId; } set { inDelivery.ShipViaId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InDeliveryShipVia { get; set; }
        public string InDeliveryInvoiceId { get { return inDelivery.InvoiceId; } set { inDelivery.InvoiceId = value; } }
        public string InDeliveryVendorInvoiceId { get { return inDelivery.VendorInvoiceId; } set { inDelivery.VendorInvoiceId = value; } }
        public decimal? InDeliveryEstimatedFreight { get { return inDelivery.EstimatedFreight; } set { inDelivery.EstimatedFreight = value; } }
        public decimal? InDeliveryFreightInvoiceAmount { get { return inDelivery.FreightInvoiceAmount; } set { inDelivery.FreightInvoiceAmount = value; } }
        public string InDeliveryChargeType { get { return inDelivery.ChargeType; } set { inDelivery.ChargeType = value; } }
        public string InDeliveryFreightTrackingNumber { get { return inDelivery.FreightTrackingNumber; } set { inDelivery.FreightTrackingNumber = value; } }
        public bool? InDeliveryDropShip { get { return inDelivery.DropShip; } set { inDelivery.DropShip = value; } }
        public string InDeliveryPackageCode { get { return inDelivery.PackageCode; } set { inDelivery.PackageCode = value; } }
        public bool? InDeliveryBillPoFreightOnOrder { get { return inDelivery.BillPoFreightOnOrder; } set { inDelivery.BillPoFreightOnOrder = value; } }
        public string InDeliveryOnlineOrderNumber { get { return inDelivery.OnlineOrderNumber; } set { inDelivery.OnlineOrderNumber = value; } }
        public string InDeliveryOnlineOrderStatus { get { return inDelivery.OnlineOrderStatus; } set { inDelivery.OnlineOrderStatus = value; } }
        public string InDeliveryDateStamp { get { return inDelivery.DateStamp; } set { inDelivery.DateStamp = value; } }




        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodRentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklyRentalTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlyRentalTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodRentalTotalIncludesTax { get; set; }

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
        public decimal? SpaceDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SpaceDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SpaceSplitPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklySpaceTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlySpaceTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodSpaceTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklySpaceTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlySpaceTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodSpaceTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VehicleDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? VehicleDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodVehicleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklyVehicleTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlyVehicleTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodVehicleTotalIncludesTax { get; set; }


        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodMiscTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklyMiscTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlyMiscTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodMiscTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodLaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklyLaborTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlyLaborTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodLaborTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? RentalSaleTotalIncludesTax { get; set; }

        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CombinedDaysPerWeek { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CombinedDiscountPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyCombinedTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyCombinedTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PeriodCombinedTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? WeeklyCombinedTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? MonthlyCombinedTotalIncludesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PeriodCombinedTotalIncludesTax { get; set; }

        //------------------------------------------------------------------------------------ 

        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingRentalRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingSalesRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingMiscellaneousRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingLaborRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingUsedSaleRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DisableEditingLossAndDamageRate { get; set; }
        //------------------------------------------------------------------------------------ 



        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasNotes { get; set; }


        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string QuoteOrderTitle { get; set; }
        //------------------------------------------------------------------------------------
        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------

        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            bool rental = false;
            bool rentalsale = false;

            OrderBaseLogic lOrig = null;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (Rental.HasValue)
                {
                    rental = Rental.Value;
                }
                if (RentalSale.HasValue)
                {
                    rentalsale = RentalSale.Value;
                }
            }
            else  //  (updating)
            {
                if (original != null)
                {
                    if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                    {
                        lOrig = ((QuoteLogic)original);
                    }
                    else
                    {
                        lOrig = ((OrderLogic)original);
                    }

                    if (Rental.HasValue)
                    {
                        rental = Rental.Value;
                    }
                    else
                    {
                        rental = lOrig.Rental.Value;
                    }

                    if (RentalSale.HasValue)
                    {
                        rentalsale = RentalSale.Value;
                    }
                    else
                    {
                        rentalsale = lOrig.RentalSale.Value;
                    }

                    if (DealId != null)
                    {
                        if (lOrig.DealId == null)
                        {
                            lOrig.DealId = "";
                        }
                        if (!DealId.Equals(lOrig.DealId))  // changing the Deal on this Quote/Order
                        {
                            if (lOrig.HasLossAndDamageItem.GetValueOrDefault(false))
                            {
                                isValid = false;
                                validateMsg = "Cannot change the Deal on this " + BusinessLogicModuleName + " because Loss and Damage items have already been added.";
                            }
                        }
                    }

                    if (isValid)
                    {
                        if ((Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) && (lOrig.Status.Equals(RwConstants.QUOTE_STATUS_ORDERED) || lOrig.Status.Equals(RwConstants.QUOTE_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.QUOTE_STATUS_CANCELLED)))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
                        }
                    }

                    if (isValid)
                    {
                        if ((Type.Equals(RwConstants.ORDER_TYPE_ORDER)) && (lOrig.Status.Equals(RwConstants.ORDER_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.ORDER_STATUS_SNAPSHOT) || lOrig.Status.Equals(RwConstants.ORDER_STATUS_CANCELLED)))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
                        }
                    }
                }

            }

            if (isValid)
            {
                if (rental && rentalsale)
                {
                    isValid = false;
                    validateMsg = "Cannot have both Rental and RentalSale on the same " + BusinessLogicModuleName + ".";
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    OrderBaseLogic orig = ((OrderBaseLogic)e.Original);
                    OutDeliveryId = orig.OutDeliveryId;
                    InDeliveryId = orig.InDeliveryId;
                    BillToAddressId = orig.BillToAddressId;
                    TaxId = orig.TaxId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a new Quote/Order.  OutDeliveryId, InDeliveryId, and TaxId were not known at time of insert.  Need to re-update the data with the known ID's
                dealOrder.OutDeliveryId = outDelivery.DeliveryId;
                dealOrder.InDeliveryId = inDelivery.DeliveryId;
                dealOrder.TaxId = tax.TaxId;
                int i = dealOrder.SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------








        //------------------------------------------------------------------------------------
        public void OnBeforeSaveDealOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = dealOrder.SetNumber().Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocation(AppConfig, UserSession, OfficeLocationId, "taxoptionid").Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSaveDealOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = false;
            billToAddress.UniqueId1 = dealOrder.OrderId;
            saved = dealOrder.SavePoASync(PoNumber, PoAmount).Result;

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
                        bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveBillToAddress(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (BillToAddressId.Equals(string.Empty))
            {
                e.PerformSave = false;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTax(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {
                    if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                    {
                        bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<QuoteLogic> CopyToQuoteAsync<T>(QuoteOrderCopyRequest copyRequest)
        {
            string newQuoteId = await dealOrder.CopyToQuote(copyRequest);
            string[] keys = { newQuoteId };

            QuoteLogic lCopy = new QuoteLogic();
            lCopy.SetDependencies(AppConfig, UserSession);
            bool x = await lCopy.LoadAsync<QuoteLogic>(keys);
            return lCopy;
        }
        //------------------------------------------------------------------------------------
        public async Task<OrderLogic> CopyToOrderAsync<T>(QuoteOrderCopyRequest copyRequest)
        {
            string newOrderId = await dealOrder.CopyToOrder(copyRequest);
            string[] keys = { newOrderId };

            OrderLogic lCopy = new OrderLogic();
            lCopy.SetDependencies(AppConfig, UserSession);
            bool x = await lCopy.LoadAsync<OrderLogic>(keys);
            return lCopy;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDaysPerWeek(ApplyBottomLineDaysPerWeekRequest request)
        {
            bool success = await dealOrder.ApplyBottomLineDaysPerWeek(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDiscountPercent(ApplyBottomLineDiscountPercentRequest request)
        {
            bool success = await dealOrder.ApplyBottomLineDiscountPercent(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineTotal(ApplyBottomLineTotalRequest request)
        {
            bool success = await dealOrder.ApplyBottomLineTotal(request);
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}