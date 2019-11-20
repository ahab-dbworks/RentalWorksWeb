﻿using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Agent.Order;
using WebApi;

namespace WebApi.Modules.Agent.Quote
{
    public class QuoteBrowseLoader : OrderBaseBrowseLoader
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string QuoteId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string QuoteNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string QuoteDate { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "ordernocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        //public string QuoteNumberColor { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Integer)]
        public int? VersionNumber { get; set; }
        //------------------------------------------------------------------------------------

        //note: when adding field here, be sure to also add them to the QuoteLoader class

        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.AddWhere("ordertype = '" + RwConstants.ORDER_TYPE_QUOTE + "'");
            //addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}
