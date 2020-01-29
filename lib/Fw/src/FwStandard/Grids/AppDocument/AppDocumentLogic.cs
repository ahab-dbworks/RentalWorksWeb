using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System;
using System.Threading.Tasks;

namespace FwStandard.Grids.AppDocument
{
    [FwLogic(Id: "zuFWMVYo0LqlY")]
    public class AppDocumentLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AppDocumentRecord appDocument = new AppDocumentRecord();
        AppDocumentLoader appDocumentLoader = new AppDocumentLoader();
        public AppDocumentLogic()
        {
            dataRecords.Add(appDocument);
            dataLoader = appDocumentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D4onPEMcc5rU", IsPrimaryKey: true)]
        public string AppDocumentId { get { return appDocument.AppDocumentId; } set { appDocument.AppDocumentId = value; } }
        [FwLogicProperty(Id: "mO9IHWbp0gHx")]
        public string DocumentTypeId { get { return appDocument.DocumentTypeId; } set { appDocument.DocumentTypeId = value; } }
        [FwLogicProperty(Id: "6p04SUJjZ0SR8")]
        public string UniqueId1 { get { return appDocument.UniqueId1; } set { appDocument.UniqueId1 = value; } }
        [FwLogicProperty(Id: "HQ5X1e02ZOflD")]
        public string UniqueId2 { get { return appDocument.UniqueId2; } set { appDocument.UniqueId2 = value; } }
        [FwLogicProperty(Id: "fIGSqv9uXCVv")]
        public int? UniqueId1Int { get { return appDocument.UniqueId1Int; } set { appDocument.UniqueId1Int = value; } }
        [FwLogicProperty(Id: "3BuqoMDOQ6zMd")]
        public string Description { get { return appDocument.Description; } set { appDocument.Description = value; } }
        [FwLogicProperty(Id: "bDYUtAjBDeww")]
        public string InputByUsersId { get { return appDocument.InputByUsersId; } set { appDocument.InputByUsersId = value; } }
        [FwLogicProperty(Id: "D4SRviqFO2oKP", IsReadOnly: true)]
        public string Extension { get; set; }
        [FwLogicProperty(Id: "Mnc6EuVdsHPRY")]
        public string AttachDate { get { return appDocument.AttachDate; } set { appDocument.AttachDate = value; } }
        [FwLogicProperty(Id: "J2ZtLCbH5uZHF")]
        public string AttachTime { get { return appDocument.AttachTime; } set { appDocument.AttachTime = value; } }
        [FwLogicProperty(Id: "i4bsUte3RNJAn", IsReadOnly: true)]
        public string DocumentType { get; set; }
        [FwLogicProperty(Id: "yclBOzCBUoK75", IsReadOnly: true)]
        public string InputBy { get; set; }
        [FwLogicProperty(Id: "hZy1MLzHv1g3v", IsReadOnly: true)]
        public bool? HasImage { get; set; }
        [FwLogicProperty(Id: "ppq4min6dQ5u", IsReadOnly: true)]
        public bool? HasFile { get; set; }
        [FwLogicProperty(Id: "TFcFiL5IVWUU6")]
        public bool? AttachToEmail { get { return appDocument.AttachToEmail; } set { appDocument.AttachToEmail = value; } }
        [FwLogicProperty(Id: "Z9H0vG2lTAL2V")]
        public bool? Inactive { get { return appDocument.Inactive; } set { appDocument.Inactive = value; } }
        [FwLogicProperty(Id: "frbbeuJWSctH")]
        public string DateStamp { get { return appDocument.DateStamp; } set { appDocument.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 

        protected override Task BeforeSaveAsync(BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                this.InputByUsersId = this.UserSession.UsersId;
                this.AttachDate = DateTime.Today.ToString("yyyy-MM-dd");
                this.AttachTime = DateTime.Now.ToString("hh:mm:ss");
            }
            return base.BeforeSaveAsync(e);
        }
        //------------------------------------------------------------------------------------ 
    }
}
