using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.CustomFormGroup
{
    [FwLogic(Id: "cl5jfGLO4bZa")]
    public class CustomFormGroupLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFormGroupRecord webFormGroup = new CustomFormGroupRecord();
        CustomFormGroupLoader webFormGroupLoader = new CustomFormGroupLoader();
        public CustomFormGroupLogic()
        {
            dataRecords.Add(webFormGroup);
            dataLoader = webFormGroupLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2rfy7JGCrc95", IsPrimaryKey: true)]
        public string CustomFormGroupId { get { return webFormGroup.CustomFormGroupId; } set { webFormGroup.CustomFormGroupId = value; } }
        [FwLogicProperty(Id: "T9zbm3p5hHX3k")]
        public string CustomFormId { get { return webFormGroup.CustomFormId; } set { webFormGroup.CustomFormId = value; } }
        [FwLogicProperty(Id: "ntqeYZ0zfGQ18", IsReadOnly: true)]
        public string CustomFormDescription { get; set; }
        [FwLogicProperty(Id: "ZYvbWN0CfHK8")]
        public string GroupId { get { return webFormGroup.GroupId; } set { webFormGroup.GroupId = value; } }
        [FwLogicProperty(Id: "F1qyq3GGTP9OI", IsReadOnly: true)]
        public string GroupName { get; set; }
        [FwLogicProperty(Id: "4zborH1PKXIw1")]
        public string DateStamp { get { return webFormGroup.DateStamp; } set { webFormGroup.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
