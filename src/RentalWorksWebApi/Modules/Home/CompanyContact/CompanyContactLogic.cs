using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.CompanyContact
{
    public class CompanyContactLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CompanyContactRecord companyContact = new CompanyContactRecord();
        CompanyContactLoader companyContactLoader = new CompanyContactLoader();
        public CompanyContactLogic()
        {
            dataRecords.Add(companyContact);
            dataLoader = companyContactLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CompanyContactId { get { return companyContact.CompanyContactId; } set { companyContact.CompanyContactId = value; } }
        public string CompanyId { get { return companyContact.CompanyId; } set { companyContact.CompanyId = value; } }
        public string ContactId { get { return companyContact.ContactId; } set { companyContact.ContactId = value; } }
        public string JobTitle { get { return companyContact.JobTitle; } set { companyContact.JobTitle = value; } }
        public string ContactTitleId { get { return companyContact.ContactTitleId; } set { companyContact.ContactTitleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        public bool IsPrimary { get { return companyContact.IsPrimary; } set { companyContact.IsPrimary = value; } }
        public string ActiveDate { get { return companyContact.ActiveDate; } set { companyContact.ActiveDate = value; } }
        public string InactiveDate { get { return companyContact.InactiveDate; } set { companyContact.InactiveDate = value; } }
        public bool Authorized { get { return companyContact.Authorized; } set { companyContact.Authorized = value; } }
        public bool OrderNotify { get { return companyContact.OrderNotify; } set { companyContact.OrderNotify = value; } }
        public string OfficePhone { get { return companyContact.OfficePhone; } set { companyContact.OfficePhone = value; } }
        public string OfficeExtension { get { return companyContact.OfficeExtension; } set { companyContact.OfficeExtension = value; } }
        public string Fax { get { return companyContact.Fax; } set { companyContact.Fax = value; } }
        public string FaxExtension { get { return companyContact.FaxExtension; } set { companyContact.FaxExtension = value; } }
        public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } }
        public string DirectPhone { get { return companyContact.DirectPhone; } set { companyContact.DirectPhone = value; } }
        public string DirectExtension { get { return companyContact.DirectExtension; } set { companyContact.DirectExtension = value; } }
        public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } }
        public string PagerPin { get { return companyContact.PagerPin; } set { companyContact.PagerPin = value; } }
        public bool Printable { get { return companyContact.Printable; } set { companyContact.Printable = value; } }
        public string MobilePhone { get { return companyContact.MobilePhone; } set { companyContact.MobilePhone = value; } }
        public bool Inactive { get { return companyContact.Inactive; } set { companyContact.Inactive = value; } }
        public string DateStamp { get { return companyContact.DateStamp; } set { companyContact.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}