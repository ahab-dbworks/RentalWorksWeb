using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System;

namespace WebApi.Modules.Utilities.QuikActivity
{
    //[FwSqlTable("masteritem")]
    public class QuikActivityLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydate", modeltype: FwDataTypes.Text)]
        public string ActivityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydesc", modeltype: FwDataTypes.Text)]
        public string ActivityDescription { get; set; }
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
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemcount", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            DateTime fromDate = GetUniqueIdAsDate("FromDate", request).GetValueOrDefault(DateTime.Today);
            DateTime toDate = GetUniqueIdAsDate("ToDate", request).GetValueOrDefault(DateTime.Today);
            string activityType = GetUniqueIdAsString("ActivityType", request) ?? "";
            bool summary = GetUniqueIdAsBoolean("Summary", request).GetValueOrDefault(false);

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getquikactivitydatadetail", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, fromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, toDate);
                    qry.AddParameter("@activitytype", SqlDbType.NVarChar, ParameterDirection.Input, activityType);
                    qry.AddParameter("@summarizeorders", SqlDbType.NVarChar, ParameterDirection.Input, (summary ? "T" : "F"));
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
