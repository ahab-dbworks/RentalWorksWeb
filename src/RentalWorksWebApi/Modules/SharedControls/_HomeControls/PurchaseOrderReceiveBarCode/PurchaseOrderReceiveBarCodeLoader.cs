using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.HomeControls.PurchaseOrderReceiveBarCode
{
    [FwSqlTable("barcodeholdingview")]
    public class PurchaseOrderReceiveBarCodeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeholdingid", modeltype: FwDataTypes.Integer, isPrimaryKey: true, identity: true)]
        public int? PurchaseOrderReceiveBarCodeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionno", modeltype: FwDataTypes.Text)]
        public string InspectionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendorid", modeltype: FwDataTypes.Text)]
        public string InspectionVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendor", modeltype: FwDataTypes.Text)]
        public string InspectionVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date)]
        public string ManufactureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printqty", modeltype: FwDataTypes.Integer)]
        public int? PrintQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer)]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text)]
        public string InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractid", modeltype: FwDataTypes.Text)]
        public string ReceiveContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractno", modeltype: FwDataTypes.Text)]
        public string ReceiveContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returncontractid", modeltype: FwDataTypes.Text)]
        public string ReturnContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablebarcode", modeltype: FwDataTypes.Text)]
        public string ScannableBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icode", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icodedescription", modeltype: FwDataTypes.Text)]
        public string ICodeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packingslipno", modeltype: FwDataTypes.Text)]
        public string PackingSlipNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfordisp", modeltype: FwDataTypes.Text)]
        public string AvailableForDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mixedcaseserialno", modeltype: FwDataTypes.Boolean)]
        public bool? SerialNumberIsMixedCase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string filterId = ""; 
            //DateTime filterDate = DateTime.MinValue; 
            //bool filterBoolean = false; 
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
            //    if (uniqueIds.ContainsKey("FilterBoolean")) 
            //    { 
            //        filterBoolean = FwConvert.FilterBoolean(uniqueIds["FilterBoolean"].ToString()); 
            //    } 
            //} 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(orderid > '')"); 
            select.AddWhere("(issuspend <> 'T')"); 
            addFilterToSelect("PurchaseOrderId", "orderid", select, request); 
            addFilterToSelect("ReceiveContractId", "receivecontractid", select, request); 
            //select.AddParameter("@filterid", filterId); 
            //select.AddParameter("@filterdate", filterDate); 
            //select.AddParameter("@filterboolean", filterBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
