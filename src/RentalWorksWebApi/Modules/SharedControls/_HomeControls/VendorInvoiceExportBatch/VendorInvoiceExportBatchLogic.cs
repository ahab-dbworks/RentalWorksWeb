using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceExportBatch
{
    [FwLogic(Id: "LfY2qEOeMOeB")]
    public class VendorInvoiceExportBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceExportBatchLoader vendorInvoiceExportBatchLoader = new VendorInvoiceExportBatchLoader();
        public VendorInvoiceExportBatchLogic()
        {
            dataLoader = vendorInvoiceExportBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "FICGVZLcQHgY", IsReadOnly: true, IsPrimaryKey: true)]
        public string BatchId { get; set; }
        [FwLogicProperty(Id: "gKENAm9lpsRf", IsReadOnly: true, IsPrimaryKey: true)]
        public string VendorInvoiceId { get; set; }
        [FwLogicProperty(Id: "TSFITCadCFJVc", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "ye06L9rhHZLEU", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "iqTLGYiazSM6", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "A1hS8LvAlNHS", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "zKd7HgHgMy0I", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "8aCb3r27UDDx", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "adGFZ7EI09W0", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "jE7QH0UHbEVL", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "cBngFEbrDUaT", IsReadOnly: true)]
        public decimal? InvoiceTotal { get; set; }
        [FwLogicProperty(Id: "jAg8dq4Lf8u5", IsReadOnly: true)]
        public string BatchNumber { get; set; }
        [FwLogicProperty(Id: "N9Dol0rGQM4qS", IsReadOnly: true)]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
