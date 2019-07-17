using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomField
{
    [FwLogic(Id:"kl89BUagDNlM")]
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
        [FwLogicProperty(Id:"0wFTC5qoof3a", IsPrimaryKey:true)]
        public string CustomFieldId { get { return customField.CustomFieldId; } set { customField.CustomFieldId = value; } }

        [FwLogicProperty(Id:"MQeFXApzAtDR", IsRecordTitle:true)]
        public string ModuleName { get { return customField.ModuleName; } set { customField.ModuleName = value; } }

        [FwLogicProperty(Id:"lWL7lHtRH7ec", IsRecordTitle:true)]
        public string FieldName { get { return customField.FieldName; } set { customField.FieldName = value; } }

        [FwLogicProperty(Id:"PpFkllR2RvPNc")]
        public string CustomTableName { get { return customField.CustomTableName; } set { customField.CustomTableName = value; } }

        [FwLogicProperty(Id:"84BMeDFjilMys")]
        public string CustomFieldName { get { return customField.CustomFieldName; } set { customField.CustomFieldName = value; } }

        [FwLogicProperty(Id:"gv54sLRxJYvH", IsReadOnly:true)]
        public string FieldType { get; set; }

        [FwLogicProperty(Id:"9Ck7RMxoEB72", IsReadOnly:true)]
        public string ControlType { get; set; }

        [FwLogicProperty(Id:"F85n6HZmv1evj")]
        public int? StringLength { get { return customField.StringLength; } set { customField.StringLength = value; } }

        [FwLogicProperty(Id:"azBl1ZFK9p8Ce")]
        public int? FloatDecimalDigits { get { return customField.FloatDecimalDigits; } set { customField.FloatDecimalDigits = value; } }

        //[FwLogicProperty(Id:"Cb7CzC0AovlxT")]
        //public bool? ShowInBrowse { get { return customField.ShowInBrowse; } set { customField.ShowInBrowse = value; } }

        //[FwLogicProperty(Id:"OCHzypTOVLA5A")]
        //public int? BrowseSizeInPixels { get { return customField.BrowseSizeInPixels; } set { customField.BrowseSizeInPixels = value; } }

        [FwLogicProperty(Id:"1QZH4BJxj8x4C")]
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
