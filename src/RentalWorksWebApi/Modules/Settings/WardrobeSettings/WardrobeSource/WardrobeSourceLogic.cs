using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeSource
{
    [FwLogic(Id:"jCgJHslAe9pvn")]
    public class WardrobeSourceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeSourceRecord wardrobeSource = new WardrobeSourceRecord();
        public WardrobeSourceLogic()
        {
            dataRecords.Add(wardrobeSource);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"fN7pxUm8uAzCT", IsPrimaryKey:true)]
        public string WardrobeSourceId { get { return wardrobeSource.WardrobeSourceId; } set { wardrobeSource.WardrobeSourceId = value; } }

        [FwLogicProperty(Id:"fN7pxUm8uAzCT", IsRecordTitle:true)]
        public string WardrobeSource { get { return wardrobeSource.WardrobeSource; } set { wardrobeSource.WardrobeSource = value; } }

        [FwLogicProperty(Id:"Js5bBpPd3q1")]
        public bool? Inactive { get { return wardrobeSource.Inactive; } set { wardrobeSource.Inactive = value; } }

        [FwLogicProperty(Id:"uXyNgcQs0es")]
        public string DateStamp { get { return wardrobeSource.DateStamp; } set { wardrobeSource.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
