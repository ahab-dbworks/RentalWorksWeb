using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.Master
{
    [FwLogic(Id:"eNvZYaWdjRpb")]
    public abstract class MasterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterRecord master = new MasterRecord();
        public MasterLogic()
        {
            dataRecords.Add(master);

            master.BeforeSave += OnBeforeSaveMaster;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"5W33grkqpS7C", IsRecordTitle:true)]
        public string ICode { get { return master.ICode; } set { master.ICode = value; } }

        [FwLogicProperty(Id:"w5YezKMiKGXs", IsRecordTitle: true)]
        public string Description { get { return master.Description; } set { master.Description = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"L64pgRC1AkoG")]
        public string AvailFor { get { return master.AvailFor; } set { master.AvailFor = value; } }

        [FwLogicProperty(Id:"G7wn6ndtfr20")]
        public string CategoryId { get { return master.CategoryId; } set { master.CategoryId = value; } }

        [FwLogicProperty(Id:"eSoo2HICHWnB", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"KfOu7uySeUz9", IsReadOnly:true)]
        public int? SubCategoryCount { get; set; }

        [FwLogicProperty(Id:"hFhu0V0JqxDn")]
        public string SubCategoryId { get { return master.SubCategoryId; } set { master.SubCategoryId = value; } }

        [FwLogicProperty(Id:"U3sdKO7PMcIx", IsReadOnly:true)]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id:"o8BYaDnucrL3")]
        public string Classification { get { return master.Classification; } set { master.Classification = value; } }

        [FwLogicProperty(Id:"Sc4NJwblCgEe", IsReadOnly:true)]
        public string ClassificationDescription { get; set; }

        [FwLogicProperty(Id:"t3rlIqrYpxWC", IsReadOnly:true)]
        public string ClassificationColor { get; set; }

        [FwLogicProperty(Id:"rLOXhfvcCGPN")]
        public string UnitId { get { return master.UnitId; } set { master.UnitId = value; } }

        [FwLogicProperty(Id:"pjDCxoTCsyyR", IsReadOnly:true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id:"GQO5gOcupong", IsReadOnly:true)]
        public string UnitType { get; set; }

        [FwLogicProperty(Id:"YjG4q7tH5jfD")]
        public bool? NonDiscountable { get { return master.NonDiscountable; } set { master.NonDiscountable = value; } }

        [FwLogicProperty(Id:"rEBN9GTNyhTd")]
        public bool? OverrideProfitAndLossCategory { get { return master.OverrideProfitAndLossCategory; } set { master.OverrideProfitAndLossCategory = value; } }

        [FwLogicProperty(Id:"OOcbhuPBnm8D")]
        public string ProfitAndLossCategoryId { get { return master.ProfitAndLossCategoryId; } set { master.ProfitAndLossCategoryId = value; } }

        [FwLogicProperty(Id:"jXUjTh0yD7du", IsReadOnly:true)]
        public string ProfitAndLossCategory { get; set; }

        [FwLogicProperty(Id:"qXCYoqqRTMfw")]
        public bool? AutoCopyNotesToQuoteOrder { get { return master.AutoCopyNotesToQuoteOrder; } set { master.AutoCopyNotesToQuoteOrder = value; } }


        [FwLogicProperty(Id:"mjPe6oMNNb7k", IsReadOnly:true)]
        public string Note { get; set; }

        [FwLogicProperty(Id:"9nLWoD7J0HtX")]
        public bool? PrintNoteOnInContract { get { return master.PrintNoteOnInContract; } set { master.PrintNoteOnInContract = value; } }

        [FwLogicProperty(Id:"E7lB9sfUn3Sa")]
        public bool? PrintNoteOnOutContract { get { return master.PrintNoteOnOutContract; } set { master.PrintNoteOnOutContract = value; } }

        [FwLogicProperty(Id:"aVuC9ztvP7Yg")]
        public bool? PrintNoteOnReceiveContract { get { return master.PrintNoteOnReceiveContract; } set { master.PrintNoteOnReceiveContract = value; } }

        [FwLogicProperty(Id:"JfPpufOtCr3b")]
        public bool? PrintNoteOnReturnContract { get { return master.PrintNoteOnReturnContract; } set { master.PrintNoteOnReturnContract = value; } }

        [FwLogicProperty(Id:"2el1EPoM4gnS")]
        public bool? PrintNoteOnInvoice { get { return master.PrintNoteOnInvoice; } set { master.PrintNoteOnInvoice = value; } }

        [FwLogicProperty(Id:"AFfvrL4OdzIv")]
        public bool? PrintNoteOnOrder { get { return master.PrintNoteOnOrder; } set { master.PrintNoteOnOrder = value; } }

        [FwLogicProperty(Id:"Z7LPGzfCDU7B")]
        public bool? PrintNoteOnPickList { get { return master.PrintNoteOnPickList; } set { master.PrintNoteOnPickList = value; } }

        [FwLogicProperty(Id:"WWCAuqrOAOk0")]
        public bool? PrintNoteOnPO { get { return master.PrintNoteOnPO; } set { master.PrintNoteOnPO = value; } }

        [FwLogicProperty(Id:"PKKD6Sip3Ox6")]
        public bool? PrintNoteOnQuote { get { return master.PrintNoteOnQuote; } set { master.PrintNoteOnQuote = value; } }

        [FwLogicProperty(Id:"g8Lg8GyPokiE")]
        public bool? PrintNoteOnReturnList { get { return master.PrintNoteOnReturnList; } set { master.PrintNoteOnReturnList = value; } }

        [FwLogicProperty(Id:"fGThWGLYBDlg")]
        public bool? PrintNoteOnPoReceiveList { get { return master.PrintNoteOnPoReceiveList; } set { master.PrintNoteOnPoReceiveList = value; } }

        [FwLogicProperty(Id:"gZ4v469tyiMq")]
        public bool? PrintNoteOnPoReturnList { get { return master.PrintNoteOnPoReturnList; } set { master.PrintNoteOnPoReturnList = value; } }


        [FwLogicProperty(Id:"DlarQ9MMkOEe")]
        public bool? Inactive { get { return master.Inactive; } set { master.Inactive = value; } }

        [FwLogicProperty(Id:"EsmQtUkWV6fa")]
        public string DateStamp { get { return master.DateStamp; } set { master.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveMaster(object sender, BeforeSaveDataRecordEventArgs e)
        {

            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(ICode))
                {
                    //jh todo: need to make a single SP for this new ICode logic
                    ICode = AppFunc.GetNextSystemCounterAsync(AppConfig, UserSession, "masterno", e.SqlConnection).Result;
                    string iCodePrefix = AppFunc.GetStringDataAsync(AppConfig, "syscontrol", "controlid", "1", "icodeprefix").Result;
                    ICode = iCodePrefix.Trim() + ICode.PadLeft(6, '0');
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveNote = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveNote = true;
            }
            else if (e.Original != null)
            {
                MasterLogic orig = (MasterLogic)e.Original;
                doSaveNote = (!orig.Note.Equals(Note));
            }
            if (doSaveNote)
            {
                bool saved = master.SaveNoteASync(Note).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
