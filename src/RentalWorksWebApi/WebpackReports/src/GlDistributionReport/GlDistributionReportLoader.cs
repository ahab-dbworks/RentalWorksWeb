using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;

namespace WebApi.Modules.Reports.GlDistributionReport
{
    [FwSqlTable("dbo.funcglsummary(@fromdate, @todate)")]
    public class GlDistributionReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;

            if ((request != null) && (request.uniqueids != null))
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
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();


            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("GlAccountId", "glaccountid", select, request);

            select.AddParameter("@fromdate", fromDate);
            select.AddParameter("@todate", toDate);

        }
        //------------------------------------------------------------------------------------ 
    }
}
