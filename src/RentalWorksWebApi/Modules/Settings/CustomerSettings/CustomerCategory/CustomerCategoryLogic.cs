using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerCategory
{
    [FwLogic(Id:"eyMpRjUk88li")]
    public class CustomerCategoryLogic : AppBusinessLogic
    {
        CustomerCategoryRecord customerCat = new CustomerCategoryRecord();
        public CustomerCategoryLogic()
        {
            dataRecords.Add(customerCat); 
        }

        [FwLogicProperty(Id:"24Fl2k3pNfhP", IsPrimaryKey:true)]
        public string CustomerCategoryId { get { return customerCat.CustomerCategoryId; } set { customerCat.CustomerCategoryId = value; } }

        [FwLogicProperty(Id:"24Fl2k3pNfhP", IsRecordTitle:true)]
        public string CustomerCategory { get { return customerCat.CustomerCategory; } set { customerCat.CustomerCategory = value; } }

        [FwLogicProperty(Id:"yBOb76pNm01e")]
        public string DateStamp { get { return customerCat.DateStamp; } set { customerCat.DateStamp = value; } }

        [FwLogicProperty(Id:"gYWfo069zL1w")]
        public bool? Inactive { get { return customerCat.Inactive; } set { customerCat.Inactive = value; } }



    }
}
