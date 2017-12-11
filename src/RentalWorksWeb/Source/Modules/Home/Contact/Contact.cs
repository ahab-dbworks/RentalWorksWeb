using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.Utilities;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace Web.Source.Modules
{
    class Contact : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            string activeView;

            FwValidate.TestPropertyDefined("FwContact.SetBrowseQry", request, "activeview");
            activeView = request.activeview;

            base.setBrowseQry(selectQry);
            selectQry.AddWhere("contactrecordtype = @contactrecordtype");

            switch(activeView)
            {
                case "ALL":      selectQry.AddParameter("@contactrecordtype", "CONTACT");  break;
                case "CUSTOMER": selectQry.AddParameter("@contactrecordtype", "CUSTOMER"); break;
                case "DEAL":     selectQry.AddParameter("@contactrecordtype", "DEAL");     break;
                case "VENDOR":   selectQry.AddParameter("@contactrecordtype", "VENDOR");   break;
                case "SIGNUP":   selectQry.AddParameter("@contactrecordtype", "SIGNUP");   break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return form.Tables["contact"].GetField("fname").Value.TrimEnd() + " " + form.Tables["contact"].GetField("lname").Value.TrimEnd();
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            string webusersid, contactid, usersid;
            
            base.beforeInsert();
            contactid  = FwSqlData.GetNextId(this.form.DatabaseConnection);
            webusersid = FwSqlData.GetNextId(this.form.DatabaseConnection);
            usersid    = FwSqlData.GetNextId(this.form.DatabaseConnection);
            form.Tables["contact"].GetField("contactid").UniqueIdentifier   = true;
            form.Tables["contact"].GetField("contactid").Value              = contactid;
            form.Tables["contact"].GetField("contactrecordtype").Value      = "CONTACT";
            form.Tables["webusers"].GetField("webusersid").UniqueIdentifier = true;
            form.Tables["webusers"].GetField("webusersid").Value            = webusersid;
            form.Tables["webusers"].GetField("contactid").Value             = contactid;
            form.Tables["webusers"].GetField("usersid").Value               = usersid;
            form.Tables["users"].GetField("usersid").UniqueIdentifier       = true;
            form.Tables["users"].GetField("usersid").Value                  = usersid;
            form.Tables["users"].GetField("loginname").Value                = usersid;
        }
        //---------------------------------------------------------------------------------------------
        public override void Load()
        {
            FwSqlCommand qry;
            string contactid, webusersid, usersid;
            
            contactid = this.getUniqueIdFromRequest("contact.contactid");
            qry = new FwSqlCommand(this.form.DatabaseConnection);
            qry.Add("select webusersid, usersid");
            qry.Add("from webusers with (nolock)");
            qry.Add("where contactid = @contactid");
            qry.AddParameter("@contactid", contactid);
            qry.Execute();
            webusersid = qry.GetField("webusersid").ToString();
            usersid    = qry.GetField("usersid").ToString();
            this.setUniqueIdOnRequest("webusers.webusersid", webusersid);
            this.setUniqueIdOnRequest("users.usersid",       usersid);
            form.Tables["webusers"].GetUniqueId("webusersid").Value = webusersid;
            form.Tables["users"].GetUniqueId("usersid").Value       = usersid;
            base.Load();
        }
        //---------------------------------------------------------------------------------------------
        protected override bool validateForm()
        {
            bool isvalid = true;

            isvalid = base.validateForm();
            isvalid = validatePasswordComplexity();

            return isvalid;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            switch((string)request.method)
            {
                case "CheckPasswordComplexity": CheckPasswordComplexity(); break;
                //case "GetDriverInfo":           GetDriverInfo();           break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["contact"].GetUniqueId("contactid").Value;
        }
        //---------------------------------------------------------------------------------------------
        private bool validatePasswordComplexity()
        {
            string password, firstname = string.Empty, lastname = string.Empty, contactid;
            bool isvalid = true;
            IDictionary<string, dynamic> fields, uniqueids;
            FwControl controlrec;
            dynamic passwordcomplexity = new ExpandoObject();
            dynamic name;

            controlrec = session.controlrec;
            fields     = request.fields;
            if ((fields.ContainsKey("webusers.webpassword")) && (fields["webusers.webpassword"].value != ""))
            {
                password  = fields["webusers.webpassword"].value;
                if ((fields.ContainsKey("contact.fname")) && (fields.ContainsKey("contact.lname")))
                {
                    firstname = fields["contact.fname"].value;
                    lastname  = fields["contact.lname"].value;
                }
                else
                {
                    uniqueids = request.ids;
                    contactid = FwCryptography.AjaxDecrypt(uniqueids["contact.contactid"].value);
                    name      = RwAppData.GetContactName(contactid);
                    firstname = name.firstname;
                    lastname  = name.lastname;
                }

                passwordcomplexity = FwFunc.CheckPasswordComplexity(controlrec, password);

                if ((Regex.Match(password.ToUpper(), firstname.ToUpper(), RegexOptions.ECMAScript).Success) || (Regex.Match(password.ToUpper(), lastname.ToUpper(), RegexOptions.ECMAScript).Success))
                {
                    isvalid = false;
                }
                if (passwordcomplexity.error == true)
                {
                    isvalid = false;
                }
            }

            return isvalid;
        }
        //---------------------------------------------------------------------------------------------
        private void CheckPasswordComplexity()
        {
            string password, firstname, lastname;
            FwControl controlrec;

            controlrec = session.controlrec;
            password   = request.value;
            firstname  = request.first;
            lastname   = request.last;

            response.passwordcomplexity = FwFunc.CheckPasswordComplexity(controlrec, password);

            if ((Regex.Match(password.ToUpper(), firstname.ToUpper(), RegexOptions.ECMAScript).Success) || (Regex.Match(password.ToUpper(), lastname.ToUpper(), RegexOptions.ECMAScript).Success))
            {
                response.passwordcomplexity.error = true;
            }
            response.passwordcomplexity.errmsg = response.passwordcomplexity.errmsg + "<br/>*Must not contain first or last name.";
        }
        //---------------------------------------------------------------------------------------------
        //private void GetDriverInfo()
        //{
        //    const string METHOD_NAME = "TwContact.GetDriverInfo";
        //    string fname, lname;

        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "fname");
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "lname");
        //    fname = request.fname;
        //    lname = request.lname;
        //    response.getdriverinfo = TwAppData.GetDriverInfo(fname, lname);
        //}
        //---------------------------------------------------------------------------------------------
    }
}
