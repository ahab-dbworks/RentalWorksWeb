using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Administrator.CustomReportLayout
{
    [FwLogic(Id:"wycGKfM5VXXCv")]
    public class CustomReportLayoutLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomReportLayoutRecord customReportLayout = new CustomReportLayoutRecord();
        CustomReportLayoutLoader customReportLayoutLoader = new CustomReportLayoutLoader();
        public CustomReportLayoutLogic()
        {
            dataRecords.Add(customReportLayout);
            dataLoader = customReportLayoutLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"jWPE2jVafgEyB", IsPrimaryKey:true)]
        public string CustomReportLayoutId { get { return customReportLayout.CustomReportLayoutId; } set { customReportLayout.CustomReportLayoutId = value; } }

        [FwLogicProperty(Id:"puRIgLHFC52Jx")]
        public string WebUserId { get { return customReportLayout.WebUserId; } set { customReportLayout.WebUserId = value; } }

        [FwLogicProperty(Id:"7FXIG2wWIyxd0", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"1QP8Mpzey3DHx")]
        public string BaseReport { get { return customReportLayout.BaseReport; } set { customReportLayout.BaseReport = value; } }

        [FwLogicProperty(Id:"ctoqEXGQaaGk8", IsRecordTitle:true)]
        public string Description { get { return customReportLayout.Description; } set { customReportLayout.Description= value; } }

        [FwLogicProperty(Id:"i9YrNY5DDc4Fg")]
        public string Html { get { return customReportLayout.Html; } set { customReportLayout.Html = value; } }

        [FwLogicProperty(Id:"qE9s7XZ0Lv10P")]
        public bool? Active { get { return customReportLayout.Active; } set { customReportLayout.Active = value; } }

        [FwLogicProperty(Id: "Dme8OvAsVCCbQ", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "J2daipnWohu9X")]
        public string AssignTo { get { return customReportLayout.AssignTo; } set { customReportLayout.AssignTo = value; } }

        [FwLogicProperty(Id:"Xr30bQgjbWBFB")]
        public string DateStamp { get { return customReportLayout.DateStamp; } set { customReportLayout.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(CustomReportLayoutLogic).GetProperty(nameof(CustomReportLayoutLogic.AssignTo));
                string[] acceptableValues = { RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_ALL, RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_GROUPS, RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_USERS };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
