using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Settings.PresentationLayerForm
{
    [FwSqlTable("presentationlayerform")]
    public class PresentationLayerFormRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerformid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PresentationLayerFormId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "formtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string FormType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "formtitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FormTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}