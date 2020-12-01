using FwStandard.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class VistekProcessCreditCardService: IProcessCreditCardService
    {
        //FwApplicationConfig AppConfig { get; }
        public HttpClient Client { get; }

        public VistekProcessCreditCardService()
        {
            HttpClient client = new HttpClient();
            this.Client = client;
        }

        public async Task<ProcessCreditCardPaymentResponse> ProcessPaymentAsync(FwApplicationConfig appConfig, ProcessCreditCardPaymentRequest request)
        {
            ProcessCreditCardPaymentResponse result = null;
            int transactionTypeOpt = (int)request.TransactionType;
            if (request.TransactionType == ProcessCreditCardPaymentRequest.TransactionTypes.Sale)
            {
                if (request.PINPadCode.Length == 0) throw new ArgumentException("PINPadeCode is required.");
                if (request.PaymentAmount <= 0) throw new ArgumentException("Payment amount must be greater than 0.");
                if (request.OrderId.Length == 0) throw new ArgumentException("OrderId is required.");
                if (request.StoreCode.Length == 0) throw new ArgumentException("StoreCode is required.");
                if (request.SalesPersonCode.Length == 0) throw new ArgumentException("SalesPersonCode is required.");
                if (request.CustomerNo.Length == 0) throw new ArgumentException("CustomerNo is required.");

                //if (request.PaymentReferenceNo.Length == 0) throw new ArgumentException("PaymentReferenceNo is required.");
                //if (request.CardType.Length == 0) throw new ArgumentException("CardType is required.");
                //if (request.CardNo.Length == 0) throw new ArgumentException("CardNo is required.");
                //if (request.AuthCode.Length == 0) throw new ArgumentException("AuthCode is required.");
            }
            else if (request.TransactionType == ProcessCreditCardPaymentRequest.TransactionTypes.VoidSale)
            {
                throw new Exception("Void Sale type is not supported.");
            }
            else if (request.TransactionType == ProcessCreditCardPaymentRequest.TransactionTypes.Refund)
            {
                if (request.PINPadCode.Length == 0) throw new ArgumentException("PINPadeCode is required.");
                if (request.PaymentAmount <= 0) throw new ArgumentException("Payment amount must be greater than 0.");
                if (request.OrderId.Length == 0) throw new ArgumentException("OrderId is required.");
                if (request.StoreCode.Length == 0) throw new ArgumentException("StoreCode is required.");
                if (request.SalesPersonCode.Length == 0) throw new ArgumentException("SalesPersonCode is required.");
                if (request.CustomerNo.Length == 0) throw new ArgumentException("CustomerNo is required.");

                if (request.PaymentReferenceNo.Length == 0) throw new ArgumentException("PaymentReferenceNo is required.");
                if (request.CardType.Length == 0) throw new ArgumentException("CardType is required.");
                if (request.CardNo.Length == 0) throw new ArgumentException("CardNo is required.");
                if (request.AuthCode.Length == 0) throw new ArgumentException("AuthCode is required.");
            }
            else if (request.TransactionType == ProcessCreditCardPaymentRequest.TransactionTypes.VoidRefund)
            {
                throw new Exception("Void Refund type is not supported.");
            }


            var content = new StringContent(
$@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""urn:microsoft-dynamics-schemas/codeunit/PaymentCapture"">
<soapenv:Header/>
<soapenv:Body>
    <pay:ProcessCardPayment>
        <pay:pINPadNo>{request.PINPadCode}</pay:pINPadNo>
        <pay:transactionTypeOpt>{(int)request.TransactionType}</pay:transactionTypeOpt>
        <pay:amount>{request.PaymentAmount}</pay:amount>
        <pay:docRefNo>{request.OrderId}</pay:docRefNo>
        <pay:storeCode>{request.StoreCode}</pay:storeCode>
        <pay:salespersonCode>{request.SalesPersonCode}</pay:salespersonCode>
        <pay:billToCustomerNo>{request.CustomerNo}</pay:billToCustomerNo>
        <pay:pPaymentRefNo>{request.PaymentReferenceNo}</pay:pPaymentRefNo>
        <pay:pCardType>{request.CardType}</pay:pCardType>
        <pay:pCardNo>{request.CardNo}</pay:pCardNo>
        <pay:pAuthCode>{request.AuthCode}</pay:pAuthCode>
    </pay:ProcessCardPayment>
</soapenv:Body>
</soapenv:Envelope>",
        Encoding.UTF8,
        "text/xml");
            content.Headers.Add("SOAPAction", "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment");
            var response = await this.Client.PostAsync($"{appConfig.PublicBaseUrl}MockVistekProcessCardPayment.svc", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            //response.EnsureSuccessStatusCode();
            string return_value = string.Empty;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(responseStream))
            {
                string responseString = reader.ReadToEnd();
                Regex regexReturnValue = new Regex(@"<\w*:*return_value>(.*)</\w*:*return_value>");
                Match match = regexReturnValue.Match(responseString);
                if (match.Groups.Count > 1)
                {
                    return_value = match.Groups[1].Value;
                    result = new ProcessCreditCardPaymentResponse();
                    result.Status = "SUCCESS";
                    result.ReturnValue = return_value;
                }
                else
                {
                    throw new Exception("Unable to parse response:\n" + responseString);
                }
            }
            return result;
        }   
    }


    //public class VisitekProcessCreditCardService : IProcessCreditCardService
    //{
    //    public VisitekProcessCreditCardService()
    //    {

    //    }

    //    public async Task<ProcessCreditCardResponse> ProcessPaymentAsync(FwApplicationConfig appConfig, ProcessCreditCardRequest request)
    //    {
    //        ProcessCreditCardResponse result = null;
    //        try
    //        {
    //            var processCardPayment = new VisitekPaymentCaptureService.ProcessCardPayment();
    //            processCardPayment.billToCustomerNo = request.CustomerId;
    //            processCardPayment.docRefNo = request.PaymentId;
    //            processCardPayment.amount = request.Amount;
    //            var portClient = new VisitekPaymentCaptureService.PaymentCapture_PortClient(VisitekPaymentCaptureService.PaymentCapture_PortClient.EndpointConfiguration.PaymentCapture_Port,
    //                //"http://10.1.8.103:8047/TEST_NAV80/WS/Vistek%20Live/Codeunit/PaymentCapture");
    //                //"http://localhost:8088/mockPaymentCapture_Binding");
    //                $"{appConfig.PublicBaseUrl}MockVisitekProcessCardPayment.svc");
    //            //string pINPadNo = processCardPayment.pINPadNo;
    //            //int transactionTypeOpt = processCardPayment.transactionTypeOpt;
    //            //string amount = processCardPayment.amount;
    //            //string docRefNo = processCardPayment.docRefNo;
    //            //string storeCode = processCardPayment.storeCode;
    //            //string salespersonCode = processCardPayment.salespersonCode;
    //            //string billToCustomerNo = processCardPayment.billToCustomerNo;
    //            string pINPadNo = "1";
    //            int transactionTypeOpt = 1;
    //            string amount = "1";
    //            string docRefNo = "1";
    //            string storeCode = "1";
    //            string salespersonCode = "1";
    //            string billToCustomerNo = "1";
    //            VisitekPaymentCaptureService.ProcessCardPayment_Result processCardPaymentResult = await portClient.ProcessCardPaymentAsync(
    //                pINPadNo: pINPadNo, 
    //                transactionTypeOpt: 
    //                transactionTypeOpt, 
    //                amount: amount, 
    //                docRefNo: docRefNo,
    //                storeCode: storeCode,
    //                salespersonCode: salespersonCode,
    //                billToCustomerNo: billToCustomerNo);
    //            //Console.WriteLine(processCardPaymentResult.return_value);
    //            result = new ProcessCreditCardResponse();
    //            result.Status = "SUCCESS";
    //            result.Message = processCardPaymentResult.return_value;
    //        } 
    //        catch(Exception ex)
    //        {
    //            result = new ProcessCreditCardResponse();
    //            result.Status = "ERROR";
    //            result.Message = ex.Message + ex.StackTrace;
    //        }
    //        return result;
    //    }
    //}
}
