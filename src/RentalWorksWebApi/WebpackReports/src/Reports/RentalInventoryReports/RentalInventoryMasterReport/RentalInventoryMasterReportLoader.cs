using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryMasterReport
{
    [FwSqlTable("rentalmasterrptview")]
    public class RentalInventoryMasterReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string MfgSerial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string RentalStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasewarehouse", modeltype: FwDataTypes.Text)]
        public string PurchaseWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyexchangerate", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTaxCurrencyConverted { get; set; }
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
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
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
        [FwSqlDataField(column: "unitvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitvalueextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasevendor", modeltype: FwDataTypes.Text)]
        public string PurchaseVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "closedpicountdate", modeltype: FwDataTypes.Date)]
        public string ClosedPiCountDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpicountdate", modeltype: FwDataTypes.Date)]
        public string LastPiCountDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retirednotes", modeltype: FwDataTypes.Text)]
        public string RetiredNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text)]
        public string OriginalShowId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "originalshow", modeltype: FwDataTypes.Text)]
        public string OriginalShow { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pattern", modeltype: FwDataTypes.Text)]
        public string Pattern { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gender", modeltype: FwDataTypes.Text)]
        public string Gender { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label", modeltype: FwDataTypes.Text)]
        public string Label { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "material", modeltype: FwDataTypes.Text)]
        public string Material { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "period", modeltype: FwDataTypes.Text)]
        public string Period { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cleaningfee", modeltype: FwDataTypes.Decimal)]
        public decimal? CleaningFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobesize", modeltype: FwDataTypes.Text)]
        public string WardrobeSize { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobepiececount", modeltype: FwDataTypes.Integer)]
        public int? WardrobePieceCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string RentalItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasevendorid", modeltype: FwDataTypes.Text)]
        public string PurchaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturerid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text)]
        public string RentalStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
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
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryMasterReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                //--------------------------------------------------------------------------------- 
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
                    select.AddWhereIn("rank", request.Ranks);
                    select.AddWhereIn("ownership", request.OwnershipTypes);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    if (request.FixedAsset.Equals(IncludeExcludeAll.IncludeOnly))
                    {
                        select.AddWhere("fixedasset = 'T'");
                    }
                    else if (request.FixedAsset.Equals(IncludeExcludeAll.Exclude))
                    {
                        select.AddWhere("fixedasset <> 'T'");
                    }
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, barcode, mfgserial");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
                //--------------------------------------------------------------------------------- 

            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Quantity", "ReplacementCostExtended", "UnitValueExtended" };
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
