using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    [FwLogic(Id:"xK8lK5QlO5NBr")]
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
        [FwLogicProperty(Id:"rUq1oh6YVMB0X", IsPrimaryKey:true)]
        public string WarehouseId { get { return warehouseDepartment.WarehouseId; } set { warehouseDepartment.WarehouseId = value; } }

        [FwLogicProperty(Id:"rUq1oh6YVMB0X", IsPrimaryKey:true)]
        public string DepartmentId { get { return warehouseDepartment.DepartmentId; } set { warehouseDepartment.DepartmentId = value; } }

        [FwLogicProperty(Id:"rUq1oh6YVMB0X", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"lgt7H3c4sSA")]
        public string RentalBarCodeRangeId { get { return warehouseDepartment.RentalBarCodeRangeId; } set { warehouseDepartment.RentalBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"pa3mFYkL9crYl", IsReadOnly:true)]
        public string RentalBarCodeRange { get; set; }

        [FwLogicProperty(Id:"T7wDn9mcJ1L")]
        public string SalesBarCodeRangeId { get { return warehouseDepartment.SalesBarCodeRangeId; } set { warehouseDepartment.SalesBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"kZQtESJtlpVd8", IsReadOnly:true)]
        public string SalesBarCodeRange { get; set; }

        [FwLogicProperty(Id:"pf2dGrF0o8zV9", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"yAhP7vkVI7k")]
        public string RequestToId { get { return warehouseDepartment.RequestToId; } set { warehouseDepartment.RequestToId = value; } }

        [FwLogicProperty(Id:"21TJNwE8K9NVm", IsReadOnly:true)]
        public string RequestTo { get; set; }

        [FwLogicProperty(Id:"3TZFJyCE8zY")]
        public string DateStamp { get { return warehouseDepartment.DateStamp; } set { warehouseDepartment.DateStamp = value; } }


        //------------------------------------------------------------------------------------ 
    }
}
