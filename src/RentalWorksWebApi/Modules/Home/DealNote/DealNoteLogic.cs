﻿using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

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

        // TODO: save to separte table
        public string Notes { get; set; }
        public bool Notify { get { return dealNote.Notify; } set { dealNote.Notify = value; } }
        public string DateStamp { get { return dealNote.DateStamp; } set { dealNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}