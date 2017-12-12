using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryCondition;

namespace WebApi.Modules.Settings.SetCondition
{
    public class SetConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord setCondition = new InventoryConditionRecord();
        SetConditionLoader setConditionLoader = new SetConditionLoader();
        public SetConditionLogic()
        {
            dataRecords.Add(setCondition);
            dataLoader = setConditionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SetConditionId { get { return setCondition.InventoryConditionId; } set { setCondition.InventoryConditionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SetCondition { get { return setCondition.InventoryCondition; } set { setCondition.InventoryCondition = value; } }
        [JsonIgnore]
        public bool? Rental { get { return setCondition.Rental; } set { setCondition.Rental = value; } }
        [JsonIgnore]
        public bool? Sales { get { return setCondition.Sales; } set { setCondition.Sales = value; } }
        [JsonIgnore]
        public bool? Props { get { return setCondition.Props; } set { setCondition.Props = value; } }
        [JsonIgnore]
        public bool? Sets { get { return setCondition.Sets; } set { setCondition.Sets = value; } }
        [JsonIgnore]
        public bool? Wardrobe { get { return setCondition.Wardrobe; } set { setCondition.Wardrobe = value; } }
        public bool? Inactive { get { return setCondition.Inactive; } set { setCondition.Inactive = value; } }
        public string DateStamp { get { return setCondition.DateStamp; } set { setCondition.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            Sets = true;
        }
        //------------------------------------------------------------------------------------
    }

}
