using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryVendor
{
    [FwLogic(Id:"rHpgPjlZVIDz")]
    public class InventoryVendorLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"jR3xnzJP78ZV", IsPrimaryKey:true)]
        public string InventoryVendorId { get { return inventoryVendor.InventoryVendorId; } set { inventoryVendor.InventoryVendorId = value; } }

        [FwLogicProperty(Id:"SbyGW48IrG9e")]
        public string InventoryId { get { return inventoryVendor.InventoryId; } set { inventoryVendor.InventoryId = value; } }

        [FwLogicProperty(Id:"RFBwMXbakDpb")]
        public string VendorId { get { return inventoryVendor.VendorId; } set { inventoryVendor.VendorId = value; } }

        [FwLogicProperty(Id:"0yEAU3KLZkmb", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"vN6laBDKs4PE", IsReadOnly:true)]
        public string ContactName { get; set; }

        [FwLogicProperty(Id:"KTVWGBqMBMDQ", IsReadOnly:true)]
        public string ContactOfficePhone { get; set; }

        [FwLogicProperty(Id:"sU07BqAa72J0", IsReadOnly:true)]
        public string ContactEmail { get; set; }

        [FwLogicProperty(Id:"xDjMuXKVCH6g")]
        public string DateStamp { get { return inventoryVendor.DateStamp; } set { inventoryVendor.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
