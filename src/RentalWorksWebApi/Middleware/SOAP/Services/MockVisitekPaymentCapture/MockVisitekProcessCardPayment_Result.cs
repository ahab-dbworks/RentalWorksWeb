using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVisitekPaymentCapture
{
    [MessageContract(WrapperName = "ProcessCardPayment_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", IsWrapped = true)]
    public class MockVisitekProcessCardPayment_Result
    {

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 0)]
        public string return_value;

        public MockVisitekProcessCardPayment_Result()
        {
        }

        public MockVisitekProcessCardPayment_Result(string return_value)
        {
            this.return_value = return_value;
        }
    }
}
