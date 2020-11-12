using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class ProcessCreditCardRequest
    {
        public string CustomerId { get; set; }
        public string PaymentId { get; set; }
        public string Amount { get; set; }
    }
    public class ProcessCreditCardResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
