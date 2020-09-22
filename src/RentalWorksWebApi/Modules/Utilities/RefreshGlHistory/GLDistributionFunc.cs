using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.GLDistribution
{

    public class InitializeGlDistributionRulesResponse : TSpStatusResponse
    {
    }

    public class RefreshGLHistoryRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class RefreshGLHistoryResponse : TSpStatusResponse
    {
    }

    public class PreviewGLHistoryResponse : TSpStatusResponse
    {
    }


    public class PostGlForDepreciationRequest
    {
        public int? DepreciationId { get; set; }
    }

    public class PostGlForDepreciationResponse : TSpStatusResponse
    {
        public int GlId { get; set; }
        public string InternalChar { get; set; }
    }


    public static class GLDistributionFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<InitializeGlDistributionRulesResponse> InitializeGlDistributionRules(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            InitializeGlDistributionRulesResponse response = new InitializeGlDistributionRulesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "initgldistribution", appConfig.DatabaseSettings.QueryTimeout);
                await qry.ExecuteNonQueryAsync();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RefreshGLHistoryResponse> RefreshGLHistory(FwApplicationConfig appConfig, FwUserSession userSession, RefreshGLHistoryRequest request)
        {
            RefreshGLHistoryResponse response = new RefreshGLHistoryResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "refreshgl", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@fromdate", SqlDbType.NVarChar, ParameterDirection.Input, request.FromDate);
                qry.AddParameter("@todate", SqlDbType.NVarChar, ParameterDirection.Input, request.ToDate);
                await qry.ExecuteNonQueryAsync();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> PostGlForInvoice(FwApplicationConfig appConfig, string invoiceId, bool previewing)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "postglforinvoice", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
                qry.AddParameter("@preview", SqlDbType.NVarChar, ParameterDirection.Input, previewing);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
     public static async Task<bool> DeleteGlForInvoice(FwApplicationConfig appConfig, string invoiceId)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "deleteglforinvoice", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PostGlForDepreciationResponse> PostGlForDepreciation(FwApplicationConfig appConfig, FwUserSession userSession, PostGlForDepreciationRequest request)
        {
            PostGlForDepreciationResponse response = new PostGlForDepreciationResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "postglfordepreciation", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@depreciationid", SqlDbType.NVarChar, ParameterDirection.Input, request.DepreciationId);
                qry.AddParameter("@glid", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@internalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = true;
                response.GlId = qry.GetParameter("@glid").ToInt32();
                response.InternalChar = qry.GetParameter("@internalchar").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        

    }
}
