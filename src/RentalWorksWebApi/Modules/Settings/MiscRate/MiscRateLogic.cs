using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;
using RentalWorksWebApi.Modules.Settings.Rate;

namespace RentalWorksWebApi.Modules.Settings.MiscRate
{
    public class MiscRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        MiscRateLoader inventoryLoader = new MiscRateLoader();
        public MiscRateLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------ 

        public override void BeforeSave()
        {
            AvailFor = "M";
        }
        //------------------------------------------------------------------------------------ 
    }
}