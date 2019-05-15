using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryMasterReport
{
    [FwSqlTable("rentalmasterrptview")]
    public class RentalInventoryMasterReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryMasterReportRequest request)
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
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("and", "rank", request.Ranks.ToString(), false);
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, barcode, mfgserial");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
                //--------------------------------------------------------------------------------- 

            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "QuantityIn", "QuantityAllocated", "QuantityInTransit", "QuantityOnPO", "QuantityInAverageCost", "QuantityInDefaultCost", "QuantityInPrice", "Revenue" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
