using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VendorClass
{
    public class VendorClassLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorClassRecord vendorClass = new VendorClassRecord();
        public VendorClassLogic()
        {
            dataRecords.Add(vendorClass);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorClassId { get { return vendorClass.VendorClassId; } set { vendorClass.VendorClassId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VendorClass { get { return vendorClass.VendorClass; } set { vendorClass.VendorClass = value; } }
        public bool? Inactive { get { return vendorClass.Inactive; } set { vendorClass.Inactive = value; } }
        public string DateStamp { get { return vendorClass.DateStamp; } set { vendorClass.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
