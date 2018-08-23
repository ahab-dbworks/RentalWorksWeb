using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Reports.LateReturnsReport
{
    [FwSqlTable("dbo.funclatereturns(@issummary, @reporttype, @days, @dueback, @contactid)")]
    public class LateReturnsReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string OrderedByContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedByName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
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
        [FwSqlDataField(column: "orderdepartmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdepartment", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderfromdate", modeltype: FwDataTypes.Date)]
        public string OrderFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderfromtime", modeltype: FwDataTypes.Text)]
        public string OrderFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderfromdatetime", modeltype: FwDataTypes.Text)]
        public string OrderFromDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertodate", modeltype: FwDataTypes.Date)]
        public string OrderToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotime", modeltype: FwDataTypes.Text)]
        public string OrderToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertodatetime", modeltype: FwDataTypes.Text)]
        public string OrderToDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemfromdate", modeltype: FwDataTypes.Date)]
        public string ItemFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemfromtime", modeltype: FwDataTypes.Text)]
        public string ItemFromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemfromdatetime", modeltype: FwDataTypes.Text)]
        public string ItemFromDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemtodate", modeltype: FwDataTypes.Date)]
        public string ItemToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemtotime", modeltype: FwDataTypes.Text)]
        public string ItemToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemtodatetime", modeltype: FwDataTypes.Text)]
        public string ItemToDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billfrom", modeltype: FwDataTypes.Date)]
        public string BillFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billto", modeltype: FwDataTypes.Date)]
        public string BillToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billdaterange", modeltype: FwDataTypes.Text)]
        public string BillDateRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderpastdue", modeltype: FwDataTypes.Integer)]
        public int? OrderPastDue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itempastdue", modeltype: FwDataTypes.Integer)]
        public int? ItemPastDue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderduein", modeltype: FwDataTypes.Integer)]
        public int? OrderDueIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemduein", modeltype: FwDataTypes.Integer)]
        public int? ItemDueIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemunitvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? ItemUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemunitvalueextended", modeltype: FwDataTypes.Decimal)]
        public decimal? ItemUnitValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemreplacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ItemReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemreplacementcostextended", modeltype: FwDataTypes.Decimal)]
        public decimal? ItemReplacementCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderunitvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderreplacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {

            /*

     create function dbo.funclatereturns(@issummary  char(01),
                                @reporttype char(10), --// PAST_DUE, DUE_IN, DUE_DATE
                                @days       integer,
                                @dueback    datetime,
                                @contactid  char(08) = '')

     */

            useWithNoLock = false;

            bool isSummary = false;
            string reportType = "";
            int days = 0;
            DateTime dueBack = DateTime.MinValue; 
            string contactId = "";

            if ((request != null) && (request.uniqueids != null)) 
            { 
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("IsSummary"))
                {
                    isSummary = FwConvert.ToBoolean(uniqueIds["IsSummary"].ToString());
                }
                if (uniqueIds.ContainsKey("ReportType")) 
                {
                    reportType = uniqueIds["ReportType"].ToString(); 
                }
                if (uniqueIds.ContainsKey("Days"))
                {
                    days = FwConvert.ToInt32(uniqueIds["Days"].ToString());
                }
                if (uniqueIds.ContainsKey("DueBack")) 
                { 
                    dueBack = FwConvert.ToDateTime(uniqueIds["DueBack"].ToString()); 
                }
                if (uniqueIds.ContainsKey("ContactId"))
                {
                    contactId = uniqueIds["ContactId"].ToString();
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            select.AddParameter("@issummary", isSummary);
            select.AddParameter("@reporttype", reportType);
            select.AddParameter("@days", days);
            select.AddParameter("@dueback", dueBack);
            select.AddParameter("@contactid", contactId);
        }
        //------------------------------------------------------------------------------------ 
    }
}
