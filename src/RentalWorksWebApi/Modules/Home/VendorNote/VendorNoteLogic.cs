using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.VendorNote
{
    [FwLogic(Id:"hJSOpVXyzWtEx")]
    public class VendorNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorNoteRecord vendorNoteRecord = new VendorNoteRecord();
        VendorNoteLoader vendorNoteLoader = new VendorNoteLoader();
        public VendorNoteLogic()
        {
            dataRecords.Add(vendorNoteRecord);
            dataLoader = vendorNoteLoader;
            vendorNoteRecord.AfterSave += OnAfterSaveVendorNote;
        }
        [FwLogicProperty(Id:"Nvsn2c8YTejCk", IsPrimaryKey:true)]
        public string VendorNoteId { get { return vendorNoteRecord.VendorNoteId; } set { vendorNoteRecord.VendorNoteId = value; } }

        [FwLogicProperty(Id:"fm0dM0L7VUaF")]
        public string VendorId { get { return vendorNoteRecord.VendorId; } set { vendorNoteRecord.VendorId = value; } }

        [FwLogicProperty(Id:"brhDrEwvkoLo")]
        public string NoteDate { get { return vendorNoteRecord.NoteDate; } set { vendorNoteRecord.NoteDate = value; } }

        [FwLogicProperty(Id:"H5RuiurbvpYZ")]
        public string Description { get { return vendorNoteRecord.Description; } set { vendorNoteRecord.Description = value; } }

        [FwLogicProperty(Id:"UAkxIcU8wFbQ")]
        public string Notes { get { return vendorNoteRecord.Notes; } set { vendorNoteRecord.Notes = value; } }

        [FwLogicProperty(Id:"IBOoBHOHXa0F")]
        public bool? Notify { get { return vendorNoteRecord.Notify; } set { vendorNoteRecord.Notify = value; } }

        [FwLogicProperty(Id:"O9EfePL5UT1z")]
        public string NotesById { get { return vendorNoteRecord.NotesById; } set { vendorNoteRecord.NotesById = value; } }

        [FwLogicProperty(Id:"LrwD0HkVcm5jk", IsReadOnly:true)]
        public string NotesBy { get; set; }

        [FwLogicProperty(Id:"MfWadExD5zLu")]
        public string DateStamp { get { return vendorNoteRecord.DateStamp; } set { vendorNoteRecord.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnAfterSaveVendorNote(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = vendorNoteRecord.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------    
    }
}
