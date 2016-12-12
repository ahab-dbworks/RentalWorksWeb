using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    public static class RwDataWarehouseInternalData
    {
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetControlRecord(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from ControlDW with (nolock)");
            result = qry.QueryToTable();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}