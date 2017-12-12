using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeDateType
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeDateTypeId { get { return orderTypeDateType.OrderTypeDateTypeId; } set { orderTypeDateType.OrderTypeDateTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderTypeId { get { return orderTypeDateType.OrderTypeId; } set { orderTypeDateType.OrderTypeId = value; } }
        public string OrderType { get; set; }
        public string Description { get { return orderTypeDateType.Description; } set { orderTypeDateType.Description = value; } }
        public string DescriptionRename { get { return orderTypeDateType.DescriptionRename; } set { orderTypeDateType.DescriptionRename = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionDisplay { get; set; }
        [JsonIgnore]
        public bool? SystemType { get { return orderTypeDateType.SystemType; } set { orderTypeDateType.SystemType = value; } }
        public bool? Enabled { get { return orderTypeDateType.Enabled; } set { orderTypeDateType.Enabled = value; } }
        public bool? Milestone { get { return orderTypeDateType.Milestone; } set { orderTypeDateType.Milestone = value; } }
        public bool? ProductionActivity { get { return orderTypeDateType.ProductionActivity; } set { orderTypeDateType.ProductionActivity = value; } }
        public bool? RequiredOnQuote { get { return orderTypeDateType.RequiredOnQuote; } set { orderTypeDateType.RequiredOnQuote = value; } }
        public bool? RequiredOnOrder { get { return orderTypeDateType.RequiredOnOrder; } set { orderTypeDateType.RequiredOnOrder = value; } }
        public int? Color { get { return orderTypeDateType.Color; } set { orderTypeDateType.Color = value; } }
        public decimal? OrderBy { get { return orderTypeDateType.OrderBy; } set { orderTypeDateType.OrderBy = value; } }
        public string DateStamp { get { return orderTypeDateType.DateStamp; } set { orderTypeDateType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}