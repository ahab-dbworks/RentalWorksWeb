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

namespace WebApi.Modules.Reports.BillingProgressReport
{
    public class BillingProgressReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? OrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billed", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Billed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedpercent", modeltype: FwDataTypes.Percentage)]
        public decimal? BilledPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            DateTime asOfDate = DateTime.Today;
            string locationId = "";
            bool includeCredits = false;

            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("AsOfDate"))
                    {
                        asOfDate = FwConvert.ToDateTime(uniqueIds["AsOfDate"].ToString());
                    }
                    if (uniqueIds.ContainsKey("LocationId"))
                    {
                        locationId = uniqueIds["LocationId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("IncludeCredits"))
                    {
                        includeCredits = FwConvert.ToBoolean(uniqueIds["IncludeCredits"].ToString());
                    }
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getbillingprogressrpt", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, asOfDate);
                    qry.AddParameter("@includecredits", SqlDbType.Text, ParameterDirection.Input, includeCredits ? "T" : "F");
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, locationId);
                    PropertyInfo[] propertyInfos = typeof(BillingProgressReportLoader).GetProperties();
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
            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
