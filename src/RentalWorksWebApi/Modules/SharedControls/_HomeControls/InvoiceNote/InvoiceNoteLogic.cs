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
            AfterSave += OnAfterSave;
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
        [FwLogicProperty(Id: "cGz7BDdal42ZX")]
        public bool? PrintOnInvoice { get { return invoiceNote.PrintOnInvoice; } set { invoiceNote.PrintOnInvoice = value; } }
        [FwLogicProperty(Id: "Ccw2UPUSAfhgu")]
        public string DateStamp { get { return invoiceNote.DateStamp; } set { invoiceNote.DateStamp = value; } }
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
                InvoiceNoteLogic orig = (InvoiceNoteLogic)e.Original;
                doSaveNote = (!orig.Notes.Equals(Notes));
            }
            if (doSaveNote)
            {
                bool saved = invoiceNote.SaveNoteASync(Notes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
