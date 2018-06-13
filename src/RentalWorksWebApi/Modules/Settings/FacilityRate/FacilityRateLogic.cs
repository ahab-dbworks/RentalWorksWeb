using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;
using WebApi.Modules.Settings.Rate;
using WebLibrary;

namespace WebApi.Modules.Settings.FacilityRate
{
    public class FacilityRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        FacilityRateLoader inventoryLoader = new FacilityRateLoader();
        public FacilityRateLogic()
        {
            dataLoader = inventoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }

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
                    FacilityRateLogic l2 = new FacilityRateLogic();
                    l2.SetDependencies(AppConfig, UserSession);
                    l2.RateId = RateId;
                    bool b = l2.LoadAsync<FacilityRateLogic>().Result;
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
            AvailFor = RwConstants.RATE_AVAILABLE_FOR_MISC;
            Classification = "SP";
        }
        //------------------------------------------------------------------------------------ 
    }
}