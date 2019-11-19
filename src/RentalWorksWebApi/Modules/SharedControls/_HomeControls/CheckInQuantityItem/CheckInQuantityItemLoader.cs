using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace WebApi.Modules.HomeControls.CheckInQuantityItem
{
    public class CheckInQuantityItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorconsignor", modeltype: FwDataTypes.Text)]
        public string VendorConsignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorconsignorcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorConsignorColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Integer)]
        public int? OrderPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowqty", modeltype: FwDataTypes.Boolean)]
        public bool? AllowQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string contractId = "";
            string orderId = "";
            bool allOrdersForDeal = false;
            bool outOnly = false;
            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("ContractId"))
                    {
                        contractId = uniqueIds["ContractId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("OrderId"))
                    {
                        orderId = uniqueIds["OrderId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("AllOrdersForDeal"))
                    {
                        allOrdersForDeal = FwConvert.ToBoolean(uniqueIds["AllOrdersForDeal"].ToString());
                    }
                    if (uniqueIds.ContainsKey("OutOnly"))
                    {
                        outOnly = FwConvert.ToBoolean(uniqueIds["OutOnly"].ToString());
                    }
                }
            }

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getcheckinquantity", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@allordersfordeal", SqlDbType.NVarChar, ParameterDirection.Input, allOrdersForDeal ? "T" : "F");
                    qry.AddParameter("@outonly", SqlDbType.NVarChar, ParameterDirection.Input, outOnly ? "T" : "F");
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;


        }
        //------------------------------------------------------------------------------------

    }
}
