using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class ProcessCreditCardRequest
    {
        public string PINPad_Code { get; set; } = string.Empty;
        public string Payment_AmountToPay { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;
        public string SalesPersonCode { get; set; } = string.Empty;
        public string CustomerNo { get; set; } = string.Empty;
    }
    public class ProcessCreditCardResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
