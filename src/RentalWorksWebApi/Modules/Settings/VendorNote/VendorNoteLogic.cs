using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VendorNote
{
    public class VendorNoteLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorNoteRecord vendorNoteRecord = new VendorNoteRecord();
        VendorNoteLoader vendorNoteLoader = new VendorNoteLoader();
        public VendorNoteLogic()
        {
            dataRecords.Add(vendorNoteRecord);
            dataLoader = vendorNoteLoader;
        }
        public string VendorId { get { return vendorNoteRecord.VendorId; } set { vendorNoteRecord.VendorId = value; } }
        public string VendorNoteId { get { return vendorNoteRecord.VendorNoteId; } set { vendorNoteRecord.VendorNoteId = value; } }
        public string NoteDate { get { return vendorNoteRecord.NoteDate; } set { vendorNoteRecord.NoteDate = value; } }
        public string Description { get { return vendorNoteRecord.Description; } set { vendorNoteRecord.Description = value; } }
        public string Notes { get { return vendorNoteRecord.Notes; } set { vendorNoteRecord.Notes = value; } }
        public bool Notify { get { return vendorNoteRecord.Notify; } set { vendorNoteRecord.Notify = value; } }
        public string NotesById { get { return vendorNoteRecord.NotesById; } set { vendorNoteRecord.NotesById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NotesBy { get; set; }
        public string DateStamp { get { return vendorNoteRecord.DateStamp; } set { vendorNoteRecord.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
