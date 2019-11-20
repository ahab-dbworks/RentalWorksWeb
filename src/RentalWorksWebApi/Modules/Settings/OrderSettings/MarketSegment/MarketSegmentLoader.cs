using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.OrderSettings.MarketSegment
{
    [FwSqlTable("marketsegmentview")]
    public class MarketSegmentLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string MarketSegmentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegment", modeltype: FwDataTypes.Text)]
        public string MarketSegment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markettypeid", modeltype: FwDataTypes.Text)]
        public string MarketTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markettype", modeltype: FwDataTypes.Text)]
        public string MarketType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("MarketTypeId", "markettypeid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
