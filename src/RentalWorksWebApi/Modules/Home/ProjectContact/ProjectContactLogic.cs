using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Home.CompanyContact;
using WebApi.Modules.Home.Contact;
using WebApi.Modules.Home.OrderContact;

namespace WebApi.Modules.Home.ProjectContact
{
    public class ProjectContactLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderContactRecord orderContact = new OrderContactRecord();
        CompanyContactRecord companyContact = new CompanyContactRecord();
        ProjectContactLoader orderContactLoader = new ProjectContactLoader();
        public ProjectContactLogic()
        {
            dataRecords.Add(orderContact);
            dataRecords.Add(companyContact);
            dataLoader = orderContactLoader;

            BeforeSave += OnBeforeSave;
            BeforeDelete += OnBeforeDelete;

            AfterSave += OnAfterSave;

            ReloadOnSave = false;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectContactId { get { return orderContact.OrderContactId; } set { orderContact.OrderContactId = value; } }
        public string ProjectId { get { return orderContact.OrderId; } set { orderContact.OrderId = value; } }
        public string ContactId { get { return orderContact.ContactId; } set { orderContact.ContactId = value; companyContact.ContactId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NameFml { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NameLfm { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Person { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PersonColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FirstName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiddleInitial { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        public string OfficePhone { get { return companyContact.OfficePhone; } set { companyContact.OfficePhone = value; } }
        public string OfficeExtension { get { return companyContact.OfficeExtension; } set { companyContact.OfficeExtension = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MobilePhone { get; set; }
        public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } }
        public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } }
        public string PagerPin { get { return companyContact.PagerPin; } set { companyContact.PagerPin = value; } }
        public string JobTitle { get { return companyContact.JobTitle; } set { companyContact.JobTitle = value; } }
        public string ContactTitleId { get { return companyContact.ContactTitleId; } set { companyContact.ContactTitleId = value; } }
        public string CompanyContactId { get { return orderContact.CompanyContactId; } set { orderContact.CompanyContactId = value; companyContact.CompanyContactId = value; } }
        public string CompanyId { get { return orderContact.CompanyId; } set { orderContact.CompanyId = value; companyContact.CompanyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsPrimary { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CountryId { get; set; }
        public bool? IsProjectFor { get { return orderContact.IsOrderedBy; } set { orderContact.IsOrderedBy = value; } }
        public bool? IsProductionContact { get { return orderContact.IsProductionContact; } set { orderContact.IsProductionContact = value; } }
        public bool? IsPrintable { get { return orderContact.IsPrintable; } set { orderContact.IsPrintable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ContactOnProject { get; set; }
        public bool? Inactive { get { return companyContact.Inactive; } set { companyContact.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (isValid)
            {
                if ((CompanyId == null) || (CompanyId.Equals(string.Empty)))
                {
                    isValid = false;
                    validateMsg = "CompanyId is required.";

                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            //if CompanyContactId not provided, go get it from the database
            if ((CompanyContactId == null) || (CompanyContactId.Equals(string.Empty)))
            {
                string[] columns = new string[] { "compcontactid" };
                string[] wherecolumns = new string[] { "contactid", "companyid" };
                string[] wherevalues = new string[] { ContactId, CompanyId };
                CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "compcontact", wherecolumns, wherevalues, columns).Result[0];
            }

            //if CompanyContactId not provided or found, then create a new one
            if ((CompanyContactId == null) || (CompanyContactId.Equals(string.Empty)))
            {
                CompanyContactLogic l3 = new CompanyContactLogic();
                l3.SetDependencies(this.AppConfig, this.UserSession);
                l3.CompanyId = CompanyId;
                l3.ContactId = ContactId;
                int i = l3.SaveAsync(null).Result;
                CompanyContactId = l3.CompanyContactId; // we need this ID saved on the orderContact record
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if ((ContactId != null) || (!ContactId.Equals(string.Empty)))
            {
                if (MobilePhone != null)
                {
                    ContactLogic l4 = new ContactLogic();
                    l4.SetDependencies(this.AppConfig, this.UserSession);
                    l4.ContactId = ContactId;
                    l4.MobilePhone = MobilePhone;
                    int i = l4.SaveAsync(null).Result; // save the MobilePhone to the Contact
                }
            }
        }
        //------------------------------------------------------------------------------------        
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            //if CompanyContactId not known
            if ((CompanyContactId == null) || (CompanyContactId.Equals(string.Empty)))
            {
                CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "ordercontact", "ordercontactid", ProjectContactId, "compcontactid").Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
