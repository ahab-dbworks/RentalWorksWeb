using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomField
{
    public class CustomFieldLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFieldRecord customField = new CustomFieldRecord();
        public CustomFieldLogic()
        {
            dataRecords.Add(customField);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomFieldId { get { return customField.CustomFieldId; } set { customField.CustomFieldId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ModuleName { get { return customField.ModuleName; } set { customField.ModuleName = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string FieldName { get { return customField.FieldName; } set { customField.FieldName = value; } }
        public string CustomTableName { get { return customField.CustomTableName; } set { customField.CustomTableName = value; } }
        public string CustomFieldName { get { return customField.CustomFieldName; } set { customField.CustomFieldName = value; } }
        public string DateStamp { get { return customField.DateStamp; } set { customField.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}