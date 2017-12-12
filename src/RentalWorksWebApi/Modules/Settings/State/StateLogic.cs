using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.State
{
    public class StateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        StateRecord state = new StateRecord();
        public StateLogic()
        {
            dataRecords.Add(state);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string StateId { get { return state.StateId; } set { state.StateId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string State { get { return state.State; } set { state.State = value; } }
        public string StateCode { get { return state.StateCode; } set { state.StateCode = value; } }

        //public string Inactive { get { return state.Inactive; } set { state.Inactive = value; } }
        public string DateStamp { get { return state.DateStamp; } set { state.DateStamp = value; } }
        //------------------------------------------------------------------------------------



    }




}
