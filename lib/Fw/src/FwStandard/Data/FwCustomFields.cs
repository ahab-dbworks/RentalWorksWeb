using FwStandard.ConfigSection;
using FwStandard.SqlServer;
using System.Collections.Generic;
//using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwCustomFields : List<FwCustomField>
    {
        protected DatabaseConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        public FwCustomFields() { }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task LoadAsync(string moduleName)
        {
            Clear();
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("select * from customfield where modulename = @modulename");
                qry.AddParameter("@modulename", moduleName);
                var customFields = (List<FwCustomField>)await qry.SelectAsync<FwCustomField>(true);
                AddRange(customFields);
            }
        }
        //------------------------------------------------------------------------------------
    }

}
