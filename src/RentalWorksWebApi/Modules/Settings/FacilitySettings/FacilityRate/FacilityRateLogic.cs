using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.HomeControls.Inventory;
using WebApi.Modules.Settings.Rate;
using WebApi;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityRate
{
    public class FacilityRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        FacilityRateLoader inventoryLoader = new FacilityRateLoader();
        public FacilityRateLogic()
        {
            dataLoader = inventoryLoader;
            ((RateBrowseLoader)browseLoader).AvailFor = RwConstants.RATE_AVAILABLE_FOR_MISC;
            Classification = RwConstants.MISC_CLASSIFICATION_FACILITIES;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"9MjjhNjnWKH", IsPrimaryKey:true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }

        [FwLogicProperty(Id:"v0nuNPXFBv5", IsReadOnly:true)]
        public string FacilityTypeId { get; set; }

        [FwLogicProperty(Id:"v0nuNPXFBv5", IsReadOnly:true)]
        public string FacilityType { get; set; }


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
                    if (!RateType.Equals(((FacilityRateLogic)original).RateType))
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
            Classification = RwConstants.MISC_CLASSIFICATION_FACILITIES;
        }
        //------------------------------------------------------------------------------------ 
    }
}
