using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventoryType
{
    public class InventoryTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        InventoryTypeLoader inventoryTypeLoader = new InventoryTypeLoader();
        public InventoryTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }
        public bool? Rental { get { return inventoryType.Rental; } set { inventoryType.Rental = value; } }
        public bool? Sales { get { return inventoryType.Sales; } set { inventoryType.Sales = value; } }
        public bool? Parts { get { return inventoryType.Parts; } set { inventoryType.Parts = value; } }
        public bool? Sets { get { return inventoryType.Sets; } set { inventoryType.Sets = value; } }
        public bool? Props { get { return inventoryType.Props; } set { inventoryType.Props = value; } }
        public bool? Wardrobe { get { return inventoryType.Wardrobe; } set { inventoryType.Wardrobe = value; } }
        public bool? Transportation { get { return inventoryType.Transportation; } set { inventoryType.Transportation = value; } }
        public int? LowAvailabilityPercent { get { return inventoryType.LowAvailabilityPercent; } set { inventoryType.LowAvailabilityPercent = value; } }
        public int? BarCodePrintQty { get { return inventoryType.BarCodePrintQty; } set { inventoryType.BarCodePrintQty = value; } }
        public bool? BarCodePrintUseDesigner { get { return inventoryType.BarCodePrintUseDesigner; } set { inventoryType.BarCodePrintUseDesigner = value; } }
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            bool rental = (Rental.HasValue ? ((bool)Rental) : false);
            bool sales = (Sales.HasValue ? ((bool)Sales) : false);
            bool parts = (Parts.HasValue ? ((bool)Parts) : false);
            bool sets = (Sets.HasValue ? ((bool)Sets) : false);
            bool props = (Props.HasValue ? ((bool)Props) : false);
            bool wardrobe = (Wardrobe.HasValue ? ((bool)Wardrobe) : false);

            if ((!rental) && (!sales) && (!parts) && (!sets) && (!props) && (!wardrobe))
            {
                Rental = true;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
