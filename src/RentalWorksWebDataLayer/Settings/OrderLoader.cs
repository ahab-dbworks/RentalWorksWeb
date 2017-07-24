using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("orderview", hasInsert: false, hasUpdate: false, hasDelete: false)]
    public class OrderLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderid",dataType: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderno", dataType: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdesc", dataType: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdate", dataType: FwDataTypes.UTCDateTime)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "customerid", dataType: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "customer", dataType: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealid", dataType: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "deal", dataType: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "maxcumulativediscount", dataType: FwDataTypes.Decimal)]
        public decimal MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "poapprovalstatusid", dataType: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
