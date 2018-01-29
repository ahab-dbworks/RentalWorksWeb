using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.AppRole;

namespace WebApi.Modules.Settings.PoApproverRole
{
    public class PoApproverRoleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        AppRoleRecord poApproverRole = new AppRoleRecord();
        PoApproverRoleLoader poApproverRoleLoader = new PoApproverRoleLoader();

        public PoApproverRoleLogic()
        {
            dataRecords.Add(poApproverRole);
            dataLoader = poApproverRoleLoader;
            BeforeSave += OnBeforeSave;
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
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RoleType = "POAPPROVER";
        }
        //------------------------------------------------------------------------------------
    }

}
