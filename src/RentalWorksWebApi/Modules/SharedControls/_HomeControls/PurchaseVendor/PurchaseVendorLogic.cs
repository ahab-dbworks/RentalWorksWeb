using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PurchaseVendor
{
    [FwLogic(Id: "1BPqniIjiR7Z")]
    public class PurchaseVendorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseVendorLoader purchaseVendorLoader = new PurchaseVendorLoader();
        public PurchaseVendorLogic()
        {
            dataLoader = purchaseVendorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1cduRUb6HYLb6", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "1cP1SaBSnj8sR", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "1cQZXCVFuNlD", IsReadOnly: true)]
        public string VendorPartNumber { get; set; }
        [FwLogicProperty(Id: "1cUWVveK075T", IsReadOnly: true)]
        public string ManufacturerPartNumber { get; set; }
        [FwLogicProperty(Id: "1eR1V3dY7iE8", IsReadOnly: true)]
        public decimal? VendorRate { get; set; }
        [FwLogicProperty(Id: "1feGf6ApwMBbc", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "1FTiEq651AkG", IsReadOnly: true)]
        public string VendorDisplayName { get; set; }
        [FwLogicProperty(Id: "1ghjbAJXov8k", IsReadOnly: true)]
        public string City { get; set; }
        [FwLogicProperty(Id: "1H1PjnUBVosB", IsReadOnly: true)]
        public string State { get; set; }
        [FwLogicProperty(Id: "1IUSFp9nbJI2", IsReadOnly: true)]
        public string Phone { get; set; }
        [FwLogicProperty(Id: "1KHpkotytrFk", IsReadOnly: true)]
        public string PurchaseOrderDate { get; set; }
        [FwLogicProperty(Id: "1nk1pd6ykHr5", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "1p0mMEaARLWdq", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "1qOz8xqlqtjB", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
