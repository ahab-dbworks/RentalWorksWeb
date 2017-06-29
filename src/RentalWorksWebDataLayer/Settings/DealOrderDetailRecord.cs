using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("dealorderdetail", hasInsert: true, hasUpdate: true, hasDelete: true)]
    public class DealOrderDetailRecord : FwDataRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField("orderid", FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";

        [FwSqlDataField("maxcumulativediscount", FwDataTypes.Decimal)]
        public decimal MaximumCumulativeDiscount { get; set; } = 0;

        [FwSqlDataField("poapprovalstatusid", FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; } = "";

        [FwSqlDataField("datestamp", FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; } = null;
        //------------------------------------------------------------------------------------
    }
}
