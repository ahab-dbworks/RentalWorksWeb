using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.HomeControls.CompanyContact;
using WebApi.Modules.HomeControls.OrderContact;

namespace WebApi.Modules.HomeControls.ProjectContact
{
    [FwLogic(Id:"3iujjJFfwstQF")]
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
        [FwLogicProperty(Id:"bGMsfzHSP2noo", IsPrimaryKey:true)]
        public int? ProjectContactId { get { return orderContact.OrderContactId; } set { orderContact.OrderContactId = value; } }

        [FwLogicProperty(Id:"0ueiGQICc1zm")]
        public string ProjectId { get { return orderContact.OrderId; } set { orderContact.OrderId = value; } }

        [FwLogicProperty(Id:"FdyL3FRvJ5V5")]
        public string ContactId { get { return orderContact.ContactId; } set { orderContact.ContactId = value; companyContact.ContactId = value; } }

        [FwLogicProperty(Id:"myUDcfainCX7o", IsReadOnly:true)]
        public string NameFml { get; set; }

        [FwLogicProperty(Id:"3uISd9iwUqVVm", IsReadOnly:true)]
        public string NameLfm { get; set; }

        [FwLogicProperty(Id:"Vr1BVusVphvCw", IsReadOnly:true)]
        public string Person { get; set; }

        [FwLogicProperty(Id:"Vr1BVusVphvCw", IsReadOnly:true)]
        public string PersonColor { get; set; }

        [FwLogicProperty(Id:"GSwMpD68CFIeq", IsReadOnly:true)]
        public string FirstName { get; set; }

        [FwLogicProperty(Id:"pUlgsBvtaRmpH", IsReadOnly:true)]
        public string MiddleInitial { get; set; }

        [FwLogicProperty(Id:"Go5uliw3mRtYe", IsReadOnly:true)]
        public string LastName { get; set; }

        [FwLogicProperty(Id:"G4OAaHHxylifD", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"KATJXQ03iIkg")]
        public string OfficePhone { get { return companyContact.OfficePhone; } set { companyContact.OfficePhone = value; } }

        [FwLogicProperty(Id:"SxHCRENIZxze")]
        public string OfficeExtension { get { return companyContact.OfficeExtension; } set { companyContact.OfficeExtension = value; } }

        [FwLogicProperty(Id:"2BNwxq4QefHTK", IsReadOnly:true)]
        public string MobilePhone { get; set; }

        [FwLogicProperty(Id:"qtLQzdfp8IBA")]
        public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } }

        [FwLogicProperty(Id:"ijKqwzICz1Od")]
        public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } }

        [FwLogicProperty(Id:"RpmB1unmUrI7")]
        public string PagerPin { get { return companyContact.PagerPin; } set { companyContact.PagerPin = value; } }

        [FwLogicProperty(Id:"h7dr0mRUuKnR")]
        public string JobTitle { get { return companyContact.JobTitle; } set { companyContact.JobTitle = value; } }

        [FwLogicProperty(Id:"Mq8kKwWySJ6f")]
        public string ContactTitleId { get { return companyContact.ContactTitleId; } set { companyContact.ContactTitleId = value; } }

        [FwLogicProperty(Id:"9DSo8zpzwoqM")]
        public string CompanyContactId { get { return orderContact.CompanyContactId; } set { orderContact.CompanyContactId = value; companyContact.CompanyContactId = value; } }

        [FwLogicProperty(Id:"AwEVgVA8UBtl")]
        public string CompanyId { get { return orderContact.CompanyId; } set { orderContact.CompanyId = value; companyContact.CompanyId = value; } }

        [FwLogicProperty(Id:"nBtAEQNGWPYVR", IsReadOnly:true)]
        public bool? IsPrimary { get; set; }

        [FwLogicProperty(Id:"7kWTpPDmV8QG5", IsReadOnly:true)]
        public string CountryId { get; set; }

        [FwLogicProperty(Id:"TKP4RJhhL1vV")]
        public bool? IsProjectFor { get { return orderContact.IsOrderedBy; } set { orderContact.IsOrderedBy = value; } }

        [FwLogicProperty(Id:"b2fEtnsxVLzx")]
        public bool? IsProductionContact { get { return orderContact.IsProductionContact; } set { orderContact.IsProductionContact = value; } }

        [FwLogicProperty(Id:"cqpuL3WRIB5K")]
        public bool? IsPrintable { get { return orderContact.IsPrintable; } set { orderContact.IsPrintable = value; } }

        [FwLogicProperty(Id:"lmIC1e8l8MmkO", IsReadOnly:true)]
        public bool? ContactOnProject { get; set; }

        [FwLogicProperty(Id:"XIMQq778DvPQ")]
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
            if (string.IsNullOrEmpty(CompanyContactId))
            {
                CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "ordercontact", "ordercontactid", ProjectContactId.ToString(), "compcontactid").Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
