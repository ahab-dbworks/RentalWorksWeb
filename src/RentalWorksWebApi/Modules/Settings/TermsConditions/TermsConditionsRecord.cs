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
    }
}
