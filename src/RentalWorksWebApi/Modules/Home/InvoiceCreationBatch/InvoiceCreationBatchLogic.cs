using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.InvoiceCreationBatch
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InvoiceCreationBatchId { get { return invoiceCreationBatch.InvoiceCreationBatchId; } set { invoiceCreationBatch.InvoiceCreationBatchId = value; } }
        public int? BatchNumber { get { return invoiceCreationBatch.BatchNumber; } set { invoiceCreationBatch.BatchNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchNumberAsString { get; set; }
        public string BatchDate { get { return invoiceCreationBatch.BatchDate; } set { invoiceCreationBatch.BatchDate = value; } }
        public string BatchType { get { return invoiceCreationBatch.BatchType; } set { invoiceCreationBatch.BatchType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InvoiceCount { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
