using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.HomeControls.CompanyContact;

namespace WebApi.Modules.HomeControls.OrderContact
{
    [FwLogic(Id:"WtDwr1IbNTRF")]
    public class OrderContactLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderContactRecord orderContact = new OrderContactRecord();
        CompanyContactRecord companyContact = new CompanyContactRecord();
        OrderContactLoader orderContactLoader = new OrderContactLoader();
        public OrderContactLogic()
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
        [FwLogicProperty(Id:"fTd0q2QfSild", IsPrimaryKey:true)]
        public int? OrderContactId { get { return orderContact.OrderContactId; } set { orderContact.OrderContactId = value; } }

        [FwLogicProperty(Id:"uPwx0eLFLp5n")]
        public string OrderId { get { return orderContact.OrderId; } set { orderContact.OrderId = value; } }

        [FwLogicProperty(Id:"puHqtoi3cUij")]
        public string ContactId { get { return orderContact.ContactId; } set { orderContact.ContactId = value; companyContact.ContactId = value; } }

        [FwLogicProperty(Id:"2wY1J3oobLfy", IsReadOnly:true)]
        public string NameFml { get; set; }

        [FwLogicProperty(Id:"VfLCgWs7r0ap", IsReadOnly:true)]
        public string NameLfm { get; set; }

        [FwLogicProperty(Id:"Ni0pEkwFvjva", IsReadOnly:true)]
        public string Person { get; set; }

        [FwLogicProperty(Id:"Ni0pEkwFvjva", IsReadOnly:true)]
        public string PersonColor { get; set; }

        [FwLogicProperty(Id:"HVkp6Hg37NaD", IsReadOnly:true)]
        public string FirstName { get; set; }

        [FwLogicProperty(Id:"GMh4Hg5SSApq", IsReadOnly:true)]
        public string MiddleInitial { get; set; }

        [FwLogicProperty(Id:"4SYuVDEB54PW", IsReadOnly:true)]
        public string LastName { get; set; }

        [FwLogicProperty(Id:"vSbzuTj8eH8L", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"9GViUNrOcBzb")]
        public string OfficePhone { get { return companyContact.OfficePhone; } set { companyContact.OfficePhone = value; } }

        [FwLogicProperty(Id:"jgT2ggh7SoJx")]
        public string OfficeExtension { get { return companyContact.OfficeExtension; } set { companyContact.OfficeExtension = value; } }

        [FwLogicProperty(Id:"Ft6XnJJU02zu", IsReadOnly:true)]
        public string MobilePhone { get; set; }

        [FwLogicProperty(Id:"xIjazwTSAQGy")]
        public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } }

        [FwLogicProperty(Id:"3AuTiuF6doqh")]
        public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } }

        [FwLogicProperty(Id:"RmEVddZXKpNX")]
        public string PagerPin { get { return companyContact.PagerPin; } set { companyContact.PagerPin = value; } }

        [FwLogicProperty(Id:"DAMzyFirUtX0")]
        public string JobTitle { get { return companyContact.JobTitle; } set { companyContact.JobTitle = value; } }

        [FwLogicProperty(Id:"I5vTBz4Xmg3h")]
        public string ContactTitleId { get { return companyContact.ContactTitleId; } set { companyContact.ContactTitleId = value; } }

        [FwLogicProperty(Id:"XvFhx0B7j5Vz")]
        public string CompanyContactId { get { return orderContact.CompanyContactId; } set { orderContact.CompanyContactId = value; companyContact.CompanyContactId = value; } }

        [FwLogicProperty(Id:"kzCqQFzAkb2T")]
        public string CompanyId { get { return orderContact.CompanyId; } set { orderContact.CompanyId = value; companyContact.CompanyId = value; } }

        [FwLogicProperty(Id:"mZ2FD66LZwab", IsReadOnly:true)]
        public bool? IsPrimary { get; set; }

        [FwLogicProperty(Id:"iAxcopiF0pTG", IsReadOnly:true)]
        public string CountryId { get; set; }

        [FwLogicProperty(Id:"AsKl4tk7iFZ7")]
        public bool? IsOrderedBy { get { return orderContact.IsOrderedBy; } set { orderContact.IsOrderedBy = value; } }

        [FwLogicProperty(Id:"hJ3pZWm8kXoX")]
        public bool? IsProductionContact { get { return orderContact.IsProductionContact; } set { orderContact.IsProductionContact = value; } }

        [FwLogicProperty(Id:"tWJXc9P75ABA")]
        public bool? IsPrintable { get { return orderContact.IsPrintable; } set { orderContact.IsPrintable = value; } }

        [FwLogicProperty(Id:"agdjDWDiZDW2", IsReadOnly:true)]
        public bool? ContactOnOrder { get; set; }

        [FwLogicProperty(Id:"iauqIr8Ph8s9")]
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
                CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "ordercontact", "ordercontactid", OrderContactId.ToString(), "compcontactid").Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
