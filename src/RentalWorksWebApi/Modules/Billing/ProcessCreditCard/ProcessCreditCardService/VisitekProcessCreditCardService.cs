using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebApi.Middleware.SOAP.Services.MockVisitekPaymentCapture;

namespace WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService
{
    public class VisitekProcessCreditCardService: IProcessCreditCardService
    {
        //FwApplicationConfig AppConfig { get; }
        public HttpClient Client { get; }

        public VisitekProcessCreditCardService()
        {
            //this.AppConfig = appConfig;
            //client.BaseAddress = new Uri($"{appConfig.PublicBaseUrl}");
            //client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            HttpClient client = new HttpClient();
            this.Client = client;
        }

        public async Task<ProcessCreditCardResponse> ProcessPaymentAsync(FwApplicationConfig appConfig, ProcessCreditCardRequest request)
        {
            ProcessCreditCardResponse result = null;
            try
            {
                string pINPadNo = request.PINPadCode;
                int transactionTypeOpt = 0;
                string amount = request.PaymentAmount.ToString();
                string docRefNo = request.OrderId;
                string storeCode = request.StoreCode;
                string salespersonCode = request.SalesPersonCode;
                string billToCustomerNo = request.CustomerNo;
                var content = new StringContent(
$@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""urn:microsoft-dynamics-schemas/codeunit/PaymentCapture"">
   <soapenv:Header/>
   <soapenv:Body>
      <pay:ProcessCardPayment>
         <pay:pINPadNo>{pINPadNo}</pay:pINPadNo>
         <pay:transactionTypeOpt>{transactionTypeOpt}</pay:transactionTypeOpt>
         <pay:amount>{amount}</pay:amount>
         <pay:docRefNo>{docRefNo}</pay:docRefNo>
         <pay:storeCode>{storeCode}</pay:storeCode>
         <pay:salespersonCode>{salespersonCode}</pay:salespersonCode>
         <pay:billToCustomerNo>{billToCustomerNo}</pay:billToCustomerNo>
      </pay:ProcessCardPayment>
   </soapenv:Body>
</soapenv:Envelope>",
            Encoding.UTF8,
            "text/xml");
                content.Headers.Add("SOAPAction", "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment");
                var response = await this.Client.PostAsync($"{appConfig.PublicBaseUrl}MockVisitekProcessCardPayment.svc", content);
                response.EnsureSuccessStatusCode();
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
                        result = new ProcessCreditCardResponse();
                        result.Status = "SUCCESS";
                        result.ReturnValue = return_value;
                    }
                    else
                    {
                        throw new Exception("Unable to parse response:\n" + responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ProcessCreditCardResponse();
                result.Status = "ERROR";
                result.ReturnValue = ex.Message + ex.StackTrace;
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
