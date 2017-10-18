using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace RentalWorksWebApi.Modules.Home.DealNote
{
    public class DealNoteLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealNoteRecord dealNote = new DealNoteRecord();
        DealNoteLoader dealNoteLoader = new DealNoteLoader();
        public DealNoteLogic()
        {
            dataRecords.Add(dealNote);
            dataLoader = dealNoteLoader;
            dealNote.AfterSaves += OnAfterSavesDealNote;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealNoteId { get { return dealNote.DealNoteId; } set { dealNote.DealNoteId = value; } }
        public string DealId { get { return dealNote.DealId; } set { dealNote.DealId = value; } }
        public string NoteDate { get { return dealNote.NoteDate; } set { dealNote.NoteDate = value; } }
        public string NotesById { get { return dealNote.NotesById; } set { dealNote.NotesById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NotesBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get { return dealNote.Description; } set { dealNote.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public bool Notify { get { return dealNote.Notify; } set { dealNote.Notify = value; } }
        public string DateStamp { get { return dealNote.DateStamp; } set { dealNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnAfterSavesDealNote(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = dealNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}