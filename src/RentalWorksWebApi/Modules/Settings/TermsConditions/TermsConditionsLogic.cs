using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.TermsConditions
{
    public class TermsConditionsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        TermsConditionsRecord termsConditions = new TermsConditionsRecord();
        TermsConditionsLoader termsConditionsLoader = new TermsConditionsLoader();
        public TermsConditionsLogic()
        {
            dataRecords.Add(termsConditions);
            dataLoader = termsConditionsLoader;
            termsConditions.AfterSave += OnAfterSaveTermsConditions;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string TermsConditionsId { get { return termsConditions.TermsConditionsId; } set { termsConditions.TermsConditionsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return termsConditions.Description; } set { termsConditions.Description = value; } }
        public string FileName { get { return termsConditions.FileName; } set { termsConditions.FileName = value; } }
        public string Html { get; set; }
        public bool? StartOnNewPage { get { return termsConditions.StartOnNewPage; } set { termsConditions.StartOnNewPage = value; } }
        public bool? Inactive { get { return termsConditions.Inactive; } set { termsConditions.Inactive = value; } }
        public string DateStamp { get { return termsConditions.DateStamp; } set { termsConditions.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTermsConditions(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = termsConditions.SaveHtmlASync(Html).Result;
        }
        //------------------------------------------------------------------------------------
    }

}
