using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class ProcessCreditCardRequest
    {
        public string PINPadCode { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; } = 0;
        public string OrderId { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;
        public string SalesPersonCode { get; set; } = string.Empty;
        public string CustomerNo { get; set; } = string.Empty;
    }
    public class ProcessCreditCardResponse
    {
        public string Status { get; set; } = string.Empty;
        public string ReturnValue { get; set; } = string.Empty;
    }
}
