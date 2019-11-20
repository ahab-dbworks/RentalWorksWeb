using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.AdministratorControls.CustomReportLayout
{
    [FwLogic(Id:"SgSLHzluaM9Kz")]
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
        [FwLogicProperty(Id:"JzEbZ2oG8xCLp", IsPrimaryKey:true)]
        public string CustomReportLayoutId { get { return customReportLayout.CustomReportLayoutId; } set { customReportLayout.CustomReportLayoutId = value; } }

        [FwLogicProperty(Id:"UgrDO8vhpTr6M")]
        public string WebUserId { get { return customReportLayout.WebUserId; } set { customReportLayout.WebUserId = value; } }

        [FwLogicProperty(Id:"WmTWaHooAIaid", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"rZU4UlSZ4eStr")]
        public string BaseReport { get { return customReportLayout.BaseReport; } set { customReportLayout.BaseReport = value; } }

        [FwLogicProperty(Id:"uaeGQnhd8KAnX", IsRecordTitle:true)]
        public string Description { get { return customReportLayout.Description; } set { customReportLayout.Description= value; } }

        [FwLogicProperty(Id:"tjhXQPdX2TXn6")]
        public string Html { get { return customReportLayout.Html; } set { customReportLayout.Html = value; } }

        [FwLogicProperty(Id:"c6F1RZmuMJWBa")]
        public bool? Active { get { return customReportLayout.Active; } set { customReportLayout.Active = value; } }

        [FwLogicProperty(Id: "CDj7UVNVa9ZWl", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "175LODTpQS8xn")]
        public string AssignTo { get { return customReportLayout.AssignTo; } set { customReportLayout.AssignTo = value; } }

        [FwLogicProperty(Id:"RV8DGIfZ7ITeu")]
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
