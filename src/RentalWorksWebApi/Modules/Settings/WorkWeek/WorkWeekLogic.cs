using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WorkWeek
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WorkWeekId { get { return workWeek.WorkWeekId; } set { workWeek.WorkWeekId = value; } }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string WorkWeek { get; set; }
        public string FromDate { get { return workWeek.FromDate; } set { workWeek.FromDate = value; } }
        public string ToDate { get { return workWeek.ToDate; } set { workWeek.ToDate = value; } }
        public bool? Inactive { get { return workWeek.Inactive; } set { workWeek.Inactive = value; } }
        public string DateStamp { get { return workWeek.DateStamp; } set { workWeek.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
