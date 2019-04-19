using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;

namespace WebApi.Modules.Reports.BillingProgressReport
{
    public class BillingProgressReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
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
        public async Task<FwJsonDataTable> RunReportAsync(BillingProgressReportRequest request)
        {

            bool includeConfirmed = false;
            bool includeHold = false;
            bool includeActive = false;
            bool includeComplete = false;
            bool includeClosed = false;

            foreach (SelectedCheckBoxListItem item in request.Statuses)
            {
                if (item.value.Equals(RwConstants.ORDER_STATUS_CONFIRMED))
                {
                    includeConfirmed = true;
                }
                if (item.value.Equals(RwConstants.ORDER_STATUS_HOLD))
                {
                    includeHold = true;
                }
                if (item.value.Equals(RwConstants.ORDER_STATUS_ACTIVE))
                {
                    includeActive = true;
                }
                if (item.value.Equals(RwConstants.ORDER_STATUS_COMPLETE))
                {
                    includeComplete = true;
                }
                if (item.value.Equals(RwConstants.ORDER_STATUS_CLOSED))
                {
                    includeClosed = true;
                }
            }

            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getbillingprogressrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, request.AsOfDate);
                    qry.AddParameter("@includeconfirmed", SqlDbType.Text, ParameterDirection.Input, includeConfirmed ? "T" : "F");
                    qry.AddParameter("@includehold", SqlDbType.Text, ParameterDirection.Input, includeHold ? "T" : "F");
                    qry.AddParameter("@includeactive", SqlDbType.Text, ParameterDirection.Input, includeActive ? "T" : "F");
                    qry.AddParameter("@includecomplete", SqlDbType.Text, ParameterDirection.Input, includeComplete ? "T" : "F");
                    qry.AddParameter("@includeclosed", SqlDbType.Text, ParameterDirection.Input, includeClosed ? "T" : "F");
                    qry.AddParameter("@includecredits", SqlDbType.Text, ParameterDirection.Input, request.IncludeCredits.GetValueOrDefault(false) ? "T" : "F");
                    qry.AddParameter("@excludebilled100", SqlDbType.Text, ParameterDirection.Input, request.ExcludeBilled100.GetValueOrDefault(false) ? "T" : "F");
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@dealcsrid", SqlDbType.Text, ParameterDirection.Input, request.DealCsrId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, request.DealTypeId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@agentid", SqlDbType.Text, ParameterDirection.Input, request.AgentId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "OrderTotal", "Billed", "Remaining" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
