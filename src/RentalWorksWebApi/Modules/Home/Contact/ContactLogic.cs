using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebLibrary;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.Contact
{
    [FwLogic(Id:"VjUVxBeTrqhk")]
    public class ContactLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ContactRecord contact = new ContactRecord();
        WebUserRecord webUser = new WebUserRecord();
        UserRecord user = new UserRecord();
        ContactLoader contactLoader = new ContactLoader();
        public ContactLogic()
        {
            dataRecords.Add(contact);
            dataRecords.Add(user);
            dataRecords.Add(webUser);
            dataLoader = contactLoader;

            BeforeSave += OnBeforeSaveContactLogic;
            BeforeValidate += BeforeValidateContact;

            contact.BeforeDelete += Contact_BeforeDelete;
            user.BeforeSave += BeforeSaveUser;
            user.BeforeDelete += User_BeforeDelete;
            webUser.BeforeSave += BeforeSaveWebUser;
            webUser.BeforeDelete += WebUser_BeforeDelete;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"QYCltLBdmJfW", IsPrimaryKey:true)]
        public string ContactId { get { return contact.ContactId; } set { contact.ContactId = value; } }

        [FwLogicProperty(Id:"cdv03j1qnZxD")]
        public string ActiveDate { get { return contact.ActiveDate; } set { contact.ActiveDate = value; } }

        [FwLogicProperty(Id:"NmrzSOJwMkoG")]
        public string Address1 { get { return contact.Address1; } set { contact.Address1 = value; } }

        [FwLogicProperty(Id:"PgRp0r51ZjW5")]
        public string Address2 { get { return contact.Address2; } set { contact.Address2 = value; } }

        [FwLogicProperty(Id:"JDPhmixWXeP0")]
        public string Barcode { get { return contact.Barcode; } set { contact.Barcode = value; } }

        [FwLogicProperty(Id:"mUR8uGAby4KO")]
        public string City { get { return contact.City; } set { contact.City = value; } }

        [FwLogicProperty(Id:"geota5y6motS")]
        public string ContactRecordType { get { return contact.ContactRecordType; } set { contact.ContactRecordType = value; } }

        [FwLogicProperty(Id:"GCn7PBO8nOvw", IsReadOnly:true)]
        public string ContactRecordTypeColor { get; set; }

        [FwLogicProperty(Id:"Pz1996uZzANm")]
        public string ContactTitleId { get { return contact.ContactTitleId; } set { contact.ContactTitleId = value; } }

        [FwLogicProperty(Id:"uKCcZM4UqLGt", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"dWlyazIyuzvs", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"Te9aMySW7ijt")]
        public string CountryId { get { return contact.CountryId; } set { contact.CountryId = value; } }

        [FwLogicProperty(Id:"gjCZBeYHdEK1")]
        public string DateStamp { get { return contact.DateStamp; } set { contact.DateStamp = value; } }

        [FwLogicProperty(Id:"ly9IeOvzYgwb")]
        public string DirectExtension { get { return contact.DirectExtension; } set { contact.DirectExtension = value; } }

        [FwLogicProperty(Id:"CsTOZwsW5diT")]
        public string DirectPhone { get { return contact.DirectPhone; } set { contact.DirectPhone = value; } }

        [FwLogicProperty(Id:"xQc3GVcwRpw2")]
        public string Email { get { return contact.Email; } set { contact.Email = value; } }

        [FwLogicProperty(Id:"cSOn6N3k8uvq")]
        public string Fax { get { return contact.Fax; } set { contact.Fax = value; } }

        [FwLogicProperty(Id:"HCV4Ld7sqnDo")]
        public string FaxExtension { get { return contact.FaxExtension; } set { contact.FaxExtension = value; } }

        [FwLogicProperty(Id:"P3HQXIdsM8TH")]
        public string FirstName { get { return contact.FirstName; } set { contact.FirstName = value; } }

        [FwLogicProperty(Id:"0srm8l16M5j2")]
        public string HomePhone { get { return contact.HomePhone; } set { contact.HomePhone = value; } }

        [FwLogicProperty(Id:"aUfcpWFOUf2G")]
        public bool? Inactive { get { return contact.Inactive; } set { contact.Inactive = value; } }

        [FwLogicProperty(Id:"q7SRhhfZcW5Q")]
        public string InactiveDate { get { return contact.InactiveDate; } set { contact.InactiveDate = value; } }

        [FwLogicProperty(Id:"4IVXAXUveYAl")]
        public string Info { get { return contact.Info; } set { contact.Info = value; } }

        [FwLogicProperty(Id:"WrqVFMq8cKzN")]
        public string InputDate { get { return contact.InputDate; } set { contact.InputDate = value; } }

        [FwLogicProperty(Id:"TiQcFWDv5ZIW")]
        public string ModifiedDate { get { return contact.ModifiedDate; } set { contact.ModifiedDate = value; } }

        [FwLogicProperty(Id:"v2O3jAnSVtXy")]
        public string LastName { get { return contact.LastName; } set { contact.LastName = value; } }

        [FwLogicProperty(Id:"czXFLly7EYf9")]
        public string MiddleInitial { get { return contact.MiddleInitial; } set { contact.MiddleInitial = value; } }

        [FwLogicProperty(Id:"zemByH4NkXhe")]
        public string MobilePhone { get { return contact.MobilePhone; } set { contact.MobilePhone = value; } }

        [FwLogicProperty(Id:"x7G8C0kbxrYT", IsRecordTitle:true)]
        public string NameFirstMiddleLast { get { return contact.NameFirstMiddleLast; } set { contact.NameFirstMiddleLast = value; } }

        [FwLogicProperty(Id:"R9EHtCYHFFqZ")]
        public string OfficeExtension { get { return contact.OfficeExtension; } set { contact.OfficeExtension = value; } }

        [FwLogicProperty(Id:"QLT4c3LXVZwz")]
        public string OfficePhone { get { return contact.OfficePhone; } set { contact.OfficePhone = value; } }

        [FwLogicProperty(Id:"MHNw9n1RYKEk")]
        public string Pager { get { return contact.Pager; } set { contact.Pager = value; } }

        [FwLogicProperty(Id:"m0iv5sPFJtWS")]
        public string PagerPin { get { return contact.PagerPin; } set { contact.PagerPin = value; } }

        [FwLogicProperty(Id:"LsT4w4vqj4R5")]
        public string Person { get { return contact.Person; } set { contact.Person = value; } }

        [FwLogicProperty(Id:"64RgOmup6l7K")]
        public string Salutation { get { return contact.Salutation; } set { contact.Salutation = value; } }

        [FwLogicProperty(Id:"UDD6ACHt0sVA")]
        public string Website { get { return contact.Website; } set { contact.Website = value; } }

        [FwLogicProperty(Id:"lbuH5VPJaTp4")]
        public string WebStatus { get { return contact.WebStatus; } set { contact.WebStatus = value; } }

        [FwLogicProperty(Id:"1cXYelWP9VgZ")]
        public string ZipCode { get { return contact.ZipCode; } set { contact.ZipCode = value; } }

        [FwLogicProperty(Id:"asYpgOpsj5lm")]
        public string State { get { return contact.State; } set { contact.State = value; } }


        //userRecord
        [FwLogicProperty(Id:"L9nN3oaMplkQ")]
        public string UserId { get { return user.UserId; } set { user.UserId = value; } }


        // WebUserRecord
        [FwLogicProperty(Id:"qy03CaOxmkQ2")]
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        [FwLogicProperty(Id:"x4Er14EG8DST")]
        public string WebUserContactId { get { return webUser.ContactId; } set { webUser.ContactId = value; } }

        [FwLogicProperty(Id:"95YhDZ5ZALZz")]
        public string WebUserUserId { get { return webUser.UserId; } set { webUser.UserId = value; } }

        [FwLogicProperty(Id:"UpQcvFKtnyXY")]
        public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }

        [FwLogicProperty(Id:"6P7hd6okvoGJ")]
        public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }

        [FwLogicProperty(Id:"8j5hfJNXnujE")]
        public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }

        [FwLogicProperty(Id:"DzHzzswzDXZn")]
        public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }

        [FwLogicProperty(Id:"hJV187gx8xUP")]
        public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }

        [FwLogicProperty(Id:"5sJJi29pSLA7")]
        public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }

        [FwLogicProperty(Id:"lq0kifooMFzd")]
        public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }

        //------------------------------------------------------------------------------------
        private void BeforeValidateContact(object sender, BeforeValidateEventArgs e)
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
        public void OnBeforeSaveContactLogic(object sender, BeforeSaveEventArgs e)
        {
            ContactRecordType = "CONTACT";
            if (!string.IsNullOrEmpty(this.WebPassword))
            {
                // Encyrypt the WebPassword if the user supplies a new one
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    this.WebPassword = FwSqlData.EncryptAsync(conn, this.AppConfig.DatabaseSettings, this.WebPassword.ToUpper()).Result;
                }
                this.PasswordLastUpdated = FwConvert.ToUtcIso8601DateTime(DateTime.UtcNow);
            }
        }
        //------------------------------------------------------------------------------------
        private void BeforeSaveUser(object sender, BeforeSaveDataRecordEventArgs e)
        {
            // get the usersid
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                var row = FwSqlCommand.GetRowAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusers", "contactid", this.ContactId, false).Result;
                if (row.ContainsKey("usersid"))
                {
                    user.UserId = row["usersid"].ToString().TrimEnd();
                    e.SaveMode = TDataRecordSaveMode.smUpdate;
                    e.PerformSave = false;
                }
            }
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert))
            {
                //fields are required for a user
                user.FirstName = string.Empty;
                user.LastName = this.UserId;
                user.LoginName = this.UserId; ;
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
        private void BeforeSaveWebUser(object sender, BeforeSaveDataRecordEventArgs e)
        {
            // get the webusersid
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                var row = FwSqlCommand.GetRowAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusers", "contactid", this.ContactId, false).Result;
                if (row.ContainsKey("webusersid"))
                {
                    webUser.WebUserId = row["webusersid"].ToString().TrimEnd();
                    e.SaveMode = TDataRecordSaveMode.smUpdate;
                }
            }
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                webUser.ApplicationTheme = "theme-material";
                webUser.UserId = user.UserId;
                webUser.ContactId = this.ContactId;
            }
        }
        //------------------------------------------------------------------------------------
        private void Contact_BeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                var row = FwSqlCommand.GetRowAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusers", "contactid", this.ContactId, false).Result;
                e.PerformDelete = row.ContainsKey("usersid");
                if (row.ContainsKey("usersid") && row.ContainsKey("webusersid"))
                {
                    this.UserId = row["usersid"].ToString().TrimEnd();
                    this.WebUserId = row["webusersid"].ToString().TrimEnd();
                }
            }
        }
        //------------------------------------------------------------------------------------
        private void User_BeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            e.PerformDelete = !string.IsNullOrEmpty(this.UserId);
        }
        //------------------------------------------------------------------------------------
        private void WebUser_BeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            e.PerformDelete = !string.IsNullOrEmpty(this.WebUserId);
        }
        //------------------------------------------------------------------------------------
        }
}
