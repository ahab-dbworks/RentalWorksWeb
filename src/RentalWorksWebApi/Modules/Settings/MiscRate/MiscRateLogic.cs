using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;
using RentalWorksWebApi.Modules.Settings.Rate;

namespace RentalWorksWebApi.Modules.Settings.MiscRate
{
    public class MiscRateLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        MiscRateLoader inventoryLoader = new MiscRateLoader();
        public MiscRateLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }
        /*
                [FwBusinessLogicField(isReadOnly: true)]
                public string Masterakatext { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Masterakatext255 { get; set; }
                public string UnitId { get { return master.UnitId; } set { master.UnitId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Unit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Unittype { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Vendor { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string WarehouseId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Whcode { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Warehouse { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Aisleloc { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Shelfloc { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string CurrencyId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Currencycode { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Hourlyrate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Dailyrate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Weeklyrate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Week2rate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Week3rate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Week4rate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Week5rate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Monthlyrate { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Hourlycost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Dailycost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Weeklycost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Monthlycost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Price { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Cost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Retail { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Markup { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Reorderqty { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Reorderpoint { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Defaultcost { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Qty { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Hasqty { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Qtyconsigned { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Qtyallocated { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Qtyintransit { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Qtystaged { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Qtyonpo { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Qtyin { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string PhysicalId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Lastphydate { get; set; }
                public bool Fixedasset { get { return master.Fixedasset; } set { master.Fixedasset = value; } }
                public bool Nodiscount { get { return master.Nodiscount; } set { master.Nodiscount = value; } }
                public string Hazardousmaterial { get { return master.Hazardousmaterial; } set { master.Hazardousmaterial = value; } }
                public bool Rank { get { return master.Rank; } set { master.Rank = value; } }
                public decimal Replacementcost { get { return master.Replacementcost; } set { master.Replacementcost = value; } }
                public decimal Manifestvalue { get { return master.Manifestvalue; } set { master.Manifestvalue = value; } }
                public string Trackedby { get { return master.Trackedby; } set { master.Trackedby = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Notes { get; set; }
                public string Partnumber { get { return master.Partnumber; } set { master.Partnumber = value; } }
                public string Originalicode { get { return master.Originalicode; } set { master.Originalicode = value; } }
                public string Ratetype { get { return master.Ratetype; } set { master.Ratetype = value; } }
                public string MfgId { get { return master.MfgId; } set { master.MfgId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Manufacturer { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Shipweightlbs { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Shipweightoz { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Weightwcaselbs { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Weightwcaseoz { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Widthft { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Widthin { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Heightft { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Heightin { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Lengthft { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Lengthin { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Shipweightkg { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Shipweightg { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Weightwcasekg { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Weightwcaseg { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Widthm { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Widthcm { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Heightm { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Heightcm { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Lengthm { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public int Lengthcm { get; set; }
                public bool Noavail { get { return master.Noavail; } set { master.Noavail = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Dw { get; set; }
                public bool Metered { get { return master.Metered; } set { master.Metered = value; } }
                public string OriginalshowId { get { return master.OriginalshowId; } set { master.OriginalshowId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Originalshow { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Container { get; set; }
                public bool Qcrequired { get { return master.Qcrequired; } set { master.Qcrequired = value; } }
                public bool Hastieredprice { get { return master.Hastieredprice; } set { master.Hastieredprice = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Chargetype { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Productionexchangeecode { get; set; }
                public bool Displaywhenrateiszero { get { return master.Displaywhenrateiszero; } set { master.Displaywhenrateiszero = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Invdeptisprops { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Invdeptiswardrobe { get; set; }
                public string ContainerId { get { return master.ContainerId; } set { master.ContainerId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string ScannablemasterId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbyhour { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbydeal { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbyasset { get; set; }
                public bool Unlockweek4rate { get { return master.Unlockweek4rate; } set { master.Unlockweek4rate = value; } }
                public string PatternId { get { return master.PatternId; } set { master.PatternId = value; } }
                public string PeriodId { get { return master.PeriodId; } set { master.PeriodId = value; } }
                public string MaterialId { get { return master.MaterialId; } set { master.MaterialId = value; } }
                public string GenderId { get { return master.GenderId; } set { master.GenderId = value; } }
                public string LabelId { get { return master.LabelId; } set { master.LabelId = value; } }
                public string Wardrobesize { get { return master.Wardrobesize; } set { master.Wardrobesize = value; } }
                public int Wardrobepiececount { get { return master.Wardrobepiececount; } set { master.Wardrobepiececount = value; } }
                public bool Tracksoftware { get { return master.Tracksoftware; } set { master.Tracksoftware = value; } }
                public bool Trackassetusageflg { get { return master.Trackassetusageflg; } set { master.Trackassetusageflg = value; } }
                public bool Tracklampusageflg { get { return master.Tracklampusageflg; } set { master.Tracklampusageflg = value; } }
                public bool Trackstrikesflg { get { return master.Trackstrikesflg; } set { master.Trackstrikesflg = value; } }
                public bool Trackcandlesflg { get { return master.Trackcandlesflg; } set { master.Trackcandlesflg = value; } }
                public int Minfootcandles { get { return master.Minfootcandles; } set { master.Minfootcandles = value; } }
                public int Lampcount { get { return master.Lampcount; } set { master.Lampcount = value; } }
                public bool Defaultprorateweeks { get { return master.Defaultprorateweeks; } set { master.Defaultprorateweeks = value; } }
                public bool Defaultproratemonths { get { return master.Defaultproratemonths; } set { master.Defaultproratemonths = value; } }
                public bool Includeonpicklist { get { return master.Includeonpicklist; } set { master.Includeonpicklist = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Maxdiscount { get; set; }
                public bool Dyed { get { return master.Dyed; } set { master.Dyed = value; } }
                public string WardrobesourceId { get { return master.WardrobesourceId; } set { master.WardrobesourceId = value; } }
                public string WardrobecareId { get { return master.WardrobecareId; } set { master.WardrobecareId = value; } }
                public decimal Cleaningfeeamount { get { return master.Cleaningfeeamount; } set { master.Cleaningfeeamount = value; } }
                public string Inputdate { get { return master.Inputdate; } set { master.Inputdate = value; } }
                public bool Overrideprofitlosscategory { get { return master.Overrideprofitlosscategory; } set { master.Overrideprofitlosscategory = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string PlcategoryId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Plcategory { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string PlinventorydepartmentId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Plinventorydepartment { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Starttime { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Stoptime { get; set; }
        */

        public override void BeforeSave()
        {
            AvailFor = "M";
        }
        //------------------------------------------------------------------------------------ 
    }
}