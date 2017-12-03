using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.InventoryCondition
{
    public class InventoryConditionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord inventoryCondition = new InventoryConditionRecord();
        InventoryConditionLoader inventoryConditionLoader = new InventoryConditionLoader();
        public InventoryConditionLogic()
        {
            dataRecords.Add(inventoryCondition);
            dataLoader = inventoryConditionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryConditionId { get { return inventoryCondition.InventoryConditionId; } set { inventoryCondition.InventoryConditionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryCondition { get { return inventoryCondition.InventoryCondition; } set { inventoryCondition.InventoryCondition = value; } }
        public bool? Rental { get { return inventoryCondition.Rental; } set { inventoryCondition.Rental = value; } }
        public bool? Sales { get { return inventoryCondition.Sales; } set { inventoryCondition.Sales = value; } }
        [JsonIgnore]
        public bool? Sets { get { return inventoryCondition.Sets; } set { inventoryCondition.Sets = value; } }
        [JsonIgnore]
        public bool? Props { get { return inventoryCondition.Props; } set { inventoryCondition.Props = value; } }
        [JsonIgnore]
        public bool? Wardrobe { get { return inventoryCondition.Wardrobe; } set { inventoryCondition.Wardrobe = value; } }
        public bool? Inactive { get { return inventoryCondition.Inactive; } set { inventoryCondition.Inactive = value; } }
        public string DateStamp { get { return inventoryCondition.DateStamp; } set { inventoryCondition.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            bool rental = (Rental.HasValue ? ((bool)Rental) : false);
            bool sales = (Sales.HasValue ? ((bool)Sales) : false);
            if ((!rental) && (!sales))
            {
                Rental = true;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
