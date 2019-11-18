using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.DealNote
{
    [FwLogic(Id:"jpUztkxP5VNI")]
    public class DealNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealNoteRecord dealNote = new DealNoteRecord();
        DealNoteLoader dealNoteLoader = new DealNoteLoader();
        public DealNoteLogic()
        {
            dataRecords.Add(dealNote);
            dataLoader = dealNoteLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"XnGRG8WE0enx", IsPrimaryKey:true)]
        public string DealNoteId { get { return dealNote.DealNoteId; } set { dealNote.DealNoteId = value; } }

        [FwLogicProperty(Id:"OUnOINEIDSxV")]
        public string DealId { get { return dealNote.DealId; } set { dealNote.DealId = value; } }

        [FwLogicProperty(Id:"6Re5VmoJHAUv")]
        public string NoteDate { get { return dealNote.NoteDate; } set { dealNote.NoteDate = value; } }

        [FwLogicProperty(Id:"qckL2miPYhpB")]
        public string NotesById { get { return dealNote.NotesById; } set { dealNote.NotesById = value; } }

        [FwLogicProperty(Id:"W5tF7QLIrVlz", IsReadOnly:true)]
        public string NotesBy { get; set; }

        [FwLogicProperty(Id:"I4WBrxLF409I", IsReadOnly:true)]
        public string Description { get { return dealNote.Description; } set { dealNote.Description = value; } }

        [FwLogicProperty(Id:"W5tF7QLIrVlz", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"E7SmVtBKdyxM")]
        public bool? Notify { get { return dealNote.Notify; } set { dealNote.Notify = value; } }

        [FwLogicProperty(Id:"F3NJ2sr3UmX3")]
        public string DateStamp { get { return dealNote.DateStamp; } set { dealNote.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveNote = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveNote = true;
            }
            else if (e.Original != null)
            {
                DealNoteLogic orig = (DealNoteLogic)e.Original;
                doSaveNote = (!orig.Notes.Equals(Notes));
            }
            if (doSaveNote)
            {
                bool saved = dealNote.SaveNoteASync(Notes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
