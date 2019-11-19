using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryColor
{
    [FwLogic(Id:"p0SWxIjeGBV3")]
    public class InventoryColorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryColorRecord inventoryColor = new InventoryColorRecord();
        InventoryColorLoader inventoryColorLoader = new InventoryColorLoader();
        public InventoryColorLogic()
        {
            dataRecords.Add(inventoryColor);
            dataLoader = inventoryColorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"WzPynD4Ol3FD", IsPrimaryKey:true)]
        public string InventoryColorId { get { return inventoryColor.InventoryColorId; } set { inventoryColor.InventoryColorId = value; } }

        [FwLogicProperty(Id:"jwc0zJzaXkVq")]
        public string InventoryId { get { return inventoryColor.InventoryId; } set { inventoryColor.InventoryId = value; } }

        [FwLogicProperty(Id:"lZQviXr1W3sy")]
        public string ColorId { get { return inventoryColor.ColorId; } set { inventoryColor.ColorId = value; } }

        [FwLogicProperty(Id:"WzPynD4Ol3FD", IsReadOnly:true)]
        public string Color { get; set; }

        [FwLogicProperty(Id:"vPKx2LyZrbpl")]
        public string DateStamp { get { return inventoryColor.DateStamp; } set { inventoryColor.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
