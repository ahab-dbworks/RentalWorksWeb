using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.SubCategory
{
    public class SubCategoryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SubCategoryRecord subCategory = new SubCategoryRecord();
        SubCategoryLoader subCategoryLoader = new SubCategoryLoader();
        public SubCategoryLogic()
        {
            dataRecords.Add(subCategory);
            dataLoader = subCategoryLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SubCategoryId { get { return subCategory.SubCategoryId; } set { subCategory.SubCategoryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SubCategory { get { return subCategory.SubCategory; } set { subCategory.SubCategory = value; } }
        public string InventoryCategoryId { get { return subCategory.InventoryCategoryId; } set { subCategory.InventoryCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public decimal? OrderBy { get { return subCategory.OrderBy; } set { subCategory.OrderBy = value; } }
        public int? PickListOrderBy { get { return subCategory.PickListOrderBy; } set { subCategory.PickListOrderBy = value; } }
        public string DateStamp { get { return subCategory.DateStamp; } set { subCategory.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
