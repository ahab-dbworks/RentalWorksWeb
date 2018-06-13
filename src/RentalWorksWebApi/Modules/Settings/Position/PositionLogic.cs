using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;
using WebLibrary;

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
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                RateType = RwConstants.RATE_TYPE_RECURRING;
            }
            else
            {
                if (RateType != null)
                {
                    PositionLogic l2 = new PositionLogic();
                    l2.SetDependencies(AppConfig, UserSession);
                    l2.PositionId = PositionId;
                    bool b = l2.LoadAsync<PositionLogic>().Result;
                    if (!RateType.Equals(l2.RateType))
                    {
                        isValid = false;
                        validateMsg = "Cannot change Rate Type.";
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = RwConstants.RATE_AVAILABLE_FOR_LABOR;
            Classification = "LP";
        }
        //------------------------------------------------------------------------------------ 
    }
}