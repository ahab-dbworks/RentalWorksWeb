using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.RateItem
{
    [FwLogic(Id: "PMB1gwgl1l3QF")]
    public class RateItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateItemLoader rateItemLoader = new RateItemLoader();
        public RateItemLogic()
        {
            dataLoader = rateItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mVRutZwxwl0lV", IsPrimaryKey: true)]
        public string RateId { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "K6AQoydyqXIXf")]
        public string ICode { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Y3FBmt8ld3QCX")]
        public string Description { get; set; }

        [FwLogicProperty(Id: "5JHcOn8tnZG24")]
        public string DescriptionWithAkas { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id: "D16VPiJJnMWgj")]
        public string AvailFor { get; set; }

        [FwLogicProperty(Id: "am6Z8cCYWbmxf")]
        public string TypeId { get; set; }

        [FwLogicProperty(Id: "PcmY9rt0ZrDUw")]
        public string Type { get; set; }

        [FwLogicProperty(Id: "eigCyIIYIZpYJ")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id: "rU3x05JkbL4gm")]
        public string Category { get; set; }

        [FwLogicProperty(Id: "vE15nplGjd5F1")]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id: "2MnEhNpcF7dKA")]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id: "II06gzfrX8u47")]
        public string Classification { get; set; }

        [FwLogicProperty(Id: "9rWlGrykIhn8N")]
        public string ClassificationDescription { get; set; }

        [FwLogicProperty(Id: "4BvwCAp0SRdmQ")]
        public string ClassificationColor { get; set; }

        [FwLogicProperty(Id: "M3RUAya0GGwyQ")]
        public string UnitId { get; set; }

        [FwLogicProperty(Id: "ve8Qr6DrVsclw")]
        public string Unit { get; set; }

        [FwLogicProperty(Id: "uRCPQxiUUKfUD")]
        public string UnitType { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
