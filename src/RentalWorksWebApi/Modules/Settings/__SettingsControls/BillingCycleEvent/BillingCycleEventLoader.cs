using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.BillingCycleEvent
{
    [FwSqlTable("billperiodeventpercent")]
    public class BillingCycleEventLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, required: true)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodeventpercentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string BillingCycleEventId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodevent", modeltype: FwDataTypes.Text, required: true)]
        public string BillingCycleEvent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billpercent", modeltype: FwDataTypes.Integer)]
        public int? BillPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.Boolean)]
        public bool? ActualRevenue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BillingCycleId", "billperiodid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}
