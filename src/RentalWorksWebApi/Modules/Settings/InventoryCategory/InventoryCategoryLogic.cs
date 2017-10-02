using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.InventoryCategory
{
    public abstract class InventoryCategoryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        protected InventoryCategoryRecord inventoryCategory = new InventoryCategoryRecord();
        public InventoryCategoryLogic()
        {
            dataRecords.Add(inventoryCategory);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryCategoryId { get { return inventoryCategory.InventoryCategoryId; } set { inventoryCategory.InventoryCategoryId = value; } }
        public string InventoryTypeId { get { return inventoryCategory.InventoryTypeId; } set { inventoryCategory.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryCategory { get { return inventoryCategory.InventoryCategory; } set { inventoryCategory.InventoryCategory = value; } }
        [JsonIgnore]
        public string RecType { get { return inventoryCategory.RecType; } set { inventoryCategory.RecType = value; } }
        public bool WarehouseCategory { get { return inventoryCategory.WarehouseCategory; } set { inventoryCategory.WarehouseCategory = value; } }
        public bool CatalogCategory { get { return inventoryCategory.CatalogCategory; } set { inventoryCategory.CatalogCategory = value; } }
        public bool OverrideProfitAndLossCategory { get { return inventoryCategory.OverrideProfitAndLossCategory; } set { inventoryCategory.OverrideProfitAndLossCategory = value; } }
        public string ProfitAndLossCategoryId { get { return inventoryCategory.ProfitAndLossCategoryId; } set { inventoryCategory.ProfitAndLossCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitAndLossCategory { get; set; }
        public bool ProfitAndLossIncludeAsMiscExpense { get { return inventoryCategory.ProfitAndLossIncludeAsMiscExpense; } set { inventoryCategory.ProfitAndLossIncludeAsMiscExpense = value; } }
        public string AssetAccountId { get { return inventoryCategory.AssetAccountId; } set { inventoryCategory.AssetAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssetAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssetAccountDescription { get; set; }
        public string IncomeAccountId { get { return inventoryCategory.IncomeAccountId; } set { inventoryCategory.IncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string IncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string IncomeAccountDescription { get; set; }
        public string SubIncomeAccountId { get { return inventoryCategory.SubIncomeAccountId; } set { inventoryCategory.SubIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubIncomeAccountDescription { get; set; }
        public string LdIncomeAccountId { get { return inventoryCategory.LdIncomeAccountId; } set { inventoryCategory.LdIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LdIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LdIncomeAccountDescription { get; set; }
        public string EquipmentSaleIncomeAccountId { get { return inventoryCategory.EquipmentSaleIncomeAccountId; } set { inventoryCategory.EquipmentSaleIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EquipSaleIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EquipSaleIncomeAccountDescription { get; set; }
        public string ExpenseAccountId { get { return inventoryCategory.ExpenseAccountId; } set { inventoryCategory.ExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ExpenseAccountDescription { get; set; }
        public string CostOfGoodsSoldExpenseAccountId { get { return inventoryCategory.CostOfGoodsSoldExpenseAccountId; } set { inventoryCategory.CostOfGoodsSoldExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }
        public string CostOfGoodsRentedExpenseAccountId { get { return inventoryCategory.CostOfGoodsRentedExpenseAccountId; } set { inventoryCategory.CostOfGoodsRentedExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }
        public decimal? OrderBy { get { return inventoryCategory.OrderBy; } set { inventoryCategory.OrderBy = value; } }
        public int? PickListOrderBy { get { return inventoryCategory.PickListOrderBy; } set { inventoryCategory.PickListOrderBy = value; } }
        public bool Inactive { get { return inventoryCategory.Inactive; } set { inventoryCategory.Inactive = value; } }
        public string DateStamp { get { return inventoryCategory.DateStamp; } set { inventoryCategory.DateStamp = value; } }
    }

}
