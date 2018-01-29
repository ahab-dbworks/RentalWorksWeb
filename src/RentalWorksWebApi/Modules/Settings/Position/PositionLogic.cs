using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;

namespace WebApi.Modules.Settings.Position
{
    public class PositionLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        PositionLoader positionLoader = new PositionLoader();
        public PositionLogic()
        {
            dataLoader = positionLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PositionId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = "L";
            Classification = "LP";
        }
        //------------------------------------------------------------------------------------ 
    }
}