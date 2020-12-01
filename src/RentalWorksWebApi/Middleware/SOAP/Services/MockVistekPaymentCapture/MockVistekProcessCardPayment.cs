using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVistekPaymentCapture
{
    //[MessageContract(WrapperName = "ProcessCardPayment", WrapperNamespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", IsWrapped = true)]
    //public class MockVistekProcessCardPayment
    //{
    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 0)]
    //    public string pINPadNo;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 1)]
    //    public int transactionTypeOpt;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 2)]
    //    public string amount;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 3)]
    //    public string docRefNo;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 4)]
    //    public string storeCode;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 5)]
    //    public string salespersonCode;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 6)]
    //    public string billToCustomerNo;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 7)]
    //    public string pPaymentRefNo;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 8)]
    //    public string pCardType;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 9)]
    //    public string pCardNo;

    //    [MessageBodyMember(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order = 10)]
    //    public string pAuthCode;

    //    public MockVistekProcessCardPayment()
    //    {
    //    }

    //    public MockVistekProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo,
    //        string pPaymentRefNo, string pCardType, string pCardNo, string pAuthCode)
    //    {
    //        this.pINPadNo = pINPadNo;
    //        this.transactionTypeOpt = transactionTypeOpt;
    //        this.amount = amount;
    //        this.docRefNo = docRefNo;
    //        this.storeCode = storeCode;
    //        this.salespersonCode = salespersonCode;
    //        this.billToCustomerNo = billToCustomerNo;
    //        this.pPaymentRefNo = pPaymentRefNo;
    //        this.pCardType = pCardType;
    //        this.pCardNo = pCardNo;
    //        this.pAuthCode = pAuthCode;
    //    }
    //}
}
