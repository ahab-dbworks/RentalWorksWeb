using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;
using WebLibrary;

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
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(RateType))
                {
                    RateType = RwConstants.RATE_TYPE_SINGLE;
                }
            }
            else
            {
                if (RateType != null)
                {
                    LaborRateLogic l2 = new LaborRateLogic();
                    l2.SetDependencies(AppConfig, UserSession);
                    l2.RateId = RateId;
                    bool b = l2.LoadAsync<LaborRateLogic>().Result;
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
        }
        //------------------------------------------------------------------------------------ 
    }
}