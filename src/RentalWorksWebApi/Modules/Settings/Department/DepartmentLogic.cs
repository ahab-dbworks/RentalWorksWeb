using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Department
{
    public class DepartmentLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DepartmentRecord department = new DepartmentRecord();
        public DepartmentLogic()
        {
            dataRecords.Add(department);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DepartmentId { get { return department.DepartmentId; } set { department.DepartmentId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Department { get { return department.Department; } set { department.Department = value; } }
        public string DepartmentCode { get { return department.DepartmentCode; } set { department.DepartmentCode = value; } }
        public string DivisionCode { get { return department.DivisionCode; } set { department.DivisionCode = value; } }
        public bool Inactive { get { return department.Inactive; } set { department.Inactive = value; } }
        public string DateStamp { get { return department.DateStamp; } set { department.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
