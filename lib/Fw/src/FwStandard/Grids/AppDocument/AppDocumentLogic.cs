using FwStandard.AppManager;
using FwStandard.BusinessLogic;

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
        public string DocumenttypeId { get { return appDocument.DocumenttypeId; } set { appDocument.DocumenttypeId = value; } }
        [FwLogicProperty(Id: "6p04SUJjZ0SR8")]
        public string Uniqueid1 { get { return appDocument.Uniqueid1; } set { appDocument.Uniqueid1 = value; } }
        [FwLogicProperty(Id: "HQ5X1e02ZOflD")]
        public string Uniqueid2 { get { return appDocument.Uniqueid2; } set { appDocument.Uniqueid2 = value; } }
        [FwLogicProperty(Id: "fIGSqv9uXCVv")]
        public int? Uniqueid1int { get { return appDocument.Uniqueid1int; } set { appDocument.Uniqueid1int = value; } }
        [FwLogicProperty(Id: "3BuqoMDOQ6zMd")]
        public string Description { get { return appDocument.Description; } set { appDocument.Description = value; } }
        [FwLogicProperty(Id: "bDYUtAjBDeww")]
        public string InputbyusersId { get { return appDocument.InputbyusersId; } set { appDocument.InputbyusersId = value; } }
        [FwLogicProperty(Id: "D4SRviqFO2oKP", IsReadOnly: true)]
        public string Extension { get; set; }
        [FwLogicProperty(Id: "Mnc6EuVdsHPRY")]
        public string Attachdate { get { return appDocument.Attachdate; } set { appDocument.Attachdate = value; } }
        [FwLogicProperty(Id: "J2ZtLCbH5uZHF")]
        public string Attachtime { get { return appDocument.Attachtime; } set { appDocument.Attachtime = value; } }
        [FwLogicProperty(Id: "i4bsUte3RNJAn", IsReadOnly: true)]
        public string Documenttype { get; set; }
        [FwLogicProperty(Id: "yclBOzCBUoK75", IsReadOnly: true)]
        public string Inputby { get; set; }
        [FwLogicProperty(Id: "hZy1MLzHv1g3v", IsReadOnly: true)]
        public bool? Hasimage { get; set; }
        [FwLogicProperty(Id: "ppq4min6dQ5u", IsReadOnly: true)]
        public bool? Hasfile { get; set; }
        [FwLogicProperty(Id: "TFcFiL5IVWUU6")]
        public bool? Attachtoemail { get { return appDocument.Attachtoemail; } set { appDocument.Attachtoemail = value; } }
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
        //------------------------------------------------------------------------------------ 
    }
}
