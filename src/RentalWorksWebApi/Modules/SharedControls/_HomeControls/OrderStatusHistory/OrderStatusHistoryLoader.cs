using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.OrderStatusHistory
{
    [FwSqlTable("orderstatushistoryview")]
    public class OrderStatusHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, isPrimaryKey: true, identity: true)]
        public int? Id { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text, isPrimaryKey: true, isPrimaryKeyOptional: true)]
        public string Internalchar { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdatetime", modeltype: FwDataTypes.Date)]
        public string StatusDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string UserFullName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "functionname", modeltype: FwDataTypes.Text)]
        public string FunctionName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("OrderId", "orderid", select, request); 
            addFilterToSelect("QuoteId", "orderid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}