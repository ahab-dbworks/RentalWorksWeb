using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.EventCategory
{
    public class EventCategoryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        EventCategoryRecord eventCategory = new EventCategoryRecord();
        public EventCategoryLogic()
        {
            dataRecords.Add(eventCategory);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string EventCategoryId { get { return eventCategory.EventCategoryId; } set { eventCategory.EventCategoryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string EventCategory { get { return eventCategory.EventCategory; } set { eventCategory.EventCategory = value; } }
        public string EventCategoryCode { get { return eventCategory.EventCategoryCode; } set { eventCategory.EventCategoryCode = value; } }
        public bool Inactive { get { return eventCategory.Inactive; } set { eventCategory.Inactive = value; } }
        public string DateStamp { get { return eventCategory.DateStamp; } set { eventCategory.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
