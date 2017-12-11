using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.DuplicateRuleField
{
    public class DuplicateRuleFieldLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DuplicateRuleFieldRecord duplicateRuleField = new DuplicateRuleFieldRecord();
        public DuplicateRuleFieldLogic()
        {
            dataRecords.Add(duplicateRuleField);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DuplicateRuleFieldId { get { return duplicateRuleField.DuplicateRuleFieldId; } set { duplicateRuleField.DuplicateRuleFieldId = value; } }
        public string DuplicateRuleId { get { return duplicateRuleField.DuplicateRuleId; } set { duplicateRuleField.DuplicateRuleId = value; } }
        public string FieldName { get { return duplicateRuleField.FieldName; } set { duplicateRuleField.FieldName = value; } }
        public string DateStamp { get { return duplicateRuleField.DateStamp; } set { duplicateRuleField.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}