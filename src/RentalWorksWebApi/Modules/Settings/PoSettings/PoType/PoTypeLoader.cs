using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Modules.Settings.OrderSettings.OrderType;

namespace WebApi.Modules.Settings.PoSettings.PoType
{
    public class PoTypeLoader : OrderTypeBaseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PoTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string PoType { get; set; }
        //------------------------------------------------------------------------------------ 
       protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(ordtype = 'PO')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 

        public List<string> PurchaseShowFields { get; set; }
        public List<string> SubRentalShowFields { get; set; }
        public List<string> SubSaleShowFields { get; set; }
        public List<string> SubMiscShowFields { get; set; }
        public List<string> SubLaborShowFields { get; set; }

    }
}