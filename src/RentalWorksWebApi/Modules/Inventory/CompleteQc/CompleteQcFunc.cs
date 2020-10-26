using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Inventory.CompleteQc
{

    public class CompleteQcItemResponse : TSpStatusResponse
    {
        public string InventoryId { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
        public string ConditionId { get; set; }
        public string Condition { get; set; }
        public string ItemId { get; set; }
        public string ItemQcId { get; set; }
        public bool CannotQcItemBecauseOfStatus { get; set; }
        public bool ItemDoesNotNeedQc { get; set; }
        public bool ShowFootCandles { get; set; }
        public int RequiredFootCandles { get; set; }
        public bool ShowSoftwareVersion { get; set; }
        public string RequiredSoftwareVersion { get; set; }
        public bool ShowAssetUsage { get; set; }
        public bool ShowLampUsage { get; set; }
        public bool ShowStrikes { get; set; }
        public int LampCount { get; set; }
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
                qry.AddParameter("@trackfootcandles", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@minfootcandles", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@tracksoftware", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@softwareversion", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@trackassetusage", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@tracklampusage", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@trackstrikes", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@lampcount", SqlDbType.Int, ParameterDirection.Output);
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
                response.ShowFootCandles = FwConvert.ToBoolean(qry.GetParameter("@trackfootcandles").ToString());
                response.ShowSoftwareVersion = FwConvert.ToBoolean(qry.GetParameter("@tracksoftware").ToString());
                response.ShowAssetUsage = FwConvert.ToBoolean(qry.GetParameter("@trackassetusage").ToString());
                response.ShowLampUsage = FwConvert.ToBoolean(qry.GetParameter("@tracklampusage").ToString());
                response.ShowStrikes = FwConvert.ToBoolean(qry.GetParameter("@trackstrikes").ToString());
                response.RequiredFootCandles = FwConvert.ToInt32(qry.GetParameter("@minfootcandles").ToString());
                response.RequiredSoftwareVersion = qry.GetParameter("@softwareversion").ToString();
                response.LampCount = FwConvert.ToInt32(qry.GetParameter("@lampcount").ToString());
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
                qry.AddParameter("@footcandles", SqlDbType.Int, ParameterDirection.Input, FwConvert.ToInt32(request.CurrentFootCandles));
                qry.AddParameter("@currentsoftware", SqlDbType.NVarChar, ParameterDirection.Input, request.CurrentSoftwareVersion);
                qry.AddParameter("@softwareeffectivedate", SqlDbType.NVarChar, ParameterDirection.Input, request.SoftwareEffectiveDate);
                qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, request.Note);
                qry.AddParameter("@assethours", SqlDbType.Int, ParameterDirection.Input, request.AssetHours);
                qry.AddParameter("@strikes", SqlDbType.Int, ParameterDirection.Input, request.Strikes);
                qry.AddParameter("@lamphours1", SqlDbType.Int, ParameterDirection.Input, request.LampHours1);
                qry.AddParameter("@lamphours2", SqlDbType.Int, ParameterDirection.Input, request.LampHours2);
                qry.AddParameter("@lamphours3", SqlDbType.Int, ParameterDirection.Input, request.LampHours3);
                qry.AddParameter("@lamphours4", SqlDbType.Int, ParameterDirection.Input, request.LampHours4);
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
