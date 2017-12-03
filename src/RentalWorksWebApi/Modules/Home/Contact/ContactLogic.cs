using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Administrator.User;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace RentalWorksWebApi.Modules.Home.Contact
{
    public class ContactLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactRecord contact = new ContactRecord();
        WebUserRecord webUser = new WebUserRecord();
        ContactLoader contactLoader = new ContactLoader();
        public ContactLogic()
        {
            dataRecords.Add(contact);
            dataRecords.Add(webUser);
            dataLoader = contactLoader;
            contact.AfterSaves += Contact_AfterSaves;
            webUser.AfterSaves += WebUser_AfterSaves;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContactId { get { return contact.ContactId; } set { contact.ContactId = value; } }
        public string ActiveDate { get { return contact.ActiveDate; } set { contact.ActiveDate = value; } }
        public string Address1 { get { return contact.Address1; } set { contact.Address1 = value; } }
        public string Address2 { get { return contact.Address2; } set { contact.Address2 = value; } }
        public string Barcode { get { return contact.Barcode; } set { contact.Barcode = value; } }
        public string City { get { return contact.City; } set { contact.City = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyContactId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyId { get; set; }
        public string Company { get { return contact.Company; } set { contact.Company = value; } }
        public string ContactNameType { get { return contact.ContactNameType; } set { contact.ContactNameType = value; } }
        public string ContactRecordType { get { return contact.ContactRecordType; } set { contact.ContactRecordType = value; } }
         [FwBusinessLogicField(isReadOnly: true)]
        public string ContactRecordTypeColor { get; set; }
        public string ContactTitleId { get { return contact.ContactTitleId; } set { contact.ContactTitleId = value; } }
         [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        public string ContactType { get { return contact.ContactType; } set { contact.ContactType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string CountryId { get { return contact.CountryId; } set { contact.CountryId = value; } }
        public string DateStamp { get { return contact.DateStamp; } set { contact.DateStamp = value; } }
        public string DealId { get { return contact.DealId; } set { contact.DealId = value; } }
        public string DirectExtension { get { return contact.DirectExtension; } set { contact.DirectExtension = value; } }
        public string DirectPhone { get { return contact.DirectPhone; } set { contact.DirectPhone = value; } }
        public string Email { get { return contact.Email; } set { contact.Email = value; } }
        public string Fax { get { return contact.Fax; } set { contact.Fax = value; } }
        public string FaxExtension { get { return contact.FaxExtension; } set { contact.FaxExtension = value; } }
        public string FirstName { get { return contact.FirstName; } set { contact.FirstName = value; } }
        public string HomePhone { get { return contact.HomePhone; } set { contact.HomePhone = value; } }
        public bool? Inactive { get { return contact.Inactive; } set { contact.Inactive = value; } }
        public string InactiveDate { get { return contact.InactiveDate; } set { contact.InactiveDate = value; } }
        public string Info { get { return contact.Info; } set { contact.Info = value; } }
        public string InputById { get { return contact.InputById; } set { contact.InputById = value; } }
        public string InputDate { get { return contact.InputDate; } set { contact.InputDate = value; } }
        public string JobTitle { get { return contact.JobTitle; } set { contact.JobTitle = value; } }
        public string LastModifiedByUserId { get { return contact.LastModifiedByUserId; } set { contact.LastModifiedByUserId = value; } }
        public string LastModifiedDate { get { return contact.LastModifiedDate; } set { contact.LastModifiedDate = value; } }
        public string LastName { get { return contact.LastName; } set { contact.LastName = value; } }
        public string LocationId { get { return contact.LocationId; } set { contact.LocationId = value; } }
        public string MiddleInitial { get { return contact.MiddleInitial; } set { contact.MiddleInitial = value; } }
        public string MobilePhone { get { return contact.MobilePhone; } set { contact.MobilePhone = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string NameFirstMiddleLast { get { return contact.NameFirstMiddleLast; } set { contact.NameFirstMiddleLast = value; } }
        public string OfficeExtension { get { return contact.OfficeExtension; } set { contact.OfficeExtension = value; } }
        public string OfficePhone { get { return contact.OfficePhone; } set { contact.OfficePhone = value; } }
        public string OverridePastDue { get { return contact.OverridePastDue; } set { contact.OverridePastDue = value; } }
        public string Pager { get { return contact.Pager; } set { contact.Pager = value; } }
        public string PagerPin { get { return contact.PagerPin; } set { contact.PagerPin = value; } }
        public string Person { get { return contact.Person; } set { contact.Person = value; } }
        public string PersonType { get { return contact.PersonType; } set { contact.PersonType = value; } }
        public string PoOrderTypeId { get { return contact.PoOrderTypeId; } set { contact.PoOrderTypeId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string RowGuid { get { return contact.RowGuid; } set { contact.RowGuid = value; } }
        public string Salutation { get { return contact.Salutation; } set { contact.Salutation = value; } }
        public string SourceId { get { return contact.SourceId; } set { contact.SourceId = value; } }
        public string WarehouseId { get { return contact.WarehouseId; } set { contact.WarehouseId = value; } }
        public string WebCatalogId { get { return contact.WebCatalogId; } set { contact.WebCatalogId = value; } }
        public string Website { get { return contact.Website; } set { contact.Website = value; } }
        public string WebStatus { get { return contact.WebStatus; } set { contact.WebStatus = value; } }
        public string WebStatusAsOf { get { return contact.WebStatusAsOf; } set { contact.WebStatusAsOf = value; } }
        public string WebStatusUpdateByUsersId { get { return contact.WebStatusUpdateByUsersId; } set { contact.WebStatusUpdateByUsersId = value; } }
        public string ZipCode { get { return contact.ZipCode; } set { contact.ZipCode = value; } }
        public string State { get { return contact.State; } set { contact.State = value; } }

        // WebUserRecord
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }
        public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }
        public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }
        public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }
        public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }
        public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }

        public bool? WebAdministrator { get { return webUser.WebAdministrator; } set { webUser.WebAdministrator = value; } }
        public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }
        public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }

        //public string GroupId { get { return webUser.GroupId; } set { webUser.GroupId = value; } }
        //------------------------------------------------------------------------------------
        private void Contact_AfterSaves(object sender, SaveEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (string.IsNullOrEmpty(webUser.WebUserId)))
            {
                ContactLogic contact2 = new ContactLogic();
                var dbConfig = this.contact.GetDbConfig();
                contact2.SetDbConfig(dbConfig);
                string[] pk = GetPrimaryKeys();
                bool b = contact2.LoadAsync<ContactLogic>(pk).Result;
                using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
                {
                    string webusersid = FwSqlCommand.GetDataAsync(conn, dbConfig.QueryTimeout, "webusers", "contactid", contact2.ContactId, "webusersid").Result.ToString().TrimEnd();
                    this.webUser.WebUserId = webusersid;
                }
            }
        }
        //------------------------------------------------------------------------------------
        private void WebUser_AfterSaves(object sender, SaveEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                this.WebUserId = webUser.WebUserId;
                int i = SaveAsync().Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
