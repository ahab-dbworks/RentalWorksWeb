using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.Master
{
    public abstract class MasterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterRecord master = new MasterRecord();
        public MasterLogic()
        {
            dataRecords.Add(master);
            master.AfterSaves += OnAfterSavesMaster;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ICode { get { return master.ICode; } set { master.ICode = value; } }
        public string Description { get { return master.Description; } set { master.Description = value; } }
        [JsonIgnore]
        public string AvailFor { get { return master.AvailFor; } set { master.AvailFor = value; } }
        public string CategoryId { get { return master.CategoryId; } set { master.CategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        public string SubCategoryId { get { return master.SubCategoryId; } set { master.SubCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubCategory { get; set; }
        public string Classification { get { return master.Classification; } set { master.Classification = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ClassificationDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ClassificationColor { get; set; }
        public string UnitId { get { return master.UnitId; } set { master.UnitId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UnitType { get; set; }
        public bool? NonDiscountable { get { return master.NonDiscountable; } set { master.NonDiscountable = value; } }
        public bool? OverrideProfitAndLossCategory { get { return master.OverrideProfitAndLossCategory; } set { master.OverrideProfitAndLossCategory = value; } }
        public string ProfitAndLossCategoryId { get { return master.ProfitAndLossCategoryId; } set { master.ProfitAndLossCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitAndLossCategory { get; set; }
        public bool? AutoCopyNotesToQuoteOrder { get { return master.AutoCopyNotesToQuoteOrder; } set { master.AutoCopyNotesToQuoteOrder = value; } }

        [FwBusinessLogicField(isReadOnly: true)]  // todo: override the save
        public string Note { get; set; }
        public bool? PrintNoteOnInContract { get { return master.PrintNoteOnInContract; } set { master.PrintNoteOnInContract = value; } }
        public bool? PrintNoteOnOutContract { get { return master.PrintNoteOnOutContract; } set { master.PrintNoteOnOutContract = value; } }
        public bool? PrintNoteOnReceiveContract { get { return master.PrintNoteOnReceiveContract; } set { master.PrintNoteOnReceiveContract = value; } }
        public bool? PrintNoteOnReturnContract { get { return master.PrintNoteOnReturnContract; } set { master.PrintNoteOnReturnContract = value; } }
        public bool? PrintNoteOnInvoice { get { return master.PrintNoteOnInvoice; } set { master.PrintNoteOnInvoice = value; } }
        public bool? PrintNoteOnOrder { get { return master.PrintNoteOnOrder; } set { master.PrintNoteOnOrder = value; } }
        public bool? PrintNoteOnPickList { get { return master.PrintNoteOnPickList; } set { master.PrintNoteOnPickList = value; } }
        public bool? PrintNoteOnPO { get { return master.PrintNoteOnPO; } set { master.PrintNoteOnPO = value; } }
        public bool? PrintNoteOnQuote { get { return master.PrintNoteOnQuote; } set { master.PrintNoteOnQuote = value; } }
        public bool? PrintNoteOnReturnList { get { return master.PrintNoteOnReturnList; } set { master.PrintNoteOnReturnList = value; } }
        public bool? PrintNoteOnPoReceiveList { get { return master.PrintNoteOnPoReceiveList; } set { master.PrintNoteOnPoReceiveList = value; } }
        public bool? PrintNoteOnPoReturnList { get { return master.PrintNoteOnPoReturnList; } set { master.PrintNoteOnPoReturnList = value; } }







        public bool? Inactive { get { return master.Inactive; } set { master.Inactive = value; } }
        public string DateStamp { get { return master.DateStamp; } set { master.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSavesMaster(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = master.SaveNoteASync(Note).Result;
        }
        //------------------------------------------------------------------------------------
    }
}