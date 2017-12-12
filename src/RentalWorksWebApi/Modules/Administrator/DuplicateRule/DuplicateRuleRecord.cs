using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.DuplicateRule
{
    [FwSqlTable("duplicaterule")]
    public class DuplicateRuleRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duplicateruleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DuplicateRuleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string RuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "casesensitive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CaseSensitive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveFields(string Fields)
        {
            bool saved = false;
            if (Fields != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateduplicaterulefields", _dbConfig.QueryTimeout);
                    qry.AddParameter("@duplicateruleid", SqlDbType.NVarChar, ParameterDirection.Input, DuplicateRuleId);
                    qry.AddParameter("@fields", SqlDbType.NVarChar, ParameterDirection.Input, Fields);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}