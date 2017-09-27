using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Inventory;

namespace RentalWorksWebApi.Modules.Home.SalesInventory
{
    public class SalesInventoryLogic : InventoryLogic 
    {
        //------------------------------------------------------------------------------------ 
        SalesInventoryLoader inventoryLoader = new SalesInventoryLoader();
        public SalesInventoryLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        /*
                [FwBusinessLogicField(isReadOnly: true)]
                public string Masterakatext { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Masterakatext255 { get; set; }
                public string UnitId { get { return inventory.UnitId; } set { inventory.UnitId = value; } }
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
                public bool Fixedasset { get { return inventory.Fixedasset; } set { inventory.Fixedasset = value; } }
                public bool Nodiscount { get { return inventory.Nodiscount; } set { inventory.Nodiscount = value; } }
                public string Hazardousmaterial { get { return inventory.Hazardousmaterial; } set { inventory.Hazardousmaterial = value; } }
                public bool Rank { get { return inventory.Rank; } set { inventory.Rank = value; } }
                public decimal Replacementcost { get { return inventory.Replacementcost; } set { inventory.Replacementcost = value; } }
                public decimal Manifestvalue { get { return inventory.Manifestvalue; } set { inventory.Manifestvalue = value; } }
                public string Trackedby { get { return inventory.Trackedby; } set { inventory.Trackedby = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Notes { get; set; }
                public string Partnumber { get { return inventory.Partnumber; } set { inventory.Partnumber = value; } }
                public string Originalicode { get { return inventory.Originalicode; } set { inventory.Originalicode = value; } }
                public string Ratetype { get { return inventory.Ratetype; } set { inventory.Ratetype = value; } }
                public string MfgId { get { return inventory.MfgId; } set { inventory.MfgId = value; } }
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
                public bool Noavail { get { return inventory.Noavail; } set { inventory.Noavail = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Dw { get; set; }
                public bool Metered { get { return inventory.Metered; } set { inventory.Metered = value; } }
                public string OriginalshowId { get { return inventory.OriginalshowId; } set { inventory.OriginalshowId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Originalshow { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Container { get; set; }
                public bool Qcrequired { get { return inventory.Qcrequired; } set { inventory.Qcrequired = value; } }
                public bool Hastieredprice { get { return inventory.Hastieredprice; } set { inventory.Hastieredprice = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Chargetype { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public string Productionexchangeecode { get; set; }
                public bool Displaywhenrateiszero { get { return inventory.Displaywhenrateiszero; } set { inventory.Displaywhenrateiszero = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Invdeptisprops { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Invdeptiswardrobe { get; set; }
                public string ContainerId { get { return inventory.ContainerId; } set { inventory.ContainerId = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public string ScannablemasterId { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbyhour { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbydeal { get; set; }
                [FwBusinessLogicField(isReadOnly: true)]
                public bool Availbyasset { get; set; }
                public bool Unlockweek4rate { get { return inventory.Unlockweek4rate; } set { inventory.Unlockweek4rate = value; } }
                public string PatternId { get { return inventory.PatternId; } set { inventory.PatternId = value; } }
                public string PeriodId { get { return inventory.PeriodId; } set { inventory.PeriodId = value; } }
                public string MaterialId { get { return inventory.MaterialId; } set { inventory.MaterialId = value; } }
                public string GenderId { get { return inventory.GenderId; } set { inventory.GenderId = value; } }
                public string LabelId { get { return inventory.LabelId; } set { inventory.LabelId = value; } }
                public string Wardrobesize { get { return inventory.Wardrobesize; } set { inventory.Wardrobesize = value; } }
                public int Wardrobepiececount { get { return inventory.Wardrobepiececount; } set { inventory.Wardrobepiececount = value; } }
                public bool Tracksoftware { get { return inventory.Tracksoftware; } set { inventory.Tracksoftware = value; } }
                public bool Trackassetusageflg { get { return inventory.Trackassetusageflg; } set { inventory.Trackassetusageflg = value; } }
                public bool Tracklampusageflg { get { return inventory.Tracklampusageflg; } set { inventory.Tracklampusageflg = value; } }
                public bool Trackstrikesflg { get { return inventory.Trackstrikesflg; } set { inventory.Trackstrikesflg = value; } }
                public bool Trackcandlesflg { get { return inventory.Trackcandlesflg; } set { inventory.Trackcandlesflg = value; } }
                public int Minfootcandles { get { return inventory.Minfootcandles; } set { inventory.Minfootcandles = value; } }
                public int Lampcount { get { return inventory.Lampcount; } set { inventory.Lampcount = value; } }
                public bool Defaultprorateweeks { get { return inventory.Defaultprorateweeks; } set { inventory.Defaultprorateweeks = value; } }
                public bool Defaultproratemonths { get { return inventory.Defaultproratemonths; } set { inventory.Defaultproratemonths = value; } }
                public bool Includeonpicklist { get { return inventory.Includeonpicklist; } set { inventory.Includeonpicklist = value; } }
                [FwBusinessLogicField(isReadOnly: true)]
                public decimal Maxdiscount { get; set; }
                public bool Dyed { get { return inventory.Dyed; } set { inventory.Dyed = value; } }
                public string WardrobesourceId { get { return inventory.WardrobesourceId; } set { inventory.WardrobesourceId = value; } }
                public string WardrobecareId { get { return inventory.WardrobecareId; } set { inventory.WardrobecareId = value; } }
                public decimal Cleaningfeeamount { get { return inventory.Cleaningfeeamount; } set { inventory.Cleaningfeeamount = value; } }
                public string Inputdate { get { return inventory.Inputdate; } set { inventory.Inputdate = value; } }
                public bool Overrideprofitlosscategory { get { return inventory.Overrideprofitlosscategory; } set { inventory.Overrideprofitlosscategory = value; } }
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
            AvailFor = "S";
        }
        //------------------------------------------------------------------------------------ 
    }
}