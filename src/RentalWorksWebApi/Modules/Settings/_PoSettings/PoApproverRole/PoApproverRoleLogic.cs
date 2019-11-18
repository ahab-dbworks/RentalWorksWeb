using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.AppRole;

namespace WebApi.Modules.Settings.PoApproverRole
{
    [FwLogic(Id:"PZV7Ig6amENH")]
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
        [FwLogicProperty(Id:"HlO9xjo7GkKT", IsPrimaryKey:true)]
        public string PoApproverRoleId { get { return poApproverRole.AppRoleId; } set { poApproverRole.AppRoleId = value; } }

        [FwLogicProperty(Id:"HlO9xjo7GkKT", IsRecordTitle:true)]
        public string PoApproverRole { get { return poApproverRole.AppRole; } set { poApproverRole.AppRole = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"2zl1Tme89LsP")]
        public string RoleType { get { return poApproverRole.RoleType; } set { poApproverRole.RoleType = value; } }

        [FwLogicProperty(Id:"9nIcicYOgxlz")]
        public string PoApproverType { get { return poApproverRole.PoApproverType; } set { poApproverRole.PoApproverType = value; } }

        [FwLogicProperty(Id:"xbBi3euDap5M")]
        public bool? Inactive { get { return poApproverRole.Inactive; } set { poApproverRole.Inactive = value; } }

        [FwLogicProperty(Id:"8pru7GPn1Cu7")]
        public string DateStamp { get { return poApproverRole.DateStamp; } set { poApproverRole.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RoleType = "POAPPROVER";
        }
        //------------------------------------------------------------------------------------
    }

}
