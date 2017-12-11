using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeCare
{
    public class WardrobeCareLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeCareRecord wardrobeCare = new WardrobeCareRecord();
        public WardrobeCareLogic()
        {
            dataRecords.Add(wardrobeCare);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeCareId { get { return wardrobeCare.WardrobeCareId; } set { wardrobeCare.WardrobeCareId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeCare { get { return wardrobeCare.WardrobeCare; } set { wardrobeCare.WardrobeCare = value; } }
        public bool? Inactive { get { return wardrobeCare.Inactive; } set { wardrobeCare.Inactive = value; } }
        public string DateStamp { get { return wardrobeCare.DateStamp; } set { wardrobeCare.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
