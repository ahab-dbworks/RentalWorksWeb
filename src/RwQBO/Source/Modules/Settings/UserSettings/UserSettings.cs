using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using System.Collections.Generic;
using System.Dynamic;

namespace RwQBO.Source.Modules
{
    class UserSettings : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return "User Settings";
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["webusers"].GetUniqueId("webusersid").Value;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            base.GetData();
            switch((string)request.method)
            {
                case "LoadSettings": LoadSettings(); break;
                case "SaveSettings": SaveSettings(); break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void LoadSettings()
        {
            FwWebUserSettings controlrec;

            controlrec                              = FwSqlData.GetWebUserSettings(this.form.DatabaseConnection, session.security.webUser.webusersid);
            response.usersettings                   = new ExpandoObject();
            response.usersettings.browsedefaultrows = controlrec.Settings.BrowseDefaultRows;
            response.usersettings.applicationtheme  = controlrec.Settings.ApplicationTheme;
        }
        //---------------------------------------------------------------------------------------------
        protected void SaveSettings()
        {
            FwWebUserSettings controlrec;
            IDictionary<string,dynamic> fields;

            fields = request.fields;
            controlrec = FwSqlData.GetWebUserSettings(this.form.DatabaseConnection, session.security.webUser.webusersid);

            if (fields.ContainsKey("#settings.browsedefaultrows")) { controlrec.Settings.BrowseDefaultRows = fields["#settings.browsedefaultrows"].value; };
            if (fields.ContainsKey("#settings.applicationtheme"))  { controlrec.Settings.ApplicationTheme  = fields["#settings.applicationtheme"].value; };
            
            controlrec.Settings.Save(this.form.DatabaseConnection, session.security.webUser.webusersid);
            response.usersettings                   = new ExpandoObject();
            response.usersettings.browsedefaultrows = controlrec.Settings.BrowseDefaultRows;
            response.usersettings.applicationtheme  = controlrec.Settings.ApplicationTheme;
        }
        //---------------------------------------------------------------------------------------------
    }
}
