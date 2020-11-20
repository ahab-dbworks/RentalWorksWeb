using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVisitekPaymentCapture
{
    [MessageContract(WrapperName = "ProcessCardPayment", WrapperNamespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", IsWrapped = true)]
    public class MockVisitekProcessCardPayment
    {
        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 0)]
        public string pINPadNo;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 1)]
        public int transactionTypeOpt;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 2)]
        public string amount;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 3)]
        public string docRefNo;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 4)]
        public string storeCode;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 5)]
        public string salespersonCode;

        [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 6)]
        public string billToCustomerNo;

        public MockVisitekProcessCardPayment()
        {
        }

        public MockVisitekProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo)
        {
            this.pINPadNo = pINPadNo;
            this.transactionTypeOpt = transactionTypeOpt;
            this.amount = amount;
            this.docRefNo = docRefNo;
            this.storeCode = storeCode;
            this.salespersonCode = salespersonCode;
            this.billToCustomerNo = billToCustomerNo;
        }
    }
}
