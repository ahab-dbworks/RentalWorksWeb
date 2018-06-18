using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.RepairPart
{
    public class RepairPartLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairPartRecord repairPart = new RepairPartRecord();
        RepairPartLoader repairPartLoader = new RepairPartLoader();
        public RepairPartLogic()
        {
            dataRecords.Add(repairPart);
            dataLoader = repairPartLoader;

            BeforeSave += OnBeforeSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairPartId { get { return repairPart.RepairPartId; } set { repairPart.RepairPartId = value; } }
        public string RepairId { get { return repairPart.RepairId; } set { repairPart.RepairId = value; } }
        public string InventoryId { get { return repairPart.InventoryId; } set { repairPart.InventoryId = value; } }
        public string WarehouseId { get { return repairPart.WarehouseId; } set { repairPart.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeColor { get; set; }
        public string Description { get { return repairPart.Description; } set { repairPart.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionColor { get; set; }
        public decimal? Quantity { get { return repairPart.Quantity; } set { repairPart.Quantity = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }
        public decimal? Price { get { return repairPart.Price; } set { repairPart.Price = value; } }
        public decimal? DiscountAmount { get { return repairPart.DiscountAmount; } set { repairPart.DiscountAmount = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Extended { get; set; }
        public bool? Taxable { get { return repairPart.Taxable; } set { repairPart.Taxable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Tax { get; set; }
        public bool? Billable { get { return repairPart.Billable; } set { repairPart.Billable = value; } }
        public string ItemClass { get { return repairPart.ItemClass; } set { repairPart.ItemClass = value; } }
        public string ItemOrder { get { return repairPart.ItemOrder; } set { repairPart.ItemOrder = value; } }
        public string DateStamp { get { return repairPart.DateStamp; } set { repairPart.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                if ((InventoryId != null) && (!InventoryId.Equals(string.Empty)))
                {
                    ItemClass = AppFunc.GetStringDataAsync(AppConfig, "master", "masterid", InventoryId, "class").Result;
                    if ((ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT)) || (ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE)))
                    {
                        RepairPartId = AppFunc.InsertRepairPackage(AppConfig, UserSession, this).Result;
                        e.PerformSave = false;
                    }
                }
                ItemOrder = "";
            }
            else  // updating
            { }
        }
        //------------------------------------------------------------------------------------ 
    }
}
