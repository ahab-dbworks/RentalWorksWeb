using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PresentationSettings.FormDesign
{
    [FwLogic(Id:"U7wX3Pt1ZYYh")]
    public class FormDesignLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FormDesignRecord formDesign = new FormDesignRecord();
        public FormDesignLogic()
        {
            dataRecords.Add(formDesign);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"2Di6hzfL68su", IsPrimaryKey:true)]
        public string FormDesignId { get { return formDesign.FormDesignId; } set { formDesign.FormDesignId = value; } }

        [FwLogicProperty(Id:"2Di6hzfL68su", IsRecordTitle:true)]
        public string FormDesign { get { return formDesign.FormDesign; } set { formDesign.FormDesign = value; } }

        [FwLogicProperty(Id:"KOl3Kp3JQLO")]
        public string FormType { get { return formDesign.FormType; } set { formDesign.FormType = value; } }

        [FwLogicProperty(Id:"In8W6ehXwLE")]
        public bool? Inactive { get { return formDesign.Inactive; } set { formDesign.Inactive = value; } }

        [FwLogicProperty(Id:"5OekD2aRfgR")]
        public string DateStamp { get { return formDesign.DateStamp; } set { formDesign.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
