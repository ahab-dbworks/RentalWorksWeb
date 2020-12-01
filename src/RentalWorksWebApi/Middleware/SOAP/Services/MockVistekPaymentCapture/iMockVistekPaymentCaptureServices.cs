using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVistekPaymentCapture
{
    [ServiceContract(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", ConfigurationName = "VisitekPaymentCaptureService.PaymentCapture_Port")]
    public interface IMockVistekPaymentCaptureService
    {
        [OperationContract(Action = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment", ReplyAction = "*")]
        Task<MockVistekProcessCardPayment_Result> ProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo, string pPaymentRefNo, string pCardType, string pCardNo, string pAuthCode);
    }
}
