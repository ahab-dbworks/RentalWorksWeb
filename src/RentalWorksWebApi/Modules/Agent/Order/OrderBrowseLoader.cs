﻿using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi;

namespace WebApi.Modules.Agent.Order
{
    public class OrderBrowseLoader : OrderBaseBrowseLoader
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
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbytitle", modeltype: FwDataTypes.Text)]
        public string OrderedByTitle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyphone", modeltype: FwDataTypes.Text)]
        public string OrderedByPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyext", modeltype: FwDataTypes.Text)]
        public string OrderedByExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyphoneext", modeltype: FwDataTypes.Text)]
        public string OrderedByPhoneAndExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyemail", modeltype: FwDataTypes.Text)]
        public string OrderedByEmail { get; set; }
        //------------------------------------------------------------------------------------

        //note: when adding field here, be sure to also add them to the OrderLoader class

        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.AddWhere("ordertype = '" + RwConstants.ORDER_TYPE_ORDER + "'");
            //addFilterToSelect("WarehouseId", "warehouseid", select, request);

            string invoiceId = GetUniqueIdAsString("InvoiceId", request);
            if (!string.IsNullOrEmpty(invoiceId))
            {
                select.AddWhere("exists (select * from orderinvoice oi where oi.orderid = " + TableAlias + ".orderid and oi.invoiceid = @invoiceid)");
                select.AddParameter("@invoiceid", invoiceId);
            }
        }
        //------------------------------------------------------------------------------------    
    }
}
