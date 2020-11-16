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
                qry.Add("select dod.orderid                     ");
                qry.Add(" from  dealorderdetail dod             ");
                qry.Add(" where recalcbillingschedule <> 'F'    ");
                //qry.Add(" and orderid = 'Y000YCOM'    ");
                qry.Add(" option (recompile)                    ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string orderId = row[dt.GetColumnNo("orderid")].ToString();

                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, "getorderbillingscheduleweb", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry2.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                        qry2.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        await qry2.ExecuteNonQueryAsync();
                        //AddPropertiesAsQueryColumns(qry);
                        //dt = await qry.QueryToFwJsonTableAsync(false, 0);
                    }
                    //string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    //string classification = row[dt.GetColumnNo("class")].ToString();
                    //float qty = FwConvert.ToFloat(row[dt.GetColumnNo("qty")].ToString());
                    //bool isPackageOwned = FwConvert.ToBoolean(row[dt.GetColumnNo("ispackageowned")].ToString());
                    //bool isInOwnedPackage = FwConvert.ToBoolean(row[dt.GetColumnNo("isinownedpackage")].ToString());
                    //bool noAvail = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());
                    //bool preCache = (((qty != 0) || isPackageOwned || isInOwnedPackage) && (!noAvail));
                    //RequestRecalc(inventoryId, warehouseId, classification, preCache);
                }
            }
            return success;
        }
    }
}
