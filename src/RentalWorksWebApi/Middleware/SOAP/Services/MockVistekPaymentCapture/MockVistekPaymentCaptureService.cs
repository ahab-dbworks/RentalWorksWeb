﻿using FwStandard.Models;
using FwStandard.SqlServer;
using shortid;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApi.Middleware.SOAP.Services.MockVistekPaymentCapture
{
    public class MockVistekPaymentCaptureService : IMockVistekPaymentCaptureService
    {
        public FwApplicationConfig AppConfig { get; set; } = null;
        public async Task<MockVistekProcessCardPayment_Result> ProcessCardPayment(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo, string pPaymentRefNo, string pCardType, string pCardNo, string pAuthCode)
        {
            if (transactionTypeOpt == 1)
            {
                // Depleting Deposit
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

                Random rnd = new Random();
                int transactionId = rnd.Next(100000, 999999);
                int last4CardNumbers = rnd.Next(1000, 9999);
                MockVistekProcessCardPayment_Result result = new MockVistekProcessCardPayment_Result();
                decimal paymentAmount = FwConvert.ToDecimal(amount);
                if ((paymentAmount == totals_weekly_grandtotal) ||
                    (paymentAmount == totals_period_grandtotal) ||
                    (paymentAmount == totals_replacement_depositdue))
                {
                    result.return_value = $"Approved|APPROVED|Captured|VISA|************${last4CardNumbers}|${transactionId}|{amount}";
                }
                else
                {
                    result.return_value = $"Declined|DECLINED|Captured|VISA|************${last4CardNumbers}|${transactionId}|{amount}";
                }
                return result;
            }
            else if (transactionTypeOpt == 2)
            {
                // Refund
                Random rnd = new Random();
                int transactionId = rnd.Next(100000, 999999);
                int last4CardNumbers = rnd.Next(1000, 9999);
                MockVistekProcessCardPayment_Result result = new MockVistekProcessCardPayment_Result();
                result.return_value = $"Approved|APPROVED|Captured|VISA|************0085|${transactionId}|{amount}";
                return result;
            }
            throw new NotSupportedException($"Unsupported transactionTypeOpt: ${transactionTypeOpt}");
        }
    }
}
