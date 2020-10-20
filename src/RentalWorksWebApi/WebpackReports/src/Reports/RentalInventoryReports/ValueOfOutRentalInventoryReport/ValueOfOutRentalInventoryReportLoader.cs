using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;


namespace WebApi.Modules.Reports.RentalInventoryReports.ValueOfOutRentalInventoryReport
{
    public class ValueOfOutRentalInventoryReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Boolean)]
        public bool? CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbolandcode", modeltype: FwDataTypes.Text)]
        public string CurrencySymbolAndCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgurl", modeltype: FwDataTypes.Text)]
        public string ManufacturerUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classification", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nondiscountable", modeltype: FwDataTypes.Boolean)]
        public bool? IsNonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Boolean)]
        public bool? IsHazardousMaterial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqty", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyin", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtystaged", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyout", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyincontainer", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyinrepair", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyintransit", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyontruck", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DefaultDepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevaluepct", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSalvageValuePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownershiptypes", modeltype: FwDataTypes.Text)]
        public string OwnershipTypes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvendor", modeltype: FwDataTypes.Text)]
        public string SubVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dateout", modeltype: FwDataTypes.Date)]
        public string DateOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outcontractno", modeltype: FwDataTypes.Text)]
        public string OutContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datein", modeltype: FwDataTypes.Date)]
        public string DateIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontrctno", modeltype: FwDataTypes.Text)]
        public string InContrctNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedrentalstartdate", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedrentalstopdate", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstartdate", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstopdate", modeltype: FwDataTypes.Date)]
        public string BillingStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTaxExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyexchangerate", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCosExtendedtCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconv", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitCostWithTaxExtendedCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencyid", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrency", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencycode", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencysymbol", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseCurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencysymbolandcode", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencySymbolAndCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciation", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Depreciation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bookvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? BookValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalvageValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepodate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousecode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
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
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
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
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Text)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ValueOfOutRentalInventoryReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getvalueofoutinventoryrptweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, request.AsOfDate);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    if (request.FixedAsset.Equals(IncludeExcludeAll.IncludeOnly))
                    {
                        qry.AddParameter("@fixedasset", SqlDbType.Text, ParameterDirection.Input, RwConstants.INCLUDE);
                    }
                    else if (request.FixedAsset.Equals(IncludeExcludeAll.Exclude))
                    {
                        qry.AddParameter("@fixedasset", SqlDbType.Text, ParameterDirection.Input, RwConstants.EXCLUDE);
                    }
                    qry.AddParameter("@includeowned", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_OWNED));
                    qry.AddParameter("@includesubbed", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_SUBBED));
                    qry.AddParameter("@includeconsigned", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_CONSIGNED));
                    qry.AddParameter("@includeleased", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_LEASED));
                    qry.AddParameter("@rank", SqlDbType.Text, ParameterDirection.Input, request.Ranks.ToString());
                    qry.AddParameter("@trackedby", SqlDbType.Text, ParameterDirection.Input, request.TrackedBys.ToString());

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Quantity", "UnitCostWithTaxExtendedCurrencyConverted", "ReplacementCostExtended" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertSubTotalRows("ICode", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 

    }
}
