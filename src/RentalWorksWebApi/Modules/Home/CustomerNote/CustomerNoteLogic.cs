using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace RentalWorksWebApi.Modules.Home.CustomerNote
{
    public class CustomerNoteLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerNoteRecord customerNote = new CustomerNoteRecord();
        CustomerNoteLoader customerNoteLoader = new CustomerNoteLoader();
        public CustomerNoteLogic()
        {
            dataRecords.Add(customerNote);
            dataLoader = customerNoteLoader;
            customerNote.AfterSaves += OnAfterSavesCustomerNote;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerNoteId { get { return customerNote.CustomerNoteId; } set { customerNote.CustomerNoteId = value; } }
        public string CustomerId { get { return customerNote.CustomerId; } set { customerNote.CustomerId = value; } }
        public string NoteDate { get { return customerNote.NoteDate; } set { customerNote.NoteDate = value; } }
        public string NotesById { get { return customerNote.NotesById; } set { customerNote.NotesById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NotesBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get { return customerNote.Description; } set { customerNote.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public bool Notify { get { return customerNote.Notify; } set { customerNote.Notify = value; } }
        public string DateStamp { get { return customerNote.DateStamp; } set { customerNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnAfterSavesCustomerNote(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = customerNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}