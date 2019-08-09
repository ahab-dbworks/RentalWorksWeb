using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace WebApi.Modules.Reports.LateReturnsReport
{
    public class LateReturnsReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "orderedbycontactid", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string RentalItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
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
        [FwSqlDataField(column: "unitvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitvalueextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemUnitValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemReplacementCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderunitvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        //public decimal? OrderUnitValue { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderreplacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        //public decimal? OrderReplacementCost { get; set; }
        ////------------------------------------------------------------------------------------ 

        public async Task<FwJsonDataTable> RunReportAsync(LateReturnsReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getlatereturnsrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@reporttype", SqlDbType.Text, ParameterDirection.Input, request.ReportType);
                    qry.AddParameter("@days", SqlDbType.Int, ParameterDirection.Input, request.Days);
                    if (request.DueBackDate != DateTime.MinValue)
                    {
                        qry.AddParameter("@dueback", SqlDbType.Date, ParameterDirection.Input, request.DueBackDate);
                    }
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@orderdbycontactid", SqlDbType.Text, ParameterDirection.Input, request.OrderedByContactId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Quantity", "ItemUnitValueExtended", "ItemReplacementCostExtended" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertSubTotalRows("OrderNumber", "RowType", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
