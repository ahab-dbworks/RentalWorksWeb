using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace WebApi.Modules.Reports.AccountingReports.GlDistributionReport
{
    [FwSqlTable("dbo.funcglsummaryrpt(@fromdate, @todate, @dealid)")]
    public class GlDistributionReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheading", modeltype: FwDataTypes.Text)]
        public string GroupHeading { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheadingorder", modeltype: FwDataTypes.Integer)]
        public int? GroupHeadingOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text)]
        public string AccountNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text)]
        public string AccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicestatus", modeltype: FwDataTypes.Text)]
        public string InvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebillingstart", modeltype: FwDataTypes.Date)]
        public string InvoiceBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebillingend", modeltype: FwDataTypes.Date)]
        public string InvoiceBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldate", modeltype: FwDataTypes.Date)]
        public string GlDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdescription", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Debit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "credit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Credit { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(GlDistributionReportRequest request)
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
                        this.OverrideTableName = "dbo.funcglsummaryrpt(@fromdate, @todate, @dealid)";
                    }
                    else if (request.IsSomeDetail.GetValueOrDefault(false))
                    {
                        this.OverrideTableName = "dbo.funcglsomedetailrpt(@fromdate, @todate, @dealid)";
                    }
                    else
                    {
                        this.OverrideTableName = "dbo.funcglfulldetailrpt(@fromdate, @todate, @dealid)";
                    }

                    useWithNoLock = false;
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    if (request.ExcludeGlAccountId.Length > 0)
                    {
                        select.AddWhere("(glaccountid not in (" + string.Join(",", request.ExcludeGlAccountId.Split(",").Select(x => "'" + x + "'")) + "))");
                    }
                    
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("glaccountid", request.GlAccountId);
                    select.AddParameter("@dealid", request.DealId);
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);
                    select.AddOrderBy("location,groupheadingorder,glno,glacctdesc");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] headerFieldsGlAccount = new string[] { "AccountDescription" };
                string[] totalFields = new string[] { "Debit", "Credit" };
                dt.InsertSubTotalRows("Location", "RowType", totalFields);
                dt.InsertSubTotalRows("GroupHeading", "RowType", totalFields);
                dt.InsertSubTotalRows("AccountNumber", "RowType", totalFields, headerFieldsGlAccount);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
