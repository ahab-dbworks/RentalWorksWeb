using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Agent.OrderManifest
{
    [FwSqlTable("dbo.funcvaluesheetweb(@orderid, @rentalvalue, @salesvalue, @filterby, @mode)")]
    public class OrderManifestLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        public OrderManifestLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quantity", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string CountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueperitem", modeltype: FwDataTypes.Decimal)]
        public decimal? ValuePerItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dimensionslwh", modeltype: FwDataTypes.Text)]
        public string DimensionsLWH { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string MfgSerial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string MfgPartNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueextended", modeltype: FwDataTypes.Decimal)]
        public decimal? ValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Integer)]
        public int? WeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Integer)]
        public int? WeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Integer)]
        public int? WeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Integer)]
        public int? WeightGr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightlbs", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightoz", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightkg", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightgr", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightGr { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "manifestshippingcontainer", modeltype: FwDataTypes.Boolean)]
        public bool? ManifestShippingContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifeststandaloneitem", modeltype: FwDataTypes.Boolean)]
        public bool? ManifestStandAloneItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordervaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedvaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingcontainertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ShippingContainerTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingitemtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ShippingItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "piececounttotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PieceCountTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "standaloneitemtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? StandAloneItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextweightlbs", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalExtendedWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextweightoz", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalExtendedWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextweightkg", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalExtendedWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextweightgr", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalExtendedWeightGr { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ShippingContainerColor
        {
            get { return getShippingContainerColor(ManifestShippingContainer); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StandAloneItemColor
        {
            get { return getStandAloneItemColor(ManifestStandAloneItem); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        public string TotalItemWeight
        {
            get { return getTotalItemWeight(WeightLbs, WeightOz); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        public string TotalExtendedItemWeight
        {
            get { return getTotalExtendedItemWeight(ExtendedWeightLbs, ExtendedWeightOz); }
            set { }
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            string rentalValue = GetUniqueIdAsString("RentalValue", request) ?? "";
            string salesValue = GetUniqueIdAsString("SalesValue", request) ?? "";
            string filterBy = GetUniqueIdAsString("FilterBy", request) ?? "";
            string mode = GetUniqueIdAsString("Mode", request) ?? "";

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            select.AddParameter("@orderid", orderId);
            select.AddParameter("@rentalvalue", rentalValue);
            select.AddParameter("@salesvalue", salesValue);
            select.AddParameter("@filterby", filterBy);
            select.AddParameter("@mode", mode);
        }
        //------------------------------------------------------------------------------------
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("ShippingContainerColor")]  = getShippingContainerColor(FwConvert.ToBoolean(row[dt.GetColumnNo("ManifestShippingContainer")].ToString()));
                        row[dt.GetColumnNo("StandAloneItemColor")]     = getStandAloneItemColor(FwConvert.ToBoolean(row[dt.GetColumnNo("ManifestStandAloneItem")].ToString()));
                        row[dt.GetColumnNo("TotalItemWeight")]         = getTotalItemWeight(FwConvert.ToInt32(row[dt.GetColumnNo("WeightLbs")]), FwConvert.ToInt32(row[dt.GetColumnNo("WeightOz")]));
                        row[dt.GetColumnNo("TotalExtendedItemWeight")] = getTotalExtendedItemWeight(FwConvert.ToInt32(row[dt.GetColumnNo("ExtendedWeightLbs")]), FwConvert.ToInt32(row[dt.GetColumnNo("ExtendedWeightOz")]));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected string getShippingContainerColor(bool? ManifestShippingContainer)
        {
            string color = null;
            if (ManifestShippingContainer == true)
            {
                color = "#ffeb3b";
            }
            return color;
        }
        //------------------------------------------------------------------------------------
        protected string getStandAloneItemColor(bool? ManifestStandAloneItem)
        {
            string color = null;
            if (ManifestStandAloneItem == true)
            {
                color = "#2196f3";
            }
            return color;
        }
        //------------------------------------------------------------------------------------
        protected string getTotalItemWeight(int? WeightLbs, int? WeightOz)
        {
            string totalweight = null;
            if (WeightLbs != 0)
            {
                totalweight = WeightLbs.ToString() + " Lbs";
            }
            if (WeightOz != 0)
            {
                totalweight += " " + WeightOz.ToString() + " Oz";
            }
            return totalweight;
        }
        //------------------------------------------------------------------------------------
        protected string getTotalExtendedItemWeight(int? ExtendedWeightLbs, int? ExtendedWeightOz)
        {
            string totalweight = null;
            if (ExtendedWeightLbs != 0)
            {
                totalweight = ExtendedWeightLbs.ToString() + " Lbs";
            }
            if (ExtendedWeightOz != 0)
            {
                totalweight += " " + ExtendedWeightOz.ToString() + " Oz";
            }
            return totalweight;
        }
        //------------------------------------------------------------------------------------
    }
}
