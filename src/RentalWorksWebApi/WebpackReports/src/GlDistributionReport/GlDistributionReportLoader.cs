using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.GlDistributionReport
{
    [FwSqlTable("dbo.funcglsummary(@fromdate, @todate)")]
    public class GlDistributionReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
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
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    useWithNoLock = false;
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("glaccountid", request.GlAccountId);
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);
                    select.AddOrderBy("location,groupheading,glno,glacctdesc");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "Debit", "Credit" };
                dt.InsertSubTotalRows("Location", "RowType", totalFields);
                dt.InsertSubTotalRows("GroupHeading", "RowType", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
