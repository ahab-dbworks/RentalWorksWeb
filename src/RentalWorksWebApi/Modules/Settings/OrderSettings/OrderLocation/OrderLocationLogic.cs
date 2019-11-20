using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderSettings.OrderLocation
{
    [FwLogic(Id:"s1JjHpmTfUF1")]
    public class OrderLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderLocationRecord orderLocation = new OrderLocationRecord();
        OrderLocationLoader orderLocationLoader = new OrderLocationLoader();
        public OrderLocationLogic()
        {
            dataRecords.Add(orderLocation);
            dataLoader = orderLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"lv3EgzBTOZi1", IsPrimaryKey:true)]
        public string OrderLocationId { get { return orderLocation.OrderLocationId; } set { orderLocation.OrderLocationId = value; } }

        [FwLogicProperty(Id:"oDoqmEFWKPIN", IsRecordTitle:true)]
        public string Description { get { return orderLocation.Description; } set { orderLocation.Description = value; } }

        [FwLogicProperty(Id:"lv3EgzBTOZi1", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"3arTV7tsn65")]
        public string LocationId { get { return orderLocation.LocationId; } set { orderLocation.LocationId = value; } }

        [FwLogicProperty(Id:"PfRacWpAVTI")]
        public bool? Inactive { get { return orderLocation.Inactive; } set { orderLocation.Inactive = value; } }

        [FwLogicProperty(Id:"ISHfpvMAiQ2")]
        public string DateStamp { get { return orderLocation.DateStamp; } set { orderLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
