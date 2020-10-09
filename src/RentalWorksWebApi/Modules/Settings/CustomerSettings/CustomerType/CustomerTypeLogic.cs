using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerType
{
    [FwLogic(Id:"IY90eV7mTjmf")]
    public class CustomerTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerTypeRecord customerType = new CustomerTypeRecord();
        public CustomerTypeLogic()
        {
            dataRecords.Add(customerType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"xkJe4pHc9xo2", IsPrimaryKey:true)]
        public string CustomerTypeId { get { return customerType.CustomerTypeId; } set { customerType.CustomerTypeId = value; } }

        [FwLogicProperty(Id:"xkJe4pHc9xo2", IsRecordTitle:true)]
        public string CustomerType { get { return customerType.CustomerType; } set { customerType.CustomerType = value; } }

        //[FwLogicProperty(Id:"uuqVpPT8A59J")]
        //public decimal? DefaultRentalDiscountPercent { get { return customerType.DefaultRentalDiscountPercent; } set { customerType.DefaultRentalDiscountPercent = value; } }
        //
        //[FwLogicProperty(Id:"4HPXAo3ZYJ1q")]
        //public decimal? DefaultSalesDiscountPercent { get { return customerType.DefaultSalesDiscountPercent; } set { customerType.DefaultSalesDiscountPercent = value; } }
        //
        //[FwLogicProperty(Id:"J1e7ZaFrW24J")]
        //public decimal? DefaultFacilitiesDiscountPercent { get { return customerType.DefaultFacilitiesDiscountPercent; } set { customerType.DefaultFacilitiesDiscountPercent = value; } }

        [FwLogicProperty(Id:"PvvQcIDlnsHS")]
        public string Color { get { return customerType.Color; } set { customerType.Color = value; } }

        [FwLogicProperty(Id:"8p20GlJYTBhI")]
        public bool? Inactive { get { return customerType.Inactive; } set { customerType.Inactive = value; } }

        [FwLogicProperty(Id:"0S7w78JEGcXq")]
        public string DateStamp { get { return customerType.DateStamp; } set { customerType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
