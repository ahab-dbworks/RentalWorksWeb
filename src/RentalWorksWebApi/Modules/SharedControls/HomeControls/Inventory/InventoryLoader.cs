using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Modules.HomeControls.Master;

namespace WebApi.Modules.HomeControls.Inventory
{
    [FwSqlTable("inventoryview")]
    public abstract class InventoryLoader : InventoryBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        //public string InventoryId { get; set; } = "";
        ////------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        //public string InventoryTypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        //public string InventoryType { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        //public string TrackedBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        //public string Rank { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        //public string ManufacturerPartNumber { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "mfgurl", modeltype: FwDataTypes.Text)]
        public string ManufacturerUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donotprintimage", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeImageFromQuoteOrderPrint { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        public bool? NoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availmanuallyresolveconflict", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityManuallyResolveConflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sendavailabilityalert", modeltype: FwDataTypes.Boolean)]
        public bool? SendAvailabilityAlert { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionuniqueid", modeltype: FwDataTypes.Text)]
        public string PrimaryDimensionUniqueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensiondescription", modeltype: FwDataTypes.Text)]
        public string PrimaryDimensionDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightlbs", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionShipWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightoz", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionShipWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaselbs", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWeightInCaseLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaseoz", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWeightInCaseOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthft", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthin", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightft", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionHeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightin", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionHeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthft", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionLengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthin", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionLengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightkg", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionShipWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightg", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionShipWeightG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcasekg", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWeightInCaseKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaseg", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWeightInCaseG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWidthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthcm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionWidthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionHeightM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightcm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionHeightCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionLengthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthcm", modeltype: FwDataTypes.Integer)]
        public int? PrimaryDimensionLengthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionuniqueid", modeltype: FwDataTypes.Text)]
        public string SecondaryDimensionUniqueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensiondescription", modeltype: FwDataTypes.Text)]
        public string SecondaryDimensionDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightlbs", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionShipWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightoz", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionShipWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaselbs", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWeightInCaseLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaseoz", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWeightInCaseOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthft", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthin", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightft", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionHeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightin", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionHeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthft", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionLengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthin", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionLengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightkg", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionShipWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightg", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionShipWeightG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcasekg", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWeightInCaseKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaseg", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWeightInCaseG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWidthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthcm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionWidthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionHeightM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightcm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionHeightCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionLengthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthcm", modeltype: FwDataTypes.Integer)]
        public int? SecondaryDimensionLengthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforiginid", modeltype: FwDataTypes.Text)]
        public string CountryOfOriginId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string CountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "displaywhenrateiszero", modeltype: FwDataTypes.Boolean)]
        public bool? DisplayInSummaryModeWhenRateIsZero { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        //public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qctime", modeltype: FwDataTypes.Text)]
        public string QcTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "copyattributesasnote", modeltype: FwDataTypes.Boolean)]
        public bool? CopyAttributesAsNote { get; set; }
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
        [FwSqlDataField(column: "lampcount", modeltype: FwDataTypes.Integer)]
        public int? LampCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "minfootcandles", modeltype: FwDataTypes.Integer)]
        public int? MinimumFootCandles { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tracksoftware", modeltype: FwDataTypes.Boolean)]
        public bool? TrackSoftware { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "softwareversion", modeltype: FwDataTypes.Text)]
        public string SoftwareVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "softwareeffectivedate", modeltype: FwDataTypes.Date)]
        public string SoftwareEffectiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousespecific", modeltype: FwDataTypes.Boolean)]
        public bool? WarehouseSpecificPackage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageprice", modeltype: FwDataTypes.Text)]
        public string CompletePackagePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageprice", modeltype: FwDataTypes.Text)]
        public string KitPackagePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "separate", modeltype: FwDataTypes.Boolean)]
        public bool? SeparatePackageOnQuoteOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text)]
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterid", modeltype: FwDataTypes.Text)]
        public string ContainerScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterno", modeltype: FwDataTypes.Text)]
        public string ContainerScannableICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemaster", modeltype: FwDataTypes.Text)]
        public string ContainerScannableDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "autorebuildcontaineratcheckin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AutomaticallyRebuildContainerAtCheckIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "autorebuildcontaineratxferin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AutomaticallyRebuildContainerAtTransferIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerstagingrule", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]  /* AUTOADD / WARN / NOWARN / ERROR */
        public string ContainerStagingRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludecontainedfromavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ExcludeContainedItemsFromAvailability { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usecontainerno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseContainerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerlistbehavior", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)] /* AUTOPRINT / PROMPT / NONE */
        public string ContainerPackingListBehavior { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "patternid", modeltype: FwDataTypes.Text)]
        public string PatternId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pattern", modeltype: FwDataTypes.Text)]
        public string Pattern { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodid", modeltype: FwDataTypes.Text)]
        public string PeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "period", modeltype: FwDataTypes.Text)]
        public string Period { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "materialid", modeltype: FwDataTypes.Text)]
        public string MaterialId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "material", modeltype: FwDataTypes.Text)]
        public string Material { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "genderid", modeltype: FwDataTypes.Text)]
        public string GenderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gender", modeltype: FwDataTypes.Text)]
        public string Gender { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labelid", modeltype: FwDataTypes.Text)]
        public string LabelId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label", modeltype: FwDataTypes.Text)]
        public string Label { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobesize", modeltype: FwDataTypes.Text)]
        public string WardrobeSize { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobepiececount", modeltype: FwDataTypes.Integer)]
        public int? WardrobePieceCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dyed", modeltype: FwDataTypes.Boolean)]
        public bool? Dyed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobesourceid", modeltype: FwDataTypes.Text)]
        public string WardrobeSourceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobesource", modeltype: FwDataTypes.Text)]
        public string WardrobeSource { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobecareid", modeltype: FwDataTypes.Text)]
        public string WardrobeCareId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobecare", modeltype: FwDataTypes.Text)]
        public string WardrobeCare { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cleaningfeeamount", modeltype: FwDataTypes.Decimal)]
        public decimal? CleaningFeeAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wardrobedetaileddescription", modeltype: FwDataTypes.Text)]
        public string WardrobeDetailedDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdeptiswardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? InventoryTypeIsWardrobe { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdeptisset", modeltype: FwDataTypes.Boolean)]
        public bool? InventoryTypeIsSets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webdetaileddescription", modeltype: FwDataTypes.Text)]
        public string WebDetailedDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overridepackagerevenuedefault", modeltype: FwDataTypes.Boolean)]
        public bool? OverrideSystemDefaultRevenueAllocationBehavior { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "splitpackagerevenue", modeltype: FwDataTypes.Boolean)]
        public bool? AllocateRevenueForAccessories { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packagerevenuebasedon", modeltype: FwDataTypes.Text)]
        public string PackageRevenueCalculationFormula { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "assetaccountid", modeltype: FwDataTypes.Text)]
        public string AssetAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetglno", modeltype: FwDataTypes.Text)]
        public string AssetAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assetglacctdesc", modeltype: FwDataTypes.Text)]
        public string AssetAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeaccountid", modeltype: FwDataTypes.Text)]
        public string IncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglno", modeltype: FwDataTypes.Text)]
        public string IncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "incomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string IncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeaccountid", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglno", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string SubIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeaccountid", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeglno", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ldincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string LdIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeaccountid", modeltype: FwDataTypes.Text)]
        public string EquipmentSaleIncomeAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeglno", modeltype: FwDataTypes.Text)]
        public string EquipmentSaleIncomeAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "equipsaleincomeglacctdesc", modeltype: FwDataTypes.Text)]
        public string EquipmentSaleIncomeAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseaccountid", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglno", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string ExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseglno", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogsexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseaccountid", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseglno", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cogrexpenseglacctdesc", modeltype: FwDataTypes.Text)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Boolean)]
        public bool? IsHazardousMaterial { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "masterakatext", modeltype: FwDataTypes.Text)]
        public string DescriptionWithAkas { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "costcalculation", modeltype: FwDataTypes.Text)]
        public string CostCalculation { get; set; }
        //------------------------------------------------------------------------------------ 


        // for cusomizing browse 
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        //public string AisleLocation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        //public string ShelfLocation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Boolean)]
        //public bool? Taxable { get; set; }
        ////------------------------------------------------------------------------------------ 






    }
}