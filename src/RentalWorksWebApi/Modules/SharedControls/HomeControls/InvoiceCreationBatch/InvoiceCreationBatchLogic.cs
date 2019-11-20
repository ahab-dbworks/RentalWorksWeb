using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InvoiceCreationBatch
{
    [FwLogic(Id:"PbGue6RD4rkF")]
    public class InvoiceCreationBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceCreationBatchRecord invoiceCreationBatch = new InvoiceCreationBatchRecord();
        InvoiceCreationBatchLoader invoiceCreationBatchLoader = new InvoiceCreationBatchLoader();
        public InvoiceCreationBatchLogic()
        {
            dataRecords.Add(invoiceCreationBatch);
            dataLoader = invoiceCreationBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qYzytX7rVIx0", IsPrimaryKey:true)]
        public string InvoiceCreationBatchId { get { return invoiceCreationBatch.InvoiceCreationBatchId; } set { invoiceCreationBatch.InvoiceCreationBatchId = value; } }

        [FwLogicProperty(Id:"akVkxeVUj2kV")]
        public int? BatchNumber { get { return invoiceCreationBatch.BatchNumber; } set { invoiceCreationBatch.BatchNumber = value; } }

        [FwLogicProperty(Id:"nBdZPaO6djL4", IsReadOnly:true)]
        public string BatchNumberAsString { get; set; }

        [FwLogicProperty(Id:"j7XbISbuqmCJ")]
        public string BatchDate { get { return invoiceCreationBatch.BatchDate; } set { invoiceCreationBatch.BatchDate = value; } }

        [FwLogicProperty(Id:"7iV4j0f6thuN")]
        public string BatchType { get { return invoiceCreationBatch.BatchType; } set { invoiceCreationBatch.BatchType = value; } }

        [FwLogicProperty(Id:"HT2FmxcQK4fa", IsReadOnly:true)]
        public int? InvoiceCount { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
