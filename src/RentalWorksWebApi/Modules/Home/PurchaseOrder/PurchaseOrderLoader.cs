using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [FwSqlTable("powebview")]
    public class PurchaseOrderLoader : PurchaseOrderBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitiondate", modeltype: FwDataTypes.Date)]
        public string RequisitionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbyusersid", modeltype: FwDataTypes.Text)]
        public string ApprovedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbysecondusersid", modeltype: FwDataTypes.Text)]
        public string ApprovedBySecondUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean)]
        public bool? SubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean)]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptlocrequiresapproval", modeltype: FwDataTypes.Boolean)]
        public bool? DepartmentLocationRequiresApproval { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string PoTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string PoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbydate", modeltype: FwDataTypes.Date)]
        public string RequiredByDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text)]
        public string PoClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text)]
        public string PoClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsdays", modeltype: FwDataTypes.Integer)]
        public int? PaymentTermsDueInDays { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicedamount", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoicedAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agreementno", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatus", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatustype", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatustype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectdesc", modeltype: FwDataTypes.Text)]
        public string ProjectDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcostobject", modeltype: FwDataTypes.Text)]
        public string Orbitsapcostobject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dateacquired", modeltype: FwDataTypes.Date)]
        public string DateAcquired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "production", modeltype: FwDataTypes.Text)]
        public string Production { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setcharacter", modeltype: FwDataTypes.Text)]
        public string SetCharacter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setno", modeltype: FwDataTypes.Text)]
        public string Setno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tranactionno", modeltype: FwDataTypes.Text)]
        public string Tranactionno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacture", modeltype: FwDataTypes.Text)]
        public string Manufacture { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hassalesitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasSalesItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasmiscitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasMiscellaneousItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haslaboritem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLaborItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasspaceitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasFacilitiesItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasfinallditem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLossAndDamageItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalsaleitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 




        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? RentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? SalesTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PartsTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labordiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? LaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------







        [FwSqlDataField(column: "subrentaldaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? SubRentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subrentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SubRentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalesdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubSalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalestotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubSalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalestotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? SubSalesTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvehicledaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? SubVehicleDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvehiclediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SubvehicleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "submiscdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubMiscDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sublabordiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubLaborDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------




    }
}
