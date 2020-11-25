using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVisitekPaymentCapture
{
    public class MockVisitekPaymentCaptureService : IMockVisitekPaymentCaptureService
    {
        public FwApplicationConfig AppConfig { get; set; } = null;
        public async Task<MockVisitekProcessCardPayment_Result> ProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo)
        {
            decimal totals_weekly_grandtotal = 0;
            decimal totals_period_grandtotal = 0;
            decimal totals_replacement_replacementcost = 0;
            decimal totals_replacement_depositpercentage = 0;
            decimal totals_replacement_depositdue = 0;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select totals_weekly_grandtotal, totals_period_grandtotal, totals_replacement_replacementcost, totals_replacement_depositpercentage");
                    qry.Add("from processcreditcardloadview with (nolock)");
                    qry.Add("where orderid = @orderid");
                    qry.AddParameter("@orderid", docRefNo);
                    await qry.ExecuteAsync();
                    totals_weekly_grandtotal = qry.GetField("totals_weekly_grandtotal").ToDecimal();
                    totals_period_grandtotal = qry.GetField("totals_period_grandtotal").ToDecimal();
                    totals_replacement_replacementcost = qry.GetField("totals_replacement_replacementcost").ToDecimal();
                    totals_replacement_depositpercentage = qry.GetField("totals_replacement_depositpercentage").ToDecimal();
                    totals_replacement_depositdue = (totals_replacement_replacementcost * totals_replacement_depositpercentage) / 100.00m;
                }
            }
            
            MockVisitekProcessCardPayment_Result result = new MockVisitekProcessCardPayment_Result();
            decimal paymentAmount = FwConvert.ToDecimal(amount);
            if ((paymentAmount == totals_weekly_grandtotal) || 
                (paymentAmount == totals_period_grandtotal) ||
                (paymentAmount == totals_replacement_depositdue))
            {
                result.return_value = $"Approved|APPROVED|Captured|VISA|************0085|119994|{amount}";
            }
            else
            {
                result.return_value = $"Declined|DECLINED|Captured|VISA|************0085|119994|{amount}";
            }
            return result;
        }
    }

    [ServiceContract(Namespace = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", ConfigurationName = "VisitekPaymentCaptureService.PaymentCapture_Port")]
    public interface IMockVisitekPaymentCaptureService
    {
        [OperationContract(Action = "urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment", ReplyAction = "*")]
        Task<MockVisitekProcessCardPayment_Result> ProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo);
    }
}
