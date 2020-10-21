using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.FixedAssetBookValue
{
    [FwSqlTable("dbo.funcfixedassetbookvaluerptweb(@asofdate)")]
    public class FixedAssetBookValueReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
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
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
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
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "received", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseqty", modeltype: FwDataTypes.Integer)]
        public int? PurchaseQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? CostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? CostWithTaxExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconvextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? CostCurrencyConvertedExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTaxCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconvextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? CostWithTaxCurrencyConvertedExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencyid", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencycode", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrency", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencysymbol", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencysymbolandcode", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencySymbolAndCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyexchangerate", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrencyid", modeltype: FwDataTypes.Text)]
        public string WarehouseCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrencycode", modeltype: FwDataTypes.Text)]
        public string WarehouseCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrency", modeltype: FwDataTypes.Text)]
        public string WarehouseCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrencysymbol", modeltype: FwDataTypes.Text)]
        public string WarehouseCurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrencysymbolandcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCurrencySymbolAndCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "asofmonthbegin", modeltype: FwDataTypes.Date)]
        public string AsOfMonthBeginDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "asofmonthend", modeltype: FwDataTypes.Date)]
        public string AsOfMonthEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "asofmonthdepreciation", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AsOfMonthDepreciation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldepreciation", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDepreciation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bookvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? BookValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalvageValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevalueextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalvageValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(FixedAssetBookValueRequest request)
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
                    select.AddParameter("@asofdate", request.AsOfDate);
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("rank", request.Ranks);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, barcode");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "PurchaseQuantity", "CostWithTaxCurrencyConvertedExtended", "AsOfMonthDepreciation", "TotalDepreciation", "BookValue" };
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
