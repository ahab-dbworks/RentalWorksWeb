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
                //qry.Add("select * from customfield where modulename = @modulename");
                //var customFields = (List<FwCustomField>)await qry.SelectAsync<FwCustomField>(true);
                //AddRange(customFields);

                //jh 07/24/2017 temporary workaround. can't get SelectAsync to work
                qry.Add("select modulename, fieldname, customtablename, customfieldname  from customfield where modulename = @modulename");
                qry.AddParameter("@modulename", moduleName);
                qry.AddColumn("modulename");
                qry.AddColumn("fieldname");
                qry.AddColumn("customtablename");
                qry.AddColumn("customfieldname");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++) {
                    FwCustomField customField = new FwCustomField(table.Rows[r][0].ToString(), table.Rows[r][1].ToString(), table.Rows[r][2].ToString(), table.Rows[r][3].ToString());
                    Add(customField);
                }
            }
        }
        //------------------------------------------------------------------------------------
    }

}
