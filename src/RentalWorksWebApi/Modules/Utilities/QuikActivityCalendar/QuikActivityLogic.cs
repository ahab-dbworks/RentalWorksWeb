using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Activity;

namespace WebApi.Modules.Utilities.QuikActivity
{
    [FwLogic(Id: "u8LEDq5KKwNY0")]
    public class QuikActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ActivityRecord activity = new ActivityRecord();
        QuikActivityLoader quikActivityLoader = new QuikActivityLoader();
        public QuikActivityLogic()
        {
            dataRecords.Add(activity);
            dataLoader = quikActivityLoader;

            ReloadOnSave = false;
            LoadOriginalBeforeSaving = false;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DSAzjLm9RRZvI", IsPrimaryKey: true)]
        public int? ActivityId { get { return activity.ActivityId; } set { activity.ActivityId = value; } }

        [FwLogicProperty(Id: "SGrnWiB0jbk", IsReadOnly: true)]
        public string ActivityDate { get; set; }

        [FwLogicProperty(Id: "jgDEOKQa2Dxst", IsReadOnly: true)]
        public string ActivityTime { get; set; }

        [FwLogicProperty(Id: "rveIxP0YgE4XB", IsReadOnly: true)]
        public string ActivityTypeId { get; set; }

        [FwLogicProperty(Id: "32SGrnWiB0jbk", IsReadOnly: true)]
        public string ActivityTypeDescription { get; set; }

        [FwLogicProperty(Id: "nBSClOUSyGeJm", IsReadOnly: true)]
        public string ActivityTypeColor { get; set; }

        [FwLogicProperty(Id: "IP12sBYs3oMqo", IsReadOnly: true)]
        public string ActivityTypeTextColor { get; set; }

        [FwLogicProperty(Id: "WhxNW1CxoDVci")]
        public int? ActivityStatusId { get { return activity.ActivityStatusId; } set { activity.ActivityStatusId = value; } }

        [FwLogicProperty(Id: "Z7VeqzzJ2TywZ", IsReadOnly: true)]
        public string ActivityStatus { get; set; }

        [FwLogicProperty(Id: "LEQnoOHeRKI85", IsReadOnly: true)]
        public string ActivityStatusColor { get; set; }

        [FwLogicProperty(Id: "Oa6EMnuN9i9re", IsReadOnly: true)]
        public string ActivityStatusTextColor { get; set; }

        [FwLogicProperty(Id: "ppcWLsnx6rXtG", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "WLkc5TblFXg8U", IsReadOnly: true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "WuIniVBRJR6IT", IsReadOnly: true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id: "0lhZ515LxrDGl", IsReadOnly: true)]
        public string OrderTypeController { get; set; }

        [FwLogicProperty(Id: "DKYiaPmxnoahF", IsReadOnly: true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "uSWgAkcqC8s2n", IsReadOnly: true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id: "TPfRpUvb5HH5r", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "gIwtkn5K0vFax", IsReadOnly: true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id: "Qtxe7PJA7sYK9", IsReadOnly: true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "InxHNuWcFEprd", IsReadOnly: true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id: "yeBNfnVdmT6IS", IsReadOnly: true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "UlZIuzD8wTj30", IsReadOnly: true)]
        public string Description { get; set; }

        //[FwLogicProperty(Id: "GUScPBh7s8htg", IsReadOnly: true)]
        //public decimal? Quantity { get; set; }

        [FwLogicProperty(Id: "ufJVOCOok1nOK", IsReadOnly: true)]
        public int? TotalQuantity { get; set; }

        [FwLogicProperty(Id: "vRMyziZJDhN4M", IsReadOnly: true)]
        public int? RemainingQuantity { get; set; }

        [FwLogicProperty(Id: "mKWDNk34rZOBN", IsReadOnly: true)]
        public int? CompleteQuantity { get; set; }

        [FwLogicProperty(Id: "Qj7mH7Ia1XL79", IsReadOnly: true)]
        public decimal? CompletePercent { get; set; }

        [FwLogicProperty(Id: "dYzXIeMoxLAtA")]
        public string AssignedToUserId { get { return activity.AssignedToUserId; } set { activity.AssignedToUserId = value; } }

        [FwLogicProperty(Id: "NvFFmWN1o96WS", IsReadOnly: true)]
        public string AssignedToUserName { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
