using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.ReceiptInvoice
{
    public class ReceiptInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptInvoiceLoader receiptInvoiceLoader = new ReceiptInvoiceLoader();
        public ReceiptInvoiceLogic()
        {
            dataLoader = receiptInvoiceLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Or3xalP9frqi2")]
        public string InvoiceReceiptId { get; set; } = "";
        [FwLogicProperty(Id: "W3AB8Dojf")]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "9ZQiE6WwE3")]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "7WEJPeiRUj")]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "2YtvSRgj2Q")]
        public string Description { get; set; }
        [FwLogicProperty(Id: "MHqLU9Acx5")]
        public string Status { get; set; }
        [FwLogicProperty(Id: "36GAIjE3D1")]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "cml6fn9mmL")]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "hTVDRe1zTN")]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "FrAqUFbnfr")]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "UcMwFimtat")]
        public decimal? Total { get; set; }
        [FwLogicProperty(Id: "DK5gYIxr1l")]
        public bool? ReceiptId { get; set; }
        [FwLogicProperty(Id: "qeQunubVq3")]
        public string InvoiceType { get; set; }
        [FwLogicProperty(Id: "tirxODa2iF3")]
        public decimal? Applied { get; set; }
        [FwLogicProperty(Id: "4jxXnmSInGL")]
        public decimal? Amount { get; set; }
        [FwLogicProperty(Id: "K31XigZR5Aq")]
        public decimal? Due { get; set; }
        [FwLogicProperty(Id: "VghI6iKeBns")]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "lV7GXEPqxz8")]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "sTi6c72jXpi")]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "xd8tdSpCIpg")]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "hcUWpbiz8Uy")]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "zTPlBz9HAqq")]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "GUkvgcwbRWmdg")]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
