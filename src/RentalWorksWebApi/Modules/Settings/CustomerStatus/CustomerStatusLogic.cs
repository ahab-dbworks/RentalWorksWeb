using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CustomerStatus
{
    [FwLogic(Id:"B1jmkkVoHzEo")]
    public class CustomerStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerStatusRecord customerStatus = new CustomerStatusRecord();
        CustomerStatusLoader customerStatusLoader = new CustomerStatusLoader();
        public CustomerStatusLogic()
        {
            dataRecords.Add(customerStatus);
            dataLoader = customerStatusLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"UGKG9VZIk6zN", IsPrimaryKey:true)]
        public string CustomerStatusId { get { return customerStatus.CustomerStatusId; } set { customerStatus.CustomerStatusId = value; } }

        [FwLogicProperty(Id:"UGKG9VZIk6zN", IsRecordTitle:true)]
        public string CustomerStatus { get { return customerStatus.CustomerStatus; } set { customerStatus.CustomerStatus = value; } }

        [FwLogicProperty(Id:"LoWF51KMsoVr")]
        public string StatusType { get { return customerStatus.StatusType; } set { customerStatus.StatusType = value; } }

        [FwLogicProperty(Id:"rufxEpjPoXA1")]
        public string CreditStatusId { get { return customerStatus.CreditStatusId; } set { customerStatus.CreditStatusId = value; } }

        [FwLogicProperty(Id:"MbboE8GYiywa", IsReadOnly:true)]
        public string CreditStatus { get; set; }

        [FwLogicProperty(Id:"El5NVVd1Glzy")]
        public bool? Inactive { get { return customerStatus.Inactive; } set { customerStatus.Inactive = value; } }

        [FwLogicProperty(Id:"uSA0QkTC0JM4")]
        public string DateStamp { get { return customerStatus.DateStamp; } set { customerStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
