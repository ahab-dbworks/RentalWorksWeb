using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobePeriod
{
    public class WardrobePeriodLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobePeriodRecord wardrobePeriod = new WardrobePeriodRecord();
        public WardrobePeriodLogic()
        {
            dataRecords.Add(wardrobePeriod);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobePeriodId { get { return wardrobePeriod.WardrobePeriodId; } set { wardrobePeriod.WardrobePeriodId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobePeriod { get { return wardrobePeriod.WardrobePeriod; } set { wardrobePeriod.WardrobePeriod = value; } }
        public bool? Inactive { get { return wardrobePeriod.Inactive; } set { wardrobePeriod.Inactive = value; } }
        public string DateStamp { get { return wardrobePeriod.DateStamp; } set { wardrobePeriod.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
