using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Home.Contact;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.Crew
{
    [FwLogic(Id:"d4ihNOoBjoRH")]
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
            BeforeSave += OnBeforeSaveCrewLogic;
            crew.AfterSave += Crew_AfterSave;
            webUser.AfterSave += WebUser_AfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qd8DYZeJmeq6", IsPrimaryKey:true)]
        public string CrewId { get { return crew.ContactId; } set { crew.ContactId = value; } }

        [FwLogicProperty(Id:"N7mXaf2k5JRi", IsReadOnly:true)]
        public string UserId { get; set; }

        [FwLogicProperty(Id:"OuCDvIXIi8Kt", IsReadOnly:true)]
        public bool? IsUser { get; set; }

        [FwLogicProperty(Id:"NmzepeZnGuP1")]
        public string Salutation { get { return crew.Salutation; } set { crew.Salutation = value; } }

        [FwLogicProperty(Id:"nQvOChIHQIBX")]
        public string NameFirstMiddleLast { get { return crew.NameFirstMiddleLast; } set { crew.NameFirstMiddleLast = value; } }

        [FwLogicProperty(Id:"n15EIEZ2lpgh", IsRecordTitle:true)]
        public string Person { get { return crew.Person; } set { crew.Person = value; } }

        [FwLogicProperty(Id:"ZBeKrFgUa6FJ")]
        public string LastName { get { return crew.LastName; } set { crew.LastName = value; } }

        [FwLogicProperty(Id:"QtO9zNC23AaL")]
        public string FirstName { get { return crew.FirstName; } set { crew.FirstName = value; } }

        [FwLogicProperty(Id:"gaNhuODG90ZW")]
        public string Address1 { get { return crew.Address1; } set { crew.Address1 = value; } }

        [FwLogicProperty(Id:"7nXnNJLHSJ9D")]
        public string Address2 { get { return crew.Address2; } set { crew.Address2 = value; } }

        [FwLogicProperty(Id:"MqbrjKwKFeWh")]
        public string City { get { return crew.City; } set { crew.City = value; } }

        [FwLogicProperty(Id:"aBnSn0iE1vkt")]
        public string State { get { return crew.State; } set { crew.State = value; } }

        [FwLogicProperty(Id:"dYIzocDzvxOF")]
        public string CountryId { get { return crew.CountryId; } set { crew.CountryId = value; } }

        [FwLogicProperty(Id:"yDQLF5TcsEQ5", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"zMfJwSd550yC")]
        public string ZipCode { get { return crew.ZipCode; } set { crew.ZipCode = value; } }

        [FwLogicProperty(Id:"6ZjkVHLt9DS4")]
        public string MiddleInitial { get { return crew.MiddleInitial; } set { crew.MiddleInitial = value; } }

        [FwLogicProperty(Id:"5lfkp8DmNkCE", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"DXHNHoa8if5r", IsReadOnly:true)]
        public string Position { get; set; }

        [FwLogicProperty(Id:"rLObVSC2JE2g")]
        public string OfficePhone { get { return crew.OfficePhone; } set { crew.OfficePhone = value; } }

        [FwLogicProperty(Id:"KSOvG8SGSA3O")]
        public string OfficeExtension { get { return crew.OfficeExtension; } set { crew.OfficeExtension = value; } }

        [FwLogicProperty(Id:"8vJqYenSZgrh")]
        public string DirectPhone { get { return crew.DirectPhone; } set { crew.DirectPhone = value; } }

        [FwLogicProperty(Id:"ZwlxyZPAeQu0")]
        public string DirectExtension { get { return crew.DirectExtension; } set { crew.DirectExtension = value; } }

        [FwLogicProperty(Id:"mnsxA75M2BWJ")]
        public string Fax { get { return crew.Fax; } set { crew.Fax = value; } }

        [FwLogicProperty(Id:"GXmazhvMuvjE")]
        public string FaxExtension { get { return crew.FaxExtension; } set { crew.FaxExtension = value; } }

        [FwLogicProperty(Id:"sHwIcFN0Izzp")]
        public string Pager { get { return crew.Pager; } set { crew.Pager = value; } }

        [FwLogicProperty(Id:"hxd92OgzfsSI")]
        public string PagerPin { get { return crew.PagerPin; } set { crew.PagerPin = value; } }

        [FwLogicProperty(Id:"xgnOUppfOOzb")]
        public string MobilePhone { get { return crew.MobilePhone; } set { crew.MobilePhone = value; } }

        [FwLogicProperty(Id:"MgwpxC9fOCUP")]
        public string HomePhone { get { return crew.HomePhone; } set { crew.HomePhone = value; } }

        [FwLogicProperty(Id:"CHmpm9lMj2LD")]
        public string Email { get { return crew.Email; } set { crew.Email = value; } }

        [FwLogicProperty(Id:"5Nfjm2HnnsKk")]
        public string ContactTitleId { get { return crew.ContactTitleId; } set { crew.ContactTitleId = value; } }

        [FwLogicProperty(Id:"GXHOcxkWT2nG", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"687xxXRijBvL")]
        public string ActiveDate { get { return crew.ActiveDate; } set { crew.ActiveDate = value; } }

        [FwLogicProperty(Id:"VrwqC5ECLaMh")]
        public string InactiveDate { get { return crew.InactiveDate; } set { crew.InactiveDate = value; } }

        [FwLogicProperty(Id:"acuD8SCRcN2c", IsReadOnly:true)]
        public bool? ContractEmployee { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"CMwVGdDhcw3P")]
        public string ContactRecordType { get { return crew.ContactRecordType; } set { crew.ContactRecordType = value; } }

        [FwLogicProperty(Id:"iiUjR4lCfKQr")]
        public bool? Inactive { get { return crew.Inactive; } set { crew.Inactive = value; } }



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
        public string DateStamp { get { return crew.DateStamp; } set { crew.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveCrewLogic(object sender, BeforeSaveEventArgs e)
        {
            //base.BeforeSave(saveMode);
            ContactRecordType = "CREW";
        }
        //------------------------------------------------------------------------------------

        private void Crew_AfterSave(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (string.IsNullOrEmpty(webUser.WebUserId)))
            {
                CrewLogic crew2 = new CrewLogic();
                crew2.AppConfig = this.crew.AppConfig;
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
