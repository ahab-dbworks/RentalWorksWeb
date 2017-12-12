using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PoImportance
{
    public class PoImportanceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PoImportanceRecord poImportance = new PoImportanceRecord();
        public PoImportanceLogic()
        {
            dataRecords.Add(poImportance);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoImportanceId { get { return poImportance.PoImportanceId; } set { poImportance.PoImportanceId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoImportance { get { return poImportance.PoImportance; } set { poImportance.PoImportance = value; } }
        public bool? Inactive { get { return poImportance.Inactive; } set { poImportance.Inactive = value; } }
        public string DateStamp { get { return poImportance.DateStamp; } set { poImportance.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
