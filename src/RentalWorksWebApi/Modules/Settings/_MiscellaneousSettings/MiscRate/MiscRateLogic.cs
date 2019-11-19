using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.HomeControls.Inventory;
using WebApi.Modules.Settings.Rate;
using WebLibrary;

namespace WebApi.Modules.Settings.MiscellaneousSettings.MiscRate
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
        [FwLogicProperty(Id:"362a7iGn1v2N", IsPrimaryKey:true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }

        [FwLogicProperty(Id:"SxwWlK63QDdQ", IsReadOnly:true)]
        public string MiscTypeId { get; set; }

        [FwLogicProperty(Id:"SxwWlK63QDdQ", IsReadOnly:true)]
        public string MiscType { get; set; }

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
                    //MiscRateLogic l2 = new MiscRateLogic();
                    //l2.SetDependencies(AppConfig, UserSession);
                    //l2.RateId = RateId;
                    //bool b = l2.LoadAsync<MiscRateLogic>().Result;
                    //if (!RateType.Equals(l2.RateType))
                    if (!RateType.Equals(((MiscRateLogic)original).RateType))
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
            AvailFor = RwConstants.RATE_AVAILABLE_FOR_MISC;
        }
        //------------------------------------------------------------------------------------ 
    }
}
