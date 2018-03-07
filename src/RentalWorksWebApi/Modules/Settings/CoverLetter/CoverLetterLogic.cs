using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CoverLetter
{
    public class CoverLetterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CoverLetterRecord coverLetter = new CoverLetterRecord();
        CoverLetterLoader coverLetterLoader = new CoverLetterLoader();
        public CoverLetterLogic()
        {
            dataRecords.Add(coverLetter);
            dataLoader = coverLetterLoader;
            coverLetter.AfterSave += OnAfterSaveCoverLetter;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CoverLetterId { get { return coverLetter.CoverLetterId; } set { coverLetter.CoverLetterId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return coverLetter.Description; } set { coverLetter.Description = value; } }
        public string FileName { get { return coverLetter.FileName; } set { coverLetter.FileName = value; } }
        public string Html { get; set; }
        public bool? Inactive { get { return coverLetter.Inactive; } set { coverLetter.Inactive = value; } }
        public string DateStamp { get { return coverLetter.DateStamp; } set { coverLetter.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveCoverLetter(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = coverLetter.SaveHtmlASync(Html).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
