using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.Master
{
    [FwSqlTable("inventoryview")]
    public class MasterLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unittype", modeltype: FwDataTypes.Text)]
        public string UnitType { get; set; }
        //------------------------------------------------------------------------------------ 



        /*
                [FwSqlDataField(column: "masterakatext", modeltype: FwDataTypes.Text)]
                public string Masterakatext { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "masterakatext255", modeltype: FwDataTypes.Text)]
                public string Masterakatext255 { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
                public string Vendor { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
                public string WarehouseId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
                public string Whcode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
                public string Warehouse { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
                public string Aisleloc { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
                public string Shelfloc { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
                public string CurrencyId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
                public string Currencycode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal)]
                public decimal Hourlyrate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
                public decimal Dailyrate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
                public decimal Weeklyrate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
                public decimal Week2rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
                public decimal Week3rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
                public decimal Week4rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
                public decimal Week5rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
                public decimal Monthlyrate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal)]
                public decimal Hourlycost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal)]
                public decimal Dailycost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal)]
                public decimal Weeklycost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal)]
                public decimal Monthlycost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
                public decimal Price { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
                public decimal Cost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal)]
                public decimal Retail { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "markup", modeltype: FwDataTypes.Decimal)]
                public decimal Markup { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "reorderqty", modeltype: FwDataTypes.Integer)]
                public int Reorderqty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "reorderpoint", modeltype: FwDataTypes.Integer)]
                public int Reorderpoint { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal)]
                public decimal Defaultcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
                public int Qty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hasqty", modeltype: FwDataTypes.Boolean)]
                public bool Hasqty { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtyconsigned", modeltype: FwDataTypes.Decimal)]
                public decimal Qtyconsigned { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtyallocated", modeltype: FwDataTypes.Decimal)]
                public decimal Qtyallocated { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtyintransit", modeltype: FwDataTypes.Decimal)]
                public decimal Qtyintransit { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
                public decimal Qtystaged { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtyonpo", modeltype: FwDataTypes.Integer)]
                public int Qtyonpo { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
                public decimal Qtyin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text)]
                public string PhysicalId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lastphydate", modeltype: FwDataTypes.UTCDateTime)]
                public string Lastphydate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
                public bool Fixedasset { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "nodiscount", modeltype: FwDataTypes.Boolean)]
                public bool Nodiscount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Text)]
                public string Hazardousmaterial { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal)]
                public decimal Replacementcost { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal)]
                public decimal Manifestvalue { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
                public string Notes { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
                public string Partnumber { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "originalicode", modeltype: FwDataTypes.Text)]
                public string Originalicode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
                public string Ratetype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text)]
                public string MfgId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
                public string Manufacturer { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipweightlbs", modeltype: FwDataTypes.Integer)]
                public int Shipweightlbs { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipweightoz", modeltype: FwDataTypes.Integer)]
                public int Shipweightoz { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightwcaselbs", modeltype: FwDataTypes.Integer)]
                public int Weightwcaselbs { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightwcaseoz", modeltype: FwDataTypes.Integer)]
                public int Weightwcaseoz { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "widthft", modeltype: FwDataTypes.Integer)]
                public int Widthft { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "widthin", modeltype: FwDataTypes.Integer)]
                public int Widthin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "heightft", modeltype: FwDataTypes.Integer)]
                public int Heightft { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "heightin", modeltype: FwDataTypes.Integer)]
                public int Heightin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthft", modeltype: FwDataTypes.Integer)]
                public int Lengthft { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthin", modeltype: FwDataTypes.Integer)]
                public int Lengthin { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipweightkg", modeltype: FwDataTypes.Integer)]
                public int Shipweightkg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "shipweightg", modeltype: FwDataTypes.Integer)]
                public int Shipweightg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightwcasekg", modeltype: FwDataTypes.Integer)]
                public int Weightwcasekg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "weightwcaseg", modeltype: FwDataTypes.Integer)]
                public int Weightwcaseg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "widthm", modeltype: FwDataTypes.Integer)]
                public int Widthm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "widthcm", modeltype: FwDataTypes.Integer)]
                public int Widthcm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "heightm", modeltype: FwDataTypes.Integer)]
                public int Heightm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "heightcm", modeltype: FwDataTypes.Integer)]
                public int Heightcm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthm", modeltype: FwDataTypes.Integer)]
                public int Lengthm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lengthcm", modeltype: FwDataTypes.Integer)]
                public int Lengthcm { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dw", modeltype: FwDataTypes.Decimal)]
                public decimal Dw { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "metered", modeltype: FwDataTypes.Boolean)]
                public bool Metered { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text)]
                public string OriginalshowId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "originalshow", modeltype: FwDataTypes.Text)]
                public string Originalshow { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "container", modeltype: FwDataTypes.Boolean)]
                public bool Container { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
                public bool Qcrequired { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "hastieredprice", modeltype: FwDataTypes.Boolean)]
                public bool Hastieredprice { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "chargetype", modeltype: FwDataTypes.Text)]
                public string Chargetype { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "productionexchangeecode", modeltype: FwDataTypes.Text)]
                public string Productionexchangeecode { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "displaywhenrateiszero", modeltype: FwDataTypes.Boolean)]
                public bool Displaywhenrateiszero { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "invdeptisprops", modeltype: FwDataTypes.Boolean)]
                public bool Invdeptisprops { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "invdeptiswardrobe", modeltype: FwDataTypes.Boolean)]
                public bool Invdeptiswardrobe { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text)]
                public string ContainerId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "scannablemasterid", modeltype: FwDataTypes.Text)]
                public string ScannablemasterId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
                public bool Availbyhour { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean)]
                public bool Availbydeal { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean)]
                public bool Availbyasset { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "unlockweek4rate", modeltype: FwDataTypes.Boolean)]
                public bool Unlockweek4rate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "patternid", modeltype: FwDataTypes.Text)]
                public string PatternId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "periodid", modeltype: FwDataTypes.Text)]
                public string PeriodId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "materialid", modeltype: FwDataTypes.Text)]
                public string MaterialId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "genderid", modeltype: FwDataTypes.Text)]
                public string GenderId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "labelid", modeltype: FwDataTypes.Text)]
                public string LabelId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobesize", modeltype: FwDataTypes.Text)]
                public string Wardrobesize { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobepiececount", modeltype: FwDataTypes.Integer)]
                public int Wardrobepiececount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "tracksoftware", modeltype: FwDataTypes.Boolean)]
                public bool Tracksoftware { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackassetusageflg", modeltype: FwDataTypes.Boolean)]
                public bool Trackassetusageflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "tracklampusageflg", modeltype: FwDataTypes.Boolean)]
                public bool Tracklampusageflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackstrikesflg", modeltype: FwDataTypes.Boolean)]
                public bool Trackstrikesflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "trackcandlesflg", modeltype: FwDataTypes.Boolean)]
                public bool Trackcandlesflg { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "minfootcandles", modeltype: FwDataTypes.Integer)]
                public int Minfootcandles { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "lampcount", modeltype: FwDataTypes.Integer)]
                public int Lampcount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "defaultprorateweeks", modeltype: FwDataTypes.Boolean)]
                public bool Defaultprorateweeks { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "defaultproratemonths", modeltype: FwDataTypes.Boolean)]
                public bool Defaultproratemonths { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "includeonpicklist", modeltype: FwDataTypes.Boolean)]
                public bool Includeonpicklist { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal)]
                public decimal Maxdiscount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "dyed", modeltype: FwDataTypes.Boolean)]
                public bool Dyed { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobesourceid", modeltype: FwDataTypes.Text)]
                public string WardrobesourceId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "wardrobecareid", modeltype: FwDataTypes.Text)]
                public string WardrobecareId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "cleaningfeeamount", modeltype: FwDataTypes.Decimal)]
                public decimal Cleaningfeeamount { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime)]
                public string Inputdate { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "overrideprofitlosscategory", modeltype: FwDataTypes.Boolean)]
                public bool Overrideprofitlosscategory { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "plcategoryid", modeltype: FwDataTypes.Text)]
                public string PlcategoryId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "plcategory", modeltype: FwDataTypes.Text)]
                public string Plcategory { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "plinventorydepartmentid", modeltype: FwDataTypes.Text)]
                public string PlinventorydepartmentId { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "plinventorydepartment", modeltype: FwDataTypes.Text)]
                public string Plinventorydepartment { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "starttime", modeltype: FwDataTypes.Text)]
                public string Starttime { get; set; }
                //------------------------------------------------------------------------------------ 
                [FwSqlDataField(column: "stoptime", modeltype: FwDataTypes.Text)]
                public string Stoptime { get; set; }
                //------------------------------------------------------------------------------------ 
        */
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}