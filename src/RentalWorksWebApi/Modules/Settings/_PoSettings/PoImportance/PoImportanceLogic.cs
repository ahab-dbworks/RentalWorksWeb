using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PoImportance
{
    [FwLogic(Id:"OTcwYvs2jOTb")]
    public class PoImportanceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PoImportanceRecord poImportance = new PoImportanceRecord();
        public PoImportanceLogic()
        {
            dataRecords.Add(poImportance);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"2JPNi1eiLGqm", IsPrimaryKey:true)]
        public string PoImportanceId { get { return poImportance.PoImportanceId; } set { poImportance.PoImportanceId = value; } }

        [FwLogicProperty(Id:"2JPNi1eiLGqm", IsRecordTitle:true)]
        public string PoImportance { get { return poImportance.PoImportance; } set { poImportance.PoImportance = value; } }

        [FwLogicProperty(Id:"Rf1PtsGgQTnR")]
        public bool? Inactive { get { return poImportance.Inactive; } set { poImportance.Inactive = value; } }

        [FwLogicProperty(Id:"J7lP6VOGgqOX")]
        public string DateStamp { get { return poImportance.DateStamp; } set { poImportance.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
