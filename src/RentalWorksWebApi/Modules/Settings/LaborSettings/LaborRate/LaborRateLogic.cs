using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.HomeControls.Inventory;
using WebApi.Modules.Settings.Rate;
using WebApi;

namespace WebApi.Modules.Settings.LaborSettings.LaborRate
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
        [FwLogicProperty(Id:"H39qX5KfG1Y7", IsPrimaryKey:true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"2kExLTksQ7V4", IsReadOnly:true)]
        public string LaborTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"2kExLTksQ7V4", IsReadOnly:true)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
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
                    //LaborRateLogic l2 = new LaborRateLogic();
                    //l2.SetDependencies(AppConfig, UserSession);
                    //l2.RateId = RateId;
                    //bool b = l2.LoadAsync<LaborRateLogic>().Result;
                    //if (!RateType.Equals(l2.RateType))
                    if (!RateType.Equals(((LaborRateLogic)original).RateType))
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
