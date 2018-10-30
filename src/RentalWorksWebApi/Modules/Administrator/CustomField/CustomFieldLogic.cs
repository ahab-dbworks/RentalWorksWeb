using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomField
{
    public class CustomFieldLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFieldRecord customField = new CustomFieldRecord();
        CustomFieldLoader customFieldLoader = new CustomFieldLoader();
        public CustomFieldLogic()
        {
            dataRecords.Add(customField);
            dataLoader = customFieldLoader;
            customField.AfterSave += OnAfterSaveCustomField;
            customField.AfterDelete += OnAfterDeleteCustomField;
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
        [FwBusinessLogicField(isReadOnly: true)]
        public string FieldType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ControlType { get; set; }
        public int? FieldSizeInPixels { get { return customField.FieldSizeInPixels; } set { customField.FieldSizeInPixels = value; } }
        public int? StringLength { get { return customField.StringLength; } set { customField.StringLength = value; } }
        public int? FloatDecimalDigits { get { return customField.FloatDecimalDigits; } set { customField.FloatDecimalDigits = value; } }
        //public bool? ShowInBrowse { get { return customField.ShowInBrowse; } set { customField.ShowInBrowse = value; } }
        //public int? BrowseSizeInPixels { get { return customField.BrowseSizeInPixels; } set { customField.BrowseSizeInPixels = value; } }
        public string DateStamp { get { return customField.DateStamp; } set { customField.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if ((saveMode.Equals(TDataRecordSaveMode.smInsert)) || (saveMode.Equals(TDataRecordSaveMode.smUpdate) && (ModuleName != null)))
            {
                if (ModuleName.Equals("CustomField"))
                {
                    isValid = false;
                    validateMsg = "Cannot add Custom Fields to the CustomField module.";
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveCustomField(object sender, AfterSaveDataRecordEventArgs e)
        {
            refreshCustomFields();
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterDeleteCustomField(object sender, AfterDeleteEventArgs e)
        {
            refreshCustomFields();
        }
        //------------------------------------------------------------------------------------ 
    }
}