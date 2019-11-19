using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobePeriod
{
    [FwLogic(Id:"MZvnCWZyGBxET")]
    public class WardrobePeriodLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobePeriodRecord wardrobePeriod = new WardrobePeriodRecord();
        public WardrobePeriodLogic()
        {
            dataRecords.Add(wardrobePeriod);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"wzo2Q1mJH0IyM", IsPrimaryKey:true)]
        public string WardrobePeriodId { get { return wardrobePeriod.WardrobePeriodId; } set { wardrobePeriod.WardrobePeriodId = value; } }

        [FwLogicProperty(Id:"wzo2Q1mJH0IyM", IsRecordTitle:true)]
        public string WardrobePeriod { get { return wardrobePeriod.WardrobePeriod; } set { wardrobePeriod.WardrobePeriod = value; } }

        [FwLogicProperty(Id:"IkYNdiParLz")]
        public bool? Inactive { get { return wardrobePeriod.Inactive; } set { wardrobePeriod.Inactive = value; } }

        [FwLogicProperty(Id:"S10kZK30fWc")]
        public string DateStamp { get { return wardrobePeriod.DateStamp; } set { wardrobePeriod.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
