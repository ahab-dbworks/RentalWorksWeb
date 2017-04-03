using Fw.Json.SqlServer;
using System;

namespace RentalWorksAPI
{
    public class AppData
    {
        //-----------------------------------------------------------------------------
        static public String Encrypt(string data)
        {
            string value;
            FwSqlCommand qry;

            value = string.Empty;
            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select value = dbo.encrypt(@data)");
            qry.AddParameter("@data", data);
            qry.Execute();
            //ag 02/28/2011 - turned off forced upper case
            value = qry.GetField("value").ToString().Trim(); //.ToUpper();
            
            return value;
        }
        //----------------------------------------------------------------------------------------------------
        static public void LogWebApiAudit(string uniqueid1, string apimethod, string jsontext, string jsonresponse, string errormessage, int duration)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "logwebapiaudit");
            sp.AddParameter("@uniqueid1", uniqueid1);
            sp.AddParameter("@apimethod", apimethod);
            sp.AddParameter("@jsontext",  jsontext);
            sp.AddParameter("@jsonresponse", jsonresponse);
            sp.AddParameter("@errormessage", errormessage);
            sp.AddParameter("@duration", duration);
            sp.Execute();
        }
        //----------------------------------------------------------------------------------------------------
    }
}