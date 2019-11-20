using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.HolidaySettings.Holiday
{
    [FwLogic(Id:"w18TUTvl2Xwf")]
    public class HolidayLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        HolidayRecord holiday = new HolidayRecord();
        HolidayLoader holidayLoader = new HolidayLoader();
        public HolidayLogic()
        {
            dataRecords.Add(holiday);
            dataLoader = holidayLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"V6HWqTfzyMnq", IsPrimaryKey:true)]
        public string HolidayId { get { return holiday.HolidayId; } set { holiday.HolidayId = value; } }

        [FwLogicProperty(Id:"V6HWqTfzyMnq", IsRecordTitle:true)]
        public string Holiday { get { return holiday.Holiday; } set { holiday.Holiday = value; } }

        [FwLogicProperty(Id:"ujmhvSziB46")]
        public string CountryId { get { return holiday.CountryId; } set { holiday.CountryId = value; } }

        [FwLogicProperty(Id:"0W2fHcD5Ynvr", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"V4r3gW0jjZi")]
        public bool? Custom { get { return holiday.Custom; } set { holiday.Custom = value; } }

        [FwLogicProperty(Id:"oCjw2xq75qI")]
        public bool? Observed { get { return holiday.Observed; } set { holiday.Observed = value; } }

        [FwLogicProperty(Id:"ihGNNjDk7ea")]
        public string Type { get { return holiday.Type; } set { holiday.Type = value; } }

        [FwLogicProperty(Id:"FSAPrbrUh8p")]
        public int? FixedMonth { get { return holiday.FixedMonth; } set { holiday.FixedMonth = value; } }

        [FwLogicProperty(Id:"RUFia4zp2kD")]
        public int? FixedDay { get { return holiday.FixedDay; } set { holiday.FixedDay = value; } }

        [FwLogicProperty(Id:"14xrX8JmN3v")]
        public int? FixedYear { get { return holiday.FixedYear; } set { holiday.FixedYear = value; } }

        [FwLogicProperty(Id:"Dsig5omGJWM")]
        public int? DayOfWeek { get { return holiday.DayOfWeek; } set { holiday.DayOfWeek = value; } }

        [FwLogicProperty(Id:"eRU9OEO2CXv")]
        public int? WeekOfMonth { get { return holiday.WeekOfMonth; } set { holiday.WeekOfMonth = value; } }

        [FwLogicProperty(Id:"W5HxNIAxYvP")]
        public int? Adjustment { get { return holiday.Adjustment; } set { holiday.Adjustment = value; } }

        [FwLogicProperty(Id:"EI4GaQUdvor")]
        public string OffsetHolidayId { get { return holiday.OffsetHolidayId; } set { holiday.OffsetHolidayId = value; } }

        [FwLogicProperty(Id:"V6HWqTfzyMnq", IsReadOnly:true)]
        public string OffsetHoliday { get; set; }

        [FwLogicProperty(Id:"yJIjFGGQZbM")]
        public bool? Inactive { get { return holiday.Inactive; } set { holiday.Inactive = value; } }

        [FwLogicProperty(Id:"K1DnKs1tnUc")]
        public string DateStamp { get { return holiday.DateStamp; } set { holiday.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
