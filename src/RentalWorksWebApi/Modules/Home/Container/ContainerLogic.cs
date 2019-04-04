using WebApi.Logic;
using FwStandard.AppManager;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebLibrary;
using Newtonsoft.Json;

namespace WebApi.Modules.Home.Container
{
    [FwLogic(Id: "xxxxxxxxxxxx")]
    public class ContainerLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord container = new DealOrderRecord();

        ContainerLoader containerLoader = new ContainerLoader();
        public ContainerLogic()
        {
            dataRecords.Add(container);
            dataLoader = containerLoader;

            Type = RwConstants.ORDER_TYPE_CONTAINER;
            Rental = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "xxxxxxxxx", IsPrimaryKey: true)]
        public string ContainerId { get { return container.OrderId; } set { container.OrderId = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "xxxxxxxxxxxxx")]
        public string Type { get { return container.Type; } set { container.Type = value; } }

        [FwLogicProperty(Id: "xxxxxxxxx", IsRecordTitle: true)]
        public string Description { get { return container.Description; } set { container.Description = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxx")]
        public bool? Rental { get { return container.Rental; } set { container.Rental = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxxx")]
        public string ScannableInventoryId { get { return container.ScannableInventoryId; } set { container.ScannableInventoryId = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxxx", IsReadOnly: true)]
        public string ScannableICode { get; set; }

        [FwLogicProperty(Id: "xxxxxxxxxxx")]
        public string DateStamp { get { return container.DateStamp; } set { container.DateStamp = value; container.DateStamp = value; } }
        //------------------------------------------------------------------------------------



    }
}
