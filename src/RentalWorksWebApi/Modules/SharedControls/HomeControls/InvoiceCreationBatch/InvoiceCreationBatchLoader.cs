using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebApi;
namespace WebApi.Modules.HomeControls.InvoiceCreationBatch
{
    [FwSqlTable("invoicebatchview")]
    public class InvoiceCreationBatchLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InvoiceCreationBatchId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchno", modeltype: FwDataTypes.Integer)]
        public int? BatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchnostr", modeltype: FwDataTypes.Text)]
        public string BatchNumberAsString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchdate", modeltype: FwDataTypes.Date)]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchtype", modeltype: FwDataTypes.Text)]
        public string BatchType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoices", modeltype: FwDataTypes.Integer)]
        public int? InvoiceCount { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string filterId = ""; 
            //DateTime filterDate = DateTime.MinValue; 
            // 
            //if ((request != null) && (request.uniqueids != null)) 
            //{ 
            //    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids); 
            //    if (uniqueIds.ContainsKey("FilterId")) 
            //    { 
            //        filterId = uniqueIds["FilterId"].ToString(); 
            //    } 
            //    if (uniqueIds.ContainsKey("FilterDate")) 
            //    { 
            //        filterDate = FwConvert.ToDateTime(uniqueIds["FilterDate"].ToString()); 
            //    } 
            //} 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(batchtype = '" + RwConstants.BATCH_TYPE_INVOICE + "')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@filterid", filterId); 
            //select.AddParameter("@filterdate", filterDate); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
