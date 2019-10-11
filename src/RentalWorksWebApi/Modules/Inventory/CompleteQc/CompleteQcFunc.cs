using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Inventory.CompleteQc
{

    public class CompleteQcItemResponse : TSpStatusResponse
    {
        public string InventoryId{ get; set; }
        public string ICode{ get; set; }
        public string Description{ get; set; }
        public string ConditionId { get; set; }
        public string Condition { get; set; }
        public string ItemId{ get; set; }
        public string ItemQcId { get; set; }
        public bool CannotQcItemBecauseOfStatus { get; set; }
        public bool ItemDoesNotNeedQc { get; set; }
    }


    public class UpdateQcItemResponse : TSpStatusResponse
    {
    }

    public static class CompleteQcFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CompleteQcItemResponse> CompleteQcItem(FwApplicationConfig appConfig, FwUserSession userSession, CompleteQcItemRequest request)
        {
            CompleteQcItemResponse response = new CompleteQcItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "completeqcitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@qcanyway", SqlDbType.NVarChar, ParameterDirection.Input, (request.QcAnyway.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@conditionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@condition", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@rentalitemqcid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.ICode = qry.GetParameter("@masterno").ToString();
                response.Description = qry.GetParameter("@description").ToString();
                response.ConditionId = qry.GetParameter("@conditionid").ToString();
                response.Condition = qry.GetParameter("@condition").ToString();
                response.ItemId = qry.GetParameter("@rentalitemid").ToString();
                response.ItemQcId = qry.GetParameter("@rentalitemqcid").ToString();
                response.CannotQcItemBecauseOfStatus = qry.GetParameter("@status").ToInt32().Equals(1250);
                response.ItemDoesNotNeedQc = qry.GetParameter("@status").ToInt32().Equals(1251);
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateQcItemResponse> UpdateQcItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateQcItemRequest request)
        {
            UpdateQcItemResponse response = new UpdateQcItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updateqcitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
                qry.AddParameter("@rentalitemqcid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemQcId);
                qry.AddParameter("@conditionid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConditionId);
                qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, request.Note);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
