using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Settings.ProjectItemsOrdered
{
    [FwSqlTable("itemordered")]
    public class ProjectItemsOrderedRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorderedid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ProjectItemsOrderedId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemordered", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string ProjectItemsOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}