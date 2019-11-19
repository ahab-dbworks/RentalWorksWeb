using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.CustomerCredit
{
    [FwLogic(Id: "iiJYCRStcCWY")]
    public class CustomerCreditLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomerCreditLoader customerCreditLoader = new CustomerCreditLoader();
        public CustomerCreditLogic()
        {
            dataLoader = customerCreditLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "l5QA0ZPU7Uyk", IsReadOnly: true)]
        public string ReceiptId { get; set; }
        [FwLogicProperty(Id: "9PL8omNVOBHd", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "u93EzstfhLDB", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "9D4nkcfnARKmA", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "Rv9xfAZtucPL", IsReadOnly: true)]
        public string PaymentBy { get; set; }
        [FwLogicProperty(Id: "szhNWHl21DSR", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "Vj2mB0kcPRKO", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        [FwLogicProperty(Id: "jFBqsTZO17oe6", IsReadOnly: true)]
        public string RecTypeColor { get; set; }
        [FwLogicProperty(Id: "APYrSIRpiG83", IsReadOnly: true)]
        public string ReceiptDate { get; set; }
        [FwLogicProperty(Id: "jzcqxFLjZOY0", IsReadOnly: true)]
        public string PaymentTypeId { get; set; }
        [FwLogicProperty(Id: "GT95vy8ePfdy", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "dCVMQp6yKJQJ", IsReadOnly: true)]
        public string CheckNumber { get; set; }
        [FwLogicProperty(Id: "LApP8tmftojK", IsReadOnly: true)]
        public decimal? Amount { get; set; }
        [FwLogicProperty(Id: "YQ2QuTE3YdQu", IsReadOnly: true)]
        public decimal? Applied { get; set; }
        [FwLogicProperty(Id: "Enc9RXh2l890", IsReadOnly: true)]
        public decimal? Refunded { get; set; }
        [FwLogicProperty(Id: "tUQqyq026q6J", IsReadOnly: true)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
