using FwStandard.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard
{
    //------------------------------------------------------------------------------------ 
    public interface IProcessCreditCardPlugin
    {
        void SetDependencies(FwApplicationConfig appConfig, FwUserSession userSession, ProcessCreditCardLogic processCreditCardLogic);
        Task<ProcessCreditCardPaymentResponse> ProcessPaymentAsync(ProcessCreditCardPaymentRequest request);
    }
    //------------------------------------------------------------------------------------ 
}
