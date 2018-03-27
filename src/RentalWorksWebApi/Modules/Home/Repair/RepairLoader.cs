using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.Repair
{
    [FwSqlTable("repairview")]
    public class RepairLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string RepairId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billinglocationid", modeltype: FwDataTypes.Text)]
        public string BillingLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billinglocation", modeltype: FwDataTypes.Text)]
        public string BillingLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemwarehouseid", modeltype: FwDataTypes.Text)]
        public string ItemWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingwarehouseid", modeltype: FwDataTypes.Text)]
        public string BillingWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingwarehouse", modeltype: FwDataTypes.Text)]
        public string BillingWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdate", modeltype: FwDataTypes.Date)]
        public string RepairDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepair", modeltype: FwDataTypes.Boolean)]
        public bool? OutsideRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepairpono", modeltype: FwDataTypes.Text)]
        public string OutsideRepairPoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfordisp", modeltype: FwDataTypes.Text)]
        public string AvailForDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedealid", modeltype: FwDataTypes.Text)]
        public string DamageDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedeal", modeltype: FwDataTypes.Text)]
        public string DamageDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderid", modeltype: FwDataTypes.Text)]
        public string DamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderno", modeltype: FwDataTypes.Text)]
        public string DamageOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderdesc", modeltype: FwDataTypes.Text)]
        public string DamageOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedbyid", modeltype: FwDataTypes.Text)]
        public string DamageScannedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedby", modeltype: FwDataTypes.Text)]
        public string DamageScannedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderid", modeltype: FwDataTypes.Text)]
        public string FinalLandDOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderno", modeltype: FwDataTypes.Text)]
        public string FinalLandDOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractid", modeltype: FwDataTypes.Text)]
        public string DamageContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractno", modeltype: FwDataTypes.Text)]
        public string DamageContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderid", modeltype: FwDataTypes.Text)]
        public string ChargeOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderno", modeltype: FwDataTypes.Text)]
        public string ChargeOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderdesc", modeltype: FwDataTypes.Text)]
        public string ChargeOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeinvoiceid", modeltype: FwDataTypes.Text)]
        public string ChargeInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeinvoiceno", modeltype: FwDataTypes.Text)]
        public string ChargeInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean)]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabletext", modeltype: FwDataTypes.Text)]
        public string BillableDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notbilled", modeltype: FwDataTypes.Boolean)]
        public bool? NotBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Text)]
        public string Priority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtype", modeltype: FwDataTypes.Text)]
        public string RepairType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popending", modeltype: FwDataTypes.Boolean)]
        public bool? PoPending { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damage", modeltype: FwDataTypes.Text)]
        public string Damage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "correction", modeltype: FwDataTypes.Text)]
        public string Correction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "released", modeltype: FwDataTypes.Boolean)]
        public bool? Released { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releasedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReleasedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferid", modeltype: FwDataTypes.Text)]
        public string TransferId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferredfromwarehouseid", modeltype: FwDataTypes.Text)]
        public string TransferredFromWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completedby", modeltype: FwDataTypes.Text)]
        public string CompletedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatusid", modeltype: FwDataTypes.Text)]
        public string RepairItemStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatus", modeltype: FwDataTypes.Text)]
        public string RepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string LocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request);


            if ((request != null) && (request.activeview != null))
            {

                if (request.activeview.Contains("WarehouseId="))
                {
                    string whId = request.activeview.Replace("WarehouseId=", "");
                    if (!whId.Equals("ALL"))
                    {
                        select.AddWhere("(warehouseid = @whid)");
                        select.AddParameter("@whid", whId);
                    }
                }

                string locId = "ALL";
                if (request.activeview.Contains("OfficeLocationId="))
                {
                    locId = request.activeview.Replace("OfficeLocationId=", "");
                }
                else if (request.activeview.Contains("LocationId="))
                {
                    locId = request.activeview.Replace("LocationId=", "");
                }
                if (!locId.Equals("ALL"))
                {
                    select.AddWhere("(locationid = @locid)");
                    select.AddParameter("@locid", locId);
                }
            }

        }
        //------------------------------------------------------------------------------------ 
    }
}
