using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Quote;
using WebApi.Modules.HomeControls.Address;
using WebApi.Modules.HomeControls.DealOrder;
using WebApi.Modules.HomeControls.DealOrderDetail;
using WebApi.Modules.HomeControls.Delivery;
using WebApi.Modules.HomeControls.OrderDates;
using WebApi.Modules.HomeControls.Tax;
using WebApi.Modules.Settings.OrderTypeDateType;
using WebApi.Modules.Settings.SystemSettings.DefaultSettings;
using WebApi;
using WebApi.Modules.Settings.OrderSettings.OrderType;
using System.Reflection;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.HomeControls.CompanyTaxOption;
using FwStandard.Models;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;

namespace WebApi.Modules.Agent.Order
{
    [FwLogic(Id: "fSlLfH5TYvRC")]
    public class OrderBaseLogic : AppBusinessLogic
    {
        protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        protected AddressRecord billToAddress = new AddressRecord();
        protected TaxRecord tax = new TaxRecord();
        protected DeliveryRecord outDelivery = new DeliveryRecord();
        protected DeliveryRecord inDelivery = new DeliveryRecord();

        private DealLogic insertingDeal = null;  // this object is loaded once during "Validate" (for speed) and used downstream 
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
            AfterMap += OnAfterMap;

            dealOrder.BeforeSave += OnBeforeSaveDealOrder;
            dealOrder.AfterSave += OnAfterSaveDealOrder;
            billToAddress.BeforeSave += OnBeforeSaveBillToAddress;
            billToAddress.UniqueId1 = dealOrder.OrderId;
            billToAddress.UniqueId2 = RwConstants.ADDRESS_TYPE_BILLING;
            tax.AfterSave += OnAfterSaveTax;
            dealOrderDetail.AssignPrimaryKeys += OrderDetailAssignPrimaryKeys;

