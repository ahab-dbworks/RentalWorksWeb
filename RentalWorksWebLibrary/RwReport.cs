using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksWebLibrary
{
    public class RwReport : FwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override FwSqlConnection GetApplicationSqlConnection()
        {
            return FwSqlConnection.RentalWorks;
        }
        //---------------------------------------------------------------------------------------------
    }
}