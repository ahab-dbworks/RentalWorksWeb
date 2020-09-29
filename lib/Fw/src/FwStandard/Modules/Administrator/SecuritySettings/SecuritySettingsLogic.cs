using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.Mobile;
using Newtonsoft.Json;
using System.Dynamic;
using System.Threading.Tasks;


namespace FwStandard.Modules.Administrator.SecuritySettings
{
    [FwLogic(Id: "LKJXQscYKh4M")]
    public class SecuritySettingsLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        public SecuritySettingsLogic()
        {
            SecuritySettingsTitle = "Security Settings";
            LoadOriginalBeforeSaving = false;
        }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        [FwLogicProperty(Id: "1nNnxO0qUdFX", IsRecordTitle: true)]
        public string SecuritySettingsTitle { get; set; }
        public async Task<SecuritySettingsLoader> GetSettingsAsync<T>(string controlid)
        {
            SecuritySettingsLoader settings = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from dbo.control d with (nolock)");
                    qry.Add("where controlid = @controlid");
                    qry.AddParameter("@controlid", controlid);
                    await qry.ExecuteAsync();
                    settings = FwXml.DeserializeString<SecuritySettingsLoader>(qry.GetField("settings").ToString().Trim());
                }
            }
            settings.RecordTitle = SecuritySettingsTitle;
            return settings;
        }
        //------------------------------------------------------------------------------------
        public async Task<dynamic> SaveSettingsAsync<T>(string controlid, SecuritySettingsLoader request)
        {
            string serializedRequest;
            dynamic response = new ExpandoObject();

            serializedRequest = FwXml.Serialize(request);
            serializedRequest = serializedRequest.Replace("true", "T").Replace("false", "F");

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("update dbo.control set settings = @settings where controlid = @controlid");
                    qry.AddParameter("@settings", serializedRequest);
                    qry.AddParameter("@controlid", controlid);
                    qry.AddParameter("@msg", System.Data.SqlDbType.VarChar, System.Data.ParameterDirection.Output);
                    await qry.ExecuteAsync();
                    response.Message = qry.GetParameter("@msg").ToString();
                }
            }
            response.RecordTitle = SecuritySettingsTitle;
            return response;
        }
        //------------------------------------------------------------------------------------

    }
}