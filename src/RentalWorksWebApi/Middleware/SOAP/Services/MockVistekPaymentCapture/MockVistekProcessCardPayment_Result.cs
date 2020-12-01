using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVistekPaymentCapture
{
    [MessageContract(WrapperName = "ProcessCardPayment_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", IsWrapped = true)]
    public class MockVistekProcessCardPayment_Result
    {

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 0)]
        public string return_value;

        public MockVistekProcessCardPayment_Result()
        {
        }

        public MockVistekProcessCardPayment_Result(string return_value)
        {
            this.return_value = return_value;
        }
    }
}
