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
            duplicateRule.AfterSave += OnAfterSaveDuplicateRule;
            duplicateRule.BeforeDelete += OnBeforeDeleteDuplicateRule;
            duplicateRule.AfterDelete += OnAfterDeleteDuplicateRule;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DuplicateRuleId { get { return duplicateRule.DuplicateRuleId; } set { duplicateRule.DuplicateRuleId = value; } }
        public string ModuleName { get { return duplicateRule.ModuleName; } set { duplicateRule.ModuleName = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RuleName { get { return duplicateRule.RuleName; } set { duplicateRule.RuleName = value; } }
        public bool? CaseSensitive { get { return duplicateRule.CaseSensitive; } set { duplicateRule.CaseSensitive = value; } }
        public bool? SystemRule { get { return duplicateRule.SystemRule; } set { duplicateRule.SystemRule = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Fields { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RuleNameColor { get; set; }
        public string DateStamp { get { return duplicateRule.DateStamp; } set { duplicateRule.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode == TDataRecordSaveMode.smInsert)
            {
                SystemRule = false;
            }
            else
            {
                DuplicateRuleLogic l2 = new DuplicateRuleLogic();
                l2.SetDependencies(this.AppConfig, this.UserSession);
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<DuplicateRuleLogic>(pk).Result;
                if (l2.SystemRule.Value)
                {
                    isValid = false;
                    validateMsg = "System Duplicate Rules Cannot be modified.";
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveDuplicateRule(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            saved = duplicateRule.SaveFields(Fields).Result;
            refreshDuplicateRules();
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeDeleteDuplicateRule(object sender, BeforeDeleteEventArgs e)
        {
            DuplicateRuleLogic l2 = new DuplicateRuleLogic();
            l2.SetDependencies(this.AppConfig, this.UserSession);
            object[] pk = GetPrimaryKeys();
            bool b = l2.LoadAsync<DuplicateRuleLogic>(pk).Result;
            if (l2.SystemRule.Value)
            {
                e.PerformDelete = false;
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterDeleteDuplicateRule(object sender, AfterDeleteEventArgs e)
        {
            refreshDuplicateRules();
        }
        //------------------------------------------------------------------------------------ 
    }
}