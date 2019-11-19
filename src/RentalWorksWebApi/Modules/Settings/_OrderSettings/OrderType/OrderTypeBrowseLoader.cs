using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.OrderSettings.OrderType
{
    public class OrderTypeBrowseLoader : OrderTypeBaseBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(ordtype not in ('PO', 'EVENT'))"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}