            UseTransactionToSave = true;
            ForceSave = true;

        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "jic1cq8SZoZk")]
        public string Description { get { return dealOrder.Description; } set { dealOrder.Description = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "HlE6RYRoSi5n", DisableDirectModify: true)]
        public string OfficeLocationId { get { return dealOrder.OfficeLocationId; } set { dealOrder.OfficeLocationId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "xxMzkHYeHUp1", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "TkkVVDhB0PDs", DisableDirectModify: true)]
        public string WarehouseId { get { return dealOrder.WarehouseId; } set { dealOrder.WarehouseId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "ssNLdYWifAA7", IsReadOnly: true)]
        public string Warehouse { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "FWqjxrD2MNvm", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "ljhXjiWv2REh")]
        public string DepartmentId { get { return dealOrder.DepartmentId; } set { dealOrder.DepartmentId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "Js4wkZ5i3CwQ", IsReadOnly: true)]
        public string Department { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "4Yy5oEjYMkzL", IsReadOnly: true)]
        public string CustomerId { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "cncMGmfbCHcg", IsReadOnly: true)]
        public string Customer { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "whbxK50pfxgf", IsReadOnly: true)]
        public string CustomerNumber { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "U8IN5vdwT4Bi")]
        public string DealId { get { return dealOrder.DealId; } set { dealOrder.DealId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "DTwTqqC8SYQN", IsReadOnly: true)]
        public string Deal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "9V3ZWR7qtJka", IsReadOnly: true)]
        public string DealNumber { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "AAnqS12iCWOa")]
        public string RateType { get { return dealOrder.RateType; } set { dealOrder.RateType = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "iZQDvZAabExyg", IsReadOnly: true)]
        public string RateTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "wf6V6mZB8RSQ")]
        public string AgentId { get { return dealOrder.AgentId; } set { dealOrder.AgentId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "cpEwr3EHSpfN", IsReadOnly: true)]
        public string Agent { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "7vpnHsnJXqdV")]
        public string ProjectManagerId { get { return dealOrder.ProjectManagerId; } set { dealOrder.ProjectManagerId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "jjTQES0YP52E", IsReadOnly: true)]
        public string ProjectManager { get; set; }

        //------------------------------------------------------------------------------------


        [FwLogicProperty(Id: "n38IDt8YXvoa")]
        public bool? Rental { get { return dealOrder.Rental; } set { dealOrder.Rental = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "KVY6ccb34eWS")]
        public bool? Sales { get { return dealOrder.Sales; } set { dealOrder.Sales = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bAdRHpqhpAXq")]
        public bool? Miscellaneous { get { return dealOrder.Miscellaneous; } set { dealOrder.Miscellaneous = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Du4IbkckBH36")]
        public bool? Labor { get { return dealOrder.Labor; } set { dealOrder.Labor = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "h5veYfmZqTvg")]
        public bool? Facilities { get { return dealOrder.Facilities; } set { dealOrder.Facilities = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bdrP2ss41glx")]
        public bool? Transportation { get { return dealOrder.Transportation; } set { dealOrder.Transportation = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "nlq5ULo7Vnt4")]
        public bool? RentalSale { get { return dealOrder.RentalSale; } set { dealOrder.RentalSale = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "PhJVRptGHZJ1")]
        public bool? LossAndDamage { get { return dealOrder.LossAndDamage; } set { dealOrder.LossAndDamage = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ieIieL9L5vCC", IsReadOnly: true)]
        public bool? HasRentalItem { get; set; }

        [FwLogicProperty(Id: "IUDN7t8Otly2", IsReadOnly: true)]
        public bool? HasSalesItem { get; set; }

        [FwLogicProperty(Id: "MzcOd13VlEBX", IsReadOnly: true)]
        public bool? HasMiscellaneousItem { get; set; }

        [FwLogicProperty(Id: "bxmzPbuNFhr7", IsReadOnly: true)]
        public bool? HasLaborItem { get; set; }

        [FwLogicProperty(Id: "POHiMl82qGpQ", IsReadOnly: true)]
        public bool? HasFacilitiesItem { get; set; }

        [FwLogicProperty(Id: "tnX6hNmKYrUK", IsReadOnly: true)]
        public bool? HasLossAndDamageItem { get; set; }

        [FwLogicProperty(Id: "ifAssZW9zbEu", IsReadOnly: true)]
        public bool? HasRentalSaleItem { get; set; }

        [FwLogicProperty(Id: "LAPj0iaAyE0Jl", IsReadOnly: true)]
        public bool? HasRepair { get; set; }

        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "y4trOTswSEcP3", IsNotAudited: true)]
        public List<OrderDatesLogic> ActivityDatesAndTimes { get; set; } = new List<OrderDatesLogic>();

        //------------------------------------------------------------------------------------ 


        [FwLogicProperty(Id: "m96kyHBAS8Pe")]
        public string PickDate { get { return dealOrder.PickDate; } set { dealOrder.PickDate = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "gqwb4u6l4atQ")]
        public string PickTime { get { return dealOrder.PickTime; } set { dealOrder.PickTime = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "vsJIBXIWUQe5")]
        public string EstimatedStartDate { get { return dealOrder.EstimatedStartDate; } set { dealOrder.EstimatedStartDate = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "XwEbz4hDcYYP")]
        public string EstimatedStartTime { get { return dealOrder.EstimatedStartTime; } set { dealOrder.EstimatedStartTime = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "b19mef3JCFmq")]
        public string EstimatedStopDate { get { return dealOrder.EstimatedStopDate; } set { dealOrder.EstimatedStopDate = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "snrXlJ4LQ1QF")]
        public string EstimatedStopTime { get { return dealOrder.EstimatedStopTime; } set { dealOrder.EstimatedStopTime = value; } }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "oBodDavk2oeXy")]
        public string PickUpDate { get { return dealOrder.PickUpDate; } set { dealOrder.PickUpDate = value; } }
        [FwLogicProperty(Id: "mot4tyZNWscWj")]
        public string PickUpTime { get { return dealOrder.PickUpTime; } set { dealOrder.PickUpTime = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ES4KMkO1XuzuI")]
        public string PrepDate { get { return dealOrder.PrepDate; } set { dealOrder.PrepDate = value; } }
        [FwLogicProperty(Id: "bZmuHZ5JvuvDl")]
        public string PrepTime { get { return dealOrder.PrepTime; } set { dealOrder.PrepTime = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "vWrZBknOvmHTb")]
        public string LoadInDate { get { return dealOrder.LoadInDate; } set { dealOrder.LoadInDate = value; } }
        [FwLogicProperty(Id: "4oZaprANyizIL")]
        public string LoadInTime { get { return dealOrder.LoadInTime; } set { dealOrder.LoadInTime = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "6Lj1eg6voS69o")]
        public string StrikeDate { get { return dealOrder.StrikeDate; } set { dealOrder.StrikeDate = value; } }
        [FwLogicProperty(Id: "2FwxtOYVAh4L5")]
        public string StrikeTime { get { return dealOrder.StrikeTime; } set { dealOrder.StrikeTime = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ydhsJYIZ58c9p")]
        public string TestDate { get { return dealOrder.TestDate; } set { dealOrder.TestDate = value; } }
        [FwLogicProperty(Id: "Ky4Xxt3Dub9gF")]
        public string TestTime { get { return dealOrder.TestTime; } set { dealOrder.TestTime = value; } }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "oKAEzwJNzY0T")]
        public string OrderTypeId { get { return dealOrder.OrderTypeId; } set { dealOrder.OrderTypeId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "S6oMGeqND3Cq", IsReadOnly: true)]
        public string OrderType { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "i8zepnd6x06e", IsReadOnly: true)]
        public bool? OrderTypeCombineActivityTabs { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "Wl2HkaT9CUHi")]
        public bool? FlatPo { get { return dealOrder.FlatPo; } set { dealOrder.FlatPo = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "k9Z7dVuOkb2J")]
        public bool? PendingPo { get { return dealOrder.PendingPo; } set { dealOrder.PendingPo = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "ifLeCNuO2prj", IsReadOnly: true)]
        public string PoNumber { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "i5LyMzHPpeU5", IsReadOnly: true)]
        public decimal? PoAmount { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "h7Oq3lqdtuWw")]
        public string Location { get { return dealOrder.Location; } set { dealOrder.Location = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "biDvrSEebbqU")]
        public string ReferenceNumber { get { return dealOrder.ReferenceNumber; } set { dealOrder.ReferenceNumber = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "r3RgAM0l5tsv", IsReadOnly: true)]
        public decimal? Total { get; set; }

        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "nR470R51RViU", DisableDirectModify: true)]
        public string Status { get { return dealOrder.Status; } set { dealOrder.Status = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "N5xzl3zlK1Nf", DisableDirectModify: true)]
        public string StatusDate { get { return dealOrder.StatusDate; } set { dealOrder.StatusDate = value; } }

        //------------------------------------------------------------------------------------
        [JsonIgnore]
        [FwLogicProperty(Id: "cKuWw9UhShBU", DisableDirectModify: true)]
        public string Type { get { return dealOrder.Type; } set { dealOrder.Type = value; } }

        //------------------------------------------------------------------------------------
        //[FwLogicProperty(Id:"Z2w6ptw3TFBW")]
        //public decimal? MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "h8TNLc9ZEjfO")]
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }

        //------------------------------------------------------------------------------------

        [FwLogicProperty(Id: "NNVMYIgBUcjs")]
        public bool? LockBillingDates { get { return dealOrderDetail.LockBillingDates; } set { dealOrderDetail.LockBillingDates = value; } }


        [FwLogicProperty(Id: "syicw91fEivDP")]
        public bool? SpecifyBillingDatesByType { get { return dealOrderDetail.SpecifyBillingDatesByType; } set { dealOrderDetail.SpecifyBillingDatesByType = value; } }

        [FwLogicProperty(Id: "nmYy0ngpZCSYi")]
        public string RentalBillingStartDate { get { return dealOrderDetail.RentalBillingStartDate; } set { dealOrderDetail.RentalBillingStartDate = value; } }
        [FwLogicProperty(Id: "xbcdyncQvx6v1")]
        public string RentalBillingEndDate { get { return dealOrderDetail.RentalBillingEndDate; } set { dealOrderDetail.RentalBillingEndDate = value; } }
        [FwLogicProperty(Id: "N7QQ3QqWqhAC4")]
        public string LaborBillingStartDate { get { return dealOrderDetail.LaborBillingStartDate; } set { dealOrderDetail.LaborBillingStartDate = value; } }
        [FwLogicProperty(Id: "XKCb85vN6iS3b")]
        public string LaborBillingEndDate { get { return dealOrderDetail.LaborBillingEndDate; } set { dealOrderDetail.LaborBillingEndDate = value; } }
        [FwLogicProperty(Id: "YksvfvZvU8JVo")]
        public string MiscellaneousBillingStartDate { get { return dealOrderDetail.MiscellaneousBillingStartDate; } set { dealOrderDetail.MiscellaneousBillingStartDate = value; } }
        [FwLogicProperty(Id: "cJ7bM7QI0VDIz")]
        public string MiscellaneousBillingEndDate { get { return dealOrderDetail.MiscellaneousBillingEndDate; } set { dealOrderDetail.MiscellaneousBillingEndDate = value; } }
        [FwLogicProperty(Id: "4j6DDStuP10IG")]
        public string FacilitiesBillingStartDate { get { return dealOrderDetail.FacilitiesBillingStartDate; } set { dealOrderDetail.FacilitiesBillingStartDate = value; } }
        [FwLogicProperty(Id: "Mof6qzREZMrjG")]
        public string FacilitiesBillingEndDate { get { return dealOrderDetail.FacilitiesBillingEndDate; } set { dealOrderDetail.FacilitiesBillingEndDate = value; } }
        [FwLogicProperty(Id: "M5nXbxaFhQSQQ")]
        public string VehicleBillingStartDate { get { return dealOrderDetail.VehicleBillingStartDate; } set { dealOrderDetail.VehicleBillingStartDate = value; } }
        [FwLogicProperty(Id: "lw2eFVbLZNu6w")]
        public string VehicleBillingEndDate { get { return dealOrderDetail.VehicleBillingEndDate; } set { dealOrderDetail.VehicleBillingEndDate = value; } }


        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "xzCqw9vuDcs2")]
        public string DelayBillingSearchUntil { get { return dealOrderDetail.DelayBillingSearchUntil; } set { dealOrderDetail.DelayBillingSearchUntil = value; } }

        [FwLogicProperty(Id: "TYnNYYcF4kkG")]
        public bool? IncludePrepFeesInRentalRate { get { return dealOrderDetail.IncludePrepFeesInRentalRate; } set { dealOrderDetail.IncludePrepFeesInRentalRate = value; } }

        [FwLogicProperty(Id: "wlLKLJzQcmyp")]
        public string BillingStartDate { get { return dealOrder.BillingStartDate; } set { dealOrder.BillingStartDate = value; } }

        [FwLogicProperty(Id: "3ipRddWbu7G1")]
        public string BillingEndDate { get { return dealOrder.BillingEndDate; } set { dealOrder.BillingEndDate = value; } }

        [FwLogicProperty(Id: "B9PwFzfAKBpV", IsReadOnly: true)]
        public decimal? BillingWeeks { get; set; }

        [FwLogicProperty(Id: "K7Ubp5PbqCpr", IsReadOnly: true)]
        public decimal? BillingMonths { get; set; }

        [FwLogicProperty(Id: "zWM15bmppESR")]
        public string DetermineQuantitiesToBillBasedOn { get { return dealOrder.DetermineQuantitiesToBillBasedOn; } set { dealOrder.DetermineQuantitiesToBillBasedOn = value; } }

        [FwLogicProperty(Id: "2pJscfqIHreL")]
        public string BillingCycleId { get { return dealOrder.BillingCycleId; } set { dealOrder.BillingCycleId = value; } }

        [FwLogicProperty(Id: "IIlkv0ANAt4H", IsReadOnly: true)]
        public string BillingCycle { get; set; }

        [FwLogicProperty(Id: "kvXhtvz6VXbSj", IsReadOnly: true)]
        public string BillingCycleType { get; set; }

        [FwLogicProperty(Id: "ifznPOB7ZOmJ")]
        public string PaymentTermsId { get { return dealOrder.PaymentTermsId; } set { dealOrder.PaymentTermsId = value; } }

        [FwLogicProperty(Id: "N407xNw4oL4u", IsReadOnly: true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id: "uRVRiAmc2o2t")]
        public string PaymentTypeId { get { return dealOrder.PaymentTypeId; } set { dealOrder.PaymentTypeId = value; } }

        [FwLogicProperty(Id: "GvaeiGtqSqRu", IsReadOnly: true)]
        public string PaymentType { get; set; }

        [FwLogicProperty(Id: "CkEfBsEdUymd")]
        public string CurrencyId { get { return dealOrder.CurrencyId; } set { dealOrder.CurrencyId = value; } }

        [FwLogicProperty(Id: "ydSxNcC99OMv", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "wRjO6jhxLOP3B", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }

        [FwLogicProperty(Id: "n24xw5yJaELy")]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "WzIDTjqbA6Ts", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "YMJkZK2ebeP5", IsReadOnly: true)]
        public string Tax1Name { get; set; }

        [FwLogicProperty(Id: "YHKcqIbMokJA", IsReadOnly: true)]
        public string Tax2Name { get; set; }


        [FwLogicProperty(Id: "DlnfpRI6Axwa", DisableDirectAssign: true, DisableDirectModify: true)]
        public string TaxId { get { return dealOrder.TaxId; } set { dealOrder.TaxId = value; tax.TaxId = value; } }

        [FwLogicProperty(Id: "VYgPeImeXW9g")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "E4psJsXCWUxE")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "JeygBclqytNi")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "jGKu070RqqJC")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "jV3NVUGbiw7g")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "C383n9AVERv4")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }



        [FwLogicProperty(Id: "2jUmusT7H2MT")]
        public bool? NoCharge { get { return dealOrder.NoCharge; } set { dealOrder.NoCharge = value; } }

        [FwLogicProperty(Id: "EGPLbvtvHw52")]
        public string NoChargeReason { get { return dealOrder.NoChargeReason; } set { dealOrder.NoChargeReason = value; } }

        //------------------------------------------------------------------------------------


        [FwLogicProperty(Id: "t82ete7uhvBq")]
        public string PrintIssuedToAddressFrom { get { return dealOrder.PrintIssuedToAddressFrom; } set { dealOrder.PrintIssuedToAddressFrom = value; } }

        [FwLogicProperty(Id: "Tz0NeIoiey5F")]
        public string IssuedToName { get { return dealOrder.IssuedToName; } set { dealOrder.IssuedToName = value; } }

        [FwLogicProperty(Id: "M7UtYCqOEVcU")]
        public string IssuedToAttention { get { return dealOrder.IssuedToAttention; } set { dealOrder.IssuedToAttention = value; } }

        [FwLogicProperty(Id: "AikGonQcnE1t")]
        public string IssuedToAttention2 { get { return dealOrder.IssuedToAttention2; } set { dealOrder.IssuedToAttention2 = value; } }

        [FwLogicProperty(Id: "jusS3Xer0rB2")]
        public string IssuedToAddress1 { get { return dealOrder.IssuedToAddress1; } set { dealOrder.IssuedToAddress1 = value; } }

        [FwLogicProperty(Id: "ffyeFVXLso2A")]
        public string IssuedToAddress2 { get { return dealOrder.IssuedToAddress2; } set { dealOrder.IssuedToAddress2 = value; } }

        [FwLogicProperty(Id: "cCDrSFejf79s")]
        public string IssuedToCity { get { return dealOrder.IssuedToCity; } set { dealOrder.IssuedToCity = value; } }

        [FwLogicProperty(Id: "ubUy4YPU1WCi")]
        public string IssuedToState { get { return dealOrder.IssuedToState; } set { dealOrder.IssuedToState = value; } }

        [FwLogicProperty(Id: "9foWuiJu3GRH")]
        public string IssuedToZipCode { get { return dealOrder.IssuedToZipCode; } set { dealOrder.IssuedToZipCode = value; } }

        [FwLogicProperty(Id: "eSArbbkgT4FS")]
        public string IssuedToCountryId { get { return dealOrder.IssuedToCountryId; } set { dealOrder.IssuedToCountryId = value; } }

        [FwLogicProperty(Id: "WF9PUOjb3Key", IsReadOnly: true)]
        public string IssuedToCountry { get; set; }





        [FwLogicProperty(Id: "uNCdTgcDDelL")]
        public bool? BillToAddressDifferentFromIssuedToAddress { get { return dealOrderDetail.BillToAddressDifferentFromIssuedToAddress; } set { dealOrderDetail.BillToAddressDifferentFromIssuedToAddress = value; } }

        [FwLogicProperty(Id: "KTTdELcVxyCK")]
        public string BillToAddressId { get { return billToAddress.AddressId; } set { billToAddress.AddressId = value; } }

        [FwLogicProperty(Id: "peLcQsbqhusS")]
        public string BillToName { get { return billToAddress.Name; } set { billToAddress.Name = value; } }

        [FwLogicProperty(Id: "RjiEN8VZp3h7")]
        public string BillToAttention { get { return billToAddress.Attention; } set { billToAddress.Attention = value; } }

        [FwLogicProperty(Id: "gG3xmtc434GE")]
        public string BillToAttention2 { get { return billToAddress.Attention2; } set { billToAddress.Attention2 = value; } }

        [FwLogicProperty(Id: "DIdZQ3aSpgFF")]
        public string BillToAddress1 { get { return billToAddress.Address1; } set { billToAddress.Address1 = value; } }

        [FwLogicProperty(Id: "UV4Iul9rTbn9")]
        public string BillToAddress2 { get { return billToAddress.Address2; } set { billToAddress.Address2 = value; } }

        [FwLogicProperty(Id: "OsqU69Mi0ElP")]
        public string BillToCity { get { return billToAddress.City; } set { billToAddress.City = value; } }

        [FwLogicProperty(Id: "9evY9TjP8Y2q")]
        public string BillToState { get { return billToAddress.State; } set { billToAddress.State = value; } }

        [FwLogicProperty(Id: "V552umiHDsfX")]
        public string BillToZipCode { get { return billToAddress.ZipCode; } set { billToAddress.ZipCode = value; } }

        [FwLogicProperty(Id: "KLonpqgRIZW9")]
        public string BillToCountryId { get { return billToAddress.CountryId; } set { billToAddress.CountryId = value; } }

        [FwLogicProperty(Id: "rn2gs0owjRqX", IsReadOnly: true)]
        public string BillToCountry { get; set; }





        [FwLogicProperty(Id: "o9WZNr4WpI3L")]
        public string DiscountReasonId { get { return dealOrderDetail.DiscountReasonId; } set { dealOrderDetail.DiscountReasonId = value; } }

        [FwLogicProperty(Id: "QGofWpqMnBB4", IsReadOnly: true)]
        public string DiscountReason { get; set; }



        [FwLogicProperty(Id: "MwKJRufYK9rz")]
        public bool? RequireContactConfirmation { get { return dealOrderDetail.RequireContactConfirmation; } set { dealOrderDetail.RequireContactConfirmation = value; } }



        [FwLogicProperty(Id: "GxcmW5c6pM3p")]
        public bool? IncludeInBillingAnalysis { get { return dealOrder.IncludeInBillingAnalysis; } set { dealOrder.IncludeInBillingAnalysis = value; } }


        [FwLogicProperty(Id: "EnvogqVx8bLR")]
        public string HiatusDiscountFrom { get { return dealOrder.HiatusDiscountFrom; } set { dealOrder.HiatusDiscountFrom = value; } }

        [FwLogicProperty(Id: "TEKTEteQsDT0")]
        public bool? RoundTripRentals { get { return dealOrderDetail.RoundTripRentals; } set { dealOrderDetail.RoundTripRentals = value; } }


        [FwLogicProperty(Id: "AImEtClQHGOf")]
        public bool? InGroup { get { return dealOrder.InGroup; } set { dealOrder.InGroup = value; } }

        [FwLogicProperty(Id: "K8gZJiuIxooK")]
        public int? GroupNumber { get { return dealOrder.GroupNumber; } set { dealOrder.GroupNumber = value; } }



        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "BwVtOSYuOoR2")]
        public string CoverLetterId { get { return dealOrderDetail.CoverLetterId; } set { dealOrderDetail.CoverLetterId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "G9JOaHxUxTtL", IsReadOnly: true)]
        public string CoverLetter { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "k7G0rBdhSjeI")]
        public string TermsConditionsId { get { return dealOrder.TermsConditionsId; } set { dealOrder.TermsConditionsId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "WwnOJs6bvHg7", IsReadOnly: true)]
        public string TermsConditions { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "JKG7F5NfspUO")]
        public string OutsideSalesRepresentativeId { get { return dealOrderDetail.OutsideSalesRepresentativeId; } set { dealOrderDetail.OutsideSalesRepresentativeId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "aKpV0JUlHB9s", IsReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }

        //------------------------------------------------------------------------------------

        [FwLogicProperty(Id: "PDdHN0EKetWp")]
        public string MarketTypeId { get { return dealOrderDetail.MarketTypeId; } set { dealOrderDetail.MarketTypeId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "3YIp8tQZm1nM", IsReadOnly: true)]
        public string MarketType { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "yOYIysdMb2oL")]
        public string MarketSegmentId { get { return dealOrderDetail.MarketSegmentId; } set { dealOrderDetail.MarketSegmentId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "3moKWePRhVN1", IsReadOnly: true)]
        public string MarketSegment { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "yOTlavRP0tbx")]
        public string MarketSegmentJobId { get { return dealOrderDetail.MarketSegmentJobId; } set { dealOrderDetail.MarketSegmentJobId = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "wKHkf2i6U2tD", IsReadOnly: true)]
        public string MarketSegmentJob { get; set; }

        //------------------------------------------------------------------------------------








        [FwLogicProperty(Id: "qOTud9ezil3W", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OutDeliveryId { get { return dealOrder.OutDeliveryId; } set { dealOrder.OutDeliveryId = value; outDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "W0AtOhbvg6tC")]
        public string OutDeliveryDeliveryType { get { return outDelivery.DeliveryType; } set { outDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "9bE1HasBldqf")]
        public string OutDeliveryRequiredDate { get { return outDelivery.RequiredDate; } set { outDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "3y22Cat2XO8J")]
        public string OutDeliveryRequiredTime { get { return outDelivery.RequiredTime; } set { outDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "MUn03z3ujhoi")]
        public string OutDeliveryTargetShipDate { get { return outDelivery.TargetShipDate; } set { outDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "uc8CacYAv3ei")]
        public string OutDeliveryTargetShipTime { get { return outDelivery.TargetShipTime; } set { outDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "0PhnUz7Eiumw")]
        public string OutDeliveryDirection { get { return outDelivery.Direction; } set { outDelivery.Direction = value; } }

        [FwLogicProperty(Id: "7ib3tLHAnqyl")]
        public string OutDeliveryAddressType { get { return outDelivery.AddressType; } set { outDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "pcrm3kLJTkKO")]
        public string OutDeliveryFromLocation { get { return outDelivery.FromLocation; } set { outDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "iF49lU8NUSzv")]
        public string OutDeliveryFromContact { get { return outDelivery.FromContact; } set { outDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "eaLLB4klYjJl")]
        public string OutDeliveryFromContactPhone { get { return outDelivery.FromContactPhone; } set { outDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "B7pYs6dRV2k1")]
        public string OutDeliveryFromAlternateContact { get { return outDelivery.FromAlternateContact; } set { outDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "i5JPHyHX0SjD")]
        public string OutDeliveryFromAlternateContactPhone { get { return outDelivery.FromAlternateContactPhone; } set { outDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "qypZJC9dMwLf")]
        public string OutDeliveryFromAttention { get { return outDelivery.FromAttention; } set { outDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "oBzGO4Fnwbj5")]
        public string OutDeliveryFromAddress1 { get { return outDelivery.FromAddress1; } set { outDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "CatxnbFMHKdK")]
        public string OutDeliveryFromAdd2ress { get { return outDelivery.FromAdd2ress; } set { outDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "hbEZAciq9AoC")]
        public string OutDeliveryFromCity { get { return outDelivery.FromCity; } set { outDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "oryNvLBXBC8b")]
        public string OutDeliveryFromState { get { return outDelivery.FromState; } set { outDelivery.FromState = value; } }

        [FwLogicProperty(Id: "wya2Nl77yi9C")]
        public string OutDeliveryFromZipCode { get { return outDelivery.FromZipCode; } set { outDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "UIh8LBJggxFl", IsReadOnly: true)]
        public string OutDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "Am2BhugydAPL")]
        public string OutDeliveryFromCountryId { get { return outDelivery.FromCountryId; } set { outDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "cCjFBS6JJJ8S")]
        public string OutDeliveryFromCrossStreets { get { return outDelivery.FromCrossStreets; } set { outDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "xQ6WbHivTbdF")]
        public string OutDeliveryToLocation { get { return outDelivery.Location; } set { outDelivery.Location = value; } }

        [FwLogicProperty(Id: "3cHvn15RhqsI")]
        public string OutDeliveryToContact { get { return outDelivery.Contact; } set { outDelivery.Contact = value; } }

        [FwLogicProperty(Id: "mcozpCMcIXgX")]
        public string OutDeliveryToContactPhone { get { return outDelivery.ContactPhone; } set { outDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "r6eRTr1RDvCB")]
        public string OutDeliveryToAlternateContact { get { return outDelivery.AlternateContact; } set { outDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "WJA1BY30TWrL")]
        public string OutDeliveryToAlternateContactPhone { get { return outDelivery.AlternateContactPhone; } set { outDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "NQp5NIidfLKV")]
        public string OutDeliveryToAttention { get { return outDelivery.Attention; } set { outDelivery.Attention = value; } }

        [FwLogicProperty(Id: "AJW7sWUSQwva")]
        public string OutDeliveryToAddress1 { get { return outDelivery.Address1; } set { outDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "4o0KumCSNI3l")]
        public string OutDeliveryToAddress2 { get { return outDelivery.Address2; } set { outDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "TgjWK2lUjLWx")]
        public string OutDeliveryToCity { get { return outDelivery.City; } set { outDelivery.City = value; } }

        [FwLogicProperty(Id: "2SaZNl0vMTij")]
        public string OutDeliveryToState { get { return outDelivery.State; } set { outDelivery.State = value; } }

        [FwLogicProperty(Id: "uyPrAJuLPMVo")]
        public string OutDeliveryToZipCode { get { return outDelivery.ZipCode; } set { outDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "XGyGjeYTPJeq")]
        public string OutDeliveryToCountryId { get { return outDelivery.CountryId; } set { outDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "TLBINO8mABwG", IsReadOnly: true)]
        public string OutDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "T2fpDAqwewMM")]
        public string OutDeliveryToContactFax { get { return outDelivery.ContactFax; } set { outDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "mJx9QYUM8jCW")]
        public string OutDeliveryToCrossStreets { get { return outDelivery.CrossStreets; } set { outDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "cSZ3NFAZPtjU")]
        public string OutDeliveryDeliveryNotes { get { return outDelivery.DeliveryNotes; } set { outDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "4RSxTmRoCOrJ")]
        public string OutDeliveryCarrierId { get { return outDelivery.CarrierId; } set { outDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "AYLsZeCt9fv7", IsReadOnly: true)]
        public string OutDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "R3XMcJTv4AlK")]
        public string OutDeliveryCarrierAccount { get { return outDelivery.CarrierAccount; } set { outDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "yAagQUerTt2F")]
        public string OutDeliveryShipViaId { get { return outDelivery.ShipViaId; } set { outDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "qbGJoPo0dqYD", IsReadOnly: true)]
        public string OutDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "PHBTkN5KVmDr")]
        public string OutDeliveryInvoiceId { get { return outDelivery.InvoiceId; } set { outDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "AllRzAtKD9nJ")]
        public string OutDeliveryVendorInvoiceId { get { return outDelivery.VendorInvoiceId; } set { outDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "rRUSMi8e5RLr")]
        public decimal? OutDeliveryEstimatedFreight { get { return outDelivery.EstimatedFreight; } set { outDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "u7tOO16mtJVT")]
        public decimal? OutDeliveryFreightInvoiceAmount { get { return outDelivery.FreightInvoiceAmount; } set { outDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "IDjCZW3FTrzJ")]
        public string OutDeliveryChargeType { get { return outDelivery.ChargeType; } set { outDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "MCc3fhkBPtjH")]
        public string OutDeliveryFreightTrackingNumber { get { return outDelivery.FreightTrackingNumber; } set { outDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "2UOMh8pmiE7Ja", IsReadOnly: true)]
        public string OutDeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id: "s4KNcQmn3LxA")]
        public bool? OutDeliveryDropShip { get { return outDelivery.DropShip; } set { outDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "29DbhuIKvrnX")]
        public string OutDeliveryPackageCode { get { return outDelivery.PackageCode; } set { outDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "havIcZhu3c0h")]
        public bool? OutDeliveryBillPoFreightOnOrder { get { return outDelivery.BillPoFreightOnOrder; } set { outDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "W7knPwi5pGzZ")]
        public string OutDeliveryOnlineOrderNumber { get { return outDelivery.OnlineOrderNumber; } set { outDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "kpI60XQfsvgI")]
        public string OutDeliveryOnlineOrderStatus { get { return outDelivery.OnlineOrderStatus; } set { outDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "Cj3UdRaUFdh8p", IsReadOnly: true)]
        public string OutDeliveryToVenue { get; set; }

        [FwLogicProperty(Id: "UvcuBfdi7i3o1")]
        public string OutDeliveryToVenueId { get { return outDelivery.VenueId; } set { outDelivery.VenueId = value; } }

        [FwLogicProperty(Id: "vWD0zGG3Tn4K")]
        public string OutDeliveryDateStamp { get { return outDelivery.DateStamp; } set { outDelivery.DateStamp = value; } }










        [FwLogicProperty(Id: "8q5n6SFpVUdm", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InDeliveryId { get { return dealOrder.InDeliveryId; } set { dealOrder.InDeliveryId = value; inDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "woUOUjMvdBVn")]
        public string InDeliveryDeliveryType { get { return inDelivery.DeliveryType; } set { inDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "kvOfysxc75k3")]
        public string InDeliveryRequiredDate { get { return inDelivery.RequiredDate; } set { inDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "wvn0SEHsODln")]
        public string InDeliveryRequiredTime { get { return inDelivery.RequiredTime; } set { inDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "GWsaFivBaOKB")]
        public string InDeliveryTargetShipDate { get { return inDelivery.TargetShipDate; } set { inDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "XjvIdQxe0SPQ")]
        public string InDeliveryTargetShipTime { get { return inDelivery.TargetShipTime; } set { inDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "CvEC7wwzbY5g")]
        public string InDeliveryDirection { get { return inDelivery.Direction; } set { inDelivery.Direction = value; } }

        [FwLogicProperty(Id: "KJthhOXByVfm")]
        public string InDeliveryAddressType { get { return inDelivery.AddressType; } set { inDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "CAX9ehI7eiFe")]
        public string InDeliveryFromLocation { get { return inDelivery.FromLocation; } set { inDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "H35BSszj1xpJ")]
        public string InDeliveryFromContact { get { return inDelivery.FromContact; } set { inDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "uwXFrLq6NW9G")]
        public string InDeliveryFromContactPhone { get { return inDelivery.FromContactPhone; } set { inDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "qQjiVAKVsbfe")]
        public string InDeliveryFromAlternateContact { get { return inDelivery.FromAlternateContact; } set { inDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "NDp3hKZqsT31")]
        public string InDeliveryFromAlternateContactPhone { get { return inDelivery.FromAlternateContactPhone; } set { inDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "UgxHNh6ak6gC")]
        public string InDeliveryFromAttention { get { return inDelivery.FromAttention; } set { inDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "2bODyVFNdwzu")]
        public string InDeliveryFromAddress1 { get { return inDelivery.FromAddress1; } set { inDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "Q6n9PV6wagbu")]
        public string InDeliveryFromAdd2ress { get { return inDelivery.FromAdd2ress; } set { inDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "b9lNCLJUm6rh")]
        public string InDeliveryFromCity { get { return inDelivery.FromCity; } set { inDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "IPbYPTYQw4ap")]
        public string InDeliveryFromState { get { return inDelivery.FromState; } set { inDelivery.FromState = value; } }

        [FwLogicProperty(Id: "yaKSMovBbJIR")]
        public string InDeliveryFromZipCode { get { return inDelivery.FromZipCode; } set { inDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "ZEWeACUz4nM4", IsReadOnly: true)]
        public string InDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "O216QT02a0kA")]
        public string InDeliveryFromCountryId { get { return inDelivery.FromCountryId; } set { inDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "NtPcla3dKz5h")]
        public string InDeliveryFromCrossStreets { get { return inDelivery.FromCrossStreets; } set { inDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "BA36cqZ9mGJU")]
        public string InDeliveryToLocation { get { return inDelivery.Location; } set { inDelivery.Location = value; } }

        [FwLogicProperty(Id: "i6RIYIlLq2Zq")]
        public string InDeliveryToContact { get { return inDelivery.Contact; } set { inDelivery.Contact = value; } }

        [FwLogicProperty(Id: "Di1AK0a2xrQY")]
        public string InDeliveryToContactPhone { get { return inDelivery.ContactPhone; } set { inDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "h397mNBGb46a")]
        public string InDeliveryToAlternateContact { get { return inDelivery.AlternateContact; } set { inDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "iP7R04xuXQQP")]
        public string InDeliveryToAlternateContactPhone { get { return inDelivery.AlternateContactPhone; } set { inDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "6QhNsWO7mVta")]
        public string InDeliveryToAttention { get { return inDelivery.Attention; } set { inDelivery.Attention = value; } }

        [FwLogicProperty(Id: "yyotEgfjzCjV")]
        public string InDeliveryToAddress1 { get { return inDelivery.Address1; } set { inDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "GYimEW5f4hud")]
        public string InDeliveryToAddress2 { get { return inDelivery.Address2; } set { inDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "tFyP2gEguhZ1")]
        public string InDeliveryToCity { get { return inDelivery.City; } set { inDelivery.City = value; } }

        [FwLogicProperty(Id: "LYrMyUquWnM8")]
        public string InDeliveryToState { get { return inDelivery.State; } set { inDelivery.State = value; } }

        [FwLogicProperty(Id: "FmvoFrY9T0xC")]
        public string InDeliveryToZipCode { get { return inDelivery.ZipCode; } set { inDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "dfQsMadNIKpX")]
        public string InDeliveryToCountryId { get { return inDelivery.CountryId; } set { inDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "4tAXKbxT43GL", IsReadOnly: true)]
        public string InDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "y9OUhj2xLmtq")]
        public string InDeliveryToContactFax { get { return inDelivery.ContactFax; } set { inDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "ggA7FyRI5kGZ")]
        public string InDeliveryToCrossStreets { get { return inDelivery.CrossStreets; } set { inDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "rO7SOZV8Z3RK")]
        public string InDeliveryDeliveryNotes { get { return inDelivery.DeliveryNotes; } set { inDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "TMyaEHO1z2bF")]
        public string InDeliveryCarrierId { get { return inDelivery.CarrierId; } set { inDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "06pVucKYKOb4", IsReadOnly: true)]
        public string InDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "XTnelwAPTCeh")]
        public string InDeliveryCarrierAccount { get { return inDelivery.CarrierAccount; } set { inDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "ZvaJ0jVwhRXQ")]
        public string InDeliveryShipViaId { get { return inDelivery.ShipViaId; } set { inDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "bnNssR8lwCuv", IsReadOnly: true)]
        public string InDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "xwguI81R9Kb6")]
        public string InDeliveryInvoiceId { get { return inDelivery.InvoiceId; } set { inDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "4fFvKHQ7SwT0")]
        public string InDeliveryVendorInvoiceId { get { return inDelivery.VendorInvoiceId; } set { inDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "bD38BN6ibnUx")]
        public decimal? InDeliveryEstimatedFreight { get { return inDelivery.EstimatedFreight; } set { inDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "EwfpaMT1xeyg")]
        public decimal? InDeliveryFreightInvoiceAmount { get { return inDelivery.FreightInvoiceAmount; } set { inDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "YLOHPDu7JCS4")]
        public string InDeliveryChargeType { get { return inDelivery.ChargeType; } set { inDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "uScn0z45Qm10")]
        public string InDeliveryFreightTrackingNumber { get { return inDelivery.FreightTrackingNumber; } set { inDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "xwggHLCeRHNUQ", IsReadOnly: true)]
        public string InDeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id: "U2PvKH1QzI32")]
        public bool? InDeliveryDropShip { get { return inDelivery.DropShip; } set { inDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "mVbz5KGFGHG3")]
        public string InDeliveryPackageCode { get { return inDelivery.PackageCode; } set { inDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "ioc5Fjeen3M9")]
        public bool? InDeliveryBillPoFreightOnOrder { get { return inDelivery.BillPoFreightOnOrder; } set { inDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "fZypVZdoDRhc")]
        public string InDeliveryOnlineOrderNumber { get { return inDelivery.OnlineOrderNumber; } set { inDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "JDBCfhU2JRKZ")]
        public string InDeliveryOnlineOrderStatus { get { return inDelivery.OnlineOrderStatus; } set { inDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "EGUw7RLqqDpLt", IsReadOnly: true)]
        public string InDeliveryToVenue { get; set; }

        [FwLogicProperty(Id: "Ghma7fPzk95M3")]
        public string InDeliveryToVenueId { get { return inDelivery.VenueId; } set { inDelivery.VenueId = value; } }

        [FwLogicProperty(Id: "uVFMwXKlKv8G")]
        public string InDeliveryDateStamp { get { return inDelivery.DateStamp; } set { inDelivery.DateStamp = value; } }


        [FwLogicProperty(Id: "AGlSqFqm9s9n")]
        public decimal? RentalDaysPerWeek { get { return dealOrder.RentalDaysPerWeek; } set { dealOrder.RentalDaysPerWeek = value; } }

        [FwLogicProperty(Id: "BXff6f2MEA8a")]
        public decimal? RentalDiscountPercent { get { return dealOrder.RentalDiscountPercent; } set { dealOrder.RentalDiscountPercent = value; } }

        [FwLogicProperty(Id: "Bw4tAXhRZdYM", IsReadOnly: true)]
        public decimal? WeeklyRentalTotal { get; set; }

        [FwLogicProperty(Id: "BJ4FAgexNvbN", IsReadOnly: true)]
        public decimal? MonthlyRentalTotal { get; set; }

        [FwLogicProperty(Id: "9i9sCBE4IzWt", IsReadOnly: true)]
        public decimal? PeriodRentalTotal { get; set; }

        [FwLogicProperty(Id: "sj3rlksckQD9", IsReadOnly: true)]
        public bool? WeeklyRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "meYdfdQbHzov", IsReadOnly: true)]
        public bool? MonthlyRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "ZExStxOkbklY", IsReadOnly: true)]
        public bool? PeriodRentalTotalIncludesTax { get; set; }


        [FwLogicProperty(Id: "Jdu2sIAZDX2J")]
        public decimal? SalesDiscountPercent { get { return dealOrder.SalesDiscountPercent; } set { dealOrder.SalesDiscountPercent = value; } }

        [FwLogicProperty(Id: "bJDa8Z222ob2", IsReadOnly: true)]
        public decimal? SalesTotal { get; set; }

        [FwLogicProperty(Id: "eNoiJDJ1zfrw", IsReadOnly: true)]
        public bool? SalesTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "GotnQKPCNJtE", IsReadOnly: true)]
        public decimal? PartsDiscountPercent { get; set; }

        [FwLogicProperty(Id: "ADKU22GVJjbR", IsReadOnly: true)]
        public decimal? PartsTotal { get; set; }

        [FwLogicProperty(Id: "D7jNMth5sUMc", IsReadOnly: true)]
        public bool? PartsTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Txoe4RbbiWIp")]
        public decimal? SpaceDaysPerWeek { get { return dealOrder.SpaceDaysPerWeek; } set { dealOrder.SpaceDaysPerWeek = value; } }

        [FwLogicProperty(Id: "gH2PxeqAFsnE")]
        public decimal? SpaceDiscountPercent { get { return dealOrder.SpaceDiscountPercent; } set { dealOrder.SpaceDiscountPercent = value; } }

        [FwLogicProperty(Id: "rHoLXHUatMo7", IsReadOnly: true)]
        public decimal? SpaceSplitPercent { get; set; }

        [FwLogicProperty(Id: "28wxIW5QH0EH", IsReadOnly: true)]
        public decimal? WeeklySpaceTotal { get; set; }

        [FwLogicProperty(Id: "CNXoRxrle9cz", IsReadOnly: true)]
        public decimal? MonthlySpaceTotal { get; set; }

        [FwLogicProperty(Id: "jies8wPW5HEO", IsReadOnly: true)]
        public decimal? PeriodSpaceTotal { get; set; }

        [FwLogicProperty(Id: "cmefhqyUobds", IsReadOnly: true)]
        public bool? WeeklySpaceTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "JbCowuqpCKgO", IsReadOnly: true)]
        public bool? MonthlySpaceTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "ye4vhYlVWu1o", IsReadOnly: true)]
        public bool? PeriodSpaceTotalIncludesTax { get; set; }


        [FwLogicProperty(Id: "XiNWrxllDcj3", IsReadOnly: true)]
        public decimal? VehicleDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "ay1Hqy9JM4Hq", IsReadOnly: true)]
        public decimal? VehicleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "3Auewf8LqPAY", IsReadOnly: true)]
        public decimal? WeeklyVehicleTotal { get; set; }

        [FwLogicProperty(Id: "LAdCRJ0U5rRG", IsReadOnly: true)]
        public decimal? MonthlyVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Rv4h4cY3jwCZ", IsReadOnly: true)]
        public decimal? PeriodVehicleTotal { get; set; }

        [FwLogicProperty(Id: "3BsJtLaYLl8k", IsReadOnly: true)]
        public bool? WeeklyVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DVXwbkylQaXB", IsReadOnly: true)]
        public bool? MonthlyVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "IUQwSmR76LKQ", IsReadOnly: true)]
        public bool? PeriodVehicleTotalIncludesTax { get; set; }



        [FwLogicProperty(Id: "oUsni3NYShJ2", IsReadOnly: true)]
        public decimal? MiscDiscountPercent { get; set; }

        [FwLogicProperty(Id: "RLM1qLYJE83C", IsReadOnly: true)]
        public decimal? WeeklyMiscTotal { get; set; }

        [FwLogicProperty(Id: "jikBbVZXvzJO", IsReadOnly: true)]
        public decimal? MonthlyMiscTotal { get; set; }

        [FwLogicProperty(Id: "kBR8pf7nWe2U", IsReadOnly: true)]
        public decimal? PeriodMiscTotal { get; set; }

        [FwLogicProperty(Id: "HobBFmhz9KZ3", IsReadOnly: true)]
        public bool? WeeklyMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "AfuoX0LRR2aH", IsReadOnly: true)]
        public bool? MonthlyMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "GuJip1zUCrzt", IsReadOnly: true)]
        public bool? PeriodMiscTotalIncludesTax { get; set; }


        [FwLogicProperty(Id: "vKOo5CvuZar1", IsReadOnly: true)]
        public decimal? LaborDiscountPercent { get; set; }

        [FwLogicProperty(Id: "zevGNAfKMfBx", IsReadOnly: true)]
        public decimal? WeeklyLaborTotal { get; set; }

        [FwLogicProperty(Id: "knfaXTYgQKPo", IsReadOnly: true)]
        public decimal? MonthlyLaborTotal { get; set; }

        [FwLogicProperty(Id: "loOURXK1XBhC", IsReadOnly: true)]
        public decimal? PeriodLaborTotal { get; set; }

        [FwLogicProperty(Id: "7IcOvmdykORE", IsReadOnly: true)]
        public bool? WeeklyLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "6AV4Ng4wGZv2", IsReadOnly: true)]
        public bool? MonthlyLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "68Y6xAQvnbN8", IsReadOnly: true)]
        public bool? PeriodLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "o1kfka9EwC71", IsReadOnly: true)]
        public decimal? RentalSaleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "pjSGMKbozjpI", IsReadOnly: true)]
        public decimal? RentalSaleTotal { get; set; }

        [FwLogicProperty(Id: "EnasDA0n1YlB", IsReadOnly: true)]
        public bool? RentalSaleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Ppf5bjmGdJHmv", IsReadOnly: true)]
        public decimal? LossAndDamageDiscountPercent { get; set; }
        [FwLogicProperty(Id: "4AkawJGwm8gSR", IsReadOnly: true)]
        public decimal? LossAndDamageTotal { get; set; }
        [FwLogicProperty(Id: "umrLp5PI5ZSOc", IsReadOnly: true)]
        public bool? LossAndDamageTotalIncludesTax { get; set; }


        [FwLogicProperty(Id: "Xawkh0awo9VR", IsReadOnly: true)]
        public decimal? CombinedDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "FXgwz1Gqs8AY", IsReadOnly: true)]
        public decimal? CombinedDiscountPercent { get; set; }

        [FwLogicProperty(Id: "VXJymeXffQKU", IsReadOnly: true)]
        public decimal? WeeklyCombinedTotal { get; set; }

        [FwLogicProperty(Id: "PAhIMfuH6U5M", IsReadOnly: true)]
        public decimal? MonthlyCombinedTotal { get; set; }

        [FwLogicProperty(Id: "VJWochjlKuwY", IsReadOnly: true)]
        public decimal? PeriodCombinedTotal { get; set; }

        [FwLogicProperty(Id: "q1ap7FAk0FlB", IsReadOnly: true)]
        public bool? WeeklyCombinedTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "o2G5yG1jz3gz", IsReadOnly: true)]
        public bool? MonthlyCombinedTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "6OwxZX912kog", IsReadOnly: true)]
        public bool? PeriodCombinedTotalIncludesTax { get; set; }


        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "3B3sgTz5Fuv0", IsReadOnly: true)]
        public bool? DisableEditingRentalRate { get; set; }

        [FwLogicProperty(Id: "Jsq9Y0Zs9tWy", IsReadOnly: true)]
        public bool? DisableEditingSalesRate { get; set; }

        [FwLogicProperty(Id: "3KUcM0fbqwFl", IsReadOnly: true)]
        public bool? DisableEditingMiscellaneousRate { get; set; }

        [FwLogicProperty(Id: "be30FXtYLhAU", IsReadOnly: true)]
        public bool? DisableEditingLaborRate { get; set; }

        [FwLogicProperty(Id: "YtkQnTGNZkDa", IsReadOnly: true)]
        public bool? DisableEditingUsedSaleRate { get; set; }

        [FwLogicProperty(Id: "q3zqI75ytlCZ", IsReadOnly: true)]
        public bool? DisableEditingLossAndDamageRate { get; set; }

        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "md8wyK3iLVu19", IsReadOnly: true)]
        public decimal? RentalExtendedTotal { get; set; }
        [FwLogicProperty(Id: "t69atepngfmcn", IsReadOnly: true)]
        public decimal? SalesExtendedTotal { get; set; }
        [FwLogicProperty(Id: "GSTvpXg6jPQm2", IsReadOnly: true)]
        public decimal? LaborExtendedTotal { get; set; }
        [FwLogicProperty(Id: "Vm9BEBQZ1Wt9D", IsReadOnly: true)]
        public decimal? MiscellaneousExtendedTotal { get; set; }
        [FwLogicProperty(Id: "OwPNPM2xfi30C", IsReadOnly: true)]
        public decimal? UsedSaleExtendedTotal { get; set; }
        [FwLogicProperty(Id: "Nv6r2tREyvp1p", IsReadOnly: true)]
        public decimal? LossAndDamageExtendedTotal { get; set; }


        [FwLogicProperty(Id: "a4M3WLLCuQor", IsReadOnly: true)]
        public bool? HasNotes { get; set; }

        [FwLogicProperty(Id: "Y20WXdv5U5cV0", IsReadOnly: true)]
        public decimal? TotalReplacementCost { get; set; }


        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "kSfl2WWGJZsWD")]
        public string PresentationLayerId { get { return dealOrderDetail.PresentationLayerId; } set { dealOrderDetail.PresentationLayerId = value; } }
        [FwLogicProperty(Id: "r2d50s00tU8m4", IsReadOnly: true)]
        public string PresentationLayer { get; set; }
        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "JLEETIIuUH1li", IsReadOnly: true)]  // input value supplied from page is ignored, overridden in BeforeSaves
        public bool? IsManualSort { get { return dealOrderDetail.IsManualSort; } set { dealOrderDetail.IsManualSort = value; } }
        //------------------------------------------------------------------------------------



        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "HmfP8Yd1BuDm", IsRecordTitle: true, IsReadOnly: true)]
        public string QuoteOrderTitle { get; set; }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "Ax3fGfGLJouY", IsReadOnly: true)]
        public bool? UnassignedSubs { get; set; }
        //------------------------------------------------------------------------------------

        [FwLogicProperty(Id: "DiohURrhZZm9F", IsReadOnly: true)]
        public bool? NonTaxable { get; set; }

        [FwLogicProperty(Id: "yzIfMghdiHJm", IsReadOnly: true)]
        public bool? EnableProjects { get; set; }

        [FwLogicProperty(Id: "FrWTnro7Ip239")]
        public string ProjectId { get { return dealOrder.ProjectId; } set { dealOrder.ProjectId = value; } }

        [FwLogicProperty(Id: "guirHi25L1jNk")]
        public string ProjectNumber { get; set; }

        [FwLogicProperty(Id: "ykFFP6Lq4wzcj")]
        public string Project { get; set; }

        [FwLogicProperty(Id: "Ua0N3uBhSEsVV")]
        public string ProjectDrawingsId { get { return dealOrderDetail.ProjectDrawingsId; } set { dealOrderDetail.ProjectDrawingsId = value; } }

        [FwLogicProperty(Id: "0N2tI78UOF39A", IsReadOnly: true)]
        public string ProjectDrawings { get; set; }

        [FwLogicProperty(Id: "sYRVihjSokWw3")]
        public string ProjectItemsOrderedId { get { return dealOrderDetail.ProjectItemsOrderedId; } set { dealOrderDetail.ProjectItemsOrderedId = value; } }

        [FwLogicProperty(Id: "4fSxgmidQ5f66", IsReadOnly: true)]
        public string ProjectItemsOrdered { get; set; }

        [FwLogicProperty(Id: "TwH9ifnmKxvlR")]
        public string ProjectDropShipId { get { return dealOrderDetail.ProjectDropShipId; } set { dealOrderDetail.ProjectDropShipId = value; } }

        [FwLogicProperty(Id: "hNyBuaQlIAloL", IsReadOnly: true)]
        public string ProjectDropShip { get; set; }

        [FwLogicProperty(Id: "Yu0QMErfqkSRK")]
        public string ProjectAsBuildId { get { return dealOrderDetail.ProjectAsBuildId; } set { dealOrderDetail.ProjectAsBuildId = value; } }

        [FwLogicProperty(Id: "ENziptmN9mXsw", IsReadOnly: true)]
        public string ProjectAsBuild { get; set; }

        [FwLogicProperty(Id: "4Jziy8VzAF8fm")]
        public string ProjectCommissioningId { get { return dealOrderDetail.ProjectCommissioningId; } set { dealOrderDetail.ProjectCommissioningId = value; } }

        [FwLogicProperty(Id: "6GInSDeDi0B9z", IsReadOnly: true)]
        public string ProjectCommissioning { get; set; }

        [FwLogicProperty(Id: "QQ0JDxYDWrW2s")]
        public string ProjectDepositId { get { return dealOrderDetail.ProjectDepositId; } set { dealOrderDetail.ProjectDepositId = value; } }

        [FwLogicProperty(Id: "GAKP5IuXOyoBs", IsReadOnly: true)]
        public string ProjectDeposit { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "wXMvGhGwlYbqi", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InputByUserId { get { return dealOrder.InputByUserId; } set { dealOrder.InputByUserId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "dV2QPotbETQju", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ModifiedByUserId { get { return dealOrder.ModifiedByUserId; } set { dealOrder.ModifiedByUserId = value; } }
        //------------------------------------------------------------------------------------ 


        [FwLogicProperty(Id: "Mj4GCUlVtnzB")]
        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        private string determineDefaultBillingCycleId()
        {
            string defaultBillingCycleId = string.Empty;
            if (insertingDeal == null)
            {
                DefaultSettingsLogic defaults = new DefaultSettingsLogic();
                defaults.SetDependencies(AppConfig, UserSession);
                defaults.DefaultSettingsId = RwConstants.CONTROL_ID;
                bool b = defaults.LoadAsync<DefaultSettingsLogic>().Result;
                defaultBillingCycleId = defaults.DefaultDealBillingCycleId;
            }
            else
            {
                defaultBillingCycleId = insertingDeal.BillingCycleId;
            }
            return defaultBillingCycleId;
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            bool rental = false, sales = false, labor = false, misc = false, rentalsale = false, ld = false;
            bool? tempB;

            //OrderTypeCombineActivityTabs

            string orderId = GetPrimaryKeys()[0].ToString();

            if (isValid)
            {
                PropertyInfo property = typeof(OrderBaseLogic).GetProperty(nameof(OrderBaseLogic.DetermineQuantitiesToBillBasedOn));
                string[] acceptableValues = { RwConstants.ORDER_DETERMINE_QUANTITIES_TO_BILL_BASED_ON_CONTRACT, RwConstants.ORDER_DETERMINE_QUANTITIES_TO_BILL_BASED_ON_ORDER };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(OrderBaseLogic).GetProperty(nameof(OrderBaseLogic.OutDeliveryOnlineOrderStatus));
                string[] acceptableValues = { string.Empty, RwConstants.ONLINE_DELIVERY_STATUS_PARTIAL, RwConstants.ONLINE_DELIVERY_STATUS_COMPLETE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            OrderBaseLogic lOrig = null;

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    rental = Rental.GetValueOrDefault(false);
                    sales = Sales.GetValueOrDefault(false);
                    labor = Labor.GetValueOrDefault(false);
                    misc = Miscellaneous.GetValueOrDefault(false);
                    rentalsale = RentalSale.GetValueOrDefault(false);
                    ld = LossAndDamage.GetValueOrDefault(false);

                    if (!string.IsNullOrEmpty(DealId))
                    {
                        // load the Deal object once here for use downstream
                        insertingDeal = new DealLogic();
                        insertingDeal.SetDependencies(AppConfig, UserSession);
                        insertingDeal.DealId = DealId;
                        bool b = insertingDeal.LoadAsync<DealLogic>().Result;
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
                        tempB = (Rental ?? lOrig.Rental);
                        rental = tempB.GetValueOrDefault(false);

                        tempB = (Sales ?? lOrig.Sales);
                        sales = tempB.GetValueOrDefault(false);

                        tempB = (Labor ?? lOrig.Labor);
                        labor = tempB.GetValueOrDefault(false);

                        tempB = (Miscellaneous ?? lOrig.Miscellaneous);
                        misc = tempB.GetValueOrDefault(false);

                        tempB = (RentalSale ?? lOrig.RentalSale);
                        rentalsale = tempB.GetValueOrDefault(false);

                        tempB = (LossAndDamage ?? lOrig.LossAndDamage);
                        ld = tempB.GetValueOrDefault(false);

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

                        //// make sure certain values are not modified directly
                        //if (isValid)
                        //{

                        //    //isValid = (isValid && ValidateNotChangingProperty(this.GetType().GetProperty(nameof(OrderBaseLogic.OfficeLocationId)), lOrig, ref validateMsg));
                        //    //isValid = (isValid && ValidateNotChangingProperty(this.GetType().GetProperty(nameof(OrderBaseLogic.WarehouseId)), lOrig, ref validateMsg));
                        //    //isValid = (isValid && ValidateNotChangingProperty(this.GetType().GetProperty(nameof(OrderBaseLogic.Status)), lOrig, ref validateMsg));
                        //    //isValid = (isValid && ValidateNotChangingProperty(this.GetType().GetProperty(nameof(OrderBaseLogic.StatusDate)), lOrig, ref validateMsg));

                        //    PropertyInfo[] properties = {
                        //       this.GetType().GetProperty(nameof(OrderBaseLogic.OfficeLocationId)),
                        //       this.GetType().GetProperty(nameof(OrderBaseLogic.WarehouseId)),
                        //       this.GetType().GetProperty(nameof(OrderBaseLogic.Status)),
                        //       this.GetType().GetProperty(nameof(OrderBaseLogic.StatusDate))
                        //    };
                        //    isValid = (isValid && ValidateNotChangingProperty(properties, lOrig, ref validateMsg));
                        //}

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
            }

            // if this quote/order has line-items, then make sure the Billing Cycle will work
            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate)) // only need to check on Update because Insert mode has no line-items yet
                {
                    if (OrderFunc.OrderHasItems(AppConfig, UserSession, orderId).Result)
                    {
                        string billingCycleId = BillingCycleId;
                        if (string.IsNullOrEmpty(billingCycleId) && (lOrig != null))
                        {
                            billingCycleId = lOrig.BillingCycleId;
                        }
                        if (string.IsNullOrEmpty(billingCycleId))
                        {
                            billingCycleId = determineDefaultBillingCycleId();
                        }
                        if (!string.IsNullOrEmpty(billingCycleId))
                        {
                            BillingCycleLogic bc = new BillingCycleLogic();
                            bc.SetDependencies(AppConfig, UserSession);
                            bc.BillingCycleId = billingCycleId;
                            bool b = bc.LoadAsync<BillingCycleLogic>().Result;
                            bool hasRecurring = OrderFunc.OrderHasRecurring(AppConfig, UserSession, orderId).Result;

                            if (hasRecurring && bc.BillingCycleType.Equals(RwConstants.BILLING_CYCLE_TYPE_IMMEDIATE))
                            {
                                isValid = false;
                                validateMsg = "The " + bc.BillingCycle + " Billing Cycle can only be used when the " + BusinessLogicModuleName + " has no recurring charges.  Switch to a recurring-type Billing Cycle.";
                            }
                            else if (!hasRecurring && (!(bc.BillingCycleType.Equals(RwConstants.BILLING_CYCLE_TYPE_IMMEDIATE) || bc.BillingCycleType.Equals(RwConstants.BILLING_CYCLE_TYPE_ONDEMAND))))
                            {
                                isValid = false;
                                validateMsg = "The " + bc.BillingCycle + " Billing Cycle can only be used when the " + BusinessLogicModuleName + " has recurring charges.  Switch to an " + RwConstants.BILLING_CYCLE_TYPE_IMMEDIATE + " or " + RwConstants.BILLING_CYCLE_TYPE_ONDEMAND + " Billing Cycle.";
                            }

                        }
                    }
                }
            }

            if (isValid)
            {
                if (rental && rentalsale)
                {
                    isValid = false;
                    validateMsg = "Cannot have both Rental and Used Sale on the same " + BusinessLogicModuleName + ".";
                }
                if (rental && ld)
                {
                    isValid = false;
                    validateMsg = "Cannot have both Rental and Loss and Damage on the same " + BusinessLogicModuleName + ".";
                }
                if (sales && ld)
                {
                    isValid = false;
                    validateMsg = "Cannot have both Sales and Loss and Damage on the same " + BusinessLogicModuleName + ".";
                }
                if (rentalsale && ld)
                {
                    isValid = false;
                    validateMsg = "Cannot have both Used Sale and Loss and Damage on the same " + BusinessLogicModuleName + ".";
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OrderDetailAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((DealOrderDetailRecord)sender).OrderId = GetPrimaryKeys()[0].ToString();
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            string orderId = GetPrimaryKeys()[0].ToString();

            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                InputByUserId = UserSession.UsersId;
                //// load Deal here for use later in this method
                //DealLogic deal = null;
                //string dealId = DealId;
                //if (!string.IsNullOrEmpty(dealId))
                //{
                //    deal = new DealLogic();
                //    deal.SetDependencies(AppConfig, UserSession);
                //    deal.DealId = dealId;
                //    bool b = deal.LoadAsync<DealLogic>().Result;
                //}

                // load Department here for use later in this method
                DepartmentLogic department = null;
                string departmentId = DepartmentId;
                if (!string.IsNullOrEmpty(departmentId))
                {
                    department = new DepartmentLogic();
                    department.SetDependencies(AppConfig, UserSession);
                    department.DepartmentId = departmentId;
                    bool b = department.LoadAsync<DepartmentLogic>().Result;
                }

                if (string.IsNullOrEmpty(BillingCycleId))
                {
                    //if (string.IsNullOrEmpty(DealId))
                    //{
                    //    DefaultSettingsLogic defaults = new DefaultSettingsLogic();
                    //    defaults.SetDependencies(AppConfig, UserSession);
                    //    defaults.DefaultSettingsId = RwConstants.CONTROL_ID;
                    //    bool b = defaults.LoadAsync<DefaultSettingsLogic>().Result;
                    //    BillingCycleId = defaults.DefaultDealBillingCycleId;
                    //}
                    //else
                    //{
                    //    if (deal != null)
                    //    {
                    //        BillingCycleId = deal.BillingCycleId;
                    //    }
                    //}
                    BillingCycleId = determineDefaultBillingCycleId();
                }

                if (string.IsNullOrEmpty(TaxOptionId))
                {
                    if (insertingDeal != null)
                    {
                        string companyId = string.Empty;
                        if (insertingDeal.UseCustomerTax.GetValueOrDefault(false))
                        {
                            companyId = insertingDeal.CustomerId;
                        }
                        else
                        {
                            companyId = insertingDeal.DealId;
                        }

                        BrowseRequest companyTaxBrowseRequest = new BrowseRequest();
                        companyTaxBrowseRequest.uniqueids = new Dictionary<string, object>();
                        companyTaxBrowseRequest.uniqueids.Add("CompanyId", companyId);
                        companyTaxBrowseRequest.uniqueids.Add("LocationId", OfficeLocationId);

                        CompanyTaxOptionLogic companyTaxSelector = new CompanyTaxOptionLogic();
                        companyTaxSelector.SetDependencies(AppConfig, UserSession);
                        List<CompanyTaxOptionLogic> companyTax = companyTaxSelector.SelectAsync<CompanyTaxOptionLogic>(companyTaxBrowseRequest).Result;

                        if (companyTax.Count > 0)
                        {
                            TaxOptionId = companyTax[0].TaxOptionId;
                        }
                    }
                    if (string.IsNullOrEmpty(TaxOptionId))
                    {
                        TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
                    }

                }


                if (insertingDeal != null)
                {
                    if ((insertingDeal.UseDiscountTemplate.GetValueOrDefault(false) && (!string.IsNullOrEmpty(insertingDeal.DiscountTemplateId))) || (insertingDeal.UseCustomerDiscount.GetValueOrDefault(false) && (!string.IsNullOrEmpty(insertingDeal.CustomerDiscountTemplateId))))
                    {
                        RentalDaysPerWeek = insertingDeal.RentalDaysPerWeek;
                        RentalDiscountPercent = insertingDeal.RentalDiscountPercent;
                        SalesDiscountPercent = insertingDeal.SalesDiscountPercent;
                        SpaceDaysPerWeek = insertingDeal.FacilitiesDaysPerWeek;
                        SpaceDiscountPercent = insertingDeal.FacilitiesDiscountPercent;
                    }
                    else
                    {
                        RentalDaysPerWeek = department.DefaultDaysPerWeek;
                        SpaceDaysPerWeek = department.DefaultDaysPerWeek;
                    }
                }

                if (!string.IsNullOrEmpty(OrderTypeId))
                {
                    OrderTypeLogic orderType = new OrderTypeLogic();
                    orderType.SetDependencies(AppConfig, UserSession);
                    orderType.OrderTypeId = OrderTypeId;
                    bool b = orderType.LoadAsync<OrderTypeLogic>().Result;
                    IsManualSort = orderType.DefaultManualSort;

                    if (string.IsNullOrEmpty(DetermineQuantitiesToBillBasedOn))
                    {
                        DetermineQuantitiesToBillBasedOn = orderType.DetermineQuantitiesToBillBasedOn;
                    }
                }

                if (string.IsNullOrEmpty(AgentId))
                {
                    AgentId = UserSession.UsersId;
                }
                if (string.IsNullOrEmpty(ProjectManagerId))
                {
                    ProjectManagerId = UserSession.UsersId;
                }

                if ((!PendingPo.GetValueOrDefault(false)) && (!FlatPo.GetValueOrDefault(false)) && (string.IsNullOrEmpty(PoNumber)))
                {
                    PendingPo = true;
                }
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                ModifiedByUserId = UserSession.UsersId;
                if (e.Original != null)
                {
                    OrderBaseLogic orig = ((OrderBaseLogic)e.Original);
                    OutDeliveryId = orig.OutDeliveryId;
                    InDeliveryId = orig.InDeliveryId;
                    BillToAddressId = orig.BillToAddressId;
                    TaxId = orig.TaxId;
                    IsManualSort = orig.IsManualSort;
                }
            }

        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {

            // if status changes, log an additional history record?
            //insert into orderstatushistory(orderid, usersid, status, functionname, statusdatetime, action, datestamp)
            //values(@orderid, @usersid, @status, @functionname, @statusdatetime, @action, getutcdate())


            string newPickDate = "", newEstimatedStartDate = "", newEstimatedStopDate = "", newPickTime = "", newEstimatedStartTime = "", newEstimatedStopTime = "", newBillingStartDate = "", newBillingEndDate = "";
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a new Quote/Order.  OutDeliveryId, InDeliveryId, and TaxId were not known at time of insert.  Need to re-update the data with the known ID's
                dealOrder.OutDeliveryId = outDelivery.DeliveryId;
                dealOrder.InDeliveryId = inDelivery.DeliveryId;
                dealOrder.TaxId = tax.TaxId;
                int i = dealOrder.SaveAsync(null, e.SqlConnection).Result;
            }
            else // updating
            {
                OrderBaseLogic orig = ((OrderBaseLogic)e.Original);

                newPickDate = PickDate ?? orig.PickDate;
                newPickTime = PickTime ?? orig.PickTime;
                newEstimatedStartDate = EstimatedStartDate ?? orig.EstimatedStartDate;
                newEstimatedStartTime = EstimatedStartTime ?? orig.EstimatedStartTime;
                newEstimatedStopDate = EstimatedStopDate ?? orig.EstimatedStopDate;
                newEstimatedStopTime = EstimatedStopTime ?? orig.EstimatedStopTime;
                newBillingStartDate = BillingStartDate ?? orig.BillingStartDate;
                newBillingEndDate = BillingEndDate ?? orig.BillingEndDate;
            }


            if (ActivityDatesAndTimes.Count > 0)
            {
                ApplyOrderDatesAndTimesRequest request = new ApplyOrderDatesAndTimesRequest();
                request.OrderId = GetPrimaryKeys()[0].ToString();
                foreach (OrderDatesLogic d in ActivityDatesAndTimes)
                {
                    OrderDateAndTime dt = new OrderDateAndTime();
                    dt.OrderTypeDateTypeId = d.OrderTypeDateTypeId;

                    //get the Activity Type from the true source: OrderTypeDateType
                    OrderTypeDateTypeLogic otdt = new OrderTypeDateTypeLogic();
                    otdt.SetDependencies(AppConfig, UserSession);
                    otdt.OrderTypeDateTypeId = dt.OrderTypeDateTypeId;
                    bool b = otdt.LoadAsync<OrderTypeDateTypeLogic>(e.SqlConnection).Result;

                    if (!string.IsNullOrEmpty(d.Date))
                    {
                        dt.Date = FwConvert.ToDateTime(d.Date);
                    }
                    dt.Time = d.Time;
                    dt.IsMilestone = d.IsMilestone;
                    dt.IsProductionActivity = d.IsProductionActivity;
                    request.DatesAndTimes.Add(dt);
                    if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_PICK))
                    {
                        newPickDate = d.Date;
                        newPickTime = d.Time;
                    }
                    else if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_START))
                    {
                        newEstimatedStartDate = d.Date;
                        newEstimatedStartTime = d.Time;
                    }
                    else if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_STOP))
                    {
                        newEstimatedStopDate = d.Date;
                        newEstimatedStopTime = d.Time;
                    }
                }
                ApplyOrderDatesAndTimesResponse response = OrderFunc.ApplyOrderDatesAndTimes(AppConfig, UserSession, request, e.SqlConnection).Result;
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    OrderBaseLogic orig = ((OrderBaseLogic)e.Original);

                    if (((newPickDate != orig.PickDate)) ||
                        ((newPickTime != orig.PickTime)) ||
                        ((newEstimatedStartDate != orig.EstimatedStartDate)) ||
                        ((newEstimatedStartTime != orig.EstimatedStartTime)) ||
                        ((newEstimatedStopDate != orig.EstimatedStopDate)) ||
                        ((newEstimatedStopTime != orig.EstimatedStopTime))
                        )
                    {
                        OrderDatesAndTimesChange change = new OrderDatesAndTimesChange();
                        change.OrderId = this.GetPrimaryKeys()[0].ToString();
                        change.OldPickDate = orig.PickDate;
                        change.NewPickDate = newPickDate;
                        change.OldPickTime = orig.PickTime;
                        change.NewPickTime = newPickTime;
                        change.OldEstimatedStartDate = orig.EstimatedStartDate;
                        change.NewEstimatedStartDate = newEstimatedStartDate;
                        change.OldEstimatedStartTime = orig.EstimatedStartTime;
                        change.NewEstimatedStartTime = newEstimatedStartTime;
                        change.OldEstimatedStopDate = orig.EstimatedStopDate;
                        change.NewEstimatedStopDate = newEstimatedStopDate;
                        change.OldEstimatedStopTime = orig.EstimatedStopTime;
                        change.NewEstimatedStopTime = newEstimatedStopTime;
                        bool b = OrderFunc.UpdateOrderItemDatesAndTimes(AppConfig, UserSession, change, e.SqlConnection).Result;
                    }

                    if (RateType != null)
                    {
                        if (!RateType.Equals(orig.RateType))
                        {
                            UpdateOrderItemRatesRequest request = new UpdateOrderItemRatesRequest();
                            request.OrderId = GetPrimaryKeys()[0].ToString();
                            request.RateType = RateType;
                            bool b = OrderFunc.UpdateOrderItemRates(AppConfig, UserSession, request, e.SqlConnection).Result;
                        }
                    }
                }
            }

            //if dates are changed, update line-item extendeds
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                bool datesChanged = false;

                if (e.Original != null)
                {
                    OrderBaseLogic orig = ((OrderBaseLogic)e.Original);
                    datesChanged = ((newEstimatedStartDate != orig.EstimatedStartDate) ||
                                    (newEstimatedStopDate != orig.EstimatedStopDate) ||
                                    (newBillingStartDate != orig.BillingStartDate) ||
                                    (newBillingEndDate != orig.BillingEndDate));
                }

                if (datesChanged)
                {
                    bool b = OrderFunc.UpdateOrderItemExtendedAllASync(this.AppConfig, this.UserSession, GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
                }
            }


            //saving PO Number and Amount
            {
                string origPoNumber = "";
                string poNumber = "";
                decimal? poAmount = 0;
                bool savePo = true;
                if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    poNumber = PoNumber ?? "";
                    poAmount = PoAmount ?? 0;
                }
                else // updating
                {
                    if (e.Original != null)
                    {
                        OrderBaseLogic orig = ((OrderBaseLogic)e.Original);
                        origPoNumber = orig.PoNumber;
                        poNumber = PoNumber ?? orig.PoNumber;
                        poAmount = PoAmount ?? orig.PoAmount;
                        savePo = ((!poNumber.Equals(orig.PoNumber)) || (!poAmount.Equals(orig.PoAmount)));
                    }
                }
                if (savePo)
                {
                    bool b = dealOrder.SavePoASync(origPoNumber, poNumber, poAmount, e.SqlConnection).Result;
                }
            }

            //after save - do work in the database
            {
                TSpStatusResponse r = OrderFunc.AfterSaveQuoteOrder(AppConfig, UserSession, this.GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
            }
        }
        //------------------------------------------------------------------------------------





        //------------------------------------------------------------------------------------
        public void OnBeforeSaveDealOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(dealOrder.OrderNumber))
                {
                    bool x = dealOrder.SetNumber(e.SqlConnection).Result;
                }
                StatusDate = FwConvert.ToString(DateTime.Today);
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSaveDealOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            billToAddress.UniqueId1 = dealOrder.OrderId;

            // justin hoffman 11/25/2019
            // this is really stupid
            // I am deleting the record that dbwIU_dealorder is giving us, so I can add my own and avoid a unique index error
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                DealOrderDetailRecord detailRec = new DealOrderDetailRecord();
                detailRec.SetDependencies(AppConfig, UserSession);
                detailRec.OrderId = GetPrimaryKeys()[0].ToString();
                bool b = detailRec.DeleteAsync(e.SqlConnection).Result;
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    TaxId = ((DealOrderRecord)e.Original).TaxId;
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
                if ((!string.IsNullOrEmpty(TaxOptionId)) && (!string.IsNullOrEmpty(TaxId)))
                {
                    bool b = false;
                    b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                    b = OrderFunc.UpdateOrderItemExtendedAllASync(this.AppConfig, this.UserSession, GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
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
        public void OnAfterMap(object sender, AfterMapEventArgs e)
        {
            if (e.Source != null)
            {
                ActivityDatesAndTimes = ((OrderBaseLoader)e.Source).ActivityDatesAndTimes;
            }
        }
        //-----------------------------------------------------------------------------------
    }
}
