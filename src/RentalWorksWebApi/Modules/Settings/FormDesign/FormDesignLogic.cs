using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.FormDesign
{
    public class FormDesignLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FormDesignRecord formDesign = new FormDesignRecord();
        public FormDesignLogic()
        {
            dataRecords.Add(formDesign);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string FormDesignId { get { return formDesign.FormDesignId; } set { formDesign.FormDesignId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string FormDesign { get { return formDesign.FormDesign; } set { formDesign.FormDesign = value; } }
        public string FormType { get { return formDesign.FormType; } set { formDesign.FormType = value; } }
        public bool? Inactive { get { return formDesign.Inactive; } set { formDesign.Inactive = value; } }
        public string DateStamp { get { return formDesign.DateStamp; } set { formDesign.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
