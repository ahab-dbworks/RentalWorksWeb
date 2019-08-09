using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Category
{
    [FwSqlTable("category")]
    public class CategoryRecord : AppDataReadWriteRecord
    {
        /*
        TODO:
transworksvehicle

             */
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string TypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text, maxlength: 25, required: true)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, maxlength: 3)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicletype", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string VehicleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventory", modeltype: FwDataTypes.Boolean)]
        public bool? WarehouseCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "catalog", modeltype: FwDataTypes.Boolean)]
        public bool? CatalogCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "qcsubrent", modeltype: FwDataTypes.Boolean)]
        public bool? SubsRequireQc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "substituteall", modeltype: FwDataTypes.Boolean)]
        public bool? AllCategoryItemsAreSubstitutes { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean)]
        public bool? BarCodePrintUseDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventoryappreportdesignerid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string InventoryBarCodeDesignerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "appreportdesignerid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string BarCodeDesignerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcodetype", modeltype: FwDataTypes.Text, maxlength: 1)]
        public string BarCodeType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "autodiscount100", modeltype: FwDataTypes.Boolean)]
        public bool? DiscountCategoryItems100PercentByDefault { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donotinvoice", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeCategoryItemsFromInvoicing { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "schedule", modeltype: FwDataTypes.Boolean)]
        public bool? ScheduleItems { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maintenance", modeltype: FwDataTypes.Boolean)]
        public bool? HasMaintenance { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmcycle", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string PreventiveMaintenanceCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmcycleperiod", modeltype: FwDataTypes.Integer)]
        public int? PreventiveMaintenanceCyclePeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "overrideprofitlosscategory", modeltype: FwDataTypes.Boolean)]
        public bool? OverrideProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlosscategoryid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string ProfitAndLossCategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossismiscexpense", modeltype: FwDataTypes.Boolean)]
        public bool? ProfitAndLossIncludeAsMiscExpense { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string AssetAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string IncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string SubIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string LdIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string EquipmentSaleIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string ExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CostOfGoodsSoldExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CostOfGoodsRentedExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer)]
        public int? PickListOrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dotperiod", modeltype: FwDataTypes.Integer)]
        public int? DotPeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclassid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string LicenseClassId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "regulated", modeltype: FwDataTypes.Boolean)]
        public bool? Regulated { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
            addFilterToSelect("RecType", "rectype", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}