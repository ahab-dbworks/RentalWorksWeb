using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace FwStandard.Modules.Administrator.DuplicateRule
{
    [FwLogic(Id: "Bf3mlgqUR")]
    public class DuplicateRuleLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DuplicateRuleRecord duplicateRule = new DuplicateRuleRecord();
        DuplicateRuleLoader duplicateRuleLoader = new DuplicateRuleLoader();
        public DuplicateRuleLogic()
        {
            duplicateRule.ForceSave = true;
            dataRecords.Add(duplicateRule);
            dataLoader = duplicateRuleLoader;
            duplicateRule.AfterSave += OnAfterSaveDuplicateRule;
            duplicateRule.BeforeDelete += OnBeforeDeleteDuplicateRule;
            duplicateRule.AfterDelete += OnAfterDeleteDuplicateRule;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "pV8NXgtRYZ", IsPrimaryKey: true)]
        public string DuplicateRuleId { get { return duplicateRule.DuplicateRuleId; } set { duplicateRule.DuplicateRuleId = value; } }

        [FwLogicProperty(Id: "rtRIa3bNGB")]
        public string ModuleName { get { return duplicateRule.ModuleName; } set { duplicateRule.ModuleName = value; } }

        [FwLogicProperty(Id: "b496ihKUJC", IsRecordTitle: true)]
        public string RuleName { get { return duplicateRule.RuleName; } set { duplicateRule.RuleName = value; } }

        [FwLogicProperty(Id: "RH2PTu1cys")]
        public bool? CaseSensitive { get { return duplicateRule.CaseSensitive; } set { duplicateRule.CaseSensitive = value; } }

        [FwLogicProperty(Id: "O1JBkVitjv")]
        public bool? SystemRule { get { return duplicateRule.SystemRule; } set { duplicateRule.SystemRule = value; } }

        [FwLogicProperty(Id: "dec9t7gTVh")]
        public string Fields { get; set; }

        [FwLogicProperty(Id: "k8u3IXggRyaYx")]
        public string FieldTypes { get; set; }

        [FwLogicProperty(Id: "7cT78TG6mD", IsReadOnly: true)]
        public string RuleNameColor { get; set; }

        [FwLogicProperty(Id: "v5uQTIhnkQ")]
        public bool? ConsiderBlanks { get { return duplicateRule.ConsiderBlanks; } set { duplicateRule.ConsiderBlanks = value; } }

        [FwLogicProperty(Id: "Ox792PKBkv")]
        public string DateStamp { get { return duplicateRule.DateStamp; } set { duplicateRule.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                if (saveMode == TDataRecordSaveMode.smInsert)
                {
                    SystemRule = false;
                }
                else
                {
                    if (original != null)
                    {
                        DuplicateRuleLogic orig = (DuplicateRuleLogic)original;
                        if (orig.SystemRule.GetValueOrDefault(false))
                        {
                            isValid = false;
                            validateMsg = "System Duplicate Rules Cannot be modified.";
                        }
                    }
                }
            }
            if (isValid)
            {
                if (((saveMode == TDataRecordSaveMode.smInsert) && (string.IsNullOrEmpty(Fields))) ||
                    ((saveMode == TDataRecordSaveMode.smUpdate) && (Fields != null) && (Fields.Equals(string.Empty))))
                {
                    isValid = false;
                    validateMsg = "Specify at least one field for this Duplicate Rule.";
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveDuplicateRule(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = false;
            saved = duplicateRule.SaveFieldsAsync(Fields, FieldTypes).Result;
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