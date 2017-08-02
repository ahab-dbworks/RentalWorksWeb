using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealType
{
    [FwSqlTable("dealtype")]
    public class DealTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealtypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string DealTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealtype", dataType: FwDataTypes.Text, length: 30)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "color", dataType: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "whitetext", dataType: FwDataTypes.Boolean)]
        public bool WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glprefix", dataType: FwDataTypes.Text, length: 10)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glsuffix", dataType: FwDataTypes.Text, length: 10)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "theatricalproduction", dataType: FwDataTypes.Boolean)]
        public bool TheatricalProduction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "theatricalautocheckout", dataType: FwDataTypes.Boolean)]
        public bool TheatricalAutoCheckout { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
