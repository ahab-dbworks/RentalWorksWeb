using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebLibrary;

namespace WebApi.Modules.Reports.PurchaseOrderMasterReport
{
    [FwSqlTable("tmpreporttable")]
    public class PurchaseOrderMasterReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string Rowtype { get; set; }
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
                    bool Rental = false;
                    bool Sales = false;
                    bool Parts = false;
                    bool Misc = false;
                    bool Labor = false;
                    bool Repair = false;
                    bool RentalSale = false;

                    foreach (SelectedCheckBoxListItem rt in request.Activities)
                    {
                        if (rt.value.Equals(RwConstants.RECTYPE_RENTAL))
                        {
                            Rental = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_SALE))
                        {
                            Sales = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_PARTS))
                        {
                            Parts = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_MISCELLANEOUS))
                        {
                            Misc = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_LABOR))
                        {
                            Labor = true;
                        }
                        else if (rt.value.Equals("REPAIR")) // add
                        {
                            Repair = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_USED_SALE))
                        {
                            RentalSale = true;
                        }
                    }
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@vendorid", SqlDbType.Text, ParameterDirection.Input, request.VendorId);
                    qry.AddParameter("@postatus", SqlDbType.Text, ParameterDirection.Input, request.Statuses.ToString());

                    qry.AddParameter("@rental", SqlDbType.Text, ParameterDirection.Input, Rental);
                    qry.AddParameter("@sales", SqlDbType.Text, ParameterDirection.Input, Sales);
                    qry.AddParameter("@parts", SqlDbType.Text, ParameterDirection.Input, Parts);
                    qry.AddParameter("@misc", SqlDbType.Text, ParameterDirection.Input, Misc);
                    qry.AddParameter("@labor", SqlDbType.Text, ParameterDirection.Input, Labor);
                    qry.AddParameter("@repair", SqlDbType.Text, ParameterDirection.Input, Repair);
                    qry.AddParameter("@rentalsale", SqlDbType.Text, ParameterDirection.Input, RentalSale);

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
