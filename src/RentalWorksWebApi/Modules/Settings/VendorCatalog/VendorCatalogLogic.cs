using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VendorCatalog
{
    public class VendorCatalogLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorCatalogId { get { return vendorCatalog.VendorCatalogId; } set { vendorCatalog.VendorCatalogId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VendorCatalog { get { return vendorCatalog.VendorCatalog; } set { vendorCatalog.VendorCatalog = value; } }
        public string CatalogType { get { return vendorCatalog.CatalogType; } set { vendorCatalog.CatalogType = value; } }
        public string InventoryTypeId { get { return vendorCatalog.InventoryTypeId; } set { vendorCatalog.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public string CategoryId { get { return vendorCatalog.CategoryId; } set { vendorCatalog.CategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        public string VendorId { get { return vendorCatalog.VendorId; } set { vendorCatalog.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        public decimal TaxRate { get { return vendorCatalog.TaxRate; } set { vendorCatalog.TaxRate = value; } }
        public decimal GlobalMarkup { get { return vendorCatalog.GlobalMarkup; } set { vendorCatalog.GlobalMarkup = value; } }
        public string CarrierId { get { return vendorCatalog.CarrierId; } set { vendorCatalog.CarrierId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Carrier { get; set; }
        public string ShipViaId { get { return vendorCatalog.ShipViaId; } set { vendorCatalog.ShipViaId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ShipVia { get; set; }
        public bool Inactive { get { return vendorCatalog.Inactive; } set { vendorCatalog.Inactive = value; } }
        public string DateStamp { get { return vendorCatalog.DateStamp; } set { vendorCatalog.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
