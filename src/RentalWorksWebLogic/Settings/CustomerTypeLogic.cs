using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class CustomerTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerTypeRecord customerType = new CustomerTypeRecord();
        public CustomerTypeLogic()
        {
            dataRecords.Add(customerType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerTypeId { get { return customerType.CustomerTypeId; } set { customerType.CustomerTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string CustomerType { get { return customerType.CustomerType; } set { customerType.CustomerType = value; } }
        public decimal DefaultRentalDiscountPercent { get { return customerType.DefaultRentalDiscountPercent; } set { customerType.DefaultRentalDiscountPercent = value; } }
        public decimal DefaultSalesDiscountPercent { get { return customerType.DefaultSalesDiscountPercent; } set { customerType.DefaultSalesDiscountPercent = value; } }
        public decimal DefaultFacilitiesDiscountPercent { get { return customerType.DefaultFacilitiesDiscountPercent; } set { customerType.DefaultFacilitiesDiscountPercent = value; } }
        public bool Inactive { get { return customerType.Inactive; } set { customerType.Inactive = value; } }
        public string DateStamp { get { return customerType.DateStamp; } set { customerType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
