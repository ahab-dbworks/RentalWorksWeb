using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.MasterItem;

namespace RentalWorksWebApi.Modules.Home.InventoryContainerItem
{
    public class InventoryContainerItemLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterItemRecord masterItem = new MasterItemRecord();
        InventoryContainerItemLoader inventoryContainerItemLoader = new InventoryContainerItemLoader();
        public InventoryContainerItemLogic()
        {
            dataRecords.Add(masterItem);
            dataLoader = inventoryContainerItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContainerItemId { get { return masterItem.MasterItemId; } set { masterItem.MasterItemId = value; } }
        public string PackageId { get; set; }
        public string ContainerId { get { return masterItem.OrderId; } set { masterItem.OrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? RowNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return masterItem.Description; } set { masterItem.Description = value; } }
        public decimal? QuantityOrdered { get { return masterItem.QuantityOrdered; } set { masterItem.QuantityOrdered = value; } }
        public decimal? Price { get { return masterItem.Price; } set { masterItem.Price = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public string InventoryId { get { return masterItem.InventoryId; } set { masterItem.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Ident { get; set; }
        //public string RecType { get { return masterItem.RecType; } set { masterItem.RecType = value; } }
        public string DateStamp { get { return masterItem.DateStamp; } set { masterItem.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}