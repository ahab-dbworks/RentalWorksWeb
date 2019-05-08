using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.Department
{
    [FwLogic(Id: "GQ73L8rlABp3")]
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
        [FwLogicProperty(Id: "DUdZQbFK8WTj", IsPrimaryKey: true)]
        public string DepartmentId { get { return department.DepartmentId; } set { department.DepartmentId = value; } }

        [FwLogicProperty(Id: "DUdZQbFK8WTj", IsRecordTitle: true)]
        public string Department { get { return department.Department; } set { department.Department = value; } }

        [FwLogicProperty(Id: "8BKMdiXmFpZY")]
        public string DepartmentCode { get { return department.DepartmentCode; } set { department.DepartmentCode = value; } }

        [FwLogicProperty(Id: "8xfpTZtshYL2")]
        public string DivisionCode { get { return department.DivisionCode; } set { department.DivisionCode = value; } }

        [FwLogicProperty(Id: "ZvZeip9cKpPa")]
        public bool? DisableEditingRentalRate { get { return department.DisableEditingRentalRate; } set { department.DisableEditingRentalRate = value; } }

        [FwLogicProperty(Id: "CVAkWSARSzoX")]
        public bool? DisableEditingSalesRate { get { return department.DisableEditingSalesRate; } set { department.DisableEditingSalesRate = value; } }

        [FwLogicProperty(Id: "fomAfTN1rdfc")]
        public bool? DisableEditingMiscellaneousRate { get { return department.DisableEditingMiscellaneousRate; } set { department.DisableEditingMiscellaneousRate = value; } }

        [FwLogicProperty(Id: "YjIILwvzZlHb")]
        public bool? DisableEditingLaborRate { get { return department.DisableEditingLaborRate; } set { department.DisableEditingLaborRate = value; } }

        [FwLogicProperty(Id: "2Xy7MQQLWxrb")]
        public bool? DisableEditingUsedSaleRate { get { return department.DisableEditingUsedSaleRate; } set { department.DisableEditingUsedSaleRate = value; } }

        [FwLogicProperty(Id: "B3HXfWCe5V6X")]
        public bool? DisableEditingLossAndDamageRate { get { return department.DisableEditingLossAndDamageRate; } set { department.DisableEditingLossAndDamageRate = value; } }

        [FwLogicProperty(Id: "UrRvRz8p9gpF")]
        public string ExportCode { get { return department.ExportCode; } set { department.ExportCode = value; } }

        [FwLogicProperty(Id: "f2B3N7ZxyeQDK")]
        public string SalesBillingRule { get { return department.SalesBillingMode; } set { department.SalesBillingMode = value; } }

        [FwLogicProperty(Id: "1StRszlOLntU0")]
        public bool? LockLineItemsWhenCustomDiscountUsed { get { return department.LockWhenCustomDiscount; } set { department.LockWhenCustomDiscount = value; } }

        [FwLogicProperty(Id: "FFuRRpmCbV1H")]
        public bool? Inactive { get { return department.Inactive; } set { department.Inactive = value; } }

        [FwLogicProperty(Id: "QAAcYaxHJPno")]
        public string DateStamp { get { return department.DateStamp; } set { department.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (isValid)
            {
                PropertyInfo property = typeof(DepartmentLogic).GetProperty(nameof(DepartmentLogic.SalesBillingRule));
                string[] acceptableValues = {
                                             RwConstants.DEPARTMENT_SALES_BILLING_RULE_BILL_WHEN_STAGED,
                                             RwConstants.DEPARTMENT_SALES_BILLING_RULE_BILL_WHEN_CHECKED_OUT,
                                             RwConstants.DEPARTMENT_SALES_BILLING_RULE_BILL_ON_CONTRACT_BILLING_START_DATE
                                             };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;

        }
        //------------------------------------------------------------------------------------
    }

}
