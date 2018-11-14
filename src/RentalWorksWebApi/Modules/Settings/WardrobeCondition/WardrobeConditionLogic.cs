using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryCondition;

namespace WebApi.Modules.Settings.WardrobeCondition
{
    [FwLogic(Id:"4HBV4H0ofi8RU")]
    public class WardrobeConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord wardrobeCondition = new InventoryConditionRecord();
        WardrobeConditionLoader wardrobeConditionLoader = new WardrobeConditionLoader();
        public WardrobeConditionLogic()
        {
            dataRecords.Add(wardrobeCondition);
            dataLoader = wardrobeConditionLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"YGqmx5PetcNXN", IsPrimaryKey:true)]
        public string WardrobeConditionId { get { return wardrobeCondition.InventoryConditionId; } set { wardrobeCondition.InventoryConditionId = value; } }

        [FwLogicProperty(Id:"YGqmx5PetcNXN", IsRecordTitle:true)]
        public string WardrobeCondition { get { return wardrobeCondition.InventoryCondition; } set { wardrobeCondition.InventoryCondition = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"xhZVrbeayM")]
        public bool? Rental { get { return wardrobeCondition.Rental; } set { wardrobeCondition.Rental = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"dnfM3eDbDW")]
        public bool? Sales { get { return wardrobeCondition.Sales; } set { wardrobeCondition.Sales = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"6a4a7W2QJf")]
        public bool? Sets { get { return wardrobeCondition.Sets; } set { wardrobeCondition.Sets = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"3JmJAOqZkM")]
        public bool? Props { get { return wardrobeCondition.Props; } set { wardrobeCondition.Props = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"I2i6MpxLao")]
        public bool? Wardrobe { get { return wardrobeCondition.Wardrobe; } set { wardrobeCondition.Wardrobe = value; } }

        [FwLogicProperty(Id:"pFyX9jhV9A")]
        public bool? Inactive { get { return wardrobeCondition.Inactive; } set { wardrobeCondition.Inactive = value; } }

        [FwLogicProperty(Id:"dMIrxH9jrB")]
        public string DateStamp { get { return wardrobeCondition.DateStamp; } set { wardrobeCondition.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Wardrobe = true;
        }
        //------------------------------------------------------------------------------------
    }

}
