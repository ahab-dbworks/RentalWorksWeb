using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryCondition;

namespace WebApi.Modules.Settings.PropsCondition
{
    public class PropsConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord propsCondition = new InventoryConditionRecord();
        PropsConditionLoader propsConditionLoader = new PropsConditionLoader();
        public PropsConditionLogic()
        {
            dataRecords.Add(propsCondition);
            dataLoader = propsConditionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PropsConditionId { get { return propsCondition.InventoryConditionId; } set { propsCondition.InventoryConditionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PropsCondition { get { return propsCondition.InventoryCondition; } set { propsCondition.InventoryCondition = value; } }
        [JsonIgnore]
        public bool? Rental { get { return propsCondition.Rental; } set { propsCondition.Rental = value; } }
        [JsonIgnore]
        public bool? Sales { get { return propsCondition.Sales; } set { propsCondition.Sales = value; } }
        [JsonIgnore]
        public bool? Sets { get { return propsCondition.Sets; } set { propsCondition.Sets = value; } }
        [JsonIgnore]
        public bool? Props { get { return propsCondition.Props; } set { propsCondition.Props = value; } }
        [JsonIgnore]
        public bool? Wardrobe { get { return propsCondition.Wardrobe; } set { propsCondition.Wardrobe = value; } }
        public bool? Inactive { get { return propsCondition.Inactive; } set { propsCondition.Inactive = value; } }
        public string DateStamp { get { return propsCondition.DateStamp; } set { propsCondition.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            Props = true;
        }
        //------------------------------------------------------------------------------------
    }

}
