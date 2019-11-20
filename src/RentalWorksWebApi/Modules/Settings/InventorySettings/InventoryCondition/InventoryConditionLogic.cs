using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventorySettings.InventoryCondition
{
    [FwLogic(Id:"TyGETIqJDgBJ")]
    public class InventoryConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord inventoryCondition = new InventoryConditionRecord();
        InventoryConditionLoader inventoryConditionLoader = new InventoryConditionLoader();
        public InventoryConditionLogic()
        {
            dataRecords.Add(inventoryCondition);
            dataLoader = inventoryConditionLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Rmt0iQ35fsaN", IsPrimaryKey:true)]
        public string InventoryConditionId { get { return inventoryCondition.InventoryConditionId; } set { inventoryCondition.InventoryConditionId = value; } }

        [FwLogicProperty(Id:"Rmt0iQ35fsaN", IsRecordTitle:true)]
        public string InventoryCondition { get { return inventoryCondition.InventoryCondition; } set { inventoryCondition.InventoryCondition = value; } }

        [FwLogicProperty(Id:"4DOamDiuJXE")]
        public bool? Rental { get { return inventoryCondition.Rental; } set { inventoryCondition.Rental = value; } }

        [FwLogicProperty(Id:"Lhdvj6cLnP1")]
        public bool? Sales { get { return inventoryCondition.Sales; } set { inventoryCondition.Sales = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"uaXUk28D67c")]
        public bool? Sets { get { return inventoryCondition.Sets; } set { inventoryCondition.Sets = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"AqsGdBSjEtz")]
        public bool? Props { get { return inventoryCondition.Props; } set { inventoryCondition.Props = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"nHvDUPTthuD")]
        public bool? Wardrobe { get { return inventoryCondition.Wardrobe; } set { inventoryCondition.Wardrobe = value; } }

        [FwLogicProperty(Id:"k2H6W3SAGCh")]
        public bool? Inactive { get { return inventoryCondition.Inactive; } set { inventoryCondition.Inactive = value; } }

        [FwLogicProperty(Id:"UARELdE3oQS")]
        public string DateStamp { get { return inventoryCondition.DateStamp; } set { inventoryCondition.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
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
