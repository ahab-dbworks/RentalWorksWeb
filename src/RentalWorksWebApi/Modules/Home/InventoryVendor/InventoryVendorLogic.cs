using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.InventoryVendor
{
    public class InventoryVendorLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryVendorRecord inventoryVendor = new InventoryVendorRecord();
        InventoryVendorLoader inventoryVendorLoader = new InventoryVendorLoader();
        public InventoryVendorLogic()
        {
            dataRecords.Add(inventoryVendor);
            dataLoader = inventoryVendorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryVendorId { get { return inventoryVendor.InventoryVendorId; } set { inventoryVendor.InventoryVendorId = value; } }
        public string InventoryId { get { return inventoryVendor.InventoryId; } set { inventoryVendor.InventoryId = value; } }
        public string VendorId { get { return inventoryVendor.VendorId; } set { inventoryVendor.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactOfficePhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactEmail { get; set; }
        public string DateStamp { get { return inventoryVendor.DateStamp; } set { inventoryVendor.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}