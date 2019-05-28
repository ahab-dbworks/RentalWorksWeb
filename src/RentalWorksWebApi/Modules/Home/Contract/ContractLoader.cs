using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Home.Contract
{
    [FwSqlTable("contractview")]
    public class ContractLoader : ContractBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
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
        [FwSqlDataField(column: "exchange", modeltype: FwDataTypes.Boolean)]
        public bool? Exchange { get; set; }
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
        [FwSqlDataField(column: "hasadjustedrentaldate", modeltype: FwDataTypes.Boolean)]
        public bool? HasAdjustedBillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasvoid", modeltype: FwDataTypes.Boolean)]
        public bool? HasVoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text)]
        public string DeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
        public string Carrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackingnumber", modeltype: FwDataTypes.Text)]
        public string DeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackingurl", modeltype: FwDataTypes.Text)]
        public string DeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
