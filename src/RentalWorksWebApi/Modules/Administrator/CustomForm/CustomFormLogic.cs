using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomForm
{
    [FwLogic(Id:"SgSLHzluaM9Kz")]
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
        [FwLogicProperty(Id:"JzEbZ2oG8xCLp", IsPrimaryKey:true)]
        public string CustomFormId { get { return customForm.CustomFormId; } set { customForm.CustomFormId = value; } }

        [FwLogicProperty(Id:"UgrDO8vhpTr6M")]
        public string WebUserId { get { return customForm.WebUserId; } set { customForm.WebUserId = value; } }

        [FwLogicProperty(Id:"WmTWaHooAIaid", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"rZU4UlSZ4eStr")]
        public string BaseForm { get { return customForm.BaseForm; } set { customForm.BaseForm = value; } }

        [FwLogicProperty(Id:"uaeGQnhd8KAnX", IsRecordTitle:true)]
        public string Description { get { return customForm.Description; } set { customForm.Description= value; } }

        [FwLogicProperty(Id:"tjhXQPdX2TXn6")]
        public string Html { get { return customForm.Html; } set { customForm.Html = value; } }

        [FwLogicProperty(Id:"c6F1RZmuMJWBa")]
        public bool? Active { get { return customForm.Active; } set { customForm.Active = value; } }

        [FwLogicProperty(Id:"RV8DGIfZ7ITeu")]
        public string DateStamp { get { return customForm.DateStamp; } set { customForm.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
