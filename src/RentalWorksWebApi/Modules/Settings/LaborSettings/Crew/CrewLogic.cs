using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Contact;
using WebApi;

namespace WebApi.Modules.Settings.LaborSettings.Crew
{
    [FwLogic(Id:"d4ihNOoBjoRH")]
    public class CrewLogic : AppBusinessLogic //ContactLogic
    {
        //------------------------------------------------------------------------------------ 
        ContactRecord contact = new ContactRecord();
        WebUserRecord webUser = new WebUserRecord();
        UserRecord user = new UserRecord();
        CrewRecord crew = new CrewRecord();

        CrewLoader crewLoader = new CrewLoader();
        private string newUserId = string.Empty;

        public CrewLogic()
        {
            dataRecords.Add(contact);
            dataRecords.Add(user);
            dataRecords.Add(webUser);
            dataRecords.Add(crew);
            dataLoader = crewLoader;
            BeforeValidate += BeforeValidateCrew;
            BeforeSave += OnBeforeSaveCrewLogic;
            contact.AfterSave += Contact_AfterSave;
            user.AfterSave += User_AfterSave;
            user.AssignPrimaryKeys += User_AssignNewId;
            crew.AssignPrimaryKeys += Crew_AssignNewId;
            webUser.AfterSave += WebUser_AfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qd8DYZeJmeq6", IsPrimaryKey:true)]
        public string CrewId { get { return contact.ContactId; } set { contact.ContactId = value; webUser.ContactId = value; crew.CrewId = value; } }

        [FwLogicProperty(Id:"N7mXaf2k5JRi", IsReadOnly:true)]
        public string UserId { get { return webUser.UserId; } set { webUser.UserId = value; user.UserId = value; } }

        [FwLogicProperty(Id:"OuCDvIXIi8Kt", IsReadOnly:true)]
        public bool? IsUser { get; set; }

        [FwLogicProperty(Id:"NmzepeZnGuP1")]
        public string Salutation { get { return contact.Salutation; } set { contact.Salutation = value; } }

        [FwLogicProperty(Id:"nQvOChIHQIBX")]
        public string NameFirstMiddleLast { get { return contact.NameFirstMiddleLast; } set { contact.NameFirstMiddleLast = value; } }

        [FwLogicProperty(Id:"n15EIEZ2lpgh", IsRecordTitle:true)]
        public string Person { get { return contact.Person; } set { contact.Person = value; } }

        [FwLogicProperty(Id:"ZBeKrFgUa6FJ")]
        public string LastName { get { return contact.LastName; } set { contact.LastName = value; } }

        [FwLogicProperty(Id:"QtO9zNC23AaL")]
        public string FirstName { get { return contact.FirstName; } set { contact.FirstName = value; } }

        [FwLogicProperty(Id:"gaNhuODG90ZW")]
        public string Address1 { get { return contact.Address1; } set { contact.Address1 = value; } }

        [FwLogicProperty(Id:"7nXnNJLHSJ9D")]
        public string Address2 { get { return contact.Address2; } set { contact.Address2 = value; } }

        [FwLogicProperty(Id:"MqbrjKwKFeWh")]
        public string City { get { return contact.City; } set { contact.City = value; } }

        [FwLogicProperty(Id:"aBnSn0iE1vkt")]
        public string State { get { return contact.State; } set { contact.State = value; } }

        [FwLogicProperty(Id:"dYIzocDzvxOF")]
        public string CountryId { get { return contact.CountryId; } set { contact.CountryId = value; } }

        [FwLogicProperty(Id:"yDQLF5TcsEQ5", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"zMfJwSd550yC")]
        public string ZipCode { get { return contact.ZipCode; } set { contact.ZipCode = value; } }

        [FwLogicProperty(Id:"6ZjkVHLt9DS4")]
        public string MiddleInitial { get { return contact.MiddleInitial; } set { contact.MiddleInitial = value; } }

        [FwLogicProperty(Id:"5lfkp8DmNkCE", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"DXHNHoa8if5r", IsReadOnly:true)]
        public string Position { get; set; }

        [FwLogicProperty(Id:"rLObVSC2JE2g")]
        public string OfficePhone { get { return contact.OfficePhone; } set { contact.OfficePhone = value; } }

        [FwLogicProperty(Id:"KSOvG8SGSA3O")]
        public string OfficeExtension { get { return contact.OfficeExtension; } set { contact.OfficeExtension = value; } }

        [FwLogicProperty(Id:"8vJqYenSZgrh")]
        public string DirectPhone { get { return contact.DirectPhone; } set { contact.DirectPhone = value; } }

        [FwLogicProperty(Id:"ZwlxyZPAeQu0")]
        public string DirectExtension { get { return contact.DirectExtension; } set { contact.DirectExtension = value; } }

        [FwLogicProperty(Id:"mnsxA75M2BWJ")]
        public string Fax { get { return contact.Fax; } set { contact.Fax = value; } }

        [FwLogicProperty(Id:"GXmazhvMuvjE")]
        public string FaxExtension { get { return contact.FaxExtension; } set { contact.FaxExtension = value; } }

        [FwLogicProperty(Id:"sHwIcFN0Izzp")]
        public string Pager { get { return contact.Pager; } set { contact.Pager = value; } }

        [FwLogicProperty(Id:"hxd92OgzfsSI")]
        public string PagerPin { get { return contact.PagerPin; } set { contact.PagerPin = value; } }

        [FwLogicProperty(Id:"xgnOUppfOOzb")]
        public string MobilePhone { get { return contact.MobilePhone; } set { contact.MobilePhone = value; } }

        [FwLogicProperty(Id:"MgwpxC9fOCUP")]
        public string HomePhone { get { return contact.HomePhone; } set { contact.HomePhone = value; } }

        [FwLogicProperty(Id:"CHmpm9lMj2LD")]
        public string Email { get { return contact.Email; } set { contact.Email = value; } }

        [FwLogicProperty(Id:"5Nfjm2HnnsKk")]
        public string ContactTitleId { get { return contact.ContactTitleId; } set { contact.ContactTitleId = value; } }

        [FwLogicProperty(Id:"GXHOcxkWT2nG", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"687xxXRijBvL")]
        public string ActiveDate { get { return contact.ActiveDate; } set { contact.ActiveDate = value; } }

        [FwLogicProperty(Id:"VrwqC5ECLaMh")]
        public string InactiveDate { get { return contact.InactiveDate; } set { contact.InactiveDate = value; } }

        [FwLogicProperty(Id:"acuD8SCRcN2c", IsReadOnly:true)]
        public bool? ContractEmployee { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"CMwVGdDhcw3P")]
        public string ContactRecordType { get { return contact.ContactRecordType; } set { contact.ContactRecordType = value; } }

