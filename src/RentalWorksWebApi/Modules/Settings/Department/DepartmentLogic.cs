using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Department
{
    public class DepartmentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DepartmentRecord department = new DepartmentRecord();
        DepartmentLoader departmentLoader = new DepartmentLoader();

        public DepartmentLogic()
        {
            dataRecords.Add(department);
            dataLoader = departmentLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DepartmentId { get { return department.DepartmentId; } set { department.DepartmentId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Department { get { return department.Department; } set { department.Department = value; } }
        public string DepartmentCode { get { return department.DepartmentCode; } set { department.DepartmentCode = value; } }
        public string DivisionCode { get { return department.DivisionCode; } set { department.DivisionCode = value; } }
        public bool? DisableEditingRentalRate { get { return department.DisableEditingRentalRate; } set { department.DisableEditingRentalRate = value; } }
        public bool? DisableEditingSalesRate { get { return department.DisableEditingSalesRate; } set { department.DisableEditingSalesRate = value; } }
        public bool? DisableEditingMiscellaneousRate { get { return department.DisableEditingMiscellaneousRate; } set { department.DisableEditingMiscellaneousRate = value; } }
        public bool? DisableEditingLaborRate { get { return department.DisableEditingLaborRate; } set { department.DisableEditingLaborRate = value; } }
        public bool? DisableEditingUsedSaleRate { get { return department.DisableEditingUsedSaleRate; } set { department.DisableEditingUsedSaleRate = value; } }
        public bool? DisableEditingLossAndDamageRate { get { return department.DisableEditingLossAndDamageRate; } set { department.DisableEditingLossAndDamageRate = value; } }
        public string ExportCode { get { return department.ExportCode; } set { department.ExportCode = value; } }
        public bool? Inactive { get { return department.Inactive; } set { department.Inactive = value; } }
        public string DateStamp { get { return department.DateStamp; } set { department.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
