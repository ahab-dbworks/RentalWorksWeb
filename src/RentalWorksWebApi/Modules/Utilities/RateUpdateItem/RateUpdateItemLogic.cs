using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateItem
{
    [FwLogic(Id: "0Ea9i0u3AtEG")]
    public class RateUpdateItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateUpdateItemRecord rateUpdateItem = new RateUpdateItemRecord();
        public RateUpdateItemLogic()
        {
            dataRecords.Add(rateUpdateItem);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "J1n135Cf0ZQI")]
        public string MasterId { get { return rateUpdateItem.MasterId; } set { rateUpdateItem.MasterId = value; } }
        [FwLogicProperty(Id: "qyo8o5YEgnRC")]
        public string WarehouseId { get { return rateUpdateItem.WarehouseId; } set { rateUpdateItem.WarehouseId = value; } }
        [FwLogicProperty(Id: "MzJtgJByqaoz")]
        public string Description { get { return rateUpdateItem.Description; } set { rateUpdateItem.Description = value; } }

        [FwLogicProperty(Id: "MIYfFqE7WSmY")]
        public decimal? HourlyRate { get { return rateUpdateItem.HourlyRate; } set { rateUpdateItem.HourlyRate = value; } }
        [FwLogicProperty(Id: "ns54VM7vN4Yu")]
        public decimal? DailyRate { get { return rateUpdateItem.DailyRate; } set { rateUpdateItem.DailyRate = value; } }
        [FwLogicProperty(Id: "sFOTCggAzoJO")]
        public decimal? MonthlyRate { get { return rateUpdateItem.MonthlyRate; } set { rateUpdateItem.MonthlyRate = value; } }
        [FwLogicProperty(Id: "CTTjRFOrSB1k")]
        public decimal? WeeklyRate { get { return rateUpdateItem.WeeklyRate; } set { rateUpdateItem.WeeklyRate = value; } }


        [FwLogicProperty(Id: "MMLsJrSIUR3I")]
        public decimal? NewHourlyRate { get { return rateUpdateItem.NewHourlyRate; } set { rateUpdateItem.NewHourlyRate = value; } }
        [FwLogicProperty(Id: "QjwUJRtL4Yyt")]
        public decimal? NewDailyRate { get { return rateUpdateItem.NewDailyRate; } set { rateUpdateItem.NewDailyRate = value; } }
        [FwLogicProperty(Id: "XTgi9DfO0ahe")]
        public decimal? NewWeeklyRate { get { return rateUpdateItem.NewWeeklyRate; } set { rateUpdateItem.NewWeeklyRate = value; } }
        [FwLogicProperty(Id: "NGUeR6QSQ312")]
        public decimal? NewMonthlyRate { get { return rateUpdateItem.NewMonthlyRate; } set { rateUpdateItem.NewMonthlyRate = value; } }

        [FwLogicProperty(Id: "599UkC5WcBPk")]
        public decimal? Week2Rate { get { return rateUpdateItem.Week2Rate; } set { rateUpdateItem.Week2Rate = value; } }
        [FwLogicProperty(Id: "BdVJqPkDsFnU")]
        public decimal? Week3Rate { get { return rateUpdateItem.Week3Rate; } set { rateUpdateItem.Week3Rate = value; } }
        [FwLogicProperty(Id: "EyvJX1xZauh8")]
        public decimal? Week4Rate { get { return rateUpdateItem.Week4Rate; } set { rateUpdateItem.Week4Rate = value; } }
        [FwLogicProperty(Id: "4N1SfE3i9Nt0")]
        public decimal? Week5Rate { get { return rateUpdateItem.Week5Rate; } set { rateUpdateItem.Week5Rate = value; } }
       
        [FwLogicProperty(Id: "0BitRrJkRiiy")]
        public decimal? NewWeek2Rate { get { return rateUpdateItem.NewWeek2Rate; } set { rateUpdateItem.NewWeek2Rate = value; } }
        [FwLogicProperty(Id: "m2WY6hwU9Iuw")]
        public decimal? NewWeek3Rate { get { return rateUpdateItem.NewWeek3Rate; } set { rateUpdateItem.NewWeek3Rate = value; } }
        [FwLogicProperty(Id: "gZ4NlvnJMzjL")]
        public decimal? NewWeek4Rate { get { return rateUpdateItem.NewWeek4Rate; } set { rateUpdateItem.NewWeek4Rate = value; } }
        [FwLogicProperty(Id: "8b2p0XcjTwF4")]
        public decimal? NewWeek5Rate { get { return rateUpdateItem.NewWeek5Rate; } set { rateUpdateItem.NewWeek5Rate = value; } }

        [FwLogicProperty(Id: "EJEErXxQQt3k")]
        public decimal? MaxDiscount { get { return rateUpdateItem.MaxDiscount; } set { rateUpdateItem.MaxDiscount = value; } }
        [FwLogicProperty(Id: "1QaP8BYmXob1")]
        public decimal? NewMaxDiscount { get { return rateUpdateItem.NewMaxDiscount; } set { rateUpdateItem.NewMaxDiscount = value; } }

        [FwLogicProperty(Id: "PzpSGvAUSAcy")]
        public decimal? Retail { get { return rateUpdateItem.Retail; } set { rateUpdateItem.Retail = value; } }
        [FwLogicProperty(Id: "weBi0FRqPYlF")]
        public decimal? NewRetail { get { return rateUpdateItem.NewRetail; } set { rateUpdateItem.NewRetail = value; } }

        [FwLogicProperty(Id: "3IZxh0c4vF0y")]
        public decimal? MinDaysPerWeek { get { return rateUpdateItem.MinDaysPerWeek; } set { rateUpdateItem.MinDaysPerWeek = value; } }
        [FwLogicProperty(Id: "NR05t3p2EaNx")]
        public decimal? NewMinDaysPerWeek { get { return rateUpdateItem.NewMinDaysPerWeek; } set { rateUpdateItem.NewMinDaysPerWeek = value; } }

       
        //[FwLogicProperty(Id: "")]
        //public decimal? Price { get { return rateUpdateItem.Price; } set { rateUpdateItem.Price = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Cost { get { return rateUpdateItem.Cost; } set { rateUpdateItem.Cost = value; } }
        //[FwLogicProperty(Id: "")]
        //public int? Reorderpoint { get { return rateUpdateItem.Reorderpoint; } set { rateUpdateItem.Reorderpoint = value; } }

        //[FwLogicProperty(Id: "")]
        //public string Shelfloc { get { return rateUpdateItem.Shelfloc; } set { rateUpdateItem.Shelfloc = value; } }

        //[FwLogicProperty(Id: "")]
        //public string Aisleloc { get { return rateUpdateItem.Aisleloc; } set { rateUpdateItem.Aisleloc = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldmonthlyrate { get { return rateUpdateItem.Oldmonthlyrate; } set { rateUpdateItem.Oldmonthlyrate = value; } }
        //[FwLogicProperty(Id: "")]
        //public int? LastQuantityonhand { get { return rateUpdateItem.LastQuantityonhand; } set { rateUpdateItem.LastQuantityonhand = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Olddailyrate { get { return rateUpdateItem.Olddailyrate; } set { rateUpdateItem.Olddailyrate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweeklyrate { get { return rateUpdateItem.Oldweeklyrate; } set { rateUpdateItem.Oldweeklyrate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newprice { get { return rateUpdateItem.Newprice; } set { rateUpdateItem.Newprice = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldprice { get { return rateUpdateItem.Oldprice; } set { rateUpdateItem.Oldprice = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Dailycost { get { return rateUpdateItem.Dailycost; } set { rateUpdateItem.Dailycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Monthlycost { get { return rateUpdateItem.Monthlycost; } set { rateUpdateItem.Monthlycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Weeklycost { get { return rateUpdateItem.Weeklycost; } set { rateUpdateItem.Weeklycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newcost { get { return rateUpdateItem.Newcost; } set { rateUpdateItem.Newcost = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldretail { get { return rateUpdateItem.Oldretail; } set { rateUpdateItem.Oldretail = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldcost { get { return rateUpdateItem.Oldcost; } set { rateUpdateItem.Oldcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Defaultcost { get { return rateUpdateItem.Defaultcost; } set { rateUpdateItem.Defaultcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newdefaultcost { get { return rateUpdateItem.Newdefaultcost; } set { rateUpdateItem.Newdefaultcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Olddefaultcost { get { return rateUpdateItem.Olddefaultcost; } set { rateUpdateItem.Olddefaultcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public string ModbyusersId { get { return rateUpdateItem.ModbyusersId; } set { rateUpdateItem.ModbyusersId = value; } }
        //[FwLogicProperty(Id: "")]
        //public int? ReorderQuantity { get { return rateUpdateItem.ReorderQuantity; } set { rateUpdateItem.ReorderQuantity = value; } }
        //[FwLogicProperty(Id: "")]
        //public bool? Qcrequired { get { return rateUpdateItem.Qcrequired; } set { rateUpdateItem.Qcrequired = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Hourlycost { get { return rateUpdateItem.Hourlycost; } set { rateUpdateItem.Hourlycost = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweek2rate { get { return rateUpdateItem.Oldweek2rate; } set { rateUpdateItem.Oldweek2rate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweek3rate { get { return rateUpdateItem.Oldweek3rate; } set { rateUpdateItem.Oldweek3rate = value; } }

        //[FwLogicProperty(Id: "")]
        //public bool? Availbyhour { get { return rateUpdateItem.Availbyhour; } set { rateUpdateItem.Availbyhour = value; } }

        //[FwLogicProperty(Id: "")]
        //public bool? Availbydeal { get { return rateUpdateItem.Availbydeal; } set { rateUpdateItem.Availbydeal = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldhourlyrate { get { return rateUpdateItem.Oldhourlyrate; } set { rateUpdateItem.Oldhourlyrate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldhourlycost { get { return rateUpdateItem.Oldhourlycost; } set { rateUpdateItem.Oldhourlycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newhourlycost { get { return rateUpdateItem.Newhourlycost; } set { rateUpdateItem.Newhourlycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Olddailycost { get { return rateUpdateItem.Olddailycost; } set { rateUpdateItem.Olddailycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newdailycost { get { return rateUpdateItem.Newdailycost; } set { rateUpdateItem.Newdailycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweeklycost { get { return rateUpdateItem.Oldweeklycost; } set { rateUpdateItem.Oldweeklycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newweeklycost { get { return rateUpdateItem.Newweeklycost; } set { rateUpdateItem.Newweeklycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldmonthlycost { get { return rateUpdateItem.Oldmonthlycost; } set { rateUpdateItem.Oldmonthlycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newmonthlycost { get { return rateUpdateItem.Newmonthlycost; } set { rateUpdateItem.Newmonthlycost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldmaxdiscount { get { return rateUpdateItem.Oldmaxdiscount; } set { rateUpdateItem.Oldmaxdiscount = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweek4rate { get { return rateUpdateItem.Oldweek4rate; } set { rateUpdateItem.Oldweek4rate = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldweek5rate { get { return rateUpdateItem.Oldweek5rate; } set { rateUpdateItem.Oldweek5rate = value; } }
        //[FwLogicProperty(Id: "")]
        //public bool? Availbyasset { get { return rateUpdateItem.Availbyasset; } set { rateUpdateItem.Availbyasset = value; } }
        //[FwLogicProperty(Id: "")]
        //public string Starttime { get { return rateUpdateItem.Starttime; } set { rateUpdateItem.Starttime = value; } }
        //[FwLogicProperty(Id: "")]
        //public string Stoptime { get { return rateUpdateItem.Stoptime; } set { rateUpdateItem.Stoptime = value; } }
        //[FwLogicProperty(Id: "")]
        //public int? Availqcdelay { get { return rateUpdateItem.Availqcdelay; } set { rateUpdateItem.Availqcdelay = value; } }
        //[FwLogicProperty(Id: "")]
        //public bool? Allowallusersaddtoorder { get { return rateUpdateItem.Allowallusersaddtoorder; } set { rateUpdateItem.Allowallusersaddtoorder = value; } }
        //[FwLogicProperty(Id: "")]
        //public string DefaultpurchasecurrencyId { get { return rateUpdateItem.DefaultpurchasecurrencyId; } set { rateUpdateItem.DefaultpurchasecurrencyId = value; } }
        //[FwLogicProperty(Id: "")]
        //public bool? Hastieredcost { get { return rateUpdateItem.Hastieredcost; } set { rateUpdateItem.Hastieredcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Manifestvalue { get { return rateUpdateItem.Manifestvalue; } set { rateUpdateItem.Manifestvalue = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newmanifestvalue { get { return rateUpdateItem.Newmanifestvalue; } set { rateUpdateItem.Newmanifestvalue = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Newreplacementcost { get { return rateUpdateItem.Newreplacementcost; } set { rateUpdateItem.Newreplacementcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldmanifestvalue { get { return rateUpdateItem.Oldmanifestvalue; } set { rateUpdateItem.Oldmanifestvalue = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Oldreplacementcost { get { return rateUpdateItem.Oldreplacementcost; } set { rateUpdateItem.Oldreplacementcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Replacementcost { get { return rateUpdateItem.Replacementcost; } set { rateUpdateItem.Replacementcost = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Restockfee { get { return rateUpdateItem.Restockfee; } set { rateUpdateItem.Restockfee = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Restockpercent { get { return rateUpdateItem.Restockpercent; } set { rateUpdateItem.Restockpercent = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Maxdw { get { return rateUpdateItem.Maxdw; } set { rateUpdateItem.Maxdw = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Seriesrate { get { return rateUpdateItem.Seriesrate; } set { rateUpdateItem.Seriesrate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Year2leaserate { get { return rateUpdateItem.Year2leaserate; } set { rateUpdateItem.Year2leaserate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Year3leaserate { get { return rateUpdateItem.Year3leaserate; } set { rateUpdateItem.Year3leaserate = value; } }
        //[FwLogicProperty(Id: "")]
        //public decimal? Year4leaserate { get { return rateUpdateItem.Year4leaserate; } set { rateUpdateItem.Year4leaserate = value; } }

        //[FwLogicProperty(Id: "")]
        //public decimal? Oldmindw { get { return rateUpdateItem.Oldmindw; } set { rateUpdateItem.Oldmindw = value; } }
     
        //[FwLogicProperty(Id: "")]
        //public string PhysicalId { get { return rateUpdateItem.PhysicalId; } set { rateUpdateItem.PhysicalId = value; } }
        [FwLogicProperty(Id: "DlEo0K6orV5N")]
        public string DateStamp { get { return rateUpdateItem.DateStamp; } set { rateUpdateItem.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
