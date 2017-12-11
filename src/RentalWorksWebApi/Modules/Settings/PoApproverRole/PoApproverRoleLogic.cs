using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.AppRole;

namespace WebApi.Modules.Settings.PoApproverRole
{
    public class PoApproverRoleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        AppRoleRecord poApproverRole = new AppRoleRecord();
        PoApproverRoleLoader poApproverRoleLoader = new PoApproverRoleLoader();

        public PoApproverRoleLogic()
        {
            dataRecords.Add(poApproverRole);
            dataLoader = poApproverRoleLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoApproverRoleId { get { return poApproverRole.AppRoleId; } set { poApproverRole.AppRoleId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoApproverRole { get { return poApproverRole.AppRole; } set { poApproverRole.AppRole = value; } }
        [JsonIgnore]
        public string RoleType { get { return poApproverRole.RoleType; } set { poApproverRole.RoleType = value; } }
        public string PoApproverType { get { return poApproverRole.PoApproverType; } set { poApproverRole.PoApproverType = value; } }
        public bool? Inactive { get { return poApproverRole.Inactive; } set { poApproverRole.Inactive = value; } }
        public string DateStamp { get { return poApproverRole.DateStamp; } set { poApproverRole.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RoleType = "POAPPROVER";
        }
        //------------------------------------------------------------------------------------
    }

}
