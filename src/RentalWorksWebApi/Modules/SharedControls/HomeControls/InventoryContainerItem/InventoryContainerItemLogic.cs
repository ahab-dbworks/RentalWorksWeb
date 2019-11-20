using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.HomeControls.MasterItem;
using WebApi;

namespace WebApi.Modules.HomeControls.InventoryContainerItem
{
    [FwLogic(Id:"rUeTflFjXQNj")]
    public class InventoryContainerItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterItemRecord masterItem = new MasterItemRecord();
        InventoryContainerItemLoader inventoryContainerItemLoader = new InventoryContainerItemLoader();
        public InventoryContainerItemLogic()
        {
            dataRecords.Add(masterItem);
            dataLoader = inventoryContainerItemLoader;
            RecType = RwConstants.RECTYPE_RENTAL;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"9vChfDlvCfnM", IsPrimaryKey:true)]
        public string ContainerItemId { get { return masterItem.MasterItemId; } set { masterItem.MasterItemId = value; } }

        [FwLogicProperty(Id:"eDeYeUyiy5qT")]
        public string PackageId { get; set; }

        [FwLogicProperty(Id:"1JvuxCM2rTRE")]
        public string ContainerId { get { return masterItem.OrderId; } set { masterItem.OrderId = value; } }

        [FwLogicProperty(Id:"n9IYOvuQffsH", IsReadOnly:true)]
        public int? RowNumber { get; set; }

        [FwLogicProperty(Id:"JKc0ePPcemlr", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"85Nnh6YlNuG9", IsRecordTitle:true)]
        public string Description { get { return masterItem.Description; } set { masterItem.Description = value; } }

        [FwLogicProperty(Id: "6MDboeDjtVhSN", IsReadOnly: true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"MmqhWNq9Y0aQ")]
        public decimal? QuantityOrdered { get { return masterItem.QuantityOrdered; } set { masterItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id:"EMUSaJWxAQXv")]
        public decimal? Price { get { return masterItem.Price; } set { masterItem.Price = value; } }

        [FwLogicProperty(Id:"SsgBQIZaohMm", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"LwuxXYr1z9ha")]
        public string InventoryId { get { return masterItem.InventoryId; } set { masterItem.InventoryId = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxx")]
        public string RecType { get { return masterItem.RecType; } set { masterItem.RecType = value; } }

        [FwLogicProperty(Id:"MZz6DHCiiZvO", IsReadOnly:true)]
        public int? Ident { get; set; }

        //[FwLogicProperty(Id:"PBhsoRms9ShK")]
        //public string RecType { get { return masterItem.RecType; } set { masterItem.RecType = value; } }

        [FwLogicProperty(Id:"1tQrPSqV8e7n")]
        public string DateStamp { get { return masterItem.DateStamp; } set { masterItem.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
