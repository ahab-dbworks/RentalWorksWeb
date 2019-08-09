using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    [FwSqlTable("webusersearchsettings")]
    public class UserSearchSettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WebUserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Mode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "resultfields", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 1000)]
        public string ResultFields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableaccessoryautoexpand", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableAccessoryAutoExpand { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidezeroqty", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? HideZeroQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override async Task<FwValidateResult> ValidateAsync(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, FwValidateResult result)
        {
            if (result.IsValid)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "checkwebusersearchsettings", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@webusersid", SqlDbType.NVarChar, ParameterDirection.Input, WebUserId);
                    int i = await qry.ExecuteNonQueryAsync();
                }
            }
            await Task.CompletedTask;
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}