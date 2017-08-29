using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VendorNoteGrid
{
    public class VendorNoteGridLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorNoteGridRecord vendorNoteGridRecord = new VendorNoteGridRecord();
        VendorNoteGridLoader vendorNoteGridLoader = new VendorNoteGridLoader();
        public VendorNoteGridLogic()
        {
            dataRecords.Add(vendorNoteGridRecord);
            dataLoader = vendorNoteGridLoader;
        }
        public string VendorId { get {return vendorNoteGridRecord.VendorId;} set{vendorNoteGridRecord.VendorId=value;} }
        //------------------------------------------------------------------------------------
        public string VendorNoteId { get {return vendorNoteGridRecord.VendorNoteId;} set {vendorNoteGridRecord.VendorNoteId=value;} }
        //------------------------------------------------------------------------------------
        public string NoteDate { get {return vendorNoteGridRecord.NoteDate;} set{vendorNoteGridRecord.NoteDate=value;} }
        //------------------------------------------------------------------------------------
        public string NotesDesc { get {return vendorNoteGridRecord.NotesDesc;} set{vendorNoteGridRecord.NotesDesc=value;} }
        //------------------------------------------------------------------------------------
        public string Notes { get {return vendorNoteGridRecord.Notes;} set{vendorNoteGridRecord.Notes=value;} }
        //------------------------------------------------------------------------------------
        public string Notify { get {return vendorNoteGridRecord.Notify;} set{vendorNoteGridRecord.Notify=value;} }
        //------------------------------------------------------------------------------------
        public string NotestById { get {return vendorNoteGridRecord.NotestById;} set{vendorNoteGridRecord.NotestById=value;} }
        //------------------------------------------------------------------------------------
        public string DateStamp { get {return vendorNoteGridRecord.DateStamp;} set{vendorNoteGridRecord.DateStamp=value;} }

    }

}
