using FwStandard.DataLayer;
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
namespace WebApi.Modules.Reports.ArAgingReport
{
    [FwSqlTable("tmpjh")]
    public class ArAgingReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
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
        [FwSqlDataField(column: "dealcsrid", modeltype: FwDataTypes.Text)]
        public string DealCsrId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcsr", modeltype: FwDataTypes.Text)]
        public string DealCsr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderagentid", modeltype: FwDataTypes.Text)]
        public string OrderAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderagent", modeltype: FwDataTypes.Text)]
        public string OrderAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicestatus", modeltype: FwDataTypes.Text)]
        public string InvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceagentid", modeltype: FwDataTypes.Text)]
        public string InvoiceAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceagent", modeltype: FwDataTypes.Text)]
        public string InvoiceAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total0030", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total0030 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total3160", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total3160 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total6190", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total6190 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total91x", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total91x { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingfinancecharge", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PendingFinanceCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        public string DivisionId { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            DateTime asOfDate = DateTime.Today;
            string locationId = "";
            string customerId = "";
            string dealCsrId = "";
            string dealTypeId = "";
            string dealId = "";

            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("AsOfDate"))
                    {
                        asOfDate = FwConvert.ToDateTime(uniqueIds["AsOfDate"].ToString());
                    }
                    if (uniqueIds.ContainsKey("OfficeLocationId"))
                    {
                        locationId = uniqueIds["OfficeLocationId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("CustomerId"))
                    {
                        customerId = uniqueIds["CustomerId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealCsrId"))
                    {
                        dealCsrId = uniqueIds["DealCsrId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealTypeId"))
                    {
                        dealTypeId = uniqueIds["DealTypeId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealId"))
                    {
                        dealId = uniqueIds["DealId"].ToString();
                    }
                }
            }
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getaragingrpt", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, asOfDate);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, locationId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, customerId);
                    qry.AddParameter("@dealcsrid", SqlDbType.Text, ParameterDirection.Input, dealCsrId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, dealTypeId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, dealId);
                    PropertyInfo[] propertyInfos = typeof(ArAgingReportLoader).GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute != null)
                        {
                            qry.AddColumn(sqlDataFieldAttribute.ColumnName, propertyInfo.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                        }
                    }
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            string[] totalFields = new string[] { "Total", "Total0030", "Total3160", "Total6190", "Total91x" };
            dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
            dt.InsertSubTotalRows("Customer", "RowType", totalFields);
            dt.InsertSubTotalRows("Deal", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
