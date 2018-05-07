using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace TrakItWorksWeb.Source
{
    public class Report : FwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override FwSqlConnection GetApplicationSqlConnection()
        {
            return FwSqlConnection.RentalWorks;
        }
        //---------------------------------------------------------------------------------------------
    }
}