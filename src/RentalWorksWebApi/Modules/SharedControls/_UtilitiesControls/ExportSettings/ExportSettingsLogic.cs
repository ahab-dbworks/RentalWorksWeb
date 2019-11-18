using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.ExportSettings
{
    [FwLogic(Id: "kfYpG41QiLDw")]
    public class ExportSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ExportSettingsRecord exportSettings = new ExportSettingsRecord();
        public ExportSettingsLogic()
        {
            dataRecords.Add(exportSettings);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "p5C3bzMQVKfeD", IsPrimaryKey: true)]
        public string ExportSettingsId { get { return exportSettings.ExportSettingsId; } set { exportSettings.ExportSettingsId = value; } }

        [FwLogicProperty(Id: "V19x664aGmKaD", IsRecordTitle: true)]
        public string Description { get { return exportSettings.Description; } set { exportSettings.Description = value; } }
        [FwLogicProperty(Id: "u7u9K0h177Fh")]
        public string OfficeLocationId { get { return exportSettings.OfficeLocationId; } set { exportSettings.OfficeLocationId = value; } }



        [FwLogicProperty(Id: "Nkr6IUXPL1iW1")]
        public string HeaderString { get { return exportSettings.HeaderString; } set { exportSettings.HeaderString = value; } }
        [FwLogicProperty(Id: "QfjvVas7PSuw")]
        public bool? ExportHeader { get { return exportSettings.ExportHeader; } set { exportSettings.ExportHeader = value; } }


        [FwLogicProperty(Id: "7oS1GJNrlcOD")]
        public string ExportString { get { return exportSettings.ExportString; } set { exportSettings.ExportString = value; } }
        [FwLogicProperty(Id: "aGf1iL8vgfD8")]
        public string FileName { get { return exportSettings.FileName; } set { exportSettings.FileName = value; } }




        [FwLogicProperty(Id: "UwyISuLayE7L")]
        public bool? ExportFooter { get { return exportSettings.ExportFooter; } set { exportSettings.ExportFooter = value; } }
        [FwLogicProperty(Id: "E5DEKkeaxg39")]
        public string FooterString { get { return exportSettings.FooterString; } set { exportSettings.FooterString = value; } }

        [FwLogicProperty(Id: "0T17D0m5cu8Z")]
        public bool? Option01 { get { return exportSettings.Option01; } set { exportSettings.Option01 = value; } }
        [FwLogicProperty(Id: "2vTtDrd1F9to")]
        public string Option01FileName { get { return exportSettings.Option01FileName; } set { exportSettings.Option01FileName = value; } }
        [FwLogicProperty(Id: "XfVpLk2I6K8vy")]
        public bool? Option02 { get { return exportSettings.Option02; } set { exportSettings.Option02 = value; } }
        [FwLogicProperty(Id: "Dln9HFVBcP0B")]
        public string Option02FileName { get { return exportSettings.Option02FileName; } set { exportSettings.Option02FileName = value; } }
        [FwLogicProperty(Id: "4OeTIMeclJc1")]
        public bool? Option03 { get { return exportSettings.Option03; } set { exportSettings.Option03 = value; } }
        [FwLogicProperty(Id: "qHijb1lfSz7x")]
        public string Option03FileName { get { return exportSettings.Option03FileName; } set { exportSettings.Option03FileName = value; } }


        [FwLogicProperty(Id: "cKgpCKFVL1AD")]
        public bool? Active { get { return exportSettings.Active; } set { exportSettings.Active = value; } }

        [FwLogicProperty(Id: "cYza3alI7XD2d")]
        public string DateStamp { get { return exportSettings.DateStamp; } set { exportSettings.DateStamp = value; } }
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
