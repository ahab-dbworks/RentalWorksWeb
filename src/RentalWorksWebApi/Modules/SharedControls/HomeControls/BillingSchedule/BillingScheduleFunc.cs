using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.HomeControls.BillingSchedule
{
    public static class BillingScheduleFunc
    {
        public static async Task<bool> KeepBillingScheduleCacheFresh(FwApplicationConfig appConfig)
        {
            bool success = true;
            Console.WriteLine("Keeping Billing Schedule fresh.");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add(" select dod.orderid                                        ");
                qry.Add(" from dealorderdetail dod                                  ");
                qry.Add(" join dealorder o on(dod.orderid = o.orderid)              ");
                qry.Add(" where recalcbillingschedule <> 'F'                        ");
                qry.Add(" and(o.status not in ('CLOSED', 'SNAPSHOT', 'CANCELLED'))  ");
                qry.Add(" and(o.ordertype = 'O')                                    ");

                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string orderId = row[dt.GetColumnNo("orderid")].ToString();

                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, "getorderbillingscheduleweb", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        try
                        {
                            qry2.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                            qry2.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                            await qry2.ExecuteNonQueryAsync();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            return success;
        }
    }
}
