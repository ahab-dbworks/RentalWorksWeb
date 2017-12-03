﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.InventoryCategory
{
    [FwSqlTable("categoryview")]
    public abstract class InventoryCategoryLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string InventoryCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
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
        [FwSqlDataField(column: "inventoryappreportdesignerid", modeltype: FwDataTypes.Text)]
        public string InventoryBarCodeDesignerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventoryappreportdesigner", modeltype: FwDataTypes.Text)]
        public string InventoryBarCodeDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "appreportdesignerid", modeltype: FwDataTypes.Text)]
        public string BarCodeDesignerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "appreportdesigner", modeltype: FwDataTypes.Text)]
        public string BarCodeDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcodetype", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "pmcycle", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "profitlosscategoryid", modeltype: FwDataTypes.Text)]
        public string ProfitAndLossCategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlosscategory", modeltype: FwDataTypes.Text)]
        public string ProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossismiscexpense", modeltype: FwDataTypes.Boolean)]
        public bool? ProfitAndLossIncludeAsMiscExpense { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetaccountid", modeltype: FwDataTypes.Text)]
        public string AssetAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetglno", modeltype: FwDataTypes.Text)]
        public string AssetAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetglacctdesc", modeltype: FwDataTypes.Text)]
        public string AssetAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeaccountid", modeltype: FwDataTypes.Text)]
        public string IncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglno", modeltype: FwDataTypes.Text)]
        public string IncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string IncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeaccountid", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglno", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeaccountid", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeglno", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeaccountid", modeltype: FwDataTypes.Text)]
        public string EquipmentSaleIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeglno", modeltype: FwDataTypes.Text)]
        public string EquipSaleIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string EquipSaleIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseaccountid", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglno", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseglno", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseglno", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer)]
        public int? PickListOrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
