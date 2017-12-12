using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryCondition;

namespace WebApi.Modules.Settings.WardrobeCondition
{
    public class WardrobeConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord wardrobeCondition = new InventoryConditionRecord();
        WardrobeConditionLoader wardrobeConditionLoader = new WardrobeConditionLoader();
        public WardrobeConditionLogic()
        {
            dataRecords.Add(wardrobeCondition);
            dataLoader = wardrobeConditionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeConditionId { get { return wardrobeCondition.InventoryConditionId; } set { wardrobeCondition.InventoryConditionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeCondition { get { return wardrobeCondition.InventoryCondition; } set { wardrobeCondition.InventoryCondition = value; } }
        [JsonIgnore]
        public bool? Rental { get { return wardrobeCondition.Rental; } set { wardrobeCondition.Rental = value; } }
        [JsonIgnore]
        public bool? Sales { get { return wardrobeCondition.Sales; } set { wardrobeCondition.Sales = value; } }
        [JsonIgnore]
        public bool? Sets { get { return wardrobeCondition.Sets; } set { wardrobeCondition.Sets = value; } }
        [JsonIgnore]
        public bool? Props { get { return wardrobeCondition.Props; } set { wardrobeCondition.Props = value; } }
        [JsonIgnore]
        public bool? Wardrobe { get { return wardrobeCondition.Wardrobe; } set { wardrobeCondition.Wardrobe = value; } }
        public bool? Inactive { get { return wardrobeCondition.Inactive; } set { wardrobeCondition.Inactive = value; } }
        public string DateStamp { get { return wardrobeCondition.DateStamp; } set { wardrobeCondition.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            Wardrobe = true;
        }
        //------------------------------------------------------------------------------------
    }

}
