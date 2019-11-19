using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.PresentationSettings.PresentationLayer
{
    [FwSqlTable("presentationlayer")]
    public class PresentationLayerRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PresentationLayerId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayer", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, required: true)]
        public string PresentationLayer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}