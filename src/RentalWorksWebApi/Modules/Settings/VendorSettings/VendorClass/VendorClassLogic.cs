using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VendorSettings.VendorClass
{
    [FwLogic(Id:"NzMdrsSZhaSld")]
    public class VendorClassLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorClassRecord vendorClass = new VendorClassRecord();
        public VendorClassLogic()
        {
            dataRecords.Add(vendorClass);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"a1PXXauTkAcxa", IsPrimaryKey:true)]
        public string VendorClassId { get { return vendorClass.VendorClassId; } set { vendorClass.VendorClassId = value; } }

        [FwLogicProperty(Id:"a1PXXauTkAcxa", IsRecordTitle:true)]
        public string VendorClass { get { return vendorClass.VendorClass; } set { vendorClass.VendorClass = value; } }

        [FwLogicProperty(Id:"vKiQryGX6YLX")]
        public bool? Inactive { get { return vendorClass.Inactive; } set { vendorClass.Inactive = value; } }

        [FwLogicProperty(Id:"vRLcskFNgV0y")]
        public string DateStamp { get { return vendorClass.DateStamp; } set { vendorClass.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
