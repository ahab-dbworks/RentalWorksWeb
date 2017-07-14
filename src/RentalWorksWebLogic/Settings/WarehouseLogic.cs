using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
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
        public string Warehouse { get { return warehouse.Warehouse; } set { warehouse.Warehouse = value; } }
        public string WarehouseCode { get { return warehouse.WarehouseCode; } set { warehouse.WarehouseCode = value; } }
        public string Inactive { get { return warehouse.Inactive; } set { warehouse.Inactive = value; } }
        public DateTime? DateStamp { get { return warehouse.DateStamp; } set { warehouse.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
