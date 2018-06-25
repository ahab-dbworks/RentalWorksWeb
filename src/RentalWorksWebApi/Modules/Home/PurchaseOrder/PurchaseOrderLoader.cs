using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [FwSqlTable("poview")]
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
        [FwSqlDataField(column: "poneedsapproval", modeltype: FwDataTypes.Boolean)]
        public bool? NeedsApproval { get; set; }
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
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyholding", modeltype: FwDataTypes.Integer)]
        public int? QuantityHolding { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtytobarcode", modeltype: FwDataTypes.Integer)]
        public int? QuantityToBarCode { get; set; }
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
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
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
        [FwSqlDataField(column: "invoicedamount", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoicedAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
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
        [FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Boolean)]
        public bool? DropShip { get; set; }
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
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string LocdefaultcurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
