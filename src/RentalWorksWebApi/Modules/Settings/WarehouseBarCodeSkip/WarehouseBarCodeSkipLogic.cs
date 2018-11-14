using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseBarCodeSkip
{
    [FwLogic(Id:"zpHZknPKDKQCZ")]
    public class WarehouseBarCodeSkipLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseBarCodeSkipRecord warehouseBarCodeSkip = new WarehouseBarCodeSkipRecord();
        public WarehouseBarCodeSkipLogic()
        {
            dataRecords.Add(warehouseBarCodeSkip);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"xePnvIIBR6dDI", IsPrimaryKey:true)]
        public string WarehouseBarCodeSkipId { get { return warehouseBarCodeSkip.WarehouseBarCodeSkipId; } set { warehouseBarCodeSkip.WarehouseBarCodeSkipId = value; } }

        [FwLogicProperty(Id:"GsFG86gyYO8")]
        public string WarehouseId { get { return warehouseBarCodeSkip.WarehouseId; } set { warehouseBarCodeSkip.WarehouseId = value; } }

        [FwLogicProperty(Id:"xePnvIIBR6dDI", IsRecordTitle:true)]
        public string WarehouseBarCodeSkip { get { return warehouseBarCodeSkip.WarehouseBarCodeSkip; } set { warehouseBarCodeSkip.WarehouseBarCodeSkip = value; } }

        [FwLogicProperty(Id:"rm1cw0Zgwr6")]
        public string DateStamp { get { return warehouseBarCodeSkip.DateStamp; } set { warehouseBarCodeSkip.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
