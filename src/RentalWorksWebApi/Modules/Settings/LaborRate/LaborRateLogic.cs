using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;
using RentalWorksWebApi.Modules.Settings.Rate;

namespace RentalWorksWebApi.Modules.Settings.LaborRate
{
    public class LaborRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        LaborRateLoader inventoryLoader = new LaborRateLoader();
        public LaborRateLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            AvailFor = "L";
        }
        //------------------------------------------------------------------------------------ 
    }
}