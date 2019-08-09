using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.GlAccount
{
    [FwSqlTable("glaccount")]
    public class GlAccountRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string GlAccountId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text, maxlength: 35, required: true)]
        public string GlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "gltype", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string GlAccountType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("GlAccountType", "gltype", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
