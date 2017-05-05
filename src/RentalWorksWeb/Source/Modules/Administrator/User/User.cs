using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    class User : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return form.Tables["users"].GetField("lastname").Value + ", " + form.Tables["users"].GetField("firstname").Value;
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            string usersid, webusersid;

            base.beforeInsert();
            usersid    = FwSqlData.GetNextId(this.form.DatabaseConnection);
            webusersid = FwSqlData.GetNextId(this.form.DatabaseConnection);
            form.Tables["users"].GetField("usersid").UniqueIdentifier       = true;
            form.Tables["users"].GetField("usersid").Value                  = usersid;
            form.Tables["webusers"].GetField("webusersid").UniqueIdentifier = true;
            form.Tables["webusers"].GetField("webusersid").Value            = webusersid;
            form.Tables["webusers"].GetField("usersid").Value               = usersid;
        }
        //---------------------------------------------------------------------------------------------
        //protected override void beforeUpdate()
        //{
        //        base.beforeUpdate();
        //}
        //---------------------------------------------------------------------------------------------
        protected override bool validateForm()
        {
            bool isvalid = true;


            isvalid = base.validateForm();
            isvalid = validatePasswordComplexity();

            return isvalid;
        }
        //---------------------------------------------------------------------------------------------
        private bool validatePasswordComplexity()
        {
            string password, firstname = string.Empty, lastname = string.Empty, usersid;
            bool isvalid = true;
            IDictionary<string, dynamic> fields, uniqueids;
            FwControl controlrec;
            dynamic passwordcomplexity = new ExpandoObject();
            dynamic name;

            controlrec = session.controlrec;
            fields     = request.fields;
            if (fields.ContainsKey("webusers.webpassword"))
            {
                password  = fields["webusers.webpassword"].value;
                if ((fields.ContainsKey("users.firstname")) && (fields.ContainsKey("users.lastname")))
                {
                    firstname = fields["users.firstname"].value;
                    lastname  = fields["users.lastname"].value;
                }
                else
                {
                    uniqueids = request.ids;
                    usersid   = FwCryptography.AjaxDecrypt(uniqueids["users.usersid"].value);
                    name      = GetUsersName(usersid);
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
        public override void Load()
        {
            FwSqlCommand qry;
            string usersid, webusersid;
            
            usersid = this.getUniqueIdFromRequest("users.usersid");
            qry = new FwSqlCommand(this.form.DatabaseConnection);
            qry.Add("select webusersid");
            qry.Add("from webusers with (nolock)");
            qry.Add("where usersid = @usersid");
            qry.AddParameter("@usersid", usersid);
            qry.Execute();
            webusersid = qry.GetField("webusersid").ToString();
            this.setUniqueIdOnRequest("webusers.webusersid", webusersid);
            form.Tables["webusers"].GetUniqueId("webusersid").Value = webusersid;
            base.Load();
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            switch((string)request.method)
            {
                case "CheckPasswordComplexity": CheckPasswordComplexity(); break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["users"].GetUniqueId("usersid").Value;
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
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetUsersName(string usersid)
        {
            FwSqlCommand qry;
            dynamic result;

            result = new ExpandoObject();
            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select firstname, lastname");
            qry.Add("from users with (nolock)");
            qry.Add("where usersid = @usersid");
            qry.AddParameter("@usersid", usersid);
            qry.Execute();
            result.firstname = qry.GetField("firstname").ToString();
            result.lastname  = qry.GetField("lastname").ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
