using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.WebForm
{
    public class WebFormLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebFormRecord webForm = new WebFormRecord();
        WebFormLoader webFormLoader = new WebFormLoader();
        public WebFormLogic()
        {
            dataRecords.Add(webForm);
            dataLoader = webFormLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WebFormId { get { return webForm.WebFormId; } set { webForm.WebFormId = value; } }
        public string WebUserId { get { return webForm.WebUserId; } set { webForm.WebUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string UserName { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string BaseForm { get { return webForm.BaseForm; } set { webForm.BaseForm = value; } }
        public string Html { get { return webForm.Html; } set { webForm.Html = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
