using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Settings.TermsConditions
{
    [FwSqlTable("termsconditions")]
    public class TermsConditionsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string TermsConditionsId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, maxlength: 40, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "newpageflg", modeltype: FwDataTypes.Boolean)]
        public bool? StartOnNewPage { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        public async Task<bool> SaveHtmlASync(string Html)
        {
            bool saved = false;
            if (Html != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateappnote", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, TermsConditionsId);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, Html);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
