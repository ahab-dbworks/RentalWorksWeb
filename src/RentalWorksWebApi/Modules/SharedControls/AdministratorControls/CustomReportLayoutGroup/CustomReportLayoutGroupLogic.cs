using WebApi.Logic;
using FwStandard.AppManager;

namespace WebApi.Modules.AdministratorControls.CustomReportLayoutGroup
{
    [FwLogic(Id: "cl5jfGLO4bZa")]
    public class CustomReportLayoutGroupLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomReportLayoutGroupRecord customReportLayoutGroup = new CustomReportLayoutGroupRecord();
        CustomReportLayoutGroupLoader customReportLayoutGroupLoader = new CustomReportLayoutGroupLoader();
        public CustomReportLayoutGroupLogic()
        {
            dataRecords.Add(customReportLayoutGroup);
            dataLoader = customReportLayoutGroupLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2rfy7JGCrc95", IsPrimaryKey: true)]
        public string CustomReportLayoutGroupId { get { return customReportLayoutGroup.CustomReportLayoutGroupId; } set { customReportLayoutGroup.CustomReportLayoutGroupId = value; } }
        [FwLogicProperty(Id: "T9zbm3p5hHX3k")]
        public string CustomReportLayoutId { get { return customReportLayoutGroup.CustomReportLayoutId; } set { customReportLayoutGroup.CustomReportLayoutId = value; } }
        [FwLogicProperty(Id: "ntqeYZ0zfGQ18", IsReadOnly: true)]
        public string CustomReportLayoutDescription { get; set; }
        [FwLogicProperty(Id: "ZYvbWN0CfHK8")]
        public string GroupId { get { return customReportLayoutGroup.GroupId; } set { customReportLayoutGroup.GroupId = value; } }
        [FwLogicProperty(Id: "F1qyq3GGTP9OI", IsReadOnly: true)]
        public string GroupName { get; set; }
        [FwLogicProperty(Id: "4zborH1PKXIw1")]
        public string DateStamp { get { return customReportLayoutGroup.DateStamp; } set { customReportLayoutGroup.DateStamp = value; } }
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
