using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Inventory.Purchase
{
    [FwSqlTable("purchasewebview")]
    public class PurchaseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        public PurchaseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
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
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public DateTime? PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "received", modeltype: FwDataTypes.Date)]
        public DateTime? ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDateString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "received", modeltype: FwDataTypes.Date)]
        public string ReceiveDateString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoid", modeltype: FwDataTypes.Text)]
        public string PurchasePoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoitemid", modeltype: FwDataTypes.Text)]
        public string PurchasePoItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsidepono", modeltype: FwDataTypes.Text)]
        public string OutsidePurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podepartmentid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podepartment", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchpoapproverid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderApproverId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchpoagentid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchvendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamt", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtax", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCostWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CostWithTaxExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconv", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCostCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtcurrconvextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CostCurrencyConvertedExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconv", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCostWithTaxCurrencyConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtaxcurrconvextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CostWithTaxCurrencyConvertedExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invcost", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorInvoiceCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invcostextended", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorInvoiceCostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryreceiptid", modeltype: FwDataTypes.Text)]
        public string InventoryReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryreceiptitemid", modeltype: FwDataTypes.Text)]
        public string InventoryReceiptItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor
        {
            get { return getCurrencyColor(CurrencyId, WarehouseDefaultCurrencyId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string WarehouseDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whdefaultcurrencycode", modeltype: FwDataTypes.Text)]
        public string WarehouseDefaultCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whdefaultcurrencysymbol", modeltype: FwDataTypes.Text)]
        public string WarehouseDefaultCurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer)]
        public int? PhysicalInventoryItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendpartno", modeltype: FwDataTypes.Text)]
        public string VendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasevendorid", modeltype: FwDataTypes.Text)]
        public string LeaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepurchasedate", modeltype: FwDataTypes.Date)]
        public string LeasePurchasedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepoid", modeltype: FwDataTypes.Text)]
        public string LeasePurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseno", modeltype: FwDataTypes.Text)]
        public string LeaseNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepurchaseamount", modeltype: FwDataTypes.Decimal)]
        public decimal? LeasePurchaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasereceivedate", modeltype: FwDataTypes.Date)]
        public string LeaseReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasedate", modeltype: FwDataTypes.Date)]
        public string LeaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseamt", modeltype: FwDataTypes.Decimal)]
        public decimal? LeaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepartno", modeltype: FwDataTypes.Text)]
        public string LeasePartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasecontact", modeltype: FwDataTypes.Text)]
        public string LeaseContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasedocid", modeltype: FwDataTypes.Text)]
        public string LeaseDocumentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseorderedpoid", modeltype: FwDataTypes.Text)]
        public string LeaseOrderedPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseorderedvendorid", modeltype: FwDataTypes.Text)]
        public string LeaseOrderedVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public DateTime? InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date)]
        public DateTime? ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractid", modeltype: FwDataTypes.Text)]
        public string ReceiveContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origpurchaseid", modeltype: FwDataTypes.Text)]
        public string OriginalPurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjid", modeltype: FwDataTypes.Text)]
        public string AdjustmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasenotes", modeltype: FwDataTypes.Text)]
        public string PurchaseNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryId", "masterid", select, request); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
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
                        row[dt.GetColumnNo("CurrencyColor")] = getCurrencyColor(row[dt.GetColumnNo("CurrencyId")].ToString(), row[dt.GetColumnNo("WarehouseDefaultCurrencyId")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
        protected string getCurrencyColor(string currencyId, string warehouseCurrencyId)
        {
            string color = null;
            if ((!string.IsNullOrEmpty(currencyId)) && (!currencyId.Equals(warehouseCurrencyId)))
            {
                color = RwGlobals.FOREIGN_CURRENCY_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}
