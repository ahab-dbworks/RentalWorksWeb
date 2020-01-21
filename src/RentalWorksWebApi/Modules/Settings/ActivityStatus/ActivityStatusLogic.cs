using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Settings.ActivityStatus
{
    [FwLogic(Id: "eB5gMMoBIwzhr")]
    public class ActivityStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ActivityStatusRecord activityStatus = new ActivityStatusRecord();
        ActivityStatusLoader activityStatusLoader = new ActivityStatusLoader();
        public ActivityStatusLogic()
        {
            dataRecords.Add(activityStatus);
            dataLoader = activityStatusLoader;
            BeforeDelete += OnBeforeDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ebMWCfDqdLkCS", IsPrimaryKey: true)]
        public int? ActivityStatusId { get { return activityStatus.ActivityStatusId; } set { activityStatus.ActivityStatusId = value; } }
        [FwLogicProperty(Id: "Ebor0D0O6fhbw", IsRecordTitle: true)]
        public string ActivityStatus { get { return activityStatus.ActivityStatus; } set { activityStatus.ActivityStatus = value; } }
        [FwLogicProperty(Id: "eCvYpGm3ZWHP9")]
        public string Rename { get { return activityStatus.Rename; } set { activityStatus.Rename = value; } }
        [FwLogicProperty(Id: "eCXirPSPzbahG")]
        public bool? IsSystemStatus { get { return activityStatus.IsSystemStatus; } set { activityStatus.IsSystemStatus = value; } }
        [FwLogicProperty(Id: "EcXoMVdsMcgvR")]
        public int? ActivityTypeId { get { return activityStatus.ActivityTypeId; } set { activityStatus.ActivityTypeId = value; } }
        [FwLogicProperty(Id: "eD70qLu7SshIR", IsReadOnly: true)]
        public string ActivityType { get; set; }
        [FwLogicProperty(Id: "EdaMHWSpdjFP6")]
        public string Color { get { return activityStatus.Color; } set { activityStatus.Color = value; } }
        [FwLogicProperty(Id: "EDjN1drF0ayLq")]
        public string TextColor { get { return activityStatus.TextColor; } set { activityStatus.TextColor = value; } }
        [FwLogicProperty(Id: "eDJzA6mVlfB9l")]
        public bool? Inactive { get { return activityStatus.Inactive; } set { activityStatus.Inactive = value; } }
        [FwLogicProperty(Id: "EDx6vxS8alPR0")]
        public string DateStamp { get { return activityStatus.DateStamp; } set { activityStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode == TDataRecordSaveMode.smInsert)
            {
                IsSystemStatus = false;
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            ActivityStatusLogic l2 = new ActivityStatusLogic();
            l2.SetDependencies(this.AppConfig, this.UserSession);
            object[] pk = GetPrimaryKeys();
            bool b = l2.LoadAsync<ActivityStatusLogic>(pk).Result;
            if (l2.IsSystemStatus.Value)
            {
                e.PerformDelete = false;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
