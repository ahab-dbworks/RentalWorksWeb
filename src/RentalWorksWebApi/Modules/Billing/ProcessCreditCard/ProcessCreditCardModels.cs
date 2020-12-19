using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Modules.Settings.CreditCardSettings.CreditCardPaymentType;

namespace WebApi.Modules.Billing.ProcessCreditCard
{
    //------------------------------------------------------------------------------------ 
    public class ProcessCreditCardPaymentRequest
    {
        public string PaymentTypeId { get; set; } = string.Empty;
        public enum TransactionTypes { Sale = 0, VoidSale = 1, Refund = 2, VoidRefund = 3 }
        public TransactionTypes TransactionType { get; set; } = TransactionTypes.Sale;
        public string PINPadCode { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; } = 0;
        public string OrderId { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;
        public string SalesPersonCode { get; set; } = string.Empty;
        public string DealNumber { get; set; } = string.Empty;
        public string PaymentReferenceNo { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string AuthorizationCode { get; set; } = string.Empty;
    }
    //------------------------------------------------------------------------------------ 
    public class ProcessCreditCardPaymentResponse
    {
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public string CardEntryMode { get; set; } = string.Empty;

        public ProcessCreditCardPaymentCardTypes CardType { get; set; } = ProcessCreditCardPaymentCardTypes.Other;
        public string CardNumber { get; set; } = string.Empty;
        public string AuthorizationCode { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        //------------------------------------------------------------------------------------
        private async Task<Dictionary<string, FwDatabaseField>> GetCreditCardPayTypeAsync(FwApplicationConfig appConfig, FwUserSession userSession, FwSqlConnection conn)
        {
            string cardTypeDescription = string.Empty;
            switch (this.CardType)
            {
                case ProcessCreditCardPaymentCardTypes.Visa:
                    cardTypeDescription = RwConstants.CREDIT_CARD_PAYMENT_TYPE_VISA;
                    break;
                case ProcessCreditCardPaymentCardTypes.MasterCard:
                    cardTypeDescription = RwConstants.CREDIT_CARD_PAYMENT_TYPE_MASTER_CARD;
                    break;
                case ProcessCreditCardPaymentCardTypes.Amex:
                    cardTypeDescription = RwConstants.CREDIT_CARD_PAYMENT_TYPE_AMEX;
                    break;
                case ProcessCreditCardPaymentCardTypes.Discover:
                    cardTypeDescription = RwConstants.CREDIT_CARD_PAYMENT_TYPE_DISCOVER;
                    break;
                case ProcessCreditCardPaymentCardTypes.Other:
                    cardTypeDescription = RwConstants.CREDIT_CARD_PAYMENT_TYPE_OTHER;
                    break;
            }
            var row = await FwSqlCommand.GetRowAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "creditcardpaytypeview", "description", cardTypeDescription, true);
            return row;
        }
        //------------------------------------------------------------------------------------

        public async Task<string> GetChargePayTypeIdAsync(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                var row = await this.GetCreditCardPayTypeAsync(appConfig, userSession, conn);
                var chargePaymentTypeId = row["chargepaytypeid"].ToString().TrimEnd();
                if (string.IsNullOrEmpty(chargePaymentTypeId))
                {
                    throw new Exception($"Please configure [Settings > Credit Card Payment Type > Charge Payment Type] for: {row["description"].ToString()}");
                }
                return chargePaymentTypeId;
            }
        }
        //------------------------------------------------------------------------------------ 
        public async Task<string> GetRefundPayTypeIdAsync(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                var row = await this.GetCreditCardPayTypeAsync(appConfig, userSession, conn);
                var refundpaytypeid = row["refundpaytypeid"].ToString().TrimEnd();
                if (string.IsNullOrEmpty(refundpaytypeid))
                {

                    throw new Exception($"Please configure [Settings > Credit Card Payment Type > Refund Payment Type] for: {row["description"].ToString()}");
                }
                return refundpaytypeid;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public enum ProcessCreditCardPaymentCardTypes { Other, Amex, Visa, MasterCard, Discover }
    //------------------------------------------------------------------------------------ 
}