using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Home.CheckInSwap
{
    [FwSqlTable("dbo.funccheckinswaps(@contractid)")]
    public class CheckInSwapLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer)]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text)]
        public string InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outorderno", modeltype: FwDataTypes.Text)]
        public string OutOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outbarcode", modeltype: FwDataTypes.Text)]
        public string OutBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inorderno", modeltype: FwDataTypes.Text)]
        public string InOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inbarcode", modeltype: FwDataTypes.Text)]
        public string InBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string contractId = "";

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("ContractId"))
                {
                    contractId = uniqueIds["ContractId"].ToString();
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            select.AddParameter("@contractid", contractId); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
