using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    public class WarehouseDepartmentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseDepartmentRecord warehouseDepartment = new WarehouseDepartmentRecord();
        WarehouseDepartmentLoader warehouseDepartmentLoader = new WarehouseDepartmentLoader();
        public WarehouseDepartmentLogic()
        {
            dataRecords.Add(warehouseDepartment);
            dataLoader = warehouseDepartmentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return warehouseDepartment.WarehouseId; } set { warehouseDepartment.WarehouseId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DepartmentId { get { return warehouseDepartment.DepartmentId; } set { warehouseDepartment.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string RentalBarCodeRangeId { get { return warehouseDepartment.RentalBarCodeRangeId; } set { warehouseDepartment.RentalBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalBarCodeRange { get; set; }
        public string SalesBarCodeRangeId { get { return warehouseDepartment.SalesBarCodeRangeId; } set { warehouseDepartment.SalesBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesBarCodeRange { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderBy { get; set; }
        public string RequestToId { get { return warehouseDepartment.RequestToId; } set { warehouseDepartment.RequestToId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RequestTo { get; set; }
        public string DateStamp { get { return warehouseDepartment.DateStamp; } set { warehouseDepartment.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}