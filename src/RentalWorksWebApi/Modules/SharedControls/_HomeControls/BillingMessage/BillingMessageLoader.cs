using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.BillingMessage
{
    [FwSqlTable("tmpbillingmsg")]
    public class BillingMessageLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tmpbillingmsgid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? BillingMessageId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "msg", modeltype: FwDataTypes.Text)]
        public string BillingMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("SessionId", "sessionid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
