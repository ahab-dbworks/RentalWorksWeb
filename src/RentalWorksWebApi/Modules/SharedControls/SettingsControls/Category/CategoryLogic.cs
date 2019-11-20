using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Category
{
    [FwLogic(Id:"VltrfepVI69c")]
    public /*abstract*/ class CategoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        protected CategoryRecord inventoryCategory = new CategoryRecord();
        public CategoryLogic() : base()
        {
            dataRecords.Add(inventoryCategory);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"nCPLXQbA735Q", IsPrimaryKey:true)]
        public string CategoryId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; } }

        [FwLogicProperty(Id:"KuD1MDbIIln0", IsRecordTitle:true)]
        public string Category { get { return inventoryCategory.Category; } set { inventoryCategory.Category = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"0hPXFnrqtQM7")]
        public string RecType { get { return inventoryCategory.RecType; } set { inventoryCategory.RecType = value; } }

        [FwLogicProperty(Id:"w1aupvazgp59")]
        public bool? WarehouseCategory { get { return inventoryCategory.WarehouseCategory; } set { inventoryCategory.WarehouseCategory = value; } }

        [FwLogicProperty(Id:"Phdw1TFYvhpF")]
        public bool? CatalogCategory { get { return inventoryCategory.CatalogCategory; } set { inventoryCategory.CatalogCategory = value; } }

        [FwLogicProperty(Id:"o46Q4mVbpCuQ")]
        public bool? OverrideProfitAndLossCategory { get { return inventoryCategory.OverrideProfitAndLossCategory; } set { inventoryCategory.OverrideProfitAndLossCategory = value; } }

        [FwLogicProperty(Id:"ViXUEiaYCOd5")]
        public string ProfitAndLossCategoryId { get { return inventoryCategory.ProfitAndLossCategoryId; } set { inventoryCategory.ProfitAndLossCategoryId = value; } }

        [FwLogicProperty(Id:"roq9Paoh2WAd", IsReadOnly:true)]
        public string ProfitAndLossCategory { get; set; }

        [FwLogicProperty(Id:"usUFhXUikpBa")]
        public bool? ProfitAndLossIncludeAsMiscExpense { get { return inventoryCategory.ProfitAndLossIncludeAsMiscExpense; } set { inventoryCategory.ProfitAndLossIncludeAsMiscExpense = value; } }

        [FwLogicProperty(Id:"Wi6tWLhIjJic")]
        public string AssetAccountId { get { return inventoryCategory.AssetAccountId; } set { inventoryCategory.AssetAccountId = value; } }

        [FwLogicProperty(Id:"BpRmM9A2t9Wm", IsReadOnly:true)]
        public string AssetAccountNo { get; set; }

        [FwLogicProperty(Id:"ix7aeCWZO69P", IsReadOnly:true)]
        public string AssetAccountDescription { get; set; }

        [FwLogicProperty(Id:"scGef7hFOTP0")]
        public string IncomeAccountId { get { return inventoryCategory.IncomeAccountId; } set { inventoryCategory.IncomeAccountId = value; } }

        [FwLogicProperty(Id:"9ocBFRGeZm8w", IsReadOnly:true)]
        public string IncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"oamoYXMsBxiD", IsReadOnly:true)]
        public string IncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"O82UCoLVamJM")]
        public string SubIncomeAccountId { get { return inventoryCategory.SubIncomeAccountId; } set { inventoryCategory.SubIncomeAccountId = value; } }

        [FwLogicProperty(Id:"32wQK7xz0QcI", IsReadOnly:true)]
        public string SubIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"GTqq9Mx0ZLII", IsReadOnly:true)]
        public string SubIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"iG9770Kspihw")]
        public string LdIncomeAccountId { get { return inventoryCategory.LdIncomeAccountId; } set { inventoryCategory.LdIncomeAccountId = value; } }

        [FwLogicProperty(Id:"GaFOeCL6r9Rw", IsReadOnly:true)]
        public string LdIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"tjzziFaFOrPt", IsReadOnly:true)]
        public string LdIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"1ZPCRmPuFMqy")]
        public string EquipmentSaleIncomeAccountId { get { return inventoryCategory.EquipmentSaleIncomeAccountId; } set { inventoryCategory.EquipmentSaleIncomeAccountId = value; } }

        [FwLogicProperty(Id:"1hBff4hBDeQL", IsReadOnly:true)]
        public string EquipmentSaleIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"aZt9kmG3Esxv", IsReadOnly:true)]
        public string EquipmentSaleIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"r3Nckf57YR6E")]
        public string ExpenseAccountId { get { return inventoryCategory.ExpenseAccountId; } set { inventoryCategory.ExpenseAccountId = value; } }

        [FwLogicProperty(Id:"ZInLUEqQTNe6", IsReadOnly:true)]
        public string ExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"3lC6kyQQGNBJ", IsReadOnly:true)]
        public string ExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"pvCcseQAVsli")]
        public string CostOfGoodsSoldExpenseAccountId { get { return inventoryCategory.CostOfGoodsSoldExpenseAccountId; } set { inventoryCategory.CostOfGoodsSoldExpenseAccountId = value; } }

        [FwLogicProperty(Id:"ugrLsBAoVqGR", IsReadOnly:true)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"8K6j3PbKMbS8", IsReadOnly:true)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"Wiq4Bx4grkyN")]
        public string CostOfGoodsRentedExpenseAccountId { get { return inventoryCategory.CostOfGoodsRentedExpenseAccountId; } set { inventoryCategory.CostOfGoodsRentedExpenseAccountId = value; } }

        [FwLogicProperty(Id:"MY8nvI6S1J3A", IsReadOnly:true)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"4rCiqlISVIq6", IsReadOnly:true)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"7722wgdcEViY")]
        public decimal? OrderBy { get { return inventoryCategory.OrderBy; } set { inventoryCategory.OrderBy = value; } }

        [FwLogicProperty(Id:"tSthmyiD40bq")]
        public int? PickListOrderBy { get { return inventoryCategory.PickListOrderBy; } set { inventoryCategory.PickListOrderBy = value; } }

        [FwLogicProperty(Id:"ty0QlkZhFI0F", IsReadOnly:true)]
        public int? SubCategoryCount { get; set; }

        [FwLogicProperty(Id:"gSwcQCCRAgFA")]
        public bool? Inactive { get { return inventoryCategory.Inactive; } set { inventoryCategory.Inactive = value; } }

        [FwLogicProperty(Id:"UEEwMRRlFexR")]
        public string DateStamp { get { return inventoryCategory.DateStamp; } set { inventoryCategory.DateStamp = value; } }

    }

}
