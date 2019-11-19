using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.CompanyContact
{
    [FwLogic(Id:"RSZfoQ7Cc9GJ")]
    public class CompanyContactLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"iA7kCVekjh7d", IsPrimaryKey:true)]
        public string CompanyContactId { get { return companyContact.CompanyContactId; } set { companyContact.CompanyContactId = value; } }

        [FwLogicProperty(Id:"wxoxSQ3otRdv")]
        public string CompanyId { get { return companyContact.CompanyId; } set { companyContact.CompanyId = value; } }

        [FwLogicProperty(Id:"iA7kCVekjh7d", IsReadOnly:true)]
        public string Company { get; set; }

        [FwLogicProperty(Id:"iA7kCVekjh7d", IsReadOnly:true)]
        public string CompanyType { get; set; }

        [FwLogicProperty(Id:"iA7kCVekjh7d", IsReadOnly:true)]
        public string CompanyTypeColor { get; set; }

        [FwLogicProperty(Id:"06QTa8cdW3Cx")]
        public string ContactId { get { return companyContact.ContactId; } set { companyContact.ContactId = value; } }

        [FwLogicProperty(Id:"xSJKecl7rJYV", IsReadOnly:true)]
        public string Salutation { get; set; }

        [FwLogicProperty(Id:"WyekE0uMtpxt", IsReadOnly:true)]
        public string NameFirstMiddleLast { get; set; }

        [FwLogicProperty(Id:"ZbiMvMkr3QaQ", IsReadOnly:true)]
        public string Person { get; set; }

        [FwLogicProperty(Id:"awc2nJQ2IQsv", IsReadOnly:true)]
        public string LastName { get; set; }

        [FwLogicProperty(Id:"G7lt1RaNFmRM", IsReadOnly:true)]
        public string FirstName { get; set; }

        [FwLogicProperty(Id:"1cTu6UbAdRAM", IsReadOnly:true)]
        public string MiddleInitial { get; set; }

        [FwLogicProperty(Id:"wlxRI3096PGV")]
        public string JobTitle { get { return companyContact.JobTitle; } set { companyContact.JobTitle = value; } }

        [FwLogicProperty(Id:"56yD4EhYhHJA")]
        public string ContactTitleId { get { return companyContact.ContactTitleId; } set { companyContact.ContactTitleId = value; } }

        [FwLogicProperty(Id:"1chrSiRJG14g", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"dlChFMcdpjvZ")]
        public bool? IsPrimary { get { return companyContact.IsPrimary; } set { companyContact.IsPrimary = value; } }

        [FwLogicProperty(Id:"4BfClXiJgenS")]
        public string ActiveDate { get { return companyContact.ActiveDate; } set { companyContact.ActiveDate = value; } }

        [FwLogicProperty(Id:"XEQme4ovJUsz")]
        public string InactiveDate { get { return companyContact.InactiveDate; } set { companyContact.InactiveDate = value; } }

        [FwLogicProperty(Id:"RJCN2sKxgGod")]
        public bool? Authorized { get { return companyContact.Authorized; } set { companyContact.Authorized = value; } }

        [FwLogicProperty(Id:"G3r28RrKqWOX")]
        public bool? OrderNotify { get { return companyContact.OrderNotify; } set { companyContact.OrderNotify = value; } }

        [FwLogicProperty(Id:"NC8rlkYQi5XF")]
        public string OfficePhone { get { return companyContact.OfficePhone; } set { companyContact.OfficePhone = value; } }

        [FwLogicProperty(Id:"jOSWZMyZJZh3")]
        public string OfficeExtension { get { return companyContact.OfficeExtension; } set { companyContact.OfficeExtension = value; } }

        [FwLogicProperty(Id:"JLz5bk8jX3Mp")]
        public string Fax { get { return companyContact.Fax; } set { companyContact.Fax = value; } }

        [FwLogicProperty(Id:"2hZWwfOfh0ke")]
        public string FaxExtension { get { return companyContact.FaxExtension; } set { companyContact.FaxExtension = value; } }

        [FwLogicProperty(Id:"gjWW7DUDEdy7")]
        public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } }

        [FwLogicProperty(Id:"Am7ZRHgRvkTn")]
        public string DirectPhone { get { return companyContact.DirectPhone; } set { companyContact.DirectPhone = value; } }

        [FwLogicProperty(Id:"Qm04pNnYX8tb")]
        public string DirectExtension { get { return companyContact.DirectExtension; } set { companyContact.DirectExtension = value; } }

        [FwLogicProperty(Id:"5TgWCTqjJEFx")]
        public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } }

        [FwLogicProperty(Id:"50xuvqs1JpmD")]
        public string PagerPin { get { return companyContact.PagerPin; } set { companyContact.PagerPin = value; } }

        [FwLogicProperty(Id:"42K1dRkCYKXN")]
        public bool? Printable { get { return companyContact.Printable; } set { companyContact.Printable = value; } }

        [FwLogicProperty(Id:"ymG4mLA7xEF1")]
        public string MobilePhone { get { return companyContact.MobilePhone; } set { companyContact.MobilePhone = value; } }

        [FwLogicProperty(Id:"BPY1HoI4XOKv")]
        public bool? Inactive { get { return companyContact.Inactive; } set { companyContact.Inactive = value; } }

        [FwLogicProperty(Id:"yl8KHY36Km3r")]
        public string DateStamp { get { return companyContact.DateStamp; } set { companyContact.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
