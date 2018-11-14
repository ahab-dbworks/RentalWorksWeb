using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryCondition;

namespace WebApi.Modules.Settings.SetCondition
{
    [FwLogic(Id:"Q01xHN3xqyv3T")]
    public class SetConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord setCondition = new InventoryConditionRecord();
        SetConditionLoader setConditionLoader = new SetConditionLoader();
        public SetConditionLogic()
        {
            dataRecords.Add(setCondition);
            dataLoader = setConditionLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"tblTHs4FParh3", IsPrimaryKey:true)]
        public string SetConditionId { get { return setCondition.InventoryConditionId; } set { setCondition.InventoryConditionId = value; } }

        [FwLogicProperty(Id:"tblTHs4FParh3", IsRecordTitle:true)]
        public string SetCondition { get { return setCondition.InventoryCondition; } set { setCondition.InventoryCondition = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"a6G0Pw3v9SHf")]
        public bool? Rental { get { return setCondition.Rental; } set { setCondition.Rental = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"PGz9lgxEbAdH")]
        public bool? Sales { get { return setCondition.Sales; } set { setCondition.Sales = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"9jTKHZcqHJ2D")]
        public bool? Props { get { return setCondition.Props; } set { setCondition.Props = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"Qoi1cvFZioR9")]
        public bool? Sets { get { return setCondition.Sets; } set { setCondition.Sets = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"jqSNWAR6mv3N")]
        public bool? Wardrobe { get { return setCondition.Wardrobe; } set { setCondition.Wardrobe = value; } }

        [FwLogicProperty(Id:"qyur44xOqKPS")]
        public bool? Inactive { get { return setCondition.Inactive; } set { setCondition.Inactive = value; } }

        [FwLogicProperty(Id:"2ZjV5ymW6hoX")]
        public string DateStamp { get { return setCondition.DateStamp; } set { setCondition.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Sets = true;
        }
        //------------------------------------------------------------------------------------
    }

}
