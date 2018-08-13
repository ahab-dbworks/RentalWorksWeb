using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.InvoiceCreationBatch
{
    public class InvoiceCreationBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceCreationBatchLoader invoiceCreationBatchLoader = new InvoiceCreationBatchLoader();
        public InvoiceCreationBatchLogic()
        {
            dataLoader = invoiceCreationBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? BatchNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchNumberAsString { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InvoiceCount { get; set; }
    }
}
