using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.PresentationSettings.FormDesign
{
    [FwSqlTable("formdesign")]
    public class FormDesignRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "formdesignid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string FormDesignId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "formdesign", modeltype: FwDataTypes.Text, maxlength: 40, required: true)]
        public string FormDesign { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "formtype", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string FormType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
