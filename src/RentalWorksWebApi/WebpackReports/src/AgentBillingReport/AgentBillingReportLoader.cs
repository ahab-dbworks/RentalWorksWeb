using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebLibrary;
using System.Threading.Tasks;
using System.Text;

namespace WebApi.Modules.Reports.AgentBillingReport
{
    [FwSqlTable("agentbillingview")]
    public class AgentBillingReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customertype", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date, hideInSummary: true)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date, hideInSummary: true)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date, hideInSummary: true)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingnote", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string BillingNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string WorkAuthorizationNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupno", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string GroupNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastbatchno", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string LastBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean, hideInSummary: true)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, hideInSummary: true)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MeterTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscellaneousTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assettotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AssetTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean, hideInSummary: true)]
        public bool? IsNonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(AgentBillingReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {

                    if (request.IsSummary.GetValueOrDefault(false))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("select     [" + TableAlias + "].[rowtype]       as [RowType],                 ");
                        sb.Append("           [" + TableAlias + "].[agent]         as [Agent],                   ");
                        sb.Append("           [" + TableAlias + "].[agentid]       as [AgentId],                 ");
                        sb.Append("       sum([" + TableAlias + "].[rentaltotal] ) as [RentalTotal],             ");
                        sb.Append("       sum([" + TableAlias + "].[metertotal]  ) as [MeterTotal],              ");
                        sb.Append("       sum([" + TableAlias + "].[salestotal]  ) as [SalesTotal],              ");
                        sb.Append("       sum([" + TableAlias + "].[spacetotal]  ) as [FacilitiesTotal],         ");
                        sb.Append("       sum([" + TableAlias + "].[misctotal]   ) as [MiscellaneousTotal],      ");
                        sb.Append("       sum([" + TableAlias + "].[labortotal]  ) as [LaborTotal],              ");
                        sb.Append("       sum([" + TableAlias + "].[partstotal]  ) as [PartsTotal],              ");
                        sb.Append("       sum([" + TableAlias + "].[assettotal]  ) as [AssetTotal],              ");
                        sb.Append("       sum([" + TableAlias + "].[invoicetax]  ) as [InvoiceTax],              ");
                        sb.Append("       sum([" + TableAlias + "].[invoicetotal]) as [InvoiceTotal]             ");
                        sb.Append("from " + TableName + " [" + TableAlias + "] with (nolock)                     ");
                        select.Add(sb.ToString());
                        AddPropertiesAsQueryColumns(qry);
                    }
                    else
                    {
                        SetBaseSelectQuery(select, qry);
                    }

                    select.Parse();
                    select.AddWhere("(agentid > '')");
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("agentid", request.AgentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);

                    string dateField = "invoicedate";
                    if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
                    {
                        dateField = "billingstart";
                    }
                    addDateFilterToSelect(dateField, request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect(dateField, request.ToDate, select, "<=", "todate");
                    if (!request.IncludeNoCharge.GetValueOrDefault(false))
                    {
                        select.AddWhere("nocharge <> 'T'");
                    }

                    if (request.IsSummary.GetValueOrDefault(false))
                    {
                        select.AddWhere("", "group by rowtype, agentid, agent");  //#jhtodo: need to be able to do select.AddGroupBy
                        select.AddOrderBy("agent");
                    }
                    else
                    {
                        select.AddOrderBy("agent,location,department,deal,orderno,invoicedate");
                    }

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "RentalTotal", "MeterTotal", "SalesTotal", "FacilitiesTotal", "MiscellaneousTotal", "LaborTotal", "PartsTotal", "AssetTotal", "InvoiceTax", "InvoiceTotal" };
                if (!request.IsSummary.GetValueOrDefault(false))
                {
                    dt.InsertSubTotalRows("Agent", "RowType", totalFields);
                }
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
