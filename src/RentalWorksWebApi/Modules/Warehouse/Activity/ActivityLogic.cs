using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Warehouse.Activity
{
    [FwLogic(Id: "DRU8lyuD59CAj")]
    public class ActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ActivityRecord activity = new ActivityRecord();
        ActivityLoader activityLoader = new ActivityLoader();
        public ActivityLogic()
        {
            dataRecords.Add(activity);
            dataLoader = activityLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DSAzjLm9RRZvI", IsPrimaryKey: true)]
        public int? ActivityId { get { return activity.ActivityId; } set { activity.ActivityId = value; } }
        [FwLogicProperty(Id: "DscBuAWxbufkd")]
        public int? ActivityTypeId { get { return activity.ActivityTypeId; } set { activity.ActivityTypeId = value; } }
        [FwLogicProperty(Id: "DSFAZnqlcN7ez", IsReadOnly: true)]
        public string ActivityType { get; set; }
        [FwLogicProperty(Id: "DSfUPQKGr75NU")]
        public int? ActivityStatusId { get { return activity.ActivityStatusId; } set { activity.ActivityStatusId = value; } }
        [FwLogicProperty(Id: "dSkt9HTrBtF09", IsReadOnly: true)]
        public string ActivityStatus { get; set; }
        [FwLogicProperty(Id: "DsPLatlgKusxu")]
        public string OrderId { get { return activity.OrderId; } set { activity.OrderId = value; } }
        [FwLogicProperty(Id: "dsS2c7xAM3Lcs")]
        public string AssignedToUserId { get { return activity.AssignedToUserId; } set { activity.AssignedToUserId = value; } }
        [FwLogicProperty(Id: "dsSpAq5uVHhGu")]
        public string ActivityDateTime { get { return activity.ActivityDateTime; } set { activity.ActivityDateTime = value; } }
        [FwLogicProperty(Id: "dt1CVVobHwHhG")]
        public int? TotalQuantity { get { return activity.TotalQuantity; } set { activity.TotalQuantity = value; } }
        [FwLogicProperty(Id: "dTHMXdmFKe7P4")]
        public int? CompleteQuantity { get { return activity.CompleteQuantity; } set { activity.CompleteQuantity = value; } }
        [FwLogicProperty(Id: "DTKQgkljZYqKv")]
        public decimal? CompletePercent { get { return activity.CompletePercent; } set { activity.CompletePercent = value; } }
        [FwLogicProperty(Id: "DTlLGEOhPFGik")]
        public string DateStamp { get { return activity.DateStamp; } set { activity.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
