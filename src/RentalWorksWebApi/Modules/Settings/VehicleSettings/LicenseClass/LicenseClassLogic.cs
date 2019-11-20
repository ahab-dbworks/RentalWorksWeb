using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VehicleSettings.LicenseClass
{
    [FwLogic(Id:"uJNJ7jNfJekd")]
    public class LicenseClassLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        LicenseClassRecord licenseClass = new LicenseClassRecord();
        public LicenseClassLogic()
        {
            dataRecords.Add(licenseClass);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"mMIx1SON44hp", IsPrimaryKey:true)]
        public string LicenseClassId { get { return licenseClass.LicenseClassId; } set { licenseClass.LicenseClassId = value; } }

        [FwLogicProperty(Id:"mMIx1SON44hp", IsRecordTitle:true)]
        public string LicenseClass { get { return licenseClass.LicenseClass; } set { licenseClass.LicenseClass = value; } }

        [FwLogicProperty(Id:"UPnTcDQ63jL")]
        public string Description { get { return licenseClass.Description; } set { licenseClass.Description = value; } }

        [FwLogicProperty(Id:"gYKmI7gXt38")]
        public bool? Regulated { get { return licenseClass.Regulated; } set { licenseClass.Regulated = value; } }

        [FwLogicProperty(Id:"KR1IYHuJRNI")]
        public bool? Inactive { get { return licenseClass.Inactive; } set { licenseClass.Inactive = value; } }

        [FwLogicProperty(Id:"vrIY7CoPOEz")]
        public string DateStamp { get { return licenseClass.DateStamp; } set { licenseClass.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
