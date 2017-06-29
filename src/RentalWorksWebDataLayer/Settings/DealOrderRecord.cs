using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("dealorder", hasInsert: true, hasUpdate: true, hasDelete: true)]
    public class DealOrderRecord : FwDataRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField("orderid", FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";

        [FwSqlDataField("orderno", FwDataTypes.Text)]
        public string OrderNumber { get; set; } = "";

        [FwSqlDataField("orderdesc", FwDataTypes.Text)]
        public string OrderDescription { get; set; } = "";

        [FwSqlDataField("orderdate", FwDataTypes.UTCDateTime)]
        public DateTime? OrderDate { get; set; } = null;

        [FwSqlDataField("datestamp", FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; } = null;
        //------------------------------------------------------------------------------------
    }
}
