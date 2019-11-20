using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.VendorSettings.SapVendorInvoiceStatus
{
    [FwLogic(Id:"kQJc1t5kB5Ult")]
    public class SapVendorInvoiceStatusLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"aClERTkLGTsKz", IsPrimaryKey:true)]
        public string SapVendorInvoiceStatusId { get { return sapVendorInvoiceStatus.SapVendorInvoiceStatusId; } set { sapVendorInvoiceStatus.SapVendorInvoiceStatusId = value; } }

        [FwLogicProperty(Id:"aClERTkLGTsKz", IsRecordTitle:true)]
        public string SapVendorInvoiceStatus { get { return sapVendorInvoiceStatus.SapVendorInvoiceStatus; } set { sapVendorInvoiceStatus.SapVendorInvoiceStatus = value; } }

        [FwLogicProperty(Id:"qftBh0oczjqq")]
        public string VendorInvoiceStatus { get { return sapVendorInvoiceStatus.VendorInvoiceStatus; } set { sapVendorInvoiceStatus.VendorInvoiceStatus = value; } }

        [FwLogicProperty(Id:"7FlMTAYFXRK3")]
        public string SapStatus { get { return sapVendorInvoiceStatus.SapStatus; } set { sapVendorInvoiceStatus.SapStatus = value; } }

        [FwLogicProperty(Id:"eQz7tUoXLP9ZK", IsReadOnly:true)]
        public string SapStatusDisplay { get; set; }

        [FwLogicProperty(Id:"9Ty5xe2jXc9t")]
        public string DateStamp { get { return sapVendorInvoiceStatus.DateStamp; } set { sapVendorInvoiceStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
