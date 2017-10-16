using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.Item
{
    public class ItemLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemRecord item = new ItemRecord();
        ItemLoader itemLoader = new ItemLoader();
        public ItemLogic()
        {
            dataRecords.Add(item);
            dataLoader = itemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemId { get { return item.ItemId; } set { item.ItemId = value; } }
        public string InventoryId { get { return item.InventoryId; } set { item.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReplacementCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Classification { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool Container { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool FixedAsset { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool Rank { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StatusType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StatusDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryStatusId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Color { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool TextColor { get; set; }
        public string BarCode { get { return item.BarCode; } set { item.BarCode = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCodeForScanning { get; set; }
        public string SerialNumber { get { return item.SerialNumber; } set { item.SerialNumber = value; } }
        public string RfId { get { return item.RfId; } set { item.RfId = value; } }
        public string OldBarCode { get { return item.OldBarCode; } set { item.OldBarCode = value; } }
        public string OldSerialNumber { get { return item.OldSerialNumber; } set { item.OldSerialNumber = value; } }
        public string OldRfId { get { return item.OldRfId; } set { item.OldRfId = value; } }
        public string ManufacturerPartNumber { get { return item.ManufacturerPartNumber; } set { item.ManufacturerPartNumber = value; } }
        public string ManufactureDate { get { return item.ManufactureDate; } set { item.ManufactureDate = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string TrackedByCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? AvailOwnershipSort { get; set; }
        public bool IsNegativeInventory { get { return item.IsNegativeInventory; } set { item.IsNegativeInventory = value; } }
        public string InspectionNo { get { return item.InspectionNo; } set { item.InspectionNo = value; } }
        public string InspectionVendorId { get { return item.InspectionVendorId; } set { item.InspectionVendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InspectionVendor { get; set; }
        public string ManufacturerModelNumber { get { return item.ManufacturerModelNumber; } set { item.ManufacturerModelNumber = value; } }
        public string PurchaseId { get { return item.PurchaseId; } set { item.PurchaseId = value; } }
        public string AisleLocation { get { return item.AisleLocation; } set { item.AisleLocation = value; } }
        public string ShelfLocation { get { return item.ShelfLocation; } set { item.ShelfLocation = value; } }
        public string SpaceId { get { return item.SpaceId; } set { item.SpaceId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BuildingRoom { get; set; }
        public string ItemNotes { get { return item.ItemNotes; } set { item.ItemNotes = value; } }
        public string PhysicalId { get { return item.PhysicalId; } set { item.PhysicalId = value; } }
        public int? PhysicalItemId { get { return item.PhysicalItemId; } set { item.PhysicalItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrentLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InventoryTypeOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CategoryOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubCategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubCategory { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubCategoryOrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Ownership { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseVendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsidePoNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchasePoId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReceiveDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchasePoNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PoCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseInvoiceNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseInvoiceDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Consignor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementNumber { get; set; }
        public string ManufacturerId { get { return item.ManufacturerId; } set { item.ManufacturerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Manufacturer { get; set; }
        public string OriginalShowId { get { return item.OriginalShowId; } set { item.OriginalShowId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OriginalShow { get; set; }
        public string ConditionId { get { return item.ConditionId; } set { item.ConditionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SurfaceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WallTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OpeningId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ResponsiblePersonId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ResponsiblePerson { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BuyerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Buyer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReceiptNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? DepreciationMonths { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RepairId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RepairNumber { get; set; }
        public bool QcRequired { get { return item.QcRequired; } set { item.QcRequired = value; } }
        public int? WidthFt { get { return item.WidthFt; } set { item.WidthFt = value; } }
        public int? WidthIn { get { return item.WidthIn; } set { item.WidthIn = value; } }
        public int? HeightFt { get { return item.HeightFt; } set { item.HeightFt = value; } }
        public int? HeightIn { get { return item.HeightIn; } set { item.HeightIn = value; } }
        public int? LengthFt { get { return item.LengthFt; } set { item.LengthFt = value; } }
        public int? LengthIn { get { return item.LengthIn; } set { item.LengthIn = value; } }
        public decimal? CurrentMeter { get { return item.CurrentMeter; } set { item.CurrentMeter = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool TrackAssetUsage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool TrackLampUsage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool TrackStrikes { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool TrackCandles { get; set; }
        public int? AssetHours { get { return item.AssetHours; } set { item.AssetHours = value; } }
        public int? LampHours1 { get { return item.LampHours1; } set { item.LampHours1 = value; } }
        public int? LampHours2 { get { return item.LampHours2; } set { item.LampHours2 = value; } }
        public int? LampHours3 { get { return item.LampHours3; } set { item.LampHours3 = value; } }
        public int? LampHours4 { get { return item.LampHours4; } set { item.LampHours4 = value; } }
        public int? Strikes { get { return item.Strikes; } set { item.Strikes = value; } }
        public int? FootCandles { get { return item.FootCandles; } set { item.FootCandles = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pattern { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Gender { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Label { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Material { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Period { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CleaningFeeAmount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WardrobeSize { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WardrobePieceCount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool InventoryTypeIsProps { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool InventoryTypeIsWardrobe { get; set; }
        public string OrderLocationId { get { return item.OrderLocationId; } set { item.OrderLocationId = value; } }
        public string ContainerNumber { get { return item.ContainerNumber; } set { item.ContainerNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool IsWardrobe { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool IsProps { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DailyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyRate { get; set; }
        public string Location { get { return item.Location; } set { item.Location = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RetiredReason { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Datstamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}