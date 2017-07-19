using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class CustomerStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerStatusRecord customerStatus = new CustomerStatusRecord();
        public CustomerStatusLogic()
        {
            dataRecords.Add(customerStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerStatusId { get { return customerStatus.CustomerStatusId; } set { customerStatus.CustomerStatusId = value; } }
        [FwBusinessLogicField(isTitle: true)]
        public string CustomerStatus { get { return customerStatus.CustomerStatus; } set { customerStatus.CustomerStatus = value; } }
        public string StatusType { get { return customerStatus.StatusType; } set { customerStatus.StatusType = value; } }
        public string CreditStatusId { get { return customerStatus.CreditStatusId; } set { customerStatus.CreditStatusId = value; } }
        public string CreditStatus { get { return customerStatus.CreditStatus; } set { customerStatus.CreditStatus = value; } }
        public string Inactive { get { return customerStatus.Inactive; } set { customerStatus.Inactive = value; } }
        public DateTime? DateStamp { get { return customerStatus.DateStamp; } set { customerStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
