using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.EventSettings.EventCategory
{
    [FwLogic(Id:"7acWvB8D61Gl")]
    public class EventCategoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        EventCategoryRecord eventCategory = new EventCategoryRecord();
        public EventCategoryLogic()
        {
            dataRecords.Add(eventCategory);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"WslYV77GAiMr", IsPrimaryKey:true)]
        public string EventCategoryId { get { return eventCategory.EventCategoryId; } set { eventCategory.EventCategoryId = value; } }

        [FwLogicProperty(Id:"WslYV77GAiMr", IsRecordTitle:true)]
        public string EventCategory { get { return eventCategory.EventCategory; } set { eventCategory.EventCategory = value; } }

        [FwLogicProperty(Id:"icbxGTRkvwpG")]
        public string EventCategoryCode { get { return eventCategory.EventCategoryCode; } set { eventCategory.EventCategoryCode = value; } }

        [FwLogicProperty(Id:"CCU0Pm9tjLxJ")]
        public bool? Inactive { get { return eventCategory.Inactive; } set { eventCategory.Inactive = value; } }

        [FwLogicProperty(Id:"3HumETfbtJjE")]
        public string DateStamp { get { return eventCategory.DateStamp; } set { eventCategory.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
