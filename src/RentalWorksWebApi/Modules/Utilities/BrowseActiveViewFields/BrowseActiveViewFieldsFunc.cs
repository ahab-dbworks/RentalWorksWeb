using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.BrowseActiveViewFields
{
    public static class BrowseActiveViewFieldsFunc
    {
        //-------------------------------------------------------------------------------------------------------

        public static async Task<bool> DeleteOthers(FwApplicationConfig appConfig, FwUserSession userSession, string ModuleName, string OfficeLocationId, string WebUserId, FwSqlConnection conn = null)
        {
            bool success = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            qry.Add("delete ");
            qry.Add(" from  webbrowseactiveviewfields");
            qry.Add(" where modulename = @modulename");
            qry.Add(" and   locationid = @locationid");
            qry.Add(" and   webusersid = @webusersid");
            qry.AddParameter("@modulename", ModuleName);
            qry.AddParameter("@locationid", OfficeLocationId);
            qry.AddParameter("@webusersid", WebUserId);
            await qry.ExecuteNonQueryAsync();
            success = true;

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}