using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.OrderItem;
using WebApi.Modules.HomeControls.SubPurchaseOrderItem;
using WebApi;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    public static class OfficeLocationFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> SetOfficeLocationDefaultCurrency(FwApplicationConfig appConfig, FwUserSession userSession, string officeLocationId, string currencyId, FwSqlConnection conn = null)
        {
            bool success = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            qry.Add("if not exists (select * from locationcurrency lc where locationid = @locationid and currencyid = @currencyid)");
            qry.Add("begin");
            qry.Add("   insert into locationcurrency (locationid, currencyid) values (@locationid, @currencyid)");
            qry.Add("end");
            qry.Add("update locationcurrency set defaultcurrency = (case when (currencyid = @currencyid) then 'T' else 'F' end) where locationid = @locationid");
            qry.AddParameter("@locationid", officeLocationId);
            qry.AddParameter("@currencyid", currencyId);
            await qry.ExecuteNonQueryAsync();
            success = true;
            return success;
        }
        //-------------------------------------------------------------------------------------------------------

    }
}
