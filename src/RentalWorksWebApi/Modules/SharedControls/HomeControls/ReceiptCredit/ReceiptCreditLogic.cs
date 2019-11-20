using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.ReceiptCredit
{
    public class ReceiptCreditLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptCreditLoader receiptCreditLoader = new ReceiptCreditLoader();
        public ReceiptCreditLogic()
        {
            dataLoader = receiptCreditLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2upfwjjG6NvGM", IsPrimaryKey: true)]
        public string CreditReceiptId { get; set; }
        [FwLogicProperty(Id: "nMOdxMgk9oZIC", IsReadOnly: true)]
        public string ReceiptId { get; set; }
        [FwLogicProperty(Id: "bvlcCDAieORew", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "nCeUJGBM7oeL3", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "UhFfuIfN3TD7G", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "cg2ljdFtI06Wt", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "g2eF3LSkw0qXq", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "5GUJbVBPGy67x", IsReadOnly: true)]
        public string PaymentBy { get; set; }
        [FwLogicProperty(Id: "7cw53Y425nqsd", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "qYZRvuSHlzWN9", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        [FwLogicProperty(Id: "9UB0YEGaTRFO5", IsReadOnly: true)]
        public string RecTypeColor { get; set; }
        [FwLogicProperty(Id: "YETAVL1nyDsfX", IsReadOnly: true)]
        public string ReceiptDate { get; set; }
        [FwLogicProperty(Id: "TIjKFwOhvOmRP", IsReadOnly: true)]
        public string PaymentTypeId { get; set; }
        [FwLogicProperty(Id: "A7CWBuCTbXqSa", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "BgI1wedqPpvGe", IsReadOnly: true)]
        public string CheckNumber { get; set; }
        [FwLogicProperty(Id: "36dEvaaHmT4sp", IsReadOnly: true)]
        public decimal? OrigAmount { get; set; }
        [FwLogicProperty(Id: "Y5gbPLg9pSp5t", IsReadOnly: true)]
        public decimal? Applied { get; set; }
        [FwLogicProperty(Id: "YcXkHPriYQf6y", IsReadOnly: true)]
        public decimal? Refunded { get; set; }
        [FwLogicProperty(Id: "qWCdfwt0McnaQ", IsReadOnly: true)]
        public decimal? Remaining { get; set; }
        [FwLogicProperty(Id: "LvtRHqzLVcKsF", IsReadOnly: true)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
