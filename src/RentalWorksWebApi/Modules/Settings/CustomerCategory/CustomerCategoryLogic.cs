using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CustomerCategory
{
    public class CustomerCategoryLogic : AppBusinessLogic
    {
        CustomerCategoryRecord customerCat = new CustomerCategoryRecord();
        public CustomerCategoryLogic()
        {
            dataRecords.Add(customerCat); 
        }

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerCategoryId { get { return customerCat.CustomerCategoryId; } set { customerCat.CustomerCategoryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string CustomerCategory { get { return customerCat.CustomerCategory; } set { customerCat.CustomerCategory = value; } }
        public string DateStamp { get { return customerCat.DateStamp; } set { customerCat.DateStamp = value; } }
        public bool? Inactive { get { return customerCat.Inactive; } set { customerCat.Inactive = value; } }


    }
}
