using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.TermsConditions
{
    public class TermsConditionsLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        TermsConditionsRecord termsConditions = new TermsConditionsRecord();
        public TermsConditionsLogic()
        {
            dataRecords.Add(termsConditions);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string TermsConditionsId { get { return termsConditions.TermsConditionsId; } set { termsConditions.TermsConditionsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return termsConditions.Description; } set { termsConditions.Description = value; } }
        public string FileName { get { return termsConditions.FileName; } set { termsConditions.FileName = value; } }
        public bool? StartOnNewPage { get { return termsConditions.StartOnNewPage; } set { termsConditions.StartOnNewPage = value; } }
        public bool? Inactive { get { return termsConditions.Inactive; } set { termsConditions.Inactive = value; } }
        public string DateStamp { get { return termsConditions.DateStamp; } set { termsConditions.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
