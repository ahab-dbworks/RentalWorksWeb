using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseBarCodeSkip
{
    public class WarehouseBarCodeSkipLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseBarCodeSkipRecord warehouseBarCodeSkip = new WarehouseBarCodeSkipRecord();
        public WarehouseBarCodeSkipLogic()
        {
            dataRecords.Add(warehouseBarCodeSkip);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseBarCodeSkipId { get { return warehouseBarCodeSkip.WarehouseBarCodeSkipId; } set { warehouseBarCodeSkip.WarehouseBarCodeSkipId = value; } }
        public string WarehouseId { get { return warehouseBarCodeSkip.WarehouseId; } set { warehouseBarCodeSkip.WarehouseId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WarehouseBarCodeSkip { get { return warehouseBarCodeSkip.WarehouseBarCodeSkip; } set { warehouseBarCodeSkip.WarehouseBarCodeSkip = value; } }
        public string DateStamp { get { return warehouseBarCodeSkip.DateStamp; } set { warehouseBarCodeSkip.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}