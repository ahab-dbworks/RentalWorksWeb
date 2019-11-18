using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.MasterWarehouse;

namespace WebApi.Modules.Home.ContainerWarehouse
{
    [FwLogic(Id:"l7jJA9WCoUEl")]
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
        [FwLogicProperty(Id:"LhfHjC7IIsl0", IsPrimaryKey:true)]
        public string InventoryId { get { return containerWarehouse.MasterId; } set { containerWarehouse.MasterId = value; } }

        [FwLogicProperty(Id:"4vVqB2TGdBAI", IsPrimaryKey:true)]
        public string WarehouseId { get { return containerWarehouse.WarehouseId; } set { containerWarehouse.WarehouseId = value; } }

        [FwLogicProperty(Id:"4vVqB2TGdBAI", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"4vVqB2TGdBAI", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"SDx6JLbZ6Phn")]
        public decimal? DailyRate { get { return containerWarehouse.DailyRate; } set { containerWarehouse.DailyRate = value; } }

        [FwLogicProperty(Id:"BCjPtn9dxZ9t")]
        public decimal? WeeklyRate { get { return containerWarehouse.WeeklyRate; } set { containerWarehouse.WeeklyRate = value; } }

        [FwLogicProperty(Id:"4gjJ2MduaERC")]
        public decimal? MonthlyRate { get { return containerWarehouse.MonthlyRate; } set { containerWarehouse.MonthlyRate = value; } }

        [FwLogicProperty(Id:"qvzL48cm6jcn", IsReadOnly:true)]
        public int? Quantity { get; set; }

        [FwLogicProperty(Id:"qvzL48cm6jcn", IsReadOnly:true)]
        public int? QuantityReady { get; set; }

        [FwLogicProperty(Id:"qvzL48cm6jcn", IsReadOnly:true)]
        public int? QuantityIncomplete { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
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
