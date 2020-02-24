using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Settings.ActivityType
{
    [FwLogic(Id: "e1tEI9OMMnk7p")]
    public class ActivityTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ActivityTypeRecord activityType = new ActivityTypeRecord();
        ActivityTypeLoader activityTypeLoader = new ActivityTypeLoader();
        public ActivityTypeLogic()
        {
            dataRecords.Add(activityType);
            dataLoader = activityTypeLoader;

            BeforeDelete += OnBeforeDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "E227PJ3po6bhS", IsPrimaryKey: true)]
        public int? ActivityTypeId { get { return activityType.ActivityTypeId; } set { activityType.ActivityTypeId = value; } }
        [FwLogicProperty(Id: "E2CHlWqgXsDLZ")]
        public string ActivityType { get { return activityType.ActivityType; } set { activityType.ActivityType = value; } }
        [FwLogicProperty(Id: "E2DZmrMSNqHhY")]
        public string Description { get { return activityType.Description; } set { activityType.Description = value; } }
        [FwLogicProperty(Id: "CoW3eutj0pfbH", IsRecordTitle: true)]
        public string DescriptionDisplay { get; set; }
        [FwLogicProperty(Id: "e2GJ0za4mBUys")]
        public string Rename { get { return activityType.Rename; } set { activityType.Rename = value; } }
        [FwLogicProperty(Id: "E3dPVa4QG6PTP", DisableDirectModify: true)]
        public bool? IsSystemType { get { return activityType.IsSystemType; } set { activityType.IsSystemType = value; } }
        [FwLogicProperty(Id: "tPwWW9VUPc4ou", IsReadOnly: true)]
        public string SystemUser { get; set; }
        [FwLogicProperty(Id: "y5oZv9jMZpah7", IsReadOnly: true)]
        public string SystemUserColor { get; set; }
        [FwLogicProperty(Id: "E3Nbpw9nEmyvE")]
        public string Color { get { return activityType.Color; } set { activityType.Color = value; } }
        [FwLogicProperty(Id: "e3OMNwqVy5ar8")]
        public string TextColor { get { return activityType.TextColor; } set { activityType.TextColor = value; } }
        [FwLogicProperty(Id: "dvtqERbFSrOnt")]
        public bool? IsWarehouseOutbound { get { return activityType.IsWarehouseOutbound; } set { activityType.IsWarehouseOutbound = value; } }
        [FwLogicProperty(Id: "dzbthlrSszpio")]
        public bool? IsWarehouseInbound { get { return activityType.IsWarehouseInbound; } set { activityType.IsWarehouseInbound = value; } }
        [FwLogicProperty(Id: "Evv6fua99ARFe")]
        public bool? IsWarehouseDispatch { get { return activityType.IsWarehouseDispatch; } set { activityType.IsWarehouseDispatch = value; } }
        [FwLogicProperty(Id: "E54G216rnW6rf")]
        public bool? Inactive { get { return activityType.Inactive; } set { activityType.Inactive = value; } }
        [FwLogicProperty(Id: "E6oKVoRG5A9v0")]
        public string DateStamp { get { return activityType.DateStamp; } set { activityType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode == TDataRecordSaveMode.smInsert)
            {
                IsSystemType = false;
            }

            if (IsSystemType.GetValueOrDefault(false))
            {
                if (original != null)
                {
                    string origActivityType = ((ActivityTypeLogic)original).ActivityType;
                    if (!ActivityType.Equals(origActivityType))
                    {
                        isValid = false;
                        validateMsg = $"Cannot modify the Activity Type of a System {this.BusinessLogicModuleName} record.";
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            ActivityTypeLogic l2 = new ActivityTypeLogic();
            l2.SetDependencies(this.AppConfig, this.UserSession);
            object[] pk = GetPrimaryKeys();
            bool b = l2.LoadAsync<ActivityTypeLogic>(pk).Result;
            if (l2.IsSystemType.Value)
            {
                e.PerformDelete = false;
                e.ErrorMessage = $"Cannot delete a System {this.BusinessLogicModuleName} record.";
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
