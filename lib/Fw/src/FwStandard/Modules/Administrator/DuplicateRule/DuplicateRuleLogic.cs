using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
//using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace FwStandard.Modules.Administrator.DuplicateRule
{
    public class DuplicateRuleLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DuplicateRuleRecord duplicateRule = new DuplicateRuleRecord();
        DuplicateRuleLoader duplicateRuleLoader = new DuplicateRuleLoader();
        public DuplicateRuleLogic()
        {
            dataRecords.Add(duplicateRule);
            dataLoader = duplicateRuleLoader;
            duplicateRule.AfterSaves += OnAfterSavesDuplicateRule;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DuplicateRuleId { get { return duplicateRule.DuplicateRuleId; } set { duplicateRule.DuplicateRuleId = value; } }
        public string ModuleName { get { return duplicateRule.ModuleName; } set { duplicateRule.ModuleName = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RuleName { get { return duplicateRule.RuleName; } set { duplicateRule.RuleName = value; } }
        public bool? CaseSensitive { get { return duplicateRule.CaseSensitive; } set { duplicateRule.CaseSensitive = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Fields { get; set; }
        public string DateStamp { get { return duplicateRule.DateStamp; } set { duplicateRule.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSavesDuplicateRule(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = duplicateRule.SaveFields(Fields).Result;
        }
        //------------------------------------------------------------------------------------    
    }
}