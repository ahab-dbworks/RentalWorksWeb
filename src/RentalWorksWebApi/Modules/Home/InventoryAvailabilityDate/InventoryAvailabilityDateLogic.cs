using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    [FwLogic(Id:"XyDpODxwxhOm")]
    public class InventoryAvailabilityDateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryAvailabilityDateLoader inventoryAvailabilityDateLoader = new InventoryAvailabilityDateLoader();
        public InventoryAvailabilityDateLogic()
        {
            dataLoader = inventoryAvailabilityDateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"fY7YdB63IfAX")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"tIMRIifQhH1d")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"m5RHDtQvxTCf")]
        public string start { get; set; }

        [FwLogicProperty(Id:"XYae0TTCFehG")]
        public string end { get; set; }

        [FwLogicProperty(Id:"1Yg9RYcsdgby")]
        public string html { get; set; }

        [FwLogicProperty(Id:"tMLqaXxtEHUT")]
        public string backColor { get; set; }

        [FwLogicProperty(Id:"mIVMwsfmZHVl")]
        public string textColor { get; set; }

        [FwLogicProperty(Id:"SjtUQ8nyCYsr")]
        public string id { get; set; } = "";

        [FwLogicProperty(Id:"BXqxgq2VoqjT")]
        public string resource { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
