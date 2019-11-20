using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.InventorySettings.InventoryCondition;

namespace WebApi.Modules.Settings.PropsSettings.PropsCondition
{
    [FwLogic(Id:"5XfXG4pMV2H43")]
    public class PropsConditionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryConditionRecord propsCondition = new InventoryConditionRecord();
        PropsConditionLoader propsConditionLoader = new PropsConditionLoader();
        public PropsConditionLogic()
        {
            dataRecords.Add(propsCondition);
            dataLoader = propsConditionLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"DNGxiOkDjNOnP", IsPrimaryKey:true)]
        public string PropsConditionId { get { return propsCondition.InventoryConditionId; } set { propsCondition.InventoryConditionId = value; } }

        [FwLogicProperty(Id:"DNGxiOkDjNOnP", IsRecordTitle:true)]
        public string PropsCondition { get { return propsCondition.InventoryCondition; } set { propsCondition.InventoryCondition = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"Q2qxQDIeJHlT")]
        public bool? Rental { get { return propsCondition.Rental; } set { propsCondition.Rental = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"3iXmbtazTftQ")]
        public bool? Sales { get { return propsCondition.Sales; } set { propsCondition.Sales = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"QDfY1AVlqeZN")]
        public bool? Sets { get { return propsCondition.Sets; } set { propsCondition.Sets = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"XdTe780aWCDJ")]
        public bool? Props { get { return propsCondition.Props; } set { propsCondition.Props = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"qhHSRjm4WH1X")]
        public bool? Wardrobe { get { return propsCondition.Wardrobe; } set { propsCondition.Wardrobe = value; } }

        [FwLogicProperty(Id:"KjR6yFR26bfm")]
        public bool? Inactive { get { return propsCondition.Inactive; } set { propsCondition.Inactive = value; } }

        [FwLogicProperty(Id:"9g7uc2ZXX4SG")]
        public string DateStamp { get { return propsCondition.DateStamp; } set { propsCondition.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Props = true;
        }
        //------------------------------------------------------------------------------------
    }

}
