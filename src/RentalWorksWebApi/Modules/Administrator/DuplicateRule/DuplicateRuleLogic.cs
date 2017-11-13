using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Administrator.DuplicateRule
{
    public class DuplicateRuleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DuplicateRuleRecord duplicateRule = new DuplicateRuleRecord();
        public DuplicateRuleLogic()
        {
            dataRecords.Add(duplicateRule);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DuplicateRuleId { get { return duplicateRule.DuplicateRuleId; } set { duplicateRule.DuplicateRuleId = value; } }
        public string ModuleName { get { return duplicateRule.ModuleName; } set { duplicateRule.ModuleName = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RuleName { get { return duplicateRule.RuleName; } set { duplicateRule.RuleName = value; } }
        public bool CaseSensitive { get { return duplicateRule.CaseSensitive; } set { duplicateRule.CaseSensitive = value; } }
        public string DateStamp { get { return duplicateRule.DateStamp; } set { duplicateRule.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}