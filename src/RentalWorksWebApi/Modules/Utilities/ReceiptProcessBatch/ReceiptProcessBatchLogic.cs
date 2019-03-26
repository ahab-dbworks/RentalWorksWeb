using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Utilities.ReceiptProcessBatch
{
    [FwLogic(Id: "f5AtriQbvVHFR")]
    public class ReceiptProcessBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptProcessBatchLoader receiptProcessBatchLoader = new ReceiptProcessBatchLoader();
        public ReceiptProcessBatchLogic()
        {
            dataLoader = receiptProcessBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Wla7ZwbRCLpn3", IsReadOnly: true, IsPrimaryKey:true)]
        public string BatchId { get; set; }
        [FwLogicProperty(Id: "QmrrouUkD8oAO", IsReadOnly: true)]
        public string LocationId { get; set; }
        [FwLogicProperty(Id: "l35ZdYVI2T2WK", IsReadOnly: true)]
        public string BatchType { get; set; }
        [FwLogicProperty(Id: "M3GxGjOJvE3y", IsReadOnly: true)]
        public string DivisionCode { get; set; }
        [FwLogicProperty(Id: "rMqkqwbtAQMJ", IsReadOnly: true)]
        public string BatchNumber { get; set; }
        [FwLogicProperty(Id: "Vgt4U3oRiavm4", IsReadOnly: true)]
        public string BatchDate { get; set; }
        [FwLogicProperty(Id: "eTAClqIScqZTp", IsReadOnly: true)]
        public string BatchTime { get; set; }
        [FwLogicProperty(Id: "R8pINQcFx5Y9", IsReadOnly: true)]
        public string BatchDateTime { get; set; }
        [FwLogicProperty(Id: "e4U6JYvE8NyOV", IsReadOnly: true)]
        public string ExportDate { get; set; }
        [FwLogicProperty(Id: "wIfIPmepmXzBx", IsReadOnly: true)]
        public bool? Exported { get; set; }
        [FwLogicProperty(Id: "BCD6jelSPV3rt", IsReadOnly: true)]
        public int? RecordCount { get; set; }
        [FwLogicProperty(Id: "jjWBZ79QdAgG1", IsReadOnly: true)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
