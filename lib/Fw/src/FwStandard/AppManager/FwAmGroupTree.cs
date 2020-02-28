using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.AppManager
{
    public class FwAmGroupTree
    {
        public const int EXPIRATION_MINUTES = 5;
        public string GroupsId { get; set; } = string.Empty;
        public FwAmSecurityTreeNode RootNode { get; set; } = null;
        public DateTime DateStamp { get; set; } = DateTime.MinValue;
        public DateTime Expiration { get; set; } = DateTime.MinValue;

        public FwAmGroupTree()
        {
            this.Renew();
        }
        //---------------------------------------------------------------------------------------------
        protected List<FwAmSqlGroupNode> GetSqlGroupNodes()
        {
            return this.RootNode.GetSqlGroupNodes(); ;
        }
        //---------------------------------------------------------------------------------------------
        public async Task SaveToDatabaseAsync(FwApplicationConfig config)
        {
            var sqlGroupNodes = this.GetSqlGroupNodes();
            var sqlGroupNodesJson = JsonConvert.SerializeObject(sqlGroupNodes);
            using (var conn = new FwSqlConnection(config.DatabaseSettings.ConnectionString))
            {
                using (var cmd = new FwSqlCommand(conn, config.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("update groups");
                    cmd.Add("set security = @security");
                    cmd.Add("where groupsid = @groupsid");
                    cmd.AddParameter("@security", sqlGroupNodesJson);
                    cmd.AddParameter("@groupsid", this.GroupsId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void Renew()
        {
            this.Expiration = DateTime.Now.AddMinutes(EXPIRATION_MINUTES);
        }
        //---------------------------------------------------------------------------------------------
    }
}
