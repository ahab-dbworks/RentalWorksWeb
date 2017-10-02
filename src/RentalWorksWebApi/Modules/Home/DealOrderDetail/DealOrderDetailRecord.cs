using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Home.DealOrderDetail
{
    [FwSqlTable("dealorderdetail")]
    public class DealOrderDetailRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
