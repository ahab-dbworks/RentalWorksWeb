using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.MasterItem
{
    [FwSqlTable("masteritem")]
    public class MasterItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string MasterItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2, required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromtime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string FromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttodate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttotime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string ToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public int? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? MarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "premiumpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? PremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewcontactid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string CrewContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? HoursOvertime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? HoursDoubletime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price2", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price3", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price4", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price4 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price5", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price5 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locked", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Locked { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowarehouseid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ReturnToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 



        //[FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string RepairId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orgmasteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string OrgmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Quoteprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Contractoutprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Invoiceprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Returnlistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Contractinprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LdorderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldmasteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LdmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accratio", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 4)]
        //public decimal? Accratio { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returnflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Returnflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string RatemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "split", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Split { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manualbillflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Manualbillflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "meterrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Meterrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldoutcontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string LdoutcontractId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "countryoforiginid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string CountryoforiginId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "excludefromquikpaydiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Excludefromquikpaydiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manufacturerid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string ManufacturerId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "mfgmodel", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)]
        //public string Mfgmodel { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PomasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PoorderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vendorpartno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        //public string Vendorpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflict", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Conflict { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Contractreceiveprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Contractreturnprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "packagecode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Packagecode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Poprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarymasteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string PrimarymasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "schedulestatusid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string SchedulestatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string SpacetypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "linkedmasteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string LinkedmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "forceconflictflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Forceconflictflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availsequence", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        //public string Availsequence { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        //public string NestedmasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prorateweeks", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Prorateweeks { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "proratemonths", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Proratemonths { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ismaxdayloc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Ismaxdayloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ismaxloc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Ismaxloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderactivity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        //public string Orderactivity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Poreceivelistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Poreturnlistprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveNoteASync(string Note)
        {
            bool saved = await AppFunc.SaveNoteAsync(AppConfig, UserSession, OrderId, MasterItemId, "", Note);
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "syncorderitem", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, MasterItemId);
                await qry.ExecuteNonQueryAsync();
                saved = true;
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}