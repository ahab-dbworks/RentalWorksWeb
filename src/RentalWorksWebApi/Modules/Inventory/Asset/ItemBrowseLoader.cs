using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;

namespace WebApi.Modules.Inventory.Asset
{
    [FwSqlTable("rentalitemwebview")]
    public class ItemBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ItemBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public virtual string ItemId { get; set; }
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
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(QcRequired); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string InventoryStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Text)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BarCodeColor
        {
            get { return getBarCodeColor(IsSuspend); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
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
        [FwSqlDataField(column: "currentlocation", modeltype: FwDataTypes.Text)]
        public string CurrentLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastcontractsuspend", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractdate", modeltype: FwDataTypes.Date)]
        public string ReceiveContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pocost", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdesc", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iscontainer", modeltype: FwDataTypes.Boolean)]
        public bool? IsContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text)]
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containermasterid", modeltype: FwDataTypes.Text)]
        public string ContainerInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containermasterno", modeltype: FwDataTypes.Text)]
        public string ContainerICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "container", modeltype: FwDataTypes.Text)]
        public string ContainerDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerstatus", modeltype: FwDataTypes.Text)]
        public string ContainerStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerstatusdate", modeltype: FwDataTypes.Date)]
        public string ContainerStatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containeritemid", modeltype: FwDataTypes.Text)]
        public virtual string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? FixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Boolean)]
        public bool? Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text)]
        public string InventoryStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeforscanning", modeltype: FwDataTypes.Text)]
        public string BarCodeForScanning { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldbarcode", modeltype: FwDataTypes.Text)]
        public string OldBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmfgserial", modeltype: FwDataTypes.Text)]
        public string OldSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldrfid", modeltype: FwDataTypes.Text)]
        public string OldRfid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date)]
        public string ManufactureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedbycode", modeltype: FwDataTypes.Text)]
        public string TrackedByCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availownershipsort", modeltype: FwDataTypes.Integer)]
        public int? AvailOwnershipSort { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isneginv", modeltype: FwDataTypes.Boolean)]
        public bool? IsNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionno", modeltype: FwDataTypes.Text)]
        public string InspectionNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendorid", modeltype: FwDataTypes.Text)]
        public string InspectionVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendor", modeltype: FwDataTypes.Text)]
        public string InspectionVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgmodel", modeltype: FwDataTypes.Text)]
        public string ManufacturerModelNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceid", modeltype: FwDataTypes.Text)]
        public string SpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingroom", modeltype: FwDataTypes.Text)]
        public string BuildingRoom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemnotes", modeltype: FwDataTypes.Text)]
        public string ItemNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text)]
        public string PhysicalId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer)]
        public int? PhysicalItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicaldate", modeltype: FwDataTypes.Date)]
        public string PhysicalDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalby", modeltype: FwDataTypes.Text)]
        public string PhysicalBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryTypeOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? SubCategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchvendorid", modeltype: FwDataTypes.Text)]
        public string PurchaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchvendor", modeltype: FwDataTypes.Text)]
        public string PurchaseVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsidepono", modeltype: FwDataTypes.Text)]
        public string OutsidePurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoid", modeltype: FwDataTypes.Text)]
        public string PurchasePoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepono", modeltype: FwDataTypes.Text)]
        public string PurchasePoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invcost", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchinvoiceno", modeltype: FwDataTypes.Text)]
        public string PurchaseInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchinvoicedate", modeltype: FwDataTypes.Date)]
        public string PurchaseInvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementno", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturerid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text)]
        public string OriginalShowId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "originalshow", modeltype: FwDataTypes.Text)]
        public string OriginalShow { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "surfaceid", modeltype: FwDataTypes.Text)]
        public string SurfaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "surface", modeltype: FwDataTypes.Text)]
        public string Surface { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "walltypeid", modeltype: FwDataTypes.Text)]
        public string WallTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "walltype", modeltype: FwDataTypes.Text)]
        public string WallType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "openingid", modeltype: FwDataTypes.Text)]
        public string OpeningId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "opening", modeltype: FwDataTypes.Text)]
        public string Opening { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsiblepersonid", modeltype: FwDataTypes.Text)]
        public string ResponsiblePersonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsibleperson", modeltype: FwDataTypes.Text)]
        public string ResponsiblePerson { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buyerid", modeltype: FwDataTypes.Text)]
        public string BuyerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buyer", modeltype: FwDataTypes.Text)]
        public string Buyer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receiptnumber", modeltype: FwDataTypes.Text)]
        public string ReceiptNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthft", modeltype: FwDataTypes.Integer)]
        public int? WidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthin", modeltype: FwDataTypes.Integer)]
        public int? WidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightft", modeltype: FwDataTypes.Integer)]
        public int? HeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightin", modeltype: FwDataTypes.Integer)]
        public int? HeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthft", modeltype: FwDataTypes.Integer)]
        public int? LengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthin", modeltype: FwDataTypes.Integer)]
        public int? LengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentmeter", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentMeter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackassetusageflg", modeltype: FwDataTypes.Boolean)]
        public bool? TrackAssetUsage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tracklampusageflg", modeltype: FwDataTypes.Boolean)]
        public bool? TrackLampUsage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackstrikesflg", modeltype: FwDataTypes.Boolean)]
        public bool? TrackStrikes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackcandlesflg", modeltype: FwDataTypes.Boolean)]
        public bool? TrackCandles { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assethours", modeltype: FwDataTypes.Integer)]
        public int? AssetHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours1", modeltype: FwDataTypes.Integer)]
        public int? LampHours1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours2", modeltype: FwDataTypes.Integer)]
        public int? LampHours2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours3", modeltype: FwDataTypes.Integer)]
        public int? LampHours3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours4", modeltype: FwDataTypes.Integer)]
        public int? LampHours4 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikes", modeltype: FwDataTypes.Integer)]
        public int? Strikes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "footcandles", modeltype: FwDataTypes.Integer)]
        public int? FootCandles { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pattern", modeltype: FwDataTypes.Text)]
        public string Pattern { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gender", modeltype: FwDataTypes.Text)]
        public string Gender { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label", modeltype: FwDataTypes.Text)]
        public string Label { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "material", modeltype: FwDataTypes.Text)]
        public string Material { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "period", modeltype: FwDataTypes.Text)]
        public string Period { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cleaningfeeamount", modeltype: FwDataTypes.Decimal)]
        public decimal? CleaningFeeAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobesize", modeltype: FwDataTypes.Text)]
        public string WardrobeSize { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobepiececount", modeltype: FwDataTypes.Integer)]
        public int? WardrobePieceCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdeptisprops", modeltype: FwDataTypes.Boolean)]
        public bool? InventoryTypeIsProps { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdeptiswardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? InventoryTypeIsWardrobe { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealorderlocationid", modeltype: FwDataTypes.Text)]
        public string OrderLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerno", modeltype: FwDataTypes.Text)]
        public string ContainerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iswardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? IsWardrobe { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprops", modeltype: FwDataTypes.Boolean)]
        public bool? IsProps { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warrantyexp", modeltype: FwDataTypes.Date)]
        public string WarrantyExpiration { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warrantyperiod", modeltype: FwDataTypes.Integer)]
        public int? WarrantyPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforiginid", modeltype: FwDataTypes.Text)]
        public string CountryOfOriginId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string CountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelflifeexpiration", modeltype: FwDataTypes.Date)]
        public string ShelfLifeExpirationDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentdealid", modeltype: FwDataTypes.Text)]
        public string CurrentDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentdeal", modeltype: FwDataTypes.Text)]
        public string CurrentDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentcustomerid", modeltype: FwDataTypes.Text)]
        public string CurrentCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentcustomer", modeltype: FwDataTypes.Text)]
        public string CurrentCustomer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderid", modeltype: FwDataTypes.Text)]
        public string CurrentOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderno", modeltype: FwDataTypes.Text)]
        public string CurrentOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentordertype", modeltype: FwDataTypes.Text)]
        public string CurrentOrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderdesc", modeltype: FwDataTypes.Text)]
        public string CurrentOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderdate", modeltype: FwDataTypes.Date)]
        public string CurrentOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderpickdate", modeltype: FwDataTypes.Date)]
        public string CurrentOrderPickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderestrentfrom", modeltype: FwDataTypes.Date)]
        public string CurrentOrderFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentorderestrentto", modeltype: FwDataTypes.Date)]
        public string CurrentOrderToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lotno", modeltype: FwDataTypes.Text)]
        public string LotNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
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
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("TrackedBy", "trackedby", select, request);

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("TrackedBy", "trackedby", select, request);

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
                        row[dt.GetColumnNo("BarCodeColor")] = getBarCodeColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsSuspend")].ToString()));
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("QcRequired")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------       
        private string getDescriptionColor(bool? qcRequired)
        {
            string descriptionColor = null;
            if (qcRequired.GetValueOrDefault(false))
            {
                descriptionColor = RwGlobals.QC_REQUIRED_COLOR;
            }
            return descriptionColor;
        }
        //------------------------------------------------------------------------------------ 
        private string getBarCodeColor(bool? isSuspend)
        {
            string barCodeColor = null;
            if (isSuspend.GetValueOrDefault(false))
            {
                barCodeColor = RwGlobals.SUSPEND_COLOR;
            }
            return barCodeColor;
        }
        //------------------------------------------------------------------------------------ 
    }
}