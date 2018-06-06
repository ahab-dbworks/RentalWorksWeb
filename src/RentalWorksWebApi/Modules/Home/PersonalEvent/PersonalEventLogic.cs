using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.PersonalEvent
{
    public class PersonalEventLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PersonalEventRecord personalEvent = new PersonalEventRecord();
        PersonalEventLoader personalEventLoader = new PersonalEventLoader();
        public PersonalEventLogic()
        {
            dataRecords.Add(personalEvent);
            dataLoader = personalEventLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PersonalEventId { get { return personalEvent.PersonalEventId; } set { personalEvent.PersonalEventId = value; } }
        public string ContactId { get { return personalEvent.ContactId; } set { personalEvent.ContactId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Person { get; set; }
        public string ContactEventId { get { return personalEvent.ContactEventId; } set { personalEvent.ContactEventId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactEvent { get; set; }
        public string EventDate { get { return personalEvent.EventDate; } set { personalEvent.EventDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Color { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Recurring { get; set; }
        public string DateStamp { get { return personalEvent.DateStamp; } set { personalEvent.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
