using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;
using WebApi.Modules.AdministratorControls.CustomFormUser;

namespace WebApi.Modules.Administrator.CustomForm
{
    [FwLogic(Id: "SgSLHzluaM9Kz")]
    public class CustomFormLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFormRecord customForm = new CustomFormRecord();
        CustomFormLoader customFormLoader = new CustomFormLoader();
        public CustomFormLogic()
        {
            dataRecords.Add(customForm);
            dataLoader = customFormLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "JzEbZ2oG8xCLp", IsPrimaryKey: true)]
        public string CustomFormId { get { return customForm.CustomFormId; } set { customForm.CustomFormId = value; } }

        [FwLogicProperty(Id: "UgrDO8vhpTr6M")]
        public string WebUserId { get { return customForm.WebUserId; } set { customForm.WebUserId = value; } }

        [FwLogicProperty(Id: "WmTWaHooAIaid", IsReadOnly: true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id: "rZU4UlSZ4eStr")]
        public string BaseForm { get { return customForm.BaseForm; } set { customForm.BaseForm = value; } }

        [FwLogicProperty(Id: "uaeGQnhd8KAnX", IsRecordTitle: true)]
        public string Description { get { return customForm.Description; } set { customForm.Description = value; } }

        [FwLogicProperty(Id: "tjhXQPdX2TXn6")]
        public string Html { get { return customForm.Html; } set { customForm.Html = value; } }

        [FwLogicProperty(Id: "c6F1RZmuMJWBa")]
        public bool? Active { get { return customForm.Active; } set { customForm.Active = value; } }

        [FwLogicProperty(Id: "CDj7UVNVa9ZWl", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "175LODTpQS8xn")]
        public string AssignTo { get { return customForm.AssignTo; } set { customForm.AssignTo = value; } }

        [FwLogicProperty(Id: "1KPyDqwKpSRf")]
        public bool? SelfAssign { get; set; }

        [FwLogicProperty(Id: "RV8DGIfZ7ITeu")]
        public string DateStamp { get { return customForm.DateStamp; } set { customForm.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(CustomFormLogic).GetProperty(nameof(CustomFormLogic.AssignTo));
                string[] acceptableValues = { RwConstants.CUSTOM_FORM_ASSIGN_TO_ALL, RwConstants.CUSTOM_FORM_ASSIGN_TO_GROUPS, RwConstants.CUSTOM_FORM_ASSIGN_TO_USERS };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (SelfAssign.GetValueOrDefault(false))
            {
                if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    AssignTo = RwConstants.CUSTOM_FORM_ASSIGN_TO_USERS;
                }
                else if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if (e.Original != null)
                    {
                        CustomFormLogic orig = ((CustomFormLogic)e.Original);
                        orig.AssignTo = RwConstants.CUSTOM_FORM_ASSIGN_TO_USERS;
                    }
                }
            }
        }

        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (SelfAssign.GetValueOrDefault(false))
                {
                    CustomFormUserLogic l = new CustomFormUserLogic();
                    l.SetDependencies(AppConfig, UserSession);
                    l.CustomFormId = CustomFormId;
                    l.WebUserId = WebUserId;
                    int saveCount = l.SaveAsync(null, e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
