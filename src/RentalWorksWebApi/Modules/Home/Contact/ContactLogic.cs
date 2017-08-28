using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Home.Contact
{
    public class ContactLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactRecord contact = new ContactRecord();
        ContactLoader contactLoader = new ContactLoader();
        public ContactLogic()
        {
            dataRecords.Add(contact);
            dataLoader = contactLoader;
        }
        //------------------------------------------------------------------------------------
	    [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContactId { get { return contact.ContactId; } set { contact.ContactId = value; } }
        public string Salutation { get { return contact.Salutation; } set { contact.Salutation = value; } }
        public string Address1 { get { return contact.Address1; } set { contact.Address2 = value; } }
        public string MiddleInitial { get { return contact.MiddleInitial; } set { contact.MiddleInitial = value; } }
        public string Address2 { get { return contact.Address2; } set { contact.Address2 = value; } }
        public string City { get { return contact.City; } set { contact.City = value; } }
        public string Zipcode { get { return contact.Zipcode; } set { contact.Zipcode = value; } }
        public string Phone { get { return contact.Phone; } set { contact.Phone = value; } }
        public string State { get { return contact.State; } set { contact.State = value; } }
        public string Fax { get { return contact.Fax; } set { contact.Fax = value; } }
        public string Email { get { return contact.Email; } set { contact.Email = value; } }
        public string OfficePhone { get { return contact.OfficePhone; } set { contact.OfficePhone = value; } }
        public string Pager { get { return contact.Pager; } set { contact.Pager = value; } }
        public string PagerPin { get { return contact.PagerPin; } set { contact.PagerPin = value; } }
        public string FaxExtension { get { return contact.FaxExtension; } set { contact.FaxExtension = value; } }
        public string MobilePhone { get { return contact.MobilePhone; } set { contact.MobilePhone = value; } }
        public string DateStamp { get { return contact.DateStamp; } set { contact.DateStamp = value; } }
        public string CountryId { get { return contact.CountryId; } set { contact.CountryId = value; } }
        public string DirectPhone { get { return contact.DirectPhone; } set { contact.DirectPhone = value; } }
        public string Extension { get { return contact.Extension; } set { contact.Extension = value; } }
        public string DirectExtension { get { return contact.DirectExtension; } set { contact.DirectExtension = value; } }
        public string Inactive { get { return contact.Inactive; } set { contact.Inactive = value; } }
        public string Info { get { return contact.Info; } set { contact.Info = value; } }
        public string Website { get { return contact.Website; } set { contact.Website = value; } }
        public string ActiveDate { get { return contact.ActiveDate; } set { contact.ActiveDate = value; } }
        public string InactiveDate { get { return contact.InactiveDate; } set { contact.InactiveDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowGuid { get { return contact.RowGuid; } set { contact.RowGuid = value; } }
        public string InputById { get { return contact.InputById; } set { contact.InputById = value; } }
        public string ModById { get { return contact.ModById; } set { contact.ModById = value; } }
        public string InputDate { get { return contact.InputDate; } set { contact.InputDate = value; } }
        public string ModDate { get { return contact.ModDate; } set { contact.ModDate = value; } }
        public string Company { get { return contact.Company; } set { contact.Company = value; } }
        public string ContactRecordType { get { return contact.ContactRecordType; } set { contact.ContactRecordType = value; } }
        public string ContactTitleId { get { return contact.ContactTitleId; } set { contact.ContactTitleId = value; } }
        public string ContactType { get { return contact.ContactType; } set { contact.ContactType = value; } }
        public string DealId { get { return contact.DealId; } set { contact.DealId = value; } }
        public string JobTitle { get { return contact.JobTitle; } set { contact.JobTitle = value; } }
        public string LocationId { get { return contact.LocationId; } set { contact.LocationId = value; } }
        public string WarehouseId { get { return contact.WarehouseId; } set { contact.WarehouseId = value; } }
        public string WebStatus { get { return contact.WebStatus; } set { contact.WebStatus = value; } }
        public string WebStatusAsOf { get { return contact.WebStatusAsOf; } set { contact.WebStatusAsOf = value; } }
        public string WebStatusUpdateByUsersId { get { return contact.WebStatusUpdateByUsersId; } set { contact.WebStatusUpdateByUsersId = value; } }
        public string PersonType { get { return contact.PersonType; } set { contact.PersonType = value; } }
        public string OverridePastDue { get { return contact.OverridePastDue; } set { contact.OverridePastDue = value; } }
        public string FirstName { get { return contact.FirstName; } set { contact.FirstName = value; } }
        public string LastName { get { return contact.LastName; } set { contact.LastName = value; } }
        public string Person { get { return contact.Person; } set { contact.Person = value; } }
        public string NameFirstMiddleLast { get { return contact.NameFirstMiddleLast; } set { contact.NameFirstMiddleLast = value; } }
        public string Barcode { get { return contact.Barcode; } set { contact.Barcode = value; } }
        public string SourceId { get { return contact.SourceId; } set { contact.SourceId = value; } }
        public string WebCatalogId { get { return contact.WebCatalogId; } set { contact.WebCatalogId = value; } }
        public string PoOrderTypeId { get { return contact.PoOrderTypeId; } set { contact.PoOrderTypeId = value; } }
        public string ContactNameType { get { return contact.ContactNameType; } set { contact.ContactNameType = value; } }
    }

}
