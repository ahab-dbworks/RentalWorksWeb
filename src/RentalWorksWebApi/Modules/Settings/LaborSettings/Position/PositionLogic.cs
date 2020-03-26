using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Rate;

namespace WebApi.Modules.Settings.LaborSettings.Position
{
    public class PositionLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        PositionLoader positionLoader = new PositionLoader();
        public PositionLogic()
        {
            dataLoader = positionLoader;
            ((RateBrowseLoader)browseLoader).AvailFor = RwConstants.RATE_AVAILABLE_FOR_LABOR;
            BeforeSave += OnBeforeSave;
            AvailFor = RwConstants.RATE_AVAILABLE_FOR_LABOR;
            Classification = RwConstants.LABOR_CLASSIFICATION_POSITION;
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
            Classification = RwConstants.LABOR_CLASSIFICATION_POSITION;
        }
        //------------------------------------------------------------------------------------ 
    }
}
