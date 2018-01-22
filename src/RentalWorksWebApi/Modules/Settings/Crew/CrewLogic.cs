using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Home.Contact;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.Crew
{
    public class CrewLogic : AppBusinessLogic //ContactLogic
    {
        //------------------------------------------------------------------------------------ 
        ContactRecord crew = new ContactRecord();
        WebUserRecord webUser = new WebUserRecord();
        CrewLoader crewLoader = new CrewLoader();

        public CrewLogic()
        {
            dataRecords.Add(crew);
            dataRecords.Add(webUser);
            dataLoader = crewLoader;
            crew.AfterSaves += Crew_AfterSaves;
            webUser.AfterSaves += WebUser_AfterSaves;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CrewId { get { return crew.ContactId; } set { crew.ContactId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsUser { get; set; }
        public string Salutation { get { return crew.Salutation; } set { crew.Salutation = value; } }
        public string NameFirstMiddleLast { get { return crew.NameFirstMiddleLast; } set { crew.NameFirstMiddleLast = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Person { get { return crew.Person; } set { crew.Person = value; } }
        public string LastName { get { return crew.LastName; } set { crew.LastName = value; } }
        public string FirstName { get { return crew.FirstName; } set { crew.FirstName = value; } }
        public string Address1 { get { return crew.Address1; } set { crew.Address1 = value; } }
        public string Address2 { get { return crew.Address2; } set { crew.Address2 = value; } }
        public string City { get { return crew.City; } set { crew.City = value; } }
        public string State { get { return crew.State; } set { crew.State = value; } }
        public string CountryId { get { return crew.CountryId; } set { crew.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return crew.ZipCode; } set { crew.ZipCode = value; } }
        public string MiddleInitial { get { return crew.MiddleInitial; } set { crew.MiddleInitial = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Position { get; set; }
        public string OfficePhone { get { return crew.OfficePhone; } set { crew.OfficePhone = value; } }
        public string OfficeExtension { get { return crew.OfficeExtension; } set { crew.OfficeExtension = value; } }
        public string DirectPhone { get { return crew.DirectPhone; } set { crew.DirectPhone = value; } }
        public string DirectExtension { get { return crew.DirectExtension; } set { crew.DirectExtension = value; } }
        public string Fax { get { return crew.Fax; } set { crew.Fax = value; } }
        public string FaxExtension { get { return crew.FaxExtension; } set { crew.FaxExtension = value; } }
        public string Pager { get { return crew.Pager; } set { crew.Pager = value; } }
        public string PagerPin { get { return crew.PagerPin; } set { crew.PagerPin = value; } }
        public string MobilePhone { get { return crew.MobilePhone; } set { crew.MobilePhone = value; } }
        public string HomePhone { get { return crew.HomePhone; } set { crew.HomePhone = value; } }
        public string Email { get { return crew.Email; } set { crew.Email = value; } }
        public string ContactTitleId { get { return crew.ContactTitleId; } set { crew.ContactTitleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        public string ActiveDate { get { return crew.ActiveDate; } set { crew.ActiveDate = value; } }
        public string InactiveDate { get { return crew.InactiveDate; } set { crew.InactiveDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ContractEmployee { get; set; }
        [JsonIgnore]
        public string ContactRecordType { get { return crew.ContactRecordType; } set { crew.ContactRecordType = value; } }
        public bool? Inactive { get { return crew.Inactive; } set { crew.Inactive = value; } }


        //// WebUserRecord
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }
        public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }
        public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }
        public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }
        public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }
        public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }

        public bool? WebAdministrator { get { return webUser.WebAdministrator; } set { webUser.WebAdministrator = value; } }
        public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }
        public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }



        public string DateStamp { get { return crew.DateStamp; } set { crew.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            base.BeforeSave();
            ContactRecordType = "CREW";
        }
        //------------------------------------------------------------------------------------

        private void Crew_AfterSaves(object sender, AfterSaveEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (e.SavePerformed) && (string.IsNullOrEmpty(webUser.WebUserId)))
            {
                CrewLogic crew2 = new CrewLogic();
                var dbConfig = this.crew.GetDbConfig();
                crew2.SetDbConfig(dbConfig);
                string[] pk = GetPrimaryKeys();
                bool b = crew2.LoadAsync<CrewLogic>(pk).Result;
                using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
                {
                    string webusersid = FwSqlCommand.GetDataAsync(conn, dbConfig.QueryTimeout, "webusers", "contactid", crew2.CrewId, "webusersid").Result.ToString().TrimEnd();
                    this.webUser.WebUserId = webusersid;
                }
            }
        }
        //------------------------------------------------------------------------------------
        private void WebUser_AfterSaves(object sender, AfterSaveEventArgs e)
        {

            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert) && (e.SavePerformed))
            {
                this.WebUserId = webUser.WebUserId;
                int i = SaveAsync().Result;
            }
        }
        //------------------------------------------------------------------------------------

    }
}