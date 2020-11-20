using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVisitekPaymentCapture
{
    public class MockVisitekPaymentCaptureService : IMockVisitekPaymentCaptureService
    {
        //public MockVisitekProcessCardPayment_Result ProcessCardPayment(MockVisitekProcessCardPayment ProcessCardPayment)
        //{
        //    MockVisitekProcessCardPayment_Result result = new MockVisitekProcessCardPayment_Result();
        //    result.return_value = "APPROVED";
        //    return result;
        //}
        public MockVisitekProcessCardPayment_Result ProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo)
        {
            MockVisitekProcessCardPayment_Result result = new MockVisitekProcessCardPayment_Result();
            result.return_value = "APPROVED";
            return result;
        }
    }

    [ServiceContract(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", ConfigurationName = "VisitekPaymentCaptureService.PaymentCapture_Port")]
    public interface IMockVisitekPaymentCaptureService
    {
        [OperationContract(Action = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment", ReplyAction = "*")]
        //MockVisitekProcessCardPayment_Result ProcessCardPayment(MockVisitekProcessCardPayment ProcessCardPayment);
        MockVisitekProcessCardPayment_Result ProcessCardPayment(
            string pINPadNo,
            int transactionTypeOpt,
            string amount,
            string docRefNo,
            string storeCode,
            string salespersonCode,
            string billToCustomerNo);
    }
}
