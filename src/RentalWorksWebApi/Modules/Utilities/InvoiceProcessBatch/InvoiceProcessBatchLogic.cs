using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Utilities.InvoiceProcessBatch
{
    [FwLogic(Id: "2I29Kp3h6aVI")]
    public class InvoiceProcessBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceProcessBatchLoader invoiceProcessBatchLoader = new InvoiceProcessBatchLoader();
        public InvoiceProcessBatchLogic()
        {
            dataLoader = invoiceProcessBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "OLvQpovUMgWv", IsReadOnly: true, IsPrimaryKey: true)]
        public string BatchId { get; set; }
        [FwLogicProperty(Id: "9OSqynOzRmpn", IsReadOnly: true)]
        public string LocationId { get; set; }
        [FwLogicProperty(Id: "dLMN1ecOlBSdA", IsReadOnly: true)]
        public string BatchType { get; set; }
        [FwLogicProperty(Id: "bI9DCFhNkpDpl", IsReadOnly: true)]
        public string DivisionCode { get; set; }
        [FwLogicProperty(Id: "TuKUrBq4IJ1Uz", IsReadOnly: true)]
        public string BatchNumber { get; set; }
        [FwLogicProperty(Id: "vQscvWAH7H5b", IsReadOnly: true)]
        public string BatchDate { get; set; }
        [FwLogicProperty(Id: "D60wCxDn9Mt6k", IsReadOnly: true)]
        public string BatchTime { get; set; }
        [FwLogicProperty(Id: "5AqbFFliWp7Eq", IsReadOnly: true)]
        public string BatchDateTime { get; set; }
        [FwLogicProperty(Id: "W5Yah7sqSUiyR", IsReadOnly: true)]
        public string ExportDate { get; set; }
        [FwLogicProperty(Id: "lYt8no5UhU0d", IsReadOnly: true)]
        public bool? Exported { get; set; }
        [FwLogicProperty(Id: "yWHKBU4bz2qNp", IsReadOnly: true)]
        public int? RecordCount { get; set; }
        [FwLogicProperty(Id: "JzjEMUwK6DfFH", IsReadOnly: true)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
