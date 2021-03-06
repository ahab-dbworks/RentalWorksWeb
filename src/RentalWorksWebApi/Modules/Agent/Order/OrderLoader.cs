﻿using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi;

namespace WebApi.Modules.Agent.Order
{
    public class OrderLoader : OrderBaseLoader
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "ordernocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        //public string OrderNumberColor { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteorderid", modeltype: FwDataTypes.Text)]
        public string SourceQuoteId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "quoteorderno", modeltype: FwDataTypes.Text)]
        public string SourceQuoteNumber { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.AddWhere("ordertype = '" + RwConstants.ORDER_TYPE_ORDER + "'");
            //addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}
