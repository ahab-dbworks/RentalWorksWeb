using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.PersonalEvent
{
    [FwLogic(Id:"HWOH4PLDLfGn")]
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
        [FwLogicProperty(Id:"o5Lc66OTFwGC", IsPrimaryKey:true)]
        public string PersonalEventId { get { return personalEvent.PersonalEventId; } set { personalEvent.PersonalEventId = value; } }

        [FwLogicProperty(Id:"3ZNTf1GCMUy3")]
        public string ContactId { get { return personalEvent.ContactId; } set { personalEvent.ContactId = value; } }

        [FwLogicProperty(Id:"o5Lc66OTFwGC", IsReadOnly:true)]
        public string Person { get; set; }

        [FwLogicProperty(Id:"Rf9HhNddc6N9")]
        public string ContactEventId { get { return personalEvent.ContactEventId; } set { personalEvent.ContactEventId = value; } }

        [FwLogicProperty(Id:"EDyZ7Qih9NZv", IsReadOnly:true)]
        public string ContactEvent { get; set; }

        [FwLogicProperty(Id:"NvRfwgJvCb8p")]
        public string EventDate { get { return personalEvent.EventDate; } set { personalEvent.EventDate = value; } }

        [FwLogicProperty(Id:"dt1jYwQVrTcK", IsReadOnly:true)]
        public string Color { get; set; }

        [FwLogicProperty(Id:"og05BSynWKnK", IsReadOnly:true)]
        public bool? Recurring { get; set; }

        [FwLogicProperty(Id:"vxiGzHw1sB6Y")]
        public string DateStamp { get { return personalEvent.DateStamp; } set { personalEvent.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
