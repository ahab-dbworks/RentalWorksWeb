using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Utilities.VendorInvoiceProcessBatch
{
    [FwLogic(Id: "HetRinedRr4i")]
    public class VendorInvoiceProcessBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceProcessBatchLoader vendorInvoiceProcessBatchLoader = new VendorInvoiceProcessBatchLoader();
        public VendorInvoiceProcessBatchLogic()
        {
            dataLoader = vendorInvoiceProcessBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mBwd27Cpebm82", IsReadOnly: true)]
        public string BatchId { get; set; }
        [FwLogicProperty(Id: "SmmPjnRDnDP7i", IsReadOnly: true)]
        public string LocationId { get; set; }
        [FwLogicProperty(Id: "dUKYodRpzWUC0", IsReadOnly: true)]
        public string BatchType { get; set; }
        [FwLogicProperty(Id: "68ur8k2P8MSuA", IsReadOnly: true)]
        public string DivisionCode { get; set; }
        [FwLogicProperty(Id: "C8XYhGKyU1K46", IsReadOnly: true)]
        public string BatchNumber { get; set; }
        [FwLogicProperty(Id: "F3O9EeGbCbnG", IsReadOnly: true)]
        public string BatchDate { get; set; }
        [FwLogicProperty(Id: "rJNU0EgrEVFp", IsReadOnly: true)]
        public string BatchTime { get; set; }
        [FwLogicProperty(Id: "eqO0zcbOz47b", IsReadOnly: true)]
        public string BatchDateTime { get; set; }
        [FwLogicProperty(Id: "4p8VNuWtbIIuK", IsReadOnly: true)]
        public string ExportDate { get; set; }
        [FwLogicProperty(Id: "V0rpwHIVrr7Nu", IsReadOnly: true)]
        public bool? Exported { get; set; }
        [FwLogicProperty(Id: "hem1jwHpoIBEM", IsReadOnly: true)]
        public int? RecordCount { get; set; }
        [FwLogicProperty(Id: "2Ug2y8ZnYqIpQ", IsReadOnly: true)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
