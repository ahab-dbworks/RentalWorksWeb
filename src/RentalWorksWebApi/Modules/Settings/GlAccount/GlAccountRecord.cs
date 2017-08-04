using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.GlAccount
{
    [FwSqlTable("glaccount")]
    public class GlAccountRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string GlAccountId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text, maxlength: 35)]
        public string GlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "gltype", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string GlAccountType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