        [FwLogicProperty(Id:"iiUjR4lCfKQr")]
        public bool? Inactive { get { return contact.Inactive; } set { contact.Inactive = value; } }



        //// WebUserRecord
        [FwLogicProperty(Id:"vwHwBRlPumgV")]
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        [FwLogicProperty(Id:"3FzRgEeM9wEa")]
        public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }

        [FwLogicProperty(Id:"PntzfcNprLmd")]
        public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }

        [FwLogicProperty(Id:"MCWRNvpPcKS2")]
        public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }

        [FwLogicProperty(Id:"bNUSqIdOumWe")]
        public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }

        [FwLogicProperty(Id:"h66xVbANLvSC")]
        public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }


        [FwLogicProperty(Id:"0BVf4vHYgkqz")]
        public bool? WebAdministrator { get { return webUser.WebAdministrator; } set { webUser.WebAdministrator = value; } }

        [FwLogicProperty(Id:"GyEPTuIJau2d")]
        public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }

        [FwLogicProperty(Id:"sBX96fVcC7S4")]
        public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }




        [FwLogicProperty(Id:"WE16jfKSepIU")]
        public string DateStamp { get { return contact.DateStamp; } set { contact.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        private void BeforeValidateCrew(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                //fields are required for a user - tempoarary
                user.FirstName = "X";
                user.LastName = "X";
                user.LoginName = "X";
                user.OfficeLocationId = "X";
                user.WarehouseId = "X";
                user.GroupId = "X";
                user.DefaultDepartmentType = RwConstants.DEPARTMENT_TYPE_RENTAL;
                user.RentalDepartmentId = "X";
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveCrewLogic(object sender, BeforeSaveEventArgs e)
        {
            //base.BeforeSave(saveMode);
            ContactRecordType = "CREW";

            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                newUserId = AppFunc.GetNextIdAsync(AppConfig, e.SqlConnection).Result;
                //fields are required for a user
                user.FirstName = string.Empty;
                user.LastName = newUserId;
                user.LoginName = newUserId; 
                user.OfficeLocationId = string.Empty;
                user.WarehouseId = string.Empty;
                user.PrimaryOfficeLocationId = string.Empty;
                user.PrimaryWarehouseId = string.Empty;
                user.GroupId = string.Empty;
                user.DefaultDepartmentType = string.Empty;
                user.RentalDepartmentId = string.Empty;
            }

        }
        //------------------------------------------------------------------------------------
        private void Contact_AfterSave(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                webUser.ContactId = contact.ContactId;
            }
            else if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (string.IsNullOrEmpty(webUser.WebUserId)))
            {
                CrewLogic crew2 = new CrewLogic();
                crew2.AppConfig = this.contact.AppConfig;
                object[] pk = GetPrimaryKeys();
                bool b = crew2.LoadAsync<CrewLogic>(pk).Result;
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    string webusersid = FwSqlCommand.GetDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusers", "contactid", crew2.CrewId, "webusersid").Result.ToString().TrimEnd();
                    this.webUser.WebUserId = webusersid;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void User_AssignNewId(object sender, EventArgs e)
        {
            ((UserRecord)sender).UserId = newUserId;
        }
        //------------------------------------------------------------------------------------ 
        public void Crew_AssignNewId(object sender, EventArgs e)
        {
            ((CrewRecord)sender).CrewId = contact.ContactId;
        }
        //------------------------------------------------------------------------------------ 
        private void User_AfterSave(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                this.webUser.UserId = user.UserId;
            }
        }
        //------------------------------------------------------------------------------------
        private void WebUser_AfterSave(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                this.WebUserId = webUser.WebUserId;
                int i = SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------

    }
}
