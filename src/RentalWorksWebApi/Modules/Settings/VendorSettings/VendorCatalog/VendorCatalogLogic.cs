using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VendorSettings.VendorCatalog
{
    [FwLogic(Id:"tmww2BzIhyrJW")]
    public class VendorCatalogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorCatalogRecord vendorCatalog = new VendorCatalogRecord();
        VendorCatalogLoader vendorCatalogLoader = new VendorCatalogLoader();
        public VendorCatalogLogic()
        {
            dataRecords.Add(vendorCatalog);
            dataLoader = vendorCatalogLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"gBHC86lp3p7uK", IsPrimaryKey:true)]
        public string VendorCatalogId { get { return vendorCatalog.VendorCatalogId; } set { vendorCatalog.VendorCatalogId = value; } }

        [FwLogicProperty(Id:"gBHC86lp3p7uK", IsRecordTitle:true)]
        public string VendorCatalog { get { return vendorCatalog.VendorCatalog; } set { vendorCatalog.VendorCatalog = value; } }

        [FwLogicProperty(Id:"EYKbmB41IkOT")]
        public string CatalogType { get { return vendorCatalog.CatalogType; } set { vendorCatalog.CatalogType = value; } }

        [FwLogicProperty(Id:"ygRFunOU4WZD")]
        public string InventoryTypeId { get { return vendorCatalog.InventoryTypeId; } set { vendorCatalog.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"1RWaP34dlGrBp", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"ncZU5OjhrnSd")]
        public string CategoryId { get { return vendorCatalog.CategoryId; } set { vendorCatalog.CategoryId = value; } }

        [FwLogicProperty(Id:"cy9yGprDqWUz5", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"TcwrDnRsofNQ")]
        public string VendorId { get { return vendorCatalog.VendorId; } set { vendorCatalog.VendorId = value; } }

        [FwLogicProperty(Id:"gBHC86lp3p7uK", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"opYNI7djszby")]
        public decimal? TaxRate { get { return vendorCatalog.TaxRate; } set { vendorCatalog.TaxRate = value; } }

        [FwLogicProperty(Id:"9VmtVyutDAEd")]
        public decimal? GlobalMarkup { get { return vendorCatalog.GlobalMarkup; } set { vendorCatalog.GlobalMarkup = value; } }

        [FwLogicProperty(Id:"MvhOLOQVvQYJ")]
        public string CarrierId { get { return vendorCatalog.CarrierId; } set { vendorCatalog.CarrierId = value; } }

        [FwLogicProperty(Id:"4ulvLuxBQrIjD", IsReadOnly:true)]
        public string Carrier { get; set; }

        [FwLogicProperty(Id:"eGmA14pnEv6H")]
        public string ShipViaId { get { return vendorCatalog.ShipViaId; } set { vendorCatalog.ShipViaId = value; } }

        [FwLogicProperty(Id:"CNupBKggndBsI", IsReadOnly:true)]
        public string ShipVia { get; set; }

        [FwLogicProperty(Id:"gaf1Vv1mDwEd")]
        public bool? Inactive { get { return vendorCatalog.Inactive; } set { vendorCatalog.Inactive = value; } }

        [FwLogicProperty(Id:"YVRZdzC3EEOq")]
        public string DateStamp { get { return vendorCatalog.DateStamp; } set { vendorCatalog.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
