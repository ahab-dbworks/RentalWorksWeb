using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.ItemVendor
{
    public class ItemVendorLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemVendorRecord itemVendor = new ItemVendorRecord();
        ItemVendorLoader itemVendorLoader = new ItemVendorLoader();
        public ItemVendorLogic()
        {
            dataRecords.Add(itemVendor);
            dataLoader = itemVendorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemVendorId { get { return itemVendor.ItemVendorId; } set { itemVendor.ItemVendorId = value; } }
        public string ItemId { get { return itemVendor.ItemId; } set { itemVendor.ItemId = value; } }
        public string VendorId { get { return itemVendor.VendorId; } set { itemVendor.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactOfficePhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactEmail { get; set; }
        public string DateStamp { get { return itemVendor.DateStamp; } set { itemVendor.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}