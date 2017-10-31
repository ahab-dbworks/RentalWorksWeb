using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules..PersonalEvent 
{ 
public class PersonalEventLogic : RwBusinessLogic 
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
public string ContacteventId { get { return personalEvent.ContacteventId; } set { personalEvent.ContacteventId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Contactevent { get; set; } 
public string Eventdate { get { return personalEvent.Eventdate; } set { personalEvent.Eventdate = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public int? Color { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public bool Textcolor { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public bool Recurring { get; set; } 
public string DateStamp { get { return personalEvent.DateStamp; } set { personalEvent.DateStamp = value; } } 
//------------------------------------------------------------------------------------ 
} 
} 
