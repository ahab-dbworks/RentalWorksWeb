using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.OrderTypePersonnelType;

namespace RentalWorksWebApi.Modules.Settings.EventTypePersonnelType
{
    public class EventTypePersonnelTypeLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string EventTypePersonnelTypeId { get { return eventTypePersonnelType.OrderTypePersonnelTypeId; } set { eventTypePersonnelType.OrderTypePersonnelTypeId = value; } }
        public string EventTypeId { get { return eventTypePersonnelType.OrderTypeId; } set { eventTypePersonnelType.OrderTypeId = value; } }
        public string PersonnelTypeId { get { return eventTypePersonnelType.PersonnelTypeId; } set { eventTypePersonnelType.PersonnelTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PersonnelType { get; set; }
        public string PersonnelTypeRename { get { return eventTypePersonnelType.PersonnelTypeRename; } set { eventTypePersonnelType.PersonnelTypeRename = value; } }
        public bool? ShowOfficePhone { get { return eventTypePersonnelType.ShowOfficePhone; } set { eventTypePersonnelType.ShowOfficePhone = value; } }
        public bool? ShowOfficeExtension { get { return eventTypePersonnelType.ShowOfficeExtension; } set { eventTypePersonnelType.ShowOfficeExtension = value; } }
        public bool? ShowCellular { get { return eventTypePersonnelType.ShowCellular; } set { eventTypePersonnelType.ShowCellular = value; } }
        public decimal? OrderBy { get { return eventTypePersonnelType.OrderBy; } set { eventTypePersonnelType.OrderBy = value; } }
        public string DateStamp { get { return eventTypePersonnelType.DateStamp; } set { eventTypePersonnelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}