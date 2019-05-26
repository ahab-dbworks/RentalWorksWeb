//using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
//using WebApi.Data;
using System.Data;
using System.Threading.Tasks;
using FwStandard.DataLayer;

namespace FwStandard.Modules.Administrator.DuplicateRule
{
    [FwSqlTable("duplicaterule")]
    public class DuplicateRuleRecord : FwDataReadWriteRecord
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
        [FwSqlDataField(column: "systemrule", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SystemRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "considerblanks", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ConsiderBlanks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveFieldsAsync(string Fields, string FieldTypes)
        {
            bool saved = false;
            if (Fields != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateduplicaterulefields", AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@duplicateruleid", SqlDbType.NVarChar, ParameterDirection.Input, DuplicateRuleId);
                    qry.AddParameter("@fields", SqlDbType.NVarChar, ParameterDirection.Input, Fields);
                    qry.AddParameter("@fieldtypes", SqlDbType.NVarChar, ParameterDirection.Input, FieldTypes);
                    await qry.ExecuteNonQueryAsync();
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}