using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SubCategory
{
    [FwLogic(Id:"XA2RJVdpODksi")]
    public class SubCategoryLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"13HSb48Va61uV", IsPrimaryKey:true)]
        public string SubCategoryId { get { return subCategory.SubCategoryId; } set { subCategory.SubCategoryId = value; } }

        [FwLogicProperty(Id:"13HSb48Va61uV", IsRecordTitle:true)]
        public string SubCategory { get { return subCategory.SubCategory; } set { subCategory.SubCategory = value; } }

        [FwLogicProperty(Id:"NPedY76j4VgP")]
        public string CategoryId { get { return subCategory.CategoryId; } set { subCategory.CategoryId = value; } }

        [FwLogicProperty(Id:"13HSb48Va61uV", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"bxfwXs2fwwpZI", IsReadOnly:true)]
        public string TypeId { get; set; }

        [FwLogicProperty(Id:"bxfwXs2fwwpZI", IsReadOnly:true)]
        public string Type { get; set; }

        [FwLogicProperty(Id:"IPRuNdoxLXnb")]
        public decimal? OrderBy { get { return subCategory.OrderBy; } set { subCategory.OrderBy = value; } }

        [FwLogicProperty(Id:"YDax09l8YvHo")]
        public int? PickListOrderBy { get { return subCategory.PickListOrderBy; } set { subCategory.PickListOrderBy = value; } }

        [FwLogicProperty(Id:"nwVtlMfd7Pse")]
        public string DateStamp { get { return subCategory.DateStamp; } set { subCategory.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
