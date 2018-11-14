using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Settings.TermsConditions
{
    [FwLogic(Id:"03fdCmk9Chp0M")]
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
        [FwLogicProperty(Id:"Hm3bRTR0i0K16", IsPrimaryKey:true)]
        public string TermsConditionsId { get { return termsConditions.TermsConditionsId; } set { termsConditions.TermsConditionsId = value; } }

        [FwLogicProperty(Id:"0zl4ZQqfW2qDD", IsRecordTitle:true)]
        public string Description { get { return termsConditions.Description; } set { termsConditions.Description = value; } }

        [FwLogicProperty(Id:"QsNZRPWzNXFT")]
        public string FileName { get { return termsConditions.FileName; } set { termsConditions.FileName = value; } }

        [FwLogicProperty(Id:"pn2EdtHzsDJR")]
        public string Html { get; set; }

        [FwLogicProperty(Id:"hKqv53xHF0mL")]
        public bool? StartOnNewPage { get { return termsConditions.StartOnNewPage; } set { termsConditions.StartOnNewPage = value; } }

        [FwLogicProperty(Id:"lk9f0iQh7UAf")]
        public bool? Inactive { get { return termsConditions.Inactive; } set { termsConditions.Inactive = value; } }

        [FwLogicProperty(Id:"p01lmEXlUukb")]
        public string DateStamp { get { return termsConditions.DateStamp; } set { termsConditions.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnAfterSaveTermsConditions(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = termsConditions.SaveHtmlASync(Html).Result;
        }
        //------------------------------------------------------------------------------------
    }

}
