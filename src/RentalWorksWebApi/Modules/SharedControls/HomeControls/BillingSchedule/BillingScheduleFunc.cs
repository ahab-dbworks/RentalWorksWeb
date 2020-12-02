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
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> KeepBillingScheduleCacheFresh(FwApplicationConfig appConfig)
        {
            bool success = true;
            Console.WriteLine("About to check for Orders that need Billing Schedule recalculated");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select orderid                               ");
                qry.Add(" from  orderneedrecalcbillingscheduleview    ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                Console.WriteLine($"Found {dt.TotalRows.ToString()} Orders that need Billing Schedule recalculated");
                int x = 0;
                foreach (List<object> row in dt.Rows)
                {
                    x++;
                    Console.WriteLine($"About to recalculate Billing Schedule for {x.ToString()} of {dt.TotalRows.ToString()}");

                    string orderId = row[dt.GetColumnNo("orderid")].ToString();

                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, "getorderbillingscheduleweb", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        try
                        {
                            qry2.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                            qry2.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                            await qry2.ExecuteNonQueryAsync();
                        }
                        catch (Exception)
                        {
                            // do nothing here.  Just skip this Order and go to the next one in the list
                        }
                    }
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
