using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebLibrary;

namespace WebApi.Modules.Home.Container
{
    [FwLogic(Id: "qAUJdNPdSkY")]
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
        [FwLogicProperty(Id: "Y2jouyImAxg", IsPrimaryKey: true)]
        public string ContainerId { get { return container.OrderId; } set { container.OrderId = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "8V9JG08Gjem")]
        public string Type { get { return container.Type; } set { container.Type = value; } }

        [FwLogicProperty(Id: "ffbePi5G9uE", IsRecordTitle: true)]
        public string Description { get { return container.Description; } set { container.Description = value; } }

        [FwLogicProperty(Id: "hCtO56IR4Z8")]
        public bool? Rental { get { return container.Rental; } set { container.Rental = value; } }

        [FwLogicProperty(Id: "Q8hbK6XYiAL")]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "LfeuHr6vWzY")]
        public string ScannableInventoryId { get { return container.ScannableInventoryId; } set { container.ScannableInventoryId = value; } }

        [FwLogicProperty(Id: "wjtSnr6y6Ch", IsReadOnly: true)]
        public string ScannableICode { get; set; }

        [FwLogicProperty(Id: "IvjngsRpzu8")]
        public string DateStamp { get { return container.DateStamp; } set { container.DateStamp = value; } }
        //------------------------------------------------------------------------------------



    }
}
