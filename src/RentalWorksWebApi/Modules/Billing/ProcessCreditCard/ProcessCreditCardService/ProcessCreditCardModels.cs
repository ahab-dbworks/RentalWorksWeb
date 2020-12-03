using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class ProcessCreditCardPaymentRequest
    {
        public enum TransactionTypes { Sale = 0, VoidSale = 1, Refund = 2, VoidRefund = 3 }
        public TransactionTypes TransactionType { get; set; } = TransactionTypes.Sale;
        public string PINPadCode { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; } = 0;
        public string OrderId { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;
        public string SalesPersonCode { get; set; } = string.Empty;
        public string CustomerNo { get; set; } = string.Empty;
        public string PaymentReferenceNo { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string CardNo { get; set; } = string.Empty;
        public string AuthCode { get; set; } = string.Empty;

    }
    public class ProcessCreditCardPaymentResponse
    {
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public string CardEntryMode { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string AuthorizationCode { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
    }


    public class ProcessCreditCardRefundRequest
    {
        public string PINPadCode { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; } = 0;
        public string OrderId { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;
        public string SalesPersonCode { get; set; } = string.Empty;
        public string CustomerNo { get; set; } = string.Empty;
        public string PaymentReferenceNo { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string CardNo { get; set; } = string.Empty;
        public string AuthCode { get; set; } = string.Empty;
    }
}
