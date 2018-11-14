using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeCare
{
    [FwLogic(Id:"SHL1Wy6SHOdKm")]
    public class WardrobeCareLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeCareRecord wardrobeCare = new WardrobeCareRecord();
        public WardrobeCareLogic()
        {
            dataRecords.Add(wardrobeCare);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"9zMkLHHst9qd8", IsPrimaryKey:true)]
        public string WardrobeCareId { get { return wardrobeCare.WardrobeCareId; } set { wardrobeCare.WardrobeCareId = value; } }

        [FwLogicProperty(Id:"9zMkLHHst9qd8", IsRecordTitle:true)]
        public string WardrobeCare { get { return wardrobeCare.WardrobeCare; } set { wardrobeCare.WardrobeCare = value; } }

        [FwLogicProperty(Id:"yEoVCFi5GWI2")]
        public bool? Inactive { get { return wardrobeCare.Inactive; } set { wardrobeCare.Inactive = value; } }

        [FwLogicProperty(Id:"IM47YqXbroPF")]
        public string DateStamp { get { return wardrobeCare.DateStamp; } set { wardrobeCare.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
