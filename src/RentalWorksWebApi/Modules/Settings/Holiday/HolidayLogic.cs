using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.Holiday
{
    public class HolidayLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string HolidayId { get { return holiday.HolidayId; } set { holiday.HolidayId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Holiday { get { return holiday.Holiday; } set { holiday.Holiday = value; } }
        public string CountryId { get { return holiday.CountryId; } set { holiday.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public bool Custom { get { return holiday.Custom; } set { holiday.Custom = value; } }
        public bool Observed { get { return holiday.Observed; } set { holiday.Observed = value; } }
        public string Type { get { return holiday.Type; } set { holiday.Type = value; } }
        public int? FixedMonth { get { return holiday.FixedMonth; } set { holiday.FixedMonth = value; } }
        public int? FixedDay { get { return holiday.FixedDay; } set { holiday.FixedDay = value; } }
        public int? FixedYear { get { return holiday.FixedYear; } set { holiday.FixedYear = value; } }
        public int? DayOfWeek { get { return holiday.DayOfWeek; } set { holiday.DayOfWeek = value; } }
        public int? WeekOfMonth { get { return holiday.WeekOfMonth; } set { holiday.WeekOfMonth = value; } }
        public int? Adjustment { get { return holiday.Adjustment; } set { holiday.Adjustment = value; } }
        public string OffsetHolidayId { get { return holiday.OffsetHolidayId; } set { holiday.OffsetHolidayId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OffsetHoliday { get; set; }
        public bool Inactive { get { return holiday.Inactive; } set { holiday.Inactive = value; } }
        public string DateStamp { get { return holiday.DateStamp; } set { holiday.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}