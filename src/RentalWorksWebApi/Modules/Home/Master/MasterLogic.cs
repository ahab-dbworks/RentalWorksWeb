using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.Master
{
    public abstract class MasterLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterRecord master = new MasterRecord();
        public MasterLogic()
        {
            dataRecords.Add(master);
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
        public string UnitId { get { return master.UnitId; } set { master.UnitId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UnitType { get; set; }
        public bool NonDiscountable { get { return master.NonDiscountable; } set { master.NonDiscountable = value; } }
        public bool OverrideProfitAndLossCategory { get { return master.OverrideProfitAndLossCategory; } set { master.OverrideProfitAndLossCategory = value; } }
        public string ProfitAndLossCategoryId { get { return master.ProfitAndLossCategoryId; } set { master.ProfitAndLossCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProfitAndLossCategory { get; set; }
        public bool AutoCopyNotesToQuoteOrder { get { return master.AutoCopyNotesToQuoteOrder; } set { master.AutoCopyNotesToQuoteOrder = value; } }







        public bool Inactive { get { return master.Inactive; } set { master.Inactive = value; } }
        public string DateStamp { get { return master.DateStamp; } set { master.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}