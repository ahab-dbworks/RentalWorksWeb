using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryQCRequiredReport
{
    [FwSqlTable("tmpreporttable")]
    public class RentalInventoryQCRequiredReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string RentalitemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfordisp", modeltype: FwDataTypes.Text)]
        public string AvailableForDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Class { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string Mfgserial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WHCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentorderby", modeltype: FwDataTypes.Integer)]
        public int? Inventorydepartmentorderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Integer)]
        public int? CategoryOrderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Integer)]
        public int? SubCategoryOrderBy { get; set; }
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
        [FwSqlDataField(column: "lastdealid", modeltype: FwDataTypes.Text)]
        public string LastdealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastdeal", modeltype: FwDataTypes.Text)]
        public string Lastdeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text)]
        public string RentalstatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string RentalStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequiredasof", modeltype: FwDataTypes.Date)]
        public string Qcrequiredasof { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        public bool? Noavail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availenableqcdelay", modeltype: FwDataTypes.Boolean)]
        public bool? Availenableqcdelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeweekend", modeltype: FwDataTypes.Boolean)]
        public bool? Availqcdelayexcludeweekend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeholiday", modeltype: FwDataTypes.Boolean)]
        public bool? Availqcdelayexcludeholiday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayindefinite", modeltype: FwDataTypes.Boolean)]
        public bool? Availqcdelayindefinite { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelay", modeltype: FwDataTypes.Integer)]
        public int? Availqcdelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
        public bool? AvailByHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean)]
        public bool? AvailByDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean)]
        public bool? AvailByAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoid", modeltype: FwDataTypes.Text)]
        public string PurchasePOId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepono", modeltype: FwDataTypes.Text)]
        public string PurchasePONumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepodesc", modeltype: FwDataTypes.Text)]
        public string PurchasePODescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string Purchasedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivdate", modeltype: FwDataTypes.Date)]
        public string Receivdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractid", modeltype: FwDataTypes.Text)]
        public string ReceiveContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractno", modeltype: FwDataTypes.Text)]
        public string ReceiveContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryQCRequiredReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                //--------------------------------------------------------------------------------- 
                // below uses a "select" query to populate the FwJsonDataTable 
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddOrderBy("warehouse, inventorydepartmentorderby, categoryorderby, subcategoryorderby, masterno, barcode, mfgserial");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.InsertSubTotalRows("InventoryType", "RowType", null);
                dt.InsertTotalRow("RowType", "detail", null, null);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
