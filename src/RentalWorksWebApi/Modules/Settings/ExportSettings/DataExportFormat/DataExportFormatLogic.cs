using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ExportSettings.DataExportFormat
{
    [FwLogic(Id: "ct9GHxH7o5Sy")]
    public class DataExportFormatLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DataExportFormatRecord dataExportFormat = new DataExportFormatRecord();
        DataExportFormatLoader dataExportFormatLoader = new DataExportFormatLoader();
        public DataExportFormatLogic()
        {
            dataRecords.Add(dataExportFormat);
            dataLoader = dataExportFormatLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YXM8el7OjZSl", IsPrimaryKey:true)]
        public int? DataExportFormatId { get { return dataExportFormat.DataExportFormatId; } set { dataExportFormat.DataExportFormatId = value; } }

        [FwLogicProperty(Id: "1c6m6xEZxKqI")]
        public string ExportType { get { return dataExportFormat.ExportType; } set { dataExportFormat.ExportType = value; } }

        [FwLogicProperty(Id: "8UieGc8QPwWL", IsRecordTitle:true)]
        public string Description { get { return dataExportFormat.Description; } set { dataExportFormat.Description= value; } }

        [FwLogicProperty(Id: "sgBpoPTzrJ6w")]
        public bool? Active { get { return dataExportFormat.Active; } set { dataExportFormat.Active = value; } }

        [FwLogicProperty(Id: "jxgZ3H3wSvWb")]
        public string ExportString { get { return dataExportFormat.ExportString; } set { dataExportFormat.ExportString = value; } }

        [FwLogicProperty(Id: "7Y04fzYnZiRT", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "BzxD4YdazX0q")]
        public bool? DefaultFormat { get { return dataExportFormat.DefaultFormat; } set { dataExportFormat.DefaultFormat = value; } }

        [FwLogicProperty(Id: "u0kuE0tMspAx")]
        public string DateStamp { get { return dataExportFormat.DateStamp; } set { dataExportFormat.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        //{
        //    bool isValid = true;
        //    if (isValid)
        //    {
        //        PropertyInfo property = typeof(CustomReportLayoutLogic).GetProperty(nameof(CustomReportLayoutLogic.AssignTo));
        //        string[] acceptableValues = { RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_ALL, RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_GROUPS, RwConstants.CUSTOM_REPORT_LAYOUT_ASSIGN_TO_USERS };
        //        isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
        //    }
        //    return isValid;
        //}
        //------------------------------------------------------------------------------------
    }
}
