using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealOrderDetail
{
    [FwSqlTable("dealorderdetail")]
    public class DealOrderDetailRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "maxcumulativediscount", dataType: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "poapprovalstatusid", dataType: FwDataTypes.Text, length: 8)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
