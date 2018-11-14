using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.CustomerNote
{
    [FwLogic(Id:"VP0oled6lBD6")]
    public class CustomerNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerNoteRecord customerNote = new CustomerNoteRecord();
        CustomerNoteLoader customerNoteLoader = new CustomerNoteLoader();
        public CustomerNoteLogic()
        {
            dataRecords.Add(customerNote);
            dataLoader = customerNoteLoader;
            customerNote.AfterSave += OnAfterSaveCustomerNote;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"UtwcaRLZ1Fpv", IsPrimaryKey:true)]
        public string CustomerNoteId { get { return customerNote.CustomerNoteId; } set { customerNote.CustomerNoteId = value; } }

        [FwLogicProperty(Id:"eyiiWG50mkf1")]
        public string CustomerId { get { return customerNote.CustomerId; } set { customerNote.CustomerId = value; } }

        [FwLogicProperty(Id:"nVlXiZ2f95GG")]
        public string NoteDate { get { return customerNote.NoteDate; } set { customerNote.NoteDate = value; } }

        [FwLogicProperty(Id:"hytymjdehuys")]
        public string NotesById { get { return customerNote.NotesById; } set { customerNote.NotesById = value; } }

        [FwLogicProperty(Id:"QXbbiCpQjlpR", IsReadOnly:true)]
        public string NotesBy { get; set; }

        [FwLogicProperty(Id:"tVnUVKbD4k5R", IsReadOnly:true)]
        public string Description { get { return customerNote.Description; } set { customerNote.Description = value; } }

        [FwLogicProperty(Id:"QXbbiCpQjlpR", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"a6ZOLl2G6fYc")]
        public bool? Notify { get { return customerNote.Notify; } set { customerNote.Notify = value; } }

        [FwLogicProperty(Id:"eYjBsiiW0TYS")]
        public string DateStamp { get { return customerNote.DateStamp; } set { customerNote.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnAfterSaveCustomerNote(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = customerNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}
