using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;

namespace WebApi.Modules.Settings.FacilityRate
{
    public class FacilityRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        FacilityRateLoader inventoryLoader = new FacilityRateLoader();
        public FacilityRateLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }

        public override void BeforeSave()
        {
            AvailFor = "M";
            Classification = "SP";
        }
        //------------------------------------------------------------------------------------ 
    }
}