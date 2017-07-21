using FwStandard.ConfigSection;
using FwStandard.SqlServer;
using System.Collections.Generic;
//using System.Threading.Tasks;

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
        //protected virtual async void LoadAsync()
        public virtual void Load(string moduleName)
        {
            Clear();
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("select * from customfield where modulename = @modulename");
                qry.AddParameter("@modulename", moduleName);
                //this.AddRange((List<FwCustomField>)await qry.SelectAsync<FwCustomField>(true, 1, 10));
                AddRange((List<FwCustomField>)qry.Select<FwCustomField>(true, 1, 10));
            }
        }
        //------------------------------------------------------------------------------------
    }

}
