using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [FwLogic(Id:"Q5iDJYjeUrBc")]
    public class OrderTypeDateTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeDateTypeRecord orderTypeDateType = new OrderTypeDateTypeRecord();
        OrderTypeDateTypeLoader orderTypeDateTypeLoader = new OrderTypeDateTypeLoader();
        public OrderTypeDateTypeLogic()
        {
            dataRecords.Add(orderTypeDateType);
            dataLoader = orderTypeDateTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"Fs92UMGmzOJ8", IsPrimaryKey:true)]
        public string OrderTypeDateTypeId { get { return orderTypeDateType.OrderTypeDateTypeId; } set { orderTypeDateType.OrderTypeDateTypeId = value; } }

        [FwLogicProperty(Id:"Fs92UMGmzOJ8", IsReadOnly:true)]
        public string OrderTypeId { get { return orderTypeDateType.OrderTypeId; } set { orderTypeDateType.OrderTypeId = value; } }

        [FwLogicProperty(Id:"rQ24yorcZv2")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"evmopqZRYP3")]
        public string Description { get { return orderTypeDateType.Description; } set { orderTypeDateType.Description = value; } }

        [FwLogicProperty(Id:"MD2vZ2PPPuw")]
        public string DescriptionRename { get { return orderTypeDateType.DescriptionRename; } set { orderTypeDateType.DescriptionRename = value; } }

        [FwLogicProperty(Id:"Px6pMD5fAU6m", IsReadOnly:true)]
        public string DescriptionDisplay { get; set; }

        [FwLogicProperty(Id: "mEnkjmCynL71C", IsReadOnly: true)]
        public string ActivityType { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"3fztsYVeD2A")]
        public bool? SystemType { get { return orderTypeDateType.SystemType; } set { orderTypeDateType.SystemType = value; } }

        [FwLogicProperty(Id:"r3HTv7hNewC")]
        public bool? Enabled { get { return orderTypeDateType.Enabled; } set { orderTypeDateType.Enabled = value; } }

        [FwLogicProperty(Id:"QfKELUy7uz3")]
        public bool? Milestone { get { return orderTypeDateType.Milestone; } set { orderTypeDateType.Milestone = value; } }

        [FwLogicProperty(Id:"PbEG2SkM72A")]
        public bool? ProductionActivity { get { return orderTypeDateType.ProductionActivity; } set { orderTypeDateType.ProductionActivity = value; } }

        [FwLogicProperty(Id:"FHScUmMOygz")]
        public bool? RequiredOnQuote { get { return orderTypeDateType.RequiredOnQuote; } set { orderTypeDateType.RequiredOnQuote = value; } }

        [FwLogicProperty(Id:"c4SWMKTQTAYo")]
        public bool? RequiredOnOrder { get { return orderTypeDateType.RequiredOnOrder; } set { orderTypeDateType.RequiredOnOrder = value; } }

        [FwLogicProperty(Id:"oM37jcODwW7b")]
        public string Color { get { return orderTypeDateType.Color; } set { orderTypeDateType.Color = value; } }

        [FwLogicProperty(Id: "FaVm45YAegNF4")]
        public string TextColor { get { return orderTypeDateType.TextColor; } set { orderTypeDateType.TextColor = value; } }

        [FwLogicProperty(Id:"lp6TThBq2eU3")]
        public decimal? OrderBy { get { return orderTypeDateType.OrderBy; } set { orderTypeDateType.OrderBy = value; } }

        [FwLogicProperty(Id:"J60GuLbDstHF")]
        public string DateStamp { get { return orderTypeDateType.DateStamp; } set { orderTypeDateType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
