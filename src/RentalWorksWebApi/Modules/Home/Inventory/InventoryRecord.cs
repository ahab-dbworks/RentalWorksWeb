using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Home.Inventory
{
    [FwSqlTable("master")]
    public class InventoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        /*
                [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Replacementcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "restockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
                public decimal Restockpercent { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "countryoforiginid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string CountryoforiginId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Manifestvalue { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                public string Inputdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                public string Moddate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "tariffcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
                public string Tariffcode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string Trackedby { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string MfgId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "nodiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Nodiscount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Qcrequired { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string Hazardousmaterial { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qctime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
                public string Qctime { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "mfgurl", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
                public string Mfgurl { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "mfgpdf", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
                public string Mfgpdf { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Fixedasset { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "restockfee", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
                public decimal Restockfee { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Noavail { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Rank { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string BuildingId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "commonsqftflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Commonsqftflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "floorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string FloorId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "metered", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Metered { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "meterrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
                public decimal Meterrate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "spacefromdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                public string Spacefromdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "spacetodate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                public string Spacetodate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string UnitId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Newmanifestvalue { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Newreplacementcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "surfaceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string SurfaceId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Hastieredcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)]
                public string Partnumber { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Oldmanifestvalue { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
                public decimal Oldreplacementcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "meterrateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string MeterrateId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "originalicode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
                public string Originalicode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
                public string Ratetype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string ModbyusersId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "packageprice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
                public string Packageprice { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "separate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Separate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string InputbyusersId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Contractinprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Contractoutprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Contractreceiveprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Contractreturnprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "copynotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Copynotes { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Invoiceprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "orbitsplitqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Orbitsplitqty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Orderprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Picklistprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Poprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Quoteprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Returnlistprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "sendavailabilityalert", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Sendavailabilityalert { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Orderby { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "openingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string OpeningId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "walltypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string WalltypeId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "sealeditem", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Sealeditem { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackedbyweight", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Trackedbyweight { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackedbylength", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string Trackedbylength { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hastieredprice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Hastieredprice { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightunitid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string WeightunitId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthunitid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string LengthunitId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightunitqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                public int Weightunitqty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthunitqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                public int Lengthunitqty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "keepqtyordered", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Keepqtyordered { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "productionexchangemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string ProductionexchangemasterId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "warehousespecific", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Warehousespecific { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "spacesqft", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
                public decimal Spacesqft { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackassetusageflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Trackassetusageflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "tracklampusageflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Tracklampusageflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackstrikesflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Trackstrikesflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackcandlesflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Trackcandlesflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "secondarydimensionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string SecondarydimensionId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "rentalmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string RentalmasterId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "displaywhenrateiszero", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Displaywhenrateiszero { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "patternid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string PatternId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "genderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string GenderId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "labelid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string LabelId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "materialid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string MaterialId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "periodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string PeriodId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "cleaningfeeamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
                public decimal Cleaningfeeamount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobesize", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                public string Wardrobesize { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobepiececount", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Wardrobepiececount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "availmanuallyresolveconflict", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Availmanuallyresolveconflict { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "splitpackagerevenue", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Splitpackagerevenue { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "packagerevenuebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                public string Packagerevenuebasedon { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "treatconsignedqtyasowned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Treatconsignedqtyasowned { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "copyattributesasnote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Copyattributesasnote { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lampcount", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Lampcount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "minfootcandles", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Minfootcandles { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "tracksoftware", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Tracksoftware { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "softwareversion", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
                public string Softwareversion { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "softwareeffectivedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                public string Softwareeffectivedate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "overridepackagerevenuedefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Overridepackagerevenuedefault { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "excludefromroa", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Excludefromroa { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string ContainerId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "containerstagingrule", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
                public string Containerstagingrule { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "excludecontainedfromavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Excludecontainedfromavail { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "nestpackages", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Nestpackages { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "unlockweek4rate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Unlockweek4rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "primarydimensionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string PrimarydimensionId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Orderbypicklist { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "donotprintimage", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Donotprintimage { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "containerlistbehavior", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
                public string Containerlistbehavior { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "autorebuildcontaineratxferin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Autorebuildcontaineratxferin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "autorebuildcontaineratcheckin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Autorebuildcontaineratcheckin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "defaultprorateweeks", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Defaultprorateweeks { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "defaultproratemonths", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Defaultproratemonths { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "proratemonthsby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
                public string Proratemonthsby { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string OriginalshowId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "occupancy", modeltype: FwDataTypes.Integer, sqltype: "int")]
                public int Occupancy { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "includeonpicklist", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Includeonpicklist { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dyed", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Dyed { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobecareid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string WardrobecareId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobesourceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string WardrobesourceId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "usecontainerno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Usecontainerno { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "overrideprofitlosscategory", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Overrideprofitlosscategory { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "profitlosscategoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string ProfitlosscategoryId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Profitlossgroup { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "assetaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string AssetaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "incomeaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string IncomeaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "subincomeaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string SubincomeaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "equipsaleincomeaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string EquipsaleincomeaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "ldincomeaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string LdincomeaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "expenseaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string ExpenseaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "cogsexpenseaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string CogsexpenseaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "cogrexpenseaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                public string CogrexpenseaccountId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "autoswap", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Autoswap { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "warrantyperiod", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
                public decimal Warrantyperiod { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Poreceivelistprint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                public bool Poreturnlistprint { get; set; }
                //------------------------------------------------------------------------------------ 
        */
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 3)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}