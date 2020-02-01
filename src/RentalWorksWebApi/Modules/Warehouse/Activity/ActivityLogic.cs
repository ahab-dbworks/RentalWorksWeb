using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System;
using FwStandard.SqlServer;

namespace WebApi.Modules.Warehouse.Activity
{
    [FwLogic(Id: "HdQVAPTOInRWe")]
    public class ActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ActivityRecord activity = new ActivityRecord();
        ActivityLoader activityLoader = new ActivityLoader();
        public ActivityLogic()
        {
            dataRecords.Add(activity);
            dataLoader = activityLoader;

            BeforeSave += OnBeforeSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "hdVJ56DIgIMkZ", IsPrimaryKey: true)]
        public int? ActivityId { get { return activity.ActivityId; } set { activity.ActivityId = value; } }
        [FwLogicProperty(Id: "hDWIbFlA6Ckv8")]
        public int? ActivityTypeId { get { return activity.ActivityTypeId; } set { activity.ActivityTypeId = value; } }
        [FwLogicProperty(Id: "He586lF3mRO9E", IsReadOnly: true)]
        public string ActivityType { get; set; }
        [FwLogicProperty(Id: "hEIAKL6qfKsza", IsReadOnly: true)]
        public string ActivityTypeDescription { get; set; }
        [FwLogicProperty(Id: "HFC1dq9M29Lyc", IsReadOnly: true)]
        public string ActivityTypeColor { get; set; }
        [FwLogicProperty(Id: "HfdgFog42SMgf", IsReadOnly: true)]
        public string ActivityTypeTextColor { get; set; }
        [FwLogicProperty(Id: "HfPJhfo9L86bw")]
        public int? ActivityStatusId { get { return activity.ActivityStatusId; } set { activity.ActivityStatusId = value; } }
        [FwLogicProperty(Id: "Hfugl6LiE0o4T", IsReadOnly: true)]
        public string ActivityStatus { get; set; }
        [FwLogicProperty(Id: "hfWLnn9FGJjRd", IsReadOnly: true)]
        public string ActivityStatusDescription { get; set; }
        [FwLogicProperty(Id: "hfZRdSYs7NKc3", IsReadOnly: true)]
        public string ActivityStatusColor { get; set; }
        [FwLogicProperty(Id: "hG9Ll6SEFNetK", IsReadOnly: true)]
        public string ActivityStatusTextColor { get; set; }
        [FwLogicProperty(Id: "hGdE9xEmcXYVU")]
        public string OfficeLocationId { get { return activity.OfficeLocationId; } set { activity.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "hgq0fyxlws8ha")]
        public string WarehouseId { get { return activity.WarehouseId; } set { activity.WarehouseId = value; } }
        [FwLogicProperty(Id: "hGwM9MDLaNDXA")]
        public string OrderId { get { return activity.OrderId; } set { activity.OrderId = value; } }
        [FwLogicProperty(Id: "HGzTl0ReaOKNL", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "HHskXLi4HD3s9", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "HHw9xz3Fi57G7", IsReadOnly: true)]
        public string OrderType { get; set; }
        [FwLogicProperty(Id: "HIE9vfsRrvRLN", IsReadOnly: true)]
        public string OrderStatus { get; set; }
        [FwLogicProperty(Id: "hJ6Pa0wJkwYqm", IsReadOnly: true)]
        public string OrderTypeId { get; set; }
        [FwLogicProperty(Id: "HKAIqdF5jZll0", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "HKdmx6LxBg36M", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "HKnZ5DQjvfaEa", IsReadOnly: true)]
        public string OrderTypeDescription { get; set; }
        [FwLogicProperty(Id: "HksOAiS7cYh5v", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "hM0RhJbZfX4IY")]
        public string AssignedToUserId { get { return activity.AssignedToUserId; } set { activity.AssignedToUserId = value; } }
        [FwLogicProperty(Id: "hm4GewcwSYxTo", IsReadOnly: true)]
        public string AssignedToUserName { get; set; }
        [FwLogicProperty(Id: "hMhAqalSeyyEo")]
        public DateTime? ActivityDateTime { get { return activity.ActivityDateTime; } set { activity.ActivityDateTime = value; } }
        [FwLogicProperty(Id: "HMobX0fa6oIoi", IsReadOnly: true)]
        public string ActivityDate { get; set; }
        [FwLogicProperty(Id: "hmqpeY8dEP6jC", IsReadOnly: true)]
        public string ActivityTime { get; set; }
        [FwLogicProperty(Id: "HMwZfoR4aX4kn")]
        public int? TotalQuantity { get { return activity.TotalQuantity; } set { activity.TotalQuantity = value; } }
        [FwLogicProperty(Id: "HnI8OvVLfkQXp")]
        public int? CompleteQuantity { get { return activity.CompleteQuantity; } set { activity.CompleteQuantity = value; } }
        [FwLogicProperty(Id: "hNja8jObTR0Li")]
        public decimal? CompletePercent { get { return activity.CompletePercent; } set { activity.CompletePercent = value; } }
        [FwLogicProperty(Id: "Hnu4TGTfRxQm4")]
        public string DateStamp { get { return activity.DateStamp; } set { activity.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            ActivityLogic orig = null;
            string origActivityDate = "";
            string origActivityTime = "";
            //DateTime? origActivityDateTime = DateTime.MinValue;
            DateTime? origActivityDateTime = null;

            if (e.Original != null)
            {
                orig = (ActivityLogic)e.Original;
                origActivityDate = orig.ActivityDate;
                origActivityTime = orig.ActivityTime;
                origActivityDateTime = orig.ActivityDateTime;

                if (ActivityDateTime == null) 
                {
                    if ((!string.IsNullOrEmpty(ActivityDate)) || (!string.IsNullOrEmpty(ActivityTime)))
                    {
                        ActivityDateTime = origActivityDateTime;

                        if (!string.IsNullOrEmpty(ActivityDate))
                        {
                            ActivityDateTime = FwConvert.ToDateTime(ActivityDate, origActivityTime, "");
                        }
                        if (!string.IsNullOrEmpty(ActivityTime))
                        {
                            ActivityDateTime = FwConvert.ToDateTime(FwConvert.ToUSShortDate(ActivityDateTime.GetValueOrDefault(DateTime.MinValue).Date), ActivityTime, "");
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
