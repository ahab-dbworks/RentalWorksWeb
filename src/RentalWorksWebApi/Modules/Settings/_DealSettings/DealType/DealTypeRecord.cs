using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.DealType
{
    [FwSqlTable("dealtype")]
    public class DealTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DealTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whitetext", modeltype: FwDataTypes.Boolean)]
        public bool? WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "theatricalproduction", modeltype: FwDataTypes.Boolean)]
        public bool? TheatricalProduction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "theatricalautocheckout", modeltype: FwDataTypes.Boolean)]
        public bool? TheatricalAutoCheckout { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
