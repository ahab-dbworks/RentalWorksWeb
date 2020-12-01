using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Modules.Utilities.InventoryRetireUtility;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public interface IProcessCreditCardService
    {
        Task<ProcessCreditCardPaymentResponse> ProcessPaymentAsync(FwApplicationConfig appConfig, ProcessCreditCardPaymentRequest request);
    }
}
