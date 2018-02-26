using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Home.Contract
{
    [FwSqlTable("contractview")]
    public class ContractLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ContractId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text)]
        public string ContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttype", modeltype: FwDataTypes.Text)]
        public string ContractType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date)]
        public string ContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttime", modeltype: FwDataTypes.Text)]
        public string ContractTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "migrated", modeltype: FwDataTypes.Boolean)]
        public bool? Migrated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "needreconcile", modeltype: FwDataTypes.Boolean)]
        public bool? NeedReconcile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchange", modeltype: FwDataTypes.Boolean)]
        public bool? PendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangecontractid", modeltype: FwDataTypes.Text)]
        public string ExchangeContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyuser", modeltype: FwDataTypes.Text)]
        public string InputByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealinactive", modeltype: FwDataTypes.Boolean)]
        public bool? DealInactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "truck", modeltype: FwDataTypes.Boolean)]
        public bool? Truck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldate", modeltype: FwDataTypes.Text)]
        public string BillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasadjustedrentaldate", modeltype: FwDataTypes.Boolean)]
        public bool? HasAdjustedBillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasvoid", modeltype: FwDataTypes.Boolean)]
        public bool? HasVoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
