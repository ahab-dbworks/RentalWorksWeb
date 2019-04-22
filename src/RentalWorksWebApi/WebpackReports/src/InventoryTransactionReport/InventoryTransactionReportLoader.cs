using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Reports.InventoryTransactionReport
{

    public class InventoryTransactionReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public SelectedCheckBoxListItems TransactionTypes { get; set; } = new SelectedCheckBoxListItems();
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
    }

    [FwSqlTable("rptinventorytransaction")]
    public abstract class InventoryTransactionReportLoader : AppDataLoadRecord
    {
        protected string AvailableForFilter = "X";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transtype", modeltype: FwDataTypes.Text)]
        public string TransactionType { get; set; }
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
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
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
        [FwSqlDataField(column: "transdate", modeltype: FwDataTypes.Date)]
        public string TransactionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersname", modeltype: FwDataTypes.Text)]
        public string UsersName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transtypedesc", modeltype: FwDataTypes.Text)]
        public string TransactionTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transnotype", modeltype: FwDataTypes.Text)]
        public string TransactionNumberType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transno", modeltype: FwDataTypes.Text)]
        public string TransactionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost ", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? CostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priceextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PriceExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(InventoryTransactionReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhere("(rectype = '" + AvailableForFilter + "')");
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);

                    addDateFilterToSelect("transdate", request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect("transdate", request.ToDate, select, "<=", "todate");

                    select.AddWhereIn("and", "transtype", request.TransactionTypes.ToString(), false);
                    select.AddOrderBy("whcode, masterno, transdate, orderby");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "Quantity" };
                dt.InsertSubTotalRows("ICode", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
