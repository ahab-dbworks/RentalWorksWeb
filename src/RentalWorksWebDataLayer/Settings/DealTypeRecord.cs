using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
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
        [FwSqlDataField(columnName: "color", dataType: FwDataTypes.Decimal)]
        public decimal Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "whitetext", dataType: FwDataTypes.Text, length: 1)]
        public string WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glprefix", dataType: FwDataTypes.Text, length: 10)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glsuffix", dataType: FwDataTypes.Text, length: 10)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "theatricalproduction", dataType: FwDataTypes.Text, length: 1)]
        public string TheatricalProduction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "theatricalautocheckout", dataType: FwDataTypes.Text, length: 1)]
        public string TheatricalAutoCheckout { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
