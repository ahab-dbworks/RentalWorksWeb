﻿using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.Order
{
    [FwSqlTable("orderview")]
    public abstract class OrderBaseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poamount", modeltype: FwDataTypes.Decimal)]
        public decimal? PoAmount { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean)]
        public bool? FlatPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean)]
        public bool? PendingPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal)]
        public decimal? MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lockbillingdates", modeltype: FwDataTypes.Boolean)]
        public bool LockBillingDates { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "delaybillingsearchuntil", modeltype: FwDataTypes.DateTime)]
        public string DelayBillingSearchUntil { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prepfeesinrentalrate", modeltype: FwDataTypes.Boolean)]
        public bool IncludePrepFeesInRentalRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        public string DetermineQuantitiesToBillBasedOn { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
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
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool NoCharge { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nochargereason", modeltype: FwDataTypes.Text)]
        public string NoChargeReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd", modeltype: FwDataTypes.Text)]
        public string PrintIssuedToAddressFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoname", modeltype: FwDataTypes.Text)]
        public string IssuedToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoattention", modeltype: FwDataTypes.Text)]
        public string IssuedToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoattention2", modeltype: FwDataTypes.Text)]
        public string IssuedToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd1", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd2", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocity", modeltype: FwDataTypes.Text)]
        public string IssuedToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtostate", modeltype: FwDataTypes.Text)]
        public string IssuedToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtozip", modeltype: FwDataTypes.Text)]
        public string IssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocountryid", modeltype: FwDataTypes.Text)]
        public string IssuedToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocountry", modeltype: FwDataTypes.Text)]
        public string IssuedToCountry { get; set; }
        //------------------------------------------------------------------------------------



        [FwSqlDataField(column: "differentbilladdress", modeltype: FwDataTypes.Boolean)]
        public bool BillToAddressDifferentFromIssuedToAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoname", modeltype: FwDataTypes.Text)]
        public string BillToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoattention", modeltype: FwDataTypes.Text)]
        public string BillToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoattention2", modeltype: FwDataTypes.Text)]
        public string BillToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text)]
        public string BillToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text)]
        public string BillToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtocountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "invoicediscountreasonid", modeltype: FwDataTypes.Text)]
        public string DiscountReasonId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicediscountreason", modeltype: FwDataTypes.Text)]
        public string DiscountReason { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "requirecontactconfirmation", modeltype: FwDataTypes.Boolean)]
        public bool RequireContactConfirmation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Boolean)]
        public bool IncludeInBillingAnalysis { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text)]
        public string HiatusDiscountFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "roundtriprentals", modeltype: FwDataTypes.Boolean)]
        public bool RoundTripRentals { get; set; }
        //------------------------------------------------------------------------------------



        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);



            if ((request != null) && (request.activeview != null))
            {
                switch (request.activeview)
                {
                    case "PROSPECT":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "PROSPECT");
                        break;
                    case "RESERVED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "RESERVED");
                        break;
                    case "CONFIRMED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CONFIRMED");
                        break;
                    case "HOLD":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "HOLD");
                        break;
                    case "ORDERED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "ORDERED");
                        break;
                    case "ACTIVE":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "ACTIVE");
                        break;
                    case "COMPLETE":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "COMPLETE");
                        break;
                    case "CLOSED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CLOSED");
                        break;
                    case "CANCELLED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CANCELLED");
                        break;
                    case "ALL":
                        break;
                }

                if (request.activeview.Contains("WarehouseId="))
                {
                    string whId = request.activeview.Replace("WarehouseId=", "");
                    if (!whId.Equals("ALL"))
                    {
                        select.AddWhere("(warehouseid = @whid)");
                        select.AddParameter("@whid", whId);
                    }
                }

                if (request.activeview.Contains("LocationId="))
                {
                    string locId = request.activeview.Replace("LocationId=", "");
                    if (!locId.Equals("ALL"))
                    {
                        select.AddWhere("(locationid = @locid)");
                        select.AddParameter("@locid", locId);
                    }
                }

            }
        }
        //------------------------------------------------------------------------------------    
    }
}
