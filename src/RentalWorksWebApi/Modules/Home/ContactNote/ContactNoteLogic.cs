using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.ContactNote
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
        public string CompanyId { get { return contactNote.CompanyId; } set { contactNote.CompanyId = value; } }
        public string CompanyContactId { get { return contactNote.CompanyContactId; } set { contactNote.CompanyContactId = value; } }
        public string NoteDate { get { return contactNote.NoteDate; } set { contactNote.NoteDate = value; } }
        public string NotesById { get { return contactNote.NotesById; } set { contactNote.NotesById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NotesBy { get; set; }
        public string Description { get; set; }
        public string Notes { get { return contactNote.Notes; } set { contactNote.Notes = value; } }
        public string DateStamp { get { return contactNote.DateStamp; } set { contactNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}