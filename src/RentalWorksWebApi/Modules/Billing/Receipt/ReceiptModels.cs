using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.Receipt
{
    public class AddDepletingDepositRequest
    {
        public string OrderId = string.Empty;
        public string DealId = string.Empty;
        public string PaymentTypeId = string.Empty;
        public decimal PaymentAmount = 0.0m;
        public DateTime ReceiptDate;
        public string LocationId = string.Empty;
        public string CurrencyId = string.Empty;
        public string CheckNumber = string.Empty;
        public string PaymentMemo = string.Empty;
    }
    public class RefundRequest
    {
        public string ReceiptId = string.Empty;
        public string DealId = string.Empty;
        public string OrderId = string.Empty;
        public decimal RefundAmount = 0.0m;
    }

}
