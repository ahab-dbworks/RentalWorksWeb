using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventoryType
{
    [FwLogic(Id: "RrpruyLPMflbe")]
    public class SubCategoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        InventoryTypeLoader inventoryTypeLoader = new InventoryTypeLoader();
        public SubCategoryLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "ygagqgx7Go53K", IsPrimaryKey:true)]
        public string SubCategoryId { get { return inventoryType.SubCategoryId; } set { inventoryType.SubCategoryId = value; } }

        [FwLogicProperty(Id: "4rMzNcnhIgrnN", IsRecordTitle:true)]
        public string SubCategory { get { return inventoryType.SubCategory; } set { inventoryType.SubCategory = value; } }

        [FwLogicProperty(Id: "cFNcVZ00C1sGi")]
        public string CategoryId { get { return inventoryType.CategoryId; } set { inventoryType.CategoryId = value; } }

        [FwLogicProperty(Id: "OOV6MkB1i3GnC", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id: "pDMm3mozBS8qu", IsReadOnly:true)]
        public string TypeId { get; set; }

        [FwLogicProperty(Id: "FcciY3c8kOzdS", IsReadOnly:true)]
        public string Type { get; set; }

        [FwLogicProperty(Id: "H83Q59cxq9uDY")]
        public decimal? OrderBy { get { return inventoryType.OrderBy; } set { inventoryType.OrderBy = value; } }

        [FwLogicProperty(Id: "GjgWFwSPu5acv")]
        public int? PickListOrderBy { get { return inventoryType.PickListOrderBy; } set { inventoryType.PickListOrderBy = value; } }

        [FwLogicProperty(Id: "LGp2npeGsIuSD")]
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
