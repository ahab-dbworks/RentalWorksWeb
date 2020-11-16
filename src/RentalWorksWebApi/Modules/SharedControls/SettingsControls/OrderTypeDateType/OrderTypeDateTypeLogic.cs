using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [FwLogic(Id: "Q5iDJYjeUrBc")]
    public class OrderTypeDateTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeDateTypeRecord orderTypeDateType = new OrderTypeDateTypeRecord();
        OrderTypeDateTypeLoader orderTypeDateTypeLoader = new OrderTypeDateTypeLoader();
        public OrderTypeDateTypeLogic()
        {
            dataRecords.Add(orderTypeDateType);
            dataLoader = orderTypeDateTypeLoader;

            BeforeDelete += OnBeforeDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Fs92UMGmzOJ8", IsPrimaryKey: true)]
        public string OrderTypeDateTypeId { get { return orderTypeDateType.OrderTypeDateTypeId; } set { orderTypeDateType.OrderTypeDateTypeId = value; } }

        [FwLogicProperty(Id: "Fs92UMGmzOJ8", IsReadOnly: true)]
        public string OrderTypeId { get { return orderTypeDateType.OrderTypeId; } set { orderTypeDateType.OrderTypeId = value; } }

        [FwLogicProperty(Id: "rQ24yorcZv2")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id: "AdRVWhcxjG2Ov", DisableDirectModify: true)]
        public int? ActivityTypeId { get { return orderTypeDateType.ActivityTypeId; } set { orderTypeDateType.ActivityTypeId = value; } }

        [FwLogicProperty(Id: "mEnkjmCynL71C", IsReadOnly: true)]
        public string ActivityType { get; set; }

        [FwLogicProperty(Id: "evmopqZRYP3", IsReadOnly: true)]
        public string Description { get; set; }

        [FwLogicProperty(Id: "MD2vZ2PPPuw", IsReadOnly: true)]
        public string Rename { get; set; }

        [FwLogicProperty(Id: "Px6pMD5fAU6m", IsReadOnly: true)]
        public string DescriptionDisplay { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id: "3fztsYVeD2A")]
        public bool? SystemType { get { return orderTypeDateType.SystemType; } set { orderTypeDateType.SystemType = value; } }

        [FwLogicProperty(Id: "r3HTv7hNewC")]
        public bool? Enabled { get { return orderTypeDateType.Enabled; } set { orderTypeDateType.Enabled = value; } }

        //[FwLogicProperty(Id:"QfKELUy7uz3")]
        //public bool? Milestone { get { return orderTypeDateType.Milestone; } set { orderTypeDateType.Milestone = value; } }

        //[FwLogicProperty(Id:"PbEG2SkM72A")]
        //public bool? ProductionActivity { get { return orderTypeDateType.ProductionActivity; } set { orderTypeDateType.ProductionActivity = value; } }

        //[FwLogicProperty(Id:"FHScUmMOygz")]
        //public bool? RequiredOnQuote { get { return orderTypeDateType.RequiredOnQuote; } set { orderTypeDateType.RequiredOnQuote = value; } }

        //[FwLogicProperty(Id:"c4SWMKTQTAYo")]
        //public bool? RequiredOnOrder { get { return orderTypeDateType.RequiredOnOrder; } set { orderTypeDateType.RequiredOnOrder = value; } }

        [FwLogicProperty(Id: "oM37jcODwW7b", IsReadOnly: true)]
        public string Color { get; set; }

        [FwLogicProperty(Id: "FaVm45YAegNF4", IsReadOnly: true)]
        public string TextColor { get; set; }

        [FwLogicProperty(Id: "lp6TThBq2eU3")]
        public decimal? OrderBy { get { return orderTypeDateType.OrderBy; } set { orderTypeDateType.OrderBy = value; } }

        [FwLogicProperty(Id: "J60GuLbDstHF")]
        public string DateStamp { get { return orderTypeDateType.DateStamp; } set { orderTypeDateType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 

        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            OrderTypeDateTypeLogic orig = null;
            if (original != null)
            {
                orig = (OrderTypeDateTypeLogic)original;
            }

            //below handled by DisableDirectModify
            //if (isValid)
            //{
            //    if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
            //    {
            //        if (ActivityType != null)
            //        {
            //            if (!ActivityType.Equals(orig.ActivityType))
            //            {
            //                isValid = false;
            //                validateMsg = $"Activity Type cannot be modified.";
            //            }
            //        }
            //    }
            //}

            if (isValid)
            {
                bool? enabled = Enabled;
                if ((enabled == null) && (orig != null))
                {
                    enabled = orig.Enabled;
                }

                string activityType = ActivityType;
                if (string.IsNullOrEmpty(activityType))
                {
                    activityType = orig.ActivityType;
                }

                if (!enabled.GetValueOrDefault(false))
                {
                    if ((activityType.Equals(RwConstants.ACTIVITY_TYPE_START)) || (activityType.Equals(RwConstants.ACTIVITY_TYPE_STOP)))
                    {
                        isValid = false;
                        validateMsg = $"Activity Type {activityType} cannot be disabled.";
                    }
                }

            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            OrderTypeDateTypeLogic l2 = new OrderTypeDateTypeLogic();
            l2.SetDependencies(this.AppConfig, this.UserSession);
            object[] pk = GetPrimaryKeys();
            bool b = l2.LoadAsync<OrderTypeDateTypeLogic>(pk).Result;
            if (l2.SystemType.Value)
            {
                e.PerformDelete = false;
                e.ErrorMessage = $"Cannot delete a System Activity record.  Disable it instead.";
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
