using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VendorSettings.OrganizationType
{
    [FwLogic(Id:"XMZKLuAYCa0a")]
    public class OrganizationTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        OrganizationTypeRecord organizationType = new OrganizationTypeRecord();
        public OrganizationTypeLogic()
        {
            dataRecords.Add(organizationType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"PUJUXnvN7jmB", IsPrimaryKey:true)]
        public string OrganizationTypeId { get { return organizationType.OrganizationTypeId; } set { organizationType.OrganizationTypeId = value; } }

        [FwLogicProperty(Id:"PUJUXnvN7jmB", IsRecordTitle:true)]
        public string OrganizationType { get { return organizationType.OrganizationType; } set { organizationType.OrganizationType = value; } }

        [FwLogicProperty(Id:"CqcjHXCVv17I")]
        public string OrganizationTypeCode { get { return organizationType.OrganizationTypeCode;  } set { organizationType.OrganizationTypeCode = value; } }

        [FwLogicProperty(Id:"Rz4EYb2BioZl")]
        public bool? Inactive { get { return organizationType.Inactive; } set { organizationType.Inactive = value; } }

        [FwLogicProperty(Id:"XN2nYLkdzcie")]
        public string DateStamp { get { return organizationType.DateStamp; } set { organizationType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
