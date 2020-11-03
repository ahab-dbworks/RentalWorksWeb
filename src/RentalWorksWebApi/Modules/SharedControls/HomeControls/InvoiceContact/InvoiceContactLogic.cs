
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.HomeControls.CompanyContact;

namespace WebApi.Modules.HomeControls.InvoiceContact
{
    [FwLogic(Id:"poOecKvIbuwCF")]
    public class InvoiceContactLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceContactLoader orderContactLoader = new InvoiceContactLoader();
        public InvoiceContactLogic()
        {
            dataLoader = orderContactLoader;

            //BeforeSave += OnBeforeSave;
            //BeforeDelete += OnBeforeDelete;
            //
            //AfterSave += OnAfterSave;

            //ReloadOnSave = false;
            //ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "zZ6khfKnBZi3m", IsPrimaryKey: true)]
        public int? OrderContactId { get; set; }

        [FwLogicProperty(Id: "uyoSQXZfm20YN", IsReadOnly: true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "WmbhGUA7iUqtm", IsReadOnly: true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "55zmLP1Fid3qs", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "bo8pEBUHkHSwt", IsReadOnly: true)]
        public string ContactId { get; set; }

        [FwLogicProperty(Id:"JceDDPHp3loSZ", IsReadOnly:true)]
        public string NameFml { get; set; }

        [FwLogicProperty(Id: "8U8xdhHoZx6KF", IsReadOnly: true)]
        public string NameLfm { get; set; }

        [FwLogicProperty(Id: "kEjbm00Y2vy22", IsReadOnly: true)]
        public string Person { get; set; }

        [FwLogicProperty(Id: "c2J1K0N5Ja3qG", IsReadOnly: true)]
        public string PersonColor { get; set; }

        [FwLogicProperty(Id: "E0YqZLkMyq75d", IsReadOnly: true)]
        public string FirstName { get; set; }

        [FwLogicProperty(Id: "NvxQ5OwwgEVQI", IsReadOnly: true)]
        public string MiddleInitial { get; set; }

        [FwLogicProperty(Id: "zHd0dFVrr5Vmf", IsReadOnly: true)]
        public string LastName { get; set; }

        [FwLogicProperty(Id: "bIf5thvY30QfB", IsReadOnly: true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id: "MrXi7Wbvi3IUN", IsReadOnly: true)]
        public string OfficePhone { get; set; }

        [FwLogicProperty(Id: "KHccl0seTL9xA", IsReadOnly: true)]
        public string OfficeExtension { get; set; }

        [FwLogicProperty(Id: "mqemfJuaVEgRQ", IsReadOnly: true)]
        public string MobilePhone { get; set; }

        [FwLogicProperty(Id: "i3IgEVQoOi5OA", IsReadOnly: true)]
        public string Email { get; set; }

        [FwLogicProperty(Id: "7yxOaSKm3qwWd", IsReadOnly: true)]
        public string Pager { get; set; }

        [FwLogicProperty(Id: "w1q9Ojis2sGtN", IsReadOnly: true)]
        public string PagerPin { get; set; }

        [FwLogicProperty(Id: "CkV8KMyTDtfNB", IsReadOnly: true)]
        public string JobTitle { get; set; }

        [FwLogicProperty(Id: "D3GK5jwc59qOC", IsReadOnly: true)]
        public string ContactTitleId { get; set; }

        [FwLogicProperty(Id: "I27ZkNAnO7Ytw", IsReadOnly: true)]
        public string CompanyContactId { get; set; }

        [FwLogicProperty(Id: "0gvSLEHKScMyA", IsReadOnly: true)]
        public string CompanyId { get; set; }

        [FwLogicProperty(Id: "INOqJu45HPuKM", IsReadOnly: true)]
        public bool? IsPrimary { get; set; }

        [FwLogicProperty(Id: "YQnvFpf9P6H6W", IsReadOnly: true)]
        public string CountryId { get; set; }

        [FwLogicProperty(Id: "NZ8K3R6KktoTW", IsReadOnly: true)]
        public bool? IsOrderedBy { get; set; }

        [FwLogicProperty(Id: "HJiOmEBWGPsO4", IsReadOnly: true)]
        public bool? IsProductionContact { get; set; }

        [FwLogicProperty(Id: "L9vbALXuaZGmf", IsReadOnly: true)]
        public bool? IsPrintable { get; set; }

        [FwLogicProperty(Id: "vbYxqqj0tA1Vu", IsReadOnly: true)]
        public bool? ContactOnOrder { get; set; }

        [FwLogicProperty(Id: "sH0oQwiefi3nu", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        //{
        //    bool isValid = true;

        //    if (isValid)
        //    {
        //        if ((CompanyId == null) || (CompanyId.Equals(string.Empty)))
        //        {
        //            isValid = false;
        //            validateMsg = "CompanyId is required.";

        //        }
        //    }

        //    return isValid;
        //}
        ////------------------------------------------------------------------------------------
        //public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        //{
        //    //if CompanyContactId not provided, go get it from the database
        //    if ((CompanyContactId == null) || (CompanyContactId.Equals(string.Empty)))
        //    {
        //        string[] columns = new string[] { "compcontactid" };
        //        string[] wherecolumns = new string[] { "contactid", "companyid" };
        //        string[] wherevalues = new string[] { ContactId, CompanyId };
        //        CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "compcontact", wherecolumns, wherevalues, columns).Result[0];
        //    }

        //    //if CompanyContactId not provided or found, then create a new one
        //    if ((CompanyContactId == null) || (CompanyContactId.Equals(string.Empty)))
        //    {
        //        CompanyContactLogic l3 = new CompanyContactLogic();
        //        l3.SetDependencies(this.AppConfig, this.UserSession);
        //        l3.CompanyId = CompanyId;
        //        l3.ContactId = ContactId;
        //        int i = l3.SaveAsync(null).Result;
        //        CompanyContactId = l3.CompanyContactId; // we need this ID saved on the orderContact record
        //    }
        //}
        ////------------------------------------------------------------------------------------
        //public void OnAfterSave(object sender, AfterSaveEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ContactId))
        //    {
        //        if (MobilePhone != null)  // user is supplying a mobile phone number, or blank
        //        {
        //            ContactLogic origContact = new ContactLogic();
        //            origContact.SetDependencies(AppConfig, UserSession);
        //            origContact.ContactId = ContactId;
        //            if (origContact.LoadAsync<ContactLogic>().Result)
        //            {
        //                if (!origContact.MobilePhone.Equals(MobilePhone))
        //                {
        //                    ContactLogic modifiedContact = origContact.MakeCopy<ContactLogic>();
        //                    modifiedContact.SetDependencies(AppConfig, UserSession);
        //                    modifiedContact.MobilePhone = MobilePhone;
        //                    int i = modifiedContact.SaveAsync(original: origContact, conn: e.SqlConnection).Result;
        //                }
        //            }
        //        }
        //    }
        //}
        ////------------------------------------------------------------------------------------        
        //public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        //{
        //    //if CompanyContactId not known
        //    if (string.IsNullOrEmpty(CompanyContactId))
        //    {
        //        CompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "ordercontact", "ordercontactid", InvoiceContactId.ToString(), "compcontactid").Result;
        //    }
        //}
        ////------------------------------------------------------------------------------------
    }
}
