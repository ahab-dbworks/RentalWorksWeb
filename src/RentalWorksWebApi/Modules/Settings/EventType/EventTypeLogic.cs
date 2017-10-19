using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.OrderType;

namespace RentalWorksWebApi.Modules.Settings.EventType
{
    public class EventTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord eventType = new OrderTypeRecord();
        EventTypeLoader eventTypeLoader = new EventTypeLoader();
        public EventTypeLogic()
        {
            dataRecords.Add(eventType);
            dataLoader = eventTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string EventTypeId { get { return eventType.OrderTypeId; } set { eventType.OrderTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string EventType { get { return eventType.OrderType; } set { eventType.OrderType = value; } }


        public bool SalesInventoryPrice { get { return eventType.Selectsalesprice; } set { eventType.Selectsalesprice = value; } }
        public bool SalesInventoryCost { get { return eventType.Selectsalescost; } set { eventType.Selectsalescost = value; } }
        public string FacilityDescription { get { return eventType.Spacedescription; } set { eventType.Spacedescription = value; } }

        public bool HideCrewBreaks { get { return eventType.Hidecrewbreaks; } set { eventType.Hidecrewbreaks = value; } }
        public bool Break1Paid { get { return eventType.Break1paId; } set { eventType.Break1paId = value; } }
        public bool Break2Paid { get { return eventType.Break2paId; } set { eventType.Break2paId = value; } }
        public bool Break3Paid { get { return eventType.Break3paId; } set { eventType.Break3paId = value; } }


        //public string RentalordertypefieldsId { get { return poType.RentalordertypefieldsId; } set { poType.RentalordertypefieldsId = value; } }
        //public string SalesordertypefieldsId { get { return poType.SalesordertypefieldsId; } set { poType.SalesordertypefieldsId = value; } }
        //public string SpaceordertypefieldsId { get { return poType.SpaceordertypefieldsId; } set { poType.SpaceordertypefieldsId = value; } }
        //public string SubrentalordertypefieldsId { get { return poType.SubrentalordertypefieldsId; } set { poType.SubrentalordertypefieldsId = value; } }
        //public string SubsalesordertypefieldsId { get { return poType.SubsalesordertypefieldsId; } set { poType.SubsalesordertypefieldsId = value; } }
        //public string PurchaseordertypefieldsId { get { return poType.PurchaseordertypefieldsId; } set { poType.PurchaseordertypefieldsId = value; } }
        //public string LaborordertypefieldsId { get { return poType.LaborordertypefieldsId; } set { poType.LaborordertypefieldsId = value; } }
        //public string SublaborordertypefieldsId { get { return poType.SublaborordertypefieldsId; } set { poType.SublaborordertypefieldsId = value; } }
        //public string MiscordertypefieldsId { get { return poType.MiscordertypefieldsId; } set { poType.MiscordertypefieldsId = value; } }
        //public string SubmiscordertypefieldsId { get { return poType.SubmiscordertypefieldsId; } set { poType.SubmiscordertypefieldsId = value; } }
        //public string RepairordertypefieldsId { get { return poType.RepairordertypefieldsId; } set { poType.RepairordertypefieldsId = value; } }
        //public string VehicleordertypefieldsId { get { return poType.VehicleordertypefieldsId; } set { poType.VehicleordertypefieldsId = value; } }
        //public string RentalsaleordertypefieldsId { get { return poType.RentalsaleordertypefieldsId; } set { poType.RentalsaleordertypefieldsId = value; } }
        //public string LdordertypefieldsId { get { return poType.LdordertypefieldsId; } set { poType.LdordertypefieldsId = value; } }


        [JsonIgnore]
        public string OrdType { get { return eventType.Ordtype; } set { eventType.Ordtype = value; } }
        public decimal? OrderBy { get { return eventType.Orderby; } set { eventType.Orderby = value; } }
        public bool Inactive { get { return eventType.Inactive; } set { eventType.Inactive = value; } }
        public string DateStamp { get { return eventType.DateStamp; } set { eventType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            OrdType = "EVENT";
        }
        //------------------------------------------------------------------------------------ 
    }
}