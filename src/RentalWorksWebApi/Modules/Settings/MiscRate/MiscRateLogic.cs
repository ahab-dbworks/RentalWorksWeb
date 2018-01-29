using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;

namespace WebApi.Modules.Settings.MiscRate
{
    public class MiscRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        MiscRateLoader inventoryLoader = new MiscRateLoader();
        public MiscRateLogic()
        {
            dataLoader = inventoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = "M";
        }
        //------------------------------------------------------------------------------------ 
    }
}