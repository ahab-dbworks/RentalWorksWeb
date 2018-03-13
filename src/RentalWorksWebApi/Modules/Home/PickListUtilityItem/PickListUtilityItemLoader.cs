using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace WebApi.Modules.Home.PickListUtilityItem
{
    [FwSqlTable("tmppicklistitemview")]
    public class PickListUtilityItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentparentid", modeltype: FwDataTypes.Text)]
        public string ParentParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accratio", modeltype: FwDataTypes.Decimal)]
        public decimal? AccessoryRatio { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentidnoparent", modeltype: FwDataTypes.Text)]
        public string InventoryTypeIdNoParent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysub", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinlocation", modeltype: FwDataTypes.Integer)]
        public int? QuantityInLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickqty", modeltype: FwDataTypes.Decimal)]
        public decimal? PickQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtypicked", modeltype: FwDataTypes.Decimal)]
        public decimal? PickedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
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
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvendorid", modeltype: FwDataTypes.Text)]
        public string SubVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        //jh 03/13/2018 note: I don't really want to override BrowseAsync this way.  This is a hack until we can figure out a clean way to parse the "exec stored_procedure_name" syntax" using teh FwSqlSelect object
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;

            string sessionId = "";
            string orderIds = "";
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("SessionId"))
                {
                    sessionId = uniqueIds["SessionId"].ToString();
                }
                if (uniqueIds.ContainsKey("OrderId"))
                {
                    orderIds = uniqueIds["OrderId"].ToString();
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("exec gettmppicklistitem '" + sessionId + "','" + orderIds + "'");
                    qry.AddColumn("sessionid");
                    qry.AddColumn("orderid");
                    qry.AddColumn("masteritemid");
                    qry.AddColumn("parentid");
                    qry.AddColumn("parentparentid");
                    qry.AddColumn("accratio");
                    qry.AddColumn("inventorydepartmentid");
                    qry.AddColumn("inventorydepartmentidnoparent");
                    qry.AddColumn("orderno");
                    qry.AddColumn("locationid");
                    qry.AddColumn("departmentid");
                    qry.AddColumn("dealid");
                    qry.AddColumn("ordertype");
                    qry.AddColumn("status");
                    qry.AddColumn("masterno");
                    qry.AddColumn("description");
                    qry.AddColumn("qtyordered");
                    qry.AddColumn("qtysub");
                    qry.AddColumn("consignqty");
                    qry.AddColumn("qtyinlocation");
                    qry.AddColumn("pickdate");
                    qry.AddColumn("pickqty");
                    qry.AddColumn("qtystaged");
                    qry.AddColumn("qtyout");
                    qry.AddColumn("qtypicked");
                    qry.AddColumn("masterid");
                    qry.AddColumn("warehouseid");
                    qry.AddColumn("warehouse");
                    qry.AddColumn("whcode");
                    qry.AddColumn("rectype");
                    qry.AddColumn("rectypedisplay");
                    qry.AddColumn("itemclass");
                    qry.AddColumn("itemorder");
                    qry.AddColumn("optioncolor");
                    qry.AddColumn("rentalitemid");
                    qry.AddColumn("barcode");
                    qry.AddColumn("serialno");
                    qry.AddColumn("subvendorid");
                    qry.AddColumn("consignorid");
                    qry.AddColumn("vendor");
                    qry.AddColumn("nestedmasteritemid");
                    dt = await qry.QueryToFwJsonTableAsync(true);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
