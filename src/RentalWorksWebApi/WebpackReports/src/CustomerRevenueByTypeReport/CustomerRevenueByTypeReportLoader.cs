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

namespace WebApi.Modules.Reports.CustomerRevenueByTypeReport
{
    [FwSqlTable("customerrevenuebytypeview")]
    public class CustomerRevenueByTypeReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
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
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assetsale", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AssetSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? NonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string dateType = "";
            string dateField = "";
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;

            string locationId = "";
            string departmentId = "";
            string customerId = "";
            string dealId = "";
            string orderTypeId = "";

            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("FromDate"))
                    {
                        fromDate = FwConvert.ToDateTime(uniqueIds["FromDate"].ToString());
                    }
                    if (uniqueIds.ContainsKey("ToDate"))
                    {
                        toDate = FwConvert.ToDateTime(uniqueIds["ToDate"].ToString());
                    }
                    if (uniqueIds.ContainsKey("DateType"))
                    {
                        dateType = uniqueIds["DateType"].ToString();
                    }
                    if (uniqueIds.ContainsKey("LocationId"))
                    {
                        locationId = uniqueIds["LocationId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DepartmentId"))
                    {
                        departmentId = uniqueIds["DepartmentId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("CustomerId"))
                    {
                        customerId = uniqueIds["CustomerId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealId"))
                    {
                        dealId = uniqueIds["DealId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("OrderTypeId"))
                    {
                        orderTypeId = uniqueIds["OrderTypeId"].ToString();
                    }
                }
            }

            dateField = "invoicedate";
            if (dateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
            {
                dateField = "billingstart";
            }

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getcustomerrevenuebytyperpt", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, fromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, toDate);
                    qry.AddParameter("@datefield", SqlDbType.Text, ParameterDirection.Input, dateField);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, locationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, departmentId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, customerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, dealId);
                    qry.AddParameter("@ordertypeid", SqlDbType.Text, ParameterDirection.Input, orderTypeId);
                    PropertyInfo[] propertyInfos = typeof(CustomerRevenueByTypeReportLoader).GetProperties();
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

            string[] totalFields = new string[] { "Rental", "Sales", "Facilities", "Labor", "Miscellaneous", "AssetSale", "Parts", "Tax", "Total" };
            dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
            dt.InsertSubTotalRows("Department", "RowType", totalFields);
            dt.InsertSubTotalRows("Customer", "RowType", totalFields);
            dt.InsertSubTotalRows("Deal", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);


            return dt;


        }
        //------------------------------------------------------------------------------------
    }
}
