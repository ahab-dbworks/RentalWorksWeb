using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.DepartmentLocation
{
    [FwLogic(Id:"9BmzHes2zYLL")]
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
        [FwLogicProperty(Id:"0KPb2rZ3L9TF", IsPrimaryKey:true)]
        public string DepartmentId { get { return departmentLocation.DepartmentId; } set { departmentLocation.DepartmentId = value; } }

        [FwLogicProperty(Id:"HclbWoQcVkCu", IsPrimaryKey:true)]
        public string LocationId { get { return departmentLocation.LocationId; } set { departmentLocation.LocationId = value; } }

        [FwLogicProperty(Id:"0KPb2rZ3L9TF", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"HclbWoQcVkCu", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"QPgJX5894rHr")]
        public string DefaultOrderTypeId { get { return departmentLocation.DefaultOrderTypeId; } set { departmentLocation.DefaultOrderTypeId = value; } }

        [FwLogicProperty(Id:"YFYXRgDBZTJ1", IsReadOnly:true)]
        public string DefaultOrderType { get; set; }

        [FwLogicProperty(Id:"hxJ4GGcsvgEX")]
        public bool? Inactive { get { return departmentLocation.Inactive; } set { departmentLocation.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
