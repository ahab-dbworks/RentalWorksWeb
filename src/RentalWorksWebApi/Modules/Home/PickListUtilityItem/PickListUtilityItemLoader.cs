using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;

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
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
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
        [FwSqlDataField(column: "qtyremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? RemainingQuantity { get; set; }
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
        [FwSqlDataField(column: "vendorcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor { get; set; }
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
                using (FwSqlCommand qry = new FwSqlCommand(conn, "gettmppicklistitem", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                    qry.AddParameter("@orderids", SqlDbType.NVarChar, ParameterDirection.Input, orderIds);
                    AddMiscFieldToQueryAsBoolean("ItemsNotYetStaged", "@itemsnotyetstaged", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsStaged", "@itemsstaged", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsOut", "@itemsout", qry, request);
                    AddMiscFieldToQueryAsDate("PickDateFrom", "@pickdatefrom", qry, request);
                    AddMiscFieldToQueryAsDate("PickDateTo", "@pickdateto", qry, request);
                    AddMiscFieldToQueryAsBoolean("RentalItems", "@rentalitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("SaleItems", "@saleitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("VendorItems", "@vendoritems", qry, request);
                    AddMiscFieldToQueryAsBoolean("LaborItems", "@laboritems", qry, request);
                    AddMiscFieldToQueryAsString("WarehouseId", "@warehouseid", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitMain", "@completekitmains", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitAccessories", "@completekitaccessories", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitOptions", "@completekitoptions", qry, request);
                    AddMiscFieldToQueryAsBoolean("StandAloneItems", "@standaloneitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsOnOtherPickLists", "@itemsonotherpicklists", qry, request);
                    AddMiscFieldToQueryAsBoolean("ReduceQuantityAlreadyPicked", "@reduceqtyalreadypicked", qry, request);
                    AddMiscFieldToQueryAsBoolean("SummarizeByICode", "@summarizebymaster", qry, request);
                    AddMiscFieldToQueryAsBoolean("SummarizeCompleteKitItems", "@summarizeacc", qry, request);
                    AddMiscFieldToQueryAsBoolean("HonorCompleteKitItemTypes", "@honorcompletekititemtypes", qry, request);
                    AddMiscFieldToQueryAsBoolean("SelectAll", "@selectall", qry, request);
                    AddMiscFieldToQueryAsBoolean("SelectNone", "@selectnone", qry, request);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
