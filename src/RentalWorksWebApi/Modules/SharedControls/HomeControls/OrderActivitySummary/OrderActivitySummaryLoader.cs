using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.OrderActivitySummary
{
    public class OrderActivitySummaryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grosstotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string GrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discount", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Discount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string SubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subtotaltaxable", modeltype: FwDataTypes.DecimalString2Digits)]
        public string SubTotalTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Total { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getorderactivitysummary", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
