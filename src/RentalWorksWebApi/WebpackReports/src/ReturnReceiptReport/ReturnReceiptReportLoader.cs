using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.ReturnReceiptReport
{
    [FwSqlTable("returnreceiptrptwebview")]
    public class ReturnReceiptReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtype", modeltype: FwDataTypes.Text)]
        public string RecordType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtypeorderby", modeltype: FwDataTypes.Integer)]
        public int? RecordTypeOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        public string DivisionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractno", modeltype: FwDataTypes.Text)]
        public string InContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractdate", modeltype: FwDataTypes.Date)]
        public string InContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inqty", modeltype: FwDataTypes.Decimal)]
        public decimal? InQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? AssignedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unassignedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? UnassignedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recorderid", modeltype: FwDataTypes.Text)]
        public string ReconciledOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recorderno", modeltype: FwDataTypes.Text)]
        public string ReconciledOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recorderdate", modeltype: FwDataTypes.Date)]
        public string ReconciledOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reccontractno", modeltype: FwDataTypes.Text)]
        public string ReconciledContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reccontractdate", modeltype: FwDataTypes.Date)]
        public string ReconciledContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recappliedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReconciledAppliedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ReturnReceiptReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    if (request.RecordType.Equals("RETURN_RECEIPT"))
                    {
                        select.AddWhereIn("recordtype", request.RecordType);
                        select.AddWhere("(unassignedqty > 0)");

                    } else if (request.RecordType.Equals("ASSIGNED"))
                    {
                        select.AddWhereIn("recordtype", request.RecordType);
                    }
                   
                    addDateFilterToSelect("incontractdate", request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect("incontractdate", request.ToDate, select, "<=", "todate");
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterno", request.InventoryId);
                    select.AddOrderBy("location,department,deal,masterno,incontractdate desc,masteritemid,recordtypeorderby");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "InQuantity", "ReconciledAppliedQuantity", "UnassignedQuantity" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("OrderItemId", "RowType", totalFields);

                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
