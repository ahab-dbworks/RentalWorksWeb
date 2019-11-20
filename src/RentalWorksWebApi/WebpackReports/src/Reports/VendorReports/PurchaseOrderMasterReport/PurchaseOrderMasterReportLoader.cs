using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using WebApi;

namespace WebApi.Modules.Reports.VendorReports.PurchaseOrderMasterReport
{
    public class PurchaseOrderMasterReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "postatus", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingPeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean)]
        public bool? Subrent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean)]
        public bool? Subsale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean)]
        public bool? Sublabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean)]
        public bool? Submisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Subvehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pototal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PurchaseOrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poopenamount", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PurchaseOrderOpenAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(PurchaseOrderMasterReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getpomasterrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    bool activityRental = false;
                    bool activitySales = false;
                    bool activityParts = false;
                    bool activityMisc = false;
                    bool activityLabor = false;
                    bool activityRepair = false;
                    bool activityVehicle = false;
                    bool activityConsignment = false;
                    bool activitySubRent = false;
                    bool activitySubSale = false;
                    bool activitySubMisc = false;
                    bool activitySubLabor = false;
                    bool activitySubVehicle = false;


                    foreach (SelectedCheckBoxListItem rt in request.Activities)
                    {
                        if (rt.value.Equals("RENTAL"))
                        {
                            activityRental = true;
                        }
                        else if (rt.value.Equals("SALES"))
                        {
                            activitySales = true;
                        }
                        else if (rt.value.Equals("PARTS"))
                        {
                            activityParts = true;
                        }
                        else if (rt.value.Equals("LABOR"))
                        {
                            activityLabor = true;
                        }
                        else if (rt.value.Equals("MISC"))
                        {
                            activityMisc = true;
                        }
                        else if (rt.value.Equals("REPAIR"))
                        {
                            activityRepair = true;
                        }
                        else if (rt.value.Equals("VEHICLE"))
                        {
                            activityVehicle = true;
                        }
                        else if (rt.value.Equals("CONSIGNMENT"))
                        {
                            activityConsignment = true;
                        }
                        else if (rt.value.Equals("SUBRENT"))
                        {
                            activitySubRent = true;
                        }
                        else if (rt.value.Equals("SUBSALE"))
                        {
                            activitySubSale = true;
                        }
                        else if (rt.value.Equals("SUBLABOR"))
                        {
                            activitySubLabor = true;
                        }
                        else if (rt.value.Equals("SUBMISC"))
                        {
                            activitySubMisc = true;
                        }
                        else if (rt.value.Equals("SUBVEHICLE"))
                        {
                            activitySubVehicle = true;
                        }
                    }
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@vendorid", SqlDbType.Text, ParameterDirection.Input, request.VendorId);
                    qry.AddParameter("@postatus", SqlDbType.Text, ParameterDirection.Input, request.Statuses.ToString());

                    qry.AddParameter("@rental", SqlDbType.Text, ParameterDirection.Input, activityRental);
                    qry.AddParameter("@sales", SqlDbType.Text, ParameterDirection.Input, activitySales);
                    qry.AddParameter("@parts", SqlDbType.Text, ParameterDirection.Input, activityParts);
                    qry.AddParameter("@misc", SqlDbType.Text, ParameterDirection.Input, activityMisc);
                    qry.AddParameter("@labor", SqlDbType.Text, ParameterDirection.Input, activityLabor);
                    qry.AddParameter("@repair", SqlDbType.Text, ParameterDirection.Input, activityRepair);
                    qry.AddParameter("@vehicle", SqlDbType.Text, ParameterDirection.Input, activityVehicle);
                    qry.AddParameter("@consignment", SqlDbType.Text, ParameterDirection.Input, activityConsignment);
                    qry.AddParameter("@subrent", SqlDbType.Text, ParameterDirection.Input, activitySubRent);
                    qry.AddParameter("@subsale", SqlDbType.Text, ParameterDirection.Input, activitySubSale);
                    qry.AddParameter("@submisc", SqlDbType.Text, ParameterDirection.Input, activitySubMisc);
                    qry.AddParameter("@sublabor", SqlDbType.Text, ParameterDirection.Input, activitySubLabor);
                    qry.AddParameter("@subvehicle", SqlDbType.Text, ParameterDirection.Input, activitySubVehicle);

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "PurchaseOrderTotal", "InvoiceTotal", "PurchaseOrderOpenAmount" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Vendor", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
