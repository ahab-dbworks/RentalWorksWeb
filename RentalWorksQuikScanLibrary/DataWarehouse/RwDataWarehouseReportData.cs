using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    public static class RwDataWarehouseReportData
    {
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetDwCustomerRevenueByMonth(FwSqlConnection conn, string fromDate, string toDate, List<string> activityTypes, List<string> customers, 
            List<string> departments, List<string> locations, List<string> deals, List<string> dealTypes, List<string> categories, List<string> iCodes)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("from dbo.dwcustomerrevenuebymonth(@fromDate, @toDate)");
            qry.Add("where 0 = 0");
            if (activityTypes.Count > 0) qry.Add("and ActivityType in (@activityTypes)");
            if (customers.Count     > 0) qry.Add("and Customer     in (@customers)");
            if (departments.Count   > 0) qry.Add("and Customer     in (@departments)");
            if (locations.Count     > 0) qry.Add("and Location     in (@locations)");
            if (deals.Count         > 0) qry.Add("and Deal         in (@deals)");
            if (dealTypes.Count     > 0) qry.Add("and DealType     in (@dealTypes)");
            if (categories.Count    > 0) qry.Add("and Category     in (@categories)");
            if (iCodes.Count        > 0) qry.Add("and ICode        in (@iCodes)");
            qry.Add("order by customer");
            qry.AddParameter("@fromDate",      fromDate);
            qry.AddParameter("@toDate",        toDate);
            qry.AddParameter("@activityTypes", FwConvert.ToSqlParameter(activityTypes));
            qry.AddParameter("@customers",     FwConvert.ToSqlParameter(customers));
            qry.AddParameter("@departments",   FwConvert.ToSqlParameter(departments));
            qry.AddParameter("@locations",     FwConvert.ToSqlParameter(locations));
            qry.AddParameter("@deals",         FwConvert.ToSqlParameter(deals));
            qry.AddParameter("@dealTypes",     FwConvert.ToSqlParameter(dealTypes));
            qry.AddParameter("@categories",    FwConvert.ToSqlParameter(categories));
            qry.AddParameter("@iCodes",        FwConvert.ToSqlParameter(iCodes));
            result = qry.QueryToTable();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}