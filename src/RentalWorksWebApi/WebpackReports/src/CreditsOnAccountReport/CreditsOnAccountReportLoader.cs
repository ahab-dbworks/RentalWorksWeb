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
namespace WebApi.Modules.Reports.CreditsOnAccountReport
{
    [FwSqlTable("creditsonaccountwebview")]
    public class CreditsOnAccountReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldepdep", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDepletingDeposit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalcredit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalover", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalOverpayment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldeposit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDeposit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalapplied", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalApplied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalrefunded", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalRefunded { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            bool onlyRemaining = false;

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("OnlyRemaining"))
                {
                    onlyRemaining = FwConvert.ToBoolean(uniqueIds["OnlyRemaining"].ToString());
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if (onlyRemaining)
            {
                select.AddWhere("remaining > 0");
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
