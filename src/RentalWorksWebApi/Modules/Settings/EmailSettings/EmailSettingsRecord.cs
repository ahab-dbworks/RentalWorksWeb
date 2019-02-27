using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.EmailSettings
{
    [FwSqlTable("emailreportcontrol")]
    public class EmailSettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string EmailSettingsId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pdfpath", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 100)]
        //public string Pdfpath { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "host", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Host { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "port", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Port { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accountname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AccountUsername { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accountpassword", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AccountPassword { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "authtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string AuthenticationType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deletedays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DeleteDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
