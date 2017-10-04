using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.SapVendorInvoiceStatus
{
    public class SapVendorInvoiceStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SapVendorInvoiceStatusRecord sapVendorInvoiceStatus = new SapVendorInvoiceStatusRecord();
        SapVendorInvoiceStatusLoader sapVendorInvoiceStatusLoader = new SapVendorInvoiceStatusLoader();

        public SapVendorInvoiceStatusLogic()
        {
            dataRecords.Add(sapVendorInvoiceStatus);
            dataLoader = sapVendorInvoiceStatusLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SapVendorInvoiceStatusId { get { return sapVendorInvoiceStatus.SapVendorInvoiceStatusId; } set { sapVendorInvoiceStatus.SapVendorInvoiceStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SapVendorInvoiceStatus { get { return sapVendorInvoiceStatus.SapVendorInvoiceStatus; } set { sapVendorInvoiceStatus.SapVendorInvoiceStatus = value; } }
        public string VendorInvoiceStatus { get { return sapVendorInvoiceStatus.VendorInvoiceStatus; } set { sapVendorInvoiceStatus.VendorInvoiceStatus = value; } }
        public string SapStatus { get { return sapVendorInvoiceStatus.SapStatus; } set { sapVendorInvoiceStatus.SapStatus = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SapStatusDisplay { get; set; }
        public string DateStamp { get { return sapVendorInvoiceStatus.DateStamp; } set { sapVendorInvoiceStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}