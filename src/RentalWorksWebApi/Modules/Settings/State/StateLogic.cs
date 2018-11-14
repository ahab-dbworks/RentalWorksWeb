using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.State
{
    [FwLogic(Id:"0lLdn9T6WPhng")]
    public class StateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        StateRecord state = new StateRecord();
        public StateLogic()
        {
            dataRecords.Add(state);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"g1UHZsnElglzo", IsPrimaryKey:true)]
        public string StateId { get { return state.StateId; } set { state.StateId = value; } }

        [FwLogicProperty(Id:"g1UHZsnElglzo", IsRecordTitle:true)]
        public string State { get { return state.State; } set { state.State = value; } }

        [FwLogicProperty(Id:"FNxSaQ5Rn3qu")]
        public string StateCode { get { return state.StateCode; } set { state.StateCode = value; } }


        //[FwLogicProperty(Id:"FYQSzQA1yuor")]
        //public string Inactive { get { return state.Inactive; } set { state.Inactive = value; } }

        [FwLogicProperty(Id:"lVDYt6cK0t1T")]
        public string DateStamp { get { return state.DateStamp; } set { state.DateStamp = value; } }

        //------------------------------------------------------------------------------------



    }




}
