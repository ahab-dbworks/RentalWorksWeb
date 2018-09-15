//using FwStandard.DataLayer;
//using FwStandard.Models;
//using FwStandard.SqlServer;
//using FwStandard.SqlServer.Attributes;
//using WebApi.Data;
//using System.Collections.Generic;
//using System;
//using WebLibrary;
//using System.Threading.Tasks;
//using System.Data;
//using System.Reflection;
//namespace WebApi.Modules.Reports.SalesInventoryTransactionReport
//{
//    [FwSqlTable("salesinventorytransactionrptview")]
//    public class SalesInventoryTransactionReportLoader : AppDataLoadRecord
//    {
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
//        public string RowType { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
//        public string OfficeLocationId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
//        public string OfficeLocation { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
//        public string DepartmentId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
//        public string Department { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
//        public string CustomerId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
//        public string Customer { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
//        public string CustomerTypeId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
//        public string CustomerType { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
//        public string DealId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
//        public string Deal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
//        public string OrderId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
//        public string OrderNumber { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
//        public string InvoiceDate { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
//        public string InvoiceDescription { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "billingnote", modeltype: FwDataTypes.Text)]
//        public string BillingNote { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
//        public string InvoiceId { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
//        public string InvoiceNumber { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
//        public string BillingStartDate { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
//        public string BillingEndDate { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
//        public string BillingCycle { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
//        public string PoNumber { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "groupno", modeltype: FwDataTypes.Text)]
//        public string InvoiceGroupNumber { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "lastbatchno", modeltype: FwDataTypes.Text)]
//        public string LastBatchNumber { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
//        public bool? IsNoCharge { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "transtype", modeltype: FwDataTypes.Text)]
//        public string TransType { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicefreight", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceFreight { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicelisttotal", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceListTotal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicegrosstotal", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceGrossTotal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicediscounttotal", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceDiscountTotal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicesubtotal", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceSubTotal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceTax { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal)]
//        public decimal? InvoiceTotal { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
//        public bool? IsNonBillable { get; set; }
//        //------------------------------------------------------------------------------------ 
//        public async Task<FwJsonDataTable> RunReportAsync(SalesInventoryTransactionReportRequest request)
//        {
//            FwJsonDataTable dt = null;
//            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
//            {
//                FwSqlSelect select = new FwSqlSelect();
//                select.EnablePaging = false;
//                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
//                {
//                    SetBaseSelectQuery(select, qry);
//                    select.Parse();

//                    //select.AddWhere("(xxxxid ^> ')"); 
//                    addStringFilterToSelect("warehouseid", request.WarehouseId, select);
//                    addStringFilterToSelect("inventorydepartmentid", request.InventoryTypeId, select);
//                    addStringFilterToSelect("categoryid", request.CategoryId, select);
//                    addStringFilterToSelect("subcategoryid", request.SubCategoryId, select);
//                    addStringFilterToSelect("masterid", request.InventoryId, select);


//                    string dateField = "invoicedate";
//                    if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
//                    {
//                        dateField = "billingstart";
//                    }
//                    addDateFilterToSelect(dateField, request.FromDate, select, ">=", "fromdate");
//                    addDateFilterToSelect(dateField, request.ToDate, select, "<=", "todate");
//                    //if (!request.IncludeNoCharge.GetValueOrDefault(false))
//                    //{
//                    //    select.AddWhere("nocharge <> 'T'");
//                    //}
//                    select.AddWhereIn("and", "transtype", request.TransTypes.ToString(), false);
//                    select.AddOrderBy("whcode, masterno, transdate, orderby");
//                    dt = await qry.QueryToFwJsonTableAsync(select, false);
//                }
//            }
//            string[] totalFields = new string[] { "InvoiceTotal" };
//            dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
//            dt.InsertSubTotalRows("Department", "RowType", totalFields);
//            dt.InsertSubTotalRows("Customer", "RowType", totalFields);
//            dt.InsertSubTotalRows("Deal", "RowType", totalFields);
//            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
//            return dt;
//        }
//        //------------------------------------------------------------------------------------ 
//    }
//}
