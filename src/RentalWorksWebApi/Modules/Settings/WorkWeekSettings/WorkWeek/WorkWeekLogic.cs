using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WorkWeekSettings.WorkWeek
{
    [FwLogic(Id:"XrtjxvANp9VCI")]
    public class WorkWeekLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WorkWeekRecord workWeek = new WorkWeekRecord();
        WorkWeekLoader workWeekLoader = new WorkWeekLoader();
        public WorkWeekLogic()
        {
            dataRecords.Add(workWeek);
            dataLoader = workWeekLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"Jd3j3NeUb2LKP", IsPrimaryKey:true)]
        public int? WorkWeekId { get { return workWeek.WorkWeekId; } set { workWeek.WorkWeekId = value; } }

        [FwLogicProperty(Id:"Jd3j3NeUb2LKP", IsRecordTitle:true, IsReadOnly:true)]
        public string WorkWeek { get; set; }

        [FwLogicProperty(Id:"H256FCix7TK")]
        public string FromDate { get { return workWeek.FromDate; } set { workWeek.FromDate = value; } }

        [FwLogicProperty(Id:"8BW6V0OlqoD")]
        public string ToDate { get { return workWeek.ToDate; } set { workWeek.ToDate = value; } }

        [FwLogicProperty(Id:"6BDNT6qP46j")]
        public bool? Inactive { get { return workWeek.Inactive; } set { workWeek.Inactive = value; } }

        [FwLogicProperty(Id:"SKZLNKdG9bt")]
        public string DateStamp { get { return workWeek.DateStamp; } set { workWeek.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
