using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleType
{
    [FwSqlTable("webvehicletypeview")]
    public abstract class VehicleTypeBaseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maintenance", modeltype: FwDataTypes.Boolean)]
        public bool? HasMaintenance { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicletype", modeltype: FwDataTypes.Text)]
        public string InternalVehicleType { get; set; }
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
        public string EquipmentSaleIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string EquipmentSaleIncomeAccountDescription { get; set; }
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
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "depreciationexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string DepreciationExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "depreciationexpenseglno", modeltype: FwDataTypes.Text)]
        public string DepreciationExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "depreciationexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string DepreciationExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accumulateddepreciationexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string AccumulatedDepreciationExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accumulateddepreciationexpenseglno", modeltype: FwDataTypes.Text)]
        public string AccumulatedDepreciationExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accumulateddepreciationexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string AccumulatedDepreciationExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer)]
        public int? PickListOrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
