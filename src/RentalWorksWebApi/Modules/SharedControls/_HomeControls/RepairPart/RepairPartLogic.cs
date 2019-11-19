using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Inventory.Repair;
using WebLibrary;

namespace WebApi.Modules.HomeControls.RepairPart
{
    [FwLogic(Id:"J1SmkGfOgm6YD")]
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
        [FwLogicProperty(Id:"M86tUdfhniBmb", IsPrimaryKey:true)]
        public string RepairPartId { get { return repairPart.RepairPartId; } set { repairPart.RepairPartId = value; } }

        [FwLogicProperty(Id:"bfqx21kyALcH")]
        public string RepairId { get { return repairPart.RepairId; } set { repairPart.RepairId = value; } }

        [FwLogicProperty(Id:"Z7sLByEJGlWT")]
        public string InventoryId { get { return repairPart.InventoryId; } set { repairPart.InventoryId = value; } }

        [FwLogicProperty(Id:"ciSwh98dE1zc")]
        public string WarehouseId { get { return repairPart.WarehouseId; } set { repairPart.WarehouseId = value; } }

        [FwLogicProperty(Id:"MCdyzmxuxhGen", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"MCdyzmxuxhGen", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"6KdSde6UtmVtg", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"6KdSde6UtmVtg", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"85xONqI7GFGw")]
        public string Description { get { return repairPart.Description; } set { repairPart.Description = value; } }

        [FwLogicProperty(Id:"OzaYXCNflb6lw", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"n3wv7tvixUaA")]
        public decimal? Quantity { get { return repairPart.Quantity; } set { repairPart.Quantity = value; } }

        [FwLogicProperty(Id:"n9Gxp25PDa4iJ", IsReadOnly:true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id:"aoeYsyY8Gfvj")]
        public decimal? Price { get { return repairPart.Price; } set { repairPart.Price = value; } }

        [FwLogicProperty(Id: "GzplruKXXhb3j", IsReadOnly: true)]
        public decimal? GrossTotal { get; set; }


        [FwLogicProperty(Id:"ZEGh3s4EPeZX")]
        public decimal? DiscountAmount { get { return repairPart.DiscountAmount; } set { repairPart.DiscountAmount = value; } }

        [FwLogicProperty(Id:"84KWvF6TZZvHl", IsReadOnly:true)]
        public decimal? Extended { get; set; }

        [FwLogicProperty(Id:"96XqcDyVEion")]
        public bool? Taxable { get { return repairPart.Taxable; } set { repairPart.Taxable = value; } }

        [FwLogicProperty(Id:"Dip5ICqD4zSAj", IsReadOnly:true)]
        public decimal? Tax { get; set; }

        [FwLogicProperty(Id: "SOiwLgHRRQZhY", IsReadOnly: true)]
        public decimal? Total { get; set; }

        [FwLogicProperty(Id:"MgtwxCUpV2Q1")]
        public bool? Billable { get { return repairPart.Billable; } set { repairPart.Billable = value; } }

        [FwLogicProperty(Id:"niHG6vDFl8QE")]
        public string ItemClass { get { return repairPart.ItemClass; } set { repairPart.ItemClass = value; } }

        [FwLogicProperty(Id:"o3kqZfQtT7de")]
        public string ItemOrder { get { return repairPart.ItemOrder; } set { repairPart.ItemOrder = value; } }

        [FwLogicProperty(Id:"G96C8POf3bCn")]
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
                        RepairPartId = RepairFunc.InsertRepairPackage(AppConfig, UserSession, this).Result;
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
