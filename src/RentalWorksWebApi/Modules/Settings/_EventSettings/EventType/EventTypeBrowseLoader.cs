using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Settings.OrderSettings.OrderType;

namespace WebApi.Modules.Settings.EventSettings.EventType
{
    public class EventTypeBrowseLoader : OrderTypeBaseBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string EventTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string EventType { get; set; }
        //------------------------------------------------------------------------------------ 
       protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(ordtype = 'EVENT')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}