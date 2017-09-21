using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.OrderLocation
{
    public class OrderLocationLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderLocationId { get { return orderLocation.OrderLocationId; } set { orderLocation.OrderLocationId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return orderLocation.Description; } set { orderLocation.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string LocationId { get { return orderLocation.LocationId; } set { orderLocation.LocationId = value; } }
        public bool Inactive { get { return orderLocation.Inactive; } set { orderLocation.Inactive = value; } }
        public string DateStamp { get { return orderLocation.DateStamp; } set { orderLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}