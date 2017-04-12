using Fw.Json.SqlServer;
using RentalWorksAPI.api.v1.Models;
using System.Data;
using System.Dynamic;

namespace RentalWorksAPI.api.v1.Data
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
            WebUsers response = new WebUsers();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from webusersview");
            qry.Add(" where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            qry.Execute();

            response.webusersid              = qry.GetField("webusersid").ToString().TrimEnd();
            response.usersid                 = qry.GetField("usersid").ToString().TrimEnd();
            response.contactid               = qry.GetField("contactid").ToString().TrimEnd();
            response.dealid                  = qry.GetField("dealid").ToString().TrimEnd();
            response.name                    = qry.GetField("name").ToString().TrimEnd();
            response.username                = qry.GetField("username").ToString().TrimEnd();
            response.fullname                = qry.GetField("fullname").ToString().TrimEnd();
            response.email                   = qry.GetField("email").ToString().TrimEnd();
            response.changepasswordatlogin   = qry.GetField("changepasswordatlogin").ToString().TrimEnd();
            response.primarydepartmentid     = qry.GetField("primarydepartmentid").ToString().TrimEnd();
            response.rentaldepartmentid      = qry.GetField("rentaldepartmentid").ToString().TrimEnd();
            response.salesdepartmentid       = qry.GetField("salesdepartmentid").ToString().TrimEnd();
            response.rentalagentusersid      = qry.GetField("rentalagentusersid").ToString().TrimEnd();
            response.salesagentusersid       = qry.GetField("salesagentusersid").ToString().TrimEnd();
            response.partsdepartmentid       = qry.GetField("partsdepartmentid").ToString().TrimEnd();
            response.labordepartmentid       = qry.GetField("labordepartmentid").ToString().TrimEnd();
            response.miscdepartmentid        = qry.GetField("miscdepartmentid").ToString().TrimEnd();
            response.spacedepartmentid       = qry.GetField("spacedepartmentid").ToString().TrimEnd();
            response.titletype               = qry.GetField("titletype").ToString().TrimEnd();
            response.title                   = qry.GetField("title").ToString().TrimEnd();
            response.department              = qry.GetField("department").ToString().TrimEnd();
            response.departmentid            = qry.GetField("departmentid").ToString().TrimEnd();
            response.locationid              = qry.GetField("locationid").ToString().TrimEnd();
            response.location                = qry.GetField("location").ToString().TrimEnd();
            response.warehouseid             = qry.GetField("warehouseid").ToString().TrimEnd();
            response.warehouse               = qry.GetField("warehouse").ToString().TrimEnd();
            response.office                  = qry.GetField("office").ToString().TrimEnd();
            response.phoneextension          = qry.GetField("phoneextension").ToString().TrimEnd();
            response.fax                     = qry.GetField("fax").ToString().TrimEnd();
            response.usertype                = qry.GetField("usertype").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static Error WebUsersSetPassword(string WebUsersId, string NewPassword)
        {
            FwSqlCommand sp;
            Error response = new Error();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webuserssetpassword");
            sp.AddParameter("@webusersid",       WebUsersId);
            sp.AddParameter("@webpassword",      NewPassword.ToUpper());
            sp.AddParameter("@cleartmppassword", "T");
            sp.AddParameter("@encryptpassword",  "T");
            sp.AddParameter("@errno",            SqlDbType.Int,      ParameterDirection.Output, 00);
            sp.AddParameter("@errmsg",           SqlDbType.NVarChar, ParameterDirection.Output, 255);
            sp.ExecuteNonQuery();

            response.errno  = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
        public static Error WebUsersResetPassword(string Email)
        {
            FwSqlCommand sp;
            Error response = new Error();

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "webuserresetpassword");
            sp.AddParameter("@usersemail",       Email);
            sp.AddParameter("@errno",            SqlDbType.Int,      ParameterDirection.Output, 00);
            sp.AddParameter("@errmsg",           SqlDbType.NVarChar, ParameterDirection.Output, 255);
            sp.ExecuteNonQuery();

            response.errno  = sp.GetParameter("@errno").ToString().TrimEnd();
            response.errmsg = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return response;
        }
        //------------------------------------------------------------------------------
    }
}