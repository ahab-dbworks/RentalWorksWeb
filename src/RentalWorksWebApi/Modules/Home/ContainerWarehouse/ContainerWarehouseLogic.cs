using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Home.MasterWarehouse;

namespace WebApi.Modules.Home.ContainerWarehouse
{
    public class ContainerWarehouseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterWarehouseRecord containerWarehouse = new MasterWarehouseRecord();
        ContainerWarehouseLoader containerWarehouseLoader = new ContainerWarehouseLoader();
        public ContainerWarehouseLogic()
        {
            dataRecords.Add(containerWarehouse);
            dataLoader = containerWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return containerWarehouse.MasterId; } set { containerWarehouse.MasterId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return containerWarehouse.WarehouseId; } set { containerWarehouse.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public decimal? DailyRate { get { return containerWarehouse.DailyRate; } set { containerWarehouse.DailyRate = value; } }
        public decimal? WeeklyRate { get { return containerWarehouse.WeeklyRate; } set { containerWarehouse.WeeklyRate = value; } }
        public decimal? MonthlyRate { get { return containerWarehouse.MonthlyRate; } set { containerWarehouse.MonthlyRate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Quantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityReady { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityIncomplete { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                isValid = false;
                validateMsg = "Cannot add new Container Warehouses.";
            }
            return isValid;
        } 
        //------------------------------------------------------------------------------------ 
    }
}
