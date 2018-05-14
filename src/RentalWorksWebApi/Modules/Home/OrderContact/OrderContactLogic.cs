using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderContact
{
    public class OrderContactLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderContactRecord orderContact = new OrderContactRecord();
        OrderContactLoader orderContactLoader = new OrderContactLoader();
        public OrderContactLogic()
        {
            dataRecords.Add(orderContact);
            dataLoader = orderContactLoader;
        }
        //------------------------------------------------------------------------------------ 

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return orderContact.OrderId; } set { orderContact.OrderId = value; } }

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContactId { get { return orderContact.ContactId; } set { orderContact.ContactId = value; } }

        [FwBusinessLogicField(isReadOnly: true)]
        public string NameFml { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NameLfm { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FirstName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiddleInitial { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficePhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Extension { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Cellular { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Email { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pager { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PagerPin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string JobTitle { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitleId { get; set; }
        public string CompanyContactId { get { return orderContact.CompanyContactId; } set { orderContact.CompanyContactId = value; } }
        public string CompanyId { get { return orderContact.CompanyId; } set { orderContact.CompanyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsPrimary { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CountryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsOrderedBy { get; set; }
        public bool? IsProductionContact { get { return orderContact.IsProductionContact; } set { orderContact.IsProductionContact = value; } }
        public bool? IsPrintable { get { return orderContact.IsPrintable; } set { orderContact.IsPrintable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ContactOnOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
