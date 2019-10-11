using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.Receipt
{
    public class RemainingDepositAmountsRequest
    {
        public string CustomerId { get; set; } = "";
        public string DealId { get; set; } = "";
        public string OfficeLocationId { get; set; } = "";
    }
    public class RemainingDepositAmountsResponse
    {
        public decimal DepletingDeposits { get; set; } = 0;
        public string DepletingDepositsFormatted { get; set; } = "";
        public decimal CreditMemos { get; set; } = 0;
        public string CreditMemosFormatted { get; set; } = "";
        public decimal Overpayments { get; set; } = 0;
        public string OverpaymentsFormatted { get; set; } = "";
    }

    public static class ReceiptFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> PostGlForReceipt(FwApplicationConfig appConfig, FwUserSession userSession, string receiptId, FwSqlConnection conn = null)
        {
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "postglforar", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
            await qry.ExecuteNonQueryAsync();
            return true;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<bool> DeleteReceipt(FwApplicationConfig appConfig, FwUserSession userSession, string receiptId, FwSqlConnection conn = null)
        {
            bool success = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "arcancel", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@success", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            success = FwConvert.ToBoolean(qry.GetParameter("@success").ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<RemainingDepositAmountsResponse> GetRemainingDepositAmounts (FwApplicationConfig appConfig, FwUserSession userSession, RemainingDepositAmountsRequest request, FwSqlConnection conn = null)
        {
            RemainingDepositAmountsResponse response = new RemainingDepositAmountsResponse();
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "argetremainingdepositamts", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@customerid", SqlDbType.NVarChar, ParameterDirection.Input, request.CustomerId);
            qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
            qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
            qry.AddParameter("@deposits", SqlDbType.Money, ParameterDirection.Output);
            qry.AddParameter("@credits", SqlDbType.Money, ParameterDirection.Output);
            qry.AddParameter("@overpayments", SqlDbType.Money, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.DepletingDeposits = qry.GetParameter("@deposits").ToDecimal();
            response.CreditMemos = qry.GetParameter("@credits").ToDecimal();
            response.Overpayments = qry.GetParameter("@overpayments").ToDecimal();

            response.DepletingDepositsFormatted = FwConvert.ToCurrencyStringNoDollarSign(response.DepletingDeposits);
            response.CreditMemosFormatted = FwConvert.ToCurrencyStringNoDollarSign(response.CreditMemos);
            response.OverpaymentsFormatted = FwConvert.ToCurrencyStringNoDollarSign(response.Overpayments);


            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
