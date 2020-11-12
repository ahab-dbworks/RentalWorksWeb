using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class VisitekProcessCreditCardService : IProcessCreditCardService
    {
        public VisitekProcessCreditCardService()
        {

        }

        public async Task<ProcessCreditCardResponse> ProcessPaymentAsync(ProcessCreditCardRequest request)
        {
            ProcessCreditCardResponse result = null;
            try
            {
                var processCardPayment = new VisitekPaymentCaptureService.ProcessCardPayment();
                processCardPayment.billToCustomerNo = request.CustomerId;
                processCardPayment.docRefNo = request.PaymentId;
                processCardPayment.amount = request.Amount;
                VisitekPaymentCaptureService.PaymentCapture_PortClient.EndpointConfiguration endpointConfiguration = VisitekPaymentCaptureService.PaymentCapture_PortClient.EndpointConfiguration.PaymentCapture_Port;
                VisitekPaymentCaptureService.PaymentCapture_PortClient portClient = new VisitekPaymentCaptureService.PaymentCapture_PortClient(endpointConfiguration,
                    //"http://10.1.8.103:8047/TEST_NAV80/WS/Vistek%20Live/Codeunit/PaymentCapture");
                    "http://MIKELAPTOP2:8088/mockPaymentCapture_Binding");
                string pINPadNo = processCardPayment.pINPadNo;
                int transactionTypeOpt = processCardPayment.transactionTypeOpt;
                string amount = processCardPayment.amount;
                string docRefNo = processCardPayment.docRefNo;
                string storeCode = processCardPayment.storeCode;
                string salespersonCode = processCardPayment.salespersonCode;
                string billToCustomerNo = processCardPayment.billToCustomerNo;
                VisitekPaymentCaptureService.ProcessCardPayment_Result processCardPaymentResult = await portClient.ProcessCardPaymentAsync(
                    pINPadNo: pINPadNo, 
                    transactionTypeOpt: 
                    transactionTypeOpt, 
                    amount: amount, 
                    docRefNo: docRefNo,
                    storeCode: storeCode,
                    salespersonCode: salespersonCode,
                    billToCustomerNo: billToCustomerNo);
                //Console.WriteLine(processCardPaymentResult.return_value);
                result = new ProcessCreditCardResponse();
                result.Status = "SUCCESS";
                result.Message = processCardPaymentResult.return_value;
            } 
            catch(Exception ex)
            {
                result = new ProcessCreditCardResponse();
                result.Status = "ERROR";
                result.Message = ex.Message + ex.StackTrace;
            }
            return result;
        }
    }
}
