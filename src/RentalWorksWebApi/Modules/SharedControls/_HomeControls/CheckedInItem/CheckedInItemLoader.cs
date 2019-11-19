using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace WebApi.Modules.HomeControls.CheckedInItem
{
    [FwSqlTable("masteritem")]
    public class CheckedInItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quantity", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indatetime", modeltype: FwDataTypes.DateTime)]
        public string CheckedInDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean)]
        public bool? QuotePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean)]
        public bool? OrderPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PickListPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractOutPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnListPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean)]
        public bool? InvoicePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractInPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractReceivePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractReturnPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderReceivelistPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderReturnlistPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inreturnusersid", modeltype: FwDataTypes.Text)]
        public string CheckedInUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inuser", modeltype: FwDataTypes.Text)]
        public string CheckedInUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BarCodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isswap", modeltype: FwDataTypes.Boolean)]
        public bool? IsSwap { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("ContractId"))
                    {
                        ContractId = uniqueIds["ContractId"].ToString();
                    }
                }
            }

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getcheckedin", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, ContractId);
                    qry.AddParameter("@summary", SqlDbType.NVarChar, ParameterDirection.Input, (false ? "T" : "F"));
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;


        }
        //------------------------------------------------------------------------------------
    }
}
