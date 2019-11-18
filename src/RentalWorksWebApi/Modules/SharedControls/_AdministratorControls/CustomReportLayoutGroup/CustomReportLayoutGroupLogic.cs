using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.CustomReportLayoutGroup
{
    [FwLogic(Id: "sDAxvkztP8gZR")]
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
        [FwLogicProperty(Id: "Stv5BmlxVS2ep", IsPrimaryKey: true)]
        public string CustomReportLayoutGroupId { get { return customReportLayoutGroup.CustomReportLayoutGroupId; } set { customReportLayoutGroup.CustomReportLayoutGroupId = value; } }
        [FwLogicProperty(Id: "PWfbhCSG5AjiV")]
        public string CustomReportLayoutId { get { return customReportLayoutGroup.CustomReportLayoutId; } set { customReportLayoutGroup.CustomReportLayoutId = value; } }
        [FwLogicProperty(Id: "DEtKyDv5ZaCet", IsReadOnly: true)]
        public string CustomReportLayoutDescription { get; set; }
        [FwLogicProperty(Id: "ATXNwspovuT0V")]
        public string GroupId { get { return customReportLayoutGroup.GroupId; } set { customReportLayoutGroup.GroupId = value; } }
        [FwLogicProperty(Id: "glttdyCtaWT9K", IsReadOnly: true)]
        public string GroupName { get; set; }
        [FwLogicProperty(Id: "nOo9ang55eqxw")]
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
