using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomForm
{
    public class CustomFormLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFormRecord customForm = new CustomFormRecord();
        CustomFormLoader customFormLoader = new CustomFormLoader();
        public CustomFormLogic()
        {
            dataRecords.Add(customForm);
            dataLoader = customFormLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomFormId { get { return customForm.CustomFormId; } set { customForm.CustomFormId = value; } }
        public string WebUserId { get { return customForm.WebUserId; } set { customForm.WebUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        public string BaseForm { get { return customForm.BaseForm; } set { customForm.BaseForm = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return customForm.Description; } set { customForm.Description= value; } }
        public string Html { get { return customForm.Html; } set { customForm.Html = value; } }
        public bool? Active { get { return customForm.Active; } set { customForm.Active = value; } }
        public string DateStamp { get { return customForm.DateStamp; } set { customForm.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
