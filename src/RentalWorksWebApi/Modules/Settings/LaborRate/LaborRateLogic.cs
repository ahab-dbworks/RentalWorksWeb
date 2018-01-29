using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;

namespace WebApi.Modules.Settings.LaborRate
{
    public class LaborRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        LaborRateLoader inventoryLoader = new LaborRateLoader();
        public LaborRateLogic()
        {
            dataLoader = inventoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = "L";
        }
        //------------------------------------------------------------------------------------ 
    }
}