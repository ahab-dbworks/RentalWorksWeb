using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [FwSqlTable("presentationlayeractivityoverride")]
    public class PresentationLayerActivityOverrideRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", isPrimaryKey: true, identity: true, modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int Id { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKeyOptional: true)]
        public string InternalChar { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayeractivityid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PresentationLayerActivityId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}