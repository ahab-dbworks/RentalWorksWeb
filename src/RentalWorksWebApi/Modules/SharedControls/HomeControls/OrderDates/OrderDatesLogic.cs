using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.SqlServer;

namespace WebApi.Modules.HomeControls.OrderDates
{
    [FwLogic(Id: "flcHNd8WETF0M")]
    public class OrderDatesLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderDatesLoader orderDatesLoader = new OrderDatesLoader();
        public OrderDatesLogic()
        {
            dataLoader = orderDatesLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "QXK8mVsLGVjjh", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "8TuiVPyK71TIK", IsReadOnly: true)]
        public string OrderTypeId { get; set; }
        [FwLogicProperty(Id: "y7Uyo0oonPaWY", IsReadOnly: true)]
        public string OrderTypeDateTypeId { get; set; }
        [FwLogicProperty(Id: "H2eNYfdlGHPk8", IsReadOnly: true)]
        public bool? IsSystemType { get; set; }
        [FwLogicProperty(Id: "HTjZoOZc7Ozs6", IsReadOnly: true)]
        public string ActivityTypeId { get; set; }
        [FwLogicProperty(Id: "5i2EoLG6vBclB3", IsReadOnly: true)]
        public string ActivityType { get; set; }
        [FwLogicProperty(Id: "LfZRLky4Dzdrm", IsReadOnly: true)]
        public string Activity { get; set; }
        [FwLogicProperty(Id: "qU1CVfk69Uisr", IsReadOnly: true)]
        public string ActivityDisplay { get; set; }
        [FwLogicProperty(Id: "1vw9dwLIf36eq", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "G4deOHC8ZaCXR", IsReadOnly: true)]
        public string DescriptionDisplay { get; set; }
        [FwLogicProperty(Id: "VztT3BvgL7j4V", IsReadOnly: true)]
        public string DescriptionDisplayTitleCase
        {
            get
            {
                string s = DescriptionDisplay;
                if (!string.IsNullOrEmpty(s))
                {
                    s = FwConvert.ToTitleCase(s.ToLower());
                    // justin hoffman 08/10/2020 #2849
                    s = s.Replace("Po ", "PO ");
                    if (s.EndsWith("Po"))
                    {
                        s = s.Substring(0, s.Length - 2) + "PO";
                    }
                    s = s.Replace("Wh ", "WH ");
                    if (s.EndsWith("Wh"))
                    {
                        s = s.Substring(0, s.Length - 2) + "WH";
                    }
                }
                return s;
            }
        }
        [FwLogicProperty(Id: "RuCxNc4aMHA2l", IsReadOnly: true)]
        public bool? IsEnabled { get; set; }
        [FwLogicProperty(Id: "6VJRz3bf3pb6G", IsReadOnly: true)]
        public string Date { get; set; }
        [FwLogicProperty(Id: "0k8Uqs2FPt6iL", IsReadOnly: true)]
        public string Time { get; set; }
        [FwLogicProperty(Id: "xf4z1C5n8bRKg", IsReadOnly: true)]
        public string DateAndTime { get; set; }
        [FwLogicProperty(Id: "npCWdwDWVI7Zn", IsReadOnly: true)]
        public string DayOfWeek { get; set; }
        [FwLogicProperty(Id: "BXj7TIQamhEYv", IsReadOnly: true)]
        public string ActualDate { get; set; }
        [FwLogicProperty(Id: "adkLKTK0nVw60", IsReadOnly: true)]
        public string ActualTime { get; set; }
        [FwLogicProperty(Id: "Aw0U7krs7C5us", IsReadOnly: true)]
        public string ActualDayOfWeek { get; set; }
        [FwLogicProperty(Id: "8rzzQNUYLGJy2", IsReadOnly: true)]
        public bool? IsRequired { get; set; }
        [FwLogicProperty(Id: "z9pWIFxbu4fPy", IsReadOnly: true)]
        public bool? IsProductionActivity { get; set; }
        [FwLogicProperty(Id: "Jn2ye1QyU3aCx", IsReadOnly: true)]
        public bool? IsMilestone { get; set; }
        [FwLogicProperty(Id: "bH8lS4wDw2j2k", IsReadOnly: true)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
