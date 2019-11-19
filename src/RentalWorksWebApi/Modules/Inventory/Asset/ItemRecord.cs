using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Inventory.Item
{
    [FwSqlTable("rentalitem")]
    public class ItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usehours", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? UseHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? PhysicalItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isneginv", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InspectionVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdesc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemnotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ItemNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VehicleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ManufactureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoriginid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryOfOriginId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warrantyexp", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string WarrantyExpiration { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgmodel", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string ManufacturerModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warrantyperiod", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WarrantyPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelflifeexpiration", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ShelfLifeExpirationDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationasof", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string LocationAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtransferflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsRepairTransfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OriginalShowId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthft", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? WidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthin", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? WidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightft", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? HeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightin", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? HeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthft", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? LengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthin", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? LengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "importrentalquantityid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? ImportRentalQuantityId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentmeter", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? CurrentMeter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assethours", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? AssetHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikes", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Strikes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "footcandles", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? FootCandles { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)]
        public string InspectionNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "softwareversion", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string SoftwareVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "softwareeffectivedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string SoftwareEffectiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours1", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LampHours1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours2", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LampHours2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours3", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LampHours3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours4", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LampHours4 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealorderlocationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ContainerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SpaceTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "facilitiestypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setcharacter", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string SetCharacter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldbarcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OldBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmfgserial", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OldSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldrfid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OldRfid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveNoteAsync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, ItemId, "", "", Note);
        }
        //-------------------------------------------------------------------------------------------------------
    }
}