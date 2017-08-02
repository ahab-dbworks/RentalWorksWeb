using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealOrder
{
    [FwSqlTable("dealorder")]
    public class DealOrderRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderno", dataType: FwDataTypes.Text, length: 16)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdesc", dataType: FwDataTypes.Text, length: 50)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdate", dataType: FwDataTypes.UTCDateTime)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealid", dataType: FwDataTypes.Text, length: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
