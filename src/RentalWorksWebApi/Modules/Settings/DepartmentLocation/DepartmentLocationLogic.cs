using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.DepartmentLocation
{
    public class DepartmentLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DepartmentLocationRecord departmentLocation = new DepartmentLocationRecord();
        DepartmentLocationLoader departmentLocationLoader = new DepartmentLocationLoader();
        public DepartmentLocationLogic()
        {
            dataRecords.Add(departmentLocation);
            dataLoader = departmentLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DepartmentId { get { return departmentLocation.DepartmentId; } set { departmentLocation.DepartmentId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string LocationId { get { return departmentLocation.LocationId; } set { departmentLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string DefaultOrderTypeId { get { return departmentLocation.DefaultOrderTypeId; } set { departmentLocation.DefaultOrderTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultOrderType { get; set; }
        public bool? Inactive { get { return departmentLocation.Inactive; } set { departmentLocation.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
