﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class CreditCardRefundRequest
    {
        [Required, MinLength(1)]
        public string ReceiptId = string.Empty;
        [Required, MinLength(1)]
        public string PINPad_Code = string.Empty;
        [Required, MinLength(1)]
        public decimal RefundAmount = 0.0m;
    }
    public class CreditCardDepletingDepositRequest
    {
        public string OrderId = string.Empty;
        public decimal AmountToPay = 0.0m;
        public string PINPad_Code = string.Empty;
        public string DealNumber = string.Empty;
    }


}