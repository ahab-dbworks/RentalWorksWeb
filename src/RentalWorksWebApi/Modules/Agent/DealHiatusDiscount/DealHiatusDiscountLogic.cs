using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.DealHiatusDiscount
{
    [FwLogic(Id: "QzL3TPCtpXFXK")]
    public class DealHiatusDiscountLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealHiatusDiscountRecord dealHiatusDiscount = new DealHiatusDiscountRecord();
        DealHiatusDiscountLoader dealHiatusDiscountLoader = new DealHiatusDiscountLoader();
        public DealHiatusDiscountLogic()
        {
            dataRecords.Add(dealHiatusDiscount);
            dataLoader = dealHiatusDiscountLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "QzN8hshQPhOTa", IsPrimaryKey: true)]
        public string DealHiatusDiscountId { get { return dealHiatusDiscount.DealHiatusDiscountId; } set { dealHiatusDiscount.DealHiatusDiscountId = value; } }
        [FwLogicProperty(Id: "rawScRfjAA3cH")]
        public string DealId { get { return dealHiatusDiscount.DealId; } set { dealHiatusDiscount.DealId = value; } }
        [FwLogicProperty(Id: "QzwnglDNAk1oJ")]
        public int? EpisodeNumber { get { return dealHiatusDiscount.EpisodeNumber; } set { dealHiatusDiscount.EpisodeNumber = value; } }
        [FwLogicProperty(Id: "qZXgTLH4Qsv9z")]
        public string FromDate { get { return dealHiatusDiscount.FromDate; } set { dealHiatusDiscount.FromDate = value; } }
        [FwLogicProperty(Id: "qzz6LAbVEJsr9")]
        public string ToDate { get { return dealHiatusDiscount.ToDate; } set { dealHiatusDiscount.ToDate = value; } }
        [FwLogicProperty(Id: "r1h69O6IySkxp", IsReadOnly: true)]
        public int? Days { get; set; }
        [FwLogicProperty(Id: "r2pk1rEjYcmaD")]
        public decimal? BillableDays { get { return dealHiatusDiscount.BillableDays; } set { dealHiatusDiscount.BillableDays = value; } }
        [FwLogicProperty(Id: "R34SCgcH6OzUX")]
        public bool? BillWeekends { get { return dealHiatusDiscount.BillWeekends; } set { dealHiatusDiscount.BillWeekends = value; } }
        [FwLogicProperty(Id: "R3avyC6P0f6tA")]
        public bool? BillHolidays { get { return dealHiatusDiscount.BillHolidays; } set { dealHiatusDiscount.BillHolidays = value; } }
        [FwLogicProperty(Id: "R3otk4Gct9sz2")]
        public bool? IsHiatus { get { return dealHiatusDiscount.IsHiatus; } set { dealHiatusDiscount.IsHiatus = value; } }
        [FwLogicProperty(Id: "r4hLKKlMHIWVl")]
        public decimal? HiatusDiscountPercent { get { return dealHiatusDiscount.HiatusDiscountPercent; } set { dealHiatusDiscount.HiatusDiscountPercent = value; } }
        [FwLogicProperty(Id: "R5H0CVeyLQbZq")]
        public int? OrderBy { get { return dealHiatusDiscount.OrderBy; } set { dealHiatusDiscount.OrderBy = value; } }
        [FwLogicProperty(Id: "R6Cx28hGGsxeT")]
        public bool? IsProrated { get { return dealHiatusDiscount.IsProrated; } set { dealHiatusDiscount.IsProrated = value; } }
        [FwLogicProperty(Id: "r6ZE7uAKEfXJG")]
        public bool? DoChangeSchedule { get { return dealHiatusDiscount.DoChangeSchedule; } set { dealHiatusDiscount.DoChangeSchedule = value; } }
        [FwLogicProperty(Id: "R705JBsWDo2Hw")]
        public bool? IsEpisode { get { return dealHiatusDiscount.IsEpisode; } set { dealHiatusDiscount.IsEpisode = value; } }
        [FwLogicProperty(Id: "ra1tecadv5upw")]
        public string DepartmentId { get { return dealHiatusDiscount.DepartmentId; } set { dealHiatusDiscount.DepartmentId = value; } }
        [FwLogicProperty(Id: "rAGp1gvLo6peH", IsReadOnly: true)]
        public string Department { get; set; }
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
