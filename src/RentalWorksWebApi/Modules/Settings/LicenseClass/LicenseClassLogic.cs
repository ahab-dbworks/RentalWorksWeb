using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.LicenseClass
{
    public class LicenseClassLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        LicenseClassRecord licenseClass = new LicenseClassRecord();
        public LicenseClassLogic()
        {
            dataRecords.Add(licenseClass);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string LicenseClassId { get { return licenseClass.LicenseClassId; } set { licenseClass.LicenseClassId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string LicenseClass { get { return licenseClass.LicenseClass; } set { licenseClass.LicenseClass = value; } }
        public string Description { get { return licenseClass.Description; } set { licenseClass.Description = value; } }
        public bool? Regulated { get { return licenseClass.Regulated; } set { licenseClass.Regulated = value; } }
        public bool? Inactive { get { return licenseClass.Inactive; } set { licenseClass.Inactive = value; } }
        public string DateStamp { get { return licenseClass.DateStamp; } set { licenseClass.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
