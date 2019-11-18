using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.ContactNote
{
    [FwLogic(Id:"AG3Me8bVnL39")]
    public class ContactNoteLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"5miwkaG0SEGP", IsPrimaryKey:true)]
        public string ContactNoteId { get { return contactNote.ContactNoteId; } set { contactNote.ContactNoteId = value; } }

        [FwLogicProperty(Id:"O3iIgXu55pf")]
        public string ContactId { get { return contactNote.ContactId; } set { contactNote.ContactId = value; } }

        [FwLogicProperty(Id:"5UA7B9PEmjv")]
        public string CompanyId { get { return contactNote.CompanyId; } set { contactNote.CompanyId = value; } }

        [FwLogicProperty(Id:"nq0GmjAGTSb")]
        public string CompanyContactId { get { return contactNote.CompanyContactId; } set { contactNote.CompanyContactId = value; } }

        [FwLogicProperty(Id:"ihn5nzqy8gK")]
        public string NoteDate { get { return contactNote.NoteDate; } set { contactNote.NoteDate = value; } }

        [FwLogicProperty(Id:"OIG4pwQqCQ0")]
        public string NotesById { get { return contactNote.NotesById; } set { contactNote.NotesById = value; } }

        [FwLogicProperty(Id:"GYoUEwSuPdAd", IsReadOnly:true)]
        public string NotesBy { get; set; }

        [FwLogicProperty(Id:"osiwK6BYgKj")]
        public string Description { get { return contactNote.Description; } set { contactNote.Description = value; } }

        [FwLogicProperty(Id:"1nK2iqC5IjI")]
        public string Notes { get { return contactNote.Notes; } set { contactNote.Notes = value; } }

        [FwLogicProperty(Id:"YRC8U2hp4xb")]
        public string DateStamp { get { return contactNote.DateStamp; } set { contactNote.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
