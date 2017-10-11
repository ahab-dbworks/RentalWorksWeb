using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Settings.SpaceType
{
    [FwSqlTable("spacetype")]
    public class SpaceTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SpaceTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, required: true)]
        public string SpaceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addtodescription", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool AddToDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "forreportsonly", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool ForReportsOnly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeclassification", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string SpaceTypeClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whitetext", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool WhiteText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool NonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}