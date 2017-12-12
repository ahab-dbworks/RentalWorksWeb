using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [FwSqlTable("presentationlayeractivity")]
    public class PresentationLayerActivityRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayeractivityid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PresentationLayerActivityId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, required: true)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrename", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string ActivityRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string GroupNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2, required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 3, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}