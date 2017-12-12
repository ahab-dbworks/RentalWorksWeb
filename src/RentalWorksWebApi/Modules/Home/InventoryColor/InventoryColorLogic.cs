using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryColor
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryColorId { get { return inventoryColor.InventoryColorId; } set { inventoryColor.InventoryColorId = value; } }
        public string InventoryId { get { return inventoryColor.InventoryId; } set { inventoryColor.InventoryId = value; } }
        public string ColorId { get { return inventoryColor.ColorId; } set { inventoryColor.ColorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Color { get; set; }
        public string DateStamp { get { return inventoryColor.DateStamp; } set { inventoryColor.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}