using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.DealNote
{
    public class DealNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealNoteRecord dealNote = new DealNoteRecord();
        DealNoteLoader dealNoteLoader = new DealNoteLoader();
        public DealNoteLogic()
        {
            dataRecords.Add(dealNote);
            dataLoader = dealNoteLoader;
            dealNote.AfterSave += OnAfterSaveDealNote;
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
        public bool? Notify { get { return dealNote.Notify; } set { dealNote.Notify = value; } }
        public string DateStamp { get { return dealNote.DateStamp; } set { dealNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveDealNote(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = dealNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}