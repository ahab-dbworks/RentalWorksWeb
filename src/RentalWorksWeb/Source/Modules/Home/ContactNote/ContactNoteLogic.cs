using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules..ContactNote 
{ 
public class ContactNoteLogic : RwBusinessLogic 
{ 
//------------------------------------------------------------------------------------ 
ContactNoteRecord contactNote = new ContactNoteRecord(); 
ContactNoteLoader contactNoteLoader = new ContactNoteLoader(); 
public ContactNoteLogic() 
{ 
dataRecords.Add(contactNote); 
dataLoader = contactNoteLoader; 
} 
//------------------------------------------------------------------------------------ 
[FwBusinessLogicField(isPrimaryKey: true)] 
public string ContactNoteId { get { return contactNote.ContactNoteId; } set { contactNote.ContactNoteId = value; } } 
public string ContactId { get { return contactNote.ContactId; } set { contactNote.ContactId = value; } } 
public string CompanyContactId { get { return contactNote.CompanyContactId; } set { contactNote.CompanyContactId = value; } } 
public string NoteDate { get { return contactNote.NoteDate; } set { contactNote.NoteDate = value; } } 
public string NotesById { get { return contactNote.NotesById; } set { contactNote.NotesById = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Namefml { get; set; } 
public string NotesDescription { get { return contactNote.NotesDescription; } set { contactNote.NotesDescription = value; } } 
public string CompanyId { get { return contactNote.CompanyId; } set { contactNote.CompanyId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Company { get; set; } 
public string Notes { get { return contactNote.Notes; } set { contactNote.Notes = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string ContactType { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Color { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public bool Inactive { get; set; } 
public string DateStamp { get { return contactNote.DateStamp; } set { contactNote.DateStamp = value; } } 
//------------------------------------------------------------------------------------ 
} 
} 
