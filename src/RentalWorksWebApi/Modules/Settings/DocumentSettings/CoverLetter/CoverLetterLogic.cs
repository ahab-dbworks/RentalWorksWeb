using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DocumentSettings.CoverLetter
{
    [FwLogic(Id:"jlFDLeOUXuYO")]
    public class CoverLetterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CoverLetterRecord coverLetter = new CoverLetterRecord();
        CoverLetterLoader coverLetterLoader = new CoverLetterLoader();
        public CoverLetterLogic()
        {
            dataRecords.Add(coverLetter);
            dataLoader = coverLetterLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"yZZ8EaKd8mc1", IsPrimaryKey:true)]
        public string CoverLetterId { get { return coverLetter.CoverLetterId; } set { coverLetter.CoverLetterId = value; } }

        [FwLogicProperty(Id:"DntyRJEQ2e9H", IsRecordTitle:true)]
        public string Description { get { return coverLetter.Description; } set { coverLetter.Description = value; } }

        [FwLogicProperty(Id:"c7FHV5qviXWB")]
        public string FileName { get { return coverLetter.FileName; } set { coverLetter.FileName = value; } }

        [FwLogicProperty(Id:"HReZgZ8WGxxe")]
        public string Html { get; set; }

        [FwLogicProperty(Id:"iYsxPjdhTqhR")]
        public bool? Inactive { get { return coverLetter.Inactive; } set { coverLetter.Inactive = value; } }

        [FwLogicProperty(Id:"ZJTtpQY5l0xY")]
        public string DateStamp { get { return coverLetter.DateStamp; } set { coverLetter.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public virtual void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveHtml = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveHtml = true;
            }
            else if (e.Original != null)
            {
                CoverLetterLogic orig = (CoverLetterLogic)e.Original;
                doSaveHtml = (!orig.Html.Equals(Html));
            }
            if (doSaveHtml)
            {
                bool saved = AppFunc.SaveNoteAsync(AppConfig, UserSession, CoverLetterId, "", "", Html).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }

}
