using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Warehouse
{
    public class WarehouseLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WarehouseRecord warehouse = new WarehouseRecord();
        public WarehouseLogic()
        {
            dataRecords.Add(warehouse);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return warehouse.WarehouseId; } set { warehouse.WarehouseId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Warehouse { get { return warehouse.Warehouse; } set { warehouse.Warehouse = value; } }
        public string WarehouseCode { get { return warehouse.WarehouseCode; } set { warehouse.WarehouseCode = value; } }
        public bool Inactive { get { return warehouse.Inactive; } set { warehouse.Inactive = value; } }
        public string DateStamp { get { return warehouse.DateStamp; } set { warehouse.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
