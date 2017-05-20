using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using System.Collections.Generic;
using System.Dynamic;

namespace RentalWorksAPI.api.v2.Data
{
    public class AccountData
    {
        //------------------------------------------------------------------------------
        public static dynamic WebGetUsers(string LoginEmail, string LoginPassword)
        {
            FwSqlCommand sp;
            string encryptedwebpassword;
            dynamic response = new ExpandoObject();

            encryptedwebpassword = AppData.Encrypt(LoginPassword.ToUpper());

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webgetusers2");
            sp.AddParameter("@userlogin",         LoginEmail);
            sp.AddParameter("@userloginpassword", encryptedwebpassword);
            sp.AddParameter("@webusersid",        System.Data.SqlDbType.Char,    System.Data.ParameterDirection.Output, 8);
            sp.AddParameter("@errno",             System.Data.SqlDbType.Int,     System.Data.ParameterDirection.Output, 4);
            sp.AddParameter("@errmsg",            System.Data.SqlDbType.VarChar, System.Data.ParameterDirection.Output, 255);
            sp.ExecuteNonQuery();

            response.webusersid = sp.GetParameter("@webusersid").ToString().TrimEnd();
            response.errno      = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg     = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static WebUsers WebUsersView(string webusersid)
        {
            FwSqlCommand qry;
            dynamic qryresult = new ExpandoObject();
            WebUsers response = new WebUsers();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_accountlogin");
            qry.Add(" where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            qryresult = qry.QueryToDynamicObject2();

            response.webusersid              = qryresult.webusersid;
            response.usersid                 = qryresult.usersid;
            response.contactid               = qryresult.contactid;
            response.dealid                  = qryresult.dealid;
            response.name                    = qryresult.name;
            response.username                = qryresult.username;
            response.fullname                = qryresult.fullname;
            response.email                   = qryresult.email;
            response.changepasswordatlogin   = qryresult.changepasswordatlogin;
            response.primarydepartmentid     = qryresult.primarydepartmentid;
            response.rentaldepartmentid      = qryresult.rentaldepartmentid;
            response.salesdepartmentid       = qryresult.salesdepartmentid;
            response.rentalagentusersid      = qryresult.rentalagentusersid;
            response.salesagentusersid       = qryresult.salesagentusersid;
            response.partsdepartmentid       = qryresult.partsdepartmentid;
            response.labordepartmentid       = qryresult.labordepartmentid;
            response.miscdepartmentid        = qryresult.miscdepartmentid;
            response.spacedepartmentid       = qryresult.spacedepartmentid;
            response.titletype               = qryresult.titletype;
            response.title                   = qryresult.title;
            response.department              = qryresult.department;
            response.departmentid            = qryresult.departmentid;
            response.locationid              = qryresult.locationid;
            response.location                = qryresult.location;
            response.warehouseid             = qryresult.warehouseid;
            response.warehouse               = qryresult.warehouse;
            response.office                  = qryresult.office;
            response.phoneextension          = qryresult.phoneextension;
            response.fax                     = qryresult.fax;
            response.usertype                = qryresult.usertype;

            return response;
        }
        //------------------------------------------------------------------------------
        public static List<WebUsers> GetRwUsers(string locationid, string departmentid, string groupsid)
        {
            FwSqlCommand qry;
            List<WebUsers> webusers = new List<WebUsers>();
            dynamic qryresult;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_accountlogin");
            qry.Add(" where locationid = @locationid");
            qry.Add("   and usertype   = 'USER'");
            qry.AddParameter("@locationid", locationid);
            if (!string.IsNullOrEmpty(departmentid))
            {
                qry.Add("   and departmentid = @departmentid");
                qry.AddParameter("@departmentid", departmentid);
            }
            if (!string.IsNullOrEmpty(groupsid))
            {
                qry.Add("   and groupsid = @groupsid");
                qry.AddParameter("@groupsid", groupsid);
            }
            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                WebUsers webuser = new WebUsers();

                webuser.webusersid            = qryresult[i].webusersid;
                webuser.usersid               = qryresult[i].usersid;
                webuser.fullname              = qryresult[i].fullname;
                webuser.locationid            = qryresult[i].locationid;
                webuser.location              = qryresult[i].location;
                webuser.primarydepartmentid   = qryresult[i].departmentid;
                webuser.primarydepartment     = qryresult[i].department;
                webuser.groupsid              = qryresult[i].groupsid;
                webuser.group                 = qryresult[i].groups;

                webusers.Add(webuser);
            }

            return webusers;
        }
        //------------------------------------------------------------------------------
    }
}