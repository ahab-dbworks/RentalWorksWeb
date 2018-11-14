using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.DuplicateRuleField
{
    [FwLogic(Id:"BIJWmVKWZDNt")]
    public class DuplicateRuleFieldLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DuplicateRuleFieldRecord duplicateRuleField = new DuplicateRuleFieldRecord();
        public DuplicateRuleFieldLogic()
        {
            dataRecords.Add(duplicateRuleField);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"APueXHdiDS2z", IsPrimaryKey:true)]
        public string DuplicateRuleFieldId { get { return duplicateRuleField.DuplicateRuleFieldId; } set { duplicateRuleField.DuplicateRuleFieldId = value; } }

        [FwLogicProperty(Id:"7OdQQBTy0oOpv")]
        public string DuplicateRuleId { get { return duplicateRuleField.DuplicateRuleId; } set { duplicateRuleField.DuplicateRuleId = value; } }

        [FwLogicProperty(Id:"rC9d5BQN4SD4q")]
        public string FieldName { get { return duplicateRuleField.FieldName; } set { duplicateRuleField.FieldName = value; } }

        [FwLogicProperty(Id:"j4vU31Ioxm3ae")]
        public string DateStamp { get { return duplicateRuleField.DateStamp; } set { duplicateRuleField.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
