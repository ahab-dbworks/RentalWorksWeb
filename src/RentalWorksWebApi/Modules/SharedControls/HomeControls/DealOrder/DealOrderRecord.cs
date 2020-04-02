using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Agent.PurchaseOrder;
using WebApi.Modules.Agent.Quote;
using WebApi;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.HomeControls.DealOrder
{
    [FwSqlTable("dealorder")]
    public class DealOrderRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 16)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15, required: true)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fromwarehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FromWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "finalld", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? LossAndDamage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------



        [FwSqlDataField(column: "pickupdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string PickUpDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickuptime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string PickUpTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string PrepDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadindate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string LoadInDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadintime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string LoadInTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string StrikeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "striketime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string StrikeTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "testdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string TestDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "testtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string TestTime { get; set; }
        //------------------------------------------------------------------------------------ 



        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? FlatPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PendingPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? VersionNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text, sqltype: "billingdates", maxlength: 10)]
        public string DetermineQuantitiesToBillBasedOn { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 01)]
        public bool? NoCharge { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nochargereason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string NoChargeReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PrintIssuedToAddressFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 100)]
        public string IssuedToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string IssuedToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string IssuedToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billzip", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string IssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string IssuedToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billemail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)]
        public string IssuedToEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billphone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string IssuedToPhone { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? IncludeInBillingAnalysis { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 07)]
        public string HiatusDiscountFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "summaryinvoice", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 01)]
        public bool? InGroup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? GroupNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string TermsConditionsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string OutDeliveryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string InDeliveryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whfromnotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FromWarehouseNotes { get; set; }
        //------------------------------------------------------------------------------------



        // purchase order
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 16)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitiondate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string RequisitionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ApprovedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubMiscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PoClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poimportanceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PoImportanceId { get; set; }
        //------------------------------------------------------------------------------------ 


        //container
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "quoteorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string QuoteOrderId { get; set; }
        //------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 


        /*
         
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Estrentfrom { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)] 
public string Estfromtime { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Estrentto { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)] 
public string Esttotime { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Versionno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "salesdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Salesdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Salestotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "miscdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Miscdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Billperiodstart { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Billperiodend { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string Flatpo { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "locked", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Locked { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Rentaldiscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 7)] 
public string Hiatusdiscfrom { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Nocharge { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string InputbyId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "labordiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Labordiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Pending { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billlockedtotal", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Billlockedtotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "whtonotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)] 
public string Whtonotes { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "whfromnotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)] 
public string Whfromnotes { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "requestsentat", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Requestsentat { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "requesttoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string RequesttoId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string Ratetype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "nochargereason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)] 
public string Nochargereason { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string FlatpoId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "enablesrmessage", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Enablesrmessage { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "issuedtoadd", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)] 
public string Issuedtoadd { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string LocationId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string OutdeliveryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Space { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "spacediscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Spacediscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "spacesplitpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Spacesplitpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "requireworksheet", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Requireworksheet { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rentaldaysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)] 
public decimal? Rentaldaysinwk { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)] 
public string Billingdates { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodlabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodlabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodmisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodmisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodspacetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodspacetotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "spacebillweekends", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Spacebillweekends { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "summaryinvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Summaryinvoice { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "summaryinvoicecolor", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Summaryinvoicecolor { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Summaryinvoicegroup { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklylabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklylabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklymisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklymisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklyrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyspacetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklyspacetotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "spacedaysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)] 
public decimal? Spacedaysinwk { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "summaryinvoiceorderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 4, scale: 1)] 
public decimal? Summaryinvoiceorderby { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "adjustcontractdate", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Adjustcontractdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Approveddate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "directbillcustomer", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Directbillcustomer { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "freightinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string FreightinvoiceId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlylabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlylabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlylabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlylabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlymisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlymisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlymisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlymisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlyrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlyrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyspacetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlyspacetotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyspacetotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlyspacetotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Partstotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "partstotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Partstotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodlabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodlabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodmisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodmisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodspacetotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodspacetotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikpayrentaldiscountdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Quikpayrentaldiscountdays { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikpayrentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 3)] 
public decimal? Quikpayrentaldiscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikpayrentaldiscounttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)] 
public string Quikpayrentaldiscounttype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikpayrentalperiodtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)] 
public decimal? Quikpayrentalperiodtotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "repairvendoronly", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Repairvendoronly { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rwnet", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Rwnet { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "salestotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Salestotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string TermsconditionsId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklylabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklylabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklymisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklymisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklyrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyspacetotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklyspacetotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ChgbatchId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "exportdetail", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Exportdetail { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "forcestatus", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Forcestatus { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapaccountno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)] 
public string Orbitsapaccountno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 6)] 
public string Orbitsapchgdeal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)] 
public string Orbitsapchgdetail { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)] 
public string Orbitsapchgmajor { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)] 
public string Orbitsapchgset { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 2)] 
public string Orbitsapchgsub { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsapcostobject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 24)] 
public string Orbitsapcostobject { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orbitsaptype", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Orbitsaptype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")] 
public int? Ownedrebaterate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ownedsplitrate", modeltype: FwDataTypes.Integer, sqltype: "smallint")] 
public int? Ownedsplitrate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string RebatecustomerId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Rebaterentalflg { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "splitrentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Splitrentalflg { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")] 
public int? Vendorrebaterate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vendorsplitrate", modeltype: FwDataTypes.Integer, sqltype: "smallint")] 
public int? Vendorsplitrate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)] 
public string Wano { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string IndeliveryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysublabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlysublabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysublabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Monthlysublabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubmisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlysubmisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubmisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Monthlysubmisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlysubrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Monthlysubrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsublabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodsublabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsublabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Periodsublabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubmisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodsubmisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubmisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Periodsubmisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodsubrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Periodsubrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "splitrentaltaxflg", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Splitrentaltaxflg { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "sublabordiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Sublabordiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "submiscdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Submiscdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "submitteddate", modeltype: FwDataTypes.Date, sqltype: "datetime")] 
public string Submitteddate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subrentaldaysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)] 
public decimal? Subrentaldaysinwk { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subrentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Subrentaldiscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subsalesdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Subsalesdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subsalestotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Subsalestotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subsalestotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Subsalestotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string WebusersId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysublabortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklysublabortotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysublabortotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Weeklysublabortotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubmisctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklysubmisctotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubmisctotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Weeklysubmisctotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklysubrentaltotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubrentaltotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "varchar")] 
public bool? Weeklysubrentaltotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)] 
public string Refno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "disablemetercharges", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Disablemetercharges { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Includeinbillinganalysis { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlysubvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlysubvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlysubvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Monthlyvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "monthlyvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Monthlyvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 2)] 
public decimal? Ordertotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodsubvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodsubvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodsubvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Periodvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "periodvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Periodvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vehicleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string VehicleId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subvehicledaysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)] 
public decimal? Subvehicledaysinwk { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "subvehiclediscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Subvehiclediscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string TaxId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vehicledaysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)] 
public decimal? Vehicledaysinwk { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vehiclediscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Vehiclediscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklysubvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklysubvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklysubvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyvehicletotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Weeklyvehicletotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "weeklyvehicletotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Weeklyvehicletotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rsdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Rsdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rstotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Rstotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rstotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Rstotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "lddiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Lddiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ldtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)] 
public decimal? Ldtotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ldtotalinctax", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Ldtotalinctax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "printlock", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Printlock { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ProjectmanagerId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "partsdiscpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 5)] 
public decimal? Partsdiscpercent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "approveby", modeltype: FwDataTypes.Date, sqltype: "datetime")] 
public string Approveby { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "applanguageid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ApplanguageId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attention2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)] 
public string Attention2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)] 
public string Billadd1 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)] 
public string Billadd2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "archived", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Archived { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ordertotalwhiatus", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)] 
public decimal? Ordertotalwhiatus { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "eventcategoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string EventcategoryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "taxtotalwhiatus", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)] 
public decimal? Taxtotalwhiatus { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikconfirmrentaldiscountdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Quikconfirmrentaldiscountdays { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikconfirmrentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 3)] 
public decimal? Quikconfirmrentaldiscountpct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "quikconfirmrentalperiodtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)] 
public decimal? Quikconfirmrentalperiodtotal { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Nonbillable { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)] 
public string Ordertype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string DealId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string OrdertypeId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string OrderId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string WarehouseId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "priority", modeltype: FwDataTypes.Integer, sqltype: "numeric")] 
public int? Priority { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "chkininclude", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Chkininclude { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string PaytermsId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string BillperiodId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "packagerate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)] 
public decimal? Packagerate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 16)] 
public string Orderno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)] 
public string Orderdesc { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "minrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)] 
public decimal? Minrate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)] 
public string Status { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)] 
public string Location { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Orderdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "webdesc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)] 
public string Webdesc { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Statusdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Rental { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Sales { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string PaytypeId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Labor { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "finalld", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Finalld { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Misc { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string DepartmentId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Rentalsale { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string BillcountryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)] 
public string Attention { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Inputdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string AgentId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)] 
public string Billname { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ModbyId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)] 
public string Billcity { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)] 
public string Billstate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billzip", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)] 
public string Billzip { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Moddate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------              
             */
        public class OrderOnHoldResponse : TSpStatusResponse
        {
            public OrderLogic order { get; set; } = null;
        }
        public class VoidPurchaseOrderResponse : TSpStatusResponse
        {
            public PurchaseOrderLogic purchaseOrder { get; set; } = null;
        }
        //public class ReserveQuoteResponse : TSpStatusResponse
        //{
        //    public QuoteLogic quote { get; set; } = null;
        //}

        //------------------------------------------------------------------------------------
        public async Task<bool> SavePoASync(string origPoNumber, string PoNumber, decimal? PoAmount, FwSqlConnection conn = null)
        {
            bool saved = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "setorderpo", this.AppConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
            qry.AddParameter("@orgpono", SqlDbType.NVarChar, ParameterDirection.Input, origPoNumber);
            qry.AddParameter("@newpono", SqlDbType.NVarChar, ParameterDirection.Input, PoNumber);
            qry.AddParameter("@poamount", SqlDbType.Decimal, ParameterDirection.Input, PoAmount);
            qry.AddParameter("@insertnew", SqlDbType.NVarChar, ParameterDirection.Input, string.IsNullOrEmpty(origPoNumber));
            await qry.ExecuteNonQueryAsync();
            saved = true;
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> SetNumber(FwSqlConnection conn)
        {
            string moduleName = "";
            if (Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
            {
                moduleName = RwConstants.MODULE_QUOTE;
            }
            else if (Type.Equals(RwConstants.ORDER_TYPE_ORDER))
            {
                moduleName = RwConstants.MODULE_ORDER;

                OfficeLocationLogic location = new OfficeLocationLogic();
                location.SetDependencies(AppConfig, UserSession);
                location.LocationId = OfficeLocationId;
                bool x = await location.LoadAsync<OrderLogic>();
                if (location.UseSameNumberForQuoteAndOrder.GetValueOrDefault(false))
                {
                    moduleName = RwConstants.MODULE_QUOTE;
                }
            }
            else if (Type.Equals(RwConstants.ORDER_TYPE_PROJECT))
            {
                moduleName = RwConstants.MODULE_PROJECT;
            }
            else if (Type.Equals(RwConstants.ORDER_TYPE_PURCHASE_ORDER))
            {
                moduleName = RwConstants.MODULE_PURCHASE_ORDER;
            }
            else if (Type.Equals(RwConstants.ORDER_TYPE_TRANSFER))
            {
                moduleName = RwConstants.MODULE_TRANSFER;
            }
            else
            {
                throw new Exception("Invalid Type " + Type + " in DealOrderRecord.SetNumber");
            }
            OrderNumber = await AppFunc.GetNextModuleCounterAsync(AppConfig, UserSession, moduleName, OfficeLocationId, conn);

            return true;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> UpdateOrderTotal(FwSqlConnection conn = null)
        {
            bool success = false;
            if ((OrderId != null) && ((Type.Equals(RwConstants.ORDER_TYPE_QUOTE) || (Type.Equals(RwConstants.ORDER_TYPE_ORDER) || (Type.Equals(RwConstants.ORDER_TYPE_PURCHASE_ORDER))))))
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
                }
                FwSqlCommand qry = new FwSqlCommand(conn, "updateordertotal", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> UpdatePoStatus(FwSqlConnection conn = null)
        {
            bool success = false;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_PURCHASE_ORDER)))
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
                }
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepostatus", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private async Task<string> Copy(QuoteOrderCopyRequest copyRequest, string copyToType)
        {
            string newId = "";
            if (OrderId != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "copyquoteorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@fromorderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@newordertype", SqlDbType.NVarChar, ParameterDirection.Input, copyToType);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.LocationId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.WarehouseId);
                    qry.AddParameter("@ratesfrominventory", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyRatesFromInventory);
                    qry.AddParameter("@combinesubs", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CombineSubs);
                    qry.AddParameter("@copydates", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyDates);
                    qry.AddParameter("@copyitemnotes", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyLineItemNotes);
                    qry.AddParameter("@copydocuments", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyDocuments);
                    qry.AddParameter("@copytodealid", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyToDealId);
                    qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    newId = qry.GetParameter("@neworderid").ToString();
                }
            }
            return newId;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<string> CopyToQuote(QuoteOrderCopyRequest copyRequest)
        {
            return await Copy(copyRequest, RwConstants.ORDER_TYPE_QUOTE);
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<string> CopyToOrder(QuoteOrderCopyRequest copyRequest)
        {
            return await Copy(copyRequest, RwConstants.ORDER_TYPE_ORDER);
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDaysPerWeek(ApplyBottomLineDaysPerWeekRequest request)
        {
            bool success = false;

            if (OrderId != null)
            {
                success = (await AppFunc.UserCanDW(this.AppConfig, UserSession.UsersId, request.DaysPerWeek));

                if (success)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, "updateadjustmentdw", this.AppConfig.DatabaseSettings.QueryTimeout);
                        qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                        qry.AddParameter("@parentid", SqlDbType.NVarChar, ParameterDirection.Input, "");   // supply a value to update all rows in a Complete or Kit
                        qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                        qry.AddParameter("@activity", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        qry.AddParameter("@issub", SqlDbType.NVarChar, ParameterDirection.Input, (request.Subs.GetValueOrDefault(false) ? "T" : "F"));
                        qry.AddParameter("@dw", SqlDbType.Decimal, ParameterDirection.Input, request.DaysPerWeek);
                        qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                        await qry.ExecuteNonQueryAsync();
                        success = true;
                    }
                }
            }
            return success;
        }
        //----------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDiscountPercent(ApplyBottomLineDiscountPercentRequest request)
        {
            bool success = false;
            if (OrderId != null)
            {
                success = (await AppFunc.UserCanDiscount(this.AppConfig, UserSession.UsersId, request.DiscountPercent));

                if (success)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, "updateadjustmentdiscount2", this.AppConfig.DatabaseSettings.QueryTimeout);
                        qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                        qry.AddParameter("@parentid", SqlDbType.NVarChar, ParameterDirection.Input, "");   // supply a value to update all rows in a Complete or Kit
                        qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                        qry.AddParameter("@activity", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        qry.AddParameter("@issub", SqlDbType.NVarChar, ParameterDirection.Input, (request.Subs.GetValueOrDefault(false) ? "T" : "F"));
                        qry.AddParameter("@discountpct", SqlDbType.Decimal, ParameterDirection.Input, request.DiscountPercent);
                        qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                        await qry.ExecuteNonQueryAsync();
                        success = true;
                    }
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineTotal(ApplyBottomLineTotalRequest request)
        {
            bool success = false;
            if (OrderId != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {

                    // W (weekly), M (monthly), E (episode), or P (period)

                    if (request.TotalType == null)
                    {
                        request.TotalType = RwConstants.TOTAL_TYPE_PERIOD;
                    }
                    if ((!request.TotalType.Equals(RwConstants.TOTAL_TYPE_WEEKLY)) && (!request.TotalType.Equals(RwConstants.TOTAL_TYPE_MONTHLY)) && (!request.TotalType.Equals(RwConstants.TOTAL_TYPE_EPISODIC)))
                    {
                        request.TotalType = RwConstants.TOTAL_TYPE_PERIOD;
                    }

                    FwSqlCommand qry = new FwSqlCommand(conn, "updateadjustmenttotal", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                    qry.AddParameter("@activity", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@episodeid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@issub", SqlDbType.NVarChar, ParameterDirection.Input, request.Subs.GetValueOrDefault(false));
                    qry.AddParameter("@totaltype", SqlDbType.NVarChar, ParameterDirection.Input, request.TotalType);
                    qry.AddParameter("@taxincluded", SqlDbType.NVarChar, ParameterDirection.Input, (request.IncludeTaxInTotal.GetValueOrDefault(false) ? "T" : "F"));
                    qry.AddParameter("@newtotal", SqlDbType.Decimal, ParameterDirection.Input, request.Total);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    await qry.ExecuteNonQueryAsync();
                    success = true;
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> CancelQuote()
        {
            bool success = false;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "cancelquote", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@quoteid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    await qry.ExecuteNonQueryAsync();
                    success = true;
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> UncancelQuote()
        {
            bool success = false;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "uncancelquote", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@quoteid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    await qry.ExecuteNonQueryAsync();
                    success = true;
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> CancelOrder()
        {
            bool success = false;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_ORDER)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "togglecancelorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    await qry.ExecuteNonQueryAsync();
                    success = true;
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> UncancelOrder()
        {
            bool success = false;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_ORDER)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "togglecancelorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    await qry.ExecuteNonQueryAsync();
                    success = true;
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<OrderOnHoldResponse> OnHoldOrder()
        {
            OrderOnHoldResponse response = new OrderOnHoldResponse();

            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_ORDER)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "orderonhold", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        //public async Task<string> CreateNewVersion()
        //{
        //    string newId = "";
        //    if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
        //    {
        //        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
        //        {
        //            //jh 08/28/2019 #922 posted hotfix 067 to fix this bug
        //            FwSqlCommand qry = new FwSqlCommand(conn, "quotenewver", this.AppConfig.DatabaseSettings.QueryTimeout);
        //            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
        //            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
        //            qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
        //            await qry.ExecuteNonQueryAsync();
        //            newId = qry.GetParameter("@neworderid").ToString();
        //        }
        //    }
        //    return newId;
        //}
        //-------------------------------------------------------------------------------------------------------
        public async Task<TSpStatusResponse> MakeQuoteActive()
        {
            TSpStatusResponse response = new TSpStatusResponse();
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "makequoteactiveweb", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@quoteid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.msg = qry.GetParameter("@msg").ToString();
                    response.success = (response.status == 0);
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<string> CreateSnapshot()
        {
            string newId = "";
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_ORDER)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "ordersnapshot", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    newId = qry.GetParameter("@neworderid").ToString();
                }
            }
            return newId;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> SubmitQuote()
        {
            bool success = false;
            int errno = 0;
            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "websubmitquote", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@webusersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.WebUsersId);
                    qry.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@errmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    errno = qry.GetParameter("@errno").ToInt32();
                    success = (errno == 0);
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<string> ActivateQuoteRequest()
        {
            string newOrderId = string.Empty;

            if ((OrderId != null) &&
                (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) &&
                (Status.Equals(RwConstants.QUOTE_STATUS_REQUEST)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "quoterequesttoquote", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    newOrderId = qry.GetParameter("@neworderid").ToString();
                }
            }
            return newOrderId;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<VoidPurchaseOrderResponse> Void()
        {
            // here is where we are making our request to the database.  I added a new stored procedure "voidpoweb" with output parameters for @status and @msg


            VoidPurchaseOrderResponse response = new VoidPurchaseOrderResponse();

            if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_PURCHASE_ORDER)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "voidpoweb", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);  // "OrderId" is part of this DealOrderRecord object.  This is our Purchase Order Id
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.msg = qry.GetParameter("@msg").ToString();
                    response.success = (response.status == 0);
                }
            }
            return response;  // return the entire object which either has a success=true or has success=false and an error message
        }
        //-------------------------------------------------------------------------------------------------------
        //public async Task<ReserveQuoteResponse> Reserve()
        //{
        //    ReserveQuoteResponse response = new ReserveQuoteResponse();
        //
        //    if ((OrderId != null) && (Type.Equals(RwConstants.ORDER_TYPE_QUOTE)))
        //    {
        //        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
        //        {
        //            FwSqlCommand qry = new FwSqlCommand(conn, "togglereservequoteweb", this.AppConfig.DatabaseSettings.QueryTimeout);
        //            qry.AddParameter("@quoteid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
        //            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
        //            qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
        //            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
        //            await qry.ExecuteNonQueryAsync();
        //            response.status = qry.GetParameter("@status").ToInt32();
        //            response.msg = qry.GetParameter("@msg").ToString();
        //            response.success = (response.status == 0);
        //        }
        //    }
        //    return response;
        //}
        //-------------------------------------------------------------------------------------------------------
        public async Task<ChangeOrderOfficeLocationResponse> ChangeOfficeLocationASync(ChangeOrderOfficeLocationRequest request)
        {
            ChangeOrderOfficeLocationResponse response = new ChangeOrderOfficeLocationResponse();
            if (OrderId != null)
            {
                FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
                FwSqlCommand qry = new FwSqlCommand(conn, "changelocationwarehousefororder", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                qry.AddParameter("@newlocationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                qry.AddParameter("@newwarehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.msg = qry.GetParameter("@msg").ToString();
                response.success = (response.status == 0);
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
