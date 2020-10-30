using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.HomeControls.Master
{
    [FwLogic(Id: "eNvZYaWdjRpb")]
    public abstract class MasterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterRecord master = new MasterRecord();
        private string tmpICode = string.Empty;

        public MasterLogic()
        {
            dataRecords.Add(master);

            master.BeforeSave += OnBeforeSaveMaster;
            AfterSave += OnAfterSave;

            BeforeValidate += OnBeforeValidate;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "5W33grkqpS7C", IsRecordTitle: true)]
        public string ICode { get { return master.ICode; } set { master.ICode = value; } }

        [FwLogicProperty(Id: "w5YezKMiKGXs", IsRecordTitle: true)]
        public string Description { get { return master.Description; } set { master.Description = value; } }

        [FwLogicProperty(Id: "L64pgRC1AkoG")]
        public string AvailFor { get { return master.AvailFor; } set { master.AvailFor = value; } }

        [FwLogicProperty(Id: "G7wn6ndtfr20")]
        public string CategoryId { get { return master.CategoryId; } set { master.CategoryId = value; } }

        [FwLogicProperty(Id: "eSoo2HICHWnB", IsReadOnly: true)]
        public string Category { get; set; }

        [FwLogicProperty(Id: "KfOu7uySeUz9", IsReadOnly: true)]
        public int? SubCategoryCount { get; set; }

        [FwLogicProperty(Id: "hFhu0V0JqxDn")]
        public string SubCategoryId { get { return master.SubCategoryId; } set { master.SubCategoryId = value; } }

        [FwLogicProperty(Id: "U3sdKO7PMcIx", IsReadOnly: true)]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id: "o8BYaDnucrL3")]
        public string Classification { get { return master.Classification; } set { master.Classification = value; } }

        [FwLogicProperty(Id: "Sc4NJwblCgEe", IsReadOnly: true)]
        public string ClassificationDescription { get; set; }

        [FwLogicProperty(Id: "t3rlIqrYpxWC", IsReadOnly: true)]
        public string ClassificationColor { get; set; }

        [FwLogicProperty(Id: "rLOXhfvcCGPN")]
        public string UnitId { get { return master.UnitId; } set { master.UnitId = value; } }

        [FwLogicProperty(Id: "pjDCxoTCsyyR", IsReadOnly: true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id: "GQO5gOcupong", IsReadOnly: true)]
        public string UnitType { get; set; }

        [FwLogicProperty(Id: "YjG4q7tH5jfD")]
        public bool? NonDiscountable { get { return master.NonDiscountable; } set { master.NonDiscountable = value; } }

        [FwLogicProperty(Id: "rEBN9GTNyhTd")]
        public bool? OverrideProfitAndLossCategory { get { return master.OverrideProfitAndLossCategory; } set { master.OverrideProfitAndLossCategory = value; } }

        [FwLogicProperty(Id: "OOcbhuPBnm8D")]
        public string ProfitAndLossCategoryId { get { return master.ProfitAndLossCategoryId; } set { master.ProfitAndLossCategoryId = value; } }

        [FwLogicProperty(Id: "jXUjTh0yD7du", IsReadOnly: true)]
        public string ProfitAndLossCategory { get; set; }

        [FwLogicProperty(Id: "qXCYoqqRTMfw")]
        public bool? AutoCopyNotesToQuoteOrder { get { return master.AutoCopyNotesToQuoteOrder; } set { master.AutoCopyNotesToQuoteOrder = value; } }


        [FwLogicProperty(Id: "mjPe6oMNNb7k", IsReadOnly: true)]
        public string Note { get; set; }

        [FwLogicProperty(Id: "9nLWoD7J0HtX")]
        public bool? PrintNoteOnInContract { get { return master.PrintNoteOnInContract; } set { master.PrintNoteOnInContract = value; } }

        [FwLogicProperty(Id: "E7lB9sfUn3Sa")]
        public bool? PrintNoteOnOutContract { get { return master.PrintNoteOnOutContract; } set { master.PrintNoteOnOutContract = value; } }

        [FwLogicProperty(Id: "aVuC9ztvP7Yg")]
        public bool? PrintNoteOnReceiveContract { get { return master.PrintNoteOnReceiveContract; } set { master.PrintNoteOnReceiveContract = value; } }

        [FwLogicProperty(Id: "JfPpufOtCr3b")]
        public bool? PrintNoteOnReturnContract { get { return master.PrintNoteOnReturnContract; } set { master.PrintNoteOnReturnContract = value; } }

        [FwLogicProperty(Id: "2el1EPoM4gnS")]
        public bool? PrintNoteOnInvoice { get { return master.PrintNoteOnInvoice; } set { master.PrintNoteOnInvoice = value; } }

        [FwLogicProperty(Id: "AFfvrL4OdzIv")]
        public bool? PrintNoteOnOrder { get { return master.PrintNoteOnOrder; } set { master.PrintNoteOnOrder = value; } }

        [FwLogicProperty(Id: "Z7LPGzfCDU7B")]
        public bool? PrintNoteOnPickList { get { return master.PrintNoteOnPickList; } set { master.PrintNoteOnPickList = value; } }

        [FwLogicProperty(Id: "WWCAuqrOAOk0")]
        public bool? PrintNoteOnPO { get { return master.PrintNoteOnPO; } set { master.PrintNoteOnPO = value; } }

        [FwLogicProperty(Id: "PKKD6Sip3Ox6")]
        public bool? PrintNoteOnQuote { get { return master.PrintNoteOnQuote; } set { master.PrintNoteOnQuote = value; } }

        [FwLogicProperty(Id: "g8Lg8GyPokiE")]
        public bool? PrintNoteOnReturnList { get { return master.PrintNoteOnReturnList; } set { master.PrintNoteOnReturnList = value; } }

        [FwLogicProperty(Id: "fGThWGLYBDlg")]
        public bool? PrintNoteOnPoReceiveList { get { return master.PrintNoteOnPoReceiveList; } set { master.PrintNoteOnPoReceiveList = value; } }

        [FwLogicProperty(Id: "gZ4v469tyiMq")]
        public bool? PrintNoteOnPoReturnList { get { return master.PrintNoteOnPoReturnList; } set { master.PrintNoteOnPoReturnList = value; } }

        [FwLogicProperty(Id: "TsG6I0pB3nio1")]
        public string AssetAccountId { get { return master.AssetAccountId; } set { master.AssetAccountId = value; } }

        [FwLogicProperty(Id: "83WU6Loq3MakM", IsReadOnly: true)]
        public string AssetAccountNo { get; set; }

        [FwLogicProperty(Id: "1ZrintrYVNul6", IsReadOnly: true)]
        public string AssetAccountDescription { get; set; }

        [FwLogicProperty(Id: "6ZPy6P7YU1uFS")]
        public string IncomeAccountId { get { return master.IncomeAccountId; } set { master.IncomeAccountId = value; } }

        [FwLogicProperty(Id: "EiM71G42Sf4v2", IsReadOnly: true)]
        public string IncomeAccountNo { get; set; }

        [FwLogicProperty(Id: "flweJl1nNtULa", IsReadOnly: true)]
        public string IncomeAccountDescription { get; set; }

        [FwLogicProperty(Id: "k0wmUu8ndNzuM")]
        public string SubIncomeAccountId { get { return master.SubIncomeAccountId; } set { master.SubIncomeAccountId = value; } }

        [FwLogicProperty(Id: "F4IpPKscn8Fa8", IsReadOnly: true)]
        public string SubIncomeAccountNo { get; set; }

        [FwLogicProperty(Id: "dxJUicVubKIsV", IsReadOnly: true)]
        public string SubIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id: "ka675xLmATkqe")]
        public string LdIncomeAccountId { get { return master.LdIncomeAccountId; } set { master.LdIncomeAccountId = value; } }

        [FwLogicProperty(Id: "wjQ4dZDO2L5I8", IsReadOnly: true)]
        public string LdIncomeAccountNo { get; set; }

        [FwLogicProperty(Id: "TYp5FnLi7EB1Z", IsReadOnly: true)]
        public string LdIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id: "qcPq3Ldm9oxVp")]
        public string EquipmentSaleIncomeAccountId { get { return master.EquipmentSaleIncomeAccountId; } set { master.EquipmentSaleIncomeAccountId = value; } }

        [FwLogicProperty(Id: "3NOqgj0x3cZCL", IsReadOnly: true)]
        public string EquipmentSaleIncomeAccountNo { get; set; }

        [FwLogicProperty(Id: "sRIPsrJxWkSjR", IsReadOnly: true)]
        public string EquipmentSaleIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id: "vxIDCdWfsxiMp")]
        public string ExpenseAccountId { get { return master.ExpenseAccountId; } set { master.ExpenseAccountId = value; } }

        [FwLogicProperty(Id: "ypxs38RNHYNWH", IsReadOnly: true)]
        public string ExpenseAccountNo { get; set; }

        [FwLogicProperty(Id: "MXUSNvDK3wdOG", IsReadOnly: true)]
        public string ExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id: "N9yqsnfAAezwa")]
        public string CostOfGoodsSoldExpenseAccountId { get { return master.CostOfGoodsSoldExpenseAccountId; } set { master.CostOfGoodsSoldExpenseAccountId = value; } }

        [FwLogicProperty(Id: "O2utjCUtlVL45", IsReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }

        [FwLogicProperty(Id: "GxC9FyFMualx1", IsReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id: "ghW57upCinRhV")]
        public string CostOfGoodsRentedExpenseAccountId { get { return master.CostOfGoodsRentedExpenseAccountId; } set { master.CostOfGoodsRentedExpenseAccountId = value; } }

        [FwLogicProperty(Id: "VE1nDApvjihPh", IsReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }

        [FwLogicProperty(Id: "MmZUwR6995DXo", IsReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id: "CQ5WGXKc7j440")]
        public string DepreciationExpenseAccountId { get { return master.DepreciationExpenseAccountId; } set { master.DepreciationExpenseAccountId = value; } }

        [FwLogicProperty(Id: "PmggN2qAFvzuc", IsReadOnly: true)]
        public string DepreciationExpenseAccountNo { get; set; }

        [FwLogicProperty(Id: "vOibaySAZYb95", IsReadOnly: true)]
        public string DepreciationExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id: "E4ybPP2bgZqLo")]
        public string AccumulatedDepreciationExpenseAccountId { get { return master.AccumulatedDepreciationExpenseAccountId; } set { master.AccumulatedDepreciationExpenseAccountId = value; } }

        [FwLogicProperty(Id: "MerM2rktkrM6k", IsReadOnly: true)]
        public string AccumulatedDepreciationExpenseAccountNo { get; set; }

        [FwLogicProperty(Id: "PWvuFO2ZHerDR", IsReadOnly: true)]
        public string AccumulatedDepreciationExpenseAccountDescription { get; set; }


        //------------------------------------------------------------------------------------
        //[FwLogicProperty(Id: "8TQztJYFBndb4")]
        //public string CurrencyId { get; set; }
        //[FwLogicProperty(Id: "M9sn5VZeUxEIz")]
        //public string CurrencyCode { get; set; }
        //[FwLogicProperty(Id: "rVptB67EX3ccW")]
        //public string Currency { get; set; }
        //[FwLogicProperty(Id: "dGaxRrSGVcoWH")]
        //public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------


        [FwLogicProperty(Id: "DlarQ9MMkOEe")]
        public bool? Inactive { get { return master.Inactive; } set { master.Inactive = value; } }

        [FwLogicProperty(Id: "EsmQtUkWV6fa")]
        public string DateStamp { get { return master.DateStamp; } set { master.DateStamp = value; } }


        [FwLogicProperty(Id: "4emU4L2rvhFz")]
        public bool? ManifestShippingContainer { get { return master.ManifestShippingContainer; } set { master.ManifestShippingContainer = value; } }

        [FwLogicProperty(Id: "Qg8p4KHsY6t1")]
        public bool? ManifestStandAloneItem { get { return master.ManifestStandAloneItem; } set { master.ManifestStandAloneItem = value; } }
        

        //------------------------------------------------------------------------------------ 
        public void OnBeforeValidate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(ICode))
                {
                    tmpICode = AppFunc.GetNextIdAsync(AppConfig).Result;
                    ICode = tmpICode;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveMaster(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if ((string.IsNullOrEmpty(ICode)) || (ICode.Equals(tmpICode)))
                {
                    ICode = AppFunc.GetNextSystemCounterAsync(AppConfig, UserSession, "masterno", e.SqlConnection).Result;
                    string iCodePrefix = AppFunc.GetStringDataAsync(AppConfig, "syscontrol", "controlid", RwConstants.CONTROL_ID, "icodeprefix").Result;
                    //AAAAA-AA (mask)
                    ICode = iCodePrefix.Trim() + ICode;
                    if (!string.IsNullOrEmpty(Classification))
                    {
                        if (Classification.Equals(RwConstants.ITEMCLASS_COMPLETE))
                        {
                            ICode += "-" + RwConstants.ITEMCLASS_COMPLETE_SUFFIX;
                        }
                        else if (Classification.Equals(RwConstants.ITEMCLASS_KIT))
                        {
                            ICode += "-" + RwConstants.ITEMCLASS_KIT_SUFFIX;
                        }
                    }
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
                bool saved = master.SaveNoteASync(Note, e.SqlConnection).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
