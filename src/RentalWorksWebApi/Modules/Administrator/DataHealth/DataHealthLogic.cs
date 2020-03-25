using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Administrator.DataHealth
{
    [FwLogic(Id: "SgSLHzluaM9Kz")]
    public class DataHealthLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DataHealthLoader dataHealthLoader = new DataHealthLoader();
        public DataHealthLogic()
        {
            dataLoader = dataHealthLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2MVI0lynHBsO", IsPrimaryKey: true)]
        public string DataHealthid { get; set; }

        [FwLogicProperty(Id: "aC3PyMIjJ0FV", IsReadOnly: true)]
        public string DataHealthType { get; set; }

        [FwLogicProperty(Id: "qY9Y4gZZOqoI", IsReadOnly: true)]
        public string CaptureDateTime { get; set; }

        [FwLogicProperty(Id: "qkT1ZbF4CUDI", IsReadOnly: true)]
        public string Json { get; set; }

        [FwLogicProperty(Id: "S5UrLHX24aGx", IsReadOnly: true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id: "A3KHXh6Khvwc", IsReadOnly: true)]
        public bool? Resolved { get; set; }

        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        //{
        //    bool isValid = true;
        //    if (isValid)
        //    {
        //        PropertyInfo property = typeof(DataHealthLogic).GetProperty(nameof(CustomFormLogic.AssignTo));
        //        string[] acceptableValues = { RwConstants.CUSTOM_FORM_ASSIGN_TO_ALL, RwConstants.CUSTOM_FORM_ASSIGN_TO_GROUPS, RwConstants.CUSTOM_FORM_ASSIGN_TO_USERS };
        //        isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
        //    }
        //    return isValid;
        //}
        //------------------------------------------------------------------------------------
    }
}
