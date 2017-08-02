using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.OrganizationType
{
    public class OrganizationTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        OrganizationTypeRecord organizationType = new OrganizationTypeRecord();
        public OrganizationTypeLogic()
        {
            dataRecords.Add(organizationType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrganizationTypeId { get { return organizationType.OrganizationTypeId; } set { organizationType.OrganizationTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrganizationType { get { return organizationType.OrganizationType; } set { organizationType.OrganizationType = value; } }
        public string OrganizationTypeCode { get { return organizationType.OrganizationTypeCode;  } set { organizationType.OrganizationTypeCode = value; } }
        public bool Inactive { get { return organizationType.Inactive; } set { organizationType.Inactive = value; } }
        public string DateStamp { get { return organizationType.DateStamp; } set { organizationType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
