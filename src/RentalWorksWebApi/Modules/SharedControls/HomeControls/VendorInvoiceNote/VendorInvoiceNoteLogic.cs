using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceNote
{
    [FwLogic(Id: "iA50v1JQJpjB")]
    public class VendorInvoiceNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceNoteRecord vendorInvoiceNote = new VendorInvoiceNoteRecord();
        VendorInvoiceNoteLoader vendorInvoiceNoteLoader = new VendorInvoiceNoteLoader();
        public VendorInvoiceNoteLogic()
        {
            dataRecords.Add(vendorInvoiceNote);
            dataLoader = vendorInvoiceNoteLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "fkQcgj53X5L0", IsPrimaryKey: true)]
        public string VendorInvoiceNoteId { get { return vendorInvoiceNote.VendorInvoiceNoteId; } set { vendorInvoiceNote.VendorInvoiceNoteId = value; } }
        [FwLogicProperty(Id: "lBEfClMvZQqn")]
        public string UniqueId1 { get { return vendorInvoiceNote.UniqueId1; } set { vendorInvoiceNote.UniqueId1 = value; } }
        [FwLogicProperty(Id: "cV4m6OPSPOj6")]
        public string UniqueId2 { get { return vendorInvoiceNote.UniqueId2; } set { vendorInvoiceNote.UniqueId2 = value; } }
        [FwLogicProperty(Id: "62KacxQyCQcw", IsReadOnly: true)]
        public string Notes { get; set; }
        [FwLogicProperty(Id: "ir0aeZ4OAigRb")]
        public string NoteDate { get { return vendorInvoiceNote.NoteDate; } set { vendorInvoiceNote.NoteDate = value; } }
        [FwLogicProperty(Id: "auMrj3uIKI3T")]
        public string UsersId { get { return vendorInvoiceNote.UsersId; } set { vendorInvoiceNote.UsersId = value; } }
        [FwLogicProperty(Id: "8mdp8HaQ311f", IsReadOnly: true)]
        public string Name { get; set; }
        [FwLogicProperty(Id: "mCrXk6UBnLZb")]
        public string NoteDescription { get { return vendorInvoiceNote.NoteDescription; } set { vendorInvoiceNote.NoteDescription = value; } }
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
