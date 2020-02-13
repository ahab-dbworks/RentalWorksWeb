using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.OrderStatusDetailReport
{
    [FwSqlTable("dbo.funcgetorderstatusdetail(@orderid,'ORDER')")]
    public class OrderStatusDetailReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string MasterItemId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "primarymasteritemid", modeltype: FwDataTypes.Text)]
        public string PrimaryMasteritemId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderwarehouse", modeltype: FwDataTypes.Text)]
        public string OrderWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdepartment", modeltype: FwDataTypes.Text)]
        public string OrderDepartment { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "masternodisplayweb", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outwarehouseid", modeltype: FwDataTypes.Text)]
        public string OutWarehouseId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "inwarehouseid", modeltype: FwDataTypes.Text)]
        public string InWarehouseId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outwhcode", modeltype: FwDataTypes.Text)]
        public string OutWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outwarehouse", modeltype: FwDataTypes.Text)]
        public string OutWarehouse { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "inwhcode", modeltype: FwDataTypes.Text)]
        public string InWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "inwarehouse", modeltype: FwDataTypes.Text)]
        public string InWarehouse { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outcontractid", modeltype: FwDataTypes.Text)]
        public string OutContractId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outcontractno", modeltype: FwDataTypes.Text)]
        public string OutContractNumber { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "issuspendout", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendOut { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outdatetime", modeltype: FwDataTypes.DateTime)]
        public string OutDateTime { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "outdatetimecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OutDateTimeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isexchangeout", modeltype: FwDataTypes.Boolean)]
        public bool? IsExchangeOut { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "istruckout", modeltype: FwDataTypes.Boolean)]
        public bool? IsTruckOut { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "incontractno", modeltype: FwDataTypes.Text)]
        public string InContractNumber { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "issuspendin", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendIn { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "indatetime", modeltype: FwDataTypes.DateTime)]
        public string InDateTime { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "indatetimecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InDateTimeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isexchangein", modeltype: FwDataTypes.Boolean)]
        public bool? IsExchangeIn { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "istruckin", modeltype: FwDataTypes.Boolean)]
        public bool? IsTruckIn { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "qtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCodeSerialRfid { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "exchordertranid", modeltype: FwDataTypes.Integer)]
        public int? ExchangeOrderTranId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "exchinternalchar", modeltype: FwDataTypes.Text)]
        public string ExchangeInternalChar { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "itemstatus", modeltype: FwDataTypes.Text)]
        public string ItemStatus { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------   
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedMasteritemId { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "iscontainer", modeltype: FwDataTypes.Boolean)]
        public bool? IsContainer { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "orderorderby", modeltype: FwDataTypes.Integer)]
        public int? OrderOrderby { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(OrderStatusDetailReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddParameter("@orderid", request.OrderId);
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            string[] totalfields = new string[] { "Quantity" };
            dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalfields);

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
