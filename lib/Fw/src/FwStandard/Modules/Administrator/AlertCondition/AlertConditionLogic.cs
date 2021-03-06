using FwStandard.AppManager;
using FwStandard.BusinessLogic;
namespace FwStandard.Modules.Administrator.AlertCondition
{
    [FwLogic(Id: "yKwRfuJ0IdqLo")]
    public class AlertConditionLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AlertConditionRecord alertCondition = new AlertConditionRecord();
        public AlertConditionLogic()
        {
            dataRecords.Add(alertCondition);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "EaBnbUHq3TRpb")]
        public string AlertId { get { return alertCondition.AlertId; } set { alertCondition.AlertId = value; } }
        [FwLogicProperty(Id: "boC8m8TkwCdOt", IsPrimaryKey: true)]
        public string AlertConditionId { get { return alertCondition.AlertConditionId; } set { alertCondition.AlertConditionId = value; } }
        [FwLogicProperty(Id: "NHIEQgkgmLu")]
        public string FieldName1 { get { return alertCondition.FieldName1; } set { alertCondition.FieldName1 = value; } }
        [FwLogicProperty(Id: "UUhe4VnImFTN")]
        public string Condition { get { return alertCondition.Condition; } set { alertCondition.Condition = value; } }
        [FwLogicProperty(Id: "VuJ4wUqrDWFma")]
        public string FieldName2 { get { return alertCondition.FieldName2; } set { alertCondition.FieldName2 = value; } }
        [FwLogicProperty(Id: "U5Z1F0YaHsm5")]
        public string Value { get { return alertCondition.Value; } set { alertCondition.Value = value; } }
        [FwLogicProperty(Id: "vAPeuVWLSx6w")]
        public string DateStamp { get { return alertCondition.DateStamp; } set { alertCondition.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
