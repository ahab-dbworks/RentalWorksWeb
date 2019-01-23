using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Home.InvoiceNote
{
    [FwLogic(Id: "K2nLPq8MXJqN")]
    public class InvoiceNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceNoteRecord invoiceNote = new InvoiceNoteRecord();
        InvoiceNoteLoader invoiceNoteLoader = new InvoiceNoteLoader();
        public InvoiceNoteLogic()
        {
            dataRecords.Add(invoiceNote);
            dataLoader = invoiceNoteLoader;
            invoiceNote.AfterSave += OnAfterSaveInvoiceNote;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ObrJarXm0xoG", IsPrimaryKey: true)]
        public string InvoiceNoteId { get { return invoiceNote.InvoiceNoteId; } set { invoiceNote.InvoiceNoteId = value; } }
        [FwLogicProperty(Id: "FoXFw0Weh5So")]
        public string InvoiceId { get { return invoiceNote.InvoiceId; } set { invoiceNote.InvoiceId = value; } }
        [FwLogicProperty(Id: "S5h5X6tjEvfg8")]
        public string OrderId { get { return invoiceNote.OrderId; } set { invoiceNote.OrderId = value; } }
        [FwLogicProperty(Id: "b20JHX20wuV2d", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "xfXdtpWm9zPc")]
        public string NoteDate { get { return invoiceNote.NoteDate; } set { invoiceNote.NoteDate = value; } }
        [FwLogicProperty(Id: "aeYMwvHzbEOs")]
        public string Description { get { return invoiceNote.Description; } set { invoiceNote.Description = value; } }
        [FwLogicProperty(Id: "17vqtU8y9wnR")]
        public string NotesById { get { return invoiceNote.NotesById; } set { invoiceNote.NotesById = value; } }
        [FwLogicProperty(Id: "e6NxbE5gj5iBM", IsReadOnly: true)]
        public string NotesBy { get; set; }
        [FwLogicProperty(Id: "dfQSxS3mnvJ7", IsReadOnly: true)]
        public string Notes { get; set; }
        [FwLogicProperty(Id: "Ccw2UPUSAfhgu")]
        public string DateStamp { get { return invoiceNote.DateStamp; } set { invoiceNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveInvoiceNote(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = invoiceNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
