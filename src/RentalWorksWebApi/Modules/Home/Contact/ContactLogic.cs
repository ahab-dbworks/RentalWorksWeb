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
        public string ContactId { get { return contact.contactid; } set { contact.contactid = value; } }
        public string Salutation { get { return contact.salutation; } set { contact.salutation = value; } }
        public string Address1 { get { return contact.add1; } set { contact.add2 = value; } }
        public string MiddleInitial { get { return contact.mi; } set { contact.mi = value; } }
        public string Address2 { get { return contact.add2; } set { contact.add2 = value; } }
        public string City { get { return contact.city; } set { contact.city = value; } }
        public string Zipcode { get { return contact.zip; } set { contact.zip = value; } }
        public string Phone { get { return contact.phone; } set { contact.phone = value; } }
        public string State { get { return contact.state; } set { contact.state = value; } }
        public string Fax { get { return contact.fax; } set { contact.fax = value; } }
        public string Email { get { return contact.email; } set { contact.email = value; } }
        public string OfficePhone { get { return contact.officephone; } set { contact.officephone = value; } }
        public string Pager { get { return contact.pager; } set { contact.pager = value; } }
        public string PagerPin { get { return contact.pagerpin; } set { contact.pagerpin = value; } }
        public string FaxExtension { get { return contact.faxext; } set { contact.faxext = value; } }
        public string MobilePhone { get { return contact.cellular; } set { contact.cellular = value; } }
        public string DateStamp { get { return contact.datestamp; } set { contact.datestamp = value; } }
        public string CountryId { get { return contact.countryid; } set { contact.countryid = value; } }
        public string DirectPhone { get { return contact.directphone; } set { contact.directphone = value; } }
        public string Extension { get { return contact.ext; } set { contact.ext = value; } }
        public string DirectExtension { get { return contact.directext; } set { contact.directext = value; } }
        public string Inactive { get { return contact.inactive; } set { contact.inactive = value; } }
        public string Info { get { return contact.info; } set { contact.info = value; } }
        public string Website { get { return contact.website; } set { contact.website = value; } }
        public string ActiveDate { get { return contact.activedate; } set { contact.activedate = value; } }
        public string InactiveDate { get { return contact.inactivedate; } set { contact.inactivedate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowGuid { get { return contact.rowguid; } set { contact.rowguid = value; } }
        public string InputById { get { return contact.inputbyid; } set { contact.inputbyid = value; } }
        public string ModById { get { return contact.modbyid; } set { contact.modbyid = value; } }
        public string InputDate { get { return contact.inputdate; } set { contact.inputdate = value; } }
        public string ModDate { get { return contact.moddate; } set { contact.moddate = value; } }
        public string Company { get { return contact.company; } set { contact.company = value; } }
        public string ContactRecordType { get { return contact.contactrecordtype; } set { contact.contactrecordtype = value; } }
        public string ContactTitleId { get { return contact.contacttitleid; } set { contact.contacttitleid = value; } }
        public string ContactType { get { return contact.contacttype; } set { contact.contacttype = value; } }
        public string DealId { get { return contact.dealid; } set { contact.dealid = value; } }
        public string JobTitle { get { return contact.jobtitle; } set { contact.jobtitle = value; } }
        public string LocationId { get { return contact.locationid; } set { contact.locationid = value; } }
        public string WarehouseId { get { return contact.warehouseid; } set { contact.warehouseid = value; } }
        public string WebStatus { get { return contact.webstatus; } set { contact.webstatus = value; } }
        public string WebStatusAsOf { get { return contact.webstatusasof; } set { contact.webstatusasof = value; } }
        public string WebStatusUpdateByUsersId { get { return contact.webstatusupdatebyusersid; } set { contact.webstatusupdatebyusersid = value; } }
        public string PersonType { get { return contact.persontype; } set { contact.persontype = value; } }
        public string OverridePastDue { get { return contact.overridepastdue; } set { contact.overridepastdue = value; } }
        public string FirstName { get { return contact.fname; } set { contact.fname = value; } }
        public string LastName { get { return contact.lname; } set { contact.lname = value; } }
        public string Person { get { return contact.person; } set { contact.person = value; } }
        public string NameFirstMiddleLast { get { return contact.namefml; } set { contact.namefml = value; } }
        public string Barcode { get { return contact.barcode; } set { contact.barcode = value; } }
        public string SourceId { get { return contact.sourceid; } set { contact.sourceid = value; } }
        public string WebCatalogId { get { return contact.webcatalogid; } set { contact.webcatalogid = value; } }
        public string PoOrderTypeId { get { return contact.poordertypeid; } set { contact.poordertypeid = value; } }
        public string ContactNameType { get { return contact.contactnametype; } set { contact.contactnametype = value; } }
    }

}
