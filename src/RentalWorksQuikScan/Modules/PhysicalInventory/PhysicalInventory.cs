using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class PhysicalInventory
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetInventoryItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetInventoryItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "phyNo");

            response.item = WebSelectPhyInv(FwSqlConnection.RentalWorks, session.security.webUser.usersid, request.phyNo);
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic WebSelectPhyInv(FwSqlConnection conn, string usersId, string phyNo)
        {
            dynamic result  = new ExpandoObject();
            FwSqlCommand sp = new FwSqlCommand(conn, "dbo.webselectphyinv");
            sp.AddParameter("@physicalno",   phyNo);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@physicalid",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealid",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@description",  SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@phystatus",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@scheduledate", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@counttype",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@rectype",      SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouse",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@department",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();

            result.phyNo         = phyNo;
            result.physicalId    = sp.GetParameter("@physicalid").ToString().TrimEnd();
            result.dealId        = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.description   = sp.GetParameter("@description").ToString().TrimEnd();
            result.phyStatus     = sp.GetParameter("@phystatus").ToString().TrimEnd();
            result.scheduleDate  = sp.GetParameter("@scheduledate").ToString().TrimEnd();
            result.counttype     = sp.GetParameter("@counttype").ToString().TrimEnd();
            result.rectype       = sp.GetParameter("@rectype").ToString().TrimEnd();
            result.warehouse     = sp.GetParameter("@warehouse").ToString().TrimEnd();
            result.department    = sp.GetParameter("@department").ToString().TrimEnd();
            result.status        = sp.GetParameter("@status").ToInt32();
            result.msg           = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}