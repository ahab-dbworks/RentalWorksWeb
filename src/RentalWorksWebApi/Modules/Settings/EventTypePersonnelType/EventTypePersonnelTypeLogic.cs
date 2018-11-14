using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Settings.OrderTypePersonnelType;

namespace WebApi.Modules.Settings.EventTypePersonnelType
{
    [FwLogic(Id:"g66pvx1B7Gi")]
    public class EventTypePersonnelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypePersonnelTypeRecord eventTypePersonnelType = new OrderTypePersonnelTypeRecord();
        EventTypePersonnelTypeLoader eventTypePersonnelTypeLoader = new EventTypePersonnelTypeLoader();
        public EventTypePersonnelTypeLogic()
        {
            dataRecords.Add(eventTypePersonnelType);
            dataLoader = eventTypePersonnelTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"CA9SxeE2o6M", IsPrimaryKey:true)]
        public string EventTypePersonnelTypeId { get { return eventTypePersonnelType.OrderTypePersonnelTypeId; } set { eventTypePersonnelType.OrderTypePersonnelTypeId = value; } }

        [FwLogicProperty(Id:"pcsADSq8T0e2")]
        public string EventTypeId { get { return eventTypePersonnelType.OrderTypeId; } set { eventTypePersonnelType.OrderTypeId = value; } }

        [FwLogicProperty(Id:"QSsuf92yx186")]
        public string PersonnelTypeId { get { return eventTypePersonnelType.PersonnelTypeId; } set { eventTypePersonnelType.PersonnelTypeId = value; } }

        [FwLogicProperty(Id:"1UrlXbhxnQu", IsReadOnly:true)]
        public string PersonnelType { get; set; }

        [FwLogicProperty(Id:"qihOTgUFPQbZ")]
        public string PersonnelTypeRename { get { return eventTypePersonnelType.PersonnelTypeRename; } set { eventTypePersonnelType.PersonnelTypeRename = value; } }

        [FwLogicProperty(Id:"vLvVd06N7Tle")]
        public bool? ShowOfficePhone { get { return eventTypePersonnelType.ShowOfficePhone; } set { eventTypePersonnelType.ShowOfficePhone = value; } }

        [FwLogicProperty(Id:"EyzmSk8I8zln")]
        public bool? ShowOfficeExtension { get { return eventTypePersonnelType.ShowOfficeExtension; } set { eventTypePersonnelType.ShowOfficeExtension = value; } }

        [FwLogicProperty(Id:"o7JSbr9cxOKa")]
        public bool? ShowCellular { get { return eventTypePersonnelType.ShowCellular; } set { eventTypePersonnelType.ShowCellular = value; } }

        [FwLogicProperty(Id:"XgtkrFp25H7A")]
        public decimal? OrderBy { get { return eventTypePersonnelType.OrderBy; } set { eventTypePersonnelType.OrderBy = value; } }

        [FwLogicProperty(Id:"BYYUi4ABPq0s")]
        public string DateStamp { get { return eventTypePersonnelType.DateStamp; } set { eventTypePersonnelType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
