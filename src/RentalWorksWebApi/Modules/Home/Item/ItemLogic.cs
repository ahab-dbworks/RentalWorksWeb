using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.Item
{
    [FwLogic(Id:"5A523YbmjOwd")]
    public class ItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected ItemRecord item = new ItemRecord();
        protected ItemLoader itemLoader = new ItemLoader();
        protected ItemBrowseLoader itemBrowseLoader = new ItemBrowseLoader();
        public ItemLogic()
        {
            dataRecords.Add(item);
            dataLoader = itemLoader;
            browseLoader = itemBrowseLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"ow9igbdLMgEw", IsPrimaryKey:true)]
        public virtual string ItemId { get { return item.ItemId; } set { item.ItemId = value; } }

        [FwLogicProperty(Id:"uG5tES8ozJya")]
        public string InventoryId { get { return item.InventoryId; } set { item.InventoryId = value; } }

        [FwLogicProperty(Id:"jn7nmgq7DJqm", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"wuEvyxJgqENx", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"BtK2J7Lt0ojO", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"Np4ru0E6a2hi")]
        public string ItemDescription { get { return item.ItemDescription; } set { item.ItemDescription = value; } }

        [FwLogicProperty(Id:"TQo3QrlXEiNo", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"6eov9pMfP7yi", IsReadOnly:true)]
        public string AvailFor { get; set; }

        [FwLogicProperty(Id:"6oWC1s61KT9j", IsReadOnly:true)]
        public decimal? ReplacementCost { get; set; }

        [FwLogicProperty(Id:"kISTXsP1ItYL", IsReadOnly:true)]
        public string Classification { get; set; }

        [FwLogicProperty(Id:"BxFj4yjlzPx8", IsReadOnly:true)]
        public bool? IsContainer { get; set; }

        [FwLogicProperty(Id:"iE1qGSPE0Dia", IsReadOnly:true)]
        public string ContainerId { get; set; }

        [FwLogicProperty(Id:"P5BN4ziJAxmQ", IsReadOnly:true)]
        public string ContainerInventoryId { get; set; }

        [FwLogicProperty(Id:"J0h8rkSI7g0S", IsReadOnly:true)]
        public string ContainerICode { get; set; }

        [FwLogicProperty(Id:"jokCg7EsW0IZ", IsReadOnly:true)]
        public string ContainerDescription { get; set; }

        [FwLogicProperty(Id:"2vzSapDug3y8", IsReadOnly:true)]
        public string ContainerStatus { get; set; }

        [FwLogicProperty(Id:"YPNYer7cUWQs", IsReadOnly:true)]
        public string ContainerStatusColor { get; set; }

        [FwLogicProperty(Id: "IIiYemJaXj9K9", IsReadOnly:true)]
        public virtual string ContainerItemId { get; set; }

        [FwLogicProperty(Id:"2vzSapDug3y8", IsReadOnly:true)]
        public string ContainerStatusDate { get; set; }

        [FwLogicProperty(Id:"lgb2kRnMEFYy", IsReadOnly:true)]
        public bool? FixedAsset { get; set; }

        [FwLogicProperty(Id:"4F7E6Sp2rYSs", IsReadOnly:true)]
        public bool? Rank { get; set; }

        [FwLogicProperty(Id:"SSQlcW1A2vNC", IsReadOnly:true)]
        public string StatusType { get; set; }

        [FwLogicProperty(Id:"M8UBOJ8jLKZl", IsReadOnly:true)]
        public string StatusDate { get; set; }

        [FwLogicProperty(Id:"eW70jsFJ22b3", IsReadOnly:true)]
        public string InventoryStatus { get; set; }

        [FwLogicProperty(Id:"eW70jsFJ22b3", IsReadOnly:true)]
        public string InventoryStatusId { get; set; }

        [FwLogicProperty(Id:"YPNYer7cUWQs", IsReadOnly:true)]
        public string Color { get; set; }

        [FwLogicProperty(Id:"YPNYer7cUWQs", IsReadOnly:true)]
        public string TextColor { get; set; }

        [FwLogicProperty(Id:"N82f7q1iQoIj")]
        public string BarCode { get { return item.BarCode; } set { item.BarCode = value; } }

        [FwLogicProperty(Id:"CzJzJBRICMxU", IsReadOnly:true)]
        public string BarCodeForScanning { get; set; }

        [FwLogicProperty(Id:"UZ5LC23eN2H3")]
        public string SerialNumber { get { return item.SerialNumber; } set { item.SerialNumber = value; } }

        [FwLogicProperty(Id:"09AUGmpF7k9l")]
        public string RfId { get { return item.RfId; } set { item.RfId = value; } }

        [FwLogicProperty(Id:"gJWynCmcbeOk")]
        public string OldBarCode { get { return item.OldBarCode; } set { item.OldBarCode = value; } }

        [FwLogicProperty(Id:"Evzk4geoyQ58")]
        public string OldSerialNumber { get { return item.OldSerialNumber; } set { item.OldSerialNumber = value; } }

        [FwLogicProperty(Id:"wggAQFRYDdyk")]
        public string OldRfid { get { return item.OldRfid; } set { item.OldRfid = value; } }

        [FwLogicProperty(Id:"4RzYd0cz8LED")]
        public string ManufacturerPartNumber { get { return item.ManufacturerPartNumber; } set { item.ManufacturerPartNumber = value; } }

        [FwLogicProperty(Id:"hiI7dQ0iGnPw")]
        public string ManufactureDate { get { return item.ManufactureDate; } set { item.ManufactureDate = value; } }

        [FwLogicProperty(Id:"TQo3QrlXEiNo", IsRecordTitle:true, IsReadOnly:true)]
        public string TrackedByCode { get; set; }

        [FwLogicProperty(Id:"BcpCJfH3YkN7", IsReadOnly:true)]
        public int? AvailOwnershipSort { get; set; }

        [FwLogicProperty(Id:"SMidXt066ePt")]
        public bool? IsNegativeInventory { get { return item.IsNegativeInventory; } set { item.IsNegativeInventory = value; } }

        [FwLogicProperty(Id:"IrE1ifaivH18")]
        public string InspectionNo { get { return item.InspectionNo; } set { item.InspectionNo = value; } }

        [FwLogicProperty(Id:"uEFZh9wJBCF8")]
        public string InspectionVendorId { get { return item.InspectionVendorId; } set { item.InspectionVendorId = value; } }

        [FwLogicProperty(Id:"eaYpDkJPU13u", IsReadOnly:true)]
        public string InspectionVendor { get; set; }

        [FwLogicProperty(Id:"L0I08WA4UXrn")]
        public string ManufacturerModelNumber { get { return item.ManufacturerModelNumber; } set { item.ManufacturerModelNumber = value; } }

        [FwLogicProperty(Id:"V7cnfr29t4OL")]
        public string PurchaseId { get { return item.PurchaseId; } set { item.PurchaseId = value; } }

        [FwLogicProperty(Id:"PHULh5L5WYWc")]
        public string AisleLocation { get { return item.AisleLocation; } set { item.AisleLocation = value; } }

        [FwLogicProperty(Id:"rbfsrv8g3azQ")]
        public string ShelfLocation { get { return item.ShelfLocation; } set { item.ShelfLocation = value; } }

        [FwLogicProperty(Id:"kxC1ujroQvAR")]
        public string SpaceId { get { return item.SpaceId; } set { item.SpaceId = value; } }

        [FwLogicProperty(Id:"a76o7WZdUmi4", IsReadOnly:true)]
        public string BuildingRoom { get; set; }

        [FwLogicProperty(Id:"KuffVP7JGBWn")]
        public string ItemNotes { get { return item.ItemNotes; } set { item.ItemNotes = value; } }

        [FwLogicProperty(Id:"vJkjhjNt3T1h")]
        public string PhysicalId { get { return item.PhysicalId; } set { item.PhysicalId = value; } }

        [FwLogicProperty(Id:"xIYNsHwMzNcj")]
        public int? PhysicalItemId { get { return item.PhysicalItemId; } set { item.PhysicalItemId = value; } }

        [FwLogicProperty(Id:"5ZWRGulmdjus", IsReadOnly:true)]
        public string PhysicalDate { get; set; }

        [FwLogicProperty(Id:"c6ESGy3TRxT8", IsReadOnly:true)]
        public string PhysicalBy { get; set; }

        [FwLogicProperty(Id:"jn7nmgq7DJqm", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"jn7nmgq7DJqm", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"Nj7h8TCaXDWt", IsReadOnly:true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"jS1qyKg3rMxE", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"XFRXnGM3Z2pz", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"BtK2J7Lt0ojO", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"FoI8gGkLzkI9", IsReadOnly:true)]
        public string CurrentLocation { get; set; }

        [FwLogicProperty(Id:"Jw0cEKdmlUIN", IsReadOnly:true)]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id:"Jw0cEKdmlUIN", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"Jw0cEKdmlUIN", IsReadOnly:true)]
        public int? InventoryTypeOrderBy { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public decimal? CategoryOrderBy { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id:"EoFC11PkENAK", IsReadOnly:true)]
        public decimal? SubCategoryOrderBy { get; set; }

        [FwLogicProperty(Id:"GyP44hpQMpqO", IsReadOnly:true)]
        public string Ownership { get; set; }

        [FwLogicProperty(Id:"vpDxRAYQRDPR", IsReadOnly:true)]
        public string PurchaseVendorId { get; set; }

        [FwLogicProperty(Id:"vpDxRAYQRDPR", IsReadOnly:true)]
        public string PurchaseVendor { get; set; }

        [FwLogicProperty(Id:"zf42tuOKRU4W", IsReadOnly:true)]
        public string OutsidePoNumber { get; set; }

        [FwLogicProperty(Id:"drb91EWQr4tk", IsReadOnly:true)]
        public string PurchasePoId { get; set; }

        [FwLogicProperty(Id:"M3bkM9BBGT6I", IsReadOnly:true)]
        public string PurchaseDate { get; set; }

        [FwLogicProperty(Id:"N8Ief0IoPt7t", IsReadOnly:true)]
        public string ReceiveDate { get; set; }

        [FwLogicProperty(Id:"Qdhm7s0FHeLv", IsReadOnly:true)]
        public string PurchasePoNumber { get; set; }

        [FwLogicProperty(Id:"ccshcsduC566", IsReadOnly:true)]
        public decimal? PoCost { get; set; }

        [FwLogicProperty(Id:"pgQowWhNAEm1", IsReadOnly:true)]
        public decimal? InvoiceCost { get; set; }

        [FwLogicProperty(Id:"77zVEEwYb22k", IsReadOnly:true)]
        public string PurchaseInvoiceNumber { get; set; }

        [FwLogicProperty(Id:"4aZwjeTyLTyT", IsReadOnly:true)]
        public string PurchaseInvoiceDate { get; set; }

        [FwLogicProperty(Id:"Wg7oOOaORNvJ", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"Wg7oOOaORNvJ", IsReadOnly:true)]
        public string Consignor { get; set; }

        [FwLogicProperty(Id:"Wg7oOOaORNvJ", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

        [FwLogicProperty(Id:"Wg7oOOaORNvJ", IsReadOnly:true)]
        public string ConsignorAgreementNumber { get; set; }

        [FwLogicProperty(Id:"ogGT0fBbMzcO")]
        public string ManufacturerId { get { return item.ManufacturerId; } set { item.ManufacturerId = value; } }

        [FwLogicProperty(Id:"CWibbpUx2HVJ", IsReadOnly:true)]
        public string Manufacturer { get; set; }

        [FwLogicProperty(Id:"RRYZglLfHhs0")]
        public string OriginalShowId { get { return item.OriginalShowId; } set { item.OriginalShowId = value; } }

        [FwLogicProperty(Id:"dSzCErnJtIlm", IsReadOnly:true)]
        public string OriginalShow { get; set; }

        [FwLogicProperty(Id:"cSvyaLGabicC")]
        public string ConditionId { get { return item.ConditionId; } set { item.ConditionId = value; } }

        [FwLogicProperty(Id:"PkLrTv5dE40X", IsReadOnly:true)]
        public string Condition { get; set; }

        [FwLogicProperty(Id:"mVUmpAwVl613", IsReadOnly:true)]
        public string SurfaceId { get; set; }

        [FwLogicProperty(Id:"mVUmpAwVl613", IsReadOnly:true)]
        public string Surface { get; set; }

        [FwLogicProperty(Id:"gREaS2bWg7M4", IsReadOnly:true)]
        public string WallTypeId { get; set; }

        [FwLogicProperty(Id:"gREaS2bWg7M4", IsReadOnly:true)]
        public string WallType { get; set; }

        [FwLogicProperty(Id:"KJzxLE1Ivgpw", IsReadOnly:true)]
        public string OpeningId { get; set; }

        [FwLogicProperty(Id:"KJzxLE1Ivgpw", IsReadOnly:true)]
        public string Opening { get; set; }

        [FwLogicProperty(Id:"CmK0ij8S7Zxp", IsReadOnly:true)]
        public string ResponsiblePersonId { get; set; }

        [FwLogicProperty(Id:"CmK0ij8S7Zxp", IsReadOnly:true)]
        public string ResponsiblePerson { get; set; }

        [FwLogicProperty(Id:"n5G5u8GxqXlG", IsReadOnly:true)]
        public string BuyerId { get; set; }

        [FwLogicProperty(Id:"n5G5u8GxqXlG", IsReadOnly:true)]
        public string Buyer { get; set; }

        [FwLogicProperty(Id:"hsQ1KVD9mGgC", IsReadOnly:true)]
        public string ReceiptNumber { get; set; }

        [FwLogicProperty(Id:"LiTf5rnRQnZ1", IsReadOnly:true)]
        public int? DepreciationMonths { get; set; }

        [FwLogicProperty(Id:"AKtV4mVQE8rb", IsReadOnly:true)]
        public string RepairId { get; set; }

        [FwLogicProperty(Id:"XmlNQLR5adnA", IsReadOnly:true)]
        public string RepairNumber { get; set; }

        [FwLogicProperty(Id:"ov6ifkgLUyz4")]
        public bool? QcRequired { get { return item.QcRequired; } set { item.QcRequired = value; } }

        [FwLogicProperty(Id:"y4yFkxzQtNXC")]
        public int? WidthFt { get { return item.WidthFt; } set { item.WidthFt = value; } }

        [FwLogicProperty(Id:"qp8NIKRIog1Z")]
        public int? WidthIn { get { return item.WidthIn; } set { item.WidthIn = value; } }

        [FwLogicProperty(Id:"HhL71ACCH71d")]
        public int? HeightFt { get { return item.HeightFt; } set { item.HeightFt = value; } }

        [FwLogicProperty(Id:"rqZBahgOvhJj")]
        public int? HeightIn { get { return item.HeightIn; } set { item.HeightIn = value; } }

        [FwLogicProperty(Id:"frQ8Bx5X0bsG")]
        public int? LengthFt { get { return item.LengthFt; } set { item.LengthFt = value; } }

        [FwLogicProperty(Id:"CEHqv7VQZyBS")]
        public int? LengthIn { get { return item.LengthIn; } set { item.LengthIn = value; } }

        [FwLogicProperty(Id:"wt4zG8sgnVqD")]
        public decimal? CurrentMeter { get { return item.CurrentMeter; } set { item.CurrentMeter = value; } }

        [FwLogicProperty(Id:"GmTchnToq5CZ", IsReadOnly:true)]
        public bool? TrackAssetUsage { get; set; }

        [FwLogicProperty(Id:"uYBpaZqjdayC", IsReadOnly:true)]
        public bool? TrackLampUsage { get; set; }

        [FwLogicProperty(Id:"Q4OqUJyZXeku", IsReadOnly:true)]
        public bool? TrackStrikes { get; set; }

        [FwLogicProperty(Id:"0mNadkaG0WcZ", IsReadOnly:true)]
        public bool? TrackCandles { get; set; }

        [FwLogicProperty(Id:"OhSQLIh5t6Ho")]
        public int? AssetHours { get { return item.AssetHours; } set { item.AssetHours = value; } }

        [FwLogicProperty(Id:"FVq1lIagvdBJ")]
        public int? LampHours1 { get { return item.LampHours1; } set { item.LampHours1 = value; } }

        [FwLogicProperty(Id:"qGXXCHzgM5kE")]
        public int? LampHours2 { get { return item.LampHours2; } set { item.LampHours2 = value; } }

        [FwLogicProperty(Id:"PILafGcgTIgA")]
        public int? LampHours3 { get { return item.LampHours3; } set { item.LampHours3 = value; } }

        [FwLogicProperty(Id:"fD4z42NbehMz")]
        public int? LampHours4 { get { return item.LampHours4; } set { item.LampHours4 = value; } }

        [FwLogicProperty(Id:"9bCt0GNjxpoE")]
        public int? Strikes { get { return item.Strikes; } set { item.Strikes = value; } }

        [FwLogicProperty(Id:"Uw1mE7akfw01")]
        public int? FootCandles { get { return item.FootCandles; } set { item.FootCandles = value; } }

        [FwLogicProperty(Id:"AxOjLblF6wH1", IsReadOnly:true)]
        public string Pattern { get; set; }

        [FwLogicProperty(Id:"WNMZ8EVompBJ", IsReadOnly:true)]
        public string Gender { get; set; }

        [FwLogicProperty(Id:"2JfZVPp537u2", IsReadOnly:true)]
        public string Label { get; set; }

        [FwLogicProperty(Id:"NQxed4Yt8L6n", IsReadOnly:true)]
        public string Material { get; set; }

        [FwLogicProperty(Id:"xFN2ANxIHBby", IsReadOnly:true)]
        public string Period { get; set; }

        [FwLogicProperty(Id:"1MHcnFmCnCw5", IsReadOnly:true)]
        public decimal? CleaningFeeAmount { get; set; }

        [FwLogicProperty(Id:"2fW9Jmf8upEZ", IsReadOnly:true)]
        public string WardrobeSize { get; set; }

        [FwLogicProperty(Id:"HTO39n8fsZLJ", IsReadOnly:true)]
        public int? WardrobePieceCount { get; set; }

        [FwLogicProperty(Id:"Jw0cEKdmlUIN", IsReadOnly:true)]
        public bool? InventoryTypeIsProps { get; set; }

        [FwLogicProperty(Id:"Jw0cEKdmlUIN", IsReadOnly:true)]
        public bool? InventoryTypeIsWardrobe { get; set; }

        [FwLogicProperty(Id:"05f8HgmUt2T6")]
        public string OrderLocationId { get { return item.OrderLocationId; } set { item.OrderLocationId = value; } }

        [FwLogicProperty(Id:"tE0o2yBfUTQS")]
        public string ContainerNumber { get { return item.ContainerNumber; } set { item.ContainerNumber = value; } }

        [FwLogicProperty(Id:"szFdy0Awspj4", IsReadOnly:true)]
        public bool? IsWardrobe { get; set; }

        [FwLogicProperty(Id:"AOsJMWukxD76", IsReadOnly:true)]
        public bool? IsProps { get; set; }

        [FwLogicProperty(Id:"yidQ96Nmm5VU", IsReadOnly:true)]
        public decimal? DailyRate { get; set; }

        [FwLogicProperty(Id:"sTA1LOpkYZAK", IsReadOnly:true)]
        public decimal? WeeklyRate { get; set; }

        [FwLogicProperty(Id:"qSOnXhtodNhH", IsReadOnly:true)]
        public decimal? MonthlyRate { get; set; }

        [FwLogicProperty(Id:"5H8p7B1ycfK4")]
        public string Location { get { return item.Location; } set { item.Location = value; } }

        [FwLogicProperty(Id:"njTDkxSvtJUI", IsReadOnly:true)]
        public string RetiredReason { get; set; }

        [FwLogicProperty(Id:"FFbyHoQtnb6v")]
        public string WarrantyExpiration { get { return item.WarrantyExpiration; } set { item.WarrantyExpiration = value; } }

        [FwLogicProperty(Id:"GoTodrLp5111")]
        public int? WarrantyPeriod { get { return item.WarrantyPeriod; } set { item.WarrantyPeriod = value; } }

        [FwLogicProperty(Id:"7Tc7kqzOr3Nz")]
        public string CountryOfOriginId { get { return item.CountryOfOriginId; } set { item.CountryOfOriginId = value; } }

        [FwLogicProperty(Id:"nz8exGDfPvoW", IsReadOnly:true)]
        public string CountryOfOrigin { get; set; }

        [FwLogicProperty(Id:"NDzxfnuVT3xu")]
        public string ShelfLifeExpirationDate { get { return item.ShelfLifeExpirationDate; } set { item.ShelfLifeExpirationDate = value; } }


        [FwLogicProperty(Id:"r0QM0272Wwsd", IsReadOnly:true)]
        public string CurrentDealId { get; set; }

        [FwLogicProperty(Id:"r0QM0272Wwsd", IsReadOnly:true)]
        public string CurrentDeal { get; set; }

        [FwLogicProperty(Id:"cbYfsMkQlfun", IsReadOnly:true)]
        public string CurrentCustomerId { get; set; }

        [FwLogicProperty(Id:"cbYfsMkQlfun", IsReadOnly:true)]
        public string CurrentCustomer { get; set; }

        [FwLogicProperty(Id:"TEthpjJNohPf", IsReadOnly:true)]
        public string CurrentOrderId { get; set; }

        [FwLogicProperty(Id:"FQtzKNsjzNU2", IsReadOnly:true)]
        public string CurrentOrderNumber { get; set; }

        [FwLogicProperty(Id:"An5X1tCqWMgj", IsReadOnly:true)]
        public string CurrentOrderDescription { get; set; }

        [FwLogicProperty(Id:"QNs5lm61LinE", IsReadOnly:true)]
        public string CurrentOrderDate { get; set; }

        [FwLogicProperty(Id:"2NGVJhtDYT1U", IsReadOnly:true)]
        public string CurrentOrderPickDate { get; set; }

        [FwLogicProperty(Id:"1wPnCepSF5uI", IsReadOnly:true)]
        public string CurrentOrderFromDate { get; set; }

        [FwLogicProperty(Id:"Wdh8QYoEVZJ8", IsReadOnly:true)]
        public string CurrentOrderToDate { get; set; }

        [FwLogicProperty(Id:"SvyWFcSXLQqq")]
        public bool? Inactive { get; set; }



        [FwLogicProperty(Id:"7YZwUCRVQ7v8")]
        public string DateStamp { get { return item.DateStamp; } set { item.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveNote = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveNote = true;
            }
            else if (e.Original != null)
            {
                ItemLogic orig = (ItemLogic)e.Original;
                doSaveNote = (!orig.ItemNotes.Equals(ItemNotes));
            }
            if (doSaveNote)
            {
                bool saved = item.SaveNoteASync(ItemNotes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
