using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FiscalYear
{
    public static class FiscalFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> DateIsInClosedMonth(FwApplicationConfig appConfig, FwUserSession userSession, DateTime theDate)
        {
            bool inClosedMonth = false;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select inclosedmonth = dbo.dateisinclosedmonth(@thedate) ");
                qry.AddParameter("@thedate", theDate);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                inClosedMonth = FwConvert.ToBoolean(dt.Rows[0][dt.GetColumnNo("inclosedmonth")].ToString());
            }

            return inClosedMonth;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
