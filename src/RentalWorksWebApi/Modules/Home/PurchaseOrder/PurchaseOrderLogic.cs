using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebApi.Modules.Home.Tax;
using WebApi.Modules.Home.Vendor;
using WebApi.Modules.Settings.DefaultSettings;
using WebApi.Modules.Settings.OfficeLocation;
using WebLibrary;
using static WebApi.Modules.Home.DealOrder.DealOrderRecord;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [FwLogic(Id: "XRpbTRGJFX88z")]
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
        [FwLogicProperty(Id: "VIVLmYhhFSMVm", IsPrimaryKey: true)]
        public string PurchaseOrderId { get { return purchaseOrder.OrderId; } set { purchaseOrder.OrderId = value; purchaseOrderDetail.OrderId = value; } }

        [FwLogicProperty(Id: "yq3FBSQUmjuzX", IsRecordTitle: true)]
        public string PurchaseOrderNumber { get { return purchaseOrder.OrderNumber; } set { purchaseOrder.OrderNumber = value; } }

        [FwLogicProperty(Id: "fDewVmifAdGcP", IsRecordTitle: true)]
        public string Description { get { return purchaseOrder.Description; } set { purchaseOrder.Description = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "R3qdAmIZf7SZ", DisableDirectModify: true)]
        public string Type { get { return purchaseOrder.Type; } set { purchaseOrder.Type = value; } }

        [FwLogicProperty(Id: "eMypPac6XH9t")]
        public string PurchaseOrderDate { get { return purchaseOrder.OrderDate; } set { purchaseOrder.OrderDate = value; } }

        [FwLogicProperty(Id: "eS25roYPI2hU")]
        public string RequisitionNumber { get { return purchaseOrder.RequisitionNumber; } set { purchaseOrder.RequisitionNumber = value; } }

        [FwLogicProperty(Id: "5My8t08kcKoZ")]
        public string RequisitionDate { get { return purchaseOrder.RequisitionDate; } set { purchaseOrder.RequisitionDate = value; } }

        [FwLogicProperty(Id: "UP6Z8mbFg6oB")]
        public string VendorId { get { return purchaseOrder.VendorId; } set { purchaseOrder.VendorId = value; } }

        [FwLogicProperty(Id: "QEgN82QcXGeOm", IsReadOnly: true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "gIEzxsRPhre2")]
        public string AgentId { get { return purchaseOrder.AgentId; } set { purchaseOrder.AgentId = value; } }

        [FwLogicProperty(Id: "oDM7VaQjF3HKd", IsReadOnly: true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id: "GeBFSHGC85Hf", DisableDirectModify: true)]
        public string Status { get { return purchaseOrder.Status; } set { purchaseOrder.Status = value; } }

        [FwLogicProperty(Id: "cpHP83yfqMvW", DisableDirectModify: true)]
        public string StatusDate { get { return purchaseOrder.StatusDate; } set { purchaseOrder.StatusDate = value; } }

        [FwLogicProperty(Id: "xGr0YxadipyP")]
        public string ReferenceNumber { get { return purchaseOrder.ReferenceNumber; } set { purchaseOrder.ReferenceNumber = value; } }

        [FwLogicProperty(Id: "xLqYZa4B3iPaD", IsReadOnly: true)]
        public bool? NeedsApproval { get; set; }

        [FwLogicProperty(Id: "k6URS6OVU8PH")]
        public string ApprovedByUserId { get { return purchaseOrder.ApprovedByUserId; } set { purchaseOrder.ApprovedByUserId = value; } }

        //[FwLogicProperty(Id:"MKsyx4k08raf")]
        //public string ApprovedBySecondUserId { get; set; }

        [FwLogicProperty(Id: "xdSSezDjZXS4")]
        public string DepartmentId { get { return purchaseOrder.DepartmentId; } set { purchaseOrder.DepartmentId = value; } }

        [FwLogicProperty(Id: "TKeJIOFmtxvQa", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "wMLGaz491jp3", DisableDirectModify: true)]
        public string OfficeLocationId { get { return purchaseOrder.OfficeLocationId; } set { purchaseOrder.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "01yCQOud66XnC", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "uxqpq3ELNy79", DisableDirectModify: true)]
        public string WarehouseId { get { return purchaseOrder.WarehouseId; } set { purchaseOrder.WarehouseId = value; } }

        [FwLogicProperty(Id: "8XKphJFdmXUxA", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "wWkK0CLM6YpTm", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "hk4TKLe87oH1y", IsReadOnly: true)]
        public int? QuantityHolding { get; set; }

        [FwLogicProperty(Id: "NltrEXqqTj8W4", IsReadOnly: true)]
        public int? QuantityToBarCode { get; set; }

        [FwLogicProperty(Id: "z7uT5aQyqM9D")]
        public bool? Rental { get { return purchaseOrder.Rental; } set { purchaseOrder.Rental = value; } }

        [FwLogicProperty(Id: "7antlUuZwtHZ")]
        public bool? Sales { get { return purchaseOrder.Sales; } set { purchaseOrder.Sales = value; } }

        [FwLogicProperty(Id: "WdBpgBgw2dGI")]
        public bool? Parts { get { return purchaseOrder.Parts; } set { purchaseOrder.Parts = value; } }

        [FwLogicProperty(Id: "IwSkzn5q2hfR")]
        public bool? Labor { get { return purchaseOrder.Labor; } set { purchaseOrder.Labor = value; } }

        [FwLogicProperty(Id: "ilnWoyy1JHKo")]
        public bool? Miscellaneous { get { return purchaseOrder.Miscellaneous; } set { purchaseOrder.Miscellaneous = value; } }

        [FwLogicProperty(Id: "PUjCaGyz57x6")]
        public bool? Vehicle { get { return purchaseOrder.Vehicle; } set { purchaseOrder.Vehicle = value; } }

        [FwLogicProperty(Id: "vxXkqkwR6xfP")]
        public bool? SubRent { get { return purchaseOrder.SubRent; } set { purchaseOrder.SubRent = value; } }

        [FwLogicProperty(Id: "hTjmsPcl5OeJ")]
        public bool? SubSale { get { return purchaseOrder.SubSale; } set { purchaseOrder.SubSale = value; } }

        [FwLogicProperty(Id: "DaqP1h98caC4")]
        public bool? SubLabor { get { return purchaseOrder.SubLabor; } set { purchaseOrder.SubLabor = value; } }

        [FwLogicProperty(Id: "yoR4lgPGziH7")]
        public bool? SubMiscellaneous { get { return purchaseOrder.SubMiscellaneous; } set { purchaseOrder.SubMiscellaneous = value; } }

        [FwLogicProperty(Id: "BHQEuizKJNds")]
        public bool? SubVehicle { get { return purchaseOrder.SubVehicle; } set { purchaseOrder.SubVehicle = value; } }

        [FwLogicProperty(Id: "ZpOB5ktmEwcc")]
        public bool? Repair { get { return purchaseOrder.Repair; } set { purchaseOrder.Repair = value; } }

        [FwLogicProperty(Id: "yHNCT4jIh6nO")]
        public bool? Consignment { get { return purchaseOrder.Consignment; } set { purchaseOrder.Consignment = value; } }

        [FwLogicProperty(Id: "eV8MfEqoXS5C")]
        public string ConsignorAgreementId { get { return purchaseOrder.ConsignorAgreementId; } set { purchaseOrder.ConsignorAgreementId = value; } }

        [FwLogicProperty(Id: "qOWXcLyuUQfsM", IsReadOnly: true)]
        public string ConsignorAgreementNumber { get; set; }

        [FwLogicProperty(Id: "VIVLmYhhFSMVm", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "yq3FBSQUmjuzX", IsReadOnly: true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "n7knYwXQ91Mbr", IsReadOnly: true)]
        public string DealNumber { get; set; }

        [FwLogicProperty(Id: "n7knYwXQ91Mbr", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "RYGMA4WYuYz2")]
        public string RateType { get { return purchaseOrder.RateType; } set { purchaseOrder.RateType = value; } }

        [FwLogicProperty(Id: "TKeJIOFmtxvQa", IsReadOnly: true)]
        public bool? DepartmentLocationRequiresApproval { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? Total { get; set; }

        [FwLogicProperty(Id: "96fIFyhQ47HF")]
        public string PoTypeId { get { return purchaseOrder.OrderTypeId; } set { purchaseOrder.OrderTypeId = value; } }

        [FwLogicProperty(Id: "c8aP7WsydAV18", IsReadOnly: true)]
        public string PoType { get; set; }

        //[FwLogicProperty(Id:"NLXl0UdgPZ2P")]
        //public string RequiredByDate { get; set; }

        [FwLogicProperty(Id: "x1hGzge6hHZH")]
        public string PoClassificationId { get { return purchaseOrder.PoClassificationId; } set { purchaseOrder.PoClassificationId = value; } }

        [FwLogicProperty(Id: "pSJOEHHADRcU5", IsReadOnly: true)]
        public string PoClassification { get; set; }

        [FwLogicProperty(Id: "HJ8GhcEZYu3a")]
        public string EstimatedStartDate { get { return purchaseOrder.EstimatedStartDate; } set { purchaseOrder.EstimatedStartDate = value; } }

        [FwLogicProperty(Id: "5Gfpglv62U6q")]
        public string EstimatedStopDate { get { return purchaseOrder.EstimatedStopDate; } set { purchaseOrder.EstimatedStopDate = value; } }

        [FwLogicProperty(Id: "Z578OE4eTaty")]
        public string BillingStartDate { get { return purchaseOrder.BillingStartDate; } set { purchaseOrder.BillingStartDate = value; } }

        [FwLogicProperty(Id: "Bzgy8WpYM2zf")]
        public string BillingEndDate { get { return purchaseOrder.BillingEndDate; } set { purchaseOrder.BillingEndDate = value; } }

        [FwLogicProperty(Id: "Qkr2CvGb90kyC", IsReadOnly: true)]
        public decimal? InvoicedAmount { get; set; }

        //[FwLogicProperty(Id:"nxS9XuVwOUk4")]
        //public decimal? WeeklyExtended { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatusId { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatus { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatustype { get; set; }

        [FwLogicProperty(Id: "hY8fUmCOOAn9")]
        public string ProjectManagerId { get { return purchaseOrder.ProjectManagerId; } set { purchaseOrder.ProjectManagerId = value; } }

        [FwLogicProperty(Id: "37LvI2DfgGPrz", IsReadOnly: true)]
        public string ProjectManager { get; set; }

        [FwLogicProperty(Id: "RBduUGu4mQ9s")]
        public string OutDeliveryId { get { return purchaseOrder.OutDeliveryId; } set { purchaseOrder.OutDeliveryId = value; } }

        [FwLogicProperty(Id: "apaJjUkKu4UIk", IsReadOnly: true)]
        public bool? DropShip { get; set; }

        [FwLogicProperty(Id: "Y9sD3wDUch7o")]
        public string InDeliveryId { get { return purchaseOrder.InDeliveryId; } set { purchaseOrder.InDeliveryId = value; } }

        [FwLogicProperty(Id: "TJnlyuYOArN3")]
        public string ProjectId { get { return purchaseOrder.ProjectId; } set { purchaseOrder.ProjectId = value; } }

        [FwLogicProperty(Id: "JcMVlViabrzP4", IsReadOnly: true)]
        public string ProjectNumber { get; set; }

        [FwLogicProperty(Id: "fDewVmifAdGcP", IsReadOnly: true)]
        public string ProjectDescription { get; set; }

        [FwLogicProperty(Id: "WYP8xqoGbI6M")]
        public string Location { get { return purchaseOrder.Location; } set { purchaseOrder.Location = value; } }

        [FwLogicProperty(Id: "e2zOESzvmQPY")]
        public string CurrencyId { get { return purchaseOrder.CurrencyId; } set { purchaseOrder.CurrencyId = value; } }

        [FwLogicProperty(Id: "T6x3UjlxoKwxj", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "uApGEj8iolr8")]
        public string BillingCycleId { get { return purchaseOrder.BillingCycleId; } set { purchaseOrder.BillingCycleId = value; } }

        [FwLogicProperty(Id: "QRNhl3XsQMUmi", IsReadOnly: true)]
        public string BillingCycle { get; set; }

        [FwLogicProperty(Id: "XfP2lmt73Pt")]
        public string PaymentTermsId { get { return purchaseOrder.PaymentTermsId; } set { purchaseOrder.PaymentTermsId = value; } }

        [FwLogicProperty(Id: "oivJq1BqYTc", IsReadOnly: true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id: "r0nQNfNwLAu", IsReadOnly: true)]
        public int? PaymentTermsDueInDays { get; set; }

        [FwLogicProperty(Id: "D60yNs4DNU1R")]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "oafJa8FREKfAu", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "gNNYzTTDz8v8", DisableDirectModify: true)]
        public string TaxId { get { return purchaseOrder.TaxId; } set { purchaseOrder.TaxId = value; } }

        [FwLogicProperty(Id: "krjw2wDrUP6R")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "kAEEeWXhtxzg")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "nyqIGqlupZes")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "NmUaCdYkAKC5")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "Aa6GJdEpQTxq")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "vpxC9SS88U27")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }

        [FwLogicProperty(Id: "HFIgsyl9ZgM2r", IsReadOnly: true)]
        public bool? HasRentalItem { get; set; }

        [FwLogicProperty(Id: "0TNYdnneENoGn", IsReadOnly: true)]
        public bool? HasSalesItem { get; set; }

        [FwLogicProperty(Id: "HGMYcHXNfJkjw", IsReadOnly: true)]
        public bool? HasMiscellaneousItem { get; set; }

        [FwLogicProperty(Id: "tBLuzGVzghpES", IsReadOnly: true)]
        public bool? HasLaborItem { get; set; }

        [FwLogicProperty(Id: "UeBCOJHXtP3oz", IsReadOnly: true)]
        public bool? HasFacilitiesItem { get; set; }

        [FwLogicProperty(Id: "zBYEvIYJxoWr0", IsReadOnly: true)]
        public bool? HasLossAndDamageItem { get; set; }

        [FwLogicProperty(Id: "DpKgFis6AxoL7", IsReadOnly: true)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? RentalDiscountPercent { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? RentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? RentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SalesDiscountPercent { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SalesTotal { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public bool? SalesTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public decimal? PartsDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public decimal? PartsTotal { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public bool? PartsTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "WRIFQEwFJ1TeJ", IsReadOnly: true)]
        public decimal? VehicleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? VehicleTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? VehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "3LltCBVzUG7a")]
        public decimal? MiscDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? MiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? MiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? LaborDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? LaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? LaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? SubRentalDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? SubRentalDiscountPercent { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? WeeklySubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? MonthlySubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? PeriodSubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? WeeklySubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? MonthlySubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? PeriodSubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SubSalesDiscountPercent { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SubSalesTotal { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public bool? SubSalesTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? SubVehicleDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "VxpP62n9VOim")]
        public decimal? SubvehicleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? WeeklySubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? MonthlySubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? PeriodSubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? WeeklySubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? MonthlySubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? PeriodSubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "2XE5e6UFQ8Xu")]
        public decimal? SubMiscDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? WeeklySubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? MonthlySubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? PeriodSubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? WeeklySubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? MonthlySubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? PeriodSubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? SubLaborDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? WeeklySubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? MonthlySubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? PeriodSubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? WeeklySubLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? MonthlySubLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? PeriodSubLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "EJHdP3FGL75fJ", IsReadOnly: true)]
        public string CurrencyColor { get; set; }
        [FwLogicProperty(Id: "mbfCIKAk0BI79", IsReadOnly: true)]
        public string StatusColor { get; set; }
        [FwLogicProperty(Id: "WHeeHTlDkTL7P", IsReadOnly: true)]
        public string PurchaseOrderNumberColor { get; set; }
        [FwLogicProperty(Id: "7Np3MnmdfipuR", IsReadOnly: true)]
        public string VendorColor { get; set; }
        [FwLogicProperty(Id: "4RDOWSMI7rlpm", IsReadOnly: true)]
        public string DescriptionColor { get; set; }
        [FwLogicProperty(Id: "DtTheaPQm2q9R", IsReadOnly: true)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 

        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            string rateType = string.Empty;
            bool misc = false, labor = false, subRent = false, subSale = false, repair = false, subMisc = false, subLabor = false;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                rateType = RateType;
                misc = Miscellaneous.GetValueOrDefault(false);
                labor = Labor.GetValueOrDefault(false);
                subRent = SubRent.GetValueOrDefault(false);
                subSale = SubSale.GetValueOrDefault(false);
                repair = Repair.GetValueOrDefault(false);
                subMisc = SubMiscellaneous.GetValueOrDefault(false);
                subLabor = SubLabor.GetValueOrDefault(false);
            }
            else
            {
                if (original != null)
                {
                    PurchaseOrderLogic lOrig = ((PurchaseOrderLogic)original);

                    rateType = RateType ?? lOrig.RateType;
                    misc = (Miscellaneous ?? lOrig.Miscellaneous).GetValueOrDefault(false);
                    labor = (Labor ?? lOrig.Labor).GetValueOrDefault(false);
                    subRent = (SubRent ?? lOrig.SubRent).GetValueOrDefault(false);
                    subSale = (SubSale ?? lOrig.SubSale).GetValueOrDefault(false);
                    repair = (Repair ?? lOrig.Repair).GetValueOrDefault(false);
                    subMisc = (SubMiscellaneous ?? lOrig.SubMiscellaneous).GetValueOrDefault(false);
                    subLabor = (SubLabor ?? lOrig.SubLabor).GetValueOrDefault(false);

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

            if (isValid)
            {
                if (string.IsNullOrEmpty(rateType) && (misc || labor || subRent || subMisc || subLabor))
                {
                    isValid = false;
                    validateMsg = "Rate Type is required for this " + BusinessLogicModuleName + ".";
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

                if (string.IsNullOrEmpty(BillingCycleId))
                {
                    if (string.IsNullOrEmpty(VendorId))
                    {
                        DefaultSettingsLogic defaults = new DefaultSettingsLogic();
                        defaults.SetDependencies(AppConfig, UserSession);
                        defaults.DefaultSettingsId = RwConstants.CONTROL_ID;
                        bool b = defaults.LoadAsync<DefaultSettingsLogic>().Result;
                        BillingCycleId = defaults.DefaultDealBillingCycleId;
                    }
                    else
                    {
                        VendorLogic vendor = new VendorLogic();
                        vendor.SetDependencies(AppConfig, UserSession);
                        vendor.VendorId = VendorId;
                        bool b = vendor.LoadAsync<VendorLogic>().Result;
                        BillingCycleId = vendor.BillingCycleId;
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
                int i = purchaseOrder.SaveAsync(null, e.SqlConnection).Result;
            }
            bool b2 = purchaseOrder.UpdatePoStatus(e.SqlConnection).Result;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSavePurchaseOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = purchaseOrder.SetNumber(e.SqlConnection).Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
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
                        bool b2 = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                    }
                }
            }

            bool b3 = purchaseOrder.UpdateOrderTotal(e.SqlConnection).Result;
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
                    bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
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
        public async Task<VoidPurchaseOrderResponse> Void()
        {
            return await purchaseOrder.Void();
        }
        //------------------------------------------------------------------------------------ 
    }
}
