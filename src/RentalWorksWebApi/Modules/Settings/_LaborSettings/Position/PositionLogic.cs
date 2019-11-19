using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;
using WebLibrary;

namespace WebApi.Modules.Settings.LaborSettings.Position
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
        [FwLogicProperty(Id:"Vm9HgzMRYu3Vm", IsPrimaryKey:true)]
        public string PositionId { get { return master.MasterId; } set { master.MasterId = value; } }

        [FwLogicProperty(Id:"NFdp6gLfofZWw", IsReadOnly:true)]
        public string LaborTypeId { get; set; }

        [FwLogicProperty(Id:"NFdp6gLfofZWw", IsReadOnly:true)]
        public string LaborType { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
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
                    //PositionLogic l2 = new PositionLogic();
                    //l2.SetDependencies(AppConfig, UserSession);
                    //l2.PositionId = PositionId;
                    //bool b = l2.LoadAsync<PositionLogic>().Result;
                    //if (!RateType.Equals(l2.RateType))
                    if (!RateType.Equals(((PositionLogic)original).RateType))
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
