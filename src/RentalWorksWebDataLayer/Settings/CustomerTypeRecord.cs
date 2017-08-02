using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("custtype")]
    public class CustomerTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custtypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CustomerTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custtype", dataType: FwDataTypes.Text, length: 20)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "defaultrentaldiscountpct", dataType: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal DefaultRentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "defaultsalesdiscountpct", dataType: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal DefaultSalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "defaultspacediscountpct", dataType: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal DefaultFacilitiesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
